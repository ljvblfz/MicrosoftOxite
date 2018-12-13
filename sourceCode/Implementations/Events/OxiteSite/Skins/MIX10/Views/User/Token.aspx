<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Sync.master" Inherits="System.Web.Mvc.ViewPage<OxiteViewModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<asp:Content ID="bodyTag" runat="server" ContentPlaceHolderID="bodyTag"><body class="syncclient"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% if (!Model.User.IsAuthenticated)
 {
     %>
    <h1><%=Model.Localize("User.Token", "Sync Framework Client") %></h1>
     <%=Html.Link(Model.Localize("UserToken.SignIn", "Sign In to get your Token"), Model.SignInUrl)%>
     <%
 }
       else if (ViewData["APIToken"] == null)
       {
     %>
    <h1><%=Model.Localize("User.Token", "Sync Framework Client") %></h1>
     <%=Model.Localize("User.Token.None", "No user token found.")%>
     <%
         }
       else
       {
           string apiToken = ViewData["APIToken"] as string;
           string logOutURL = Model.SignOutUrl;

           if (!String.IsNullOrEmpty(apiToken))
           {%>
       <div id="silverlightControlHost">
        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="100%">
		  <param name="source" value="/ClientBin/Mix2010.xap"/>
		  <param name="background" value="white" />
		  <param name="minRuntimeVersion" value="3.0.40818.0" />
		  <param name="autoUpgrade" value="true" />
		  <param name="enablehtmlaccess" value="true" />
		  <param name="initParams" value="token=<%=apiToken%>,signouturl=<%=logOutURL%>" />
		  <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration:none">
 			  <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style:none"/>
		  </a>
	    </object>	    
	    <iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe>
	    </div>
          <%
           }
       }%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("User.Token.Title", "MIX10 Session Planner (Beta)")) %>
</asp:Content>
