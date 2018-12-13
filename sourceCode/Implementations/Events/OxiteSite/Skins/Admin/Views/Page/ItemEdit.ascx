<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<PageInput>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.CMS.Models" %>
<form method="post" action="">
    <div class="sections">
        <div class="primary"><%=Html.ValidationSummary() %>
                <% Html.RenderPartialFromSkin("ItemEditContent"); %>
	    </div>
	    <div class="secondary">
	        <% Html.RenderPartialFromSkin("ItemEditMetadata"); %>
	        <% Html.RenderPartialFromSkin("ItemEditButtons"); %>
	    </div>
    </div>
</form>