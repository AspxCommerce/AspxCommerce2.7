using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxSiteMap_SiteMap : BaseAdministrationUserControl
{
    public string ModulePath = "";
    public int UserModuleID = 0;
    public int PortalID = 0;
    public string UserName = "", CultureName = "";
    public string PageExtension = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageExtension = SageFrameSettingKeys.PageExtension;
        UserName = GetUsername;
        UserModuleID = Int32.Parse(SageUserModuleID);
        PortalID = GetPortalID;
        CultureName = GetCurrentCultureName;
        ModulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
        IncludeCss("sitemapcss", "/Templates/" + TemplateName + "/css/AspxSiteMap/slickmap.css");
        if (!IsPostBack)
        {
            GetCategoryMenuListWithProduct();
        }
    }

    private void GetCategoryMenuListWithProduct()
    {

        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = GetStoreID;
        aspxCommonObj.PortalID = GetPortalID;
        aspxCommonObj.UserName = GetUsername;
        aspxCommonObj.CultureName = GetCurrentCultureName;
        List<CategoryInfo> catInfo = AspxCategoryListController.GetCategoryMenuList(aspxCommonObj);
        if (catInfo != null && catInfo.Count > 0)
        {
            int categoryID = 0;
            StringBuilder catListmaker = new StringBuilder();
            string catList = string.Empty;
            catListmaker.Append("<ul class='sfSiteMap' id='primaryNav'>");
            foreach (CategoryInfo eachCat in catInfo)
            {
                categoryID = eachCat.CategoryID;
                if (eachCat.CategoryLevel == 0)
                {
                    string hrefParentCategory = string.Empty;
                    catListmaker.Append("<li><a href=");
                    catListmaker.Append(aspxRedirectPath);
                    catListmaker.Append("category/");
                    string strRet = fixedEncodeURIComponent(eachCat.AttributeValue);
                    catListmaker.Append(strRet);
                    catListmaker.Append(SageFrameSettingKeys.PageExtension);
                    catListmaker.Append(">");
                    catListmaker.Append(eachCat.AttributeValue);
                    catListmaker.Append("</a>");
                    string items = BindProductByCategory(eachCat.CategoryID);
                    if (!string.IsNullOrEmpty(items))
                    {
                        catListmaker.Append(items);
                    }
                    if (eachCat.ChildCount > 0)
                    {
                        catListmaker.Append("<ul>");
                        catListmaker.Append(BindChildCategory(catInfo, categoryID));
                        catListmaker.Append("</ul>");
                    }
                    catListmaker.Append("</li>");
                }
            }
            catListmaker.Append("</ul>");
            ltAspxSiteMap.Text = catListmaker.ToString();
        }
        //else
        //{
        //    string strText = ("<span id=\"spanCatNotFound\" class=\"cssClassNotFound\"></span>");//Need to add Local Text
        //}

    }
    private string BindProductByCategory(int categoryID)
    {   AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = GetStoreID;
        aspxCommonObj.PortalID = GetPortalID;
        aspxCommonObj.UserName = GetUsername;
        aspxCommonObj.CultureName = GetCurrentCultureName;
        StringBuilder productList = new StringBuilder();
         List<ItemCommonInfo> itemsList= AspxItemMgntController.GetItemsListByCategory(aspxCommonObj, categoryID);
         if (itemsList.Count>0)
        {
            productList.Append("<li class=\"sfProductList\"><ul class=\"sfProduct\">");
            foreach (ItemCommonInfo itemCommonInfo in itemsList)
            {
                productList.Append("<li>");
                productList.Append("<a href=\"" + aspxRedirectPath + "item/" + fixedEncodeURIComponent(itemCommonInfo.SKU) + SageFrameSettingKeys.PageExtension + " \">");
                productList.Append(itemCommonInfo.ItemName);
                productList.Append("</li>");
            }
         
            productList.Append("</ul></li>");
        }

        return productList.ToString();
    }

    public string BindChildCategory(List<CategoryInfo> response, int categoryID)
    {
        StringBuilder strListmaker = new StringBuilder();
        string childNodes = string.Empty;
        foreach (CategoryInfo eachCat in response)
        {
            if (eachCat.CategoryLevel > 0)
            {
                if (eachCat.ParentID == categoryID)
                {
                    strListmaker.Append("<li><a href=");
                    strListmaker.Append(aspxRedirectPath);
                    strListmaker.Append("category/");
                    string strRet = fixedEncodeURIComponent(eachCat.AttributeValue);
                    strListmaker.Append(strRet);
                    strListmaker.Append(SageFrameSettingKeys.PageExtension);
                    strListmaker.Append(">");
                    strListmaker.Append(eachCat.AttributeValue);
                    strListmaker.Append("</a>");
                    string items = BindProductByCategory(eachCat.CategoryID);
                    if (!string.IsNullOrEmpty(items))
                    {
                        strListmaker.Append(items);
                    }
                    childNodes = BindChildCategory(response, eachCat.CategoryID);
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
}
