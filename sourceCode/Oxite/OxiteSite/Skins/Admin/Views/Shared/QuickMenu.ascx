<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Modules.Membership.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions" %><%
if (Model.GetUser().IsInRole("Admin"))
{ 
    %><ul class="admin quick menu" id="quickMenu">
        <li class="first"><%=Model.Localize("Admin.Quicklinks", "Quicklinks")%></li>
        <li><%=Html.Link(Model.Localize("Admin.MySite", "My Site"), Url.Posts(), new { @class = "posts" })%></li>
        <li><%=Html.Link(Model.Localize("Admin.AddPost", "Add a post"), Url.PostAdd(Model.Container as Blog), new { @class = "add post" })%></li>
        <li><%=Html.Link(Model.Localize("Admin.AddPage", "Add a page"), Url.PageAdd(), new { @class = "add page" })%></li>
        <li class="last"><%=Html.Link(Model.Localize("Admin.Comments", "Manage Comments"), Url.ManageComments(), new { @class = "manage comments" })%></li>
    </ul><%
}
%>