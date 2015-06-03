#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
#endregion

namespace SageFrame.Shared
{
    /// <summary>
    ///  This class holds the properties for GoogleAnalyticsInfo
    /// </summary>
    public class GoogleAnalyticsInfo
    {
        #region "Private Members"
        int _GoogleAnalyticsID;
        string _googleJSCode;
        bool _isActive;
        bool _isModified;
        DateTime _addedOn;
        DateTime _updatedOn;
        int _portalID;
        string _addedBy;
        string _updatedBy;
        #endregion

        #region "Constructors"
        /// <summary>
        /// Initializes a new instance of the GoogleAnalyticsInfo.
        /// </summary>
        public GoogleAnalyticsInfo() { }
        /// <summary>
        /// Initializes a new instance of the GoogleAnalyticsInfo.
        /// </summary>
        /// <param name="GoogleAnalyticsID">GoogleAnalyticsID</param>
        /// <param name="googleJSCode">googleJSCode</param>
        /// <param name="isActive">True for active.</param>
        /// <param name="isModified">True for modified.</param>
        /// <param name="addedOn">Added date</param>
        /// <param name="updatedOn">Updated date</param>
        /// <param name="portalID">PortalID</param>
        /// <param name="addedBy">User name.</param>
        /// <param name="updatedBy">User name.</param>
        public GoogleAnalyticsInfo(int GoogleAnalyticsID, string googleJSCode, bool isActive, bool isModified, DateTime addedOn, DateTime updatedOn, int portalID, string addedBy, string updatedBy)
        {
            this.GoogleAnalyticsID = GoogleAnalyticsID;
            this.GoogleJSCode = googleJSCode;
            this.IsActive = isActive;
            this.IsModified = isModified;
            this.AddedOn = addedOn;
            this.UpdatedOn = updatedOn;
            this.PortalID = portalID;
            this.AddedBy = addedBy;
            this.UpdatedBy = updatedBy;
        }
        #endregion
        
        #region "Public Properties"
        /// <summary>
        /// Get or set GoogleAnalyticsID
        /// </summary>
        public int GoogleAnalyticsID
        {
            get { return _GoogleAnalyticsID; }
            set { _GoogleAnalyticsID = value; }
        }
        /// <summary>
        /// Get or set code.
        /// </summary>
        public string GoogleJSCode
        {
            get { return _googleJSCode; }
            set { _googleJSCode = value; }
        }
        /// <summary>
        ///  Get or set true if active.  
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        /// <summary>
        ///  Get or set true if modified.
        /// </summary>
        public bool IsModified
        {
            get { return _isModified; }
            set { _isModified = value; }
        }
        /// <summary>
        /// Get or set added date and time.
        /// </summary>
        public DateTime AddedOn
        {
            get { return _addedOn; }
            set { _addedOn = value; }
        }
        /// <summary>
        /// Get or set updated date and time.
        /// </summary>
        public DateTime UpdatedOn
        {
            get { return _updatedOn; }
            set { _updatedOn = value; }
        }
        /// <summary>
        /// Get or set portal ID.
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
        #endregion
    }
}
