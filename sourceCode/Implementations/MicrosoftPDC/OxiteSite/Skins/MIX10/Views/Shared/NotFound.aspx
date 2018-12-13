<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title"><%=Model.Localize("NotFoundMessage", "Not Found") %></h2>
    <div class="error message m_404"><%=Model.Localize("NotFoundTitle", "The url you requested could not be found.")%><%
    if (!string.IsNullOrEmpty((string)ViewData["Description"]))
    { %>
    <p><%=ViewData["Description"] %></p><%
    } %>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("NotFoundMessage", "Not Found")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>
