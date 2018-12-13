<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.ViewModels"%>
<%@ Import Namespace="Oxite.Extensions" %>
<div id="whatshappening" class="bucket">
	<h3>What's Happening <a class="rss" href="/WhatsHappening/RSS">RSS Feed</a></h3>
	<%
	    var headlinesViewModel = Model.GetModelItem<Last3HeadlinesViewModel>();
        if (headlinesViewModel != null)
        { %><%= string.Join("", headlinesViewModel.Posts.Select(
                    (p, i) => string.Format(
                        "<p>{0}&nbsp;{1}<span class=\"date\">{2}</span></p>",
                        p.Title.CleanText(),
                        Html.Link("more&#0187;", Url.Post(p)),
                        p.Published != null ? ((DateTime)p.Published).ToString("MMM dd, yyyy") : ""
                        )
                    ).ToArray()) %><%
        } %>
</div>