using System;
using System.Collections.Generic;
using System.Web;
using SageFrame.Web;
using SageFrame;
using SageFrame.Framework;
using SageFrame.Web.Common.SEO;
using SageFrame.Web.Utilities;
using AspxCommerce.Core;
using SageFrame.Core;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using AspxCommerce.ImageResizer;
using System.Data;

public partial class Modules_AspxDetails_AspxCategoryDetails_CategoryDetails : BaseAdministrationUserControl
{
    public int StoreID, PortalID, CustomerID;
    public string UserName, CultureName;
    public string SessionCode = string.Empty;
    public string Categorykey = string.Empty;
    public string NoImageCategoryDetailPath, AllowAddToCart, AllowOutStockPurchase;
    public int NoOfItemsInARow;
    public string ItemDisplayMode, SortByOptions, ViewAsOptions;
    public decimal minPrice = 0;
    public decimal maxPrice = 0;
    public int SortByOptionsDefault, ViewAsOptionsDefault, IsCategoryHasItems = 0;
    public List<Filter> arrPrice = new List<Filter>();
    public List<AspxTemplateKeyValue> AspxTemplateValue = new List<AspxTemplateKeyValue>();


    protected void Page_Load(object sender, EventArgs e)
    {
        //Check more to decide where to put in page_init or here
        SageFrameRoute parentPage = (SageFrameRoute)this.Page;
        Categorykey = parentPage.Key;
        Categorykey = AspxUtility.fixedDecodeURIComponent(Categorykey);
        GetPortalCommonInfo(out StoreID, out PortalID, out CustomerID, out UserName, out CultureName, out SessionCode);
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName);
        if (!IsPostBack)
        {
            OverRideSEOInfo(Categorykey, aspxCommonObj);
        }
        string sortByOptionsDefault = string.Empty;
        string viewAsOptionsDefault = string.Empty;
        StoreSettingConfig ssc = new StoreSettingConfig();
        ssc.GetStoreSettingParamEight(StoreSetting.DefaultProductImageURL, StoreSetting.ShowAddToCartButton,
            StoreSetting.AllowOutStockPurchase, StoreSetting.ItemDisplayMode, StoreSetting.SortByOptions, StoreSetting.SortByOptionsDefault,
            StoreSetting.ViewAsOptions, StoreSetting.ViewAsOptionsDefault, out NoImageCategoryDetailPath, out AllowAddToCart,
            out AllowOutStockPurchase, out ItemDisplayMode, out SortByOptions, out sortByOptionsDefault, out ViewAsOptions,
            out viewAsOptionsDefault, StoreID, PortalID, CultureName);
        NoOfItemsInARow = 3;
        SortByOptionsDefault = Int32.Parse(sortByOptionsDefault);
        ViewAsOptionsDefault = Int32.Parse(viewAsOptionsDefault);

        //Untill this
        if (!IsPostBack)
        {
            string templateName = TemplateName;

            IncludeCss("CategoryDetailcss", "/Templates/" + templateName + "/css/MessageBox/style.css",
                "/Templates/" + templateName + "/css/JQueryUIFront/jquery-ui.css",
                "/Templates/" + templateName + "/css/ToolTip/tooltip.css",
                "/Templates/" + templateName + "/css/JQueryCheckBox/uniform.default.css",
                "/Templates/" + templateName + "/css/MessageBox/style.css", "/Templates/" + templateName + "/css/CategoryBanner/cycle.css");
            IncludeJs("CategoryDetailjs", "/js/Templating/tmpl.js", "/js/encoder.js", "/js/Paging/jquery.pagination.js",
                         "/js/jquery.cycle.min.js", "/js/DateTime/date.js", "/js/MessageBox/jquery.easing.1.3.js",
                      "/js/MessageBox/alertbox.js", "/js/jquery.cookie.js", "/js/Scroll/jquery.tinyscrollbar.min.js",
                        "/js/JQueryCheckBox/jquery.uniform.js", "/Modules/AspxCommerce/DetailsBrowse/js/CategoryDetails.js");
        }
        IncludeLanguageJS();
        GetAspxTemplates();
        GetAllSubCategoryForFilter(aspxCommonObj);
        if (IsCategoryHasItems == 1)
        {
            CreateSortViewOption();
            GetShoppingFilterItemsResult(aspxCommonObj);
        }

    }

    private void GetAspxTemplates()
    {
        AspxTemplateValue = AspxGetTemplates.GetAspxTemplateKeyValue();

        foreach (AspxTemplateKeyValue value in AspxTemplateValue)
        {
            string templateInfos = value.TemplateKey + "@" + value.TemplateValue;
            Page.ClientScript.RegisterArrayDeclaration("jsTemplateArray", "\'" + templateInfos + "\'");
        }
    }

    private void OverRideSEOInfo(string categorykey, AspxCommonInfo aspxCommonObj)
    {
        CategorySEOInfo dtCatSEO = AspxFilterController.GetSEOSettingsByCategoryName(categorykey, aspxCommonObj);
        if (dtCatSEO != null)
        {
            string Name = dtCatSEO.Name.ToString();
            string PageTitle = dtCatSEO.MetaTitle.ToString();
            string PageKeyWords = dtCatSEO.MetaKeywords.ToString();
            string PageDescription = dtCatSEO.MetaDescription.ToString();

            if (!string.IsNullOrEmpty(PageTitle))
                SEOHelper.RenderTitle(this.Page, PageTitle, false, true, PortalID);
            else
                SEOHelper.RenderTitle(this.Page, Name, false, true, PortalID);

            if (!string.IsNullOrEmpty(PageKeyWords))
                SEOHelper.RenderMetaTag(this.Page, "KEYWORDS", PageKeyWords, true);

            if (!string.IsNullOrEmpty(PageDescription))
                SEOHelper.RenderMetaTag(this.Page, "DESCRIPTION", PageDescription, true);
        }
    }

    Hashtable hst = null;
    public void GetAllSubCategoryForFilter(AspxCommonInfo aspxCommonObj)
    {
        string resolvedUrl = ResolveUrl("~/");
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        hst = AppLocalized.getLocale(modulePath);
        string aspxTemplateFolderPath = resolvedUrl + "Templates/" + TemplateName;
        DataSet ds = AspxFilterController.GetCategoryDetailInfoForFilter(Categorykey, aspxCommonObj);
        if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
        {
            StringBuilder elem = new StringBuilder();
            DataTable dtSubCatWithItems = ds.Tables[0];
            elem.Append("<div class=\"filter\">");
            if (dtSubCatWithItems != null && dtSubCatWithItems.Rows.Count > 0)
            {
                elem.Append("<div id=\"divCat\" value=\"b01\" class=\"cssClasscategorgy\">");
                elem.Append("<div class=\"divTitle\"><b><label style=\"color:#006699\">");
                elem.Append(getLocale("Categories"));
                elem.Append("</label></b><img align=\"right\" src=\"");
                elem.Append(aspxTemplateFolderPath);
                elem.Append("/images/arrow_up.png\"/></div> <div id=\"scrollbar1\" class=\"cssClassScroll\"><div class=\"viewport\"><div class=\"overview\" id=\"catOverview\"><div class=\"divContentb01\"><ul id=\"cat\">");
                for (int i = 0; i < dtSubCatWithItems.Rows.Count; i++)
                {
                    elem.Append("<li><label><input class=\"chkCategory\" type=\"checkbox\" name=\"");
                    elem.Append(dtSubCatWithItems.Rows[i]["CategoryName"]);
                    elem.Append("\" ids=\"");
                    elem.Append(dtSubCatWithItems.Rows[i]["CategoryID"]);
                    elem.Append("\" value=\"");
                    elem.Append(dtSubCatWithItems.Rows[i]["CategoryName"]);
                    elem.Append("\"/> ");
                    elem.Append(dtSubCatWithItems.Rows[i]["CategoryName"]);
                    elem.Append("<span> (");
                    elem.Append(dtSubCatWithItems.Rows[i]["ItemCount"]);
                    elem.Append(")</span></label></li>");
                }
                elem.Append("</ul></div></div></div></div></div>");

            }

            string brandFilter = GetAllBrandForCategory(aspxCommonObj);
            elem.Append(brandFilter);
            elem.Append("</div>");
            ltrFilter.Text = elem.ToString();

            DataTable dtCatSlider = ds.Tables[1];
            if (dtCatSlider != null && dtCatSlider.Rows.Count > 0)
            {
                string Imagelist = string.Empty;
                StringBuilder strCatHeadSlider = new StringBuilder();
                StringBuilder strCatHeadScript = new StringBuilder();
                bool isNoCategoryImage = false;
                string categoryImagePath = "Modules/AspxCommerce/AspxCategoryManagement/uploads/";
                int imgCount = 0;
                strCatHeadSlider.Append("<div id=\"divHeader\" class=\"cssClassSlider\" style=\"display: none;\">");

                strCatHeadSlider.Append("<div id=\"slider-container\"><div id=\"sliderObj\" class=\"cat-slideshow-wrap\">");
                strCatHeadSlider.Append("<div class=\"cat_Slides cat-slide-container\">");

                for (int i = 0; i < dtCatSlider.Rows.Count; i++)
                {
                    if (dtCatSlider.Rows[i]["CategoryImage"].ToString() != string.Empty)
                    {
                        string[] imgUrlSegments = dtCatSlider.Rows[i]["CategoryImage"].ToString().Split('/');
                        string imgToBeAdded = imgUrlSegments[imgUrlSegments.Length - 1] + ';';
                        Imagelist += imgToBeAdded;
                        isNoCategoryImage = true;
                        string catDesc = dtCatSlider.Rows[i]["CategoryShortDesc"].ToString();

                        if (catDesc.Length > 300)
                        {
                            catDesc = catDesc.Substring(0, 300);
                            int index = 0;
                            index = catDesc.LastIndexOf(' ');
                            catDesc = catDesc.Substring(0, index);
                            catDesc = catDesc + "...";
                        }

                        //check for this
                        string href = resolvedUrl + "category/" + AspxUtility.fixedEncodeURIComponent(dtCatSlider.Rows[i]["CategoryName"].ToString()) + SageFrameSettingKeys.PageExtension;
                        imgCount++;
                        string catImagePath = dtCatSlider.Rows[i]["CategoryImage"].ToString();
                        strCatHeadSlider.Append("<div class=\"cat-slide-container-inner\"><div class=\"cssCatImage\"><a href=");
                        strCatHeadSlider.Append(href);
                        strCatHeadSlider.Append("><img src='");
                        strCatHeadSlider.Append(resolvedUrl);
                        strCatHeadSlider.Append(categoryImagePath);
                        strCatHeadSlider.Append(catImagePath);
                        strCatHeadSlider.Append("' alt='");
                        strCatHeadSlider.Append(dtCatSlider.Rows[i]["CategoryName"]);
                        strCatHeadSlider.Append("' title='");
                        strCatHeadSlider.Append(dtCatSlider.Rows[i]["CategoryName"]);
                        strCatHeadSlider.Append("' /></a></div><div class=\"cssCatDesc\"><span>");
                        strCatHeadSlider.Append(dtCatSlider.Rows[i]["CategoryName"]);
                        strCatHeadSlider.Append("</span><p>");
                        strCatHeadSlider.Append(catDesc);
                        strCatHeadSlider.Append("</p></div></div>");
                    }
                }
                if (isNoCategoryImage)
                {
                    InterceptImageController objImageResize = new InterceptImageController();
                    objImageResize.DynamicImageResizer(Imagelist, "Medium", "Category", aspxCommonObj);
                }
                strCatHeadSlider.Append("</div>");
                strCatHeadSlider.Append("<div class=\"slideshow-progress-bar-wrap\" style=\"display: none;\">");
                strCatHeadSlider.Append("<div class=\"highlight-bar\">");
                strCatHeadSlider.Append("<div class=\"edge left\"></div><div class=\"edge right\"></div>");
                strCatHeadSlider.Append("</div><div class=\"slideshow-progress-bar\"></div></div></div></div>");
                strCatHeadSlider.Append("</div>");
                strCatHeadScript.Append("$('#divHeader').show();");
                if (imgCount > 1)
                {
                    strCatHeadScript.Append("var catSlideshowWrap = jQuery('#slider-container').find('#sliderObj');");
                    strCatHeadScript.Append("var catSlidesContainer = catSlideshowWrap.find('div.cat-slide-container');");
                    strCatHeadScript.Append("var catSlides = catSlidesContainer.children('div');");
                    strCatHeadScript.Append("var pager = catSlideshowWrap.find('div.slideshow-progress-bar-wrap div.slideshow-progress-bar');");
                    strCatHeadScript.Append("var highlightBar = catSlideshowWrap.find('div.highlight-bar');");
                    strCatHeadScript.Append("var pagerMarkup = new Array();");
                    strCatHeadScript.Append("var pagerElPercentW = 1 / catSlides.length * 100;");
                    strCatHeadScript.Append("catSlides.each(function (i) {");
                    strCatHeadScript.Append("var oneBasedIndex = i + 1;");
                    strCatHeadScript.Append("pagerMarkup.push('<div class=pagerLink style=width: ' + pagerElPercentW + '%;><div class=pager' + oneBasedIndex + '></div></div>');");
                    strCatHeadScript.Append("});");
                    strCatHeadScript.Append("pager.append(pagerMarkup.join(''));");
                    strCatHeadScript.Append("highlightBar.css('width', pagerElPercentW + '%');");
                    strCatHeadScript.Append("var TRANSITION_SPEED = 500;");
                    strCatHeadScript.Append("catSlidesContainer.cycle({");
                    strCatHeadScript.Append("activePagerClass: 'active',");
                    strCatHeadScript.Append("before: function (curr, next, opts) {");
                    strCatHeadScript.Append("highlightBar.stop(true).animate(");
                    strCatHeadScript.Append("{");
                    strCatHeadScript.Append("'left': pager.find('div.pagerLink').eq(jQuery(next).index()).position().left");
                    strCatHeadScript.Append("},");
                    strCatHeadScript.Append("TRANSITION_SPEED");
                    strCatHeadScript.Append(");");
                    strCatHeadScript.Append("},");
                    strCatHeadScript.Append("fx: 'fade',");
                    strCatHeadScript.Append("speed: TRANSITION_SPEED,");
                    strCatHeadScript.Append("timeout: 5000,");
                    strCatHeadScript.Append("pause: 1,");
                    strCatHeadScript.Append("pauseOnPagerHover: 1,");
                    strCatHeadScript.Append("pager: '#slider-container #sliderObj div.slideshow-progress-bar-wrap div.slideshow-progress-bar',");
                    strCatHeadScript.Append("pagerAnchorBuilder: function (idx, slide) {");
                    strCatHeadScript.Append("return '#slider-container #sliderObj div.slideshow-progress-bar-wrap div.slideshow-progress-bar div.pagerLink:eq(' + idx + ')';");
                    strCatHeadScript.Append("},");
                    strCatHeadScript.Append("pagerEvent: 'mouseenter.cycle'");
                    strCatHeadScript.Append("});");
                    strCatHeadScript.Append("$('.slideshow-progress-bar-wrap').show();");
                    strCatHeadScript.Append("}");

                }
                if (!isNoCategoryImage)
                {
                    strCatHeadScript.Append(" $('#divHeader').remove();");
                }
                string script = GetStringScript(strCatHeadScript.ToString());
                strCatHeadSlider.Append(script);
                ltrCatSlider.Text = strCatHeadSlider.ToString();

            }

        }
    }
    public string GetAllBrandForCategory(AspxCommonInfo aspxCommonObj)
    {
        bool isByCategory = false;
        string aspxTemplateFolderPath = ResolveUrl("~/") + "Templates/" + TemplateName;
        List<BrandItemsInfo> lstBrandItem = AspxFilterController.GetAllBrandForCategory(Categorykey, isByCategory, aspxCommonObj);
        StringBuilder elem = new StringBuilder();
        List<int> arrBrand = new List<int>();

        if (lstBrandItem.Count > 0)
        {
            elem.Append("<div value=\"0\" class=\"cssClasscategorgy\">");
            elem.Append("<div class=\"divTitle\"><b><label style=\"color:#006699\">");
            elem.Append(getLocale("Brands"));
            elem.Append("</label></b><img align=\"right\" src=\"");
            elem.Append(aspxTemplateFolderPath);
            elem.Append("/images/arrow_up.png\" /></div><div id=\"scrollbar2\" class=\"cssClassScroll\"><div class=\"viewport\"><div class=\"overview\"><div class=\"divContent0\"><ul>");
            foreach (BrandItemsInfo value in lstBrandItem)
            {
                if (arrBrand.IndexOf(value.BrandID) == -1)
                {
                    elem.Append("<li><label><input class=\"chkFilter chkBrand\" type=\"checkbox\" name=\"");
                    elem.Append(value.BrandName);
                    elem.Append("\" ids=\"");
                    elem.Append(value.BrandID);
                    elem.Append("\" value=\"0\"/> ");
                    elem.Append(value.BrandName);
                    elem.Append("<span id=\"count\"> (");
                    elem.Append(value.ItemCount);
                    elem.Append(")</span></label></li>");
                    arrBrand.Add(value.BrandID);
                }
            }
            elem.Append("</ul></div></div></div></div></div>");
        }
        string shopFilter = GetShoppingFilter(aspxCommonObj);
        elem.Append(shopFilter);
        return elem.ToString();
    }
    public string GetShoppingFilter(AspxCommonInfo aspxCommonObj)
    {
        bool isByCategory = false;
        string aspxTemplateFolderPath = ResolveUrl("~/") + "Templates/" + TemplateName;
        List<Filter> lstFilter = AspxFilterController.GetShoppingFilter(aspxCommonObj, Categorykey, isByCategory);
        List<string> attrID = new List<string>();
        List<string> attrValue = new List<string>();
        StringBuilder elem = new StringBuilder();
        //StringBuilder scriptExecute = new StringBuilder();

        if (lstFilter != null && lstFilter.Count > 0)
        {
            int currentAttributeID = 0;
            //scriptExecute.Append("$('#divShopFilter').show();$('.divRange').show();");
            foreach (Filter value in lstFilter)
            {
                IsCategoryHasItems = 1;
                if (Int32.Parse(value.AttributeID) != 8 && Int32.Parse(value.AttributeID) > 48)
                {
                    if (attrID.IndexOf(value.AttributeID) == -1)
                    {
                        attrID.Add(value.AttributeID);
                        if (attrID.IndexOf(value.AttributeID) != -1)
                        {
                            if (Int32.Parse(value.AttributeID) != currentAttributeID && currentAttributeID > 0)
                            {
                                elem.Append("</ul></div></div></div></div></div>");
                            }
                            currentAttributeID = Int32.Parse(value.AttributeID);
                        }
                        elem.Append("<div value=");
                        elem.Append(value.AttributeID);
                        elem.Append(" class=\"cssClasscategorgy\"><div class=\"divTitle\"><b><label style=\"color:#006699\">");
                        elem.Append(value.AttributeName);
                        elem.Append("</label></b><img align=\"right\" src=\"");
                        elem.Append(aspxTemplateFolderPath);
                        elem.Append("/images/arrow_up.png\"/></div> <div id=\"scrollbar3\" class=\"cssClassScroll\"><div class=\"viewport\"><div class=\"overview\"><div class=");
                        elem.Append("divContent");
                        elem.Append(value.AttributeID);
                        elem.Append("><ul>");
                        attrValue = new List<string>();
                        elem.Append("<li><label><input class= \"chkFilter\" type=\"checkbox\" name=\"");
                        elem.Append(value.AttributeValue);
                        elem.Append("\" inputTypeID=\"");
                        elem.Append(value.InputTypeID);
                        elem.Append("\"  value=\"");
                        elem.Append(value.AttributeID);
                        elem.Append("\"/> ");
                        elem.Append(value.AttributeValue);
                        elem.Append("<span id=\"count\"> (");
                        elem.Append(value.ItemCount);
                        elem.Append(")</span></label></li>");
                        attrValue.Add(value.AttributeValue);

                    }
                    else
                    {
                        if (attrID.IndexOf(value.AttributeID) != -1)
                        {
                            if (Int32.Parse(value.AttributeID) != currentAttributeID && currentAttributeID > 0)
                            {
                                elem.Append("</ul></div></div></div></div></div>");
                            }
                            currentAttributeID = Int32.Parse(value.AttributeID);
                        }

                        if (attrValue.IndexOf(value.AttributeValue) == -1)
                        {
                            elem.Append("<li><label><input class=\"chkFilter\" type=\"checkbox\" name=\"");
                            elem.Append(value.AttributeValue);
                            elem.Append("\" inputTypeID=\"");
                            elem.Append(value.InputTypeID);
                            elem.Append("\"  value=\"");
                            elem.Append(value.AttributeID);
                            elem.Append("\"/> ");
                            elem.Append(value.AttributeValue);
                            elem.Append("<span id=\"count\"> (");
                            elem.Append(value.ItemCount);
                            elem.Append(")</span></label></li>");
                            attrValue.Add(value.AttributeValue);
                        }
                    }
                }

                else if (Int32.Parse(value.AttributeID) == 8)
                {
                    arrPrice.Add(value);
                    if (decimal.Parse(value.AttributeValue) > maxPrice)
                    {
                        maxPrice = decimal.Parse(value.AttributeValue);
                    }
                }
            }
            if (attrID.Count > 0)
            {
                elem.Append("</ul></div></div></div></div></div>");
            }
            decimal interval = (maxPrice - minPrice) / 4;
            elem.Append("<div value=\"8\" class=\"cssClassbrowseprice\">");
            elem.Append("<div class=\"divTitle\"><b><label style=\"color:#006699\">");
            elem.Append(getLocale("Price"));
            elem.Append("</label></b><img align=\"right\" src=\"");
            elem.Append(aspxTemplateFolderPath);
            elem.Append("/images/arrow_up.png\"/></div><div class=\"divContent8\"><ul>");
            if (arrPrice.Count > 1)
            {

                elem.Append(GetPriceData(minPrice, 1, interval) != minPrice.ToString("N2") ? "<li><a id=\"f1\" href=\"#\"  minprice=" + GetPriceData(minPrice, 0, interval) + " maxprice=" + GetPriceData(minPrice, 1, interval) + ">" + "<span class=\"cssClassFormatCurrency\">" + minPrice.ToString("N2") + "</span>" + " - " + "<span class=\"cssClassFormatCurrency\">" + GetPriceDataFloat(minPrice, 1, interval) + "</span>" + "</a></li>" : "");
                elem.Append(GetPriceData(minPrice, 0, interval) != GetPriceData(minPrice + decimal.Parse("0.01"), 1, interval) && GetPriceData(minPrice, 2, interval) != GetPriceData(minPrice, 1, interval) ? "<li><a id=\"f2\" href=\"#\"  minprice=" + GetPriceData(minPrice + decimal.Parse("0.01"), 1, interval) + " maxprice=" + GetPriceData(minPrice, 2, interval) + ">" + "<span class=\"cssClassFormatCurrency\">" + GetPriceDataFloat(minPrice + decimal.Parse("0.01"), 1, interval) + "</span>" + " - " + "<span class=\"cssClassFormatCurrency\">" + GetPriceDataFloat(minPrice, 2, interval) + "</span>" + "</a></li>" : "");
                elem.Append(GetPriceData(minPrice + decimal.Parse("0.01"), 1, interval) != GetPriceData(minPrice + decimal.Parse("0.01"), 2, interval) && GetPriceData(minPrice, 2, interval) != GetPriceData(minPrice, 3, interval) ? "<li><a id=\"f3\" href=\"#\"  minprice=" + GetPriceData(minPrice + decimal.Parse("0.01"), 2, interval) + " maxprice=" + GetPriceData(minPrice, 3, interval) + ">" + "<span class=\"cssClassFormatCurrency\">" + GetPriceDataFloat(minPrice + decimal.Parse("0.01"), 2, interval) + "</span>" + " - " + "<span class=\"cssClassFormatCurrency\">" + GetPriceDataFloat(minPrice, 3, interval) + "</span>" + "</a></li>" : "");
                elem.Append(GetPriceData(minPrice, 3, interval) != maxPrice.ToString("N2") ? "<li><a id=\"f4\" href=\"#\"  minprice=" + GetPriceData(minPrice + decimal.Parse("0.01"), 3, interval) + " maxprice=" + maxPrice + ">" + "<span class=\"cssClassFormatCurrency\">" + GetPriceDataFloat(minPrice + decimal.Parse("0.01"), 3, interval) + "</span>" + " - " + "<span class=\"cssClassFormatCurrency\">" + maxPrice.ToString("N2") + "</span>" + "</a></li>" : "");
            }
            if (arrPrice.Count == 1)
            {
                elem.Append("<li><a id=\"f1\" href=\"#\"   minprice=");
                elem.Append(GetPriceData(minPrice, 0, interval));
                elem.Append(" maxprice=");
                elem.Append(GetPriceData(minPrice, 1, interval));
                elem.Append(">");
                elem.Append("<span class=\"cssClassFormatCurrency\">");
                elem.Append(minPrice.ToString("N2"));
                elem.Append("</span>");
                elem.Append("</a></li>");
                minPrice = 0;
            }

            elem.Append("</ul></div>");
            elem.Append("<div class=\"divRange\"><div id=\"slider-range\"></div>");
            elem.Append("<p><b style=\"color: #006699\">");
            elem.Append(getLocale("Range: "));
            elem.Append("<span id=\"amount\">");
            elem.Append("<span class=\"cssClassFormatCurrency\">");
            elem.Append(minPrice.ToString("N2"));
            elem.Append("</span>");
            elem.Append(" - ");
            elem.Append("<span class=\"cssClassFormatCurrency\">");
            elem.Append(maxPrice.ToString("N2"));
            elem.Append("</span>");
            elem.Append("</span></b></p>");
            elem.Append("</div></div>");
            //string script = GetStringScript(scriptExecute.ToString());
            //elem.Append(script);
            return elem.ToString();
        }
        return string.Empty;
    }

    public void GetShoppingFilterItemsResult(AspxCommonInfo aspxCommonObj)
    {
        StringBuilder strItemsResult = new StringBuilder();
        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        string template = AspxGetTemplates.GetAspxTemplate(ViewAsOptionsDefault);
        string itemImagePath = ResolveUrl("~/") + "Modules/AspxCommerce/AspxItemsManagement/uploads/";
        List<ItemBasicDetailsInfo> lstItems = AspxFilterController.GetShoppingFilterItemsResult(1, 9, "", "", minPrice, maxPrice, Categorykey, false, SortByOptionsDefault, aspxCommonObj);
        if (lstItems != null && lstItems.Count > 0)
        {
            if (ViewAsOptionsDefault == 1)
            {
                strItemsResult.Append("<div class='cssCatProductGridWrapper'>");
            }
            Page.ClientScript.RegisterArrayDeclaration("arrItemsOptionToBind", json_serializer.Serialize(lstItems));
            string price = "0.00";
            string listPrice = "0.00";
            foreach (ItemBasicDetailsInfo value in lstItems)
            {
                string template1 = template;
                string imagePath = itemImagePath + value.BaseImage;
                if (value.BaseImage == string.Empty)
                {
                    imagePath = NoImageCategoryDetailPath;
                }
                else
                {
                    InterceptImageController.ImageBuilder(value.BaseImage, ImageType.Medium, ImageCategoryType.Item, aspxCommonObj);
                }
                string name = string.Empty;
                if (value.Name.Length > 50)
                {
                    name = value.Name.Substring(0, 50);
                    var i = 0;
                    i = name.LastIndexOf(' ');
                    name = name.Substring(0, i);
                    name = name + "...";
                }
                else
                {
                    name = value.Name;
                }
                string sku = json_serializer.Serialize(value.SKU);

                string title = json_serializer.Serialize(value.Name);
                price = (!string.IsNullOrEmpty(value.Price) ? Convert.ToDecimal(value.Price).ToString("N2") : "0.00");
                listPrice = (!string.IsNullOrEmpty(value.ListPrice) ? Convert.ToDecimal(value.ListPrice).ToString("N2") : "0.00");
                Dictionary<string, string> replacements = new Dictionary<string, string>();
                replacements.Add("${sku}", value.SKU);
                replacements.Add("${aspxRedirectPath}", aspxRedirectPath);
                replacements.Add("${pageExtension}", SageFrameSettingKeys.PageExtension);
                replacements.Add("${imagePath}", imagePath);
                replacements.Add("${alternateText}", value.AlternateText);
                replacements.Add("${name}", name);
                replacements.Add("${titleName}", title);
                replacements.Add("${parseFloat(price).toFixed(2)}", price);
                replacements.Add("${JSON2.stringify(sku)}", sku);
                replacements.Add("${isCostVariant}", json_serializer.Serialize(value.IsCostVariantItem.ToString()));
                replacements.Add("${1}", "1");
                replacements.Add("${price}", value.Price);
                if (value.ListPrice != string.Empty)
                {
                    replacements.Add("${parseFloat(listPrice).toFixed(2)}", listPrice);

                }
                else
                {
                    replacements.Add("<p class=\"cssClassProductOffPrice\">", string.Empty);
                    replacements.Add("<span class=\"cssRegularPrice_${itemID} cssClassFormatCurrency\">${parseFloat(listPrice).toFixed(2)}</span></p>", string.Empty);
                }
                // Replace
                if (value.AttributeValues != null)
                {
                    if (value.AttributeValues != string.Empty)
                    {
                        string attrHtml = string.Empty;
                        string[] attrValues = value.AttributeValues.Split(',');
                        for (var i = 0; i < attrValues.Length; i++)
                        {
                            string[] attributes = attrValues[i].Split('#');
                            string attributeName = attributes[0];
                            string attributeValue = attributes[1];
                            int inputType = Int32.Parse(attributes[2]);
                            string validationType = attributes[3];
                            attrHtml += "<div class=\"cssDynamicAttributes\">";
                            if (inputType == 7)
                            {
                                attrHtml += "<span class=\"cssClassFormatCurrency\">";
                            }
                            else
                            {
                                attrHtml += "<span>";
                            }
                            attrHtml += attributeValue;
                            attrHtml += "</span></div>";
                        }
                        replacements.Add("$DynamicAttr", attrHtml);
                    }
                    else
                    {
                        if (ViewAsOptionsDefault == 1)
                        {
                            replacements.Add("<div class=\"cssGridDyanamicAttr\">$DynamicAttr</div>", string.Empty);
                        }
                        else
                        {
                            replacements.Add("<div class=\"cssListDyanamicAttr\">$DynamicAttr</div>", string.Empty);
                        }
                    }
                }
                else
                {
                    if (ViewAsOptionsDefault == 1)
                    {
                        replacements.Add("<div class=\"cssGridDyanamicAttr\">$DynamicAttr</div>", string.Empty);
                    }
                    else
                    {
                        replacements.Add("<div class=\"cssListDyanamicAttr\">$DynamicAttr</div>", string.Empty);
                    }
                }

                if (AllowAddToCart.ToLower() == "true")
                {
                    if (AllowOutStockPurchase.ToLower() == "false")
                    {
                        if ((bool)value.IsOutOfStock)
                        {
                            replacements.Add("<span class=\"sfLocale\">" + getLocale("Cart +") + "</span>", "<span class=\"sfLocale\">" + getLocale("Out Of Stock") + "</span>");
                            replacements.Add("<div class=\"cssClassAddtoCard_${itemID} cssClassAddtoCard\">", "<div class=\"cssClassAddtoCard_${itemID} cssClassOutOfStock\">");
                            replacements.Add("<label class=\"i-cart cssClassCartLabel cssClassGreenBtn\">", "<label class=\"cssClassCartLabel\">");
                            replacements.Add("onclick=\"AspxCommerce.RootFunction.AddToCartFromJS(${itemID},${price},${JSON2.stringify(sku)},${1},${isCostVariant},this);", string.Empty);
                        }
                    }
                }
                else
                {
                    replacements.Add("<div class=\"cssClassAddtoCard_${itemID} cssClassAddtoCard\">", "<div class=\"cssClassAddtoCard_${itemID} cssClassAddtoCard\" style=\"display: none;\">");
                }
                if (value.ItemTypeID == 5)
                {
                    replacements.Add("<p class=\"cssClassProductRealPrice\">", "<p class=\"cssClassProductRealPrice\">" + getLocale("Starting At") + "");
                }
                if (ViewAsOptionsDefault == 2)
                {
                    replacements.Add("{{html shortDescription}}", Server.HtmlDecode(value.ShortDescription));

                }

                replacements.Add("${itemID}", value.ItemID.ToString());
                foreach (var replacement in replacements)
                {
                    template1 = template1.Replace(replacement.Key, replacement.Value);
                }
                strItemsResult.Append(template1);
            }
            if (ViewAsOptionsDefault == 1)
            {

                strItemsResult.Append("</div>");
                StringBuilder strScriptExecute = new StringBuilder();
                strScriptExecute.Append("var $container");
                strScriptExecute.Append("= ");
                strScriptExecute.Append("$('.cssCatProductGridWrapper');");
                strScriptExecute.Append("$container.imagesLoaded(function () {");
                strScriptExecute.Append("$container.masonry({");
                strScriptExecute.Append("itemSelector: '.cssClassProductsBox',");
                strScriptExecute.Append("EnableSorting: false");
                strScriptExecute.Append("});");
                strScriptExecute.Append("});");
                string script = GetStringScript(strScriptExecute.ToString());
                strItemsResult.Append(script);
            }

            ltrCategoryItems.Text = strItemsResult.ToString();
            CreateDropdownPageSize(lstItems.Count);
        }
        else
        {
            ltrCategoryItems.Text = ("<span class=\"cssClassNotFound\">" + getLocale("No items found or matched!") + "</span>").ToString();
        }
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
        strPage.Append("<select class=\"sfListmenu\" id=\"ddlPageSize\">");
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
            strSortView.Append("<select id=\"ddlViewAs\" class=\"sfListmenu\" style=\"display: none\">");
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
        strSortView.Append("<select id=\"ddlSortBy\" class=\"sfListmenu\">");
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
        script.Append("<script type=\"text/javascript\">$(document).ready(function(){ ");
        script.Append(codeToRun);
        script.Append("});</script>");
        return script.ToString();
    }

    //private string GetStringScript(string codeToRun)
    //{
    //    StringBuilder script = new StringBuilder();
    //    script.Append("<script type=\"text/javascript\">$(document).ready(function(){ setTimeout(function(){ " + codeToRun + "},500); });</script>");
    //    return script.ToString();
    //}


    private string getLocale(string messageKey)
    {
        string retStr = messageKey;
        if (hst != null && hst[messageKey] != null)
        {
            retStr = hst[messageKey].ToString();
        }
        return retStr;
    }

    private string GetPriceData(decimal price, int count, decimal interval)
    {
        return ((price + (count * interval)).ToString());
    }
    private string GetPriceDataFloat(decimal price, int count, decimal interval)
    {
        return ((price + (count * interval)).ToString("N2"));
    }
}