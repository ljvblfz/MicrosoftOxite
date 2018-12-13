<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelList<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %><%
    ArchiveData archiveData = ((ArchiveContainer)Model.Container).ArchiveData;
%><%=Model.Localize("Archives") 
%> / <%=Html.Link(archiveData.Year.ToString(), Url.Posts(archiveData.Year))
%><%=archiveData.Month > 0 ? string.Format(" / {0}", Html.Link(archiveData.ToDateTime().ToString("MMMM"), Url.Posts(archiveData.Year, archiveData.Month))) : ""
%><%=archiveData.Day > 0 ? string.Format(" / {0}", Html.Link(archiveData.Day.ToString(), Url.Posts(archiveData.Year, archiveData.Month, archiveData.Day))) : ""
%>