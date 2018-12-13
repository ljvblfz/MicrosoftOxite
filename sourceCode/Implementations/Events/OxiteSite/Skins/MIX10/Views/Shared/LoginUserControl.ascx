<%@ Control Language="C#"AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%
    if (Model.User != null && Model.User.IsAuthenticated)
    { %> <%=Html.Link(Model.Localize("User.SignOut", "Sign Out"), Model.SignOutUrl)%>        
    <% } else { %>
		<%=Html.Link(Model.Localize("User.SignIn", "Sign In"), Model.SignInUrl)%><%
    } %>

