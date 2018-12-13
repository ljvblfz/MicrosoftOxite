<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<OxiteModelList<KeyValuePair<Tag, int>>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="sections">
        <div class="primary">
            <h2 class="title"><%
                if (Model.Site.HasMultipleAreas) { %><%=Html.Link(Model.Container.GetDisplayName(), Url.Posts(Model.Container as Area), new { @class = string.Format("area {0}", Model.Container.Name) }) %> &raquo; <% }
                %><%=Model.Localize("TagsTitle", "Tags") %></h2><%
        double? averagePostCount = null;
            double? standardDeviationPostCount = null;
            
            Response.Write(
                Html.UnorderedList(
                    Model.List.OrderBy(kvp => kvp.Key.Name),
                    t => string.Format(
                        "<a href=\"{2}\" rel=\"tag\" class=\"t{3}\">{0} ({1})</a> ",
                        t.Key.DisplayName,
                        t.Value,
                        Url.Posts(Model.Container as Area, t.Key),
                        t.Key.GetTagWeight(Model.List, ref averagePostCount, ref standardDeviationPostCount)
                    ),
                    "tagCloud"
                )
            ); %>
        </div>
        <div class="secondary"><% 
            Html.RenderPartialFromSkin("SideBar"); %>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Site.HasMultipleAreas ? Model.Container.GetDisplayName() : null, Model.Localize("Tags"))%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Scripts"><%
    Html.RenderScriptTag("base.js"); %>
</asp:Content>