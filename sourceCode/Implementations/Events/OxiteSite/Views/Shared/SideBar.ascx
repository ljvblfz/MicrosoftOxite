<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.ViewModels" %>
<% 
Html.RenderPartialFromSkin("Search");
Html.RenderPartialFromSkin("Archives", new OxiteViewModelPartial<ArchiveViewModel>(Model, Model.GetModelItem<ArchiveViewModel>())); %>