<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
        <h2>Sunday</h2>
		
        <table id="day0" class="" cellpadding="0" cellspacing="0">

        	<tbody>
        	<tr>
        		<td class="time">8:00<span>am</span> – 8:00<span>pm</span></td>
                <td class="event">Registration</td>
                <td class="location">&nbsp;</td>
        	</tr>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2010, 3, 14, 9, 00, 00), new DateTime(2010, 3, 14, 12, 30, 00)),
        "Workshops"),
        Model.Items,
        Model
        )
    ); %>
        	<tr>
        		<td class="time">12:30<span>pm</span> – 1:30<span>pm</span></td>
                <td class="event">Lunch</td>
                <td class="location"></td>
        	</tr>
<% Html.RenderPartialFromSkin(
    "ScheduleItemTimeSlot",
    new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
        new TimeslotDescription(
        new DateRangeAddress(new DateTime(2010, 3, 14, 13, 30, 00), new DateTime(2010, 3, 14, 17, 00, 00)),
        "Workshops"),
        Model.Items,
        Model
        )
    ); %>

        </tbody></table>
