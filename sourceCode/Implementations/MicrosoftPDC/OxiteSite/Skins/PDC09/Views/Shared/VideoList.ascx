<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelItems<ScheduleItem>>"  %>
<%@ Import Namespace="OxiteSite.App_Code.Modules.OxiteSite.Extensions"%>
<%@ Import Namespace="Oxite.Extensions"%>
<%@ Import Namespace="Oxite.Modules.Conferences.Models"%><%
                                                             if (Model.Items != null)
                                                             {
                                                                 List<ScheduleItem> videos = Model.Items.OrderBy(e => e.Slug).ToList();
                                                                 if (videos.Count > 0)
                                                                 { %>
<table id="videolist" border="0">
    <thead>
        <tr>
            <th></th>
            <th>Title</th>
            <th>WMV</th>
            <th>WMV High</th>
            <th>MP4</th>
            <th>Slides</th>
        </tr>
    </thead>
    <tbody>
<%
                                                             var counter = 0;
                                                             const string linkMask = "<a href=\"{0}\" alt=\"{1}\">{1}</a>";

                                                             foreach (var video in videos)
                                                             {
                                                                 if (video.Start < new DateTime(2009, 11, 20, 10, 0, 0))
                                                                 {

                                                                     try
                                                                     {
                                                                         string cacheKeyID = Url.Session(video); 
                                                                         List<File> files = null;
                                                                         files =
                                                                             Cache.Get("filesFor:" + cacheKeyID) as
                                                                             List<File>;
                                                                         if (files == null)
                                                                         {
                                                                             files = video.Files.ToList();
                                                                             Cache.Add("filesFor:" + cacheKeyID, files,
                                                                                       null,
                                                                                       DateTime.Now.AddHours(1),
                                                                                       System.Web.Caching.Cache.
                                                                                           NoSlidingExpiration,
                                                                                       CacheItemPriority.Normal, null);
                                                                         }
                                                                         //video.Files.ToList();

                                                                         List<Speaker> speakerList = null;
                                                                         speakerList =
                                                                             Cache.Get("speakersFor:" + cacheKeyID) as
                                                                             List<Speaker>;
                                                                         if (speakerList == null)
                                                                         {
                                                                             speakerList = video.Speakers.ToList();
                                                                             Cache.Add("speakersFor:" + cacheKeyID,
                                                                                       speakerList, null,
                                                                                       DateTime.Now.AddHours(1),
                                                                                       System.Web.Caching.Cache.
                                                                                           NoSlidingExpiration,
                                                                                       CacheItemPriority.Normal, null);
                                                                         }
                                                                         //video.Speakers.ToList();

                                                                         var speakers = String.Join(", ",
                                                                                                    speakerList.Select(
                                                                                                        s =>
                                                                                                        HttpUtility.
                                                                                                            HtmlDecode(
                                                                                                            s.
                                                                                                                DisplayName
                                                                                                                .
                                                                                                                Trim()))
                                                                                                        .
                                                                                                        ToArray());
                                                                         var className = counter%2 != 0 ? "oddrow" : "";

                                                                         var smooth =
                                                                             files.SingleOrDefault(
                                                                                 f =>
                                                                                 f.TypeName.Equals("Smooth",
                                                                                                   StringComparison.
                                                                                                       InvariantCultureIgnoreCase));

                                                                         if (smooth != null)
                                                                             className += " smooth";


                                                                         // WMV
                                                                         var wmv =
                                                                             files.SingleOrDefault(
                                                                                 f =>
                                                                                 f.TypeName.Equals("WMV",
                                                                                                   StringComparison.
                                                                                                       InvariantCultureIgnoreCase));
                                                                         var wmvLink = wmv != null
                                                                                           ? String.Format(linkMask,
                                                                                                           wmv.Url,
                                                                                                           "WMV")
                                                                                           : "";
                                                                         // WMV High
                                                                         var wmvHigh =
                                                                             files.SingleOrDefault(
                                                                                 f =>
                                                                                 f.TypeName.Equals("WMVHigh",
                                                                                                   StringComparison.
                                                                                                       InvariantCultureIgnoreCase));
                                                                         var wmvHighLink = wmvHigh != null
                                                                                               ? String.Format(linkMask,
                                                                                                               wmvHigh.
                                                                                                                   Url,
                                                                                                               "WMVHigh")
                                                                                               : "";
                                                                         // MP4
                                                                         var mp4 =
                                                                             files.SingleOrDefault(
                                                                                 f =>
                                                                                 f.TypeName.Equals("MP4",
                                                                                                   StringComparison.
                                                                                                       InvariantCultureIgnoreCase));
                                                                         var mp4Link = mp4 != null
                                                                                           ? String.Format(linkMask,
                                                                                                           mp4.Url,
                                                                                                           "MP4")
                                                                                           : "";
                                                                         // Slides (PPTX)



                                                                         var slides =
                                                                             files.SingleOrDefault(
                                                                                 f =>
                                                                                 f.TypeName.Equals("PPT",
                                                                                                   StringComparison.
                                                                                                       InvariantCultureIgnoreCase));
                                                                         var slidesLink = slides != null
                                                                                              ? String.Format(linkMask,
                                                                                                              slides.Url,
                                                                                                              "Slides")
                                                                                              : "";
%>
    <tr class="<%=className%>">
        <td><%=video.Code%></td>
        <td>
            <a href="<%=Url.Session(video)%>" alt=""><%=
                                                                             HttpUtility.HtmlEncode(video.Title)%></a>
            <br />
            <span class="speakers"><em><%=speakers%></em></span>
        </td>
        <td><%=wmvLink%></td>
        <td><%=wmvHighLink%></td>
        <td><%=mp4Link%></td>
        <td><%=slidesLink%></td>
    </tr>
    <%
                                                                         counter++;
                                                                     }
                                                                     catch
                                                                     {

                                                                     }
                                                                 }
                                                             }
%>
    </tbody>
</table>
<%
                                                                 }
                                                             }
                                                             else
                                                             { //todo: (nheskew) need an Html.Message html helper extension method that takes a message %>
<div class="message info"><%=Model.Localize("Exhibitor.NoneFound", "There were no videos found.")%></div><%        
                                                             } %>