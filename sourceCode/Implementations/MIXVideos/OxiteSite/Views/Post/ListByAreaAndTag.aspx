<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteModelList<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server"><%
            string tagName = ViewContext.RouteData.Values["tagName"] as string; %>
            <div class="sections">
                <div class="primary">
                    <h2 class="title"><%
                        if (Model.Site.HasMultipleAreas) { %><%=Html.Link(Model.Container.GetDisplayName(), Url.Posts(Model.Container as Area), new { @class = string.Format("area {0}", Model.Container.Name) }) %> &raquo; <% }
                        %><%=Html.Link(Model.Localize("TagsTitle", "Tags"), Model.Site.HasMultipleAreas ? Url.Tags(Model.Container as Area) : Url.Tags())
                        %> &raquo; <%=tagName %></h2>
                    <%=Html.PageState((IPageOfList<Post>)Model.List, (k, v) => Model.Localize(k, v)) %><%
                    Html.RenderPartialFromSkin("PostListMedium");
                    %><%=Html.PostListByAreaAndTagPager((IPageOfList<Post>)Model.List, (k, v) => Model.Localize(k, v), Model.Container.Name, tagName) %>
                </div>
                <div class="secondary"><% 
                    Html.RenderPartialFromSkin("SideBar"); %>
                </div>
            </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Site.HasMultipleAreas ? Model.Container.GetDisplayName() : null, Model.Localize("TagsTitle", "Tags"), ViewContext.RouteData.Values["tagName"] as string)%>
</asp:Content>
<asp:Content ContentPlaceHolderID="MetaDescription" runat="server">
    <%=Html.PageDescription(((Area)Model.Container).Description) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadCustom" runat="server"><%
    Html.RenderFeedDiscoveryRss(string.Format("{0} Posts (RSS)", Model.Container.GetDisplayName()), Url.Container(Model.Container, "RSS"));
    Html.RenderFeedDiscoveryAtom(string.Format("{0} Posts (ATOM)", Model.Container.GetDisplayName()), Url.Container(Model.Container, "ATOM"));
    Html.RenderFeedDiscoveryRss(string.Format("All {0} Comments (RSS)", Model.Container.GetDisplayName()), Url.ContainerComments(Model.Container, "RSS"));
    Html.RenderFeedDiscoveryAtom(string.Format("All {0} Comments (ATOM)", Model.Container.GetDisplayName()), Url.ContainerComments(Model.Container, "ATOM"));
    Html.RenderFeedDiscoveryRss(string.Format("{0} (RSS)", Model.Site.DisplayName), Url.Posts("RSS"));
    Html.RenderFeedDiscoveryAtom(string.Format("{0} (ATOM)", Model.Site.DisplayName), Url.Posts("ATOM"));
    Html.RenderRsd(Model.Container.Name);
    Html.RenderLiveWriterManifest(); %>
</asp:Content>
