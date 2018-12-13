<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<PluginContainer>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Plugins"%>
<%@ Import Namespace="Oxite.Plugins.Extensions"%>
<%
    
Plugin plugin = Model.PartialModel.Tag as Plugin;
StringBuilder className = new StringBuilder("plugin info");

if (Model.PartialModel.CompilationException != null)
{
    className.Append(" error");
}
else if ((Model.PartialModel.GetDateModified() ?? DateTime.Now) > Model.PartialModel.CompilationDate)
{
    className.Append(" warning");
}

if (plugin == null)
    className.Append(" notInstalled");
else if (!plugin.Enabled)
    className.Append(" disabled");

%><div id="<%=plugin != null ? plugin.ID.ToString() : "" %>" class="<%=className.ToString() %>"><% 
if (plugin != null)
{
    Html.RenderPartialFromSkin(
        Model.PartialModel.CompilationException == null ? "PluginInfo" : "PluginInfoErrorSmall",
        new OxiteViewModelPartial<Plugin>(Model, plugin)
        );
}
else
{
    Html.RenderPartialFromSkin(
       Model.PartialModel.CompilationException == null ? "PluginInfo" : "PluginInfoErrorSmall",
       new OxiteViewModelPartial<Plugin>(Model, new Plugin(Guid.Empty, Guid.Empty, null, false) { Container = Model.PartialModel })
       );
}
%>
</div>