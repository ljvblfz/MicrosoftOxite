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
            <tr>
        		<td class="time">9:00<span>am</span> – 10:00<span>am</span></td>
                <td class="event"><a href="/Sessions">Sessions</a></td>
                <td class="location">&nbsp;</td>
        	</tr>
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
        	<tr>
        		<td class="time">10:30<span>am</span> – 11:30<span>am</span></td>
                <td class="event"><a href="/Sessions">Sessions</a></td>
                <td class="location">&nbsp;</td>
        	</tr>
            <tr>
        		<td class="time">11:30<span>pm</span> – 12:00<span>pm</span></td>
                <td class="event">Break (pick up box lunch)</td>
                <td class="location">&nbsp;</td>
        	</tr>
        	<tr>
        		<td class="time">12:00<span>pm</span> – 1:00<span>pm</span></td>
                <td class="event"><a href="/Sessions">Sessions</a></td>
                <td class="location">&nbsp;</td>
        	</tr>
        	<tr>
        		<td class="time">1:00<span>pm</span> – 1:30<span>pm</span></td>
                <td class="event">Break</td>
                <td class="location">&nbsp;</td>
        	</tr>
        	<tr> 
        		<td class="time">1:30<span>pm</span> – 2:30<span>pm</span></td>
                <td class="event"><a href="/Sessions">Sessions</a></td>
                <td class="location">&nbsp;</td>
        	</tr>
        	<tr>
        		<td class="time">2:30<span>pm</span> – 3:00<span>pm</span></td>
                <td class="event">Break</td>
                <td class="location">&nbsp;</td>
        	</tr>
        	<tr> 
        		<td class="time">3:00<span>pm</span> – 4:00<span>pm</span></td>
                <td class="event"><a href="/Sessions">Sessions</a></td>
                <td class="location">&nbsp;</td>
        	</tr>
        	<tr class="last">
        		<td class="time">4:00<span>pm</span></td>
                <td class="event">Conference Ends</td>
                <td class="location">&nbsp;</td>
        	</tr>
        </tbody></table>
