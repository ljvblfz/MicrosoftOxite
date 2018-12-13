<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<PostComment>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<%@ Import Namespace="Oxite.Modules.Plugins.Extensions" %>
<form class="flag spam" method="post" action="<%=Url.PluginRouteUrl(((Plugin)ViewData["Plugin"]), "MarkAsSpam") %>">
    <fieldset>
        <input type="image" class="ibutton spam" src="<%=Url.PluginImagesPath(ViewData["Plugin"] as Plugin, "flag_red.png") %>" title="<%=Model.Localize("Comment.MarkAsSpam", "This is spam!") %>" />
        <input type="hidden" name="commentCreatorName" value="<%=Model.PartialModel.CreatorName %>" />
        <input type="hidden" name="commentCreatorEmail" value="<%=Model.PartialModel.CreatorEmail %>" />
        <input type="hidden" name="commentCreatorUrl" value="<%=Model.PartialModel.CreatorUrl %>" />
        <input type="hidden" name="commentBody" value="<%=Model.PartialModel.Body %>" />
        <input type="hidden" name="commentCreatorIP" value="<%=new System.Net.IPAddress(Model.PartialModel.CreatorIP).ToString() %>" />
        <input type="hidden" name="commentCreatorUserAgent" value="<%=Model.PartialModel.CreatorUserAgent %>" />
        <input type="hidden" name="commentReferrerUri" value="" />
        <input type="hidden" name="permalinkUri" value="<%=ViewContext.HttpContext.Request.Url.AbsoluteUri %>" />
        <input type="hidden" name="redirectUri" value="<%=ViewContext.HttpContext.Request.Url.AbsoluteUri %>" />
        <%=Html.OxiteAntiForgeryToken() %>
    </fieldset>
</form>