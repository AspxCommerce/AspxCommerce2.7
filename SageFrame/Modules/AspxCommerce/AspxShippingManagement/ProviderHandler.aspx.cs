using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using RegisterModule;
using SageFrame.Framework;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using AspxCommerce.Core;

public partial class Modules_AspxCommerce_AspxShippingManagement_ProviderHandler : PageBase
{
    private int _storeId=1, _portalId = 1;
    private string _userName = string.Empty, _cultureName = "en-US";
    private string  _providerInfo = string.Empty;
    private AspxCommonInfo _commonInfo=new AspxCommonInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            bool isValidFile = false;
            string validExtension = string.Empty;
            string retFilePath = string.Empty;
            string retMsg = "fail";
            int maxFileSize = 0;
            int retStatus = -1;
            string strBaseLocation = string.Empty;
            string filename = string.Empty;
            bool isRepair = false;
            _commonInfo.CultureName = GetCurrentCultureName;

            var rnd = new Random();
            var shippingMethodList = string.Empty;
            if (Request.Form["StoreId"] != null)
            {
                _commonInfo.StoreID = int.Parse(Request.Form["StoreId"].ToString());
            }
            if (Request.Form["PortalId"] != null)
            {
                _commonInfo.PortalID = int.Parse(Request.Form["PortalId"].ToString());
            }

            if (Request.Form["ValidExtension"] != null)
            {
                validExtension = Request.Form["ValidExtension"].ToString();
            }
            if (Request.Form["BaseLocation"] != null)
            {
                strBaseLocation = Request.Form["BaseLocation"].ToString();
            }
            if (Request.Form["MaxFileSize"] != null && Request.Form["MaxFileSize"] != "" &&
                int.Parse(Request.Form["MaxFileSize"].ToString()) > 0)
            {
                maxFileSize = int.Parse(Request.Form["MaxFileSize"].ToString());
            }
            if (Request.Form["IsRepair"] != null)
            {
                isRepair = bool.Parse(Request.Form["IsRepair"].ToString());
            }
            if (Request.Form["UserName"] != null)
            {
                _commonInfo.UserName = Request.Form["UserName"].ToString();
            }
            if (Request.Files != null)
            {
                HttpFileCollection files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    if (file.ContentLength > 0 && file.ContentLength < maxFileSize*1024)
                    {
                        if (validExtension.Length > 0)
                        {
                            string[] allowExtensions = validExtension.Split(' ');
                            if (allowExtensions.Contains(GetExtension(file.FileName),
                                                         StringComparer.InvariantCultureIgnoreCase))
                            {
                                isValidFile = true;
                                retMsg = "Valid file Extension";
                            }
                            else
                            {
                                retMsg = "Not valid file Extension";
                            }
                        }
                        else
                        {
                            isValidFile = true;
                        }
                        if (isValidFile)
                        {
                            retFilePath = strBaseLocation;
                            strBaseLocation = Server.MapPath("~/" + strBaseLocation);
                            if (!Directory.Exists(strBaseLocation))
                            {
                                Directory.CreateDirectory(strBaseLocation);
                            }
                            else
                            {
                                DeleteTempDirectory(strBaseLocation);
                                Directory.CreateDirectory(strBaseLocation);
                            }
                            filename = System.IO.Path.GetFileName(file.FileName);
                            string strExtension = GetExtension(filename);
                            filename = filename.Substring(0, (filename.Length - strExtension.Length) - 1);
                            filename = filename + '_' + rnd.Next(111111, 999999).ToString() + '.' + strExtension;
                            string filePath = strBaseLocation + "\\" + filename;
                            retFilePath = retFilePath + "/" + filename;
                            file.SaveAs(filePath);

                            int value = ReadZip(strBaseLocation, filename);

                            switch (value)
                            {
                                case 1:

                                    CopyDlltoBin();
                                    var obj =
                                        (Dictionary<string, string>)
                                        DynamicMethodInvoke(_provider.AssemblyName, _provider.ShippingProviderNamespace,
                                                            _provider.ShippingProviderClass, "GetAvailableServiceMethod");
                                    shippingMethodList = JSONHelper.Serialize<Dictionary<string, string>>(obj);
                                                                                                         
                                                                       _provider.TempFolderPath = Regex.Replace(_provider.TempFolderPath, @"(\\)", @"//");
                                                                           _provider.ExtractedPath = Regex.Replace(_provider.ExtractedPath, @"(\\)", @"//");
                                                                                                        
                                                                       if (!string.IsNullOrEmpty(_provider.InstallScript))
                                        ReadSQLFile(_provider.ExtractedPath, _provider.InstallScript);
                                    retMsg = "Successfully read shipping provider data.";
                                    AspxShipProviderMgntController objProvider = new AspxShipProviderMgntController();

                                    string keys = _provider.Settings.Aggregate("", (current, setting) => current + (setting.Key + "#"));
                                    string values = _provider.Settings.Aggregate("",
                                                                                 (current, setting) =>
                                                                                 current + (setting.Value + "#"));

                                    objProvider.SaveUpdateProviderSetting(_provider, keys, values, _commonInfo);
                                    retStatus = 1;
                                    break;
                                case 2:
                                                                                                                                             retMsg = "Shipping provider already exist!";
                                    retStatus = -1;
                                    if (isRepair)
                                    {
                                        UninstallShippingProvider(_provider);
                                        CopyDlltoBin();
                                        var obj1 =
                                            (Dictionary<string, string>)
                                            DynamicMethodInvoke(_provider.AssemblyName,
                                                                _provider.ShippingProviderNamespace,
                                                                _provider.ShippingProviderClass,
                                                                "GetAvailableServiceMethod");
                                        shippingMethodList = JSONHelper.Serialize<Dictionary<string, string>>(obj1);
                                                                                                                     
                                                                               _provider.TempFolderPath = Regex.Replace(_provider.TempFolderPath, @"(\\)",
                                                                                 @"//");
                                                                               _provider.ExtractedPath = Regex.Replace(_provider.ExtractedPath, @"(\\)", @"//");
                                                                                                                    
                                                                               if (!string.IsNullOrEmpty(_provider.InstallScript))
                                            ReadSQLFile(_provider.ExtractedPath, _provider.InstallScript);
                                        AspxShipProviderMgntController objProvider1 = new AspxShipProviderMgntController();
                                        string key = _provider.Settings.Aggregate("", (current, setting) => current + (setting.Key + "#"));
                                        string keyvalue = _provider.Settings.Aggregate("",
                                                                                     (current, setting) =>
                                                                                     current + (setting.Value + "#"));

                                        objProvider1.SaveUpdateProviderSetting(_provider, key, keyvalue,_commonInfo);
                                        retMsg = "Successfully read shipping provider data.";
                                        retStatus = 1;
                                    }
                                    break;
                                case 3:
                                    retMsg = "Invalid shipping providers zip!";
                                    retStatus = -1;
                                    break;

                                case 4:
                                    retMsg = "Invalid shipping providers manifest file!";
                                    retStatus = -1;
                                    break;


                            }

                                                                              }
                    }
                    else
                    {
                        retMsg = "Sorry, the file must be less than " + maxFileSize + "KB";
                    }
                }
            }
            var lit = new Literal();
            lit.Text = "<pre id='response'>({  'Status': '" + retStatus + "','Methods' :'" + shippingMethodList +
                       "','Message': '" + retMsg + "','UploadedPath': '" + retFilePath + "' })</pre>";
            this.Page.Form.Controls.Add(lit);
        }
        catch (Exception ex)
        {
            var lit = new Literal();
            lit.Text = "<pre id='response'>({  'Status': '" + -1 + "','Methods' :'" + "" +
                       "','Message': '/" + ex.Message + "/','UploadedPath': '" + "" + "' })</pre>";
            this.Page.Form.Controls.Add(lit);

                  }
     
    }

    public void DeleteTempDirectory(string tempDirectory)
    {
        try
        {
           
            if (!string.IsNullOrEmpty(tempDirectory))
            {
                if (Directory.Exists(tempDirectory))
                {
                                       var directoryInfo = new DirectoryInfo(tempDirectory);
                    EmptyFolder(directoryInfo);
                }

            }
        }
        catch (IOException ex)
        {
            throw ex;//cant delete folder
        }
    }

    private void EmptyFolder(DirectoryInfo directoryInfo)
    {
        foreach (FileInfo file in directoryInfo.GetFiles())
        {
            file.Delete();
        }

        foreach (DirectoryInfo subfolder in directoryInfo.GetDirectories())
        {
            EmptyFolder(subfolder);
        }
    }

    private string GetExtension(string fileName)
    {
        int index = fileName.LastIndexOf('.');
        string ext = fileName.Substring(index + 1, (fileName.Length - index) - 1);
        return ext;
    }

   

    private ShippingProvider _provider = new ShippingProvider();
    private List<KeyValuePair<string, string>> _providerSetting = new List<KeyValuePair<string, string>>();
    private List<DynamicMethod> _dynamicMethods = new List<DynamicMethod>(); 

    private void CopyDlltoBin()
    {
       
            string path = HttpContext.Current.Server.MapPath("~/");
         
            for (int i = 0; i < _provider.DllFiles.Count; i++)
            {
                string sourcedllFile = _provider.ExtractedPath + '\\' +_provider.DllFiles[i].ToString();
                string targetdllPath = path + SageFrame.Common.RegisterModule.Common.DLLTargetPath + '\\' + _provider.DllFiles[i].ToString();

                if (File.Exists(targetdllPath))
                {
                                       File.Move(targetdllPath, _provider.TempFolderPath + @"\\dll");
                }
                else
                {
                    File.Copy(sourcedllFile, targetdllPath, true);
                }
                           }

         }

    private static object DynamicMethodInvoke(string assemblyName,string namespaceName,string typeName,
                           string methodName)
    {
               Type calledType = Type.GetType(namespaceName + "." + typeName + "," + assemblyName);

               try
        {
            var obj = calledType.InvokeMember(
                     methodName,
                     BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                     null,
                     null,
                     null);
            return obj;
        }
        catch (Exception ex)
        {
                
            throw ex;
        }
     
    }
    private int ReadZip(string filepath,string fileName)
    {
        int returnValue = 0;
        string extractedPath = string.Empty;
        string destPath = filepath + "\\extracted";
        ZipUtil.UnZipFiles(filepath + "\\" + fileName, destPath, ref extractedPath, SageFrame.Common.RegisterModule.Common.Password,false);
        _provider.ExtractedPath = destPath;
        _provider.TempFolderPath = filepath;
        _provider.TempFileName = fileName;
      
      
              if (!string.IsNullOrEmpty(_provider.ExtractedPath) && Directory.Exists(_provider.ExtractedPath))
        {
            switch (Step1CheckLogic(_provider.ExtractedPath, _provider))
            {
                case 0://No manifest file
                    DeleteTempDirectory(_provider.TempFolderPath);
                    returnValue = 3;
                    break;
                case -1://Invalid Manifest file
                    DeleteTempDirectory(_provider.TempFolderPath);
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
        return returnValue;
    }

    private string CheckFormanifestFile(string tempUnzippedPath, ShippingProvider provider)
    {
        DirectoryInfo dir = new DirectoryInfo(tempUnzippedPath);
        foreach (FileInfo f in dir.GetFiles("*.*"))
        {
            if (f.Extension.ToLower() == ".sfe")
            {
                provider.ManifestFile = f.Name;
                return provider.ManifestFile;
            }
            else
            {
                provider.ManifestFile = "";
            }
        }
        return provider.ManifestFile;
    }

    private bool CheckValidManifestFile(XmlElement root)
    {
        if (root.Name == "sageframe")//need to change the root node for valid manifest file at root node  
        {
            string packageType = root.GetAttribute("type");                       switch (packageType.ToLower())
            {
                               case "shippingprovider":
                    return true;
            }
        }
        return false;
    }

    private int Step1CheckLogic(string tempUnzippedPath, ShippingProvider provider)
    {
        if (CheckFormanifestFile(tempUnzippedPath, provider) != "")
        {
            var doc = new XmlDocument();
            doc.Load(tempUnzippedPath + '\\' + provider.ManifestFile);
            XmlElement root = doc.DocumentElement;
            if (CheckValidManifestFile(root))
            {

                XmlNodeList xnList3 = doc.SelectNodes("sageframe/folders/folder/files/file");
                if (xnList3.Count != 0)
                {var dlls=new ArrayList();
                    foreach (XmlNode xn3 in xnList3)
                    {
                        string fileName = xn3["name"].InnerXml;
                        try
                        {
                            #region CheckValidDataSqlProvider

                            if (!String.IsNullOrEmpty(fileName) &&
                                fileName.Contains("1.00.00.SqlDataProvider"))
                            {
                                provider.InstallScript = fileName;
                            }

                            #endregion

                            #region CheckAlldllFiles

                            if (!String.IsNullOrEmpty(fileName) && fileName.Contains(".dll"))
                            {
                                dlls.Add(fileName);
                                _provider.DllFiles = dlls;
                            }

                            #endregion

                            #region ReadUninstall SQL FileName

                            if (!String.IsNullOrEmpty(fileName) && fileName.Contains("Uninstall.SqlDataProvider"))
                            {
                                provider.UninstallScript = fileName;
                            }

                            #endregion



                        }
                        catch
                        {
                           break;
                        }
                    }
                }
                XmlNodeList xnList = doc.SelectNodes("sageframe/folders/folder");
                foreach (XmlNode xn in xnList)
                {
                    provider.ShippingProviderName = xn["providername"].InnerXml;
                    provider.ShippingProviderAliasHelp = xn["provideraliashelp"].InnerXml;
                    provider.ShippingProviderServiceCode = xn["servicecode"].InnerXml;
                    provider.AssemblyName = xn["assemblyname"].InnerXml;
                    provider.ShippingProviderNamespace = xn["namespace"].InnerXml;
                    provider.ShippingProviderClass = xn["class"].InnerXml;
                    provider.ModuleFolder = "Modules/" + xn["foldername"].InnerXml;
                                                                         provider.IsActive = true;
                    
                                                                          
                    if (IsProviderExist(provider.ShippingProviderName,_storeId,_portalId))
                    {
                        XmlNodeList settingCtl = doc.SelectNodes("sageframe/folders/folder/modules/module/controls/control");
                        foreach (XmlNode xn5 in settingCtl)
                        {
                            if (xn5["name"].InnerXml.ToLower().Trim() == "setting")
                                provider.SettingControlPath = xn5["src"].InnerXml;
                            if (xn5["name"].InnerXml.ToLower().Trim() == "label")
                                provider.LabelControlPath = xn5["src"].InnerXml;
                            if (xn5["name"].InnerXml.ToLower().Trim() == "track")
                                provider.TrackControlPath = xn5["src"].InnerXml;
                            
                        }
                        XmlNodeList xnList2 = doc.SelectNodes("sageframe/folders/folder/settings/setting");
                       
                        foreach (XmlNode xn2 in xnList2)
                        {
                            string settingkey = xn2["key"].InnerXml;
                            string settingvalue = xn2["value"].InnerXml;
                            _providerSetting.Add(new KeyValuePair<string, string>(settingkey, settingvalue));
                        }
                        provider.Settings = _providerSetting;

                        XmlNodeList methods = doc.SelectNodes("sageframe/folders/folder/dynamicmethods/method");
                        foreach (XmlNode method in methods)
                        {
                            var dymethod = new DynamicMethod();
                            dymethod.MethodName = method["name"].InnerXml;
                            dymethod.MethodType = method.Attributes["type"].Value;
                            var parlist = new List<DynamicParam>();
                           XmlNodeList parameters= method.SelectNodes("params/param");
                           if (parameters != null)
                           {
                              
                               int parCount = 0;
                               foreach (XmlNode param in parameters)
                               {
                                   var pars = new DynamicParam();
                                   parCount++;
                                   pars.ParameterName = param.Attributes["type"].Value;
                                   pars.ParameterType = param.Attributes["objectType"].Value;
                                   pars.ParameterOrder =int.Parse(param.Attributes["order"].Value);
                                   parlist.Add(pars);
                               }
                               dymethod.DynamicParams = parlist;
                           }
                            _dynamicMethods.Add(dymethod);
                        }
                        provider.DynamicMethods = _dynamicMethods;
                        _provider.SettingControlTempPath = ResolveUrl(this.AppRelativeTemplateSourceDirectory) +
                                         "temp\\extracted\\" +
                                         _provider.SettingControlPath.Split('/').Last();
                        _provider.SettingControlTempPath = Regex.Replace(_provider.SettingControlTempPath, @"(\\)", @"//");
                        return 1;//Already exist
                    }
                    else
                    {
                        XmlNodeList settingCtl = doc.SelectNodes("sageframe/folders/folder/modules/module/controls/control");
                        foreach (XmlNode xn5 in settingCtl)
                        {
                            if (xn5["name"].InnerXml.ToLower().Trim()=="setting")
                            provider.SettingControlPath = xn5["src"].InnerXml;
                            if (xn5["name"].InnerXml.ToLower().Trim() == "label")
                                provider.LabelControlPath = xn5["src"].InnerXml;
                            if (xn5["name"].InnerXml.ToLower().Trim() == "track")
                                provider.TrackControlPath = xn5["src"].InnerXml;
                        }
                        XmlNodeList xnList2 = doc.SelectNodes("sageframe/folders/folder/settings/setting");

                        foreach (XmlNode xn2 in xnList2)
                        {
                            string settingkey = xn2["key"].InnerXml;
                            string settingvalue = xn2["value"].InnerXml;
                            _providerSetting.Add(new KeyValuePair<string, string>(settingkey, settingvalue));
                        }
                        provider.Settings = _providerSetting;
                        XmlNodeList methods = doc.SelectNodes("sageframe/folders/folder/dynamicmethods/method");
                        foreach (XmlNode method in methods)
                        {
                            var dymethod = new DynamicMethod();
                            dymethod.MethodName = method["name"].InnerXml;

                            XmlNodeList parameters = method.SelectNodes("params/param");
                            if (parameters != null)
                            {
                                var listParam = new List<DynamicParam>();
                                int parCount = 0;
                                foreach (XmlNode param in parameters)
                                {
                                    var pars = new DynamicParam();
                                    parCount++;
                                    pars.ParameterName = param.Attributes["type"].Value;
                                    pars.ParameterType = param.Attributes["objectType"].Value;
                                    pars.ParameterOrder = int.Parse(param.Attributes["order"].Value);
                                    listParam.Add(pars);

                                }
                                dymethod.DynamicParams = listParam;
                            }
                            _dynamicMethods.Add(dymethod);
                        }
                        provider.DynamicMethods = _dynamicMethods;
                        _provider.SettingControlTempPath = ResolveUrl(this.AppRelativeTemplateSourceDirectory) +
                                         "temp\\extracted\\" +
                                         _provider.SettingControlPath.Split('/').Last();
                        _provider.SettingControlTempPath = Regex.Replace(_provider.SettingControlTempPath, @"(\\)", @"//");

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

    private string ReadSQLFile(string tempUnzippedPath, string sqlProvidername)
    {
        string exceptions = string.Empty;
        try
        {
            var objReader = new StreamReader(tempUnzippedPath + '\\' + sqlProvidername);
            string sLine = "";
            string scriptDetails = "";
            var arrText = new ArrayList();

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
            var sqlHandler = new SQLHandler();
            exceptions = sqlHandler.ExecuteScript(scriptDetails, true);
        }
        catch (Exception ex)
        {
            exceptions += ex.Message.ToString();
        }
        return exceptions;
    }

    private bool IsProviderExist(string providerName, int storeId, int portalId)
    {
        try
        {
            var sqlH = new SQLHandler();
            var parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", storeId));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", portalId));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", _cultureName));
            parameter.Add(new KeyValuePair<string, object>("@ShippingProviderID", 0));
            parameter.Add(new KeyValuePair<string, object>("@ShippingProviderName", providerName));
            var data=sqlH.ExecuteAsObject<ShippingProvider>("[dbo].[usp_Aspx_IsShippingProviderExist]", parameter);
            _provider.IsUnique = data.IsUnique;
            _provider.ShippingProviderID = data.ShippingProviderID;
            return !data.IsUnique;
        }
        catch (Exception e)
        {
            throw e;
        }

    }
   

    #region Uninstall Existing Shipping Provider data

    private void UninstallShippingProvider(ShippingProvider provider)
    {
        string path = HttpContext.Current.Server.MapPath("~/");
        DeleteAllDllsFromBin(provider.DllFiles);
      
    }
    private void DeleteShippingProvider(int providerId,string userName,string cultureName)
    {
        try
        {
            var sqlH = new SQLHandler();
            var parameter = new List<KeyValuePair<string, object>>();
            parameter.Add(new KeyValuePair<string, object>("@StoreID", _storeId));
            parameter.Add(new KeyValuePair<string, object>("@PortalID", _portalId));
            parameter.Add(new KeyValuePair<string, object>("@ShippingProviderID", providerId));
            parameter.Add(new KeyValuePair<string, object>("@UserName", userName));
            parameter.Add(new KeyValuePair<string, object>("@CultureName", cultureName));
            sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeleteShippingProviderByID]", parameter);

        }
        catch (Exception e)
        {
            throw e;
        }
    }

    private void DeleteAllDllsFromBin(ArrayList dllFiles)
    {
        try
        {
            string path = HttpContext.Current.Server.MapPath("~/");

            foreach (string dll in dllFiles)
            {
                string targetdllPath = path + SageFrame.Common.RegisterModule.Common.DLLTargetPath + '\\' + dll;
                FileInfo imgInfo = new FileInfo(targetdllPath);
                if (imgInfo != null)
                {
                    imgInfo.Delete();
                }
            }
        }
        catch
        {
        }
    }

    #endregion
}
