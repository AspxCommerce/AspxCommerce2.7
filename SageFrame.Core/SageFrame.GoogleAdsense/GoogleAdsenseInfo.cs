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


namespace SageFrame.GoogleAdsense
{
    /// <summary>
    /// This class holds the properties for Google Adsense
    /// </summary>
    public class GoogleAdsenseInfo
    {
        /// <summary>
        /// Gets or sets the  google adsense setting name.
        /// </summary>
        public string SettingName { get; set; }

        /// <summary>
        /// Gets or sets the google adsense setting value.
        /// </summary>
        public string SettingValue { get; set; }

        /// <summary>
        /// Returns or retains true if the google adsense is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Returns or retains true if the google adsense is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Returns or retains true if the google adsense is modified
        /// </summary>
        public bool IsModified { get; set; }

        /// <summary>
        /// Returns or retains the google adsense added date
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        ///  Returns or retains the google adsense update date
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        ///  Returns or retains the google adsense deleted date
        /// </summary>
        public DateTime DeletedOn { get; set; }

        /// <summary>
        ///  Gets or sets portalID 
        /// </summary>
        public int PortalID { get; set; }

        /// <summary>
        /// Gets or sets the google adsense added user's name
        /// </summary>
        public string AddedBy { get; set; }

        /// <summary>
        /// Gets or sets the google adsense updated user's  name
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the google adsense deleted user's  name
        /// </summary>
        public string DeletedBy { get; set; }

        /// <summary>
        /// Initializes a new instance of the GoogleAdsenseInfo class.
        /// </summary>
        public GoogleAdsenseInfo() { }
    }
}
