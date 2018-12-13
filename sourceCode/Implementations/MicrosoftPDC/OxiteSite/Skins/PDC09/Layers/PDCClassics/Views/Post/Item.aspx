<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile= "~/Skins/PDC09/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<Post>>" %>
<%@ Import Namespace="Oxite.Modules.Tags.Models"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SearchTags" runat="server">
<%=Html.SearchTag("Section", "Blogs", false)%>
<%=Html.SearchTag("Title", Model.Item.Title.CleanText(), false)%>
<%=Html.SearchTag("Author", Model.Item.Creator.Name.CleanText(), false) %>
<%=Html.SearchTag("Section", Model.Container.Name, false)%>
<link rel="canonical" href="<%=Url.AbsolutePath(Url.Post(Model.Item)) %>" /></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="post">
    <% Html.RenderPartialFromSkin("ManagePost"); %>
    <h1><%=Html.Link(Model.Item.Title.CleanText(), Url.Post(Model.Item)) %></h1>
    <div class="metadata">
        <div class="posted">Posted <%=Html.Published() %> by <%=Model.Item.Creator.Name.CleanText() %></div>
    </div>
    <div class="content"><%=Model.Item.Body %></div>
    <ul class="more">
<%--<%
        if (Model.Item.Files.Count > 0)
        { %>
        <li><% Html.RenderPartialFromSkin("Download", new OxiteModelPartial<Post>(Model, Model.Item)); %></li><%
        } %>--%>
        <li><% Html.RenderPartialFromSkin("PostShare", new OxiteViewModelPartial<Post>(Model, Model.Item)); %></li>        
    </ul><%
        if (!(Model.CommentingDisabled && Model.Item.Comments.Count() < 1))
        {
            Html.RenderPartialFromSkin("Comments", new OxiteViewModelItemItems<Post, PostComment>(Model.Item, Model.Item.Comments.OrderBy(pc => pc.Created), Model));
        }

        if (Model.CommentingDisabled)
        { %>
    <div class="message"><%=Model.Localize("CommentingDisabled", "Commenting is disabled for this post.")%></div><%
        } %>
</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server"><%=Html.PageTitle(Model.Container.GetDisplayName(), Model.Item.GetDisplayName()) %></asp:Content>
<asp:Content ContentPlaceHolderID="MetaDescription" runat="server"><%=Html.PageDescription(Model.Item.GetBodyShort()) %></asp:Content>
<asp:Content ContentPlaceHolderID="ScriptVariablesPre" runat="server">
    <script type="text/javascript">
        <% Html.RenderScriptVariable("computeHashPath", Url.ComputeEmailHash()); %>
        window.jsonComments = "<%=Url.Comments(Model.Item, "JSON") %>";
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js");
    Html.RenderScriptTag("Blog.js");
    %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadCustom" runat="server"><%
    //Html.RenderFeedDiscoveryRss("Post Comments (RSS)", Url.Comments(Model.Item, "RSS"));
    //Html.RenderFeedDiscoveryAtom("Post Comments (ATOM)", Url.Comments(Model.Item, "ATOM"));
    //if (Model.Site.HasMultipleBlogs)
    //{
        //Html.RenderFeedDiscoveryRss(string.Format("{0} Posts (RSS)", Model.Container.GetDisplayName()), Url.Container(Model.Container, "RSS"));
        //Html.RenderFeedDiscoveryAtom(string.Format("{0} Posts (ATOM)", Model.Container.GetDisplayName()), Url.Container(Model.Container, "ATOM"));
        //Html.RenderFeedDiscoveryRss(string.Format("All {0} Comments (RSS)", Model.Container.GetDisplayName()), Url.ContainerComments(Model.Container, "RSS"));
        //Html.RenderFeedDiscoveryAtom(string.Format("All {0} Comments (ATOM)", Model.Container.GetDisplayName()), Url.ContainerComments(Model.Container, "ATOM"));
    //}
    //Html.RenderFeedDiscoveryRss(string.Format("{0} (RSS)", Model.Site.DisplayName), Url.Posts("RSS"));
    //Html.RenderFeedDiscoveryAtom(string.Format("{0} (ATOM)", Model.Site.DisplayName), Url.Posts("ATOM"));
    Response.Write(Html.PingbackDiscovery(Model.Item)); %>
</asp:Content>
