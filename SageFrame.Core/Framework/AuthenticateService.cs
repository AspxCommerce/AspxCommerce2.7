using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web;
using SageFrame.Framework;
using SageFrame.Web.Utilities;

namespace SageFrame.Services
{
    /// <summary>
    /// Authenticate if the user have permission for the module or not
    /// </summary>
    public class AuthenticateService : System.Web.Services.WebService
    {
        /// <summary>
        /// Initializes a new instance of the AuthenticateService class.
        /// </summary>
        public AuthenticateService()
        {
        }
        /// <summary>
        /// strict use for http post method for edit or setting control.
        /// </summary>
        /// <param name="portalId">portalId</param>
        /// <param name="userModuleId">userModuleId</param>
        /// <param name="userName">userName</param>
        /// <returns>bool </returns>
        public bool IsPostAuthenticated(int portalId, int userModuleId, string userName, string authToken)
        {

            string user = GetUsername(portalId, authToken);
            if (user == "superuser")
            {
                return true;
            }
            else if (user != "anonymoususer" && user == userName)
            {
                List<KeyValuePair<string, object>> para = new List<KeyValuePair<string, object>>();
                para.Add(new KeyValuePair<string, object>("@UserModuleID", userModuleId));
                para.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                para.Add(new KeyValuePair<string, object>("@userName", userName));
                SQLHandler handler = new SQLHandler();
                int flag = handler.ExecuteAsScalar<int>("usp_CheckModulePermissionEdit", para);
                if (flag == 1)
                    return true;
                else
                    return false;
            }
            return false;
        }

        /// <summary>
        /// authenticates method for view controls.
        /// </summary>
        /// <param name="portalId">portalId</param>
        /// <param name="userModuleId">userModuleId</param>
        /// <param name="userName">userName</param>
        /// <returns>bool </returns>
        public bool IsPostAuthenticatedView(int portalId, int userModuleId, string userName, string authToken)
        {
            string user = GetUsername(portalId, authToken);
            if (user == "superuser")
            {
                return true;
            }
            else if (user != "anonymoususer" && user == userName)
            {
                List<KeyValuePair<string, object>> para = new List<KeyValuePair<string, object>>();
                para.Add(new KeyValuePair<string, object>("@UserModuleID", userModuleId));
                para.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                para.Add(new KeyValuePair<string, object>("@userName", userName));
                SQLHandler handler = new SQLHandler();
                int flag = handler.ExecuteAsScalar<int>("usp_CheckModulePermissionView", para);
                if (flag == 1)
                    return true;
                else
                    return false;
            }
            return false;
        }

        /// <summary>
        /// Returns username
        /// </summary>
        /// <param name="portalID">portalID</param>
        /// <param name="authToken"> authentication token</param>
        /// <returns>Returns userName</returns>
        private string GetUsername(int portalID, string authToken)
        {
            try
            {
                SecurityPolicy objSecurity = new SecurityPolicy();
                string userName = objSecurity.GetUser(portalID, authToken);
                if (userName != ApplicationKeys.anonymousUser)
                {
                    return userName;
                }
                else
                {
                    return ApplicationKeys.anonymousUser;
                }
            }
            catch
            {
                return ApplicationKeys.anonymousUser;
            }
        }
    }
}
