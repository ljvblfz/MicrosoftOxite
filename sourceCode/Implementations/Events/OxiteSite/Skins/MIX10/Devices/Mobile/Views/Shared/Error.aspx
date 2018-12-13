<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<ExceptionOxiteViewModel>" MasterPageFile="../Shared/Site.Master" %>
<%@ Import Namespace="Oxite.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Error")) %>
</asp:Content>

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