using System;
using System.Collections.Generic;
using System.Text;
using SageFrame.Web;
using System.Collections;
using SageFrame;
using AspxCommerce.Core;
using System.Globalization;
using System.Web;
using System.Linq;

public partial class Modules_AspxCommerce_AspxItemDetailTab_ItemDetailTab : BaseUserControl
{
    public string itemSKU;
    private int storeID,
               portalID,
               customerID;
    private string userName, cultureName;
    private string sessionCode = string.Empty;
    private int itemTypeId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        SageFrameConfig pagebase = new SageFrameConfig();
        SageFrameRoute parentPage = (SageFrameRoute)this.Page;
        itemSKU = parentPage.Key;
        string templateName = TemplateName;
        GetPortalCommonInfo(out storeID, out portalID, out customerID, out userName, out cultureName, out sessionCode);
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo(storeID, portalID, userName, cultureName, customerID, sessionCode);

        if (!IsPostBack)
        {
            IncludeCss("ItemDetails", "/Templates/" + templateName + "/css/StarRating/jquery.rating.css",
                       "/Templates/" + templateName + "/css/JQueryUIFront/jquery-ui.all.css",
                       "/Templates/" + templateName + "/css/MessageBox/style.css",
                       "/Templates/" + templateName + "/css/FancyDropDown/fancy.css",
                       "/Templates/" + templateName + "/css/ToolTip/tooltip.css",
                       "/Templates/" + templateName + "/css/Scroll/scrollbars.css",
                       "/Templates/" + templateName + "/css/ResponsiveTab/responsive-tabs.css",
                       "/Modules/AspxCommerce/AspxItemDetailTab/css/module.css"
                       );

            IncludeJs("ItemDetails", "/js/DateTime/date.js",
                      "/js/StarRating/jquery.rating.js",
                      "/js/ResponsiveTab/responsiveTabs.js",
                      "/js/PopUp/popbox.js", "/js/Scroll/mwheelIntent.js",
                      "/js/Scroll/jScrollPane.js",
                      "/js/VideoGallery/jquery.youtubepopup.min.js", "/js/jquery.labelify.js", "/js/encoder.js",
                      "/js/StarRating/jquery.rating.pack.js", "/js/StarRating/jquery.MetaData.js", "/js/Paging/jquery.pagination.js", 
                      "/Modules/AspxCommerce/AspxItemDetailTab/js/ItemDetailTab.js");
        }
        IncludeLanguageJS();
        GetFormFieldList(itemSKU, aspxCommonObj);
    }

    private class GroupInfo
    {
        public int key { get; set; }
        public string value { get; set; }
        public string html { get; set; }
    }

    private Hashtable hst = null;

    public void GetFormFieldList(string itemSKU, AspxCommonInfo aspxCommonObj)
    {
        int RowTotal = 0;
        string resolvedUrl = ResolveUrl("~/");
        string modulePath = this.AppRelativeTemplateSourceDirectory;
        string aspxTemplateFolderPath = resolvedUrl + "Templates/" + TemplateName;
        string aspxRootPath = resolvedUrl;
        hst = AppLocalized.getLocale(modulePath);
        string pageExtension = SageFrameSettingKeys.PageExtension;
        List<GroupInfo> arrList = new List<GroupInfo>();
        int attributeSetId = 0;
        int index = 0;
        List<AttributeFormInfo> frmItemFieldList = AspxItemMgntController.GetItemFormAttributesByItemSKUOnly(itemSKU,
                                                                                                               aspxCommonObj);
        List<ItemRatingByUserInfo> lstRatingByUser = AspxRatingReviewController.GetItemRatingPerUser(1, 5, itemSKU, aspxCommonObj);

        List<ItemRatingByUserInfo> lstAvgRating = lstRatingByUser.GroupBy(x => x.ItemReviewID).Select(g => g.First()).ToList<ItemRatingByUserInfo>();     // var lstAvgRating=lstRatingByUser.Distinct(a)
       
        StringBuilder dynHtml = new StringBuilder();
        foreach (AttributeFormInfo item in frmItemFieldList)
        {
            bool isGroupExist = false;
            dynHtml = new StringBuilder();

            if (index == 0)
            {
                attributeSetId = (int)item.AttributeSetID;
                itemTypeId = (int)item.ItemTypeID;
            }
            index++;
            GroupInfo objGroup = new GroupInfo();
            objGroup.key = (int)item.GroupID;
            objGroup.value = item.GroupName;
            objGroup.html = "";
            foreach (GroupInfo objGroup1 in arrList)
            {
                if (objGroup1.key == item.GroupID)
                {
                    isGroupExist = true;
                    break;
                }
            }
            if (!isGroupExist)
            {
                if ((item.ItemTypeID == 2 || item.ItemTypeID == 3) && item.GroupID == 11)
                {
                }
                else
                {
                    arrList.Add(objGroup);
                }
            }
            StringBuilder tr = new StringBuilder();
            if ((item.ItemTypeID == 2 || item.ItemTypeID == 3) && item.AttributeID == 32 && item.AttributeID == 33 && item.AttributeID == 34)
            {
            }
            else
            {
                tr.Append("<tr><td class=\"cssClassTableLeftCol\"><label class=\"cssClassLabel\">");
                tr.Append(item.AttributeName);
                tr.Append(": </label></td>");
                tr.Append("<td><div id=\"");
                tr.Append(item.AttributeID);
                tr.Append("_");
                tr.Append(item.InputTypeID);
                tr.Append("_");
                tr.Append(item.ValidationTypeID);

                tr.Append("_");
                tr.Append(item.IsRequired);
                tr.Append("_");
                tr.Append(item.GroupID);
                tr.Append("_");
                tr.Append(item.IsIncludeInPriceRule);
                tr.Append("_");
                tr.Append(item.IsIncludeInPromotions);
                tr.Append("_");
                tr.Append(item.DisplayOrder);
                tr.Append("\" name=\"");
                tr.Append(item.AttributeID);
                tr.Append("_");

                tr.Append(item.InputTypeID);
                tr.Append("_");
                tr.Append(item.ValidationTypeID);
                tr.Append("_");
                tr.Append(item.IsRequired);
                tr.Append("_");
                tr.Append(item.GroupID);
                tr.Append("_");
                tr.Append(item.IsIncludeInPriceRule);
                tr.Append("_");
                tr.Append(item.IsIncludeInPromotions);

                tr.Append("_");
                tr.Append(item.DisplayOrder);
                tr.Append("\" title=\"");
                tr.Append(item.ToolTip);
                tr.Append("\">");
                tr.Append("</div></td>");
                tr.Append("</tr>");
            }
            foreach (GroupInfo objGroupInfo in arrList)
            {
                if (objGroupInfo.key == item.GroupID)
                {
                    objGroupInfo.html += tr;
                }

            }

            StringBuilder itemTabs = new StringBuilder();
            dynHtml.Append("<div id=\"dynItemDetailsForm\" class=\"sfFormwrapper\" style=\"display:none\">");
            dynHtml.Append("<div class=\"cssClassTabPanelTable\">");
            dynHtml.Append("<div id=\"ItemDetails_TabContainer\" class=\"responsive-tabs\">");
            for (var i = 0; i < arrList.Count; i++)
            {
                itemTabs.Append("<h2><span>");
                itemTabs.Append(arrList[i].value);
                itemTabs.Append("</span></a></h2>");

                itemTabs.Append("<div id=\"ItemTab-");
                itemTabs.Append(arrList[i].key);
                itemTabs.Append("\"><div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
                itemTabs.Append(arrList[i].html);
                itemTabs.Append("</table></div></div>");
            }
            itemTabs.Append("<h2><span>");
            itemTabs.Append(getLocale("Tags"));
            itemTabs.Append("</span></h2>");
            StringBuilder itemTagsBody = new StringBuilder();
            itemTagsBody.Append("<div class=\"cssClassPopularItemTags\"><div id=\"popularTag\"></div>");
            if (aspxCommonObj.CustomerID > 0 && aspxCommonObj.UserName.ToLower() != "anonymoususer")
            {
                itemTagsBody.Append("<h2>");
                itemTagsBody.Append(getLocale("My Tags:"));
                itemTagsBody.Append("</h2><div id=\"divMyTags\" class=\"cssClassMyTags\"></div>");
                itemTagsBody.Append("<table id=\"AddTagTable\"><tr><td>");
                itemTagsBody.Append("<input type=\"text\" class=\"classTag\" maxlength=\"20\"/>");
                itemTagsBody.Append("<button class=\"cssClassDecrease\" type=\"button\"><span>-</span></button>");
                itemTagsBody.Append("<button class=\"cssClassIncrease\" type=\"button\"><span>+</span></button>");
                itemTagsBody.Append("</td></tr></table>");
                itemTagsBody.Append("<div class=\"sfButtonwrapper\"><button type=\"button\" id=\"btnTagSubmit\"><span>");
                itemTagsBody.Append(getLocale("+ Tag"));
                itemTagsBody.Append("</span></button></div>");
            }
            else
            {
                SageFrameConfig sfConfig = new SageFrameConfig();
                itemTagsBody.Append("<a href=\"");
                itemTagsBody.Append(aspxRedirectPath);
                itemTagsBody.Append(sfConfig.GetSettingsByKey(SageFrameSettingKeys.PortalLoginpage));
                itemTagsBody.Append(pageExtension);
                itemTagsBody.Append("?ReturnUrl=");
                itemTagsBody.Append(aspxRedirectPath);
                itemTagsBody.Append("item/");
                itemTagsBody.Append(itemSKU);
                itemTagsBody.Append(pageExtension);
                itemTagsBody.Append("\" class=\"cssClassLogIn\"><span>");
                itemTagsBody.Append(getLocale("Sign in to enter tags"));
                itemTagsBody.Append("</span></a>");
            }
            itemTagsBody.Append("</div>");
            itemTabs.Append("<div  id=\"ItemTab-Tags\"><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td>");
            itemTabs.Append(itemTagsBody);
            itemTabs.Append("</td></tr></table></div>");
            itemTabs.Append("<h2><span>");
            itemTabs.Append(getLocale("Ratings & Reviews"));
            itemTabs.Append(" </span></h2>");
            //
            StringBuilder strUserRating = new StringBuilder();
            itemTabs.Append("<div id=\"ItemTab-Reviews\">");
            itemTabs.Append("<table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" border=\"0\" id=\"tblRatingPerUser\">");
            if (lstRatingByUser != null && lstRatingByUser.Count > 0)
            {
                foreach (ItemRatingByUserInfo UserItemRating in lstAvgRating)
                {

                    RowTotal = UserItemRating.RowTotal;
                    strUserRating.Append("<tr><td><div class=\"cssClassRateReview\"><div class=\"cssClassItemRating\">");
                    strUserRating.Append("<div class=\"cssClassItemRatingBox\">");
                    StringBuilder ratingStars = new StringBuilder();
                    string[] ratingTitle = { getLocale("Worst"), getLocale("Ugly"), getLocale("Bad"), getLocale("Not Bad"), getLocale("Average"), getLocale("OK"), getLocale("Nice"), getLocale("Good"), getLocale("Best"), getLocale("Excellent") };
                    string[] ratingText = { "0.5", "1.0", "1.5", "2.0", "2.5", "3.0", "3.5", "4.0", "4.5", "5.0" };
                    int i = 0;
                    string ratingTitleText = string.Empty;
                    ratingStars.Append("<div class=\"cssClassRatingStar\"><div class=\"cssClassToolTip\">");
                   
                    for (i = 0; i < 10; i++)
                    {
                        if ((UserItemRating.RatingAverage).ToString() == ratingText[i])
                        {
                            ratingStars.Append("<span class=\"cssClassRatingTitle2 cssClassUserRatingTitle_");
                            ratingStars.Append(UserItemRating.ItemReviewID);
                            ratingStars.Append("\">");
                            ratingStars.Append(ratingTitle[i]);
                            ratingStars.Append("</span>");

                            ratingStars.Append("<input name=\"avgRatePerUser");
                            ratingStars.Append(UserItemRating.ItemReviewID);
                            ratingStars.Append("\"type=\"radio\" class=\"star-rate {split:2}\" disabled=\"disabled\" checked=\"checked\" value=\"");
                            ratingStars.Append(ratingTitle[i]);
                            ratingStars.Append("\" />");
                            ratingTitleText = ratingTitle[i];
                        }
                        else
                        {      
                            ratingStars.Append("<input name=\"avgRatePerUser");
                            ratingStars.Append(UserItemRating.ItemReviewID);
                            ratingStars.Append("\" type=\"radio\" class=\"star-rate {split:2}\" disabled=\"disabled\" value=");
                            ratingStars.Append(ratingTitle[i]);
                            ratingStars.Append(" />");
                        }
                    }
                    ratingStars.Append("<input type=\"hidden\" value=\"");
                    ratingStars.Append(ratingTitleText);
                    ratingStars.Append("\" id=\"hdnRatingTitle");
                    ratingStars.Append(UserItemRating.ItemReviewID);
                    ratingStars.Append("\"></input><span class=\"cssClassToolTipInfo cssClassReviewId_");
                    ratingStars.Append(UserItemRating.ItemReviewID);
                    ratingStars.Append("\">");

                    List<ItemRatingByUserInfo> lstRatingCriteria = lstRatingByUser.Where(x => x.ItemReviewID == UserItemRating.ItemReviewID).ToList<ItemRatingByUserInfo>();
                    StringBuilder userRatingStarsDetailsInfo = new StringBuilder();
                    foreach (ItemRatingByUserInfo ratingCriteria in lstRatingCriteria)
                    {
                          
                        string[] ratingText1 = { getLocale("Worst"), getLocale("Ugly"), getLocale("Bad"), getLocale("Not Bad"), getLocale("Average"), getLocale("OK"), getLocale("Nice"), getLocale("Good"), getLocale("Best"), getLocale("Excellent") };
                        string[] ratingValue = { "0.50", "1.00", "1.50", "2.00", "2.50", "3.00", "3.50", "4.00", "4.50", "5.00" };
                        int j = 0;


                        userRatingStarsDetailsInfo.Append("<div class=\"cssClassToolTipDetailInfo\">");
                        userRatingStarsDetailsInfo.Append("<span class=\"cssClassCriteriaTitle\">");
                        userRatingStarsDetailsInfo.Append(ratingCriteria.ItemRatingCriteria);
                        userRatingStarsDetailsInfo.Append(": </span>");
                        for (j = 0; j < 10; j++)
                        {
                            if ((ratingCriteria.RatingValue).ToString() == ratingValue[j])
                            {
                                userRatingStarsDetailsInfo.Append("<input name=\"avgUserDetailRate");
                                userRatingStarsDetailsInfo.Append(ratingCriteria.ItemRatingCriteria );
                                userRatingStarsDetailsInfo.Append("_" );
                                userRatingStarsDetailsInfo.Append(ratingCriteria.ItemReviewID );
                                userRatingStarsDetailsInfo.Append("\" type=\"radio\" class=\"star-rate {split:2}\" disabled=\"disabled\" checked=\"checked\" value=");
                                userRatingStarsDetailsInfo.Append(ratingText1[j]);
                                userRatingStarsDetailsInfo.Append(" />");
                            }
                            else
                            {
                                userRatingStarsDetailsInfo.Append("<input name=\"avgUserDetailRate");
                                userRatingStarsDetailsInfo.Append(ratingCriteria.ItemRatingCriteria );
                                userRatingStarsDetailsInfo.Append("_" );
                                userRatingStarsDetailsInfo.Append(ratingCriteria.ItemReviewID );
                                userRatingStarsDetailsInfo.Append("\" type=\"radio\" class=\"star-rate {split:2}\" disabled=\"disabled\" value=");
                                userRatingStarsDetailsInfo.Append(ratingText1[j] );
                                userRatingStarsDetailsInfo.Append(" />");
                            }
                        }
                        userRatingStarsDetailsInfo.Append("</div>");
                       
                    }
                    ratingStars.Append(userRatingStarsDetailsInfo.ToString());
                    ratingStars.Append("</span></div></div><div class=\"cssClassClear\"></div>");
                    strUserRating.Append(ratingStars);
                    strUserRating.Append("</div>");

                    strUserRating.Append("<div class=\"cssClassRatingInfo\"><p><span>");
                    strUserRating.Append(getLocale("Reviewed by "));
                    strUserRating.Append("<strong>");
                    strUserRating.Append(UserItemRating.Username);
                    strUserRating.Append("</strong></span></p><p class=\"cssClassRatingReviewDate\">(");
                    strUserRating.Append(getLocale("Posted on"));
                    strUserRating.Append("&nbsp;<strong>");
                    strUserRating.Append(Format_The_Date(UserItemRating.AddedOn));
                    strUserRating.Append("</strong>)</p></div></div>");
                    strUserRating.Append("<div class=\"cssClassRatingdesc\"><p>");
                    strUserRating.Append(HttpUtility.HtmlDecode(UserItemRating.ReviewSummary));
                    strUserRating.Append("</p><p class=\"cssClassRatingReviewDesc\">");
                    strUserRating.Append(HttpUtility.HtmlDecode(UserItemRating.Review));
                    strUserRating.Append("</p></div>");
                    strUserRating.Append("</div></td></tr>");

                    StringBuilder strScript = new StringBuilder();

                    strScript.Append("$('input.star-rate').rating();");
                    strScript.Append("$('#tblRatingPerUser tr:even').addClass('sfOdd');");
                    strScript.Append("$('#tblRatingPerUser tr:odd').addClass('sfEven');");
                    strUserRating.Append(GetScriptRun(strScript.ToString()));

                }
                string strPage = CreateDropdownPageSize(RowTotal);
                itemTabs.Append(strPage);
                
            }
            else
            {
                strUserRating.Append(getLocale("Currently no reviews and ratings available"));
            }

            itemTabs.Append(strUserRating.ToString());
            itemTabs.Append("</table>"); 
            itemTabs.Append(BindItemVideo(aspxCommonObj));
            dynHtml.Append(itemTabs);
            dynHtml.Append("</div></div></div>");
        }
        if (itemSKU.Length > 0)
        {
            string script = BindDataInTab(itemSKU, attributeSetId, itemTypeId, aspxCommonObj);
            string tagBind = "";
            tagBind = GetItemTags(itemSKU, aspxCommonObj);
            dynHtml.Append(script);
            dynHtml.Append(tagBind);
            ltrItemDetailsForm.Text = dynHtml.ToString();
        }

    }

    public string CreateDropdownPageSize(int RowTotal)
    {

        StringBuilder strPage = new StringBuilder();
        strPage.Append("<div class=\"cssClassPageNumber\" id=\"divSearchPageNumber\">");
        strPage.Append("<div id=\"Pagination\">");
        strPage.Append("<div class=\"pagination\">");
        decimal noOfPages = ((decimal)RowTotal / 5);
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
                strPage.Append("<a href=\"#\" onclick=\"ItemDetailTab.GetItemRatingPerUser(");
                strPage.Append((((i - 1) * 5) + 1));
                strPage.Append(",");
                strPage.Append(5);
                strPage.Append(",");
                strPage.Append(i);
                strPage.Append(")\">");
                strPage.Append(i);
                strPage.Append("</a>");
            }

        }
        if (numberOfPages > 1)
        {
            strPage.Append("<a class=\"next\" href=\"#\" onclick=\"ItemDetailTab.GetItemRatingPerUser(");
            strPage.Append((((2 - 1) * 5) + 1));
            strPage.Append(",");
            strPage.Append(5);
            strPage.Append(",");
            strPage.Append(2);
            strPage.Append(")\">");
            strPage.Append("Next");
            strPage.Append("</a>");
        }
        int recordCount = 5;
        if (RowTotal < 5)
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
        strPage.Append("<option data-html-text='5' value='5'>");
        strPage.Append(5);
        strPage.Append("</option>");
        strPage.Append("<option data-html-text='10' value='10'>");
        strPage.Append(10);
        strPage.Append("</option>");
        strPage.Append("<option data-html-text='15' value='15'>");
        strPage.Append(15);
        strPage.Append("</option>");
        strPage.Append("<option data-html-text='20' value='20'>");
        strPage.Append(20);
        strPage.Append("</option>");
        strPage.Append("<option data-html-text='25' value='25'>");
        strPage.Append(25);
        strPage.Append("</option>");
        strPage.Append("<option data-html-text='40' value='40'>");
        strPage.Append(40);
        strPage.Append("</option>");
        strPage.Append("</select>");
        strPage.Append("</div>");
        strPage.Append("<div class=\"clear\">");
        strPage.Append("</div>");
        strPage.Append("</div>");
        return strPage.ToString();
    }

    private StringBuilder BindItemVideo(AspxCommonInfo aspxCommonObj)
    {
        StringBuilder videoContainer = new StringBuilder();
        string itemVideo = AspxItemMgntController.GetItemVideos(itemSKU, aspxCommonObj);
        if (itemVideo != null && itemVideo != "")
        {
            videoContainer.Append("<h2 ><span>");
            videoContainer.Append(getLocale("Videos"));
            videoContainer.Append(" </span></h2>");
            videoContainer.Append("<div><div id=\"ItemVideos\"><ul>");
            string[] arr = itemVideo.Split(',');
            string href = "http://img.youtube.com/vi/";
            foreach (string item in arr)
            {
                string source = href + item + "/default.jpg";
                videoContainer.Append("<li><img class='youtube' id=\"");
                videoContainer.Append(item);
                videoContainer.Append("\" src=\"");
                videoContainer.Append(source);
                videoContainer.Append("title=\"Click me to play!\" /></li>");
            }
            videoContainer.Append("</ul></div></div>");
        }
        return videoContainer;
    }

    private string GetItemTags(string sku, AspxCommonInfo aspxCommonObj)
    {
        StringBuilder itemTags = new StringBuilder();
        StringBuilder tagNames = new StringBuilder();
        StringBuilder myTags = new StringBuilder();
        StringBuilder userTags = new StringBuilder();
        StringBuilder bindTag = new StringBuilder();
        StringBuilder popularTag = new StringBuilder();
        List<ItemTagsInfo> lstItemTags = AspxTagsController.GetItemTags(itemSKU, aspxCommonObj);
        foreach (ItemTagsInfo item in lstItemTags)
        {
            if (tagNames.ToString().IndexOf(item.Tag) == -1)
            {
                itemTags.Append(item.Tag + "(" + item.TagCount + "), ");
                tagNames.Append(item.Tag);
            }

            if (item.AddedBy == aspxCommonObj.UserName)
            {
                if (userTags.ToString().IndexOf(item.Tag) == -1)
                {
                    myTags.Append(item.Tag);
                    myTags.Append("<button type=\"button\" class=\"cssClassCross\" value=");
                    myTags.Append(item.ItemTagID);
                    myTags.Append(" onclick =ItemDetail.DeleteMyTag(this)><span>");
                    myTags.Append(getLocale("x"));
                    myTags.Append("</span></button>, ");

                    userTags.Append(item.Tag);
                }
            }

            bindTag.Append("$('#divItemTags').html('");
            bindTag.Append(itemTags.ToString().Substring(0, itemTags.Length - 2));
            bindTag.Append("');");
            if (myTags.Length > 2)
            {
                bindTag.Append("$('#divMyTags').html('");
                bindTag.Append(myTags.ToString().Substring(0, myTags.Length - 2));
                bindTag.Append("');");
            }
        }
        if (!String.IsNullOrEmpty(itemTags.ToString()))
        {
            popularTag.Append("<h2>");
            bindTag.Append(getLocale("PopularTags:"));
            bindTag.Append("");
            popularTag.Append("</h2><div id=\"divItemTags\" class=\"cssClassPopular-Itemstags\">");
            popularTag.Append(itemTags.ToString().Substring(0, itemTags.Length - 2));
            popularTag.Append("</div>");
            bindTag.Append("$('#popularTag').html('");
            bindTag.Append(popularTag);
            bindTag.Append("')");
        }
        string tag = GetScriptRun(bindTag.ToString());
        return tag;
    }

    public string BindDataInTab(string sku, int attrId, int itemTypeId, AspxCommonInfo aspxCommonObj)
    {
        List<AttributeFormInfo> frmItemAttributes = AspxItemMgntController.GetItemDetailsInfoByItemSKU(itemSKU, attrId,
                                                                                                       itemTypeId,
                                                                                                       aspxCommonObj);
        StringBuilder scriptBuilder = new StringBuilder();

        foreach (AttributeFormInfo item in frmItemAttributes)
        {
            string id = item.AttributeID + "_" + item.InputTypeID + "_" + item.ValidationTypeID + "_" + item.IsRequired +
                        "_" + item.GroupID
                        + "_" + item.IsIncludeInPriceRule + "_" + item.IsIncludeInPromotions + "_" + item.DisplayOrder;
            switch (item.InputTypeID)
            {
                case 1:
                    if (item.ValidationTypeID == 3)
                    {
                        if (item.AttributeValues != string.Empty)
                        {
                            scriptBuilder.Append(" $('#");
                            scriptBuilder.Append(id);
                            scriptBuilder.Append("').html('");
                            scriptBuilder.Append(Math.Round(decimal.Parse(item.AttributeValues), 2).ToString());
                            scriptBuilder.Append("');");
                        }
                        else
                        {
                            scriptBuilder.Append(" $('#");
                            scriptBuilder.Append(id);
                            scriptBuilder.Append("').html('");
                            scriptBuilder.Append(item.AttributeValues);
                            scriptBuilder.Append("');");
                        }

                        break;
                    }
                    else if (item.ValidationTypeID == 5)
                    {
                        scriptBuilder.Append(" $('#");
                        scriptBuilder.Append(id);
                        scriptBuilder.Append("').html('");
                        scriptBuilder.Append(item.AttributeValues);
                        scriptBuilder.Append("');");
                        break;
                    }
                    else
                    {

                        scriptBuilder.Append(" $(\"#");
                        scriptBuilder.Append(id);
                        scriptBuilder.Append("\").html(\"");
                        scriptBuilder.Append(item.AttributeValues);
                        scriptBuilder.Append("\");");
                        break;
                    }
                case 2:
                    scriptBuilder.Append(" $('#");
                    scriptBuilder.Append(id);
                    scriptBuilder.Append("').html(Encoder.htmlDecode('");
                    scriptBuilder.Append(item.AttributeValues);
                    scriptBuilder.Append("'));");
                    break;
                case 3:
                    scriptBuilder.Append(" $('#");
                    scriptBuilder.Append(id);
                    scriptBuilder.Append("').html('");
                    scriptBuilder.Append(Format_The_Date(item.AttributeValues));
                    scriptBuilder.Append("');");
                    break;
                case 4:
                    scriptBuilder.Append(" $('#");
                    scriptBuilder.Append(id);
                    scriptBuilder.Append("').html('");
                    scriptBuilder.Append(item.AttributeValues);
                    scriptBuilder.Append("');");
                    break;
                case 5:
                    scriptBuilder.Append(" $('#");
                    scriptBuilder.Append(id);
                    scriptBuilder.Append("').append('");
                    scriptBuilder.Append(item.AttributeValues);
                    scriptBuilder.Append(",');");
                    break;
                case 6:
                    scriptBuilder.Append(" $('#");
                    scriptBuilder.Append(id);
                    scriptBuilder.Append("').html('");
                    scriptBuilder.Append(item.AttributeValues);
                    scriptBuilder.Append("');");
                    break;
                case 7:
                    scriptBuilder.Append(" $('#");
                    scriptBuilder.Append(id);
                    scriptBuilder.Append("').html('");
                    scriptBuilder.Append(item.AttributeValues);
                    scriptBuilder.Append("');");
                    break;
                case 8:
                    scriptBuilder.Append("var div = $('#");
                    scriptBuilder.Append(id);
                    scriptBuilder.Append("');");
                    scriptBuilder.Append("var filePath = '");
                    scriptBuilder.Append(item.AttributeValues);
                    scriptBuilder.Append("';");
                    scriptBuilder.Append("var fileName = filePath.substring(filePath.lastIndexOf('/') + 1);");
                    scriptBuilder.Append("if (filePath != '') {");
                    scriptBuilder.Append("var fileExt = (-1 !== filePath.indexOf('.')) ? filePath.replace(/.*[.]/, '') : '';");
                    scriptBuilder.Append("myregexp = new RegExp('(jpg|jpeg|jpe|gif|bmp|png|ico)', 'i');");
                    scriptBuilder.Append("if (myregexp.test(fileExt)) {");
                    scriptBuilder.Append("$(div).append('<span class=\"response\"><img src=' + aspxRootPath + filePath + ' class=\"uploadImage\" /></span>')");
                    scriptBuilder.Append("} else {");

                    scriptBuilder.Append("$(div).append('<span class=\"response\"><span id=\"spanFileUpload\"  class=\"cssClassLink\"  href=' + 'uploads/' + fileName + '>' + fileName + '</span></span>');");
                    scriptBuilder.Append("}");
                    scriptBuilder.Append("}");
                    break;
                case 9:
                    scriptBuilder.Append(" $('#");
                    scriptBuilder.Append(id);
                    scriptBuilder.Append("').html('");
                    scriptBuilder.Append(item.AttributeValues);
                    scriptBuilder.Append("');");
                    break;
                case 10:
                    scriptBuilder.Append(" $('#");
                    scriptBuilder.Append(id);
                    scriptBuilder.Append("').html('");
                    scriptBuilder.Append(item.AttributeValues);
                    scriptBuilder.Append("');");
                    break;
                case 11:
                    scriptBuilder.Append(" $('#");
                    scriptBuilder.Append(id);
                    scriptBuilder.Append("').html('");
                    scriptBuilder.Append(item.AttributeValues);
                    scriptBuilder.Append("');");
                    break;
                case 12:
                    scriptBuilder.Append(" $('#");
                    scriptBuilder.Append(id);
                    scriptBuilder.Append("').html('");
                    scriptBuilder.Append(item.AttributeValues);
                    scriptBuilder.Append("');");
                    break;
                case 13:
                    scriptBuilder.Append(" $('#");
                    scriptBuilder.Append(id);
                    scriptBuilder.Append("').html('");
                    scriptBuilder.Append(item.AttributeValues);
                    scriptBuilder.Append("');");
                    break;
            }
        }
        string spt = GetScriptRun(scriptBuilder.ToString());
        return spt;
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

    private string GetScriptRun(string code)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type=\"text/javascript\">$(document).ready(function(){");
        sb.Append(code);
        sb.Append("});</script>");
        return sb.ToString();
    }

}