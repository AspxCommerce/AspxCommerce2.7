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
using SageFrame.Web.Utilities;
using System.Data;
using System.Data.SqlClient;
using SageFrame.Web;
using System.Data.Common;
#endregion

namespace SageFrame.FileManager
{
    /// <summary>
    /// Manipulates data for FileManagerController class.
    /// </summary>
    public class FileMangerDataProvider
    {
        /// <summary>
        /// Connects to database and add folder for given object of Folder class.
        /// </summary>
        /// <param name="folder">Object of Folder class.</param>
        public static void AddFolder(Folder folder)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalId", folder.PortalId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ParentFolderID", folder.ParentID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FolderPath", folder.FolderPath));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StorageLocation", folder.StorageLocation));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UniqueId", folder.UniqueId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@VersionGuid", folder.VersionGuid));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", folder.IsActive));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", folder.AddedBy));
            SQLHandler sagesql = new SQLHandler();
            sagesql.ExecuteNonQuery("usp_FileManagerAddFolder", ParaMeterCollection);

        }
        /// <summary>
        /// Connects to database and add root folder for given object of Folder class.
        /// </summary>
        /// <param name="folder">Object of Folder class.</param>
        public static void AddRootFolder(Folder folder)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalId", folder.PortalId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FolderPath", folder.FolderPath));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StorageLocation", folder.StorageLocation));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UniqueId", folder.UniqueId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@VersionGuid", folder.VersionGuid));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", folder.IsActive));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", folder.AddedBy));
            SQLHandler sagesql = new SQLHandler();
            sagesql.ExecuteNonQuery("usp_FileManagerAddRootFolder", ParaMeterCollection);
        }

        /// <summary>
        /// Connects to database and inserts folder and returns its ID.
        /// </summary>
        /// <param name="folder">Object of Folder Class.</param>
        /// <returns>FolderID if the folder is inserted succesfully</returns>
        public static int AddFolderReturnFolderID(Folder folder)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalId", folder.PortalId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ParentFolderID", folder.ParentID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FolderPath", folder.FolderPath));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StorageLocation", folder.StorageLocation));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UniqueId", folder.UniqueId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@VersionGuid", folder.VersionGuid));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", folder.IsActive));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", folder.AddedBy));
            SQLHandler sagesql = new SQLHandler();
            int FolderID = sagesql.ExecuteNonQueryAsGivenType<int>("[usp_FileManagerAddFolderRetFolderID]", ParaMeterCollection, "@FolderID");

            return FolderID;

        }
        /// <summary>
        /// Connects to database and add file for given object of ATTFile class.
        /// </summary>
        /// <param name="file">Object of ATTFile class.</param>
        public static void AddFile(ATTFile file)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalId", file.PortalId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UniqueId", file.UniqueId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@VersionGuid", file.VersionGuid));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FileName", file.FileName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Extension", file.Extension));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Size", file.Size));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@ContentType", file.ContentType));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Folder", file.Folder));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FolderId", file.FolderId));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", file.IsActive));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", file.AddedBy));
            if (file.StorageLocation == 2)
            {
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Content", file.Content));
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("usp_FileManagerAddDatabaseFile", ParaMeterCollection);
            }
            else
            {
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("usp_FileManagerAddFile", ParaMeterCollection);

            }
        }
        /// <summary>
        /// Connects to database and obtains FolderId,FolderPath and StorageLocation.
        /// </summary>
        /// <returns>List of Folder class containing FolderId,FolderPath and StorageLocation.  </returns>
        public static List<Folder> GetFolders()
        {
            List<Folder> lstFolders = new List<Folder>();
            string StoredProcedureName = "usp_FileManagerGetFolders";
            SqlDataReader SQLReader = null;
            SQLHandler sagesql = new SQLHandler();
            try
            {

                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName);
                while (SQLReader.Read())
                {
                    Folder obj = new Folder();
                    obj.FolderId = int.Parse(SQLReader["FolderId"].ToString());
                    obj.FolderPath = SQLReader["FolderPath"].ToString();
                    obj.StorageLocation = int.Parse(SQLReader["StorageLocation"].ToString());
                    lstFolders.Add(obj);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (SQLReader != null)
                {
                    SQLReader.Close();
                }
            }


            return lstFolders;
        }
        /// <summary>
        /// Connnects to database and obtains root folders.
        /// </summary>
        /// <returns>List of Folder class containing FolderId,FolderPath,StorageLocation and IsActive.</returns>
        public static List<Folder> GetRootFolders()
        {
            List<Folder> lstFolders = new List<Folder>();
            string StoredProcedureName = "usp_FileManagerGetRootFolders";
            SqlDataReader SQLReader = null;
            try
            {

                SQLHandler sagesql = new SQLHandler();
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName);
                while (SQLReader.Read())
                {
                    Folder obj = new Folder();
                    obj.FolderId = int.Parse(SQLReader["FolderId"].ToString());
                    obj.FolderPath = SQLReader["FolderPath"].ToString();
                    obj.StorageLocation = int.Parse(SQLReader["StorageLocation"].ToString());
                    obj.IsEnabled = bool.Parse(SQLReader["IsActive"].ToString());
                    lstFolders.Add(obj);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (SQLReader != null)
                {
                    SQLReader.Close();
                }
            }
            return lstFolders;
        }
        /// <summary>
        /// Connects to database and obtains active root folders.
        /// </summary>
        /// <returns>List of Folder class containing FolderId,FolderPath,StorageLocation and IsActive.</returns>
        public static List<Folder> GetActiveRootFolders()
        {
            List<Folder> lstFolders = new List<Folder>();
            string StoredProcedureName = "usp_FileManagerGetActiveRootFolders";
            SqlDataReader SQLReader = null;
            try
            {

                SQLHandler sagesql = new SQLHandler();
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName);
                while (SQLReader.Read())
                {
                    Folder obj = new Folder();
                    obj.FolderId = int.Parse(SQLReader["FolderId"].ToString());
                    obj.FolderPath = SQLReader["FolderPath"].ToString();
                    obj.StorageLocation = int.Parse(SQLReader["StorageLocation"].ToString());
                    obj.IsEnabled = bool.Parse(SQLReader["IsActive"].ToString());
                    lstFolders.Add(obj);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (SQLReader != null)
                {
                    SQLReader.Close();
                }
            }

            return lstFolders;
        }
        /// <summary>
        /// Connects to database and obtains all folders.
        /// </summary>
        /// <returns>List of Folder class containing FolderId,FolderPath and StorageLocation.</returns>
        public static List<Folder> GetAllFolders()
        {
            List<Folder> lstFolders = new List<Folder>();
            string StoredProcedureName = "usp_FileManagerGetAllFolders";
            SqlDataReader SQLReader = null;
            try
            {

                SQLHandler sagesql = new SQLHandler();
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName);
                while (SQLReader.Read())
                {
                    Folder obj = new Folder();
                    obj.FolderId = int.Parse(SQLReader["FolderId"].ToString());
                    obj.FolderPath = SQLReader["FolderPath"].ToString();
                    obj.StorageLocation = int.Parse(SQLReader["StorageLocation"].ToString());
                    lstFolders.Add(obj);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (SQLReader != null)
                {
                    SQLReader.Close();
                }
            }

            return lstFolders;


        }
        /// <summary>
        /// Connects to database and get files for given FolderID.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        /// <returns>List of ATTFile class containing file details. </returns>
        public static List<ATTFile> GetFiles(int FolderID)
        {
            List<ATTFile> lstFiles = new List<ATTFile>();
            string StoredProcedureName = "usp_FileManagerGetFiles";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FolderID", FolderID));
            SqlDataReader SQLReader = null;
            try
            {
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName, ParaMeterCollection);
                while (SQLReader.Read())
                {
                    ATTFile obj = new ATTFile();
                    obj.FileId = int.Parse(SQLReader["FileId"].ToString());
                    obj.FolderId = int.Parse(SQLReader["FolderId"].ToString());
                    obj.FileName = SQLReader["FileName"].ToString();
                    obj.Folder = SQLReader["Folder"].ToString();
                    obj.Extension = SQLReader["Extension"].ToString();
                    obj.Size = int.Parse(SQLReader["Size"].ToString());
                    obj.AddedOn = SQLReader["AddedOn"].ToString();
                    obj.Content = SQLReader["Content"] == DBNull.Value ? null : (byte[])SQLReader["Content"];
                    obj.StorageLocation = int.Parse(SQLReader["StorageLocation"].ToString());

                    lstFiles.Add(obj);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (SQLReader != null)
                {
                    SQLReader.Close();
                }
            }
            return lstFiles;
        }
        /// <summary>
        /// Connects to database and searchs file for given SearchQuery.
        /// </summary>
        /// <param name="SearchQuery">SearchQuery</param>
        /// <returns>List of ATTFile class containing file details. </returns>
        public static List<ATTFile> SearchFiles(string SearchQuery)
        {
            List<ATTFile> lstFiles = new List<ATTFile>();
            string StoredProcedureName = "usp_FileManagerSearchFiles";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@SearchQuery", SearchQuery));
            SqlDataReader SQLReader = null;
            try
            {
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName, ParaMeterCollection);
                while (SQLReader.Read())
                {
                    ATTFile obj = new ATTFile();
                    obj.FileId = int.Parse(SQLReader["FileId"].ToString());
                    obj.FileName = SQLReader["FileName"].ToString();
                    obj.Folder = SQLReader["Folder"].ToString();
                    obj.Extension = SQLReader["Extension"].ToString();
                    obj.Size = int.Parse(SQLReader["Size"].ToString());
                    obj.AddedOn = SQLReader["AddedOn"].ToString();
                    obj.Content = SQLReader["Content"] == DBNull.Value ? null : (byte[])SQLReader["Content"];
                    obj.StorageLocation = int.Parse(SQLReader["StorageLocation"].ToString());

                    lstFiles.Add(obj);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (SQLReader != null)
                {
                    SQLReader.Close();
                }
            }
            return lstFiles;
        }
        /// <summary>
        /// Connects to database and deletes from file or folder.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        /// <param name="FileID">FileID</param>
        public static void DeleteFileFolder(int FolderID, int FileID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FolderID", FolderID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FileID", FileID));

            SQLHandler sagesql = new SQLHandler();
            sagesql.ExecuteNonQuery("usp_FileManagerDeleteFileFolder", ParaMeterCollection);
        }
        /// <summary>
        /// Connects to database and deletes root folder.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        public static void DeleteRootFolder(int FolderID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FolderID", FolderID));

            SQLHandler sagesql = new SQLHandler();
            sagesql.ExecuteNonQuery("usp_FileManagerDeleteRootFolder", ParaMeterCollection);
        }
        /// <summary>
        /// Connects to database and enables root folder.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        /// <param name="IsEnabled">IsEnabled</param>
        public static void EnableRootFolder(int FolderID, bool IsEnabled)
        {
            string sp = "usp_FileManagerDisableRootFolder";
            if (IsEnabled)
                sp = "usp_FileManagerEnableRootFolder";
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FolderID", FolderID));

            SQLHandler sagesql = new SQLHandler();
            sagesql.ExecuteNonQuery(sp, ParaMeterCollection);
        }
        /// <summary>
        /// Connects to database and obtains file details for given FileID.
        /// </summary>
        /// <param name="FileID">FileID</param>
        /// <returns>List of ATTFile class containing file details.</returns>
        public static ATTFile GetFileDetails(int FileID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FileID", FileID));

            SQLHandler sagesql = new SQLHandler();
            ATTFile obj = new ATTFile();
            obj = sagesql.ExecuteAsObject<ATTFile>("usp_FileManagerGetFileDetails", ParaMeterCollection);
            return obj;
        }
        /// <summary>
        /// Connects to database and updates file.
        /// </summary>
        /// <param name="FileID">FileID</param>
        /// <param name="fileName">FileName</param>
        public static void UpdateFile(int FileID, string fileName)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FileID", FileID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FileName", fileName));

            SQLHandler sagesql = new SQLHandler();
            sagesql.ExecuteNonQuery("usp_FileManagerUpdateFile", ParaMeterCollection);


        }
        /// <summary>
        /// Connects to database and copies file for given FileID,FolderID,UniqueID and VersionGuid.
        /// </summary>
        /// <param name="FileID">FileID</param>
        /// <param name="FolderID">FolderID</param>
        /// <param name="Folder">Folder</param>
        /// <param name="UniqueID">UniqueID</param>
        /// <param name="VersionGuid">VersionGuid</param>
        public static void CopyFile(int FileID, int FolderID, string Folder, Guid UniqueID, Guid VersionGuid)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FileID", FileID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FolderID", FolderID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Folder", Folder));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UniqueID", UniqueID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@VersionGuid", VersionGuid));

            SQLHandler sagesql = new SQLHandler();
            sagesql.ExecuteNonQuery("usp_FileManagerCopyFile", ParaMeterCollection);

        }
        /// <summary>
        /// Connects to database and moves file for given FileID,FolderID,Folder,UniqueID and VersionGuid.
        /// </summary>
        /// <param name="FileID">FileID</param>
        /// <param name="FolderID">FolderID</param>
        /// <param name="Folder">Folder</param>
        /// <param name="UniqueID">UniqueID</param>
        /// <param name="VersionGuid">VersionGuid</param>
        public static void MoveFile(int FileID, int FolderID, string Folder, Guid UniqueID, Guid VersionGuid)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FileID", FileID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FolderID", FolderID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Folder", Folder));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UniqueID", UniqueID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@VersionGuid", VersionGuid));

            SQLHandler sagesql = new SQLHandler();
            sagesql.ExecuteNonQuery("usp_FileManagerMoveFile", ParaMeterCollection);
        }
        /// <summary>
        /// Connects to database and sets folder permission for given FolderID,PermissionKey,UserID,RoleID,IsActive and AddedBy.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        /// <param name="PermissionKey">PermissionKey</param>
        /// <param name="UserID">UserID</param>
        /// <param name="RoleID">RoleID</param>
        /// <param name="IsActive">IsActive</param>
        /// <param name="AddedBy">AddedBy</param>
        public static void SetFolderPermission(int FolderID, string PermissionKey, int UserID, Guid RoleID, int IsActive, string AddedBy)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FolderID", FolderID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PermissionKey", PermissionKey));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserID", UserID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@RoleID", RoleID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", AddedBy));

            SQLHandler sagesql = new SQLHandler();
            sagesql.ExecuteNonQuery("usp_FileManagerSetFolderPermission", ParaMeterCollection);
        }
        /// <summary>
        /// Connects to database and deletes existing permissions for given FolderID.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        public static void DeleteExistingPermissions(int FolderID)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FolderID", FolderID));

            SQLHandler sagesql = new SQLHandler();
            sagesql.ExecuteNonQuery("usp_FileManagerDeleteExistingPermissions", ParaMeterCollection);

        }
        /// <summary>
        /// Connects to database and obtains folder permission for given FolderID.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        /// <returns>List of FolderPermission class containing FolderID,UserID,PermissionID,PermissionKey,RoleID and UserName.</returns>
        public static List<FolderPermission> GetFolderPermissions(int FolderID)
        {
            List<FolderPermission> lstFolderPer = new List<FolderPermission>();
            string StoredProcedureName = "usp_FileManagerGetFolderPermission";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FolderID", FolderID));
            SqlDataReader SQLReader = null;
            try
            {
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName, ParaMeterCollection);
                while (SQLReader.Read())
                {
                    FolderPermission obj = new FolderPermission();
                    obj.FolderID = int.Parse(SQLReader["FolderID"].ToString());
                    obj.UserID = int.Parse(SQLReader["UserID"].ToString());
                    obj.PermissionID = int.Parse(SQLReader["PermissionID"].ToString());
                    obj.PermissionKey = SQLReader["PermissionKey"].ToString();
                    obj.RoleID = new Guid(SQLReader["RoleID"].ToString());
                    obj.UserName = SQLReader["UserName"].ToString() ?? SQLReader["UserName"].ToString();

                    lstFolderPer.Add(obj);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (SQLReader != null)
                {
                    SQLReader.Close();
                }
            }
            return lstFolderPer;
        }
        /// <summary>
        /// Connects to database and obtains user list for folder for given FolderID.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        /// <returns>List of FolderPermission class containing UserID and UserName.</returns>
        public static List<FolderPermission> GetUserListForFolder(int FolderID)
        {
            List<FolderPermission> lstFolderPer = new List<FolderPermission>();
            string StoredProcedureName = "usp_FileManagerGetUsersWithPermissions";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FolderID", FolderID));
            SqlDataReader SQLReader = null;
            try
            {
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName, ParaMeterCollection);
                while (SQLReader.Read())
                {
                    FolderPermission obj = new FolderPermission();
                    obj.UserID = int.Parse(SQLReader["UserID"].ToString());
                    obj.UserName = SQLReader["UserName"].ToString() ?? SQLReader["UserName"].ToString();

                    lstFolderPer.Add(obj);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (SQLReader != null)
                {
                    SQLReader.Close();
                }
            }
            return lstFolderPer;
        }
        /// <summary>
        /// Connects to database and obtains user id for given userName.
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns>UserId</returns>
        public static int GetUserID(string userName)
        {
            string StoredProcedureName = "usp_FileManagerGetUserID";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));

            int UserID = 0;
            UserID = sagesql.ExecuteAsScalar<int>(StoredProcedureName, ParaMeterCollection);
            return UserID;
        }
        /// <summary>
        /// Connects to database and obtains permission keys for given FolderID,UserID,UserModuleID and UserName.
        /// </summary>
        /// <param name="FolderID">FolderID</param>
        /// <param name="UserID">UserID</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="UserName">UserName</param>
        /// <returns>List of folder permission key.</returns>
        public static List<string> GetPermissionKeys(int FolderID, int UserID, int UserModuleID, string UserName)
        {
            List<string> lstPermissions = new List<string>();
            string StoredProcedureName = "usp_FileManagerGetFolderPermissionByUserID";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FolderID", FolderID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserID", UserID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", UserName));
            SqlDataReader SQLReader = null;
            try
            {
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName, ParaMeterCollection);
                while (SQLReader.Read())
                {
                    lstPermissions.Add(SQLReader["permissionkey"].ToString());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (SQLReader != null)
                {
                    SQLReader.Close();
                }
            }
            return lstPermissions;
        }
        /// <summary>
        /// Connects to database and obtains module permission.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="UserName">UserName</param>
        /// <returns>List of file manager module permission key.</returns>
        public static List<string> GetModulePermission(int UserModuleID, string UserName)
        {
            List<string> lstPermissions = new List<string>();
            string StoredProcedureName = "usp_FileManagerGetModulePermission";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", UserName));
            SqlDataReader SQLReader = null;
            try
            {
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName, ParaMeterCollection);
                while (SQLReader.Read())
                {
                    lstPermissions.Add(SQLReader["permissionkey"].ToString());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (SQLReader != null)
                {
                    SQLReader.Close();
                }
            }
            return lstPermissions;
        }
        /// <summary>
        /// Connects to database and obtains file manager settings for given UserModuleID and PortalID.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        /// <returns>List of FileManagerSettingInfo class containing file manager settings. </returns>
        public static List<FileManagerSettingInfo> GetFileManagerSettings(int UserModuleID, int PortalID)
        {
            List<FileManagerSettingInfo> lstSettings = new List<FileManagerSettingInfo>();
            string StoredProcedureName = "usp_FileManagerSettingGetAll";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

            try
            {
                lstSettings = sagesql.ExecuteAsList<FileManagerSettingInfo>(StoredProcedureName, ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstSettings;
        }
        /// <summary>
        /// Connects to database and adds or updates file manager setting value.
        /// </summary>
        /// <param name="lstSettings">List of object of FileManagerSettingInfo class. </param>
        public static void AddUpdateSettings(List<FileManagerSettingInfo> lstSettings)
        {
            foreach (FileManagerSettingInfo obj in lstSettings)
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserModuleID", obj.UserModuleID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@SettingKey", obj.SettingKey));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@SettingValue", obj.SettingValue));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", obj.IsActive));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", obj.PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@UpdatedBy", obj.UpdatedBy));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@AddedBy", obj.AddedBy));

                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("usp_FileManagerSettingAddUpdate", ParaMeterCollection);
            }
        }
    }


}
