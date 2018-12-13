<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelPartial<File>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.ViewModels.Extensions" %><%
if (Model.RootModel.User.GetCanAccessAdmin())
{ %>
<div class="manageFile" id="mf-<%=Model.PartialModel.ID %>">
    <form class="flag removeFile" method="post" action="<%=Url.RemoveFile(Model.RootModel.Container as Post) %>">
        <fieldset>
            <input type="image" class="ibutton remove" src="<%=Url.CssPath("/images/delete.png", Model.RootModel) %>" title="<%=Model.RootModel.Localize("Remove") %>" />
            <input type="hidden" name="fileUrl" value="<%=Model.PartialModel.Url %>" />
            <input type="hidden" name="returnUri" value="<%=Request.Url.AbsoluteUri %>" />
            <%=Html.OxiteAntiForgeryToken(m => m.RootModel)%>
        </fieldset>
    </form>
   <form action="<%=Url.AddFile(Model.RootModel.Container as Post) %>#mf-<%=Model.PartialModel.ID %>" method="post">
        <fieldset class="fileInfo">
            <label for="<%=string.Format("fileUrl-{0}", Model.PartialModel.ID) %>"><%=Model.RootModel.Localize("URL")%></label>
            <%=Html.TextBox("fileUrl", Model.PartialModel.Url, new { title = Model.RootModel.Localize("URL"), id = string.Format("fileUrl-{0}", Model.PartialModel.ID), @class = "fileUrl text", size = 60 })%>
            <label for="<%=string.Format("fileDisplayName-{0}", Model.PartialModel.ID) %>"><%=Model.RootModel.Localize("DisplayName", "Display Name") %></label>
            <%=Html.TextBox("fileDisplayName", Model.PartialModel.DisplayName, new { title = Model.RootModel.Localize("DisplayName", "Display Name"), id = string.Format("fileDisplayName-{0}", Model.PartialModel.ID), @class = "fileDisplayName text", size = 35 })%>
            <label for="<%=string.Format("fileMimeType-{0}", Model.PartialModel.ID) %>"><%=Model.RootModel.Localize("MimeType", "Mime Type")%></label>
            <%=Html.TextBox("fileMimeType", Model.PartialModel.MimeType, new { title = Model.RootModel.Localize("MimeType", "Mime Type"), id = string.Format("fileMimeType-{0}", Model.PartialModel.ID), @class = "fileMimeType text", size = 12 })%>
            <label for="<%=string.Format("sizeInBytes-{0}", Model.PartialModel.ID) %>"><%=Model.RootModel.Localize("SizeInBytes", "Size In Bytes")%></label>
            <%=Html.TextBox("fileSizeInBytes", Model.PartialModel.SizeInBytes, new { title = Model.RootModel.Localize("SizeInBytes", "Size In Bytes"), id = string.Format("sizeInBytes-{0}", Model.PartialModel.ID), @class = "fileSizeInBytes text", size = 6 })%>
        </fieldset>
        <fieldset class="buttons">
            <input type="submit" value="<%=Model.RootModel.Localize("Save") %>" class="button submit" />
            <input type="hidden" name="returnUri" value="<%=Request.Url.AbsoluteUri %>" />
            <%=Html.OxiteAntiForgeryToken(m => m.RootModel)%>
        </fieldset>
    </form>
</div>
<%
} %>