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
    <form action="<%=Url.SetupModules()%>" method="post" id="siteSettings">
    <div>
        <%=Html.Hidden("siteID", Model.Item.SiteID)%>
        <%=Html.Hidden("siteType", Model.Item.SiteType)%>
        <%=Html.Hidden("storageType", Model.Item.StorageType)%>
        <%=Html.Hidden("adminUserName", Model.Item.AdminUserName)%>
        <%=Html.Hidden("adminDisplayName", Model.Item.AdminDisplayName)%>
        <%=Html.Hidden("adminEmail", Model.Item.AdminEmail)%>
        <%=Html.Hidden("adminPassword", Model.Item.AdminPassword)%>
        <%=Html.Hidden("adminPasswordConfirm", Model.Item.AdminPasswordConfirm)%>
    </div>
    <fieldset>
        <h3>
            <%=Model.Localize("Setup.BasicSettings", "Enter site information")%></h3>
        <div>
            <%=Html.TextBox("siteDisplayName", m => "My Site", "Display Name", new { size = 60, @class = "text" })%></div>
        <div>
            <%=Html.TextArea("siteDescription", m => "This is a new Oxite site", 4, 80, "Description")%></div>
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
