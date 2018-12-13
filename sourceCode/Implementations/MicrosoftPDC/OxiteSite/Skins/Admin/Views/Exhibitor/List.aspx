<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Oxite.Modules.Conferences.Models.Exhibitor>>" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="sections">
        <a href="<%= Url.EditExhibitor("New") %>">
                <%= Html.SkinImage("/images/add.png", Model.Localize("add"), null)%>
                <%= Html.Link(Model.Localize("Exhibitor.Add", "Add new exhibitor"), Url.AddExhibitor(), new{ @class="text" }) %>
        </a>
        <div class="lone">
            <% Html.RenderPartialFromSkin("List"); %>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
    <h2><%=Model.Localize("Exhibitors") %></h2>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>