<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<PluginContainer>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Plugins"%>
<%@ Import Namespace="Oxite.Plugins.Extensions"%>
<%
if (Model.Items.Count() > 0)
{ %>
<ul class="plugins"><%
    var pluginGroup = Model.Items.GroupBy(p => p.CompilationException != null ? null : (p.Tag is Plugin).ToString()).OrderBy(pliGroup => pliGroup.Key);
    int counter = 0;           
    foreach (var pliGroup in pluginGroup)
    {
        var pliGroupOrdered = pliGroup.Key == bool.TrueString
            ? pliGroup.OrderBy(p => p.GetDisplayName())
            : pliGroup.OrderByDescending(p => p.GetDateModified() ?? DateTime.Now);

        foreach (PluginContainer pluginContainer in pliGroupOrdered)
        {
            string className = counter % 3 == 2 ? "end" : "";
        %>
    <li<%=!string.IsNullOrEmpty(className) ? string.Format(" class=\"{0}\"", className) : "" %>>
        <% Html.RenderPartialFromSkin("PluginDetails", new OxiteViewModelPartial<PluginContainer>(Model, pluginContainer)); %>
    </li><%
            counter++;
        }
    } %>
</ul><%
}
else
{ %>
    <div class="info message"><%=Model.Localize("Plugins.NothingAvailable", "There are no plugins available. <em>Some help text might be nice</em>") %></div><%
} %>