/*
SageFrame® - http://www.sageframe.com
Copyright (c) 2009-2010 by SageFrame
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
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Framework;
using SageFrame.Web;
using SageFrame.Message;
using System.Text;
using SageFrame.Web.Utilities;


namespace SageFrame.Modules.Admin.MessageManagement
{
    public partial class ctl_MessageTemplateManagement : BaseAdministrationUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindMessageTemplateType();
                    ClearForm();
                    BindData();
                    pnlMessageTemplateList.Style.Add("display", "block");
                    BindMessageToken();
                    lnkAddSubjectMessageToken.Attributes.Add("onclick",
                                                             "showMessageToken('" + txtSubject.ClientID + "','" +
                                                             mpeAddMessageTokenModalPopup.ClientID + "',false);");
                    lnkAddBodyMessageToken.Attributes.Add("onclick",
                                                          "showMessageToken('" + txtBody.ClientID + "','" +
                                                          mpeAddMessageTokenModalPopup.ClientID + "',true);");
                    lblAddSubjectMessageToken.Attributes.Add("onclick",
                                                             "showMessageToken('" + txtSubject.ClientID + "','" +
                                                             mpeAddMessageTokenModalPopup.ClientID + "',false);");
                    lblAddBodyMessageToken.Attributes.Add("onclick",
                                                          "showMessageToken('" + txtBody.ClientID + "','" +
                                                          mpeAddMessageTokenModalPopup.ClientID + "',true);");
                    btnAddMessageTokenOk.Attributes.Add("onclick",
                                                        "AddMessageToken('" + lstMessageToken.ClientID + "','" +
                                                        mpeAddMessageTokenModalPopup.ClientID + "' )");
                    btnAddMessageTokenCancel.Attributes.Add("onclick",
                                                            "hideModalPopup('" + mpeAddMessageTokenModalPopup.ClientID +
                                                            "');");
                    btnCancelMessageTemplateType.Attributes.Add("onclick",
                                                                "ClearTemplateText('" + txtMessageTemplateType.ClientID +
                                                                "', '" + lblErrorMessageTemplateType.ClientID +
                                                                "'); hideModalPopup('" + mpeMessageTemplateType.ClientID + "');");
                    btnCancelMessageTemplateToken.Attributes.Add("onclick",
                                                                 "ClearTemplateText('" +
                                                                 txtMessageTemplateToken.ClientID + "', '" +
                                                                 lblErrorMessageTemplateToken.ClientID +
                                                                 "'); hideModalPopup('" + mpeMessageTemplateToken.ClientID + "');");
                    lstMessageToken.Attributes.Add("onchange", "setMessageToken(this.value);");
                    hypAddMessageTemplateType.Attributes.Add("onclick",
                                                             "ClearTemplateText('" + txtMessageTemplateType.ClientID +
                                                             "', '" + lblErrorMessageTemplateType.ClientID +
                                                             "'); showModalPopup('" + mpeMessageTemplateType.ClientID +
                                                             "');");
                    hypAddMessageTemplateToken.Attributes.Add("onclick",
                                                              "ClearTemplateText('" + txtMessageTemplateToken.ClientID +
                                                              "', '" + lblErrorMessageTemplateToken.ClientID +
                                                              "'); showModalPopup('" + mpeMessageTemplateToken.ClientID +
                                                              "');");
                    lblAddMessageTemplateType.Attributes.Add("onclick",
                                                             "ClearTemplateText('" + txtMessageTemplateType.ClientID +
                                                             "', '" + lblErrorMessageTemplateType.ClientID +
                                                             "'); showModalPopup('" + mpeMessageTemplateType.ClientID +
                                                             "');");
                    lblAddMessageTemplateToken.Attributes.Add("onclick",
                                                              "ClearTemplateText('" + txtMessageTemplateToken.ClientID +
                                                              "', '" + lblErrorMessageTemplateToken.ClientID +
                                                              "'); showModalPopup('" + mpeMessageTemplateToken.ClientID +
                                                              "');");
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ckEditorUserModuleID", " var ckEditorUserModuleID='" + SageUserModuleID + "';", true);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }


        private void BindMessageToken()
        {
            try
            {
                lstMessageToken.Items.Clear();
                if (ddlMessageTemplateType.SelectedIndex != -1)
                {
                    if (Int32.Parse(ddlMessageTemplateType.SelectedValue) > 0)
                    {
                        MessageManagementController objMsgController = new MessageManagementController();
                        List<MessageManagementInfo> messageTokens = objMsgController.GetMessageTemplateTypeTokenListByMessageTemplateType(Int32.Parse(ddlMessageTemplateType.SelectedValue), GetPortalID);
                        foreach (MessageManagementInfo messageToken in messageTokens)
                        {
                            ListItem li = new ListItem(messageToken.MessageTokenKey, messageToken.MessageTokenKey);
                            lstMessageToken.Items.Add(li);
                        }
                    }
                }
                if (lstMessageToken.Items.Count > 0)
                {
                    lstMessageToken.SelectedIndex = 0;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "InitializedMessageToken", "setMessageToken('" + lstMessageToken.SelectedValue + "');", true);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void BindMessageTemplateType()
        {
            try
            {
                CommonFunction comm = new CommonFunction();
                MessageManagementController objMsgController = new MessageManagementController();
                var LINQ = objMsgController.GetMessageTemplateTypeList(true, false, GetPortalID, GetUsername, GetCurrentCultureName);
                DataTable dtTemplateType = comm.LINQToDataTable(LINQ);
                ddlMessageTemplateType.DataSource = dtTemplateType;
                ddlMessageTemplateType.DataTextField = "CultureName";
                ddlMessageTemplateType.DataValueField = "MessageTemplateTypeID";
                ddlMessageTemplateType.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imbAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                ClearForm();
                hdnMessageTemplateID.Value = "0";
                pnlMessageTemplate.Style.Add("display", "block");
                pnlMessageTemplateList.Style.Add("display", "none");
                //hypAddMessageTemplateType.Style.Add("display", "inline");
                //hypAddMessageTemplateToken.Style.Add("display", "inline");
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }

        }

        protected void imbSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            try
            {
                StringBuilder strMessage = new StringBuilder();
                if (Validate(strMessage))
                {
                    MessageManagementController objMsgController = new MessageManagementController();
                    if (Int32.Parse(hdnMessageTemplateID.Value) > 0)
                    {
                        try
                        {
                            Int32 MessageTemplateID = Int32.Parse(Session["MessageTemplateID"].ToString());
                            objMsgController.UpdateMessageTemplate(MessageTemplateID, Int32.Parse(ddlMessageTemplateType.SelectedValue), txtSubject.Text,
                                txtBody.Value, txtMailFrom.Text, chkIsActive.Checked, DateTime.Now, GetPortalID, GetUsername, GetCurrentCultureName);
                            ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("MessageManagement", "MessageTemplateIsUpdatedSuccessfully"), "", SageMessageType.Success);
                            BindData();
                            ClearForm();
                        }
                        catch
                        {
                            ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("MessageManagement", "MessageTemplateCouldnotBeUpdated"), "", SageMessageType.Error);
                        }
                    }
                    else
                    {
                        int newMessageTemplateID = objMsgController.AddMessageTemplate(int.Parse(ddlMessageTemplateType.SelectedValue), txtSubject.Text, txtBody.Value, txtMailFrom.Text, chkIsActive.Checked, DateTime.Now, GetPortalID, GetUsername, GetCurrentCultureName);
                        if (newMessageTemplateID > 0)
                        {
                            BindData();
                            ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("MessageManagement", "MessageTemplateIsAddedSuccessfully"), "", SageMessageType.Success);
                            ClearForm();
                        }
                        else
                        {

                            ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("MessageManagement", "MessageTemplateCouldnotBeAdded"), "", SageMessageType.Error);
                        }
                    }
                }
                else
                {
                    ShowMessage(SageMessageTitle.Information.ToString(), strMessage.ToString(), "", SageMessageType.Success);

                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private bool Validate(StringBuilder strMessage)
        {
            bool IsValid = true;

            strMessage.AppendLine(GetSageMessage("MessageManagement", "PleaseFill") + Environment.NewLine);
            if (txtSubject.Text.Trim() == string.Empty)
            {
                IsValid = false;

                strMessage.AppendLine(GetSageMessage("MessageManagement", "MessageTemplateSubject") + Environment.NewLine);
            }
            if (txtBody.Value.Trim() == string.Empty)
            {
                IsValid = false;

                strMessage.AppendLine(GetSageMessage("MessageManagement", "MessageTemplateBody") + Environment.NewLine);
            }
            if (txtMailFrom.Text.Trim() == string.Empty)
            {
                IsValid = false;

                strMessage.AppendLine(GetSageMessage("MessageManagement", "FromEmailAddressIsRequired") + Environment.NewLine);
            }
            return IsValid;
        }

        protected void imbCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearForm();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void ClearForm()
        {
            try
            {
                chkIsActive.Checked = false;
                txtSubject.Text = "";
                txtBody.Value = "";
                txtMailFrom.Text = "";
                Session["MessageTemplateID"] = null;
                HideAll();
                pnlMessageTemplateList.Style.Add("display", "block");
                pnlMessageTemplate.Style.Add("display", "none");
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void HideAll()
        {
            try
            {
                pnlMessageTemplate.Style.Add("display", "none");
                pnlMessageTemplateList.Style.Add("display", "none");
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void BindData()
        {
            try
            {
                MessageManagementController objMsgController = new MessageManagementController();
                var LINQ = objMsgController.GetMessageTemplateList(true, false, GetPortalID, GetUsername, GetCurrentCultureName);
                grdList.DataSource = LINQ;
                grdList.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void grdList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdList.PageIndex = e.NewPageIndex;
                BindData();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void grdList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Int32 MessageTemplateID = Int32.Parse(e.CommandArgument.ToString());
                switch (e.CommandName.ToString())
                {
                    case "Edit":
                        EditMessageTemplate(MessageTemplateID);
                        BindMessageToken();
                        break;
                    case "Delete":
                        DeleteMessageTemplate(MessageTemplateID);

                        break;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void EditMessageTemplate(Int32 MessageTemplateID)
        {
            try
            {
                MessageManagementController objMsgController = new MessageManagementController();
                var LINQ = objMsgController.GetMessageTemplate(MessageTemplateID, GetPortalID);
                if (LINQ.MessageTemplateTypeID > 0)
                {
                    hdnMessageTemplateID.Value = MessageTemplateID.ToString();
                    txtSubject.Text = LINQ.Subject;
                    txtBody.Value = LINQ.Body;
                    txtMailFrom.Text = LINQ.MailFrom;
                    chkIsActive.Checked = (LINQ.IsActive == true ? true : false);
                    ddlMessageTemplateType.SelectedValue = LINQ.MessageTemplateTypeID == 0 ? MessageTemplateID.ToString() : LINQ.MessageTemplateTypeID.ToString();
                    Session["MessageTemplateID"] = MessageTemplateID;
                    HideAll();
                    pnlMessageTemplate.Style.Add("display", "block");
                    //hypAddMessageTemplateType.Style.Add("display", "none");
                    //hypAddMessageTemplateToken.Style.Add("display", "none");

                }
                else
                {
                    ClearNewForm();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void DeleteMessageTemplate(Int32 MessageTemplateID)
        {
            try
            {
                MessageManagementController objMsgController = new MessageManagementController();
                objMsgController.DeleteMessageTemplate(MessageTemplateID, GetPortalID, DateTime.Now, GetUsername);
                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("MessageManagement", "MessageTemplateIsDeletedSuccessfully"), "", SageMessageType.Success);
                BindData();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void grdList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void grdList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void btnOkMessageTemplateType_Click(object sender, EventArgs e)
        {
            try
            {
                MessageManagementController objMsgController = new MessageManagementController();
                if (objMsgController.CheckMessgeTemplateUnique(txtMessageTemplateType.Text.TrimStart(','), GetPortalID))
                {

                    if (txtMessageTemplateType.Text.Trim() != "")
                    {
                        string messageTemplateTypeName = txtMessageTemplateType.Text.TrimStart(',');
                        int MessageTemplateTypeID = 0;
                        MessageTemplateTypeID = objMsgController.AddMessageTemplateType(messageTemplateTypeName, true, DateTime.Now, GetPortalID, GetUsername);
                        if (MessageTemplateTypeID > 0)
                        {
                            ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("MessageManagement", "MessageTemplateIsAddedSuccessfully"), "", SageMessageType.Success);
                            BindMessageTemplateType();
                            txtMessageTemplateType.Text = "";
                            ClearNewForm();
                        }
                        else
                        {
                            ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("MessageManagement", "PleaseSaveMessageTemplateTypeAgain"), "", SageMessageType.Error);
                        }
                    }
                    else
                    {

                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("MessageManagement", "MessageTemplateTypeIsRequiredField"), "", SageMessageType.Alert);
                    }
                }
                else
                {
                    mpeMessageTemplateType.Show();
                    lblErrorMessageTemplateType.Text = GetSageMessage("MessageManagement",
                                                                       "UniqueMessageTemplateTypeIsRequired");
                    lblErrorMessageTemplateType.Visible = true;
                    // ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("MessageManagement", "UniqueMessageTemplateTypeIsRequired"), "", SageMessageType.Alert);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void ddlMessageTemplateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            EditMessageTemplate(Int32.Parse(ddlMessageTemplateType.SelectedValue));
            BindMessageToken();
        }

        protected void btnCustomizeEditor_Click(object sender, EventArgs e)
        {
            txtBody.ToolbarSet = "Default";
            btnCustomizeEditor.Visible = false;
            lblCustomizeEditor.Visible = false;
            btnDefault.Visible = true;
            lblDefault.Visible = true;
            txtBody.Visible = true;
        }

        protected void btnDefault_Click(object sender, EventArgs e)
        {
            txtBody.ToolbarSet = "SageFrameLimited";
            btnCustomizeEditor.Visible = true;
            lblCustomizeEditor.Visible = true;
            btnDefault.Visible = false;
            lblDefault.Visible = false;
            txtBody.Visible = true;
        }
        protected void btnOkMessageTemplateToken_Click(object sender, EventArgs e)
        {
            try
            {
                MessageManagementController objMsgController = new MessageManagementController();
                if (objMsgController.CheckMessgeTokenUnique(txtMessageTemplateToken.Text.TrimStart(','), Int32.Parse(ddlMessageTemplateType.SelectedValue), GetPortalID))
                {
                    if (ddlMessageTemplateType.SelectedIndex != -1)
                    {
                        if (Int32.Parse(ddlMessageTemplateType.SelectedValue) > 0)
                        {
                            if (txtMessageTemplateToken.Text.Trim() != "")
                            {
                                string messageTemplateTokenName = txtMessageTemplateToken.Text.TrimStart(',');
                                int MessageTokenID = 0;
                                MessageTokenID = objMsgController.MessageTemplateTokenAdd(MessageTokenID, Int32.Parse(ddlMessageTemplateType.SelectedValue), messageTemplateTokenName, true, DateTime.Now, GetPortalID, GetUsername);
                                if (MessageTokenID > 0)
                                {

                                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("MessageManagement", "MessageTemplateTokenIsAddedSuccessfully"), "", SageMessageType.Success);
                                    BindMessageToken();
                                    //BindMessageTemplateType();
                                    txtMessageTemplateToken.Text = "";
                                }
                                else
                                {

                                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("MessageManagement", "PleaseSaveMessageTemplateTokenAgain"), "", SageMessageType.Error);
                                }
                            }
                            else
                            {

                                ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("MessageManagement", "MessageTemplateTokenIsRequiredField"), "", SageMessageType.Alert);
                            }
                        }
                    }
                }
                else
                {
                    mpeMessageTemplateToken.Show();
                    lblErrorMessageTemplateToken.Text = GetSageMessage("MessageManagement",
                                                                       "UniqueMessageTemplateTokenIsRequired");
                    lblErrorMessageTemplateToken.Visible = true;
                    //ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("MessageManagement", "UniqueMessageTemplateTokenIsRequired"), "", SageMessageType.Alert);
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void ClearNewForm()
        {
            chkIsActive.Checked = false;
            txtSubject.Text = "";
            txtBody.Value = "";
            txtMailFrom.Text = "";
        }
    }
}