<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions" %>
  <title type="html"><%=Html.PageTitle("Sessions") %></title>
  <icon><%=Url.AbsolutePath(Url.AppPath(Model.Site.FavIconUrl)) %></icon>
  <logo><%=Url.AbsolutePath(Url.AppPath(Model.Site.FavIconUrl.Replace(".ico", ".png"))) %></logo>
  <updated><%=XmlConvert.ToString(Model.Items.First().Modified, XmlDateTimeSerializationMode.Utc) %></updated><%
    if (!string.IsNullOrEmpty(Model.Site.Description))
    { %>
  <subtitle type="html"><%=Html.Encode(Model.Site.Description)%></subtitle><%
    } %>
  <id><%=Context.Request.Url.ToString().ToLower() %></id>
  <link rel="alternate" type="text/html" hreflang="<%=Model.Site.LanguageDefault %>" href="<%=Url.AbsolutePath(Url.Sessions()).ToLower() %>"/>
  <link rel="self" type="application/atom+xml" href="<%=Context.Request.Url.ToString() %>"/>
  <generator uri="<%=Url.Oxite() %>" version="1.0">Oxite</generator><%
    foreach (ScheduleItem scheduleItem in Model.Items)
    {
        Html.RenderPartialFromSkin("AtomFeedEntry", new OxiteViewModelPartial<ScheduleItem>(Model, scheduleItem));
    } %>