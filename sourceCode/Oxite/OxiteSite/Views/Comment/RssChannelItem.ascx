<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<PostComment>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %><%
    PostComment c = Model.PartialModel;
    string postUrl = Url.AbsolutePath(Url.Comment(c)).Replace("%23", "#"); %>
        <item>
            <dc:creator><%=Html.Encode(c.CreatorName) %></dc:creator>
            <title>RE: <%=Html.Encode(c.Post.Title) %></title>
            <description><%=Html.Encode(c.Body) %></description>
            <link><%=postUrl %></link>
            <guid isPermaLink="true"><%=postUrl %></guid>
            <pubDate><%=c.Created.ToStringForFeed()%></pubDate>
        </item>