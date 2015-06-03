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
    /// Class that contains properties for extracting user modules.
    /// </summary>
    public class ExtractUserModule
    {

        /// <summary>
        /// Gets or sets page ID.
        /// </summary>
        public int PageID { get; set; }

        /// <summary>
        /// Gets or sets page module's pane name.
        /// </summary>
        public string PaneName { get; set; }

        /// <summary>
        /// Gets or sets user module ID.
        /// </summary>
        public int UserModuleId { get; set; }

        /// <summary>
        /// Gets or sets user module order.
        /// </summary>
        public int ModuleOrder { get; set; }

        /// <summary>
        /// Returns or retains true if the user module is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets pages ID join by ','.
        /// </summary>
        public string ShowInPages { get; set; }

        /// <summary>
        /// Gets or sets user module title.
        /// </summary>
        public string UserModuleTitle { get; set; }

        /// <summary>
        /// Gets or sets user module header.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets user moduel footer.
        /// </summary>
        public string Footer { get; set; }

        /// <summary>
        /// Gets or sets page  SEO name.
        /// </summary>
        public string SEOName { get; set; }

        /// <summary>
        /// Gets or sets user module suffix class.
        /// </summary>
        public string SuffixClass { get; set; }

        /// <summary>
        /// Gets or sets user module header text.
        /// </summary>
        public string HeaderText { get; set; }

        /// <summary>
        /// Gets or sets query.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Returns or retains true if the module is to be shown in all pages.
        /// </summary>
        public bool AllPages { get; set; }

        /// <summary>
        /// Returns or retains true if the module inherits view permissions.
        /// </summary>
        public bool InheritViewPermissions { get; set; }

        /// <summary>
        /// Returns or retains true if the module is loaded in handheld.
        /// </summary>
        public bool IsHandheld { get; set; }

        /// <summary>
        /// Returns or retains true if the module has to show header text.
        /// </summary>
        public bool ShowHeaderText { get; set; }

        /// <summary>
        /// Returns or retains true if the module is admin.
        /// </summary>
        public bool IsInAdmin { get; set; }

        /// <summary>
        /// Gets or sets module level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets list of TemplatePermission class object to return or retain template permission.
        /// </summary>
        public List<TemplatePermission> TemplatePermission { get; set; }

        /// <summary>
        /// Initializes an instance of ExtractUserModule class.
        /// </summary>
        public ExtractUserModule()
        {
        }
    }
}
