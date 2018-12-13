<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Setup.master"
    Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<SetupInput>>" %>

<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Setup.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Setup.Models" %>
<%@ Import Namespace="Oxite.Modules.Setup" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title">
        <%=Model.Localize("Site.Add", "Initial Site Setup") %></h2>
    <%=Html.ValidationSummary() %>
    <form action="<%=Url.SetupBasicSettings()%>" method="post" id="siteSettings">
    <div>
        <%=Html.Hidden("siteID", Model.Item.SiteID)%>
        <%=Html.Hidden("siteType", Model.Item.SiteType)%>
        <%=Html.Hidden("storageType", Model.Item.StorageType)%>
    </div>
    <fieldset>
        <h3>
            <%=Model.Localize("Setup.AdminSettings", "Enter admin information")%></h3>
        <div>
            <%=Html.TextBox("adminUserName", m => "", "Username", true, new { size = 20, @class = "text" })%></div>
        <div>
            <%=Html.TextBox("adminDisplayName", m => "", "Display Name", true, new { size = 20, @class = "text" })%></div>
        <div>
            <%=Html.TextBox("adminEmail", m => "", "Email", true, new { size = 20, @class = "text" })%></div>
        <div>
            <%=Html.Password("adminPassword", "", "Password", true, new { size = 40, @class = "text" })%></div>
        <div>
            <%=Html.Password("adminPasswordConfirm", "", "Password (Confirm)", true, new { size = 40, @class = "text" })%></div>
    </fieldset>
    <div class="buttons">
        <input type="submit" name="submit" class="button submit" value="<%=Model.Localize("Site.Next", "Next")%>" />
        <%=Html.Button(
                "cancel",
                Model.Localize("Cancel"),
                new { @class = "cancel", onclick = string.Format("if (window.confirm('{0}')){{window.document.location='{1}';}}return false;", Model.Localize("really?"), Url.ManageSite()) }
                )%>
        <%=Html.OxiteAntiForgeryToken() %>
    </div>
    </form>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Site"), Model.Localize("Setup")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <%
        Html.RenderScriptTag("base.js"); %>
</asp:Content>
