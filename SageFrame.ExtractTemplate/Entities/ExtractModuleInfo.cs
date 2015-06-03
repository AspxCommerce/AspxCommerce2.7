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
    /// Class that contains extracting module properties.
    /// </summary>
    public class ExtractModuleInfo
    {
        /// <summary>
        /// Gets or sets module ID.
        /// </summary>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets module's supported features.
        /// </summary>
        public int SupportedFeatures { get; set; }

        /// <summary>
        /// Gets or sets package ID.
        /// </summary>
        public int PackageID { get; set; }

        /// <summary>
        /// Gets or sets module description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets module friendly name.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets module version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets module's business controller class name.
        /// </summary>
        public string BusinessControllerClass { get; set; }

        /// <summary>
        /// Gets or sets  module's folder name.
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or sets  modules permissions.
        /// </summary>
        public string Permissions { get; set; }

        /// <summary>
        /// Gets or sets  module name.
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets module compatible version.
        /// </summary>
        public string CompatibleVersions { get; set; }

        /// <summary>
        /// Gets or sets  module dependencies.
        /// </summary>
        public string Dependencies { get; set; }

        /// <summary>
        /// Gets or sets  module added user's name.
        /// </summary>
        public string AddedBy { get; set; }

        /// <summary>
        /// Gets or sets  module updated user's name.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets  module deleted user's name.
        /// </summary>
        public string DeletedBy { get; set; }

        /// <summary>
        /// Returns or retains true if the module is premium
        /// </summary>
        public bool IsPremium { get; set; }

        /// <summary>
        /// Returns or retains true if the module is admin.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Returns or retains true if the module is required.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Returns or retains true if the module is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Returns or retains true if the module is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Returns or retains true if the module is modified.
        /// </summary>
        public bool IsModified { get; set; }

        /// <summary>
        /// Gets or sets ExtractModuleDefInfo object containing module definition.
        /// </summary>
        public ExtractModuleDefInfo ModuleDef { get; set; }

        /// <summary>
        /// Initializes an instance of ExtractModuleInfo class.
        /// </summary>
        public ExtractModuleInfo()
        {
        }
    }
}
