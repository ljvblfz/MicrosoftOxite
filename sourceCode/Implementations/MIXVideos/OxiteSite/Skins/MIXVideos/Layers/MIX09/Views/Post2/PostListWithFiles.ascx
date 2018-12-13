<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelList<Post>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %>
<%@ Import Namespace="MIXVideos.Oxite.Extensions" %><%
if (((IPageOfList<Post>)Model.List).TotalItemCount > 0)
{ %><table>
    <tr>
        <th></th>
        <th>Title</th>
        <th>WMV</th>
        <th>WMV High</th>
        <th>MP4</th>
        <!--<th>WMA</th>
        <th>MP3</th>-->
        <th>Slides</th>
    </tr><%
    int counter = 0;
    foreach (Post post in Model.List.OrderBy(p => p.Slug))
    {
        StringBuilder className = new StringBuilder("post", 15);

        if (counter % 2 != 0) { className.Append(" odd"); }

        File wmv = post.Files.ByTypeName("WMV");
        File wmvHigh = post.Files.ByTypeName("WMVHigh");
        File mp4 = post.Files.ByTypeName("MP4");
        //File wma = post.Files.ByTypeName("WMA");
        //File mp3 = post.Files.ByTypeName("MP3");
        File ppt = post.Files.ByTypeName("PPT");
        %>
    <tr class="<%=className.ToString() %>">
        <td><%=post.Slug %></td>
        <td class="title"><%=Html.Link(post.Title.CleanText().Ellipsize(85, "&nbsp;&#8230;"), Url.Post(post), new { title = post.Title })%></td>
        <td><%=wmv != null ? Html.Link("WMV", wmv.Url.ToString()) : "&nbsp;" %></td>
        <td><%=wmvHigh != null ? Html.Link("WMVHigh", wmvHigh.Url.ToString()) : "&nbsp;" %></td>
        <td><%=mp4 != null ? Html.Link("MP4", mp4.Url.ToString()) : "&nbsp;" %></td>
        <%--<td><%=wma != null ? Html.Link("WMA", wma.Url.ToString()) : "&nbsp;" %></td>
        <td><%=mp3 != null ? Html.Link("MP3", mp3.Url.ToString()) : "&nbsp;" %></td>--%>
        <td><%=ppt != null ? Html.Link("Slides", ppt.Url.ToString()) : "&nbsp;" %></td>
    </tr>
    <tr class="fullDescription">
        <td><%= post.Body %></td>
    </tr><%
        counter++;
    } %>
</table><% 
} 
else
{ //todo: (nheskew) need an Html.Message html helper extension method that takes a message %>
<div class="message info"><%=Model.Localize("NoneFound", "None found.")%></div><%        
} %>