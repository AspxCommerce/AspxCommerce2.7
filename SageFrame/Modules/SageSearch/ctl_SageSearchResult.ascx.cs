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
using System.Web.UI.HtmlControls;
using SageFrame.Web;
using SageFrame.Search;
using SageFrame.SageFrameClass;
using SageFrame.Web.Common.SEO;
using SageFrame.Framework;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using SageFrame.Common.CommonFunction;
using System.IO;
#endregion

public partial class Modules_SageSearch_ctl_SageSearchResult : BaseAdministrationUserControl
{
    public int PortalID;
    public string CulturalName;
    public string baseURL;
    public string UserName;
    public int viewPerPage = 10;
    string IDOfTxtBox = string.Empty;
    public string SageSearchResultPage
    {
        get
        {
            string strResqltPage = string.Empty;
            if (ViewState["__mSageSearchRPage"] != null)
            {
                strResqltPage = ViewState["__mSageSearchRPage"].ToString();
            }
            return strResqltPage;
        }
        set
        {
            ViewState["__mSageSearchRPage"] = value;
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        IncludeJs("SageSearch", "/Modules/SageSearch/js/searchResult.js", "/Modules/SageSearch/js/Paging/jquery.pagination.js");
        IncludeCss("SageSearch", "/Modules/SageSearch/css/module.css");
        GenrateSageSerchForm();
        SetSearchText();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SearchVariable", " var SagePageExtension='" + SageFrameSettingKeys.PageExtension + "';", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CulturalName = GetCurrentCultureName;
            PortalID = GetPortalID;
            UserName = GetUsername;
            baseURL = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            if (!IsPostBack)
            {
                Session["SageDtv"] = null;
            }
        }
        catch
        {
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        SageFrameSearch con = new SageFrameSearch();
        SageFrameSearchSettingInfo objSearchSettingInfo = con.LoadSearchSettings(GetPortalID, GetCurrentCultureName);
        string ClientID = string.Empty;
        if (objSearchSettingInfo.SearchButtonType == 0)
        {
            ClientID = ((Button)this.FindControl("btnSageSearchWord")).ClientID;
        }
        else if (objSearchSettingInfo.SearchButtonType == 1)
        {
            ClientID = ((ImageButton)this.FindControl("btnSageSearchWord")).ClientID;
        }
        else if (objSearchSettingInfo.SearchButtonType == 2)
        {
            ClientID = ((LinkButton)this.FindControl("btnSageSearchWord")).ClientID;
        }
        ((TextBox)this.FindControl(IDOfTxtBox)).Attributes.Add("onkeypress", "return clickButton(event,'" + ClientID + "')");
    }

    private void SetSearchText()
    {
        if (Request.QueryString["searchword"] != null && Request.QueryString["searchword"].ToString() != string.Empty)
        {
            #region "Get Data From Form Page"
            foreach (Control ctl in pnlSearchWord.Controls)
            {
                if (ctl.HasControls())
                {
                    foreach (Control mctl in ctl.Controls)
                    {
                        if (mctl.HasControls())
                        {
                            foreach (Control nctl in mctl.Controls)
                            {
                                if (nctl.GetType() == typeof(TextBox))
                                {
                                    TextBox txtSearch = (TextBox)nctl;
                                    if (txtSearch != null)
                                    {
                                        txtSearch.Text = Request.QueryString["searchword"].ToString();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
        }
    }

    private void GenrateSageSerchForm()
    {
        try
        {
            if (pnlSearchWord.Controls.Count == 1)
            {
                SageFrameSearch con = new SageFrameSearch();
                SageFrameSearchSettingInfo objSearchSettingInfo = con.LoadSearchSettings(GetPortalID, GetCurrentCultureName);
                viewPerPage = objSearchSettingInfo.SearchResultPerPage;
                HtmlGenericControl sageUl = new HtmlGenericControl("ul");
                sageUl.Attributes.Add("class", "sfSearchheader");
                HtmlGenericControl sageLi = new HtmlGenericControl("li");
                sageUl.Attributes.Add("class", "sfSearchheader");
                TextBox txtSageSearch = new TextBox();
                txtSageSearch.CssClass = "sfInputbox";
                txtSageSearch.MaxLength = objSearchSettingInfo.MaxSearchChracterAllowedWithSpace;
                IDOfTxtBox = "txtSage_" + this.Page.Controls.Count.ToString();
                txtSageSearch.ID = IDOfTxtBox;
                RequiredFieldValidator ReqV = new RequiredFieldValidator();
                ReqV.ControlToValidate = IDOfTxtBox;
                ReqV.ErrorMessage = "*";
                ReqV.CssClass = "sfError";
                ReqV.ValidationGroup = "grp_SageSearch";
                sageLi.Controls.Add(ReqV);
                sageLi.Controls.Add(txtSageSearch);
                HtmlGenericControl sageLiButton = new HtmlGenericControl("li");
                string SearchReasultPageName = objSearchSettingInfo.SearchResultPageName;
                if (!SearchReasultPageName.Contains(SageFrameSettingKeys.PageExtension))
                {
                    SearchReasultPageName += SageFrameSettingKeys.PageExtension;
                }
                SageSearchResultPage = SearchReasultPageName;
                if (objSearchSettingInfo.SearchButtonType == 0)
                {
                    Button btnSageSearch = new Button();
                    btnSageSearch.ID = "btnSageSearchWord";
                    btnSageSearch.Text = "Search";
                    btnSageSearch.CssClass = "sfBtn";
                    btnSageSearch.Click += new EventHandler(btnSageSearch_Click);
                    btnSageSearch.ValidationGroup = "grp_SageSearch_" + SageUserModuleID.ToString();
                    sageLiButton.Controls.Add(btnSageSearch);
                }
                else if (objSearchSettingInfo.SearchButtonType == 1)
                {
                    ImageButton btnSageSearch = new ImageButton();
                    btnSageSearch.ID = "btnSageSearchWord";
                    btnSageSearch.AlternateText = objSearchSettingInfo.SearchButtonText;
                    string SearchButtonImageUrl = objSearchSettingInfo.SearchButtonImage;
                    btnSageSearch.ImageUrl = GetTemplateImageUrl(SearchButtonImageUrl, true);
                    btnSageSearch.CssClass = "sfBtn";
                    btnSageSearch.ValidationGroup = "grp_SageSearch_" + SageUserModuleID.ToString();
                    btnSageSearch.Click += new ImageClickEventHandler(btnSageSearch_Click);
                    sageLiButton.Controls.Add(btnSageSearch);
                }
                else if (objSearchSettingInfo.SearchButtonType == 2)
                {
                    LinkButton btnSageSearch = new LinkButton();
                    btnSageSearch.ID = "btnSageSearchWord";
                    btnSageSearch.Text = "Search";
                    //btnSageSearch.CssClass = "sfBtn";
                    btnSageSearch.Click += new EventHandler(btnSageSearch_Click);
                    btnSageSearch.ValidationGroup = "grp_SageSearch_" + SageUserModuleID.ToString();
                    sageLiButton.Controls.Add(btnSageSearch);
                }
                sageUl.Controls.Add(sageLi);
                sageUl.Controls.Add(sageLiButton);
                pnlSearchWord.Controls.Add(sageUl);
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    void btnSageSearch_Click(object sender, ImageClickEventArgs e)
    {
        GetSearchParameter();
    }

    void btnSageSearch_Click(object sender, EventArgs e)
    {
        GetSearchParameter();
    }

    private void GetSearchParameter()
    {
        try
        {
            string SearchKey = string.Empty;
            #region "Get Data From Page"
            foreach (Control ctl in pnlSearchWord.Controls)
            {
                if (ctl.HasControls())
                {
                    foreach (Control mctl in ctl.Controls)
                    {
                        if (mctl.HasControls())
                        {
                            foreach (Control nctl in mctl.Controls)
                            {
                                if (nctl.GetType() == typeof(TextBox))
                                {
                                    TextBox txtSearch = (TextBox)nctl;
                                    if (txtSearch != null)
                                    {
                                        SearchKey = txtSearch.Text.Trim();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            //Remove unwanted html text from the Search text
            SearchKey = RemoveUnwantedSearchText(SearchKey);
            SageFrameSearch SFS = new SageFrameSearch();
            if (SFS.CheckIgnorWords(SearchKey, GetCurrentCultureName))
            {
                //Call Search function to get result
                if (SearchKey != string.Empty)
                {
                    SearchData(SearchKey);
                }
                else
                {
                    ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("SageFrameSearch", "PleaseFillValidTextToSearch"), "", SageMessageType.Alert);
                }
            }
            else
            {
                ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("SageFrameSearch", "PleaseFillValidTextToSearch"), "", SageMessageType.Alert);
            }

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private string RemoveUnwantedSearchText(string SearchKey)
    {
        try
        {
            SEOHelper seoHelper = new SEOHelper();
            return seoHelper.RemoveUnwantedHTMLTAG(SearchKey);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void SearchData(string SearchKey)
    {
        try
        {
            //Now Send to The Result Page;
            string strURL = string.Empty;
            SearchKey = Server.HtmlEncode(SearchKey);
            SageFrameSearch objSearch = new SageFrameSearch();
            if (objSearch.SearchPageExists(GetPortalID, Path.GetFileNameWithoutExtension(SageSearchResultPage)))
            {
                if (!IsParent)
                {
                    strURL = GetParentURL + "/portal/" + GetPortalSEOName + "/" + SageSearchResultPage + "?searchword=" + SearchKey;
                }
                else
                {
                    strURL = GetParentURL + "/" + SageSearchResultPage + "?searchword=" + SearchKey;
                }
            }
            else
            {
                if (!IsParent)
                {
                    strURL = GetParentURL + "/sf/portal/" + GetPortalSEOName + "/Search-Result" + SageFrameSettingKeys.PageExtension + "?searchword=" + SearchKey;
                }
                else
                {
                    strURL = GetParentURL + "/sf/Search-Result" + SageFrameSettingKeys.PageExtension + "?searchword=" + SearchKey;
                }
            }
            Session["SageDtv"] = null;
            Response.Redirect(strURL, false);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}