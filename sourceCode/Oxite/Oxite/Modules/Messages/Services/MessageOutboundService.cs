//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Modules.Messages.Models;
using Oxite.Modules.Messages.Repositories;

namespace Oxite.Modules.Messages.Services
{
    public class MessageOutboundService : IMessageOutboundService
    {
        private readonly IMessageOutboundRepository repository;

        public MessageOutboundService(IMessageOutboundRepository repository)
        {
            this.repository = repository;
        }

        #region IMessageService Members

        public IEnumerable<MessageOutbound> GetNext(TimeSpan interval, int blockSize)
        {
            return repository.GetNext(interval, blockSize);
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
