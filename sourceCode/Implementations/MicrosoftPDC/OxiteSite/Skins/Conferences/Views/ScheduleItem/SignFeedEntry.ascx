<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<ScheduleItem>>" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
<%@ Import Namespace="Oxite.Modules.Tags.Models" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions" %><%
    ScheduleItem scheduleItem = Model.PartialModel;%>    
            <topic>
                <topicid><%=scheduleItem.ID %></topicid>
                <created><%=scheduleItem.Created %></created>
                <modified><%=scheduleItem.Modified %></modified>
                <topiccode><%=scheduleItem.Code %></topiccode>
                <title><%=Html.Encode(scheduleItem.Title)%></title>
                <description><%if (!String.IsNullOrEmpty(scheduleItem.Body))
                               {%><![CDATA[<%=Html.Encode(scheduleItem.Body) %>]]><% } %></description>
                <categories>
                <%
                    foreach (ScheduleItemTag tag in scheduleItem.Tags)
                    { %>                    
                <category>
                    <parentname>Tag</parentname>
                    <category><![CDATA[<%=tag.Name %>]]></category>
                </category>
            <%} %>                        
                </categories>
                <resources>
                </resources>
                <associatedtopics>
                </associatedtopics>
                <sessions>                    
                    <session>
                        <sessionid><%=scheduleItem.ID %></sessionid>
                        <start><%if (scheduleItem.Start != scheduleItem.End) {%><%=scheduleItem.Start%><%} %></start>
                        <finish><%if (scheduleItem.Start != scheduleItem.End) {%><%=scheduleItem.End%><%} %></finish>
                        <room><![CDATA[<%if (!String.IsNullOrEmpty(scheduleItem.Location)) {%><%=scheduleItem.Location%><%} %>]]></room>
                        <speakers>
                        <%
                    foreach (Speaker speaker in scheduleItem.Speakers)
                    {
                        %>
                            <speaker>
                                <firstname><%=speaker.FirstName %></firstname>
                                <lastname><%=speaker.LastName %></lastname>
                                <%if (!String.IsNullOrEmpty(speaker.Bio)){%>
                                <description><![CDATA[<%=Html.Encode(speaker.Bio) %>]]></description> <%} %>
                            </speaker>    
                        
                        <%
                        
                    } %>
                        </speakers>
                    </session>                    
                </sessions>
            </topic>