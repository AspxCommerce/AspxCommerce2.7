/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace SageFrame.Dashboard
{
    /// <summary>
    /// This class holds the properties for Link
    /// </summary>
    public class Link
    {
        /// <summary>
        /// Gets or sets PageID
        /// </summary>
        public int PageID { get; set; }
        /// <summary>
        /// Gets or sets PageName
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// Gets or sets TabPath
        /// </summary>
        public string TabPath { get; set; }
        /// <summary>
        /// Initializes a new instance of the Link class.
        /// </summary>
        public Link() { }
    }
}
