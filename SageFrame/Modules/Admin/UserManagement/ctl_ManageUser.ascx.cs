#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using SageFrame.Web;
using SageFrame.RolesManagement;
using SageFrame.UserManagement;
using SageFrame.Security.Entities;
using SageFrame.Security.Crypto;
using SageFrame.Security.Helpers;
using SageFrame.Security.Providers;
using SageFrame.Security;
using System.Text.RegularExpressions;
using SageFrame.Security.Enums;
using SageFrame.UserProfile;
using System.IO;
using SageFrame.Common;
using SageFrame.ExportUser;
using System.Data.OleDb;
using Microsoft.VisualBasic.FileIO;
using SageFrame.Security.Controllers;
#endregion

namespace SageFrame.Modules.Admin.UserManagement
{
    public partial class ctl_ManageUser : BaseAdministrationUserControl
    {
        MembershipController m = new MembershipController();
        RoleController role = new RoleController();
        List<ExportUserInfo> lstUserImportUsers = new List<ExportUserInfo>();
        List<ExportUserInfo> lstDuplicateUserList = new List<ExportUserInfo>();
        public int Flag = 0;
        string falseDate = "10/10/1900";
        string defaultDate = "1/1/0001";
        public string UserImportFilePath = string.Empty;
        public string ImportFilePath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            IncludeJs("UserManagement", false, "/js/jquery.pstrength-min.1.2.js");
            IncludeJsTop("UserManagement", "/js/jquery.validate.js", "/js/jquery.alerts.js");
            IncludeCss("UserManagement", "/css/jquery.alerts.css");
            imgProfileEdit.Visible = false;
            lblDuplicateUser.Visible = false;
            try
            {
                if (!IsPostBack)
                {
                    Session["csv"] = null;
                    aceSearchText.CompletionSetCount = GetPortalID;
                    BindRolesInListBox(lstAvailableRoles);
                    BindUsers(string.Empty);
                    PanelVisibility(false, true, false, false, false);
                    pnlSettings.Visible = false;
                    BindRolesInDropDown(ddlSearchRole);
                    AddImageUrls();
                    LoadSuspendedIp();
                    hideSubmit();
                }
                int index = rbFilterMode.SelectedIndex;
                rbFilterMode.Items[index].Attributes.Add("class", "active");
                RoleController _role = new RoleController();
                string[] roles = _role.GetRoleNames(GetUsername, GetPortalID).ToLower().Split(',');
                if (!roles.Contains(SystemSetting.SUPER_ROLE[0].ToLower()))
                {
                    imgBtnExportUser.Visible = false;
                    imgBtnImportUser.Visible = false;
                    imgBtnSuspendedIP.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void AddImageUrls()
        {
            //imgBack.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);
            //imgUserInfoSave.ImageUrl = GetTemplateImageUrl("imgupdate.png", true);
            //imgManageRoleSave.ImageUrl = GetTemplateImageUrl("imgupdate.png", true);
            //imgSearch.ImageUrl = GetTemplateImageUrl("imgpreview.png", true);
            //imgAddUser.ImageUrl = GetTemplateImageUrl("imgadduser.png", true);
            //imbBackinfo.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);
            //imgBtnDeleteSelected.ImageUrl = GetTemplateImageUrl("imgdelete.png", true);
            //imgBtnSaveChanges.ImageUrl = GetTemplateImageUrl("imgupdate.png", true);
            //imbCreateUser.ImageUrl = GetTemplateImageUrl("btnadduser1.png", true);
            //imgBtnSettings.ImageUrl = GetTemplateImageUrl("settings.png", true);
            //btnSaveSetting.ImageUrl = GetAdminImageUrl("btnsave.png", true);
            //btnCancel.ImageUrl = GetAdminImageUrl("imgcancel.png", true);
            // btnDeleteProfilePic.ImageUrl = GetAdminImageUrl("imgdelete.png", true);
        }

        private void PanelVisibility(bool VisibleUserPanel, bool VisibleUserListPanel, bool VisibleManageUserPanel, bool VisibleExportUserPanel, bool VisibleSuspendedIPPanel)
        {
            pnlUser.Visible = VisibleUserPanel;
            pnlUserList.Visible = VisibleUserListPanel;
            pnlManageUser.Visible = VisibleManageUserPanel;
            pnlUserImport.Visible = VisibleExportUserPanel;
            pnlSuspendedIP.Visible = VisibleSuspendedIPPanel;
        }

        private DataTable GetAllRoles()
        {
            DataTable dtRole = new DataTable();
            dtRole.Columns.Add("RoleID");
            dtRole.Columns.Add("RoleName");
            dtRole.AcceptChanges();
            RolesManagementController objController = new RolesManagementController();
            List<RolesManagementInfo> objRoles = objController.PortalRoleList(GetPortalID, false, GetUsername);
            foreach (RolesManagementInfo role in objRoles)
            {
                string roleName = role.RoleName;
                if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                {
                    DataRow dr = dtRole.NewRow();
                    dr["RoleID"] = role.RoleId;
                    dr["RoleName"] = roleName;
                    dtRole.Rows.Add(dr);
                }
                else
                {
                    string rolePrefix = GetPortalSEOName + "_";
                    roleName = roleName.Replace(rolePrefix, "");
                    DataRow dr = dtRole.NewRow();
                    dr["RoleID"] = role.RoleId;
                    dr["RoleName"] = roleName;
                    dtRole.Rows.Add(dr);
                }
            }
            return dtRole;
        }

        private void BindRolesInListBox(ListBox lst)
        {
            DataTable dtRoles = GetAllRoles();
            lst.DataSource = dtRoles;
            lst.DataTextField = "RoleName";
            lst.DataValueField = "RoleName";
            lst.DataBind();
            //lst.Items.RemoveAt(0);
        }

        private void BindRolesInDropDown(DropDownList ddl)
        {
            DataTable dtRoles = GetAllRoles();
            ddl.DataSource = dtRoles;
            ddl.DataTextField = "RoleName";
            ddl.DataValueField = "RoleID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("<Not Specified >", ""));
        }
        private void BindUsers(string searchText)
        {
            ViewState.Clear();
            string RoleID = ddlSearchRole.SelectedValue.ToString();
            if (Flag == 0)
            {
                MembershipController m = new MembershipController();
                List<UserInfo> lstUsers = m.SearchUsers(RoleID, searchText.Trim(), GetPortalID, GetUsername).UserList;
                gdvUser.DataSource = (lstUsers);
                gdvUser.DataBind();
                ViewState["UserList"] = lstUsers;
            }
            if (Flag == 1)
            {
                List<UserInfo> lstUsers = m.SearchUsers(RoleID, searchText.Trim(), GetPortalID, GetUsername).UserList;
                if (txtTo.Text != "" && txtFrom.Text == "")
                {
                    DateTime toDate = DateTime.Parse(txtTo.Text);
                    if (toDate == DateTime.Now.Date)
                    {
                        toDate = DateTime.Now;
                    }
                    List<UserInfo> filteredUsers = lstUsers.FindAll(delegate(UserInfo objUserInfo)
                    {
                        return objUserInfo.AddedOn <= toDate;
                    });
                    gdvUser.DataSource = (filteredUsers);
                    gdvUser.DataBind();
                    ViewState["UserList"] = filteredUsers;
                }
                if (txtFrom.Text != "" && txtTo.Text == "")
                {

                    List<UserInfo> filteredUsers = lstUsers.FindAll(delegate(UserInfo objUserInfo)
                    {
                        return objUserInfo.AddedOn >= DateTime.Parse(txtFrom.Text);
                    });
                    gdvUser.DataSource = (filteredUsers);
                    gdvUser.DataBind();
                    ViewState["UserList"] = filteredUsers;

                }
                if (txtFrom.Text != "" && txtTo.Text != "")
                {
                    DateTime toDate = DateTime.Parse(txtTo.Text);
                    DateTime fromDate = DateTime.Parse(txtFrom.Text);
                    if (fromDate <= toDate)
                    {
                        if (DateTime.Today.Date == toDate.Date)
                        {
                            toDate = DateTime.Now;
                        }

                        List<UserInfo> filteredUsers = lstUsers.FindAll(delegate(UserInfo objUserInfo)
                        {
                            return objUserInfo.AddedOn >= fromDate && objUserInfo.AddedOn <= toDate;
                        });
                        gdvUser.DataSource = (filteredUsers);
                        gdvUser.DataBind();
                        ViewState["UserList"] = filteredUsers;
                    }
                    else
                    {
                        ShowMessage("", GetSageMessage("UserManagement", "FromIsLowerThanTo"), "", SageMessageType.Error);
                    }
                }
            }
        }

        private List<UserInfo> ReorderUserList(List<UserInfo> lstUsers)
        {
            List<UserInfo> lstNewUsers = new List<UserInfo>();
            foreach (UserInfo user in lstUsers)
            {
                if (Regex.Replace(user.UserName.ToLower(), @"\s", "") == "superuser")
                {
                    lstNewUsers.Insert(0, user);
                }
                else
                {
                    lstNewUsers.Add(user);
                }
            }
            return lstNewUsers;
        }

        private void CheckForSuperuser(ref List<UserInfo> lstUsers)
        {
            foreach (UserInfo obj in lstUsers)
            {
                if (obj.UserName.ToLower().Equals("superuser"))
                {
                    lstUsers.Remove(obj);
                }
            }
        }

        protected void imgAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                PanelVisibility(true, false, false, false, false);
                ClearForm();
                lstAvailableRoles.SelectedIndex = lstAvailableRoles.Items.IndexOf(lstAvailableRoles.Items.FindByValue("Registered User"));
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private string GetListBoxText(ListBox lstBox)
        {
            string selectedRoles = string.Empty;
            foreach (ListItem li in lstBox.Items)
            {
                string roleName = li.Text;
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

        private string SelectedRoles()
        {
            string selectedRoles = string.Empty;
            foreach (ListItem li in lstAvailableRoles.Items)
            {
                if (li.Selected == true)
                {
                    string roleName = li.Text;
                    if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                    {
                        selectedRoles += roleName + ",";
                    }
                    else
                    {
                        string rolePrefix = GetPortalSEOName + "_";
                        selectedRoles += rolePrefix + roleName + ",";
                    }
                }
            }
            if (selectedRoles.Length > 0)
            {
                selectedRoles = selectedRoles.Substring(0, selectedRoles.Length - 1);
            }
            return selectedRoles;
        }

        private void ClearForm()
        {
            txtUserName.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtRetypePassword.Text = "";
            txtSecurityQuestion.Text = "";
            txtSecurityAnswer.Text = "";
        }

        protected void imbFinish_Click(object sender, EventArgs e)
        {
            try
            {
                BindUsers(string.Empty);
                PanelVisibility(false, true, false, false, false);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        public void GetSageUserInfo(string EditUserName)
        {
            try
            {
                UserInfo sageUser = m.GetUserDetails(GetPortalID, EditUserName);
                string[] Emails = sageUser.Email.Split(',');
                txtManageEmail.Text = Emails[0];
                txtManageFirstName.Text = sageUser.FirstName;
                txtManageLastName.Text = sageUser.LastName;
                txtManageUsername.Text = sageUser.UserName;
                chkIsActive.Checked = sageUser.IsApproved == true ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void gdvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (!e.CommandName.Equals("Page"))
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    if (gdvUser.PageIndex > 0)
                    {
                        rowIndex = int.Parse(e.CommandArgument.ToString()) - (gdvUser.PageSize * gdvUser.PageIndex);
                    }
                    hdnEditUsername.Value = gdvUser.DataKeys[rowIndex]["Username"].ToString();
                    hdnEditUserID.Value = gdvUser.DataKeys[rowIndex]["UserId"].ToString();
                    if (e.CommandName == "EditUser")
                    {
                        string username = gdvUser.DataKeys[rowIndex]["Username"].ToString();
                        string[] userRoles = Roles.GetRolesForUser(username);
                        UserInfo sageUser = m.GetUserDetails(GetPortalID, hdnEditUsername.Value);
                        hdnCurrentEmail.Value = sageUser.Email;
                        txtManageEmail.Text = sageUser.Email;
                        txtManageFirstName.Text = sageUser.FirstName;
                        txtManageLastName.Text = sageUser.LastName;
                        txtManageUsername.Text = sageUser.UserName;
                        chkIsActive.Checked = sageUser.IsApproved == true ? true : false;
                        if (SystemSetting.SYSTEM_USERS.Contains(hdnEditUsername.Value) || hdnEditUsername.Value == GetUsername)
                        {
                            chkIsActive.Enabled = false;
                            chkIsActive.Attributes.Add("class", "disabledClass");
                            tabUserRoles.Visible = false;
                        }
                        else
                        {
                            tabUserRoles.Visible = true;
                        }
                        txtCreatedDate.Text = sageUser.CreatedDate.ToShortDateString();
                        txtLastActivity.Text = sageUser.LastActivityDate.ToShortDateString();
                        txtLastLoginDate.Text = sageUser.LastLoginDate.ToShortDateString();
                        txtLastPasswordChanged.Text = sageUser.LastPasswordChangeDate.ToShortDateString();
                        if (!sageUser.IsApproved)
                        {
                            txtLastActivity.Text = "N/A";
                            txtLastLoginDate.Text = "N/A";
                            txtLastPasswordChanged.Text = "N/A";
                        }
                        lstSelectedRoles.Items.Clear();
                        lstUnselectedRoles.Items.Clear();
                        RolesManagementController objController = new RolesManagementController();
                        List<RolesManagementInfo> objRoles = objController.PortalRoleList(GetPortalID, false, GetUsername);
                        foreach (RolesManagementInfo role in objRoles)
                        {
                            string roleName = role.RoleName;
                            if (SystemSetting.SYSTEM_ROLES.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                            {
                                if (userRoles.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                                {
                                    lstSelectedRoles.Items.Add(new ListItem(roleName, roleName));
                                }
                                else
                                {
                                    lstUnselectedRoles.Items.Add(new ListItem(roleName, roleName));
                                }
                            }
                            else
                            {
                                if (userRoles.Contains(roleName, StringComparer.OrdinalIgnoreCase))
                                {
                                    string rolePrefix = GetPortalSEOName + "_";
                                    roleName = roleName.Replace(rolePrefix, "");
                                    lstSelectedRoles.Items.Add(new ListItem(roleName, roleName));
                                }
                                else
                                {
                                    string rolePrefix = GetPortalSEOName + "_";
                                    roleName = roleName.Replace(rolePrefix, "");
                                    lstUnselectedRoles.Items.Add(new ListItem(roleName, roleName));
                                }
                            }
                        }
                        if (userRoles.Contains("Super User") && username.ToLower() == "superuser")
                        {
                            btnAddAllRole.Enabled = false;
                            btnAddRole.Enabled = false;
                            btnRemoveAllRole.Enabled = false;
                            btnRemoveRole.Enabled = false;
                            lstUnselectedRoles.Enabled = false;
                            lstSelectedRoles.Enabled = false;
                        }
                        PanelVisibility(false, false, true, false, false);
                        LoadUserProfileData();
                    }
                    else if (e.CommandName == "DeleteUser")
                    {
                        if (hdnEditUsername.Value != "")
                        {
                            UserInfo user = new UserInfo(hdnEditUsername.Value, GetPortalID, Membership.ApplicationName, GetUsername);
                            m.DeleteUser(user);
                            ShowMessage("", GetSageMessage("UserManagement", "UserDeletedSuccessfully"), "", SageMessageType.Success);
                            BindUsers(string.Empty);
                        }
                        else
                        {
                            ShowMessage("", GetSageMessage("UserManagement", "SelectDeleteButtonOnceAgain"), "", SageMessageType.Alert);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        public void LoadUserProfileData()
        {
            tblEditProfile.Visible = false;
            tblViewProfile.Visible = true;
            LoadUserDetails();
        }

        protected void imgUserInfoSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnEditUsername.Value != "")
                {
                    if (txtManageFirstName.Text != "" && txtManageLastName.Text != "" && txtManageEmail.Text != "")
                    {
                        MembershipUser member = Membership.GetUser(hdnEditUsername.Value);
                        member.Email = txtManageEmail.Text;
                        if (!EmailAddressExists(txtManageEmail.Text, m.RequireUniqueEmail))
                        {
                            UserInfo user = new UserInfo(Membership.ApplicationName, hdnEditUsername.Value, new Guid(hdnEditUserID.Value), txtManageFirstName.Text, txtManageLastName.Text, txtManageEmail.Text, GetPortalID, chkIsActive.Checked, GetUsername);
                            UserUpdateStatus status = new UserUpdateStatus();
                            m.UpdateUser(user, out status);
                            if (status == UserUpdateStatus.DUPLICATE_EMAIL_NOT_ALLOWED)
                            {
                                ShowMessage("", GetSageMessage("UserManagement", "EmailAddressAlreadyIsInUse"), "", SageMessageType.Alert);
                            }
                            else if (status == UserUpdateStatus.USER_UPDATE_SUCCESSFUL)
                            {
                                BindUsers(string.Empty);
                                FilterUserGrid(int.Parse(rbFilterMode.SelectedValue.ToString()));
                                ShowMessage("", GetSageMessage("UserManagement", "UserInformationSaveSuccessfully"), "", SageMessageType.Success);
                                LoadUserDetails();
                            }
                        }
                        else
                        {
                            ShowMessage("", GetSageMessage("UserManagement", "EmailAddressAlreadyIsInUse"), "", SageMessageType.Alert);
                        }
                    }
                    else
                    {
                        ShowMessage("", GetSageMessage("UserManagement", "PleaseEnterTheRequiredFields"), "", SageMessageType.Alert);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected bool EmailAddressExists(string email, bool AllowDuplicateEmail)
        {
            bool status = false;
            Guid UserID = new Guid(hdnEditUserID.Value);
            if (!AllowDuplicateEmail)
            {
                SageFrameUserCollection userColl = m.GetAllUsers();
                status = userColl.UserList.Exists(
                            delegate(UserInfo obj)
                            {
                                return (obj.Email == email && obj.UserID != UserID);
                            }
                        );
            }
            return status;
        }

        protected void imgManageRoleSave_Click(object sender, EventArgs e)
        {
            try
            {
                string unselectedRoles = GetListBoxText(lstUnselectedRoles);
                string selectedRoles = GetListBoxText(lstSelectedRoles);
                if (hdnEditUsername.Value != "")
                {
                    string userRoles = role.GetRoleNames(hdnEditUsername.Value, GetPortalID);
                    string[] arrRoles = userRoles.Split(',');
                    UserInfo user = new UserInfo(Membership.ApplicationName, new Guid(hdnEditUserID.Value), userRoles, GetPortalID);
                    if (arrRoles.Length > 0 && selectedRoles.Length > 0)
                    {
                        role.ChangeUserInRoles(Membership.ApplicationName, new Guid(hdnEditUserID.Value), userRoles, selectedRoles, GetPortalID);
                        ShowMessage("", GetSageMessage("UserManagement", "UserRolesUpdatedSuccessfully"), "", SageMessageType.Success);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
                ShowMessage("", GetSageMessage("UserManagement", "UnknownErrorOccur"), "", SageMessageType.Error);
            }
        }

        protected void btnManagePasswordSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewPassword.Text != "" && txtRetypeNewPassword.Text != "" && txtNewPassword.Text == txtRetypeNewPassword.Text && hdnEditUsername.Value != "")
                {
                    if (txtNewPassword.Text.Length >= 4)
                    {
                        MembershipUser member = Membership.GetUser(hdnEditUsername.Value);
                        string Password, PasswordSalt;
                        PasswordHelper.EnforcePasswordSecurity(m.PasswordFormat, txtNewPassword.Text, out Password, out PasswordSalt);
                        UserInfo user = new UserInfo(new Guid(hdnEditUserID.Value), Password, PasswordSalt, m.PasswordFormat);
                        m.ChangePassword(user);
                        ShowMessage("", GetSageMessage("UserManagement", "UserPasswordChangedSuccessfully"), "", SageMessageType.Success);
                    }
                    else
                    {
                        ShowMessage("", GetSageMessage("UserManagement", "PasswordLength"), "", SageMessageType.Alert);
                    }
                }
                else
                {
                    ShowMessage("", GetSageMessage("UserManagement", "PleaseEnterTheRequiredField"), "", SageMessageType.Alert);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void btnAddAllRole_Click(object sender, EventArgs e)
        {
            foreach (ListItem li in lstUnselectedRoles.Items)
            {
                lstSelectedRoles.Items.Add(li);
            }
            lstUnselectedRoles.Items.Clear();
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
                        lstSelectedRoles.Items.Add(lstUnselectedRoles.Items[selectedIndexs[i]]);
                        lstUnselectedRoles.Items.Remove(lstUnselectedRoles.Items[selectedIndexs[i]]);
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
                        if (lstSelectedRoles.Items.Count > 1)
                        {
                            lstUnselectedRoles.Items.Add(lstSelectedRoles.Items[selectedIndexs[i]]);
                            lstSelectedRoles.Items.Remove(lstSelectedRoles.Items[selectedIndexs[i]]);
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
                List<string> remRoles = new List<string>();
                for (int i = 0; i < Count; i++)
                {
                    if (lstSelectedRoles.Items[i].Text.ToLower() != "super user")
                    {
                        lstUnselectedRoles.Items.Add(lstSelectedRoles.Items[i]);
                        remRoles.Add(lstSelectedRoles.Items[i].Text);
                    }
                }
                foreach (string remRole in remRoles)
                {
                    lstSelectedRoles.Items.Remove(remRole);
                }

            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imgBack_Click(object sender, EventArgs e)
        {
            try
            {
                PanelVisibility(false, true, false, false, false);
                BindUsers(string.Empty);
                FilterUserGrid(int.Parse(rbFilterMode.SelectedValue.ToString()));
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void txtSearchText_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BindUsers(txtSearchText.Text);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imgSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFrom.Text != "" || txtTo.Text != "")
                {
                    Flag = 1;
                }
                BindUsers(txtSearchText.Text);
                this.rbFilterMode.SelectedIndex = 0;
                ClearSearch();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        public void ClearSearch()
        {
            txtSearchText.Text = "";
            txtFrom.Text = "";
            txtTo.Text = "";
        }

        protected void gdvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HiddenField hdnIsActive = (HiddenField)e.Row.FindControl("hdnIsActive");
                LinkButton imgDelete = (LinkButton)e.Row.FindControl("imgDelete");
                LinkButton lnkUsername = (LinkButton)e.Row.FindControl("lnkUsername");
                LinkButton imgEdit = (LinkButton)e.Row.FindControl("imgEdit");
                HtmlInputCheckBox chkItem = (HtmlInputCheckBox)e.Row.FindControl("chkBoxItem");
                chkItem.Attributes.Add("onclick",
                                       "javascript:Check(this,'cssCheckBoxHeader','" + gdvUser.ClientID +
                                       "','cssCheckBoxItem');");
                HtmlInputCheckBox chkIsActiveItem = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsActiveItem");
                chkIsActiveItem.Attributes.Add("onclick",
                                               "javascript:Check(this,'cssCheckBoxIsActiveHeader','" + gdvUser.ClientID +
                                               "','cssCheckBoxIsActiveItem');");
                chkIsActiveItem.Checked = bool.Parse(hdnIsActive.Value);
                if (lnkUsername.Text.ToLower() == GetUsername.ToLower())
                {
                    imgDelete.Visible = false;
                    chkIsActiveItem.Disabled = true;
                    chkItem.Disabled = true;
                    chkItem.Attributes.Add("class", "disabledClass");
                    chkIsActiveItem.Attributes.Add("class", "disabledClass");
                }
                else if (lnkUsername.Text.ToLower() == "superuser")
                {
                    lnkUsername.Enabled = false;
                    imgEdit.Visible = false;
                    imgDelete.Visible = false;
                    chkIsActiveItem.Disabled = true;
                    chkItem.Disabled = true;
                    chkItem.Attributes.Add("class", "disabledClass");
                    chkIsActiveItem.Attributes.Add("class", "disabledClass");
                }
                else if (lnkUsername.Text.ToLower() == "superuser" && Roles.IsUserInRole(GetUsername, "Super User"))
                {
                    lnkUsername.Enabled = false;
                    imgEdit.Visible = false;
                    imgDelete.Visible = false;
                    chkIsActiveItem.Disabled = true;
                    chkItem.Disabled = true;
                    chkItem.Attributes.Add("class", "disabledClass");
                    chkIsActiveItem.Attributes.Add("class", "disabledClass");

                }
                else if (Roles.IsUserInRole(GetUsername, "Site Admin"))
                {
                    string[] userRoles = Roles.GetRolesForUser(lnkUsername.Text);
                    foreach (var userRole in userRoles)
                    {

                        if (userRole.ToLower() == SystemSetting.SUPER_ROLE[0].ToLower() || userRole.ToLower() == SystemSetting.SITEADMIN.ToString().ToLower())
                        {
                            lnkUsername.Enabled = false;
                            imgEdit.Visible = false;
                            imgDelete.Visible = false;
                            chkIsActiveItem.Disabled = true;
                            chkItem.Disabled = true;
                            chkItem.Attributes.Add("class", "disabledClass");
                            chkIsActiveItem.Attributes.Add("class", "disabledClass");
                        }
                    }
                }

            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                HtmlInputCheckBox chkHeader = (HtmlInputCheckBox)e.Row.FindControl("chkBoxHeader");
                chkHeader.Attributes.Add("onclick",
                                         "javascript:SelectAllCheckboxesSpecific(this,'" + gdvUser.ClientID +
                                         "','cssCheckBoxItem');");
                HtmlInputCheckBox chkIsActiveHeader = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsActiveHeader");
                chkIsActiveHeader.Attributes.Add("onclick",
                                                 "javascript:SelectAllCheckboxesSpecific(this,'" + gdvUser.ClientID +
                                                 "','cssCheckBoxIsActiveItem');");
            }
        }

        protected void imgBtnDeleteSelected_Click(object sender, EventArgs e)
        {
            try
            {
                string seletedUsername = string.Empty;
                for (int i = 0; i < gdvUser.Rows.Count; i++)
                {
                    HtmlInputCheckBox chkBoxItem = (HtmlInputCheckBox)gdvUser.Rows[i].FindControl("chkBoxItem");
                    if (chkBoxItem.Checked == true)
                    {
                        LinkButton lnkUsername = (LinkButton)gdvUser.Rows[i].FindControl("lnkUsername");
                        if (!SystemSetting.SYSTEM_DEFAULT_USERS.Contains(lnkUsername.Text.Trim(), StringComparer.OrdinalIgnoreCase))
                        {
                            seletedUsername = seletedUsername + lnkUsername.Text.Trim() + ",";
                        }

                    }
                }
                if (seletedUsername.Length > 1)
                {
                    seletedUsername = seletedUsername.Substring(0, seletedUsername.Length - 1);
                    UserManagementProvider.DeleteSelectedUser(seletedUsername, GetPortalID, GetUsername);
                    BindUsers(string.Empty);
                    ShowMessage("", GetSageMessage("UserManagement", "SelectedUsersAreDeletedSuccessfully"), "", SageMessageType.Success);
                }

            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imgBtnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                string seletedUsername = string.Empty;
                string IsActive = string.Empty;
                for (int i = 0; i < gdvUser.Rows.Count; i++)
                {
                    HtmlInputCheckBox chkBoxItem = (HtmlInputCheckBox)gdvUser.Rows[i].FindControl("chkBoxIsActiveItem");
                    LinkButton lnkUsername = (LinkButton)gdvUser.Rows[i].FindControl("lnkUsername");
                    seletedUsername = seletedUsername + lnkUsername.Text.Trim() + ",";
                    IsActive = IsActive + (chkBoxItem.Checked ? "1" : "0") + ",";
                }
                if (seletedUsername.Length > 1 && IsActive.Length > 0)
                {
                    seletedUsername = seletedUsername.Substring(0, seletedUsername.Length - 1);
                    IsActive = IsActive.Substring(0, IsActive.Length - 1);
                    UserManagementProvider.UpdateUsersChanges(seletedUsername, IsActive, GetPortalID, GetUsername);
                    this.rbFilterMode.SelectedIndex = 0;
                    ShowMessage("", GetSageMessage("UserManagement", "SelectedChangesAreSavedSuccessfully"), "", SageMessageType.Success);
                    BindUsers(string.Empty);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void ddlSearchRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindUsers(string.Empty);
                this.rbFilterMode.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ProcessCancelRequest(Request.RawUrl);
        }

        protected void ddlRecordsPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            gdvUser.PageSize = int.Parse(ddlRecordsPerPage.SelectedValue.ToString());
            if (ViewState["FilteredUser"] != null)
            {
                gdvUser.DataSource = ViewState["FilteredUser"];
            }
            else
            {
                gdvUser.DataSource = ViewState["UserList"];
            }
            gdvUser.DataBind();
            //BindUsers(txtSearchText.Text);
        }

        protected void gdvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvUser.PageIndex = e.NewPageIndex;
            if (ViewState["FilteredUser"] != null)
            {
                gdvUser.DataSource = ViewState["FilteredUser"];
            }
            else
            {
                gdvUser.DataSource = ViewState["UserList"];
            }
            gdvUser.DataBind();
        }
        protected void imbCreateUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (SystemSetting.SYSTEM_DEFAULT_USERS.Contains(txtUserName.Text.Trim(), StringComparer.OrdinalIgnoreCase))
                {
                    ShowMessage("", GetSageMessage("UserManagement", "ConflictInUserNameRoleName"), "", SageMessageType.Alert);
                }
                else
                {
                    if (txtUserName.Text != "" && txtSecurityQuestion.Text != "" && txtSecurityAnswer.Text != "" && txtFirstName.Text != "" && txtLastName.Text != "" && txtEmail.Text != "")
                    {
                        if (lstAvailableRoles.SelectedIndex > -1)
                        {
                            if (txtPassword.Text.Length >= 4)
                            {
                                UserInfo objUser = new UserInfo();
                                objUser.ApplicationName = Membership.ApplicationName;
                                objUser.FirstName = txtFirstName.Text.Trim();
                                objUser.UserName = txtUserName.Text.Trim();
                                objUser.LastName = txtLastName.Text.Trim();
                                string Password, PasswordSalt;
                                PasswordHelper.EnforcePasswordSecurity(m.PasswordFormat, txtPassword.Text, out Password, out PasswordSalt);
                                objUser.Password = Password;
                                objUser.PasswordSalt = PasswordSalt;
                                objUser.Email = txtEmail.Text;
                                objUser.SecurityQuestion = txtSecurityQuestion.Text;
                                objUser.SecurityAnswer = txtSecurityAnswer.Text;
                                objUser.IsApproved = true;
                                objUser.CurrentTimeUtc = DateTime.Now;
                                objUser.CreatedDate = DateTime.Now;
                                objUser.UniqueEmail = 0;
                                objUser.PasswordFormat = m.PasswordFormat;
                                objUser.PortalID = GetPortalID;
                                objUser.AddedOn = DateTime.Now;
                                objUser.AddedBy = GetUsername;
                                objUser.UserID = Guid.NewGuid();
                                objUser.RoleNames = GetSelectedRoleNameString();
                                objUser.StoreID = GetStoreID;
                                objUser.CustomerID = GetCustomerID;

                                UserCreationStatus status = new UserCreationStatus();
                                try
                                {
                                    MembershipDataProvider.CreatePortalUser(objUser, out status, UserCreationMode.CREATE);

                                    if (status == UserCreationStatus.DUPLICATE_USER)
                                    {
                                        ShowMessage("", GetSageMessage("UserManagement", "NameAlreadyExists"), "", SageMessageType.Alert);

                                    }
                                    else if (status == UserCreationStatus.DUPLICATE_EMAIL)
                                    {
                                        ShowMessage("", GetSageMessage("UserManagement", "EmailAddressAlreadyIsInUse"), "", SageMessageType.Alert);

                                    }
                                    else if (status == UserCreationStatus.SUCCESS)
                                    {
                                        PanelVisibility(false, true, false, false, false);
                                        BindUsers(string.Empty);
                                        ShowMessage("", GetSageMessage("UserManagement", "UserCreatedSuccessfully"), "", SageMessageType.Success);


                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                            else
                            {
                                ShowMessage("", GetSageMessage("UserManagement", "PasswordLength"), "", SageMessageType.Alert);
                            }
                        }
                        else
                        {
                            ShowMessage("", GetSageMessage("UserManagement", "PleaseSelectRole"), "", SageMessageType.Alert);

                        }
                    }
                    else
                    {
                        ShowMessage("", GetSageMessage("UserManagement", "PleaseEnterAllRequiredFields"), "", SageMessageType.Alert);
                    }

                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private string GetSelectedRoleNameString()
        {
            List<string> roleList = new List<string>();
            foreach (ListItem li in lstAvailableRoles.Items)
            {
                if (li.Selected == true)
                {
                    roleList.Add(li.Text);
                }
            }

            return (String.Join(",", roleList.ToArray()));
        }

        protected void imgBtnSettings_Click(object sender, EventArgs e)
        {
            PanelVisibility(false, false, false, false, false);
            pnlSettings.Visible = true;
            LoadSettings();
        }
        private void LoadSettings()
        {
            foreach (SettingInfo obj in MembershipDataProvider.GetSettings())
            {
                switch (obj.SettingKey)
                {
                    case "DUPLICATE_USERS_ACROSS_PORTALS":
                        chkEnableDupNames.Checked = obj.SettingValue.Equals("1") ? true : false;
                        break;
                    case "DUPLICATE_ROLES_ACROSS_PORTALS":
                        chkEnableDupRole.Checked = obj.SettingValue.Equals("1") ? true : false;
                        break;
                    case "SELECTED_PASSWORD_FORMAT":
                        SetPasswordFormat(int.Parse(obj.SettingValue));
                        break;
                    case "DUPLICATE_EMAIL_ALLOWED":
                        chkEnableDupEmail.Checked = obj.SettingValue.Equals("1") ? true : false;
                        break;
                    case "ENABLE_CAPTCHA":
                        chkEnableCaptcha.Checked = obj.SettingValue.Equals("1") ? true : false;
                        break;
                }
            }

        }
        protected void btnSaveSetting_Click(object sender, EventArgs e)
        {
            List<SettingInfo> lstSettings = new List<SettingInfo>();
            SettingInfo dupUsers = new SettingInfo();
            dupUsers.SettingKey = SettingsEnum.DUPLICATE_USERS_ACROSS_PORTALS.ToString();
            dupUsers.SettingValue = chkEnableDupNames.Checked ? "1" : "0";
            SettingInfo dupRoles = new SettingInfo();
            dupRoles.SettingKey = SettingsEnum.DUPLICATE_ROLES_ACROSS_PORTALS.ToString();
            dupRoles.SettingValue = chkEnableDupRole.Checked ? "1" : "0";
            SettingInfo passwordFormat = new SettingInfo();
            passwordFormat.SettingKey = SettingsEnum.SELECTED_PASSWORD_FORMAT.ToString();
            passwordFormat.SettingValue = GetPasswordFormat().ToString();
            SettingInfo dupEmail = new SettingInfo();
            dupEmail.SettingKey = SettingsEnum.DUPLICATE_EMAIL_ALLOWED.ToString();
            dupEmail.SettingValue = chkEnableDupEmail.Checked ? "1" : "0";
            SettingInfo enableCaptcha = new SettingInfo();
            enableCaptcha.SettingKey = SettingsEnum.ENABLE_CAPTCHA.ToString();
            enableCaptcha.SettingValue = chkEnableCaptcha.Checked ? "1" : "0";
            lstSettings.Add(dupUsers);
            lstSettings.Add(dupRoles);
            lstSettings.Add(passwordFormat);
            lstSettings.Add(dupEmail);
            lstSettings.Add(enableCaptcha);

            try
            {
                MembershipDataProvider.SaveSettings(lstSettings);
                ShowMessage("", GetSageMessage("UserManagement", "SettingSavedSuccessfully"), "", SageMessageType.Success);


            }
            catch (Exception)
            {
                throw;
            }
        }
        private int GetPasswordFormat()
        {
            int pwdFormat = (int)SettingsEnum.DEFAULT_PASSWORD_FORMAT;

            pwdFormat = int.Parse(rdbLst.SelectedValue.ToString());
            if (pwdFormat == 3)
            {
                pwdFormat = 3;
            }
            return pwdFormat;

        }
        private void SetPasswordFormat(int PasswordFormat)
        {
            if (PasswordFormat < 3)
            {
                rdbLst.SelectedIndex = PasswordFormat - 1;
            }
            else
            {
                rdbLst.SelectedIndex = 1;
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlSettings.Visible = false;
            PanelVisibility(false, true, false, false, false);

        }

        protected void rbFilterMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = rbFilterMode.SelectedIndex;
            rbFilterMode.Items[index].Attributes.Add("class", "active");
            FilterUserGrid(int.Parse(rbFilterMode.SelectedValue.ToString()));
        }

        protected void FilterUserGrid(int FilterMode)
        {
            List<UserInfo> lstUsers = (List<UserInfo>)ViewState["UserList"];
            List<UserInfo> lstNew = new List<UserInfo>();
            switch (FilterMode)
            {
                case 0:

                    gdvUser.DataSource = ReorderUserList(lstUsers);
                    gdvUser.DataBind();
                    ViewState["FilteredUser"] = lstUsers;
                    break;
                case 1:

                    foreach (UserInfo user in lstUsers)
                    {
                        if (user.IsActive)
                        {
                            lstNew.Add(user);
                        }
                    }
                    gdvUser.DataSource = ReorderUserList(lstNew);
                    gdvUser.DataBind();
                    ViewState["FilteredUser"] = lstNew;

                    break;
                case 2:
                    foreach (UserInfo user in lstUsers)
                    {
                        if (!user.IsActive)
                        {
                            lstNew.Add(user);
                        }
                    }
                    gdvUser.DataSource = ReorderUserList(lstNew);
                    gdvUser.DataBind();
                    ViewState["FilteredUser"] = lstNew;
                    break;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UserProfileInfo objinfo = new UserProfileInfo();
                string filename = "";
                string thumbTarget = Server.MapPath("~/Modules/Admin/UserManagement/UserPic");
                if (!Directory.Exists(thumbTarget))
                {
                    Directory.CreateDirectory(thumbTarget);
                }
                System.Drawing.Image.GetThumbnailImageAbort thumbnailImageAbortDelegate = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                if (fuImage.HasFile)
                {
                    double fs = fuImage.PostedFile.ContentLength / (1024 * 1024);
                    if (fs > 3)
                    {
                        ShowHideProfile();
                        ShowMessage("", GetSageMessage("UserManagement", "ImageTooLarge"), "", SageMessageType.Alert);
                        return;
                    }
                    else
                    {
                        filename = fuImage.PostedFile.FileName.Substring(fuImage.PostedFile.FileName.LastIndexOf("\\") + 1);
                        imgUser.ImageUrl = "~/Modules/Admin/UserManagement/UserPic/" + filename;
                        using (System.Drawing.Bitmap originalImage = new System.Drawing.Bitmap(fuImage.PostedFile.InputStream))
                        {
                            using (System.Drawing.Image thumbnail = originalImage.GetThumbnailImage(200, 150, thumbnailImageAbortDelegate, IntPtr.Zero))
                            {
                                thumbnail.Save(System.IO.Path.Combine(thumbTarget, fuImage.FileName));
                            }
                        }
                    }
                }
                if (filename == "")
                {
                    if (Session[SessionKeys.UserImage] != null)
                    {
                        filename = Session[SessionKeys.UserImage].ToString();
                    }
                    btnDeleteProfilePic.Visible = false;
                }
                else
                {
                    btnDeleteProfilePic.Visible = true;
                }
                objinfo.Image = filename;
                objinfo.UserName = hdnEditUsername.Value;
                objinfo.FirstName = txtFName.Text;
                objinfo.LastName = txtLName.Text;
                objinfo.FullName = txtFullName.Text;
                objinfo.Location = txtLocation.Text;
                objinfo.AboutYou = txtAboutYou.Text;
                objinfo.Email = txtEmail1.Text + (txtEmail2.Text != "" ? "," + txtEmail2.Text : "") + (txtEmail3.Text != "" ? ',' + txtEmail3.Text : "");
                objinfo.ResPhone = txtResPhone.Text;
                objinfo.MobilePhone = txtMobile.Text;
                objinfo.Others = txtOthers.Text;
                objinfo.AddedOn = DateTime.Now;
                objinfo.AddedBy = GetUsername;
                objinfo.UpdatedOn = DateTime.Now;
                objinfo.PortalID = GetPortalID;
                objinfo.UpdatedBy = GetUsername;
                objinfo.BirthDate = txtBirthDate.Text == string.Empty ? DateTime.Parse(falseDate) : DateTime.Parse(txtBirthDate.Text);
                objinfo.Gender = rdbGender.SelectedIndex;
                UserProfileController.AddUpdateProfile(objinfo);
                LoadUserDetails();
                GetSageUserInfo(hdnEditUsername.Value);
                tblEditProfile.Visible = false;
                //LoadUserDetails();
                tblViewProfile.Visible = true;
                imgProfileEdit.Visible = false;
                imgProfileView.Visible = true;
                btnDeleteProfilePic.Visible = false;
                //ShowHideProfile();
                //btnDeleteProfilePic.Visible = true;
                Session[SessionKeys.UserImage] = null;
                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("UserManagement", "UserProfileSavedSuccessfully"), "", SageMessageType.Success);
            }
            catch (Exception)
            {

                throw;
            }


        }

        public bool ThumbnailCallback()
        {
            return false;
        }


        public void GetUserDetails()
        {
            try
            {
                UserProfileInfo objinfo = new UserProfileInfo();
                objinfo = UserProfileController.GetProfile(hdnEditUsername.Value, GetPortalID);
                if (objinfo != null)
                {
                    string[] Emails = objinfo.Email.Split(',');
                    if (objinfo.Image != "")
                    {
                        imgUser.ImageUrl = "~/Modules/Admin/UserManagement/UserPic/" + objinfo.Image;
                        imgUser.Visible = true;
                        btnDeleteProfilePic.Visible = true;
                        Session[SessionKeys.UserImage] = objinfo.Image;
                        imgProfileEdit.Visible = true;
                    }
                    else
                    {
                        imgUser.Visible = false;
                        btnDeleteProfilePic.Visible = false;
                        imgProfileEdit.Visible = false;
                    }
                    lblDisplayUserName.Text = objinfo.UserName;
                    txtFName.Text = objinfo.FirstName;
                    txtLName.Text = objinfo.LastName;
                    txtFullName.Text = objinfo.FullName;
                    txtLocation.Text = objinfo.Location;
                    txtAboutYou.Text = objinfo.AboutYou;
                    txtEmail1.Text = Emails[0];
                    txtBirthDate.Text = (objinfo.BirthDate.ToShortDateString() == falseDate || objinfo.BirthDate.ToShortDateString() == defaultDate) ? "" : objinfo.BirthDate.ToShortDateString();
                    rdbGender.SelectedIndex = objinfo.Gender;
                    if (Emails.Length == 2)
                    {
                        txtEmail2.Text = Emails[1];
                    }
                    if (Emails.Length == 3)
                    {
                        txtEmail2.Text = Emails[1];
                        txtEmail3.Text = Emails[2];
                    }
                    txtResPhone.Text = objinfo.ResPhone;
                    txtMobile.Text = objinfo.Mobile;
                    txtOthers.Text = objinfo.Others;
                }
            }

            catch (Exception)
            {

                throw;
            }
        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideProfile();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ShowHideProfile()
        {
            try
            {
                tblEditProfile.Visible = true;
                //imgProfileEdit.Visible = true;
                tblViewProfile.Visible = false;
                imgProfileView.Visible = false;
                GetUserDetails();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void LoadUserDetails()
        {
            try
            {
                UserProfileInfo objinfo = new UserProfileInfo();
                objinfo = UserProfileController.GetProfile(hdnEditUsername.Value, GetPortalID);
                if (objinfo != null)
                {
                    string[] Emails = objinfo.Email.Split(',');
                    if (objinfo.Image != "")
                    {
                        imgViewImage.ImageUrl = "~/Modules/Admin/UserManagement/UserPic/" + objinfo.Image;
                        imgViewImage.Visible = true;
                    }
                    else
                    {
                        imgViewImage.Visible = false;
                    }
                    lblViewUserName.Text = objinfo.UserName;
                    lblViewFirstName.Text = objinfo.FirstName;
                    lblViewLastName.Text = objinfo.LastName;
                    if (objinfo.FullName != "")
                    {
                        lblviewFullName.Text = objinfo.FullName;
                        trviewFullName.Visible = true;
                    }
                    else { trviewFullName.Visible = false; }
                    if (objinfo.Location != "")
                    {
                        lblViewLocation.Text = objinfo.Location;
                        trViewLocation.Visible = true;
                    }
                    else { trViewLocation.Visible = false; }
                    string AboutYou = objinfo.AboutYou.Replace("\r\n", "<br>");
                    if (AboutYou != "")
                    {
                        lblViewAboutYou.Text = AboutYou;
                        trViewAboutYou.Visible = true;
                    }
                    else { trViewAboutYou.Visible = false; }
                    if (Emails.Length != 0)
                    {
                        lblViewEmail1.Text = Emails[0];
                        lblViewEmail2.Text = Emails.Length == 3 ? Emails[1] : "";
                        lblViewEmail3.Text = Emails.Length == 3 ? Emails[2] : "";
                        trViewEmail.Visible = true;
                    }
                    else { trViewEmail.Visible = false; }
                    if (objinfo.ResPhone != "")
                    {
                        lblViewResPhone.Text = objinfo.ResPhone;
                        trViewResPhone.Visible = true;
                    }
                    else { trViewResPhone.Visible = false; }
                    if (objinfo.Mobile != "")
                    {
                        lblViewMobile.Text = objinfo.Mobile;
                        trViewMobile.Visible = true;
                    }
                    else { trViewMobile.Visible = false; }
                    if (objinfo.Others != "")
                    {
                        lblViewOthers.Text = objinfo.Others;
                        trViewOthers.Visible = true;
                    }
                    else { trViewOthers.Visible = false; }
                    if (objinfo.Gender != -1)
                    {
                        int gender = objinfo.Gender;
                        trviewGender.Visible = false;
                        if (gender == 0)
                        {
                            trviewGender.Visible = true;
                            lblviewGender.Text = "Male";
                        }
                        else if (gender == 1)
                        {
                            trviewGender.Visible = true;
                            lblviewGender.Text = "Female";
                        }
                    }
                    else
                    {
                        trviewGender.Visible = false;
                    }
                    if (objinfo.BirthDate.ToShortDateString() != falseDate && objinfo.BirthDate.ToShortDateString() != defaultDate)
                    {
                        trviewBirthDate.Visible = true;
                        lblviewBirthDate.Text = objinfo.BirthDate.ToShortDateString();
                    }
                    else
                    {
                        trviewBirthDate.Visible = false;
                    }
                }
            }

            catch (Exception)
            {

                throw;
            }
        }
        //protected void btnCancelProfile_Click(object sender, EventArgs e)
        //{
        //    tblEditProfile.Visible = false;
        //    LoadUserDetails();
        //    tblViewProfile.Visible = true;
        //    imgProfileEdit.Visible = false;
        //    imgProfileView.Visible = true;
        //    btnDeleteProfilePic.Visible = false;
        //}
        protected void btnDeleteProfilePic_Click(object sender, EventArgs e)
        {
            try
            {
                UserProfileInfo objinfo = new UserProfileInfo();
                objinfo = UserProfileController.GetProfile(hdnEditUsername.Value, GetPortalID);
                if (objinfo.Image != "")
                {
                    string imagePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory) + "UserPic/" + objinfo.Image;
                    string path = Server.MapPath(imagePath);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
                UserProfileController.DeleteProfilePic(hdnEditUsername.Value, GetPortalID);
                GetUserDetails();
                LoadUserDetails();
                Session[SessionKeys.UserImage] = null;

            }
            catch (Exception)
            {

                throw;
            }
        }

        #region "UserExportImport"
        protected void imgBtnExportUser_Click(object sender, EventArgs e)
        {
            RoleController _role = new RoleController();
            string[] roles = _role.GetRoleNames(GetUsername, GetPortalID).ToLower().Split(',');
            if (roles.Contains(SystemSetting.SUPER_ROLE[0].ToLower()))
            {
                UserExportToExcel();
                ShowMessage(SageMessageTitle.Exception.ToString(), "No any data to export", "", SageMessageType.Alert);
            }

        }

        private void UserExportToExcel()
        {
            string csv = string.Empty;
            try
            {
                List<ExportUserInfo> lstInfo = new List<ExportUserInfo>();
                UserProfileController objCon = new UserProfileController();
                lstInfo = objCon.GetUserExportList();
                if (lstInfo.Count > 0)
                {
                    csv += "UserName ,";
                    csv += "FirstName ,";
                    csv += "LastName,";
                    csv += "Email,";
                    csv += "Password,";
                    csv += "PasswordSalt,";
                    csv += "PasswordFormat,";
                    csv += "RoleName,";
                    csv += "PortalID,";
                    csv += "IsActive,";
                    csv += "\r\n";

                    foreach (ExportUserInfo objInfo in lstInfo)
                    {
                        csv += objInfo.UserName + ",";
                        csv += objInfo.FirstName + ",";
                        csv += objInfo.LastName + ",";
                        csv += "\"" + objInfo.Email + "\"" + ",";
                        csv += objInfo.Password + ",";
                        csv += objInfo.PasswordSalt + ",";
                        csv += objInfo.PasswordFormat + ",";
                        csv += "\"" + objInfo.RoleName + "\"" + ",";
                        csv += objInfo.PortalID + ",";
                        csv += objInfo.IsApproved + ",";
                        csv += "\r\n";
                    }

                    ExportToExcel(ref csv, "User-Report");
                }
                else
                {
                    ShowMessage(SageMessageTitle.Exception.ToString(), "No any data to export", "", SageMessageType.Alert);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ExportToExcel(ref string table, string fileName)
        {
            try
            {
                table = table.Replace("&gt;", ">");
                table = table.Replace("&lt;", "<");
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".csv");
                HttpContext.Current.Response.ContentType = "application/text";
                HttpContext.Current.Response.Write(table);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        protected void imgBtnImportUser_Click(object sender, EventArgs e)
        {
            RoleController _role = new RoleController();
            string[] roles = _role.GetRoleNames(GetUsername, GetPortalID).ToLower().Split(',');
            if (roles.Contains(SystemSetting.SUPER_ROLE[0].ToLower()))
            {
                PanelVisibility(false, false, false, true, false);
            }
        }

        public DataSet ImportUserFile()
        {
            DataSet userImportDataSet = new DataSet();
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;
            try
            {
                string connString = string.Empty;
                string UserImportFile = string.Empty;
                ImportFilePath = Server.MapPath("~/Modules/Admin/UserManagement/temp/");
                if (!Directory.Exists(ImportFilePath))
                {
                    Directory.CreateDirectory(ImportFilePath);
                }
                if (fuUserImport.HasFile)
                {

                    string ext = Path.GetExtension(fuUserImport.FileName);
                    ext = ext.ToLower();
                    if (ext == ".csv")
                    {
                        string currentDateTime = DateTime.Now.ToString().Replace('/', '_').Replace(':', '_').Replace(' ', '_');
                        UserImportFile = Path.GetFileNameWithoutExtension(fuUserImport.FileName) + "_" + currentDateTime + ext;
                        UserImportFilePath = ImportFilePath + UserImportFile;
                        fuUserImport.SaveAs(UserImportFilePath);
                        DataTable csvData = new DataTable();
                        using (TextFieldParser csvReader = new TextFieldParser(UserImportFilePath))
                        {
                            csvReader.SetDelimiters(new string[] { "," });
                            csvReader.HasFieldsEnclosedInQuotes = true;

                            //Read columns from CSV file, remove this line if columns not exits  
                            string[] colFields = csvReader.ReadFields();

                            foreach (string column in colFields)
                            {
                                DataColumn datecolumn = new DataColumn(column);
                                datecolumn.AllowDBNull = true;
                                csvData.Columns.Add(datecolumn);
                            }

                            while (!csvReader.EndOfData)
                            {
                                string[] fieldData = csvReader.ReadFields();
                                csvData.Rows.Add(fieldData);
                            }
                        }
                        userImportDataSet.Tables.Add(csvData);
                    }
                    else
                    {
                        ShowMessage("", GetSageMessage("UserManagement", "InvalidUserFileExtension"), "", SageMessageType.Alert);
                    }
                }
                else
                {
                    ShowMessage("", GetSageMessage("UserManagement", "PleaseSelectUserFile"), "", SageMessageType.Alert);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
            return userImportDataSet;
        }

        public List<string> MappingList()
        {

            List<string> lstMappingList = new List<string>();
            lstMappingList.Add(txtImportUserName.Text);
            lstMappingList.Add(txtImportFirstName.Text);
            lstMappingList.Add(txtImportLastName.Text);
            lstMappingList.Add(txtImportEmail.Text);
            lstMappingList.Add(txtImportPassword.Text);
            lstMappingList.Add(txtImportPasswordSalt.Text);
            lstMappingList.Add(txtImportPasswordFormat.Text);
            lstMappingList.Add(txtImportRoleName.Text);
            lstMappingList.Add(txtImportPortalID.Text);
            lstMappingList.Add(txtImportIsApproved.Text);
            return lstMappingList;
        }

        protected void btnUserImport_Click(object sender, EventArgs e)
        {
            try
            {
                bool Flag = false;
                DataSet userImportDataSet = ImportUserFile();
                List<string> lstColumnHeader = new List<string>();
                List<string> ObjMappingList = MappingList();

                if (userImportDataSet.Tables.Count > 0)
                {
                    int columnLength = userImportDataSet.Tables[0].Columns.Count;
                    for (int i = 0; i < columnLength; i++)
                    {
                        lstColumnHeader.Add(userImportDataSet.Tables[0].Columns[i].ColumnName);
                    }

                    Flag = !ObjMappingList.Except(lstColumnHeader).Any();
                    if (Flag)
                    {
                        for (int i = 0; i < columnLength; i++)
                        {
                            if (txtImportUserName.Text == userImportDataSet.Tables[0].Columns[i].ColumnName)
                            {
                                userImportDataSet.Tables[0].Columns[i].ColumnName = "UserName";
                            }
                            if (txtImportFirstName.Text == userImportDataSet.Tables[0].Columns[i].ColumnName)
                            {
                                userImportDataSet.Tables[0].Columns[i].ColumnName = "FirstName";
                            }
                            if (txtImportLastName.Text == userImportDataSet.Tables[0].Columns[i].ColumnName)
                            {
                                userImportDataSet.Tables[0].Columns[i].ColumnName = "LastName";
                            }
                            if (txtImportEmail.Text == userImportDataSet.Tables[0].Columns[i].ColumnName)
                            {
                                userImportDataSet.Tables[0].Columns[i].ColumnName = "Email";
                            }
                            if (txtImportPassword.Text == userImportDataSet.Tables[0].Columns[i].ColumnName)
                            {
                                userImportDataSet.Tables[0].Columns[i].ColumnName = "Password";
                            }
                            if (txtImportPasswordSalt.Text == userImportDataSet.Tables[0].Columns[i].ColumnName)
                            {
                                userImportDataSet.Tables[0].Columns[i].ColumnName = "PasswordSalt";
                            }
                            if (txtImportPasswordFormat.Text == userImportDataSet.Tables[0].Columns[i].ColumnName)
                            {
                                userImportDataSet.Tables[0].Columns[i].ColumnName = "PasswordFormat";
                            }
                            if (txtImportRoleName.Text == userImportDataSet.Tables[0].Columns[i].ColumnName)
                            {
                                userImportDataSet.Tables[0].Columns[i].ColumnName = "RoleName";
                            }
                            if (txtImportPortalID.Text == userImportDataSet.Tables[0].Columns[i].ColumnName)
                            {
                                userImportDataSet.Tables[0].Columns[i].ColumnName = "PortalID";
                            }
                            if (txtImportIsApproved.Text == userImportDataSet.Tables[0].Columns[i].ColumnName)
                            {
                                userImportDataSet.Tables[0].Columns[i].ColumnName = "IsActive";
                            }
                        }
                    }
                    else
                    {
                        ShowMessage("", GetSageMessage("UserManagement", "ColumnMappingError"), "", SageMessageType.Alert);
                        clearField();
                        return;
                    }

                    //Listing Excel Users
                    foreach (DataRow dr in userImportDataSet.Tables[0].Rows)
                    {
                        ExportUserInfo userImportInfo = new ExportUserInfo();
                        userImportInfo.UserName = dr["UserName"].ToString();
                        userImportInfo.FirstName = dr["FirstName"].ToString();
                        userImportInfo.LastName = dr["LastName"].ToString();
                        userImportInfo.Email = dr["Email"].ToString();
                        userImportInfo.Password = dr["Password"].ToString();
                        userImportInfo.PasswordSalt = dr["PasswordSalt"].ToString();
                        userImportInfo.PasswordFormat = dr["PasswordFormat"].ToString();
                        userImportInfo.RoleName = dr["RoleName"].ToString();
                        userImportInfo.PortalID = Convert.ToInt32(dr["PortalID"]);
                        userImportInfo.IsApproved = Convert.ToBoolean(dr["IsActive"]);
                        lstUserImportUsers.Add(userImportInfo);
                    }

                    //Extracting Excel Roles

                    List<RoleInfo> lstExcelRolesSplit = new List<RoleInfo>();
                    foreach (ExportUserInfo objExport in lstUserImportUsers)
                    {
                        string[] excelRolesArr = objExport.RoleName.Split(',');
                        foreach (string role in excelRolesArr)
                        {
                            RoleInfo objRoles = new RoleInfo();
                            objRoles.PortalID = objExport.PortalID;
                            objRoles.RoleName = role.Trim();
                            lstExcelRolesSplit.Add(objRoles);
                        }
                    }

                    List<RoleInfo> lstExcelRolesIdentical = new List<RoleInfo>();
                    var ExcelRolesIdentical = lstExcelRolesSplit.Select(i => new { i.RoleName, i.PortalID }).Distinct();
                    foreach (var objRole in ExcelRolesIdentical)
                    {
                        RoleInfo objRoleInfo = new RoleInfo();
                        objRoleInfo.RoleName = objRole.RoleName;
                        objRoleInfo.PortalID = objRole.PortalID;
                        lstExcelRolesIdentical.Add(objRoleInfo);
                    }

                    //Extracting Sage Roles
                    List<RolesManagementInfo> lstSageRoles = new List<RolesManagementInfo>();
                    RolesManagementController objController = new RolesManagementController();
                    lstSageRoles = objController.GetSageFramePortalList();
                    List<RoleInfo> lstSageRolesSplit = new List<RoleInfo>();
                    foreach (RolesManagementInfo objRoleMgntInfo in lstSageRoles)
                    {
                        RoleInfo objSageRoles = new RoleInfo();
                        objSageRoles.RoleName = objRoleMgntInfo.RoleName;
                        objSageRoles.PortalID = objRoleMgntInfo.PortalID;
                        lstSageRolesSplit.Add(objSageRoles);
                    }

                    //Retrieve Identical Roles in Sage Roles and Excel Roles
                    List<RoleInfo> lstIdenticalRoles = lstExcelRolesIdentical.Except(lstSageRolesSplit).ToList();

                    //Adding Identical Roles in SageRoles
                    for (int i = 0; i < lstIdenticalRoles.Count; i++)
                    {
                        RoleInfo objRole = new RoleInfo();
                        string rolePrefix = GetPortalSEOName + "_";
                        objRole.ApplicationName = Membership.ApplicationName;
                        objRole.RoleName = lstIdenticalRoles[i].RoleName;
                        objRole.PortalID = lstIdenticalRoles[i].PortalID;
                        objRole.IsActive = 1;
                        objRole.AddedOn = DateTime.Now;
                        objRole.AddedBy = GetUsername;
                        RoleController objRoleCon = new RoleController();
                        RoleCreationStatus status = new RoleCreationStatus();
                        objRoleCon.CreateRole(objRole, out status);
                    }

                    //Listing SageFrame Users
                    UserProfileController objUserProfile = new UserProfileController();
                    List<ExportUserInfo> lstSageUsers = objUserProfile.GetSageFrameUserList();

                    //Extracting Excel Username
                    List<string> lstExcelUserName = new List<string>();
                    lstExcelUserName = lstUserImportUsers.Select(x => x.UserName).ToList();

                    //Extracting Excel Email
                    List<string> lstExcelEmail = new List<string>();
                    lstExcelEmail = lstUserImportUsers.Select(x => x.Email).ToList();

                    //Extracting SageFrame Username
                    List<string> lstSageUserName = new List<string>();
                    lstSageUserName = lstSageUsers.Select(x => x.UserName).ToList();

                    //Extracting SageFrame Email
                    List<string> lstSageEmail = new List<string>();
                    lstSageEmail = lstSageUsers.Select(x => x.Email).ToList();

                    //Check duplicacy of Self Excel Users and Email
                    List<string> lstUserNameDuplicacyinExcel = new List<string>();
                    lstUserNameDuplicacyinExcel = lstExcelUserName.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

                    List<string> lstEmailDuplicacyinExcel = new List<string>();
                    lstEmailDuplicacyinExcel = lstExcelEmail.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

                    if (lstUserNameDuplicacyinExcel.Count > 0 || lstEmailDuplicacyinExcel.Count > 0)
                    {
                        ShowMessage("", GetSageMessage("UserManagement", "DuplicateUsers"), "", SageMessageType.Alert);
                        clearField();
                        DeleteTempFolder();
                        return;
                    }

                    //check UserName duplicacy SageUsers And Excel Users               
                    List<string> lstUserNameDuplicacy = new List<string>();
                    lstUserNameDuplicacy = lstExcelUserName.Intersect(lstSageUserName).ToList();

                    //Removing duplicate List by UserName
                    ExportUserInfo dupUserListByUName = null;
                    List<ExportUserInfo> lstdubUserListByName = new List<ExportUserInfo>();
                    foreach (string DupUserName in lstUserNameDuplicacy)
                    {
                        List<ExportUserInfo> obj = lstUserImportUsers;
                        dupUserListByUName = lstUserImportUsers.Single(x => x.UserName == DupUserName);
                        lstUserImportUsers.Remove(dupUserListByUName);
                        //list users in excel
                        lstdubUserListByName.Add(dupUserListByUName);
                    }

                    //Extracting Email duplicacy in SageEmail and listUserImportUsers
                    List<string> lstExcelEmailInImportUsers = new List<string>();
                    lstExcelEmailInImportUsers = lstUserImportUsers.Select(x => x.Email).ToList();

                    //check Email duplicacy SageEmail And Excel Email 
                    List<string> lstEmailDuplicacy = new List<string>();
                    if (!m.RequireUniqueEmail)
                    {
                        lstEmailDuplicacy = lstExcelEmailInImportUsers.Intersect(lstSageEmail).ToList();
                    }
                    //Removing duplicate List by Email
                    ExportUserInfo dupUserListByEmail = null;
                    List<ExportUserInfo> lstdubUserListByEmail = new List<ExportUserInfo>();
                    if (lstUserImportUsers.Count != 0)
                    {
                        foreach (string DupEmail in lstEmailDuplicacy)
                        {
                            List<ExportUserInfo> obj = lstUserImportUsers;
                            dupUserListByEmail = lstUserImportUsers.Single(x => x.Email == DupEmail);
                            lstUserImportUsers.Remove(dupUserListByEmail);
                            //list users in excel
                            lstdubUserListByEmail.Add(dupUserListByEmail);
                        }
                    }

                    //Retrieve Duplicate UserList in SageUsers and Excel Users
                    lstDuplicateUserList = lstdubUserListByName.Concat(lstdubUserListByEmail).ToList();

                    //Retrieve Identical UserList in SageUsers and Excel Users
                    List<ExportUserInfo> lstIdenticalUserList = lstUserImportUsers;

                    //Adding Identical User List in SageUserList
                    if (lstIdenticalUserList.Count > 0)
                    {
                        for (int i = 0; i < lstIdenticalUserList.Count; i++)
                        {
                            UserInfo objUser = new UserInfo();
                            objUser.ApplicationName = Membership.ApplicationName;
                            objUser.FirstName = lstIdenticalUserList[i].FirstName;
                            objUser.UserName = lstIdenticalUserList[i].UserName;
                            objUser.LastName = lstIdenticalUserList[i].LastName;
                            objUser.Password = lstIdenticalUserList[i].Password;
                            objUser.PasswordSalt = lstIdenticalUserList[i].PasswordSalt;
                            objUser.Email = lstIdenticalUserList[i].Email;
                            objUser.SecurityQuestion = "";
                            objUser.SecurityAnswer = "";
                            objUser.IsApproved = lstIdenticalUserList[i].IsApproved;
                            objUser.CurrentTimeUtc = DateTime.Now;
                            objUser.CreatedDate = DateTime.Now;
                            objUser.UniqueEmail = 0;
                            objUser.PasswordFormat = Int32.Parse(lstIdenticalUserList[i].PasswordFormat);
                            objUser.PortalID = lstIdenticalUserList[i].PortalID;
                            objUser.AddedOn = DateTime.Now;
                            objUser.AddedBy = GetUsername;
                            objUser.UserID = Guid.NewGuid();
                            objUser.RoleNames = lstIdenticalUserList[i].RoleName;
                            objUser.StoreID = GetStoreID;
                            objUser.CustomerID = GetCustomerID;
                            UserCreationStatus status = new UserCreationStatus();
                            MembershipDataProvider.CreatePortalUser(objUser, out status, UserCreationMode.CREATE);
                        }
                    }
                    else
                    {
                        lblDuplicateUser.Visible = true;
                        ShowMessage("", GetSageMessage("UserManagement", "UsersNotAdded"), "", SageMessageType.Error);
                        ExportDuplicateUserList();
                    }
                    if (lstDuplicateUserList.Count > 0 && lstIdenticalUserList.Count > 0)
                    {
                        lblDuplicateUser.Visible = true;
                        ShowMessage("", GetSageMessage("UserManagement", "UsersAddedSuccessfullyWithDuplicateUserReport"), "", SageMessageType.Success);
                        ExportDuplicateUserList();
                    }
                    if (lstDuplicateUserList.Count == 0 && lstIdenticalUserList.Count > 0)
                    {
                        ShowMessage("", GetSageMessage("UserManagement", "UsersAddedSuccessfully"), "", SageMessageType.Success);
                    }
                    clearField();
                    DeleteTempFolder();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void clearField()
        {
            txtImportUserName.Text = string.Empty;
            txtImportFirstName.Text = string.Empty;
            txtImportLastName.Text = string.Empty;
            txtImportEmail.Text = string.Empty;
            txtImportPassword.Text = string.Empty;
            txtImportPasswordSalt.Text = string.Empty;
            txtImportPasswordFormat.Text = string.Empty;
            txtImportRoleName.Text = string.Empty;
            txtImportPortalID.Text = string.Empty;
            txtImportIsApproved.Text = string.Empty;
        }
        private void DeleteTempFolder()
        {
            if (Directory.Exists(ImportFilePath))
            {
                if (File.Exists(UserImportFilePath))
                {
                    File.SetAttributes(UserImportFilePath, FileAttributes.Normal);
                    File.Delete(UserImportFilePath);
                }
                Directory.Delete(ImportFilePath, true);
            }
        }

        private void ExportDuplicateUserList()
        {
            try
            {
                string csv = string.Empty;
                List<ExportUserInfo> lstInfo = new List<ExportUserInfo>();
                csv += "UserName ,";
                csv += "FirstName ,";
                csv += "LastName,";
                csv += "Email,";
                csv += "Password ,";
                csv += "PasswordSalt,";
                csv += "PasswordFormat,";
                csv += "RoleName,";
                csv += "PortalID,";
                csv += "IsActive,";
                csv += "\r\n";
                foreach (ExportUserInfo objInfo in lstDuplicateUserList)
                {
                    csv += objInfo.UserName + ",";
                    csv += objInfo.FirstName + ",";
                    csv += objInfo.LastName + ",";
                    csv += "\"" + objInfo.Email + "\"" + ",";
                    csv += objInfo.Password + ",";
                    csv += objInfo.PasswordSalt + ",";
                    csv += objInfo.PasswordFormat + ",";
                    csv += "\"" + objInfo.RoleName + "\"" + ",";
                    csv += objInfo.PortalID + ",";
                    csv += objInfo.IsApproved + ",";
                    csv += "\r\n";
                }
                Session["csv"] = csv;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void btnImportCancel_Click(object sender, EventArgs e)
        {
            Session["csv"] = null;
            Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
        }
        protected void btnDuplicateUser_Click(object sender, EventArgs e)
        {
            if (Session["csv"] != null && Session["csv"].ToString() != string.Empty)
            {
                string csv = Session["csv"].ToString();
                Session["csv"] = null;
                ExportToExcel(ref csv, "Duplicate User-Report");
            }
            else
            {
                ShowMessage("", GetSageMessage("UserManagement", "NoDuplicateList"), "", SageMessageType.Alert);
            }
        }
        #endregion
        #region "SuspendedUser"
        protected void imgBtnSuspendedIP_Click(object sender, EventArgs e)
        {
            RoleController _role = new RoleController();
            string[] roles = _role.GetRoleNames(GetUsername, GetPortalID).ToLower().Split(',');
            if (roles.Contains(SystemSetting.SUPER_ROLE[0].ToLower()))
            {
                PanelVisibility(false, false, false, false, true);
            }
        }
        public void LoadSuspendedIp()
        {
            SuspendedIPController objSuspendedIP = new SuspendedIPController();
            gdvSuspendedIP.DataSource = objSuspendedIP.GetSuspendedIP();
            gdvSuspendedIP.DataBind();
        }
        protected void gdvSuspendedIP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvSuspendedIP.PageIndex = e.NewPageIndex;
            LoadSuspendedIp();
        }
        protected void gdvSuspendedIP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnIsSuspended = (HiddenField)e.Row.FindControl("hdnIsSuspended");
                HtmlInputCheckBox chkIsSuspendedItem = (HtmlInputCheckBox)e.Row.FindControl("chkBoxIsSuspendedItem");
                chkIsSuspendedItem.Attributes.Add("onclick", "javascript:Check(this,'cssCheckBoxIsSuspendedHeader','" + gdvSuspendedIP.ClientID + "','cssCheckBoxIsActiveItem');");
                chkIsSuspendedItem.Checked = bool.Parse(hdnIsSuspended.Value);
            }
        }
        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedSuspendedIPID = string.Empty;
                string IsSuspended = string.Empty;
                for (int i = 0; i < gdvSuspendedIP.Rows.Count; i++)
                {
                    HtmlInputCheckBox chkBoxItem = (HtmlInputCheckBox)gdvSuspendedIP.Rows[i].FindControl("chkBoxIsSuspendedItem");
                    HiddenField hdnSuspendedIPID = (HiddenField)gdvSuspendedIP.Rows[i].FindControl("hdnIPAddressID");
                    selectedSuspendedIPID = hdnSuspendedIPID.Value.Trim();
                    IsSuspended = chkBoxItem.Checked ? "1" : "0";
                    SuspendedIPController objSuspendedCon = new SuspendedIPController();
                    objSuspendedCon.UpdateSuspendedIP(selectedSuspendedIPID, IsSuspended);
                    hideSubmit();
                }
                LoadSuspendedIp();
                ShowMessage("", GetSageMessage("UserManagement", "SelectedChangesAreSavedSuccessfully"), "", SageMessageType.Success);
            }

            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        protected void btnCancelSuspendedUser_Click(object sender, EventArgs e)
        {
            pnlSuspendedIP.Visible = false;
            PanelVisibility(false, true, false, false, false);
        }
        private void hideSubmit()
        {
            List<SuspendedIPInfo> lstSuspendedIP = new List<SuspendedIPInfo>();
            SuspendedIPController objSuspendedIP = new SuspendedIPController();
            lstSuspendedIP = objSuspendedIP.GetSuspendedIP();
            if (lstSuspendedIP.Count == 0)
            {
                divSave.Visible = false;
            }
        }
        #endregion
    }
}