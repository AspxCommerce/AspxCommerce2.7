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
    ///  This class holds the properties of MenuPermissionInfo class.
    /// </summary>
    public class MenuPermissionInfo
    {
        #region Private Members

        private int _menuID;
        private int _portalID;
        private int _permissionID;
        private string _roleID;
        private string _username;
        private bool _allowAccess;
        private bool _isActive;
        private string _addedBy;

        #endregion

        #region Public Members
        /// <summary>
        /// Get or set menuID.
        /// </summary>
        public int MenuID
        {
            get { return _menuID; }
            set { _menuID = value; }
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
        public int PermissionID
        {
            get { return _permissionID; }
            set { _permissionID = value; }
        }
        /// <summary>
        /// Get or set RoleID.
        /// </summary>
        public string RoleID
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
        /// Get or set added user name.
        /// </summary>
        public string AddedBy
        {
            get { return _addedBy; }
            set { _addedBy = value; }
        }

        #endregion
        /// <summary>
        /// Initializes a new instance of the MenuPermissionInfo class.
        /// </summary>
        public MenuPermissionInfo() { }

    }

}
