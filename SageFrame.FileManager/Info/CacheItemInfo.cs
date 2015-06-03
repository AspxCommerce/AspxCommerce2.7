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
using System.Web;
#endregion

namespace SageFrame.FileManager
{
    /// <summary>
    /// This class holds the properties for FileCacheInfo.
    /// </summary>
    public class FileCacheInfo
    {
        /// <summary>
        /// Gets or sets FolderID.
        /// </summary>
        public int FolderID { get; set; }
        /// <summary>
        /// Gets or sets list of ATTFile objects.
        /// </summary>
        public List<ATTFile> LSTFiles { get; set; }
        /// <summary>
        /// Initializes a new instance of the FileCacheInfo class.
        /// </summary>
        public FileCacheInfo() { }

    }
   
}
