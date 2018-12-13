<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
<%@ Import Namespace="Oxite.Modules.Tags.Models" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions" %><%
    ScheduleItem scheduleItem = Model.PartialModel;
    string scheduleItemUrl = Url.AbsolutePath(Url.Session(scheduleItem)); %>
        <item>
            <title><%=Html.Encode(scheduleItem.Title)%></title>
            <itunes:summary><%=Html.Encode(scheduleItem.Body) %></itunes:summary>
            <description><%=Html.Encode(scheduleItem.Body)%></description>
            <link><%=scheduleItemUrl%></link>
            <guid isPermaLink="true"><%=scheduleItemUrl%></guid>
            <pubDate><%=scheduleItem.Modified.ToStringForFeed()%></pubDate><%
        foreach (ScheduleItemTag tag in scheduleItem.Tags.Where(t=>!String.IsNullOrEmpty(t.Name)))
        { %>
            <category><%=tag.Name %></category><%
        } %>
        <% 
            List<File> files;
            string cacheKeyID = Url.Session(scheduleItem);
            files =
                Cache.Get("filesFor:" + cacheKeyID) as List<File>;
            if (files == null)
            {
                files = scheduleItem.Files.ToList();
                Cache.Add("filesFor:" + cacheKeyID, files, null,
                          DateTime.Now.AddHours(1),
                          Cache.NoSlidingExpiration,
                          CacheItemPriority.Normal, null);
            }
            if (files.Count > 0)
            {
                string fileFormat = 
               ViewContext.RouteData.Values["fileFormat"] as String;
                
                if (string.IsNullOrEmpty(fileFormat))
                    fileFormat = "WMVHigh";
                
                File enclosure = files.Where(
                    f => string.Compare(f.TypeName,
                        fileFormat, true) == 0).FirstOrDefault();
                if (enclosure != null)
                { %>
            <enclosure url="<%=enclosure.Url.ToString() %>" length="<%=enclosure.SizeInBytes > 0 ? enclosure.SizeInBytes : 1 %>" type="<%=enclosure.MimeType %>" />
                 <% } } %>
            <evnet:starttime><%= scheduleItem.Start.ToStringForFeed()%></evnet:starttime>
            <evnet:endtime><%= scheduleItem.End.ToStringForFeed()%></evnet:endtime>
            <evnet:location><%= Html.Encode(scheduleItem.Location) %></evnet:location><%
            string authors = "";
            foreach (var speaker in scheduleItem.Speakers)
            {%>
            <evnet:speaker id="<%= Html.Encode(speaker.Name) %>">
                <evnet:displayname><%= Html.Encode(speaker.DisplayName)%></evnet:displayname>
                <evnet:moreinfo><%= Url.AbsolutePath(Url.Speaker(speaker))%></evnet:moreinfo><% if (!String.IsNullOrEmpty(speaker.LargeImage))
                                                                                                 { %>
                <evnet:largeimage><%= Url.AbsolutePath(speaker.LargeImage)%></evnet:largeimage><% } %><% if (!String.IsNullOrEmpty(speaker.SmallImage))
                                                                                                          { %>
                <evnet:smallimage><%= Url.AbsolutePath(speaker.SmallImage)%></evnet:smallimage><% } %><% if (!String.IsNullOrEmpty(speaker.Twitter))
                                                                                                          {%>
                <evnet:twitter><%=speaker.Twitter%></evnet:twitter><% } %>
            </evnet:speaker>
            <%
                authors += speaker.DisplayName + ", ";
            } 
            if (!String.IsNullOrEmpty(authors))
            {
                authors = authors.Substring(0, authors.Length - 2);
                %><dc:creator><%=authors %></dc:creator><itunes:author><%=authors %></itunes:author>
<%
            }
            %>              
        </item>