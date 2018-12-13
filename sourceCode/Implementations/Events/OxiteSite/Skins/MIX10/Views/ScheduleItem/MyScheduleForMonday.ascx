<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<div class="timeslot">
	<h3>8:00 AM</h3><div class="timeslotdesc">Registration Opens</div>
</div>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2009, 11, 16, 10, 00, 00), new DateTime(2009, 11, 16, 17, 45, 00)),
        "Workshops"),
        Model.Items,
        Model
        )
    ); %>
<div class="timeslot">
	<h3>12:00 PM - 1:15 PM</h3><div class="timeslotdesc">Lunch</div>
</div>
<div class="timeslot">
	<h3>1:15 PM - 5:45 PM</h3><div class="timeslotdesc">Workshops (continued)</div>
</div>
<div class="timeslot">
	<h3>6:30 PM - 7:30 PM</h3><div class="timeslotdesc">Movie Screening: The Visual Studio Documentary</div>
</div>
