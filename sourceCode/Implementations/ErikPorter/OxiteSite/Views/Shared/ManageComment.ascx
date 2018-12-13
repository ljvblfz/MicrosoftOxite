<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelPartial<ParentAndChild<Post, Comment>>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.ViewModels.Extensions" %><%
if (Model != null && Model.RootModel.User.GetCanAccessAdmin())
{ %>
<div class="flags">
    <form class="flag remove" method="post" action="<%=Url.RemoveComment(Model.PartialModel.Parent) %>">
        <fieldset>
            <input type="image" class="ibutton remove" src="<%=Url.CssPath("/images/delete.png", Model.RootModel) %>" title="<%=Model.RootModel.Localize("Remove") %>" />
            <input type="hidden" name="id" value="<%=Model.PartialModel.Child.ID %>" />
            <input type="hidden" name="returnUri" value="<%=Request.Url.AbsoluteUri %>" />
            <%=Html.OxiteAntiForgeryToken(m => m.RootModel) %>
        </fieldset>
    </form><%
    if (Model.PartialModel.Child.State == EntityState.PendingApproval)
    { %>
    <form class="flag approve" method="post" action="<%=Url.ApproveComment(Model.PartialModel.Parent) %>">
        <fieldset>
            <input type="image" class="ibutton approve" src="<%=Url.CssPath("/images/accept.png", Model.RootModel) %>" title="<%=Model.RootModel.Localize("Approve") %>" />
            <input type="hidden" name="id" value="<%=Model.PartialModel.Child.ID %>" />
            <input type="hidden" name="returnUri" value="<%=Request.Url.AbsoluteUri %>" />
            <%=Html.OxiteAntiForgeryToken(m => m.RootModel) %>
        </fieldset>
    </form><%
    } %>
</div><%
} %>