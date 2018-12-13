<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItem<PostInput>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Models" %><%
IDictionary<string, object> extendedProperties = Model.GetModelItem<IDictionary<string, object>>();
bool tweetThis = extendedProperties != null && extendedProperties.ContainsKey("TweetThis") && extendedProperties["TweetThis"] is bool ? (bool)extendedProperties["TweetThis"] : true; %>
<fieldset class="twitter">
    <legend>Twitter</legend>
    <%=Html.PluginCheckBox("TweetThis", m => tweetThis, Model.Localize("Post.TweetThis", "Tweet this post when it's published"))%>
</fieldset>