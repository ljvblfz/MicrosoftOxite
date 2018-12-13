<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%
    if (Model.User != null && Model.User.IsAuthenticated)
    { %>
<div id="user">    
    <%=string.Format("{0} (<a href=\"{1}\">{2}</a>)", Model.User.DisplayName.CleanText(), Model.SignOutUrl, Model.Localize("User.SignOut", "sign out"))%>
</div>
<div id="mysessions">
    <a href="/Sessions/Mine">My Sessions</a>
</div><%
    }
    else
    { %>
<div class="login"><%=Html.Link(Model.Localize("User.SignIn", "sign in"), Model.SignInUrl)%></div><%
    } %>
