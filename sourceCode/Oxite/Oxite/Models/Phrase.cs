//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Models
{
    public class Phrase
    {
        public Phrase(string key, string value, string language)
        {
            Key = key;
            Value = value;
            Language = language;
        }

        public string Key { get; private set; }
        public string Value { get; private set; }
        public string Language { get; private set; }
    }
}
