#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web;

#endregion

namespace SageFrame.Web
{
    /// <summary>
    /// Class containing the session details.
    /// </summary>
    [Serializable]
    public class SessionTracker
    {
        private string _browser = string.Empty;
        private string _crawler = string.Empty;
        private ArrayList _pages = new ArrayList();
        private string _sessionTrackerID;
        private DateTime _expires;
        private string _visitCount;
        private string _userHostAddress;
        private string _userAgent;
        private string _originalReferrer;
        private string _originalURL;
        private string _sessionReferrer;
        private string _sessionURL;
        private string _portalID;
        private string _username;
        private string _insertSessionTrackerPages;

        /// <summary>
        /// Gets or sets session tracker ID
        /// </summary>
        public string SessionTrackerID
        {
            get
            {
                return this._sessionTrackerID;
            }
            set
            {
                this._sessionTrackerID = value;
            }
        }

        /// <summary>
        /// Gets or sets session visit count. 
        /// </summary>
        public int VisitCount
        {
            get
            {
                return int.Parse(this._visitCount);
            }
        }

        /// <summary>
        /// Gets or sets session visit count. 
        /// </summary>
        public string OriginalReferrer
        {
            get
            {
                return this._originalReferrer;
            }
        }

        /// <summary>
        /// Gets or sets session's original URL.
        /// </summary>
        public string OriginalURL
        {
            get
            {
                return this._originalURL;
            }
        }

        /// <summary>
        /// Gets or sets session 's referrer URL.
        /// </summary>
        public string SessionReferrer
        {
            get
            {
                return this._sessionReferrer;
            }
        }

        /// <summary>
        /// Gets or sets session's URL.
        /// </summary>
        public string SessionURL
        {
            get
            {
                return this._sessionURL;
            }
        }

        /// <summary>
        /// Gets or sets host user's address.
        /// </summary>
        public string SessionUserHostAddress
        {
            get
            {
                return this._userHostAddress;
            }
        }

        /// <summary>
        /// Gets or sets session's user agent.
        /// </summary>
        public string SessionUserAgent
        {
            get
            {
                return this._userAgent;
            }
        }

        /// <summary>
        /// Gets or sets arrylist of pages.
        /// </summary>
        public ArrayList Pages
        {
            get
            {
                return this._pages;
            }
        }

        /// <summary>
        /// Gets or sets request generated browser. 
        /// </summary>
        public string Browser
        {
            get
            {
                return this._browser;
            }
        }

        /// <summary>
        /// Gets or sets session crawler.
        /// </summary>
        public string Crawler
        {
            get
            {
                return this._crawler;
            }
        }

        /// <summary>
        /// Gets or sets session  portal ID.
        /// </summary>
        public string PortalID
        {
            get
            {
                return this._portalID;
            }
            set
            {
                this._portalID = value;
            }
        }

        /// <summary>
        /// Gets or sets session user's name.
        /// </summary>
        public string Username
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }

        /// <summary>
        /// Gets or sets page name.
        /// </summary>
        public string InsertSessionTrackerPages
        {
            get
            {
                return this._insertSessionTrackerPages;
            }
            set
            {
                this._insertSessionTrackerPages = value;
            }
        }

        /// <summary>
        /// Initializes an instance of SessionTracker class.
        /// </summary>
        public SessionTracker()
        {
            try
            {
                this._expires = DateTime.Now.AddYears(1);
                this.IncrementVisitCount();
                this._userHostAddress = ((object)HttpContext.Current.Request.UserHostAddress).ToString();
                if (HttpContext.Current.Request.UserAgent != null)
                    this._userAgent = ((object)HttpContext.Current.Request.UserAgent).ToString();
                if (HttpContext.Current.Request.UrlReferrer != (Uri)null)
                {
                    this.SetOriginalReferrer(HttpContext.Current.Request.UrlReferrer.ToString());
                    this._sessionReferrer = HttpContext.Current.Request.UrlReferrer.ToString();
                }
                if (HttpContext.Current.Request.Url != (Uri)null)
                {
                    this.SetOriginalURL(HttpContext.Current.Request.Url.ToString());
                    this._sessionURL = HttpContext.Current.Request.Url.ToString();
                }
                this._browser = HttpContext.Current.Request.Browser.Browser != null ? ((object)HttpContext.Current.Request.Browser.Browser).ToString() : string.Empty;
                this._crawler = HttpContext.Current.Request.Browser.Crawler.ToString();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Adds page name to session recorded page.
        /// </summary>
        /// <param name="pageName">Page name.</param>
        public void AddPage(string pageName)
        {
            this._pages.Add((object)new SessionTrackerPage()
            {
                PageName = pageName,
                Time = DateTime.Now
            });
        }

        /// <summary>
        /// Increases the page visit count.
        /// </summary>
        public void IncrementVisitCount()
        {
            this._visitCount = HttpContext.Current.Request.Cookies.Get("VisitCount") != null ? (int.Parse(((object)HttpContext.Current.Request.Cookies.Get("VisitCount").Value).ToString()) + 1).ToString() : "1";
            this.addCookie("VisitCount", this._visitCount);
        }

        /// <summary>
        /// Sets original referer URL.
        /// </summary>
        /// <param name="val">Referer URL.</param>
        public void SetOriginalReferrer(string val)
        {
            if (HttpContext.Current.Request.Cookies.Get("OriginalReferrer") != null)
            {
                this._originalReferrer = HttpContext.Current.Request.Cookies.Get("OriginalReferrer").Value;
            }
            else
            {
                this.addCookie("OriginalReferrer", val);
                this._originalReferrer = val;
            }
        }

        /// <summary>
        /// Sets original URL.
        /// </summary>
        /// <param name="val">Original URL.</param>
        public void SetOriginalURL(string val)
        {
            if (HttpContext.Current.Request.Cookies.Get("OriginalURL") != null)
            {
                this._originalURL = HttpContext.Current.Request.Cookies.Get("OriginalURL").Value;
            }
            else
            {
                this.addCookie("OriginalURL", val);
                this._originalURL = val;
            }
        }

        /// <summary>
        /// Adds cookie's key and value.
        /// </summary>
        /// <param name="key">Cookie's key</param>
        /// <param name="value">Cookie's value.</param>
        private void addCookie(string key, string value)
        {
            HttpContext.Current.Response.Cookies.Set(new HttpCookie(key, value)
            {
                Expires = this._expires
            });
        }
    }
}
