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

namespace SageFrame.Security.Enums
{
    /// <summary>
    /// Enum for user update status.
    /// </summary>
    public enum UserUpdateStatus
    {
        USER_UPDATE_SUCCESSFUL=0,
        DUPLICATE_EMAIL_NOT_ALLOWED=1
    }
}
