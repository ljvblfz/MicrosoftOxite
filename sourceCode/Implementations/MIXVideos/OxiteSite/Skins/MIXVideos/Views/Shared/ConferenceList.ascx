<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<div class="sub areas">
    <h3><%=Model.Localize("Conferences") %></h3>
    <ul><li class="first"><a class="MIX09" href="/MIX09">MIX 09</a></li><li><a class="MIX08" href="/MIX08">MIX 08</a></li><li><a class="MIX07" href="/MIX07">MIX 07</a></li><li class="last"><a class="MIX06" href="/MIX06">MIX 06</a></li></ul>
</div>