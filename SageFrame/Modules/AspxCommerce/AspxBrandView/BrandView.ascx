<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BrandView.ascx.cs" Inherits="Modules_AspxCommerce_AspxBrandView_BrandView" %>

<script type="text/javascript">
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxBrandView
        });
        $(this).BrandView({
            brandModulePath: '<%=BrandModulePath %>',
            enableBrandRss: '<%=EnableBrandRss %>',
            brandRssPage: '<%=BrandRssPage %>'
        });
    });
</script>

<div class="sfMainFeaturedBrands">
    <div class="sfInnerFaeturedLogo">
        <h2 class="cssClassMiddleHeader">
            <span class="sfLocale">Featured Brands</span>
             <a href="#" class="cssRssImage" style="display: none">
            <img id="featureBrandRssImage" alt="" src="" title="" />
        </a>
            </h2>
    </div>
    <div class="sfFeaturedBrands">
    </div>
</div>
<div id="divSlideWrapper" class="cssClassAllBrand">
    <h2 class="cssClassMiddleHeader">
        <span class="sfLocale ">All Brands</span>
         <a href="#" class="cssRssImage" style="display: none">
            <img id="allBrandRssImg" alt="" src="" title="" />
        </a>
        </h2>
    <br />
    <h1 align='center' class="cssBrandHeader sfLocale" style="display: none" class="sfLocale">
        A-D</h1>
    <ul id='a-d' class='slider1 mk-cf' style="display: none">
    </ul>
    <h1 align='center' class="cssBrandHeader sfLocale" style="display: none" class="sfLocale">
        E-L</h1>
    <ul id='e-l' class='slider1 mk-cf' style="display: none">
    </ul>
    <h1 align='center' class="cssBrandHeader sfLocale" style="display: none" class="sfLocale">
        M-P</h1>
    <ul id='m-p' class='slider1 mk-cf' style="display: none">
    </ul>
    <h1 align='center' class="cssBrandHeader sfLocale" style="display: none" class="sfLocale">
        Q-Z</h1>
    <ul id='q-z' class='slider1 mk-cf' style="display: none">
    </ul>
</div>
