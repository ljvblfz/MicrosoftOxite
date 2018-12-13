//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Runtime.Serialization;

namespace Oxite.Infrastructure
{
    [DataContract]
    public class UserCookieProxy
    {
        public UserCookieProxy()
        {
        }

        public UserCookieProxy(UserAnonymous user)
        {
            Name = user.Name;
            Email = user.Email;
            EmailHash = user.EmailHash;
            Url = user.Url;
        }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string EmailHash { get; set; }
        [DataMember]
        public string Url { get; set; }

        public UserAnonymous ToUserAnonymous()
        {
            return new UserAnonymous(Name, Email, EmailHash, Url);
        }
    }
}