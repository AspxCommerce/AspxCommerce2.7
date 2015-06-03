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

namespace SageFrame.Templating
{
    /// <summary>
    ///  This class holds the properties of PresetKeyValue.
    /// </summary>
    public class PresetKeyValue
    {
        /// <summary>
        /// Get or set preset key.
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Get or set preset value.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Initializes a new instance of the PresetKeyValue class.
        /// </summary>
        public PresetKeyValue() { }
    }
}
