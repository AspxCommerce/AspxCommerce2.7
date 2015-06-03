using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace SageFrame.FileManager
{
    
    public class SynchronizeFiles
    {
        #region "Declaration"
        public static string UserName { get; set; }
        public static int PortalID { get; set; }
        public static string absolutePath { get; set; }
        #endregion

        /// <summary>
        /// The list of folders in the database is fetched at once into this
        /// </summary>
        public static List<Folder> lstFolders { get; set; }
        
        /// <summary>
        /// Gets or sets file extensions.
        /// </summary>
        public static string extensions { get; set; }
        /// <summary>
        /// Gets or sets for valid extension.
        /// </summary>
        /// <param name="ext">ext</param>
        /// <returns>True if extension is valid.</returns>
        public static bool IsExtensionValid(string ext)
        {
            string[] arrExt = extensions.Split(',');
            if (arrExt.Contains(ext) || arrExt.Contains(ext.Substring(1,ext.Length-1)))
                return true;
            else return false;
        }

        /// <summary>
        /// Folder To Database synchronization
        /// </summary>
        public static void F2DSync()
        {
            List<Folder> lstRootFolders = FileManagerController.GetRootFolders();
            foreach(Folder root in lstRootFolders)
            {
                string dirPath = Path.Combine(absolutePath, root.FolderPath);
                DirectoryInfo dir=new DirectoryInfo(dirPath);
                RecurseThroughDirectory(dir,root.FolderId,root.StorageLocation);
            }
        }
        /// <summary>
        /// Database to Folder Synchronization
        /// </summary>
        public static void D2FSync()
        {
            ///Get List of Folders from database
            foreach(Folder folder in FileManagerController.GetAllFolders())
            {
                if(!Directory.Exists(Path.Combine(absolutePath,folder.FolderPath)))
                {
                    //delete the directory
                    FileManagerController.DeleteFileFolder(folder.FolderId,0);
                }
                else
                {
                    //get the file list with that folderId
                    List<ATTFile> lstFile = FileManagerController.GetFiles(folder.FolderId);
                    foreach (ATTFile file in lstFile)
                    {
                        if(file.StorageLocation==0)
                        {
                            if(!File.Exists(Path.Combine(Path.Combine(absolutePath,file.Folder),file.FileName)))
                            {
                                //delete the file from database
                                FileManagerController.DeleteFileFolder(0,file.FileId);
                            }
                        }
                        else if (file.StorageLocation == 1)
                        {
                            if (!File.Exists(Path.Combine(Path.Combine(absolutePath, file.Folder), file.FileName+".resources")))
                            {
                                //delete the file from database
                                FileManagerController.DeleteFileFolder(0, file.FileId);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        ///Search file recursively on folder.
        /// </summary>
        /// <param name="di">DirectoryInfo object.</param>
        /// <param name="folderId">folderId</param>
        /// <param name="folderType">folderType</param>
        public static void RecurseThroughDirectory(DirectoryInfo di,int folderId,int folderType)
        {
            FileInfo[] files = di.GetFiles();
            foreach (FileInfo fi in files)
            {
                List<ATTFile> lstFile = FileManagerController.GetFiles(folderId);
                bool exists = false;
                int size = int.Parse(fi.Length.ToString());
                if(Path.GetExtension(fi.Name)==".resources")
                {
                     exists= lstFile.Exists(
                        delegate(ATTFile obj)
                        {
                            return (obj.FileName == Path.GetFileNameWithoutExtension(fi.Name) && obj.FolderId == folderId);
                        }
                        );
                    
                }
                else
                {
                    exists = lstFile.Exists(
                    delegate(ATTFile obj)
                    {
                        return (obj.FileName == fi.Name && obj.FolderId == folderId);
                    }
                    );
                
                }
                if(!exists)
                {
                    //Add file to database
                    if (folderType == 1)
                    {
                        File.Move(fi.FullName,fi.FullName+".resources");
                        ATTFile fileObj = new ATTFile(fi.Name, FileManagerHelper.ReplaceBackSlash(GetFolderPath(di.FullName.Replace(absolutePath, ""))), folderId, UserName, Path.GetExtension(fi.Name.Replace(".resources", "")), PortalID, Guid.NewGuid(), Guid.NewGuid(),size, FileManagerHelper.ReturnExtension(Path.GetExtension(fi.Name.Replace(".resources", ""))), 1,folderType);
                        FileManagerController.AddFile(fileObj);
                    }
                    else
                    {
                        if (Path.HasExtension(fi.Name))
                        {
                            if (IsExtensionValid(Path.GetExtension(fi.Name)))
                            {
                                ATTFile fileObj = new ATTFile(fi.Name, FileManagerHelper.ReplaceBackSlash(GetFolderPath(di.FullName.Replace(absolutePath, ""))), folderId, UserName, Path.GetExtension(fi.Name), PortalID, Guid.NewGuid(), Guid.NewGuid(), int.Parse(fi.Length.ToString()), FileManagerHelper.ReturnExtension(Path.GetExtension(fi.Name)), 1, 0);
                                FileManagerController.AddFile(fileObj);
                            }
                        }
                    }
                }
            }
            foreach (DirectoryInfo di_child in di.GetDirectories())
            {
                int newFolderId = 0;
                int storageLocation = 0;
                int index = lstFolders.FindIndex(
                    delegate (Folder obj)
                        {
                            return (obj.FolderPath == GetFolderPath(di_child.FullName));
                        }
                    );
                if(index==-1)
                {
                    //Add folder to the database and get the folderId into newFolderId
                    Folder folder = new Folder();
                    folder.PortalId = PortalID;
                    folder.FolderPath = FileManagerHelper.ReplaceBackSlash(GetFolderPath(di_child.FullName));
                    folder.StorageLocation = storageLocation;
                    folder.UniqueId = Guid.NewGuid();
                    folder.VersionGuid = Guid.NewGuid();
                    folder.IsActive = 1;
                    folder.AddedBy = UserName;
                    folder.IsRoot = false;
                    newFolderId = FileManagerController.AddFolderReturnFolderID(folder);
                }
                else if(index>-1)
                {
                    newFolderId = lstFolders[index].FolderId;
                    storageLocation = lstFolders[index].StorageLocation;
                }
                RecurseThroughDirectory(di_child,newFolderId,storageLocation);
            }
            
        }
        /// <summary>
        /// Obtains folder path.
        /// </summary>
        /// <param name="folderPath">folderPath</param>
        /// <returns>Absolute path.</returns>
        public  static string GetFolderPath(string folderPath)
        {
            return(FileManagerHelper.ReplaceBackSlash(folderPath.Replace(absolutePath,"")));
            
        }
    }
}
