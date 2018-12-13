<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>
<div id="sponsors" class="bucket">
	<h3>Sponsors</h3>
    <%=Html.Content("Sponsors") %>
</div>