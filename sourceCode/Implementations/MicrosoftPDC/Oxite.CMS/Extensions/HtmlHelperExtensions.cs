// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Modules.CMS.Models;
using Oxite.Modules.CMS.ViewModels;
using Oxite.ViewModels;

namespace Oxite.Modules.CMS.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string Content<TModel>(this HtmlHelper<TModel> htmlHelper, string name) where TModel : OxiteViewModel
        {
            StringBuilder sb = new StringBuilder();
            ContentItem contentItem = getContent(htmlHelper.ViewData.Model, name);

            if (htmlHelper.ViewData.Model.User.IsInRole("Admin"))
                 sb.Append(contentItem != null
                    ? contentItem.Page != null
                        ? htmlHelper.manageContent(name)
                        : htmlHelper.manageGlobalContent(name)
                    : htmlHelper.addContent(name)
                    );

            if (contentItem != null)
                sb.Append(contentItem.Body);

            return sb.ToString();
        }

        private static ContentItem getContent<TModel>(TModel model, string name) where TModel : OxiteViewModel
        {
            ContentItemViewModel contentItemViewModel = model.GetModelItem<ContentItemViewModel>();

            if (contentItemViewModel == null)
                return null;

            ContentItem contentItem = contentItemViewModel.ContentItems.Where(ci => string.Compare(ci.Name, name, true) == 0).FirstOrDefault();

            return contentItem;
        }

        private static string manageContent<TModel>(this HtmlHelper<TModel> htmlHelper, string name) where TModel : OxiteViewModel
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            OxiteViewModelItem<Page> modelItem = htmlHelper.ViewData.Model as OxiteViewModelItem<Page>;
            string linkText = "*manage";
            object htmlAttributes = new { @class = "manageContentItem" };

            if (modelItem != null)
                return htmlHelper.Link(linkText, url.PageContentEdit(modelItem.Item, name), htmlAttributes);

            if (htmlHelper.ViewContext.RouteData.Values.ContainsKey("pagePath"))
                return htmlHelper.Link(linkText, url.PageContentEdit(htmlHelper.ViewContext.RouteData.Values["pagePath"] as string, name), htmlAttributes);

            return htmlHelper.manageGlobalContent(name);
        }

        private static string manageGlobalContent<TModel>(this HtmlHelper<TModel> htmlHelper, string name) where TModel : OxiteViewModel
        {
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return htmlHelper.Link("*manage", url.PageGlobalContentEdit(name), new { @class = "manageContentItem" });
        }

        private static string addContent<TModel>(this HtmlHelper<TModel> htmlHelper, string name) where TModel : OxiteViewModel
        {
            return "<div>+add</div>";
        }
    }
}
