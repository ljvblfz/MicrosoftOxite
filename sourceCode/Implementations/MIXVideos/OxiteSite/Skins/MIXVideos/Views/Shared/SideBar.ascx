<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="MIXVideos.Oxite.ViewModels" %>
<div id="discovermore">
<% 
    Html.RenderPartialFromSkin("Search");
    Html.RenderPartialFromSkin("TagCloud", new OxiteModelPartial<TagCloudViewModel>(Model, Model.GetModelItem<TagCloudViewModel>()));
    Html.RenderPartialFromSkin("SidebarVideo", new OxiteModelPartial<SidebarViewModel>(Model, Model.GetModelItem<SidebarViewModel>()));
    Html.RenderPartialFromSkin("ConferenceList"); %>
</div>