/*
* ******************************************************************************
*  jquery.mb.components
*  file: jquery.browser.js
*
*  Copyright (c) 2001-2013. Matteo Bicocchi (Pupunzi);
*  Open lab srl, Firenze - Italy
*  email: matteo@open-lab.com
*  site: http://pupunzi.com
*  blog: http://pupunzi.open-lab.com
*  http://open-lab.com
*
*  Licences: MIT, GPL
*  http://www.opensource.org/licenses/mit-license.php
*  http://www.gnu.org/licenses/gpl.html
*
*  last modified: 16/01/13 20.38
*  *****************************************************************************
*/

/*******************************************************************************
*
* jquery.browser
* Author: pupunzi
* Creation date: 16/01/13
*
******************************************************************************/
/*Browser detection patch*/

(function (e) { var t = jQuery.fn.jquery.split("."); if (t[1] < 8) { return } jQuery.browser = {}; jQuery.browser.mozilla = false; jQuery.browser.webkit = false; jQuery.browser.opera = false; jQuery.browser.msie = false; var n = navigator.userAgent; jQuery.browser.name = navigator.appName; jQuery.browser.fullVersion = "" + parseFloat(navigator.appVersion); jQuery.browser.majorVersion = parseInt(navigator.appVersion, 10); var r, i, s; if ((i = n.indexOf("Opera")) != -1) { jQuery.browser.opera = true; jQuery.browser.name = "Opera"; jQuery.browser.fullVersion = n.substring(i + 6); if ((i = n.indexOf("Version")) != -1) { jQuery.browser.fullVersion = n.substring(i + 8) } } else if ((i = n.indexOf("MSIE")) != -1) { jQuery.browser.msie = true; jQuery.browser.name = "Microsoft Internet Explorer"; jQuery.browser.fullVersion = n.substring(i + 5) } else if ((i = n.indexOf("Chrome")) != -1) { jQuery.browser.webkit = true; jQuery.browser.name = "Chrome"; jQuery.browser.fullVersion = n.substring(i + 7) } else if ((i = n.indexOf("Safari")) != -1) { jQuery.browser.webkit = true; jQuery.browser.name = "Safari"; jQuery.browser.fullVersion = n.substring(i + 7); if ((i = n.indexOf("Version")) != -1) { juery.browser.fullVersion = n.substring(i + 8) } } else if ((i = n.indexOf("Firefox")) != -1) { jQuery.browser.mozilla = true; jQuery.browser.name = "Firefox"; jQuery.browser.fullVersion = n.substring(i + 8) } else if ((r = n.lastIndexOf(" ") + 1) < (i = n.lastIndexOf("/"))) { jQuery.browser.name = n.substring(r, i); jQuery.browser.fullVersion = n.substring(i + 1); if (jQuery.browser.name.toLowerCase() == jQuery.browser.name.toUpperCase()) { jQuery.browser.name = navigator.appName } } if ((s = jQuery.browser.fullVersion.indexOf(";")) != -1) { jQuery.browser.fullVersion = jQuery.browser.fullVersion.substring(0, s) } if ((s = jQuery.browser.fullVersion.indexOf(" ")) != -1) { jQuery.browser.fullVersion = jQuery.browser.fullVersion.substring(0, s) } jQuery.browser.majorVersion = parseInt("" + jQuery.browser.fullVersion, 10); if (isNaN(jQuery.browser.majorVersion)) { jQuery.browser.fullVersion = "" + parseFloat(navigator.appVersion); jQuery.browser.majorVersion = parseInt(navigator.appVersion, 10) } jQuery.browser.version = jQuery.browser.majorVersion })(jQuery);