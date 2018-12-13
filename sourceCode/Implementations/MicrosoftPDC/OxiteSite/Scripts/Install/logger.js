if (!window.SLS)
	window.SLS = {};

// deployment settings
SLS.hqPlayerSlUri = "http://go.microsoft.com/fwlink/?linkid=124807";
SLS.hqPlayerMsLogUri = "http://pdc.logqueue.com";
SLS.hqPlayerMsLogDelay = 1250;
//SLS.appName = "JEC";
//SLS.appVersion = "1.0";
//SLS.hqPlayerSlVersion = "3.0.40818.0";
SLS.hqPlayerErrorHandler = function(source, error) { alert("Oops! An unexpected error occurred.\n\nSource: " + source + "\nDescription: " + error.description); };


SLS.entryFlowCookieName = function() {
	return SLS.appName + "entryFlow";
}

SLS.appIdCookieName = function() {
	return SLS.appName + "appId";
}

SLS.installFlowCookieName = function() {
	return SLS.appName + "installFlow";
}

// common functions
SLS.getClientState = function() {
	if (!SLS.clientState) {
		var o = {};
		o.uid = SLS.getUid();
		o.isLogEntryFlowCookieDefined = SLS.isCookieDefined(SLS.entryFlowCookieName(), "1");
		o.isLogInstallFlowCookieDefined = SLS.isCookieDefined(SLS.installFlowCookieName(), "2");
		o.isSlVersionInstalled = Silverlight.isInstalled(SLS.hqPlayerSlVersion);
		o.isSlVersionSupported = Silverlight.supportedUserAgent(SLS.hqPlayerSlVersion.substring(0, 1).concat(".0"));
		o.isSlUpgradeRequired = !o.isSlVersionInstalled && Silverlight.isInstalled(null);
		SLS.clientState = o;
	}

	return SLS.clientState;
};

SLS.onPlayerPageBeforeUnload = function() {
	// log abandoned install on badge
	if (SLS.installState == 0) {
		SLS.logInstallFlow(3);
	}

	// log app event
	if (SLS.hqAppSessionId)
		SLS.logAppEvent();

	// force delay to allow logging to complete
	var date = new Date();
	while (new Date() - date < SLS.hqPlayerMsLogDelay);
};


SLS.isCookieDefined = function(name, value) {
	var cookieValue = SLS.getCookieValue(name);
	return SLS.equals(cookieValue, value);
};

SLS.getCookieValue = function(name, caseSensitive) {
	var cookie = document.cookie;

	if (cookie && cookie.length > 0) {
		var items = cookie.split(";");
		return SLS.getParamValue(items, name, caseSensitive);
	}
};

SLS.getParamValue = function(items, name, caseSensitive) {
	for (i = 0; i < items.length; i++) {
		var item = items[i].split("=");

		if (SNF.equals(SNF.trim(unescape(item[0])), name, caseSensitive))
			return item[1] ? unescape(item[1]) : item[1];
	}
};

SLS.equals = function(s1, s2, caseSensitive) {
	if (s1 == s2)
		return true;
	else if (s1 == null || s2 == null || caseSensitive)
		return false;
	else
		return s1.toLowerCase() == s2.toLowerCase();
};

SLS.trim = function(s) {
	return s.replace(/^\s+|\s+$/g, "");
};

SLS.setCookie = function(name, value, hours) {
	if (hours) {
		var expDate = new Date(new Date().getTime() + hours * 60 * 60 * 1000);
		document.cookie = name + "=" + value + ";expires=" + expDate.toGMTString();
	}
	else {
		document.cookie = name + "=" + value;
	}
};

SLS.clearCookie = function(name) {
	document.cookie = name + "=;expires=Thu, 01-Jan-1970 00:00:01 GMT";
};

SLS.getUid = function() {
	var uid = SLS.getCookieValue(SLS.appIdCookieName());

	if (!uid) {
		uid = Math.uuid();
		SLS.setCookie(SLS.appIdCookieName(), uid);
	}

	return uid;
};

SLS.getParamValue = function(items, name, caseSensitive) {
	for (i = 0; i < items.length; i++) {
		var item = items[i].split("=");

		if (SLS.equals(SLS.trim(unescape(item[0])), name, caseSensitive))
			return item[1] ? unescape(item[1]) : item[1];
	}
};

SLS.appendScript = function(index, src) {
	try {
		var script = document.createElement("script");
		script.id = "script" + index;
		script.src = src;
		script.type = "text/javascript";

		var head = document.getElementsByTagName("head")[0];
		head.appendChild(script);
	}
	catch (e) {
		SLS.hqPlayerErrorHandler("SLS.appendScript", e);
	}
};

SLS.removeScript = function(index) {
	try {
		var script = document.getElementById("script" + index);
		var head = document.getElementsByTagName("head")[0];
		head.removeChild(script);
	}
	catch (e) {
		SLS.hqPlayerErrorHandler("SLS.removeScript", e);
	}
};

SLS.logCount = 0;

SLS.setAppSessionId = function(id) {
	SLS.hqAppSessionId = id;
};

SLS.logAppEvent = function() {
	try {
		// svc parameters
		var u = SLS.hqAppSessionId;
		var i = SLS.logCount++;
		var t = new Date().getTime();

		// append script tag
		var src = SLS.hqPlayerMsLogUri + "/appevent.svc/parms?u=" + u + "&i=" + i + '&t=' + t + '&an=' + SLS.appName + '&av=' + SLS.appVersion;
		SLS.appendScript(i, src);
	}
	catch (e) {
		SLS.hqPlayerErrorHandler("SLS.logAppEvent", e);
	}
};

SLS.logEntryFlow = function() {
	try {
		var state = SLS.getClientState();

		if (!state.isLogEntryFlowCookieDefined) {
			// set the session cookie to avoid multiple calls
			SLS.setCookie(SLS.entryFlowCookieName(), "1");

			// svc parameters
			var s, o, p;
			var u = state.uid;
			var r = document.referrer;
			var i = SLS.logCount++;
			var t = new Date().getTime();

			// Silverlight install state
			if (state.isSlVersionInstalled && state.isSlVersionSupported)
				s = 2;
			else if (state.isSlUpgradeRequired && state.isSlVersionSupported)
				s = 1;
			else
				s = 0;

			// Silverlight support state
			if (state.isSlVersionSupported)
				o = 1;
			else
				o = 0;
				
			// append script tag
			var src = SLS.hqPlayerMsLogUri + "/entryflow.svc/parms?u=" + u + "&s=" + s + "&o=" + o + "&i=" + i + '&t=' + t + '&an=' + SLS.appName + '&av=' + SLS.appVersion;
			SLS.appendScript(i, src);
		}
	}
	catch (e) {
		SLS.hqPlayerErrorHandler("SLS.logEntryFlow", e);
	}
};

SLS.logInstallFlow = function(action) {
	SLS.installState = action;
	try {
		// attempting to run silverlight install
		if (action == 1 || action == 2)
			SLS.setCookie(SLS.installFlowCookieName(), "2", 1);
		// install success
		else if (action == 4 || action == 3)
			SLS.clearCookie(SLS.installFlowCookieName());

		var state = SLS.getClientState();

		// svc parameters
		var u = state.uid;
		var a = action;
		var i = SLS.logCount++;
		var t = new Date().getTime();

		// append script tag
		var src = SLS.hqPlayerMsLogUri + "/install.svc/parms?u=" + u + "&a=" + a + "&i=" + i + '&t=' + t + '&an=' + SLS.appName + '&av=' + SLS.appVersion;
		SLS.appendScript(i, src);
	}
	catch (e) {
		SLS.hqPlayerErrorHandler("SLS.logInstallFlow", e);
	}
};

function onLauncherPageLoad() {
	// ensure that if browser is closed/page left it gets tracked
	window.onbeforeunload = SLS.onPlayerPageBeforeUnload;

	// would be good to track install version at this point
	SLS.logEntryFlow();
 
	Silverlight.__startup()
} 

function onSilverlightError(sender, args) {
	// 8001 code for upgrade required
	// 8002 code for restart required
	// 5014 code for improper installation (also fires if sl1.0 is installed)
	if ((args.ErrorCode == 8001) || (args.ErrorCode == 5014)) {
	// ignore as should be captured via silverlight.js events
	} else if (args.ErrorCode == 8002) {
		Silverlight.onRestartRequired()
		document.getElementById("silverlightControlHost").innerHTML = PromptRestart;
	} else {
		alert("Debug:  Error Code = " + args.ErrorCode);
	}
}


function onSilverlightLoad(sender) {

	Silverlight.IsVersionAvailableOnLoad(sender);

	var state = SLS.getClientState();

	if (state.isSlVersionInstalled) {
		// log successful install
		if (state.isLogInstallFlowCookieDefined) {
			SLS.logInstallFlow(4);
		}
	}
}

Silverlight.onRequiredVersionAvailable = function() {  };

Silverlight.onRestartRequired = function() {
	document.getElementById("silverlightControlHost").innerHTML = PromptRestart;
};

Silverlight.onUpgradeRequired = function() {
	SLS.logInstallFlow(0);
	if (CheckSupported(PromptUpgrade)) {
	}
};

Silverlight.onInstallRequired = function() {
	SLS.logInstallFlow(0);
	if (CheckSupported(PromptInstall)) {
	}
};

function UpgradeClicked() {
	SLS.logInstallFlow(2);
	document.getElementById("silverlightControlHost").innerHTML = PromptFinishUpgrade;
	window.location = "http://go.microsoft.com/fwlink/?LinkID=149156&v=" + SLS.hqPlayerSlVersion;
}

function InstallClicked() {
	SLS.logInstallFlow(1);
	document.getElementById("silverlightControlHost").innerHTML = PromptFinishInstall;
	window.location = "http://go.microsoft.com/fwlink/?LinkID=149156&v=" + SLS.hqPlayerSlVersion;
}

function CheckSupported(msg) {
// check to ensure Version 3 is supported
var tst = Silverlight.supportedUserAgent(3);
	if (tst) {
	// Do nothing
		document.getElementById("silverlightControlHost").innerHTML = msg;
		return(true);
	} else if (SLBrowser == "Chrome" && OperatingSystem == "Windows") {
		// alert that Chrome isn't fully tested, but allow install
		SLS.logInstallFlow(6);
		document.getElementById("silverlightControlHost").innerHTML = msg;
		alert(WarningNotSupported);
		return(true);
	} else {
		SLS.logInstallFlow(5);
		document.getElementById("silverlightControlHost").innerHTML = PromptNotSupported;
		return(false);
	}
}