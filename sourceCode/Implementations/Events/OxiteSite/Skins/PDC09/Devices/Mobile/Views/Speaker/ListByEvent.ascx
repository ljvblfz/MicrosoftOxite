<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<Speaker>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>

<div id="speakers">
<% Html.RenderPartialFromSkin(
       "SpeakerItemList",
       new OxiteViewModelItems<Speaker>(
           Model.Items,
           Model
           )
       ); %>
</div>       
<div id="pagingContainer">
	<%=Html.MobileSpeakerListPager((IPageOfItems<Speaker>)Model.Items, (k, v) => Model.Localize(k, v))%>
</div>
