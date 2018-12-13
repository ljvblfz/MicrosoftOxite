<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<PostInput>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<fieldset class="tags">
    <label for="post_tags"><%=Model.Localize("Post.Tags", "Tags (comma-delimited)")%></label> <%=Html.ValidationMessage("Post.Tags")%>
    <%= Html.TextBox(
            "tags", 
            string.Join(", ", Model.Item.Tags.ToArray()), 
            new { id = "post_tags", @class="text", size = "60" }
            ) %>
</fieldset>
<fieldset class="publish">
<legend><%=Model.Localize("Post.PublishOptions", "Status") %></legend>
<%-- change this to draft | published | publish @ datetime --%>
    <%=Html.RadioButton(
            "isPublished",
            false,
            !(Model.Item.Published.HasValue && Model.Item.Published.Value <= DateTime.UtcNow),
            new { @class = "checkbox withLabel", id = "post_stateDraft" }
            ) %> <label for="post_stateDraft" class="radio forCheckbox"><%=Model.Localize("Post.Draft", "Draft") %></label>
    <%=Html.RadioButton("isPublished",
            true,
            Model.Item.Published.HasValue && Model.Item.Published.Value <= DateTime.UtcNow,
            new { @class = "checkbox withLabel", id = "post_statePublished" },
            true
            )%> <label for="post_statePublished" class="radio forCheckbox"><%=Model.Localize("Post.Published", "Published")%></label>
    <label for="post_published" class="date"><%=Model.Localize("Post.PublishDate", "&#64;")%></label><%= Html.ValidationMessage("Post.Published") %>
    <%=Html.TextBox(
        "published",
        Model.Item.Published.HasValue && Model.Item.Published.Value <= DateTime.UtcNow ? Model.Item.Published.Value.ToStringForEdit() : "",
        new { id = "post_published", @class = "text date", size="24" },
        true
        ) %>
</fieldset>
<fieldset class="commenting">
    <legend><%=Model.Localize("Post.Options", "Options") %></legend>
    <%=Html.CheckBox("commentingEnabled",
        m => !m.Item.CommentingDisabled,
        Model.Localize("Post.CommentingEnabled", "Commenting Enabled")
        )%>
</fieldset>