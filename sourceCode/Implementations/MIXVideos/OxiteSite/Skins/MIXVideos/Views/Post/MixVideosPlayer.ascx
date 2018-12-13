<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteModelPartial<PlayerViewModel>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions" %><%
    if (Model.PartialModel != null && Model.PartialModel.Media != null && Model.PartialModel.Preview != null)
    {
        string videoWidth = "320";
        string videoHeight = "240";
        string videoThumbnail = Model.PartialModel.Preview.Url.ToString();

        string typeName = Model.PartialModel.Media.TypeName;

        if (typeName == "WMVHigh" || typeName == "WMVStreamingOnly")
        {
            videoWidth = "960";
            videoHeight = "540";

            if (string.Compare(Model.RootModel.Container.Name, "MIX09", true) == 0)
            {
                videoThumbnail = "";
            }

            if (string.Compare(Model.RootModel.Container.Name, "MIX08", true) == 0)
            {
                videoWidth = "936";
                videoHeight = "484";
            }
            else if (string.Compare(Model.RootModel.Container.Name, "MIX07", true) == 0)
            {
                videoWidth = "956";
                videoHeight = "484";
            }
            else if (string.Compare(Model.RootModel.Container.Name, "MIX06", true) == 0)
            {
                videoWidth = "936";
                videoHeight = "484";
            }
        }
        else if (typeName == "WMV640x360")
        {
            videoHeight = "360";
            videoWidth = "640";
        }
         %>
    <object class="player" width="<%=videoWidth %>" height="<%=videoHeight %>" type="application/x-silverlight-2" data="data:application/x-silverlight-2,">
        <param value="<%=Url.CssPath("/players/VideoPlayer2009_03_27.xap", ViewContext) %>" name="source" />
        <param value="m=<%=Model.PartialModel.Media.Url %>,thumbnail=<%=videoThumbnail %>,autohide=true,showembed=true" name="initParams" />
        <param value="#00000000" name="background" />
        <div style="background:url(<%=Model.PartialModel.Preview.Url.ToString() %>) no-repeat 0 0;width:320px;height:250px;">
            <a href="http://go.microsoft.com/fwlink/?LinkID=124807" style="text-decoration:none;display:block;width:320px;height:250px;padding:80px 0 0 50px;">
                <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style: none" />
            </a>
        </div>
    </object>
<%
    } %>
