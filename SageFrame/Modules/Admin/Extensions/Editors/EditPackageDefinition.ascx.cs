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
using SageFrame.Web.Utilities;
using SageFrame.SageFrameClass;
using SageFrame.Core;
using SageFrame.Core.Services.Installer;
using System.Web.Hosting;
using System.IO;

#endregion

namespace SageFrame.Modules.Admin.Extensions.Editors
{
    public partial class EditPackageDefinition : BaseAdministrationUserControl
    {
        private string packageName = string.Empty;
        private string description = string.Empty;
        private string license = string.Empty;
        private string releaseNotes = string.Empty;
        private string owner = string.Empty;
        private string organization = string.Empty;
        private string url = string.Empty;
        private string email = string.Empty;
        private string firstVersion = "01";
        private string secondVersion = "00";
        private string lastVersion = "00";
        public CompositeModule Package = new CompositeModule();

        protected void Page_Init(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "GlobalVariable1", " var CancelURL='" + ResolveUrl("~/") + "Admin/Modules" + SageFrameSettingKeys.PageExtension + "';", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "GlobalVariable2", " var sageRootPath='" + ResolveUrl("~/") + "';", true);            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtPackageName.Text = packageName;
                    txtDescription.Text = description;
                    txtLicense.Text = license;
                    txtReleaseNotes.Text = releaseNotes;
                    txtOwner.Text = owner;
                    txtOrganization.Text = organization;
                    txtUrl.Text = url;
                    txtEmail.Text = email;
                    BindVersionDropdownlist();
                    ddlFirst.SelectedIndex = ddlFirst.Items.IndexOf(ddlFirst.Items.FindByValue(firstVersion));
                    ddlSecond.SelectedIndex = ddlSecond.Items.IndexOf(ddlSecond.Items.FindByValue(secondVersion));
                    ddlLast.SelectedIndex = ddlLast.Items.IndexOf(ddlLast.Items.FindByValue(lastVersion));
                    //AddImageUrls();
                    BindControls();

                }
            }
            catch (Exception ex)
            {

                ProcessException(ex);
            }
        }


        private void AddImageUrls()
        {
            //imbCreate.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
            //imbBack.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);
        }
        protected void BindControls()
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                SQLHandler sqlH = new SQLHandler();
                List<ModuleInfo> lstAvailableModules = new List<ModuleInfo>();
                lstAvailableModules = sqlH.ExecuteAsList<ModuleInfo>("sp_ModulesGetByPortalID", ParaMeterCollection);
                lbAvailableModules.DataSource = lstAvailableModules;
                lbAvailableModules.DataTextField = "ModuleName";
                lbAvailableModules.DataValueField = "ModuleID";
                lbAvailableModules.DataBind();
                ViewState["ModulesList"] = lstAvailableModules;

            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

        }
        private void BindVersionDropdownlist()
        {
            try
            {
                ddlFirst.DataSource = SageFrameLists.VersionType();
                ddlFirst.DataBind();
                ddlSecond.DataSource = SageFrameLists.VersionType();
                ddlSecond.DataBind();
                ddlLast.DataSource = SageFrameLists.VersionType();
                ddlLast.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imbCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string tmpFoldName = String.Format("{0}", DateTime.Now.ToString("dd-MMM-yyyy.hhmmssffff"));
                string tempFolderPath = HostingEnvironment.ApplicationPhysicalPath + GetTempPath(tmpFoldName);
                Package.Name = txtPackageName.Text.ToString();
                Package.FriendlyName = txtFriendlyName.Text.ToString();
                Package.Description = txtDescription.Text.ToString();
                string version = ddlFirst.Text.ToString() + "." + ddlSecond.Text.ToString() + "." + ddlLast.Text.ToString();
                Package.Version = version;
                Package.License = txtLicense.Text.ToString();
                Package.ReleaseNotes = txtReleaseNotes.Text.ToString();
                Package.Owner = txtOwner.Text.ToString();
                Package.Organization = txtOrganization.Text.ToString();
                Package.URL = txtUrl.Text.ToString();
                Package.Email = txtEmail.Text.ToString();
                Package.PackageType = "Composite";
                if (lbModulesList.Items.Count > 0)
                {
                    List<ModuleInfo> modulesList = (List<ModuleInfo>)ViewState["ModulesList"];
                    foreach (ListItem li in lbModulesList.Items)
                    {
                        foreach (ModuleInfo Modules in modulesList)
                        {
                            if (Modules.ModuleID.ToString() == li.Value.ToString())
                            {
                                Component comp = new Component();
                                comp.Name = Modules.ModuleName;
                                comp.FriendlyName = Modules.FriendlyName;
                                comp.Description = Modules.Description;
                                comp.Version = Modules.Version;
                                comp.BusinesscontrollerClass = Modules.BusinessControllerClass;
                                comp.ZipFile = Modules.ModuleName + ".zip";
                                Package.Components.Add(comp);
                            }
                        }
                    }

                }
                SfeWriter writer = new SfeWriter(Package);
                writer.Package = Package;
                txtPackageName.Text = "";
                txtDescription.Text = "";
                txtLicense.Text = "";
                txtReleaseNotes.Text = "";
                txtOwner.Text = "";
                txtOrganization.Text = "";
                txtUrl.Text = "";
                txtEmail.Text = "";
                writer.CreatePackage(Package.Name, Package.Name, this.Context.Response, tempFolderPath);//, this.Context.Response                
                txtEmail.Text = "";
            }
            catch (Exception)
            {
            }
        }

        protected void imbBack_Click(object sender, ImageClickEventArgs e)
        {
            ProcessCancelRequest(Request.RawUrl);
        }

        private string GetTempPath(string tmpFoldName)
        {
            string path = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Resources");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string folderPath = Path.Combine(path, "temp");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            folderPath = Path.Combine(folderPath, "CompositeModules");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            folderPath = Path.Combine(folderPath, tmpFoldName);
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath);
            }
            Directory.CreateDirectory(folderPath);
            return "Resources\\temp\\CompositeModules\\" + tmpFoldName + "\\";
        }

        protected void add_Click(object sender, EventArgs e)
        {
            string error = "The item ";
            bool isshow = false;
            if (lbAvailableModules.SelectedItem != null)
            {
                foreach (ListItem item in lbAvailableModules.Items)
                {
                    if (item.Selected == true)
                    {
                        if (lbModulesList.Items.FindByValue(item.Value.ToString()) != null)
                        {
                            error += item.Text + ",";
                            isshow = true;
                        }
                        else
                        {
                            lbModulesList.Items.Add(lbAvailableModules.Items.FindByValue(item.Value.ToString()));
                        }

                    }
                }
                if (isshow)
                    Show(error + " Already exists");
            }

        }
        protected void remove_Click(object sender, EventArgs e)
        {
            if (lbModulesList.SelectedItem != null)
            {
                while (lbModulesList.SelectedItem != null)
                {
                    lbModulesList.Items.Remove(lbModulesList.SelectedItem);

                }
            }
            else
            {
                Show(" No items selected to remove");
            }
        }
        protected void ReturnBack()
        {
            ProcessCancelRequest(Request.RawUrl);
            //txtPackageName.Text = packageName;
            //txtDescription.Text = description;
            //txtLicense.Text = license;
            //txtReleaseNotes.Text = releaseNotes;
            //txtOwner.Text = owner;
            //txtOrganization.Text = organization;
            //txtUrl.Text = url;
            //txtEmail.Text = email;
            //BindVersionDropdownlist();
            //ddlFirst.SelectedIndex = ddlFirst.Items.IndexOf(ddlFirst.Items.FindByValue(firstVersion));
            //ddlSecond.SelectedIndex = ddlSecond.Items.IndexOf(ddlSecond.Items.FindByValue(secondVersion));
            //ddlLast.SelectedIndex = ddlLast.Items.IndexOf(ddlLast.Items.FindByValue(lastVersion));
            //lbModulesList.Items.Clear();
            //AddImageUrls();
        }
        protected void Show(string error)
        {
            Page page = HttpContext.Current.Handler as Page;
            if (page != null)
            {
                error = error.Replace("'", "\'");
                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);
            }
        }
    }
}
