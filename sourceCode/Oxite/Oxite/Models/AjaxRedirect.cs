//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Runtime.Serialization;

namespace Oxite.Models
{
    [DataContract]
    public class AjaxRedirect
    {
        public AjaxRedirect(string url)
        {
            this.url = url;
        }

        [DataMember]
        public string url { get; private set; }
    }
}
