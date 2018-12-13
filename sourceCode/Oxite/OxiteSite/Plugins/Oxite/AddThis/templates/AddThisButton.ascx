<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<Post>>" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%
    string addThisUsername = ((Plugin)ViewData["Plugin"]).ExtendedProperties.GetValue<string>("Username");
%>
<!-- AddThis Button BEGIN -->
<div class="addthis_toolbox addthis_default_style">
<a href="http://www.addthis.com/bookmark.php?v=250" class="addthis_button_compact"><%=((Plugin)ViewData["Plugin"]).ExtendedProperties.GetValue<string>("ButtonText").CleanText()%></a>
</div>
<script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js<%=!string.IsNullOrEmpty(addThisUsername) ? string.Format("?pub={0}", addThisUsername) : "" %>"></script>
<!-- AddThis Button END -->