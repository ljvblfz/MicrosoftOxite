<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<File>>" %>
<%@ Import Namespace="Oxite.Modules.Membership.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Files.Models"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<%
if (Model.GetUser().IsInRole("Admin"))
{ %>
<div class="manageFile" id="mf-<%=Model.Item.ID %>">
    <form class="flag removeFile" method="post" action="<%=Url.RemoveFileFromPost(Model.Container as Post) %>">
        <fieldset>
            <input type="image" class="ibutton remove" src="<%=Url.CssPath("images/delete.png", ViewContext) %>" title="<%=Model.Localize("Remove") %>" />
            <input type="hidden" name="existingFileUrl" value="<%=Model.Item.Url %>" />
            <input type="hidden" name="returnUri" value="<%=Url.FilesByPost(Model.Container as Post) %>" />
            <%=Html.OxiteAntiForgeryToken()%>
        </fieldset>
    </form>
   <form class="editFile" action="<%=Url.EditFileOnPost(Model.Container as Post) %>#mf-<%=Model.Item.ID %>" method="post">
        <fieldset class="fileInfo">
            <label for="<%=string.Format("fileUrl-{0}", Model.Item.ID) %>"><%=Model.Localize("URL")%></label>
            <%=Html.TextBox("fileUrl", Model.Item.Url, new { title = Model.Localize("URL"), id = string.Format("fileUrl-{0}", Model.Item.ID), @class = "fileUrl text", size = 60 })%>
            <label for="<%=string.Format("fileTypeName-{0}", Model.Item.ID) %>"><%=Model.Localize("TypeName", "Type Name") %></label>
            <%=Html.TextBox("fileTypeName", Model.Item.TypeName, new { title = Model.Localize("TypeName", "Type Name"), id = string.Format("fileTypeName-{0}", Model.Item.ID), @class = "fileTypeName text", size = 35 })%>
            <label for="<%=string.Format("fileMimeType-{0}", Model.Item.ID) %>"><%=Model.Localize("MimeType", "Mime Type")%></label>
            <%=Html.TextBox("fileMimeType", Model.Item.MimeType, new { title = Model.Localize("MimeType", "Mime Type"), id = string.Format("fileMimeType-{0}", Model.Item.ID), @class = "fileMimeType text", size = 12 })%>
            <label for="<%=string.Format("sizeInBytes-{0}", Model.Item.ID) %>"><%=Model.Localize("SizeInBytes", "Size In Bytes")%></label>
            <%=Html.TextBox("fileSizeInBytes", Model.Item.SizeInBytes, new { title = Model.Localize("SizeInBytes", "Size In Bytes"), id = string.Format("sizeInBytes-{0}", Model.Item.ID), @class = "fileSizeInBytes text", size = 6 })%>
        </fieldset>
        <fieldset>
            <input type="image" class="ibutton save" src="<%=Url.CssPath("images/disk.png", ViewContext) %>" title="<%=Model.Localize("Save") %>" />
            <input type="hidden" name="existingFileUrl" value="<%=Model.Item.Url %>" />
            <input type="hidden" name="returnUri" value="<%=Url.FilesByPost(Model.Container as Post) %>" />
            <%=Html.OxiteAntiForgeryToken()%>
        </fieldset>
    </form>
</div>
<%
} %>