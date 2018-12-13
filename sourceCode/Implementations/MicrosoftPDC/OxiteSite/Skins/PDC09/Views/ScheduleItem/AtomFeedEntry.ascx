<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<ScheduleItem>>" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
<%@ Import Namespace="Oxite.Modules.Tags.Models" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions" %><%
    ScheduleItem scheduleItem = Model.PartialModel;
    string scheduleItemUrl = Url.AbsolutePath(Url.Session(scheduleItem)); %>
  <entry>
    <title type="html"><%=Html.Encode(scheduleItem.Title)%></title>
    <link rel="alternate" type="text/html" href="<%=scheduleItemUrl %>"/>
    <id><%=scheduleItemUrl %></id>
    <updated><%=XmlConvert.ToString(scheduleItem.Modified, XmlDateTimeSerializationMode.Utc)%></updated>
    <published><%=XmlConvert.ToString(scheduleItem.Created, XmlDateTimeSerializationMode.Utc)%></published>
    <author>
      <name>Microsoft PDC09</name>
    </author><%
        foreach (ScheduleItemTag tag in scheduleItem.Tags)
        { %>
    <category term="<%=tag.Name %>" /><%
        } %>
    <content type="html" xml:lang="<%=Model.Site.LanguageDefault %>">
      <%=Html.Encode(scheduleItem.Body)%>
    </content>
  </entry>