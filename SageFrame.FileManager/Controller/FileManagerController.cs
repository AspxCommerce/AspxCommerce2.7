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
    /// Business logic class for FileManager.
    /// </summary>
    public class FileManagerController
    {
        /// <summary>
        /// Adds folder for given object of Folder class.
        /// </summary>
        /// <param name="folder">Object of Folder class. </param>
        public static void AddFolder(Folder folder)
        {
            try
            {
                FileMangerDataProvider.AddFolder(folder);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Adds root folder for given object of Folder class.
        /// </summary>
        /// <param name="folder">Object of Folder class.</param>
        public static void AddRootFolder(Folder folder)
        {
            try
            {
                FileMangerDataProvider.AddRootFolder(folder);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Inserts folder and returns its ID.
        /// </summary>
        /// <param name="folder">Object of Folder class.</param>
        /// <returns>FolderID if the folder is inserted succesfully</returns>
        public static int AddFolderReturnFolderID(Folder folder)
        {
            try
            {
                return (FileMangerDataProvider.AddFolderReturnFolderID(folder));
            }
            catch (Exception)
            {

                throw;

            }

        }
        /// <summary>
        /// Enables root folder.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        /// <param name="IsEnabled">IsEnabled</param>
        public static void EnableRootFolder(int FolderID, bool IsEnabled)
        {
            try
            {
                FileMangerDataProvider.EnableRootFolder(FolderID, IsEnabled);
            }
            catch (Exception)
            {

                throw;

            }
        }
        /// <summary>
        /// Obtains FolderId,FolderPath and StorageLocation.
        /// </summary>
        /// <returns>List of Folder class containing FolderId,FolderPath and StorageLocation.</returns>
        public static List<Folder> GetFolders()
        {

            return (FileMangerDataProvider.GetFolders());

        }
        /// <summary>
        /// Adds file for given object of ATTFile class.
        /// </summary>
        /// <param name="file">Object of ATTFile class.</param>
        public static void AddFile(ATTFile file)
        {
            try
            {
                FileMangerDataProvider.AddFile(file);
            }
            catch (Exception)
            {
                throw;

            }
        }
        /// <summary>
        /// Obtains files for given FolderID.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        /// <returns>List of ATTFile class containing file details.</returns>
        public static List<ATTFile> GetFiles(int FolderID)
        {
            try
            {
                return (FileMangerDataProvider.GetFiles(FolderID));
            }
            catch (Exception)
            {

                throw;

            }
        }
        /// <summary>
        /// searchs file for given SearchQuery.
        /// </summary>
        /// <param name="SearchQuery">SearchQuery</param>
        /// <returns>List of ATTFile class containing file details.</returns>
        public static List<ATTFile> SearchFiles(string SearchQuery)
        {
            try
            {
                return (FileMangerDataProvider.SearchFiles(SearchQuery));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Deletes from file or folder.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        /// <param name="FileID">FileID</param>
        public static void DeleteFileFolder(int FolderID, int FileID)
        {
            try
            {
                FileMangerDataProvider.DeleteFileFolder(FolderID, FileID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Deletes root folder.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        public static void DeleteRootFolder(int FolderID)
        {
            try
            {
                FileMangerDataProvider.DeleteRootFolder(FolderID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Updates file.
        /// </summary>
        /// <param name="FileID">FileID</param>
        /// <param name="fileName">fileName</param>
        public static void UpdateFile(int FileID, string fileName)
        {
            try
            {
                FileMangerDataProvider.UpdateFile(FileID, fileName);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Copies file for given FileID,FolderID,UniqueID and VersionGuid.
        /// </summary>
        /// <param name="FileID">FileID</param>
        /// <param name="FolderID">FolderID</param>
        /// <param name="Folder">Folder</param>
        /// <param name="UniqueID">UniqueID</param>
        /// <param name="VersionGuid">VersionGuid</param>
        public static void CopyFile(int FileID, int FolderID, string Folder, Guid UniqueID, Guid VersionGuid)
        {
            try
            {
                FileMangerDataProvider.CopyFile(FileID, FolderID, Folder, UniqueID, VersionGuid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Moves file for given FileID,FolderID,Folder,UniqueID and VersionGuid
        /// </summary>
        /// <param name="FileID">FileID</param>
        /// <param name="FolderID">FolderID</param>
        /// <param name="Folder">Folder</param>
        /// <param name="UniqueID">UniqueID</param>
        /// <param name="VersionGuid">VersionGuid</param>
        public static void MoveFile(int FileID, int FolderID, string Folder, Guid UniqueID, Guid VersionGuid)
        {
            try
            {
                FileMangerDataProvider.MoveFile(FileID, FolderID, Folder, UniqueID, VersionGuid);
            }
            catch (Exception)
            {

                throw;

            }
        }
        /// <summary>
        /// Sets folder permission for given FolderID,PermissionKey,UserID,RoleID,IsActive and AddedBy.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        /// <param name="PermissionKey">PermissionKey</param>
        /// <param name="UserID">UserID</param>
        /// <param name="RoleID">RoleID</param>
        /// <param name="IsActive">IsActive</param>
        /// <param name="AddedBy">AddedBy</param>
        public static void SetFolderPermission(int FolderID, string PermissionKey, int UserID, Guid RoleID, int IsActive, string AddedBy)
        {
            try
            {
                FileMangerDataProvider.SetFolderPermission(FolderID, PermissionKey, UserID, RoleID, IsActive, AddedBy);
            }
            catch (Exception)
            {

                throw;

            }
        }
        /// <summary>
        /// Deletes existing permissions for given FolderID.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        public static void DeleteExistingPermissions(int FolderID)
        {
            try
            {
                FileMangerDataProvider.DeleteExistingPermissions(FolderID);
            }
            catch (Exception)
            {

                throw;

            }
        }
        /// <summary>
        /// Obtains folder permission for given FolderID.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        /// <returns>List of FolderPermission class containing folder details.</returns>
        public static List<FolderPermission> GetFolderPermissions(int FolderID)
        {
            try
            {
                return (FileMangerDataProvider.GetFolderPermissions(FolderID));
            }
            catch (Exception)
            {

                throw;

            }
        }
        /// <summary>
        /// Obtains user id for given userName.
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns>UserId</returns>
        public static int GetUserID(string userName)
        {
            try
            {
                return (FileMangerDataProvider.GetUserID(userName));
            }
            catch (Exception)
            {

                throw;

            }
        }
        /// <summary>
        /// Obtains user list for folder for given FolderID.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        /// <returns>List of FolderPermission class containing UserID and UserName.</returns>
        public static List<FolderPermission> GetUserListForFolder(int FolderID)
        {
            try
            {
                return (FileMangerDataProvider.GetUserListForFolder(FolderID));
            }
            catch (Exception)
            {

                throw;

            }
        }
        /// <summary>
        /// Obtains permission keys for given FolderID,UserID,UserModuleID and UserName.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        /// <param name="UserID">UserID</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="UserName">UserName</param>
        /// <returns>List of folder permission key.</returns>
        public static List<string> GetPermissionKeys(int FolderID, int UserID, int UserModuleID, string UserName)
        {
            try
            {
                return (FileMangerDataProvider.GetPermissionKeys(FolderID, UserID, UserModuleID, UserName));
            }
            catch (Exception)
            {

                throw;

            }
        }
        /// <summary>
        /// Obtains file details for given FileID.
        /// </summary>
        /// <param name="FileID">FileID</param>
        /// <returns>List of ATTFile class containing file details.</returns>
        public static ATTFile GetFileDetails(int FileID)
        {
            try
            {
                return (FileMangerDataProvider.GetFileDetails(FileID));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtains root folders.
        /// </summary>
        /// <returns>List of Folder class containing FolderId,FolderPath,StorageLocation and IsActive.</returns>
        public static List<Folder> GetRootFolders()
        {
            try
            {
                return (FileMangerDataProvider.GetRootFolders());
            }
            catch (Exception)
            {

                throw;

            }
        }
        /// <summary>
        /// Obtains active root folders.
        /// </summary>
        /// <returns>List of Folder class containing FolderId,FolderPath,StorageLocation and IsActive.</returns>
        public static List<Folder> GetActiveRootFolders()
        {
            try
            {
                return (FileMangerDataProvider.GetActiveRootFolders());
            }
            catch (Exception)
            {

                throw;

            }
        }
        /// <summary>
        /// Obtains all folders.
        /// </summary>
        /// <returns>List of Folder class containing FolderId,FolderPath and StorageLocation</returns>
        public static List<Folder> GetAllFolders()
        {
            try
            {
                return (FileMangerDataProvider.GetAllFolders());
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtains module permission.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="UserName">UserName</param>
        /// <returns>List of file manager module permission key.</returns>
        public static List<string> GetModulePermission(int UserModuleID, string UserName)
        {
            try
            {
                return (FileMangerDataProvider.GetModulePermission(UserModuleID, UserName));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Obtains file manager settings for given UserModuleID and PortalID.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>List of FileManagerSettingInfo class containing file manager settings.</returns>
        public static List<FileManagerSettingInfo> GetFileManagerSettings(int UserModuleID, int PortalID)
        {
            try
            {
                return (FileMangerDataProvider.GetFileManagerSettings(UserModuleID, PortalID));
            }
            catch (Exception)
            {

                throw;

            }
        }
        /// <summary>
        /// Adds or updates file manager setting value.
        /// </summary>
        /// <param name="lstSettings">List of object of FileManagerSettingInfo class. </param>
        public static void AddUpdateSettings(List<FileManagerSettingInfo> lstSettings)
        {

            try
            {
                FileMangerDataProvider.AddUpdateSettings(lstSettings);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
