<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Admin.master" Inherits="System.Web.Mvc.ViewPage<OxiteModelItem<Oxite.Models.Page>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="post addPage" id="post">
    <% Html.RenderPartialFromSkin("ItemEditForm"); %>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="HeadCssFiles"><%
    Html.RenderCssFile("jquery.css"); %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptVariablesPre">
    <script type="text/javascript">
        window.calImgPath = "<%=Url.CssPath("/images/calendar.png", ViewContext) %>";
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Scripts"><%
    Html.RenderScriptTag("base.js");
    Html.RenderScriptTag("jquery-ui-20081126-1.5.2.js", "jquery-ui-20081126-1.5.2.min.js"); %>
</asp:Content>