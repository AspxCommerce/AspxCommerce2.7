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
    /// This class holds the properties of SectionInfo.

    /// </summary>
    public class SectionInfo
    {
        /// <summary>
        /// Get or set SectionID.
        /// </summary>
        public int SectionID { get; set; }
        /// <summary>
        /// Get or set section name.
        /// </summary>
        public string SectionName { get; set; }
        /// <summary>
        /// Get or set section markup.
        /// </summary>
        public string SectionMarkup { get; set; }
    }
}
