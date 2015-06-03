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
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using SageFrame.Web;
#endregion

namespace SageFrame.FileManager
{

    #region "Declaration"
    public enum StorageLocation
    {
        STANDARD = 0,
        SECURED_FILE_SYSTEM = 1,
        SECURED_DATABASE_SYSTEM = 2
    }
    #endregion

    public class FileManagerHelper
    {
        /// <summary>
        /// Returns file type for given input fileExtension.
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns>File type</returns>
        public static string ReturnExtension(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".doc":
                    return "application/ms-word";
                case ".docx":
                    return "application/ms-word";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                //case ".zip":
                //    return "application/zip";
                case ".xls":
                case ".csv":
                    return "application/vnd.ms-excel";
                case ".gif":
                    return "image/gif";
                case ".png":
                    return "image/png";
                case ".jpg":
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".wav":
                    return "audio/wav";
                case ".mp3":
                    return "audio/mpeg3";
                case ".mpg":
                case "mpeg":
                    return "video/mpeg";
                case ".rtf":
                    return "application/rtf";
                case ".asp":
                    return "text/asp";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".ppt":
                    return "application/mspowerpoint";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".xml":
                case ".sdxl":
                    return "application/xml";
                case ".xdp":
                    return "application/vnd.adobe.xdp+xml";
                default:
                    return "application/octet-stream";
            }
        }
        /// <summary>
        /// Checks for the valid file extensions.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="ext">ext</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>True if fie extension is valid.</returns>
        public static bool CheckForValidExtensions(int UserModuleID, string ext, int PortalID)
        {
            string extension = "";
            SageFrameConfig config = new SageFrameConfig();
            extension = config.GetSettingsByKey(SageFrameSettingKeys.FileExtensions);
            string[] arrExt = extension.Split('#');
            if (arrExt.Contains(ext))
                return true;
            else return false;


        }
        /// <summary>
        /// Unzip files for given zip file and after unzip deletes zip file.
        /// </summary>
        /// <param name="zipPathAndFile">zipPathAndFile</param>
        /// <param name="outputFolder">outputFolder</param>
        /// <param name="ExtractedPath">ExtractedPath</param>
        /// <param name="password">password</param>
        /// <param name="deleteZipFile">deleteZipFile</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        public static void UnZipFiles(string zipPathAndFile, string outputFolder, ref string ExtractedPath, string password, bool deleteZipFile, int UserModuleID, int PortalID)
        {
            ZipInputStream s = new ZipInputStream(File.OpenRead(zipPathAndFile));
            if (password != null && password != String.Empty)
                s.Password = password;
            ZipEntry theEntry;
            string tmpEntry = String.Empty;
            theEntry = s.GetNextEntry();
            ExtractedPath = Path.GetDirectoryName(outputFolder + "\\" + theEntry.Name);

            while (theEntry != null)
            {
                string fileName = Path.GetFileName(theEntry.Name);
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }
                if (fileName != String.Empty)
                {
                    if (theEntry.Name.IndexOf(".ini") < 0)
                    {
                        string fullPath = outputFolder + "\\" + theEntry.Name;
                        fullPath = fullPath.Replace("\\ ", "\\");
                        string fullDirPath = Path.GetDirectoryName(fullPath);
                        if (!Directory.Exists(fullDirPath)) Directory.CreateDirectory(fullDirPath);
                        FileStream streamWriter = File.Create(fullPath);
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        streamWriter.Close();
                        streamWriter.Dispose();
                    }
                }
                theEntry = s.GetNextEntry();
            }
            s.Close();
            if (deleteZipFile)
            {
                File.Delete(zipPathAndFile);
            }
        }
        /// <summary>
        /// Removes resource extension.
        /// </summary>
        /// <param name="fileName">fileName</param>
        /// <returns>File without extension</returns>
        public static string RemoveResourceExtension(string fileName)
        {
            return (fileName.Substring(0, fileName.LastIndexOf('.')));

        }

        /// <summary>
        /// Function to get byte array from a file
        /// </summary>
        /// <param name="_FileName">File name to get byte array</param>
        /// <returns>Byte Array</returns>
        public static byte[] FileToByteArray(string _FileName)
        {
            byte[] _Buffer = null;

            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // attach filestream to binary reader
                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);

                // get total byte length of the file
                long _TotalBytes = new System.IO.FileInfo(_FileName).Length;

                // read entire file into buffer
                _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);

                // close file reader
                _FileStream.Close();
                _FileStream.Dispose();
                _BinaryReader.Close();
            }
            catch (Exception)
            {
                // Error

            }

            return _Buffer;
        }
        /// <summary>
        /// Set the file attributes.
        /// </summary>
        /// <param name="filePath">filePath</param>
        /// <param name="settingString">settingString</param>
        public static void SetFileAttributes(string filePath, string settingString)
        {
            if (settingString != "")
            {
                string[] attributes = settingString.Split('-');
                int count = 0;
                foreach (string attr in attributes)
                {
                    count++;
                }

                switch (count)
                {
                    case 0:
                        File.SetAttributes(filePath, FileAttributes.Normal);
                        break;
                    case 1:
                        File.SetAttributes(filePath, GetAttributeKey(attributes[0]));
                        break;
                    case 2:
                        File.SetAttributes(filePath, GetAttributeKey(attributes[0]) | GetAttributeKey(attributes[1]));
                        break;
                    case 3:
                        File.SetAttributes(filePath, GetAttributeKey(attributes[0]) | GetAttributeKey(attributes[1]) | GetAttributeKey(attributes[2]));
                        break;
                    case 4:
                        File.SetAttributes(filePath, GetAttributeKey(attributes[0]) | GetAttributeKey(attributes[1]) | GetAttributeKey(attributes[2]) | GetAttributeKey(attributes[3]));
                        break;

                }
                foreach (string att in attributes)
                {
                    FileAttributes fa = GetAttributeKey(att);

                }
            }
            else
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
            }

        }
        /// <summary>
        /// Obtains the file attributes.
        /// </summary>
        /// <param name="filePath">filePath</param>
        /// <returns>File attribute.</returns>
        public static string GetAttributeString(string filePath)
        {

            string strResult = "";
            try
            {
                // check whether a file is read only
                bool isReadOnly = ((File.GetAttributes(filePath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);
                if (isReadOnly) strResult += "R-";
                // check whether a file is hidden
                bool isHidden = ((File.GetAttributes(filePath) & FileAttributes.Hidden) == FileAttributes.Hidden);
                if (isHidden) strResult += "H-";
                // check whether a file has archive attribute
                bool isArchive = ((File.GetAttributes(filePath) & FileAttributes.Archive) == FileAttributes.Archive);
                if (isArchive) strResult += "A-";
                // check whether a file is system file
                bool isSystem = ((File.GetAttributes(filePath) & FileAttributes.System) == FileAttributes.System);
                if (isSystem) strResult += "S-";
                if (strResult != "")
                {
                    strResult = strResult.Substring(0, strResult.LastIndexOf('-'));
                }
            }
            catch (Exception)
            {

                throw;
            }

            return strResult;
        }
        /// <summary>
        /// Obtains valid extensions.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Valid Extension.</returns>
        public static string GetValidExtensions(int UserModuleID, int PortalID)
        {
            string extension = "";
            List<FileManagerSettingInfo> lstSettings = FileManagerController.GetFileManagerSettings(UserModuleID, PortalID);
            foreach (FileManagerSettingInfo obj in lstSettings)
            {
                switch (obj.SettingKey)
                {
                    case "FileManagerExtensions":
                        extension = obj.SettingValue;
                        break;
                }
            }
            return extension;
        }
        /// <summary>
        /// Obtain attribute key.
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>File attribute</returns>
        public static System.IO.FileAttributes GetAttributeKey(string key)
        {
            FileAttributes fa = new FileAttributes();
            switch (key)
            {
                case "A":
                    fa = FileAttributes.Archive;
                    break;
                case "R":
                    fa = FileAttributes.ReadOnly;
                    break;
                case "H":
                    fa = FileAttributes.Hidden;
                    break;
                case "S":
                    fa = FileAttributes.System;
                    break;

            }
            return fa;
        }
        /// <summary>
        /// Returns file without extension.
        /// </summary>
        /// <param name="filepath">filepath.</param>
        /// <returns> Returns file without extension.</returns>

        public static string GetFilePathWithoutExtension(string filepath)
        {
            return (filepath.Substring(0, filepath.LastIndexOf(".")));
        }

        /// <summary>
        /// Replace the backslash
        /// </summary>
        /// <param name="filepath">filepath</param>
        /// <returns>File path.</returns>
        public static string ReplaceBackSlash(string filepath)
        {
            if (filepath != null)
            {
                filepath = filepath.Replace("\\", "/");
            }
            return filepath;
        }

        /// <summary>
        /// Write the byte content to the file
        /// </summary>
        /// <param name="FilePath">FilePath</param>
        /// <param name="FileContent">FileContent</param>
        public static void WriteBinaryFile(string FilePath, byte[] FileContent)
        {
            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        bw.Write(FileContent);
                    }
                }
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Copy or Move the file to the fullToPath location
        /// </summary>
        /// <param name="filePath">filePath</param>
        /// <param name="fileId">fileId</param>
        /// <param name="toFolderId">toFolderId</param>
        /// <param name="toPath">toPath</param>
        /// <param name="action">action</param>
        /// <param name="mode">mode</param>
        /// <param name="fullFilePath">fullFilePath</param>
        /// <param name="fullFromPath">fullFromPath</param>
        /// <param name="fullToPath">fullToPath</param>
       
        public static void TransferFile(string filePath, string toPath, int action, int mode, string fullFilePath, string fullFromPath, string fullToPath)
        {

            switch (action)
            {
                case 1:

                    FileInfo fileCopy = new FileInfo(fullFilePath);
                    string fileNameCopy = fileCopy.Name;
                    if (!File.Exists(Path.Combine(fullToPath, fileNameCopy)))
                    {
                        try
                        {
                            fileCopy.CopyTo(Path.Combine(fullToPath, fileNameCopy));
                           
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }
                    break;
                case 2:
                    FileInfo fileMove = new FileInfo(fullFilePath);
                    string fileNameMove = fileMove.Name;
                    if (fileMove.Exists)
                    {
                        try
                        {
                            fileMove.MoveTo(fullToPath + fileNameMove);

                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }
                    break;

            }

        }

      
        /// <summary>
        /// Creates a html string to be rendered based upon the dynamic conditions
        /// </summary>
        /// <param name="IsZip">IsZip</param>
        /// <param name="IsImg">IsImg</param>
        /// <param name="StorageLocation">StorageLocation</param>
        /// <param name="ext">ext</param>
        /// <param name="urlPath">urlPath</param>
        /// <param name="absolutePath">absolutePath</param>
        /// <param name="downloadPath">downloadPath</param>
        /// <param name="checkId">checkId</param>
        /// <param name="folderId">folderId</param>
        /// <param name="file">file</param>
        /// <param name="sb">sb</param>
        /// <param name="permission">permission</param>
        /// <param name="dictImages">dictImages</param>
        public static void ConstructHTMLString(bool IsZip, bool IsImg, int StorageLocation, string ext, string urlPath, string absolutePath, string downloadPath, string checkId, int folderId, ATTFile file, ref StringBuilder sb, string permission, Dictionary<string, string> dictImages, int index)
        {
            switch (StorageLocation)
            {
                case 0:
                    string attStr = FileManagerHelper.GetAttributeString(absolutePath);
                    if (IsZip)
                    {
                        sb.Append(AddExtension(ext, index));
                        sb.Append(AddCheckBox(checkId));
                        sb.Append(AddDownloadLink(downloadPath, folderId, file, permission, ext));
                        sb.Append(AddInfoSpan(file, attStr, StorageLocation));
                        sb.Append(AddExtractButton(Path.Combine(file.Folder, file.FileName), dictImages["Extract"].ToString(), permission));
                        sb.Append(AddEditButton(Path.Combine(file.Folder, file.FileName), dictImages["Edit"].ToString(), permission));
                        sb.Append(AddDeleteButton(Path.Combine(file.Folder, file.FileName), dictImages["Delete"].ToString(), permission));


                        sb.Append("</tr>");
                    }
                    else
                    {
                        if (IsImg)
                        {
                            string imgpath = urlPath + file.Folder;
                            imgpath = Path.Combine(imgpath, file.FileName);
                            sb.Append(AddExtension(ext, index));
                            sb.Append(AddCheckBox(checkId));
                            //sb.Append(AddDownloadLink(downloadPath, folderId, file, permission, ext));
                            sb.Append(AddPopupLink(downloadPath, folderId, file, permission, ext));
                            sb.Append(AddInfoSpan(file, attStr, StorageLocation));
                            sb.Append(AddPreviewButton(urlPath, dictImages["Preview"].ToString()));
                            sb.Append(AddEditButton(Path.Combine(file.Folder, file.FileName), dictImages["Edit"].ToString(), permission));
                            sb.Append(AddDeleteButton(Path.Combine(file.Folder, file.FileName), dictImages["Delete"].ToString(), permission));
                            sb.Append("</tr>");
                        }
                        else
                        {
                            sb.Append(AddExtension(ext, index));
                            sb.Append(AddCheckBox(checkId));
                            //sb.Append(AddDownloadLink(downloadPath, folderId, file, permission, ext));
                            sb.Append(AddPopupLink(downloadPath, folderId, file, permission, ext));
                            sb.Append(AddInfoSpan(file, attStr, StorageLocation));
                            sb.Append(AddBlankSpan());
                            sb.Append(AddEditButton(Path.Combine(file.Folder, file.FileName), dictImages["Edit"].ToString(), permission));

                            sb.Append(AddDeleteButton(Path.Combine(file.Folder, file.FileName), dictImages["Delete"].ToString(), permission));

                            sb.Append("</tr>");
                        }
                    }
                    break;
                case 1:
                    string attStr1 = FileManagerHelper.GetAttributeString(absolutePath + ".resources");
                    sb.Append(AddExtension(ext, index));
                    sb.Append(AddCheckBox(checkId));
                    sb.Append(AddDownloadLink(downloadPath, folderId, file, permission, ext));
                    sb.Append(AddInfoSpan(file, attStr1, StorageLocation));
                    sb.Append(AddDeleteButton(Path.Combine(file.Folder, file.FileName), dictImages["Delete"].ToString(), permission));

                    sb.Append(AddBlankSpan());
                    sb.Append(AddEditButton(Path.Combine(file.Folder, file.FileName), dictImages["Edit"].ToString(), permission));
                    sb.Append("</tr>");
                    break;
                case 2:
                    sb.Append(AddExtension(ext, index));
                    sb.Append(AddCheckBox(checkId));
                    sb.Append(AddDownloadLink(downloadPath, folderId, file, permission, ext));
                    sb.Append(AddInfoSpan(file, "", StorageLocation));
                    sb.Append(AddBlankSpan());
                    sb.Append(AddDeleteButton(Path.Combine(file.Folder, file.FileName), dictImages["Delete"].ToString(), permission));

                    sb.Append(AddEditButton(Path.Combine(file.Folder, file.FileName), dictImages["Edit"].ToString(), permission));
                    sb.Append("</li>");
                    break;
            }
        }

        #region HTMLConstructerHelper
        /// <summary>
        /// Return table row for desire class.
        /// </summary>
        /// <param name="extension">extension</param>
        /// <param name="index">index</param>
        /// <returns>Return table row for desire class.</returns>
        public static string AddExtension(string extension, int index)
        {
            extension = string.Format("{0}", index % 2 == 0 ? "sfEven" : "sfOdd");
            return ("<tr class=\"" + extension + "\">");
        }
        /// <summary>
        /// Return table column for desire class.
        /// </summary>
        /// <param name="checkId">checkId</param>
        /// <returns>Return table column for desire class.</returns>
        public static string AddCheckBox(string checkId)
        {
            return ("<td><span class=\"check\"><input type=\"checkbox\" id=" + checkId + "></span></td>");
        }
        /// <summary>
        /// Return table column for desire class.
        /// </summary>
        /// <param name="filePath">filePath</param>
        /// <param name="folderId">folderId</param>
        /// <param name="file">file</param>
        /// <param name="permission">permission</param>
        /// <param name="extension">extension</param>
        /// <returns>Return table column for desire class.</returns>
        public static string AddDownloadLink(string filePath, int folderId, ATTFile file, string permission, string extension)
        {
            extension = string.Format("ext_{0}", extension);
            string name = file.FileName;
            if (file.FileName.Length > 40)
            {
                name = file.FileName.Substring(0, 40) + "...";
            }
            if (permission == "edit" || permission == "view")
                return ("<td class='" + extension + "' width='30%'><a class=\"download_link\" href=\"" + filePath + "FileName=" + file.FileName + "&FolderName=" + file.Folder + "\" rel=" + file.FileName + ">" + name + "</a></td>");
            else
                return ("<td class='" + extension + "' width='30%'><a class=\"download_link\" href=\"#\" rel=" + file.FileName + ">" + file.FileName + "</a></td>");

        }
        /// <summary>
        /// Return table column for desire class.
        /// </summary>
        /// <param name="filePath">filePath</param>
        /// <param name="folderId">folderId</param>
        /// <param name="file">file</param>
        /// <param name="permission">permission</param>
        /// <param name="extension">extension</param>
        /// <returns>Return table column for desire class.</returns>
        public static string AddPopupLink(string filePath, int folderId, ATTFile file, string permission, string extension)
        {
            extension = string.Format("icon-{0}", extension);
            string name = file.FileName;
            if (file.FileName.Length > 40)
            {
                name = file.FileName.Substring(0, 40) + "...";
            }
            //if (permission == "edit" || permission == "view")
            //    return ("<td class='" + extension + "' width='30%'><a class=\"download_link\" href=\"" + filePath + "FileName=" + file.FileName + "&FolderName=" + file.Folder + "\" rel=" + file.FileName + ">" + name + "</a></td>");
            //else
            //    return ("<td class='" + extension + "' width='30%'><a class=\"download_link\" href=\"#\" rel=" + file.FileName + ">" + file.FileName + "</a></td>");
            // return ("<td class='" + extension + "' width='30%'><a class=\"download_link\" href=\"" + filePath + "FileName=" + file.FileName + "&FolderName=" + file.Folder + "\" rel=" + file.FileName + ">" + name + "</a></td>");

            return ("<td class='" + extension + "' width='30%'><a href=# class=\"download_link\"><span   value=\"" + filePath + "FileName=" + file.FileName + "&FolderName=" + file.Folder + "\"  id=\"spnEditImage\">" + name + "</a></span></td>");


        }
        /// <summary>
        /// Return table column for desire class.
        /// </summary>
        /// <param name="file">file</param>
        /// <param name="attributeString">attributeString</param>
        /// <param name="mode">mode</param>
        /// <returns>Return table column for desire class.</returns>

        public static string AddInfoSpan(ATTFile file, string attributeString, int mode)
        {
            string html = "";

            switch (mode)
            {
                case 2:
                    html = "<td><span class=\"info\">" + file.AddedOn + "||" + file.Size + "bytes" + "</span></span></td>";
                    break;
                default:
                    html = "<td><span class=\"info\">" + file.AddedOn + "||" + file.Size + "bytes" + "||<span class=\"attr\">" + attributeString + "</span></span></td>";
                    break;
            }
            return html;
        }
        /// <summary>
        /// Return table column for desire class.
        /// </summary>
        /// <param name="filePath">filePath</param>
        /// <param name="image">image</param>
        /// <param name="permission">permission</param>
        /// <returns>Return table column for desire class.</returns>
        public static string AddDeleteButton(string filePath, string image, string permission)
        {
            if (permission == "edit")
                return ("<td><a class=\"delete icon-delete\" href=\"#\" rel=\"" + filePath + "\"></a></td>");
            else
                return ("");

        }
        /// <summary>
        /// Return table column for desire class.
        /// </summary>
        /// <param name="filePath">filePath</param>
        /// <param name="image">image</param>
        /// <param name="permission">permission</param>
        /// <returns>Return table column for desire class.</returns>
        public static string AddEditButton(string filePath, string image, string permission)
        {
            if (permission == "edit")
                return ("<td><span class=\"edit\"><a class=\"edit icon-edit\" href=\"#\" rel=\"" + filePath + "\"></a></span></td>");
            else
                return ("");
        }
        /// <summary>
        /// Return table column for desire class.
        /// </summary>
        /// <param name="filePath">filePath</param>
        /// <param name="image">image</param>
        /// <returns>Return table column for desire class.</returns>
        public static string AddPreviewButton(string filePath, string image)
        {

            return ("<td><span class=\"preview\"><a class=\"preview icon-preview\" href=\"" + filePath + "\" title=\"Preview\" rel=\"" + filePath + "\"></a></span></td>");

        }
        /// <summary>
        /// Return table column for desire class.
        /// </summary>
        /// <param name="filePath">filePath</param>
        /// <param name="image">image</param>
        /// <param name="permission">permission</param>
        /// <returns>Return table column for desire class.</returns>
        public static string AddExtractButton(string filePath, string image, string permission)
        {
            if (permission == "edit")
                return ("<td><span class=\"decompress\"><a class=\"decompress icon-extract\" title=\"Extract\" href=\"#\" rel=\"" + filePath + "\"></a></span></td>");
            else
                return ("<td><span class=\"decompress\"></span></td>");
        }
        /// <summary>
        /// Return table column for desire class.
        /// </summary>
        /// <returns>Return table column for desire class.</returns>
        public static string AddBlankSpan()
        {
            return ("<td><span class=\"decompress\"></span></td>");
        }
        #endregion

    }



}
