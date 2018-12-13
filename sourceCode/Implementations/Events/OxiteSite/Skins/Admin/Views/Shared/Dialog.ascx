<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<Dialog>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<div id="dialog" class="<%=Model.Item.Format.CssClass %>">
    <p><%=Model.Item.Message %></p><%
    foreach (DialogButton button in Model.Item.Buttons)
    { %>
    <form method="post" action="<%=Request.Url %>" class="<%=button.CssClass %>">
        <input type="submit" name="<%=button.Name %>" value="<%=button.DisplayName %>">
        <input type="hidden" name="selectedButton" value="<%=button.Name %>"><%
        if (button.PostData)
        {
            foreach (string key in Request.Form.AllKeys)
            { %>
        <input type="hidden" name="<%=key %>" value="<%=Request.Form[key] %>"><%
            }
        }
        else
        { %>
        <input type="hidden" name="returnUrl" value="<%=Request.Form["returnUrl"] %>">
        <%=Html.OxiteAntiForgeryToken() %><%
        } %>
    </form><%
    } %>
</div>
