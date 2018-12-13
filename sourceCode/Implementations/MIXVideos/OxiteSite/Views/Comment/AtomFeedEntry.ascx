<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelPartial<ParentAndChild<PostBase, Comment>>>" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="Oxite.Extensions" %><%
    ParentAndChild<PostBase, Comment> pac = Model.PartialModel;
    string itemUrl = Url.AbsolutePath(Url.Comment(pac.Parent as Post, pac.Child)).Replace("%23", "#"); %>
  <entry>
    <title type="html"><%=Html.Encode(pac.Parent.Title)%></title>
    <link rel="alternate" type="text/html" href="<%=itemUrl%>"/>
    <id><%=itemUrl%></id>
    <updated><%=XmlConvert.ToString(pac.Child.Modified.Value, XmlDateTimeSerializationMode.RoundtripKind)%></updated>
    <published><%=XmlConvert.ToString(pac.Child.Created.Value, XmlDateTimeSerializationMode.RoundtripKind)%></published>
    <author>
      <name><%=Html.Encode(pac.Child.Creator.Name)%></name>
    </author>
    <content type="html" xml:lang="<%=Model.RootModel.Site.LanguageDefault %>">
      <%=Html.Encode(pac.Child.Body)%>
    </content>
  </entry>