using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using SageFrame.Web;
using SageFrame.Framework;
using AspxCommerce.Core;
using AspxCommerce.AdvanceSearch;
using System.Data;
using System.Reflection;
using System.Diagnostics;

public partial class Modules_AspxCommerce_AspxAdvanceSearch_AdvanceSearch : BaseAdministrationUserControl
{
    private int StoreID;
    private int CustomerID;
    private int PortalID;
    private string UserName;
    private string CultureName;
    private string UserIP, CountryName;
    private string SessionCode = string.Empty;
    public string NoImageAdSearchPath, AllowAddToCart, AllowOutStockPurchase, SortByOptions, ViewAsOptions;
    public int NoOfItemsInARow;
    private int SortByOptionsDefault, ViewAsOptionsDefault;
    public string ItemDisplayMode;
    public string AdvanceSearchModulePath;
    private AdvanceSearchSettingInfo adSettingInfo = new AdvanceSearchSettingInfo();
    private List<AspxTemplateKeyValue> AspxTemplateValue = AspxGetTemplates.GetAspxTemplateKeyValue();
    Hashtable hst = null;
    AdvanceSearchController asc = new AdvanceSearchController();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            

            GetPortalCommonInfo(out StoreID, out PortalID, out CustomerID, out UserName, out CultureName, out SessionCode);
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName);
            if (!IsPostBack)
            {
                string templateName = TemplateName;
                IncludeCss("AdvanceSearch", "/Templates/"+ templateName+ "/css/MessageBox/style.css",
                           "/Templates/"+ templateName+ "/css/PopUp/style.css",
                           "/Templates/"+ templateName+ "/css/ToolTip/tooltip.css",
                           "/Templates/"+ templateName+ "/css/FancyDropDown/fancy.css",
                           "/Modules/AspxCommerce/AspxAdvanceSearch/css/AdvanceSearch.css");

                IncludeLanguageJS();
                IncludeJs("AdvanceSearch", "/js/Templating/tmpl.js", "/js/encoder.js", "/js/Paging/jquery.pagination.js",
                    "/js/Templating/AspxTemplate.js", "/js/PopUp/custom.js",
                          "/js/jquery.tipsy.js", "/js/FancyDropDown/itemFancyDropdown.js",
                          "/js/SageFrameCorejs/itemTemplateView.js",
                          "/Modules/AspxCommerce/AspxAdvanceSearch/js/AdvanceSearch.js",
                          "/Modules/AspxCommerce/AspxAdvanceSearch/APIjs/AdvanceSearchAPI.js");

                AdvanceSearchModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
                UserIP = HttpContext.Current.Request.UserHostAddress;
                IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();
                ipToCountry.GetCountry(UserIP, out CountryName);
                string sortByOptionsDefault = string.Empty;
                string viewAsOptionsDefault = string.Empty;
                GetAdvanceSearchSetting(aspxCommonObj,0, false);
                StoreSettingConfig ssc = new StoreSettingConfig();
                ssc.GetStoreSettingParamEight(StoreSetting.DefaultProductImageURL,
                                            StoreSetting.ShowAddToCartButton,
                                            StoreSetting.AllowOutStockPurchase,
                                            StoreSetting.ItemDisplayMode,
                                            StoreSetting.SortByOptionsDefault,
                                            StoreSetting.ViewAsOptions,
                                            StoreSetting.ViewAsOptionsDefault,
                                            StoreSetting.SortByOptions,
                                            out NoImageAdSearchPath,
                                            out AllowAddToCart,
                                            out AllowOutStockPurchase,
                                            out ItemDisplayMode,
                                            out sortByOptionsDefault,
                                            out ViewAsOptions,
                                            out viewAsOptionsDefault,
                                            out SortByOptions,
                                            StoreID,
                                            PortalID,
                                            CultureName
                                            );

                SortByOptionsDefault = Int32.Parse(sortByOptionsDefault);
                ViewAsOptionsDefault = Int32.Parse(viewAsOptionsDefault);
                NoOfItemsInARow = adSettingInfo.NoOfItemsInARow;

            }

            string modulePath = this.AppRelativeTemplateSourceDirectory;
            hst = AppLocalized.getLocale(modulePath);
            
            trBrand.Visible = true;
            
            CreateSortViewOption();

            GetAspxTemplates();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void GetAdvanceSearchSetting(AspxCommonInfo aspxCommonObj, int categoryID, bool isGiftCard)
    {
        AspxCoreController aspxController = new AspxCoreController();
        DataSet adDataSet = asc.GetAdvanceSearchDataSet("---", true, aspxCommonObj, categoryID, isGiftCard);

        DataTable advSettings = adDataSet.Tables[0];
        adSettingInfo.AdvanceSearchPageName = advSettings.Rows[0]["AdvanceSearchPageName"].ToString();
        adSettingInfo.IsEnableAdvanceSearch = true;
        adSettingInfo.IsEnableBrandSearch = Convert.ToBoolean(advSettings.Rows[0]["IsEnableBrandSearch"]);
        adSettingInfo.NoOfItemsInARow = Convert.ToInt32(advSettings.Rows[0]["NoOfItemsInARow"]);

        DataTable dtCatList = adDataSet.Tables[1];
        LoadAllCategoryForSearch(aspxCommonObj, AspxCommerce.Core.CommonHelper.ConvertTo<CategoryInfo>(dtCatList));

        DataTable dtBrandItem = adDataSet.Tables[2];
        GetAllBrandForItem(0, false, aspxCommonObj, AspxCommerce.Core.CommonHelper.ConvertTo<BrandItemsInfo>(dtBrandItem));
    }

    public void LoadAllCategoryForSearch(AspxCommonInfo aspxCommonObj, List<CategoryInfo> catList)
    {
        int rowCount = 0;
        StringBuilder categoryContent = new StringBuilder();
        categoryContent.Append("<select id=\"ddlCategory\" class=\"\">");
        if (catList != null && catList.Count > 0)
        {
            categoryContent.Append("<option value='0'>"+ getLocale("--All Category--")+ "</option>");
            categoryContent.Append("<optgroup label=\"");
            categoryContent.Append(getLocale("General Categories"));
            categoryContent.Append("\">");

            foreach (CategoryInfo item in catList)
            {
                if (!(bool)item.IsChecked)
                {
                    categoryContent.Append("<option value="+ item.CategoryID+ " isGiftCard="+ item.IsChecked+ ">"+ item.LevelCategoryName+ "</option>");
                }
                else
                {

                    rowCount += 1;
                    if (rowCount == 1)
                    {
                        categoryContent.Append("</optgroup>");
                        categoryContent.Append("<optgroup label=\"");
                        categoryContent.Append(getLocale("Gift Card Categories"));
                        categoryContent.Append("\">");
                    }
                    categoryContent.Append("<option value="+ item.CategoryID+ " isGiftCard="+ item.IsChecked+ ">"+ item.LevelCategoryName+ "</option>");
                }

            }
            if (rowCount > 0)
            {
                categoryContent.Append("</optgroup>");
            }
        }
        else
        {
            categoryContent.Append("<option value=\"-1\">No Category Listed!</option>");
        }
        categoryContent.Append("</select>");
        ltrCategories.Text = categoryContent.ToString();
        CreateDropdownPageSize(catList.Count);
    }

    public void GetAllBrandForItem(int categoryID, bool isGiftCard, AspxCommonInfo aspxCommonObj, List<BrandItemsInfo> lstBrandItem)
    {
        StringBuilder brandContent = new StringBuilder();
        brandContent.Append("<select id=\"lstBrands\">");

        if (lstBrandItem != null && lstBrandItem.Count > 0)
        {
            brandContent.Append("<option value='0'>"+ getLocale("All Brands")+ "</option>");
            if (lstBrandItem != null && lstBrandItem.Count > 0)
            {
                foreach (BrandItemsInfo item in lstBrandItem)
                {
                    brandContent.Append("<option value="+ item.BrandID+ ">"+ item.BrandName+ "</option>");
                }
                brandContent.Append("</select>");
            }
        }
        else
        {
            brandContent.Append("<option value='0'>"+ getLocale("No Brands")+ "</option>");
        }
        ltrBrands.Text = brandContent.ToString();
    }

    private void GetAspxTemplates()
    {
        foreach (AspxTemplateKeyValue value in AspxTemplateValue)
        {
            string xx = value.TemplateKey+ "@"+ value.TemplateValue;
            Page.ClientScript.RegisterArrayDeclaration("jsTemplateArray", "\'" + xx + "\'");
        }
    }

    private string getLocale(string messageKey)
    {
        string retStr = messageKey;
        if (hst != null && hst[messageKey] != null)
        {
            retStr = hst[messageKey].ToString();
        }
        return retStr;
    }

    public void CreateDropdownPageSize(int RowTotal)
    {

        StringBuilder strPage = new StringBuilder();
        strPage.Append("<div class=\"cssClassPageNumber\" id=\"divSearchPageNumber\">");
        strPage.Append("<div id=\"Pagination\">");
        strPage.Append("<div class=\"pagination\">");
        decimal noOfPages = ((decimal)RowTotal / 9);
        int numberOfPages = Convert.ToInt32(Math.Ceiling(noOfPages));
        for (int i = 1; i <= numberOfPages; i++)
        {
            if (i == 1)
            {
                strPage.Append("<span  class=\"current\">");
                strPage.Append(i);
                strPage.Append("</span>");
            }
            else
            {
                strPage.Append("<a href=\"#\" onclick=\"categoryDetails.GetDetail(");
                strPage.Append((((i - 1) * 9) + 1));
                strPage.Append(",");
                strPage.Append(9);
                strPage.Append(",");
                strPage.Append(i);
                strPage.Append(",'");
                strPage.Append(SortByOptionsDefault);
                strPage.Append("')\">");
                strPage.Append(i);
                strPage.Append("</a>");
            }

        }
        if (numberOfPages > 1)
        {
            strPage.Append("<a class=\"next\" href=\"#\" onclick=\"categoryDetails.GetDetail(");
            strPage.Append((((2 - 1) * 9) + 1));
            strPage.Append(",");
            strPage.Append(9);
            strPage.Append(",");
            strPage.Append(2);
            strPage.Append(",'");
            strPage.Append(SortByOptionsDefault);
            strPage.Append("')\">");
            strPage.Append("Next");
            strPage.Append("</a>");
        }
        int recordCount = 9;
        if (RowTotal < 9)
        {
            recordCount = RowTotal;
        }
        strPage.Append("<span class='showingPags'>Showing&nbsp;1-");
        strPage.Append(recordCount);
        strPage.Append("&nbsp;Of&nbsp;");
        strPage.Append(RowTotal);
        strPage.Append("&nbsp;records</span>");
        strPage.Append("</div>");
        strPage.Append("</div>");
        strPage.Append("<div class=\"cssClassViewPerPage\">");
        strPage.Append("<span>");
        strPage.Append(getLocale("View Per Page:"));
        strPage.Append("</span>");
        strPage.Append("<select class=\"sfListmenu\" id=\"ddlPageSize2\">");
        strPage.Append("<option data-html-text='9' value='9'>");
        strPage.Append(9);
        strPage.Append("</option>");
        strPage.Append("<option data-html-text='18' value='18'>");
        strPage.Append(18);
        strPage.Append("</option>");
        strPage.Append("<option data-html-text='27' value='27'>");
        strPage.Append(27);
        strPage.Append("</option>");
        strPage.Append("<option data-html-text='36' value='36'>");
        strPage.Append(36);
        strPage.Append("</option>");
        strPage.Append("<option data-html-text='45' value='45'>");
        strPage.Append(45);
        strPage.Append("</option>");
        strPage.Append("<option data-html-text='90' value='90'>");
        strPage.Append(90);
        strPage.Append("</option>");
        strPage.Append("</select>");
        strPage.Append("</div>");
        strPage.Append("<div class=\"clear\">");
        strPage.Append("</div>");
        strPage.Append("</div>");
        ltrPagination.Text = strPage.ToString();
    }

    public void CreateSortViewOption()
    {
        StringBuilder strSortView = new StringBuilder();
        StringBuilder strToExecute = new StringBuilder();
        strSortView.Append("<div id=\"divItemViewOptions\" class=\"viewWrapper\">");
        strSortView.Append("<div id=\"divViewAs\" class=\"view\">");
        if (ItemDisplayMode.ToLower() == "dropdown")
        {
            strSortView.Append("<h4>");
            strSortView.Append(getLocale("View as:"));
            strSortView.Append("</h4>");
            strSortView.Append("<select id=\"ddlAdvanceSearchViewAs\" class=\"sfListmenu\" style=\"display: none\">");
            //Add view as option here
            if (ViewAsOptions != string.Empty)
            {
                string[] strViewAS = ViewAsOptions.Split(',');

                foreach (string strViewOpt in strViewAS)
                {
                    if (strViewOpt != string.Empty)
                    {
                        string[] viewAsOption1 = strViewOpt.Split('#');
                        if (viewAsOption1[1].Length > 0)
                        {
                            strSortView.Append("<option value=");
                            strSortView.Append(viewAsOption1[0]);
                            strSortView.Append(">");
                            strSortView.Append(viewAsOption1[1]);
                            strSortView.Append("</option>");
                        }
                    }
                }
                strToExecute.Append("$('#ddlViewAs').val(");
                strToExecute.Append(ViewAsOptionsDefault);
                strToExecute.Append(");");
                strToExecute.Append("$('#ddlViewAs').show();");
                strToExecute.Append("$('#divViewAs').show();");

            }

            strSortView.Append("</select>");
        }
        else
        {
            if (ViewAsOptions != string.Empty)
            {
                string[] strViewAs = ViewAsOptions.Split(',');
                foreach (string strViewOpt in strViewAs)
                {
                    if (strViewOpt != string.Empty)
                    {
                        string[] viewAsOption1 = strViewOpt.Split('#');

                        strSortView.Append("<a class='cssClass");
                        strSortView.Append(viewAsOption1[1]);
                        strSortView.Append(" i-");
                        strSortView.Append(viewAsOption1[1]);
                        strSortView.Append("\" id=\"view_");
                        strSortView.Append(viewAsOption1[0]);
                        strSortView.Append(" displayId=");
                        strSortView.Append(viewAsOption1[0]);
                        strSortView.Append("   title=");
                        strSortView.Append(viewAsOption1[1]);
                        strSortView.Append("></a>");
                    }
                }
                strToExecute.Append("$('#divViewAs').find('a').each(function (i){");
                strToExecute.Append("if ($(this).attr('displayId') ==");
                strToExecute.Append(ViewAsOptionsDefault);
                strToExecute.Append("{  $(this).addClass('sfactive'); } }); $('#divViewAs').show();");
            }
        }
        strSortView.Append("</div>");

        strSortView.Append("<div id=\"divSortBy\" class=\"sort\">");
        strSortView.Append("<h4>");
        strSortView.Append(getLocale("Sort by:"));
        strSortView.Append("</h4>");
        strSortView.Append("<select id=\"ddlAdvanceSearchSortBy\" class=\"sfListmenu\">");
        if (SortByOptions != string.Empty)
        {
            string[] strShortBy = SortByOptions.Split(',');

            foreach (string strSortOpt in strShortBy)
            {
                if (strSortOpt != string.Empty)
                {
                    string[] sortByOption1 = strSortOpt.Split('#');
                    strSortView.Append("<option data-html-text='");
                    strSortView.Append(sortByOption1[1]);
                    strSortView.Append("' value=");
                    strSortView.Append(sortByOption1[0]);
                    strSortView.Append(">");
                    strSortView.Append(sortByOption1[1]);
                    strSortView.Append("</option>");

                }

            }

        }
        strToExecute.Append("$('#ddlSortBy').val(");
        strToExecute.Append(SortByOptionsDefault);
        strToExecute.Append(");");
        strToExecute.Append("$('#divSortBy').show();");
        string script = GetStringScript(strToExecute.ToString());
        strSortView.Append("</select>");
        strSortView.Append("</div>");
        strSortView.Append("</div>");
        strSortView.Append(script);
        ltrSortView.Text = strSortView.ToString();
    }

    private string GetStringScript(string codeToRun)
    {
        StringBuilder script = new StringBuilder();
        script.Append("<script type=\"text/javascript\">$(document).ready(function(){");
        script.Append(codeToRun);
        script.Append("});</script>");
        return script.ToString();
    }
}