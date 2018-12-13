<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<ScheduleItem>>" %>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<%
    string itemUrl = Url.AbsolutePath(Url.Session(Model.PartialModel)).CleanForQueryString();
    string itemTitle = Model.PartialModel.Title.CleanForQueryString(); %>
	
	<ul>
		<li><a href="http://del.icio.us/post?v=4&amp;url=<%=itemUrl %>&amp;title=<%=itemTitle %>" class="delicious">Del.icio.us</a></li>
		<li><a href="http://digg.com/submit?phase=2&amp;url=<%=itemUrl %>&amp;title=<%=itemTitle %>" class="digg">Digg</a></li>
		<li><a href="http://www.facebook.com/sharer.php?u=<%=itemUrl %>&amp;t=<%=itemTitle %>" class="facebook">Facebook</a></li>
		<li class="last"><a href="http://twitter.com/home?status=<%=string.Format("{0}%20{1}%20(via%20@mixevent)", itemTitle, Url.CompressUrl(itemUrl)) %>" class="twitter">Twitter</a></li>
	</ul>

