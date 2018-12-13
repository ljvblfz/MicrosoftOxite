<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<ExceptionOxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title"><%=Model.Localize("Error") %></h2>
<%
bool debug = false;

#if DEBUG
debug = true;
#endif

if (debug)
{ %>
    <div style="overflow:auto;">
        <%=Html.Encode(Model.Exception.ToString()) %>
    </div><%
}
else
{ %>
    <p><%=Model.Localize("GenericError", "Sorry, an error occurred while processing your request.")%></p><%
} %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Error")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>
