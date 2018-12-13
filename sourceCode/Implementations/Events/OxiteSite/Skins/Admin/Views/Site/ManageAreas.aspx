<%@ Page Language="C#" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.ViewModels" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server"><%
    BlogAdminDataViewModel adminData = Model.GetModelItem<BlogAdminDataViewModel>(); %>
    <h2 class="title"><%=Model.Localize("ManageBlogs", "Manage Blogs")%></h2>
    <ul>
        <li><%/*=Html.Link(Model.Localize("Blogs.Manage", "Edit Blog"), Model.Site.HasMultipleBlogs ? Url.BlogFind() : Url.BlogEdit(adminData.Blogs.ElementAt(0)))*/%></li>
        <li><%=Html.Link(Model.Localize("Blogs.Add", "Add New blog"), Url.BlogAdd()) %></li>
        <li><%/*=Html.Link("BlogML", Model.Site.HasMultipleBlogs ? Url.BlogFind() : Url.BlogML(adminData.Blogs.ElementAt(0)))*/ %></li>
    </ul>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("ManageBlogs", "Manage Blogs"))%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Scripts"><%
    Html.RenderScriptTag("base.js");
 %>
</asp:Content>