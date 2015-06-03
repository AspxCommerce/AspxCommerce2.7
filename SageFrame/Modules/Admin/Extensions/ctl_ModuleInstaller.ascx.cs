#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.SageFrameClass.Services;
using System.Collections;
using SageFrame.Web.Utilities;
using SageFrame.Web;
using System.Xml;
using RegisterModule;
using System.IO;
using System.Web.Hosting;
#endregion 

namespace SageFrame.DesktopModules.Admin.Extensions
{
    public partial class ctl_ModuleInstaller : BaseAdministrationUserControl
    {
        Installers installhelp = new Installers();
        ModuleInfo module = new ModuleInfo();
        CompositeModule compositeModule = new CompositeModule();
        string Exceptions = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (ViewState["ModuleInfo"] != null)
                    {
                        module = (ModuleInfo)ViewState["ModuleInfo"];
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void AddImageUrls()
        {
            wizInstall.CancelButtonImageUrl = GetTemplateImageUrl("imgcancel.png", true);
            wizInstall.StartNextButtonImageUrl = GetTemplateImageUrl("imgforward.png", true);
            wizInstall.StepNextButtonImageUrl = GetTemplateImageUrl("imgforward.png", true);
            wizInstall.StepPreviousButtonImageUrl = GetTemplateImageUrl("imgback.png", true);
            wizInstall.FinishPreviousButtonImageUrl = GetTemplateImageUrl("imgback.png", true);
            wizInstall.FinishCompleteButtonImageUrl = GetTemplateImageUrl("imgfinished.png", true);
        }

        protected void chkRepairInstall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkRepairInstall.Checked)
                {
                    lblWarningMessage.Text = GetSageMessage("Extensions", "WarningMessageWillDeleteAllFiles");
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        public bool IsMultipleModule(XmlDocument xmlDocument)
        {
            bool flag = false;
            XmlNodeList list = xmlDocument.SelectNodes("sageframe/folders/folder/modules/module");

            if (list.Count > 1) flag = true;

            return flag;
        }


        protected void wizInstall_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                //System.Threading.Thread.Sleep(9000);
                int activeIndex = 0;
                switch (e.CurrentStepIndex)
                {
                    case 0://Upload Page         
                        ArrayList arrColl = installhelp.Step0CheckLogic(this.fileModule);
                        int ReturnValue;
                        if (arrColl != null && arrColl.Count > 0)
                        {
                            if ((bool)arrColl[2])
                            {
                                compositeModule = (CompositeModule)arrColl[1];
                                compositeModule = installhelp.fillCompositeModuleInfo(compositeModule);
                                ViewState["CompositeModule"] = compositeModule;
                            }
                            else
                            {
                                module = (ModuleInfo)arrColl[1];
                                ViewState["ModuleInfo"] = module;
                            }

                            ReturnValue = (int)arrColl[0];
                            if (ReturnValue == 0)
                            {
                                lblLoadMessage.Text = GetSageMessage("Extensions", "YouNeedToSelectAFileToUploadFirst");
                                lblLoadMessage.Visible = true;
                                e.Cancel = true;
                                activeIndex = 0;
                                break;
                            }
                            else if (ReturnValue == -1)
                            {
                                lblLoadMessage.Text = GetSageMessage("Extensions", "InvalidFileExtension ") + this.fileModule.FileName;
                                lblLoadMessage.Visible = true;
                                e.Cancel = true;
                                activeIndex = 0;
                                break;
                            }
                            else if (ReturnValue == 1)
                            {
                                BindPackage();
                                activeIndex = 2;
                                break;
                            }
                            else if (ReturnValue == 2)
                            {
                                activeIndex = 1;
                                BindPackage();
                                break;
                            }
                            else if (ReturnValue == 3)
                            {
                                lblLoadMessage.Text = GetSageMessage("Extensions", "ThisPackageIsNotValid");
                                lblLoadMessage.Visible = true;
                                e.Cancel = true;
                                activeIndex = 0;
                                break;
                            }
                            else if (ReturnValue == 4)
                            {
                                lblLoadMessage.Text = GetSageMessage("Extensions", "ThisPackageDoesNotAppearToBeValid");
                                lblLoadMessage.Visible = true;
                                e.Cancel = true;
                                activeIndex = 0;
                                break;
                            }
                            else
                            {
                                lblLoadMessage.Text = GetSageMessage("Extensions", "ThereIsErrorWhileInstallingThisModule");
                                lblLoadMessage.Visible = true;
                                e.Cancel = true;
                                activeIndex = 0;
                                break;
                            }
                        }
                        break;
                    case 1://Warning Page                        
                        if (chkRepairInstall.Checked)
                        {
                            if (ViewState["CompositeModule"] != null)
                            {
                                CompositeModule tmpcompositeModule = (CompositeModule)ViewState["CompositeModule"];
                                this.lblLicense.Text = tmpcompositeModule.License;
                                this.lblReleaseNotes.Text = tmpcompositeModule.ReleaseNotes;
                                foreach (Component component in tmpcompositeModule.Components)
                                {
                                    if (component.IsChecked)
                                    {
                                        bool isexist = installhelp.IsModuleExist(component.Name);
                                        if (isexist)
                                        {
                                            ModuleInfo objModule = installhelp.GetModuleByName(component.Name);
                                            if ((objModule != null))
                                            {
                                                string path = HttpContext.Current.Server.MapPath("~/");
                                                string targetPath = path + SageFrame.Common.RegisterModule.Common.ModuleFolder + '\\' + objModule.FolderName;

                                                objModule.InstalledFolderPath = targetPath;
                                                UninstallModule(objModule, true);
                                            }

                                        }
                                    }
                                }
                            }
                            else
                            {
                                module = (ModuleInfo)ViewState["ModuleInfo"];
                                UninstallModule(module, true);
                            }
                            activeIndex = 2;
                        }
                        else
                        {
                            UninstallModule(module, false);
                            activeIndex = 1;
                        }


                        if (ViewState["CompositeModule"] != null)
                        {
                            compositeModule = (CompositeModule)ViewState["CompositeModule"];

                            activeIndex = 3;
                        }
                        else
                            module = (ModuleInfo)ViewState["ModuleInfo"];
                        break;
                    case 2:
                        Panel pnl = (Panel)this.Step2.FindControl("pnlPackage");
                        GridView grd = (GridView)pnl.FindControl("gdvModule");
                        string existingModules = string.Empty;
                        bool IsErrorFlag = false;
                        foreach (GridViewRow row in grd.Rows)
                        {
                            CheckBox cbInstall = (CheckBox)row.FindControl("cbInstall");
                            if (cbInstall.Checked == true)
                            {
                                Label lbl = (Label)row.FindControl("lblname");
                                if (ViewState["CompositeModule"] != null)
                                {
                                    CompositeModule tmpcompositeModule = (CompositeModule)ViewState["CompositeModule"];
                                    this.lblLicense.Text = tmpcompositeModule.License;
                                    this.lblReleaseNotes.Text = tmpcompositeModule.ReleaseNotes;
                                    foreach (Component component in tmpcompositeModule.Components)
                                    {
                                        if (component.Name.Equals(lbl.Text))
                                        {
                                            component.IsChecked = true;
                                            bool isexist = installhelp.IsModuleExist(component.Name.ToLower());
                                            if (isexist)
                                            {
                                                IsErrorFlag = true;
                                                existingModules += component.Name + ", ";
                                            }
                                            break;
                                        }

                                    }
                                    ViewState["CompositeModule"] = tmpcompositeModule;
                                }
                            }
                            else
                            {
                                Label lbl = (Label)row.FindControl("lblname");
                                if (ViewState["CompositeModule"] != null)
                                {
                                    CompositeModule compositeModule = (CompositeModule)ViewState["CompositeModule"];
                                    foreach (Component component in compositeModule.Components)
                                    {
                                        if (component.Name.Equals(lbl.Text))
                                        {
                                            if (!installhelp.IsModuleExist(component.Name.ToLower()))
                                            {
                                                installhelp.AddAvailableModules(compositeModule.TempFolderPath, component);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (IsErrorFlag)
                        {
                            string existingModuleName = existingModules.Substring(0, existingModules.LastIndexOf(","));
                            ShowMessage("Modules " + existingModuleName + " already exists", "Modules " + existingModuleName + " already exists", "Modules " + existingModuleName + " already exists", SageMessageType.Error);
                            activeIndex = 1;
                            BindPackage();
                            IsErrorFlag = false;
                            break;
                        }
                        activeIndex = 3;
                        break;
                    case 3:
                        activeIndex = 4;
                        break;
                    case 4://Accept Terms
                        if (chkAcceptLicense.Checked)
                        {
                            if (ViewState["CompositeModule"] != null)
                            {
                                ModuleInfo moduleInfo = null;
                                compositeModule = (CompositeModule)ViewState["CompositeModule"];
                                bool confirmationFlag = true;
                                foreach (Component component in compositeModule.Components)
                                {
                                    if (component.IsChecked)
                                    {
                                        ArrayList list = installhelp.Step0CheckLogic(component.ZipFile, compositeModule.TempFolderPath);
                                        moduleInfo = (ModuleInfo)list[1];
                                        installhelp.fillModuleInfo(moduleInfo);
                                        installhelp.InstallPackage(moduleInfo);
                                        if (moduleInfo.ModuleID < 0)
                                        {
                                            confirmationFlag = false;
                                            InstallConfirmation(moduleInfo, ref activeIndex);
                                        }
                                    }
                                }
                                if (confirmationFlag && moduleInfo != null)
                                {
                                    InstallConfirmation(moduleInfo, ref activeIndex);
                                }
                            }
                            else if ((ModuleInfo)ViewState["ModuleInfo"] != null)
                            {
                                module = (ModuleInfo)ViewState["ModuleInfo"];
                                installhelp.InstallPackage(module);
                                InstallConfirmation(module, ref activeIndex);
                            }
                            activeIndex = 5;
                        }
                        else
                        {
                            lblAcceptMessage.Text = GetSageMessage("Extensions", "AcceptThePackageLicenseAgreementFirst");
                            e.Cancel = true;
                            activeIndex = 4;
                        }
                        break;
                }
                wizInstall.ActiveStepIndex = activeIndex;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        public void InstallConfirmation(ModuleInfo module, ref int activeIndex)
        {
            if (module.ModuleID <= 0)
            {
                lblLoadMessage.Text = GetSageMessage("Extensions", "ThereIsErrorWhileInstalling");
                ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("Extensions", "ErrorWhileInstalling"), "", SageMessageType.Error);
                lblLoadMessage.Visible = true;
                chkAcceptLicense.Checked = false;
                ViewState["ModuleInfo"] = null;
                activeIndex = 0;
            }
            else
            {
                lblInstallMessage.Text = GetSageMessage("Extensions", "ModuleInstalledSuccessfully");
                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("Extensions", "TheModuleIsInstalledSuccessfully"), "", SageMessageType.Success);
                wizInstall.DisplayCancelButton = false;
                activeIndex = 5;
            }
        }

        protected void wizInstall_ActiveStepChanged(object sender, EventArgs e)
        {
            switch (wizInstall.ActiveStepIndex)
            {
                case 1:
                    lblWarningMessage.Text = GetSageMessage("Extensions", "WarningMessageWillDeleteAllFiles");
                    chkRepairInstall.Checked = true;
                    break;
                case 2:
                    if (chkRepairInstall.Checked)
                    {
                        Button prevButton = GetWizardButton("StepNavigationTemplateContainerID", "CancelButton");
                        prevButton.Visible = false;
                    }
                    break;
            }
        }

        private Button GetWizardButton(string containerID, string buttonID)
        {
            Control navContainer = wizInstall.FindControl(containerID);
            Button button = null;
            if ((navContainer != null))
            {
                button = navContainer.FindControl(buttonID) as Button;
            }
            return button;
        }
        #region Uninstall Existing Module

        private void UninstallModule(ModuleInfo moduleInfo, bool deleteModuleFolder)
        {
            Installers installerClass = new Installers();
            string path = HttpContext.Current.Server.MapPath("~/");

            //checked if directory exist for current module foldername
            string moduleFolderPath = moduleInfo.InstalledFolderPath;
            if (!string.IsNullOrEmpty(moduleFolderPath))
            {
                if (Directory.Exists(moduleFolderPath))
                {
                    //check for valid .sfe file exist or not
                    string fileName = installerClass.checkFormanifestFile(moduleFolderPath, moduleInfo);
                    if (fileName != "")
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(moduleFolderPath + '\\' + fileName);
                        XmlElement root = doc.DocumentElement;
                        if (installerClass.checkValidManifestFile(root, moduleInfo))
                        {
                            XmlNodeList xnList = doc.SelectNodes("sageframe/folders/folder");
                            foreach (XmlNode xn in xnList)
                            {
                                moduleInfo.ModuleName = xn["modulename"].InnerXml.ToString();
                                moduleInfo.FolderName = xn["foldername"].InnerXml.ToString();

                                if (!String.IsNullOrEmpty(moduleInfo.ModuleName) && !String.IsNullOrEmpty(moduleInfo.FolderName) && installerClass.IsModuleExist(moduleInfo.ModuleName.ToLower(), moduleInfo))
                                {

                                }
                                else
                                {
                                    ShowMessage(SageMessageTitle.Exception.ToString(), GetSageMessage("Extensions", "ThisModuleSeemsToBeCorrupted"), "", SageMessageType.Error);
                                }
                            }
                            try
                            {
                                if (moduleInfo.ModuleID > 0)
                                {
                                    //Run script  
                                    ReadUninstallScriptAndDLLFiles(doc, moduleFolderPath, installerClass);
                                    //Rollback moduleid
                                    installerClass.ModulesRollBack(moduleInfo.ModuleID, GetPortalID);
                                    if (deleteModuleFolder == true)
                                    {
                                        //Delete Module's Folder
                                        installerClass.DeleteTempDirectory(moduleInfo.InstalledFolderPath);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Exceptions = ex.Message;
                            }

                            if (Exceptions != string.Empty)
                            {
                                ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("Extensions", "ModuleExtensionIsUninstallError"), "", SageMessageType.Alert);
                            }
                            else
                            {
                                string ExtensionMessage = GetSageMessage("Extensions", "ModuleExtensionIsUninstalledSuccessfully");
                                //UninstallProcessCancelRequestBase(Request.RawUrl, true, ExtensionMessage);
                            }
                        }
                        else
                        {
                            ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("Extensions", "ThisPackageIsNotValid"), "", SageMessageType.Alert);
                        }
                    }
                    else
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("Extensions", "ThisPackageDoesNotAppearToBeValid"), "", SageMessageType.Alert);
                    }
                }
                else
                {
                    ShowMessage(SageMessageTitle.Exception.ToString(), GetSageMessage("Extensions", "ModuleFolderDoesnotExist"), "", SageMessageType.Error);
                }
            }
        }

        private void ReadUninstallScriptAndDLLFiles(XmlDocument doc, string moduleFolderPath, Installers installerClass)
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
                            Exceptions = ex.Message;
                        }
                    }
                    if (_unistallScriptFile != "")
                    {
                        RunUninstallScript(_unistallScriptFile, moduleFolderPath, installerClass);
                    }
                    DeleteAllDllsFromBin(dllFiles, moduleFolderPath);
                }
            }
        }

        private void RunUninstallScript(string _unistallScriptFile, string moduleFolderPath, Installers installerClass)
        {
            Exceptions = installerClass.ReadSQLFile(moduleFolderPath, _unistallScriptFile);
        }

        private void DeleteAllDllsFromBin(ArrayList dllFiles, string moduleFolderPath)
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
                Exceptions = ex.Message;
            }
        }

        #endregion

        private void BindPackage()
        {
            if (ViewState["CompositeModule"] != null)
            {
                CompositeModule package = (CompositeModule)ViewState["CompositeModule"];                
                ViewState["CompositeModule"] = package;
                if (package.Components != null && package.Components.Count > 0)
                {
                    gdvModule.DataSource = package.Components;
                    gdvModule.DataBind();
                }
            }
            else if (ViewState["ModuleInfo"] != null)
            {
                ModuleInfo moduleInfo = installhelp.fillModuleInfo(module);
                ViewState["ModuleInfo"] = moduleInfo;
                List<ModuleInfo> list = new List<ModuleInfo>();
                list.Add(moduleInfo);
                gdvModule.Columns[4].Visible = false;
                gdvModule.DataSource = list;
                gdvModule.DataBind();

                this.lblReleaseNotesD.Text = moduleInfo.ReleaseNotes;
                this.lblLicenseD.Text = moduleInfo.License;
            }
        }

        protected void wizInstall_CancelButtonClick(object sender, EventArgs e)
        {
            try
            {
                module = (ModuleInfo)ViewState["ModuleInfo"];
                if (module != null)
                {
                    installhelp.DeleteTempDirectory(module.TempFolderPath);
                    ViewState["ModuleInfo"] = null;
                }
                else
                {

                    CompositeModule package = (CompositeModule)ViewState["CompositeModule"];
                    if (package != null)
                    {
                        installhelp.DeleteTempDirectory(package.TempFolderPath);


                        ViewState["CompositeModule"] = null;
                    }
                }
                //Redirect to Definitions page
                ReturnBack();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void wizInstall_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            try
            {
                module = (ModuleInfo)ViewState["ModuleInfo"];
                if (module != null)
                {
                    installhelp.DeleteTempDirectory(module.TempFolderPath);
                    ViewState["ModuleInfo"] = null;
                }
                else
                {
                    CompositeModule package = (CompositeModule)ViewState["CompositeModule"];
                    if (package != null)
                    {
                        installhelp.DeleteTempDirectory(package.TempFolderPath);


                        ViewState["CompositeModule"] = null;
                    }
                }
                ReturnBack();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void ReturnBack()
        {
            ProcessCancelRequest(Request.RawUrl);
        }
    }
}







