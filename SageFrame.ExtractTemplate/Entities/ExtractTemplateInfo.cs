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

namespace SageFrame.ExtractTemplate
{
    /// <summary>
    /// Class that contains extractting template information.
    /// </summary>
    public class ExtractTemplateInfo
    {
        /// <summary>
        /// Gets or sets page ID.
        /// </summary>
        public int PageID { get; set; }

        /// <summary>
        /// Gets or sets row number.
        /// </summary>
        public int RowNum { get; set; }

        /// <summary>
        /// Gets or sets pana name.
        /// </summary>
        public string PaneName { get; set; }

        /// <summary>
        /// Gets or sets usermodule ID.
        /// </summary>
        public int UserModuleId { get; set; }

        /// <summary>
        /// Gets or sets numeric value for core template.
        /// </summary>
        public int IsCore { get; set; }

        /// <summary>
        /// Gets or sets numeric value for module order.
        /// </summary>
        public int ModuleOrder { get; set; }

        /// <summary>
        /// Gets or sets module definitions id.
        /// </summary>
        public int ModuleDefId { get; set; }

        /// <summary>
        /// Gets or sets template friendly name.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets module ID.
        /// </summary>
        public int ModuleID { get; set; }

        /// <summary>
        /// Returns or retains true if the module is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets page ID's of pages join by ',' where the page module is to be shown.
        /// </summary>
        public string ShowInPages { get; set; }

        /// <summary>
        /// Initializes an instance of ExtractTemplateInfo class.
        /// </summary>
        public ExtractTemplateInfo()
        {
        }
    }
}
