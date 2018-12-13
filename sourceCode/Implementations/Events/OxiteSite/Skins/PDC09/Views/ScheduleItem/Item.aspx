<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<ScheduleItem>>" %>
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
<% string postURL = Url.Session(Model.Item); %>
    <div id="sessions" class="sessions">
        <ul class="scheduleItems medium">
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
        if (Model.Item.Start < new DateTime(2009,11,20,10,0,0)) {
              Html.RenderPlayer("VideoPlayer", files, Url.ViewTrack("schedule","player", Model.Item.ID.ToString("N")), postURL); }%>

    <p class="content"><%=Model.Item.Body %></p>
    <ul class="more">
    
        <li><%=Model.Localize("Tags") %>: <%
                                              
        IEnumerable<Tag> tags = Model.Item.GetTags();
        if (tags.Count() > 0)
        {
            %><%=string.Join(", ", tags.Select(t => Html.Link(t.GetDisplayName().CleanText(), Url.Sessions(t), new { rel = "tag" })).ToArray()) %><%
        } else { %><%=Model.Localize("none") %><% } %></li>
        <li><% Html.RenderPartialFromSkin("ScheduleItemShare", new OxiteViewModelPartial<ScheduleItem>(Model, Model.Item)); %></li>


        <%
        bool showSurveyLink = false;
        bool showFiles = false;
        if (Model.Item.Start < DateTime.Now.AddHours(-3) && Model.Item.Start < new DateTime(2009,11,20,10,0,0))
        {
            showFiles = true;
        }

        string surveyURL =
            "http://www.surveymonkey.com/s.aspx?sm=I7c0_2b4_2byFnzQN5iaH6a_2fFw_3d_3d&amp;c={0}";
            
        if (Model.User.IsAuthenticated)
        {
            if (Model.Item.Start < DateTime.Now.AddHours(-3))
            {
                UserAuthenticated userAuthenticated = Model.User.ToUserAuthenticated();

                if (userAuthenticated != null)
                {
                    string userCode = "{0}_{1}";
                    userCode = string.Format(userCode, Model.Item.Slug, userAuthenticated.ID.ToString("N"));
                    surveyURL = string.Format(surveyURL, userCode);
                    showSurveyLink = true;
                }
            }


        }
%>
        
        
        <%
            if (showSurveyLink) {
%>
        <li><a href="<%=surveyURL %>">Session Evaluation</a></li>
        <% } %>
        <li><a href="<%= Url.Session(Model.Item) %>/ICS"><%=Model.Localize("Add to Outlook") %></a></li>
       <%if (files.Count() > 0 && showFiles) { %>
        <li><% Html.RenderPartialFromSkin("Download", new OxiteViewModelPartial<IEnumerable<File>>(Model, files)); 
%></li>
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
