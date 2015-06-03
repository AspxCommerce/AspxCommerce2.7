using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.FAQ.Controller;
using SageFrame.FAQ.Info;
using SageFrame.SageFrameClass.MessageManagement;

public partial class Modules_FAQ_FAQEdit : BaseAdministrationUserControl
{
    public int FAQId = 0;
    public int PortalID=0;
    public int UserModuleID=0;
    public string baseURL = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        dvForm.Visible = false;
        PortalID = GetPortalID;
        UserModuleID = int.Parse(SageUserModuleID);
        baseURL = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        LoadImage();
        if (!IsPostBack)
        {
            LoadFAQList();
            LoadCategory();
            LoadCategoryOnGrid();
        }
        
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        txtSearchFAQ.Attributes.Add("onkeypress", "return clickButton(event,'" + imbSearchFAQ.ClientID + "')");
    }

    private void LoadImage()
    {
        imbSaveCategory.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
        imbCancel.ImageUrl = GetTemplateImageUrl("imgdelete.png", true);
        imbAddFaq.ImageUrl = GetTemplateImageUrl("add.png", true);
        imgSave.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
        imbSearchFAQ.ImageUrl = GetTemplateImageUrl("search.png", true);
        IncludeJs("FAQ", "/Modules/FAQ/js/visualize.jQuery.js");
        IncludeCss("FAQ", "/Modules/FAQ/css/visualize.css",
            "/Modules/FAQ/css/visualize-dark.css", 
            "/Modules/FAQ/css/basic.css", 
            "/Modules/FAQ/css/popup.css", 
            "/Modules/FAQ/css/module.css");
    }

    public void LoadCategory()
    {
        try
        {
            FAQController ctl = new FAQController();
            ddlCategory.DataSource = ctl.LoadCategory(GetPortalID,int.Parse(SageUserModuleID),GetCurrentCulture());
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataBind();
            ddlCategoryList.DataSource = ctl.LoadCategory(GetPortalID, int.Parse(SageUserModuleID), GetCurrentCulture());
            ddlCategoryList.DataTextField = "CategoryName";
            ddlCategoryList.DataValueField = "CategoryID";
            ddlCategoryList.DataBind();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    protected void imbAddFaq_Click(object sender, EventArgs e)
    {
        ShowForm();
    }

    public void AddFAQ()
    {
        try
        {
            FAQController clt = new FAQController();
            FAQInfo obj = new FAQInfo();
            if (Session["FAQId"] != null && Session["FAQId"].ToString() != string.Empty)
            {
                FAQId = Int32.Parse(Session["FAQId"].ToString());
                obj.FAQId = FAQId;
            }
            else
            {
                obj.FAQId = 0;
            }
            obj.UserName = GetUsername;
            obj.Question = txtQuestion.Text;
            obj.Answer = CkEditorFAQAnswer.Text;
            obj.PortalID = GetPortalID;
            obj.AddedBy = GetUsername;
            obj.CultureName = GetCurrentCulture();
            obj.UserModuleID = int.Parse(SageUserModuleID);
            obj.EmailAddress = "";
            obj.CategoryID = Convert.ToInt32(ddlCategory.SelectedItem.Value);
            obj.IsActive = true;
            clt.SaveFAQ(obj);
            ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/FAQ/ModuleLocalText", "FAQSaveSuccessfully"), "", SageMessageType.Success);
            HideForm();
            LoadFAQList();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    private void HideForm()
    {
        divGrid.Visible = true;
        dvAddFAQ.Visible = true;
        dvForm.Visible = false;
        txtQuestion.Text = string.Empty;
        CkEditorFAQAnswer.Text = string.Empty;
        Session["FAQId"] = string.Empty;
    }

    private void ShowForm()
    {
        dvAddFAQ.Visible = false;
        dvForm.Visible = true;
        divGrid.Visible = false;
    }

    private void LoadFAQList()
    {
        try
        {
            FAQController clt = new FAQController();
            gdvFAQ.DataSource = clt.LoadFAQList(GetPortalID, int.Parse(SageUserModuleID),GetCurrentCulture());
            gdvFAQ.DataBind();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    private void DeleteFAQ(int FAQid)
    {
        FAQController clt = new FAQController();        
        clt.DeleteFAQ(FAQid, GetPortalID, int.Parse(SageUserModuleID));
        ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/FAQ/ModuleLocalText", "DeleteSuccessfully"), "", SageMessageType.Success);
        LoadFAQList();      
    }

    private void EditFAQ(int FAQid)
    {
        try
        {
            ShowForm();
            FAQController ctl = new FAQController();
            FAQInfo obj = ctl.GetFAQByID(FAQid,GetPortalID,int.Parse(SageUserModuleID));
            txtQuestion.Text = obj.Question;
            CkEditorFAQAnswer.Text = obj.Answer;
            Session["FAQId"] = obj.FAQId;
            ddlCategory.SelectedItem.Selected = false;
			ddlCategory.Items.FindByText(obj.CategoryName).Selected = true;  
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    protected void imgSave_Click(object sender, ImageClickEventArgs e)
    {
        AddFAQ();
    }

    protected void imbSearchFAQ_Click(object sender, ImageClickEventArgs e)
    {
        FAQController clt = new FAQController();
        gdvFAQ.DataSource = clt.LoadSearchFAQList(GetPortalID, int.Parse(SageUserModuleID), GetCurrentCulture(),txtSearchFAQ.Text);
        gdvFAQ.DataBind();
    }        

    protected void imbCancel_Click(object sender, ImageClickEventArgs e)
    {
        HideForm();
    }

    protected void gdvFAQ_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int OprationID = Int32.Parse(e.CommandArgument.ToString());
            switch (e.CommandName.ToString())
            {
                case "Delete":
                    DeleteFAQ(OprationID);
                    break;
                case "Edit":
                    EditFAQ(OprationID);
                    break;
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }
    protected void gdvFAQ_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ImageId = (ImageButton)e.Row.FindControl("imgViewGraph");
                ImageButton ImageReviewId = (ImageButton)e.Row.FindControl("imgUserReview");
                ImageId.Attributes.Add("onclick", "javascript:return " + "ShowPopup(this);");
                ImageReviewId.Attributes.Add("onclick", "javascript:return " + "ShowReviewList(this);");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gdvFAQ_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void gdvFAQ_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gdvFAQ_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gdvFAQ_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
      
    }

    protected void chkIsActive_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int FAQId = 0;
            bool isApproved = ((CheckBox)sender).Checked;
            GridViewRow row = (GridViewRow)((CheckBox)sender).Parent.Parent;
            HiddenField hdfFAQId = row.FindControl("hdfFAQId") as HiddenField;
            FAQId = Convert.ToInt32(hdfFAQId.Value);
            FAQController.ApproveFAQ(FAQId, isApproved);
            ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/FAQ/ModuleLocalText", "ChangeSaveSuccessfully"), "", SageMessageType.Success);
            LoadFAQList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void ddlRecordsPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        gdvFAQ.PageSize = int.Parse(ddlRecordsPerPage.SelectedValue.ToString());
        LoadFAQList();
    }

    protected void ddlCategoryList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int CategoryID = Convert.ToInt32(ddlCategoryList.SelectedValue);
            FAQController clt = new FAQController();
            gdvFAQ.DataSource = clt.LoadFAQListAsCategory(GetPortalID, int.Parse(SageUserModuleID), GetCurrentCulture(),CategoryID);
            gdvFAQ.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gdvFAQ_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvFAQ.PageIndex = e.NewPageIndex;
        LoadFAQList();
    }
   
    private void LoadCategoryOnGrid()
    {
        try
        {
            FAQController clt = new FAQController();
            gdvCategory.DataSource = clt.LoadCategoryOnGrid(GetPortalID, int.Parse(SageUserModuleID),GetCurrentCulture());
            gdvCategory.DataBind();
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    protected void gdvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvCategory.PageIndex = e.NewPageIndex;
        LoadCategoryOnGrid();
    }

    protected void gdvCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int OprationID = Int32.Parse(e.CommandArgument.ToString());
            switch (e.CommandName.ToString())
            {
                case "Delete":
                    DeleteCategory(OprationID);
                    break;
                case "Edit":
                    EditCategory(OprationID);
                    break;
            }
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    private void DeleteCategory(int catID)
    {
        FAQController clt = new FAQController();

        clt.DeleteCategory(catID, GetPortalID, int.Parse(SageUserModuleID));
        ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/FAQ/ModuleLocalText", "CategoryDeletedSuccessfully"), "", SageMessageType.Success);
        LoadCategoryOnGrid();
    }

    private void EditCategory(int catID)
    {
        try
        {
            dvUpdateCategory.Visible = true;
            FAQController ctl = new FAQController();
            FAQInfo obj = ctl.GetCategoryByID(catID, GetPortalID, int.Parse(SageUserModuleID));
            txtCategoryName.Text = obj.CategoryName;
            ddlCategory.SelectedValue = Convert.ToString(obj.CategoryID);
            Session["CategoryID"] = obj.CategoryID;

        }
        catch (Exception e)
        {
            throw e;
        }
    }

    protected void gdvCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gdvCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gdvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void gdvCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gdvCategory_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
   
    protected void imbSaveCategory_Click(object sender, EventArgs e)
    {
        int categoryID;
        if (Session["CategoryID"] != null)
        {
            categoryID = Convert.ToInt32(Session["CategoryID"]);
        }
        else
        {
            categoryID = 0;
        }
        FAQController clt = new FAQController();
        clt.UpdateCategory(categoryID, GetPortalID, int.Parse(SageUserModuleID), txtCategoryName.Text, GetUsername, GetCurrentCulture());
        ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/FAQ/ModuleLocalText", "UpdateSuccessfully"), "", SageMessageType.Success);
        LoadCategoryOnGrid();
        txtCategoryName.Text = string.Empty;
        Session["CategoryID"] = null;
    }
   
}
