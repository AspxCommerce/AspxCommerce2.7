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

namespace SageFrame.FileManager
{
    /// <summary>
    /// This class holds the properties for Folder class.
    /// </summary>
    public class Folder
    {
        /// <summary>
        /// Gets or sets FolderId.
        /// </summary>
        public int FolderId { get; set; }
        /// <summary>
        /// Gets or sets ParentID.
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// Gets or sets PortalId.
        /// </summary>
        public int PortalId { get; set; }
        /// <summary>
        /// Gets or sets FolderPath.
        /// </summary>
        public string FolderPath { get; set; }
        /// <summary>
        /// Gets or sets StorageLocation.
        /// </summary>
        public int StorageLocation { get; set; }
        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public Guid UniqueId { get; set; }
        /// <summary>
        /// Gets or sets VersionGuid.
        /// </summary>
        public Guid VersionGuid { get; set; }
        /// <summary>
        /// Gets or sets IsActive.
        /// </summary>
        public int IsActive { get; set; }
        /// <summary>
        /// Gets or sets IsEnabled.
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// Gets or sets AddedBy.
        /// </summary>
        public string AddedBy { get; set; }
        /// <summary>
        /// Gets or sets IsRoot.
        /// </summary>
        public bool IsRoot { get; set; }
        /// <summary>
        /// Initializes a new instance of the Folder class.
        /// </summary>
        public Folder() { }
    }
}
