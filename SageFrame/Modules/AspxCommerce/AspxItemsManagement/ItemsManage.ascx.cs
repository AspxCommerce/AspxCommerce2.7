/*
SageComers® - http://www.aspxcommerce.com
Copyright (c) 2011-2014 by SageComers

The Software can be used without modification for any purpose provided that the copy you are 
using is the licensed product that you have paid for. The number of unique installation of the
software is limited which is fixed at the time of purchase. You are not liable to copy, decompile
or disassemble the Software for any non-production or production purposes. 

You may make copies of the Software for back-up purposes, provided that you reproduce the
Software in its original form and with all copyright information, attribution and proprietary
notices remaining intact within the files of the back-up copy. 

The above copyright notice and this permission notice shall be included in all copies
or substantial portions of the Software.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS
OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/



using System;
using System.Web.UI;
using SageFrame.Framework;
using SageFrame.Security;
using SageFrame.Security.Entities;
using SageFrame.Web;
using System.Web.Security;
using AspxCommerce.Core;
using SageFrame.Core;
using System.Web.Script.Serialization;
using System.Text;
using SageFrame.Localization;
using System.Collections.Generic;
using System.Web;
using SageFrame.Common;
using System.Linq;

public partial class Modules_AspxItemsManagement_ItemsManage : BaseAdministrationUserControl
{
    public int StoreID, PortalID;
    public string UserName, CultureName, PriceUnit, DimensionUnit, WeightUnit, AllowOutStockPurchase,OutOfStockQuantity;
    public string userEmail = string.Empty;    
    public int MaximumFileSize, MaxDownloadFileSize;
    public string LowStockItemRss, RssFeedUrl,AllowRealTimeNotifications;
    public string CurrencyCodeSlected;
    public string Settings, UserModuleId;
    protected void Page_Load(object sender, EventArgs e)    
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("ItemsManage", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css", "/Templates/" + TemplateName + "/css/AjaxUploader/fileuploader.css",
                   "/Templates/" + TemplateName + "/css/Tabs/slidingtabs-vertical.css","/Modules/AspxCommerce/AspxItemsManagement/css/module.css");
              
                IncludeJs("ItemsManage",  "/js/GridView/jquery.grid.js","/js/FormValidation/jquery.validate.js",
                          "/js/GridView/SagePaging.js", "/js/GridView/jquery.global.js",
                          "/js/GridView/jquery.dateFormat.js", "/js/DateTime/date.js",
                          "/js/ImageGallery/jquery.mousewheel.js",
                          "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js",                        
                          "/js/Tabs/jquery.slidingtabs.js",
                          "/js/AjaxFileUploader/ajaxupload.js", "/js/PopUp/custom.js",
                          "/Modules/AspxCommerce/AspxItemsManagement/js/ItemManagement.js", "/js/PopUp/popbox.js",
                          "/js/CurrencyFormat/jquery.formatCurrency-1.4.0.js",
                          "/js/CurrencyFormat/jquery.formatCurrency.all.js", "/js/AjaxFileUploader/fileuploader.js");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ckEditorUserModuleID", " var ckEditorUserModuleID='" + SageUserModuleID + "';", true);
                IncludeJs("ItemsManageCk", "/Editors/ckeditor/ckeditor.js", "/Editors/ckeditor/adapters/jquery.js");
                //Page.ClientScript.RegisterClientScriptInclude("JQueryFormValidated", ResolveUrl("~/js/FormValidation/jquery.validate.js"));
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                UserModuleId = SageUserModuleID;
                CultureName = GetCurrentCultureName;
                SecurityPolicy objSecurity = new SecurityPolicy();
                FormsAuthenticationTicket ticket = objSecurity.GetUserTicket(GetPortalID);
                if (ticket != null && ticket.Name != ApplicationKeys.anonymousUser)
                {
                    MembershipController member = new MembershipController();
                    UserInfo userDetail = member.GetUserDetails(GetPortalID, GetUsername);
                    userEmail = userDetail.Email;
                }
                StoreSettingConfig ssc = new StoreSettingConfig();
                MaximumFileSize = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.MaximumImageSize, StoreID, PortalID, CultureName));
                MaxDownloadFileSize = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.MaxDownloadFileSize, StoreID, PortalID, CultureName));
                PriceUnit = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, StoreID, PortalID, CultureName);
                WeightUnit =ssc.GetStoreSettingsByKey(StoreSetting.WeightUnit, StoreID, PortalID, CultureName);
                DimensionUnit = ssc.GetStoreSettingsByKey(StoreSetting.DimensionUnit, StoreID, PortalID, CultureName);
                LowStockItemRss = ssc.GetStoreSettingsByKey(StoreSetting.LowStockItemRss, StoreID, PortalID, CultureName);
                CurrencyCodeSlected = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, StoreID, PortalID,CultureName);
                AllowOutStockPurchase = ssc.GetStoreSettingsByKey(StoreSetting.AllowOutStockPurchase, StoreID, PortalID, CultureName);
                AllowRealTimeNotifications = ssc.GetStoreSettingsByKey(StoreSetting.AllowRealTimeNotifications, StoreID, PortalID, CultureName);
                if (AllowRealTimeNotifications.ToLower() == "true")
                {
                    IncludeJs("SignalR", false, "/js/SignalR/jquery.signalR-2.2.0.min.js", "/signalr/hubs", "/Modules/AspxCommerce/AspxStartUpEvents/js/RealTimeAspxMgmt.js");
                }
                if(LowStockItemRss.ToLower()=="true")
                {
                   RssFeedUrl = ssc.GetStoreSettingsByKey(StoreSetting.RssFeedURL, StoreID, PortalID, CultureName);
                }
                GetItemTabSetting();
            }
            
            AddLanguage();
            IncludeLanguageJS();
           
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalVariables", " var aspxItemModulePath='" + ResolveUrl(modulePath) + "';", true);
            InitializeJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    private void GetItemTabSetting()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = GetStoreID;
        aspxCommonObj.PortalID = GetPortalID;
        aspxCommonObj.CultureName = GetCurrentCultureName;
        ItemTabSettingInfo lstItemSetting = new ItemTabSettingInfo();
        lstItemSetting = AspxItemMgntController.ItemTabSettingGet(aspxCommonObj);
        if (lstItemSetting == null)
        {
            lstItemSetting = new ItemTabSettingInfo();
        }
        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        if (lstItemSetting != null)
        {
            object obj = new
           {
               EnableCostVariantOption = lstItemSetting.EnableCostVariantOption,
               EnableGroupPrice = lstItemSetting.EnableGroupPrice,
               EnableTierPrice = lstItemSetting.EnableTierPrice,
               EnableRelatedItem = lstItemSetting.EnableRelatedItem,
               EnableCrossSellItem = lstItemSetting.EnableCrossSellItem,
               EnableUpSellItem = lstItemSetting.EnableUpSellItem
           };
            Settings = json_serializer.Serialize(obj);
        }
        else
        {
            object obj = new
            {
                EnableCostVariantOption = lstItemSetting.EnableCostVariantOption,
                EnableGroupPrice = lstItemSetting.EnableGroupPrice,
                EnableTierPrice = lstItemSetting.EnableTierPrice,
                EnableRelatedItem = lstItemSetting.EnableRelatedItem,
                EnableCrossSellItem = lstItemSetting.EnableCrossSellItem,
                EnableUpSellItem = lstItemSetting.EnableUpSellItem
            };
            Settings = json_serializer.Serialize(obj);
        }

    }

    private void AddLanguage()
    {

        List<Language> lstLanguage = LocalizationSqlDataProvider.GetPortalLanguages(GetPortalID);
        List<Language> lstLanguageFlags = LocaleController.AddNativeNamesToList(AddFlagPath(LocalizationSqlDataProvider.GetPortalLanguages(GetPortalID), GetApplicationName));
        if (lstLanguage.Count < 1 || lstLanguageFlags.Count < 1)
        {
            languageSetting.Visible = false;
        }
        else
        {
            var query = from listlang in lstLanguage
                        join listflag in lstLanguageFlags
                             on listlang.LanguageCode equals listflag.LanguageCode
                        select new
                        {
                            listlang.LanguageCode,
                            listflag.FlagPath
                        };
            StringBuilder ddlLanguage = new StringBuilder();
            string cultureCode = GetCurrentCulture();
            ddlLanguage.Append("<ul id=\"languageSelect\" class=\"sfListmenu\"");
            ddlLanguage.Append(">");
            foreach (var item in query)
            {
                if (item.LanguageCode == cultureCode)
                {
                    ddlLanguage.Append("<li value=\"" + item.LanguageCode + "\" class='languageSelected'>");
                    ddlLanguage.Append(item.LanguageCode + "-" + "<img src=\"" + item.FlagPath + "\">");
                    ddlLanguage.Append("</li>");
                }
                else
                {
                    ddlLanguage.Append("<li value=\"" + item.LanguageCode + "\">");
                    ddlLanguage.Append(item.LanguageCode + "-" + "<img src=\"" + item.FlagPath + "\">");
                    ddlLanguage.Append("</li>");
                }
            }
            ddlLanguage.Append("</ul>");
            languageSetting.Text = ddlLanguage.ToString();
        }
    }

    private List<Language> AddFlagPath(List<Language> lstAvailableLocales, string baseURL)
    {
        List<Language> filtered = new List<Language> { };
        string currentCulture = GetCurrentCultureInfo();
        foreach (Language li in lstAvailableLocales)
        {
            //if (li.LanguageCode != currentCulture)
            //{
            filtered.Add(li);
            //}
        }

        filtered.ForEach(
            delegate(Language obj)
            {
                obj.FlagPath = baseURL + "/images/flags/" + obj.LanguageCode.Substring(obj.LanguageCode.IndexOf("-") + 1).ToLower() + ".png";
            }
            );
        return filtered;
    }
    public string GetCurrentCultureInfo()
    {
        if (HttpContext.Current.Session != null)
        {
            string code = HttpContext.Current.Session[SessionKeys.SageUICulture] == null ? "en-US" : HttpContext.Current.Session[SessionKeys.SageUICulture].ToString();
            return code;
        }
        return "en-US";
    }
    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
        Page.ClientScript.RegisterClientScriptInclude("JQueryFormValidate", ResolveUrl("~/js/FormValidation/jquery.form-validation-and-hints.js"));          
        Page.ClientScript.RegisterClientScriptInclude("J10", ResolveUrl("~/js/encoder.js"));
         
      
    }
}
