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
using SageFrame.Pages;
using SageFrame.PagePermission;
#endregion

namespace SageFrame.Pages
{
    /// <summary>
    /// This class holds the properties of PageEntity class.
    /// </summary>
    public class PageEntity
    {
        #region "Private Members"

        int _pageID;
        int _pageOrder;
        string _pageName;
        bool _isVisible;
        int _parentID;
        int _level;
        string _iconFile;
        bool _disableLink;
        string _title;
        string _description;
        string _keyWords;
        string _url;
        string _tabPath;
        string _startDate;
        string _endDate;
        decimal _refreshInterval;
        string _pageHeadText;
        bool _isSecure;
        bool _isPublished;
        bool _isActive;
        bool _isDeleted;
        bool _isModified;
        string _addedOn;
        string _updatedOn;
        string _deletedOn;
        int _portalID;
        string _addedBy;
        string _updatedBy;
        string _deletedBy;
        string _sEOName;
        bool _isShowInFooter;
        bool _isRequiredPage;
        int _beforeID;
        int _afterID;
        string _viewPermissionRoles;
        string _editPermissionRoles;
        string _viewUsers;
        string _editUsers;
        string _prefix;
        int _childCount;
        string _pageNameWithoughtPrefix;
        private List<PagePermissionInfo> _lstPagePermission;

        #endregion

        #region "Public Properties"
        /// <summary>
        /// Get or set PageID.
        /// </summary>
        public int PageID
        {
            get { return _pageID; }
            set { _pageID = value; }
        }
        /// <summary>
        /// Get or set page order.
        /// </summary>
        public int PageOrder
        {
            get { return _pageOrder; }
            set { _pageOrder = value; }
        }
        /// <summary>
        /// Get or set page name.
        /// </summary>
        public string PageName
        {
            get { return _pageName; }
            set { _pageName = value; }
        }
        /// <summary>
        /// Get or set true for visible.
        /// </summary>
        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }
        /// <summary>
        /// Get or set ParentID.
        /// </summary>
        public int ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }
        /// <summary>
        /// Get or set page level.
        /// </summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
        /// <summary>
        /// Get or set page icon file.
        /// </summary>
        public string IconFile
        {
            get { return _iconFile; }
            set { _iconFile = value; }
        }
        /// <summary>
        /// Get or set true for disable link.
        /// </summary>
        public bool DisableLink
        {
            get { return _disableLink; }
            set { _disableLink = value; }
        }
        /// <summary>
        /// Get or set page title.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// Get or set description.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// Get or set key word.
        /// </summary>
        public string KeyWords
        {
            get { return _keyWords; }
            set { _keyWords = value; }
        }
        /// <summary>
        /// Get or set page url.
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        /// <summary>
        /// Get or set page tab path.
        /// </summary>
        public string TabPath
        {
            get { return _tabPath; }
            set { _tabPath = value; }
        }
        /// <summary>
        /// Get or set start date.
        /// </summary>
        public string StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        /// <summary>
        /// Get or set end date.
        /// </summary>
        public string EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        /// <summary>
        /// Get or set page refresh interval.
        /// </summary>
        public decimal RefreshInterval
        {
            get { return _refreshInterval; }
            set { _refreshInterval = value; }
        }
        /// <summary>
        /// Get or set page header text.
        /// </summary>
        public string PageHeadText
        {
            get { return _pageHeadText; }
            set { _pageHeadText = value; }
        }
        /// <summary>
        /// Get or set true if secure.
        /// </summary>
        public bool IsSecure
        {
            get { return _isSecure; }
            set { _isSecure = value; }
        }
        /// <summary>
        /// Get or set true if published.
        /// </summary>
        public bool IsPublished
        {
            get { return _isPublished; }
            set { _isPublished = value; }
        }
        /// <summary>
        /// Get or set true if active.
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        /// <summary>
        /// Get or set true if deleted.
        /// </summary>
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }
        /// <summary>
        /// Get or set true if modified.
        /// </summary>
        public bool IsModified
        {
            get { return _isModified; }
            set { _isModified = value; }
        }
        /// <summary>
        /// Get or set added date.
        /// </summary>
        public string AddedOn
        {
            get { return _addedOn; }
            set { _addedOn = value; }
        }
        /// <summary>
        /// Get or set updated date.
        /// </summary>
        public string UpdatedOn
        {
            get { return _updatedOn; }
            set { _updatedOn = value; }
        }
        /// <summary>
        /// Get or set deleted date.
        /// </summary>
        public string DeletedOn
        {
            get { return _deletedOn; }
            set { _deletedOn = value; }
        }
        /// <summary>
        /// Get or set PortalID.
        /// </summary>
        public int PortalID
        {
            get { return _portalID; }
            set { _portalID = value; }
        }
        /// <summary>
        /// Get or set user name.
        /// </summary>
        public string AddedBy
        {
            get { return _addedBy; }
            set { _addedBy = value; }
        }
        /// <summary>
        /// Get or set user name.
        /// </summary>
        public string UpdatedBy
        {
            get { return _updatedBy; }
            set { _updatedBy = value; }
        }
        /// <summary>
        /// Get or set user name.
        /// </summary>
        public string DeletedBy
        {
            get { return _deletedBy; }
            set { _deletedBy = value; }
        }
        /// <summary>
        /// Get or set page SEO name.
        /// </summary>
        public string SEOName
        {
            get { return _sEOName; }
            set { _sEOName = value; }
        }
        /// <summary>
        /// Get or set true if shoew in footer.
        /// </summary>
        public bool IsShowInFooter
        {
            get { return _isShowInFooter; }
            set { _isShowInFooter = value; }
        }
        /// <summary>
        /// Get or set true for required page.
        /// </summary>
        public bool IsRequiredPage
        {
            get { return _isRequiredPage; }
            set { _isRequiredPage = value; }
        }
        /// <summary>
        /// Get or set previous page ID.
        /// </summary>
        public int BeforeID
        {
            get { return _beforeID; }
            set { _beforeID = value; }
        }
        /// <summary>
        /// Get or set next page ID.
        /// </summary>
        public int AfterID
        {
            get { return _afterID; }
            set { _afterID = value; }
        }
        /// <summary>
        /// Get or set view permission roles.
        /// </summary>
        public string ViewPermissionRoles
        {
            get { return _viewPermissionRoles; }
            set { _viewPermissionRoles = value; }
        }
        /// <summary>
        /// Get or set edit permission roles.
        /// </summary>
        public string EditPermissionRoles
        {
            get { return _editPermissionRoles; }
            set { _editPermissionRoles = value; }
        }
        /// <summary>
        /// Get or set users view.
        /// </summary>
        public string ViewUsers
        {
            get { return _viewUsers; }
            set { _viewUsers = value; }
        }
        /// <summary>
        /// Get or set edit users.
        /// </summary>
        public string EditUsers
        {
            get { return _editUsers; }
            set { _editUsers = value; }
        }
        /// <summary>
        /// Get pr set page prefix.
        /// </summary>
        public string Prefix
        {
            get { return _prefix; }
            set { _prefix = value; }
        }
        /// <summary>
        /// Get or set child count.
        /// </summary>
        public int ChildCount
        {
            get { return _childCount; }
            set { _childCount = value; }
        }
        /// <summary>
        /// Get or set page name without prifix.
        /// </summary>
        public string PageNameWithoughtPrefix
        {
            get { return _pageNameWithoughtPrefix; }
            set { _pageNameWithoughtPrefix = value; }
        }

        /// <summary>
        /// Get or set list of PagePermissionInfo class.
        /// </summary>
        
        public List<PagePermissionInfo> LstPagePermission
        {
            get { return _lstPagePermission; }
            set { _lstPagePermission = value; }
        }
        /// <summary>
        /// Get or set level of page name.
        /// </summary>
        public string LevelPageName { get; set; }
        /// <summary>
        /// Get or set max page order.
        /// </summary>
        public int MaxPageOrder { get; set; }
        /// <summary>
        /// Get or set min page order.
        /// </summary>
        public int MinPageOrder { get; set; }
        /// <summary>
        /// Get or set true for admin page.
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// Get or set menu list.
        /// </summary>
        public string MenuList { get; set; }
        /// <summary>
        /// Get or set pages include in menu.
        /// </summary>
        public string MenuPages {get;set;}
        #endregion
        /// <summary>
        /// Get or set mode for add or update.
        /// </summary>
        public string Mode { get; set; }
        /// <summary>
        /// Get or set caption.
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Get or set update level
        /// </summary>
        public string UpdateLabel { get; set; }
        /// <summary>
        /// Get or set preview code..
        /// </summary>
        public string PreviewCode { get; set; }

        #region "Constructors"
        /// <summary>
        /// Initializes a new instance of the PageEntity class.
        /// </summary>
        public PageEntity() { }
        /// <summary>
        /// Initializes a new instance of the PageEntity class. 
        /// </summary>
        /// <param name="pageID">PageID</param>
        /// <param name="pageOrder">Page order.</param>
        /// <param name="pageName">Page name.</param>
        /// <param name="isVisible">True for visible.</param>
        /// <param name="parentID">ParentID</param>
        /// <param name="level">Page level.</param>
        /// <param name="iconFile">Page icon file.</param>
        /// <param name="disableLink">True for disable link.</param>
        /// <param name="title">Page title.</param>
        /// <param name="description">Page description.</param>
        /// <param name="keyWords">Page key word.</param>
        /// <param name="url">Page url.</param>
        /// <param name="tabPath">Page tab path.</param>
        /// <param name="startDate">Start date.</param>
        /// <param name="endDate">End date.</param>
        /// <param name="refreshInterval">Page refresh interval</param>
        /// <param name="pageHeadText">Page header text.</param>
        /// <param name="isSecure">True if secure.</param>
        /// <param name="isActive">True for active.</param>
        /// <param name="isDeleted">True for deleted.</param>
        /// <param name="isModified">True for modified.</param>
        /// <param name="addedOn">Added date.</param>
        /// <param name="updatedOn">Updated date.</param>
        /// <param name="deletedOn">Deleted date.</param>
        /// <param name="portalID">PortalID</param>
        /// <param name="addedBy">User name.</param>
        /// <param name="updatedBy">User name.</param>
        /// <param name="deletedBy">User name.</param>
        /// <param name="sEOName">Page SEO name.</param>
        /// <param name="isShowInFooter">True if show in footer.</param>
        /// <param name="isRequiredPage">True if requirede page.</param>
        public PageEntity(int pageID, int pageOrder, string pageName, bool isVisible, int parentID, int level, string iconFile, bool disableLink, string title, string description, string keyWords, string url, string tabPath, string startDate, string endDate, decimal refreshInterval, string pageHeadText, bool isSecure, bool isActive, bool isDeleted, bool isModified, string addedOn, string updatedOn, string deletedOn, int portalID, string addedBy, string updatedBy, string deletedBy, string sEOName, bool isShowInFooter, bool isRequiredPage)
        {
            this.PageID = pageID;
            this.PageOrder = pageOrder;
            this.PageName = pageName;
            this.IsVisible = isVisible;
            this.ParentID = parentID;
            this.Level = level;
            this.IconFile = iconFile;
            this.DisableLink = disableLink;
            this.Title = title;
            this.Description = description;
            this.KeyWords = keyWords;
            this.Url = url;
            this.TabPath = tabPath;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.RefreshInterval = refreshInterval;
            this.PageHeadText = pageHeadText;
            this.IsSecure = isSecure;
            this.IsActive = isActive;
            this.IsDeleted = isDeleted;
            this.IsModified = isModified;
            this.AddedOn = addedOn;
            this.UpdatedOn = updatedOn;
            this.DeletedOn = deletedOn;
            this.PortalID = portalID;
            this.AddedBy = addedBy;
            this.UpdatedBy = updatedBy;
            this.DeletedBy = deletedBy;
            this.SEOName = sEOName;
            this.IsShowInFooter = isShowInFooter;
            this.IsRequiredPage = isRequiredPage;
        }
        #endregion
    }

}