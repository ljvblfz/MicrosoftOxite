//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Security.Cryptography;
using System.Text;

namespace Oxite.Modules.Core.Extensions
{
    public static class StringExtensions
    {
        public static string SaltAndHash(this string rawString, string salt)
        {
            byte[] salted = Encoding.UTF8.GetBytes(string.Concat(rawString, salt));
            SHA256 hasher = new SHA256Managed();
            byte[] hashed = hasher.ComputeHash(salted);

            return Convert.ToBase64String(hashed);
        }
    }
}
