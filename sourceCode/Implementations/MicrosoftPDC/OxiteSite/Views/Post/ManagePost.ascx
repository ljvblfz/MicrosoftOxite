<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %> 
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %><%
bool urlIsLocked = Model.Item.State == EntityState.Normal
    && Model.Item.Published.HasValue
    && Model.Item.Published.Value.AddHours(Model.Site.PostEditTimeout) < DateTime.Now.ToUniversalTime();

if (Model.User.IsInRole("Admin"))
{ %>
<div class="admin manage buttons"><%
    if (Model.Item.State != EntityState.Removed)
    { %>
    <a href="<%=Url.FilesByPost(Model.Item) %>" title="<%=Model.Localize("ManageFiles", "Manage Files") %>" class="ibutton files"><%=Html.SkinImage("/images/page_files.png", Model.Localize("ManageFiles", "Manage Files"), new { width = 16, height = 16 })%></a>
    <a href="<%=Url.PostEdit(Model.Item) %>" title="<%=Model.Localize("Edit") %>" class="ibutton edit"><%=Html.SkinImage("/images/page_edit.png", Model.Localize("Edit"), new { width = 16, height = 16 })%></a><%
        if (!urlIsLocked)
        { %>
    <form class="remove post" method="post" action="<%=Url.PostRemove(Model.Item) %>">
        <fieldset>
            <input type="image" src="<%=Url.CssPath("images/page_delete.png", ViewContext) %>" alt="<%=Model.Localize("Remove") %>" title="<%=Model.Localize("Remove") %>" class="ibutton image remove" />
            <%=Html.Hidden("returnUri", Request.Url.AbsoluteUri)%>
            <%=Html.OxiteAntiForgeryToken() %>
        </fieldset>
    </form><%
        }
    } %>
</div><%
} %>