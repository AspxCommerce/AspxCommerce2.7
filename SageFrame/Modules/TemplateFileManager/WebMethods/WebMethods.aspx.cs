using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using SageFrame.Localization;
using System.Data;
using SageFrame.Web.Utilities;
using System.Text;
using System.IO;
using System.Net;
using SageFrame.FileManager;
using RegisterModule;
using System.Collections;
using SageFrame.Web;
using SageFrame.Framework;
using System.Web.Caching;
using SageFrame.Templating.xmlparser;
using System.Drawing.Imaging;
using System.Drawing;
using SageFrame.Services;

[ScriptService]
public partial class Modules_FileManager_js_WebMethods : System.Web.UI.Page
{

    public static FileManagerBase fb = new FileManagerBase();
    #region WebMethods
    [WebMethod(true)]
    public static string GetFileList(string filePath, int folderId, int UserID, int IsSort, int CurrentPage, int PageSize, int portalID, string userName, int userModuleID, string secureToken)
    {
        StringBuilder sb = new StringBuilder();
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(filePath);
            List<ATTFile> lstFiles = new List<ATTFile>();
            if (filePath == "/")
            {
                filePath = HttpContext.Current.Server.MapPath("~/");
            }
            try
            {
                if (Directory.Exists(filePath))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(filePath);
                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        ATTFile obj = new ATTFile();
                        obj.FileName = file.Name;
                        obj.Folder = file.Directory.ToString();
                        obj.Size = int.Parse(file.Length.ToString());
                        obj.Extension = file.Extension;
                        lstFiles.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                fb.ProcessException(ex);
            }
            if (IsSort == 1)
            {
                SortList(ref lstFiles);
            }
            Dictionary<string, string> dictImages = GetImages();
            if (lstFiles.Count > 0)
            {
                lstFiles = lstFiles.GetRange(GetStartRange(CurrentPage, PageSize), GetEndRange(CurrentPage, PageSize, lstFiles.Count));
                ///Get the dictionary of images used in buttons

                sb.Append("<div class='sfGridwrapper'><table  width='100%' cellspacing='0' cellpadding='0' class=\"jqueryFileTree\" id=\"fileList\">\n");
                if (lstFiles.Count > 0 && HttpContext.Current.Session["SortDir"] == null || (string)HttpContext.Current.Session["SortDir"] == "asc")
                {
                    sb.Append("<tr><th><span class='selectAll'><input type='checkbox' id='chkSelectAll'/></span></th><th><span class='fileName'>FileName &nbsp;&nbsp;<i class='icon-ascending-order' id='imgSort'></i></span></th><th><span class='fileInfo'>FileInfo</span></th><th class='sfEdit'></th><th class='sfEdit'></th><th class='sfEdit'></th></tr>");
                }
                else if (lstFiles.Count > 0 && (string)HttpContext.Current.Session["SortDir"] == "desc")
                {
                    sb.Append("<tr><th><span class='selectAll'><input type='checkbox' id='chkSelectAll'/></span></th><th><span class='fileName'>FileName &nbsp;&nbsp;<i class='icon-descending-order' id='imgSort' ></i></span></th><th><span class='fileInfo'>FileInfo</span></th><th class='sfEdit'></th><th class='sfEdit'></th><th class='sfEdit'></th></tr>");
                }
            }
            if (lstFiles.Count == 0)
            {
                sb.Append("<div class='sfEmptyrow'>No Files</div>");
            }
            string downloadPath = FileManagerHelper.ReplaceBackSlash(Path.Combine(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), GetRelativePath("Modules/FileManager/DownloadHandler.ashx?")));
            // string urlPath = GetUrlPath(filePath);
            string urlPath = GetPreviewUrlPath(filePath);
            string absolutePath = FileManagerHelper.ReplaceBackSlash(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath.ToString(), filePath));
            int index = 0;
            foreach (ATTFile fi in lstFiles)
            {
                string ext = "";
                bool IsZip = false;
                bool IsImg = false;

                if (fi.Extension.Length > 1)
                    ext = fi.Extension.Substring(1).ToLower();
                if (ext == "zip")
                    IsZip = true;
                if (ext == "png" || ext == "gif" || ext == "jpg" || ext == "jpeg")
                    IsImg = true;
                string checkId = "chk_" + fi.FileId;
                try
                {
                    FileManagerHelper.ConstructHTMLString(IsZip, IsImg, fi.StorageLocation, ext, urlPath + fi.FileName, Path.Combine(absolutePath, fi.FileName), downloadPath, checkId, folderId, fi, ref sb, "edit", dictImages, index);
                }
                catch (Exception ex)
                {
                    fb.ProcessException(ex);
                }
                index++;
            }
            sb.Append("</table></div>");
            sb.Append("<div id='divBottomControl'>");
            sb.Append("</div>");
        }
        return sb.ToString();
    }

    [WebMethod(true)]
    public static string SearchFiles(string SearchQuery, int CurrentPage, int PageSize, string FilePath, int portalID, string userName, int userModuleID, string secureToken)
    {
        StringBuilder sb = new StringBuilder();
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            if (FilePath == "/")
            {
                FilePath = HttpContext.Current.Server.MapPath("~/");
            }

            List<ATTFile> lstFiles = new List<ATTFile>();
            if (Directory.Exists(FilePath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(FilePath);
                foreach (FileInfo file in dirInfo.GetFiles(SearchQuery + "*"))
                {
                    ATTFile obj = new ATTFile();
                    obj.FileName = file.Name;
                    obj.Folder = file.Directory.ToString();
                    obj.Size = int.Parse(file.Length.ToString());
                    obj.Extension = file.Extension;
                    lstFiles.Add(obj);
                }
            }

            Dictionary<string, string> dictImages = GetImages();
            List<string> lstPermissions = FileManagerController.GetModulePermission(userModuleID, userName);
            int UserPermissionKey = lstPermissions.Contains("EDIT") ? 1 : 0;
            if (lstFiles.Count > 0)
                lstFiles = lstFiles.GetRange(GetStartRange(CurrentPage, PageSize), GetEndRange(CurrentPage, PageSize, lstFiles.Count));

            sb.Append("<div class='sfGridwrapper'><table  width='100%' cellspacing='0' cellpadding='0' class=\"jqueryFileTree\" id=\"fileList\">\n");
            if (lstFiles.Count > 0)
            {
                sb.Append("<tr><th><span class='selectAll'><input type='checkbox' id='chkSelectAll'/></span></th><th><span class='fileName'>FileName<img src=" + dictImages["Sort"].ToString() + "></span></th><th><span class='fileInfo'>FileInfo</span></th><th class='sfEdit'></th><th class='sfEdit'></th><th class='sfEdit'></th></tr>");
            }
            if (lstFiles.Count == 0)
            {
                sb.Append("<div class='sfEmptyrow'>No Files</div>");
            }
            string downloadPath = FileManagerHelper.ReplaceBackSlash(Path.Combine(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), GetRelativePath("Modules/FileManager/DownloadHandler.ashx?")));



            if (UserPermissionKey == 1)
            {
                int index = 0;
                foreach (ATTFile fi in lstFiles)
                {
                    string ext = "";
                    //bool IsZip = false;
                    bool IsImg = false;
                    if (fi.Extension.Length > 1)
                        ext = fi.Extension.Substring(1).ToLower();
                    // if (ext == "zip")
                    //     IsZip = true;
                    if (ext == "png" || ext == "gif" || ext == "jpg" || ext == "jpeg")
                        IsImg = true;
                    string checkId = "chk_" + fi.FileId;
                    try
                    {
                        FileManagerHelper.ConstructHTMLString(false, IsImg, fi.StorageLocation, ext, Path.Combine(GetUrlPath(fi.Folder), fi.FileName), Path.Combine(GetAbsolutePath(fi.Folder), fi.FileName), downloadPath, checkId, 0, fi, ref sb, "edit", dictImages, index);

                    }
                    catch (Exception ex)
                    {

                        fb.ProcessException(ex);
                    }
                    index++;
                }
            }
            else
            {
                int index = 0;
                foreach (ATTFile fi in lstFiles)
                {
                    string ext = "";
                    //  bool IsZip = false;
                    bool IsImg = false;
                    if (fi.Extension.Length > 1)
                        ext = fi.Extension.Substring(1).ToLower();
                    //if (ext == "zip")
                    //   IsZip = true;
                    if (ext == "png" || ext == "gif" || ext == "jpg" || ext == "jpeg")
                        IsImg = true;
                    string checkId = "chk_" + fi.FileId;
                    try
                    {
                        FileManagerHelper.ConstructHTMLString(false, IsImg, fi.StorageLocation, ext, Path.Combine(GetUrlPath(fi.Folder), fi.FileName), Path.Combine(GetAbsolutePath(fi.Folder), fi.FileName), downloadPath, checkId, 0, fi, ref sb, "view", dictImages, index);
                    }
                    catch (Exception ex)
                    {

                        fb.ProcessException(ex);
                    }
                    index++;
                }
            }
            sb.Append("</table>");
            sb.Append("<div id='divPagerNav'></div>");
            sb.Append("</div>");

        }
        return sb.ToString();

    }

    [WebMethod()]
    public static string DeleteFile(string filePath, int portalID, string userName, int userModuleID, string secureToken)
    {
        string message = "";
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {

            filePath = GetAbsolutePath(filePath);

            if (filePath.EndsWith("/"))
            {
                DirectoryInfo di = new DirectoryInfo(filePath);
                if (di.Exists)
                {
                    try
                    {
                        di.Delete();
                        message = "Folder deleted successfully";

                    }
                    catch (Exception ex)
                    {

                        fb.ProcessException(ex);
                        message = "Folder contaning files cannot be deleted";
                    }

                }
            }
            else
            {

                FileInfo file = new FileInfo(filePath);
                // Checking if file exists
                if (file.Exists)
                {
                    ///Reset the file attribute before deleting
                    try
                    {
                        File.SetAttributes(filePath, FileAttributes.Normal);
                        file.Delete();

                        message = "File deleted successfully";
                    }
                    catch (Exception ex)
                    {

                        fb.ProcessException(ex);
                        message = "File could be deleted";
                    }
                }
            }
            //CacheHelper.Clear("FileManagerFileList");
            //CacheHelper.Clear("FileManagerFolders");
        }
        return message;
    }

    [WebMethod]
    public static void DeleteRootFolder(int FolderID, int portalID, string userName, int userModuleID, string secureToken)
    {
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            FileManagerController.DeleteRootFolder(FolderID);
        }
    }

    [WebMethod]
    public static void CreateFolder(int FolderID, string filePath, string folderName, int fileType, int portalID, string userName, int userModuleID, string secureToken)
    {
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            string absolutePath = FileManagerHelper.ReplaceBackSlash(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath.ToString(), filePath));
            DirectoryInfo dir = new DirectoryInfo(absolutePath);
            if (!dir.Exists)
            {
                dir.Create();
                Folder folder = new Folder();
                folder.PortalId = fb.GetPortalID;
                folder.ParentID = FolderID;
                folder.FolderPath = filePath;
                folder.StorageLocation = fileType;
                folder.UniqueId = Guid.NewGuid();
                folder.VersionGuid = Guid.NewGuid();
                folder.IsActive = 1;
                folder.AddedBy = fb.GetUsername;
                try
                {
                    FileManagerController.AddFolder(folder);
                    CacheHelper.Clear("FileManagerFolders");
                }
                catch (Exception ex)
                {

                    fb.ProcessException(ex);
                }
            }
        }
    }

    [WebMethod]
    public static void AddRootFolder(string FolderName, int portalID, string userName, int userModuleID, string secureToken)
    {
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            string absolutePath = FileManagerHelper.ReplaceBackSlash(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath.ToString(), FolderName));
            DirectoryInfo dir = new DirectoryInfo(absolutePath);
            if (dir.Exists)
            {
                dir.Create();
                Folder folder = new Folder();
                folder.PortalId = fb.GetPortalID;
                folder.FolderPath = FolderName;
                folder.StorageLocation = 0;
                folder.UniqueId = Guid.NewGuid();
                folder.VersionGuid = Guid.NewGuid();
                folder.IsActive = 1;
                folder.AddedBy = fb.GetUsername;
                FileManagerController.AddRootFolder(folder);
                CacheHelper.Clear("FileManagerFolders");

            }
        }
    }

    [WebMethod]
    public static List<Folder> GetRootFolders(int portalID, string userName, int userModuleID, string secureToken)
    {
        AuthenticateService objService = new AuthenticateService();
        List<Folder> objRootFolderList = new List<Folder>();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            objRootFolderList = FileManagerController.GetRootFolders();
        }
        return objRootFolderList;
    }

    [WebMethod()]
    public static void SetFilePath(string filePath, int folderId, int portalID, string userName, int userModuleID, string secureToken)
    {

        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            HttpContext.Current.Session["Path"] = filePath;
            HttpContext.Current.Session["FolderID"] = folderId;
        }
    }

    [WebMethod()]
    public static void UpdateFile(string fileName, string filePath, string attrString, int portalID, string userName, int userModuleID, string secureToken)
    {
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            filePath = GetAbsolutePath(filePath);
            try
            {
                FileInfo file = new FileInfo(filePath);
                /// Checking if file exists
                if (file.Exists)
                {
                    ///get the folder path
                    filePath = filePath.Substring(0, filePath.LastIndexOf("/") + 1);
                    filePath = filePath + fileName;
                    file.MoveTo(filePath);
                    //FileManagerController.UpdateFile(fileId, fileName);
                    FileManagerHelper.SetFileAttributes(filePath, attrString);
                    //CacheHelper.Clear("FileManagerFileList");

                }
            }
            catch (Exception ex)
            {

                fb.ProcessException(ex);
            }
        }
    }

    [WebMethod()]
    public static void CopyFile(string filePath, string fromPath, string toPath, int portalID, string userName, int userModuleID, string secureToken)
    {
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            string fullFilePath = GetAbsolutePath(filePath);
            string fullFromPath = GetAbsolutePath(fromPath);
            string fullToPath = GetAbsolutePath(toPath);
            try
            {
                //public static void TransferFile(string filePath, string toPath, int action, int mode, string fullFilePath, string fullFromPath, string fullToPath)
                FileManagerHelper.TransferFile(filePath, toPath, (int)Action.COPY, (int)TransferMode.NORMALTONORMAL, fullFilePath, fullFromPath, fullToPath);
            }
            catch (Exception ex)
            {

                fb.ProcessException(ex);
            }
        }
    }
    [WebMethod()]
    public static void MoveFile(string filePath, string fromPath, string toPath, int portalID, string userName, int userModuleID, string secureToken)
    {
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            string fullFilePath = GetAbsolutePath(filePath);
            string fullFromPath = GetAbsolutePath(fromPath);
            string fullToPath = GetAbsolutePath(toPath);
            FileManagerHelper.TransferFile(filePath, toPath, (int)Action.MOVE, (int)TransferMode.NORMALTONORMAL, fullFilePath, fullFromPath, fullToPath);
        }
    }

    [WebMethod]
    public static List<Roles> GetAllRoles(int portalID, string userName, int userModuleID, string secureToken)
    {
        AuthenticateService objService = new AuthenticateService();
        List<Roles> lstNewRoles = new List<Roles>();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            List<Roles> lstRoles = PermissionHelper.GetAllRoles(1, true, "superuser");
            foreach (Roles r in lstRoles)
            {
                if (Regex.Replace(r.RoleName.ToLower(), @"\s", "") == "superuser")
                {
                    lstNewRoles.Insert(0, r);
                }
                else if (Regex.Replace(r.RoleName.ToLower(), @"\s", "") == "siteadmin")
                {
                    lstNewRoles.Insert(1, r);
                }
                else
                {
                    lstNewRoles.Add(r);
                }
            }
        }
        return lstNewRoles;
    }

    [WebMethod]
    public static void SetFolderPermission(int FolderID, int UserID, int IsActive, string AddedBy, List<Permission> lstPermission, int portalID, string userName, int userModuleID, string secureToken)
    {
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            FileManagerController.DeleteExistingPermissions(FolderID);
            foreach (Permission objPer in lstPermission)
            {
                try
                {
                    if (objPer.UserID != 0)
                    {
                        objPer.RoleID = Guid.Empty;
                    }
                    FileManagerController.SetFolderPermission(FolderID, objPer.PermissionKey, objPer.UserID, objPer.RoleID, IsActive, AddedBy);
                }
                catch (Exception ex)
                {

                    fb.ProcessException(ex);
                }
            }
        }

    }

    [WebMethod]
    public static List<FolderPermission> GetFolderPermissions(int FolderID, int portalID, string userName, int userModuleID, string secureToken)
    {
        List<FolderPermission> objFolder = new List<FolderPermission>();
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            objFolder = FileManagerController.GetFolderPermissions(FolderID);
        }
        return objFolder;
    }

    [WebMethod]
    public static int ValidateUser(int portalID, string userName, int userModuleID, string secureToken)
    {
        int UserID = 0;
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            UserID = FileManagerController.GetUserID(userName);
        }
        return UserID;
    }

    [WebMethod]
    public static List<FolderPermission> GetUserListForFolder(int FolderID, int portalID, string userName, int userModuleID, string secureToken)
    {
        List<FolderPermission> objFolderPErmission = new List<FolderPermission>();
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            objFolderPErmission = FileManagerController.GetUserListForFolder(FolderID);
        }
        return objFolderPErmission;
    }

    [WebMethod]
    public static string GetPermissionKeys(int FolderID, int UserID, int portalID, string userName, int userModuleID, string secureToken)
    {
        AuthenticateService objService = new AuthenticateService();
        string keyString = string.Empty;
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            List<string> lstPermission = FileManagerController.GetPermissionKeys(FolderID, UserID, userModuleID, userName);
            keyString = string.Join("-", lstPermission.ToArray());
        }
        return keyString;
    }

    [WebMethod]
    public static string UnzipFiles(string FilePath, int FolderID, int portalID, string userName, int userModuleID, string secureToken)
    {
        StringBuilder sb = new StringBuilder();
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            string absolutePath = GetAbsolutePath(FilePath);
            FileInfo file = new FileInfo(absolutePath);
            string folderName = file.Name;
            string newFolderPath = FileManagerHelper.GetFilePathWithoutExtension(absolutePath);
            DirectoryInfo dir = new DirectoryInfo(newFolderPath);
            if (!dir.Exists)
            {
                string path = string.Empty;
                FileManagerHelper.UnZipFiles(absolutePath, newFolderPath, ref path, SageFrame.Common.RegisterModule.Common.Password, false, userModuleID, fb.GetPortalID);
                Folder folder = new Folder();
                folder.PortalId = fb.GetPortalID;
                folder.ParentID = FolderID;
                folder.FolderPath = FileManagerHelper.ReplaceBackSlash(FileManagerHelper.GetFilePathWithoutExtension(FilePath));
                folder.StorageLocation = 0;
                folder.UniqueId = Guid.NewGuid();
                folder.VersionGuid = Guid.NewGuid();
                folder.IsActive = 1;
                folder.IsRoot = false;
                folder.AddedBy = fb.GetUsername;
                try
                {
                    int folderID = FileManagerController.AddFolderReturnFolderID(folder);
                    RecurseThroughDirectory(dir, folderID, userModuleID, ref sb);
                }
                catch (Exception ex)
                {
                    fb.ProcessException(ex);
                }
            }
            CacheHelper.Clear("FileManagerFileList");
            CacheHelper.Clear("FileManagerFolders");
        }
        return sb.ToString();
    }



    [WebMethod]
    public static string GetExtensions(int portalID, string userName, int userModuleID, string secureToken)
    {
        string extension = "";
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            SageFrameConfig config = new SageFrameConfig();
            extension = config.GetSettingValueByIndividualKey(SageFrameSettingKeys.FileExtensions);
        }
        return extension;
    }


    [WebMethod]
    public static void Synchronize(int portalID, string userName, int userModuleID, string secureToken)
    {
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            try
            {

                SynchronizeFiles.UserName = fb.GetUsername;
                SynchronizeFiles.PortalID = fb.GetPortalID;
                SynchronizeFiles.extensions = "jpg,zip,txt,doc,docx,tif,css,js,jpeg,png";
                SynchronizeFiles.absolutePath = HttpContext.Current.Request.PhysicalApplicationPath.ToString();
                //SynchronizeFiles.lstFolders = FileManagerController.GetAllFolders();
                SynchronizeFiles.F2DSync();
                SynchronizeFiles.D2FSync();

            }
            catch (Exception ex)
            {
                fb.ProcessException(ex);
            }
            finally
            {
                CacheHelper.Clear("FileManagerFolders");
                CacheHelper.Clear("FileManagerFileList");
            }
        }
    }
    #endregion

    #region pagermethods
    [WebMethod]
    public static int GetCount(string FilePath, int portalID, string userName, int userModuleID, string secureToken)
    {
        AuthenticateService objService = new AuthenticateService();
        int numFiles = 0;
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            if (FilePath == "/")
            {
                FilePath = HttpContext.Current.Server.MapPath("~/");
            }
            string[] files;

            files = Directory.GetFiles(FilePath);
            numFiles = files.Length;
        }
        return numFiles;
    }

    [WebMethod(true)]    
    public static int GetSearchCount(string SearchQuery, string FilePath, int portalID, string userName, int userModuleID, string secureToken)
    {
        List<ATTFile> lstFiles = new List<ATTFile>();
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            if (FilePath == "/")
            {
                FilePath = HttpContext.Current.Server.MapPath("~/");
            }
           
            if (Directory.Exists(FilePath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(FilePath);
                foreach (FileInfo file in dirInfo.GetFiles(SearchQuery + "*"))
                {
                    ATTFile obj = new ATTFile();
                    obj.FileName = file.Name;
                    lstFiles.Add(obj);
                }
            }
        }
        return lstFiles.Count;
    }

    public static int GetStartRange(int CurrentPage, int PageSize)
    {
        int startIndex = PageSize * (CurrentPage - 1);
        return startIndex;
    }

    public static int GetEndRange(int CurrentPage, int PageSize, int rowCount)
    {
        int endRange = PageSize;
        int startIndex = GetStartRange(CurrentPage, PageSize);
        if (startIndex + PageSize > rowCount)
        {
            endRange = rowCount - startIndex;
        }
        return endRange;
    }
    #endregion

    #region HelperMethods
    public static void SortList(ref List<ATTFile> lstFiles)
    {
        if (HttpContext.Current.Session != null)
            if (HttpContext.Current.Session["SortDir"] == null || (string)HttpContext.Current.Session["SortDir"] == "asc")
            {
                lstFiles.Sort(
                    delegate(ATTFile f1, ATTFile f2)
                    {
                        return f1.FileName.CompareTo(f2.FileName);
                    }

                    );
                HttpContext.Current.Session["SortDir"] = "desc";
            }
            else
            {
                lstFiles.Sort(
                    delegate(ATTFile f1, ATTFile f2)
                    {
                        return f2.FileName.CompareTo(f1.FileName);
                    }

                    );
                HttpContext.Current.Session["SortDir"] = "asc";
            }
    }

    public static Dictionary<string, string> GetImages()
    {
        Dictionary<string, string> dictImageList = new Dictionary<string, string>();

        dictImageList.Add("Delete", GetRelativePath("Modules/FileManager/images/delete.png"));
        dictImageList.Add("Upload", GetRelativePath("/Modules/FileManager/images/btnupload.png"));
        dictImageList.Add("Edit", GetRelativePath("Modules/FileManager/images/edit.png"));
        dictImageList.Add("Preview", GetRelativePath("Modules/FileManager/images/img_preview.png"));
        dictImageList.Add("Extract", GetRelativePath("Modules/FileManager/images/extract.png"));
        dictImageList.Add("Sort", GetRelativePath("Modules/FileManager/images/sort.png"));
        dictImageList.Add("Sort2", GetRelativePath("Modules/FileManager/images/sort2.png"));

        return dictImageList;
    }
    protected static string GetRelativePath(string filePath)
    {
        return (FileManagerHelper.ReplaceBackSlash(Path.Combine(HttpContext.Current.Request.ApplicationPath.ToString(), filePath)));
    }
    public static string RecurseThroughDirectory(DirectoryInfo dir, int folderId, int UserModuleID, ref StringBuilder sb)
    {

        foreach (FileInfo file in dir.GetFiles())
        {
            ATTFile obj = new ATTFile();
            obj.PortalId = fb.GetPortalID;
            obj.UniqueId = Guid.NewGuid();
            obj.VersionGuid = Guid.NewGuid();
            obj.FileName = file.Name;
            obj.Extension = file.Extension;
            obj.Size = int.Parse(file.Length.ToString());
            obj.ContentType = FileManagerHelper.ReturnExtension(file.Extension);
            obj.Folder = FileManagerHelper.ReplaceBackSlash(dir.FullName.Replace(HttpContext.Current.Request.PhysicalApplicationPath, ""));
            obj.FolderId = folderId;
            obj.IsActive = 1;
            obj.StorageLocation = 0;
            obj.AddedBy = fb.GetUsername;

            try
            {

                if (FileManagerHelper.CheckForValidExtensions(UserModuleID, file.Extension.Replace(".", ""), fb.GetPortalID))
                {
                    FileManagerController.AddFile(obj);
                    sb.Append("File ").Append("Extraction completed successfully");
                }
                else
                {
                    sb.Append("File ").Append(file.Name).Append(" has invalid extension \n");
                }
            }
            catch (Exception ex)
            {

                fb.ProcessException(ex);
            }

        }
        foreach (DirectoryInfo childDir in dir.GetDirectories())
        {
            Folder folder = new Folder();
            folder.PortalId = fb.GetPortalID;
            folder.ParentID = folderId;
            folder.FolderPath = FileManagerHelper.ReplaceBackSlash(childDir.FullName.Replace(HttpContext.Current.Request.PhysicalApplicationPath, ""));
            folder.StorageLocation = 0;
            folder.UniqueId = Guid.NewGuid();
            folder.VersionGuid = Guid.NewGuid();
            folder.IsActive = 1;
            folder.IsRoot = false;
            folder.AddedBy = fb.GetUsername;
            try
            {
                int FolderID = FileManagerController.AddFolderReturnFolderID(folder);
                RecurseThroughDirectory(childDir, FolderID, UserModuleID, ref sb);
            }
            catch (Exception ex)
            {

                fb.ProcessException(ex);
            }


        }
        return sb.ToString();
    }
    public static string GetAbsolutePath(string filepath)
    {
        return (FileManagerHelper.ReplaceBackSlash(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath.ToString(), filepath)));
    }
    public static string GetUrlPath(string path)
    {
        string relativePathInitial = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/";
        relativePathInitial = (FileManagerHelper.ReplaceBackSlash(Path.Combine(relativePathInitial, path)));
        if (relativePathInitial.Contains("SageFrame/"))
        {
            string[] stringSeparators = new string[] { "SageFrame/" };
            string[] imgPath;
            imgPath = relativePathInitial.Split(stringSeparators, StringSplitOptions.None);
            string testapth = HttpContext.Current.Request.ApplicationPath + "/" + imgPath[1];
            return HttpContext.Current.Request.ApplicationPath + "/" + imgPath[1];
        }
        else
            return HttpContext.Current.Request.ApplicationPath;

    }
    public static string GetPreviewUrlPath(string path)
    {
        string root = HttpContext.Current.Server.MapPath("~/");
        string relativePathInitial = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/";
        if (root != path)
        {
            string stringSeparators = path.Substring(root.Length).Replace('\\', '/');
            relativePathInitial += stringSeparators;
        }
        return relativePathInitial;
    }
    #endregion

    #region Classes and Enums
    public class Permission
    {
        public Guid RoleID { get; set; }
        public string PermissionKey { get; set; }
        public int UserID { get; set; }
        public Permission() { }
    }
    public enum Action
    {
        COPY = 1,
        MOVE = 2
    }
    public enum TransferMode
    {
        NORMALTONORMAL = 1,
        NORMALTOSECURE = 2,
        NORMALTODATABASE = 3,
        SECURETONORMAL = 4,
        SECURETOSECURE = 5,
        SECURETODATABASE = 6,
        DATABASETONORMAL = 7,
        DATABASETOSECURE = 8,
        DATABASETODATABASE = 9
    }
    #endregion


    //new

    [WebMethod(true)]
    public static string EditFile(string FilePath ,int portalID, string userName, int userModuleID, string secureToken )
    {
        AuthenticateService objService = new AuthenticateService();
        string ImgPath = string.Empty;
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
             ImgPath = HttpContext.Current.Request.ApplicationPath.ToString() + FilePath;            
        }
        return ImgPath;
    }

    [WebMethod(true)]
    public static string ReadFiles(string FullPath, int portalID, string userName, int userModuleID, string secureToken)
    {
        string html = string.Empty;
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            try
            {
                int index = 0;
                index = HttpContext.Current.Request.ApplicationPath == "/" ? 0 : 1;
                string originalFile = string.Concat(HttpContext.Current.Server.MapPath("~/"), FullPath.Substring(FullPath.IndexOf("/", index)).Replace("/", "\\"));
                html = GetXMLString(originalFile, portalID, userName, userModuleID, secureToken);
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        return html;
    }
    public static string GetXMLString(string filePath, int portalID, string userName, int userModuleID, string secureToken)
    {
        AuthenticateService objService = new AuthenticateService();
        string xml = null;
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            StreamReader sr = null;           
            try
            {
                sr = new StreamReader(filePath);
                xml = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr = null;
                }
            }
        }
        return xml;
    }
    [WebMethod(true)]
    public static void UpdateFileContain(string FullPath, string UpdateValue, int portalID, string userName, int userModuleID, string secureToken)
    {
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            int index = 0;
            index = HttpContext.Current.Request.ApplicationPath == "/" ? 0 : 1;
            string originalFile = string.Concat(HttpContext.Current.Server.MapPath("~/"), FullPath.Substring(FullPath.IndexOf('/', index)).Replace("/", "\\"));
            System.IO.StreamWriter objStreamWriter = new System.IO.StreamWriter(originalFile);
            objStreamWriter.Write(UpdateValue);
            objStreamWriter.Close();            
        }
    }
    [WebMethod(true)]
    public static string CropImage(string ImagePath, int X1, int Y1, int X2, int Y2, int w, int h, int portalID, string userName, int userModuleID, string secureToken)
    {
        string CroppedImag = "";
        AuthenticateService objService = new AuthenticateService();
        if (objService.IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            string dir = "";
            string imagename = ImagePath.Substring(ImagePath.LastIndexOf('/') + 1).ToString();
            dir = ImagePath.Replace(imagename, "");
            string imagenamewithoutext = imagename.Substring(imagename.LastIndexOf('.') + 1).ToString();

            int X = System.Math.Min(X1, X2);
            int Y = System.Math.Min(Y1, Y2);
            int index = 0;
            index = HttpContext.Current.Request.ApplicationPath == "/" ? 0 : 1;
            string originalFile = string.Concat(HttpContext.Current.Server.MapPath("~/"), ImagePath.Substring(ImagePath.IndexOf('/', index)).Replace("/", "\\"));
            string savePath = Path.GetDirectoryName(originalFile) + "\\";
            if (File.Exists(originalFile))
            {
                using (System.Drawing.Image img = System.Drawing.Image.FromFile(originalFile))
                {
                    using (System.Drawing.Bitmap _bitmap = new System.Drawing.Bitmap(w, h))
                    {
                        _bitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);
                        using (Graphics _graphic = Graphics.FromImage(_bitmap))
                        {
                            _graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            _graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            _graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                            _graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            _graphic.DrawImage(img, 0, 0, w, h);
                            _graphic.DrawImage(img, new Rectangle(0, 0, w, h), X, Y, w, h, GraphicsUnit.Pixel);

                            Random rand = new Random((int)DateTime.Now.Ticks);
                            int RandomNumber;
                            RandomNumber = rand.Next(1, 200);
                            int CharCode = rand.Next(Convert.ToInt32('a'), Convert.ToInt32('z'));
                            char RandomChar = Convert.ToChar(CharCode);
                            string extension = Path.GetExtension(originalFile);
                            //string croppedFileName = Guid.NewGuid().ToString();
                            string croppedFileName = imagenamewithoutext + "_" + RandomNumber.ToString() + RandomChar.ToString();
                            string path = savePath;

                            // If the image is a gif file, change it into png
                            if (extension.EndsWith("gif", StringComparison.OrdinalIgnoreCase))
                            {
                                extension = ".png";
                            }

                            string newFullPathName = string.Concat(path, croppedFileName, extension);

                            using (EncoderParameters encoderParameters = new EncoderParameters(1))
                            {
                                encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                                _bitmap.Save(newFullPathName, GetImageCodec(extension), encoderParameters);
                            }

                            //lblCroppedImage.Text = string.Format("<img src='{0}' alt='Cropped image'>", path + extension);

                            CroppedImag = string.Format("<img src='{0}' alt='Cropped image'>", string.Concat(dir, croppedFileName, extension));
                        }
                    }
                }
            }
        }
        return CroppedImag;
    }
    /// <summary>
    /// Find the right codec
    /// </summary>
    /// <param name="extension"></param>
    /// <returns></returns>
    public static ImageCodecInfo GetImageCodec(string extension)
    {
        extension = extension.ToUpperInvariant();
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FilenameExtension.Contains(extension))
            {
                return codec;
            }
        }
        return codecs[1];
    }
}