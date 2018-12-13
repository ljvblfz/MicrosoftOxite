<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteModelList<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
            <div id="windowsSearch"><%=Html.OpenSearchOSDXLink() %></div>
            <form method="get" action="" class="search main">
                <div class="main search">
                    <div class="search-box">
                        <fieldset>
                            <label for="search_term"><%=Model.Localize("search_term", "Search this site...")%></label>
                            <input id="search_term" name="Term" class="text" type="text" size="42" value="<%=Request["term"].CleanAttribute() %>" />
                            <input class="button" type="submit" value="Search" />
                        </fieldset>
                    </div>
                    <%=Html.PageState((IPageOfList<Post>)Model.List, (k, v) => Model.Localize(k, v)) %><%
                    Html.RenderPartialFromSkin("PostListMedium");
                    %><%=Html.PostListBySearchPager((IPageOfList<Post>)Model.List, (k, v) => Model.Localize(k, v)) %>
                </div>
            </form>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Search")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>
