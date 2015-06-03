//using System.Web.Security;
//using SageFrame.Framework;
//using SageFrame.Security;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web;

//namespace ServiceInvoker
//{
//    public class ServiceSecurity
//    {


//        public static string CreateToken()
//        {
//            string uniqueId = new Guid().ToString();
//            RoleController _role = new RoleController();
//            string roles = _role.GetRoleNames(GetUser(1), 1).ToLower();//.Split(',');
//            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
//            string token = uniqueId + ":" + roles + ":" + timestamp;
//            HttpContext.Current.Session["Auth_Token"] = token;
//            byte[] byteArray = Encoding.ASCII.GetBytes(token);
//            return Convert.ToBase64String(byteArray);



//            //return Guid.NewGuid().ToString();
//        }

//        private static string getToken(string encodedString)
//        {
//            byte[] data = Convert.FromBase64String(encodedString);
//            string decodedString = Encoding.ASCII.GetString(data);
//            return decodedString;
//        }

//        private static string FormsCookieName(int portalID)
//        {
//            string formName = string.Empty;
//            if (HttpContext.Current.Session["RandomCookieValue" + portalID] != null && HttpContext.Current.Session["RandomCookieValue" + portalID].ToString() != string.Empty)
//            {
//                formName = FormsAuthentication.FormsCookieName + HttpContext.Current.Session.SessionID + HttpContext.Current.Session["RandomCookieValue" + portalID].ToString() + portalID.ToString();
//            }
//            return formName;
//        }

//        private static string GetUser(int portalID)
//        {
//            string user = string.Empty;
//            try
//            {
//                PageBase objPageBase = new PageBase();
//                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsCookieName(portalID)];

//                //authCookie.
//                if (authCookie != null && authCookie.Value != ApplicationKeys.anonymousUser)
//                {
//                    if (authCookie.Value != null)
//                    {
//                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
//                        if (ticket != null)
//                        {
//                            user = ticket.Name;
//                        }
//                        else
//                        {
//                            user = ApplicationKeys.anonymousUser;
//                        }
//                    }
//                    else
//                    {
//                        user = ApplicationKeys.anonymousUser;
//                    }
//                }
//                else
//                {
//                    user = ApplicationKeys.anonymousUser;
//                }
//            }
//            catch (Exception)
//            {

//            }
//            return user;
//        }

//        private void tokenPattern() { }


//        private static bool validateTimeStamp(string timestamp)
//        {

//            DateTime startTime = DateTime.Parse(timestamp);

//            DateTime endTime = DateTime.Now;

//            TimeSpan span = endTime.Subtract(startTime);
//            if (span.Minutes > 5)
//            {
//                return false;
//            }
//            return true;
//            //Console.WriteLine("Time Difference (seconds): " + span.Seconds);
//            //Console.WriteLine("Time Difference (minutes): " + span.Minutes);
//            //Console.WriteLine("Time Difference (hours): " + span.Hours);
//            //Console.WriteLine("Time Difference (days): " + span.Days);
//        }

//        public static string AllowRoles = "";
//        public static bool ValidateToken()
//        {
//            HttpContext context = HttpContext.Current;
//            if (context != null)
//            {
//                string headerTicket = context.Request.Headers["ASPX-TOKEN"];
//                if (string.IsNullOrEmpty(headerTicket))
//                    throw new System.Security.SecurityException("Security token must be present.");

//                string clientToken = getToken(headerTicket);
//                string serverToken = HttpContext.Current.Session["Auth_Token"].ToString();
//                if (clientToken.ToLower() == serverToken.ToLower())
//                {
//                    var tokenParts = clientToken.Split(':');
//                    if (validateTimeStamp(tokenParts[2]))
//                    {
//                        AllowRoles = tokenParts[1];
//                        return true;

//                    }
//                    else
//                    {
//                        AllowRoles = "";
//                        //reload
//                        throw new System.Security.SecurityException("Security token has been expired.");
//                        return false;
//                    }

//                }
//                else
//                {
//                    throw new System.Security.SecurityException("Invalid Security token!");
//                }
//                //string Key = _tokenFormat + context.Session.SessionID;
//                //string ServerTicket = Convert.ToString(context.Session[Key]);

//                //if (string.Compare(headerTicket, ServerTicket, false) != 0)
//                //{
//                //    throw new System.Security.SecurityException("Invalid Security token.");
//                //}
//                //else return true;
//            }
//            else
//                throw new System.Security.SecurityException("Authentication Failed.");
//        }

//    }
//}
