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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.RewardPoints;
using SageFrame.Framework;
using SageFrame.Web;
using System.Collections;
using System.Web.Script.Serialization;
using SageFrame.Security;
using SageFrame.Security.Entities;
using SageFrame.Security.Helpers;
using System.Web.Security;
using System.IO;
using SageFrame.RolesManagement;
using SageFrame.Web.Utilities;
using SageFrame.Security.Crypto;
using SageFrame.Shared;
using AspxCommerce.Core;
using SageFrame.Core;
using SageFrame.Common;
using System.Text.RegularExpressions;



public partial class Modules_AspxCheckoutInformationContent_CheckoutInformationContent : BaseAdministrationUserControl
{
    private int storeID, portalID, customerID;
    private string userName;
    private string cultureName;
    private string sessionCode = string.Empty;
    private string strRoles = string.Empty;
    private bool RegisterURL = true;
    public string RewardSettings = string.Empty;
    private decimal Discount = 0;

    private string ShippingDetailPage, ShowSubscription, noImageCheckOutInfoPath, myAccountURL,
        SingleAddressCheckOutURL, DimentionalUnit, WeightUnit, ShoppingCartURL = string.Empty;
    private string AllowededShippingCountry = string.Empty;
    private string AllowededBillingCountry = string.Empty;
    public string MainCurrency;
    public int cartCount;
    private Random random = new Random();
    private string allowMultipleAddress = string.Empty;
    SageFrameConfig pagebase = new SageFrameConfig();
    StoreSettingConfig ssc = new StoreSettingConfig();
    AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
    private int CountDownloadableItem = 0, CountAllItem = 0;

    public string Coupon = "", Items = "", ServerVars = "", GiftCard = "", ScriptsToRun = "";
    JavaScriptSerializer json_serializer = new JavaScriptSerializer();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            IncludeLanguageJS();

            GetPortalCommonInfo(out storeID, out portalID, out customerID, out userName, out cultureName, out sessionCode);
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo(storeID, portalID, userName, cultureName, customerID, sessionCode);

            List<CouponSession> cs = new List<CouponSession>();
            cs = CheckOutSessions.Get<List<CouponSession>>("CouponSession");
            Coupon = json_serializer.Serialize(cs);

            List<GiftCardUsage> gc = CheckOutSessions.Get<List<GiftCardUsage>>("UsedGiftCard");
            GiftCard = json_serializer.Serialize(gc);

            Discount = CheckOutSessions.Get<Decimal>("DiscountAmount", 0);

            string templateName = TemplateName;
            IncludeCss("CheckOutInformationContent", "/Templates/" + templateName + "/css/MessageBox/style.css", "/Templates/" + templateName + "/css/JQueryUIFront/jquery.ui.all.css",
                "/Templates/" + templateName + "/css/ToolTip/tooltip.css", "/Modules/AspxCommerce/AspxCheckoutWithSingleAddress/css/module.css");
            IncludeJs("CheckOutInformationContent", "/js/encoder.js", "/js/FormValidation/jquery.validate.js", "/js/jquery.cookie.js", "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js", "/js/jquery.tipsy.js", "/Modules/AspxCommerce/AspxCheckoutWithSingleAddress/js/SingleCheckOut.js");

            ssc.GetStoreSettingParamEleven(StoreSetting.DefaultProductImageURL, StoreSetting.ShoppingCartURL, StoreSetting.MyAccountURL,
                StoreSetting.AllowedShippingCountry, StoreSetting.AllowedBillingCountry, StoreSetting.SingleCheckOutURL,
                StoreSetting.DimensionUnit, StoreSetting.WeightUnit, StoreSetting.AskCustomerToSubscribe, StoreSetting.ShipDetailPageURL,
                StoreSetting.AllowUsersToCreateMultipleAddress, out noImageCheckOutInfoPath, out ShoppingCartURL, out myAccountURL,
                out AllowededShippingCountry, out AllowededBillingCountry, out SingleAddressCheckOutURL, out DimentionalUnit,
                out WeightUnit, out ShowSubscription, out ShippingDetailPage, out allowMultipleAddress, storeID, portalID, cultureName);

            if (!IsPostBack)
            {

                HideSignUp();
                PasswordAspx.Attributes.Add("onkeypress", "return clickButton(event,'" + LoginButton.ClientID + "')");

                if (!IsParent)
                {
                    hypForgotPassword.NavigateUrl =
                        ResolveUrl(GetParentURL + "/portal/" + GetPortalSEOName + "/sf/" +
                                   pagebase.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalForgotPassword) + SageFrameSettingKeys.PageExtension);
                }
                else
                {
                    hypForgotPassword.NavigateUrl =
                        ResolveUrl("~/sf/" + pagebase.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalForgotPassword) +
                                  SageFrameSettingKeys.PageExtension);
                }
                string registerUrl =
                    ResolveUrl("~/sf/" + pagebase.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalUserRegistration) +
                               SageFrameSettingKeys.PageExtension);
                signup.Attributes.Add("href", ResolveUrl("~/sf/sfUser-Registration" + SageFrameSettingKeys.PageExtension));
                signup1.Attributes.Add("href", ResolveUrl("~/sf/sfUser-Registration" + SageFrameSettingKeys.PageExtension));

                if (Boolean.Parse(pagebase.GetSettingValueByIndividualKey(SageFrameSettingKeys.RememberCheckbox)))
                {
                    RememberMe.Visible = true;
                    lblrmnt.Visible = true;
                }
                else
                {
                    RememberMe.Visible = false;
                    lblrmnt.Visible = false;
                }

                object serverVars = new
                {
                    noImageCheckOutInfoPath = noImageCheckOutInfoPath,
                    ShoppingCartURL = ShoppingCartURL,
                    myAccountURL = myAccountURL,
                    singleAddressCheckOutURL = SingleAddressCheckOutURL,
                    CartUrl = ShoppingCartURL,
                    AllowedShippingCountry = AllowededShippingCountry,
                    AllowedBillingCountry = AllowededBillingCountry,
                    dimentionalUnit = DimentionalUnit,
                    weightunit = WeightUnit,
                    showSubscription = ShowSubscription,
                    allowMultipleAddress = allowMultipleAddress,
                    shippingDetailPage = ShippingDetailPage,
                    Discount = Discount

                };

                ServerVars = json_serializer.Serialize(serverVars);
                LoadCartDetails(aspxCommonObj);
                LoadCountry();
                LoadAddress(aspxCommonObj);
                LoadPaymentGateway(aspxCommonObj);
                LoadRewardPoints(aspxCommonObj);
            }


            if (HttpContext.Current.User != null)
            {
                SecurityPolicy objSecurity = new SecurityPolicy();
                FormsAuthenticationTicket ticket = objSecurity.GetUserTicket(GetPortalID);
                if (ticket != null && ticket.Name != ApplicationKeys.anonymousUser)
                {
                    int LoggedInPortalID = int.Parse(ticket.UserData.ToString());
                    string[] sysRoles = SystemSetting.SUPER_ROLE;
                    MembershipController member = new MembershipController();
                    UserInfo userDetail = member.GetUserDetails(GetPortalID, GetUsername);
                    if (GetPortalID == LoggedInPortalID || Roles.IsUserInRole(userDetail.UserName, sysRoles[0]))
                    {
                        RoleController _role = new RoleController();
                        string userinroles = _role.GetRoleNames(GetUsername, LoggedInPortalID);
                        if (userinroles != "" || userinroles != null)
                        {
                            MultiView1.ActiveViewIndex = 1;
                        }
                        else
                        {
                            MultiView1.ActiveViewIndex = 0;
                        }
                    }
                    else
                    {
                        MultiView1.ActiveViewIndex = 0;
                    }
                }
                else
                {
                    MultiView1.ActiveViewIndex = 0;
                }
            }

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

    }

    private void LoadAddress(AspxCommonInfo aspxCommonObj)
    {
        StringBuilder addressBuilder = new StringBuilder();
        addressBuilder.Append("<div id=\"ddlBilling\" class=\"clearfix\">");
        StringBuilder addressBuilderShip = new StringBuilder();
        addressBuilderShip.Append("<div id=\"ddlShipping\" class=\"clearfix\">");
        StringBuilder addressScript = new StringBuilder();
        List<AddressInfo> lstAddress = AspxUserDashController.GetUserAddressDetails(aspxCommonObj);
        addressBuilder.Append("");
        string tempAddress = "";
        tempAddress += "[";
        if (lstAddress.Count > 0)
        {
            foreach (AddressInfo item in lstAddress)
            {


                string add = "{" +
                             string.Format(
                                 "Address1:\\'{0}\\',Address2:\\'{1}\\',AddressID:\\'{2}\\',City:\\'{3}\\',Company:\\'{4}\\',Country:\\'{5}\\' ,{6} DefaultBilling:\\'{7}\\',DefaultShipping:\\'{8}\\',Email:\\'{9}\\',Fax:\\'{10}\\',FirstName:\\'{11}\\',LastName:\\'{12}\\',Mobile:\\'{13}\\',Phone:\\'{14}\\',State:\\'{15}\\',Website:\\'{16}\\',Zip:\\'{17}\\'",
                                 item.Address1, item.Address2,
                                 item.AddressID, item.City, item.Company, item.Country, "",
                                 item.DefaultBilling.ToString().ToLower(),
                                 item.DefaultShipping.ToString().ToLower(), item.Email, item.Fax, item.FirstName,
                                 item.LastName,
                                 item.Mobile, item.Phone, item.State, item.Website, item.Zip)
                             + "},";
                tempAddress += add;
                if (item.DefaultBilling != null && bool.Parse(item.DefaultBilling.ToString()))
                {
                    addressBuilder.Append("<div><label><input type=\"radio\" name=\"billing\" value=\"");
                    addressBuilder.Append(item.AddressID);
                    addressBuilder.Append("\" checked=\"checked\" class=\"cssBillingShipping\" />");
                }
                else
                {
                    addressBuilder.Append("<div><label><input type=\"radio\" name=\"billing\" value=\"");
                    addressBuilder.Append(item.AddressID);
                    addressBuilder.Append("\" class=\"cssBillingShipping\" />");
                }
                addressBuilder.Append(item.FirstName.Replace(",", "-"));
                addressBuilder.Append(" ");
                addressBuilder.Append(item.LastName.Replace(",", "-"));

                if (item.Address1 != string.Empty)
                {
                    addressBuilder.Append(", ");
                    addressBuilder.Append(item.Address1.Replace(",", "-"));
                }

                if (item.City != string.Empty)
                {
                    addressBuilder.Append(", ");
                    addressBuilder.Append(item.City.Replace(",", "-"));
                }

                if (item.State != string.Empty)
                {
                    addressBuilder.Append(", ");
                    addressBuilder.Append(item.State.Replace(",", "-"));
                }

                if (item.Country != string.Empty)
                {
                    addressBuilder.Append(", ");
                    addressBuilder.Append(item.Country.Replace(",", "-"));
                }

                if (item.Zip != string.Empty)
                {
                    addressBuilder.Append(", ");
                    addressBuilder.Append(item.Zip.Replace(",", "-"));
                }

                if (item.Email != string.Empty)
                {
                    addressBuilder.Append(", ");
                    addressBuilder.Append(item.Email.Replace(",", "-"));
                }

                if (item.Phone != string.Empty)
                {
                    addressBuilder.Append(", ");
                    addressBuilder.Append(item.Phone.Replace(",", "-"));
                }

                if (item.Mobile != string.Empty)
                {
                    addressBuilder.Append(", ");
                    addressBuilder.Append(item.Mobile.Replace(",", "-"));
                }

                if (item.Fax != string.Empty)
                {
                    addressBuilder.Append(", ");
                    addressBuilder.Append(item.Fax.Replace(",", "-"));
                }

                if (item.Website != string.Empty)
                {
                    addressBuilder.Append(", ");
                    addressBuilder.Append(item.Website.Replace(",", "-"));
                }

                if (item.Address2 != string.Empty)
                {
                    addressBuilder.Append(", ");
                    addressBuilder.Append(item.Address2.Replace(",", "-"));
                }

                if (item.Company != string.Empty)
                {
                    addressBuilder.Append(", ");
                    addressBuilder.Append(item.Company.Replace(",", "-"));
                   
                }
                addressBuilder.Append("</label></div>");

                if (item.DefaultShipping != null && bool.Parse(item.DefaultShipping.ToString()))
                {
                    addressBuilderShip.Append("<div><label><input type=\"radio\" name=\"shipping\" value=\"");
                    addressBuilderShip.Append(item.AddressID);
                    addressBuilderShip.Append("\" checked=\"checked\" class=\"cssBillingShipping\" />");
                }
                else
                {
                    addressBuilderShip.Append("<div><label><input type=\"radio\" name=\"shipping\" value=\"");
                    addressBuilderShip.Append(item.AddressID);
                    addressBuilderShip.Append("\" class=\"cssBillingShipping\" />");

                }
                addressBuilderShip.Append(item.FirstName.Replace(",", "-"));
                addressBuilderShip.Append(" ");
                addressBuilderShip.Append(item.LastName.Replace(",", "-"));

                if (item.Address1 != string.Empty)
                {
                    addressBuilderShip.Append(", ");
                    addressBuilderShip.Append(item.Address1.Replace(",", "-"));
                }

                if (item.City != string.Empty)
                {
                    addressBuilderShip.Append(", ");
                    addressBuilderShip.Append(item.City.Replace(",", "-"));
                }

                if (item.State != string.Empty)
                {
                    addressBuilderShip.Append(", ");
                    addressBuilderShip.Append(item.State.Replace(",", "-"));
                }

                if (item.Country != string.Empty)
                {
                    addressBuilderShip.Append(", ");
                    addressBuilderShip.Append(item.Country.Replace(",", "-"));
                }

                if (item.Zip != string.Empty)
                {
                    addressBuilderShip.Append(", ");
                    addressBuilderShip.Append(item.Zip.Replace(",", "-"));
                }

                if (item.Email != string.Empty)
                {
                    addressBuilderShip.Append(", ");
                    addressBuilderShip.Append(item.Email.Replace(",", "-"));
                }

                if (item.Phone != string.Empty)
                {
                    addressBuilderShip.Append(", ");
                    addressBuilderShip.Append(item.Phone.Replace(",", "-"));
                }

                if (item.Mobile != string.Empty)
                {
                    addressBuilderShip.Append(", ");
                    addressBuilderShip.Append(item.Mobile.Replace(",", "-"));
                }

                if (item.Fax != string.Empty)
                {
                    addressBuilderShip.Append(", ");
                    addressBuilderShip.Append(item.Fax.Replace(",", "-"));
                }

                if (item.Website != string.Empty)
                {
                    addressBuilderShip.Append(", ");
                    addressBuilderShip.Append(item.Website.Replace(",", "-"));
                }

                if (item.Address2 != string.Empty)
                {
                    addressBuilderShip.Append(", ");
                    addressBuilderShip.Append(item.Address2.Replace(",", "-"));
                }

                if (item.Company != string.Empty)
                {
                    addressBuilderShip.Append(", ");
                    addressBuilderShip.Append(item.Company.Replace(",", "-"));
                    
                }
                addressBuilderShip.Append("</label></div>");

            }
            addressBuilderShip.Append("</div>");
            addressBuilder.Append("</div>");
            tempAddress = tempAddress.Substring(0, tempAddress.Length - 1);
            tempAddress += "]";
            string script = string.Empty;
            if (CountDownloadableItem == CountAllItem)
            {



                ScriptsToRun += addressScript.Append("CheckOut.SetTempAddr(eval(\"" + tempAddress +
                                      "\"));$(\"#dvBilling .cssClassCheckBox\").hide();$(\"#dvCPShipping\").parent(\"div\").hide();$(\"#dvCPShippingMethod\").parent(\"div\").hide();$(\"#addBillingAddress\").show(); $(\"#addShippingAddress\").show();")
                     .ToString();
            }
            else
            {



                ScriptsToRun += addressScript.Append("CheckOut.SetTempAddr(eval(\"" + tempAddress +
                                        "\"));$(\"#dvBilling .cssClassCheckBox\").show(); $(\"#dvCPShipping\").parent(\"div\").show();$(\"#dvCPShippingMethod\").parent(\"div\").show(); $(\"#addShippingAddress\").show();")
                       .ToString();
            }

            ltddlBilling.Text = addressBuilder.ToString();
            ltddlShipping.Text = addressBuilderShip.ToString();


        }
        else
        {
            addressBuilderShip.Append("</div>");
            addressBuilder.Append("</div>");
            ltddlBilling.Text = addressBuilder.ToString();
            ltddlShipping.Text = addressBuilderShip.ToString();
        }
    }

    private void LoadCountry()
    {

        StringBuilder blCountry = new StringBuilder();
        StringBuilder spCountry = new StringBuilder();
        StringBuilder optionCountry = new StringBuilder();
        List<CountryInfo> lstCountry = AspxCommonController.BindCountryList();
        blCountry.Append("<select id=\"ddlBLCountry\">");
        spCountry.Append("<select id=\"ddlSPCountry\">");
        foreach (var countryInfo in lstCountry)
        {
            optionCountry.Append("<option class=\"cssBillingShipping\" value=\"");
            optionCountry.Append(countryInfo.Value);
            optionCountry.Append("\"> ");
            optionCountry.Append(countryInfo.Text);
            optionCountry.Append("</option>");

        }
        blCountry.Append(optionCountry);
        spCountry.Append(optionCountry);
        blCountry.Append("</select>");
        spCountry.Append("</select>");

        ltSPCountry.Text = spCountry.ToString();
        ltBLCountry.Text = blCountry.ToString();
    }

    private void LoadCartDetails(AspxCommonInfo aspxCommonObj)
    {
        string resolvedUrl = ResolveUrl("~/");
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        hst = AppLocalized.getLocale(modulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        string aspxTemplateFolderPath = resolvedUrl + "Templates/" + TemplateName;
        string aspxRootPath = resolvedUrl;


        StringBuilder cartDetails = new StringBuilder();
        StringBuilder scriptBuilder = new StringBuilder();

        List<CartInfo> lstCart = AspxCartController.GetCartCheckOutDetails(aspxCommonObj);
        cartCount = lstCart.Count;
        lstCart = lstCart.Select(e => { e.KitData = Regex.Replace(e.KitData, "[\"\"]+", "'"); return e; }).ToList();
        Items = json_serializer.Serialize(lstCart);
        cartDetails.Append("<table class=\"sfGridTableWrapper\" width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" id=\"tblCartList\">");
        cartDetails.Append("<thead><tr class=\"cssClassHeadeTitle\">");
        cartDetails.Append("<th class=\"cssClassSN\"> Sn.");
        cartDetails.Append(" </th><th class=\"cssClassProductImageWidth\">");
        cartDetails.Append(getLocale("Item Image"));
        cartDetails.Append("</th>");
        cartDetails.Append("<th>");
        cartDetails.Append(getLocale("Variants"));
        cartDetails.Append("</th>");
        cartDetails.Append("<th class=\"cssClassQTY\">");
        cartDetails.Append(getLocale("Qty"));
        cartDetails.Append("</th>");
        cartDetails.Append("<th class=\"cssClassProductPrice\">");
        cartDetails.Append(getLocale("Unit Price"));
        cartDetails.Append("</th>");
        cartDetails.Append("<th class=\"cssClassSubTotal\">");
        cartDetails.Append(getLocale("Line Total"));
        cartDetails.Append("</th>");
        cartDetails.Append("<th class=\"cssClassTaxRate\">");
        cartDetails.Append(getLocale("Unit Tax"));
        cartDetails.Append("</th>");
        cartDetails.Append("</tr>");
        cartDetails.Append("</thead");
        cartDetails.Append("<tbody>");

        int giftcardCount = 0;
        int index = 0;
        string itemids = "";
        bool IsDownloadItemInCart = false, ShowShippingAdd = false;
        int CartID = 0;//int CountDownloadableItem = 0, CountAllItem = 0, 
        string bsketItems = "";
        bsketItems += "[";
        foreach (CartInfo item in lstCart)
        {
            if (item.ItemTypeID == 1 || item.ItemTypeID == 6)
            {
                string bitems = "{" +
                             string.Format(
                                 "Height:{0},ItemName:\\'{1}\\',Length:{2},Quantity:{3},WeightValue:{4},Width:{5}",
                                 item.Height ?? 0, HttpUtility.HtmlEncode(item.ItemName),
                                 item.Length ?? 0, item.Quantity.ToString(), decimal.Parse(item.Weight.ToString()), item.Width ?? 0
                                 )

                             + "},";
                bsketItems += bitems;

            }


            itemids += item.ItemID + "#" + item.CostVariantsValueIDs + ",";

            index++;
            string imagePath = "Modules/AspxCommerce/AspxItemsManagement/uploads/" + item.ImagePath;
            if (item.ImagePath == "")
            {
                imagePath = noImageCheckOutInfoPath;
            }
            else if (item.AlternateText == string.Empty)
            {
                item.AlternateText = item.ItemName;
            }
            if (item.ItemTypeID == 2)
            {
                IsDownloadItemInCart = true;
                CountDownloadableItem++;
            }
            var isVirtual = false;
            if (item.ItemTypeID == 3)
            {

                int typ = AspxGiftCardController.GetGiftCardType(aspxCommonObj, item.CartItemID);

                if (typ > 0)
                {
                    if (typ == 2)
                    {
                        ShowShippingAdd = false;
                        isVirtual = false;
                    }
                    else
                    {
                        ShowShippingAdd = true;
                        isVirtual = true;
                    }

                }

                giftcardCount++;
                if (lstCart.Count != giftcardCount)
                {
                    ShowShippingAdd = false;
                }
            }

            CountAllItem++;
            cartDetails.Append("<tr >");
            cartDetails.Append("<td><input type=\"hidden\" name=\"cartItemId\" value=\"");
            cartDetails.Append(item.CartItemID);
            cartDetails.Append("\" />");
            cartDetails.Append("<b>");
            cartDetails.Append(index);
            cartDetails.Append(".</b>");
            cartDetails.Append("</td>");
            cartDetails.Append("<td>");
            cartDetails.Append("<p class=\"cssClassCartPicture\">");
            cartDetails.Append("<img title=\"");
            cartDetails.Append(item.AlternateText);
            cartDetails.Append("\" src=\"");
            cartDetails.Append(aspxRedirectPath);
            cartDetails.Append(imagePath.Replace("uploads", "uploads/Small"));
            cartDetails.Append("\" ></p>");
            cartDetails.Append("<div class=\"cssClassCartPictureInformation\">");
            cartDetails.Append("<h3>");
            if (item.CostVariantsValueIDs != string.Empty)
            {
                cartDetails.Append("<a class=\"cssClassLink\" id=\"item_");
                cartDetails.Append(item.ItemID);
                cartDetails.Append(" itemType=\"");
                cartDetails.Append(item.ItemTypeID);
                cartDetails.Append("\"  href=\"");
                cartDetails.Append(aspxRedirectPath);
                cartDetails.Append("item/");
                cartDetails.Append(item.SKU);
                cartDetails.Append(pageExtension);
                cartDetails.Append("?varId=");
                cartDetails.Append(item.CostVariantsValueIDs);
                cartDetails.Append("\">");
                cartDetails.Append(item.ItemName);
                cartDetails.Append("\" </a></h3>");

            }
            else
            {

                if (item.ItemTypeID == 3)
                {
                    cartDetails.Append("<a class=\"cssClassLink\" id=\"item_");
                    cartDetails.Append(item.ItemID);
                    cartDetails.Append("_");
                    cartDetails.Append(index);
                    cartDetails.Append("\" isvirtual=\"");

                    cartDetails.Append(isVirtual);
                    cartDetails.Append("\" itemType=\"");
                    cartDetails.Append(item.ItemTypeID);
                    cartDetails.Append("\"  href=\"");
                    cartDetails.Append(aspxRedirectPath);
                    cartDetails.Append("item/");
                    cartDetails.Append(item.SKU);
                    cartDetails.Append(pageExtension);
                    cartDetails.Append("\">");
                    cartDetails.Append(item.ItemName);
                    cartDetails.Append("</a></h3>");
                }
                else
                {
                    cartDetails.Append("<a class=\"cssClassLink\" id=\"item_");
                    cartDetails.Append(item.ItemID);
                    cartDetails.Append("_");
                    cartDetails.Append(index);
                    cartDetails.Append("\"  itemType=\"");

                    cartDetails.Append(item.ItemTypeID);
                    cartDetails.Append("\"  href=\"");
                    cartDetails.Append(aspxRedirectPath);
                    cartDetails.Append("item/");
                    cartDetails.Append(item.SKU);
                    cartDetails.Append(pageExtension);
                    cartDetails.Append("\">");
                    cartDetails.Append(item.ItemName);
                    cartDetails.Append("</a></h3>");

                }
            }
            cartDetails.Append("</div>");
            cartDetails.Append("</td>");
            cartDetails.Append("<td class=\"row-variants\" varIDs=\"");
            cartDetails.Append(item.CostVariantsValueIDs);
            cartDetails.Append("\">");
            cartDetails.Append(item.CostVariants);
            cartDetails.Append("</td>");
            cartDetails.Append("<td class=\"cssClassPreviewQTY\">");
            cartDetails.Append("<input class=\"num-pallets-input\" taxrate=\"0\" price=\"");
            cartDetails.Append(item.Price);
            cartDetails.Append("\" id=\"txtQuantity_");
            cartDetails.Append(item.CartID);
            cartDetails.Append("\" type=\"text\" readonly=\"readonly\" disabled=\"disabled\" value=\"");
            cartDetails.Append(item.Quantity);
            cartDetails.Append("\">");
            cartDetails.Append("</td>");
            cartDetails.Append("<td class=\"price-per-pallet\">");
            cartDetails.Append("<span id=\"");
            cartDetails.Append(item.Weight);
            cartDetails.Append("\" class=\"cssClassFormatCurrency\">");
            cartDetails.Append(Convert.ToDecimal(item.Price).ToString("N2") + "</span>");
            cartDetails.Append("</td>");
            cartDetails.Append("<td class=\"row-total\">");
            cartDetails.Append("<input class=\"row-total-input cssClassFormatCurrency\" id=\"txtRowTotal_");
            cartDetails.Append(item.CartID);
            cartDetails.Append("\"  value=\"");
            cartDetails.Append(Convert.ToDecimal(item.TotalItemCost).ToString("N2"));
            cartDetails.Append("\" baseValue=\"");
            cartDetails.Append(Convert.ToDecimal(item.TotalItemCost).ToString("N2"));
            cartDetails.Append("\"  readonly=\"readonly\" type=\"text\" />");
            cartDetails.Append("</td>");
            cartDetails.Append("<td class=\"row-taxRate\">");
            cartDetails.Append("<span class=\"cssClassFormatCurrency\">0.00</span>");
            cartDetails.Append("</td>");
            cartDetails.Append("</tr>");
            CartID = item.CartID;
        }
        cartDetails.Append("</table>");
        if (bsketItems.Length > 1)
            bsketItems = bsketItems.Substring(0, bsketItems.Length - 1);
        bsketItems += "]";
        scriptBuilder.Append("  CheckOut.SetBasketItems(eval(\"");
        scriptBuilder.Append(bsketItems);
        scriptBuilder.Append("\")); CheckOut.Vars.ItemIDs=\"");
        scriptBuilder.Append(itemids);
        scriptBuilder.Append("\";");
        scriptBuilder.Append("CheckOut.UserCart.CartID=");
        scriptBuilder.Append(CartID);
        scriptBuilder.Append(";");
        scriptBuilder.Append(" CheckOut.UserCart.ShowShippingAdd=");
        scriptBuilder.Append(ShowShippingAdd.ToString().ToLower());
        scriptBuilder.Append(";");
        scriptBuilder.Append(" CheckOut.UserCart.IsDownloadItemInCart=");
        scriptBuilder.Append(IsDownloadItemInCart.ToString().ToLower());
        scriptBuilder.Append(";");
        scriptBuilder.Append(" CheckOut.UserCart.CountDownloadableItem=");
        scriptBuilder.Append(CountDownloadableItem);
        scriptBuilder.Append(";");
        scriptBuilder.Append(" CheckOut.UserCart.CountAllItem=");
        scriptBuilder.Append(CountAllItem);
        scriptBuilder.Append("; ");//CheckOut.BindFunction();
        scriptBuilder.Append("$(\"#tblCartList tr:even\").addClass(\"sfEven\");$(\"#tblCartList tr:odd\").addClass(\"sfOdd\");");
        ScriptsToRun += scriptBuilder.ToString();
        ltTblCart.Text = cartDetails.ToString();

    }

    private void LoadRewardPoints(AspxCommonInfo aspxCommonObj)
    {
        List<GeneralSettingInfo> lstGeneralSet = RewardPointsController.GetGeneralSetting(aspxCommonObj);

        StringBuilder scriptrewardPoint = new StringBuilder();

        if (lstGeneralSet.Count > 0)
        {
            RewardSettings = new JavaScriptSerializer().Serialize(lstGeneralSet.FirstOrDefault()).ToString();
        }

    }

    private void LoadPaymentGateway(AspxCommonInfo aspxCommonObj)
    {
        string aspxRootPath = ResolveUrl("~/");
        List<PaymentGatewayListInfo> pginfo = AspxCartController.GetPGList(aspxCommonObj);

        StringBuilder paymentGateWay = new StringBuilder();
        foreach (var item in pginfo)
        {
            if (item.LogoUrl != string.Empty)
            {
                paymentGateWay.Append("<label><input id=\"rdb");

                paymentGateWay.Append(item.PaymentGatewayTypeName);
                paymentGateWay.Append("\" name=\"PGLIST\" type=\"radio\" realname=\"");
                paymentGateWay.Append(item.PaymentGatewayTypeName);
                paymentGateWay.Append("\" friendlyname=\"");
                paymentGateWay.Append(item.FriendlyName);
                paymentGateWay.Append("\"  source=\"");

                paymentGateWay.Append(item.ControlSource);
                paymentGateWay.Append("\" value=\"");
                paymentGateWay.Append(item.PaymentGatewayTypeID);
                paymentGateWay.Append("\" class=\"cssClassRadioBtn\" /><img class=\"cssClassImgPGList\" src=\"");
                paymentGateWay.Append(aspxRootPath);
                paymentGateWay.Append(item.LogoUrl);
                paymentGateWay.Append("\" alt=\"");
                paymentGateWay.Append(item.PaymentGatewayTypeName);
                paymentGateWay.Append("\" title=\"");

                paymentGateWay.Append(item.PaymentGatewayTypeName);
                paymentGateWay.Append("\" visible=\"true\" /></label>");

            }
            else
            {
                paymentGateWay.Append("<label><input id=\"rdb");
                paymentGateWay.Append(item.PaymentGatewayTypeName);
                paymentGateWay.Append("\" name=\"PGLIST\" type=\"radio\" realname=\"");
                paymentGateWay.Append(item.PaymentGatewayTypeName);
                paymentGateWay.Append("\" friendlyname=\"");
                paymentGateWay.Append(item.FriendlyName);
                paymentGateWay.Append("\"  source=\"");
                paymentGateWay.Append(item.ControlSource);

                paymentGateWay.Append("\" value=\"");
                paymentGateWay.Append(item.PaymentGatewayTypeID);
                paymentGateWay.Append("\" class=\"cssClassRadioBtn\" /><b>");

                paymentGateWay.Append(item.PaymentGatewayTypeName);
                paymentGateWay.Append("</b></label>");

            }

        }
        ScriptsToRun += "CheckOut.BindPGFunction();";
        ltPgList.Text = paymentGateWay.ToString();
    }

    Hashtable hst = null;
    private string getLocale(string messageKey)
    {
        string retStr = messageKey;
        if (hst != null && hst[messageKey] != null)
        {
            retStr = hst[messageKey].ToString();
        }
        return retStr;
    }




    #region ServerSide Mthods
    private void HideSignUp()
    {
        int UserRegistrationType = Int32.Parse(pagebase.GetSettingValueByIndividualKey(SageFrameSettingKeys.PortalUserRegistration));
        RegisterURL = UserRegistrationType > 0 ? true : false;
        if (!RegisterURL)
        {
            this.divSignUp.Visible = false;
        }
    }


    public void SetUserRoles(string strRoles)
    {
        Session[SessionKeys.SageUserRoles] = strRoles;
        HttpCookie cookie = HttpContext.Current.Request.Cookies[CookiesKeys.SageUserRolesCookie];
        if (cookie == null)
        {
            cookie = new HttpCookie(CookiesKeys.SageUserRolesCookie);
        }
        cookie[CookiesKeys.SageUserRolesCookie] = strRoles;
        HttpContext.Current.Response.Cookies.Add(cookie);
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        MembershipController member = new MembershipController();
        RoleController role = new RoleController();
        UserInfo user = member.GetUserDetails(GetPortalID, UserName.Text);
        if (user.UserExists && user.IsApproved)
        {
            if (!(string.IsNullOrEmpty(UserName.Text) && string.IsNullOrEmpty(PasswordAspx.Text)))
            {
                if (PasswordHelper.ValidateUser(user.PasswordFormat, PasswordAspx.Text, user.Password, user.PasswordSalt))
                {
                    string userRoles = role.GetRoleNames(user.UserName, GetPortalID);
                    strRoles += userRoles;
                    if (strRoles.Length > 0)
                    {
                        SetUserRoles(strRoles);
                        //SessionTracker sessionTracker = (SessionTracker)Session[SessionKeys.Tracker];
                        //SessionTracker sessionTracker = (SessionTracker)Session[SessionKeys.Tracker];
                        //sessionTracker.PortalID = GetPortalID.ToString();
                        //sessionTracker.Username = UserName.Text;
                        //Session[SessionKeys.Tracker] = sessionTracker;
                        SageFrame.Web.SessionLog SLog = new SageFrame.Web.SessionLog();
                        SLog.SessionTrackerUpdateUsername(UserName.Text, GetPortalID.ToString());

                        StringBuilder redirectURL = new StringBuilder();
                        SecurityPolicy objSecurity = new SecurityPolicy();
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                          user.UserName,
                          DateTime.Now,
                          DateTime.Now.AddMinutes(30),
                          true,
                          GetPortalID.ToString(),
                          FormsAuthentication.FormsCookiePath);

                        string encTicket = FormsAuthentication.Encrypt(ticket);

                        string randomCookieValue = GenerateRandomCookieValue();
                        Session[SessionKeys.RandomCookieValue] = randomCookieValue;
                        HttpCookie cookie = new HttpCookie(objSecurity.FormsCookieName(GetPortalID), encTicket);
                        SageFrameConfig objConfig = new SageFrameConfig();
                        string ServerCookieExpiration = objConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.ServerCookieExpiration);
                        int expiryTime = Math.Abs(int.Parse(ServerCookieExpiration));
                        expiryTime = expiryTime < 5 ? 5 : expiryTime;
                        cookie.Expires = DateTime.Now.AddMinutes(expiryTime);
                        Response.Cookies.Add(cookie);
                        ServiceSecurity.IssueToken(GetPortalID);
                        if (Request.QueryString["ReturnUrl"] != null)
                        {
                            string PageNotFoundPage = PortalAPI.PageNotFoundURLWithRoot;
                            string UserRegistrationPage = PortalAPI.RegistrationURLWithRoot;
                            string PasswordRecoveryPage = PortalAPI.PasswordRecoveryURLWithRoot;
                            string ForgotPasswordPage = PortalAPI.ForgotPasswordURL;
                            string PageNotAccessiblePage = PortalAPI.PageNotAccessibleURLWithRoot;

                            string ReturnUrlPage = Request.QueryString["ReturnUrl"].Replace("%2f", "-").ToString();

                            if (ReturnUrlPage == PageNotFoundPage || ReturnUrlPage == UserRegistrationPage || ReturnUrlPage == PasswordRecoveryPage || ReturnUrlPage == ForgotPasswordPage || ReturnUrlPage == PageNotAccessiblePage)
                            {
                                redirectURL.Append(GetParentURL);
                                redirectURL.Append(PortalAPI.DefaultPageWithExtension);
                            }
                            else
                            {
                                redirectURL.Append(ResolveUrl(Request.QueryString["ReturnUrl"].ToString()));
                            }
                        }
                        else
                        {




                            if (!IsParent)
                            {
                                redirectURL.Append(GetParentURL);
                                redirectURL.Append("/portal/");
                                redirectURL.Append(GetPortalSEOName);
                                redirectURL.Append("/");
                                redirectURL.Append(ssc.GetStoreSettingsByKey(StoreSetting.SingleCheckOutURL, GetStoreID, GetPortalID, GetCurrentCultureName));
                                redirectURL.Append(SageFrameSettingKeys.PageExtension);
                            }
                            else
                            {
                                redirectURL.Append(GetParentURL);
                                redirectURL.Append("/");
                                redirectURL.Append(ssc.GetStoreSettingsByKey(StoreSetting.SingleCheckOutURL, GetStoreID, GetPortalID, GetCurrentCultureName));
                                redirectURL.Append(SageFrameSettingKeys.PageExtension);
                            }

                        }

                        int customerID = GetCustomerID;
                        if (customerID == 0)
                        {
                            CustomerGeneralInfo sageUserCust = CustomerGeneralInfoController.CustomerIDGetByUsername(user.UserName, storeID, portalID);
                            if (sageUserCust != null)
                            {
                                customerID = sageUserCust.CustomerID;
                            }
                        }
                        AspxCommonController objCommonCont = new AspxCommonController();
                        objCommonCont.UpdateCartAnonymoususertoRegistered(storeID, portalID, customerID, sessionCode);
                        Response.Redirect(redirectURL.ToString(), false);
                    }
                    else
                    {
                        FailureText.Text = string.Format("<p class='sfError'>{0}</p>", GetSageMessage("UserLogin", "Youarenotauthenticatedtothisportal"));
                    }
                }
                else
                {
                    FailureText.Text = string.Format("<p class='sfError'>{0}</p>", GetSageMessage("UserLogin", "UsernameandPasswordcombinationdoesntmatched"));//"Username and Password combination doesn't matched!";
                }
            }
        }
        else
        {
            FailureText.Text = string.Format("<p class='sfError'>{0}</p>", GetSageMessage("UserLogin", "UserDoesnotExist"));
        }
    }
    private string GenerateRandomCookieValue()
    {
        string s = "";
        string[] CapchaValue = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        for (int i = 0; i < 10; i++)
            s = String.Concat(s, CapchaValue[this.random.Next(36)]);
        return s;
    }

    #endregion
}