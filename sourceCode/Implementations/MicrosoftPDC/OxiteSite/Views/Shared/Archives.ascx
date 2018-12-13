<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<ArchiveViewModel>>" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.ViewModels" %>
<%@ Import Namespace="Oxite.Extensions" %><%
if (Model.PartialModel != null)
{
    var months = Model.PartialModel.Archives; %>
        <div class="sub archives">
            <h3><%=Model.Localize("Archives") %></h3><%
    if (Model.PartialModel.Archives != null && Model.PartialModel.Archives.Count() > 0)
    {
        if (months.Count() > 20)
        { %>
            <% Html.RenderPartialFromSkin("ArchivesByYear"); %><%
        }
        else
        {
            Response.Write(
                Html.UnorderedList(
                months,
                t => Html.Link(
                    string.Format("{0:MMMM yyyy} ({1})", t.Key.ToDateTime(), t.Value),
                    Url.Posts(t.Key.Year, t.Key.Month)),
                "archiveMonthList"
                )
            );
        } 
    }
    else
    {
        %><div class="message info"><%=Model.Localize("NoneFound", "None found.")%></div><%
    } %>
        </div><%
} %>
