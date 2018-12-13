<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>

<table id="schedule" cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td class="time">7:00am</td>
		<td class="desc">Registration Opens</td>
	</tr>
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
	<tr>
		<td class="time">10:30am - 11:00am</td>
		<td class="desc">Break</td>
	</tr>
	<tr>
		<td class="time">11:00am - 12:00pm</td>
		<td class="desc"><a href='/Sessions'>Sessions</a></td>
	</tr>
	<tr>
		<td class="time">11:00am - 5:30pm</td>
		<td class="desc">The Big Room</td>
	</tr>
	<tr>
		<td class="time">12:00am - 1:30pm</td>
		<td class="desc">Lunch</td>
	</tr>
	<tr>
		<td class="time">12:00am - 1:15pm</td>
		<td class="desc"><a href='/Sessions'>Lunch Sessions</a></td>
	</tr>
	<tr>
		<td class="time">1:30pm - 5:30pm</td>
		<td class="desc"><a href='/Sessions'>Sessions</a></td>
	</tr>
	<tr>
		<td class="time">5:30pm - 9:00pm</td>
		<td class="desc">Attendee Social</td>
	</tr>
</table>
<div class="pagination">
	<a href="/Schedule/Tuesday">&#0171; Previous Day</a>
	|
	<a href="/Schedule/Thursday">Next Day &#0187;</a>
</div>