<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
        <div class="sub feeds">
            <h3><%=Model.Localize("Subscribe") %></h3>
            <ul>
                <li><%=Html.Link(Model.Localize("All"), Url.Posts("RSS")) %></li>
            </ul>
        </div>