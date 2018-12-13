<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<Plugin>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Plugins.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Plugins.Extensions" %>
<span class="icon"><%=Html.Image(
    ((!Model.PartialModel.Enabled
        ? Model.PartialModel.GetIconLargeDisabled()
        : Model.PartialModel.GetIconLarge())
    ?? Url.CssPath(string.Format("images/plugin_large{0}.png", (Model.PartialModel.Container.GetDateModified() ?? DateTime.Now) > Model.PartialModel.Container.CompilationDate ? "_warning" : !Model.PartialModel.Enabled ? "_disabled" : "" ), ViewContext)).CleanHref(),
    string.Format(Model.Localize("Plugin.IconAltFormat", "{0} icon"),
    Model.PartialModel.GetDisplayName().CleanAttribute()), new { height = 32, width = 32 }) %></span>
<span class="name"><%=Model.PartialModel.GetDisplayName().CleanText() %></span><%
    if (!string.IsNullOrEmpty(Model.PartialModel.GetDescription())) { %>
<span class="description metadata"><%=Model.PartialModel.GetDescription().Ellipsize(210, s => s.CleanText()) %></span><%
    } %>
<table class="plugin metadata">
    <tr class="author first">
        <th><%=Model.PartialModel.GetAuthors().Length > 1 ? Model.Localize("Plugin.Authors", "Authors") : Model.Localize("PluginAuthor", "Author")%></th>
        <td><%=Html.PluginAuthors() %></td>
    </tr>
    <tr class="version">
        <th><%=Model.Localize("Plugin.Version", "Version") %></th>
        <td><%=Model.PartialModel.GetVersion() != null ? Model.PartialModel.GetVersion().ToString().CleanText() : Model.Localize("Unknown", "&#8734;")%></td>
    </tr>
    <tr class="category">
        <th><%=Model.Localize("Plugin.Category", "Category") %></th>
        <td><%=Model.PartialModel.GetCategory().CleanText() ?? Model.Localize("None") %></td>
    </tr>
    <tr class="tags">
        <th><%=Model.Localize("Plugin.Tags", "Tags") %></th>
        <td><%=Model.PartialModel.GetTags().Length > 0 ? string.Join(", ", Model.PartialModel.GetTags()).CleanText() : Html.DefaultText(Model.Localize("None")) %></td>
    </tr><%
if (!string.IsNullOrEmpty(Model.PartialModel.GetHomePage()))
{
    %>
    <tr class="homepage">
        <th><%=Model.Localize("Plugin.HomePage", "Homepage")%></th>
        <td><%=Html.Link(Model.PartialModel.GetHomePage().EllipsizeUri(45, s => s.CleanText()), Model.PartialModel.GetHomePage().CleanHref())%></td>
    </tr><%
} %>
</table>
