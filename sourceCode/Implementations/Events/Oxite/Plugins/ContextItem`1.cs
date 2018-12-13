//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Infrastructure;
using Oxite.Services;

namespace Oxite.Plugins
{
    public class ContextItem<T> : OxiteContext
    {
        public ContextItem(OxiteContext context, T item)
            : base(context)
        {
            Item = item;
        }

        public T Item { get; private set; }
    }
}
