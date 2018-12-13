// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Oxite.Plugins.Extensions
{
    public static class PluginAssemblyContainerExtensions
    {
        public static bool IsCodeFileValid(this PluginAssemblyContainer pluginAssemblyContainer)
        {
            string assemblyFileText =
                File.ReadAllText(HttpContext.Current.Server.MapPath(pluginAssemblyContainer.VirtualPath));

            Regex cSharpConstructorRegex = new Regex(
                @"^(.*?class\s+(\w+plugin).*?\n)([^\n]*public\s+\2\s*\([^\)]*\))",
                RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);

            if (cSharpConstructorRegex.IsMatch(assemblyFileText))
            {
                Match match = cSharpConstructorRegex.Match(assemblyFileText);

                throw new PluginLoadException(
                    string.Format(
                        "Constructors aren't allowed in the plugin class, implement an Initialize method instead. line: {0}; signature: {1}",
                        match.Groups[1].Value.Split('\n').Length,
                        match.Groups[3].Value
                    )
                );
            }

            return true;
        }
    }
}
