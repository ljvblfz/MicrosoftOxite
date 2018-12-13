//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Net;
using System.Net.Mail;
using Oxite.Models;
using Oxite.Services;

namespace Oxite.BackgroundServices
{
    public class SendMessages : BackgroundServiceBase
    {
        private readonly IMessageOutboundService messageService;

        public SendMessages(IBackgroundServiceService backgroundServiceService, IMessageOutboundService messageService)
            : base(backgroundServiceService)
        {
            this.messageService = messageService;

            ID = new Guid("{A0EE9809-375F-407c-B797-DBFD352D914F}");
            Name = "Oxite Message Sender";
            Category = "Background Services";
        }

        #region Methods

        public override void Run()
        {
            if (string.IsNullOrEmpty(Settings["FromEmailAddress"])) throw new ArgumentException("FromEmailAddress must be set");

            SmtpClient smtpClient = getSmtpClient();

            foreach (MessageOutbound message in messageService.GetNext(ExecuteOnAll, Interval))
            {
                try
                {
                    //creating explicit MailMessage object to allow flagging body of email as HTML
                    MailMessage messageToSend = new MailMessage(Settings["FromEmailAddress"], message.To, message.Subject, message.Body);

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

        protected override void OnInitializeSettings()
        {
            base.OnInitializeSettings();

            Settings["ExecuteOnAll"] = true.ToString();
            Settings["Interval"] = TimeSpan.FromMinutes(2).Ticks.ToString();
            Settings["RetryInterval"] = TimeSpan.FromHours(12).Ticks.ToString();
            Settings["RetryCount"] = 4.ToString();
            Settings["FromEmailAddress"] = "";
            Settings["SmtpClient.Host"] = "";
            Settings["SmtpClient.Port"] = "";
            Settings["SmtpClient.UseDefaultCredentials"] = "";
            Settings["SmtpClient.Credentials.Username"] = "";
            Settings["SmtpClient.Credentials.Password"] = "";
            Settings["SmtpClient.Credentials.Domain"] = "";
        }

        private SmtpClient getSmtpClient()
        {
            SmtpClient client = new SmtpClient();

            client.Host = GetSetting("SmtpClient.Host");

            if (!string.IsNullOrEmpty(GetSetting("SmtpClient.Port")))
            {
                client.Port = int.Parse(GetSetting("SmtpClient.Port"));
            }

            if (!string.IsNullOrEmpty(GetSetting("SmtpClient.UseDefaultCredentials")))
            {
                client.UseDefaultCredentials = bool.Parse(GetSetting("SmtpClient.UseDefaultCredentials"));
            }

            string username = GetSetting("SmtpClient.Credentials.Username");
            string password = GetSetting("SmtpClient.Credentials.Password");
            string domain = GetSetting("SmtpClient.Credentials.Domain");

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
