<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<PostComment>>" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %><%
    PostComment c = Model.PartialModel;
    string itemUrl = Url.AbsolutePath(Url.Comment(c)).Replace("%23", "#"); %>
  <entry>
    <title type="html"><%=Html.Encode(c.Post.Title)%></title>
    <link rel="alternate" type="text/html" href="<%=itemUrl%>"/>
    <id><%=itemUrl%></id>
    <updated><%=XmlConvert.ToString(c.Modified, XmlDateTimeSerializationMode.Utc)%></updated>
    <published><%=XmlConvert.ToString(c.Created, XmlDateTimeSerializationMode.Utc)%></published>
    <author>
      <name><%=Html.Encode(c.CreatorName)%></name>
    </author>
    <content type="html" xml:lang="<%=Model.Site.LanguageDefault %>">
      <%=Html.Encode(c.Body)%>
    </content>
  </entry>