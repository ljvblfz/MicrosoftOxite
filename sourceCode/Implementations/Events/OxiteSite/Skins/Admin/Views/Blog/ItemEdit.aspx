<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItem<Blog>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="sections">
        <div class="lone"><%
        if (Model.Item != null && !Model.Site.HasMultipleBlogs)
        { %>
            <div class="message info"><%=string.Format(Model.Localize("Site.HasSingleBlog", "This site only has one blog.  The below settings can be changed on the <a href=\"{0}\">site settings page</a>"), Url.Site()) %></div><%
        } %>
            <%=Html.ValidationSummary() %>
            <form action="" method="post"><%
            if (Model.Item != null)
            { %>
                <div><input type="hidden" name="blogID" value="<%=Model.Item.ID.ToString() %>" /></div><%
            } %>
                <fieldset>
                    <%=Html.TextBox("blogName", m => m.Item != null ? m.Item.Name : "", "Name", Model.Item == null || Model.Site.HasMultipleBlogs, new { size = 20, @class = "text" })%>
                    <%=Html.TextBox("blogDisplayName", m => m.Item != null ? m.Item.DisplayName : "", "Display Name", Model.Item == null || Model.Site.HasMultipleBlogs, new { size = 40, @class = "text" })%>
                    <%=Html.TextArea("blogDescription", m => m.Item != null ? m.Item.Description : "", 6, 80, "Description", Model.Item == null || Model.Site.HasMultipleBlogs, new { @class = "text" })%>
                </fieldset>
                <fieldset>
                    <%=Html.CheckBox("blogCommentingEnabled",
                        m => m.Item != null ? !m.Item.CommentingDisabled : true,
                        Model.Localize("CommentingEnabled", "Commenting Enabled")
                        )%>
                </fieldset><%
            if (Model.Item != null)
            { %>
                <div><%=Html.Link(Model.Localize("BlogML.LinkTitle", "BlogML Import/Export"), Url.BlogML(Model.Item)) %></div><%
            } %>
                <div class="buttons">
                    <input type="submit" name="addBlog" class="button submit" value="<%=Model.Localize("Save") %>"/>
                    <%=Html.OxiteAntiForgeryToken() %>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Admin"), Model.Localize("Blogs"), Model.Item == null ? Model.Localize("Add") : Model.Localize("Edit")) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentHeader" runat="server">
    <h2><%=Html.Link(
            Model.Localize("Blogs"),
            "#",
            new { @class = "blogs" }
            ) %> &gt; <%=Model.Item == null ? Model.Localize("Blogs.Add", "Add") : Model.Localize("Blogs.Edit", "Edit")%></h2>
</asp:Content>
<asp:Content ContentPlaceHolderID="Scripts" runat="server"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>