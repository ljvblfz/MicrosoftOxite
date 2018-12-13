<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<ScheduleItem>>" %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<div class="overview">
    <% Html.RenderPartialFromSkin("ScheduleItemAdd"); %>
    <h3><%=Html.Link(Model.PartialModel.Title.WidowControl(), Url.Session(Model.PartialModel)) %></h3>
    <p><%=Model.PartialModel.SpeakerLocationTime(Url, Html) %></p>
</div>
<div class="details">
    <div class="itemsidebar"><%
        Html.RenderPartialFromSkin("ScheduleItemShare"); %>
        <div class="comments">
	        <a href="<%= Url.Session(Model.PartialModel) %>#comments">
	        <%=Model.PartialModel.Comments.Count()%> Comments</a>
        </div>
        <div class="comments">
            <a href="<%= Url.Session(Model.PartialModel) %>/ICS">Add to Outlook</a>
        </div>
        <%
        bool showSurveyLink = false;
        string surveyURL =
            "http://www.surveymonkey.com/s.aspx?sm=I7c0_2b4_2byFnzQN5iaH6a_2fFw_3d_3d&amp;c={0}";
        if (Model.User.IsAuthenticated)
        {
            if (Model.PartialModel.Start < DateTime.Now.AddHours(-3))
            {
                UserAuthenticated userAuthenticated = Model.User.ToUserAuthenticated();

                if (userAuthenticated != null)
                {
                    string userCode = "{0}_{1}";
                    userCode = string.Format(userCode, Model.PartialModel.Slug, userAuthenticated.ID.ToString("N"));
                    surveyURL = string.Format(surveyURL, userCode);
                    showSurveyLink = true;
                }
            }


        }
            
            if (showSurveyLink) {

%>
        <div class="comments">            
            <a href="<%=surveyURL %>">Session Evaluation</a>
        </div>
        <% } %>
    </div>    
    <%--//todo: (nheskew) need to pull in the thumbnail for the session--%>
        <div class="content">
        <p class="abstract"><%=Model.PartialModel.Body.Ellipsize(200, s => s) %></p>
        <p class="tags"><%=Model.Localize("Tags") %>: <%
    if (Model.PartialModel.Tags.Count() > 0)
         %><%=string.Join(", ", Model.PartialModel.Tags.Select(t => Html.Link(Html.Encode(t.GetDisplayName()), Url.Sessions(t), new { rel = "tag" })).ToArray()) %><%
    else
        %><%=Model.Localize("none") %><%
        %></p>
    </div>
</div>
