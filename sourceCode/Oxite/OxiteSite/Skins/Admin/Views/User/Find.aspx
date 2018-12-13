<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<User>>" %>
<%@ Import Namespace="Oxite.Infrastructure"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Membership.Models" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server"><%
UserSearchCriteria searchCriteria = Model.GetModelItem<UserSearchCriteria>(); %>
    <h2 class="title"><%=Model.Localize("Users.Find", "Find User")%></h2>
    <form action="" id="user" method="post">
        <div class="add"><%=Html.Link(Model.Localize("Users.Create", "Add a User"), Url.UserAdd())%></div>
        <div class="find"><%=Html.TextBox("userNameSearch", m => searchCriteria != null ? searchCriteria.Name : "", Model.Localize("Users.FindAUser", "Find a User"), new { size = 40, @class = "text" })%></div>
        <div class="buttons"><input type="submit" name="findUser" class="button submit" value="<%=Model.Localize("Find") %>" /><%=
            Html.OxiteAntiForgeryToken() %></div>
    </form><%
if (Model.Items != null && Model.Items.Count() > 0)
{ %>
    <ul class="users"><%
    //TODO: (erikpo) Grab a reference to the AuthorizationManager model item and use it to make sure the users list can be removed
    foreach (User user in Model.Items)
    { %>
        <li>
            <form action="<%=Url.UserRemove(user) %>" method="post">
                <fieldset>
                    <%=Html.Link(user.Name + (!string.IsNullOrEmpty(user.DisplayName) ? string.Format(" ({0})", user.DisplayName) : ""), Url.UserEdit(user)) %>
                    <%=Html.Hidden("userName", user.Name) %>
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
    <p><%=Model.Localize("Users.NoneFound", "No users found.") %></p><%
} %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Users.Find", "Find User")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>