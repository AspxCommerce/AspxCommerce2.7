/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Collections.Generic;
using SageFrame.Web;
using System.Web;
using System.IO;
using RegisterModule;
using System.Collections;
using System.Xml;
using System.Data;
using SageFrame.Web.Utilities;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using SageFrame.Modules;

namespace AspxCommerce.Core
{

    public class PaymentGatewayInstaller : BaseAdministrationUserControl
    {
        string _exceptions = string.Empty;
        SQLHandler sqlH = new SQLHandler();
        PaymentGateWayModuleInfo module = new PaymentGateWayModuleInfo();
       // int update = 0;
        public enum ControlType
        {
            View = 1,
            Edit = 2,
            Setting = 3
        }
        public PaymentGatewayInstaller()
        {

        }
        public ArrayList Step0CheckLogic(FileUpload fileModule)
        {
            //PaymentGateWayModuleInfo module = new PaymentGateWayModuleInfo();
            
            int returnValue = 0;
            try
            {
                if (fileModule.HasFile)//check for Empty entry
                {
                    if (IsVAlidZipContentType(fileModule.FileName))//Check if valid Zip file submitted
                    {
                        string path = HttpContext.Current.Server.MapPath("~/");
                        string temPath = SageFrame.Common.RegisterModule.Common.TemporaryFolder;
                        string destPath = Path.Combine(path, temPath);
                        if (!Directory.Exists(destPath))
                            Directory.CreateDirectory(destPath);

                        string filePath = destPath + "\\" + fileModule.FileName;
                        fileModule.SaveAs(filePath);
                        string extractedPath = string.Empty;
                        ZipUtil.UnZipFiles(filePath, destPath, ref extractedPath, SageFrame.Common.RegisterModule.Common.Password, SageFrame.Common.RegisterModule.Common.RemoveZipFile);
                        module.TempFolderPath = extractedPath;
                        fileModule.FileContent.Dispose();
                        if (!string.IsNullOrEmpty(module.TempFolderPath) && Directory.Exists(module.TempFolderPath))
                        {
                            switch (Step1CheckLogic(module.TempFolderPath, module))
                            {
                                case 0://No manifest file
                                    DeleteTempDirectory(module.TempFolderPath);
                                    returnValue = 3;
                                    break;
                                case -1://Invalid Manifest file
                                    DeleteTempDirectory(module.TempFolderPath);
                                    returnValue = 4;
                                    break;
                                case 1://Already exist
                                    returnValue = 2;
                                    break;
                                case 2://Fresh Installation
                                    returnValue = 1;
                                    break;
                            }
                        }
                        else
                        {
                            returnValue = 0;
                        }
                    }
                    else
                    {
                        returnValue = -1;//"Invalid Zip file submited to upload!";
                    }
                }
                else
                {
                    returnValue = 0;// "No package file is submited to upload!";
                }
            }
            catch
            {
                returnValue = -1;
            }
            ArrayList arrColl = new ArrayList();
            arrColl.Add(returnValue);
            arrColl.Add(module);
            return arrColl;
        }

        public bool CheckValidManifestFile(XmlElement root, PaymentGateWayModuleInfo module)
        {
            if (root.Name == "sageframe")//need to change the root node for valid manifest file at root node  
            {
                string packageType = root.GetAttribute("type"); //root.NodeType
                //module.PackageType = PackageType;
                switch (packageType.ToLower())
                {
                    //need to check for many cases for like skin /..
                    case "paymentgateway":
                        return true;
                }
            }
            return false;
        }

        public bool IsModuleExist(string moduleName,string folderName,string friendlyName,int storeID,int portalID)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
                SqlCommand sqlCmd = new SqlCommand();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                DataSet sqlDs = new DataSet();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = "[dbo].[usp_Aspx_CheckPaymentGatewayTypeName]";
                sqlCmd.CommandType = CommandType.StoredProcedure;              
                sqlCmd.Parameters.AddWithValue("@PaymentGatewayTypeName", moduleName);
                sqlCmd.Parameters.AddWithValue("@FolderName", folderName);
                sqlCmd.Parameters.AddWithValue("@FriendlyName", friendlyName);
                sqlCmd.Parameters.AddWithValue("@StoreID", storeID);
                sqlCmd.Parameters.AddWithValue("@PortalID", portalID);
                sqlAdapter.SelectCommand = sqlCmd;
                sqlConn.Open();
                SqlDataReader dr = null;
                
                dr = sqlCmd.ExecuteReader();
             
               
                if (dr.Read())
                {
                    module.PaymentGatewayTypeID =int.Parse(dr["PaymentGatewayTypeID"].ToString());
                    return true;                  

                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public int Step1CheckLogic(string tempUnzippedPath, PaymentGateWayModuleInfo module)
        {
            if (CheckFormanifestFile(tempUnzippedPath, module) != "")
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(tempUnzippedPath + '\\' + module.ManifestFile);
                XmlElement root = doc.DocumentElement;
                if (CheckValidManifestFile(root, module))
                {
                    XmlNodeList xnList = doc.SelectNodes("sageframe/folders/folder");
                    foreach (XmlNode xn in xnList)
                    {
                        module.PaymentGatewayTypeName = xn["paymentgatewayname"].InnerXml.ToString();
                        module.FolderName = xn["foldername"].InnerXml.ToString();
                        module.FriendlyName = xn["friendlyname"].InnerXml.ToString();   
                        module.Description = xn["description"].InnerXml.ToString();  
                        module.Version = xn["version"].InnerXml.ToString();  
                        module.Name = xn["name"].InnerXml.ToString();
                        module.StoreID = GetStoreID;//int.Parse(xn["storeid"].InnerXml.ToString());
                        module.PortalID = GetPortalID;//int.Parse(xn["portalid"].InnerXml.ToString());
                        module.CultureName = xn["culturename"].InnerXml.ToString();

                        if (!String.IsNullOrEmpty(module.PaymentGatewayTypeName) && IsModuleExist(module.PaymentGatewayTypeName.ToLower(), module.FolderName.ToString(), module.FriendlyName.ToString(),int.Parse(GetStoreID.ToString()),int.Parse(GetPortalID.ToString())))
                        {
                            string path = HttpContext.Current.Server.MapPath("~/");
                            string targetPath = path + SageFrame.Common.RegisterModule.Common.ModuleFolder + '\\' + module.FolderName;
                            module.InstalledFolderPath = targetPath;
                           // DeleteTempDirectory(tempUnzippedPath);
                            return 1;//Already exist
                        }
                        else
                        {
                            return 2;//Not Exists
                        }
                    }
                }
                else
                {
                    return -1;//Invalid Manifest file
                }
            }
            return 0;//No manifest file
        }

        public string CheckFormanifestFile(string tempUnzippedPath, PaymentGateWayModuleInfo module)
        {
            DirectoryInfo dir = new DirectoryInfo(tempUnzippedPath);
            foreach (FileInfo f in dir.GetFiles("*.*"))
            {
                if (f.Extension.ToLower() == ".sfe")
                {
                    module.ManifestFile = f.Name;
                    return module.ManifestFile;
                }
                else
                {
                    module.ManifestFile = "";
                }
            }
            return module.ManifestFile;
        }

        private bool IsVAlidZipContentType(string p)
        {
            // extract and store the file extension into another variable
            String fileExtension = p.Substring(p.Length - 3, 3);
            // array of allowed file type extensions
            string[] validFileExtensions = { "zip" };
            var flag = false;
            // loop over the valid file extensions to compare them with uploaded file
            for (var index = 0; index < validFileExtensions.Length; index++)
            {
                if (fileExtension.ToLower() == validFileExtensions[index].ToString().ToLower())
                {
                    flag = true;
                }
            }
            return flag;
        }

        private int checkControlType(string controlType)
        {
            int returnValue = 0;
            switch (controlType)
            {
                case "View":
                    returnValue = (int)ControlType.View;
                    break;
                case "Edit":
                    returnValue = (int)ControlType.Edit;
                    break;
                case "Setting":
                    returnValue = (int)ControlType.Setting;
                    break;
                default:
                    returnValue = 0;
                    break;
            }
            return returnValue;
        }

        public void PaymentGatewayRollBack(int paymentGatewayTypeID, int portalID, int storeID)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = "usp_Aspx_PaymentGatewayRollBack";
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add(new SqlParameter("@PaymentGatewayTypeID", paymentGatewayTypeID));
                sqlCmd.Parameters.Add(new SqlParameter("@PortalID", portalID));
                sqlCmd.Parameters.Add(new SqlParameter("@StoreID", storeID));
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception e)
            {
                ProcessException(e);
            }
        }

        public string ReadSQLFile(string tempUnzippedPath, string sqlProvidername)
        {
            string exceptions = string.Empty;
            try
            {
                StreamReader objReader = new StreamReader(tempUnzippedPath + '\\' + sqlProvidername);
                string sLine = "";
                string scriptDetails = "";
                ArrayList arrText = new ArrayList();

                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        arrText.Add(sLine);
                }
                objReader.Close();
                foreach (string sOutput in arrText)
                {
                    scriptDetails += sOutput + "\r\n";
                }
                SQLHandler sqlHandler = new SQLHandler();
                exceptions = sqlHandler.ExecuteScript(scriptDetails, true);
            }
            catch (Exception ex)
            {
                exceptions += ex.Message.ToString();
            }
            return exceptions;
        }

        public ModuleInfo FillModuleInfo(ModuleInfo module)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(module.TempFolderPath + '\\' + module.ManifestFile);
            XmlElement root = doc.DocumentElement;
            if (!String.IsNullOrEmpty(root.ToString()))
            {
                XmlNodeList xnList = doc.SelectNodes("sageframe/folders/folder");
                foreach (XmlNode xn in xnList)
                {
                    module.ModuleName = xn["modulename"].InnerXml.ToString();
                    module.Name = xn["name"].InnerXml.ToString();
                    module.FriendlyName = xn["friendlyname"].InnerXml.ToString();
                    module.Description = xn["description"].InnerXml.ToString();
                    module.Version = xn["version"].InnerXml.ToString();
                    module.BusinessControllerClass = xn["businesscontrollerclass"].InnerXml.ToString();
                    module.FolderName = xn["foldername"].InnerXml.ToString();
                    module.CompatibleVersions = xn["compatibleversions"].InnerXml.ToString();
                    module.Owner = xn["owner"].InnerXml.ToString();
                    module.Organization = xn["organization"].InnerXml.ToString();
                    module.URL = xn["url"].InnerXml.ToString();
                    module.Email = xn["email"].InnerXml.ToString();
                    module.ReleaseNotes = xn["releasenotes"].InnerXml.ToString();
                    module.License = xn["license"].InnerXml.ToString();
                    module.PackageType = "Module";
                }
            }
            return module;
        }

        public void InstallPackage(PaymentGateWayModuleInfo module,int update)
        {
            ModuleController moduleCtr = new ModuleController();
            XmlDocument doc = new XmlDocument();
            ArrayList dllFiles = new ArrayList();
            string unistallScriptFile = string.Empty;
            doc.Load(module.TempFolderPath + '\\' + module.ManifestFile);
            XmlElement root = doc.DocumentElement;
            if (!String.IsNullOrEmpty(root.ToString()))
            {
                XmlNodeList xnList = doc.SelectNodes("sageframe/folders/folder");
                foreach (XmlNode xn in xnList)
                {
                    #region Module Exist check
                    try
                    {
                        System.Nullable<Int32> newModuleID = 0;
                        System.Nullable<Int32> newModuleDefID = 0;
                        System.Nullable<Int32> newPortalmoduleID = 0;
                        //System.Nullable<Int32> _newPortalmoduleID = 0;
                        //System.Nullable<Int32> _newModuleDefPermissionID = 0;
                        //System.Nullable<Int32> _newPortalModulePermissionID = 0;
                        #region "Module Creation Logic"
                        SQLHandler sqhl = new SQLHandler();
                        SqlConnection sqlConn = new SqlConnection(SystemSetting.SageFrameConnectionString);
                        SqlCommand sqlCmd = new SqlCommand();
                        int ReturnValue = -1;
                        sqlCmd.Connection = sqlConn;
                        sqlCmd.CommandText = "[dbo].[usp_Aspx_PaymentGatewayTypeAdd]";
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@PaymentGatewayTypeName", module.PaymentGatewayTypeName));
                        sqlCmd.Parameters.Add(new SqlParameter("@StoreID", module.StoreID));
                        sqlCmd.Parameters.Add(new SqlParameter("@PortalID", module.PortalID));
                        sqlCmd.Parameters.Add(new SqlParameter("@FolderName", module.FolderName));
                        sqlCmd.Parameters.Add(new SqlParameter("@FriendlyName", module.FriendlyName));
                        sqlCmd.Parameters.Add(new SqlParameter("@CultureName", module.CultureName));
                        sqlCmd.Parameters.Add(new SqlParameter("@Description", module.Description));
                        sqlCmd.Parameters.Add(new SqlParameter("@Version", module.Version));
                        sqlCmd.Parameters.Add(new SqlParameter("@AddedBy", GetUsername));
                        sqlCmd.Parameters.Add(new SqlParameter("@Update", update));
                        sqlCmd.Parameters.Add(new SqlParameter("@NewModuleId", SqlDbType.Int));
                        sqlCmd.Parameters["@NewModuleId"].Direction = ParameterDirection.Output;

                        sqlConn.Open();
                        sqlCmd.ExecuteNonQuery();
                        if (update==0)
                        {
                            ReturnValue = (int)sqlCmd.Parameters["@NewModuleId"].Value;
                            module.PaymentGatewayTypeID = ReturnValue;
                        }
                        sqlConn.Close();

                        XmlNodeList xnList5 = doc.SelectNodes("sageframe/folders/folder/modules/module/controls/control");
                        int displayOrder = 0;
                        foreach (XmlNode xn5 in xnList5)
                        {
                            displayOrder++;
                            string ctlKey = xn5["key"].InnerXml.ToString();
                            string ctlSource = xn5["src"].InnerXml.ToString();
                            string ctlTitle = xn5["title"].InnerXml.ToString();
                            string _ctlType = xn5["type"].InnerXml.ToString();
                            int ctlType = checkControlType(_ctlType);
                            string ctlHelpUrl = xn5["helpurl"].InnerXml.ToString();
                            string ctlSupportPr = xn5["supportspartialrendering"].InnerXml.ToString();


                            List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
                            paramCol.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", module.PaymentGatewayTypeID));
                            paramCol.Add(new KeyValuePair<string, object>("@ControlName", ctlKey));
                            paramCol.Add(new KeyValuePair<string, object>("@ControlType", ctlType));
                            paramCol.Add(new KeyValuePair<string, object>("@ControlSource", ctlSource));
                            paramCol.Add(new KeyValuePair<string, object>("@DisplayOrder", displayOrder));
                            paramCol.Add(new KeyValuePair<string, object>("@StoreID", module.StoreID));
                            paramCol.Add(new KeyValuePair<string, object>("@PortalID", module.PortalID));
                            paramCol.Add(new KeyValuePair<string, object>("@CultureName", module.CultureName));
                            paramCol.Add(new KeyValuePair<string, object>("@AddedBy", GetUsername));
                            paramCol.Add(new KeyValuePair<string, object>("@Update", update));
                            paramCol.Add(new KeyValuePair<string, object>("@HelpUrl", ctlHelpUrl));
                            paramCol.Add(new KeyValuePair<string, object>("@SupportsPartialRendering", bool.Parse(ctlSupportPr.ToString())));
                            if (xn5.Attributes["type"] == null)
                            {
                                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_PaymentGateWayControlAdd]", paramCol);
                            }
                            else
                            {
                                if (xn5.Attributes["type"] != null && xn5.Attributes["type"].Value.ToString().ToLower() == "page")
                                {

                                    if (!IsPageExists(ctlKey))
                                    {
                                        //if (update == 0)
                                        // {
                                        try
                                        {
                                            #region "Module Creation Logic"

                                            // add into module table
                                            ModuleInfo moduleObj = new ModuleInfo();
                                            moduleObj.ModuleName = "AspxCommerce." + module.FriendlyName;
                                            moduleObj.Name = module.Name;
                                            moduleObj.PackageType = "Module";
                                            moduleObj.Owner = "AspxCommerce";
                                            moduleObj.Organization = "";
                                            moduleObj.URL = "";
                                            moduleObj.Email = "";
                                            moduleObj.ReleaseNotes = "";
                                            moduleObj.FriendlyName = ctlKey;
                                            moduleObj.Description = ctlKey;
                                            moduleObj.Version = module.Version;
                                            moduleObj.isPremium = true;
                                            moduleObj.BusinessControllerClass = "";
                                            moduleObj.FolderName = module.FolderName;                                           
                                            moduleObj.supportedFeatures = 0;
                                            moduleObj.CompatibleVersions = "";
                                            moduleObj.dependencies = "";
                                            moduleObj.permissions = "";                                     

                                            int[] outputValue;
                                            outputValue = moduleCtr.AddModules(moduleObj, false, 0, true,
                                                                                      DateTime.Now, GetPortalID,
                                                                                      GetUsername);
                                            moduleObj.ModuleID = outputValue[0];
                                            moduleObj.ModuleDefID = outputValue[1];
                                            newModuleID = moduleObj.ModuleID;
                                            newModuleDefID = moduleObj.ModuleDefID;


                                            //insert into ProtalModule table

                                            newPortalmoduleID = moduleCtr.AddPortalModules(GetPortalID,
                                                                                                  newModuleID, true,
                                                                                                  DateTime.Now,
                                                                                                  GetUsername);
                                            #endregion

                                            //install permission for the installed module in ModuleDefPermission table with ModuleDefID and PermissionID
                                            int controlType = 0;
                                            controlType = ctlType;
                                            string IconFile = "";

                                            //add into module control table
                                            moduleCtr.AddModuleCoontrols(newModuleDefID, ctlKey + "View",
                                                                               ctlTitle + "View", ctlSource,
                                                                               IconFile, controlType, 0, ctlHelpUrl,
                                                                               bool.Parse(ctlSupportPr), true,
                                                                               DateTime.Now,
                                                                               GetPortalID, GetUsername);

                                            //sp_ModuleDefPermissionAdd
                                            string ModuleDefPermissionID;
                                            List<KeyValuePair<string, object>> paramDef =
                                                new List<KeyValuePair<string, object>>();
                                            paramDef.Add(new KeyValuePair<string, object>("@ModuleDefID",
                                                                                          newModuleDefID));
                                            paramDef.Add(new KeyValuePair<string, object>("@PortalModuleID",
                                                                                          newPortalmoduleID));
                                            paramDef.Add(new KeyValuePair<string, object>("@PermissionID", 1));
                                            paramDef.Add(new KeyValuePair<string, object>("@IsActive", true));
                                            paramDef.Add(new KeyValuePair<string, object>("@AddedOn", DateTime.Now));
                                            paramDef.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                                            paramDef.Add(new KeyValuePair<string, object>("@AddedBy", GetUsername));
                                            ModuleDefPermissionID =
                                                sqlH.ExecuteNonQueryAsGivenType<string>(
                                                    "[dbo].[sp_ModuleDefPermissionAdd]", paramDef,
                                                    "@ModuleDefPermissionID");

                                            //ModuleDefPermissionID
                                            List<KeyValuePair<string, object>> paramPage =
                                                new List<KeyValuePair<string, object>>();
                                            paramPage.Add(new KeyValuePair<string, object>("@ModuleDefID",
                                                                                           newModuleDefID));
                                            paramPage.Add(new KeyValuePair<string, object>("@PageName", ctlKey));
                                            paramPage.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                                            paramPage.Add(new KeyValuePair<string, object>(
                                                              "@ModuleDefPermissionID",
                                                              int.Parse(ModuleDefPermissionID)));
                                            ;
                                            sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_CreatePaymentGatewayPage]",
                                                                 paramPage);

                                        }
                                        catch (Exception ex)
                                        {

                                            ProcessException(ex);
                                        }
                                        //   }
                                    }

                                }
                            }
                        }


                        XmlNodeList xnList2 = doc.SelectNodes("sageframe/folders/folder/settings/setting");
                        int onetime = 0;
                        foreach (XmlNode xn2 in xnList2)
                        {
                            onetime++;
                            string settingkey = xn2["key"].InnerXml.ToString();
                            string settingvalue = xn2["value"].InnerXml.ToString();
                            List<KeyValuePair<string, object>> paramCol = new List<KeyValuePair<string, object>>();
                            paramCol.Add(new KeyValuePair<string, object>("@PaymentGatewayTypeID", module.PaymentGatewayTypeID));
                            paramCol.Add(new KeyValuePair<string, object>("@StoreID", module.StoreID));
                            paramCol.Add(new KeyValuePair<string, object>("@PortalID", module.PortalID));
                            paramCol.Add(new KeyValuePair<string, object>("@SettingKey", settingkey));
                            paramCol.Add(new KeyValuePair<string, object>("@SettingValue", settingvalue));
                            paramCol.Add(new KeyValuePair<string, object>("@AddedBy", GetUsername));
                            paramCol.Add(new KeyValuePair<string, object>("@Update", update));
                            paramCol.Add(new KeyValuePair<string, object>("@onetime", onetime));
                            sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_PaymentGateWaySettingByKeyAdd]", paramCol);
                        }

                        XmlNodeList xnList3 = doc.SelectNodes("sageframe/folders/folder/files/file");
                        if (xnList3.Count != 0)
                        {
                            foreach (XmlNode xn3 in xnList3)
                            {
                                string fileName = xn3["name"].InnerXml;
                                try
                                {
                                    #region CheckValidDataSqlProvider
                                    if (!String.IsNullOrEmpty(fileName) && fileName.Contains(module.Version + ".SqlDataProvider"))
                                    {
                                        _exceptions = ReadSQLFile(module.TempFolderPath, fileName);
                                    }
                                    #endregion

                                    #region CheckAlldllFiles
                                    if (!String.IsNullOrEmpty(fileName) && fileName.Contains(".dll"))
                                    {
                                        dllFiles.Add(fileName);
                                    }
                                    #endregion

                                    #region ReadUninstall SQL FileName
                                    if (!String.IsNullOrEmpty(fileName) && fileName.Contains("Uninstall.SqlDataProvider"))
                                    {
                                        unistallScriptFile = fileName;
                                    }
                                    #endregion
                                }
                                catch (Exception ex)
                                {
                                    _exceptions += ex.Message;
                                    break;
                                }
                            }
                        }

                        if (_exceptions != string.Empty)
                        {
                            if (module.PaymentGatewayTypeID.ToString() != null && module.PaymentGatewayTypeID > 0)
                            {
                                //Run unstallScript
                                if (unistallScriptFile != "")
                                {
                                    _exceptions = ReadSQLFile(module.TempFolderPath, unistallScriptFile);
                                }
                                //Delete Module info from data base
                                PaymentGatewayRollBack(module.PaymentGatewayTypeID, GetPortalID, module.StoreID);
                                module.PaymentGatewayTypeID = -1;
                            }
                        }
                        #endregion
                    }
                    catch
                    {
                        if (module.PaymentGatewayTypeID.ToString() != null && module.PaymentGatewayTypeID > 0)
                        {
                            //Run unstallScript
                            if (unistallScriptFile != "")
                            {
                                _exceptions = ReadSQLFile(module.TempFolderPath, unistallScriptFile);
                            }
                            //Delete Module info from data base
                            if (update == 0)
                            {
                               PaymentGatewayRollBack(module.PaymentGatewayTypeID, GetPortalID, module.StoreID);
                            }
                            module.PaymentGatewayTypeID = -1;
                        }
                    }
                    #endregion
                }
            }

            if (module.PaymentGatewayTypeID.ToString() != null && module.PaymentGatewayTypeID > 0 && _exceptions == string.Empty)
            {
                string path = HttpContext.Current.Server.MapPath("~/");
                string flPath = module.FolderName.ToString().Replace("/","\\"); 
                string targetPath = path + SageFrame.Common.RegisterModule.Common.ModuleFolder + '\\' + flPath;
                CopyDirectory(module.TempFolderPath, targetPath);
                for (int i = 0; i < dllFiles.Count; i++)
                {
                    string sourcedllFile = module.TempFolderPath + '\\' + dllFiles[i].ToString();
                    string targetdllPath = path + SageFrame.Common.RegisterModule.Common.DLLTargetPath + '\\' + dllFiles[i].ToString();
                    File.Copy(sourcedllFile, targetdllPath, true);
                    //File.Move();
                }
            }
            DeleteTempDirectory(module.TempFolderPath);
        }

        private void CopyDirectory(string sourceDirectory, string destinationDirectory)
        {
            if (Directory.Exists(sourceDirectory))
            {
                string[] files = Directory.GetFiles(sourceDirectory);
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }
                foreach (string s in files)
                {
                    string fileName = Path.GetFileName(s);
                    string destFile = Path.Combine(destinationDirectory, ParseFileNameWithoutPath(fileName));
                    File.Copy(s, destFile, true);
                }
                string[] directories = Directory.GetDirectories(sourceDirectory);
                foreach (string d in directories)
                {
                    char splitter = '\\';
                    string[] directory = d.Split(splitter);
                    string directoryName = directory[directory.Length - 1];
                    string destDirectory = Path.Combine(destinationDirectory, directoryName);
                    CopyDirectory(d, destDirectory);
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

        public void DeleteTempDirectory(string tempDirectory)
        {
            try
            {
                if (!string.IsNullOrEmpty(tempDirectory))
                {
                    if (Directory.Exists(tempDirectory))
                        Directory.Delete(tempDirectory, true);
                }
            }
            catch (IOException ex)
            {
                throw ex;//cant delete folder
            }
        }
       private bool IsPageExists(string pagename)
       {
           var paramCol = new List<KeyValuePair<string, object>>();
           paramCol.Add(new KeyValuePair<string, object>("@PortalID", -1));
           paramCol.Add(new KeyValuePair<string, object>("@PageName", pagename));
           // paramCol.Add(new KeyValuePair<string, object>("@PageName", pagename));
           return sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckPaymentPage]", paramCol, "@IsExist");

       }

    }
}
