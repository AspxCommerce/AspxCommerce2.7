#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SageFrame.Web;
using SageFrame.Templating;
using System.IO;
using System.Collections.Generic;
using RegisterModule;
using SageFrame.ExtractTemplate;
using System.Text.RegularExpressions;
using System.Xml;
#endregion

public partial class Modules_LayoutManager_LayoutManager : BaseAdministrationUserControl
{
    public string appPath = string.Empty;
    public string UnexpectedEOF = "Unexpected EOF";
    public int PortalID = 0;
    public string Extension;
    public string EditFiles = string.Empty;
    public string UserModuleID = string.Empty;
    protected void Page_Init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Extension = SageFrameSettingKeys.PageExtension;
        IncludeJs("LayoutManager", false, "/js/jquery.easy-confirm-dialog.js");
        IncludeCss("WorkflowCssalert", "/css/jquery.alerts.css");
        IncludeJs("WorkflowJs", "/js/jquery.alerts.js");

        IncludeJs("LayoutManager", "/Modules/LayoutManager/js/resize.js");
        IncludeJs("LayoutManager", "/Modules/LayoutManager/js/LayoutManager.js");

        IncludeJs("LayoutManager", false, "/Editors/ckeditor/ckeditor.js", "/Editors/ckeditor/adapters/jquery.js");
        IncludeJs("LayoutManager", false, "/Modules/LayoutManager/CodeMirror/codemirror.js");
        IncludeJs("LayoutManager", "/Modules/LayoutManager/CodeMirror/xml.js");
        IncludeCss("LayoutManager", "/Modules/LayoutManager/CodeMirror/codemirror.css");
        IncludeCss("LayoutManager", "/Modules/LayoutManager/CodeMirror/default.css");
        IncludeCss("LayoutManager", "/Modules/LayoutManager/css/module.css");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ckEditorUserModuleID", " var ckEditorUserModuleID='" + SageUserModuleID + "';", true);
        appPath = GetApplicationName;
        EditFiles = appPath;
        PortalID = GetPortalID;
        UserModuleID = SageUserModuleID;
        if (!IsParent)
        {
            EditFiles = appPath + "/portal/" + GetPortalSEOName;
        }
        if (!IsPostBack)
        {
            string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "FileManagerGlobalVariable1", " var LayoutManagerPath='" + ResolveUrl(modulePath) + "';", true);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (fupUploadTemp.HasFile && fupUploadTemp.PostedFile.FileName != string.Empty)
            {
                string fileName = fupUploadTemp.PostedFile.FileName;
                string cntType = fupUploadTemp.PostedFile.ContentType;
                if (fileName.Substring(fileName.Length - 3, 3).ToLower() == "zip")
                {
                    string path = HttpContext.Current.Server.MapPath("~/");
                    string temPath = SageFrame.Common.RegisterModule.Common.TemporaryFolder;
                    string destPath = Path.Combine(path, temPath);
                    string downloadPath = SageFrame.Common.RegisterModule.Common.TemporaryTemplateFolder;
                    string downloadDestPath = Path.Combine(path, downloadPath);
                    string templateName = ParseFileNameWithoutPath(fileName.Substring(0, fileName.Length - 4));
                    string templateFolderPath = path + "Templates\\" + templateName;
                    if (!Directory.Exists(templateFolderPath))
                    {
                        if (!Directory.Exists(destPath))
                            Directory.CreateDirectory(destPath);
                        string filePath = destPath + "\\" + fupUploadTemp.FileName;
                        fupUploadTemp.SaveAs(filePath);
                        string ExtractedPath = string.Empty;
                        ZipUtil.UnZipFiles(filePath, destPath, ref ExtractedPath, SageFrame.Common.RegisterModule.Common.Password, SageFrame.Common.RegisterModule.Common.RemoveZipFile);
                        DirectoryInfo temp = new DirectoryInfo(ExtractedPath);
                        bool templateWithData = false;
                        FileInfo[] files = temp.GetFiles();
                        foreach (FileInfo file in files)
                        {
                            if (file.Name.ToLower() == "manifest.sfe")
                                templateWithData = true;
                        }
                        if (templateWithData == false)
                        {
                            if (!Directory.Exists(downloadDestPath))
                                Directory.CreateDirectory(downloadDestPath);
                            fupUploadTemp.SaveAs(downloadDestPath + "\\" + fupUploadTemp.FileName);
                            Directory.Move(ExtractedPath, templateFolderPath);
                        }
                        else
                        {
                            ExtractTemplate(ExtractedPath + "/" + templateName, templateName);
                            Directory.Move(ExtractedPath + "/" + templateName + "/" + templateName, templateFolderPath);
                            Directory.Delete(ExtractedPath, true);
                        }
                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("TemplateManagement", "TemplateInstallSuccessfully"), "", SageMessageType.Success);
                    }
                    else
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("TemplateManagement", "TemplateAlreadyInstall"), "", SageMessageType.Error);
                    }
                }
                else
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("TemplateManagement", "InvalidTemplateZip"), "", SageMessageType.Alert);
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Equals(UnexpectedEOF, StringComparison.OrdinalIgnoreCase))
            {
                ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("TemplateManagement", "ZipFileIsCorrupt"), "", SageMessageType.Alert);

            }
            else
            {
                ProcessException(ex);
            }
        }
    }

    private string ParseFileNameWithoutPath(string path)
    {
        if (path != null && path != string.Empty)
        {
            char seperator = '\\';
            string[] file = path.Split(seperator);
            return file[file.Length - 1];
        }
        return string.Empty;
    }

    //added by BJ

    //Extracting The Template
    public void ExtractTemplate(string filepath, string templateName)
    {
        try
        {
            //Template Xml Reader
            List<ExtractPageInfo> lstPageNodes = new List<ExtractPageInfo>();
            string filexmlpath = filepath + "/Modules.xml";
            XmlDocument doc = SageFrame.Templating.xmlparser.XmlHelper.LoadXMLDocument(filexmlpath);
            XmlNodeList pagelist = doc.SelectNodes("Template/Page");
            //data Xml reader
            string xmlPath = filepath + "/Data.xml";
            DataSet objDataset = new DataSet();
            objDataset.ReadXml(xmlPath);
            foreach (XmlNode page in pagelist)
            {
                ExtractPageInfo p = new ExtractPageInfo();
                p.PageID = int.Parse(Utils.CleanString(page["PageID"].InnerText));
                p.PageOrder = int.Parse(Utils.CleanString(page["PageOrder"].InnerText));
                p.PageName = page["PageName"].InnerText;
                p.Isvisible = int.Parse(Utils.CleanString(page["IsVisible"].InnerText));
                p.ParentID = int.Parse(Utils.CleanString(page["ParentID"].InnerText));
                p.Level = int.Parse(Utils.CleanString(page["Level"].InnerText));
                p.IconFile = Utils.CleanString(page["IconFile"].InnerText);
                p.DisableLink = bool.Parse(Utils.CleanString(page["DisableLink"].InnerText));
                p.Title = Utils.CleanString(page["Title"].InnerText);
                p.Description = Utils.CleanString(page["Description"].InnerText);
                p.KeyWords = Utils.CleanString(page["KeyWords"].InnerText);
                p.Url = Utils.CleanString(page["Url"].InnerText);
                p.TabPath = Utils.CleanString(page["TabPath"].InnerText);
                p.RefreshInterval = float.Parse(Utils.CleanString(page["RefreshInterval"].InnerText));
                p.PageHeadText = Utils.CleanString(page["PageHeadText"].InnerText);
                p.IsSecure = bool.Parse(Utils.CleanString(page["IsSecure"].InnerText));
                p.IsActive = bool.Parse(Utils.CleanString(page["IsActive"].InnerText));
                p.SEOName = Utils.CleanString(page["SEOName"].InnerText);
                p.IsShowInFooter = bool.Parse(Utils.CleanString(page["IsShowInFooter"].InnerText));
                p.IsRequiredPage = bool.Parse(Utils.CleanString(page["IsRequiredPage"].InnerText));
                p.PagePermissionList = PagePermissionList(page);
                p.ModuleList = ModuleList(page, objDataset);
                lstPageNodes.Add(p);
            }
            List<TemplateMenuAll> objTemplateMenuall = GetMenuDetail(filepath);
            doc.Save(filexmlpath);
            InsertTemplate(lstPageNodes, objTemplateMenuall);
            CopyFiles(filepath, templateName);// copy the Files  that are in .zip Template
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void InsertTemplate(List<ExtractPageInfo> lstPageList, List<TemplateMenuAll> objTemplateMenuall)
    {
        ExtractTemplateController objController = new ExtractTemplateController();
        objController.InsertTemplate(lstPageList, objTemplateMenuall, GetPortalID);
    }
    //Copy files
    public void CopyFiles(string filePath, string templateName)
    {
        string fileDetailPath = filePath + "/Files.xml";
        XmlDocument doc = SageFrame.Templating.xmlparser.XmlHelper.LoadXMLDocument(fileDetailPath);
        XmlNodeList fileList = doc.SelectNodes("files/file");
        foreach (XmlNode file in fileList)
        {
            string filesrcpath = file["src"].InnerText;
            string filedespath = file["destination"].InnerText;
            string tempFolder = appPath + "/Install/Temp/" + templateName + "/" + filesrcpath;
            string descFolder = appPath + "/" + filedespath;
            FileList(file, tempFolder, descFolder);
        }
    }
    public void FileList(XmlNode file, string src, string des)
    {
        XmlNodeList fileList = file.SelectNodes("fileNames/fileName");
        foreach (XmlNode filename in fileList)
        {
            string name = filename.InnerText;
            string source = HttpContext.Current.Server.MapPath(src + "/" + name);
            string destionation = HttpContext.Current.Server.MapPath(des + "/" + name);
            CopyFile(source, destionation);
        }
    }
    public void CopyFile(string source, string destination)
    {
        File.Copy(source, destination, true);
    }
    // extracting the page permission
    public List<PagePermission> PagePermissionList(XmlNode page)
    {
        List<PagePermission> objPagePermissionList = new List<PagePermission>();
        XmlNodeList permissionList = page.SelectNodes("PagePermissions/PagePermission");
        foreach (XmlNode permission in permissionList)
        {
            PagePermission objPagePermission = new PagePermission();
            objPagePermission.PageID = int.Parse(Utils.CleanString(permission["PageID"].InnerText));
            objPagePermission.PermissionID = int.Parse(Utils.CleanString(permission["PermissionID"].InnerText));
            objPagePermission.RoleName = permission["RoleName"].InnerText;
            objPagePermission.AllowAcess = bool.Parse(Utils.CleanString(permission["Allowacess"].InnerText));
            objPagePermission.IsActive = bool.Parse(Utils.CleanString(permission["IsActive"].InnerText));
            objPagePermissionList.Add(objPagePermission);
        }
        return objPagePermissionList;
    }
    // Extracting Menu Details Including setting key and value
    public List<TemplateMenuAll> GetMenuDetail(string filepath)
    {
        string menuDetailPath = filepath + "/MenuDetail.xml";
        DataSet objDatasetMenu = new DataSet();
        objDatasetMenu.ReadXml(menuDetailPath);
        List<TemplateMenuAll> objmenuAll = new List<TemplateMenuAll>();
        List<TemplateMenu> objTemplateMenuList2 = new List<TemplateMenu>();
        List<TemplateMenuSettingValue> objTemplateMenuSettingValueList2 = new List<TemplateMenuSettingValue>();
        string temp = "";
        int rowLength = 0;
        foreach (DataTable dt in objDatasetMenu.Tables)
        {
            rowLength += dt.Rows.Count;
        }
        int count = 1;
        foreach (DataTable dt in objDatasetMenu.Tables)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (temp != Utils.CleanString(row[dt.Columns["MenuName"]].ToString()))
                {
                    TemplateMenuAll objTempMenu = new TemplateMenuAll();
                    objTempMenu.LstTemplateMenu = objTemplateMenuList2;
                    objTempMenu.LstTemplateSetting = objTemplateMenuSettingValueList2;

                    objmenuAll.Add(objTempMenu);
                    objTemplateMenuList2 = new List<TemplateMenu>();
                    objTemplateMenuSettingValueList2 = new List<TemplateMenuSettingValue>();
                    temp = Utils.CleanString(row[dt.Columns["MenuName"]].ToString());
                }
                if (temp == Utils.CleanString(row[dt.Columns["MenuName"]].ToString()))
                {
                    if (row.Table.Columns.Contains("MenuItemID"))
                    {
                        TemplateMenu objTemplateMenu = new TemplateMenu();
                        objTemplateMenu.MenuID = int.Parse(Utils.CleanString(row[dt.Columns["MenuID"]].ToString()));
                        objTemplateMenu.LinkType = int.Parse(Utils.CleanString(row[dt.Columns["LinkType"]].ToString()));
                        objTemplateMenu.PageID = int.Parse(Utils.CleanString(row[dt.Columns["PageID"]].ToString()));
                        objTemplateMenu.Title = row[dt.Columns["Title"]].ToString();
                        objTemplateMenu.LinkURL = row[dt.Columns["LinkURL"]].ToString();
                        objTemplateMenu.ImageIcon = row[dt.Columns["ImageIcon"]].ToString();
                        objTemplateMenu.Caption = row[dt.Columns["Caption"]].ToString();
                        objTemplateMenu.MenuLevel = int.Parse(Utils.CleanString(row[dt.Columns["ParentID"]].ToString()));
                        objTemplateMenu.MenuLevel = int.Parse(Utils.CleanString(row[dt.Columns["MenuLevel"]].ToString()));
                        objTemplateMenu.MenuOrder = int.Parse(Utils.CleanString(row[dt.Columns["MenuOrder"]].ToString()));
                        objTemplateMenu.Isvisible = bool.Parse(Utils.CleanString(row[dt.Columns["IsVisible"]].ToString()));
                        objTemplateMenu.IsActive = bool.Parse(Utils.CleanString(row[dt.Columns["IsActive"]].ToString()));
                        objTemplateMenu.HtmlContent = row[dt.Columns["HtmlContent"]].ToString();
                        objTemplateMenu.MenuName = row[dt.Columns["MenuName"]].ToString();
                        objTemplateMenu.UserModuleID = int.Parse(Utils.CleanString(row[dt.Columns["UserModuleID"]].ToString()));
                        objTemplateMenuList2.Add(objTemplateMenu);
                    }
                    else
                    {
                        TemplateMenuSettingValue objSetting = new TemplateMenuSettingValue();
                        objSetting.MenuMgrSettingValueID = int.Parse(Utils.CleanString(row[dt.Columns["MenuMgrSettingValueID"]].ToString()));
                        objSetting.MenuID = int.Parse(Utils.CleanString(row[dt.Columns["MenuID"]].ToString()));
                        objSetting.MenuName = row[dt.Columns["MenuName"]].ToString();
                        objSetting.SettingKey = row[dt.Columns["SettingKey"]].ToString();
                        objSetting.SettingValue = int.Parse(Utils.CleanString(row[dt.Columns["SettingValue"]].ToString()));
                        objTemplateMenuSettingValueList2.Add(objSetting);
                    }
                }
                temp = Utils.CleanString(row[dt.Columns["MenuName"]].ToString());
                if (count == rowLength)
                {
                    TemplateMenuAll objTempMenu = new TemplateMenuAll();
                    objTempMenu.LstTemplateMenu = objTemplateMenuList2;
                    objTempMenu.LstTemplateSetting = objTemplateMenuSettingValueList2;
                    objmenuAll.Add(objTempMenu);
                }
                count++;
            }
        }
        return objmenuAll;
    }
    // Extratracting ModuleList
    List<ExtractModuleInfo> ModuleList(XmlNode page, DataSet objDataset)
    {
        List<ExtractModuleInfo> lstModules = new List<ExtractModuleInfo>();
        XmlNodeList modulelist = page.SelectNodes("Module");
        foreach (XmlNode module in modulelist)
        {
            ExtractModuleInfo m = new ExtractModuleInfo();
            m.ModuleID = int.Parse(Utils.CleanString(module["ModuleID"].InnerText));
            m.FriendlyName = Utils.CleanString(module["FriendlyName"].InnerText);
            m.Description = Utils.CleanString(module["Description"].InnerText);
            m.Version = Utils.CleanString(module["Version"].InnerText);
            m.IsPremium = bool.Parse(Utils.CleanString(module["IsPremium"].InnerText));
            m.IsAdmin = bool.Parse(Utils.CleanString(module["IsAdmin"].InnerText));
            m.IsRequired = bool.Parse(Utils.CleanString(module["IsRequired"].InnerText));
            m.BusinessControllerClass = Utils.CleanString(module["BusinessControllerClass"].InnerText);
            m.FolderName = Utils.CleanString(module["FolderName"].InnerText);
            m.ModuleName = Utils.CleanString(module["ModuleName"].InnerText);
            m.SupportedFeatures = int.Parse(Utils.CleanString(module["SupportedFeatures"].InnerText));
            m.CompatibleVersions = Utils.CleanString(module["CompatibleVersions"].InnerText);
            m.Dependencies = Utils.CleanString(module["Dependencies"].InnerText);
            m.PackageID = int.Parse(Utils.CleanString(module["PackageID"].InnerText));
            m.IsActive = bool.Parse(Utils.CleanString(module["IsActive"].InnerText));
            m.ModuleDef = ModuleDef(module["ModuleDef"], objDataset);
            lstModules.Add(m);
        }
        return lstModules;
    }
    ExtractModuleDefInfo ModuleDef(XmlNode module, DataSet objDataset)
    {
        ExtractModuleDefInfo moduledef = new ExtractModuleDefInfo();
        moduledef.ModuleDefID = int.Parse(Utils.CleanString(module["ModuleDefID"].InnerText));
        moduledef.FriendlyName = Utils.CleanString(module["FriendlyName"].InnerText);
        moduledef.ModuleID = int.Parse(Utils.CleanString(module["ModuleID"].InnerText));
        moduledef.DefaultCacheTime = int.Parse(Utils.CleanString(module["DefaultCacheTime"].InnerText));
        moduledef.UserModule = UserModule(module["UserModule"], objDataset);
        return moduledef;
    }
    ExtractUserModule UserModule(XmlNode usermodule, DataSet objDataset)
    {
        ExtractUserModule um = new ExtractUserModule();
        um.PageID = int.Parse(Utils.CleanString(usermodule["PageID"].InnerText));
        um.UserModuleId = int.Parse(Utils.CleanString(usermodule["UserModuleId"].InnerText));
        um.PaneName = Utils.CleanString(usermodule["PaneName"].InnerText);
        um.ModuleOrder = int.Parse(Utils.CleanString(usermodule["ModuleOrder"].InnerText));
        um.IsActive = bool.Parse(Utils.CleanString(usermodule["IsActive"].InnerText));
        um.ShowInPages = Utils.CleanString(usermodule["ShowInPages"].InnerText);
        um.AllPages = bool.Parse(Utils.CleanString(usermodule["AllPages"].InnerText));
        um.Footer = Utils.CleanString(usermodule["Footer"].InnerText);
        um.Header = Utils.CleanString(usermodule["Header"].InnerText);
        um.HeaderText = Utils.CleanString(usermodule["HeaderText"].InnerText);
        um.InheritViewPermissions = bool.Parse(Utils.CleanString(usermodule["InheritViewPermissions"].InnerText));
        um.IsHandheld = bool.Parse(Utils.CleanString(usermodule["IsHandheld"].InnerText));
        um.IsInAdmin = bool.Parse(Utils.CleanString(usermodule["IsInAdmin"].InnerText));
        um.ModuleOrder = int.Parse(Utils.CleanString(usermodule["ModuleOrder"].InnerText));
        um.SEOName = Utils.CleanString(usermodule["SEOName"].InnerText);
        um.ShowHeaderText = bool.Parse(Utils.CleanString(usermodule["ShowHeaderText"].InnerText));
        um.SuffixClass = usermodule["SuffixClass"].InnerText;
        um.UserModuleTitle = Utils.CleanString(usermodule["UserModuleTitle"].InnerText);
        um.TemplatePermission = Permission(usermodule);
        string query = "";
        um.Level = 1;
        foreach (DataTable dt in objDataset.Tables)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row[dt.Columns["ReferenceTable"]].ToString().Trim().Length == 0)
                {
                    if (row[dt.Columns["ToRefer"]].ToString().Trim().Length == 0)
                    {
                        if (row[dt.Columns["UserModuleID"]].ToString() == um.UserModuleId.ToString())
                        {
                            string columnName = "";
                            string value = "";
                            string tempValue = "";
                            string tableName = (row[dt.Columns["TableName"]].ToString());
                            foreach (DataColumn col in dt.Columns.Cast<DataColumn>().Skip(1))
                            {
                                if (col.ColumnName != "HTMLTextID" && col.ColumnName != "LogoID" && col.ColumnName != "ReferenceTable" && col.ColumnName != "ToRefer")
                                {
                                    if (col.ColumnName == "UserModuleID")
                                    {
                                        value += "##userModuleID,";
                                        columnName += col.ColumnName + ",";
                                    }
                                    else
                                    {
                                        string ColValue = row[col].ToString();
                                        int Num;
                                        bool isNum = int.TryParse(ColValue, out Num);
                                        if (isNum)
                                            value += ColValue + ",";
                                        else
                                        {
                                            DateTime datetime;
                                            if (DateTime.TryParse(ColValue, out datetime))
                                                value += datetime.Date.ToShortDateString() + ",";
                                            else
                                            {
                                                tempValue = CleanString(ColValue);
                                                value += "'" + tempValue + "',";
                                            }
                                        }
                                        columnName += col.ColumnName + ",";
                                    }
                                }
                            }
                            value = value.Remove(value.Length - 1, 1);
                            columnName = columnName.Remove(columnName.Length - 1, 1);
                            query += "Insert Into " + tableName + "(" + columnName + ")  Values(" + value + ") ";
                        }
                    }
                    else
                    {
                        if (row[dt.Columns["UserModuleID"]].ToString() == um.UserModuleId.ToString())
                        {
                            string columnName = "";
                            string value = "";
                            string additionalQuery = "";
                            string tempValue = "";
                            string tableName = row[dt.Columns["TableName"]].ToString();
                            foreach (DataColumn col in dt.Columns.Cast<DataColumn>().Skip(1))
                            {
                                if (col.ColumnName != "BannerID" && col.ColumnName != "ReferenceTable")
                                {
                                    if (col.ColumnName == "UserModuleID")
                                    {
                                        value += "##userModuleID,";
                                        columnName += col.ColumnName + ",";
                                    }
                                    else if (col.ColumnName == "ToRefer")
                                    {
                                        additionalQuery += SecondLevel(objDataset, row[dt.Columns["TableName"]].ToString(), um.UserModuleId.ToString(), row[dt.Columns["BannerID"]].ToString());
                                        um.Level = 2;
                                    }
                                    else
                                    {
                                        string ColValue = row[col].ToString();
                                        int Num;
                                        bool isNum = int.TryParse(ColValue, out Num);
                                        if (isNum)
                                            value += ColValue + ",";
                                        else
                                        {
                                            DateTime datetime;
                                            if (DateTime.TryParse(ColValue, out datetime))
                                                value += datetime.Date.ToShortDateString() + ",";
                                            else
                                            {
                                                tempValue = CleanString(ColValue);
                                                value += "'" + tempValue + "',";
                                            }
                                        }
                                        columnName += col.ColumnName + ",";
                                    }
                                }
                            }
                            value = value.Remove(value.Length - 1, 1);
                            columnName = columnName.Remove(columnName.Length - 1, 1);
                            query += "^ declare @tempID int Insert Into " + tableName + "(" + columnName + ")  Values(" + value + ") set @tempID = SCOPE_IDENTITY() select @tempID as TempID";
                            query += additionalQuery;
                        }
                    }
                }
            }
        }
        um.Query = query;
        return um;
    }
    //Variables to insert the usermoduleId  and Setting Id so as ensure that they donot repeat
    List<string> listSettingID = new List<string>();
    List<string> lstImageID = new List<string>();

    // this is for second level query: query that needs  insert setting key value as well
    public string SecondLevel(DataSet objDataset, string referTable, string userModuleID, string bannerID)
    {
        string query = "";
        string tableName = "";
        foreach (DataTable dt in objDataset.Tables)
        {
            foreach (DataRow row in dt.Rows)
            {
                tableName = row[dt.Columns["TableName"]].ToString();
                if (row[dt.Columns["ReferenceTable"]].ToString() == referTable && row[dt.Columns["ToRefer"]].ToString() == referTable)
                {
                    //This is for Setting query
                    bool isSetting = false;
                    foreach (string settingID in listSettingID)
                    {
                        if (settingID == row[dt.Columns["SageBannerSettingValueID"]].ToString())
                            isSetting = true;
                    }
                    if (isSetting == false)
                    {
                        if (row[dt.Columns["UserModuleID"]].ToString() == userModuleID)
                        {
                            string value = "";
                            string tempValue = "";
                            string columnName = "";
                            foreach (DataColumn col in dt.Columns.Cast<DataColumn>().Skip(1))
                            {
                                if (col.ColumnName != "ReferenceID" && col.ColumnName != "ToRefer" && col.ColumnName != "ReferenceTable" && col.ColumnName != "SageBannerSettingValueID")
                                {
                                    if (col.ColumnName == "UserModuleID")
                                    {
                                        value += "##userModuleID,";
                                        columnName += col.ColumnName + ",";
                                    }
                                    else
                                    {
                                        string ColValue = row[col].ToString();
                                        int Num;
                                        bool isNum = int.TryParse(ColValue, out Num);
                                        if (isNum)
                                            value += ColValue + ",";
                                        else
                                        {
                                            DateTime datetime;
                                            if (DateTime.TryParse(ColValue, out datetime))
                                                value += datetime.Date.ToShortDateString() + ",";
                                            else
                                            {
                                                tempValue = CleanString(ColValue);
                                                value += "'" + tempValue + "',";
                                            }
                                        }
                                        columnName += col.ColumnName + ",";
                                    }
                                }
                            }
                            listSettingID.Add(row[dt.Columns["SageBannerSettingValueID"]].ToString());
                            value = value.Remove(value.Length - 1, 1);
                            columnName = columnName.Remove(columnName.Length - 1, 1);
                            query += " ! Insert Into " + tableName + "(" + columnName + ")  Values(" + value + ")";
                        }
                    }
                }
                else if (row[dt.Columns["ReferenceTable"]].ToString().Trim().Length != 0 && row[dt.Columns["ReferenceTable"]].ToString() == referTable && row[dt.Columns["UserModuleID"]].ToString() == userModuleID && row[dt.Columns["BannerID"]].ToString() == bannerID)
                {
                    bool isSetting = false;
                    foreach (string settingID in lstImageID)
                    {
                        if (settingID == row[dt.Columns["ImageID"]].ToString())
                            isSetting = true;
                    }
                    if (isSetting == false)
                    {
                        string value = "";
                        string columnName = "";
                        string tempValue = "";
                        foreach (DataColumn col in dt.Columns.Cast<DataColumn>().Skip(1))
                        {
                            if (col.ColumnName != "ImageID" && col.ColumnName != "ReferenceID" && col.ColumnName != "ToRefer" && col.ColumnName != "ReferenceTable")
                            {
                                if (col.ColumnName == "UserModuleID")
                                {
                                    value += "##userModuleID,";
                                    columnName += col.ColumnName + ",";
                                }
                                else if (col.ColumnName == "BannerID")
                                {
                                    value += "@tempID,";
                                    columnName += col.ColumnName + ",";
                                }
                                else
                                {
                                    string ColValue = row[col].ToString();
                                    int Num;
                                    bool isNum = int.TryParse(ColValue, out Num);
                                    if (isNum)
                                        value += ColValue + ",";
                                    else
                                    {
                                        DateTime datetime;
                                        if (DateTime.TryParse(ColValue, out datetime))
                                            value += datetime.Date.ToShortDateString() + ",";
                                        else
                                        {
                                            tempValue = CleanString(ColValue);
                                            value += "'" + tempValue + "',";
                                        }
                                    }
                                    columnName += col.ColumnName + ",";
                                }
                            }
                        }
                        lstImageID.Add(row[dt.Columns["ImageID"]].ToString());
                        value = value.Remove(value.Length - 1, 1);
                        columnName = columnName.Remove(columnName.Length - 1, 1);
                        query += " ! Insert Into " + tableName + "(" + columnName + ")  Values(" + value + ")";
                    }
                }
            }
        }
        return query;
    }
    // Cleaning the  html string  while getting inserting data : 
    public static string CleanString(string value)
    {
        value = Regex.Replace(value, "'", "");// removing ' sigh because it will break the query while executing in storeProcedure.
        return value;
    }
    List<TemplatePermission> Permission(XmlNode usermodule)
    {
        List<TemplatePermission> permissionList = new List<TemplatePermission>();
        XmlNodeList PermissionList = usermodule.SelectNodes("Permission");
        foreach (XmlNode PermissionNode in PermissionList)
        {
            TemplatePermission objPermission = new TemplatePermission();
            objPermission.AllowAccess = int.Parse(Utils.CleanString(PermissionNode["AllowAccess"].InnerText));
            objPermission.UserModuleID = int.Parse(Utils.CleanString(PermissionNode["UserModuleID"].InnerText));
            objPermission.RoleName = PermissionNode["RoleName"].InnerText;
            objPermission.PermissionID = int.Parse(Utils.CleanString(PermissionNode["PermissionID"].InnerText));
            permissionList.Add(objPermission);
        }
        return permissionList;
    }
}
