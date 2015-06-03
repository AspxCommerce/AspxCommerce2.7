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

namespace SageFrame.Security.Helpers
{
    /// <summary>
    /// Enum for user creation status.
    /// </summary>
    public enum UserCreationStatus
    {
        DUPLICATE_EMAIL=3,
        DUPLICATE_USER=6,
        SUCCESS=1
    }
}
