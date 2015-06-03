/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.BreadCrum
{  
    /// <summary>
    /// This class holds the properties for BreadCrum.
    /// </summary>
    public class BreadCrumInfo
    {   
        /// <summary>
        /// Gets or sets PageID.
        /// </summary>
        public int PageID { get; set; }
        /// <summary>
        /// Gets or sets PageName.
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// Gets or sets breadcrumb path.
        /// </summary>
        public string TabPath { get; set; }
        /// <summary>
        /// Gets or sets SEOName(pagename).
        /// </summary>
        public string SEOName { get; set; }
        /// <summary>
        /// Gets or sets culture.
        /// </summary>
        public string LocalPage { get; set; }
        /// <summary>
        /// Initializes a new instance of the BreadCrumInfo class.
        /// </summary>
        public BreadCrumInfo() { }

    }
}
