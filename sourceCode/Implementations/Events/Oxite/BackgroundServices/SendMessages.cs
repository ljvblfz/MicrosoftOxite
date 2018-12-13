//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Net;
using System.Net.Mail;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Services;

namespace Oxite.BackgroundServices
{
    //public class SendMessages : BackgroundServiceBase
    //{
    //    private readonly IMessageOutboundService messageService;

    //    public SendMessages(OxiteModuleConfigurationElement config, IMessageOutboundService messageService)
    //        : base(config)
    //    {
    //        this.messageService = messageService;

    //        load();
    //    }

    //    #region Methods

    //    public override void Start()
    //    {
    //    }

    //    public override void Stop()
    //    {
    //    }

    //    public override void Run()
    //    {
    //        if (string.IsNullOrEmpty(Settings["FromEmailAddress"])) throw new ArgumentException("FromEmailAddress must be set");

    //        SmtpClient smtpClient = getSmtpClient();

    //        foreach (MessageOutbound message in messageService.GetNext(ExecuteOnAll, Interval))
    //        {
    //            try
    //            {
    //                MailMessage messageToSend = new MailMessage(Settings["FromEmailAddress"], message.To, message.Subject, message.Body);

    //                messageToSend.IsBodyHtml = true;

    //                smtpClient.Send(messageToSend);

    //                message.MarkAsCompleted();
    //            }
    //            catch
    //            {
    //                message.MarkAsFailed();
    //            }
    //            finally
    //            {
    //                messageService.Save(message);
    //            }
    //        }
    //    }

    //    private SmtpClient getSmtpClient()
    //    {
    //        AppSettingsHelper settings = new AppSettingsHelper(Settings);
    //        SmtpClient client = new SmtpClient();

    //        client.Host = settings.GetString("SmtpClient.Host");
    //        client.Port = settings.GetInt32("SmtpClient.Port", client.Port);
    //        client.UseDefaultCredentials = settings.GetBoolean("SmtpClient.UseDefaultCredentials", client.UseDefaultCredentials);

    //        string username = settings.GetString("SmtpClient.Credentials.Username", "");
    //        string password = settings.GetString("SmtpClient.Credentials.Password", "");
    //        string domain = settings.GetString("SmtpClient.Credentials.Domain", "");

    //        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
    //        {
    //            client.Credentials = !string.IsNullOrEmpty(domain)
    //                ? new NetworkCredential(username, password, domain)
    //                : new NetworkCredential(username, password);
    //        }

    //        return client;
    //    }

    //    #endregion

    //    public override object Clone()
    //    {
    //        return new SendMessages(config, messageService);
    //    }

    //    private void load()
    //    {
    //        TimeSpan? interval = null;
    //        string intervalString = Settings["Interval"];
    //        if (!string.IsNullOrEmpty(intervalString))
    //        {
    //            TimeSpan intervalValue;

    //            if (TimeSpan.TryParse(intervalString, out intervalValue))
    //                interval = intervalValue;
    //        }
    //        Interval = interval.GetValueOrDefault(TimeSpan.FromMinutes(2));

    //        //RetryInterval = 12 hours
    //        //RetryCount = 4
    //    }
    //}
}
