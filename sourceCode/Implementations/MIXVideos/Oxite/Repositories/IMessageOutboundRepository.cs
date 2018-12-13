//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Models;

namespace Oxite.Repositories
{
    public interface IMessageOutboundRepository
    {
        IEnumerable<MessageOutbound> GetNext(bool executeOnAll, TimeSpan interval);
        void Save(MessageOutbound message);
        void Save(IEnumerable<MessageOutbound> messages);
    }
}
