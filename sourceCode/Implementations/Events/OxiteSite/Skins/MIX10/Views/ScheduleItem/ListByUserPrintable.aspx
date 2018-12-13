<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ID="searchtags" ContentPlaceHolderID="SearchTags" runat="server"><%=Html.SearchTag("Section", "Sessions", false)%><%=Html.SearchTag("PageType", "List", false)%>
<%=Html.SearchTag("Section", Model.Container.Name, false)%></asp:Content>
<asp:Content ID="robotBlock" ContentPlaceHolderID="robots" runat="server"><meta name="robots" content="noindex,follow" /></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
    <h1><%=Model.Localize("Sessions") %></h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.Content("SessionsDescription") %>
    <div id="browser">
	    <div id="tabs">
		    <a href="/Sessions" class="tab" id="sessionstab">Sessions</a>
		    <a href="/Schedule" class="tab" id="scheduletab">Schedule</a>		    
	    </div>	    
	    <% var sundayStart = new DateTime(2010, 3, 14, 00, 00, 00); 
	       var sundayEnd = new DateTime(2010, 3, 14, 23, 59, 59);
           var mondayStart = new DateTime(2010, 3, 15, 00, 00, 00);
           var mondayEnd = new DateTime(2010, 3, 15, 23, 59, 59);
           var tuesdayStart = new DateTime(2010, 3, 16, 00, 00, 00);
           var tuesdayEnd = new DateTime(2010, 3, 16, 23, 59, 59);
           var wednesdayStart = new DateTime(2010, 3, 17, 00, 00, 00);
           var wednesdayEnd = new DateTime(2010, 3, 17, 23, 59, 59); %>	       
        <% var sunday = Model.Items.Where(si => si.Start >= sundayStart && si.Start <= sundayEnd); %>
        <% if (sunday.Count() > 0) { %>
	    <h2>Sunday March 14th, 2010</h2>
        <% //<h3>9:00am – 12:30pm</h3> %>
        <% Html.RenderPartialFromSkin(
        "ScheduleItemTimeSlot",
            new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
            new TimeslotDescription(
            new DateRangeAddress(new DateTime(2010, 3, 14, 9, 00, 00), new DateTime(2010, 3, 14, 12, 30, 00)),
            "Workshops"),
            Model.Items,
            Model)
        ); %>        
        <% } %>
        
        <% //<h3>1:30pm - 5:00pm</h3> %>
        <% Html.RenderPartialFromSkin(
        "ScheduleItemTimeSlot",
            new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
            new TimeslotDescription(
            new DateRangeAddress(new DateTime(2010, 3, 14, 13, 30, 00), new DateTime(2010, 3, 14, 17, 00, 00)),
            "Workshops"),
            Model.Items,
            Model)
        ); %>
        
        <% var monday = Model.Items.Where(si => si.Start >= mondayStart && si.Start <= mondayEnd); %>
        <% if (monday.Count() > 0) { %>        
	    <h2>Monday March 15th, 2010</h2>
        <% //<h3>11:30am – 12:30pm</h3> %>
        <% Html.RenderPartialFromSkin(
        "ScheduleItemTimeSlot",
            new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
            new TimeslotDescription(
            new DateRangeAddress(new DateTime(2010, 3, 15, 11, 30, 00), new DateTime(2010, 3, 15, 12, 30, 00)),
            "Sessions"),
            Model.Items,
            Model)
        ); %>
        
        <% //<h3>2:00pm - 3:00pm</h3> %>
        <% Html.RenderPartialFromSkin(
        "ScheduleItemTimeSlot",
            new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
            new TimeslotDescription(
            new DateRangeAddress(new DateTime(2010, 3, 15, 14, 00, 00), new DateTime(2010, 3, 15, 15, 00, 00)),
            "Sessions"),
            Model.Items,
            Model)
        ); %>
        
        <% //<h3>3:30pm - 4:30pm</h3> %>
        <% Html.RenderPartialFromSkin(
        "ScheduleItemTimeSlot",
            new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
            new TimeslotDescription(
            new DateRangeAddress(new DateTime(2010, 3, 15, 15, 30, 00), new DateTime(2010, 3, 15, 16, 30, 00)),
            "Sessions"),
            Model.Items,
            Model)
        ); %>
        <% } %>
        
        <% var tuesday = Model.Items.Where(si => si.Start >= tuesdayStart && si.Start <= tuesdayEnd); %>
        <% if (tuesday.Count() > 0) { %>   
        <h2>Tuesday March 16th, 2010</h2>
        <% // <h3>11:00am – 12:00pm</h3> %>
        <% Html.RenderPartialFromSkin(
        "ScheduleItemTimeSlot",
            new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
            new TimeslotDescription(
            new DateRangeAddress(new DateTime(2010, 3, 16, 11, 00, 00), new DateTime(2010, 3, 16, 12, 00, 00)),
            "Sessions"),
            Model.Items,
            Model)
        ); %>
        
        <% //<h3>1:30pm – 2:30pm</h3> %>
        <% Html.RenderPartialFromSkin(
        "ScheduleItemTimeSlot",
            new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
            new TimeslotDescription(
            new DateRangeAddress(new DateTime(2010, 3, 16, 13, 30, 00), new DateTime(2010, 3, 16, 14, 30, 00)),
            "Sessions"),
            Model.Items,
            Model)
        ); %>
        
        <% //<h3>3:00pm – 4:00pm</h3> %>
        <% Html.RenderPartialFromSkin(
        "ScheduleItemTimeSlot",
            new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
            new TimeslotDescription(
            new DateRangeAddress(new DateTime(2010, 3, 16, 15, 00, 00), new DateTime(2010, 3, 16, 16, 00, 00)),
            "Sessions"),
            Model.Items,
            Model)
        ); %>
        
        <% //<h3>4:30pm – 5:30pm</h3> %>
        <% Html.RenderPartialFromSkin(
        "ScheduleItemTimeSlot",
            new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
            new TimeslotDescription(
            new DateRangeAddress(new DateTime(2010, 3, 16, 16, 30, 00), new DateTime(2010, 3, 16, 17, 30, 00)),
            "Sessions"),
            Model.Items,
            Model)
        ); %>
        <% } %>
        
        <% var wednesday = Model.Items.Where(si => si.Start >= wednesdayStart && si.Start <= wednesdayEnd); %>
        <% if (wednesday.Count() > 0) { %>   
        <h2>Wednesday March 17th, 2010</h2> 
        <% //<h3>9:00am – 10:00am</h3> %>
        <% Html.RenderPartialFromSkin(
        "ScheduleItemTimeSlot",
            new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
            new TimeslotDescription(
            new DateRangeAddress(new DateTime(2010, 3, 17, 9, 00, 00), new DateTime(2010, 3, 17, 10, 00, 00)),
            "Sessions"),
            Model.Items,
            Model)
        ); %>
        
        <% //<h3>10:30am – 11:30am</h3> %>
        <% Html.RenderPartialFromSkin(
        "ScheduleItemTimeSlot",
            new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
            new TimeslotDescription(
            new DateRangeAddress(new DateTime(2010, 3, 17, 10, 30, 00), new DateTime(2010, 3, 17, 11, 30, 00)),
            "Sessions"),
            Model.Items,
            Model)
        ); %>
        
        <% //<h3>12:00pm – 1:00pm</h3> %>
        <% Html.RenderPartialFromSkin(
        "ScheduleItemTimeSlot",
            new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
            new TimeslotDescription(
            new DateRangeAddress(new DateTime(2010, 3, 17, 12, 00, 00), new DateTime(2010, 3, 17, 13, 00, 00)),
            "Sessions"),
            Model.Items,
            Model)
        ); %>
        
        <% //<h3>1:30pm – 2:30pm</h3> %>
        <% Html.RenderPartialFromSkin(
        "ScheduleItemTimeSlot",
            new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
            new TimeslotDescription(
            new DateRangeAddress(new DateTime(2010, 3, 17, 13, 30, 00), new DateTime(2010, 3, 17, 14, 30, 00)),
            "Sessions"),
            Model.Items,
            Model)
        ); %>
        
        <% //<h3>3:00pm – 4:00pm</h3> %>    
        <% Html.RenderPartialFromSkin(
        "ScheduleItemTimeSlot",
            new OxiteViewModelItemItems<TimeslotDescription, ScheduleItem>(
            new TimeslotDescription(
            new DateRangeAddress(new DateTime(2010, 3, 17, 15, 00, 00), new DateTime(2010, 3, 17, 16, 00, 00)),
            "Sessions"),
            Model.Items,
            Model)
        ); %>        
        <% } %>    
        
        <% // No Date/Time Available %>    
        <% var na = Model.Items.Where(si => si.Start >= wednesdayEnd || si.Start < sundayStart); %>
        <% if (na.Count() > 0) { %>
        <h2>No Date/Time Available</h2>        
            <div class="sessions">
                <% Html.RenderPartialFromSkin("ScheduleItemList",new OxiteViewModelItems<ScheduleItem>(na, Model)); %>
            </div><%
        } %>               
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Sessions")) %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="HeadCssFiles" runat="server"><%
    Html.RenderCssFile("jquery.autocomplete.css"); %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js");
    Html.RenderScriptTag("Sessions.js?ver=2");
    Html.RenderScriptTag("jquery.autocomplete.js");
    Html.RenderScriptTag("SessionBrowser.4.js?rev=04152010"); %>
    <script type="text/javascript">/*<![CDATA[*/pdc.sessionBrowser.which = "sessions";//]]></script>
    <script type="text/javascript">/*<![CDATA[*/
        pdc.sessionBrowser.sessions.speakers = ("").split("|");
        pdc.sessionBrowser.sessions.tags = ("").split("|");
    //]]></script>
</asp:Content>
