/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace SageFrame.Dashboard
{
    /// <summary>
    /// This class holds the properties for Sidebar
    /// </summary>
    public class Sidebar
    {
        /// <summary>
        /// Gets or sets SidebarItemID
        /// </summary>
        public int SidebarItemID { get; set; }
        /// <summary>
        /// Gets or sets DisplayName
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Gets or sets ImagePath
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// Gets or sets URL
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// Gets or sets Depth
        /// </summary>
        public int Depth { get; set; }
        /// <summary>
        /// Gets or sets DisplayOrder
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// Gets or sets IsActive
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets ParentID
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// Gets or sets ChildCount
        /// </summary>
        public int ChildCount { get; set; }
        /// <summary>
        /// Gets or sets PageID
        /// </summary>
        public int PageID { get; set; }       
        /// <summary>
        /// Initializes a new instance of the Sidebar class.
        /// </summary>
        public Sidebar() { }
    }
    /// <summary>
    /// This class holds the properties for DisplayOrder
    /// </summary>
    public class DisplayOrder
    {  
        /// <summary>
        /// Gets or sets Id
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Gets or sets Order
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// Initializes a new instance of the DisplayOrder class.
        /// </summary>
        public DisplayOrder() { }
    }
}
