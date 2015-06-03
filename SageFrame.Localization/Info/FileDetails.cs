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

namespace SageFrame.Localization
{
    /// <summary>
    /// This class holds the properties for FileDetails.
    /// </summary>
    public class FileDetails
    {
        /// <summary>
        /// Gets or sets file name.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Gets or sets file path.
        /// </summary>
        public string FilePath { get; set; }
        //Gets or sets folder info.
        public string FolderInfo { get; set; }
        /// <summary>
        /// Gets or sets isexists.
        /// </summary>
        public bool IsExists { get; set; }
        /// <summary>
        /// Initializes a new instance of the FileDetails class.
        /// </summary>
        public FileDetails() { }
        /// <summary>
        /// Initializes a new instance of the FileDetails class.
        /// </summary>
        /// <param name="filename">filename</param>
        /// <param name="filepath">filepath</param>
        public FileDetails(string filename, string filepath)
        {
            this.FileName = filename;
            this.FilePath = filepath;
        }
        /// <summary>
        /// Initializes a new instance of the FileDetails class.
        /// </summary>
        /// <param name="filename">filename</param>
        /// <param name="filepath">filepath</param>
        /// <param name="folderinfo">folderinfo</param>
        public FileDetails(string filename, string filepath,string folderinfo)
        {
            this.FileName = filename;
            this.FilePath = filepath;
            this.FolderInfo = folderinfo;
        }
    }
}
