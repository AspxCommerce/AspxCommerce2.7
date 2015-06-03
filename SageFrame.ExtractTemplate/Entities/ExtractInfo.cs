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
    /// Class that contains module details.
    /// </summary>
    public class ExtractInfo
    {
        /// <summary>
        /// Gets or sets row number.
        /// </summary>
        public int RowNum { get; set; }

        /// <summary>
        /// Gets or sets module ID.
        /// </summary>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets suppored feature of module.
        /// </summary>
        public int SupportedFeatures { get; set; }

        /// <summary>
        /// Gets or sets package ID.
        /// </summary>
        public int PackageID { get; set; }

        /// <summary>
        /// Gets or sets module description.
        /// </summary>
        public string ModuleDescription { get; set; }

        /// <summary>
        /// Gets or sets module's friendly name.
        /// </summary>
        public string ModuleFriendlyName { get; set; }

        /// <summary>
        /// Gets or sets moduel's version.
        /// </summary>
        public string ModuleVersion { get; set; }

        /// <summary>
        /// Gets or sets business controller class.
        /// </summary>
        public string BusinessControllerClass { get; set; }

        /// <summary>
        /// Gets or sets folder name.
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or sets permissions.
        /// </summary>
        public string Permissions { get; set; }

        /// <summary>
        /// Gets or sets module name.
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets module compatible versions.
        /// </summary>
        public string CompatibleVersions { get; set; }

        /// <summary>
        /// Gets or sets module dependencies.
        /// </summary>
        public string Dependencies { get; set; }

        /// <summary>
        /// Returns or retains true if the module is premium.
        /// </summary>
        public bool IsPremium { get; set; }

        /// <summary>
        /// Returns or retains true if the module is of admin.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Returns or retains true if the module is required.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Returns or retains true if the module is active.
        /// </summary>
        public bool ModuleIsActive { get; set; }

        //PageInfo
        /// <summary>
        /// Gets or sets page ID.
        /// </summary>
        public int PageID { get; set; }

        /// <summary>
        /// Gets or sets page order.
        /// </summary>
        public int PageOrder { get; set; }

        /// <summary>
        /// Gets or sets numerical value if the page is visible.
        /// </summary>
        public int PageIsVisible { get; set; }

        /// <summary>
        /// Gets or sets page's parent's ID.
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// Gets or sets page level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Returns or retains true if the link is disable.
        /// </summary>
        public bool DisableLink { get; set; }

        /// <summary>
        /// Returns or retains true if the page is secure.
        /// </summary>
        public bool IsSecure { get; set; }

        /// <summary>
        /// Returns or retains true if the page is active.
        /// </summary>
        public bool PageIsActive { get; set; }

        /// <summary>
        /// Returns or retains true if the menu is show in footer.
        /// </summary>
        public bool IsShowInFooter { get; set; }

        /// <summary>
        /// Returns or retains true if the page is required.
        /// </summary>
        public bool IsRequiredPage { get; set; }

        /// <summary>
        /// Gets or sets page name.
        /// </summary>
        public string PageName { get; set; }

        /// <summary>
        /// Gets or sets page icon file name.
        /// </summary>
        public string PageIconFile { get; set; }

        /// <summary>
        /// Gets or sets page title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets page description.
        /// </summary>
        public string PageDescription { get; set; }

        /// <summary>
        /// Gets or sets page keywords.
        /// </summary>
        public string KeyWords { get; set; }

        /// <summary>
        /// Gets or sets page URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets page tab path.
        /// </summary>
        public string TabPath { get; set; }

        /// <summary>
        /// Gets or sets page head text.
        /// </summary>
        public string PageHeadText { get; set; }

        /// <summary>
        /// Gets or sets page SEO name.
        /// </summary>
        public string PageSEOName { get; set; }

        /// <summary>
        /// Gets or sets refresh interval time.
        /// </summary>
        public float RefreshInterval { get; set; }

        //ModuleDefInfo

        /// <summary>
        /// Gets or sets module definition ID.
        /// </summary>
        public int ModuleDefID { get; set; }

        /// <summary>
        /// Gets or sets modules friendly name.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets default cache time.
        /// </summary>
        public int DefaultCacheTime { get; set; }

        /// <summary>
        /// Returns or retains true if the module definitions is active.
        /// </summary>
        public bool ModuleDefIsActive { get; set; }

        //PageModule

        /// <summary>
        /// Gets or sets page module ID.
        /// </summary>
        public int PageModuleID { get; set; }

        /// <summary>
        /// Gets or sets module order.
        /// </summary>
        public int ModuleOrder { get; set; }

        /// <summary>
        /// Gets or sets page module cache time.
        /// </summary>
        public int CacheTime { get; set; }

        /// <summary>
        /// Gets or sets page module numeric value for visibility.
        /// </summary>
        public int Visibility { get; set; }

        /// <summary>
        /// Gets or sets page module pane name.
        /// </summary>
        public string PaneName { get; set; }

        /// <summary>
        /// Gets or sets page module alignment.
        /// </summary>
        public string Alignment { get; set; }

        /// <summary>
        /// Gets or sets page module color.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets page module border.
        /// </summary>
        public string Border { get; set; }

        /// <summary>
        /// Gets or sets page module icon file name.
        /// </summary>
        public string PageModuleIconFile { get; set; }

        /// <summary>
        /// Returns or retains true if the page module has display title.
        /// </summary>
        public bool DisplayTitle { get; set; }

        /// <summary>
        /// Returns or retains true if page module has display print.
        /// </summary>
        public bool DisplayPrint { get; set; }

        /// <summary>
        /// Returns or retains true if the page module is active
        /// </summary>
        public bool PageModuleIsActive { get; set; }


        //userModule

        /// <summary>
        /// Gets or sets user module ID.
        /// </summary>
        public int UserModuleID { get; set; }

        /// <summary>
        /// Gets or sets user module title.
        /// </summary>
        public string UserModuleTitle { get; set; }

        /// <summary>
        /// Gets or sets user module header.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets user module footer.
        /// </summary>
        public string Footer { get; set; }

        /// <summary>
        /// Gets or sets user module SEO name.
        /// </summary>
        public string UserModuleSEOName { get; set; }

        /// <summary>
        /// Gets or sets user module to be shown in pages join by ','.
        /// </summary>
        public string ShowInPages { get; set; }

        /// <summary>
        /// Gets or sets user module header text.
        /// </summary>
        public string HeaderText { get; set; }

        /// <summary>
        /// Gets or sets user module suffix class.
        /// </summary>
        public string SuffixClass { get; set; }

        /// <summary>
        /// Returns or retains true if the user module is to be shown in all pages.
        /// </summary>
        public bool AllPages { get; set; }

        /// <summary>
        /// Returns or retains true if the user module has to inherit permissions.
        /// </summary>
        public bool InheritViewPermissions { get; set; }

        /// <summary>
        /// Returns or retains true if the user module is active.
        /// </summary>
        public bool UserModuleIsActive { get; set; }

        /// <summary>
        /// Returns or retains true if the user module is handheld.
        /// </summary>
        public bool IsHandheld { get; set; }

        /// <summary>
        /// Returns or retains true if the user module has to show header text.
        /// </summary>
        public bool ShowHeaderText { get; set; }

        /// <summary>
        /// Returns or retains true if the user module is of admin.
        /// </summary>
        public bool IsInAdmin { get; set; }

        /// <summary>
        /// Initializes an instance of ExtracInfo class.
        /// </summary>
        public ExtractInfo()
        {
        }
    }
}
