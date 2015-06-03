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

namespace SageFrame.MenuManager
{
    /// <summary>
    /// This class holds the properties of MenuManagerInfo class.
    /// </summary>
    public class MenuManagerInfo
    {
        /// <summary>
        /// Get or set MenuID.
        /// </summary>
        public int MenuID { get; set; }
        /// <summary>
        /// Get or set MenuItemID.
        /// </summary>
        public int MenuItemID { get; set; }
        /// <summary>
        /// Get or set menu name.
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// Get or set true for default menu.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Get or set style for menu type.
        /// </summary>
        public string MenuTypeStyle { get; set; }
        /// <summary>
        /// Get or set menu header text.
        /// </summary>
        public string MenuHeaderText { get; set; }
        /// <summary>
        /// Get or set true if image show in menu.
        /// </summary>
        public bool ShowImage { get; set; }
        /// <summary>
        /// Get or set true if text show in menu.
        /// </summary>
        public bool Showtext { get; set; }


        /// <summary>
        /// Get or set setting key.
        /// </summary>
        public string SettingKey { get; set; }
        /// <summary>
        /// Get or set setting value.
        /// </summary>
        public string SettingValue { get; set; }
        /// <summary>
        /// Get or set paortalID.
        /// </summary>
        public int PortalID { get; set; }
        /// <summary>
        /// Get or set UserModuleID.
        /// </summary>
        public int UserModuleID { get; set; }
        /// <summary>
        /// Get or set added user name.
        /// </summary>
        public string AddedBy { get; set; }
        /// <summary>
        /// Get or set true if active.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Get or set updated user name.
        /// </summary>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Get or set display mode.
        /// </summary>
        public string DisplayMode { get; set; }
        /// <summary>
        /// Get or set menu sub type.
        /// </summary>
        public string TopMenuSubType { get; set; }
        /// <summary>
        /// Get or set menu subtitle level.
        /// </summary>
        public string SubTitleLevel { get; set; }
        /// <summary>
        /// Get or set selected menu.
        /// </summary>
        public string SelectedMenu { get; set; }
        /// <summary>
        /// Get or set PageID.
        /// </summary>
        public int PageID { get; set; }
        /// <summary>
        /// Get or set menu type.
        /// </summary>
        public string MenuType { get; set; }
        /// <summary>
        /// Get or set page order.
        /// </summary>
        public int PageOrder { get; set; }
        /// <summary>
        /// Get or set page name.
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// Get or set page parent ID. 
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// Get or set level.
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Get or set page name leve.
        /// </summary>
        public string LevelPageName { get; set; }
        /// <summary>
        /// Get or set page child count.
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
        /// Get or set sub text.
        /// </summary>
        public string SubText { get; set; }
        /// Get or set true if allow visible.
        /// </summary>
        public bool IsVisible { get; set; }
        /// <summary>
        ///  Get or set true if page show in menu.
        /// </summary>
        /// /// <summary>
        public bool ShowInMenu { get; set; }
        /// <summary>
        /// Get or set link type.
        /// </summary>
        public string LinkType { get; set; }
        /// <summary>
        /// Get or set title.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Get or set link url.
        /// </summary>
        public string LinkURL { get; set; }
        /// <summary>
        /// Get or set icon for menu.
        /// </summary>
        public string ImageIcon { get; set; }
        /// <summary>
        /// Get or set caption.
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Get or set HTML content.
        /// </summary>
        public string HtmlContent { get; set; }
        /// <summary>
        /// Get or set menu level.
        /// </summary>
        public string MenuLevel { get; set; }
        /// <summary>
        /// Get or set menu order.
        /// </summary>
        public string MenuOrder { get; set; }
        /// <summary>
        /// Get or set mode.
        /// </summary>
        public string Mode { get; set; }
        /// <summary>
        /// Get or set URL.
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// Get or set true if default page order is preserved.
        /// </summary>
        public bool PreservePageOrder { get; set; }
        /// <summary>
        /// Get or set parent.
        /// </summary>
        public int MainParent { get; set; }
        /// <summary>
        /// Get or set type of side nenu.
        /// </summary>
        public string SideMenuType { get; set; }
        /// <summary>
        /// Get or set culture code.
        /// </summary>
        public string CultureCode { get; set; }
        /// <summary>
        /// Initializes a new instance of the MenuManagerInfo class.
        /// </summary>
        public MenuManagerInfo() { }
        /// <summary>
        /// Initializes a new instance of the MenuManagerInfo class.
        /// </summary>
        /// <param name="_PageID">PageID</param>
        /// <param name="_PageOrder">Page order.</param>
        /// <param name="_PageName">Page name.</param>
        /// <param name="_ParentID">Page parent ID.</param>
        /// <param name="_Level">Level</param>
        /// <param name="_LevelPageName">Page name.</param>
        /// <param name="_SEOName">Page SEO name.</param>
        /// <param name="_TabPath">TabPath</param>
        /// <param name="_IsVisible">true if allow visible.</param>
        /// <param name="_ShowInMenu">true if page show in menu.</param>
        public MenuManagerInfo(int _PageID, int _PageOrder, string _PageName, int _ParentID, int _Level, string _LevelPageName, string _SEOName, string _TabPath, bool _IsVisible, bool _ShowInMenu)
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

        //SiteMap
        /// <summary>
        /// Get or set Description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Get or set updated date time..
        /// </summary>
        public DateTime UpdatedOn { get; set; }
        /// <summary>
        /// Get or set added date time.
        /// </summary>
        public DateTime AddedOn { get; set; }
        /// <summary>
        /// Get or set sitemap change frequency.
        /// </summary>
        public string ChangeFreq { get; set; }


    }
}
