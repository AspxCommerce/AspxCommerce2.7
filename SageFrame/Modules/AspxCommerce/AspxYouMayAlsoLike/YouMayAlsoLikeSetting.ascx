<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YouMayAlsoLikeSetting.ascx.cs" Inherits="Modules_AspxCommerce_AspxYouMayAlsoLike_YouMayAlsoLikeSetting" %>

<div class="cssYouMayAlsoLikeSetting">
    <table>
        <thead>
            <tr><td class="sfLocale">
                YouMayAlsoLike Item Settings</td></tr>
        </thead>
         <tr>
            <td>
                <asp:Label ID="lblEnableYouMayAlsoLike" runat="server" Text="Enable YouMayAlsoLike"></asp:Label>
            </td>
            <td>
                <input type="checkbox" id="chkEnableYouMayAlsoLike" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblYouMayAlsoLikeCount" runat="server" Text="Enter the Number of Products Displayed"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtYouMayAlsoLikeCount" />
            </td>
        </tr>  
         <tr>
            <td>
                <asp:Label ID="lblYouMayAlsoLikeInARow" runat="server" Text="Enter the Number of Products Dispalyed In Row"></asp:Label>
            </td>
            <td>
                <input type="text" id="txtYouMayAlsoLikeInARow" />
            </td>
        </tr>
        <tr>
            <td>
                <input type="button" id="btnYouMayAlsoLikeSettingSave" class="sfLocale sfbtn" value="Save" />
            </td>
        </tr>
    </table>
</div>

<script type="text/javascript"> 
 (function($) {
     $.YouMayAlsoLikeSettingView = function(param) {
         param = $.extend
        ({
            Settings: ''
        }, param);
         var p = $.parseJSON(param.Settings);
         function aspxCommonObj() {
             var aspxCommonInfo = {
                 StoreID: AspxCommerce.utils.GetStoreID(),
                 PortalID: AspxCommerce.utils.GetPortalID(),
                 CultureName: AspxCommerce.utils.GetCultureName()
             };
             return aspxCommonInfo;
         }
         var YouMayAlsoLikeSetting = {
             config: {
                 isPostBack: false,
                 async: false,
                 cache: false,
                 type: 'POST',
                 contentType: "application/json; charset=utf-8",
                 data: '{}',
                 dataType: 'json',
                 baseURL: p.YouMayAlsoLikeModulePath + "Services/AspxYouMayAlsoLikeServices.asmx/",
                 method: "",
                 url: "",
                 ajaxCallMode: ""
             },
             ajaxCall: function(config) {
                 $.ajax({
                     type: YouMayAlsoLikeSetting.config.type,
                     contentType: YouMayAlsoLikeSetting.config.contentType,
                     cache: YouMayAlsoLikeSetting.config.cache,
                     async: YouMayAlsoLikeSetting.config.async,
                     url: YouMayAlsoLikeSetting.config.url,
                     data: YouMayAlsoLikeSetting.config.data,
                     dataType: YouMayAlsoLikeSetting.config.dataType,
                     success: YouMayAlsoLikeSetting.config.ajaxCallMode,
                     error: YouMayAlsoLikeSetting.config.ajaxFailure
                 });
             },

             BindYouMayAlsoLikeSetting: function(data) {
                 $("#chkEnableYouMayAlsoLike").prop("checked", p.EnableYouMayAlsoLike);
                 $("#txtYouMayAlsoLikeCount").val(p.NoOfYouMayAlsoLikeItems);
                 $("#txtYouMayAlsoLikeInARow").val(p.NoOfYouMayAlsoLikeInARow);
             },
             YouMayAlsoLikeSettingUpdate: function() {
                 var isEnableYouMayAlsoLike = $("#chkEnableYouMayAlsoLike").prop("checked");
                 var YouMayAlsoLikeCount = $("#txtYouMayAlsoLikeCount").val();
                 var YouMayAlsoLikeInARow = $("#txtYouMayAlsoLikeInARow").val();
                 var settingKeys = "IsEnableYouMayAlsoLike*YouMayAlsoLikeCount*YouMayAlsoLikeInARow";
                 var settingValues = isEnableYouMayAlsoLike + " * " + YouMayAlsoLikeCount + " * " + YouMayAlsoLikeInARow;
                 var param = JSON2.stringify({ SettingValues: settingValues, SettingKeys: settingKeys, aspxCommonObj: aspxCommonObj() });
                 this.config.method = "YouMayAlsoLikeSettingUpdate";
                 this.config.url = this.config.baseURL + this.config.method;
                 this.config.data = param;
                 this.config.ajaxCallMode = YouMayAlsoLikeSetting.YouMayAlsoLikeSettingSuccess;
                 this.ajaxCall(this.config);
             },
             YouMayAlsoLikeSettingSuccess: function(data) {
                 SageFrame.messaging.show(getLocale(AspxYouMayAlsoLike, "Setting Saved Successfully"), "Success");
             },
             init: function() {
                 YouMayAlsoLikeSetting.BindYouMayAlsoLikeSetting();
                 $("#btnYouMayAlsoLikeSettingSave").click(function() {
                     YouMayAlsoLikeSetting.YouMayAlsoLikeSettingUpdate();
                 });
             }
         };
         YouMayAlsoLikeSetting.init();
     };
     $.fn.YouMayAlsoLikeSetting = function(p) {
         $.YouMayAlsoLikeSettingView(p);
     };
 })(jQuery);
 $(function() {
     $(".sfLocale").localize({
         moduleKey: AspxYouMayAlsoLike
     });
     $(this).YouMayAlsoLikeSetting({
         Settings: '<%=Settings %>'
     });
 });
</script>