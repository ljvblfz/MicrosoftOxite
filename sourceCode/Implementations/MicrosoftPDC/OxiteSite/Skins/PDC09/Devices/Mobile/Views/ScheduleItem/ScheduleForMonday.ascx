<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>

<table id="schedule" cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td class="time">8:00am</td>
		<td class="desc">Registration Opens</td>
	</tr>
	<tr>
		<td class="time">10:00am - 12:00pm</td>
		<td class="desc"><a href='Sessions/Tags/Workshop'>Workshops</a></td>
	</tr>
	<tr>
		<td class="time">12:00am - 1:15pm</td>
		<td class="desc">Lunch</td>
	</tr>
	<tr>
		<td class="time">1:15pm - 5:45pm</td>
		<td class="desc"><a href='Sessions/Tags/Workshop'>Workshops</a></td>
	</tr>
</table>
<div class="pagination">
	<a href="/Schedule/Tuesday">Next Day &#0187;</a>
</div>