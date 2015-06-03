/*
AspxCommerce® - http://www.AspxCommerce.com
Copyright (c) 20011-2012 by AspxCommerce
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
 * 
 * 
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Web;
using System.Web.UI;
using AspxCommerce.Core;
using SageFrame.Web;
using System.Collections;
using System.Xml;
using System.IO;

public partial class Modules_PaymentGatewayManagement_PaymentGatewayManagement : BaseAdministrationUserControl
{
    private PaymentGatewayInstaller installhelp = new PaymentGatewayInstaller();
    private PaymentGateWayModuleInfo paymentModule = new PaymentGateWayModuleInfo();
    private string _exceptions = string.Empty;
    public int StoreID, PortalID;
    public string UserName, CultureName;
    public int ErrorCode = 0;
    public string pageURL = string.Empty, LogoDestination = string.Empty;
    public int MaxFileSize = 0;

    protected void page_init(object sender, EventArgs e)
    {
        try
        {
            string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            LogoDestination = modulePath + "Logos/";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalPayementGatewayPath",
                                                    " var aspxPayementGatewayPath='" + ResolveUrl(modulePath) + "';",
                                                    true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalPayementGatewayUploadPath",
                                                    " var aspxPayementGatewayUploadPath='" + ResolveUrl(modulePath) +
                                                    "'+'uploads/';", true);
            lblRepairInstallHelp.Text =
                SageMessage.GetSageModuleLocalMessageByVertualPath(
                    "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText",
                    "WarningMessageWillDeleteAllFiles");
            chkRepairInstall.Checked = true;
            pnlRepair.Visible = true;
            InitializeJS();
            UserModuleID = SageUserModuleID;

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            StoreID = GetStoreID;
            PortalID = GetPortalID;
            UserName = GetUsername;
            CultureName = GetCurrentCultureName;

            IncludeCss("StoreOrdersReport", "/Templates/" + TemplateName + "/css/GridView/tablesort.css",
                       "/Templates/" + TemplateName + "/css/MessageBox/style.css",
                       "/Templates/" + TemplateName + "/css/Print/print.css", "/Modules/AspxCommerce/AspxPaymentGateWayManagement/css/module.css");
            IncludeJs("PaymentGateWayManagement", "/js/MessageBox/jquery.easing.1.3.js",
                      "/js/MessageBox/alertbox.js", "/js/GridView/jquery.grid.js", "/js/GridView/SagePaging.js",
                      "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js", "/js/PopUp/custom.js",
                      "/js/CurrencyFormat/jquery.formatCurrency-1.4.0.js",
                      "/js/CurrencyFormat/jquery.formatCurrency.all.js",
                      "/Modules/AspxCommerce/AspxPaymentGateWayManagement/js/PaymentGatewayManagement.js",
                      "/js/AjaxFileUploader/ajaxupload.js");


            StoreSettingConfig ssc = new StoreSettingConfig();
            MaxFileSize =
                int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.MaximumImageSize, StoreID, PortalID, CultureName));


            IncludeAllLanguageJS();
            pageURL = HttpContext.Current.Request.Url.ToString();
            string x = pageURL;
            string[] url = pageURL.Split('?');
            pageURL = url[0];

            if (Request.QueryString["deleted"] != null)
            {
                if (bool.Parse(Request.QueryString["deleted"]) == true)
                {
                    ShowMessage(SageMessageTitle.Information.ToString(),
                                SageMessage.GetSageModuleLocalMessageByVertualPath(
                                    "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText",
                                    "PaymentGatewayDeletedSuccessfully"), "", SageMessageType.Success);
                }
            }
            if (Request.QueryString["installed"] != null)
            {
                if (bool.Parse(Request.QueryString["installed"]) == true)
                {
                    ShowMessage(SageMessageTitle.Information.ToString(),
                                SageMessage.GetSageModuleLocalMessageByVertualPath(
                                    "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText",
                                    "PaymentGatewayInstalledSuccessfully"), "", SageMessageType.Success);
                }
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
    }

    #region Install New GateWay

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {

            InstallPaymentGateWay();
        }
        catch (Exception ex)
        {

            ProcessException(ex);
        }
    }


    private void InstallPaymentGateWay()
    {
        try
        {
            ArrayList arrColl = installhelp.Step0CheckLogic(fuPGModule);
            int ReturnValue;
            if (arrColl != null && arrColl.Count > 0)
            {
                ReturnValue = (int)arrColl[0];
                paymentModule = (PaymentGateWayModuleInfo)arrColl[1];
                ViewState["PaymentGateway"] = paymentModule.PaymentGatewayTypeID.ToString();
                if (ReturnValue == 0)
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(),
             SageMessage.GetSageModuleLocalMessageByVertualPath(
                 "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText",
                 "YouNeedToSelectAFileToUploadFirst"), "", SageMessageType.Alert);
                    //lblLoadMessage.Text =
                    //    SageMessage.GetSageModuleLocalMessageByVertualPath(
                    //        "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText",
                    //        "YouNeedToSelectAFileToUploadFirst");
                    lblLoadMessage.Visible = true;
                    ErrorCode = 1;
                    return;
                }
                else if (ReturnValue == -1)
                {
                    ShowMessage(SageMessageTitle.Exception.ToString(),
             SageMessage.GetSageModuleLocalMessageByVertualPath(
                 "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText",
                 "InvalidFileExtension") + this.fuPGModule.FileName, "", SageMessageType.Alert);
                    //lblLoadMessage.Text =
                    //    SageMessage.GetSageModuleLocalMessageByVertualPath(
                    //        "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "InvalidFileExtension") +
                    //    this.fuPGModule.FileName;
                    lblLoadMessage.Visible = true;
                    ErrorCode = 1;
                }
                else if (ReturnValue == 1)
                {
                    installhelp.InstallPackage(paymentModule, 0);
                    ShowMessage(SageMessageTitle.Information.ToString(),
                                SageMessage.GetSageModuleLocalMessageByVertualPath(
                                    "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText",
                                    "PaymentGatewayInstalledSuccessfully"), "", SageMessageType.Success);
                    ErrorCode = 0;
                    Response.Redirect(pageURL + "?installed=true", false);
                }
                else if (ReturnValue == 2)
                {
                    if (chkRepairInstall.Checked == true)
                    {
                        int gatewayID = int.Parse(ViewState["PaymentGateway"].ToString());
                        if (paymentModule != null)
                        {
                            UninstallPaymentGateway(paymentModule, true, gatewayID);
                            ViewState["PaymentGateway"] = null;
                            ShowMessage(SageMessageTitle.Information.ToString(),
                                        SageMessage.GetSageModuleLocalMessageByVertualPath(
                                            "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText",
                                            "PaymentGatewayInstalledSuccessfully"), "", SageMessageType.Success);
                            ErrorCode = 0;
                            Response.Redirect(pageURL + "?installed=true", false);
                        }
                    }
                    else
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(),
             SageMessage.GetSageModuleLocalMessageByVertualPath(
                 "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText",
                 "AlreadyExistPaymentGateway"), "", SageMessageType.Alert);
                        ErrorCode = 1;
                    }
                }
                else if (ReturnValue == 3)
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(),
             SageMessage.GetSageModuleLocalMessageByVertualPath(
                 "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText",
                 "ThisPackageIsNotValid"), "", SageMessageType.Alert);
                    lblLoadMessage.Text =
                        SageMessage.GetSageModuleLocalMessageByVertualPath(
                            "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "ThisPackageIsNotValid");
                    lblLoadMessage.Visible = true;
                    ErrorCode = 1;
                }
                else if (ReturnValue == 4)
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(),
             SageMessage.GetSageModuleLocalMessageByVertualPath(
                 "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText",
                 "ThisPackageDoesNotAppearToBeValid"), "", SageMessageType.Alert);
                    lblLoadMessage.Text =
                        SageMessage.GetSageModuleLocalMessageByVertualPath(
                            "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText",
                            "ThisPackageDoesNotAppearToBeValid");
                    lblLoadMessage.Visible = true;
                    ErrorCode = 1;
                }
                else
                {
                    ShowMessage(SageMessageTitle.Exception.ToString(),
             SageMessage.GetSageModuleLocalMessageByVertualPath(
                 "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText",
                 "ThereIsErrorWhileInstallingThisModule"), "", SageMessageType.Error);
                    lblLoadMessage.Text =
                        SageMessage.GetSageModuleLocalMessageByVertualPath(
                            "Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText",
                            "ThereIsErrorWhileInstallingThisModule");
                    lblLoadMessage.Visible = true;
                    ErrorCode = 1;
                }
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }



    #endregion

    #region Uninstall Existing Payment Gateway

    private void UninstallPaymentGateway(PaymentGateWayModuleInfo paymentGateWay, bool deleteModuleFolder, int gatewayID)
    {
        PaymentGatewayInstaller installerClass = new PaymentGatewayInstaller();
        string path = HttpContext.Current.Server.MapPath("~/");

        string paymentGatewayFolderPath = paymentGateWay.InstalledFolderPath;
        if (!string.IsNullOrEmpty(paymentGatewayFolderPath))
        {
            if (Directory.Exists(paymentGatewayFolderPath))
            {
                XmlDocument doc = new XmlDocument();
                if (File.Exists(paymentGatewayFolderPath + '\\' + paymentGateWay.ManifestFile))
                {

                    doc.Load(paymentGatewayFolderPath + '\\' + paymentGateWay.ManifestFile);

                    try
                    {
                        if (paymentGateWay.PaymentGatewayTypeID > 0)
                        {
                            ReadUninstallScriptAndDLLFiles(doc, paymentGatewayFolderPath, installerClass);
                            if (deleteModuleFolder == true)
                            {
                                installerClass.DeleteTempDirectory(paymentGateWay.InstalledFolderPath);
                            }
                            installhelp.InstallPackage(paymentModule, gatewayID);
                        }
                    }
                    catch (Exception ex)
                    {
                        _exceptions = ex.Message;
                    }
                }
                else
                {
                    if (deleteModuleFolder == true)
                    {
                        installerClass.DeleteTempDirectory(paymentGateWay.InstalledFolderPath);
                    }
                    installhelp.InstallPackage(paymentModule, gatewayID);
                }
            }
            else if (!Directory.Exists(paymentGatewayFolderPath))
            {
                installhelp.InstallPackage(paymentModule, gatewayID);
            }
            else
            {
                ShowMessage(SageMessageTitle.Exception.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/AspxCommerce/AspxPaymentGateWayManagement/ModuleLocalText", "PaymentGatewayFolderDoesnotExist"), "", SageMessageType.Error);
            }
        }
    }

    private void ReadUninstallScriptAndDLLFiles(XmlDocument doc, string paymentGatewayFolderPath, PaymentGatewayInstaller installerClass)
    {
        XmlElement root = doc.DocumentElement;
        if (!String.IsNullOrEmpty(root.ToString()))
        {
            ArrayList dllFiles = new ArrayList();
            string _unistallScriptFile = string.Empty;
            XmlNodeList xnFileList = doc.SelectNodes("sageframe/folders/folder/files/file");
            if (xnFileList.Count != 0)
            {
                foreach (XmlNode xn in xnFileList)
                {
                    string _fileName = xn["name"].InnerXml;
                    try
                    {
                        #region CheckAlldllFiles
                        if (!String.IsNullOrEmpty(_fileName) && _fileName.Contains(".dll"))
                        {
                            dllFiles.Add(_fileName);
                        }
                        #endregion
                        #region ReadUninstall SQL FileName
                        if (!String.IsNullOrEmpty(_fileName) && _fileName.Contains("Uninstall.SqlDataProvider"))
                        {
                            _unistallScriptFile = _fileName;
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        _exceptions = ex.Message;
                    }
                }
                if (_unistallScriptFile != "")
                {
                    RunUninstallScript(_unistallScriptFile, paymentGatewayFolderPath, installerClass);
                }
                DeleteAllDllsFromBin(dllFiles, paymentGatewayFolderPath);
            }
        }
    }

    private void RunUninstallScript(string _unistallScriptFile, string paymentGatewayFolderPath, PaymentGatewayInstaller installerClass)
    {
        _exceptions = installerClass.ReadSQLFile(paymentGatewayFolderPath, _unistallScriptFile);
    }

    private void DeleteAllDllsFromBin(ArrayList dllFiles, string paymentGatewayFolderPath)
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
        catch (Exception ex)
        {
            _exceptions = ex.Message;
        }
    }

    #endregion

    protected void btnSavePDFForm2_Click(object sender, EventArgs e)
    {
        try
        {

            string strTableContent = HttpUtility.HtmlDecode(HdnValue.Value);
            strTableContent = strTableContent.Trim().Replace("[\"", "").Replace("\\\"", "\"").Replace("}\",\"", "},").Replace("\"]", "");
            string strHiddenValue = HttpUtility.HtmlDecode(hdnDescriptionValue.Value);
            strHiddenValue = strHiddenValue.Trim().Replace("[\"", "[").Replace("\\\"", "\"").Replace("}\",\"", "},").Replace("\"]", "]").Replace("\"{", "{").Replace("}\"", "}");

            string tableContent = "[" + strTableContent + "]";
            string hiddenValue = strHiddenValue;
            string templateFolderPath = TemplateName;
            GeneratePDF gPdf = new GeneratePDF();
            gPdf.GenerateOrderDetailsPDF(tableContent, hiddenValue, templateFolderPath, GetStoreID, GetPortalID, GetCurrentCultureName);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    public string UserModuleID { get; set; }
}
