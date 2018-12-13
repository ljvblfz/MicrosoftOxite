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
            <url>http://ecn.channel9.msdn.com/o9/content/images/MIX10_300px.jpg</url>
            <title><%=Model.Site.DisplayName + " Sessions" %></title>
            <link><%=Url.AbsolutePath(Url.Sessions()) %></link>
            <width>300</width>
            <height>300</height>
        </image>
        <itunes:summary><%=Model.Site.Description %></itunes:summary>
        <itunes:author>Microsoft</itunes:author>
        <itunes:subtitle><%=Model.Site.Description %></itunes:subtitle>
        <itunes:image href="http://ecn.channel9.msdn.com/o9/content/images/MIX10_300px.jpg" />
        <itunes:category text="Technology" />
        
        <%
    foreach (ScheduleItem scheduleItem in Model.Items)
    {
        Html.RenderPartialFromSkin("RssChannelItem", new OxiteViewModelPartial<ScheduleItem>(Model, scheduleItem));
    } %>
    </channel>