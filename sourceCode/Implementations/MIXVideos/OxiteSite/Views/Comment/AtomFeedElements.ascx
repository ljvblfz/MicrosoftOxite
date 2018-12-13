<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelList<ParentAndChild<PostBase, Comment>>>" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="Oxite.Extensions" %>
  <title type="html"><%=Html.PageTitle(Model.Container.Name, "Comments") %></title>
  <icon><%=Url.AbsolutePath(Url.AppPath(Model.Site.FavIconUrl)) %></icon>
  <logo><%=Url.AbsolutePath(Url.AppPath(Model.Site.FavIconUrl.Replace(".ico", ".png"))) %></logo>
  <updated><%=XmlConvert.ToString(Model.List.First().Child.Created.Value, XmlDateTimeSerializationMode.RoundtripKind) %></updated><%
    if (!string.IsNullOrEmpty(Model.Site.Description))
    { %>
  <subtitle type="html"><%=Html.Encode(Model.Site.Description)%></subtitle><%
    } %>
  <id><%=Context.Request.Url.ToString().ToLower() %></id>
  <link rel="alternate" type="text/html" hreflang="<%=Model.Site.LanguageDefault %>" href="<%=Url.Container(Model.Container).ToLower()%>"/>
  <link rel="self" type="application/atom+xml" href="<%=Context.Request.Url.ToString() %>"/>
  <generator uri="<%=Url.Oxite() %>" version="1.0">Oxite</generator>
  <logo><%=Url.AbsolutePath(Url.AppPath(Model.Site.FavIconUrl.Replace(".ico", ".png"))) %></logo><%
    foreach (ParentAndChild<PostBase, Comment> pac in Model.List)
    {
        Html.RenderPartialFromSkin("AtomFeedEntry", new OxiteModelPartial<ParentAndChild<PostBase, Comment>>(Model, pac));
    } %>