<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelPartial<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="MIXVideos.Oxite.Extensions" %><%
    File enclosure = Model.PartialModel.Files.GetMediaForFeed();
                                                        
    if (enclosure != null) { %>
            <enclosure url="<%=enclosure.Url.ToString() %>" length="<%=enclosure.SizeInBytes > 0 ? enclosure.SizeInBytes : 1 %>" type="<%=enclosure.MimeType %>" /><%

        string[] mediaTypes = new string[] { "WMV", "WMVHigh", "WMVStreaming", "WMA", "MP3", "MP4" };
        IEnumerable<File> mediaFiles = Model.PartialModel.Files.Where(f => mediaTypes.Contains(f.TypeName, StringComparer.OrdinalIgnoreCase));

        if (mediaFiles.Count() > 1) { %>
            <media:group><%
            foreach (File mediaFile in mediaFiles) { %>
                <media:content url="<%=mediaFile.Url.ToString() %>" expression="full" fileSize="<%=mediaFile.SizeInBytes > 0 ? mediaFile.SizeInBytes : 1 %>" type="<%=mediaFile.MimeType %>" medium="<%=mediaFile.MimeType.Split('/')[0] %>" /><%
            } %>
            </media:group><%
        }
    } %>