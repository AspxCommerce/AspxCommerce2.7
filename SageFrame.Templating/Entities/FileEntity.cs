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

namespace SageFrame.Templating
{
    /// <summary>
    /// This class holds the properties of FileEntity.
    /// </summary>
    public class FileEntity
    {
        /// <summary>
        /// Get or set FileID.
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// Get or set file name.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Get or set file path.
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// Get or set file extension.
        /// </summary>
        public string FileExtension { get; set; }
        /// <summary>
        /// Get or set true for folder.
        /// </summary>
        public bool IsFolder { get; set; }
        /// <summary>
        /// Get or set file size.
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// Get or set file created date.
        /// </summary>
        public string CreatedDate { get; set; }
        /// <summary>
        /// Initializes a new instance of the FileEntity class.
        /// </summary>
        public FileEntity() { }
        /// <summary>
        /// Initializes a new instance of the FileEntity class.
        /// </summary>
        /// <param name="_FileName">File name.</param>
        /// <param name="_FilePath">File path.</param>
        /// <param name="_FileExtension">File extension.</param>
        /// <param name="_IsFolder">True for folder.</param>
        /// <param name="_Size">File size.</param>
        /// <param name="_CreatedDate">File created date.</param>
        public FileEntity(string _FileName, string _FilePath, string _FileExtension, bool _IsFolder, long _Size,string _CreatedDate)
        {
            this.FileName = _FileName;
            this.FilePath = _FilePath;
            this.FileExtension = _FileExtension;
            this.IsFolder = _IsFolder;
            this.Size = _Size;
            this.CreatedDate = _CreatedDate;
        }
    }
}
