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
    /// Enum for password format.
    /// </summary>
    public enum PasswordFormats
    {
        CLEAR=1,
        ONE_WAY_HASHED=2,
        ENCRYPTED_AES=3,
        ENCRYPTED_RSA=4
    }
}
