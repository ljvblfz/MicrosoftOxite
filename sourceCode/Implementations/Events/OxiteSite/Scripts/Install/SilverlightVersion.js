if (!window.Silverlight)
    window.Silverlight = { };

Silverlight.ua = null;

function detectSilverlightVersion() {
	var container = null;
	var silverlightVersion;

	try {
		var control = null;

		if (Silverlight.ua.Browser == 'MSIE') 
		{
			control = new ActiveXObject('AgControl.AgControl');
		}
		else 
		{
			if ( navigator.plugins["Silverlight Plug-In"] )
			{
				container = document.createElement('div');
				document.body.appendChild(container);
				container.innerHTML= '<embed type="application/x-silverlight" />';
				control = container.childNodes[0];
			}
		}		
		if (control.Settings) {
			try {
				silverlightVersion = control.Settings.version;
			}
			catch (z) {
			}
		}
		
		if (!silverlightVersion)
		{
			var major = 0;
			var minor = 0;
			var revision = 0;
			var build = 0;
			
			var buildVersionString = function(versionNumbers) {
				var versionString = "";
				for (var l = 0; l < versionNumbers.length; ++l) {
					versionString += versionNumbers[l];
					if (l != versionNumbers.length - 1)
						versionString += ".";
				}
				return versionString;
			}
			
			var versionNumbers = [0,0,0,0];
			for(var i = 0; i < versionNumbers.length; ++i) {
				for (var incrementor = 0; incrementor < 100000; ++incrementor) {
					versionNumbers[i] = incrementor;

					if (!control.IsVersionSupported(buildVersionString(versionNumbers))) {
						versionNumbers[i] = incrementor - 1;
						break;
					} 
				}
			}
			
			silverlightVersion = buildVersionString(versionNumbers);
		}


		control = null; 
	}
	catch (e) {
	}
	if (container) {
		document.body.removeChild(container);
	}
	
	if (!silverlightVersion)
		silverlightVersion = "Not installed!";
	
	return silverlightVersion;
}

///////////////////////////////////////////////////////////////////////////////
// detectUserAgent Parses UA string and stores relevant data in Silverlight.ua.
///////////////////////////////////////////////////////////////////////////////
Silverlight.detectUserAgent = function()
{
    var ua = window.navigator.userAgent;
   
    
    Silverlight.ua = {OS:'Unsupported',Browser:'Unsupported'};
    
    //Silverlight does not support pre-Windows NT platforms
    if (ua.indexOf('Windows NT') >= 0) {
        Silverlight.ua.OS = 'Windows';
    }
    else if (ua.indexOf('PPC Mac OS X') >= 0) {
        Silverlight.ua.OS = 'MacPPC';
    }
    else if (ua.indexOf('Intel Mac OS X') >= 0) {
        Silverlight.ua.OS = 'MacIntel';
    }
    
    if ( Silverlight.ua.OS != 'Unsupported' )
    {
        if (ua.indexOf('MSIE') >= 0) {
            if (navigator.userAgent.indexOf('Win64') == -1)
            {
                if (parseInt(ua.split('MSIE')[1]) >= 6) {
                    Silverlight.ua.Browser  = 'MSIE';
                }
                
            }
        }
        else if (ua.indexOf('Firefox') >= 0) {
            var version = ua.split('Firefox/')[1].split('.');
            var major = parseInt(version[0]);
            if (major >= 2) {
                Silverlight.ua.Browser = 'Firefox';
            }
            else {
                var minor = parseInt(version[1]);
                if ((major == 1) && (minor >= 5)) {
                    Silverlight.ua.Browser  = 'Firefox';
                }
            }
        }
        
        else if (ua.indexOf('Safari') >= 0) {
            Silverlight.ua.Browser = 'Safari';
        }            
    }
}

// Detect the user agent at script load time
Silverlight.detectUserAgent();