<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<PageInput>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.CMS.Models" %>
<%@ Import Namespace="Oxite.Modules.CMS.ViewModels" %>
<fieldset class="title">
    <%=Html.ValidationMessage("Page.Title", Model.Localize("Title isn't valid.")) %>
    <%=Html.TextBox(
        "title",
        m => m.Item.Title,
        Model.Localize("Page.Title", "Title"),
        new { id = "page_title", @class = "text", size = "60" }
        ) %>
    <%=Html.OxiteAntiForgeryToken() %>
</fieldset>
<fieldset class="url">
    <label for="page_slug"><%=Model.Localize("Page.Url", "Location") %></label><%=Html.ValidationMessage("Page.Slug", Model.Localize("URL isn't valid.")) %>
    <span><%=string.Format(
                "{0}/",
                Url.AbsolutePath(Url.Home())
                ) 
    %></span>
    <%=Html.TextBox(
        "slug", 
        Model.Item.Slug,
        new { id = "page_slug", @class = "text", size = "60" }
        ) %>
</fieldset>
<fieldset class="description">
    <label for="page_description"><%=Model.Localize("Page.Description", "Description") %></label><%=Html.ValidationMessage("Page.Description", Model.Localize("Description isn't valid.")) %>
    <%=Html.TextArea(
        "description", 
        Model.Item.Description,
        new { id = "page_description", @class = "text", size = "60", rows = "4" }
        ) %>
</fieldset>