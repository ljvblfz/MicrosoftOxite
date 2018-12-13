<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.ViewModels.Extensions" %>
            <div id="title">
                <h1><a href="http://live.visitmix.com"><%=Model.Site.DisplayName %></a></h1>
            </div>
            <div id="logindisplay"><% Html.RenderPartialFromSkin("LoginUserControl"); %></div>
            <div id="menucontainer">
                <div class="masthead"><%=Html.Link("Internet Explorer 8", Model.Container is Area ? Url.Posts(Model.Container as Area) : "/IE8") %></div>
                <%--<div class="subscribe"><%=Html.Link(Model.Localize("Subscribe"), Url.Posts("RSS")) %></div>--%>
                <ul class="menu nav">
                    <li><a id="aboutNav" href="http://live.visitmix.com/About/"><%=Model.Localize("About") %></a></li>
                    <li><a id="agendaNav" href="http://live.visitmix.com/Agenda/"><%=Model.Localize("Agenda") %></a></li>
                    <li><a id="registrationNav" href="http://live.visitmix.com/Registration/"><%=Model.Localize("Registration")%></a></li>
                    <li><a id="mixtifyNav" href="http://live.visitmix.com/MIXtify/"><%=Model.Localize("MIXtify")%></a></li>
                    <li><a id="sponsorsNav" href="http://live.visitmix.com/Sponsors/"><%=Model.Localize("Sponsors")%></a></li>
                    <li><a id="worldwideNav" href="http://visitmix.com/Worldwide/"><%=Model.Localize("Worldwide")%></a></li>
                    <li><a id="mixonlineNav" href="http://visitmix.com/"><%=Model.Localize("MIX Online") %></a></li>
                </ul><%
    if (Model.User.GetCanAccessAdmin())
    { %>
                <ul class="menu admin">
                    <li><%=Html.Link(Model.Localize("Admin", "Admin"), Url.Admin(), new { @class = "admin" })%></li>
                    <li><%=Html.Link(Model.Localize("AddPostLinkText", "Add Post"), Url.AddPost(Model.Container as Area))%></li>
                    <li><%=Html.Link(Model.Localize("AddPageLinkText", "Add Page"), Url.AddPage())%></li>
                    <li><%=Html.Link(Model.Localize("ManageSiteLinkText", "Manage Site"), Url.ManageSite())%></li>
                </ul><%
    } %>
            </div>