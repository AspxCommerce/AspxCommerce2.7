/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace SageFrame.Dashboard
{
    /// <summary>
    /// This class holds the properties for QuickLink
    /// </summary>
    public class QuickLink
    {
        /// <summary>
        /// Gets or sets QuickLinkID
        /// </summary>
        public int QuickLinkID { get; set; }
        /// <summary>
        /// Gets or sets DisplayName
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Gets or sets URL
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// Gets or sets ImagePath
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// Gets or sets DisplayOrder
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// Gets or sets PageID
        /// </summary>
        public int PageID { get; set; }
        /// <summary>
        /// Gets or sets boolean value to check isactive.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Initializes a new instance of the QuickLink class.
        /// </summary>
        public QuickLink() { }
    }
}
