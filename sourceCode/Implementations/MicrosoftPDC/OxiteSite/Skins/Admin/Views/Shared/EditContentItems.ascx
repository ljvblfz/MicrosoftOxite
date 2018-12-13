<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ContentItemInput>>" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.CMS.Models"%>
<ul class="tab panes">
<%

var page = Model.GetModelItem<Oxite.Modules.CMS.Models.Page>();
var content = Model.Items;

if (content != null && content.Count() > 0)
{
%>
    <li id="editContent" class="active">
        <h3><%=Model.Localize("Content.Edit", "Edit Content") %></h3>
        <div class="pane">
            <form action="<%=page != null ? Url.PageEditContent(page) : Url.GlobalContentEdit() %>" method="post">
                <ul class="contentItems"><%
                foreach (var contentItemInput in content)
                {
                    %>
                    <li id="<%=contentItemInput.Name %>">
                        <fieldset>
                        <h4><%=contentItemInput.Name %></h4>
                        <%=Html.Hidden("content", contentItemInput.Name) %>
                        <%=Html.Hidden(string.Format("publishedDate_{0}", contentItemInput.Name), contentItemInput.Published.HasValue ? contentItemInput.Published.Value.ToString() : "") %>
                        <label for="<%=string.Format("displayName_{0}", contentItemInput.Name) %>"><%=Model.Localize("Content.DisplayName", "Display Name") %></label>
                        <%=Html.TextBox(
                            string.Format("displayName_{0}", contentItemInput.Name),
                            contentItemInput.DisplayName,
                            new { size = 42 }
                            ) %>
                        <label for="<%=string.Format("body_{0}", contentItemInput.Name) %>"><%=Model.Localize("Content.Body", "Body") %></label>
                        <%=Html.TextArea(
                            string.Format("body_{0}", contentItemInput.Name),
                            contentItemInput.Body,
                            10 /*rows*/,
                            100 /*cols*/,
                            new { @class = "html" }
                            ) %>
                        </fieldset>
                    </li><%
                } %>
                </ul>
                <fieldset>
                    <input type="submit" value="<%=Model.Localize("Content.SaveAll", "Save all content") %>" class="button submit" />
                    <%=Html.OxiteAntiForgeryToken() %>
                </fieldset>
            </form>
        </div>
    </li>
<%
} %>
    <li id="addContent"<%=content == null || content.Count() < 1 ? " class=\"active\"" : "" %>>
        <h3><%=Model.Localize("Content.Add", "Add Content") %></h3>
        <div class="pane">
            <form action="" method="post">
                <fieldset>
                    <label for="name"><%=Model.Localize("Content.Name", "Name") %></label>
                    <%=Html.TextBox(
                        "name",
                        "",
                        new { size = 21 }
                        ) %>
                    <label for="displayName"><%=Model.Localize("Content.DisplayName", "Display Name") %></label>
                    <%=Html.TextBox(
                        "displayName",
                        "",
                        new { size = 42 }
                        ) %>
                    <label for="body"><%=Model.Localize("Content.Body", "Body") %></label>
                    <%=Html.TextArea(
                        "body",
                        "",
                        10 /*rows*/,
                        100 /*cols*/,
                        new { @class = "html" }
                        ) %>
                </fieldset>
                <fieldset>
                    <input type="submit" value="<%=Model.Localize("Content.Add", "Add Content") %>" class="button submit" />
                    <%=Html.OxiteAntiForgeryToken() %>
                </fieldset>
            </form>
        </div>
    </li>
</ul>