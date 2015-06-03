using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using SageFrame.Security;
using SageFrame.Security.Entities;
using SageFrame.Web;
using System.Collections;
using SageFrame.Framework;
using System.Web.Security;
using SageFrame;
using SageFrame.Web.Utilities;
using SageFrame.Web.Common.SEO;
using AspxCommerce.Core;
using System.Web.Script.Serialization;
using System.Data;


public partial class Modules_AspxCommerce_AspxItemDetails_ItemDetails : BaseUserControl
{
    public string itemSKU, ItemBasicInfo, lstItemCostVariant,
        itemName, userEmail, aspxfilePath, AllowAddToCart,
        allowOutStockPurchase, allowAnonymousReviewRate,
        enableEmailFriend, noItemDetailImagePath, variantQuery,
        AllowRealTimeNotifications, ItemPagePath = string.Empty;

    public bool allowMultipleReviewPerIP,
        allowMultipleReviewPerUser,
        isReviewExistByUser,
        isReviewExistByIP;

    public int itemID, RatingCount, itemTypeId = 0;
    public double AvarageRating = 0.0;

    private int storeID,
               portalID,
               customerID,
               minimumItemQuantity,
               maximumItemQuantity;

    private string userIP, userName, cultureName, sessionCode, countryName = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        SageFrameRoute parentPage = (SageFrameRoute)this.Page;
        itemSKU = parentPage.Key;
        userIP = HttpContext.Current.Request.UserHostAddress;

        string templateName = TemplateName;
        aspxfilePath = ResolveUrl("~") + "Modules/AspxCommerce/AspxItemsManagement/";

        GetPortalCommonInfo(out storeID, out portalID, out customerID, out userName, out cultureName, out sessionCode);
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo(storeID, portalID, userName, cultureName, customerID, sessionCode);

        variantQuery = Request.QueryString["varId"];
        IPAddressToCountryResolver ipToCountry = new IPAddressToCountryResolver();

        ipToCountry.GetCountry(userIP, out countryName);
        if (countryName == null)
            countryName = string.Empty;

        SecurityPolicy objSecurity = new SecurityPolicy();
        FormsAuthenticationTicket ticket = objSecurity.GetUserTicket(GetPortalID);
        if (ticket != null && ticket.Name != ApplicationKeys.anonymousUser)
        {
            MembershipController member = new MembershipController();
            UserInfo userDetail = member.GetUserDetails(GetPortalID, GetUsername);
            userEmail = userDetail.Email;
        }
        string AllowMultipleReviewPerUser, AllowMultipleReviewPerIP = string.Empty;

        StoreSettingConfig ssc = new StoreSettingConfig();
        ssc.GetStoreSettingParamEight(StoreSetting.AllowRealTimeNotifications, StoreSetting.DefaultProductImageURL, StoreSetting.EnableEmailAFriend,
           StoreSetting.AllowAnonymousUserToWriteItemRatingAndReviews, StoreSetting.AllowOutStockPurchase, StoreSetting.ShowAddToCartButton,
           StoreSetting.AllowMultipleReviewsPerUser, StoreSetting.AllowMultipleReviewsPerIP, out AllowRealTimeNotifications,
           out noItemDetailImagePath, out enableEmailFriend, out allowAnonymousReviewRate, out allowOutStockPurchase, out AllowAddToCart,
           out AllowMultipleReviewPerUser, out AllowMultipleReviewPerIP, storeID, portalID, cultureName);

        allowMultipleReviewPerUser = Boolean.Parse(AllowMultipleReviewPerUser);
        allowMultipleReviewPerIP = Boolean.Parse(AllowMultipleReviewPerIP);

        ItemPagePath = ResolveUrl("~/Item/");



        if (!IsPostBack)
        {
            IncludeCss("ItemDetails",
                       "/Templates/" + templateName + "/css/PopUp/style.css",
                       "/Templates/" + templateName + "/css/StarRating/jquery.rating.css",
                       "/Templates/" + templateName + "/css/JQueryUIFront/jquery-ui.all.css",
                       "/Templates/" + templateName + "/css/MessageBox/style.css",
                       "/Templates/" + templateName + "/css/FancyDropDown/fancy.css",
                       "/Templates/" + templateName + "/css/ToolTip/tooltip.css",
                       "/Templates/" + templateName + "/css/PopUp/popbox.css",
                       "/Modules/AspxCommerce/AspxItemDetails/css/module.css"
                       );
            IncludeJs("ItemDetails",
                      "/js/jDownload/jquery.jdownload.js", "/js/MessageBox/alertbox.js", "/js/DateTime/date.js",
                      "/js/PopUp/custom.js", "/js/FormValidation/jquery.validate.js",
                      "/js/StarRating/jquery.rating.js",
                       "/Modules/AspxCommerce/AspxItemDetails/js/jquery.currencydropdown.js",
                      "/js/PopUp/popbox.js",
                      "/js/FancyDropDown/itemFancyDropdown.js",
                      "/js/jquery.labelify.js", "/js/encoder.js", "/js/StarRating/jquery.rating.pack.js",
                      "/js/StarRating/jquery.MetaData.js", "/js/Paging/jquery.pagination.js",
                       "/Modules/AspxCommerce/AspxItemDetails/js/ItemDetails.js");
        }
        IncludeLanguageJS();
        GetItemDetailsInfo(aspxCommonObj, itemSKU, userIP, countryName);

    }
    private void GetItemDetailsInfo(AspxCommonInfo aspxCommonObj, string itemSKU, string userIP, string countryName)
    {
        DataSet dsItemDetails = new DataSet();
        dsItemDetails = AspxItemMgntController.GetItemDetailsInfo(aspxCommonObj, itemSKU, userIP, countryName);
        if (dsItemDetails != null && dsItemDetails.Tables != null && dsItemDetails.Tables.Count > 0)
        {
            DataTable dtQtyDiscount = dsItemDetails.Tables[0];
            DataTable dtItemRating = dsItemDetails.Tables[1];
            DataTable dtItemRateCriteria = dsItemDetails.Tables[2];
            DataTable dtReviewStat = dsItemDetails.Tables[3];
            DataTable dtItemBasicInfo = dsItemDetails.Tables[4];
            DataTable dtItemSEO = dsItemDetails.Tables[5];

            if (dtQtyDiscount != null && dtQtyDiscount.Rows.Count > 0)
            {
                BindItemQuantityDiscountByUserName(dtQtyDiscount);
            }
            BindItemAverageRating(dtItemRating);
            if (dtItemRateCriteria != null && dtItemRateCriteria.Rows.Count > 0)
            {
                BindRatingCriteria(dtItemRateCriteria);
            }
            if (dtReviewStat != null && dtReviewStat.Rows.Count > 0)
            {
                GetUserReviewStatus(dtReviewStat);
            }

            if (dtItemSEO != null && dtItemSEO.Rows.Count > 0)
            {
                OverRideSEOInfo(dtItemSEO);
            }

            if (dtItemBasicInfo != null && dtItemBasicInfo.Rows.Count > 0)
            {
                GetItemBasicByitemSKU(itemSKU, aspxCommonObj, dtItemBasicInfo);
            }
            if (itemTypeId != 5)
            {
                DataTable dtPriceHistory = dsItemDetails.Tables[6];
                if (dtPriceHistory != null && dtPriceHistory.Rows.Count > 0)
                {
                    GetPriceHistory(dtPriceHistory);

                }

            }
        }
    }
    private void OverRideSEOInfo(DataTable dtItemSEO)
    {
        if (dtItemSEO != null && dtItemSEO.Rows.Count > 0)
        {
            itemID = int.Parse(dtItemSEO.Rows[0]["ItemID"].ToString());
            itemName = dtItemSEO.Rows[0]["Name"].ToString();
            string PageTitle = dtItemSEO.Rows[0]["MetaTitle"].ToString();
            string PageKeyWords = dtItemSEO.Rows[0]["MetaKeywords"].ToString();
            string PageDescription = dtItemSEO.Rows[0]["MetaDescription"].ToString();

            if (!string.IsNullOrEmpty(PageTitle))
                SEOHelper.RenderTitle(this.Page, PageTitle, false, true, this.GetPortalID);
            else
                SEOHelper.RenderTitle(this.Page, itemName, false, true, this.GetPortalID);
            if (!string.IsNullOrEmpty(PageKeyWords))
                SEOHelper.RenderMetaTag(this.Page, "KEYWORDS", PageKeyWords, true);

            if (!string.IsNullOrEmpty(PageDescription))
                SEOHelper.RenderMetaTag(this.Page, "DESCRIPTION", PageDescription, true);
        }
    }

    public ItemSEOInfo GetSEOSettingsBySKU(string itemSKU, AspxCommonInfo aspxCommonObj)
    {
        return AspxItemMgntController.GetSEOSettingsBySKU(itemSKU, aspxCommonObj);
    }

    private void BindRatingCriteria(DataTable dtItemRateCriteria)
    {
        if (dtItemRateCriteria != null && dtItemRateCriteria.Rows.Count > 0)
        {
            StringBuilder ratingCriteria = new StringBuilder();
            for (int i = 0; i < dtItemRateCriteria.Rows.Count; i++)
            {
                ratingCriteria.Append("<tr><td class='cssClassReviewCriteria'><label class='cssClassLabel'>");
                ratingCriteria.Append(dtItemRateCriteria.Rows[i]["ItemRatingCriteria"]);
                ratingCriteria.Append(":<span class='cssClassRequired'>*</span></label></td><td>");
                ratingCriteria.Append("<input name=\"star");
                ratingCriteria.Append(dtItemRateCriteria.Rows[i]["ItemRatingCriteriaID"]);
                ratingCriteria.Append("\" type='radio' class='auto-submit-star' value='1' title=\"");
                ratingCriteria.Append(getLocale("Worst"));
                ratingCriteria.Append("\" validate='required:true' />");
                ratingCriteria.Append("<input name=\"star");
                ratingCriteria.Append(dtItemRateCriteria.Rows[i]["ItemRatingCriteriaID"]);
                ratingCriteria.Append("\" type='radio' class='auto-submit-star' value='2' title=\"");
                ratingCriteria.Append(getLocale("Bad"));
                ratingCriteria.Append("\"/>");
                ratingCriteria.Append("<input name=\"star");
                ratingCriteria.Append(dtItemRateCriteria.Rows[i]["ItemRatingCriteriaID"]);
                ratingCriteria.Append("\" type='radio' class='auto-submit-star' value='3' title=\"");
                ratingCriteria.Append(getLocale("OK"));
                ratingCriteria.Append("\"/>");
                ratingCriteria.Append("<input name=\"star");
                ratingCriteria.Append(dtItemRateCriteria.Rows[i]["ItemRatingCriteriaID"]);
                ratingCriteria.Append("\" type='radio' class='auto-submit-star' value='4' title=\"");
                ratingCriteria.Append(getLocale("Good"));
                ratingCriteria.Append("\"/>");
                ratingCriteria.Append("<input name=\"star");
                ratingCriteria.Append(dtItemRateCriteria.Rows[i]["ItemRatingCriteriaID"]);
                ratingCriteria.Append("\" type='radio' class='auto-submit-star' value='5' title=\"");
                ratingCriteria.Append(getLocale("Best"));
                ratingCriteria.Append("\"/>");
                ratingCriteria.Append("<span id=\"hover-test");
                ratingCriteria.Append(dtItemRateCriteria.Rows[i]["ItemRatingCriteriaID"]);
                ratingCriteria.Append("\" class='cssClassRatingText'></span>");
                ratingCriteria.Append("<label for=\"star");
                ratingCriteria.Append(dtItemRateCriteria.Rows[i]["ItemRatingCriteriaID"]);
                ratingCriteria.Append("\" class='error'>");
                ratingCriteria.Append(getLocale("Please rate for"));
                ratingCriteria.Append(' ');
                ratingCriteria.Append(dtItemRateCriteria.Rows[i]["ItemRatingCriteria"]);
                ratingCriteria.Append("</label></td></tr>");
            }
            ltrRatingCriteria.Text = ratingCriteria.ToString();
        }
    }
    public void BindItemAverageRating(DataTable dtRating)
    {
        int index = 0;
        StringBuilder ratingBind = new StringBuilder();
        if (dtRating != null && dtRating.Rows.Count > 0)
        {
            string script = "$('.cssClassAddYourReview').html('" + getLocale("Write Your Own Review") +
                            "');$('.cssClassItemRatingBox').addClass('cssClassToolTip');";
            string rating = "<div class=\"cssClassToolTipInfo\">",
                starrating = "<table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" border=\"0\" id=\"tblAverageRating\">";
            for (int i = 0; i < dtRating.Rows.Count; i++)
            {
                if (index == 0)
                {
                    string spt = "$('.cssClassTotalReviews').html('" + getLocale("Read Reviews") + "[" +
                                             dtRating.Rows[i]["TotalReviewsCount"] + "]" + "');";
                    RatingCount = (int)dtRating.Rows[i]["TotalReviewsCount"];
                    AvarageRating = FormatRatingAverage(double.Parse(dtRating.Rows[i]["TotalRatingAverage"].ToString()));
                    starrating += BindStarRating(AvarageRating, script, spt);
                }
                index++;
                rating += BindViewDetailsRatingInfo((int)dtRating.Rows[i]["ItemRatingCriteriaID"], dtRating.Rows[i]["ItemRatingCriteria"].ToString(),
                                            FormatRatingAverage(double.Parse(dtRating.Rows[i]["RatingCriteriaAverage"].ToString())));
            }
            starrating += "</table>";
            rating += "</div>";
            rating += GetScriptRun("$('input.star').rating();");
            starrating += GetScriptRun(ratingScript);
            ltrRatings.Text = starrating;
            ltrratingDetails.Text = rating;
        }
        else
        {
            ratingBind.Append("<table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" border=\"0\" id=\"tblAverageRating\"><tr><td>");
            ratingBind.Append(getLocale("Currently there are no reviews"));
            ratingBind.Append("</td></tr></table>");
            string script = "$('.cssClassItemRatingBox').removeClass('cssClassToolTip');$('.cssClassSeparator').hide();$('.cssClassAddYourReview').html('" +
                                         getLocale("Be the first to review this item.") + "');";
            ratingBind.Append(GetScriptRun(script));
            ltrRatings.Text = ratingBind.ToString();

        }
    }

    private double FormatRatingAverage(double ratingAverage)
    {
        if ((Math.Round(ratingAverage, 1) - Math.Floor(ratingAverage)) > 0.50)
        {
            return Math.Ceiling(ratingAverage);
        }
        else if ((Math.Round(ratingAverage, 1) - Math.Floor(ratingAverage)) > 0.50)
        {
            return Math.Floor(ratingAverage);
        }
        else if ((Math.Round(ratingAverage, 1) - Math.Floor(ratingAverage)) == 0.50)
        {
            return Math.Floor(ratingAverage) + 0.5;
        }
        else
        {
            return ratingAverage;
        }

    }

    private string ratingScript = "";

    public string BindStarRating(double totalTatingAvg, string spt, string sp)
    {
        StringBuilder ratingStars = new StringBuilder();
        string[] ratingTitle = {
                                   getLocale("Worst"), getLocale("Ugly"), getLocale("Bad"), getLocale("Not Bad"),
                                   getLocale("Average"), getLocale("OK"), getLocale("Nice"), getLocale("Good"),
                                   getLocale("Best"), getLocale("Excellent")
                               }; double[] ratingText = { 0.5, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5 };
        int i = 0;
        ratingStars.Append("<tr><td>");
        for (i = 0; i < 10; i++)
        {
            if (totalTatingAvg == ratingText[i])
            {
                ratingStars.Append("<input name=\"avgItemRating\" type=\"radio\" class=\"star {split:2}\" disabled=\"disabled\" checked=\"checked\" value=");
                ratingStars.Append(ratingTitle[i]);
                ratingStars.Append(" />");
                ratingScript += "$('.cssClassRatingTitle').html('" + ratingTitle[i] + "');";
            }
            else
            {
                ratingStars.Append("<input name=\"avgItemRating\" type=\"radio\" class=\"star {split:2}\" disabled=\"disabled\" value=");
                ratingStars.Append(ratingTitle[i]);
                ratingStars.Append(" />");
            }
        }
        ratingStars.Append("</td></tr>");
        ratingStars.Append(GetScriptRun(spt + sp));
        return ratingStars.ToString();
    }

    public string BindViewDetailsRatingInfo(int itemRatingCriteriaId, string itemRatingCriteriaNm, double ratingAvg)
    {
        StringBuilder ratingStarsDetailsInfo = new StringBuilder();
        string[] ratingTitle1 = {
                                    getLocale("Worst"), getLocale("Ugly"), getLocale("Bad"), getLocale("Not Bad"),
                                    getLocale("Average"), getLocale("OK"), getLocale("Nice"), getLocale("Good"),
                                    getLocale("Best"), getLocale("Excellent")
                                };
        double[] ratingText1 = { 0.5, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5 };
        int i = 0;
        ratingStarsDetailsInfo.Append("<div class=\"cssClassToolTipDetailInfo\">");
        ratingStarsDetailsInfo.Append("<span class=\"cssClassCriteriaTitle\">");
        ratingStarsDetailsInfo.Append(itemRatingCriteriaNm);
        ratingStarsDetailsInfo.Append(": </span>");
        for (i = 0; i < 10; i++)
        {
            if (ratingAvg == ratingText1[i])
            {
                ratingStarsDetailsInfo.Append("<input name=\"avgItemDetailRating");
                ratingStarsDetailsInfo.Append(itemRatingCriteriaId);

                ratingStarsDetailsInfo.Append("\" type=\"radio\" class=\"star {split:2}\" disabled=\"disabled\" checked=\"checked\" value=");

                ratingStarsDetailsInfo.Append(ratingTitle1[i]);
                ratingStarsDetailsInfo.Append(" />");
            }
            else
            {
                ratingStarsDetailsInfo.Append("<input name=\"avgItemDetailRating");
                ratingStarsDetailsInfo.Append(itemRatingCriteriaId);

                ratingStarsDetailsInfo.Append("\" type=\"radio\" class=\"star {split:2}\" disabled=\"disabled\" value=");

                ratingStarsDetailsInfo.Append(ratingTitle1[i]);
                ratingStarsDetailsInfo.Append(" />");
            }
        }
        ratingStarsDetailsInfo.Append("</div>");
        return ratingStarsDetailsInfo.ToString();
    }

    private Hashtable hst = null;
    private void GetPriceHistory(DataTable dtPriceHistory)
    {
        if (dtPriceHistory != null && dtPriceHistory.Rows.Count > 0)
        {
            StringBuilder priceHistory = new StringBuilder();
            priceHistory.Append("<div class='popbox'><a class='open sfLocale' href='javascript:void(0);'>Price History</a><div class='collapse'>");
            priceHistory.Append("<div class='box'><div class='arrow'></div><div class='arrow-border'></div><div class='classPriceHistory'>");
            priceHistory.Append("<table class=classPriceHistoryList><thead><th>Date</th><th>Price</th></thead><tbody>");
            for (int i = 0; i < dtPriceHistory.Rows.Count; i++)
            {
                priceHistory.Append("<tr><td><span>");
                priceHistory.Append(dtPriceHistory.Rows[i]["Date"]);
                priceHistory.Append("</span></td><td><span class='cssClassFormatCurrency'>");
                priceHistory.Append(Convert.ToInt32(dtPriceHistory.Rows[i]["ConvertedPrice"]).ToString("N2"));
                priceHistory.Append("</span></td></tr>");
            }
            priceHistory.Append("</tbody></table>");
            priceHistory.Append("</div></div></div></div>");
            ltrPriceHistory.Text = priceHistory.ToString();
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
    public string Format_The_Date(string input)
    {
        string dt;
        DateTime strDate = DateTime.Parse(input);
        dt = strDate.ToString("yyyy/MM/dd");//Specify Format you want the O/P as...
        return dt;
    }

    public void BindItemQuantityDiscountByUserName(DataTable dtQtyDiscount)
    {
        StringBuilder QtyDiscount = new StringBuilder();
        if (dtQtyDiscount != null && dtQtyDiscount.Rows.Count > 0)
        {
            QtyDiscount.Append("<div id=\"divQtyDiscount\">");
            QtyDiscount.Append("<div class=\"cssClassCommonGrid\">");
            QtyDiscount.Append("<p>");
            QtyDiscount.Append(getLocale("Bulk Discount available:"));
            QtyDiscount.Append("</p>");
            QtyDiscount.Append("<table id=\"itemQtyDiscount\">");
            QtyDiscount.Append("<thead>");
            QtyDiscount.Append("<tr class=\"cssClassHeadeTitle\"><th class=\"sfLocale\">Quantity (more than)</th><th class=\"sfLocale\">Price Per Unit</th></tr>");
            QtyDiscount.Append("</thead><tbody>");
            for (int i = 0; i < dtQtyDiscount.Rows.Count; i++)
            {
                QtyDiscount.Append("<tr><td>");
                QtyDiscount.Append(Convert.ToInt32(dtQtyDiscount.Rows[i]["Quantity"]));
                QtyDiscount.Append("</td><td><span class='cssClassFormatCurrency'>");
                QtyDiscount.Append(Convert.ToInt32(dtQtyDiscount.Rows[i]["Price"]).ToString("N2"));
                QtyDiscount.Append("</span></td></tr>");
            }
            QtyDiscount.Append("</tbody></table>");
            QtyDiscount.Append("</div>");
            QtyDiscount.Append("</div>");
            litQtyDiscount.Text = QtyDiscount.ToString();
        }
    }

    private void GetItemBasicByitemSKU(string itemSKU, AspxCommonInfo aspxCommonObj, DataTable dtItemBasicInfo)
    {

        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        if (dtItemBasicInfo != null && dtItemBasicInfo.Rows.Count > 0)
        {
            itemTypeId = (int)dtItemBasicInfo.Rows[0]["ItemTypeID"];
            ItemBasicInfo = AspxCommerce.Core.CommonHelper.ConvertDataTableTojSonString(dtItemBasicInfo);
            if (itemTypeId != 5 && (bool)dtItemBasicInfo.Rows[0]["IsCostVariantItem"])
            {
                List<ItemCostVariantsInfo> lstItemCostVar = AspxItemMgntController.GetCostVariantsByItemSKU(itemSKU, aspxCommonObj);
                lstItemCostVariant = json_serializer.Serialize(lstItemCostVar);
            }
        }
    }

    private void GetUserReviewStatus(DataTable dtReviewStat)
    {
        isReviewExistByIP = (bool)dtReviewStat.Rows[0]["IsReviewByIPExist"];
        isReviewExistByUser = (bool)dtReviewStat.Rows[0]["IsReviewByUserExist"];
    }
    private string GetScriptRun(string code)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type=\"text/javascript\">$(document).ready(function(){");
        sb.Append(code);
        sb.Append("});</script>");
        return sb.ToString();
    }
}