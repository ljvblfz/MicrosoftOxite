<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<PageInput>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.CMS.Models" %>
<fieldset class="publish">
<legend><%=Model.Localize("Page.PublishOptions", "Status") %></legend>
<%-- change this to draft | published | publish @ datetime --%>
    <%=Html.RadioButton(
            "isPublished",
            false,
            !(Model.Item.Published.HasValue && Model.Item.Published.Value <= DateTime.UtcNow),
            new { @class = "checkbox withLabel", id = "page_stateDraft" }
            ) %> <label for="page_stateDraft" class="radio forCheckbox"><%=Model.Localize("Page.Draft", "Draft") %></label>
    <%=Html.RadioButton("isPublished",
            true,
            Model.Item.Published.HasValue && Model.Item.Published.Value <= DateTime.UtcNow,
            new { @class = "checkbox withLabel", id = "page_statePublished" },
            true
            )%> <label for="page_statePublished" class="radio forCheckbox"><%=Model.Localize("Page.Published", "Published")%></label>
    <label for="page_published" class="date"><%=Model.Localize("Page.PublishDate", "&#64;")%></label><%= Html.ValidationMessage("Page.Published") %>
    <%=Html.TextBox(
        "published",
        Model.Item.Published.HasValue && Model.Item.Published.Value <= DateTime.UtcNow ? Model.Item.Published.Value.ToStringForEdit() : "",
        new { id = "page_published", @class = "text date", size="24" },
        true
        ) %>
</fieldset>
