//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Infrastructure;
using Oxite.Services;

namespace Oxite.Plugins
{
    public class ContextItemEdit<T> : ContextItem<T>
    {
        public ContextItemEdit(OxiteContext context, T item, T originalItem)
            : base(context, item)
        {
            OriginalItem = originalItem;
        }

        public T OriginalItem { get; private set; }
    }
}
