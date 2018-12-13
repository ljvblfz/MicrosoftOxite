<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models.Extensions" %>



<h2><%=Html.Link(Model.PartialModel.Title.CleanText().WidowControl(), Url.Post(Model.PartialModel))%></h2>
<div class="posted">By <%=Model.PartialModel.Creator.Name.CleanText()%> | <%=Html.Published(Model.PartialModel)%></div>