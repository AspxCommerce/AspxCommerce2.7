<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CouponReports.ascx.cs" Inherits="Modules_AspxCommerce_AspxCouponManagement_CouponReports" %>


<%@ Register src="CouponPerSalesManage.ascx" tagname="CouponPerSalesManage" tagprefix="uc1" %>
<%@ Register src="CouponPerUsersManage.ascx" tagname="CouponPerUsersManage" tagprefix="uc2" %>


<div class="cssCouponReports">
<ul>
<li><a href="#divCouponPerSale" class="sfLocale">Coupon Per Sales</a></li>
<li><a href="#divCouponPerUser" class="sfLocale">Coupon Per Customers</a></li>
</ul>
    <div class="cssCouponPerSale" id="divCouponPerSale">
        <uc1:CouponPerSalesManage ID="CouponPerSalesManage1" runat="server" />
    </div>
    <div class="cssCouponPerUser" id="divCouponPerUser">
        <uc2:CouponPerUsersManage ID="CouponPerUsersManage1" runat="server" />
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $(".cssCouponReports").tabs();
    });
</script>

