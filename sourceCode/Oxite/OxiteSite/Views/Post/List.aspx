<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
            <div class="sections">
                <div class="primary">
                    <%=Html.PageState((IPageOfItems<Post>)Model.Items, (k, v) => Model.Localize(k, v))%><% 
                    Html.RenderPartialFromSkin("PostListMedium"); 
                    %><%=Html.PostListPager((IPageOfItems<Post>)Model.Items, (k,v) => Model.Localize(k,v)) %>
                </div>
                <div class="secondary"><% 
                    Html.RenderPartialFromSkin("SideBar"); %>
                </div>
            </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadCustom" runat="server"><%
    Html.RenderFeedDiscoveryRss(string.Format("{0} (RSS)", Model.Site.DisplayName), Url.Posts("RSS"));
    Html.RenderFeedDiscoveryAtom(string.Format("{0} (ATOM)", Model.Site.DisplayName), Url.Posts("ATOM"));
    Html.RenderFeedDiscoveryRss("All Comments (RSS)", Url.Comments("RSS"));
    Html.RenderFeedDiscoveryAtom("All Comments (ATOM)", Url.Comments("ATOM"));
    Html.RenderRsd();
    Html.RenderLiveWriterManifest(); %>
</asp:Content>