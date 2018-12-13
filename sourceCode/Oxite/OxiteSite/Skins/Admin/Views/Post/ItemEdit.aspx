<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<PostInput>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="sections">
        <div class="lone post editPost" id="post">
            <% Html.RenderPartialFromSkin("ItemEdit"); %>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="HeadCssFiles"><%
    Html.RenderCssFile("jquery.css");
    Html.RenderCssFile("markitup/skins/simple/style.css");
    Html.RenderCssFile("markitup/sets/html/style.css"); %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptVariablesPre">
    <script type="text/javascript">
        window.__getDateTimePath = "<%=Url.GetDateTime() %>";
        window.__markitupPreviewTemplatePath = "<%=Url.CssPath("markitup/templates/preview.html", ViewContext) %>";
        window.__markitupPreviewCssPath = "<%=Url.CssPath("base.css", ViewContext) %>";
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Post.Edit", "Edit Post"), Model.Item.Title)%>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
    <h2><%=Html.Link(
            Model.Localize("Posts"),
            Url.PostsWithDrafts(),
            new { @class = "posts" }
            ) %> &gt; <%=Model.Localize("Posts.Edit", "Edit") %></h2>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Scripts"><%
    Html.RenderScriptTag("base.js");
    Html.RenderScriptTag("markitup/jquery.markitup.js", "markitup/jquery.markitup.pack.js"); %>
</asp:Content>