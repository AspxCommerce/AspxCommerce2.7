#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Permissions;
using System.Web.Security;
using SageFrame.Common;
using SageFrame.Web;

#endregion

namespace SageFrame.Framework
{
    /// <summary>
    /// Contains methods that helps in maintaining security in the application
    /// </summary>
    [Serializable]
    public class SecurityPolicy
    {
        #region "Private Static Variables"

        private static bool m_Initialized = false;

        private static bool m_ReflectionPermission;

        private static bool m_WebPermission;

        private static bool m_AspNetHostingPermission;

        #endregion

        #region "Public Constants"

        //  all supported permissions need an associated public string constant
        public const string ReflectionPermission = "ReflectionPermission";

        public const string WebPermission = "WebPermission";

        public const string AspNetHostingPermission = "AspNetHostingPermission";

        #endregion

        #region "Private Methods"

        /// <summary>
        /// Initializes the permissions variables
        /// </summary>
        private static void GetPermissions()
        {
            if (!m_Initialized)
            {
                //  test RelectionPermission
                System.Security.CodeAccessPermission securityTest;
                try
                {
                    securityTest = new ReflectionPermission(PermissionState.Unrestricted);
                    securityTest.Demand();
                    m_ReflectionPermission = true;
                }
                catch
                {
                    m_ReflectionPermission = false;
                }
                //  test WebPermission
                try
                {
                    securityTest = new System.Net.WebPermission(PermissionState.Unrestricted);
                    securityTest.Demand();
                    m_WebPermission = true;
                }
                catch
                {
                    m_WebPermission = false;
                }
                //  test WebHosting Permission (Full Trust)
                try
                {
                    securityTest = new AspNetHostingPermission(AspNetHostingPermissionLevel.Unrestricted);
                    securityTest.Demand();
                    m_AspNetHostingPermission = true;
                }
                catch
                {
                    m_AspNetHostingPermission = false;
                }
                m_Initialized = true;
            }
        }

        #endregion

        #region "Public Methods"

        /// <summary>
        ///  Get Permission For The Web Application On Which The Application Is Deployed
        /// </summary>
        public static string Permissions
        {
            get
            {
                string strPermissions = "";
                if (Framework.SecurityPolicy.HasReflectionPermission())
                {
                    strPermissions = (strPermissions + (", " + Framework.SecurityPolicy.ReflectionPermission));
                }
                if (Framework.SecurityPolicy.HasWebPermission())
                {
                    strPermissions = (strPermissions + (", " + Framework.SecurityPolicy.WebPermission));
                }
                if (Framework.SecurityPolicy.HasAspNetHostingPermission())
                {
                    strPermissions = (strPermissions + (", " + Framework.SecurityPolicy.AspNetHostingPermission));
                }
                if ((strPermissions != ""))
                {
                    strPermissions = strPermissions.Substring(2);
                }
                return strPermissions;
            }
        }

        /// <summary>
        /// Checks if the server has ASP.NET hosting permission
        /// </summary>
        /// <returns>Returns true if the server has ASP.NET hosting permission</returns>
        public static bool HasAspNetHostingPermission()
        {
            GetPermissions();
            return m_AspNetHostingPermission;
        }

        /// <summary>
        /// Checks if the application has reflection permission
        /// </summary>
        /// <returns></returns>
        public static bool HasReflectionPermission()
        {
            GetPermissions();
            return m_ReflectionPermission;
        }

        /// <summary>
        /// Provide Has Web Permission Granted
        /// </summary>
        /// <returns></returns>
        public static bool HasWebPermission()
        {
            GetPermissions();
            return m_WebPermission;
        }

        /// <summary>
        /// Check Permission 
        /// </summary>
        /// <param name="permissions">Permissions</param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public static bool HasPermissions(string permissions, ref string permission)
        {
            bool _HasPermission = true;
            if ((permissions != ""))
            {
                foreach (string mpermission in ((permissions + ";")).Split(Convert.ToChar(";")))
                {
                    if ((mpermission.Trim() != ""))
                    {
                        switch (mpermission)
                        {
                            case AspNetHostingPermission:
                                if ((HasAspNetHostingPermission() == false))
                                {
                                    _HasPermission = false;
                                }
                                break;
                            case ReflectionPermission:
                                if ((HasReflectionPermission() == false))
                                {
                                    _HasPermission = false;
                                }
                                break;
                            case WebPermission:
                                if ((HasWebPermission() == false))
                                {
                                    _HasPermission = false;
                                }
                                break;
                        }
                    }
                }
            }
            return _HasPermission;
        }

        /// <summary>
        /// Return the name of the logged in user by portalID
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <returns>Logged in userName</returns>
        public string GetUser(int portalID)
        {
            string user = string.Empty;
            try
            {
                PageBase objPageBase = new PageBase();
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsCookieName(portalID)];

                //authCookie.
                if (authCookie != null && authCookie.Value != ApplicationKeys.anonymousUser)
                {
                    if (authCookie.Value != null)
                    {
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                        if (ticket != null)
                        {
                            user = ticket.Name;
                        }
                        else
                        {
                            user = ApplicationKeys.anonymousUser;
                        }
                    }
                    else
                    {
                        user = ApplicationKeys.anonymousUser;
                    }
                }
                else
                {
                    user = ApplicationKeys.anonymousUser;
                }
            }
            catch (Exception)
            {

            }
            return user;
        }


        /// <summary>
        /// Returns the name of the logged in user by portalID
        /// </summary>
        /// <param name="PortalID">portalID</param>
        /// <returns>Logged in userName</returns>
        public string GetUser(int portalID, string cookieFormName)
        {
            string user = string.Empty;
            try
            {
                PageBase objPageBase = new PageBase();
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[cookieFormName];

                //authCookie.
                if (authCookie != null && authCookie.Value != ApplicationKeys.anonymousUser)
                {
                    if (authCookie.Value != null)
                    {
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                        if (ticket != null)
                        {
                            user = ticket.Name;
                        }
                        else
                        {
                            user = ApplicationKeys.anonymousUser;
                        }
                    }
                    else
                    {
                        user = ApplicationKeys.anonymousUser;
                    }
                }
                else
                {
                    user = ApplicationKeys.anonymousUser;
                }
            }
            catch (Exception)
            {

            }
            return user;
        }

        /// <summary>
        /// Returns the forms  authentication ticket object that hold the value of logged in username.
        /// </summary>
        /// <param name="portalID">portalID</param>
        /// <returns>Returns forms authentication ticket</returns>
        public FormsAuthenticationTicket GetUserTicket(int portalID)
        {
            PageBase objPageBase = new PageBase();
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsCookieName(portalID)];              
            if (authCookie != null && authCookie.Value != string.Empty)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                return ticket;
            }
            else
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, ApplicationKeys.anonymousUser, DateTime.Now,
                                                                            DateTime.Now.AddMinutes(30),
                                                                              true,
                                                                              portalID.ToString(),
                                                                              FormsAuthentication.FormsCookiePath);
                return ticket;
            }
        }

        /// <summary>
        /// Returns the name of  the  forms cookie of logged in user by portalID
        /// </summary>
        /// <param name="portalID">portalID</param>
        /// <returns>forms cookie name</returns>
        public string FormsCookieName(int portalID)
        {
            string formName = string.Empty;
            if (HttpContext.Current.Session[SessionKeys.RandomCookieValue] != null && HttpContext.Current.Session[SessionKeys.RandomCookieValue].ToString() != string.Empty)
            {
                formName = FormsAuthentication.FormsCookieName + HttpContext.Current.Session.SessionID + HttpContext.Current.Session[SessionKeys.RandomCookieValue].ToString() + portalID.ToString();
            }
            return formName;
        }


        /// <summary>
        /// To update cookies expiry time from browser while refressing page 
        /// </summary>
        /// <param name="userName">logged in userName</param>
        /// <param name="portalID">portalID</param>
        public void UpdateExpireTime(string userName, int portalID)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                     userName,
                     DateTime.Now,
                     DateTime.Now.AddMinutes(30),
                     true,
                     portalID.ToString(),
                     FormsAuthentication.FormsCookiePath);
            // Encrypt the ticket.
            string encTicket = FormsAuthentication.Encrypt(ticket);
            // Create the cookie.
            HttpCookie objCookie = HttpContext.Current.Request.Cookies[FormsCookieName(portalID)];
            if (objCookie != null)
            {
                SageFrameConfig objConfig = new SageFrameConfig();
                string ServerCookieExpiration = objConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.ServerCookieExpiration);
                int expiryTime = Math.Abs(int.Parse(ServerCookieExpiration));
                expiryTime = expiryTime < 5 ? 5 : expiryTime;
                objCookie.Expires = DateTime.Now.AddMinutes(expiryTime);
                HttpContext.Current.Response.Cookies.Set(objCookie);
            }
            else
            {
                HttpContext.Current.Response.Cookies.Remove(FormsCookieName(portalID));
            }
        }
        #endregion

        #region "Obsolete Methods"

        [Obsolete("Replaced by correctly spelt method")]
        public static bool HasRelectionPermission()
        {
            GetPermissions();
            return m_ReflectionPermission;
        }

        #endregion
    }
}