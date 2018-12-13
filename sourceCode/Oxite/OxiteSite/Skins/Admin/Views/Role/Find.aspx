<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Role>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Membership.Models" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server"><%
RoleSearchCriteria searchCriteria = Model.GetModelItem<RoleSearchCriteria>(); %>
    <h2 class="title"><%=Model.Localize("Roles.Find", "Find Role")%></h2>
    <form action="" id="role" method="post">
        <div class="add"><%=Html.Link(Model.Localize("Roles.Create", "Add a Role"), Url.RoleAdd())%></div>
        <div class="find">
            <%=Html.TextBox("roleNameSearch", m => searchCriteria != null ? searchCriteria.RoleName : "", Model.Localize("Roles.FindARole", "Find a Role"), new { size = 40, @class = "text" })%><br />
            <%=Html.CheckBox("roleTypeSite", m => searchCriteria != null ? (searchCriteria.RoleType & RoleType.Site) == RoleType.Site : false, "Site", null) %>
            <%=Html.CheckBox("roleTypeBlog", m => searchCriteria != null ? (searchCriteria.RoleType & RoleType.Blog) == RoleType.Blog : false, "Blog", null) %>
            <%=Html.CheckBox("roleTypePost", m => searchCriteria != null ? (searchCriteria.RoleType & RoleType.Post) == RoleType.Post : false, "Post", null) %>
            <%=Html.CheckBox("roleTypePage", m => searchCriteria != null ? (searchCriteria.RoleType & RoleType.Page) == RoleType.Page : false, "Page", null) %>
        </div>
        <div class="buttons"><input type="submit" name="findRole" class="button submit" value="<%=Model.Localize("Find") %>" /><%=
            Html.OxiteAntiForgeryToken() %></div>
    </form><%
    if (Model.Items != null && Model.Items.Count() > 0)
    { %>
    <ul class="roles"><%
        //TODO: (erikpo) Grab a reference to the AuthorizationManager model item and use it to make sure the roles list can be removed
        foreach (Role role in Model.Items)
        { %>
        <li>
            <form action="<%=Url.RoleRemove(role) %>" method="post">
                <fieldset>
                    <%=Html.Link(role.Name, Url.RoleEdit(role)) %>
                    <%=Html.Hidden("roleName", role.Name) %>
                    <%=Html.OxiteAntiForgeryToken() %>
                    <input type="image" src="<%=Url.CssPath("images/delete.png", ViewContext) %>" class="ibutton remove" />
                </fieldset>
            </form>
        </li><%
        } %>
    </ul><%
    }
    else if (searchCriteria != null)
    { %>
    <p><%=Model.Localize("Roles.NoneFound", "No roles found.") %></p><%
    } %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Roles.Find", "Find Role")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>