<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
    <channel>
        <title><%=Html.PageTitle("Sessions") %></title>
        <description><%=Model.Site.Description %></description>
        <link><%=Url.AbsolutePath(Url.Sessions()) %></link>
        <language><%=Model.Site.LanguageDefault %></language>
        <image>
            <url><%=Url.AbsolutePath(Url.AppPath(Model.Site.FavIconUrl.Replace(".ico", ".png"))) %></url>
            <title><%=Model.Site.DisplayName + " Sessions" %></title>
            <link><%=Url.AbsolutePath(Url.Sessions()) %></link>
            <width>64</width>
            <height>64</height>
        </image><%
    foreach (ScheduleItem scheduleItem in Model.Items)
    {
        Html.RenderPartialFromSkin("RssChannelItem", new OxiteViewModelPartial<ScheduleItem>(Model, scheduleItem));
    } %>
    </channel>