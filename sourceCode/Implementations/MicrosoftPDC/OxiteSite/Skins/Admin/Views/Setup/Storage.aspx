<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master"
    Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<Site>>" %>

<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title">
        <%=(Model.Item.ID == Guid.Empty ? Model.Localize("Site.Add", "Initial Site Setup") : Model.Localize("Site.Edit", "Edit Site")) %></h2>
    <%=Html.ValidationSummary() %>
    <form action="" method="post" id="siteSettings">
    <%
        if (Model.Item.ID != Guid.Empty)
        { %>
    <div>
        <%=Html.Hidden("siteID", Model.Item.ID) %></div>
    <%
        } %>
    <div>
        <%=Html.Hidden("hasMultipleBlogs", Model.Item.ID == Guid.Empty ? false : Model.Item.HasMultipleBlogs) %></div>
    <h3>
        <%=Model.Localize("Setup.Storage", "What is you storage mechanism?") %></h3>
    <div>
        <%=Html.CheckBox("xmlFileStorage", false, "Xml Files", new { size = 60, @class = "checkbox" })%></div>
    <div>
        <%=Html.CheckBox("sqlStorage", true, "SQL Server", new { size = 60, @class = "checkbox" })%></div>
    <div class="buttons">
        <input type="submit" name="submit" class="button submit" value="<%=Model.Item.ID == Guid.Empty ? Model.Localize("Site.Create", "Create Site") : Model.Localize("Site.Edit", "Edit Site") %>" />
        <%
                                                                                                                                                                                                           if (Model.Item.ID != Guid.Empty)
                                                                                                                                                                                                           { %>
        <%=Html.Button(
                "cancel",
                Model.Localize("Cancel"),
                new { @class = "cancel", onclick = string.Format("if (window.confirm('{0}')){{window.document.location='{1}';}}return false;", Model.Localize("really?"), Url.ManageSite()) }
                )%>
        <%=Html.Link(
                Model.Localize("Cancel"),
                Url.ManageSite(),
                new { @class = "cancel" })%><%
                                                } %><%=
            Html.OxiteAntiForgeryToken() %>
    </div>
    </form>i
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Site"), Model.Item.ID == Guid.Empty ? Model.Localize("Setup") : Model.Localize("Edit")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <%
        Html.RenderScriptTag("base.js"); %>
</asp:Content>
