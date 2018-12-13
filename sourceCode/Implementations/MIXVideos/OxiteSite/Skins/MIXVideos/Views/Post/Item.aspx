<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../../../../Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteModelItem<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="MIXVideos.Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="sections">
    <div class="primary">
        <div class="post">
            <% Html.RenderPartialFromSkin("ManagePost"); %>
            <h2 class="title"><%=Model.Item.Title.CleanText() %></h2>
            <div class="posted"><%=Model.Localize("From") %>&nbsp;<%=Html.Link(Model.Container.GetDisplayName(), Url.Posts((Area)Model.Container)) %> | <%=Html.Published() %></div>
            <div class="content"><% Html.RenderPlayer("MixVideosPlayer"); %><%=Model.Item.Body %></div>
            <ul class="more">
                <li><%=Model.Localize("Tags") %>: <%
                if (Model.Item.Tags.Count > 0)
                {
                    %><%=Html.UnorderedList(Model.Item.Tags.OrderBy(t => t.DisplayName), (t, i) => Html.Link(t.GetDisplayName().CleanText(), Url.Posts(t), new { rel = "tag" }), "tags") %><%
                } else { %>none<% } %></li><%
                if (Model.Item.Files.Count > 0)
                { %>
                <li><% Html.RenderPartialFromSkin("Download", new OxiteModelPartial<Post>(Model, Model.Item)); %></li><%
                } %>
            </ul><%
                if (!(Model.CommentingDisabled && Model.Item.Comments.Count < 1))
                {
                    Html.RenderPartialFromSkin("Comments");
                }

                if (Model.CommentingDisabled)
                {
                    %><div class="message"><%=Model.Localize("CommentingDisabled", "Commenting is disabled for this post.")%></div><%
                } %>
        </div>
    </div>
    <div class="secondary"><%
        Html.RenderPartialFromSkin("SideBar"); %>
    </div>
</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Site.HasMultipleAreas ? Model.Container.GetDisplayName() : null, Model.Item.GetDisplayName()) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="MetaDescription" runat="server">
    <%=Html.PageDescription(Model.Item.GetBodyShort()) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptVariablesPre" runat="server">
    <script type="text/javascript">
        <% Html.RenderScriptVariable("computeHashPath", Url.ComputeHash()); %>
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadCustom" runat="server"><%
    Html.RenderSearchTags(Model.Item);
    Html.RenderFeedDiscoveryRss("Post Comments (RSS)", Url.Comments(Model.Item, "RSS"));
    Html.RenderFeedDiscoveryAtom("Post Comments (ATOM)", Url.Comments(Model.Item, "ATOM"));
    if (Model.Site.HasMultipleAreas)
    {
        Html.RenderFeedDiscoveryRss(string.Format("{0} Posts (RSS)", Model.Container.GetDisplayName()), Url.Container(Model.Container, "RSS"));
        Html.RenderFeedDiscoveryAtom(string.Format("{0} Posts (ATOM)", Model.Container.GetDisplayName()), Url.Container(Model.Container, "ATOM"));
        Html.RenderFeedDiscoveryRss(string.Format("All {0} Comments (RSS)", Model.Container.GetDisplayName()), Url.ContainerComments(Model.Container, "RSS"));
        Html.RenderFeedDiscoveryAtom(string.Format("All {0} Comments (ATOM)", Model.Container.GetDisplayName()), Url.ContainerComments(Model.Container, "ATOM"));
    }
    Html.RenderFeedDiscoveryRss(string.Format("{0} (RSS)", Model.Site.DisplayName), Url.Posts("RSS"));
    Html.RenderFeedDiscoveryAtom(string.Format("{0} (ATOM)", Model.Site.DisplayName), Url.Posts("ATOM"));
    Response.Write(Html.PingbackDiscovery(Model.Item)); %>
</asp:Content>
