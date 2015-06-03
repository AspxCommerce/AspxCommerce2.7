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

namespace SageFrame.HTMLText
{
    /// <summary>
    /// This class holds the properties for HTML Text.
    /// </summary>
    public class HTMLContentInfo
    {
        /// <summary>
        /// Gets or sets HTML comment ID
        /// </summary>
        public int HTMLCommentID { get; set; }

        /// <summary>
        /// Gets or sets HTML comments.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Returns or retains true if the HTML comment is approved.
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Returns or retains true if the HTML comment is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Returns or retains true if the HTML comment is modified.
        /// </summary>
        public bool IsModified { get; set; }

        /// <summary>
        /// Gets or sets  HTML text added date.
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Gets or sets  HTML text updated date.
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets  HTML text deleted date.
        /// </summary>
        public DateTime DeletedOn { get; set; }

        /// <summary>
        /// Gets or sets  HTML text approved date.
        /// </summary>
        public DateTime ApprovedOn { get; set; }

        /// <summary>
        /// Sets or gets HTML text's current portalID.
        /// </summary>
        public int PortalID { get; set; }

        /// <summary>
        /// Gets or sets  HTML text added user's name.
        /// </summary>
        public string AddedBy { get; set; }

        /// <summary>
        /// Gets or sets  HTML text updated user's name.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets  HTML text deleted user's name.
        /// </summary>
        public string DeletedBy { get; set; }

        /// <summary>
        /// Gets or sets  HTML text approved user's name.
        /// </summary>
        public string ApprovedBy { get; set; }

        /// <summary>
        /// Gets or sets  HTML text's usermoduleID.
        /// </summary>
        private int UserModuleID { get; set; }


        /// <summary>
        /// Gets or sets  HTML text's CultureName.
        /// </summary>
        private string CultureName { get; set; }       

		
		
        #region "Private Members"
        int _htmlTextID;
        string _content;
        bool _isActive;
        bool _isAllowedToComment;
        #endregion

        #region "Public Properties"

        /// <summary>
        /// Gets or sets  HTMLTextID.
        /// </summary>
        public int HtmlTextID
        {
            get { return _htmlTextID; }
            set { _htmlTextID = value; }
        }

        /// <summary>
        /// Gets or sets  HTML text's Content.
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        /// <summary>
        /// Returns or retains true if the HTML comment is active.
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        /// <summary>
        /// Returns or retains true if the user is allod to comment.
        /// </summary>
        public bool IsAllowedToComment
        {
            get { return _isAllowedToComment; }
            set { _isAllowedToComment = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the HTMLContentInfo class.
        /// </summary>
        public HTMLContentInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the HTMLContentInfo class.
        /// </summary>
        /// <param name="htmlTextID">htmlTextID</param>
        /// <param name="content">content</param>
        /// <param name="isActive">isActive</param>
        /// <param name="isAllowedToComment">isAllowedToComment</param>
        public HTMLContentInfo(int htmlTextID, string content, bool isActive, bool isAllowedToComment)
        {
            this.HtmlTextID = htmlTextID;
            this.Content = content;
            this.IsActive = isActive;
            this.IsAllowedToComment = isAllowedToComment;
        } 
        #endregion

       
    }
}
