using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using AspxCommerce.MegaCategory;
using SageFrame.Web;
using AspxCommerce.ImageResizer;

public partial class Modules_AspxCommerce_AspxMegaCategory_MegaCategoryView : BaseAdministrationUserControl
{
    public int noOfColumn;
    public bool showCatImage, showSubCatImage;
    public string modeOfView, speed, direction, eventMega, effect, NoImageCategoryDetailPath;
    string categoryImagePath = string.Empty;
    public string CategoryRss, RssFeedUrl;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int StoreID, PortalID;
            string UserName, CultureName;
            GetPortalCommonInfo(out StoreID, out PortalID, out UserName, out CultureName);
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName); 
            if (!IsPostBack)
            {
                string templateName = TemplateName;
                IncludeJs("MegaCategoryView", "/js/jquery.cookie.js", "/Modules/AspxCommerce/AspxMegaCategory/js/hoverIntent.js", 
                    "/Modules/AspxCommerce/AspxMegaCategory/js/jquery.dcverticalmegamenu.1.3.js", 
                    "/Modules/AspxCommerce/AspxMegaCategory/js/jquery.dcmegamenu.1.3.3.js",
                    "/Modules/AspxCommerce/AspxMegaCategory/js/jquery.dcjqaccordion.2.7.js");
                IncludeCss("MegaCategoryView", "/Templates/" + templateName + "/css/MegaMenu/dcverticalmegamenu.css",
                    "/Templates/" + templateName + "/css/MegaMenu/dcmegamenu.css", "/Modules/AspxCommerce/AspxMegaCategory/css/module.css");
                StoreSettingConfig ssc = new StoreSettingConfig();
                ssc.GetStoreSettingParamTwo(StoreSetting.DefaultProductImageURL, StoreSetting.NewCategoryRss, out NoImageCategoryDetailPath,
                    out CategoryRss, StoreID, PortalID, CultureName);
                if (CategoryRss.ToLower() == "true")
                {
                    RssFeedUrl = ssc.GetStoreSettingsByKey(StoreSetting.RssFeedURL, StoreID, PortalID, CultureName);
                }
            }
            GetMegaCategorySetting(aspxCommonObj);
            GetCategoryMenuList(aspxCommonObj);
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    private void GetMegaCategorySetting (AspxCommonInfo aspxCommonObj)
    {
        MegaCategoryController objCat = new MegaCategoryController();
        MegaCategorySettingInfo megaCatSetting = objCat.GetMegaCategorySetting(aspxCommonObj);
        if (megaCatSetting != null)
        {
            modeOfView = megaCatSetting.ModeOfView;
            noOfColumn = megaCatSetting.NoOfColumn;
            showCatImage = megaCatSetting.ShowCategoryImage;
            showSubCatImage = megaCatSetting.ShowSubCategoryImage;
            speed = megaCatSetting.Speed;
            effect = megaCatSetting.Effect;
            eventMega = megaCatSetting.EventMega;
            direction = megaCatSetting.Direction;
        }
    }
    Hashtable hst = null;
    private void GetCategoryMenuList(AspxCommonInfo aspxCommonObj)
    {
        categoryImagePath = "Modules/AspxCommerce/AspxCategoryManagement/uploads/";
        string scriptAdd = string.Empty;
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        hst = AppLocalized.getLocale(modulePath);
        MegaCategoryController objCat = new MegaCategoryController();
        List<MegaCategoryViewInfo> megaCatIfo = objCat.GetCategoryMenuList(aspxCommonObj);
        if (megaCatIfo != null && megaCatIfo.Count > 0)
        {
            int categoryID = 0;
            StringBuilder catListmaker = new StringBuilder();
            if (modeOfView == "vertical")
            {
                catListmaker.Append(
                    "<div id=\"divCatHeader\" class=\"divHeaderTitle\"><h2 class=\"cssClassLeftHeader\"><span>");
                catListmaker.Append(getLocale("Categories"));
                catListmaker.Append("</span>");

                catListmaker.Append(
                    "<a class=\"cssRssImage\" href=\"#\" style=\"display: none\"><img id=\"categoryRssImage\" alt=\"\" src=\"\" title=\"\" /></a></h2></div>");
                catListmaker.Append("<ul class='mega-menuV' id='divMegaMenu'>");               
            }
            else if (modeOfView == "horizontal")
            {               
                catListmaker.Append("<ul class='mega-menuH' id='mega-menuH'>");               
            }
            else
            {
                catListmaker.Append("<div id=\"divCatHeader\" style='display:none'><span>");
                catListmaker.Append(getLocale("Categories"));
                catListmaker.Append("</span></div>");
                catListmaker.Append("<ul class='mega-menuV cssMegaCategoryLi' id='divMegaMenu'>");
                catListmaker.Append("<div class=\"cssCollapable\"><h2><span>");
                catListmaker.Append(getLocale("All Categories"));
                catListmaker.Append("</span><a class=\"cssRssImage\" href=\"#\" style=\"display: none\"><img id=\"categoryRssImage\" alt=\"\" src=\"\" title=\"\" /></a></h2></div>");
               
            }
           
            foreach (MegaCategoryViewInfo eachCat in megaCatIfo)
            {
                categoryID = eachCat.CategoryID;
                if (eachCat.CategoryLevel == 0)
                {
                    catListmaker.Append("<li><a href=\"");
                    catListmaker.Append(aspxRedirectPath);
                    catListmaker.Append("category/");
                    string strRet = AspxUtility.fixedEncodeURIComponent(eachCat.AttributeValue);
                    catListmaker.Append(strRet);
                    catListmaker.Append(SageFrameSettingKeys.PageExtension);
                    catListmaker.Append("\">");
                    catListmaker.Append(eachCat.AttributeValue);
                    catListmaker.Append("</a>");
                    if (eachCat.ChildCount > 0)
                    {
                        catListmaker.Append("<ul>");
                        catListmaker.Append(BindChildCategory(megaCatIfo, categoryID, aspxCommonObj));
                        if (showCatImage == true)
                        {
                            if (eachCat.CategoryImagePath != null && eachCat.CategoryImagePath != "")
                            {
                                //Resize Image Dynamically
                                InterceptImageController.ImageBuilder(eachCat.CategoryImagePath, ImageType.Medium, ImageCategoryType.Category, aspxCommonObj);
                                string imagePath = aspxRedirectPath + categoryImagePath + eachCat.CategoryImagePath;
                                catListmaker.Append("<div class=\"classCatImage\"><img src=\"");
                                catListmaker.Append(imagePath.Replace("uploads", "uploads/Medium"));
                                catListmaker.Append("\" alt=\"");
                                catListmaker.Append(eachCat.AttributeValue);
                                catListmaker.Append("\" title=\"");
                                catListmaker.Append(eachCat.AttributeValue);
                                catListmaker.Append("\" /></div>");

                            }
                        }
                        catListmaker.Append("</ul>");
                    }
                    catListmaker.Append("</li>");
                }
            }
            catListmaker.Append("</ul>");
                       catListmaker.Append("<div id=\"sf-Responsive-Cat\" style=\"display:none;\">");
            catListmaker.Append("<div id=\"sf-CatMenu\">");
            catListmaker.Append(getLocale("Categories"));
            catListmaker.Append("</div>");
            catListmaker.Append("<ul class=\"sf-CatContainer\" style=\"display:none;\">");
            foreach (MegaCategoryViewInfo eachCat in megaCatIfo)
            {
                categoryID = eachCat.CategoryID;
                if (eachCat.CategoryLevel == 0)
                {
                    if (eachCat.ChildCount > 0)
                    {
                        catListmaker.Append("<li class=\"parent\">");
                    }
                    else
                    {
                        catListmaker.Append("<li>");
                    }
                    catListmaker.Append("<a href=\"");
                    catListmaker.Append(aspxRedirectPath);
                    catListmaker.Append("category/");
                    string strRet = AspxUtility.fixedEncodeURIComponent(eachCat.AttributeValue);
                    catListmaker.Append(strRet);
                    catListmaker.Append(SageFrameSettingKeys.PageExtension);
                    catListmaker.Append("\">");
                    catListmaker.Append(eachCat.AttributeValue);
                    catListmaker.Append("</a>");
                    if (eachCat.ChildCount > 0)
                    {
                        catListmaker.Append("<ul style=\"display:none;\">");
                        catListmaker.Append(BindResChildCategory(megaCatIfo, categoryID, aspxCommonObj));
                        catListmaker.Append("</ul style=\"display:none;\">");
                    }
                    catListmaker.Append("</li>");
                }
            }
            catListmaker.Append("</ul></div>");
            divMegaCategory.InnerHtml = catListmaker.ToString();

        }
        else
        {
            string strText = string.Empty;
            if (modeOfView == "collapseable")
            {
                strText = "<div class=\"cssCollapable\"><h2><span>"+getLocale("All Categories")+"</span><a class=\"cssRssImage\" href=\"#\" style=\"display: none\"><img id=\"categoryRssImage\" alt=\"\" src=\"\" title=\"\" /></a></h2></div>";
            }
            strText += ("<span id=\"spanCatNotFound\" class=\"cssClassNotFound\">" + getLocale("This store has no category found!") + "</span>");//Need to add Local Text
                                 divMegaCategory.InnerHtml = strText;
        }
    }


    public string BindChildCategory(List<MegaCategoryViewInfo> response, int categoryID,AspxCommonInfo aspxCommonObj)
    {
        StringBuilder strListmaker = new StringBuilder();
        string childNodes = string.Empty;
        foreach (MegaCategoryViewInfo eachCat in response)
        {
            if (eachCat.CategoryLevel > 0)
            {
                if (eachCat.ParentID == categoryID)
                {

                    strListmaker.Append("<li>");
                    strListmaker.Append("<a href=\"");
                    strListmaker.Append(aspxRedirectPath);
                    strListmaker.Append("category/");
                    string strRet = AspxUtility.fixedEncodeURIComponent(eachCat.AttributeValue);
                    strListmaker.Append(strRet);
                    strListmaker.Append(SageFrameSettingKeys.PageExtension);
                    strListmaker.Append("\">");
                    strListmaker.Append(eachCat.AttributeValue);
                    strListmaker.Append("</a>");
                    childNodes = BindChildCategory(response, eachCat.CategoryID, aspxCommonObj);
                    if (childNodes != string.Empty)
                    {
                        strListmaker.Append("<ul>");
                        strListmaker.Append(childNodes);
                        strListmaker.Append("</ul>");
                    }
                    if (showSubCatImage == true)
                    {
                        if (eachCat.CategoryLevel == 1)
                        {
                            if (eachCat.CategoryImagePath != null && eachCat.CategoryImagePath != "")
                            {
                                //Resize Image Dynamically
                                InterceptImageController.ImageBuilder(eachCat.CategoryImagePath, ImageType.Medium, ImageCategoryType.Category, aspxCommonObj);
                                string imagePath=aspxRedirectPath + categoryImagePath + eachCat.CategoryImagePath;
                                strListmaker.Append("<div class=\"classMegaSubCatImage\"><img src=\"");
                                strListmaker.Append(imagePath.Replace("uploads", "uploads/Small"));
                                strListmaker.Append("\" alt=\"");
                                strListmaker.Append(eachCat.AttributeValue);
                                strListmaker.Append("\" title=\"");
                                strListmaker.Append(eachCat.AttributeValue);
                                strListmaker.Append("\" /></div>");
                            }
                        }
                    }
                    strListmaker.Append("</li>");
                }
            }
        }
        return strListmaker.ToString();

    }
    public string BindResChildCategory(List<MegaCategoryViewInfo> response, int categoryID, AspxCommonInfo aspxCommonObj)
    {
        StringBuilder strListmaker = new StringBuilder();
        string childNodes = string.Empty;
        foreach (MegaCategoryViewInfo eachCat in response)
        {
            if (eachCat.CategoryLevel > 0)
            {
                if (eachCat.ParentID == categoryID)
                {
                    if (eachCat.ChildCount > 0)
                    {
                        strListmaker.Append("<li class=\"parent\">");
                    }
                    else
                    {
                        strListmaker.Append("<li>");
                    }
                    strListmaker.Append("<a href=\"");
                    strListmaker.Append(aspxRedirectPath);
                    strListmaker.Append("category/");
                    string strRet = AspxUtility.fixedEncodeURIComponent(eachCat.AttributeValue);
                    strListmaker.Append(strRet);
                    strListmaker.Append(SageFrameSettingKeys.PageExtension);
                    strListmaker.Append("\">");
                    strListmaker.Append(eachCat.AttributeValue);
                    strListmaker.Append("</a>");
                    childNodes = BindChildCategory(response, eachCat.CategoryID, aspxCommonObj);
                    if (childNodes != string.Empty)
                    {
                        strListmaker.Append("<ul>");
                        strListmaker.Append(childNodes);
                        strListmaker.Append("</ul>");
                    }                  
                    strListmaker.Append("</li>");
                }
            }
        }
        return strListmaker.ToString();

    }
    private string GetStringScript(string codeToRun)
    {
        StringBuilder script = new StringBuilder();
        script.Append("<script type=\"text/javascript\">$(document).ready(function(){");
        script.Append(codeToRun );
        script.Append(" });</script>");
        return script.ToString();
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
}
