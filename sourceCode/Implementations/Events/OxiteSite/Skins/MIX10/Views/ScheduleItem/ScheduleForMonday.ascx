<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
        <h2>Monday</h2>
		
        <table id="day1" class="" cellpadding="0" cellspacing="0">
        	<tbody><tr>
        		<td class="time">7:00<span>am</span> – 7:30<span>pm</span></td>
                <td class="event">Registration</td>
                <td class="location">&nbsp;</td>
        	</tr>
            <tr>
        		<td class="time">7:00<span>am</span> – 9:00<span>am</span></td>
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
        		<td class="time">11:00<span>am</span> – 5:00<span>pm</span></td>
                <td class="event"><a href="/Social">The Commons</a></td>
                <td class="location">&nbsp;</td>
        	</tr>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2010, 3, 15, 11, 30, 00), new DateTime(2010, 3, 15, 12, 30, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
            <tr>
        		<td class="time">12:30<span>pm</span> – 2:00<span>pm</span></td>
                <td class="event">Lunch</td>
                <td class="location">&nbsp;</td>
        	</tr>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2010, 3, 15, 14, 00, 00), new DateTime(2010, 3, 15, 15, 00, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
        	<tr>
        		<td class="time">3:00<span>pm</span> – 3:30<span>pm</span></td>
                <td class="event">Break</td>
                <td class="location">&nbsp;</td>
        	</tr>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2010, 3, 15, 15, 30, 00), new DateTime(2010, 3, 15, 16, 30, 00)),
        "Sessions"),
        Model.Items,
        Model
        )
    ); %>
        	<tr>
        		<td class="time">5:00<span>pm</span> – 6:30<span>pm</span></td>
                <td class="event">Ask The Experts</td>
                <td class="location">&nbsp;</td>
        	</tr>
        	<tr class="last">
        		<td class="time">6:30<span>pm</span> – 7:30<span>pm</span></td>
                <td class="event">Phizzpop Design Challenge<br />Championship Round</td>
                <td class="location">&nbsp;</td>
        	</tr>
        </tbody></table>
