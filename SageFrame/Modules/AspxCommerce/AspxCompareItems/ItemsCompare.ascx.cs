/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

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
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SageFrame.Framework;
using SageFrame.Web;
using AspxCommerce.Core;
using SageFrame.Core;
using AspxCommerce.CompareItem;
using AspxCommerce.ImageResizer;
using System.Data;

public partial class Modules_AspxCompareItems_ItemsCompare : BaseAdministrationUserControl
{
    public string SessionCode = string.Empty;
    private int StoreID, PortalID, CustomerID;
    private string UserName, CultureName, modulePath;
    private Hashtable hst = null;

    public int MaxCompareItemCount = 0, compareLen = 0;
    public string CompareItemListURL, DefaultImagePath, ServicePath;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetPortalCommonInfo(out StoreID, out PortalID, out CustomerID, out UserName, out CultureName, out SessionCode);
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo(StoreID, PortalID, UserName, CultureName, CustomerID, SessionCode);
            if (!IsPostBack)
            {
                modulePath = this.AppRelativeTemplateSourceDirectory;
                ServicePath = ResolveUrl(modulePath);
                hst = AppLocalized.getLocale(modulePath);
                IncludeJs("ItemsCompare", "/js/jquery.cookie.js",
                    "/Modules/AspxCommerce/AspxCompareItems/js/ItemsCompare.js");
                IncludeCss("ItemsCompare", "/Modules/AspxCommerce/AspxCompareItems/css/module.css");
                StoreSettingConfig ssc = new StoreSettingConfig();
                DefaultImagePath = ssc.GetStoreSettingsByKey(StoreSetting.DefaultProductImageURL, StoreID, PortalID, CultureName);
            }
            IncludeLanguageJS();
            CompareItems(aspxCommonObj);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    #region Localization
    private string getLocale(string messageKey)
    {
        string retStr = messageKey;
        if (hst != null && hst[messageKey] != null)
        {
            retStr = hst[messageKey].ToString();
        }
        return retStr;
    }
    #endregion

    #region CompareItems
    private void CompareItems(AspxCommonInfo aspxCommonObj)
    {
        string aspxRootPath = ResolveUrl("~/");
        string imagePath = string.Empty, ItemCostVariantValue = string.Empty, costVariantIds = string.Empty;
        int totalRowsOfItem = 0;
        StringBuilder compareItemContent;
        DataSet dsCompareItems = CompareItemController.GetCompareItemsDataSet(aspxCommonObj);
        if (dsCompareItems != null && dsCompareItems.Tables.Count == 2)
        {
            #region Compare Item Setting
            DataTable dtCompareItemSetting = dsCompareItems.Tables[0];
            if (dtCompareItemSetting != null && dtCompareItemSetting.Rows.Count > 0)
            {
                MaxCompareItemCount = int.Parse(dtCompareItemSetting.Rows[0]["CompareItemCount"].ToString());
                CompareItemListURL = dtCompareItemSetting.Rows[0]["CompareDetailsPage"].ToString();
            }
            #endregion
            #region Bind Compare Items
            DataTable dtCompareItems = dsCompareItems.Tables[1];
            totalRowsOfItem = dtCompareItems.Rows.Count;
            compareItemContent = new StringBuilder();
            int loopCount = (totalRowsOfItem < MaxCompareItemCount) ? totalRowsOfItem : MaxCompareItemCount;
            if (dtCompareItems != null && totalRowsOfItem > 0)
            {
                for (int i = 0; i < loopCount; i++)
                {
                    imagePath = dtCompareItems.Rows[i]["ImagePath"].ToString();
                    if (string.IsNullOrEmpty(imagePath.Trim()))
                    {
                        imagePath = DefaultImagePath;
                    }
                    else
                    {
                        imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + imagePath;
                        //Resize Image Dynamically
                        InterceptImageController.ImageBuilder(imagePath, ImageType.Small, ImageCategoryType.Item, aspxCommonObj);
                    }
                    compareItemContent.Append("<div class=\"productBox compareProduct\" id=\"compareProductBox-");
                    compareItemContent.Append(dtCompareItems.Rows[i]["CompareItemID"]);
                    compareItemContent.Append("\" data=");
                    compareItemContent.Append(dtCompareItems.Rows[i]["ItemID"]);
                    compareItemContent.Append(" costVariant=");
                    compareItemContent.Append(dtCompareItems.Rows[i]["CostVariantValueID"]);
                    compareItemContent.Append(">");
                    compareItemContent.Append("<div id=\"compareCompareClose-");
                    compareItemContent.Append(dtCompareItems.Rows[i]["ItemID"]);
                    compareItemContent.Append("\" onclick=\"ItemsCompareAPI.RemoveFromAddToCompareBox(");
                    compareItemContent.Append(dtCompareItems.Rows[i]["ItemID"]);
                    compareItemContent.Append(',');
                    compareItemContent.Append(dtCompareItems.Rows[i]["CompareItemID"]);
                    compareItemContent.Append(");\" class=\"compareProductClose\"><i class='i-close'>cancel</i></div>");
                    compareItemContent.Append("<div class=\"productImage\"><img src=");
                    compareItemContent.Append(aspxRootPath);
                    compareItemContent.Append(imagePath.Replace("uploads", "uploads/Small"));
                    compareItemContent.Append("></div>");
                    compareItemContent.Append("<div class=\"productName\">");
                    compareItemContent.Append(dtCompareItems.Rows[i]["ItemName"]);
                    ItemCostVariantValue = dtCompareItems.Rows[i]["ItemCostVariantValue"].ToString();
                    if (!string.IsNullOrEmpty(ItemCostVariantValue))
                    {
                        compareItemContent.Append("<br/>");
                        compareItemContent.Append(ItemCostVariantValue);
                    }
                    compareItemContent.Append("</div></div>");
                    costVariantIds += ItemCostVariantValue + "#";
                    compareLen++;
                }
            }
            if (MaxCompareItemCount - totalRowsOfItem > 0)
            {
                for (int i = 0; i < (MaxCompareItemCount - totalRowsOfItem); i++)
                {
                    compareItemContent.Append("<div class=\"empty productBox\"></div>");
                }
            }
            StringBuilder errorText = new StringBuilder();
            errorText.Append("<div id=\"compareErrorText\">");
            errorText.Append(getLocale("Sorry, You can not add more than"));
            errorText.Append("&nbsp;");
            errorText.Append(MaxCompareItemCount);
            errorText.Append("&nbsp;");
            errorText.Append(getLocale("items"));
            errorText.Append(".</div>");
            ltrCompareItem.Text = compareItemContent.ToString();
            ltrError.Text = errorText.ToString();
            #endregion
        }
    }
    #endregion
}
