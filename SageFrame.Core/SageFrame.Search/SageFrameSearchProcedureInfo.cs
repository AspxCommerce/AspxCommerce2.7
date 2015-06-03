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


namespace SageFrame.Search
{
    /// <summary>
    /// Entity clas for search.
    /// </summary>
    public class SageFrameSearchProcedureInfo
    {
        #region "Private Members"
        int _sageFrameSearchProcedureID;
        string _sageFrameSearchTitle;
        string _sageFrameSearchProcedureName;
        string _sageFrameSearchProcedureExecuteAs;
        bool _isActive;
        bool _isDeleted;
        bool _isModified;
        DateTime _addedOn;
        DateTime _updatedOn;
        DateTime _deletedOn;
        int _portalID;
        string _addedBy;
        string _updatedBy;
        string _deletedBy;
        #endregion

        #region "Constructors"
        /// <summary>
        /// Initializes an instance of SageFrameSearchProcedureInfo  class.
        /// </summary>
        public SageFrameSearchProcedureInfo()
        {
        }

        /// <summary>
        /// Initializes an instance of SageFrameSearchProcedureInfo class.
        /// </summary>
        /// <param name="sageFrameSearchProcedureID">Stored procedure ID.</param>
        /// <param name="sageFrameSearchTitle">Search title</param>
        /// <param name="sageFrameSearchProcedureName">Search procedure name</param>
        /// <param name="sageFrameSearchProcedureExecuteAs">Search procedure executes as.</param>
        /// <param name="isActive">Set true if the searching stored procedure is active.</param>
        /// <param name="isDeleted">Set true if the searching stored procedure is deleted.</param>
        /// <param name="isModified">Set true if the searching stored procedure is modified.</param>
        /// <param name="addedOn">Store procedure added date</param>
        /// <param name="updatedOn">Store procedure updated date.</param>
        /// <param name="deletedOn">Store procedure deleted date.</param>
        /// <param name="portalID">Portal ID.</param>
        /// <param name="addedBy">Store procedure added user's name.</param>
        /// <param name="updatedBy">Store procedure updated user's name.</param>
        /// <param name="deletedBy">Store procedure deleted user's name.</param>
        public SageFrameSearchProcedureInfo(int sageFrameSearchProcedureID, string sageFrameSearchTitle, string sageFrameSearchProcedureName, string sageFrameSearchProcedureExecuteAs, bool isActive, bool isDeleted, bool isModified, DateTime addedOn, DateTime updatedOn, DateTime deletedOn, int portalID, string addedBy, string updatedBy, string deletedBy)
        {
            this.SageFrameSearchProcedureID = sageFrameSearchProcedureID;
            this.SageFrameSearchTitle = sageFrameSearchTitle;
            this.SageFrameSearchProcedureName = sageFrameSearchProcedureName;
            this.SageFrameSearchProcedureExecuteAs = sageFrameSearchProcedureExecuteAs;
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
        }
        #endregion

        #region "Public Properties"

        /// <summary>
        /// Gets or sets searching stored procedure ID.
        /// </summary>
        public int SageFrameSearchProcedureID
        {
            get { return _sageFrameSearchProcedureID; }
            set { _sageFrameSearchProcedureID = value; }
        }


        /// <summary>
        /// Gets or sets searching title.
        /// </summary>
        public string SageFrameSearchTitle
        {
            get { return _sageFrameSearchTitle; }
            set { _sageFrameSearchTitle = value; }
        }


        /// <summary>
        /// Gets or sets stored procedure name.
        /// </summary>
        public string SageFrameSearchProcedureName
        {
            get { return _sageFrameSearchProcedureName; }
            set { _sageFrameSearchProcedureName = value; }
        }


        /// <summary>
        /// Gets or sets stored procedure executes as.
        /// </summary>
        public string SageFrameSearchProcedureExecuteAs
        {
            get { return _sageFrameSearchProcedureExecuteAs; }
            set { _sageFrameSearchProcedureExecuteAs = value; }
        }

        /// <summary>
        /// Returns or retains true if stored procedure is active.
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        /// <summary>
        /// Returns or retains true if stored procedure is deleted.
        /// </summary>
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }

        /// <summary>
        /// Returns or retains true if stored procedure is modified.
        /// </summary>
        public bool IsModified
        {
            get { return _isModified; }
            set { _isModified = value; }
        }

        /// <summary>
        /// Gets or sets store procedure added date.
        /// </summary>
        public DateTime AddedOn
        {
            get { return _addedOn; }
            set { _addedOn = value; }
        }

        /// <summary>
        /// Gets or sets store procedure updated date.
        /// </summary>
        public DateTime UpdatedOn
        {
            get { return _updatedOn; }
            set { _updatedOn = value; }
        }

        /// <summary>
        /// Gets or sets store procedure deleted date. 
        /// </summary>
        public DateTime DeletedOn
        {
            get { return _deletedOn; }
            set { _deletedOn = value; }
        }

        /// <summary>
        /// Gets or sets portal ID.
        /// </summary>
        public int PortalID
        {
            get { return _portalID; }
            set { _portalID = value; }
        }

        /// <summary>
        /// Gets or sets store procedure added user's name.
        /// </summary>
        public string AddedBy
        {
            get { return _addedBy; }
            set { _addedBy = value; }
        }

        /// <summary>
        /// Gets or sets store procedure updated user's name.
        /// </summary>
        public string UpdatedBy
        {
            get { return _updatedBy; }
            set { _updatedBy = value; }
        }

        /// <summary>
        /// Gets or sets store procedure deleted user's name.
        /// </summary>
        public string DeletedBy
        {
            get { return _deletedBy; }
            set { _deletedBy = value; }
        }
        #endregion
    }
}
