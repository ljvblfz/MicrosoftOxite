<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<div id="primarynav">
	<ul class="nav"> 
		<li id="navregistration" class="primary">
			<a href="/Registration" class="primary">Registration</a>
			<ul class="secondary">
				<li><a href="/Hotels" id="navhotels">Hotels</a></li>
<%--				<li><a href="/Maps" id="navmaps">Maps</a></li> --%>			</ul>
		</li>
		<li id="navabout" class="primary">
			<a href="/About" class="primary">About</a>
			<ul class="secondary">
				<li><a href="/PDCClassics" id="navpdcclassics">PDC Classics</a></li>
				<li><a href="/Art" id="navart">PDC09 Art</a></li>
			</ul>
		</li>
		<li id="navworkshopssessions" class="primary">
			<a href="/WorkshopsAndSessions" class="primary">Workshops + Sessions</a>
			<ul class="secondary">
				<li><a href="/Sessions" id="navsessions">Sessions</a></li>
				<li><a href="/Schedule" id="navschedule">Schedule</a></li>
				<li><a href="/Videos" id="navvideos">Videos</a></li>
				<li><a href="/Workshops" id="navworkshops">Workshops</a></li>
				<li><a href="/Speakers" id="navspeakers">Speakers</a></li>
				<li><a href="/SpecialEvents" id="navspecialevents">Special Events</a></li>
			</ul>				
		</li>
		<li id="navblog" class="primary">
			<a href="/BehindTheScenes" class="primary">Blog</a>
			<ul class="secondary">
			    <li><a href="/BehindTheScenes" id="navbehindthescenes">Behind the Scenes</a></li>
			    <li><a href="/WhatsHappening" id="navwhatshappening">What's Happening</a></li>
			</ul>
		</li>
		<li id="navsponsorsexhibitors" class="primary">
			<a href="/Sponsors" class="primary">Sponsors + Exhibitors</a>
			<ul class="secondary">
				<li><a href="/Exhibitors" id="navexhibitors">Exhibitors</a></li>
				<li><a href="/PartnerOpportunities" id="navpartneropportunities">Partner Opportunities</a></li>
			</ul>						
		</li>
		<li id="navthegoods" class="primary">
			<a href="/Resources" class="primary">The Goods</a>
			<ul class="secondary">
				<li><a href="/Resources" id="navresources">Resources</a></li>
				<li><a href="/tablet" id="navtablet">Acer Tablet Details</a></li>
			</ul>						
		</li>
	</ul>
</div>
