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


namespace SageFrame.Core
{
    /// <summary>
    /// Properties collection for startup events.
    /// </summary>
    public class SystemEventStartUpInfo
    {
        /// <summary>
        /// Gets or sets PortalStartUpID.
        /// </summary>
        public int PortalStartUpID { get; set; }

        /// <summary>
        /// Gets or sets portal ID.
        /// </summary>
        public int PortalID { get; set; }

        /// <summary>
        /// Gets or sets event location name.
        /// </summary>
        public string EventLocationName { get; set; }

        /// <summary>
        /// Gets or sets control URL.
        /// </summary>
        public string ControlUrl { get; set; }

        /// <summary>
        /// Returns or retains true if the startup event is admin.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Returns or retains true if the startup event is control type.
        /// </summary>
        public bool IsControlUrl { get; set; }

        /// <summary>
        ///  Returns or retains true if the startup event is of system.
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        ///  Returns or retains true if the startup event is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        ///  Returns or retains true if the startup event is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        ///  Returns or retains true if the startup event is modified.
        /// </summary>
        public bool IsModified { get; set; }

        /// <summary>
        /// Gets or sets startup event added date.
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Gets or sets startup event updated date.
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets startup event deleted date,
        /// </summary>
        public DateTime DeletedOn { get; set; }

        /// <summary>
        /// Gets or sets startup event added user's name.
        /// </summary>
        public string AddedBy { get; set; }

        /// <summary>
        /// Gets or sets updated user's name.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets deleted user's name.
        /// </summary>
        public string DeletedBy { get; set; }
       
        /// <summary>
        /// Initializes an instance of SystemEventStartUpInfo.
        /// </summary>
        public SystemEventStartUpInfo() { }

    }

    /// <summary>
    /// Collection of properties of startup event's list.
    /// </summary>
    public class GetPortalStartUpList
    {
        /// <summary>
        /// Gets or sets portal startup ID.
        /// </summary>
        public int PortalStartUpID { get; set; }

        /// <summary>
        /// Gets or sets portal ID.
        /// </summary>
        public int PortalID { get; set; }

        /// <summary>
        /// Gets or sets startup event location name.
        /// </summary>
        public string EventLocationName { get; set; }

        /// <summary>
        /// Gets or sets startup event control URL.
        /// </summary>
        public string ControlUrl { get; set; }

        /// <summary>
        /// Initializes an instance of GetportalStartUpList.
        /// </summary>
        public GetPortalStartUpList()
        {
        }
    }

    /// <summary>
    /// Enum class for startup event for the position.
    /// </summary>
    public enum SystemEventLocation
    {
        Top = 1,
        Middle = 2,
        Bottom = 3
    }
}
