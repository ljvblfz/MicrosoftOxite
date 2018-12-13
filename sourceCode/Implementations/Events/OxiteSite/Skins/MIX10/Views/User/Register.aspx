<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyTag" runat="server" ><body id="register"></asp:Content>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1><%=Model.Localize("User.Register", "Register") %></h1>

    <form method="post" id="register" action="" class="register">
<%--        <p><%=Model.Localize("User.Register.Help", "This is some text explaining why we need this information and what it will be used for.") %></p>
--%>        <%=Html.ValidationSummary() %>
<%--        <img src="http://storage.live.com/mydata/myprofile/generalprofile/profilephoto" alt="" />
--%>        <fieldset class="username">
            <label for="username"><%=Model.Localize("Username") %></label>
            <%
                string userNameValue = "";
                string displayNameValue = "";
                try
                {
                    if (!String.IsNullOrEmpty(Request["displayName"]))
                    {
                        displayNameValue = Request["displayName"];
                    }

                    if (!String.IsNullOrEmpty(Request["username"]))
                        userNameValue = Request["username"];
                    else
                    {
                        if (Model.User != null)
                        {
                            UserUnregistered unregistered = Model.User.ToUserUnregistered();
                            if (unregistered != null && unregistered.AuthenticationValues != null)
                            {
                                string firstName = unregistered.AuthenticationValues["FirstName"] as String;
                                string lastName = unregistered.AuthenticationValues["LastName"] as String;

                                if (firstName != null && lastName != null)
                                {
                                    userNameValue = firstName + lastName;
                                    displayNameValue = firstName + " " + lastName;
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {

                }

                 %>
            <%=Html.TextBox("username", userNameValue, new { id = "username", @class = "text" })%>
        </fieldset>
        <fieldset class="displayName">
            <label for="displayName"><%=Model.Localize("Display Name") %></label>
            <%=Html.TextBox("displayName", displayNameValue , new { id = "displayName", @class = "text" })%>
        </fieldset>
        <input type="submit" value="Register" />
    </form>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("User.Register.Title", "Register")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>