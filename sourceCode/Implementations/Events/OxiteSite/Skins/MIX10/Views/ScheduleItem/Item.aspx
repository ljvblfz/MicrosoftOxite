<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Session.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Modules.Core.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Tags.Models"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MetaDescription" runat="server"><%=Html.PageDescription(Model.Item.Slug + " " + Model.Item.GetBodyShort())%></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SearchTags" runat="server"><%=Html.SearchTag("Section", "Sessions", false)%><%=Html.SearchTag("Speakers", string.Join(", ", Model.Item.Speakers.Select(s => Html.Link(s.DisplayName, Url.Speaker(s), new { @class = "speaker" })).ToArray()), true) %><%=Html.SearchTag("Title", Model.Item.Title, true)%><%=Html.MetaTag("Keywords", string.Join(", ", Model.Item.Tags.Select(s => s.DisplayName).ToArray()), true) %>
<link rel="canonical" href="<%=Url.AbsolutePath(Url.Session(Model.Item)) %>" /></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<!-- MIX 10 \ ScheduleItem \ Item.aspx -->
<% string postURL = Url.Session(Model.Item); %>
    <div id="sessions" class="sessions">
        <ul class="scheduleItems medium permalink">
            <li class="seminar first">
                <div class="overview">
                    <% Html.RenderPartialFromSkin("ScheduleItemAdd", new OxiteViewModelPartial<ScheduleItem>(Model, Model.Item)); %>
                </div>
            </li>
       </ul>
    </div>
    <h1><%=Html.Link(Model.Item.Title.CleanText().WidowControl(), Url.Session(Model.Item)) %></h1>
    <div class="metadata">
        <p><%=Model.Item.SpeakerLocationTime(Url, Html) %></p>
    </div>
    <%        
    
        List<File> files = null;
        string cacheKeyID = Url.Session(Model.Item);
        files =
            Cache.Get("filesFor:" + cacheKeyID) as List<File>;
        if (files == null)
        {
            files = Model.Item.Files.ToList();
            Cache.Add("filesFor:" + cacheKeyID, files, null,
                      DateTime.Now.AddHours(1),
                      Cache.NoSlidingExpiration,
                      CacheItemPriority.Normal, null);
        }
        
        bool showSurveyLink = false;
        bool showFiles = false;
        if (Model.Item.Start < DateTime.Now.AddHours(-3) && Model.Item.Start < new DateTime(2010,03,18,10,0,0))
        {
            showFiles = true;
        }
        
        Html.RenderPlayer("VideoPlayer", files, Url.ViewTrack("schedule","player", Model.Item.ID.ToString("N")), postURL); %>

       <%if (files.Count() > 0 && showFiles) { %>
        <div class="downloadlist"><% Html.RenderPartialFromSkin("Download", new OxiteViewModelPartial<IEnumerable<File>>(Model, files)); 
%></div>
    <p class="content"><%=Model.Item.Body %></p>
    <ul class="more">
    
        <li><%=Model.Localize("Tags") %>: <%
                                              
        IEnumerable<Tag> tags = Model.Item.GetTags();
        if (tags.Count() > 0)
        {
            %><%=string.Join(", ", tags.Where(t=> !String.IsNullOrEmpty(t.GetDisplayName())).Select(t => Html.Link(t.GetDisplayName().CleanText(), Url.Sessions(t), new { rel = "tag" })).ToArray()) %><%
        } else { %><%=Model.Localize("none") %><% } %></li>
        <li class="share"><div><%=Model.Localize("Share:") %></div><% Html.RenderPartialFromSkin("ScheduleItemShare", new OxiteViewModelPartial<ScheduleItem>(Model, Model.Item)); %></li>


        <% if (Model.Item.Start.Year > 2009) {%>
        <li><%=Model.Localize("Calendar") %>: <a href="<%= Url.Session(Model.Item) %>/ICS"><%=Model.Localize("Add to Outlook") %></a></li>
        <% } %>
        <%
        } %>

    </ul>
    
    <%
        if (!(Model.CommentingDisabled && Model.Item.Comments.Count() < 1))
        {
            Html.RenderPartialFromSkin("Comments", new OxiteViewModelItem<ScheduleItem>(Model.Item, Model));
        }

        if (Model.CommentingDisabled)
        { %>
    <div class="message"><%=Model.Localize("CommentingDisabled", "Commenting is disabled for this post.")%></div><%
        } %>
        <%= Html.WebBug("schedule", Model.Item.ID.ToString("N")) %>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Title" runat="server"><%=Html.PageTitle(Model.Localize("Sessions"), Model.Item.Title) %></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ScriptVariablesPre" runat="server">
    <script type="text/javascript">
        <% Html.RenderScriptVariable("computeHashPath", Url.ComputeEmailHash()); %>
    </script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js");
    Html.RenderScriptTag("Sessions.js?ver=2"); %>
</asp:Content>
