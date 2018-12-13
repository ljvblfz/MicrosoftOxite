<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelList<File>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.ViewModels.Extensions" %>
<%
if (Model.User.GetCanAccessAdmin())
{ %>
<h2><%=Model.Localize("ManageFiles", "Manage Files") %></h2>
<form action="<%=Url.AddFile(Model.Container as Post) %>" method="post" id="addFile">
    <fieldset>
        <legend><%=Model.Localize("AddFile", "Add a new file") %></legend>
        <h3><%=Model.Localize("AddFileFromDisk", "From disk") %></h3>
        <div id="addLocal">
            <label for="newFile"><%=Model.Localize("Location") %></label>
            <input type="file" id="newFile" name="newFile" size="60" class="text" title="<%=Model.Localize("AddFile", "Add a new file from disk") %>" />
        </div>
        <h3><%=Model.Localize("AddFileFromURI", "Existing") %></h3>
        <div id="addUrl">
            <label for="existingFileUrl"><%=Model.Localize("URL") %></label>
            <input type="text" id="existingFileUrl" name="fileUrl" size="60" class="text" title="<%=Model.Localize("AddFile", "URL...") %>" />
            <label for="existingFileDisplayName"><%=Model.Localize("DisplayName", "Display name") %></label>
            <input type="text" id="existingFileDisplayName" name="fileDisplayName" size="35" class="text" title="<%=Model.Localize("DisplayName", "Display name...") %>" />
            <label for="existingFileMimeType"><%=Model.Localize("MimeType", "Mime type") %></label>
            <input type="text" id="existingFileMimeType" name="fileMimeType" size="12" class="text" title="<%=Model.Localize("MimeType", "Mime type...") %>" />
            <label for="existingFileSizeInBytes"><%=Model.Localize("SizeInBytes", "Size in bytes") %></label>
            <input type="text" id="existingFileSizeInBytes" name="fileSizeInBytes" size="6" class="text" title="<%=Model.Localize("SizeInBytes", "Size in bytes...") %>" />
        </div>
        <div class="buttons">
            <input type="submit" value="<%=Model.Localize("Save") %>" class="button submit" />
            <%=Html.Button(
                "cancel",
                Model.Localize("Cancel"),
                new { @class = "cancel", onclick = string.Format("if (window.confirm('{0}')){{window.document.location='{1}';}}return false;", Model.Localize("really?"), Model.Container is Post ? Url.Post(Model.Container as Post) : Url.Home()) }
                )%>
            <%=Html.Link(
                Model.Localize("Cancel"),
                Model.Container is Post ? Url.Post(Model.Container as Post) : Url.Home(),
                new { @class = "cancel" })%>
            <input type="hidden" name="returnUri" value="<%=Request.Url.AbsoluteUri %>" />
            <%=Html.OxiteAntiForgeryToken() %>
        </div>
    </fieldset>
</form><%
    if (Model.List.Count > 0)
    {
        %><h3><%=Model.Localize("ManageFiles", "Manage Files") %></h3><%
    }
    
    foreach (File file in Model.List)
    {
        Html.RenderPartial("ManageFile", new OxiteModelPartial<File>(Model, file));
    }
} %>