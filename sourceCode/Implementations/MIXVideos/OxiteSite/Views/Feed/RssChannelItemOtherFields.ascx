<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelPartial<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="MIXVideos.Oxite.Extensions" %><%
    File enclosure = Model.PartialModel.Files.Where(f => string.Compare(f.TypeName, ViewContext.RouteData.GetRequiredString("typeName"), true) == 0).First(); %>
            <enclosure url="<%=enclosure.Url.ToString() %>" length="<%=enclosure.SizeInBytes > 0 ? enclosure.SizeInBytes : 1 %>" type="<%=enclosure.MimeType %>" />