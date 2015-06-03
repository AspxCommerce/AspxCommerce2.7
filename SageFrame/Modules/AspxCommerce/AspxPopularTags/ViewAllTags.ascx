<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewAllTags.ascx.cs" Inherits="Modules_AspxCommerce_AspxPopularTags_ViewAllTags" %>

<script type="text/javascript">
    //<![CDATA[
    $(function () {
        var popularTagsModulePath = "<%=PopularTagsModulePath %>";
        var viewTaggedItemPage = "<%=ViewTaggedItemPage %>";
        var popularTagsCount = "<%=PopularTagsCount %>";

        $(".sfLocale").localize({
            moduleKey: AspxPopularTags
        });

        var AspxCommonObj = function () {
            var aspxCommonObj = {
                StoreID: AspxCommerce.utils.GetStoreID(),
                PortalID: AspxCommerce.utils.GetPortalID(),
                CultureName: AspxCommerce.utils.GetCultureName(),
                UserName: AspxCommerce.utils.GetUserName(),
                CustomerID: AspxCommerce.utils.GetCustomerID()
            };
            return aspxCommonObj;
        };

        var AllTags = {
            BindAllTags: function () {
                $.ajax({
                    type: "POST",
                    url: popularTagsModulePath + "Services/PopularTagsWebService.asmx/GetAllPopularTags",
                    data: JSON2.stringify({ aspxCommonObj: AspxCommonObj(), count: popularTagsCount }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var length = msg.d.length;
                        if (length > 0) {                        
                            var popularTags = '';
                            var item;
                            for (var index = 0; index < length; index++) {
                                item = msg.d[index];
                                var totalTagCount = item.TagCount;
                                var fontSize = (totalTagCount / 10 < 1) ? totalTagCount / 10 + 1 + "em" : (totalTagCount / 10 > 2) ? "2em" : totalTagCount / 10 + "em";
                                                                popularTags += "<a href=' " + aspxRedirectPath + viewTaggedItemPage + pageExtension + '?tagsId=' + item.ItemTagIDs + "' title=" + getLocale(AspxPopularTags, 'See all pages tagged with') + item.Tag + "' style='font-size: " + fontSize + "'>" + item.Tag + "</a>(<span class=\"cssClassTagCloudCount\">" + totalTagCount + "</span>), ";
                            };
                            $("#divAllTags").html(popularTags.substring(0, popularTags.length - 2));
                        }
                        else {
                            $("#divAllTags").html(getLocale(AspxPopularTags, 'Not any items have been tagged yet!'));
                        }
                    }
                });
            }
        }
        $(".cssClassMasterLeft").html('');
        $("#divCenterContent").removeClass("cssClassMasterWrapperLeftCenter");
        $("#divCenterContent").addClass("cssClassMasterWrapperCenter");
        AllTags.BindAllTags();
    });

    //]]>
</script>

<div class="cssClassCommonSideBox">
    <h2>
        <asp:Label ID="lblAllTagsTitle" runat="server" Text="All Tags" 
            CssClass="cssClassPopularTags" meta:resourcekey="lblAllTagsTitleResource1"></asp:Label>
    </h2>
    <div id="divAllTags" class="cssClassPopularTags">
    </div>
</div>
