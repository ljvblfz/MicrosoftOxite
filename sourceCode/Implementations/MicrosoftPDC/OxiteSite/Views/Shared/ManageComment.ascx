<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<PostComment>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %><%
if (Model != null && Model.User.IsInRole("Admin"))
{ %>
<div class="flags">
    <form class="flag remove" method="post" action="<%=Url.RemoveComment(Model.PartialModel) %>">
        <fieldset>
            <input type="image" class="ibutton remove" src="<%=Url.CssPath("images/delete.png", ViewContext) %>" title="<%=Model.Localize("Remove") %>" />
            <input type="hidden" name="returnUri" value="<%=Request.Url.AbsoluteUri %>" />
            <%=Html.OxiteAntiForgeryToken() %>
        </fieldset>
    </form><%
    if (Model.PartialModel.State == EntityState.PendingApproval)
    { %>
    <form class="flag approve" method="post" action="<%=Url.ApproveComment(Model.PartialModel) %>">
        <fieldset>
            <input type="image" class="ibutton approve" src="<%=Url.CssPath("images/accept.png", ViewContext) %>" title="<%=Model.Localize("Approve") %>" />
            <input type="hidden" name="returnUri" value="<%=Request.Url.AbsoluteUri %>" />
            <%=Html.OxiteAntiForgeryToken() %>
        </fieldset>
    </form><%
    } %>
</div><%
} %>