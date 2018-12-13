<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelPartial<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="MIXVideos.Oxite.Extensions" %>
<h4 class="downloads"><%=Model.RootModel.Localize("Downloads")%>:</h4><%=Html.UnorderedList(Model.PartialModel.Files.Where(f => f.TypeName != "WMVStreaming" && f.TypeName != "WMVStreamingOnly" && f.TypeName != "Preview Image (Medium)" && f.TypeName != "Preview Image (Large)").OrderBy(f => f.TypeName), (f, i) => Html.Link(f.GetDisplayName(), f.Url.ToString()), "downloads", f => f.TypeName.CleanCssClassName(), null)%>
