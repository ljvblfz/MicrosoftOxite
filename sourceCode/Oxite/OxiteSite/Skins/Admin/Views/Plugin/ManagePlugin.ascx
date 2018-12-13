<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<Plugin>>" %>
<%@ Import Namespace="Oxite.Plugins.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Plugins.Extensions" %>
<div class="managePlugin"><%
if (Model.PartialModel.Container.GetDateModified() != null)
{ %>
    <a class="editPlugin ibutton edit" href="<%=Url.PluginEdit(Model.PartialModel) %>" title="<%=Model.Localize("Edit") %>"><%=Html.SkinImage("/images/page_edit.png", Model.Localize("Edit"), new { width = 16, height = 16 })%></a>
    <%
}
bool? installed = (bool?) ViewContext.RouteData.Values["installed"];
string returnUrl = installed != null && (bool) installed ? Url.PluginsInstalled() : Url.Plugins();
        
if (Model.PartialModel.Container.CompilationException == null)
{
    if (Model.PartialModel.Enabled)
    {
    %><form class="disablePlugin modifyPlugin" method="POST" action="<%=Url.PluginDisable(Model.PartialModel) %>">
        <input type="image" src="<%=Url.CssPath("images/plugin_disabled.png", ViewContext) %>" alt="<%=Model.Localize("Disable") %>" title="<%=Model.Localize("Disable") %>" class="ibutton image pluginDisable" />
        <input type="hidden" name="returnUrl" value="<%=returnUrl %>">
        <%=Html.OxiteAntiForgeryToken() %>
    </form><%
    }
    else 
    {
    %><form class="enablePlugin modifyPlugin" method="POST" action="<%=Url.PluginEnable(Model.PartialModel) %>">
        <input type="image" src="<%=Url.CssPath("images/plugin.png", ViewContext) %>" alt="<%=Model.Localize("Enable") %>" title="<%=Model.Localize("Enable") %>" class="ibutton image pluginEnable" />
        <input type="hidden" name="returnUrl" value="<%=returnUrl %>">
        <%=Html.OxiteAntiForgeryToken() %>
    </form><%
    }
}
    %>
    <form class="uninstallPlugin modifyPlugin" method="POST" action="<%=Url.PluginUninstall(Model.PartialModel) %>">
        <input type="image" src="<%=Url.CssPath("images/delete.png", ViewContext) %>" alt="<%=Model.Localize("Uninstall") %>" title="<%=Model.Localize("Uninstall") %>" class="ibutton image remove" />
        <input type="hidden" name="returnUrl" value="<%=returnUrl %>">
        <%=Html.OxiteAntiForgeryToken() %>
    </form>
</div>