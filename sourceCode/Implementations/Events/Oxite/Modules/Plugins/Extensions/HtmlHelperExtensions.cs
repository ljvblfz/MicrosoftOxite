// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Plugins.Models;
using Oxite.Plugins.Attributes;
using Oxite.ViewModels;

namespace Oxite.Modules.Plugins.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string PluginAuthors<TModel>(this HtmlHelper<TModel> htmlHelper) where TModel : OxiteViewModelPartial<Plugin>
        {
            Plugin plugin = htmlHelper.ViewData.Model.PartialModel;
            string[] authors = plugin.GetAuthors();

            if (authors.Length == 0)
                return htmlHelper.ViewData.Model.Localize("Unknown");

            if (authors.Length == 1)
            {
                string[] authorUrls = plugin.GetAuthorUrls();

                return htmlHelper.LinkOrDefault(authors[0].CleanText(), authorUrls.Length == 1 ? authorUrls[0].CleanHref() : "");
            }
            else
            {
                List<string> authorList = new List<string>(authors.Length);
                string[] authorUrls = plugin.GetAuthorUrls();

                for (int i = 0; i < authors.Length; i++)
                    authorList.Add(htmlHelper.LinkOrDefault(authors[i].CleanText(), authorUrls.Length == authors.Length ? authorUrls[i].CleanHref() : ""));

                return string.Join(", ", authorList.ToArray());
            }
        }

        public static string PluginPropertyFieldsets(this HtmlHelper htmlHelper, Plugin plugin, PluginEditInput pluginEditInput, Func<string, string, string> localize)
        {
            StringBuilder outputBuilder = new StringBuilder();

            IEnumerable<ExtendedProperty> editInputProperties = pluginEditInput.GetExtendedProperties(plugin.ExtendedProperties);

            IEnumerable<IGrouping<KeyValuePair<string, int>, ExtendedProperty>> extendedPropertyGroupings =
                plugin.ExtendedProperties.GroupBy(
                    ps => plugin.GetGroup(ps.Name) != null
                        ? new KeyValuePair<string, int>(plugin.GetGroup(ps.Name).Name, plugin.GetGroup(ps.Name).Order)
                        : new KeyValuePair<string, int>(localize("Plugin.OtherPropertyGroup", "Other"), int.MaxValue));

            foreach (var extendedPropertyGrouping in extendedPropertyGroupings.OrderBy(psg => psg.Key.Value))
            {
                if (extendedPropertyGroupings.Count(epg => epg.Key.Key != "Other") > 0)
                    outputBuilder.AppendFormat("<h4>{0}</h4>", extendedPropertyGrouping.Key.Key.CleanText());

                foreach (var extendedProperty in extendedPropertyGrouping.OrderBy(ps => plugin.GetOrder(ps.Name)))
                {
                    string extendedPropertyName = extendedProperty.Name;

                    ExtendedProperty editInputProperty =
                        editInputProperties.FirstOrDefault(ep => ep.Name == extendedPropertyName);
                    object extendedPropertyValue = editInputProperty != null
                        ? editInputProperty.Value
                        : extendedProperty.Value;

                    bool labelFirst = true;
                    string fieldName = string.Format("ps_{0}", extendedPropertyName.CleanAttribute());
                    Func<string> fieldRenderField = () => extendedPropertyName.EndsWith("Password") && extendedProperty.Type == typeof(string)
                        ? htmlHelper.Password(fieldName, extendedPropertyValue, new { id = fieldName })
                        : htmlHelper.TextBox(fieldName, extendedPropertyValue, new { id = fieldName });

                    var fieldset = new TagBuilder("fieldset");

                    string extendedPropertyValidationKey = string.Format("PluginPropertiesInput.{0}", extendedProperty.Name);
                    if (htmlHelper.ViewData.ModelState.ContainsKey(extendedPropertyValidationKey) && htmlHelper.ViewData.ModelState[extendedPropertyValidationKey].Errors.Count > 0)
                        fieldset.AddCssClass("error");

                    fieldset.Attributes["id"] = string.Format("psf_{0}", extendedPropertyName.CleanAttribute());

                    if (extendedPropertyValue is bool)
                    {
                        labelFirst = false;
                        fieldRenderField = () => htmlHelper.CheckBox(fieldName, (bool)extendedPropertyValue, null, null);
                    }
                    else if (extendedPropertyValue is DateTime)
                    {
                        fieldset.AddCssClass("datetime");
                    }
                    else
                    {
                        fieldset.AddCssClass("textbox");
                    }

                    PropertyAppearance appearance = plugin.GetAppearance(extendedPropertyName);
                    if (appearance != null)
                    {
                        if (!string.IsNullOrEmpty(appearance.Width))
                        {
                            fieldset.Attributes["style"] = string.Format("width:{0}", appearance.Width);
                            fieldset.AddCssClass("hasWidth");
                        }

                        //TODO: (erikpo) Add height (multiline) when appropriate
                    }

                    fieldset.InnerHtml = htmlHelper.fieldWithLabel(
                        fieldName.CleanAttribute(),
                        fieldRenderField,
                        (plugin.GetLabelText(extendedPropertyName) ?? extendedPropertyName).CleanText().AutoAnchor(),
                        labelFirst,
                        true
                        );

                    outputBuilder.Append(fieldset.ToString());
                }
            }

            return outputBuilder.ToString();
        }

        private static string fieldWithLabel(this HtmlHelper htmlHelper, string name, Func<string> renderText, string labelInnerHtml, bool labelFirst, bool enabled)
        {
            StringBuilder output = new StringBuilder(200);

            if (!string.IsNullOrEmpty(labelInnerHtml))
            {
                if (!labelFirst)
                {
                    output.Append(renderText());
                    output.Append(" ");
                }

                TagBuilder builder = new TagBuilder("label");

                if (!enabled)
                    builder.Attributes["disabled"] = "disabled";
                builder.Attributes["for"] = name;
                builder.InnerHtml = labelInnerHtml;

                output.Append(builder.ToString());

                if (labelFirst)
                {
                    output.Append(" ");
                    output.Append(renderText());
                }
            }
            else
                output.Append(renderText());

            return output.ToString();
        }
    }
}
