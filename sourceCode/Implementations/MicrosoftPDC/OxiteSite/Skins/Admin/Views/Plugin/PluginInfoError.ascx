<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<Plugin>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Plugins.Extensions "%>
<%@ Import Namespace="Oxite.Modules.Plugins.Extensions" %>
<span class="icon"><%=Html.Image(Model.PartialModel.GetIconLargeError() ?? Url.CssPath("images/plugin_large_error.png", ViewContext),
                       string.Format(Model.Localize("PluginIconAltFormat", "{0} icon"),
                       Model.PartialModel.GetDisplayName().CleanAttribute()), new { height = 32, width = 32 }) %></span>
<span class="name metadata"><%=Model.PartialModel.Container.GetDisplayName().CleanText() %></span>