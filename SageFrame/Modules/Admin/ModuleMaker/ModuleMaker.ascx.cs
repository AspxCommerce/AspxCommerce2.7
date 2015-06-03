using Microsoft.CSharp;
using RegisterModule;
using SageFrame.Core;
using SageFrame.Modules;
using SageFrame.Packages;
using SageFrame.SageFrameClass.Services;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Modules_Admin_ModuleMaker : BaseAdministrationUserControl
{
    public int userModuleID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        IncludeJs("One", "/Modules/Admin/ModuleMaker/js/ModuleMaker.js");
        IncludeCss("css", "/Modules/Admin/ModuleMaker/css/module.css");
        userModuleID = int.Parse(SageUserModuleID);
        //CreateModule();
        GetTableList();
        GetModuleNameFromFolder();
        GetModuleNameFromDataBase();
    }
    public void GetModuleNameFromDataBase()
    {
        List<PackagesInfo> objPackageList = PackagesController.GetPackagesByPortalID(GetPortalID, string.Empty);
        StringBuilder packageName = new StringBuilder();
        foreach (PackagesInfo objPackage in objPackageList)
        {
            packageName.Append(objPackage.FriendlyName.ToLower());
            packageName.Append(",");
        }
        hdnModuleListFromDatabase.Value = packageName.ToString();
    }

    public void GetModuleNameFromFolder()
    {
        StringBuilder moduleName = new StringBuilder();
        string path = HttpContext.Current.Server.MapPath("~/");
        string[] arrFolders = Directory.GetDirectories(path + "Modules");
        foreach (string strFolder in arrFolders)
        {
            string folderName = strFolder.Remove(0, (path + "Modules\\").Length);
            string[] arrSubFolders = Directory.GetDirectories(path + "Modules\\" + folderName);
            if (arrSubFolders.Length > 0)
            {
                foreach (string strSubFolder in arrSubFolders)
                {
                    string subFolderName = strSubFolder.Remove(0, (path + "Modules\\" + folderName + "\\").Length);
                    moduleName.Append(subFolderName.ToLower());
                    moduleName.Append(",");
                }
            }
            moduleName.Append(folderName.ToLower());
            moduleName.Append(",");
        }
        hdnModuleList.Value = moduleName.ToString();
    }
    public void GetTableList()
    {
        ModuleController objModuleController = new ModuleController();
        List<KeyValue> objTableList = objModuleController.GetBasicTableName();
        StringBuilder tables = new StringBuilder();
        foreach (KeyValue objKey in objTableList)
        {
            tables.Append(objKey.Value.ToLower());
            tables.Append(",");
        }
        hdnTableList.Value = tables.ToString();
    }

    public void CreateModule(bool adminModule, Dictionary<string, string> columnlist, List<string> storeprocedureList, Dictionary<string, string> updateList, bool autoIncrement)
    {
        string moduleName = txtModuleName.Text;
        string moduleFolderPath = "/Modules/";
        if (adminModule)
        {
            moduleFolderPath += "Admin/";
        }
        //Generates the library files: Info , Controller and the dataprovider and compiled it.
        bool isNewModule = true;
        bool compilationsuccess = GenerateandCompileLibrary(moduleFolderPath, moduleName, columnlist, updateList, isNewModule, autoIncrement);
        if (compilationsuccess)
        {
            string moduleDescription = txtModuleDescription.Text;
            bool includeCSS = chkCss.Checked;
            bool includeJS = chkJS.Checked;
            bool includeEditModule = chkEdit.Checked;
            bool includeSettingModule = chkSetting.Checked;
            bool includeWebService = chkWebService.Checked;

            string baseFolderPath = moduleFolderPath + moduleName;
            string folderpath = Server.MapPath(baseFolderPath);
            ModuleHelper.CreateDirectory(folderpath);

            string filePathFront = Server.MapPath("/Modules/Admin/ModuleMaker/RawModule/ModuleName.ascx");
            string filePathBehind = Server.MapPath("/Modules/Admin/ModuleMaker/RawModule/ModuleName.ascx.cs");
            string fileNameFrontEdit = Server.MapPath("/Modules/Admin/ModuleMaker/RawModule/ModuleNameEdit.ascx");
            string fileNameBehindEdit = Server.MapPath("/Modules/Admin/ModuleMaker/RawModule/ModuleNameEdit.ascx.cs");
            string fileNameFrontSetting = Server.MapPath("/Modules/Admin/ModuleMaker/RawModule/ModuleNameSetting.ascx");
            string fileNameBehindSetting = Server.MapPath("/Modules/Admin/ModuleMaker/RawModule/ModuleNameSetting.ascx.cs");

            //Creates file for View and its compulsory
            CreateFile(filePathFront, folderpath, moduleName, ".ascx", baseFolderPath, false, false);
            CreateFile(filePathBehind, folderpath, moduleName, ".ascx.cs", baseFolderPath, includeCSS, includeJS);
            //Creates file for Edit if the user wants
            if (includeEditModule)
            {
                CreateFile(fileNameFrontEdit, folderpath, moduleName + "Edit", ".ascx", baseFolderPath, false, includeJS);
                CreateFile(fileNameBehindEdit, folderpath, moduleName + "Edit", ".ascx.cs", baseFolderPath, includeCSS, includeJS);
            }
            //Creates file for Settings if the user wants
            if (includeSettingModule)
            {
                CreateFile(fileNameFrontSetting, folderpath, moduleName + "Setting", ".ascx", baseFolderPath, includeCSS, false);
                CreateFile(fileNameBehindSetting, folderpath, moduleName + "Setting", ".ascx.cs", baseFolderPath, includeCSS, false);
            }
            //Creates css file for the module if the user wants
            if (includeCSS)
            {
                CreateCssFile(moduleFolderPath, moduleName, ".css");
            }
            //Creates Js file for the module if the user wants
            if (includeJS)
            {
                string jsRawFile = Server.MapPath("/Modules/Admin/ModuleMaker/RawModule/js/module.js");
                CreateJsFile(jsRawFile, folderpath, moduleName, ".js", baseFolderPath);
                CreateJsFile(jsRawFile, folderpath, moduleName + "Edit", ".js", baseFolderPath);
                //Creates webservice file for the module if the user wants
                if (includeWebService)
                {
                    string webServiceRawFile = Server.MapPath("/Modules/Admin/ModuleMaker/RawModule/webservice/WebService.asmx");
                    CreateWebserviceFile(webServiceRawFile, folderpath, moduleName, ".asmx", baseFolderPath, columnlist);
                }
            }
            //SQL file write 
            string procedure = SqlFileCreate(moduleFolderPath, moduleName, storeprocedureList);
            RegisterModule(moduleName, moduleFolderPath.Remove(0, 1), procedure);
        }
    }

    public string SqlFileCreate(string moduleFolderPath, string moduleName, List<string> storeprocedureList)
    {
        string SqlFilePath = Server.MapPath(moduleFolderPath + moduleName + "/Script");
        ModuleHelper.CreateDirectory(SqlFilePath);
        string sqldestinationPath = SqlFilePath + "/" + moduleName + ".sql";
        StringBuilder procedure = new StringBuilder();
        using (StreamWriter sw = new StreamWriter(sqldestinationPath))
        {
            foreach (string storeprocedure in storeprocedureList)
            {
                procedure.Append("GO");
                procedure.Append(Environment.NewLine);
                procedure.Append(storeprocedure);
                procedure.Append(Environment.NewLine);
            }
            sw.Write(procedure.ToString());
        }
        return procedure.ToString();
    }

    public bool GenerateandCompileLibrary(string moduleFolderPath, string moduleName, Dictionary<string, string> columnlist, Dictionary<string, string> updateList, bool isNewModule, bool autoIncrement)
    {
        string filePathInfo = Server.MapPath("/Modules/Admin/ModuleMaker/RawModule/library/ModuleInfo.cs");
        string filePathController = Server.MapPath("/Modules/Admin/ModuleMaker/RawModule/library/ModuleController.cs");
        string filePathDataProvider = Server.MapPath("/Modules/Admin/ModuleMaker/RawModule/library/ModuleDataProvider.cs");
        string bigCode = string.Empty;
        string code = string.Empty;
        bool isCompiled = false;
        try
        {
            string libraryPath = Server.MapPath(moduleFolderPath + moduleName + "/SageFrame." + moduleName);
            ModuleHelper.CreateDirectory(libraryPath);
            string infoPath = libraryPath + "/Info";
            ModuleHelper.CreateDirectory(infoPath);
            string destinationPath = infoPath + "/" + moduleName + "Info.cs";
            using (StreamWriter sw = new StreamWriter(destinationPath))
            {
                using (StreamReader rdr = new StreamReader(filePathInfo))
                {
                    code = rdr.ReadToEnd();
                }
                StringBuilder html = new StringBuilder();
                foreach (KeyValuePair<string, string> datatype in columnlist)
                {
                    html.Append(ModuleHelper.SageProp(datatype.Value, datatype.Key));
                }
                code = code.Replace("//properties", html.ToString());
                string references = "using System;";
                references = "using System;\nusing SageFrame.Web.Utilities;\nusing System.Collections.Generic;\n";
                code = code.Replace("//references", references);
                code = code.Replace("ModuleName", moduleName);
                sw.Write(code);
                bigCode = code;
            }
            string contollerPath = libraryPath + "/Controller";
            ModuleHelper.CreateDirectory(contollerPath);
            destinationPath = contollerPath + "/" + moduleName + "Controller.cs";
            using (StreamWriter sw = new StreamWriter(destinationPath))
            {
                using (StreamReader rdr = new StreamReader(filePathController))
                {
                    code = rdr.ReadToEnd();
                }
                string controllerCode = ModuleHelper.ControllerCode(columnlist, moduleName).ToString();
                code = code.Replace("//properties", controllerCode);
                code = code.Replace("ModuleName", moduleName);
                bigCode += "\n" + code;
                string references = "using System; using System.Collections.Generic;";
                code = code.Replace("//references", references);
                sw.Write(code);
            }
            string dataProviderPath = libraryPath + "/DataProvider";
            ModuleHelper.CreateDirectory(dataProviderPath);
            destinationPath = dataProviderPath + "/" + moduleName + "DataProvider.cs";
            using (StreamWriter sw = new StreamWriter(destinationPath))
            {
                using (StreamReader rdr = new StreamReader(filePathDataProvider))
                {
                    code = rdr.ReadToEnd();
                }
                string dataProviderCode = ModuleHelper.DataProviderCode(columnlist, moduleName, updateList, autoIncrement).ToString();
                code = code.Replace("//properties", dataProviderCode);
                code = code.Replace("ModuleName", moduleName);
                bigCode += "\n" + code;
                string references = "using System;\n using SageFrame.Web.Utilities;\n using System.Collections.Generic;\n";
                code = code.Replace("//references", references);
                sw.Write(code);
            }
        }
        catch (Exception ex)
        {
            ShowMessage("", ex.ToString(), "", SageMessageType.Error);
            ProcessException(ex);
        }
        if (isNewModule)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            //CSharpCodeProvider csp = new CSharpCodeProvider();
            // ICodeCompiler cc = provider.CreateCompiler();
            CompilerParameters cp = new CompilerParameters();
            string OutputAssembly = Path.Combine(Server.MapPath("/bin/"), "SageFrame." + moduleName + ".dll");
            cp.OutputAssembly = OutputAssembly;
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add(AppDomain.CurrentDomain.BaseDirectory + "bin\\SageFrame.Common.dll");
            cp.WarningLevel = 3;
            //cp.CompilerOptions = "/target:library /optimize";
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = false;
            System.CodeDom.Compiler.TempFileCollection tfc = new TempFileCollection(GetApplicationName, false);
            CompilerResults cr = new CompilerResults(tfc);
            cr = provider.CompileAssemblyFromSource(cp, bigCode);
            if (cr.Errors.Count > 0)
            {
                string error = string.Empty;
                foreach (CompilerError ce in cr.Errors)
                {
                    error += ce.ErrorNumber + ": " + ce.ErrorText;
                }
                ShowMessage("", error, "", SageMessageType.Error);
                isCompiled = false;
            }
            else
            {
                isCompiled = true;
            }
        }
        return isCompiled;
    }

    public void RegisterModule(string moduleName, string moduleFolderPath, string procedure)
    {
        ModuleInfo objModule = new ModuleInfo();
        int _newModuleID = 0;
        int _newModuleDefID = 0;
        int _newPortalmoduleID = 0;
        string Exceptions = string.Empty;
        string ExtensionMessage = string.Empty;
        try
        {
            //add module and package tables
            objModule.ModuleName = moduleName;
            objModule.Name = moduleName;
            objModule.FriendlyName = moduleName;
            objModule.Description = txtModuleDescription.Text;
            objModule.FolderName = moduleName;
            objModule.Version = "01.00.00";//ddlFirst.SelectedValue + "." + ddlSecond.SelectedValue + "." + ddlLast.SelectedValue;//"01.00.00"; //new Version(1, 0, 0);
            objModule.Owner = "";
            objModule.Organization = "";
            objModule.URL = "";
            objModule.Email = "";
            objModule.ReleaseNotes = "";
            objModule.License = "";
            objModule.PackageType = "Module";
            objModule.isPremium = true;
            objModule.supportedFeatures = 0;
            objModule.BusinessControllerClass = "";
            objModule.CompatibleVersions = "";
            objModule.dependencies = "";
            objModule.permissions = "";
            ModuleController objController = new ModuleController();

            int[] outputValue;
            outputValue = objController.AddModules(objModule, false, 0, true, DateTime.Now, GetPortalID, GetUsername);
            objModule.ModuleID = outputValue[0];
            objModule.ModuleDefID = outputValue[1];
            _newModuleID = objModule.ModuleID;
            _newModuleDefID = objModule.ModuleDefID;

            //insert into ProtalModule table
            _newPortalmoduleID = objController.AddPortalModules(GetPortalID, _newModuleID, true, DateTime.Now, GetUsername);

            //install permission for the installed module in ModuleDefPermission table with ModuleDefID and PermissionID

            // get the default module VIEW permissions
            int _permissionIDView = objController.GetPermissionByCodeAndKey("SYSTEM_VIEW", "VIEW");
            objController.AddModulePermission(_newModuleDefID, _permissionIDView, GetPortalID, _newPortalmoduleID, true, GetUsername, true, DateTime.Now, GetUsername);
            int _permissionIDEdit = objController.GetPermissionByCodeAndKey("SYSTEM_EDIT", "EDIT");
            objController.AddModulePermission(_newModuleDefID, _permissionIDEdit, GetPortalID, _newPortalmoduleID, true, GetUsername, true, DateTime.Now, GetUsername);

            RegisterControl(moduleName, moduleFolderPath + moduleName + "/" + moduleName + ".ascx", 1, _newModuleDefID);
            if (chkEdit.Checked)
            {
                RegisterControl(moduleName, moduleFolderPath + moduleName + "/" + moduleName + "Edit" + ".ascx", 2, _newModuleDefID);
            }
            if (chkSetting.Checked)
            {
                RegisterControl(moduleName, moduleFolderPath + moduleName + "/" + moduleName + "Setting" + ".ascx", 3, _newModuleDefID);
            }
            if (Exceptions != string.Empty)
            {
                if (objModule.ModuleID > 0 && _newModuleDefID > 0)
                {
                    Installers install = new Installers();
                    //Delete Module info from data base
                    install.ModulesRollBack(objModule.ModuleID, GetPortalID);
                }
            }
            else
            {
                SQLHandler sageSQLHandler = new SQLHandler();
                sageSQLHandler.ExecuteScript(procedure, true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage("", ex.ToString(), "", SageMessageType.Error);
            ProcessException(ex);
        }
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    public string RegisterControl(string moduleName, string _moduleControlSrc, int _controlType, int _newModuleDefID)
    {
        string extensionMessage = string.Empty;
        //Logic for modulecontrol installation
        string _moduleControlKey = string.Empty;
        string _moduleControlHelpUrl = "";
        bool _moduleSupportsPartialRendering = false;

        switch (_controlType)
        {
            case 1:
                _moduleControlKey = moduleName + "View";
                break;
            case 2:
                _moduleControlKey = moduleName + "Edit";
                break;
            case 3:
                _moduleControlKey = moduleName + "Setting";
                break;
        }
        string _moduleControlTitle = _moduleControlKey;
        string _iconFile = "";
        int _displayOrder = 0;//int.Parse(txtDisplayOrder.Text);
        ModuleController objController = new ModuleController();
        objController.AddModuleCoontrols(_newModuleDefID, _moduleControlKey, _moduleControlTitle, _moduleControlSrc,
            _iconFile, _controlType, _displayOrder, _moduleControlHelpUrl, _moduleSupportsPartialRendering, true, DateTime.Now,
            GetPortalID, GetUsername);
        extensionMessage = GetSageMessage("Extensions", "ModuleExtensionIsAddedSuccessfully");
        return extensionMessage;
    }




    public void CreateFile(string filePath, string folderPath, string moduleName, string extension, string baseFolderPath, bool includeCss, bool includeJs)
    {
        if (File.Exists(filePath))
        {
            try
            {
                string destinationPath = folderPath + "/" + moduleName + extension;
                string code = string.Empty;
                using (StreamWriter sw = new StreamWriter(destinationPath))
                {
                    using (StreamReader rdr = new StreamReader(filePath))
                    {
                        code = rdr.ReadToEnd();
                    }
                    string includeScript = string.Empty;
                    StringBuilder calltoJsfile = new StringBuilder();
                    if (includeCss)
                    {
                        includeScript += "\n\t\tIncludeCss(\"" + moduleName + "css\", \"" + baseFolderPath + "/css/module.css\");";
                    }
                    if (includeJs)
                    {
                        includeScript += "\n\t\tIncludeJs(\"" + moduleName + "js\", \"" + baseFolderPath + "/js/" + moduleName + ".js\");";
                        calltoJsfile.Append("<script type=\"text/javascript\">");
                        calltoJsfile.Append("\n\t//<![CDATA[");
                        calltoJsfile.Append("\n\t$(function () { $(this).ModuleName({ UserModuleID: '<%=userModuleID%>' }); });");
                        calltoJsfile.Append("\n\t//]]>	");
                        calltoJsfile.Append("\n</script>");
                        code = code.Replace("calltoJsfile", calltoJsfile.ToString());
                    }
                    if (includeScript != string.Empty)
                    {
                        code = code.Replace("//includeExternalfiles", includeScript);
                    }
                    code = code.Replace("ModuleName", moduleName);
                    code = code.Replace("Usercontrollocation", baseFolderPath + "/" + moduleName + extension);
                    sw.Write(code);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("", ex.ToString(), "", SageMessageType.Error);
                ProcessException(ex);
            }
        }
    }

    public void CreateCssFile(string folderPath, string moduleName, string extension)
    {
        try
        {
            folderPath = folderPath + moduleName + "/css/";
            folderPath = Server.MapPath(folderPath);
            ModuleHelper.CreateDirectory(folderPath);
            string destinationPath = folderPath + moduleName + extension;
            File.Create(destinationPath);
        }
        catch (Exception ex)
        {
            ShowMessage("", ex.ToString(), "", SageMessageType.Error);
            ProcessException(ex);
        }
    }


    public void CreateJsFile(string filePath, string folderPath, string moduleName, string extension, string baseFolderPath)
    {
        if (File.Exists(filePath))
        {
            try
            {
                string destinationPath = folderPath + "/js/";
                ModuleHelper.CreateDirectory(destinationPath);
                string code = string.Empty;
                destinationPath += moduleName + extension;
                using (StreamWriter sw = new StreamWriter(destinationPath))
                {
                    using (StreamReader rdr = new StreamReader(filePath))
                    {
                        code = rdr.ReadToEnd();
                    }
                    code = code.Replace("ModuleName", moduleName);
                    code = code.Replace("FolderPath", baseFolderPath);
                    sw.Write(code);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("", ex.ToString(), "", SageMessageType.Error);
                ProcessException(ex);
            }
        }
    }
    public void CreateWebserviceFile(string filePath, string folderPath, string moduleName, string extension, string baseFolderPath, Dictionary<string, string> columnlist)
    {
        if (File.Exists(filePath))
        {
            try
            {
                string destinationPath = folderPath + "/Webservice/";
                ModuleHelper.CreateDirectory(destinationPath);
                string code = string.Empty;
                destinationPath += moduleName + extension;
                using (StreamWriter sw = new StreamWriter(destinationPath))
                {
                    using (StreamReader rdr = new StreamReader(filePath))
                    {
                        code = rdr.ReadToEnd();
                    }
                    string webserviceCode = ModuleHelper.WebServiceCode(columnlist, moduleName).ToString();
                    code = code.Replace("ModuleName", moduleName.Replace("Edit", ""));
                    code = code.Replace("//Methodshere", webserviceCode);
                    code = code.Replace("//references", "using SageFrame." + moduleName + ";");
                    sw.Write(code);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("", ex.ToString(), "", SageMessageType.Error);
                ProcessException(ex);
            }
        }
    }

    protected void btnCreateZip_Click(object sender, EventArgs e)
    {
        CreateNewModule(false);
    }
    protected void btnCreateNewModule_Click(object sender, EventArgs e)
    {
        CreateNewModule(true);
    }

    private void CreateNewModule(bool isnewModule)
    {
        string xml = HttpUtility.HtmlDecode(hdnXML.Value);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xml);
        XmlElement root = xmlDoc.DocumentElement;
        XmlNode adminNode = root.SelectSingleNode("isadmin");
        bool isAdmin = adminNode.InnerText.ToString() == "0" ? false : true;

        XmlNode autoincrementNode = root.SelectSingleNode("autoincrement");
        bool autoIncrement = autoincrementNode.InnerText.ToString() == "0" ? false : true;

        Dictionary<string, string> columsList = new Dictionary<string, string>();
        XmlNodeList objXmlList = root.SelectSingleNode("rows").SelectNodes("row");
        foreach (XmlNode xmlnode in objXmlList)
        {
            string properties = xmlnode.SelectSingleNode("properties").InnerText;
            string datatype = xmlnode.SelectSingleNode("datatype").InnerText;
            datatype = ModuleHelper.CsharpDatatype(datatype);
            columsList.Add(properties, datatype);
        }
        List<string> storeProcedureList = new List<string>();
        objXmlList = root.SelectSingleNode("storeProcedures").SelectNodes("storeProcedure");
        foreach (XmlNode xmlnode in objXmlList)
        {
            string storeProcedure = xmlnode.InnerText;
            storeProcedureList.Add(storeProcedure);
        }
        Dictionary<string, string> updateList = new Dictionary<string, string>();
        XmlNodeList objXmlUpdateList = root.SelectSingleNode("updatelist").SelectNodes("row");
        foreach (XmlNode xmlnode in objXmlUpdateList)
        {
            string properties = xmlnode.SelectSingleNode("properties").InnerText;
            string datatype = xmlnode.SelectSingleNode("datatype").InnerText;
            updateList.Add(properties, datatype);
        }
        if (isnewModule)
        {
            CreateModule(isAdmin, columsList, storeProcedureList, updateList, autoIncrement);
        }
        else
        {
            CreateZipFile(columsList, storeProcedureList, updateList, autoIncrement);
        }
    }

    public void CreateZipFile(Dictionary<string, string> columnlist, List<string> storeprocedureList, Dictionary<string, string> updateList, bool autoIncrement)
    {
        string destinationfile = string.Empty;
        try
        {
            string moduleName = txtModuleName.Text;
            string moduleFolderPath = "/Temp/";
            bool compilationsuccess = GenerateandCompileLibrary(moduleFolderPath, moduleName, columnlist, updateList, false, autoIncrement);
            SqlFileCreate(moduleFolderPath, moduleName, storeprocedureList);
            string folderZipPath = Server.MapPath(moduleFolderPath + moduleName + "/");
            destinationfile = moduleFolderPath + moduleName + ".zip";
            string folderDestinationPath = Server.MapPath(destinationfile);
            ZipUtil.ZipFiles(folderZipPath, Server.MapPath(destinationfile), string.Empty);
            Directory.Delete(folderZipPath);
        }
        catch (Exception ex)
        {
            ShowMessage("", ex.ToString(), "", SageMessageType.Error);
            ProcessException(ex);
        }
        if (destinationfile != string.Empty)
        {
            Response.Redirect(GetHostURL() + destinationfile);
        }
    }
}