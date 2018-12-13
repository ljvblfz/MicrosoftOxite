<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>
<h4 class="split">Sponsors</h4>
<!--<%=Html.Content("Sponsors") %>-->
<!-- sample content for CMS -->
<a class="image_a" href="http://"><img src="<%= Url.ImagePath("hp.jpg", ViewContext) %>" alt="HP" /></a>
<a class="image_a" href="http://"><img src="<%= Url.ImagePath("dev-express.jpg", ViewContext) %>" alt="dev express" /></a>