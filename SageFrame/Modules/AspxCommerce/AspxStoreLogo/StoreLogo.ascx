<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreLogo.ascx.cs" Inherits="Modules_AspxCommerce_AspxStoreLogo_StoreLogo" %>

<script type="text/javascript">
    //$(function() {
    //    $(".sfLocale").localize({
    //        moduleKey: AspxStoreLogo
    //    });
    //});
    //<![CDATA[
    var storeLogoImg = '<%=StoreLogoImg%>';
    var storeName='<%=StoreName %>'
    $(function() {
    $('#storeLogo').attr('src', aspxRootPath + storeLogoImg);
    $('#storeLogo').attr('title', storeName);
    $('#storeLogo').attr('alt', storeName);
		if (AspxCommerce.utils.IsUserFriendlyUrl()) {
		    $('.cssClassLogo > a').attr('href', AspxCommerce.utils.GetAspxRedirectPath() + 'Home' + pageExtension);
        } else {
            $('.cssClassLogo > a').attr('href', AspxCommerce.utils.GetAspxRedirectPath() + 'Home');
        }
    });
    //]]>
</script>

<div class="cssClassLogo">
    <a href="#">
        <img src="" id="storeLogo" alt="" title="" /></a>
</div>
