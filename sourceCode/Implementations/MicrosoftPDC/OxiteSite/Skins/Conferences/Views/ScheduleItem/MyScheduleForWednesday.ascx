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
        new DateRangeAddress(new DateTime(2009, 11, 18, 8, 30, 00), new DateTime(2009, 11, 18, 10, 30, 00)),
        "Keynote"),
        Model.Items,
        Model
        )
    ); %>
<div class="timeslot">
	<h3>10:30 AM - 11:00 AM</h3><div class="timeslotdesc">Break</div>
</div>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2009, 11, 18, 11, 00, 00), new DateTime(2009, 11, 18, 12, 00, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
<div class="timeslot">
	<h3>11:00 AM - 5:30 PM</h3><div class="timeslotdesc">The Big Room</div>
</div>
<div class="timeslot">
	<h3>12:00 PM - 1:30 PM</h3><div class="timeslotdesc">Lunch</div>
</div>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2009, 11, 18, 12, 30, 00), new DateTime(2009, 11, 18, 13, 15, 00)),
        "Lunch Sessions"),
        Model.Items,
        Model
        )
    ); %>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2009, 11, 18, 13, 30, 00), new DateTime(2009, 11, 18, 14, 30, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
<div class="timeslot">
	<h3>2:30 PM - 3:00 PM</h3><div class="timeslotdesc">Break</div>
</div>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2009, 11, 18, 15, 00, 00), new DateTime(2009, 11, 18, 16, 00, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
<div class="timeslot">
	<h3>4:00 PM - 4:30 PM</h3><div class="timeslotdesc">Break</div>
</div>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2009, 11, 18, 16, 30, 00), new DateTime(2009, 11, 18, 17, 30, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %><div class="timeslot">
	<h3>5:30 PM - 7:00 PM</h3><div class="timeslotdesc">Ask The Experts</div>
</div>
<div class="timeslot">
	<h3>7:00 PM - 9:00 PM</h3><div class="timeslotdesc">GeekFest</div>
</div>