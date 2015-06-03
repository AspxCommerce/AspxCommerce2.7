#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Text;
using SageFrame.Web;

#endregion

namespace SageFrame.Common
{
    /// <summary>
    /// Application session keys.
    /// </summary>
    [Serializable]
    public static partial class SessionKeys
    {
        private static int portalID = PortalID();
        public static string TemplateError = "TemplateError_" + portalID;
        public static string SageFrame_PortalSEOName = "SageFrame.PortalSEOName_" + portalID;
        public static string SageFrame_PortalID = "SageFrame.PortalID";
        public static string SageFrame_ActiveTemplate = "SageFrame.ActiveTemplate_" + portalID;
        public static string SageFrame_ActivePreset = "SageFrame.ActivePreset_" + portalID;
        public static string SageFrame_CustomerID = "SageFrame.CustomerID_" + portalID;
        public static string SageFrame_AdminTheme = "SageFrame.AdminTheme_" + portalID;
        public static string SageUserRoles = "SageUserRoles_" + portalID;
        public static string Tracker = "Tracker_" + portalID;
        public static string Ssl = "Ssl_" + portalID;
        public static string prevurl = "prevurl_" + portalID;
        public static string CaptchaImageText = "CaptchaImageText_" + portalID;
        public static string SageUICulture = "SageUICulture_" + portalID;
        public static string SageCulture = "SageCulture_" + portalID;
        public static string SageFrame_UserProfilePic = "SageFrame.UserProfilePic_" + portalID;
        public static string ModuleCss = "ModuleCss";
        public static string ModuleJs = "ModuleJs";
        public static string ModuleList = "ModuleList";
        public static string SageFrame_StoreID = "SageFrame.StoreID_" + portalID;
        public static string pagename = "pagename";
        public static string moduleid = "moduleid";
        public static string LoginHitCount = "LoginHitCount_" + portalID;
        public static string ServiceProvider = "ServiceProvider_" + portalID;
        public static string Identifier = "Identifier";
        public static string MessageTemplateID = "MessageTemplateID";
        public static string UserImage = "UserImage_" + portalID;
        public static string PropertyValueDataTable = "PropertyValueDataTable";
        public static string EditProfileID = "EditProfileID";
        public static string Profile_Image = "Profile_Image_" + portalID;
        public static string FolderID = "FolderID";
        public static string Path = "Path";
        public static string SageRoles = "SageRoles_" + portalID;
        public static string IsParent = "IsParent";
        public static string ParentURL = "ParentURL";
        public static string IsLoginClick = "IsLoginClick" + UserName();
        public static string RandomCookieValue = "RandomCookieValue" + portalID;
        /// <summary>
        /// Initializes a new instance of PortalID. 
        /// </summary>
        /// <returns>PortalID</returns>
        public static int PortalID()
        {
            SageFrameConfig objSage = new SageFrameConfig();
            return objSage.GetPortalID;
        }

        public static string UserName()
        {
            SageFrameConfig objSage = new SageFrameConfig();
            return objSage.GetUsername;
        }
    }
}