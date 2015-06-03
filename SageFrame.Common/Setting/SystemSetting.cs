#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Web;
#endregion

namespace SageFrame.Web
{
    /// <summary>
    /// Declaration of system settings.
    /// </summary>
    public static class SystemSetting
    {
        #region "Declaration"
        public static string glbDefaultPane = "ContentPane";
        public static string glbImageFileTypes = "jpg,jpeg,jpe,gif,bmp,png,swf";
        public static string SageFrameDBName = System.Configuration.ConfigurationManager.AppSettings["DatabaseName"].ToString();
        public static string SageFrameConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SageFrameConnectionString"].ToString();
        public static string Register = "Register";
        public static string Login = "Login";
        public static string Logout = "Logout";
        public static string Administration = "Administration";
        public static string AnonymousUsername = "anonymous user";
        public static string REGISTER_USER_ROLENAME = "Registered User";
        public static string ANONYMOUS_ROLEID = string.Empty;
        public static string DataBaseOwner = GetDataBaseOwner();
        public static string ObjectQualifer = GetObjectQualifer();
        public static string[] SYSTEM_ROLES = { "registered user", "anonymous user", "site admin", "super user" };
        public static string[] SYSTEM_USERS = { "anonymoususer", "siteadmin", "Admin" };
        public static string[] SYSTEM_SUPER_ROLES = { "site admin", "super user" };
        public static string[] SUPER_ROLE = {"super user" };
        public static string  SITEADMIN = "site admin";
        public static string[] SYSTEM_APPLICATION_ROLES = { "super user" };
        public static string[] SYSTEM_USER_NOTALLOW_HTMLCOMMENT = { "anonymoususer" };
        public static string[] SYSTEM_ROLES_ALLOW_HTMLCOMMENT = { "registered user", "site admin", "super user" };
        public static int[] SYSTEM_MESSAGE_TEMPLATES = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16};
        public static Int32 ACCOUNT_ACTIVATION_EMAIL = 1;
        public static Int32 PASSWORD_CHANGE_REQUEST_EMAIL = 2;
        public static Int32 ACTIVATION_SUCCESSFUL_EMAIL = 3;
        public static Int32 PASSWORD_RECOVERED_SUCCESSFUL_EMAIL = 4;
        public static Int32 USER_REGISTRATION_HELP = 5;
        public static Int32 USER_RESISTER_SUCESSFUL_INFORMATION_NONE = 6;
        public static Int32 USER_RESISTER_SUCESSFUL_INFORMATION_PRIVATE = 7;
        public static Int32 USER_RESISTER_SUCESSFUL_INFORMATION_VERIFIED = 8;
        public static Int32 USER_RESISTER_SUCESSFUL_INFORMATION_PUBLIC = 9;
        public static Int32 ACTIVATION_SUCCESSFUL_INFORMATION = 10;
        public static Int32 ACTIVATION_FAIL_INFORMATION = 11;
        public static Int32 PASSWORD_FORGOT_HELP = 12;
        public static Int32 PASSWORD_RECOVERED_SUCESSFUL_INFORMATION = 13;
        public static Int32 PASSWORD_RECOVERD_FAIL_INFORMATION = 14;
        public static Int32 PASSWORD_FORGOT_USERNAME_PASSWORD_MATCH = 15;
        public static Int32 PASSWORD_RECOVERED_HELP = 16;
        public static Int32 ORDER_PLACED = 17;
        public static Int32 ORDER_STATUS_CHANGED = 18;
        public static Int32 SHARED_WISHED_LIST = 19;
        public static Int32 REFER_A_FRIEND_EMAIL = 20;
        public static Int32 OUT_OF_STOCK_NOTIFICATION = 21;
        public static string glbConfigFolder = "\\Config\\";
        public static string glbVersionConfigFolder = "\\Config\\VersionConfig\\";
        public static string glbConnStringConfigFolder = "\\Config\\ConnStringConfig\\";
        public static string[] INCOMPRESSIBLE_EXTENSIONS = { ".gif", ".jpg", ".png", ".axd", ".asmx", ".css", ".js", "Fconnector.aspx", ".html", ".htm", "connector.aspx?", "fckstyles.xml" };
        public static string[] ALLOWED_EXTENSIONS = { ".gif", ".jpg", ".png" };
        public static string[] ALLOWED_FILES = { ".gif", ".jpg", ".png", ".htm", ".xml", ".html", ".cs", ".ascx", ".js", ".asmx",".css" };
        public static string[] SYSTEM_DEFAULT_USERS = { "anonymoususer", "siteadmin"};
        #endregion
        /// <summary>
        /// Return database owner.
        /// </summary>
        /// <returns>Database owner.</returns>
        public static string GetDataBaseOwner()
        {
            string _databaseOwner = System.Configuration.ConfigurationManager.AppSettings["databaseOwner"].ToString();
            if (_databaseOwner != "" && _databaseOwner.EndsWith(".") == false)
            {
                _databaseOwner += ".";
            }
            return _databaseOwner;
        }
        /// <summary>
        /// Return object qualifer.
        /// </summary>
        /// <returns>Object qualifer.</returns>
        public static string GetObjectQualifer()
        {
            string _objectQualifier = System.Configuration.ConfigurationManager.AppSettings["objectQualifier"].ToString();
            if ((_objectQualifier != "") && (_objectQualifier.EndsWith("_") == false))
            {
                _objectQualifier += "_";
            }
            return _objectQualifier;
        }        
    }
}
