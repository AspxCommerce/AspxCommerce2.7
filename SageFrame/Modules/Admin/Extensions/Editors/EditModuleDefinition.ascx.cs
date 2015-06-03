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
using SageFrame.Web;
using System.IO;
using Microsoft.VisualBasic;
using SageFrame.SageFrameClass;
using SageFrame.Modules;
using SageFrame.ModuleControls;
using SageFrame.Web.Utilities;
using RegisterModule;
using SageFrame.SageFrameClass.Services; 

#endregion

namespace SageFrame.Modules.Admin.Extensions.Editors
{

    /// <summary>
    /// <remarks>This class is used for Editing the module definitions 
    /// </remarks>
    /// </summary>
    public partial class EditModuleDefinition : BaseAdministrationUserControl
    {
        string path = HttpContext.Current.Server.MapPath("~/");
        string Exceptions = string.Empty;
        System.Nullable<Int32> _newModuleID = 0;
        System.Nullable<Int32> _newModuleDefID = 0;
        System.Nullable<Int32> _newPortalmoduleID = 0;
        System.Nullable<Int32> _moduleControlID = 0;


        /// <summary>
        /// This is for page load event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //AddImageUrls();
                    LoadOwnerFolders(string.Empty);
                    LoadModuleFolders(string.Empty);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        //private void AddImageUrls()
        //{
        //    imbCreate.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
        //    imbBack.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);
        //}

        /// <summary>
        /// This populates the owner folders
        /// </summary>
        /// <param name="selectedValue"></param>
        private void LoadOwnerFolders(string selectedValue)
        {
            ddlOwner.Items.Clear();
            string[] arrFolders = Directory.GetDirectories(path + "Modules\\");
            foreach (string strFolder in arrFolders)
            {
                if (!strFolder.ToLower().Contains(".svn"))
                {
                    //exclude Module folders
                    string[] files = Directory.GetFiles(strFolder, "*.ascx");
                    if (files.Length == 0 || strFolder.ToLower() == "admin")
                    {
                        ListItem item = new ListItem(strFolder.Replace(Path.GetDirectoryName(strFolder) + "\\", ""));
                        if (item.Value == selectedValue)
                        {
                            item.Selected = true;
                        }
                        ddlOwner.Items.Add(item);
                    }
                }
            }
            ddlOwner.Items.Insert(0, new ListItem("<Not Specified >", ""));
        }


        /// <summary>
        /// This populates the Module folders
        /// </summary>
        /// <param name="selectedValue"></param>
        private void LoadModuleFolders(string selectedValue)
        {
            ddlModule.Items.Clear();
            string[] arrFolders = Directory.GetDirectories(path + "Modules\\" + ddlOwner.SelectedItem.Value);
            foreach (string strFolder in arrFolders)
            {
                //exclude Admin folders
                if (!strFolder.ToLower().Contains(".svn"))
                {
                    ListItem item = new ListItem(strFolder.Replace(Path.GetDirectoryName(strFolder) + "\\", ""));
                    if (item.Value == selectedValue)
                    {
                        item.Selected = true;
                    }
                    ddlModule.Items.Add(item);
                }
            }
            ddlModule.Items.Insert(0, new ListItem("<Not Specified>", ""));
        }

        /// <summary>
        /// Loads the ascx files only 
        /// </summary>
        /// <param name="strExtensions"></param>
        public void LoadFiles(string strExtensions)
        {
            LoadFiles(strExtensions, "");
        }

        private void LoadFiles(string strExtensions, string strFolder)
        {
            if (string.IsNullOrEmpty(strFolder))
            {
                strFolder = Server.MapPath("~/Modules/" + GetSourceFolder());
            }

            ddlFiles.Items.Clear();
            string[] arrFiles = Directory.GetFiles(strFolder);
            foreach (string strFile in arrFiles)
            {
                if (Path.GetExtension(strFile) != "")
                {
                    if (strExtensions.Contains(Path.GetExtension(strFile)))
                    {
                        ddlFiles.Items.Add(Path.GetFileName(strFile));
                    }
                }
            }
        }

        /// <summary>
        /// Loads the image files only 
        /// </summary>
        /// <param name="strExtensions"></param>
        public void LoadIcons(string strExtensions)
        {
            LoadIcons(strExtensions, "");
        }

        private void LoadIcons(string strExtensions, string strFolder)
        {
            DropDownList ddlIcon = ((DropDownList)ModuleControlsDetails1.FindControl("ddlIcon"));
            ddlIcon.Items.Clear();
            ddlIcon.Items.Insert(0, new ListItem("<Not Specified>", ""));

            //if (!string.IsNullOrEmpty(ddlFiles.SelectedItem.Value))
            //{
            if (string.IsNullOrEmpty(strFolder))
            {
                strFolder = Server.MapPath("~/Modules/" + GetSourceFolder());
            }
            string[] arrFiles = Directory.GetFiles(strFolder);
            foreach (string strFile in arrFiles)
            {
                string strExtension = Path.GetExtension(strFile).Replace(".", "");
                if (strExtensions.Contains(strExtension))
                {
                    ddlIcon.Items.Add(new ListItem(Path.GetFileName(strFile), Path.GetFileName(strFile).ToLower()));
                }
            }
            // }
        }

        /// <summary>
        /// setup Owner Folder
        /// </summary>
        private void SetupOwnerFolders()
        {
            LoadModuleFolders(string.Empty);
            SetupModuleFolders();
        }

        /// <summary>
        /// Setup Module folders
        /// </summary>
        private void SetupModuleFolders()
        {
            switch (ddlCreate.SelectedItem.Value)
            {
                case "Control":
                    LoadFiles(".ascx");
                    break;
            }
        }

        /// <summary>
        /// Get source folder
        /// </summary>
        /// <remarks>
        /// To bind with ddlFiles</remarks>
        /// <returns></returns>
        private string GetSourceFolder()
        {
            string strFolder = string.Empty;
            if (!string.IsNullOrEmpty(ddlOwner.SelectedItem.Value))
            {
                strFolder += ddlOwner.SelectedItem.Value + "/";
            }
            if (!string.IsNullOrEmpty(ddlModule.SelectedItem.Value))
            {
                strFolder += ddlModule.SelectedItem.Value + "/";
            }
            return strFolder;
        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupModuleFolders();
            if (ddlFiles.SelectedIndex != -1)
            {
                LoadIcons(SystemSetting.glbImageFileTypes);
            }
        }

        protected void ddlOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupOwnerFolders();
        }

        protected void imbCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string strMessage = string.Empty;
                if (ddlFiles.SelectedItem != null)
                {
                    switch (ddlCreate.SelectedItem.Value)
                    {
                        case "Control":

                            string message = ImportControl(ddlFiles.SelectedItem.Value);
                            ProcessCancelRequestBase(Request.RawUrl, false, message);                            
                            break;
                    }
                }
                else
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("Extensions", "ModuleExtensionSourceIsEmpty"), "", SageMessageType.Alert);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private string ImportControl(string controlSrc)
        {
            string ExtensionMessage = string.Empty;
            if (!string.IsNullOrEmpty(controlSrc))
            {
                DropDownList ddlFirst = ((DropDownList)PackageDetails1.FindControl("ddlFirst"));
                DropDownList ddlSecond = ((DropDownList)PackageDetails1.FindControl("ddlSecond"));
                DropDownList ddlLast = ((DropDownList)PackageDetails1.FindControl("ddlLast"));
                DropDownList ddlIcon = ((DropDownList)ModuleControlsDetails1.FindControl("ddlIcon"));

                TextBox txtKey = ((TextBox)ModuleControlsDetails1.FindControl("txtKey"));
                TextBox txtTitle = ((TextBox)ModuleControlsDetails1.FindControl("txtTitle"));
                TextBox txtHelpURL = ((TextBox)ModuleControlsDetails1.FindControl("txtHelpURL"));
                TextBox txtDisplayOrder = ((TextBox)ModuleControlsDetails1.FindControl("txtDisplayOrder"));
                //CheckBox chkSupportsPartialRendering = ((CheckBox)ModuleControlsDetails1.FindControl("chkSupportsPartialRendering"));
                DropDownList ddlType = ((DropDownList)ModuleControlsDetails1.FindControl("ddlType"));

                ModuleInfo objModule = new ModuleInfo();
                Installers install = new Installers();
                try
                {
                    string folder = RemoveTrailingSlash(GetSourceFolder());
                    string friendlyName = PackageDetails1.PackageName;
                    string name = GetClassName();
                    string moduleControl = "Modules/" + folder + "/" + controlSrc;

                    //add module and package tables
                    objModule.ModuleName = name;
                    objModule.Name = name;
                    objModule.FriendlyName = friendlyName;
                    objModule.Description = PackageDetails1.Description;
                    objModule.FolderName = folder;
                    objModule.Version = ddlFirst.SelectedValue + "." + ddlSecond.SelectedValue + "." + ddlLast.SelectedValue;//"01.00.00"; //new Version(1, 0, 0);
                    objModule.Owner = PackageDetails1.Owner;
                    objModule.Organization = PackageDetails1.Organization;
                    objModule.URL = PackageDetails1.Url;
                    objModule.Email = PackageDetails1.Email;
                    objModule.ReleaseNotes = PackageDetails1.ReleaseNotes;
                    objModule.License = PackageDetails1.License;
                    objModule.PackageType = "Module";
                    objModule.isPremium = true;
                    objModule.supportedFeatures = 0;
                    objModule.BusinessControllerClass = "";
                    objModule.CompatibleVersions = "";
                    objModule.dependencies = "";
                    objModule.permissions = "";
                    ModuleController objController = new ModuleController();
                    try
                    {
                        int[] outputValue;
                        outputValue = objController.AddModules(objModule, false, 0, true, DateTime.Now, GetPortalID, GetUsername);
                        objModule.ModuleID = outputValue[0];
                        objModule.ModuleDefID = outputValue[1];
                        _newModuleID = objModule.ModuleID;
                        _newModuleDefID = objModule.ModuleDefID;
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }

                    try
                    {
                        //insert into ProtalModule table
                        _newPortalmoduleID = objController.AddPortalModules(GetPortalID, _newModuleID, true, DateTime.Now, GetUsername);
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }
                    //install permission for the installed module in ModuleDefPermission table with ModuleDefID and PermissionID
                    try
                    {
                        // get the default module VIEW permissions
                        int _permissionIDView = objController.GetPermissionByCodeAndKey("SYSTEM_VIEW", "VIEW");
                        objController.AddModulePermission(_newModuleDefID, _permissionIDView, GetPortalID, _newPortalmoduleID, true, GetUsername, true, DateTime.Now, GetUsername);
                        int _permissionIDEdit = objController.GetPermissionByCodeAndKey("SYSTEM_EDIT", "EDIT");
                        objController.AddModulePermission(_newModuleDefID, _permissionIDEdit, GetPortalID, _newPortalmoduleID, true, GetUsername, true, DateTime.Now, GetUsername);
                    }
                    catch (Exception ex)
                    {
                        Exceptions += ex.Message;
                    }

                    try
                    {
                        //Logic for modulecontrol installation
                        string _moduleControlKey = txtKey.Text;
                        string _moduleControlTitle = txtTitle.Text;
                        string _moduleControlSrc = moduleControl;
                        string _moduleControlHelpUrl = txtHelpURL.Text;
                        //bool _moduleSupportsPartialRendering = chkSupportsPartialRendering.Checked;
                        bool _moduleSupportsPartialRendering = false;
                        int _controlType = int.Parse(ddlType.SelectedItem.Value);
                        string _iconFile = "";
                        if (ddlIcon.SelectedIndex != -1)
                        {
                            _iconFile = ddlIcon.SelectedItem.Value;
                        }
                        int _displayOrder = int.Parse(txtDisplayOrder.Text);

                        _moduleControlID = objController.AddModuleCoontrols(_newModuleDefID, _moduleControlKey, _moduleControlTitle, _moduleControlSrc,
                            _iconFile, _controlType, _displayOrder, _moduleControlHelpUrl, _moduleSupportsPartialRendering, true, DateTime.Now,
                            GetPortalID, GetUsername);
                        ExtensionMessage = GetSageMessage("Extensions", "ModuleExtensionIsAddedSuccessfully");
                    }
                    catch (Exception ex)
                    {
                        Exceptions = ex.Message;
                    }

                    if (Exceptions != string.Empty)
                    {
                        if (objModule.ModuleID > 0 && _newModuleDefID != null && _newModuleDefID > 0)
                        {
                            //Delete Module info from data base
                            install.ModulesRollBack(objModule.ModuleID, GetPortalID);
                        }
                    }
                }
                catch
                {
                    if (objModule.ModuleID > 0 && _newModuleDefID != null && _newModuleDefID > 0)
                    {
                        //Delete Module info from data base
                        install.ModulesRollBack(objModule.ModuleID, GetPortalID);
                    }
                }
            }
            return ExtensionMessage;
        }

        /// <summary>
        /// Removes trailing // for source folder
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        private string RemoveTrailingSlash(string strSource)
        {
            if (string.IsNullOrEmpty(strSource)) return "";
            if (Strings.Mid(strSource, Strings.Len(strSource), 1) == "\\" | Strings.Mid(strSource, Strings.Len(strSource), 1) == "/")
            {
                return strSource.Substring(0, Strings.Len(strSource) - 1);
            }
            else
            {
                return strSource;
            }
        }

        /// <summary>
        /// gives name for the package name
        /// </summary>
        /// <returns></returns>
        /// 
        private string GetClassName()
        {
            string strClass = string.Empty;
            if (!string.IsNullOrEmpty(ddlOwner.SelectedItem.Value))
            {
                strClass += ddlOwner.SelectedItem.Value + ".";
            }
            if (!string.IsNullOrEmpty(ddlModule.SelectedItem.Value))
            {
                strClass += ddlModule.SelectedItem.Value;
            }
            return strClass;
        }

        protected void imbBack_Click(object sender, EventArgs e)
        {
            ProcessCancelRequest(Request.RawUrl);
        }
    }
}