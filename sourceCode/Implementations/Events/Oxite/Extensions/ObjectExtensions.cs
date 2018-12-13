//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Oxite.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object o)
        {
            string serializedObject = string.Empty;

            if (o != null)
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(o.GetType());

                using (MemoryStream ms = new MemoryStream())
                {
                    jsonSerializer.WriteObject(ms, o);
                    ms.Position = 0;
                    serializedObject = new StreamReader(ms).ReadToEnd();
                    ms.Close();
                }
            }

            return serializedObject;
        }

        public static object FromJson(this Type type, string serializedObject)
        {
            object filledObject = null;

            if (!string.IsNullOrEmpty(serializedObject))
            {
                DataContractJsonSerializer dcjs = new DataContractJsonSerializer(type);

                using (MemoryStream ms = new MemoryStream())
                {
                    StreamWriter writer = new StreamWriter(ms, Encoding.Default);
                    writer.Write(serializedObject);
                    writer.Flush();

                    ms.Position = 0;

                    try
                    {
                        filledObject = dcjs.ReadObject(ms);
                    }
                    catch (SerializationException)
                    {
                        filledObject = null;
                    }
                    ms.Close();
                }
            }

            return filledObject;
        }
    }
}
