<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>

        <h2>Wednesday</h2>
		
        <table id="day3" class="" cellpadding="0" cellspacing="0">

        	<tbody>
        	<tr>
        		<td class="time">8:00<span>am</span> – 4:00<span>pm</span></td>
                <td class="event">Registration</td>
                <td class="location">&nbsp;</td>
        	</tr>
            <tr>
        		<td class="time">8:00<span>am</span> – 9:00<span>am</span></td>
                <td class="event">Breakfast</td>
                <td class="location">&nbsp;</td>
        	</tr>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2010, 3, 17, 9, 00, 00), new DateTime(2010, 3, 17, 10, 00, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
            <tr>
        		<td class="time">9:00<span>am</span> – 4:00<span>pm</span></td>
                <td class="event"><a href="/Social">The Commons</a></td>
                <td class="location">&nbsp;</td>
        	</tr>
            <tr>
        		<td class="time">10:00<span>am</span> – 10:30<span>am</span></td>
                <td class="event">Break</td>
                <td class="location">&nbsp;</td>
        	</tr>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2010, 3, 17, 10, 30, 00), new DateTime(2010, 3, 17, 11, 30, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
            <tr>
        		<td class="time">11:30<span>pm</span> – 12:00<span>pm</span></td>
                <td class="event">Break (pick up box lunch)</td>
                <td class="location">&nbsp;</td>
        	</tr>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2010, 3, 17, 12, 00, 00), new DateTime(2010, 3, 17, 13, 00, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
        	<tr>
        		<td class="time">1:00<span>pm</span> – 1:30<span>pm</span></td>
                <td class="event">Break</td>
                <td class="location">&nbsp;</td>
        	</tr>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2010, 3, 17, 13, 30, 00), new DateTime(2010, 3, 17, 14, 30, 00)),
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
        new DateRangeAddress(new DateTime(2010, 3, 17, 15, 00, 00), new DateTime(2010, 3, 17, 16, 00, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
        	<tr class="last">
        		<td class="time">4:00<span>pm</span></td>
                <td class="event">Conference Ends</td>
                <td class="location">&nbsp;</td>
        	</tr>
        </tbody></table>
