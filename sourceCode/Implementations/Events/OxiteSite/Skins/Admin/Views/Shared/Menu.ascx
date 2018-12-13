<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %><%
if (Model.User.IsInRole("Admin"))
{  
    %><ul class="admin menu" id="adminMenu">
        <li class="first">
            <% Html.RenderPartialFromSkin("QuickMenu"); %>
        </li>
        <li class="last">
            <% Html.RenderPartialFromSkin("SettingsMenu"); %>
        </li>
    </ul><%
}
%>