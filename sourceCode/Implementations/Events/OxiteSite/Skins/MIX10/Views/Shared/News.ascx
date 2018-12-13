<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.ViewModels"%>
<%@ Import Namespace="Oxite.Extensions" %>
<h4>MIX News</h4>
<dl class="right_news clearfix">
	<%
	    var headlinesViewModel = Model.GetModelItem<Last3HeadlinesViewModel>();
        if (headlinesViewModel != null)
        { %><%= string.Join("", headlinesViewModel.Posts.Select(
                    (p, i) => string.Format(
                        "<dt>{0}</dt><dd class=\"rdate\">{2}</dd><dd class=\"rexcerpt\">{3} {1}</dd>",
                        p.Title.CleanText().WidowControl(),
                        Html.Link("continue&nbsp;reading", Url.Post(p)),
                        p.Published != null ? ((DateTime)p.Published).ToString("MMM dd, yyyy") : "",
                        p.Body.Ellipsize(60, s => s)
                        )
                    ).ToArray()) %><%
        } %>
</dl>
<p class="more news">
    <a href="/news">More News</a></p>
