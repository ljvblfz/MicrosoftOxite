<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Extensions" %>
<div id="comments"><% 
    string statusClass = "status";
    if (Model.Item.Comments.Count() < 1)
        statusClass = statusClass + " empty";
    %>
	<div class="<%=statusClass %>">
		<h3><%= string.Format(Model.Item.Comments.Count() == 1 ? Model.Localize("SingleCommentStatus", "{0} Comment") : Model.Localize("MultiCommentStatus", "{0} Comments"), Model.Item.Comments.Count())%></h3>
		<div><a href="#comment"><%=Model.Localize("Comments.Add", "+ Add comment")%></a></div>
	</div>
	<%
    Html.RenderPartialFromSkin("CommentListMedium", new OxiteViewModelItems<ScheduleItemComment>(Model.Item.Comments.OrderBy(sic => sic.Created), Model));

    if (!Model.CommentingDisabled && string.IsNullOrEmpty(ViewData["CommentingOnComment"] as string))
    {
        Html.RenderPartialFromSkin("CommentOnPost");
    }
    %>
</div>