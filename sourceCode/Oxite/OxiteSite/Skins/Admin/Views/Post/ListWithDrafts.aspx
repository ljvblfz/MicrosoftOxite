<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
            <div class="sections">
                <div class="lone">
                    <%=Html.PageState((IPageOfItems<Post>)Model.Items, (k, v) => Model.Localize(k, v)) %><% 
                    Html.RenderPartialFromSkin("PostListMedium");
                    %><%=Html.PostListPager((IPageOfItems<Post>)Model.Items, (k,v) => Model.Localize(k,v)) %>
                </div>
            </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
    <h2><%=Model.Localize("Posts") %></h2>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>