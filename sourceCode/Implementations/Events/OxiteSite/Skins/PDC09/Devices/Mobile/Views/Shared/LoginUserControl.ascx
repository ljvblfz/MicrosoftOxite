<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>

<% if (Model.User.IsAuthenticated) { %>
<div id="user"> 
	<a href="/Sessions/Mine">My Sessions</a>&nbsp;|&nbsp;<%=string.Format("{0} <a href=\"{1}\">{2}</a>", Model.User.DisplayName.CleanText(), Model.SignOutUrl, Model.Localize("User.SignOut", "(sign out)"))%><img src="<%=ResolveClientUrl("../../Styles/images/") %>ico_liveid.gif" alt="" />	
</div>
<% } else { %>
<div class="login"><%=Html.Link(Model.Localize("User.SignIn", "Sign in"), Model.SignInUrl)%><img src="<%=ResolveClientUrl("../../Styles/images/") %>ico_liveid.gif" alt="" /></div>
<%} %>