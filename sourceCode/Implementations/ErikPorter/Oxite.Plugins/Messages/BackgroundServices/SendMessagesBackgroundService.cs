//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Mail;
using Oxite.Infrastructure;
using Oxite.Plugins.Messages.Models;
using Oxite.Plugins.Messages.Services;

namespace Oxite.Plugins.Messages.BackgroundServices
{
    public class SendMessagesBackgroundService : IBackgroundService
    {
        private IMessageOutboundService messageService;

        public SendMessagesBackgroundService(IMessageOutboundService messageService)
        {
            this.messageService = messageService;
        }

        public void Run(NameValueCollection settings)
        {
            bool executeOnAll = bool.Parse(settings["ExecuteOnAll"]);
            TimeSpan interval = TimeSpan.FromTicks(long.Parse(settings["Interval"]));
            string fromEmailAddress = settings["FromEmailAddress"];
            SmtpClient smtpClient = getSmtpClient(settings);

            foreach (MessageOutbound message in messageService.GetNext(executeOnAll, interval))
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

        private SmtpClient getSmtpClient(NameValueCollection settings)
        {
            SmtpClient client = new SmtpClient();

            client.Host = settings["SmtpClient.Host"];

            string portValue = settings["SmtpClient.Port"];
            if (portValue != "")
                client.Port = int.Parse(portValue);

            string useDefaultCredentialsValue = settings["SmtpClient.UseDefaultCredentials"];
            if (useDefaultCredentialsValue != "")
                client.UseDefaultCredentials = bool.Parse(useDefaultCredentialsValue);

            string enableSslValue = settings["SmtpClient.EnableSsl"];
            if (!string.IsNullOrEmpty(enableSslValue))
            {
                bool enableSsl;

                if (bool.TryParse(enableSslValue, out enableSsl))
                    client.EnableSsl = enableSsl;
            }

            string username = settings["SmtpClient.Credentials.Username"];
            string password = settings["SmtpClient.Credentials.Password"];
            string domain = settings["SmtpClient.Credentials.Domain"];

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                client.Credentials = !string.IsNullOrEmpty(domain)
                    ? new NetworkCredential(username, password, domain)
                    : new NetworkCredential(username, password);
            }

            return client;
        }
    }
}
