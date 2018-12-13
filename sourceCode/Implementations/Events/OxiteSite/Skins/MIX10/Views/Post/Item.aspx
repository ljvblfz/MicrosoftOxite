<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<Post>>" MasterPageFile="../Shared/Site.master" %>
<%@ Import Namespace="Oxite.Modules.Core.Extensions"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Tags.Models"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models.Extensions" %>
<asp:Content ContentPlaceHolderID="SearchTags" runat="server"> 
<% string postURL = Url.Post(Model.Item); %>
<%=Html.SearchTag("Section", "Blogs", false)%>
<%=Html.SearchTag("Title", Model.Item.Title.CleanText(), false)%>
<%=Html.SearchTag("Author", Model.Item.Creator.Name.CleanText(), false) %>
<%=Html.SearchTag("Section", Model.Container.Name, false)%>
<%=Html.MetaTag("Keywords", string.Join(", ", Model.Item.Tags.Select(s => s.DisplayName).ToArray()), true) %>
<link rel="canonical" href="<%=Url.AbsolutePath(postURL) %>" /></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% string postURL = Url.Post(Model.Item); %><div class="post">
    <% Html.RenderPartialFromSkin("ManagePost"); %>
    <h1><%=Html.Link(Model.Item.Title.CleanText().WidowControl(), postURL) %></h1>
    <div class="metadata">
        <div class="posted">Posted <%=Html.Published() %> by <%=Model.Item.Creator.Name.CleanText() %></div>
    </div>
    <% Html.RenderPlayer("VideoPlayer", Model.Item.Files, Url.ViewTrack("post", "player", Model.Item.ID.ToString("N")),postURL); %>
    <div class="content"><%=Model.Item.Body %></div>
    <ul class="more">
        <li><%=Model.Localize("Tags") %>: <%
                                              
        IEnumerable<Tag> tags = Model.Item.GetTags();
        if (tags.Count() > 0)
        {
            %><%=string.Join(", ", tags.Select(t => Html.Link(t.GetDisplayName().CleanText(), Url.Posts(t), new { rel = "tag" })).ToArray()) %><%
        } else { %><%=Model.Localize("none") %><% } %></li><%
        if (Model.Item.Files.Count() > 0)
        { %>
        <li><% Html.RenderPartialFromSkin("Download", new OxiteViewModelPartial<IEnumerable<File>>(Model, Model.Item.Files)); %></li><%
        } %>
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
<asp:Content ContentPlaceHolderID="bodyTag" runat="server" ><body id="blog <%=Model.Container.Name %>"></asp:Content>
