<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ContentItemInput>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.CMS.Models"%>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="sections">
        <div class="lone contentItem editGlobalContent">
            <% Html.RenderPartialFromSkin("GlobalContentEdit"); %>
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
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Content.Edit", "Edit Global Content"))%>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
    <h2><%=Html.Link(
            Model.Localize("Content"),
            "#",
            new { @class = "content" }
            ) %> &gt; <%=Model.Localize("Content.Edit", "Edit") %></h2>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Scripts"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>