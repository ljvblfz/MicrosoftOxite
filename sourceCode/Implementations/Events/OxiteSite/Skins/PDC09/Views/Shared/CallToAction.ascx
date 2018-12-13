<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>
<div id="cta" class="bucket"><%
if (!Model.User.IsAuthenticated) { %>
    <%=Html.Content("CallToAction1A") %><%
} else if (Model.User.AuthenticationValues.ContainsKey("IsRegistered") && (bool)Model.User.AuthenticationValues["IsRegistered"]) { %>
    <%=Html.Content("CallToAction1C") %><%
} else { %>
    <%=Html.Content("CallToAction1B") %><%
} %>
<%--    <%
if (!Model.User.IsAuthenticated) { %>
    <p>Log in to build your 'My Sessions' page, and share the link with others.</p>
    <a class="button" href="<%= Model.SignInUrl %>">
        <span>Sign in now</span>
    </a>
    <% } %>
--%></div>