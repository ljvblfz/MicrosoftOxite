<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
        <h2>Tuesday</h2>
		<!-- with sessions -->
        <table id="day2" class="" cellpadding="0" cellspacing="0">

        	<tbody>
        	<tr>
        		<td class="time">7:30<span>am</span> – 6:00<span>pm</span></td>
                <td class="event">Registration</td>
                <td class="location">&nbsp;</td>
        	</tr>
            <tr>
        		<td class="time">7:30<span>am</span> – 9:00<span>am</span></td>
                <td class="event">Breakfast</td>
                <td class="location">&nbsp;</td>
        	</tr>
            <tr>
        		<td class="time">9:00<span>am</span> – 11:00<span>am</span></td>
                <td class="event">Keynote</td>
                <td class="location">&nbsp;</td>
        	</tr>
            <tr>
        		<td class="time">11:00<span>am</span> – 11:30<span>am</span></td>
                <td class="event">Break</td>
                <td class="location">&nbsp;</td>
        	</tr>
            <tr>
        		<td class="time">11:00<span>am</span> – 6:00<span>pm</span></td>
                <td class="event"><a href="/Social">The Commons</a></td>
                <td class="location">&nbsp;</td>
        	</tr>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2010, 3, 16, 11, 30, 00), new DateTime(2010, 3, 16, 12, 30, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
            <tr>
        		<td class="time">12:30<span>pm</span> – 1:30<span>pm</span></td>
                <td class="event">Lunch</td>
                <td class="location">&nbsp;</td>
        	</tr>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2010, 3, 16, 13, 30, 00), new DateTime(2010, 3, 16, 14, 30, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
        	<tr>
        		<td class="time">2:30<span>pm</span> – 3:00<span>pm</span></td>
                <td class="event">Break</td>
                <td class="location">&nbsp;</td>
        	</tr>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2010, 3, 16, 15, 00, 00), new DateTime(2010, 3, 16, 16, 00, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
        	<tr>
        		<td class="time">4:00<span>pm</span> – 4:30<span>pm</span></td>
                <td class="event">Break</td>
                <td class="location">&nbsp;</td>
        	</tr>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2010, 3, 16, 16, 30, 00), new DateTime(2010, 3, 16, 17, 30, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
        	<tr class="last">
        		<td class="time">9:00<span>pm</span> – 1:00<span>am</span></td>
                <td class="event">Attendee Party</td>
                <td class="location">LAX</td>
        	</tr>
        </tbody></table>
