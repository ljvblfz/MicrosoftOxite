//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Membership.Extensions;
using Oxite.Modules.Membership.ViewModels;
using Oxite.ViewModels;

namespace Oxite.Extensions
{
    public static class HtmlHelperExtensions
    {
        #region OxiteAntiForgeryToken

        public static string OxiteAntiForgeryToken<TModel>(this HtmlHelper<TModel> htmlHelper) where TModel : class
        { 
            return OxiteAntiForgeryToken(htmlHelper, htmlHelper.ViewData.Model as OxiteViewModel);
        }

        internal static string OxiteAntiForgeryToken(this HtmlHelper htmlHelper, OxiteViewModel model)
        {
            if (model != null)
                return htmlHelper.AntiForgeryToken(model.Site.ID.ToString());

            return "";
        }

        #endregion

        #region BodyClass

        public static string BodyClass<TModel>(this HtmlHelper<TModel> htmlHelper, params string[] items) where TModel : OxiteViewModel
        {
            string controllerName = htmlHelper.ViewContext.RouteData.Values["controller"] as string ?? "";

            StringBuilder sb = new StringBuilder(50);

            sb.Append(controllerName);

            if (items == null || items.Length == 0)
                return sb.ToString().ToLower().CleanAttribute();

            List<string> itemList = new List<string>(items);
            itemList.RemoveAll(s => s == null);

            for (int i = itemList.Count - 1; i >= 0; i--)
                sb.AppendFormat(" {0}", string.Join(" ", itemList[i].Split('/')));

            return sb.ToString().ToLower().CleanAttribute();
        }

        #endregion

        #region CheckBox

        public static string CheckBox<TModel>(this HtmlHelper<TModel> htmlHelper, string name, Func<TModel, bool> getIsChecked, string labelInnerHtml) where TModel : OxiteViewModel
        {
            return CheckBox(htmlHelper, name, getIsChecked, labelInnerHtml, true);
        }

        public static string CheckBox<TModel>(this HtmlHelper<TModel> htmlHelper, string name, Func<TModel, bool> getIsChecked, string labelInnerHtml, bool enabled) where TModel : OxiteViewModel
        {
            return CheckBox(htmlHelper, name, getIsChecked(htmlHelper.ViewData.Model), labelInnerHtml, enabled, null);
        }

        public static string CheckBox<TModel>(this HtmlHelper<TModel> htmlHelper, string name, Func<TModel, bool> getIsChecked, string labelInnerHtml, object htmlAttributes) where TModel : OxiteViewModel
        {
            return CheckBox(htmlHelper, name, getIsChecked(htmlHelper.ViewData.Model), labelInnerHtml, true, htmlAttributes);
        }

        public static string CheckBox<TModel>(this HtmlHelper<TModel> htmlHelper, string name, Func<TModel, bool> getIsChecked, string labelInnerHtml, bool enabled, object htmlAttributes) where TModel : OxiteViewModel
        {
            return CheckBox(htmlHelper, name, getIsChecked(htmlHelper.ViewData.Model), labelInnerHtml, enabled, htmlAttributes);
        }

        public static string CheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, string labelInnerHtml, object htmlAttributes)
        {
            return CheckBox(htmlHelper, name, isChecked, labelInnerHtml, true, htmlAttributes);
        }

        public static string CheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, string labelInnerHtml, bool enabled, object htmlAttributes)
        {
            bool value = htmlHelper.ViewContext.HttpContext.Request.Form.GetValues(name) != null
                ? htmlHelper.ViewContext.HttpContext.Request.Form.IsTrue(name)
                : isChecked;

            RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);

            if (!enabled)
                attributes.Add("disabled", "disabled");

            string checkBoxClassName = string.Format("{0}checkbox withLabel",
                attributes.ContainsKey("class")
                ? (attributes["class"] as string) + " "
                : "");

            attributes["class"] = checkBoxClassName;

            return fieldWithLabel(htmlHelper, name, () => htmlHelper.CheckBox(name, value, attributes), labelInnerHtml, "checkbox forCheckbox", false, enabled);
        }

        #endregion

        #region ConvertToLocalTime

        public static DateTime ConvertToLocalTime<TModel>(this HtmlHelper<TModel> htmlHelper, DateTime dateTime) where TModel : OxiteViewModel
        {
            return ConvertToLocalTime(htmlHelper, dateTime, htmlHelper.ViewData.Model);
        }

        public static DateTime ConvertToLocalTime(this HtmlHelper htmlHelper, DateTime dateTime, OxiteViewModel model)
        {
            if (!model.GetUser().IsAuthenticated)
            {
                if (model.Site.TimeZoneOffset != 0)
                    return dateTime.Add(TimeSpan.FromHours(model.Site.TimeZoneOffset));

                return dateTime;
            }

            return dateTime; //TODO: (erikpo) Get the timezone offset from the current user and apply it
        }

        #endregion

        #region DefaultText

        public static string DefaultText(this HtmlHelper htmlHelper, string text)
        {
            return string.Format("<span class=\"default\">{0}</span>", text.CleanText());
        }

        #endregion

        #region Gravatar

        public static string Gravatar<TModel>(this HtmlHelper<TModel> htmlHelper, string size) where TModel : OxiteViewModel
        {
            OxiteViewModel model = htmlHelper.ViewData.Model;
            UserViewModel user = model.GetUser();

            string emailHash = user.EmailHash ?? user.Email.ComputeHash();
            string name = user.DisplayName ?? user.Name;

            return htmlHelper.Gravatar(
                !string.IsNullOrEmpty(emailHash) ? emailHash.CleanAttribute() : null,
                !string.IsNullOrEmpty(name) ? name.CleanAttribute() : null, 
                size, 
                model.Site.GravatarDefault
                );
        }

        public static string Gravatar<TModel>(this HtmlHelper<TModel> htmlHelper, UserAnonymous user, string size) where TModel : OxiteViewModel
        {
            OxiteViewModel model = htmlHelper.ViewData.Model;

            return htmlHelper.Gravatar(
                !string.IsNullOrEmpty(user.EmailHash) ? user.EmailHash.CleanAttribute() : null,
                !string.IsNullOrEmpty(user.Name) ? user.Name.CleanAttribute() : null,
                size,
                model.Site.GravatarDefault
                );
        }

        public static string Gravatar(this HtmlHelper htmlHelper, OxiteViewModel model, string size)
        {
            UserViewModel user = model.GetUser();

            return htmlHelper.Gravatar(
                !string.IsNullOrEmpty(user.EmailHash) ? user.EmailHash.CleanAttribute() : null,
                !string.IsNullOrEmpty(user.Name) ? user.DisplayName.CleanAttribute() : null,
                size,
                model.Site.GravatarDefault
                );
        }

        public static string Gravatar(this HtmlHelper htmlHelper, OxiteViewModel model, UserAnonymous user, string size)
        {
            return htmlHelper.Gravatar(
                !string.IsNullOrEmpty(user.EmailHash) ? user.EmailHash.CleanAttribute() : null,
                !string.IsNullOrEmpty(user.Name) ? user.Name.CleanAttribute() : null,
                size,
                model.Site.GravatarDefault
                );
        }

        public static string Gravatar(this HtmlHelper htmlHelper, string id, string name, string size, string defaultImage)
        {
            return htmlHelper.Image(
                string.Format("http://www.gravatar.com/avatar/{0}?s={1}&d={2}", id ?? "@", size, defaultImage),
                string.Format("{0} (gravatar)", name ?? "Gravatar"),
                new RouteValueDictionary(new {width = size, height = size, @class = "gravatar"})
                );
        }

        #endregion

        #region HeadLink

        public static string HeadLink(this HtmlHelper htmlHelper, string rel, string href, string type, string title)
        {
            return htmlHelper.HeadLink(rel, href, type, title, null);
        }

        public static string HeadLink(this HtmlHelper htmlHelper, string rel, string href, string type, string title, object htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("link");

            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            if (!string.IsNullOrEmpty(rel))
            {
                tagBuilder.MergeAttribute("rel", rel);
            }
            if (!string.IsNullOrEmpty(href))
            {
                tagBuilder.MergeAttribute("href", href);
            }
            if (!string.IsNullOrEmpty(type))
            {
                tagBuilder.MergeAttribute("type", type);
            }
            if (!string.IsNullOrEmpty(title))
            {
                tagBuilder.MergeAttribute("title", title);
            }

            return tagBuilder.ToString(TagRenderMode.SelfClosing);
        }

        #endregion

        #region Image

        public static string Image(this HtmlHelper helper, string src, string alt, object htmlAttributes)
        {
            return helper.Image(src, alt, new RouteValueDictionary(htmlAttributes));
        }

        public static string Image(this HtmlHelper helper, string src, string alt, IDictionary<string, object> htmlAttributes)
        {
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);
            string imageUrl = url.Content(src);
            TagBuilder imageTag = new TagBuilder("img");

            if (!string.IsNullOrEmpty(imageUrl))
            {
                imageTag.MergeAttribute("src", imageUrl);
            }

            if (!string.IsNullOrEmpty(alt))
            {
                imageTag.MergeAttribute("alt", alt);
            }

            imageTag.MergeAttributes(htmlAttributes, true);

            if (imageTag.Attributes.ContainsKey("alt") && !imageTag.Attributes.ContainsKey("title"))
            {
                imageTag.MergeAttribute("title", imageTag.Attributes["alt"] ?? "");
            }

            return imageTag.ToString(TagRenderMode.SelfClosing);
        }

        #endregion

        #region Input

        public static string DropDownList(this HtmlHelper htmlHelper, string name, SelectList selectList, object htmlAttributes, bool isEnabled)
        {
            RouteValueDictionary htmlAttributeDictionary = new RouteValueDictionary(htmlAttributes);

            if (!isEnabled)
            {
                htmlAttributeDictionary["disabled"] = "disabled";

                StringBuilder inputItemBuilder = new StringBuilder();
                inputItemBuilder.AppendLine(htmlHelper.DropDownList(string.Format("{0}_view", name), selectList,
                                                                    htmlAttributeDictionary));
                inputItemBuilder.AppendLine(htmlHelper.Hidden(name, selectList.SelectedValue));
                return inputItemBuilder.ToString();
            }

            return htmlHelper.DropDownList(name, selectList, htmlAttributeDictionary);
        }

        public static string DropDownList<T>(this HtmlHelper htmlHelper, string name, IEnumerable<T> items, Func<T, string> getText, Func<T, string> getValue, object selectedValue, object htmlAttributes, bool isEnabled)
        {
            IEnumerable<KeyValuePair<string, string>> itemsForSelectList =
                items.ToList().Select((item, i) => new KeyValuePair<string, string>(getValue(item), getText(item)));

            return htmlHelper.DropDownList(name, new SelectList(itemsForSelectList, "Key", "Value", selectedValue), htmlAttributes, isEnabled);
        }

        public static string TextBox(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes, bool isEnabled)
        {
            RouteValueDictionary htmlAttributeDictionary = new RouteValueDictionary(htmlAttributes);

            if (!isEnabled)
            {
                htmlAttributeDictionary["disabled"] = "disabled";

                StringBuilder inputItemBuilder = new StringBuilder();

                inputItemBuilder.Append(htmlHelper.TextBox(string.Format("{0}_view", name), value, htmlAttributeDictionary));
                inputItemBuilder.Append(htmlHelper.Hidden(name, value));

                return inputItemBuilder.ToString();
            }

            return htmlHelper.TextBox(name, value, htmlAttributeDictionary);
        }

        public static string RadioButton(this HtmlHelper htmlHelper, string name, object value, bool isChecked, object htmlAttributes, bool isEnabled)
        {
            RouteValueDictionary htmlAttributeDictionary = new RouteValueDictionary(htmlAttributes);

            if (!isEnabled)
            {
                htmlAttributeDictionary["disabled"] = "disabled";

                StringBuilder inputItemBuilder = new StringBuilder();

                inputItemBuilder.AppendLine(htmlHelper.RadioButton(string.Format("{0}_view", name), value, isChecked, htmlAttributeDictionary));
                
                if (isChecked)
                {
                    inputItemBuilder.AppendLine(htmlHelper.Hidden(name, value));
                }
                
                return inputItemBuilder.ToString();
            }

            return htmlHelper.RadioButton(name, value, isChecked, htmlAttributeDictionary);
        }

        public static string Button(this HtmlHelper htmlHelper, string name, string buttonContent, object htmlAttributes)
        {
            return htmlHelper.Button(name, buttonContent, new RouteValueDictionary(htmlAttributes));
        }

        public static string Button(this HtmlHelper htmlHelper, string name, string buttonContent, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("button") { InnerHtml = buttonContent};

            tagBuilder.MergeAttributes(htmlAttributes);

            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        #endregion

        #region Link

        public static string Link(this HtmlHelper htmlHelper, string linkText, string href)
        {
            return htmlHelper.Link(linkText, href, null);            
        }

        public static string Link(this HtmlHelper htmlHelper, string linkText, string href, object htmlAttributes)
        {
            return htmlHelper.Link(linkText, href, new RouteValueDictionary(htmlAttributes));
        }

        public static string Link(this HtmlHelper htmlHelper, string linkText, string href, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("a")
            {
                InnerHtml = linkText
            };
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("href", href);
            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        #endregion

        #region LinkOrDefault

        public static string LinkOrDefault(this HtmlHelper htmlHelper, string linkContents, string href)
        {
            return htmlHelper.LinkOrDefault(linkContents, href, null);
        }

        public static string LinkOrDefault(this HtmlHelper htmlHelper, string linkContents, string href, object htmlAttributes)
        {
            return htmlHelper.LinkOrDefault(linkContents, href, new RouteValueDictionary(htmlAttributes));
        }

        public static string LinkOrDefault(this HtmlHelper htmlHelper, string linkContents, string href, IDictionary<string, object> htmlAttributes)
        {
            if (!string.IsNullOrEmpty(href))
            {
                TagBuilder tagBuilder = new TagBuilder("a")
                {
                    InnerHtml = linkContents
                };
                tagBuilder.MergeAttributes(htmlAttributes);
                tagBuilder.MergeAttribute("href", href);
                linkContents = tagBuilder.ToString(TagRenderMode.Normal);
            }

            return linkContents;
        }

        #endregion

        #region SearchTag
        public static string SearchTag<TModel>(this HtmlHelper<TModel> htmlHelper, string tagName, string tagValue, bool clean) where TModel:OxiteViewModel
        {
            if (String.IsNullOrEmpty(tagName) || String.IsNullOrEmpty(tagValue))
                return "";

            if (clean)
            {
                tagValue = tagValue.CleanHtmlTags().CleanHref();
            }
            return string.Format("<meta name=\"Search.{0}\" content=\"{1}\" />", tagName, tagValue);

        }
        #endregion

        #region OpenSearchOSDXLink

        public static string OpenSearchOSDXLink<TModel>(this HtmlHelper<TModel> htmlHelper) where TModel : OxiteViewModel
        {
            OxiteViewModel model = htmlHelper.ViewData.Model;

            if (model.Site.IncludeOpenSearch && htmlHelper.ViewContext.HttpContext.Request.UserAgent.Contains("Windows NT 6.1"))
            {
                UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

                return htmlHelper.Link(string.Format(model.Localize("Search.WindowsSearch", "Search {0} from Windows"), model.Site.DisplayName), urlHelper.AppPath(urlHelper.OpenSearchOSDX()), new { @class = "windowsSearch" });
            }

            return "";
        }

        #endregion

        #region PageClassification

        private static readonly Regex pageClassificationAllowedRegex = new Regex(@"^([a-z](?:[\w]{0,22}[a-z0-9])?)(?<!(?:true|false))$", RegexOptions.Compiled | RegexOptions.IgnoreCase); 
        public static string PageClassification<TModel>(this HtmlHelper<TModel> htmlHelper) where TModel : OxiteViewModel
        {
            StringBuilder className = new StringBuilder();
            RouteValueDictionary routeData = htmlHelper.ViewContext.RouteData.Values;

            foreach (var routeDataKvp in routeData)
            {
                string routeDataValue = routeDataKvp.Value.ToString().ToLower();
                if (pageClassificationAllowedRegex.IsMatch(routeDataValue))
                    className.AppendFormat(" {0}", routeDataValue);
            }

            return className.ToString();
        }

        #endregion

        #region PageDescription

        public static string PageDescription<TModel>(this HtmlHelper<TModel> htmlHelper) where TModel : OxiteViewModel
        {
            return htmlHelper.PageDescription(null);
        }

        public static string PageDescription<TModel>(this HtmlHelper<TModel> htmlHelper, string description) where TModel : OxiteViewModel
        {
            if (description == null)
                description = htmlHelper.Encode(htmlHelper.ViewData.Model.Site.Description);

            description = description.CleanHtmlTags().CleanHref();

            if (description != null && description.Length > 200)
                description = description.Substring(0, 200);

            return string.Format("<meta name=\"description\" content=\"{0}\" />", description);
        }

        #endregion

        #region PageTitle

        public static string PageTitle<TModel>(this HtmlHelper<TModel> htmlHelper, params string[] items) where TModel : OxiteViewModel
        {
            OxiteViewModel model = htmlHelper.ViewData.Model;

            if (items == null || items.Length == 0)
                return model.Site.DisplayName;
            
            StringBuilder sb = new StringBuilder(50);
            List<string> itemList = new List<string>(items);

            itemList.RemoveAll(s => s == null);

            itemList.Insert(0, model.Site.DisplayName);

            for (int i = itemList.Count - 1; i >= 0; i--)
            {
                sb.Append(itemList[i]);

                if (i > 0)
                    sb.Append(model.Site.PageTitleSeparator);
            }

            return htmlHelper.Encode(sb.ToString());
        }

        #endregion

        #region PageState

        public static string PageState<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize)
        {
            if (pageOfAList != null && pageOfAList.TotalPageCount > 1)
                return string.Format(
                    "<div class=\"pageState\">{0}</div>", 
                    string.Format(
                        localize("PageStateFormat", "{0}-{1} of {2}"), 
                        (pageOfAList.PageIndex * pageOfAList.PageSize) + 1, 
                        (pageOfAList.PageIndex * pageOfAList.PageSize) + pageOfAList.Count(), 
                        pageOfAList.TotalItemCount
                        )
                    );

            return "";
        }

        #endregion

        #region Pager

        public static string SimplePager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, string routeName, object values, string previousText, string nextText, bool alwaysShowPreviousAndNext)
        {
            if (pageOfAList == null || pageOfAList.TotalPageCount < 2) return "";

            StringBuilder sb = new StringBuilder(75);
            ViewContext viewContext = htmlHelper.ViewContext;
            RouteValueDictionary rvd = new RouteValueDictionary();

            foreach (KeyValuePair<string, object> item in viewContext.RouteData.Values)
            {
                rvd.Add(item.Key, item.Value);
            }

            UrlHelper urlHelper = new UrlHelper(viewContext.RequestContext);

            rvd.Remove("controller");
            rvd.Remove("action");
            rvd.Remove("id");
            rvd.Remove("dataFormat");

            if (values != null)
            {
                RouteValueDictionary rvd2 = new RouteValueDictionary(values);

                foreach (KeyValuePair<string, object> item in rvd2)
                {
                    rvd[item.Key] = item.Value;
                }
            }

            sb.Append("<div class=\"pager\">");

            if (pageOfAList.PageIndex < pageOfAList.TotalPageCount - 1 || alwaysShowPreviousAndNext)
            {
                rvd["pageNumber"] = string.Format("Page{0}", pageOfAList.PageIndex + 2);

                sb.AppendFormat("<a href=\"{1}{2}\" class=\"next\">{0}</a>", nextText,
                                urlHelper.RouteUrl(routeName, rvd),
                                viewContext.HttpContext.Request.QueryString.ToQueryString());
            }

            if (pageOfAList.PageIndex > 0 || alwaysShowPreviousAndNext)
            {
                rvd["pageNumber"] = string.Format("Page{0}", pageOfAList.PageIndex);

                sb.AppendFormat("<a href=\"{1}{2}\" class=\"previous\">{0}</a>", previousText,
                                urlHelper.RouteUrl(routeName, rvd),
                                viewContext.HttpContext.Request.QueryString.ToQueryString());
            }

            sb.Append("</div>");

            return sb.ToString();
        }

        #endregion

        #region Password

        public static string Password<TModel>(this HtmlHelper<TModel> htmlHelper, string name, object value, string labelInnerHtml) where TModel : OxiteViewModel
        {
            return Password(htmlHelper, name, value, labelInnerHtml, true);
        }

        public static string Password<TModel>(this HtmlHelper<TModel> htmlHelper, string name, object value, string labelInnerHtml, bool enabled) where TModel : OxiteViewModel
        {
            return Password(htmlHelper, name, value, labelInnerHtml, enabled, null);
        }

        public static string Password<TModel>(this HtmlHelper<TModel> htmlHelper, string name, object value, string labelInnerHtml, object htmlAttributes) where TModel : OxiteViewModel
        {
            return Password(htmlHelper, name, value, labelInnerHtml, true, htmlAttributes);
        }

        public static string Password<TModel>(this HtmlHelper<TModel> htmlHelper, string name, object value, string labelInnerHtml, bool enabled, object htmlAttributes) where TModel : OxiteViewModel
        {
            RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);

            if (!enabled)
                attributes.Add("disabled", "disabled");

            return fieldWithLabel(htmlHelper, name, () => htmlHelper.Password(name, value, attributes), labelInnerHtml, false, enabled);
        }

        #endregion

        #region PluginCheckBox

        public static string PluginCheckBox<TModel>(this HtmlHelper<TModel> htmlHelper, string name, Func<TModel, bool> getIsChecked, string labelInnerHtml) where TModel : OxiteViewModel
        {
            return htmlHelper.CheckBox("ep_" + name, getIsChecked, labelInnerHtml);
        }

        #endregion

        #region RadioButton

        public static string RadioButton(this HtmlHelper htmlHelper, string name, object value, bool isChecked, object htmlAttributes, string labelInnerHtml)
        {
            return RadioButton(htmlHelper, name, value, isChecked, htmlAttributes, true, labelInnerHtml);
        }

        public static string RadioButton(this HtmlHelper htmlHelper, string name, object value, bool isChecked, object htmlAttributes, bool enabled, string labelInnerHtml)
        {
            RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);

            if (!enabled)
                attributes.Add("disabled", "disabled");

            string checkBoxClassName = string.Format("{0}checkbox withLabel",
                attributes.ContainsKey("class")
                ? (attributes["class"] as string) + " "
                : "");

            attributes["class"] = checkBoxClassName;

            return fieldWithLabel(htmlHelper, name, () => htmlHelper.RadioButton(name, value, isChecked, attributes), labelInnerHtml, "checkbox forCheckbox", false, enabled);
        }

        #endregion

        #region RenderCssFile

        public static void RenderCssFile<TModel>(this HtmlHelper<TModel> htmlHelper, string path) where TModel : OxiteViewModel
        {
            htmlHelper.RenderCssFile(path, null);
        }

        public static void RenderCssFile<TModel>(this HtmlHelper<TModel> htmlHelper, string path, string releasePath) where TModel : OxiteViewModel
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

#if DEBUG
#else
            if (!string.IsNullOrEmpty(releasePath))
                path = releasePath;
#endif

            if (!(path.StartsWith("http://") || path.StartsWith("https://")))
            {
                UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

                string newPath = urlHelper.CssPath(path, htmlHelper.ViewContext);

                if (!string.IsNullOrEmpty(newPath))
                    path = newPath;
            }

            htmlHelper.ViewContext.HttpContext.Response.Write(
                htmlHelper.HeadLink("stylesheet", path, "text/css", "")
                );
        }

        #endregion

        #region RenderFavIcon

        public static void RenderFavIcon<TModel>(this HtmlHelper<TModel> htmlHelper) where TModel : OxiteViewModel
        {
            OxiteViewModel model = htmlHelper.ViewData.Model;

            if (!string.IsNullOrEmpty(model.Site.FavIconUrl))
            {
                UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

                htmlHelper.ViewContext.HttpContext.Response.Write(htmlHelper.HeadLink("shortcut icon", urlHelper.AppPath(model.Site.FavIconUrl), null, null));
            }
        }

        #endregion

        #region RenderFeedDiscovery

        public static void RenderFeedDiscoveryRss(this HtmlHelper htmlHelper, string title, string url)
        {
            htmlHelper.RenderFeedDiscovery(title, url, "application/rss+xml");
        }

        public static void RenderFeedDiscoveryAtom(this HtmlHelper htmlHelper, string title, string url)
        {
            htmlHelper.RenderFeedDiscovery(title, url, "application/atom+xml");
        }

        public static void RenderFeedDiscovery(this HtmlHelper htmlHelper, string title, string url, string type)
        {
            htmlHelper.ViewContext.HttpContext.Response.Write(
                htmlHelper.HeadLink("alternate", url, type, title)
                );
        }

        #endregion

        #region RenderOpenSearch

        public static void RenderOpenSearch<TModel>(this HtmlHelper<TModel> htmlHelper) where TModel : OxiteViewModel
        {
            OxiteViewModel model = htmlHelper.ViewData.Model;

            if (model.Site.IncludeOpenSearch)
            {
                UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

                htmlHelper.ViewContext.HttpContext.Response.Write(htmlHelper.HeadLink("search", urlHelper.AbsolutePath(urlHelper.OpenSearch()), "application/opensearchdescription+xml", string.Format(model.Localize("SearchFormat","{0} Search"), model.Site.DisplayName)));
            }
        }

        #endregion

        #region RenderPartialFromSkin

        //TODO: (erikpo) These need to be renamed to something else since it's not rendering from only skins now

        public static void RenderPartialFromSkin(this HtmlHelper htmlHelper, string partialViewName)
        {
            htmlHelper.RenderPartialFromSkin(partialViewName, null, htmlHelper.ViewData);
        }

        public static void RenderPartialFromSkin(this HtmlHelper htmlHelper, string partialViewName, ViewDataDictionary viewData)
        {
            htmlHelper.RenderPartialFromSkin(partialViewName, null, viewData);
        }

        public static void RenderPartialFromSkin(this HtmlHelper htmlHelper, string partialViewName, object model)
        {
            htmlHelper.RenderPartialFromSkin(partialViewName, model, htmlHelper.ViewData);
        }

        public static void RenderPartialFromSkin(this HtmlHelper htmlHelper, string partialViewName, object model, ViewDataDictionary viewData)
        {
            htmlHelper.renderPartialFromSkin(partialViewName, model, viewData, "OxiteViewEngines");
        }

        private static void renderPartialFromSkin(this HtmlHelper htmlHelper, string partialViewName, object model, ViewDataDictionary viewData, string viewEngineCollectionName)
        {
            List<string> searchedLocations = new List<string>(50);
            ViewDataDictionary newViewData;

            if (model == null)
                newViewData = viewData == null ? new ViewDataDictionary(htmlHelper.ViewData) : new ViewDataDictionary(viewData);
            else
                newViewData = viewData == null ? new ViewDataDictionary(model) : new ViewDataDictionary(viewData) { Model = model };

            foreach (IViewEngine viewEngine in (IEnumerable<IOxiteViewEngine>)newViewData[viewEngineCollectionName])
            {
                ViewEngineResult result = viewEngine.FindPartialView(htmlHelper.ViewContext, partialViewName, true);

                if (result.View != null)
                {
                    List<PartialViewRegistration> partialViewRegistrations = htmlHelper.ViewContext.HttpContext.Items["PartialViewRegistrations"] as List<PartialViewRegistration>;

                    if (partialViewRegistrations == null)
                    {
                        partialViewRegistrations = new List<PartialViewRegistration>();
                        htmlHelper.ViewContext.HttpContext.Items.Add("PartialViewRegistrations", partialViewRegistrations);
                    }

                    partialViewRegistrations.Add(new PartialViewRegistration(partialViewName, newViewData.Model));

                    result.View.Render(new ViewContext(htmlHelper.ViewContext, htmlHelper.ViewContext.View, newViewData, htmlHelper.ViewContext.TempData), htmlHelper.ViewContext.HttpContext.Response.Output);

                    searchedLocations.Clear();

                    break;
                }
                
                searchedLocations.AddRange(result.SearchedLocations);
            }

            if (searchedLocations.Count > 0)
            {
                StringBuilder locationsText = new StringBuilder();

                foreach (string location in searchedLocations)
                {
                    locationsText.AppendLine();
                    locationsText.Append(location);
                }

                throw new InvalidOperationException(string.Format("The partial view '{0}' could not be found. The following locations were searched:{1}", partialViewName, locationsText));
            }
        }

        #endregion

        #region RenderScriptTag

        public static void RenderScriptTag<TModel>(this HtmlHelper<TModel> htmlHelper, string path) where TModel : OxiteViewModel
        {
            htmlHelper.RenderScriptTag(path, null);
        }

        public static void RenderScriptTag<TModel>(this HtmlHelper<TModel> htmlHelper, string path, string releasePath) where TModel : OxiteViewModel
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

#if DEBUG
#else
            if (!string.IsNullOrEmpty(releasePath))
                path = releasePath;
#endif

            if (!(path.StartsWith("http://") || path.StartsWith("https://")))
            {
                UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

                string newPath = urlHelper.ScriptPath(path, htmlHelper.ViewContext);

                if (!string.IsNullOrEmpty(newPath))
                    path = newPath;
            }

            htmlHelper.ViewContext.HttpContext.Response.Write(htmlHelper.ScriptBlock("text/javascript", path));
        }

        #endregion

        #region RenderScriptVariable

        public static void RenderScriptVariable(this HtmlHelper htmlHelper, string name, object value)
        {
            const string scriptVariableFormat = "window.{0} = {1};";
            string script;

            if (value != null)
            {
                DataContractJsonSerializer dcjs = new DataContractJsonSerializer(value.GetType());

                using (MemoryStream ms = new MemoryStream())
                {
                    dcjs.WriteObject(ms, value);

                    script = string.Format(scriptVariableFormat, name, Encoding.Default.GetString(ms.ToArray()));

                    ms.Close();
                }
            }
            else
            {
                script = string.Format(scriptVariableFormat, name, "null");
            }

            htmlHelper.ViewContext.HttpContext.Response.Write(script);
        }

        #endregion

        #region ScriptBlock

        public static string ScriptBlock(this HtmlHelper htmlHelper, string type, string src)
        {
            return htmlHelper.ScriptBlock(type, src, null);
        }

        public static string ScriptBlock(this HtmlHelper htmlHelper, string type, string src, object htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("script");

            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            if (!string.IsNullOrEmpty(type))
            {
                tagBuilder.MergeAttribute("type", type);
            }
            if (!string.IsNullOrEmpty(src))
            {
                tagBuilder.MergeAttribute("src", src);
            }

            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        #endregion

        #region SkinImage

        public static string SkinImage(this HtmlHelper htmlHelper, string src, string alt, object htmlAttributes)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            src = urlHelper.CssPath(src, htmlHelper.ViewContext);

            return htmlHelper.Image(src, alt, new RouteValueDictionary(htmlAttributes));
        }

        #endregion

        #region TextArea

        public static string TextArea<TModel>(this HtmlHelper<TModel> htmlHelper, string name, Func<TModel, string> getValue, int rows, int columns, string labelInnerHtml) where TModel : OxiteViewModel
        {
            return TextArea(htmlHelper, name, getValue, rows, columns, labelInnerHtml, true);
        }

        public static string TextArea<TModel>(this HtmlHelper<TModel> htmlHelper, string name, Func<TModel, string> getValue, int rows, int columns, string labelInnerHtml, bool enabled) where TModel : OxiteViewModel
        {
            return TextArea(htmlHelper, name, getValue, rows, columns, labelInnerHtml, enabled, null);
        }

        public static string TextArea<TModel>(this HtmlHelper<TModel> htmlHelper, string name, Func<TModel, string> getValue, int rows, int columns, string labelInnerHtml, object htmlAttributes) where TModel : OxiteViewModel
        {
            return TextArea(htmlHelper, name, getValue, rows, columns, labelInnerHtml, true, htmlAttributes);
        }

        public static string TextArea<TModel>(this HtmlHelper<TModel> htmlHelper, string name, Func<TModel, string> getValue, int rows, int columns, string labelInnerHtml, bool enabled, object htmlAttributes) where TModel : OxiteViewModel
        {
            string value = htmlHelper.ViewContext.HttpContext.Request.Form[name] ?? getValue(htmlHelper.ViewData.Model) ?? "";

            RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);

            if (!enabled)
                attributes.Add("disabled", "disabled");

            return fieldWithLabel(htmlHelper, name, () => htmlHelper.TextArea(name, value, rows, columns, attributes), labelInnerHtml, true, enabled);
        }

        #endregion

        #region TextBox

        public static string TextBox<TModel>(this HtmlHelper<TModel> htmlHelper, string name, Func<TModel, object> getValue, string labelInnerHtml) where TModel : OxiteViewModel
        {
            return TextBox(htmlHelper, name, getValue, labelInnerHtml, true);
        }

        public static string TextBox<TModel>(this HtmlHelper<TModel> htmlHelper, string name, Func<TModel, object> getValue, string labelInnerHtml, bool enabled) where TModel : OxiteViewModel
        {
            return TextBox(htmlHelper, name, getValue, labelInnerHtml, enabled, null);
        }

        public static string TextBox<TModel>(this HtmlHelper<TModel> htmlHelper, string name, Func<TModel, object> getValue, string labelInnerHtml, object htmlAttributes) where TModel : OxiteViewModel
        {
            return TextBox(htmlHelper, name, getValue, labelInnerHtml, true, htmlAttributes);
        }

        public static string TextBox<TModel>(this HtmlHelper<TModel> htmlHelper, string name, Func<TModel, object> getValue, string labelInnerHtml, bool enabled, object htmlAttributes) where TModel : OxiteViewModel
        {
            object value = htmlHelper.ViewContext.HttpContext.Request.Form[name] ?? getValue(htmlHelper.ViewData.Model);

            RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);

            if (!enabled)
                attributes.Add("disabled", "disabled");

            return fieldWithLabel(htmlHelper, name, () => htmlHelper.TextBox(name, value, attributes), labelInnerHtml, true, enabled);
        }

        #endregion

        #region TextBoxWithValidation

        public static string TextBoxWithValidation<TModel>(this HtmlHelper<TModel> htmlHelper, string validationKey, string name) where TModel : OxiteViewModel
        {
            return TextBoxWithValidation(htmlHelper, validationKey, name, null);
        }

        public static string TextBoxWithValidation<TModel>(this HtmlHelper<TModel> htmlHelper, string validationKey, string name, object value) where TModel : OxiteViewModel
        {
            return TextBoxWithValidation(htmlHelper, validationKey, name, value, null);
        }

        public static string TextBoxWithValidation<TModel>(this HtmlHelper<TModel> htmlHelper, string validationKey, string name, object value, object htmlAttributes) where TModel : OxiteViewModel
        {
            OxiteViewModel model = htmlHelper.ViewData.Model;

            return htmlHelper.TextBox(name, value, htmlAttributes) + htmlHelper.ValidationMessage(validationKey, model.Localize(validationKey));
        }

        public static string TextBoxWithValidation<TModel>(this HtmlHelper<TModel> htmlHelper, string validationKey, string name, object value, object htmlAttributes, bool isEnabled) where TModel : OxiteViewModel
        {
            RouteValueDictionary htmlAttributeDictionary = new RouteValueDictionary(htmlAttributes);

            if (!isEnabled)
            {
                htmlAttributeDictionary["disabled"] = "disabled";

                StringBuilder inputItemBuilder = new StringBuilder();

                inputItemBuilder.Append(TextBoxWithValidation(htmlHelper, validationKey, string.Format("{0}_view", name), value, htmlAttributeDictionary));
                inputItemBuilder.Append(htmlHelper.Hidden(name, value));

                return inputItemBuilder.ToString();
            }

            return htmlHelper.TextBoxWithValidation(validationKey, name, value, htmlAttributes);
        }

        #endregion

        #region UnorderedList

        public static string UnorderedList<T>(this HtmlHelper htmlHelper, IEnumerable<T> items, Func<T, string> generateContent)
        {
            return UnorderedList(htmlHelper, items, (t, i) => generateContent(t));
        }

        public static string UnorderedList<T>(this HtmlHelper htmlHelper, IEnumerable<T> items, Func<T, string> generateContent, string cssClass)
        {
            return UnorderedList(htmlHelper, items, (t, i) => generateContent(t), cssClass, null, (string)null);
        }

        public static string UnorderedList<T>(this HtmlHelper htmlHelper, IEnumerable<T> items, Func<T, int, string> generateContent)
        {
            return UnorderedList(htmlHelper, items, generateContent, null);
        }

        public static string UnorderedList<T>(this HtmlHelper htmlHelper, IEnumerable<T> items, Func<T, int, string> generateContent, string cssClass)
        {
            return UnorderedList(htmlHelper, items, generateContent, cssClass, null, (string)null);
        }

        public static string UnorderedList<T>(this HtmlHelper htmlHelper, IEnumerable<T> items, Func<T, int, string> generateContent, string cssClass, string itemCssClass, string alternatingItemCssClass)
        {
            return UnorderedList(htmlHelper, items, generateContent, cssClass, t => cssClass, t => alternatingItemCssClass);
        }

        public static string UnorderedList<T>(this HtmlHelper htmlHelper, IEnumerable<T> items, Func<T, int, string> generateContent, string cssClass, Func<T, string> generateItemCssClass, Func<T, string> generateAlternatingItemCssClass)
        {
            if (items == null || items.Count() == 0) return "";

            StringBuilder sb = new StringBuilder(100);
            int counter = 0;

            sb.Append("<ul");
            if (!string.IsNullOrEmpty(cssClass))
                sb.AppendFormat(" class=\"{0}\"", cssClass);
            sb.Append(">");
            foreach (T item in items)
            {
                StringBuilder sbClass = new StringBuilder(40);
                string itemCssClass = generateItemCssClass != null ? generateItemCssClass(item) : null;
                string alternatingItemCssClass = generateAlternatingItemCssClass != null ? generateAlternatingItemCssClass(item) : null;

                if (counter == 0)
                    sbClass.Append("first ");

                if (item.Equals(items.Last()))
                    sbClass.Append("last ");

                if (!string.IsNullOrEmpty(itemCssClass))
                    sbClass.AppendFormat("{0} ", itemCssClass);

                if (counter % 2 != 0 && !string.IsNullOrEmpty(alternatingItemCssClass))
                    sbClass.AppendFormat("{0} ", alternatingItemCssClass);

                sb.Append("<li");
                if (sbClass.Length > 0)
                    sb.AppendFormat(" class=\"{0}\"", sbClass.Remove(sbClass.Length - 1, 1));
                sb.AppendFormat(">{0}</li>", generateContent(item, counter));

                counter++;
            }
            sb.Append("</ul>");

            return sb.ToString();
        }

        #endregion

        #region Private Methods

        private static string fieldWithLabel(this HtmlHelper htmlHelper, string name, Func<string> renderField, string labelInnerHtml, bool labelFirst, bool enabled)
        {
            return htmlHelper.fieldWithLabel(name, renderField, labelInnerHtml, null, labelFirst, enabled);
        }

        private static string fieldWithLabel(this HtmlHelper htmlHelper, string name, Func<string> renderField, string labelInnerHtml, string labelClassName, bool labelFirst, bool enabled)
        {
            StringBuilder output = new StringBuilder(200);

            if (!string.IsNullOrEmpty(labelInnerHtml))
            {
                if (!labelFirst)
                {
                    output.Append(renderField());
                    output.Append(" ");
                }

                TagBuilder builder = new TagBuilder("label");

                if (!enabled)
                    builder.Attributes["disabled"] = "disabled";
                builder.Attributes["for"] = name;
                if (!string.IsNullOrEmpty(labelClassName))
                    builder.Attributes["class"] = labelClassName;
                builder.InnerHtml = labelInnerHtml;

                output.Append(builder.ToString());

                if (labelFirst)
                {
                    output.Append(" ");
                    output.Append(renderField());
                }
            }
            else
                output.Append(renderField());

            return output.ToString();
        }

        #endregion
    }
}
