//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Net;
using System.Net.Mail;
using Oxite.Configuration;
using Oxite.Configuration.Extensions;
using Oxite.Infrastructure;
using Oxite.Modules.Messages.Models;
using Oxite.Modules.Messages.Services;

namespace Oxite.Modules.Messages.BackgroundServices
{
    public class SendMessages : IBackgroundService
    {
        private readonly IMessageOutboundService messageService;

        public SendMessages(IMessageOutboundService messageService)
        {
            this.messageService = messageService;
        }

        #region Methods

        public void Initialize(OxiteModuleConfigurationElement moduleConfiguration)
        {
        }

        public void Unload(OxiteModuleConfigurationElement moduleConfiguration)
        {
        }

        public void Run(OxiteModuleConfigurationElement moduleConfiguration)
        {
            AppSettingsHelper settings = new AppSettingsHelper(moduleConfiguration.Settings.ToNameValueCollection());
            string fromEmailAddress = settings.GetString("SendMessages.FromEmailAddress");
            SmtpClient smtpClient = getSmtpClient(moduleConfiguration);
            //TODO: (erikpo) Refactor GetNext call to not need an interval if possible
            TimeSpan interval = settings.GetTimeSpan("SendMessages.Interval", TimeSpan.FromMinutes(1));
            int blockSize = settings.GetInt32("SendMessages.BlockSize", 1);

            foreach (MessageOutbound message in messageService.GetNext(interval, blockSize))
            {
                try
                {
                    MailMessage messageToSend = new MailMessage(fromEmailAddress, message.To, message.Subject, message.Body);

                    messageToSend.IsBodyHtml = true;

                    smtpClient.Send(messageToSend);

                    message.MarkAsCompleted();
                }
                catch
                {
                    message.MarkAsFailed();
                }
                finally
                {
                    messageService.Save(message);
                }
            }
        }

        #endregion

        #region Private Methods

        private SmtpClient getSmtpClient(OxiteModuleConfigurationElement moduleConfiguration)
        {
            AppSettingsHelper settings = new AppSettingsHelper(moduleConfiguration.Settings.ToNameValueCollection());
            SmtpClient client = new SmtpClient();

            client.Host = settings.GetString("SmtpClient.Host");
            client.Port = settings.GetInt32("SmtpClient.Port", client.Port);
            client.UseDefaultCredentials = settings.GetBoolean("SmtpClient.UseDefaultCredentials", client.UseDefaultCredentials);

            string username = settings.GetString("SmtpClient.Credentials.Username", "");
            string password = settings.GetString("SmtpClient.Credentials.Password", "");
            string domain = settings.GetString("SmtpClient.Credentials.Domain", "");

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                client.Credentials = !string.IsNullOrEmpty(domain)
                    ? new NetworkCredential(username, password, domain)
                    : new NetworkCredential(username, password);
            }

            return client;
        }

        #endregion
    }
}
