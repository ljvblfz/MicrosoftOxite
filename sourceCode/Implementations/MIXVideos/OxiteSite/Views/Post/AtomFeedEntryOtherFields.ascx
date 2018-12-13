<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelPartial<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="MIXVideos.Oxite.Extensions" %><%
    string[] mediaTypes = new string[] { "WMV", "WMVHigh", "WMVStreaming", "WMA", "MP3", "MP4" };
    IEnumerable<File> mediaFiles = Model.PartialModel.Files.Where(f => mediaTypes.Contains(f.TypeName, StringComparer.OrdinalIgnoreCase));

    foreach (File mediaFile in mediaFiles)
    { %>
                <link rel="enclosure" href="<%=mediaFile.Url.ToString() %>" length="<%=mediaFile.SizeInBytes %>" type="<%=mediaFile.MimeType %>" title="<%=mediaFile.GetDisplayName() %>" /><%
    } %>