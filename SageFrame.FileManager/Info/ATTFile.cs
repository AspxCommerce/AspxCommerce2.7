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
    /// This class holds the properties for ATTFile.
    /// </summary>
    public class ATTFile
    {
        /// <summary>
        /// Gets or sets FileId. 
        /// </summary>
        public int FileId { get; set; }
        /// <summary>
        /// Gets or sets PortalId.
        /// </summary>
        public int PortalId { get; set; }
        /// <summary>
        /// Gets or sets FileName.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Gets or sets Extension.
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// Gets or sets Size.
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public Guid UniqueId { get; set; }
        /// <summary>
        /// Gets or sets VersionGuid.
        /// </summary>
        public Guid VersionGuid { get; set; }
        /// <summary>
        /// Gets or sets ContentType.
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// Gets or sets Folder.
        /// </summary>
        public string Folder { get; set; }
        /// <summary>
        /// Gets or sets FolderId.
        /// </summary>
        public int FolderId { get; set; }
        /// <summary>
        /// Gets or sets StorageLocation.
        /// </summary>
        public int StorageLocation { get; set; }
        /// <summary>
        /// Gets or sets IsActive.
        /// </summary>
        public int IsActive { get; set; }
        /// <summary>
        /// Gets or sets AddedBy.
        /// </summary>
        public string AddedBy { get; set; }
        /// <summary>
        /// Gets or sets AddedOn.
        /// </summary>
        public string AddedOn { get; set; }
        /// <summary>
        /// Gets or sets byte array of Content.
        /// </summary>
        public byte[] Content { get; set; }
        /// <summary>
        /// Initializes a new instance of the ATTFile class.
        /// </summary>
        /// <param name="fileName">fileName</param>
        /// <param name="folder">folder</param>
        /// <param name="folderID">folderID</param>
        /// <param name="addedBy">addedBy</param>
        /// <param name="extension">extension</param>
        /// <param name="portalId">portalId</param>
        /// <param name="uniqueId">uniqueId</param>
        /// <param name="versionId">versionId</param>
        /// <param name="size">size</param>
        /// <param name="contentType">contentType</param>
        /// <param name="isActive">isActive</param>
        /// <param name="storageLocation">storageLocation</param>
        public ATTFile(string fileName, string folder, int folderID, string addedBy, string extension, int portalId, Guid uniqueId, Guid versionId, int size, string contentType, int isActive, int storageLocation)
        {
            this.FileName = fileName;
            this.Folder = folder;
            this.FolderId = folderID;
            this.AddedBy = addedBy;
            this.Extension = extension;
            this.PortalId = portalId;
            this.UniqueId = uniqueId;
            this.VersionGuid = versionId;
            this.Size = size;
            this.ContentType = contentType;
            this.IsActive = isActive;
            this.StorageLocation = storageLocation;

        }
        /// <summary>
        /// Initializes a new instance of the ATTFile class.
        /// </summary>
        /// <param name="fileName">fileName</param>
        /// <param name="folder">folder</param>
        /// <param name="size">size</param>
        /// <param name="contentType">contentType</param>
        public ATTFile(string fileName, string folder, int size, string contentType)
        {
            this.FileName = fileName;
            this.Folder = folder;
            this.Size = size;
            this.ContentType = contentType;
        }
        /// <summary>
        /// Initializes a new instance of the ATTFile class.
        /// </summary>
        public ATTFile() { }

    }
   
}
