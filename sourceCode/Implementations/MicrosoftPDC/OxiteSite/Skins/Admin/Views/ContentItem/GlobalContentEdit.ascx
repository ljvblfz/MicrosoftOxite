<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ContentItemInput>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.CMS.Models" %>
<div class="sections">
    <div class="lone"><%=Html.ValidationSummary() %>
        <% Html.RenderPartialFromSkin("EditContentItems"); %>
    </div>
</div>