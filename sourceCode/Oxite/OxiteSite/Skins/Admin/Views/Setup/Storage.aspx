<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Setup.master"
    Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<SetupInput>>" %>

<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Setup.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Setup.Models" %>
<%@ Import Namespace="Oxite.Modules.Setup" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="title">
        <%=Model.Localize("Site.Add", "Initial Site Setup")%></h2>
    <%=Html.ValidationSummary() %>
    <form action="<%=Url.SetupAdminSettings()%>" method="post" id="siteSettings">
    <div>
        <%
            foreach (string key in Request.Form.AllKeys)
            { %>
                <input type="hidden" name="<%=key %>" value="<%=Request.Form[key] %>"><%
            }
        %>
    </div>
    <h3>
        <%=Model.Localize("Setup.Storage", "What is your storage mechanism?") %></h3>
    <div>
        <%=Html.RadioButton("storageType", StorageType.Xml, Model.Item.StorageType == StorageType.Xml, new { id = "useXmlStorage", size = 60, @class = "checkbox" }, "Xml Files")%></div>
    <div>
        <%=Html.RadioButton("storageType", StorageType.Sql, Model.Item.StorageType == StorageType.Sql, new { id = "useSqlStorage", size = 60, @class = "checkbox" }, "SQL Server")%></div>
    <div class="buttons">
        <input type="submit" name="submit" class="button submit" value="<%=Model.Localize("Site.Next", "Next") %>" />
        <%=Html.Button(
                "Cancel",
                Model.Localize("Cancel"),
                new { @class = "cancel", onclick = string.Format("if (window.confirm('{0}')){{window.document.location='{1}';}}return false;", Model.Localize("really?"), Url.ManageSite()) }
                )%>
        <%=
            Html.OxiteAntiForgeryToken() %>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Site"), Model.Localize("Setup")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <%
        Html.RenderScriptTag("base.js"); %>
</asp:Content>
