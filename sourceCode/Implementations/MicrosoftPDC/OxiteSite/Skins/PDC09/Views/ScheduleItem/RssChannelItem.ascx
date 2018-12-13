<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
<%@ Import Namespace="Oxite.Modules.Tags.Models" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions" %><%
    ScheduleItem scheduleItem = Model.PartialModel;
    string scheduleItemUrl = Url.AbsolutePath(Url.Session(scheduleItem)); %>
        <item>
            <dc:creator>Microsoft PDC09</dc:creator>
            <title><%=Html.Encode(scheduleItem.Title)%></title>
            <description><%=Html.Encode(scheduleItem.Body)%></description>
            <link><%=scheduleItemUrl%></link>
            <guid isPermaLink="true"><%=scheduleItemUrl%></guid>
            <pubDate><%=scheduleItem.Modified.ToStringForFeed()%></pubDate><%
        foreach (ScheduleItemTag tag in scheduleItem.Tags)
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
               ViewContext.RouteData.GetRequiredString("fileFormat");
                if (string.IsNullOrEmpty(fileFormat))
                    fileFormat = "WMVHigh";
                
                File enclosure = files.Where(
                    f => string.Compare(f.TypeName,
                        fileFormat, true) == 0).FirstOrDefault();
                if (enclosure != null)
                {
                    %>
            <enclosure url="<%=enclosure.Url.ToString() %>" length="<%=enclosure.SizeInBytes > 0 ? enclosure.SizeInBytes : 1 %>" type="<%=enclosure.MimeType %>" />
                    <%
                }
           }
            
            %>
        </item>