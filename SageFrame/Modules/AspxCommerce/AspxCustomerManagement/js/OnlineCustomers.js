 var OnLinecustomers;
 $(function() {
     var aspxCommonObj = function() {
         var aspxCommonInfo = {
             StoreID: AspxCommerce.utils.GetStoreID(),
             PortalID: AspxCommerce.utils.GetPortalID(),
             UserName: AspxCommerce.utils.GetUserName()
         };
         return aspxCommonInfo;
     };
     OnLinecustomers = {
         config: {
             isPostBack: false,
             async: false,
             cache: false,
             type: "POST",
             contentType: "application/json; charset=utf-8",
             data: '{}',
             dataType: "json",
             baseURL: aspxservicePath + "AspxCoreHandler.ashx/",
             url: "",
             method: ""
         },
         init: function() {
             OnLinecustomers.SelectFirstTab();
             OnLinecustomers.bindRegisteredUserGrid(null, null, null);
             OnLinecustomers.bindAnonymousUserGrid(null, null);
             $("#btnSearchRegisteredUser").click(function() {
                 OnLinecustomers.SearchOnlineRegisteredUser();
             });
             $('#txtSearchUserName1,#txtSearchHostAddress1,#txtBrowserName1').keyup(function(event) {
                 if (event.keyCode == 13) {
                     OnLinecustomers.SearchOnlineRegisteredUser();
                 }
             });
             $("#btnSearchAnonymousUser").click(function() {
                 OnLinecustomers.SearchOnlineAnonymousUser();
             });
             $('#txtSearchHostAddress0,#txtBrowserName0').keyup(function(event) {
                 if (event.keyCode == 13) {
                     OnLinecustomers.SearchOnlineAnonymousUser();
                 }
             });
         },
         SelectFirstTab: function() {
             var $tabs = $('#container-7').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
             $tabs.tabs('option', 'active', 0);
         },
         bindRegisteredUserGrid: function(searchUsername, hostaddress, browser) {
             this.config.method = "GetRegisteredUserOnlineCount";
             var offset_ = 1;
             var current_ = 1;
             var perpage = ($("#gdvOnlineRegisteredUser_pagesize").length > 0) ? $("#gdvOnlineRegisteredUser_pagesize :selected").text() : 10;

             $("#gdvOnlineRegisteredUser").sagegrid({
                 url: this.config.baseURL,
                 functionMethod: this.config.method,
                 colModel: [
                     { display: getLocale(AspxCustomerManagement, "User Name"), name: 'user_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxCustomerManagement, "Session User Host Address"), name: 'hostaddress_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxCustomerManagement, "Session User Agent"), name: 'agent_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxCustomerManagement, "Session Browser"), name: 'browser_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxCustomerManagement, "Session URL"), name: 'url_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxCustomerManagement, "Start Time"), name: 'start_time', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' }
                 ],
                 rp: perpage,
                 nomsg: getLocale(AspxCustomerManagement, "No Records Found!"),
                 param: { searchUserName: searchUsername, searchHostAddress: hostaddress, searchBrowser: browser, aspxCommonObj: aspxCommonObj() },
                 current: current_,
                 pnew: offset_,
                 sortcol: {}
             });
         },
         bindAnonymousUserGrid: function(hostaddress, browser) {
             this.config.method = "GetAnonymousUserOnlineCount";
             var offset_ = 1;
             var current_ = 1;
             var perpage = ($("#gdvOnlineAnonymousUser_pagesize").length > 0) ? $("#gdvOnlineAnonymousUser_pagesize :selected").text() : 10;

             $("#gdvOnlineAnonymousUser").sagegrid({
                 url: this.config.baseURL,
                 functionMethod: this.config.method,
                 colModel: [
                                                     {display: getLocale(AspxCustomerManagement, "User Name"), name: 'user_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                     { display: getLocale(AspxCustomerManagement, "Session User Host Address"), name: 'hostaddress_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxCustomerManagement, "Session User Agent"), name: 'agent_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxCustomerManagement, "Session Browser"), name: 'browser_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                     { display: getLocale(AspxCustomerManagement, "Session URL"), name: 'url_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                                     {display: getLocale(AspxCustomerManagement, "Start Time"), name: 'start_time', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' }
                 ],
                 rp: perpage,
                 nomsg: getLocale(AspxCustomerManagement, "No Records Found!"),
                 param: { searchHostAddress: hostaddress, searchBrowser: browser, aspxCommonObj: aspxCommonObj() },
                 current: current_,
                 pnew: offset_,
                 sortcol: {}
             });
         },
         SearchOnlineAnonymousUser: function() {
             var HostAddress = $.trim($("#txtSearchHostAddress0").val());
             var Browser = $.trim($("#txtBrowserName0").val());

             if (HostAddress.length < 1) {
                 HostAddress = null;
             }
             if (Browser.length < 1) {
                 Browser = null;
             }
             OnLinecustomers.bindAnonymousUserGrid(HostAddress, Browser);
         },
         SearchOnlineRegisteredUser: function() {
             var SearchUserName = $.trim($("#txtSearchUserName1").val());
             var HostAddress = $.trim($("#txtSearchHostAddress1").val());
             var Browser = $.trim($("#txtBrowserName1").val());

             if (SearchUserName.length < 1) {
                 SearchUserName = null;
             }
             if (HostAddress.length < 1) {
                 HostAddress = null;
             }
             if (Browser.length < 1) {
                 Browser = null;
             }
             OnLinecustomers.bindRegisteredUserGrid(SearchUserName, HostAddress, Browser);
         }
     };
     OnLinecustomers.init();
 });