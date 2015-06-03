#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.HTMLText;
using System.Web.Security;
using System.Collections;
using SageFrame.Web.Utilities;
#endregion 

namespace SageFrame.Modules.AdvanceHTML
{
    public partial class HTMLEdit : BaseAdministrationUserControl
    {
        System.Nullable<Int32> _newHTMLContentID = 0;
        protected void Page_Init(object sender, EventArgs e)
        {
            hdnUserModuleID.Value = SageUserModuleID;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigEditor();
                int portalid = GetPortalID;
                if (!IsPostBack)
                {
                    AddImageUrls();
                    string strCommentErrorMSG = GetSageMessage("HTML", "PleaseWriteComments");
                    imbAdd.Attributes.Add("onclick", string.Format("return ValidateHTMLComments('{0}','{1}','{2}');", txtComment.ClientID, lblErrorMessage.ClientID, strCommentErrorMSG));

                }
                BindEditor();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ckEditorUserModuleID", " var ckEditorUserModuleID='" + SageUserModuleID + "';", true);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        public void ConfigEditor()
        {
            txtBody.config.toolbar = new object[]
			{
				new object[] { "Source", "-", "Save", "NewPage", "Preview", "-", "Templates" },
				new object[] { "Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-", "Print", "SpellChecker", "Scayt" },
				new object[] { "Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat" },
				new object[] { "Form", "Checkbox", "Radio", "TextField", "Textarea", "Select", "Button", "ImageButton", "HiddenField" },
				"/",
				new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
				new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv" },
				new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
				new object[] { "BidiLtr", "BidiRtl" },
				new object[] { "Link", "Unlink", "Anchor" },
				new object[] { "Image", "Flash", "Table", "HorizontalRule", "Smiley", "SpecialChar", "PageBreak", "Iframe" },
				"/",
				new object[] { "Styles", "Format", "Font", "FontSize" },
				new object[] { "TextColor", "BGColor" },
				    new object[] { "Maximize", "ShowBlocks", "-", "About" }
			};
        }
        private void AddImageUrls()
        {
            imbAddComment.Visible = false;
            lblAddComment.Visible = false;
            imbAdd.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
            imbBack.ImageUrl = GetTemplateImageUrl("imgcancel.png", true);
            imbSave.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
        }

        private void HideAll()
        {
            try
            {
                divViewWrapper.Visible = false;
                divEditWrapper.Visible = false;
                divEditComment.Visible = false;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void BindContent()
        {
            try
            {
                HTMLController _html = new HTMLController();
                HTMLContentInfo contentInfo = _html.GetHTMLContent(GetPortalID, Int32.Parse(hdnUserModuleID.Value), GetCurrentCultureName);
                if (contentInfo != null)
                {
                    hdfHTMLTextID.Value = contentInfo.HtmlTextID.ToString();
                    ltrContent.Text = contentInfo.Content.ToString();
                    if (contentInfo.IsActive == true)
                    {
                        divViewWrapper.Visible = true;

                        if (HTMLController.IsAuthenticatedToEdit(hdnUserModuleID.Value, GetUsername, GetPortalID) && GetUsername != SystemSetting.SYSTEM_USER_NOTALLOW_HTMLCOMMENT[0])
                        {
                            divEditContent.Visible = true;
                        }
                        else
                        {
                            divEditContent.Visible = false;
                        }

                        if (IsAuthenticatedForComment() && contentInfo.IsAllowedToComment == true && GetUsername != SystemSetting.SYSTEM_USER_NOTALLOW_HTMLCOMMENT[0])
                        {
                            divAddComment.Visible = true;
                            divViewComment.Visible = true;
                            if (!IsPostBack)
                            {
                                BindComment();
                            }
                        }
                        else
                        {
                            divAddComment.Visible = false;
                            divViewComment.Visible = true;
                            divEditComment.Visible = false;
                            if (!IsPostBack)
                            {
                                BindComment();
                            }
                        }
                    }
                    else
                    {
                        HideAll();
                        divAddComment.Visible = false;
                        divViewComment.Visible = false;
                        divEditComment.Visible = false;
                        if (HTMLController.IsAuthenticatedToEdit(hdnUserModuleID.Value, GetUsername, GetPortalID) && GetUsername != SystemSetting.SYSTEM_USER_NOTALLOW_HTMLCOMMENT[0])
                        {
                            divViewWrapper.Visible = true;
                            divEditContent.Visible = true;
                        }
                    }
                }
                else if (contentInfo == null && Request.QueryString["ManageReturnUrl"] != null && GetUsername != SystemSetting.SYSTEM_USER_NOTALLOW_HTMLCOMMENT[0])
                {
                    HideAll();
                    divEditWrapper.Visible = true;
                    divAddComment.Visible = false;
                    divViewComment.Visible = false;
                    BindEditor();
                }
                else
                {
                    if (HTMLController.IsAuthenticatedToEdit(hdnUserModuleID.Value, GetUsername, GetPortalID) && GetUsername != SystemSetting.SYSTEM_USER_NOTALLOW_HTMLCOMMENT[0])
                    {
                        HideAll();
                        divViewWrapper.Visible = true;
                        divEditContent.Visible = true;
                        divAddComment.Visible = false;
                        divViewComment.Visible = false;
                    }
                    else
                    {
                        HideAll();
                        divEditContent.Visible = false;
                        divAddComment.Visible = false;
                        divViewComment.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void BindComment()
        {
            HTMLController objHtml = new HTMLController();
            try
            {
                SQLHandler Sq = new SQLHandler();
                if (IsSuperUser() && GetUsername != SystemSetting.SYSTEM_USER_NOTALLOW_HTMLCOMMENT[0])
                {
                    List<HTMLContentInfo> ml = objHtml.BindCommentForSuperUser(GetPortalID, hdfHTMLTextID.Value);
                    if (ml != null)
                    {
                        gdvHTMLList.DataSource = ml;
                        gdvHTMLList.DataBind();
                        if (gdvHTMLList.Rows.Count > 0)
                        {
                            //Setting comment Count
                            Label lblCommentCount = gdvHTMLList.HeaderRow.FindControl("lblCommentCount") as Label;
                            if (lblCommentCount != null)
                            {
                                lblCommentCount.Text = gdvHTMLList.Rows.Count.ToString();
                            }
                            gdvHTMLList.Columns[gdvHTMLList.Columns.Count - 1].Visible = true;
                            gdvHTMLList.Columns[gdvHTMLList.Columns.Count - 2].Visible = true;
                            rowApprove.Visible = true;
                            rowIsActive.Visible = true;
                        }
                    }
                }
                else
                {
                    List<HTMLContentInfo> nl = objHtml.BindCommentForOthers(GetPortalID, hdfHTMLTextID.Value);
                    if (nl != null)
                    {
                        gdvHTMLList.DataSource = nl;
                        gdvHTMLList.DataBind();
                        if (gdvHTMLList.Rows.Count > 0)
                        {
                            //Setting comment Count
                            Label lblCommentCount = gdvHTMLList.HeaderRow.FindControl("lblCommentCount") as Label;
                            if (lblCommentCount != null)
                            {
                                lblCommentCount.Text = gdvHTMLList.Rows.Count.ToString();
                            }
                            divViewComment.Style.Add("display", "block");
                            gdvHTMLList.Columns[gdvHTMLList.Columns.Count - 1].Visible = false;
                            gdvHTMLList.Columns[gdvHTMLList.Columns.Count - 2].Visible = false;
                        }
                        rowApprove.Visible = false;
                        rowIsActive.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private bool IsSuperUser()
        {
            bool flag = false;
            string[] allowRoles = SystemSetting.SYSTEM_SUPER_ROLES;
            for (int i = 0; i < allowRoles.Length; i++)
            {
                if (Roles.IsUserInRole(GetUsername, allowRoles[i]))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        private bool IsAuthenticatedForComment()
        {
            bool isAllow = false;
            string[] allowRoles = SystemSetting.SYSTEM_ROLES_ALLOW_HTMLCOMMENT; //SYSTEM_SUPER_ROLES;
            for (int i = 0; i < allowRoles.Length; i++)
            {
                if (Roles.IsUserInRole(GetUsername, allowRoles[i]))
                {
                    isAllow = true;
                    break;
                }
            }
            return isAllow;
        }

        private void imbEdit_Click()
        {
            try
            {
                HideAll();
                divEditWrapper.Visible = true;
                BindEditor();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void BindEditor()
        {
            try
            {
                HTMLController _html = new HTMLController();
                HTMLContentInfo objHtmlInfo = _html.GetHTMLContent(GetPortalID, Int32.Parse(hdnUserModuleID.Value), GetCurrentCultureName);
                if (objHtmlInfo != null)
                {
                    txtBody.Text = objHtmlInfo.Content;
                    chkPublish.Checked = bool.Parse(objHtmlInfo.IsActive.ToString());
                    chkAllowComment.Checked = bool.Parse(objHtmlInfo.IsAllowedToComment.ToString());
                    ViewState["EditHtmlTextID"] = objHtmlInfo.HtmlTextID;
                    divEditWrapper.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imbSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                HTMLController objHtml = new HTMLController();
                ArrayList arrColl = null;
                arrColl = IsContentValid(txtBody.Text.ToString());
                SQLHandler sq = new SQLHandler();
                string HTMLBodyText = arrColl[1].ToString().Trim();
                if (ViewState["EditHtmlTextID"] != null)
                {
                    objHtml.HtmlTextUpdate(hdnUserModuleID.Value, HTMLBodyText, GetCurrentCultureName, chkAllowComment.Checked, chkPublish.Checked, true, DateTime.Now, GetPortalID, GetUsername);
                    ViewState["EditHtmlTextID"] = null;
                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("HTML", "HTMLContentIsUpdatedSuccessfully"), "", SageMessageType.Success);
                }
                else
                {
                    objHtml.HtmlTextAdd(hdnUserModuleID.Value, HTMLBodyText, GetCurrentCultureName, chkAllowComment.Checked, true, chkPublish.Checked, DateTime.Now, GetPortalID, GetUsername);
                    if (_newHTMLContentID != 0)
                    {
                        ShowMessage("", GetSageMessage("HTML", "HTMLContentIsAddedSuccessfully"), "", SageMessageType.Success);
                    }
                }
                BindComment();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private ArrayList IsContentValid(string str)
        {
            bool isValid = true;
            str = RemoveUnwantedHTMLTAG(str);
            if (str == string.Empty)
                isValid = false;
            ArrayList arrColl = new ArrayList();
            arrColl.Add(isValid);
            arrColl.Add(str);
            return arrColl;
        }

        public string RemoveUnwantedHTMLTAG(string str)
        {
            str = System.Text.RegularExpressions.Regex.Replace(str, "<br/>$", "");
            str = System.Text.RegularExpressions.Regex.Replace(str, "<br />$", "");
            str = System.Text.RegularExpressions.Regex.Replace(str, "^&nbsp;", "");
            str = System.Text.RegularExpressions.Regex.Replace(str, "<form[^>]*>", "");
            str = System.Text.RegularExpressions.Regex.Replace(str, "</form>", "");
            return str;
        }

        protected void AfterSaveContent()
        {
            try
            {
                HideAll();
                Response.Redirect(Request.Url.OriginalString);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imbAddComment_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                HideAll();

                divViewWrapper.Visible = true;
                divViewComment.Visible = false;
                divEditComment.Visible = true;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imbAdd_Click(object sender, ImageClickEventArgs e)
        {
            HTMLController objHtml = new HTMLController();
            try
            {
                SQLHandler sq = new SQLHandler();
                if (Session["EditCommentID"] != null)
                {
                    objHtml.HtmlCommentUpdate(Session["EditCommentID"], txtComment.Text, chkApprove.Checked, chkApprove.Checked, true, DateTime.Now, GetPortalID, GetUsername);
                    ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("HTML", "CommentIsUpdatedSuccessfully"), "", SageMessageType.Success);
                }
                else
                {
                    objHtml.HtmlCommentAdd(hdfHTMLTextID.Value, txtComment.Text, chkApprove.Checked, chkIsActive.Checked, DateTime.Now, GetPortalID, GetUsername);
                    if (chkApprove.Checked && chkIsActive.Checked)
                    {
                        ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("HTML", "CommentIsAddedSuccessfully"), "", SageMessageType.Success);
                    }
                    else
                    {
                        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("HTML", "CommentWillBeAddedAfterApproved"), "", SageMessageType.Alert);
                    }
                }

                HideAll();
                BindComment();
                divViewWrapper.Visible = true;
                divViewComment.Visible = true;
                ClearComment();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void ClearComment()
        {
            Session["EditCommentID"] = null;
            txtComment.Text = "";
            chkApprove.Checked = false;
            chkIsActive.Checked = false;
        }

        protected void gdvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gdvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToString())
                {
                    case "Edit":
                        int EditID = Int32.Parse(e.CommandArgument.ToString());
                        Edit(EditID);
                        break;
                    case "Delete":
                        int DeleteID = Int32.Parse(e.CommandArgument.ToString());
                        Delete(DeleteID);
                        break;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void Edit(int EditID)
        {
            HTMLController objHtml = new HTMLController();
            try
            {
                SQLHandler sq = new SQLHandler();
                HTMLContentInfo CommentInfo = objHtml.HtmlCommentGetByHTMLCommentID(GetPortalID, EditID);
                if (CommentInfo != null)
                {
                    txtComment.Text = CommentInfo.Comment;
                    chkApprove.Checked = (bool)CommentInfo.IsApproved;
                    chkIsActive.Checked = (bool)CommentInfo.IsActive;
                    Session["EditCommentID"] = EditID;
                    HideAll();
                    divViewWrapper.Visible = true;
                    divEditComment.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void Delete(int DeleteID)
        {
            try
            {
                HTMLController objHtml = new HTMLController();
                objHtml.HTMLCommentDeleteByCommentID(DeleteID, GetPortalID, GetUsername);
                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("HTML", "CommentIsDeletedSuccessfully"), "", SageMessageType.Success);
                BindComment();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void gdvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");
                btnDelete.Attributes.Add("onclick", "javascript:return confirm('" + GetSageMessage("HTML", "AreYouSureToDelete") + "')");
            }
        }

        protected void gdvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gdvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gdvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void imbBack_Click(object sender, ImageClickEventArgs e)
        {
            HideAll();
            divViewWrapper.Visible = true;
            divViewComment.Visible = true;
            ClearComment();
        }

        protected void gdvHTMLList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnCustomizeEditor_Click(object sender, EventArgs e)
        {
            btnCustomizeEditor.Visible = false;
            btnDefault.Visible = true;
            HideAll();
            divEditWrapper.Visible = true;
        }

        protected void btnDefault_Click(object sender, EventArgs e)
        {
            btnCustomizeEditor.Visible = true;
            btnDefault.Visible = false;
            HideAll();
            divEditWrapper.Visible = true;
        }
    }
}