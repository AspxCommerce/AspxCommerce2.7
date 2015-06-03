using System;
using System.Collections.Generic;
using System.Text;
using SageFrame.Web;
using System.Web;

namespace SageFrame.Common
{
    /// <summary>
    /// Application common api about pages and URL.
    /// </summary>
    public partial class PortalAPI
    {

        # region "Page Name With Extension only"
        /// <summary>
        /// Get application page with extension.
        /// </summary>
        public static string RegistrationPageWithExtension
        {
            get
            {
                return BuildPageNameWithExtension(SageFrameSettingKeys.PortalRegistrationPage);
            }
        }
        /// <summary>
        /// Get login page with extension.
        /// </summary>
        public static string LoginPageWithExtension
        {
            get
            {
                return BuildPageNameWithExtension(SageFrameSettingKeys.PortalLoginpage);
            }
        }
        /// <summary>
        /// Get application default page with extension.
        /// </summary>
        public static string DefaultPageWithExtension
        {
            get
            {
                return BuildPageNameWithExtension(SageFrameSettingKeys.PortalDefaultPage);
            }
        }
        /// <summary>
        /// Get application profile page with extension.
        /// </summary>
        public static string ProfilePageWithExtension
        {
            get
            {
                return BuildPageNameWithExtension(SageFrameSettingKeys.PortalUserProfilePage);
            }
        }
        /// <summary>
        /// Get application forgot password page with extension.
        /// </summary>
        public static string ForgotPasswordPageWithExtension
        {
            get
            {
                return BuildPageNameWithExtension(SageFrameSettingKeys.PortalForgotPassword);
            }
        }
        /// <summary>
        /// Get application page not found page with extension.
        /// </summary>
        public static string PageNotFoundPageWithExtension
        {
            get
            {
                return BuildPageNameWithExtension(SageFrameSettingKeys.PortalPageNotFound);
            }
        }
        /// <summary>
        /// Get application password recovery page with extension.
        /// </summary>
        public static string PasswordRecoveryPageWithExtension
        {
            get
            {
                return BuildPageNameWithExtension(SageFrameSettingKeys.PortalPasswordRecovery);
            }
        }
        /// <summary>
        /// Get application page not accessible page with extension.
        /// </summary>
        public static string PageNotAccessiblePageWithExtension
        {
            get
            {
                return BuildPageNameWithExtension(SageFrameSettingKeys.PortalPageNotAccessible);
            }
        }
        #endregion

        #region "WithOut Root URL"
        /// <summary>
        ///  Get application registration url without root path.
        /// </summary>
        public static string RegistrationURL
        {
            get
            {
                return BuildURL(SageFrameSettingKeys.PortalRegistrationPage, false);
            }
        }
        /// <summary>
        /// Get application login url without root path.
        /// </summary>
        public static string LoginURL
        {
            get
            {
                return BuildURL(SageFrameSettingKeys.PortalLoginpage, false);
            }
        }
        /// <summary>
        /// Get application default url without root path.
        /// </summary>
        public static string DefaultPageURL
        {
            get
            {
                return BuildURL(SageFrameSettingKeys.PortalDefaultPage, false);
            }
        }
        /// <summary>
        /// Get application profile page url without root path.
        /// </summary>
        public static string ProfilePageURL
        {
            get
            {
                return BuildURL(SageFrameSettingKeys.PortalUserProfilePage, false);
            }
        }
        /// <summary>
        /// Get application forgot password url without root path.
        /// </summary>
        public static string ForgotPasswordURL
        {
            get
            {
                return BuildURL(SageFrameSettingKeys.PortalForgotPassword, false);
            }
        }
        /// <summary>
        /// Get application page not found url without root path.
        /// </summary>
        public static string PageNotFoundURL
        {
            get
            {
                return BuildURL(SageFrameSettingKeys.PortalPageNotFound, false);
            }
        }
        /// <summary>
        /// Get application password recovery url without root path.
        /// </summary>
        public static string PasswordRecoveryURL
        {
            get
            {
                return BuildURL(SageFrameSettingKeys.PortalPasswordRecovery, false);
            }
        }
        /// <summary>
        /// Get application page not accessible url without root path.
        /// </summary>
        public static string PageNotAccessibleURL
        {
            get
            {
                return BuildURL(SageFrameSettingKeys.PortalPageNotAccessible, false);
            }
        }

        #endregion

        #region "With Root URL"
        /// <summary>
        ///  Get application registration url with  root path.
        /// </summary>
        public static string RegistrationURLWithRoot
        {
            get
            {
                return BuildURL(SageFrameSettingKeys.PortalRegistrationPage, true);
            }
        }
        /// <summary>
        ///  Get application login url with root path.
        /// </summary>
        public static string LoginURLWithRoot
        {
            get
            {
                return BuildURL(SageFrameSettingKeys.PortalLoginpage, true);
            }
        }
        /// <summary>
        ///  Get application default page url with root path.
        /// </summary>
        public static string DefaultPageURLWithRoot
        {
            get
            {
                return BuildURL(SageFrameSettingKeys.PortalDefaultPage, true);
            }
        }
        /// <summary>
        ///  Get application profile page url with root path.
        /// </summary>
        public static string ProfilePageURLWithRoot
        {
            get
            {
                return BuildURL(SageFrameSettingKeys.PortalUserProfilePage, true);
            }
        }
        /// <summary>
        ///  Get application forgot password url with root path.
        /// </summary>
        public static string ForgotPasswordURLWithRoot
        {
            get
            {
                return BuildURL(SageFrameSettingKeys.PortalForgotPassword, true);
            }
        }
        /// <summary>
        ///  Get application page not found url with root path.
        /// </summary>
        public static string PageNotFoundURLWithRoot
        {
            get
            {
                return BuildURL(SageFrameSettingKeys.PortalPageNotFound, true);
            }
        }
        /// <summary>
        ///  Get application password recovery url with root path.
        /// </summary>
        public static string PasswordRecoveryURLWithRoot
        {
            get
            {
                return BuildURL(SageFrameSettingKeys.PortalPasswordRecovery, true);
            }
        }
        /// <summary>
        ///  Get application page not accessible url with root path.
        /// </summary>
        public static string PageNotAccessibleURLWithRoot
        {
            get
            {
                return BuildURL(SageFrameSettingKeys.PortalPageNotAccessible, true);
            }
        }

        #endregion

        /// <summary>
        /// Returns application path.
        /// </summary>
        public static string GetApplicationName
        {
            get
            {
                return (HttpContext.Current.Request.ApplicationPath == "/" ? "" : HttpContext.Current.Request.ApplicationPath);
            }
        }

        #region "Private Methods"
        /// <summary>
        /// Return page name with pageextension.
        /// </summary>
        /// <param name="settingKey">SageFrame settingkey</param>
        /// <returns>string</returns>
        private static string BuildPageNameWithExtension(string settingKey)
        {
            StringBuilder strBuilder = new StringBuilder();
            SageFrameConfig sfConfig = new SageFrameConfig();
            strBuilder.Append(ReplaceString(sfConfig.GetSettingValueByIndividualKey(settingKey)));
            strBuilder.Append(SageFrameSettingKeys.PageExtension);
            return strBuilder.ToString();
        }
        /// <summary>
        /// Return application url.
        /// </summary>
        /// <param name="settingKey">SageFrame setting key</param>
        /// <param name="withRoot">Boolean variable to check for inculding a root path.</param>
        /// <returns>string</returns>
        private static string BuildURL(string settingKey, bool withRoot)
        {
            string url = string.Empty;
            try
            {
                StringBuilder strBuilder = new StringBuilder();
                SageFrameConfig sfConfig = new SageFrameConfig();

                if (withRoot)
                {
                    strBuilder.Append(HttpContext.Current.Request.Url.Authority);
                }
                strBuilder.Append(GetApplicationName);
                strBuilder.Append("/");
                strBuilder.Append(ReplaceString(sfConfig.GetSettingValueByIndividualKey(settingKey)));
                strBuilder.Append(SageFrameSettingKeys.PageExtension);
                url = strBuilder.ToString();
            }
            catch
            {
            }
            return url;
        }
        /// <summary>
        /// Replaces "blank sapce" with "-" and "&" with "-and-" and returns page name
        /// </summary>
        /// <param name="strPageName">Page name.</param>
        /// <returns>string</returns>
        public static string ReplaceString(string strPageName)
        {
            strPageName = strPageName.Replace(" ", "-");
            strPageName = strPageName.Replace("&", "-and-");
            return strPageName;
        }

        #endregion
    }
}