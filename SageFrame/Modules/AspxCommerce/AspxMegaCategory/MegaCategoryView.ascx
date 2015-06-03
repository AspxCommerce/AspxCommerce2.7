<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MegaCategoryView.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxMegaCategory_MegaCategoryView" %>

<script type="text/javascript">
    //<![CDATA[
    $(function () {
        var categoryRss = '<%=CategoryRss %>';
        var rssFeedUrl = '<%=RssFeedUrl %>';
        var modeOfView = '<%=modeOfView %>';
        var speed = '<%=speed %>';
        var direction = '<%=direction %>';
        var eventMega = '<%=eventMega %>';
        var effect = '<%=effect %>';
        var noOfColumn = '<%=noOfColumn %>';

        //$(".sfLocale").localize({
        //    moduleKey: AspxMegaCategory
        //});
        ResponsiveCategory();
        if (categoryRss.toLowerCase() == 'true') {
            LoadCategoryRssImage();
        }
        $(window).resize(function () {
            var windowsWidth = $(window).width();
            if (windowsWidth > 800) {
                $("#divMegaMenu").addClass("mega-menu");
                if (modeOfView == "vertical") {
                    $("#sf-Responsive-Cat").hide();
                    $("#divMegaCategoryContainer").show();
                    $('#divMegaMenu').dcVerticalMegaMenu({
                        rowItems: noOfColumn,
                        speed: speed,
                        effect: effect,
                        direction: direction
                    });
                }
                else if (modeOfView == "horizontal") {
                    $("#sf-Responsive-Cat").hide();
                    $("#mega-menuH").show();
                    $('#mega-menuH').dcMegaMenu({
                        rowItems: noOfColumn,
                        speed: speed,
                        effect: effect,
                        event: eventMega,
                        fullWidth: false
                    });
                }
                else {
                    $("#sf-Responsive-Cat").hide();
                    $('#divMegaMenu').dcVerticalMegaMenu({
                        rowItems: noOfColumn,
                        speed: speed,
                        effect: effect,
                        direction: direction
                    });
                    $("#divMegaCategoryContainer").show();
                    $('#divMegaMenu li').hide();
                    $('#divMegaMenu').mouseover(function () {
                        $('#divMegaMenu li').show();
                    });
                    $('#divMegaMenu').mouseout(function () {
                        $('#divMegaMenu li').hide();
                    });
                }
            }
            else {
                $("#mega-menuH").hide();
                $("#divMegaCategoryContainer").show();
                $("#sf-Responsive-Cat").show();
            }
        });

        function ResponsiveCategory() {
            var windowsWidth2 = $(window).width();
            if (windowsWidth2 > 800) {
                $("#divMegaMenu").addClass("mega-menu");
                if (modeOfView == "vertical") {
                    $("#divMegaCategoryContainer").show();
                    $('#divMegaMenu').dcVerticalMegaMenu({
                        rowItems: noOfColumn,
                        speed: speed,
                        effect: effect,
                        direction: direction
                    });
                }
                else if (modeOfView == "horizontal") {
                    $("#divMegaCategoryContainer").show();
                    $('#mega-menuH').dcMegaMenu({
                        rowItems: noOfColumn,
                        speed: speed,
                        effect: effect,
                        event: eventMega,
                        fullWidth: false
                    });
                }
                else {
                    $("#divMegaCategoryContainer").show();
                    $('#divMegaMenu').dcVerticalMegaMenu({
                        rowItems: noOfColumn,
                        speed: speed,
                        effect: effect,
                        direction: direction
                    });

                    $('#divMegaMenu li').hide();
                    $('#divMegaMenu').mouseover(function () {
                        $('#divMegaMenu li').show();
                    });
                    $('#divMegaMenu').mouseout(function () {
                        $('#divMegaMenu li').hide();
                    });
                }
            }
            else {
                $("#mega-menuH").hide();
                $("#divMegaCategoryContainer").show();
                $("#sf-Responsive-Cat").show();

            }
        }
        function LoadCategoryRssImage() {
                        var pageurl = aspxRedirectPath + rssFeedUrl + pageExtension;
            $('#categoryRssImage').parent('a').show();
            $('#categoryRssImage').parent('a').removeAttr('href').attr('href', pageurl + '?type=rss&action=category');
            $('#categoryRssImage').removeAttr('src').attr('src', aspxTemplateFolderPath + '/images/rss-icon.png');
            $('#categoryRssImage').removeAttr('title').attr('title', getLocale(AspxRssFeedLocale, "Category Rss Feed Title"));
            $('#categoryRssImage').removeAttr('alt').attr('alt', getLocale(AspxRssFeedLocale, "Category Rss Feed Alt"));
        }
        $("#sf-CatMenu").on("click", function () {
            $(".sf-CatContainer").slideToggle();
            $(this).toggleClass("active");
        });
                $("<strong class=\"catViewer\"><i class=\"i-plus\"></i>+</strong>").insertAfter($('.sf-CatContainer').find('li.parent>a'))

        $('.sf-CatContainer .catViewer>i').on("click", function () {

            var $parentLi = $(this).parents('li.parent:eq(0)');
            var goDown = $(this).hasClass('i-plus');
            if (goDown)
                $parentLi.find('ul').hide();
            else {

                $parentLi.find('ul:gt(1)').hide();
                $parentLi.find('ul:first').slideToggle();
              
                if (goDown) {
                    $(this).removeClass('i-plus').addClass('i-minus');
                } else {
                    $(this).removeClass('i-minus').addClass('i-plus');
                }
                $parentLi.find('li.parent .catViewer>i').removeClass('i-minus').addClass('i-plus');
                
                return;
            }


            if ($parentLi.hasClass('slidedown')) {
                $parentLi.find('ul:first').slideToggle();
            }
            else {
                $parentLi.addClass('slidedown').find('ul:first').slideToggle();
            }

            if (goDown) {
                $(this).removeClass('i-plus').addClass('i-minus');
            } else {
                $(this).removeClass('i-minus').addClass('i-plus');
            }
        });
    });

    //]]>
</script>

<div id="divMegaCategoryContainer" class="cssClassMegaCategoryWrapper" style="display: none;">
    <div id="divMegaCategory" class="cssClassMegaCategory" runat="server" enableviewstate="false">
    </div>
</div>
