<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<TrackbackViewModel>" %>
<%@ Import Namespace="Oxite.Modules.Blogs.ViewModels" %>
<response>
    <error><%=Model.ErrorCode %></error>
    <%if (!string.IsNullOrEmpty(Model.ErrorMessage))
      {%>
    <message><%=Model.ErrorMessage%></message>
    <%} %>
</response>