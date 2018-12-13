<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<% 
    Html.RenderPartialFromSkin("CallToAction");
    Html.RenderPartialFromSkin("WhatsHappening");
    Html.RenderPartialFromSkin("ConnectLinks");
    //Html.RenderPartialFromSkin("TwitterFeed");
    Html.RenderPartialFromSkin("Sponsors"); %>