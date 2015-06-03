<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AllCoupons.ascx.cs" Inherits="Modules_Admin_DetailsBrowse_AllCoupons" %>

<script type="text/javascript">
    //<![CDATA[

    $(function() {
    
        $(".sfLocale").localize({
             moduleKey:DetailsBrowse
        });

        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName(),
            CustomerID: AspxCommerce.utils.GetCustomerID()           
        };
        var couponShowCount = 0;
        var couponList = new Array();
        var AllCoupon = {
            pageselectCallback: function(page_index, jq) {
                                var items_per_page = $('#ddlPageSize').val();
                var max_elem = Math.min((page_index + 1) * items_per_page, couponList.length);
                $("#divCouponList").html('');
                coupon = '';
                for (var i = page_index * items_per_page; i < max_elem; i++) {
                    AllCoupon.BindCouponListForDisplay(couponList[i]);
                    coupon += couponList[i].CouponID;
                }
                return false;
            }, Init: function() {
                AllCoupon.BindAllCouponList();
                $("#ddlPageSize").change(function() {
                    var optInit = AllCoupon.getOptionsFromForm();
                    $("#Pagination").pagination(couponList.length, optInit);
                });
            }, BindAllCouponList: function() {
                var coupon = '';
                $.ajax({
                    type: "POST",
                    url: aspxservicePath + "AspxCommerceWebService.asmx/GetCouponDetailListFront",
                    data: JSON2.stringify({ count: couponShowCount, aspxCommonObj: aspxCommonObj }),
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function(msg) {
                        couponList = [];
                        var length = msg.d.length;
                        if (length > 0) {
                            var item;
                            for (var index = 0; index < length; index++) {
                                item = msg.d[index];
                               AllCoupon.BindCouponList(item, index);
                            };
                            var optInit = AllCoupon.getOptionsFromForm();
                            $("#Pagination").pagination(couponList.length, optInit);
                            $("#divSearchPageNumber").show();
                        }
                        else {
                            $("#divSearchPageNumber").hide();
                            $("#divCouponList").html("<span class=\"cssClassNotFound\">"+getLocale(DetailsBrowse,"No Data Found!!")+"</span>");
                        }
                    }                   
                });
            }, BindCouponList: function(item, index) {
                if (coupon.indexOf(item.CouponID) == -1) {
                    coupon += item.CouponID;
                } couponList.push(item);
            }, BindCouponListForDisplay: function(item) {
                var htmlListt = "";
                htmlListt += '<ul class="couponList"><li><span>'+getLocale(DetailsBrowse,"Coupon Type: ")+'<span>' + item.CouponType + '</li>';
                htmlListt += '<li><span>' + getLocale(DetailsBrowse, "Coupon Code: ") + '<span>' + item.CouponCode + '</li>';
                htmlListt += '<li><span>'+getLocale(DetailsBrowse,"Amount: ")+'<span class="cssClassFormatCurrency">' + parseFloat(item.CouponAmount).toFixed(2) + '</li>';
                htmlListt += '<li><span>'+getLocale(DetailsBrowse,"Valid Till: ")+'<span>' + item.ValidateTo + '</li>';
                htmlListt += '</ul><br />';
                $("#divCouponList").append(htmlListt);
                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
            }, getOptionsFromForm: function() {
                var opt = { callback:AllCoupon.pageselectCallback };
                opt["items_per_page"] = $('#ddlPageSize').val();
                opt["prev_text"] = "Prev";
                opt["next_text"] = "Next";
                opt["prev_show_always"] = false;
                opt["next_show_always"] = false;
                return opt;
            }
        }
        $(".cssClassMasterLeft").html('');
        $("#divCenterContent").removeClass("cssClassMasterWrapperLeftCenter");
        $("#divCenterContent").addClass("cssClassMasterWrapperCenter");   
        AllCoupon.Init();
    });
   
//]]>
</script>

<div id="divCouponDetailFront">
    <div class="sfFormwrapper">
        <div class="couponlistheader">
            <h2>
                <asp:Label ID="lblWishHeading" runat="server" CssClass="sfLocale" 
                    Text="Available Coupon List" meta:resourcekey="lblWishHeadingResource1"></asp:Label></h2>
            <%-- <a class="btnPrevious" href="#">
                    <img alt="" src="<%=ResolveUrl("~/")%>Templates/AspxCommerce/images/admin/btnback.png" /></a>
                <a class="btnNext" href="#">
                    <img alt="" src="<%=ResolveUrl("~/")%>Templates/AspxCommerce/images/admin/imgforward.png" /></a>--%>
        </div>
        <div id="divCouponList">
        </div>
        <%-- <div>
            <p>
                <a href="#" class"btnSeeAllCoupon" onclick="SeeAllCoupon(0)">See all Coupons >></a>
                <a href="#" class"btnColapseAllCoupon" onclick="SeeAllCoupon(1)"> << </a>              
            </p>
        </div>--%>
    </div>
    <div class="cssClassPageNumber" id="divSearchPageNumber">
        <div class="cssClassPageNumberMidBg">
            <div id="Pagination">
            </div>
            <div class="cssClassViewPerPage">
                <h4 class="sfLocale">
                    View Per Page:
                </h4>
                <select id="ddlPageSize" class="sfListmenu">
                    <option value="8" class="sfLocale">8</option>
                    <option value="16" class="sfLocale">16</option>
                    <option value="24" class="sfLocale">24</option>
                    <option value="32" class="sfLocale">32</option>
                    <option value="40" class="sfLocale">40</option>
                    <option value="64" class="sfLocale">64</option>
                </select>
            </div>
        </div>
    </div>
</div>
