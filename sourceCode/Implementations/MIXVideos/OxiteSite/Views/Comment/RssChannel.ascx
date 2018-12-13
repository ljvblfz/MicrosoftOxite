<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelList<ParentAndChild<PostBase, Comment>>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
    <channel>
        <title><%=Html.PageTitle(Model.Container.Name, "Comments")%></title>
        <description><%=Model.Site.Description %></description>
        <link><%=Url.Container(Model.Container) %></link>
        <language><%=Model.Site.LanguageDefault %></language>
        <image>
            <url><%=Url.AbsolutePath(Url.AppPath(Model.Site.FavIconUrl.Replace(".ico", ".png"))) %></url>
            <title><%=Model.Site.DisplayName %></title>
            <link><%=Url.Container(Model.Container)%></link>
            <width>64</width>
            <height>64</height>
        </image><%
    foreach (ParentAndChild<PostBase, Comment> pac in Model.List)
    {
        Html.RenderPartialFromSkin("RssChannelItem", new OxiteModelPartial<ParentAndChild<PostBase, Comment>>(Model, pac));
    } %>
    </channel>