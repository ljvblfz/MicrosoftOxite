<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<Oxite.Modules.CMS.Models.Page>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions" %>
<%@ Import Namespace="Oxite.Plugins" %>
<%@ Import Namespace="Oxite.Plugins.Extensions" %>
<%
if (Model.Items.Count() > 0)
{ %>
<%=Html.UnorderedList(Model.Items.OrderBy(p => p.Slug), p => Html.Link(p.Slug, Url.Page(p.Slug)), "pages") %>

<%
}
else
{ %>
    <div class="info message"><%=Model.Localize("Pages.NothingAvailable", "There are no pages for the site. Add (&lt;- todo: (nheskew) add a link) one?") %></div><%
} %>