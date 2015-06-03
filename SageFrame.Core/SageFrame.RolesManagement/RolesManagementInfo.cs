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


namespace SageFrame.RolesManagement
{
    /// <summary>
    /// 
    /// </summary>
    public class RolesManagementInfo
    {
        /// <summary>
        /// Gets or sets application ID.
        /// </summary>
        public Guid ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets role ID.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets role  name
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets lowered role name.
        /// </summary>
        public string LoweredRoleName { get; set; }

        /// <summary>
        /// Gets or sets role description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets portal role ID.
        /// </summary>
        public int PortalRoleID { get; set; }

        /// <summary>
        /// Gets or sets portal ID.
        /// </summary>
        public int PortalID { get; set; }

        /// <summary>
        /// Gets or sets RoleID1.
        /// </summary>
        public Guid RoleId1{ get; set; }

        /// <summary>
        /// Returns and retains true if the role is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Returns and retains true if the role is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Returns and retains true if the role is modified.
        /// </summary>
        public bool IsModified { get; set; }

        /// <summary>
        /// Gets or sets role added date.
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Gets or sets role updated date.
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets role deleted date.
        /// </summary>
        public DateTime DeletedOn { get; set; }

        /// <summary>
        /// Gets or sets role added user's name.
        /// </summary>
        public string AddedBy { get; set; }

        /// <summary>
        /// Gets or sets role updated user's name.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets roles deleted user's name.
        /// </summary>
        public string DeletedBy { get; set; }
	
        /// <summary>
        /// Initializes an instance of RolesManagementInfo class.
        /// </summary>
        public RolesManagementInfo() { }
		
    }
}
