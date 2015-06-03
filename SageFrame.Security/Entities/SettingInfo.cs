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
    /// This class holds the properties of SettingInfo.
    /// </summary>
    public class SettingInfo
    {
        /// <summary>
        /// Get or set setting key.
        /// </summary>
        public string SettingKey { get; set; }
        /// <summary>
        /// Get or set setting value.
        /// </summary>
        public string SettingValue { get; set; }

    }
}
