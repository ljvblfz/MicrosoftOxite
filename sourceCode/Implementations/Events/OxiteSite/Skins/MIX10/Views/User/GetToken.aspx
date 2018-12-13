<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Sync.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ID="bodyTag" runat="server" ContentPlaceHolderID="bodyTag"><body class="syncclient"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% if (!Model.User.IsAuthenticated)
 {
     %>
    <h1><%=Model.Localize("User.Token", "Get API Token") %></h1>
     <%=Html.Link(Model.Localize("UserToken.SignIn", "Sign In to get your Token"), Model.SignInUrl)%>
     <%
 }
       else if (ViewData["APIToken"] == null)
       {
     %>
    <h1><%=Model.Localize("User.Token", "Sync Framework Client") %></h1>
     <%=Model.Localize("User.Token.None", "No user token found.")%>
     <%
         }
       else
       {
           string apiToken = ViewData["APIToken"] as string;

           if (!String.IsNullOrEmpty(apiToken))
           {%>
       <div id="apitoken"><%=apiToken%></div>
          <%
           }
       }%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("User.Token.Title", "Get API Token")) %>
</asp:Content>
