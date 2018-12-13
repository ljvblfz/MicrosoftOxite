<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<PostInput>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<%@ Import Namespace="Oxite.Modules.Blogs.ViewModels" %><%

BlogListViewModel blogListViewModel = Model.GetModelItem<BlogListViewModel>();

 %><fieldset class="title">
    <%=Html.ValidationMessage("Post.Title", Model.Localize("Title isn't valid.")) %>
    <%=Html.TextBox(
        "title",
        m => m.Item.Title,
        Model.Localize("Post.Title", "Title"),
        new { id = "post_title", @class = "text", size = "60" }
        ) %>
    <%=Html.OxiteAntiForgeryToken() %>
</fieldset>
<fieldset class="url">
    <label for="post_slug"><%=Model.Localize("Post.Url", "Location") %></label><%=Html.ValidationMessage("Post.Slug", Model.Localize("URL isn't valid.")) %>
    <span><%=string.Format(
                "{0}{1}/",
                Url.AbsolutePath(Url.Home()),
                blogListViewModel.Blogs.Count() > 1
                    ? Html.DropDownList(
                        "blogName",
                        blogListViewModel.Blogs,
                        a => Url.Posts(a),
                        a => a.Name,
                        Model.Item.BlogName,
                        new { id = "post_blog" },
                        true)
                    : Url.Posts(blogListViewModel.Blogs.FirstOrDefault()) + Html.Hidden("blogName", blogListViewModel.Blogs.FirstOrDefault().Name)
              ) 
    %></span>
    <%=Html.TextBox(
        "slug", 
        Model.Item.Slug,
        new { id = "post_slug", @class = "text", size = "60" }
        ) %>
</fieldset>
<fieldset class="excerpt">
    <%=Html.ValidationMessage("Post.BodyShort", Model.Localize("Post.ExcerptEdit", "Excerpt isn't valid.")) %>
    <%=Html.TextArea(
        "bodyShort",
        m => m.Item.BodyShort,
        6 /*rows*/,
        100 /*cols*/,
        Model.Localize("Post.Excerpt", "Excerpt"),
        new { @class = !string.IsNullOrEmpty(Model.Item.BodyShort) ? "hasValue" : "" }
        ) %>
</fieldset>
<fieldset class="content">
    <%=Html.ValidationMessage("Post.Body", Model.Localize("Post.BodyEdit", "Body isn't valid.")) %>
    <%=Html.TextArea(
        "body",
        m => m.Item.Body,
        18 /*rows*/,
        100 /*cols*/,
        Model.Localize("Post.Body", "Content"),
        new { @class = "html" }
        ) %>
</fieldset>