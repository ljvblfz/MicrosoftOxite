<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<Oxite.Modules.Conferences.Models.Exhibitor>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Conferences.Extensions" %>
<%@ Import Namespace="Oxite.Plugins" %>
<%@ Import Namespace="Oxite.Plugins.Extensions" %>
<%
if (Model.Items.Count() > 0)
{ 
%>
    <ul>    
<%
    foreach(var exhibitor in Model.Items.OrderBy(p => p.Name))
    {
%>
        <li>
            <%= Html.Link(exhibitor.Name, Url.EditExhibitor(exhibitor.Name), "exhibitor") %>
            <a href="<%= Url.RemoveExhibitor(exhibitor.Name) %>">
                <%= Html.SkinImage("/images/delete.png",
                                   Model.Localize("remove"),
                                                  new {
                                                          onclick = string.Format("if (window.confirm('{0}')){{window.document.location='{1}';}}return false;", Model.Localize("Do you really want to remove this exhibitor?"), 
                                                          Url.RemoveExhibitor(exhibitor.Name))
                                                      }
                                                     )%>
            </a>
        </li>
<%
    }
%>
    </ul>
<%
}
else
{ %>
    <div class="info message"><%=Model.Localize("Exhibitors.NoneFound", "There are currently no exhibitors for this event.") %></div><%
} %>