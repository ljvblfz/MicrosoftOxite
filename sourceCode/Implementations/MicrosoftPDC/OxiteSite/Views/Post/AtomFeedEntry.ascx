<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<Post>>" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<%@ Import Namespace="Oxite.Modules.Tags.Models" %><%
    Post post = Model.PartialModel;
    string postUrl = Url.AbsolutePath(Url.Post(post)); %>
  <entry>
    <title type="html"><%=Html.Encode(post.Title)%></title>
    <link rel="alternate" type="text/html" href="<%=postUrl %>"/>
    <id><%=postUrl %></id>
    <updated><%=XmlConvert.ToString(post.Modified, XmlDateTimeSerializationMode.Utc)%></updated>
    <published><%=XmlConvert.ToString(post.Published.Value, XmlDateTimeSerializationMode.Utc)%></published>
    <author>
      <name><%=Html.Encode(post.Creator.Name)%></name>
    </author><%
        if (Model.Site.HasMultipleBlogs)
        { %>
    <category term="<%=post.Blog.Name %>" /><%
        }
        foreach (Tag tag in post.Tags)
        { %>
    <category term="<%=tag.Name %>" /><%
        } %>
    <content type="html" xml:lang="<%=Model.Site.LanguageDefault %>">
      <%=Html.Encode(post.Body)%>
    </content>
  </entry>