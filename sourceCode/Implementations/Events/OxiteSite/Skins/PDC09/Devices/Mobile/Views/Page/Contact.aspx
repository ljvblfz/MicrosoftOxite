<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Shared/Site.master"  Inherits="System.Web.Mvc.ViewPage<OxiteViewModelItems<string>>"  %>
<%@ Import Namespace="Oxite.Modules.CMS.Extensions"%>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%=Model.Localize("Contact Us") %></h1>
    <% if (Model.Items.Count() > 0)
       {  %>    
			<p>Thank you for your feedback, it has been sent along to the Microsoft PDC team.</p>
    <% }
       else
       {%>
		<form method="post" id="contactUs" action="" class="contactUs">
		<p><%=Model.Localize("ContactUsHelp", "Please use the form below to send us any comments or questions you have.")%></p>
		<%=Html.ValidationSummary()%>
        <fieldset class="subject">
            <label for="contact_subject"><%=Model.Localize("Subject")%></label>
            <%=Html.TextBox("subject", Request["subject"], new {id = "contact_subject", @class = "text"})%>
        </fieldset>
        <fieldset class="email">
            <label for="contact_email"><%=Model.Localize("Email")%></label>
            <%=Html.TextBox("email", Request["email"], new {id = "contact_email", @class = "text"})%>
            <%=Html.ValidationMessage("ContactForm.Email", "Valid email required to submit feedback.")%>
            <%=Html.ValidationMessage("UserBase.Email", "Invalid email.")%>
        </fieldset>
        <fieldset class="message">
            <label for="contact_message"><%=Model.Localize("Message")%></label>
            <%=Html.TextArea("message", Request["message"], new {id = "contact_message", @class = "text"})%>
        </fieldset>
        <fieldset class="submit">
            <input type="submit" value="<%=Model.Localize("SendFeedbackButton", "Send Feedback")%>" id="contact_submit" class="submit button" />
        </fieldset>
    </form>
    <%
       }
        %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
    <%=Html.PageTitle(Model.Localize("Contact Us"))%>
</asp:Content>