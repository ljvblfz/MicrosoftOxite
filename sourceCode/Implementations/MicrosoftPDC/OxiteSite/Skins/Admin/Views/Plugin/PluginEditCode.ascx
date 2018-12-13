<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<PluginEditInput>>" %>
<%@ Import Namespace="Oxite.Plugins"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Plugins.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Plugins.Models" %><%
if (Model.Item.Code == null)
{ 
    %><%=Model.Localize("Plugins.NoCodeFile", "No code file was found for this plugin.") %><%
}
else
{
    PluginContainer pluginContainer = Model.GetModelItem<PluginContainer>();
    Plugin plugin = pluginContainer.Tag as Plugin;        
    
    bool isWritable = plugin != null
        ? plugin.GetIsFileWritable(ViewContext.HttpContext)
        : Model.Item.VirtualPath.IsFileWritable(ViewContext.HttpContext);

    if (pluginContainer.CompilationException != null)
   { %>
    <div class="error message">
        <span class="error metadata"><%=pluginContainer.CompilationException.Message.CleanText() %></span>
        <span class="description metadata"><%=pluginContainer.CompilationException.StackTrace.CleanText() %></span>
    </div><%
   } %>
<form action="<%=Url.PluginEdit(plugin != null ? plugin.ID : Guid.Empty) %>" method="post" class="edit plugin">
    <fieldset><%
if (isWritable)
{
    %><textarea name="code" rows="20" cols="100"><%=Model.Item.Code.CleanText() %></textarea><%
}
else
{
    %><div class="info message"><%=Model.Localize("Plugin.NotWritable", "Cannot write to the code file.") %></div>
    <textarea name="code" rows="20" cols="100" disabled="disabled"><%=Model.Item.Code %></textarea><%
}
%>
    </fieldset>
    <div class="actions">
        <input type="submit" value="<%=Model.Localize("Plugin.SaveCode", "Save Code") %>"<%=!isWritable ? " disabled=\"disabled\"" : "" %> />
        <%=Html.OxiteAntiForgeryToken() %><%
        if (!string.IsNullOrEmpty(Model.Item.VirtualPath))
        {%>
        <%=Html.Hidden("virtualPath", Model.Item.VirtualPath) %><%
        } %>
    </div>
</form><%
} 
%>
