//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Microsoft.Practices.Unity;

namespace Oxite.Infrastructure
{
    public class FactoryMethodLifetimeManager : LifetimeManager
    {
        private readonly Func<object> getValue;

        public FactoryMethodLifetimeManager(Func<object> getValue)
        {
            this.getValue = getValue;
        }

        public override object GetValue()
        {
            return getValue();
        }

        public override void RemoveValue()
        {
        }

        public override void SetValue(object newValue)
        {
        }
    }
}
