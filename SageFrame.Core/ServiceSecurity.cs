using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Security;
using SageFrame.Framework;
using SageFrame.Security;
using System.Text;
using System.Web;
using SageFrame.Web;
using SageFrame.Web.Utilities;


namespace SageFrame.Core
{
    public class ServiceSecurity
    {
        private static HttpContext context;

        private static bool IsUserLoggedIn(int portalID)
        {
            bool IsLoggedIn = false;
            SecurityPolicy objSecurity = new SecurityPolicy();
            FormsAuthenticationTicket ticket = objSecurity.GetUserTicket(portalID);
            if (ticket != null)
            {
                int LoggedInPortalID = ticket.UserData != "" && ticket.UserData != null ? int.Parse(ticket.UserData.ToString()) : 0;
                if (ticket.Name != ApplicationKeys.anonymousUser)
                {
                    string[] sysRoles = SystemSetting.SUPER_ROLE;
                    if (portalID == LoggedInPortalID || Roles.IsUserInRole(ticket.Name, sysRoles[0]))
                    {
                        IsLoggedIn = true;
                    }
                }
            }
            return IsLoggedIn;
        }

        /// <summary>
        /// create new token on every new page is requested by browser
        /// </summary>
        /// <returns>new token</returns>
        public static string CreateToken(int portalID)
        {
            string token = "";
            if (HttpContext.Current.Session["Auth_Token"] != null)
            {
                token = HttpContext.Current.Session["Auth_Token"].ToString();
            }
            else
            {
                token = IssueToken(portalID);
                HttpContext.Current.Session["Auth_Token"] = token;
            }

            byte[] byteArray = Encoding.ASCII.GetBytes(token);
            return Convert.ToBase64String(byteArray);

            //return Guid.NewGuid().ToString();
        }

        public static string IssueToken(int portalID)
        {
            string token = "";
            string uniqueId = Guid.NewGuid().ToString();
            RoleController role = new RoleController();
            string roles = role.GetRoleNames(GetUser(portalID), portalID).ToLower(); //.Split(',');
            SecurityPolicy objSecurity = new SecurityPolicy();
            string authcookie = objSecurity.FormsCookieName(portalID);
            string auth = IsUserLoggedIn(portalID) ? authcookie : "anonymoususer";
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            token = uniqueId + ":" + auth + ":" + timestamp;
            HttpContext.Current.Session["Auth_Token"] = token;
            return token;
        }

        private static string GetToken(string encodedString)
        {
            byte[] data = Convert.FromBase64String(encodedString);
            string decodedString = Encoding.ASCII.GetString(data);
            return decodedString;
        }

        private static string FormsCookieName(int portalID)
        {
            string formName = string.Empty;
            if (HttpContext.Current.Session["RandomCookieValue" + portalID] != null && HttpContext.Current.Session["RandomCookieValue" + portalID].ToString() != string.Empty)
            {
                formName = FormsAuthentication.FormsCookieName + HttpContext.Current.Session.SessionID + HttpContext.Current.Session["RandomCookieValue" + portalID].ToString() + portalID.ToString();
            }
            return formName;
        }

        private static string GetUser(int portalID)
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

        private static bool ValidateTimeStamp(string timestamp)
        {
            DateTime startTime = DateTime.ParseExact(timestamp, "yyyyMMddHHmmssffff", CultureInfo.InvariantCulture);
            //DateTime.Parse(timestamp);

            DateTime endTime = DateTime.Now;

            TimeSpan span = endTime.Subtract(startTime);
            //if (span.Minutes > 20)
            //{
            //    return false;
            //}
            return true;
            //Console.WriteLine("Time Difference (seconds): " + span.Seconds);
            //Console.WriteLine("Time Difference (minutes): " + span.Minutes);
            //Console.WriteLine("Time Difference (hours): " + span.Hours);
            //Console.WriteLine("Time Difference (days): " + span.Days);
        }

        public static string AllowRoles = "";
        public static bool ValidateToken(HttpContext currentContext)
        {

            context = currentContext;

            if (context != null)
            {
                //X-Requested-With=XMLHttpRequest 
                //this means only allowing ajax call 
                if (context.Request.Headers["X-Requested-With"] != null)//X-Requested-With
                {
                    string token = context.Request.Headers.GetValues("X-Requested-With").FirstOrDefault();
                    if (token != "XMLHttpRequest")//testtest
                    {
                        //throw new System.Security.SecurityException("Invalid Request!");
                        context.Response.StatusCode = 401;
                        context.Response.End();
                    }
                }
                else
                {
                    throw new System.Security.SecurityException("Invalid Request!");
                }
                //context.User.Identity.IsAuthenticated

                string headerTicket = context.Request.Headers["ASPX-TOKEN"];
                if (string.IsNullOrEmpty(headerTicket))
                    throw new System.Security.SecurityException("Security token must be present.");

                string clientToken = GetToken(headerTicket);
                string serverToken = HttpContext.Current.Session["Auth_Token"].ToString();

                int userModuleId = 0, portalId = 0;
                string userName = "", permissionType = "";
                bool escapeCookieAuth = true;
                if (context.Request.Headers["Escape"] != null)
                    escapeCookieAuth = int.Parse(context.Request.Headers["Escape"]) != 0;
                if (!escapeCookieAuth)
                {
                    if (context.Request.Headers["UMID"] != null)
                        userModuleId = int.Parse(context.Request.Headers["UMID"]);
                    if (context.Request.Headers["UName"] != null)
                        userName = context.Request.Headers["UName"];
                    if (context.Request.Headers["PID"] != null)
                        portalId = int.Parse(context.Request.Headers["PID"]);
                    if (context.Request.Headers["PType"] != null)
                        permissionType = context.Request.Headers["PType"];
                }
                if (clientToken.ToLower() == serverToken.ToLower())
                {
                    var tokenParts = clientToken.Split(':');
                    if (ValidateTimeStamp(tokenParts[2]))
                    {
                        //special cases when requesting fx doesnot require authorization
                        if (escapeCookieAuth)
                            return true;
                        string authCookieName = tokenParts[1];
                        return CheckAuth(portalId, userModuleId, userName, authCookieName, permissionType);

                    }
                    else
                    {
                        IssueToken(portalId);
                        throw new System.Security.SecurityException("Security token has been expired.");

                    }

                }
                else
                {
                    throw new System.Security.SecurityException("Invalid Security token!");
                }
                //string Key = _tokenFormat + context.Session.SessionID;
                //string ServerTicket = Convert.ToString(context.Session[Key]);

                //if (string.Compare(headerTicket, ServerTicket, false) != 0)
                //{
                //    throw new System.Security.SecurityException("Invalid Security token.");
                //}
                //else return true;
            }
            else
                throw new System.Security.SecurityException("Authentication Failed.");
        }




        private static bool CheckAuth(int portalId, int userModuleId, string uName, string authToken, string permType)
        {
            string spName = "";
            spName = permType == "v" ? "usp_CheckModulePermissionView" : "usp_CheckModulePermissionEdit";
            SecurityPolicy objSecurity = new SecurityPolicy();
            string userName = objSecurity.GetUser(portalId, authToken);

            if (userName == "superuser")
            {
                return true;
            }
            else if (permType == "e" && uName != "anonymoususer" && uName == userName)
            {
                List<KeyValuePair<string, object>> para = new List<KeyValuePair<string, object>>();
                para.Add(new KeyValuePair<string, object>("@UserModuleID", userModuleId));
                para.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                para.Add(new KeyValuePair<string, object>("@userName", uName));
                SQLHandler handler = new SQLHandler();
                int flag = handler.ExecuteAsScalar<int>(spName, para);
                if (flag == 1)
                    return true;
                else
                    return false;
            }
            else if (permType == "v" && uName == userName)
            {
                List<KeyValuePair<string, object>> para = new List<KeyValuePair<string, object>>();
                para.Add(new KeyValuePair<string, object>("@UserModuleID", userModuleId));
                para.Add(new KeyValuePair<string, object>("@PortalID", portalId));
                para.Add(new KeyValuePair<string, object>("@userName", uName));
                SQLHandler handler = new SQLHandler();
                int flag = handler.ExecuteAsScalar<int>(spName, para);
                if (flag == 1)
                    return true;
                else
                    return false;

            }
            return false;
        }

    }
}
