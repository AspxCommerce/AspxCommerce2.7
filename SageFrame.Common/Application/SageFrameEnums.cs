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


/// <summary>
/// Description for application Enums.
/// </summary>
namespace SageFrame.Web
{
    public class SageFrameEnums
    {
        public SageFrameEnums()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// Application data type.
        /// </summary>
        public enum ECTDataTypes
        {
            Integer = 1,
            Decimal = 2,
            String = 3
        }
        /// <summary>
        /// Application error type.
        /// </summary>
        public enum ErrorType
        {
            Unknown,
            CustomerError,
            MailError,
            OrderError,
            AdministrationArea,
            CommonError,
            ShippingErrror,
            TaxError,
            WCF,
            WebService,
            PageMethod
        }
        /// <summary>
        /// Application permission type.
        /// </summary>
        public enum ViewPermissionType
        {
            View = 0,
            Edit = 1
        }

        /// <summary>
        /// Application control type.
        /// </summary>

        public enum ControlType
        {
            View = 1,
            Edit = 2,
            Setting = 3
        }


    }
    /// <summary>
    /// Application message type
    /// </summary>
    public enum SageMessageType
    {
        Success,
        Error,
        Alert
    }
    /// <summary>
    /// Application message title.
    /// </summary>
    public enum SageMessageTitle
    {
        Information,
        Notification,
        Exception
    }   

}

namespace SageFrame.Modules.Admin.PortalSettings
{
    /// <summary>
    /// Application setting type.
    /// </summary>
    public enum SettingType
    {
        SiteAdmin,
        SuperUser
    }
}