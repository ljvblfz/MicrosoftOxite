<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master"
    Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<Site>>" %>

<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title">
        <%=Model.Localize("Site.Add", "Initial Site Setup") %></h2>
    <%=Html.ValidationSummary() %>
    <form action="" method="post" id="siteSettings">
    <div>
        <%=Html.Hidden("siteID", Model.Item.ID) %></div>
    <h3>
        <%=Model.Localize("Setup.BasicSettings", "Enter site information")%></h3>
    <div>
        <%=Html.TextBox("siteDisplayName", m => "My Site", "Display Name", new { size = 60, @class = "text" })%></div>
    <div>
        <%=Html.TextArea("siteDescription", m => "This is a new Oxite site", 4, 80, "Description")%></div>
    <div class="buttons">
        <input type="submit" name="submit" class="button submit" value="<%=Model.Localize("Setup.UpdateAdmin", "Next: Create Admin Information")%>" />
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
