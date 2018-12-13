<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<Exhibitor>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%
    var levels = Model.Items
        .Select(e => e.ParticipantLevel).Distinct()
        .OrderBy(l => l.Equals("Platinum"))
        .OrderBy(l => l.Equals("Gold"))
        .OrderBy(l => l.Equals("Silver"));
%>
<% if (Model.Items != null && Model.Items.Count() > 0) { %>
<% foreach (var level in levels)
   {
       var value = level; // closure
       var sponsors = Model.Items.Where(e => e.ParticipantLevel.Equals(value));
       if(sponsors.Count() < 1)
       {
           continue;
       }
%>
    <h2><%= level.ToUpper() %> Sponsors</h2>
    <% foreach(var sponsor in sponsors)
       {
           var siteUrl = sponsor.SiteUrl.Contains("http") ? sponsor.SiteUrl : "http://" + sponsor.SiteUrl;      
    %>  
    <div>
        <a href="<%= siteUrl %>">
            <img alt="<%= sponsor.Name %>" src="<%= Url.SponsorImage(sponsor)  %>" />
        </a>
        <h3><%= sponsor.Name %></h3>
        <a href="<%= siteUrl %>"><%= sponsor.SiteUrl %></a></div>
    <p>
        <%= sponsor.Description %>
    </p>
    <% if(!string.IsNullOrEmpty(sponsor.ContactEmail) && !string.IsNullOrEmpty(sponsor.ContactEmail)) { %>
        <p>Sales Contact: <a href="mailto:<%= sponsor.ContactEmail %>"><%= sponsor.ContactName %></a></p>
    <% } %>
<% } %><% } %><% } %> 
