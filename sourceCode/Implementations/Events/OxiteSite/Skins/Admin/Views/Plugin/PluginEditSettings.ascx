<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<PluginEditInput>>" %>
<%@ Import Namespace="Oxite.Plugins"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Plugins.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Plugins.Models" %><%
Plugin plugin = Model.GetModelItem<PluginContainer>().Tag as Plugin;
if (plugin != null && plugin.ExtendedProperties.Count() > 0)
{ %>
<form action="<%=Url.PluginEdit(plugin) %>" method="post" class="edit plugin">
    <%=Html.ValidationSummary() %>
    <fieldset>
        <%=Html.PluginPropertyFieldsets(plugin, Model.Item, (key, def) => Model.Localize(key, def))%>
    </fieldset>
    <div class="actions">
        <input type="submit" value="<%=Model.Localize("Plugin.SaveSettings", "Save Settings") %>" />
        <%=Html.OxiteAntiForgeryToken() %>
    </div>
</form><%
}
else
{
    %><div class="message info"><%=Model.Localize("Plugin.NoSettings", "There are no settings for this plugin.") %></div><%
} %>