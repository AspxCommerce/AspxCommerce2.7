#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.RolesManagement;
using SageFrame.Security;
using SageFrame.Security.Entities;
using SageFrame.Security.Helpers;
using System.Text;
using SageFrame.Pages;
#endregion

namespace SageFrame.Modules.Admin.UserManagement
{
    public partial class ctl_ManageRoles : BaseAdministrationUserControl
    {
        public int userModuleID = 0;
        public Guid RoleID;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                userModuleID = int.Parse(SageUserModuleID);
                IncludeJs("PageRoleSettingsJS", "/Modules/Admin/UserManagement/js/PageRoleSettings.js");
                IncludeCss("PageRoleSettings", "/Modules/Admin/UserManagement/css/module.css");
                BuildAccessControlledSelection();
                GetSuperRoleID();
                if (!IsPostBack)
                {
                    pnlRole.Visible = false;
                    pnlRoles.Visible = true;
                    panelDashboardRoles.Visible = false;
                    pnlPageRoleSettings.Visible = false;
                    BindRoles();
                    BindListBoxRole();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected void BuildAccessControlledSelection()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfRadiobutton'>");
            sb.Append("<label id='portalPages' class='sfActive'><input type='radio' id='rdbPortalPages' value='0' checked='checked' name='PageMode' style='display:none;'/>");
            sb.Append("Portal Pages</label>");
            sb.Append("<label id='adminPages'><input type='radio' id='rdbAdmin' name='PageMode' value='1' style='display:none;'/>");
            sb.Append("Admin Pages</label>");
            sb.Append("</div>");
            ltrPagesRadioButtons.Text = sb.ToString();
        }
        protected void GetSuperRoleID()
        {
            PageController objCon = new PageController();
            RoleID = objCon.GetSuperRoleID();
        }
        protected void gdvRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (SystemSetting.SYSTEM_ROLES.Contains(e.Row.Cells[0].Text, StringComparer.OrdinalIgnoreCase))
                {
                    LinkButton btnDelete = (LinkButton)e.Row.FindControl("imbDelete");
                    btnDelete.Visible = false;
                }
                else
                {
                    LinkButton btnDelete = (LinkButton)e.Row.FindControl("imbDelete");
                    btnDelete.Attributes.Add("onclick", "javascript:return confirm('" + GetSageMessage("UserManagement", "AreYouSureToDelete") + "')");
                }
            }
        }

        private void AddDeleteCommandFieldInGrid()
        {
            CommandField field = new CommandField();
            field.ButtonType = ButtonType.Image;
            field.DeleteImageUrl = GetTemplateImageUrl("imgdelete.png", true);
            field.ShowDeleteButton = true;
            field.ShowHeader = false;
            gdvRoles.Columns.Add(field);
            gdvRoles.DataBind();
        }

        protected void imgAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string rolePrefix = GetPortalSEOName + "_";
                RoleInfo objRole = new RoleInfo();
                objRole.ApplicationName = Membership.ApplicationName;
                objRole.RoleName = txtRole.Text.Trim();
                objRole.PortalID = GetPortalID;
                objRole.IsActive = 1;
                objRole.AddedOn = DateTime.Now;
                objRole.AddedBy = GetUsername;
                if (txtRole.Text.ToLower().Equals("superuser"))
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "ThisRoleAlreadyExists"), "", SageMessageType.Error);
                }
                else
                {
                    RoleController r = new RoleController();
                    RoleCreationStatus status = new RoleCreationStatus();
                    r.CreateRole(objRole, out status);
                    if (status == RoleCreationStatus.DUPLICATE_ROLE)
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "ThisRoleAlreadyExists"), "", SageMessageType.Error);
                    }
                    else if (status == RoleCreationStatus.SUCCESS)
                    {
                        BindRoles();
                        BindListBoxRole();
                        pnlRole.Visible = false;
                        pnlRoles.Visible = true;
                        ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("UserManagement", "RoleSavedSuccessfully"), "", SageMessageType.Success);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imgCancel_Click(object sender, EventArgs e)
        {
            try
            {
                pnlRole.Visible = false;
                pnlRoles.Visible = true;
                pnlPageRoleSettings.Visible = false;
                panelDashboardRoles.Visible = false;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        private void DeleteRole(string role, string roleid)
        {
            try
            {
                if (SystemSetting.SYSTEM_ROLES.Contains(role, StringComparer.OrdinalIgnoreCase))
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "ThisIsSystemRoleAndCannotBeDeleted"), "", SageMessageType.Alert);
                }
                else
                {
                    Guid RoleID = new Guid(roleid);
                    RoleController roleObj = new RoleController();
                    roleObj.DeleteRole(RoleID, GetPortalID);
                    BindRoles();
                    BindListBoxRole();
                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("UserManagement", "RoleIsDeletedSuccessfully"), "", SageMessageType.Success);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
                ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "RoleCannnotBeDeleted"), "", SageMessageType.Error);
            }
        }

        private void BindRoles()
        {
            try
            {
                DataTable dtRoles = new DataTable();
                dtRoles.Columns.Add("Role");
                dtRoles.Columns.Add("RoleID");
                dtRoles.AcceptChanges();
                RolesManagementController objController = new RolesManagementController();
                List<RolesManagementInfo> objRoles = objController.PortalRoleList(GetPortalID, true, GetUsername);
                foreach (RolesManagementInfo role in objRoles)
                {
                    string roleName = role.RoleName;
                    if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                    {
                        DataRow dr = dtRoles.NewRow();
                        dr["Role"] = roleName;
                        dr["RoleID"] = role.RoleId;
                        dtRoles.Rows.Add(dr);
                    }
                    else
                    {
                        string rolePrefix = GetPortalSEOName + "_";
                        roleName = roleName.Replace(rolePrefix, "");
                        DataRow dr = dtRoles.NewRow();
                        dr["Role"] = roleName;
                        dr["RoleID"] = role.RoleId;
                        dtRoles.Rows.Add(dr);
                    }
                }
                gdvRoles.DataSource = dtRoles;
                gdvRoles.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imbAddNewRole_Click(object sender, EventArgs e)
        {
            try
            {
                txtRole.Text = "";
                pnlRole.Visible = true;
                pnlRoles.Visible = false;
                pnlPageRoleSettings.Visible = false;
                panelDashboardRoles.Visible = false;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string RoleID = gdvRoles.DataKeys[int.Parse(e.CommandArgument.ToString())]["RoleID"].ToString();
                string Role = gdvRoles.DataKeys[int.Parse(e.CommandArgument.ToString())]["Role"].ToString();
                switch (e.CommandName.ToString())
                {
                    case "Delete":
                        DeleteRole(Role, RoleID);
                        break;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected void imbPageRoleSettings_Click(object sender, EventArgs e)
        {
            try
            {
                txtRole.Text = "";
                pnlRole.Visible = false;
                pnlRoles.Visible = false;
                pnlPageRoleSettings.Visible = true;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        #region AdminRolemanagement

        protected void btnAddAllRole_Click(object sender, EventArgs e)
        {
            int Count = lstUnselectedRoles.Items.Count;
            List<ListItem> addAllRoles = new List<ListItem>();
            for (int i = 0; i < Count; i++)
            {
                if (lstUnselectedRoles.Items[i].Text.ToLower() == "anonymous user")
                {
                    ShowMessage("", GetSageMessage("UserManagement", "CannotSwitchAnonymousUser"), "", SageMessageType.Alert);
                }
                else
                {
                    lstSelectedRoles.Items.Add(lstUnselectedRoles.Items[i]);
                    addAllRoles.Add(lstUnselectedRoles.Items[i]);
                }
            }
            foreach (ListItem remRole in addAllRoles)
            {
                lstUnselectedRoles.Items.Remove(remRole);
            }
        }

        protected void btnAddRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstUnselectedRoles.SelectedIndex != -1)
                {
                    int[] selectedIndexs = lstUnselectedRoles.GetSelectedIndices();
                    for (int i = selectedIndexs.Length - 1; i >= 0; i--)
                    {
                        if (lstUnselectedRoles.Items[selectedIndexs[i]].Text.ToLower() == "anonymous user")
                        {
                            ShowMessage("", GetSageMessage("UserManagement", "CannotSwitchAnonymousUser"), "", SageMessageType.Alert);
                        }
                        else
                        {
                            lstSelectedRoles.Items.Add(lstUnselectedRoles.Items[selectedIndexs[i]]);
                            lstUnselectedRoles.Items.Remove(lstUnselectedRoles.Items[selectedIndexs[i]]);
                        }
                    }
                    lstUnselectedRoles.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void btnRemoveRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstSelectedRoles.SelectedIndex != -1)
                {
                    int[] selectedIndexs = lstSelectedRoles.GetSelectedIndices();
                    for (int i = selectedIndexs.Length - 1; i >= 0; i--)
                    {
                        if (lstSelectedRoles.Items.Count > 0)
                        {
                            if (lstSelectedRoles.Items[selectedIndexs[i]].Text.ToLower() == "super user")
                            {
                                ShowMessage("", GetSageMessage("UserManagement", "CannotSwitchSuperUser"), "", SageMessageType.Alert);
                            }
                            else
                            {
                                lstUnselectedRoles.Items.Add(lstSelectedRoles.Items[selectedIndexs[i]]);
                                lstSelectedRoles.Items.Remove(lstSelectedRoles.Items[selectedIndexs[i]]);
                            }
                        }
                    }
                    lstSelectedRoles.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void btnRemoveAllRole_Click(object sender, EventArgs e)
        {
            try
            {
                int Count = lstSelectedRoles.Items.Count;
                List<ListItem> remRoles = new List<ListItem>();
                for (int i = 0; i < Count; i++)
                {
                    if (lstSelectedRoles.Items[i].Text.ToLower() == "super user")
                    {
                        ShowMessage("", GetSageMessage("UserManagement", "CannotSwitchSuperUser"), "", SageMessageType.Alert);
                    }
                    else
                    {
                        lstUnselectedRoles.Items.Add(lstSelectedRoles.Items[i]);
                        remRoles.Add(lstSelectedRoles.Items[i]);
                    }
                }

                foreach (ListItem remRole in remRoles)
                {
                    lstSelectedRoles.Items.Remove(remRole);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void BindListBoxRole()
        {
            lstSelectedRoles.Items.Clear();
            lstUnselectedRoles.Items.Clear();
            RolesManagementController objRoleController = new RolesManagementController();
            List<RolesManagementInfo> objRoleList = objRoleController.DashboardRoleList(GetPortalID);
            foreach (RolesManagementInfo role in objRoleList)
            {
                if (role.IsActive == true)
                {
                    lstSelectedRoles.Items.Add(new ListItem(role.RoleName, role.RoleId.ToString()));
                }
                else
                {
                    lstUnselectedRoles.Items.Add(new ListItem(role.RoleName, role.RoleId.ToString()));
                }
            }
        }

        protected void imgManageRoleSave_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedRoles = GetListBoxText(lstSelectedRoles);
                RolesManagementController objRoleController = new RolesManagementController();
                objRoleController.UpdateDashboardRoleList(GetPortalID, selectedRoles, GetUsername);
                BindListBoxRole();
                ShowMessage("", GetSageMessage("UserManagement", "SuccessfullyUpdatedDashboardRoles"), "", SageMessageType.Success);

            }
            catch (Exception ex)
            {
                ProcessException(ex);
                ShowMessage("", GetSageMessage("UserManagement", "UnknownErrorOccur"), "", SageMessageType.Error);
            }
        }
        private string GetListBoxText(ListBox lstBox)
        {
            string selectedRoles = string.Empty;
            foreach (ListItem li in lstBox.Items)
            {
                string roleName = li.Value;
                if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                {
                    selectedRoles += roleName + ",";
                }
                else
                {
                    selectedRoles += roleName + ",";
                }
            }
            if (selectedRoles.Length > 0)
            {
                selectedRoles = selectedRoles.Substring(0, selectedRoles.Length - 1);
            }
            return selectedRoles;
        }
        protected void imbDashboardRoleSettings_Click(object sender, EventArgs e)
        {
            try
            {
                txtRole.Text = "";
                pnlRole.Visible = false;
                pnlRoles.Visible = false;
                pnlPageRoleSettings.Visible = false;
                panelDashboardRoles.Visible = true;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected void imgManageRoleCancel_Click(object sender, EventArgs e)
        {
            try
            {
                pnlRole.Visible = false;
                pnlRoles.Visible = true;
                pnlPageRoleSettings.Visible = false;
                panelDashboardRoles.Visible = false;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        #endregion

    }
}