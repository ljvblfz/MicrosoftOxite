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
            <tr>
        		<td class="time">9:00<span>am</span> – 12:30<span>pm</span></td>
                <td class="event"><a href="/workshops">Workshops</a></td>
                <td class="location"></td>
        	</tr>
        	<tr>
        		<td class="time">12:30<span>pm</span> – 1:30<span>pm</span></td>
                <td class="event">Lunch</td>
                <td class="location"></td>
        	</tr>
        	<tr class="last">
        		<td class="time">1:30<span>pm</span> – 5:00<span>pm</span></td>
                <td class="event"><a href="/workshops">Workshops</a></td>
                <td class="location">&nbsp;</td>
        	</tr>

        </tbody></table>
