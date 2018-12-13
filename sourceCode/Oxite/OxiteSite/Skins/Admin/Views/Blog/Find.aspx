<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<Blog>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server"><%
    BlogSearchCriteria searchCriteria = Model.GetModelItem<BlogSearchCriteria>(); %>
    <div class="sections">
        <div class="lone">
            <form action="" id="blog" method="post">
                <div class="add"><%=Html.Link(Model.Localize("Blogs.Create", "Add a Blog"), Url.BlogAdd())%></div>
                <div class="find"><%=Html.TextBox("blogNameSearch", m => searchCriteria != null ? searchCriteria.Name : "", "Find a Blog", new { size = 40, @class = "text" })%></div>
                <div class="buttons"><input type="submit" name="findBlog" class="button submit" value="<%=Model.Localize("Find") %>" /><%=
                    Html.OxiteAntiForgeryToken() %></div>
            </form><%
        if (Model.Items != null && Model.Items.Count() > 0)
        {
            Response.Write(
                Html.UnorderedList(
                    Model.Items,
                    a => string.Format("<a href=\"{1}\">{0}</a>",
                        a.Name + (!string.IsNullOrEmpty(a.DisplayName) ? string.Format(" ({0})", a.DisplayName) : ""),
                        Url.BlogEdit(a)
                    ),
                    "blogs"
                    )
            );
        }
        else if (searchCriteria != null)
        { %>
            <p><%=Model.Localize("Blogs.NoneFound", "No blogs found.") %></p><%
        } %>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Blogs.Find", "Find Blog")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>