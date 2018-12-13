//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Repositories;

namespace Oxite.Services
{
    public class MessageOutboundService : IMessageOutboundService
    {
        private readonly IMessageOutboundRepository repository;

        public MessageOutboundService(IMessageOutboundRepository repository)
        {
            this.repository = repository;
        }

        #region IMessageService Members

        public IEnumerable<MessageOutbound> GetNext(bool executeOnAll, TimeSpan interval)
        {
            return repository.GetNext(executeOnAll, interval);
        }

        public void Save(MessageOutbound message)
        {
            repository.Save(message);
        }

        public void Save(IEnumerable<MessageOutbound> messages)
        {
            repository.Save(messages);
        }

        #endregion
    }
}
