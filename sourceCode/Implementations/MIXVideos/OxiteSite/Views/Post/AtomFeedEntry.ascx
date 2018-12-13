<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelPartial<Post>>" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="Oxite.Extensions" %><%
    Post post = Model.PartialModel;
    string postUrl = Url.AbsolutePath(Url.Post(post)); %>
  <entry>
    <title type="html"><%=Html.Encode(post.Title)%></title>
    <link rel="alternate" type="text/html" href="<%=postUrl %>"/>
    <id><%=postUrl %></id>
    <updated><%=XmlConvert.ToString(post.Modified.Value, XmlDateTimeSerializationMode.RoundtripKind)%></updated>
    <published><%=XmlConvert.ToString(post.Published.Value, XmlDateTimeSerializationMode.RoundtripKind)%></published>
    <author>
      <name><%=Html.Encode(post.Creator.Name)%></name>
    </author><%
        if (Model.RootModel.Site.HasMultipleAreas)
        { %>
    <category term="<%=post.Area.Name %>" /><%
        }
        foreach (Tag tag in post.Tags)
        { %>
    <category term="<%=tag.Name %>" /><%
        } %>
    <content type="html" xml:lang="<%=Model.RootModel.Site.LanguageDefault %>">
      <%=Html.Encode(post.Body)%>
    </content><%
    Html.RenderPartialFromSkin("AtomFeedEntryOtherFields", Model); %>
  </entry>