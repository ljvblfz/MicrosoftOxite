<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<Post>>" %>
<%@ Import Namespace="Oxite.Modules.Blogs.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Blogs.Models"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%>
<div class="share"><%
    string itemUrl = Url.AbsolutePath(Url.Post(Model.PartialModel)).CleanForQueryString();
    string itemTitle = Model.PartialModel.Title.CleanForQueryString(); %>
	<div><%=Model.Localize("Share") %>:</div>
	<ul>
		<li class="first"><a href="http://del.icio.us/post?v=4&amp;url=<%=itemUrl %>&amp;title=<%=itemTitle %>" class="delicious">Del.icio.us</a></li>
		<li><a href="http://digg.com/submit?phase=2&amp;url=<%=itemUrl %>&amp;title=<%=itemTitle %>" class="digg">Digg</a></li>
		<li><a href="http://www.facebook.com/sharer.php?u=<%=itemUrl %>&amp;t=<%=itemTitle %>" class="facebook">Facebook</a></li>
		<li class="last"><a href="http://twitter.com/home?status=<%=string.Format("{0}%20{1}%20(via%20@pdc09)", itemTitle, Url.CompressUrl(itemUrl)) %>" class="twitter">Twitter</a></li>
	</ul>
</div>