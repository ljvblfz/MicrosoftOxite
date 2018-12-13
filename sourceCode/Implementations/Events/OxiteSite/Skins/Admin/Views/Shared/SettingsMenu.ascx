<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Plugins.Extensions" %><%
if (Model.User.IsInRole("Admin"))
{ 
    //TODO: (nheskew)bring in some admin data to be smart with link destinations (like linking directly to edit a blog from "Edit Blog"...
    // if the site has only one blog, otherwise link to Url.BlogFind - see the site setting in views\site\dashboard
    %><ul class="admin settings menu" id="settingsMenu">
        <li id="dashboard" class="first"><h3><%=Html.Link(Model.Localize("Admin.Dashboard", "Dashboard"), Url.Admin(), new { @class = "admin dashboard" })%></h3></li>
        <li id="manageBlogsMenu">
            <h3><%=Html.Link(Model.Localize("Blogs.Manage", "Blogs"), Url.ManageBlogs(), new { @class = "manage blogs" })%></h3>
            <ul>
                <li class="first"><%=Html.Link(Model.Localize("Blogs.Add", "Add a blog"), Url.BlogAdd(), new { @class = "add blog" })%></li>
                <li class="last"><%=Html.Link(Model.Localize("Blog.Manage", "Edit a blog"), Url.BlogFind(), new { @class = "edit blog" })%></li>
            </ul>
        </li>
        <li id="managePostsMenu">
            <h3><%=Html.Link(Model.Localize("Posts.Manage", "Posts"), Url.PostsWithDrafts(), new { @class = "manage posts" })%></h3>
            <ul>
                <li class="first last"><%=Html.Link(Model.Localize("Admin.AddPost", "Add a post"), Url.PostAdd(Model.Container as Blog), new { @class = "add post" })%></li>
            </ul>
        </li>
        <li id="managePagesMenu">
            <h3><%=Html.Link(Model.Localize("Pages.Manage", "Pages"), Url.Pages(), new { @class = "manage posts" })%></h3>
            <ul>
                <li class="first last"><%=Html.Link(Model.Localize("Admin.AddPage", "Add a page"), Url.PageAdd(), new { @class = "add page" })%></li>
            </ul>
        </li>
        <li id="manageUsersMenu">
            <h3><%=Html.Link(Model.Localize("Users.Manage", "Users"), Url.ManageUsers(), new { @class = "manage users" })%></h3>
            <ul>
                <li class="first"><%=Html.Link(Model.Localize("Users.Add", "Add a user"), Url.UserAdd(), new { @class = "add user" })%></li>
                <li><%=Html.Link(Model.Localize("Users.Find", "Find a user"), Url.UserFind(), new { @class = "find user" })%></li>
                <li class="last"><%=Html.Link(Model.Localize("Roles.Find", "Find a role"), Url.RoleFind(), new { @class = "find roles" })%></li>
            </ul>
        </li>
        <li id="managePluginsMenu">
            <h3><%=Html.Link(Model.Localize("Plugins.Manage", "Plugins"), Url.Plugins(), new { @class = "manage plugins" })%></h3>
<%--            <ul>
                <li class="first last"><%=Html.Link(Model.Localize("Plugins.Install", "Install a plugin"), Url.PluginsNotInstalled(), new { @class = "add plugin" })%></li>
            </ul>
--%>        </li>
        <li id="manageSettingsMenu" class="last">
            <h3><%=Html.Link(Model.Localize("Settings.Manage", "Settings"), Url.ManageSite(), new { @class = "manage settings" })%></h3>
            <ul>
                <li class="first"><%=Html.Link(Model.Localize("Settings.Site", "Site"), Url.Site(), new { @class = "site settings" })%></li>
                <li class="last"><%=Html.Link("BlogML", Url.BlogFind(), new { @class = "blogml import" })%></li>
            </ul>
        </li>
    </ul><%
}
%>