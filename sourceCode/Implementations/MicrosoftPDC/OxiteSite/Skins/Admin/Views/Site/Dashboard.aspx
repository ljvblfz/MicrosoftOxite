<%@ Page Language="C#" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<%@ Import Namespace="Oxite.Modules.Blogs.ViewModels" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server"><%
    BlogAdminDataViewModel adminData = Model.GetModelItem<BlogAdminDataViewModel>(); %>
    <h2 class="title"><%=Model.Localize("AdminDashboardTitle", "Admin Dashboard")%></h2>
    <div id="dashboard">
        <ul>
            <li id="recentComments">
                <h3><%=Model.Localize("RecentComments", "Recent Comments") %></h3>
                <% Html.RenderPartialFromSkin("CommentListSmall", new OxiteViewModelItemItems<Post, PostComment>(null, adminData.Comments, Model)); %>
                <% if (adminData.Comments.Count() > 0) { %><div class="more"><%=Html.Link(Model.Localize("AllComments", "More/Manage &raquo;"), Url.ManageComments())%></div><% } %>
            </li>
            <li id="recentPosts">
                <h3><%=Model.Localize("RecentPosts", "Recent Posts") %></h3>
                <% Html.RenderPartialFromSkin("PostListSmall", new OxiteViewModelItems<Post>(adminData.Posts, Model)); %>
                <% if (adminData.Posts.Count() > 0) { %><div class="more"><%=Html.Link(Model.Localize("AllPosts", "More &raquo;"), Url.PostsWithDrafts()) %></div><% } %>
            </li>
            <li id="recentTrackbacks">
                <h3><%=Model.Localize("RecentTrackbacks", "Recent Trackbacks") %></h3>
            </li>
            <li id="stats">
                <h3><%=Model.Localize("Stats") %></h3>
            </li>
            <li id="feedback">
                <h3><%=Model.Localize("Feedback") %></h3>
            </li>
            <li id="news">
                <h3><%=Model.Localize("News") %></h3>
            </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Dashboard")) %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Scripts"><%
    Html.RenderScriptTag("base.js");
 %>
</asp:Content>