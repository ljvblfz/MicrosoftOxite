//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
namespace Oxite.Models
{
    public class ContactInput
    {
        public ContactInput(string message, string email, string subject)
        {
            Message = message;
            Email = email;
            Subject = subject;
        }

        public string Message { get; private set; }
        public string Email { get; private set; }
        public string Subject { get; private set; }
    }
}
