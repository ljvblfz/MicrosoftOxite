<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<Exhibitor>>" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models" %>
<%
    var postUrl = Url.EditExhibitor(Model.Item.Name ?? "New");
%>
<form method="post" action="<%= postUrl %>">
    <div class="sections">
        <div class="primary"><%=Html.ValidationSummary() %>
                <% Html.RenderPartialFromSkin("EditContent"); %>
	    </div>
	    <div class="secondary">	       
	        <div class="admin buttons">
                <input type="submit" value="<%=Model.Localize("Save") %>" class="button submit" />
            </div>
	    </div>
    </div>
</form>