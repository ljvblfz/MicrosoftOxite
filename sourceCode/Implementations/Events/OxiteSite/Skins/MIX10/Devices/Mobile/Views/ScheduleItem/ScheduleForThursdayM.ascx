<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>

<table id="schedule" cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td class="time">7:00am</td>
		<td class="desc">Registration Opens</td>
	</tr>
	<tr>
		<td class="time">8:30am - 12:30am</td>
		<td class="desc"><a href='/Sessions'>Sessions</a></td>
	</tr>
	<tr>
		<td class="time">9:30am - 4:00pm</td>
		<td class="desc">The Big Room</td>
	</tr>
	<tr>
		<td class="time">12:30am - 1:30pm</td>
		<td class="desc">Lunch</td>
	</tr>
	<tr>
		<td class="time">12:45am - 1:30pm</td>
		<td class="desc"><a href='/Sessions'>Lunch Sessions</a></td>
	</tr>
	<tr>
		<td class="time">1:30pm - 4:00pm</td>
		<td class="desc"><a href='/Sessions'>Sessions</a></td>
	</tr>
	<tr>
		<td class="time">4:00pm</td>
		<td class="desc">Conference Ends</td>
	</tr>
</table>
<div class="pagination">
	<a href="/Schedule/Wednesday">&#0171; Previous Day</a>
</div>