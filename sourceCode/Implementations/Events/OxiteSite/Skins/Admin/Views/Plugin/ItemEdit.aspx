<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<PluginEditInput>>" %>
<%@ Import Namespace="Oxite.Plugins"%>
<%@ Import Namespace="Oxite.Plugins.Extensions"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Infrastructure" %>
<%@ Import Namespace="Oxite.Modules.Plugins.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Plugins.Models" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server"><%
PluginContainer pluginContainer = Model.GetModelItem<PluginContainer>(); %>
    <div class="sections">
        <div class="primary">
            <ul class="tab panes"><%
                if (pluginContainer.Tag is Plugin)
                { %>
                <li<%=pluginContainer.CompilationException == null ? " class=\"active\"" : "" %> id="editSettings">
                    <h3><%=Model.Localize("Plugin.Settings", "Settings") %></h3>
                    <div class="pane">
                        <% Html.RenderPartialFromSkin("PluginEditSettings"); %>
                    </div>
                </li>
                <li<%=pluginContainer.CompilationException != null ? " class=\"active\"" : "" %> id="editCode"><%
                }
                else
                {
                %><li class="active" id="editCode"><%
                }%>
                    <h3><%=Model.Localize("Plugin.Code", "Code") %></h3>
                    <div class="pane">
                        <% Html.RenderPartialFromSkin("PluginEditCode"); %>
                    </div>
                </li>
            </ul>
        </div>
        <div class="secondary">
            <% Html.RenderPartialFromSkin("PluginDetailsSmall", new OxiteViewModelPartial<PluginContainer>(Model, pluginContainer)); %>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Plugin.Edit", "Edit Plugin"), Model.GetModelItem<PluginContainer>().GetDisplayName().CleanText())%>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
    <h2><%=Html.Link(
            Model.Localize("Plugins"),
            Url.Plugins(),
            new { @class = "plugins" }
            ) %> &gt; <%=Model.Localize("Plugins.Edit", "Edit") %></h2>
    <h3><%=Html.Image(
            (Model.GetModelItem<PluginContainer>().GetIconLarge() ?? Url.CssPath("images/plugin_large.png", ViewContext)).CleanHref(),
            string.Format(Model.Localize("Plugin.IconAltFormat", "{0} icon"),
            Model.GetModelItem<PluginContainer>().GetDisplayName().CleanAttribute()),
            new { height = 32, width = 32 })
            %> <%=Model.GetModelItem<PluginContainer>().GetDisplayName().CleanText() %></h3>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js");
 %>
</asp:Content>