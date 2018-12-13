<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models.Extensions" %>
    <h2><%=Html.Link(Model.PartialModel.Title.CleanText(), Url.Post(Model.PartialModel))%></h2>
    <div class="posted">Posted <%=Html.Published(Model.PartialModel) %> by <%=Model.PartialModel.Creator.Name.CleanText() %></div>
    
    <div class="body"><%=Html.ExcerptWithoutLink(Model.PartialModel, (key, def) => Model.Localize(key, def)) %> <%=Html.Link(Model.Localize("Post.More", "Click To View Video &#0187;"), Url.Post(Model.PartialModel), new { @class = "viewfullpost" })%>
    <ul class="more">
        <li><% Html.RenderPartialFromSkin("PostShare", new OxiteViewModelPartial<Post>(Model, Model.PartialModel)); %></li>
    </ul>
    </div>
