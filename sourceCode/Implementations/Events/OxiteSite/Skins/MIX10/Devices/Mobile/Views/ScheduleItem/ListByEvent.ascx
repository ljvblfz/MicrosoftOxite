<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>

<% Html.RenderPartialFromSkin(
       "ScheduleItemList",
       new OxiteViewModelItems<ScheduleItem>(
           Model.Items,
           Model
           )
       ); %>
       
<div id="pagingContainer">
	<%=Html.MobileSessionListPager((IPageOfItems<ScheduleItem>)Model.Items, (k,v) => Model.Localize(k,v)) %>
</div>