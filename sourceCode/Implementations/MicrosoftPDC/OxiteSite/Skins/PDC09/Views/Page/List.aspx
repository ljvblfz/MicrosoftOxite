<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Oxite.Modules.CMS.Models.Page>>" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models"%>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions" %>
<%@ Import Namespace="Oxite.Modules.CMS.Models" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>Pages</h1>
<%if (Model.Items != null && (Model.Items.Count() > 0))
{ %><ul class="pages small"><%
    int counter = 0;
    foreach (Oxite.Modules.CMS.Models.Page page in Model.Items)
    {
        StringBuilder className = new StringBuilder("page", 15);
        
        if (page.Equals(Model.Items.First())) { className.Append(" first"); }
        if (page.Equals(Model.Items.Last())) { className.Append(" last"); }

        if (counter % 2 != 0) { className.Append(" odd"); }
        %>
    <li class="<%=className.ToString() %>">
     <%=Html.Link(page.Title, Url.Page(page)) %>
    </li><%
        counter++;
    } %>
</ul><% 
} 
else
{ //todo: (nheskew) need an Html.Message html helper extension method that takes a message %>
<div class="message info"><%=Model.Localize("NoneFound", "None found.")%></div><%        
} %></asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">Pages</asp:Content>
<asp:Content ContentPlaceHolderID="MetaDescription" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js");
    Html.RenderScriptTag("Blog.js");                                                             
    %>
</asp:Content>
<asp:Content ID="searchtags" ContentPlaceHolderID="SearchTags" runat="server"><%=Html.SearchTag("PageType", "List", false)%></asp:Content>
