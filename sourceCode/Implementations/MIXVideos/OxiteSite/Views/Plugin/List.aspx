<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Admin.master" Inherits="System.Web.Mvc.ViewPage<OxiteModelList<Oxite.BackgroundServices.IBackgroundService>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title"><%=Model.Localize("Plugins") %></h2>
    <p><%=Html.Link(Model.Localize("Plugins.Install.Title", "Install Plugin"), Url.PluginsInstall()) %></p><%
    if (Model.List.Count > 0)
    { %>
    <ul id="pluginCategories"><%
        string lastCategory = "";
        int categoryCount = 0;
        
        for (int i = 0; i < Model.List.Count; i++)
        {
            if (Model.List[i].Category != lastCategory)
            {
                if (i > 0)
                { %>
            </ul>
        </li><%
                } %>
        <li class="category m3<%=categoryCount++ % 3 %>">
            <h3><%=Model.List[i].Category %></h3>
            <ul class="plugins"><%
            } %>
                <li class="<%=(Model.List[i].Enabled ? "enabled" : "disabled") %>"><%=Html.Link(Model.List[i].Name, Url.Plugin(Model.List[i])) %></li><%
            lastCategory = Model.List[i].Category;
        } %>
            </ul>
        </li>
    </ul><%
    }
    else
    { %>
    <div class="message info"><%=Model.Localize("Plugins.NoneFound", "No plugins found") %></div><%
    } %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Plugins")) %>
</asp:Content>
