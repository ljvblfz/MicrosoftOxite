<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.ViewModels.Extensions" %>
            <div id="title">
                <h1><a href="<%=Url.Posts() %>"><%=Model.Site.DisplayName %></a></h1>
            </div>
            <div id="logindisplay"><% Html.RenderPartialFromSkin("LoginUserControl"); %></div><%
    if (Model.User.GetCanAccessAdmin())
    { %>
            <div id="menucontainer">
                <ul class="menu admin">
                    <li><%=Html.Link(Model.Localize("Admin", "Admin"), Url.Admin(), new { @class = "admin" })%></li>
                    <li><%=Html.Link(Model.Localize("AddPostLinkText", "Add Post"), Url.AddPost(Model.Container as Area))%></li>
                    <li><%=Html.Link(Model.Localize("AddPageLinkText", "Add Page"), Url.AddPage())%></li>
                    <li><%=Html.Link(Model.Localize("ManageSiteLinkText", "Manage Site"), Url.ManageSite())%></li>
                </ul>
            </div><%
    } %>
            <div class="sub search">
                <form id="searchInHeader" method="get" action="<%=Url.Search() %>">
                    <fieldset>
                        <label for="search_term_in_header"><%=Model.Localize("SearchInputTitle", "Search this site...")%></label>
                        <input class="text" id="search_term_in_header" name="term" title="<%=Model.Localize("SearchInputTitle", "Search this site...")%>" type="text" value="" />
                        <input type="submit" value="Search" class="button" />
                    </fieldset>
                </form>
            </div>
            <% Html.RenderPartialFromSkin("ConferenceList"); %>