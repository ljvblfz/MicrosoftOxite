<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<File>>" %>
<%@ Import Namespace="Oxite.Modules.Membership.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Files.Models"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %><%
if (Model.GetUser().IsInRole("Admin"))
{ %>
<h2 class="title"><%=Model.Localize("ManageFiles", "Manage Files") %></h2>
<% if (Model.Container is Post) { %><h3>For post: <%=Html.Link(Model.Container.DisplayName, Url.Post(Model.Container as Post)) %></h3><% } %>
<%--<form action="<%=Url.AddFileContentToPost(Model.Container as Post) %>" method="post" class="addFile" id="addNewFile" enctype="multipart/form-data">
    <fieldset>
        <h3><%=Model.Localize("AddFileFromDisk", "Add a new file") %></h3>
        <div id="addLocal">
            <label for="fileTypeName"><%=Model.Localize("TypeName", "Type name") %></label>
            <input type="text" id="fileTypeName" name="fileTypeName" size="35" class="text" title="<%=Model.Localize("TypeName", "Type name...") %>" />
            <label for="fileToAdd"><%=Model.Localize("Location") %></label>
            <input type="file" id="fileToAdd" name="fileTypeName" size="60" class="text" title="<%=Model.Localize("AddFile", "Add a new file from disk") %>" />
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
</form>--%>
<form action="<%=Url.AddFileToPost(Model.Container as Post) %>" method="post" class="addFile" id="addExistingFile">
    <fieldset>
        <h3><%=Model.Localize("AddFileFromURI", "Add reference to an existing file") %></h3>
        <div id="addUrl">
            <label for="existingFileUrl"><%=Model.Localize("URL") %></label>
            <input type="text" id="existingFileUrl" name="fileUrl" size="60" class="text" title="<%=Model.Localize("AddFile", "URL...") %>" />
            <label for="existingFileTypeName"><%=Model.Localize("TypeName", "Type name") %></label>
            <input type="text" id="existingFileTypeName" name="fileTypeName" size="35" class="text" title="<%=Model.Localize("TypeName", "Type name...") %>" />
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
            <input type="hidden" name="returnUri" value="<%=Url.FilesByPost(Model.Container as Post) %>" />
            <%=Html.OxiteAntiForgeryToken() %>
        </div>
    </fieldset>
</form><%
    foreach (File file in Model.Items)
    {
        Html.RenderPartialFromSkin("ManageFile", new OxiteViewModelItem<File>(file, Model));
    }
} %>