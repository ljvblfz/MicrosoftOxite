<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OxiteViewModelPartial<PlayerViewModel>>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<%@ Import Namespace="Oxite.Models.Extensions"%>
<%
    bool isSmoothStreaming = false;
    if (Model.PartialModel != null && Model.PartialModel.Media != null && Model.PartialModel.Preview != null)
    {
        isSmoothStreaming = Model.PartialModel.Media.Url.ToString().EndsWith("fest");
        string videoWidth = "320";
        string videoHeight = "240";
        string videoThumbnail = Model.PartialModel.Preview.Url.ToString();

        string typeName = Model.PartialModel.Media.TypeName;
        //478
        if (typeName == "Smooth")
        {
            videoWidth = "800";
            videoHeight = "478";
        }
        else if (typeName == "WMVHigh" || typeName == "WMVStreamingOnly" )
        {
            videoWidth = "800";
            videoHeight = "450";

            /*if (string.Compare(Model.Container.Name, "MIX09", true) == 0)
            {
                videoThumbnail = "";
            }

            if (string.Compare(Model.Container.Name, "MIX08", true) == 0)
            {
                videoWidth = "936";
                videoHeight = "484";
            }
            else if (string.Compare(Model.Container.Name, "MIX07", true) == 0)
            {
                videoWidth = "956";
                videoHeight = "484";
            }
            else if (string.Compare(Model.Container.Name, "MIX06", true) == 0)
            {
                videoWidth = "936";
                videoHeight = "484";
            }*/
        }
        else if (typeName == "WMV640x360" || typeName=="WMV")
        {
            videoHeight = "360";
            videoWidth = "640";
        }
        string playerPath = "/players/VideoPlayer2009_03_27.xap";
        if (isSmoothStreaming)
        {
            playerPath = "/players/Microsoft.SilverlightMediaFramework.PDC.xap";
        }
        string trackingURL = "http://microsoftpdc.com" + Model.PartialModel.Bug;
         %>
    <object class="player" width="<%=videoWidth %>" height="<%=videoHeight %>" type="application/x-silverlight-2" data="data:application/x-silverlight-2,">
        <param value="<%=Url.CssPath(playerPath, ViewContext) %>" name="source" />
        <param value="m=<%=Model.PartialModel.Media.Url %>,thumbnail=<%=videoThumbnail %>,autohide=true,showembed=true,bug=<%=trackingURL %>" name="initParams" />
        <param value="#00000000" name="background" />
	    <param name="minRuntimeVersion" value="3.0.40624.0" />
        <param name="EnableGPUAcceleration" value="true" />
		<param name="autoUpgrade" value="true" />
		  <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40624.0" style="text-decoration:none">
 			  <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style:none"/>
		  </a>
    </object>
    <%=Model.PartialModel.Message %>
    <%
    } %>
