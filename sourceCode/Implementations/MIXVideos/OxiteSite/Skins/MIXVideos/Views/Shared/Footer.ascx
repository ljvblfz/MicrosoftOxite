<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<OxiteModel>" %>
<%@ Import Namespace="Oxite.Extensions" %>
<div id="poweredandinfo">
    <div class="powered"><span><%=Model.Localize("PoweredBy", "Powered by") %> </span><%=Html.Link("Oxite", Url.Oxite()) %></div>
    <ul class="logos">
        <li class="channel8 first"><a href="http://channel8.msdn.com" title="Channel 8" class="bgimage">Channel 8</a></li>
        <li class="channel9"><a href="http://channel9.msdn.com" title="Channel 9" class="bgimage">Channel 9</a></li>
        <li class="channel10"><a href="http://on10.net" title="Channel 10" class="bgimage">Channel 10</a></li>
        <li class="visitmix"><a href="http://visitmix.com" title="MIX Online" class="bgimage">MIX Online</a></li>
        <li class="edge"><a href="http://edge.technet.com" title="TechNet Edge" class="bgimage">TechNet Edge</a></li>
    </ul>
    <div class="info">
        <div class="container">
            <div class="fineprint">
                <ul>
                    <li class="first"><a href="http://visitmix.com/Contact">Contact Us</a></li>
                    <li><a href="http://visitmix.com/CodeOfConduct">Code Of Conduct</a></li>
                    <li><a href="http://visitmix.com/Terms">Terms Of Use</a></li>
                    <li class="last"><a href="http://visitmix.com/Privacy">Privacy Statement</a></li>
                </ul>
                <p>This site is hosted for Microsoft by ORCSWEB Copyright 2007-2009 Microsoft</p>
            </div>
        </div>
    </div>
</div>