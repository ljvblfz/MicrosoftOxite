<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<PluginContainer>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Plugins"%>
<%@ Import Namespace="Oxite.Plugins.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Plugins.Extensions" %>
<div class="managePlugin"><%
if (Model.PartialModel.GetDateModified() != null)
{ %>
    <form class="editPlugin" method="POST" action="<%=Url.PluginEdit()%>">
        <input type="image" src="<%=Url.CssPath("images/page_edit.png", ViewContext)%>" alt="<%=Model.Localize("Edit")%>" title="<%=Model.Localize("Edit")%>" class="ibutton image edit" />
        <%=Html.Hidden("virtualPath", Model.PartialModel.GetVirtualPath())%>
        <%=Html.OxiteAntiForgeryToken()%>
    </form><%
}    
if (Model.PartialModel.CompilationException == null)
{
    bool? installed = (bool?)ViewContext.RouteData.Values["installed"]; %>
    <form class="installPlugin modifyPlugin" method="POST" action="<%=Url.PluginInstall() %>">
        <input type="image" src="<%=Url.CssPath("images/add.png", ViewContext) %>" alt="<%=Model.Localize("Install") %>" title="<%=Model.Localize("Install") %>" class="ibutton image add" />
        <%=Html.Hidden("virtualPath", Model.PartialModel.GetVirtualPath())%>
        <input type="hidden" name="returnUrl" value="<%=installed != null ? Url.PluginsNotInstalled() : Url.Plugins() %>">
        <%=Html.OxiteAntiForgeryToken()%>
    </form><%
} %>
</div>