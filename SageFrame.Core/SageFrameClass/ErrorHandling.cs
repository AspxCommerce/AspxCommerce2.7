#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using SageFrame.ErrorLog;
using SageFrame.Web;
using SageFrame.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using SageFrame.Web.Common.SEO;
using SageFrame.Web.Utilities;
using System.Collections;

#endregion


namespace SageFrame.Web
{
    /// <summary>
    /// Collection of methods for error handling in the application.
    /// </summary>
    public class ErrorHandler
    {
        /// <summary>
        /// Recors all the common exception.
        /// </summary>
        /// <param name="exc">Exceptions.</param>
        /// <returns>Returns true if custom error in the application is on.</returns>
        public bool LogCommonException(Exception exc)
        {
            string strIPaddress = string.Empty;
            string strPageUrl = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.UserHostAddress != string.Empty)
            {
                strIPaddress = HttpContext.Current.Request.UserHostAddress;
            }

            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.RawUrl != string.Empty)
            {
                strPageUrl = HttpContext.Current.Request.RawUrl;
            }

            int inID = 0;
            SageFrameConfig sfConfig = new SageFrameConfig();
            ErrorLogController objController = new ErrorLogController();
            inID = objController.InsertLog((int)SageFrame.Web.SageFrameEnums.ErrorType.AdministrationArea, 11, exc.Message, exc.ToString(),
             strIPaddress, strPageUrl, true, sfConfig.GetPortalID, sfConfig.GetUsername);

            return sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseCustomErrorMessages);
        }

        /// <summary>
        /// Records all page method exceptions.
        /// </summary>
        /// <param name="exc">Exceptions.</param>
        /// <returns>Returns true if custom error in the application is on.</returns>
        public bool LogPageMethodException(Exception exc)
        {
            string strIPaddress = string.Empty;
            string strPageUrl = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.UserHostAddress != string.Empty)
            {
                strIPaddress = HttpContext.Current.Request.UserHostAddress;
            }

            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.RawUrl != string.Empty)
            {
                strPageUrl = HttpContext.Current.Request.RawUrl;
            }

            int inID = 0;
            SageFrameConfig sfConfig = new SageFrameConfig();
            ErrorLogController objController = new ErrorLogController();
            inID = objController.InsertLog((int)SageFrame.Web.SageFrameEnums.ErrorType.AdministrationArea, 11, exc.Message, exc.ToString(),
           strIPaddress, strPageUrl, true, sfConfig.GetPortalID, sfConfig.GetUsername);
            return sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseCustomErrorMessages);

        }

        /// <summary>
        /// Records  all the WCF exception
        /// </summary>
        /// <param name="exc">Exceptions.</param>
        /// <returns>Returns true if custom error in the application is on.</returns>
        public bool LogWCFException(Exception exc)
        {
            string strIPaddress = string.Empty;
            string strPageUrl = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.UserHostAddress != string.Empty)
            {
                strIPaddress = HttpContext.Current.Request.UserHostAddress;
            }

            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.RawUrl != string.Empty)
            {
                strPageUrl = HttpContext.Current.Request.RawUrl;
            }

            int inID = 0;
            SageFrameConfig sfConfig = new SageFrameConfig();
            ErrorLogController objController = new ErrorLogController();
            inID = objController.InsertLog((int)SageFrame.Web.SageFrameEnums.ErrorType.AdministrationArea, 11, exc.Message, exc.ToString(),
                     strIPaddress, strPageUrl, true, sfConfig.GetPortalID, sfConfig.GetUsername);
            return sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseCustomErrorMessages);

        }

        /// <summary>
        /// Records  web service type exception.
        /// </summary>
        /// <param name="exc">Exceptions.</param>
        /// <returns>Returns true if custom error in the application is on.</returns>
        public bool LogWebServiceException(Exception exc)
        {
            string strIPaddress = string.Empty;
            string strPageUrl = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.UserHostAddress != string.Empty)
            {
                strIPaddress = HttpContext.Current.Request.UserHostAddress;
            }

            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.RawUrl != string.Empty)
            {
                strPageUrl = HttpContext.Current.Request.RawUrl;
            }

            int inID = 0;
            SageFrameConfig sfConfig = new SageFrameConfig();
            ErrorLogController objController = new ErrorLogController();
            inID = objController.InsertLog((int)SageFrame.Web.SageFrameEnums.ErrorType.AdministrationArea, 11, exc.Message, exc.ToString(),
                 strIPaddress, strPageUrl, true, sfConfig.GetPortalID, sfConfig.GetUsername);
            return sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseCustomErrorMessages);

        }
    }
}
