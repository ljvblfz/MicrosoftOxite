<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ISearchResult>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Search.Models" %>

<%

if (Model.Items != null && ((IPageOfItems<ISearchResult>)Model.Items).TotalItemCount > 0){ %>
<ul class="posts medium">
<%
    int counter = 0;
    foreach (ISearchResult result in Model.Items)
    {
        StringBuilder className = new StringBuilder("post", 15);
        
        if (result.Equals(Model.Items.First())) { className.Append(" first"); }
        if (result.Equals(Model.Items.Last())) { className.Append(" last"); }

        if (counter % 2 != 0) { className.Append(" odd"); }
        %>
    <li class="<%=className.ToString() %>">
        <h2 class="title"><%=Html.Link(result.Title.CleanText(), result.Url)%></h2>
			<div class="content">
				<%=result.Body %>
			</div>
    </li><%
        counter++;
    } %>
</ul>
<% } %>