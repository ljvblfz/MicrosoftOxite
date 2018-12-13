//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Infrastructure;

namespace Oxite.Tests.Fakes
{
    public class FakeFormsAuthentication : IFormsAuthentication
    {
        public string LastUserName { get; set; }
        public bool LastPersistCookie { get; set; }
        public bool SignedOut { get; set; }

        #region IFormsAuthentication Members

        public void SetAuthCookie(string userName, bool createPersistentCookie)
        {
            this.LastUserName = userName;
            this.LastPersistCookie = createPersistentCookie;
        }

        public void SignOut()
        {
            this.SignedOut = true;
        }

        #endregion
    }
}
