//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Plugins.Messages.Models;

namespace Oxite.Plugins.Messages.Repositories
{
    public interface IMessageOutboundRepository
    {
        IEnumerable<MessageOutbound> GetNext(bool executeOnAll, TimeSpan interval);
        void Save(MessageOutbound message);
        void Save(IEnumerable<MessageOutbound> messages);
    }
}
