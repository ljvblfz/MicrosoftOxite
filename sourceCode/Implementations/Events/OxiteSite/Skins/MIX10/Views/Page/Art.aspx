<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<string>>" MasterPageFile="../Shared/Site.master" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions" %>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1><%=Model.Localize("Art") %></h1>
<div id="primary">
    <ul class="blogBling">
        <h3>Love the Web</h3>
        <li class="banner">
            <p><img alt="Love the Web" src="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_LoveTheWeb_blk_240.jpg") %>"></p>
            <ul class="sizes">
                <li><a href="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_LoveTheWeb_blk_240.jpg") %>">240px x 320px</a></li>
            </ul>
        </li>
        <li class="banner">
            <p><img alt="Love the Web" src="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_LoveTheWeb_grn_240.jpg") %>"></p>
            <ul class="sizes">
                <li><a href="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_LoveTheWeb_grn_240.jpg") %>">240px x 320px</a></li>
            </ul>
        </li>
        <h3>See You</h3>
        <li class="banner">
            <p><img alt="See You" src="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_SeeYou_blk_240.jpg") %>"></p>
            <ul class="sizes">
                <li><a href="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_SeeYou_blk_240.jpg") %>">240px x 320px</a></li>
            </ul>
        </li>
        <li class="banner">
            <p><img alt="See You" src="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_SeeYou_grn_240.jpg") %>"></p>
            <ul class="sizes">
                <li><a href="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_SeeYou_grn_240.jpg") %>">240px x 320px</a></li>
            </ul>
        </li>
        <h3>Vote</h3>
        <li class="banner">
            <p><img alt="Vote" src="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_Vote_blk_240.jpg") %>"></p>
            <ul class="sizes">
                <li><a href="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_Vote_blk_240.jpg") %>">240px x 320px</a></li>
            </ul>
        </li>
        <li class="banner">
            <p><img alt="Vote" src="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_Vote_brn_240.jpg") %>"></p>
            <ul class="sizes">
                <li><a href="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_Vote_brn_240.jpg") %>">240px x 320px</a></li>
            </ul>
        </li>
        <li class="banner">
            <p><img alt="Vote" src="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_Vote_grn_240.jpg") %>"></p>
            <ul class="sizes">
                <li><a href="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_Vote_grn_240.jpg") %>">240px x 320px</a></li>
            </ul>
        </li>
        <h3>Love Fest</h3>
        <li class="banner">
            <p><img alt="Love Fest" src="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_LoveFest_blk_240.jpg") %>"></p>
            <ul class="sizes">
                <li><a href="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_LoveFest_blk_240.jpg") %>">240px x 320px</a></li>
            </ul>
        </li>
        <li class="banner">
            <p><img alt="Love Fest" src="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_LoveFest_brn_240.jpg") %>"></p>
            <ul class="sizes">
                <li><a href="<%= Url.Content("~/Skins/MIX10/Styles/img/Mix10_LoveFest_brn_240.jpg") %>">240px x 320px</a></li>
            </ul>
        </li>
    </ul>
</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Art"))%>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>