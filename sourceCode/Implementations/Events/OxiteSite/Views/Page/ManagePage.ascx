<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<Oxite.Modules.CMS.Models.Page>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions" %><% 
bool urlIsLocked = false; //Model.Item.HasChildren;

if (Model.User.IsInRole("Admin"))
{ %>
<div class="admin manage buttons">
<a href="<%=Url.PageAdd() %>" title="<%=Model.Localize("Add") %>" class="ibutton add"><%=Html.SkinImage("/images/page_add.png", Model.Localize("Add"), new { width = 16, height = 16 })%></a>
<a href="<%=Url.PageEdit(Model.Item) %>" title="<%=Model.Localize("Edit") %>" class="ibutton edit"><%=Html.SkinImage("/images/page_edit.png", Model.Localize("Edit"), new { width = 16, height = 16 })%></a><%
        if (!urlIsLocked)
        { %>
<form class="remove post" method="post" action="<%=Url.PageRemove(Model.Item) %>">
    <fieldset>
        <input type="image" src="/Skins/MIX10/Styles/img/page_delete.png" alt="<%=Model.Localize("Remove") %>" title="<%=Model.Localize("Remove") %>" class="ibutton image remove" />
        <%=Html.Hidden("returnUri", Url.Page(Model.Item.Slug))%>
        <%=Html.OxiteAntiForgeryToken() %>
    </fieldset>
</form><%
        } %>
</div><%
} %>