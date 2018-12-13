<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelList<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
    <channel>
        <title><%=Html.PageTitle(Model.Container.Name) %></title>
        <description><%=Model.Site.Description %></description>
        <link><%=Url.AbsolutePath(Url.Container(Model.Container)) %></link>
        <language><%=Model.Site.LanguageDefault %></language>
        <image>
            <url><%=Url.AbsolutePath(Url.AppPath(Model.Site.FavIconUrl.Replace(".ico", ".png"))) %></url>
            <title><%=Model.Site.DisplayName %></title>
            <link><%=Url.AbsolutePath(Url.Container(Model.Container)) %></link>
            <width>64</width>
            <height>64</height>
        </image><%
    foreach (Post post in Model.List)
    {
        Html.RenderPartialFromSkin("RssChannelItem", new OxiteModelPartial<Post>(Model, post));
    } %>
    </channel>