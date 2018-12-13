<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelPartial<ParentAndChild<PostBase, Comment>>>" %>
<%@ Import Namespace="Oxite.Extensions" %><%
    ParentAndChild<PostBase, Comment> pac = Model.PartialModel;
    string postUrl = Url.AbsolutePath(Url.Comment(pac.Parent as Post, pac.Child)).Replace("%23", "#"); %>
        <item>
            <dc:creator><%=Html.Encode(pac.Child.Creator.Name) %></dc:creator>
            <title>RE: <%=Html.Encode(pac.Parent.Title) %></title>
            <description><%=Html.Encode(pac.Child.Body) %></description>
            <link><%=postUrl %></link>
            <guid isPermaLink="true"><%=postUrl %></guid>
            <pubDate><%=pac.Child.Created.Value.ToStringForFeed()%></pubDate>
        </item>