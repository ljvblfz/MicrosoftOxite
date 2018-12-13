<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<Exhibitor>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%
if (Model.Items != null && Model.Items.Count() > 0)
{ %>
<%
    var counter = 0;
    foreach (var exhibitor in Model.Items.OrderBy(e => e.Name))
    {
        var className = counter%2 != 0 ? "oddrow" : "";
        var level = exhibitor.ParticipantLevel;
        var isSponsor = !level.Equals("Exhibitor");
        className += " " + level;
        string url = exhibitor.SiteUrl;
        bool hasURL = !string.IsNullOrEmpty(url);
        if (hasURL && !url.StartsWith("http://", true, null))
        {
            url = "http://" + url;
        }
        %>

    <div class="<%=className %>">
        <h3><%= exhibitor.Name %></h3>
        <% if (isSponsor)
           {%>
        <p class="level"><%=level%> Sponsor</p>
        <% } %>
        
        <% if (!string.IsNullOrEmpty(exhibitor.Location))
           { %>        
        <p class="booth">Booth: <%=exhibitor.Location%></p>
        <% } %>
        <%if (hasURL)
          { %>
        <p class="site">Web Site: <a href="<%= url %>"><%= url%></a></p>
        <% } %>
        <p class="description">
            <%= exhibitor.Description %>
        </p>
        <% if (!string.IsNullOrEmpty(exhibitor.Tags))
           {%>
        <p class="tags">Tags: <% = exhibitor.Tags.Substring(0, exhibitor.Tags.Length-1).Replace(",", ", ")%></p>
        <% } %>
    </div>
    <%
        counter++;
    } 
    %>
<% 
} 
else
{ //todo: (nheskew) need an Html.Message html helper extension method that takes a message %>
<div class="message info"><%=Model.Localize("Exhibitor.NoneFound", "There were no exhibitors found.")%></div><%        
} %>
