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
using System.Web.Security;
using SageFrame.ErrorLog;
using SageFrame.Web;
using System.Web.UI;
using SageFrame.Common;
#endregion

namespace SageFrame.FileManager
{ 
    public class FileManagerBase : System.Web.UI.Page
    {
        int PortalID = 1;
        /// <summary>
        /// Handles application exceptions.
        /// </summary>
        /// <param name="exc">exc</param>
        public void ProcessException(Exception exc)
        {
            int inID = 0;
            ErrorLogController objController = new ErrorLogController();
            inID = objController.InsertLog((int)SageFrame.Web.SageFrameEnums.ErrorType.AdministrationArea, 11, exc.Message, exc.ToString(),
                                       HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.RawUrl, true, GetPortalID, GetUsername);
        }
        /// <summary>
        /// Obtain portalId.
        /// </summary>
        public int GetPortalID
        {
            get
            {
                try
                {
                    if (Session[SessionKeys.SageFrame_PortalID] != null && Session[SessionKeys.SageFrame_PortalID].ToString() != "")
                    {
                        return int.Parse(Session[SessionKeys.SageFrame_PortalID].ToString());
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch
                {
                    return 1;
                }
            }
        }
        /// <summary>
        /// Sets portalId
        /// </summary>
        /// <param name="portalID">portalID</param>
        public void SetPortalID(int portalID)
        {
            PortalID = portalID;
        }
        /// <summary>
        /// Obtains userName.
        /// </summary>
        public string GetUsername
        {
            get
            {
                try
                {
                    if (Session["UserName"] == null)
                    {
                        MembershipUser user = Membership.GetUser();
                        if (user != null)
                        {
                            Session["UserName"] = user.UserName;
                            return user.UserName;
                        }
                        else
                        {
                            return "anonymoususer";
                        }

                    }
                    else
                    {
                        return Session["UserName"].ToString();
                    }

                }
                catch
                {
                    return "anonymoususer";
                }
            }
        }
    }
}
