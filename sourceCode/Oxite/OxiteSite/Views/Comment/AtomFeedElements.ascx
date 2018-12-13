<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<PostComment>>" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
  <title type="html"><%=Html.PageTitle(Model.Container.Name, "Comments") %></title>
  <icon><%=Url.AbsolutePath(Url.AppPath(Model.Site.FavIconUrl)) %></icon>
  <logo><%=Url.AbsolutePath(Url.AppPath(Model.Site.FavIconUrl.Replace(".ico", ".png"))) %></logo>
  <updated><%=XmlConvert.ToString(Model.Items.First().Created, XmlDateTimeSerializationMode.RoundtripKind) %></updated><%
    if (!string.IsNullOrEmpty(Model.Site.Description))
    { %>
  <subtitle type="html"><%=Html.Encode(Model.Site.Description)%></subtitle><%
    } %>
  <id><%=Context.Request.Url.ToString().ToLower() %></id>
  <link rel="alternate" type="text/html" hreflang="<%=Model.Site.LanguageDefault %>" href="<%=Url.Container(Model.Container).ToLower()%>"/>
  <link rel="self" type="application/atom+xml" href="<%=Context.Request.Url.ToString() %>"/>
  <generator uri="<%=Url.Oxite() %>" version="1.0">Oxite</generator>
  <logo><%=Url.AbsolutePath(Url.AppPath(Model.Site.FavIconUrl.Replace(".ico", ".png"))) %></logo><%
    foreach (PostComment c in Model.Items)
    {
        Html.RenderPartialFromSkin("AtomFeedEntry", new OxiteViewModelPartial<PostComment>(Model, c));
    } %>