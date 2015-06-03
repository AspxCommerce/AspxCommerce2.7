<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AspxAPIEvent.ascx.cs" Inherits="Modules_AspxCommerce_AspxStartUpEvents_AspxAPIEvent" %>
<asp:Literal ID="litAPIjs" runat="server"></asp:Literal>
<script type="text/javascript">
 //<![CDATA[
    (function ($) {
        var moduleRedirectPath = "<%= ModuleRedirectPath%>";
        $(window).load(function () {
            Currency.format = 'money_format';
            var storeDefaultCurrency = '<%=StoreDefaultCurrency %>';
            var cookieCurrency = Currency.cookie.read();
            // When the page loads.            
            if (cookieCurrency == "" || cookieCurrency == null || cookieCurrency == storeDefaultCurrency) {
                Currency.currentCurrency = storeDefaultCurrency;
                Currency.convertAll(Currency.currentCurrency, storeDefaultCurrency);
            }
            else {
                Currency.currentCurrency = cookieCurrency;
                Currency.convertAll(storeDefaultCurrency, cookieCurrency);
            }
        });
    })(jQuery);
 //]]>
</script>
<script type="text/javascript">
    window.onload = function () {
        var html = '<div class="sfModule ResponsiveHtml"><div class="sfModulecontent clearfix"><div class="sfHtmlview"><div style="" id="responsiveIcon" class="cssTopHide"><a href="javascript:void(0);"><i class="i-menu"></i>Header</a></div></div></div><div class="sfClearDivTemp" style="clear:both"></div></div>';
        if ($("#sfLogin").find(".sfWrapper").find("div:first").hasClass("ResponsiveHtml")) {
        } else {
            $(html).insertBefore($("#sfLogin").find(".sfWrapper").find("div:first"));
        }

        $.fn.outside = function (ename, cb) {
            return this.each(function () {
                var $this = $(this),
                    self = this;

                $(document).bind(ename, function tempo(e) {
                    if (e.target !== self && !$.contains(self, e.target)) {
                        cb.apply(self, [e]);
                        if (!self.parentNode) $(document.body).unbind(ename, tempo);
                    }
                });
            });
        };
        var windowsWidth = $(window).width();
        if (windowsWidth <= 800) {
            $("#responsiveIcon").show();
            top_animate_hide();
        }
        else {
            $("#responsiveIcon").hide();
        }

        $(window).resize(function () {
            var windowsWidth = $(window).width();
            if (windowsWidth <= 800) {
                $("#responsiveIcon").show();
                top_animate_hide();

            }
            else {
                $("#responsiveIcon").hide();
            }
        });
        function top_animate_show() {
            $('#responsiveIcon').addClass('active');
            $('#sfToplinks').addClass('active');
            $('body').addClass('cssClassResTab');
            $('#responsiveIcon').removeClass("cssTopHide").addClass("cssTopShow");

        }
        function top_animate_hide() {
            $('#responsiveIcon').removeClass('active');
            $('#sfToplinks').removeClass('active');
            $('#responsiveIcon').removeClass("cssTopShow").addClass("cssTopHide");
            $('body').removeClass('cssClassResTab');
        }
        $("#responsiveIcon").on("click", function () {
            if ($(this).hasClass("cssTopHide")) {
                top_animate_show();
                return false;
            }
            else {
                top_animate_hide();
                return false;
            }
        });
        $('#sfToplinks').outside('click', function () {
            if ($("#responsiveIcon").is(":visible")) {
                top_animate_hide();
            }
        });
    };
</script>