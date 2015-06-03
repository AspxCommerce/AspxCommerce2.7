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

namespace SageFrame.Pages
{  
    /// <summary>
    /// This class holds the properties of PageModuleInfo class.
    /// </summary>
   public class PageModuleInfo
    {
       /// <summary>
        /// Get or set module ID.
       /// </summary>
       public int UserModuleID { get; set; }
       /// <summary>
       /// Get or set Module title.
       /// </summary>
       public string UserModuleTitle { get; set; }
       /// <summary>
       /// Get or set pane name.
       /// </summary>
       public string PaneName { get; set; }
       /// <summary>
       /// Get or set total row.
       /// </summary>
       public int _RowTotal { get; set; }
       /// <summary>
       /// Initializes a new instance of the PageModuleInfo class.
       /// </summary>
       public PageModuleInfo() { }
       
    }
}
