#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SageFrame.Utilities;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using SageFrame.Common;
using SageFrame.Web;

#endregion

namespace SageFrame.Framework
{
    [Serializable]
    public partial class ApplicationController
    {
        #region "Public Methods"

        /// <summary>
        /// Application controller class
        /// </summary>
        public ApplicationController()
        {
        }

        /// <summary>
        /// Find The HTML Element Inside The HTML Controls, Inside The User Control, Inside The Page And Change The Css Property Of It.
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="userControlID">UserControlID</param>
        /// <param name="htmlControlID">HTML ControlID</param>
        /// <param name="elementID">Element ID</param>
        /// <param name="key"> Css Tag</param>
        /// <param name="value">Value</param>
        public void ChangeCss(Page page, string userControlID, string htmlControlID, string elementID, string key, string value)
        {
            PlaceHolder pchWhole = page.FindControl(userControlID) as PlaceHolder;
            UserControl uc = pchWhole.FindControl(htmlControlID) as UserControl;
            HtmlGenericControl div = uc.FindControl(elementID) as HtmlGenericControl;
            div.Attributes.Add(key, value);
        }

        /// <summary>
        /// To Check Whether SageFrame Is Installed Or Not.
        /// </summary>
        /// <returns>True If It Is Install</returns>
        public bool IsInstalled()
        {
            bool isInstalled = false;
            string IsInstalled = Config.GetSetting("IsInstalled").ToString();
            string InstallationDate = Config.GetSetting("InstallationDate").ToString();
            if ((IsInstalled != "" && IsInstalled != "false") && InstallationDate != "")
            {
                isInstalled = true;
            }
            return isInstalled;
        }

        /// <summary>
        /// Check Either The Request Is Done By Admin Or Not By URL.
        /// </summary>
        /// <returns>True If The User Is Admin</returns>
        public bool IsAdminRequest()
        {
            string url = HttpContext.Current.Request.RawUrl.ToLower();
            if (url.Contains("?"))
            {
                url = url.Split('?')[0];
            }
            bool isAdminRequest = url.Contains("/admin/") ||
                url.Contains("/sagin/") ||
                url.Contains("/admin" + SageFrameSettingKeys.PageExtension) ||
                url.Contains("/super-User/") ||
                url.Contains("/super-User" + SageFrameSettingKeys.PageExtension) ||
                url.Contains("managereturnurl=");
            return isAdminRequest;
        }

        /// <summary>
        /// Check The Requested URL For Extension: ".gif", ".jpg", ".png", "fonts"
        /// </summary>
        /// <param name="httpReq">HttpRequest</param>
        /// <returns>True If It Contains Extensions</returns>
        public bool CheckRequestExtension(HttpRequest httpReq)
        {
            bool isExtension = false;
            if (
                    (
                        httpReq.CurrentExecutionFilePath.Contains(".gif") ||
                        httpReq.CurrentExecutionFilePath.Contains(".jpg") ||
                        httpReq.CurrentExecutionFilePath.Contains(".png") ||
                        httpReq.CurrentExecutionFilePath.Contains("fonts")
                    )
              )
            {
                isExtension = true;
            }
            return isExtension;
        }

        /// <summary>
        /// Get PortalName By PortalID
        /// </summary>
        /// <param name="hstPortals">HashTable Containig PortalID And PortalName</param>
        /// <param name="portalID">PortalID</param>
        /// <returns>Portal Name</returns>
        public string GetPortalNameByKey(Hashtable hstPortals, int portalID)
        {
            string portalname = "";
            foreach (string key in hstPortals.Keys)
            {
                if (Int32.Parse(hstPortals[key].ToString()) == portalID)
                {
                    portalname = key;
                }
            }
            return portalname;
        }

        /// <summary>
        /// Get Css Colored Template URL.
        /// </summary>
        /// <param name="adminTheme">Admin Theme</param>
        /// <returns>Css Root Path</returns>
        public string CssColoredTemplate(string adminTheme)
        {
            string cssColoredTemplate = string.Empty;
            switch (adminTheme.ToLower())
            {
                case "green":
                    cssColoredTemplate = string.Format("~/Administrator/Templates/Default/themes/{0}/css/green.css", adminTheme);
                    break;
                case "red":
                    cssColoredTemplate = string.Format("~/Administrator/Templates/Default/themes/{0}/css/blue.css", adminTheme);
                    break;
                case "gray":
                    cssColoredTemplate = string.Format("~/Administrator/Templates/Default/themes/{0}/css/gray.css", adminTheme);
                    break;
                case "black":
                    cssColoredTemplate = string.Format("~/Administrator/Templates/Default/themes/{0}/css/black.css", adminTheme);
                    break;
            }
            return cssColoredTemplate;
        }

        /// <summary>
        /// Checks if the request is image request or not.
        /// </summary>
        /// <param name="url"> URL</param>
        /// <returns>Returns "True" if the request is for image</returns>
        public bool IsImageRequest(string url)
        {
            bool isImageRequest = false;
            if (url.EndsWith("png") && url.EndsWith("jpg") && url.EndsWith("gif") && url.EndsWith("jpeg") && url.EndsWith("bmp"))
            {
                isImageRequest = true;
            }
            return isImageRequest;
        }

        #endregion
    }
}
