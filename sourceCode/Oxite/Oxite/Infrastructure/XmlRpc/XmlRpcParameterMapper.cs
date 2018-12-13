//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Mvc;

namespace Oxite.Infrastructure.XmlRpc
{
    public static class XmlRpcParameterMapper
    {
        public static IDictionary<string, object> Map(ParameterDescriptor[] methodParameters, IList<XmlRpcParameter> rpcParameters)
        {
            Dictionary<string, object> mappedValues = new Dictionary<string, object>();

            for (int i = 0; i < rpcParameters.Count; i++)
                mappedValues.Add(methodParameters[i].ParameterName, rpcParameters[i].AsType(methodParameters[i].ParameterType));

            return mappedValues;
        }
    }
}
