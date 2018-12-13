<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<IEnumerable<File>>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<h3><%=Model.Localize("Downloads")%></h3>
<%=Html.UnorderedList(Model.PartialModel.Where(f => f.TypeName != "WMVStreaming" &&
    f.TypeName != "Smooth" && !f.TypeName.Contains("Preview")).OrderBy(f => f.TypeName), (f, i) => Html.Link(f.GetDisplayName(), f.Url.ToString()), "downloads", f => f.TypeName.CleanCssClassName(), null)%>
