<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
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
    foreach (Post post in Model.Items)
    {
        Html.RenderPartialFromSkin("RssChannelItem", new OxiteViewModelPartial<Post>(Model, post));
    } %>
    </channel>