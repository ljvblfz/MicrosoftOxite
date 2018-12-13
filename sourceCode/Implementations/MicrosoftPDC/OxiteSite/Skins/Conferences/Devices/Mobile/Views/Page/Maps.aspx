<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<string>>"  %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Maps"))%>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
<% 
	string mapType = Model.Items.First();
	string mapClass1 = string.Empty;
	string mapClass2 = string.Empty;
	string mapClass3 = string.Empty;
   string mapClass4 = string.Empty;
   
	if(string.IsNullOrEmpty(mapType))
	{
		mapType = "conventioncenter";
	}
	
	switch (mapType.ToLower())
	{
		case "conventioncenter":
			mapClass1 = "active";
			break;
		case "lastreetmap":
			mapClass2 = "active";
			break;
		case "restaurants":
			mapClass3 = "active";
			break;
      case "hotel":
         mapClass4 = "active";
         break;
		default:
			break;
	}

%>

<ul>
	<li class="<%=mapClass1 %>"><a href="/Maps">PDC09</a></li>
	<li class="<%=mapClass2 %>"><a href="/Maps/LaStreetMap">City</a></li>
	<li class="<%=mapClass3 %>"><a href="/Maps/Restaurants">Food</a></li>
	<li class="<%=mapClass4 %>"><a href="/Maps/Hotel">Hotels</a></li>
</ul>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% 
	string mapType = Model.Items.First();
	
	if(string.IsNullOrEmpty(mapType))
	{
		mapType = "conventioncenter";
	}
%>
	
<%if (mapType.ToLower() == "conventioncenter") { %>
	<img src="<%= ResolveClientUrl("../../Styles/images/g_map.gif") %>" border="0" alt="" />
<%} else if (mapType.ToLower() == "lastreetmap") { %>
	
<%} else if (mapType.ToLower() == "resaurants") { %>

<%} %>

<%--<%=Html.Content("Content") %>--%>
</asp:Content>
