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
#endregion

namespace SageFrame.Security.Entities
{
    /// <summary>
    /// Enum for application settings.
    /// </summary>
    public enum SettingsEnum
    {
        DUPLICATE_USERS_ACROSS_PORTALS=2,
        DUPLICATE_ROLES_ACROSS_PORTALS=3,
        DEFAULT_PASSWORD_FORMAT=1,
        SELECTED_PASSWORD_FORMAT=4,
        DUPLICATE_EMAIL_ALLOWED=5,
        ENABLE_CAPTCHA=6,
        DEFAULT_CAPTCHA_STATUS=0

    }
}
