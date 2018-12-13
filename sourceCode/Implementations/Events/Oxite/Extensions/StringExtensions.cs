//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Security.Application;
using Oxite.Validation;

namespace Oxite.Extensions
{
    public static class StringExtensions
    {
        public static string CleanHtmlTags(this string s)
        {
            return s.CleanHtmlTags(null);
        }

        private static readonly Regex tagRegex = new Regex("<[^<>]*>", RegexOptions.Compiled | RegexOptions.Singleline);
        public static string CleanHtmlTags(this string s, string exceptionPattern)
        {
            if (!string.IsNullOrEmpty(exceptionPattern))
                return
                    new Regex(string.Format("<(?!{0})[^<>]*>", exceptionPattern),
                              RegexOptions.Compiled | RegexOptions.Singleline).Replace(s, "");

            return tagRegex.Replace(s, "");
        }

        private static readonly Regex spaceRegex = new Regex(@"\s+", RegexOptions.Compiled | RegexOptions.Singleline);
        public static string CleanWhitespace(this string s)
        {
            return spaceRegex.Replace(s, " ");
        }
        public static string IsRequired(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new ValidationException(string.Format("String is required: {0}", s));
            }

            return s;
        }

        private static readonly Regex nonWordCharsRegex = new Regex(@"[^\w]+", RegexOptions.Compiled | RegexOptions.Singleline);
        public static string CleanCssClassName(this string s)
        {
            return nonWordCharsRegex.Replace(s, "_").ToLower(System.Globalization.CultureInfo.CurrentCulture);
        }

        public static string CleanText(this string s)
        {
            if (s == null) return null;

            return AntiXss.HtmlEncode(s);
        }

        public static string CleanHtml(this string s)
        {
            //AntiXss library from Microsoft 
            //(http://antixss.codeplex.com)
            string encodedText = AntiXss.HtmlEncode(s);
            //convert line breaks into an html break tag
            return encodedText.Replace("&#13;&#10;", "<br />");
        }

        public static string CleanForQueryString(this string s)
        {
            return AntiXss.UrlEncode(s);
        }

        public static string CleanAttribute(this string s)
        {
            return AntiXss.HtmlAttributeEncode(s);
        }

        //todo: (nheskew) rename to something more generic (CleanAttributeALittle?) because not everything needs
        // the cleaning power of CleanAttribute (everything should but AntiXss.HtmlAttributeEncode encodes 
        // *everyting* incl. white space :|) so attributes can get really long...but then my only current worry is around
        // the description meta tag. Attributes from untrusted sources *do* need the current CleanAttribute...
        public static string CleanHref(this string s)
        {
            return HttpUtility.HtmlAttributeEncode(s);
        }

        public static string CleanCommentBody(this string s)
        {
            return s.CleanHtmlTags().CleanHtml().AutoAnchor();
        }

        public static string CleanSlug(this string s)
        {
            var slug = s.Replace("#", "Sharp").Replace("++", "PlusPlus");

            // note: borrowed from erikpo's implementation
            slug = Regex.Replace(slug, "([^a-z0-9]?)", "",
                RegexOptions.IgnoreCase |
                RegexOptions.Compiled);

            return slug;
        }

        private static readonly Regex uriRegex = new Regex("(^|[^\\w'\"]|\\G)(?<uri>(?:https?|ftp)(?:&#58;|:)(?:&#47;&#47;|//)(?:[^./\\s'\"<)\\]]+\\.)+[^./\\s'\"<)\\]]+(?:(?:&#47;|/).*?)?)(?:[\\s\\.,\\)\\]'\"]?(?:\\s|\\.|\\)|\\]|,|<|$))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static string AutoAnchor(this string s)
        {
            MatchCollection uriMatches = uriRegex.Matches(s);

            foreach (Match uriMatch in uriMatches)
            {
                string encodedUri = uriMatch.Groups["uri"].Value;

                if (!string.IsNullOrEmpty(encodedUri))
                {
                    string uri = HttpUtility.HtmlDecode(encodedUri);
                    s = s.Replace(encodedUri, string.Format("<a href=\"{0}\">{1}</a>", uri.CleanHref(), uri.CleanText()));
                }
            }

            return s;
        }

        public static string WidowControl(this string s)
        {
            const string nbsp = "&nbsp;";
            string output = s.TrimEnd();
            int lastSpace = output.LastIndexOf(' ');
            if (lastSpace > 0)
                output = output.Substring(0, lastSpace) +
                         nbsp + output.Substring(lastSpace + 1);

            return output;

        }

        public static string Shorten(this string s, int characterCount)
        {
            string text = !string.IsNullOrEmpty(s) ? s.CleanHtmlTags().CleanWhitespace() : "";

            if (!string.IsNullOrEmpty(text) && characterCount > 0 && text.Length > characterCount)
            {
                text = text.Substring(0, characterCount);
            }

            return text;
        }

        public static string Ellipsize(this string s, int characterCount, Func<string, string> processStringPart)
        {
            return s.Ellipsize(characterCount, processStringPart, "&#160;&#8230;");
        }

        public static string Ellipsize(this string s, int characterCount, Func<string, string> processStringPart, string ellipsis)
        {
            ++characterCount;
            string text = !string.IsNullOrEmpty(s) ? s.CleanHtmlTags().CleanWhitespace() : "";

            if (string.IsNullOrEmpty(text) || characterCount < 1 || text.Length <= characterCount)
                return text;

            string[] words = text.Substring(0, characterCount).Split(' ');

            return processStringPart(string.Join(" ", words.Take(words.Length - 1).ToArray())) + ellipsis;
        }

        public static string EllipsizeUri(this string s, int characterCount, Func<string, string> processStringPart)
        {
            return s.EllipsizeUri(characterCount, processStringPart, "&#160;&#8230;&#160;");
        }

        //info: (nheskew) ellipsis length hard-coded to the default decoded
        public static string EllipsizeUri(this string s, int characterCount, Func<string, string> processStringPart, string ellipsis)
        {
            Uri uri;
            int ellipsisLength = 3; // not really accurate considering the use of the hellip character

            // return because we're not going to mess with the "URI" string
            if (string.IsNullOrEmpty(s) ||
                characterCount < 1 ||
                s.Length <= characterCount ||
                !Uri.TryCreate(s, UriKind.Absolute, out uri))
                return processStringPart(s);

            string start = uri.Scheme + "://";
            string end = uri.Segments.LastOrDefault() ?? "";

            if (!string.IsNullOrEmpty(uri.Query))
                end = end + "?" + ellipsis;

            // need to ellipsize the host name because the string is already getting too long
            if (start.Length + uri.Host.Length + ellipsisLength + end.Length > characterCount)
            {
                string host = uri.Host;
                int endLength = characterCount - (start.Length + host.Length + ellipsisLength);

                if (endLength < 0)
                {
                    int hostSubLength = (characterCount - (start.Length + ellipsisLength * 2)) / 2; // two ellilpsis. host and end

                    host = hostSubLength > 0
                        ? processStringPart(host.Substring(0, hostSubLength)) +
                            ellipsis +
                            processStringPart(host.Substring(host.Length - hostSubLength, hostSubLength))
                        : "";
                    
                    endLength = 0;
                }
                else
                {
                    host = processStringPart(host);
                }

                return processStringPart(start) +
                       host +
                       ellipsis +
                       (endLength > 0 ? processStringPart(end.Substring(end.Length - endLength, endLength)) : "");
            }

            start = start + uri.Host;

            // add as many path segments as we can
            var pathParts = uri.Segments.Take(uri.Segments.Length - 1);
            foreach (string pathPart in pathParts)
            {
                if (start.Length + pathPart.Length + ellipsisLength + end.Length > characterCount)
                    return processStringPart(start) + ellipsis + processStringPart(end);

                start = start + pathPart;
            }

            return processStringPart(start + end);
        }

        public static string ComputeEmailHash(this string email)
        {
            return !string.IsNullOrEmpty(email)
                ? email.Trim().ToLower().ComputeHash()
                : email;
        }

        public static string ComputeHash(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] data = Encoding.ASCII.GetBytes(value);
                string hash = "";

                data = md5.ComputeHash(data);
                
                for (int i = 0; i < data.Length; i++)
                    hash += data[i].ToString("x2");

                return hash;
            }

            return value;
        }

        public static bool GuidTryParse(this string s, out Guid result)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            try
            {
                result = new Guid(s);
                return true;
            }
            catch (FormatException)
            {
                result = Guid.Empty;
                return false;
            }
            catch (OverflowException)
            {
                result = Guid.Empty;
                return false;
            }
        }

        public static string GetFileText(this string virtualPath)
        {
            return virtualPath.GetFileText(new HttpContextWrapper(HttpContext.Current));
        }

        public static string GetFileText(this string virtualPath, HttpContextBase httpContext)
        {
            string path = httpContext.Server.MapPath(virtualPath);

            if (File.Exists(path))
                return File.ReadAllText(path);

            return null;
        }

        public static string GetFileName(this string virtualPath)
        {
            return Path.GetFileNameWithoutExtension(virtualPath);
        }

        public static void SaveFileText(this string virtualPath, string code)
        {
            virtualPath.SaveFileText(code, new HttpContextWrapper(HttpContext.Current));
        }

        public static void SaveFileText(this string virtualPath, string code, HttpContextBase httpContext)
        {
            string path = httpContext.Server.MapPath(virtualPath);

            if (path.IsFileWritable())
                File.WriteAllText(path, code);
        }

        public static bool IsFileWritable(this string filePath)
        {
            //TODO: (nheskew)still doesn't catch if write is explicitly denied
            return !string.IsNullOrEmpty(filePath)
                && File.Exists(filePath)
                && (File.GetAttributes(filePath) & FileAttributes.ReadOnly) != FileAttributes.ReadOnly
                && SecurityManager.IsGranted(new FileIOPermission(FileIOPermissionAccess.Write, filePath));
        }

        public static bool IsFileWritable(this string virtualPath, HttpContextBase httpContext)
        {
            return IsFileWritable(httpContext.Server.MapPath(virtualPath));
        }

        public static DateTime? FileModifiedDate(this string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                return File.GetLastWriteTime(filePath);

            return null;
        }

        public static DateTime? FileModifiedDate(this string virtualPath, HttpContextBase httpContext)
        {
            return httpContext.Server.MapPath(virtualPath).FileModifiedDate();
        }
    }
}
