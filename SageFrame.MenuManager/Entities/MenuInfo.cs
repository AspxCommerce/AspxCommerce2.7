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

namespace SageFrame.SageMenu
{
    /// <summary>
    /// This class holds the properties of MenuInfo class.
    /// </summary>
    public class MenuInfo
    {
        /// <summary>
        /// Get or set PageID.
        /// </summary>
        public int PageID { get; set; }
        /// <summary>
        /// Get or set Page order.
        /// </summary>
        public int PageOrder { get; set; }
        /// <summary>
        /// Get or set Page name.
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// Get or set ParentID.
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// Get or set page level.
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Get or set page name associate with level.
        /// </summary>
        public string LevelPageName { get; set; }
        /// <summary>
        /// Get or set count of  child page.
        /// </summary>
        public int ChildCount { get; set; }
        /// <summary>
        /// Get or set page SEO name.
        /// </summary>
        public string SEOName { get; set; }
        /// <summary>
        /// Get or set tab path.
        /// </summary>
        public string TabPath { get; set; }
        /// <summary>
        /// Get or set true if allow visible.
        /// </summary>
        public bool IsVisible { get; set; }
        /// <summary>
        /// Get or set true if page show in menu.
        /// </summary>
        public bool ShowInMenu { get; set; }
        /// <summary>
        /// Get or set icon file.
        /// </summary>
        public string IconFile { get; set; }
        /// <summary>
        /// Get or set title.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Get or set list of MenuInfo class.
        /// </summary>
        public List<MenuInfo> LISTMenu { get; set; }
        /// <summary>
        /// Initializes a new instance of the MenuInfo class.
        /// </summary>
        public MenuInfo() { }
        /// <summary>
        /// Initializes a new instance of the MenuInfo class.
        /// </summary>
        /// <param name="_PageID">PageID</param>
        /// <param name="_PageOrder">Page order.</param>
        /// <param name="_PageName">Page name.</param>
        /// <param name="_ParentID">ParentID</param>
        /// <param name="_Level">Page level.</param>
        /// <param name="_LevelPageName">Page name associate with level.</param>
        /// <param name="_SEOName">Page SEO name.</param>
        /// <param name="_TabPath">TabPath</param>
        /// <param name="_IsVisible">true if allow visible.</param>
        /// <param name="_ShowInMenu">true if page show in menu.</param>
        public MenuInfo(int _PageID, int _PageOrder, string _PageName, int _ParentID, int _Level,string _LevelPageName,string _SEOName,string _TabPath,bool _IsVisible,bool _ShowInMenu)
        {
            this.PageID = _PageID;
            this.PageOrder = _PageOrder;
            this.PageName = _PageName;
            this.ParentID = _ParentID;
            this.Level = _Level;
            this.LevelPageName = _LevelPageName;
            this.SEOName = _SEOName;
            this.TabPath = _TabPath;
            this.IsVisible = _IsVisible;
            this.ShowInMenu = _ShowInMenu;
        }
        /// <summary>
        /// Initializes a new instance of the MenuInfo class.
        /// </summary>
        /// <param name="_PageID">PageID</param>
        /// <param name="_PageOrder">Page order.</param>
        /// <param name="_PageName">Page name.</param>
        /// <param name="_ParentID">ParentID </param>
        /// <param name="_Level">Page level.</param>
        /// <param name="_LevelPageName">Page name associate with level.</param>
         /// <param name="_SEOName">Page SEO name.</param>
        /// <param name="_TabPath">TabPath</param>
        /// <param name="_IsVisible">true if allow visible.</param>
        /// <param name="_ShowInMenu">true if page show in menu.</param>
        /// <param name="_IconFile">IconFile</param>
        public MenuInfo(int _PageID, int _PageOrder, string _PageName, int _ParentID, int _Level, string _LevelPageName, string _SEOName, string _TabPath, bool _IsVisible, bool _ShowInMenu, string _IconFile)
        {
            this.PageID = _PageID;
            this.PageOrder = _PageOrder;
            this.PageName = _PageName;
            this.ParentID = _ParentID;
            this.Level = _Level;
            this.LevelPageName = _LevelPageName;
            this.SEOName = _SEOName;
            this.TabPath = _TabPath;
            this.IsVisible = _IsVisible;
            this.ShowInMenu = _ShowInMenu;
            this.IconFile = _IconFile;

        }
        /// <summary>
        /// Initializes a new instance of the MenuInfo class.
        /// </summary>
        /// <param name="_PageID">PageID</param>
        /// <param name="_ParentID">ParentID </param>
        /// /// <param name="_PageName">Page name.</param>
        /// <param name="_Level">Page level.</param>
        /// <param name="_TabPath">TabPath</param>
         public MenuInfo(int _PageID, int _ParentID, string _PageName, int _Level,string _TabPath)
        {
            this.PageID = _PageID;
            this.ParentID = _ParentID;
            this.PageName = _PageName;
            this.Level = _Level;
            this.TabPath = _TabPath;
        }
        /// <summary>
         /// Initializes a new instance of the MenuInfo class.
        /// </summary>
         /// <param name="_PageID">ParentID</param>
        /// <param name="_PageName">Page name.</param>
        public MenuInfo(int _PageID, string _PageName)
        {
            this.PageID = _PageID;
            this.PageName = _PageName;
        }
    }
}
