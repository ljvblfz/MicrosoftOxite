<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<div class="timeslot">
	<h3>7:30 AM</h3><div class="timeslotdesc">Registration Opens</div>
</div>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2009, 11, 19, 8, 30, 00), new DateTime(2009, 11, 19, 9, 30, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
<div class="timeslot">
	<h3>9:30 AM - 10:00 AM</h3><div class="timeslotdesc">Break</div>
</div>
<div class="timeslot">
	<h3>9:30 AM - 4:00 PM</h3><div class="timeslotdesc">The Big Room</div>
</div>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2009, 11, 19, 10, 00, 00), new DateTime(2009, 11, 19, 11, 00, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
<div class="timeslot">
	<h3>11:00 AM - 11:30 AM</h3><div class="timeslotdesc">Break</div>
</div>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2009, 11, 19, 11, 30, 00), new DateTime(2009, 11, 19, 12, 30, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
<div class="timeslot">
	<h3>12:30 PM - 1:30 PM</h3><div class="timeslotdesc">Lunch</div>
</div>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2009, 11, 19, 12, 45, 00), new DateTime(2009, 11, 19, 13, 30, 00)),
        "Lunch Sessions"),
        Model.Items,
        Model
        )
    ); %>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2009, 11, 19, 13, 45, 00), new DateTime(2009, 11, 19, 14, 45, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
<div class="timeslot">
	<h3>2:45 PM - 3:00 PM</h3><div class="timeslotdesc">Break</div>
</div>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2009, 11, 19, 15, 00, 00), new DateTime(2009, 11, 19, 16, 00, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
<div class="timeslot">
	<h3>4:00 PM</h3><div class="timeslotdesc">Conference Ends</div>
</div>