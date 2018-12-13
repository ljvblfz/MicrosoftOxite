<%@ Page 
    Language="C#" 
    AutoEventWireup="true"
    MasterPageFile="../Shared/Site.master" 
    Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%@ Import Namespace="Oxite.Extensions"%>
<asp:Content ID="HeaderSiteName" ContentPlaceHolderID="HeaderSiteName" runat="server">
    <h1 id="siteName"><%=Html.Link("Microsoft PDC09", Url.Home()) %></h1>
</asp:Content>
<asp:Content ContentPlaceHolderID="Title" runat="server">
    <% var userDisplayName = ViewData["UserDisplayName"] ?? "?"; %>
    <%= userDisplayName + "'s Sessions :: " + Html.PageTitle() %>
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div id="browser">
        <div id="sessions" class="sessions">
            <% Html.RenderPartialFromSkin("UserScheduleItemsList"); %>
            <div class="pager">
                <a href="/Sessions" class="allSessions"><%=Model.Localize("see all sessions »")%></a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="Scripts" runat="server">
<%
    Html.RenderScriptTag("base.js");
    Html.RenderScriptTag("http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.js", "http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js");
    Html.RenderScriptTag("HeroBanner.js");
    Html.RenderScriptTag("Sessions.js?ver=2");
%>
<script type="text/javascript">
    // Wireup AJAX for checkbox
    $("#makeschedulepublic").click(function(e) {
        var postUrl = "/Schedule/Share/Json";
        var postData = "schedulesharing=Submit";
        var cb = $(this).get(0).checked;
        if (cb) {
            postData += "&makeschedulepublic=true";
        }

        // Expected result:
        // Success: { result : "success" }
        // Error: { result : "error", message : "[error messaging]" }
        $.ajax({
            method: "POST",
            dataType: "json",
            //##DEV url below
            url: postUrl,
            //##INT url below
            //url : "/Sessions",
            data: postData,
            beforeSend: function() {
                $("#makeschedulepublic").attr("disabled", "disabled");
                $("body").addClass("wait");
            },
            complete: function() {
                $("#makeschedulepublic").attr("disabled", "");
                $("body").removeClass("wait");
            },
            success: function(data) {
                if (data.result == "error") {
                    // In case of AJAX failure, submit the form via HTTP
                    $("#schedulesharing").trigger("click");
                }
            },
            error: function(xhr, ajaxOptions, thrownError) {
                // In case of AJAX failure, submit the form via HTTP
                $("#schedulesharing").trigger("click");
            }
        });
    });
</script>
</asp:Content>
