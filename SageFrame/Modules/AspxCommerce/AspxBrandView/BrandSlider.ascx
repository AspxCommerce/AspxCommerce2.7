<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BrandSlider.ascx.cs" Inherits="Modules_AspxCommerce_AspxBrandView_BrandSlider" %>
<div class="cssClassBrandWrapper" style="display:none;">
    <h2 class="cssClassMiddleHeader">
        <span id="lblBrands" class="sfLocale">Brands</span>
         <a href="#" class="cssRssImage" style="display: none">
            <img id="frontBrandRssImage" alt="" src="" title="" />
        </a>
    </h2>
    <div id='slide'>
        <asp:Literal id="litSlide" runat="server" EnableViewState="False"></asp:Literal>
    </div>
</div>
<div class="cssClassClear">
</div>
<script type="text/javascript">
    $(function() {
        $(this).BrandSlide({
            enableBrandRss: '<%=EnableBrandRss %>',
            brandRssPage: '<%=BrandRssPage %>'
        });
    });   
</script>