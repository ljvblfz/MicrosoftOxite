<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItemItems<Speaker, ScheduleItem>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.ViewModels"%>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyTag" runat="server" ><body id="speaker"></asp:Content>
<asp:Content ID="description" ContentPlaceHolderID="MetaDescription" runat="server"><%=Html.PageDescription(Model.Item.Bio.CleanHtml()) %></asp:Content>
<asp:Content ID="searchtags" ContentPlaceHolderID="SearchTags" runat="server"><%=Html.SearchTag("Section", "Speakers", false)%>
<%=Html.SearchTag("Title", Model.Item.DisplayName, true)%>
<link rel="canonical" href="<%=Url.AbsolutePath(Url.Speaker(Model.Item)) %>" /></asp:Content>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1><%=Model.Item.DisplayName %></h1>
<% if (!String.IsNullOrEmpty(Model.Item.Bio))
 {%>
<p class="bio"><%
     string speakerImage = Url.SpeakerImage(Model.Item, "lrg");

     if (speakerImage != null)
     {
%>
<a href="<%=Url.Speaker(Model.Item)%>" title="<%=Model.Item.DisplayName%>" class="headshot">
<%=Html.Image(speakerImage, Model.Item.DisplayName, null)%>
</a>
<%
     }

%>
<%=Model.Item.Bio%></p><%
 }
   if (Model.Items.Count() > 0) { %>
    <h2><%=Model.Localize("Speaker.SessionsHeader", "Sessions I'm presenting at:")%></h2>
    <% Html.RenderPartialFromSkin("ScheduleItemList", new OxiteViewModelItems<ScheduleItem>(Model.Items, Model)); %><%
} %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Speakers"), Model.Item.DisplayName) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js");
    Html.RenderScriptTag("Sessions.js?ver=2"); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="bodyTag" runat="server" ><body id="speaker"></asp:Content>
