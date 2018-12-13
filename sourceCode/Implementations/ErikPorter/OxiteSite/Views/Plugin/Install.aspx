<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Shared/Admin.master" Inherits="System.Web.Mvc.ViewPage<OxiteModelList<Oxite.Infrastructure.IPlugin>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title"><%=Model.Localize("Plugins.Install.Title", "Install Plugins")%></h2>
    <h3><%=Model.Localize("Plugins.Install.Description", "The following are the available plugins that can be installed.")%></h3><%
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
                <li class="enabled">
                    <form method="post" action="">
                        <h3><%=Model.List[i].Name %></h3>
                        <div><input type="hidden" name="pluginName" value="<%=Model.List[i].Name %>" /></div>
                        <div><input type="hidden" name="pluginCategory" value="<%=Model.List[i].Category %>" /></div>
                        <div><input type="submit" value="<%=Model.Localize("Install") %>" /></div>
                    </form>
                </li><%
            lastCategory = Model.List[i].Category;
        } %>
            </ul>
        </li>
    </ul><%
    }
    else
    { %>
    <div class="message info"><%=Model.Localize("Plugins.Install.NoneFound", "No plugins available to install were found.") %></div><%
    } %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Plugins"), Model.Localize("Install")) %>
</asp:Content>
