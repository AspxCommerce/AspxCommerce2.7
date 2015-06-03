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
    /// This class holds the properties of SageFrameUserCollection
    /// </summary>
    public class SageFrameUserCollection
    {
        /// <summary>
        /// Get or set list of UserInfo class.
        /// </summary>
        public List<UserInfo> UserList { get; set; }
    }
}
