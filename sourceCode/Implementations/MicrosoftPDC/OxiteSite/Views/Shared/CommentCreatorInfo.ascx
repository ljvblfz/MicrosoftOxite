<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<PostComment>>" %>
<%@ Import Namespace="Oxite.Extensions" %> 
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %>
<span><%=Model.Localize("said") %><br /><%=
        Html.Link(Model.PartialModel.Created.ToRelativeDateTime(), Url.Comment(Model.PartialModel)) %></span>