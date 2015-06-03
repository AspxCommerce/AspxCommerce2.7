using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageFrame.Pages
{
    public class PageRoleSettingsInfo
    {

         #region Private Members
         
        private int _pageID;
        private int _portalID;
        private string _permissionID;
        private Guid _roleID;
        private string _username;
        private bool _allowAccess;
        private bool _isActive;
        private string _addedBy;
        private string _roleName;
        private string _pageName;
        private List<PageRoleSettingsInfo> _lstPagePermission;
        #endregion
         
        #region Public Members
        /// <summary>
        /// Get or set PageID.
        /// </summary>
        public int PageID
        {
            get { return _pageID; }
            set { _pageID = value; }
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
        /// Get or set PermissionID.
        /// </summary>
        public string PermissionID
        {
            get { return _permissionID; }
            set { _permissionID = value; }
        }
        /// <summary>
        /// Get or set RoleID.
        /// </summary>
        public Guid RoleID
        {
            get { return _roleID; }
            set { _roleID = value; }
        }
        /// <summary>
        /// Get or set user name.
        /// </summary>
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        /// <summary>
        /// Get or set true if allow access.
        /// </summary>
        public bool AllowAccess
        {
            get { return _allowAccess; }
            set { _allowAccess = value; }
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
        /// Get or set user name.
        /// </summary>
        public string AddedBy
        {
            get { return _addedBy; }
            set { _addedBy = value; }
        }
        /// <summary>
        /// Get or set role name.
        /// </summary>
        public string RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }
        /// <summary>
        /// Get or set page name.
        /// </summary>
        public string PageName
        {
            get { return _pageName; }
            set { _pageName = value; }
        }
        public List<PageRoleSettingsInfo> LstPagePermission
        {
            get { return _lstPagePermission; }
            set { _lstPagePermission = value; }
        }
        #endregion
        /// <summary>
        /// Initializes a new instance of the PageRoleSettingsInfo class.
        /// </summary>
        public PageRoleSettingsInfo() { }
    }
}
