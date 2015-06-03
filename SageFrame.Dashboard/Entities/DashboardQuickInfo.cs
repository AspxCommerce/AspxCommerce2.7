/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace SageFrame.Dashboard
{
    /// <summary>
    /// This class holds the properties for DashboardQuickInfo
    /// </summary>
    public class DashboardQuickInfo
    {   
        /// <summary>
        /// Gets or sets list of QuickLink objects
        /// </summary>
        public List<QuickLink> LSTQuickLinks { get; set; }
        /// <summary>
        ///  Gets or sets list of Sidebar objects
        /// </summary>
        public List<Sidebar> LSTSidebar { get; set; }
        /// <summary>
        /// Gets or sets BreadCrumb
        /// </summary>
        public string BreadCrumb { get; set; }
        /// <summary>
        /// Initializes a new instance of the DashboardQuickInfo class.
        /// </summary>
        public DashboardQuickInfo() { }
    }
}
