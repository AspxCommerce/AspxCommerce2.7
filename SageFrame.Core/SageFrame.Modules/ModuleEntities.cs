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


namespace SageFrame.Modules
{
    public class ModuleEntities
    {
        /// <summary>
        /// Gets or sets module control ID
        /// </summary>
        public int ModuleControlID { get; set; }

        /// <summary>
        /// Gets or sets module definition ID
        /// </summary>
        public int ModuleDefID { get; set; }

        /// <summary>
        /// Gets or sets module control key 
        /// </summary>
        public string ControlKey { get; set; }

        /// <summary>
        /// Gets or sets module control title 
        /// </summary>
        public string ControlTitle { get; set; }

        /// <summary>
        /// Gets or sets module control source 
        /// </summary>
        public string ControlSrc { get; set; }

        /// <summary>
        /// Gets or sets icon file 
        /// </summary>
        public string IconFile { get; set; }

        /// <summary>
        /// Gets or sets module control type 
        /// </summary>
        public int ControlType { get; set; }

        /// <summary>
        /// Gets or sets module display order 
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets module help URL 
        /// </summary>
        public string HelpUrl { get; set; }

        /// <summary>
        /// Returns or retains true if the module supports partial rendering
        /// </summary>
        public bool SupportsPartialRendering { get; set; }

        /// <summary>
        /// Returns or retains true if the module is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Returns or retains true if the module is deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Returns or retains true if the module is modified
        /// </summary>
        public bool IsModified { get; set; }

        /// <summary>
        /// Gets or sets module added date
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Gets or sets  module updated date
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets  module deleted date
        /// </summary>
        public DateTime DeletedOn { get; set; }

        /// <summary>
        /// Gets or sets portal ID
        /// </summary>
        public int PortalID { get; set; }

        /// <summary>
        /// Gets or sets module added user's name
        /// </summary>
        public string AddedBy { get; set; }

        /// <summary>
        /// Gets or sets 
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets module deleted user's name
        /// </summary>
        public string DeletedBy { get; set; }

        /// <summary>
        /// Initializes an instance of ModuleEntities class
        /// </summary>
        public ModuleEntities() { }
    }
}
