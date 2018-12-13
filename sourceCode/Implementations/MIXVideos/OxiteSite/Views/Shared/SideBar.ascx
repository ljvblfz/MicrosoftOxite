<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<% 
    Html.RenderPartialFromSkin("Search");
    Html.RenderPartialFromSkin("Archives", new OxiteModelPartial<ArchiveViewModel>(Model, Model.GetModelItem<ArchiveViewModel>())); %>