<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<PostInput>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<div class="admin buttons">
    <input type="submit" value="<%=Model.Localize("Save") %>" class="button submit" />
</div>