<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<Exhibitor>>" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>
<fieldset class="title">
    <%=Html.ValidationMessage("Exhibitor.Name", Model.Localize("Name isn't valid.")) %>
    <%=Html.ValidationMessage("Exhibitor.ContactEmail", Model.Localize("Contact email isn't valid.")) %>

    <%=Html.Hidden("id", Model.Item.ID) %>
    <%=Html.ValidationMessage("Exhibitor.Name", Model.Localize("Name isn't valid."))%>
    <%=Html.TextBox("name",m => m.Item.Name,Model.Localize("Exhibitor.Name", "Name"),new { id = "exhibitor_title", @class = "text", size = "60" }) %>
    <label for="exhibitor_participant_level">
    <%=Model.Localize("Exhibitor.ParticipantLevel", "Participant Level")%></label>
    <%=Html.DropDownList(
        "participantLevel", 
        new SelectList( new[]{"Platinum", "Gold", "Silver", "Exhibitor"}, Model.Item.ParticipantLevel), 
        new { id="exhibitor_participant_level", @class="text"}) %>    
    <%=Html.TextBox("siteUrl",m => m.Item.SiteUrl,Model.Localize("Exhibitor.SiteUrl", "Site URL"),new { id = "exhibitor_site_url", @class = "text", size = "60" }) %>
    <%=Html.TextBox("logoUrl",m => m.Item.LogoUrl,Model.Localize("Exhibitor.LogoUrl", "Logo URL (absolute path)"),new { id = "exhibitor_logo_url", @class = "text", size = "60" }) %>
    <%=Html.TextBox("contactName",m => m.Item.ContactName,Model.Localize("Exhibitor.ContactName", "Contact Name"),new { id = "exhibitor_contact_name", @class = "text", size = "60" }) %>
    <%=Html.TextBox("contactEmail",m => m.Item.ContactEmail,Model.Localize("Exhibitor.ContactEmail", "Contact Email"),new { id = "exhibitor_contact_email", @class = "text", size = "60" }) %>
</fieldset>
<fieldset class="description">
    <label for="exhibitor_description">
    <%=Model.Localize("Exhibitor.Description", "Description (markup allowed)")%></label>
    <%=Html.TextArea(
        "description",
        Model.Item.Description, 15, 57,
        new { id = "exhibitor_description", @class = "text", size = "60" }) %>
    <%=Html.OxiteAntiForgeryToken() %>    
</fieldset>