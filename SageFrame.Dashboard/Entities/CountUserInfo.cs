/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace SageFrame.Dashboard
{ 
    /// <summary>
    ///  This class holds the properties for CountUserInfo
    /// </summary>
    public class CountUserInfo
    {
        /// <summary>
        /// Gets or sets AnonymousUser
        /// </summary>
        public int AnonymousUser { get; set; }
        /// <summary>
        /// Gets or sets LoginUser
        /// </summary>
        public int LoginUser { get; set; }
        /// <summary>
        /// Gets or sets PageCount 
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// Gets or sets UserCount
        /// </summary>
        public int UserCount { get; set; }
        /// <summary>
        /// Initializes a new instance of the CountUserInfo class.
        /// </summary>
        public CountUserInfo() { }
    }
}
