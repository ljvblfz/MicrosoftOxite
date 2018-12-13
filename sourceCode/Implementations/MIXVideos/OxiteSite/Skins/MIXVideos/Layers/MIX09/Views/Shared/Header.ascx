<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.ViewModels.Extensions" %>
            <div id="title">
                <h1><a href="http://live.visitmix.com"><%=Model.Site.DisplayName %></a></h1>
            </div>
            <div id="logindisplay"><% Html.RenderPartialFromSkin("LoginUserControl"); %></div>
            <div id="menucontainer">
                <div class="view">
                    <div><%=Model.Localize("View") %>:</div>
                    <ul>
                        <li class="first"><%=Html.Link("Thumbnails", Url.Posts(new Area() { Name = "MIX09" }), ViewContext.RouteData.GetRequiredString("controller") != "Post2" ? new { @class = "selected" } : null)%></li>
                        <li class="last"><%=Html.RouteLink("List All", "PostsAll", new { }, ViewContext.RouteData.GetRequiredString("controller") == "Post2" ? new { @class = "selected" } : null) %></li>
                    </ul>
                </div>
                <div class="subscribe">
                    <ul>
                        <li class="first"><%=Html.Link(Model.Localize("All"), Url.Posts(new Area() { Name = "MIX09" }, "RSS"))%></li>
                        <li><a href="<%=Url.RouteUrl("MIX09FileFeed", new { typeName = "WMV" }) %>">WMV</a></li>
                        <li><a href="<%=Url.RouteUrl("MIX09FileFeed", new { typeName = "WMVHigh" }) %>">WMV High</a></li>
                        <li><a href="<%=Url.RouteUrl("MIX09FileFeed", new { typeName = "MP4" }) %>">MP4</a></li>
                        <li><a href="<%=Url.RouteUrl("MIX09FileFeed", new { typeName = "WMA" }) %>">WMA</a></li>
                        <li class="last"><a href="<%=Url.RouteUrl("MIX09FileFeed", new { typeName = "MP3" }) %>">MP3</a></li>
                    </ul>
                    <div><!--<%=Html.SkinImage("/images/rssIcon2.gif", "", new { width = "16", height = "16" })%> --><%=Model.Localize("Subscribe") %>:</div>
                </div>
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