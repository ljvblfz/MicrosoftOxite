<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<PluginContainer>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Plugins" %>
<%@ Import Namespace="Oxite.Plugins.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Plugins.Extensions" %><%

Plugin plugin = Model.PartialModel.Tag as Plugin;
StringBuilder className = new StringBuilder("plugin info");

bool error = false;
bool warning = false;
bool? installed = (bool?)ViewContext.RouteData.Values["installed"];

if (Model.PartialModel.CompilationException != null)
{
    error = true;
    className.Append(" error");
}
else if ((Model.PartialModel.GetDateModified() ?? DateTime.Now) > Model.PartialModel.CompilationDate)
{
    warning = true;
    className.Append(" warning");
}

if (plugin == null)
    className.Append(" notInstalled");
else if (!plugin.Enabled)
    className.Append(" disabled");

%><div id="<%=plugin != null ? plugin.ID.ToString() : "" %>" class="<%=className.ToString() %>"><%
if (warning || error)
{ %>
    <div class="warning message"><%
    if (Model.PartialModel.GetDateModified() == null && plugin != null)
    {
        %>
        <p><%=Model.Localize("Plugin.FileMissing", "This plugin's code file is missing. Try replacing this plugin's file(s) or uninstall.")%></p>
        <form method="POST" class="uninstallPlugin" action="<%=Url.PluginUninstall(plugin) %>">
            <input type="submit" value="<%=Model.Localize("Plugin.Uninstall", "Uninstall")%>" class="submit" />
            <input type="hidden" name="returnUrl" value="<%=installed != null && (bool)installed ? Url.PluginsInstalled() : Url.Plugins() %>">
            <%=Html.OxiteAntiForgeryToken() %>
       </form><%
    }
    else
    {
        if (error)
        { %>
            <p><%=Model.Localize("Plugin.EditToFixError", "There was an error trying to load this plugin. Edit this plugin to get more info on the error and fix the problem.")%></p><%
            if (plugin != null)
            {%>
            <form method="GET" action="<%=Url.PluginEdit(plugin)%>">
                <input type="submit" value="<%=Model.Localize("Plugin.Edit", "Edit Plugin")%>" class="submit" />
            </form><%
            }
            else
            { %>
                <form method="POST" action="<%=Url.PluginEdit(plugin) %>">
                <input type="submit" value="<%=Model.Localize("Plugin.Edit", "Edit Plugin") %>" class="submit" />
                <%=Html.Hidden("virtualPath", Model.PartialModel.VirtualPath) %>
                <%=Html.OxiteAntiForgeryToken() %>
            </form><%
            }
        }
        if (Model.PartialModel.GetDateModified() > Model.PartialModel.CompilationDate)
        { %>
            <p><%=Model.Localize("Plugin.NeedsReloadMessage", "This plugin's code file has been updated. Reload this plugin to run on the recent code.")%></p>
            <form method="POST" action="<%=Url.PluginReload(plugin) %>">
                <input type="submit" value="<%=Model.Localize("Plugin.Reload", "Reload Plugin") %>" class="submit" />
                <%=Html.Hidden("virtualPath", Model.PartialModel.VirtualPath) %>
                <%=Html.OxiteAntiForgeryToken() %>
            </form><%
        }
    } %>
    </div><%
}
if (plugin != null)
{
    Html.RenderPartialFromSkin(
        plugin.Container.CompilationException == null ? "PluginInfo" : "PluginInfoError",
        new OxiteViewModelPartial<Plugin>(Model, plugin)
        );
    Html.RenderPartialFromSkin("ManagePlugin", new OxiteViewModelPartial<Plugin>(Model, plugin));
}
else
{
    Html.RenderPartialFromSkin(
       Model.PartialModel.CompilationException == null ? "PluginInfo" : "PluginInfoError",
       new OxiteViewModelPartial<Plugin>(Model, new Plugin(Guid.Empty, Guid.Empty, null, false) { Container = Model.PartialModel })
       );
    Html.RenderPartialFromSkin("ManagePluginNotInstalled");
}
%>
</div>