<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<Plugin>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Plugins.Extensions"%>
<%

Exception exception = Model.PartialModel.Container.CompilationException;
   
%>
<table class="plugin metadata">
    <tr class="author first">
        <th><%=Model.Localize("Plugin.ExceptionType", "Exception Type") %></th>
        <td><%=exception.GetType().ToString().CleanText() %></td>
    </tr>
</table>