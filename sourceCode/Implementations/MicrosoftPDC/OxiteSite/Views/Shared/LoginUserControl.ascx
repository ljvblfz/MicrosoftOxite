<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %><%
    if (Model.User.IsAuthenticated)
    { %>
<%=string.Format(Model.Localize("WelcomeUserMessageFormat", "Welcome, {0}!"), string.Format("<span class=\"username\">{0}</span>", Html.Encode(Model.User.DisplayName))) %>
<span class="logout"> / <%=Html.Link(Model.Localize("Logout"), Model.SignOutUrl)%></span><%
    }
    else
    { %>
<span class="login"><%=Html.Link(Model.Localize("Login"), Model.SignInUrl)%></span><%
    } %>