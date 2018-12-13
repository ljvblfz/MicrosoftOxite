<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Modules.Membership.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %><%
    if (Model.GetUser().IsAuthenticated)
    { %>
<%=string.Format(Model.Localize("WelcomeUserMessageFormat", "Welcome, {0}!"), string.Format("<span class=\"username\">{0}</span>", Html.Encode(Model.GetUser().DisplayName))) %>
<span class="logout"> / <%=Html.Link(Model.Localize("Logout"), Model.GetSignOutUrl())%></span><%
    }
    else
    { %>
<span class="login"><%=Html.Link(Model.Localize("Login"), Model.GetSignInUrl())%></span><%
    } %>