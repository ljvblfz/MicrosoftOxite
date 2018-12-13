<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models.Extensions" %>
    <h2><%=Html.Link(Model.PartialModel.Title.CleanText().WidowControl(), Url.Post(Model.PartialModel))%></h2>
    <div class="posted meta">
        <ul class="more">
            <li class="share"><% Html.RenderPartialFromSkin("PostShare", new OxiteViewModelPartial<Post>(Model, Model.PartialModel)); %></li>
        </ul>
        <span>Posted <%=Html.Published(Model.PartialModel) %> by <%=Model.PartialModel.Creator.Name.CleanText() %></span>
    </div>
    <div><%=Html.Excerpt(Model.PartialModel, (key, def) => Model.Localize(key, def)) %></div>
    <p class="tags"><%=Model.Localize("Tags")%>: <%
if (Model.PartialModel.Tags.Count() > 0)
     %><%=string.Join(", ", Model.PartialModel.Tags.Select(t => Html.Link(t.GetDisplayName(), Url.Posts(t), new { rel = "tag" })).ToArray())%><%
else
    %><%=Model.Localize("none") %><%
    %></p>
