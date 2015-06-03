(function ($) {
    $.createModuleManager = function (p) {
        p = $.extend
        ({
            ShowSideBar: 1,
            UserModuleID: 1
        }, p);
        var ModuleManager = {
            config: {
                isPostBack: false,
                async: true,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: SageFrameAppPath + '/Modules/Pages/services/ModuleManagerWebService.asmx/',
                method: "",
                url: "",
                categoryList: "",
                ajaxCallMode: 0, ///0 for get categories and bind, 1 for notification,2 for versions bind                                   
                arr: [],
                arrModules: [],
                PortalID: SageFramePortalID,
                AppPath: SageFrameAppPath,
                ActiveElement: "",
                TemplateName: SageFrameActiveTemplate,
                flag: 0,
                adminmode: 0,
                IsMobile: 0,
                Mode: 0, //0:Add 1:Edit
                UserModuleID: 0,
                IsHandheld: false,
                admincheck: 0,
                UserName: SageFrameUserName,
                PortalName: SageFramePortalName,
                ShowSideBar: p.ShowSideBar,
                nopopupflag: false,
                IsType3: false
            },
            messages: {
                nomodules: "No UserModules in this Portal"
            },
            init: function () {
                this.GetPages(false);
                SageFrame.tooltip.GetToolTip("imgToolTip1", "#lblInherit", "Inherits permission from the page directly");
                SageFrame.tooltip.GetToolTip("imgToolTip2", "#lblDonotshow", "Does not show this popup until a postback. <br/> Inherits permission from page");
                $('#imgMobileSwitch').tooltip({
                    showURL: false,
                    fade: 250,
                    track: true
                });
                $('#imgType3').tooltip({
                    showURL: false,
                    fade: 250,
                    track: true
                });
                $('#spnFix').tooltip({
                    bodyHandler: function () {
                        return "Make the Module Bar Floatable</br>By Unpinning it "
                    },
                    showURL: false,
                    fade: 250,
                    track: true
                });
                $('#chkDonotshow').prop("checked", ModuleManager.config.nopopupflag);
                $('#chkDonotshow').on("change", function () {
                    ModuleManager.config.nopopupflag = $(this).prop("checked");
                });
                //$('#divDroppable').height($('#sfOuterwrapper').height());
                $('#spnFix').bind("click", function () {
                    if (!$(this).hasClass("sfDocked")) {
                        $('#divFloat').removeAttr("style");
                        $('#spnFix').addClass("sfDocked").find("img").prop("src", SageFrame.utils.GetAdminImage("unpin.png"));
                        $.cookie("FloatStatus", "Pinned");
                    }
                    else {
                        InitModuleFloat($('#sidebar').width() + 25);
                        $('#divFloat').css("padding", "8px");
                        $('#spnFix').removeClass("sfDocked").find("img").prop("src", SageFrame.utils.GetAdminImage("pin.png"));
                        $.cookie("FloatStatus", "Floating");
                    }

                });
                $('#divIncludeModules').hide();
                ModuleManager.BindPageClickEvent();
                $('#showPopup').hide();
                $('#fade').on("click", function () {
                    $('#showPopup,#fade').fadeOut();
                    ModuleManager.config.Mode = 0;
                    ModuleManager.config.UserModuleID = 0;
                });
                $('#imgMobileSwitch').bind("click", function () {
                    $('#imgMobileSwitch').tooltip('destroy');
                    var self = $(this);
                    ModuleManager.config.IsType3 = false;
                    ModuleManager.config.IsHandheld = false;
                    ModuleManager.config.IsMobile = 0;
                    ModuleManager.LoadLayoutWireFrame();
                    $('#imgPCSwitch').removeClass('active');
                    $('#imgMobileSwitch').addClass('active');
                    $('.ChangeLayout').show();
                });

                $('#imgPCSwitch').bind("click", function () {
                    $('#imgPCSwitch').tooltip('destroy');
                    var self = $(this);
                    var imagepath = "";
                    ModuleManager.config.IsType3 = false;
                    ModuleManager.config.IsHandheld = true;
                    ModuleManager.config.IsMobile = 1;
                    ModuleManager.LoadLayoutWireFrame();
                    $('#imgPCSwitch').addClass('active');
                    $('#imgMobileSwitch').removeClass('active');
                    //                    $('#imgMobileSwitch').tooltip({
                    //                        showURL: false,
                    //                        fade: 250,
                    //                        track: true
                    //                    });
                    $('.ChangeLayout').hide();
                    $('#btnSavelayout').hide();
                    $('#btnLayoutCancel').hide();
                });


                $('#imgType3').bind("click", function () {
                    var self = $(this);
                    ModuleManager.config.IsType3 = true;
                    if ($(self).parent().hasClass("active")) {
                        $(self).parent().removeClass("active");
                        ModuleManager.config.IsHandheld = false;
                        ModuleManager.config.IsMobile = 0;
                        ModuleManager.LoadLayoutWireFrame();
                        ModuleManager.config.IsType3 = false;
                    }
                    else {
                        $(self).parent().addClass("active");
                        ModuleManager.config.IsHandheld = true;
                        ModuleManager.config.IsMobile = 1;
                        ModuleManager.LoadLayoutWireFrame();


                    }
                });
                $('#imgSearch').on("click", function () {
                    var SearchText = $('#txtSearchModules').val();
                    var isadmin = $('#rdbAdminModules').prop("checked") ? $('#chkPortalModules').prop("checked") ? false : true : false;
                    if (isadmin) {
                        ModuleManager.LoadAdminSearchModules();
                        //ModuleManager.LoadLayoutWireFrame();
                    }
                    else {
                        ModuleManager.LoadSearchModules();
                    }
                });
                $('#txtSearchModules').bind("keyup", function () {
                    var SearchText = $('#txtSearchModules').val();
                    var isadmin = $('#rdbAdminModules').prop("checked") ? $('#chkPortalModules').prop("checked") ? false : true : false;
                    if (isadmin) {
                        ModuleManager.LoadAdminSearchModules();
                        //ModuleManager.LoadLayoutWireFrame();
                    }
                    else {
                        ModuleManager.LoadSearchModules();
                    }
                });
                $('#rdbAdminModules').on("click", function () {

                    $('#chkPortalModules').prop('checked', false);
                    ModuleManager.config.adminmode = 1;
                    //ModuleManager.GetPages(true);

                    ModuleManager.LoadModules();
                    $('#layoutSwitch').slideUp();
                    $('#divIncludeModules').slideDown();
                });
                $('#rdbGenralModules').on("click", function () {
                    $('#chkPortalModules').prop('checked', false);
                    ModuleManager.config.adminmode = 0;
                    ModuleManager.config.admincheck = 0;
                    //ModuleManager.GetPages(false);
                    ModuleManager.LoadModules();
                    $('#layoutSwitch').slideDown();
                    $('#divIncludeModules').slideUp();
                });
                $('#chkPortalModules').on("click", function () {
                    if ($(this).prop("checked")) {
                        ModuleManager.config.adminmode = 0;
                        ModuleManager.config.admincheck = 1;
                        ModuleManager.LoadModules();
                    }
                    else {
                        ModuleManager.config.adminmode = 1;
                        ModuleManager.config.admincheck = 1;
                        ModuleManager.LoadModules();
                    }
                });
                $('#imgSort').bind("click", function () {
                    if (ModuleManager.config.flag == 0) {
                        var sortModules = ModuleManager.config.flag++;
                    }
                    else {
                        var sortModules1 = ModuleManager.config.flag--;
                    }
                    if ($(this).hasClass("icon-ascending-order")) {
                        $(this).removeClass("icon-ascending-order").addClass("icon-descending-order"); //.prop("src", SageFrame.utils.GetAdminImage("sort.png"));
                    }
                    else {
                        $(this).removeClass("icon-descending-order").addClass("icon-ascending-order").prop("src", SageFrame.utils.GetAdminImage("sort2.png"));
                    }
                    ModuleManager.LoadSortModules();
                });
                //$('#btnSearchUser').on("click", function() {
                //    if ($('#txtSearchUser').val() != "") {
                //        $('#divModuleUser table').html('');
                //        ModuleManager.SearchUsers();
                //        $('#txtSearchUser').parent().find("p.sfRequired").remove();
                //    }
                //    else {
                //        $('#txtSearchUser').parent().find("p.sfRequired").remove();
                //        $('#txtSearchUser').after("<p class='sfRequired'>Please enter search text</p>");
                //    }
                //});

                //$('#txtSearchUser').keyup(function(event) {
                //    if (event.keyCode == 13) {
                //        if ($('#txtSearchUser').val() != "") {
                //            $('#divModuleUser table').html('');
                //            ModuleManager.SearchUsers();
                //            $('#txtSearchUser').parent().find("p.sfRequired").remove();
                //        }
                //        else {
                //            $('#txtSearchUser').parent().find("p.sfRequired").remove();
                //            $('#txtSearchUser').after("<p class='sfRequired'>Please enter search text</p>");
                //        }
                //    }
                //});
                $('#rbAllPages').on("click", function () {
                    $('#trPages').slideUp();
                    $('#rbCustomPages').prop("checked", false);
                });
                $('#rbCustomPages').on("click", function () {
                    if ($(this).prop("checked")) {
                        $('#trPages').slideDown();
                        $('#rbAllPages').prop("checked", false);
                        var pageID = $('#hdnModuleInPage').val();
                        $('#hdnPageList').find('li#' + pageID).addClass('sfActive');
                        $('#pageTree_popup').find('li#' + pageID).addClass('sfActive');
                    }
                    else {
                        $('#trPages').slideUp();
                        $('#rbAllPages').prop("checked", false);
                    }
                });
                $('#spnBtnSave').on("click", function () {
                    var checks = $('div.divPermissions tr:gt(0), #divModuleUser tr').find('input.sfCheckbox:checked');
                    lstModulePermission = [];
                    $.each(checks, function (index, item) {
                        if ($(this).prop("checked")) {
                            if ($(this).closest('table').prop('id') == "tblModuleUser")
                                lstModulePermission[index] = { "PermissionID": $(this).prop('title') == "view" ? 1 : 2, "RoleID": null, "UserName": $(this).closest('tr').find('td:eq(0) label').html(), "AllowAccess": true };
                            else
                                lstModulePermission[index] = { "PermissionID": $(this).prop('title') == "view" ? 1 : 2, "RoleID": $(this).closest('tr').prop('id'), "UserName": "", "AllowAccess": true };
                        }
                    });
                    var _ModuleOrder = $(ModuleManager.config.ActiveElement).index();
                    _ModuleOrder = _ModuleOrder + 1;
                    var usermoduletitle = $('#txtModuleTitle').val() == "" ? $('#lblmoduleName').text() : $('#txtModuleTitle').val();
                    var pageId;
                    if ($('#categoryTree').find("li.active").find('.ui-tree-selected').find('span.true').length > 0) {
                        pageId = $('#categoryTree').find("li.active").find('.ui-tree-selected').find('span.true').parents('li').attr('id');
                    }
                    else {
                        pageId = $('#categoryTree li:eq(0)').attr('id');;
                    }

                    var UserModuleDetails = {
                        UserModule: {
                            UserModuleID: ModuleManager.config.UserModuleID,
                            ModuleDefID: parseInt(ModuleDefID),
                            UserModuleTitle: usermoduletitle,
                            AllPages: $('#rbAllPages').prop("checked") ? true : false,
                            ShowInPages: ModuleManager.GetSelectedPages(),
                            InheritViewPermissions: $('#chkInheritPermissions').prop("checked"),
                            IsActive: $('#chkIsActive').prop("checked") ? true : false,
                            SEOName: ModuleManager.GetSEOName(usermoduletitle),
                            PageID: pageId,
                            PaneName: $('#spnPaneName').text().toLowerCase(),
                            ModuleOrder: _ModuleOrder, CacheTime: "10",
                            Alignment: "test", Color: "black", Visibility: true,
                            PortalID: parseInt(ModuleManager.config.PortalID),
                            IsHandheld: ModuleManager.config.IsHandheld,
                            SuffixClass: ' ' + $('#txtModuleSuffix').val(),
                            HeaderText: $('#txtHeaderTxt').val(),
                            ShowHeaderText: $('#chkShowHeader').prop("checked"),
                            AddedBy: ModuleManager.config.UserName,
                            LSTUserModulePermission: lstModulePermission,
                            IsInAdmin: $('#rdbAdminModules').prop("checked") ? true : false
                        },
                        portalID: parseInt(SageFramePortalID),
                        userName: SageFrameUserName,
                        userModuleID: p.UserModuleID,
                        secureToken: SageFrameSecureToken
                    };
                    $(ModuleManager.config.ActiveElement).find("p.basics").text(usermoduletitle);
                    ModuleManager.config.method = ModuleManager.config.Mode == 0 ? "AddUserModule" : "UpdateUserModule";
                    ModuleManager.config.url = ModuleManager.config.baseURL + ModuleManager.config.method;
                    ModuleManager.config.data = JSON2.stringify(UserModuleDetails);
                    ModuleManager.config.ajaxCallMode = 5;
                    ModuleManager.ajaxCall(ModuleManager.config);
                    $('#rbAllPages').attr('checked', false);
                });
                $('#closeBtn').bind("click", function () {
                    $('#customLayout').fadeOut();
                });
                $('#selCount').bind("change", function () {
                    var divCount = $(this).val();
                    var splitClass = "divisions width100";
                    var splitControlClass = "divisionsTxt width100";
                    var controlClass = "width100";
                    var controlValue = "100%";
                    switch (parseInt(divCount)) {
                        case 1:
                            splitClass = "divisions width100";
                            splitControlClass = "divisionsTxt width100";
                            controlClass = "width100";
                            controlValue = "100%";
                            break;
                        case 2:
                            splitClass = "divisions width50";
                            splitControlClass = "divisionsTxt width50";
                            controlClass = "width50";
                            controlValue = "50%";
                            break;
                        case 3:
                            splitClass = "divisions width33";
                            splitControlClass = "divisionsTxt width33";
                            controlClass = "width33";
                            controlValue = "33%";
                            break;
                        case 4:
                            splitClass = "divisions width25";
                            splitControlClass = "divisionsTxt width25";
                            controlClass = "width25";
                            controlValue = "25%";
                            break;
                    }
                    var html = '';
                    var htmlControls = '';
                    for (var i = 0; i < divCount; i++) {
                        var newSplitClass = splitClass + " " + i;
                        var newSplitControlClass = splitControlClass + " " + i;
                        html += '<div class="' + newSplitClass + '" ></div>';
                        htmlControls += '<div class="' + newSplitControlClass + '"><input type="text" class="width100" value="' + controlValue + '"></div>';
                    }
                    $('#splitPreview').html('').append(html);
                    $('#splitControl').html('').append(htmlControls);
                });
                $('#splitControl div input').on("keyup", function () {
                    var self = $(this);
                    var inputs = $('#splitControl div input').not(this);
                    var currVal = $(this).val().substring(0, $(this).val().length - 1);
                    //change the current div
                    var CurUniqueVal = $(this).parent().prop("class");
                    var newClass = "divisions " + CurUniqueVal;
                    CurUniqueVal = CurUniqueVal.substring(CurUniqueVal.length - 1, CurUniqueVal.length);
                    var divSelector = '#splitPreview div.' + CurUniqueVal;
                    $(divSelector).prop("class", "").addClass(newClass).css("width", (currVal > 1 ? currVal - 2 : currVal) + '%');
                    var colCount = $('#selCount').val();
                    var increment = (100 - currVal) / (colCount - 1);
                    $.each(inputs, function (index, item) {
                        if (self != $(this)) {
                            $(this).val(increment + '%');
                            var uniqueVal = $(this).parent().prop("class");
                            newClass = "divisions " + uniqueVal;
                            uniqueVal = uniqueVal.substring(uniqueVal.length - 1, uniqueVal.length);
                            divSelector = '#splitPreview div.' + uniqueVal;
                            $(divSelector).prop("class", "").addClass(newClass).css("width", (increment > 3 ? increment - 3 : increment) + '%');
                        }
                    });
                });
                $('#applyBtn').bind("click", function () {
                    var currDiv = $(parentDiv).find("div.divContent");
                    var splits = $('#splitControl div input');
                    var html = '';
                    $.each(splits, function (index, item) {
                        var width = $(this).val().substring(0, $(this).val().length - 1);
                        width = parseInt(width);
                        width = width - 2;
                        width = width + '%';
                        html += '<div style="width:' + width + '" class="subLayout"></div>';
                    });
                    $(currDiv).html('').append(html);
                    $('#customLayout').fadeOut();
                });
                var $tabs = $('#dvTabPanel').tabs();

                // $tabs.tabs('select', 0);
                $tabs.tabs('option', 'selected');

                $('#main div.layout').on("mouseover", function () {
                    $('#main div span.spnControls').html('');
                    $(this).fadeIn("slow", function () {
                        $(this).addClass("activeDiv").find("span.spnControls").html('');
                    });
                });
                $('#main div.layout').on("mouseout", function () {
                    $(this).removeClass("activeDiv");
                });
                $('#main div.layout div.spnHeader span.spnControls').on("click", function (e) {

                    parentDiv = $(this).parent().parent();
                    //getting height and width of the message box
                    var height = $('#customLayout').height();
                    var width = $('#customLayout').width();
                    //calculating offset for displaying popup message
                    leftVal = e.pageX - width + "px";
                    topVal = e.pageY + "px";
                    //show the popup message and hide with fading effect
                    $('#customLayout').css({ left: leftVal, top: topVal }).fadeIn();
                });
                $('#pageTree_popup ').on("click", 'li', function () {
                    var id = $(this).prop("id");
                    var pageID = $('#hdnModuleInPage').val();
                    if (id != pageID) {
                        var realElem = $('#hdnPageList li[id=' + id + ']');
                        if ($(realElem).hasClass("sfActive")) {
                            $(realElem).removeClass("sfActive");
                            $(this).removeClass("sfActive");
                        }
                        else {
                            $(realElem).addClass("sfActive");
                            $(this).addClass("sfActive");
                        }
                    }
                    else {
                        $('#sf_lblConfirmation').text("You cannot unassign module from original page");
                        $("#dialog").dialog({
                            modal: true,
                            buttons: {
                                "Cancel": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });
                    }
                });
                $(document).on('click', 'div.sfCurve div.widget-instance  p.basics', function () {

                    ModuleManager.ClearPopUpControl();
                    var ids = new Array();
                    ids = $(this).parents("div").prop("id").split('_');
                    ModuleDefID = ids[2];
                    ModuleManager.config.Mode = 1;
                    ModuleManager.config.UserModuleID = ids[1];
                    $('#trPages').hide();
                    ModuleManager.LoadModuleDetails(ids[1]);
                    ModuleManager.ShowPopUp('showPopup', "Edit Module");
                    $('#dvTabPanel').find('ul').eq(0).find('li a').eq(0).trigger('click');
                    if ($('#rdbAdminModules').prop('checked')) {
                        $('#rbAllPages').hide();
                        $('#lblAll').hide();
                        $('#trheader').hide();
                        $('#trSuffix').hide();
                    }
                    else {
                        $('#lblAll').show();
                        $('#rbAllPages').show();
                        $('#trheader').show();
                        $('#trSuffix').show();
                    }
                });
                $('#spnBtnSaveModuleOrder').on("click", function () {
                    ModuleManager.UpdateModuleOrder();
                });
                $('#tblUser').on('click', 'i.delete', function () {
                    $(this).parents('tr').remove();
                });
                $('#spnBtnCancel').bind("click", function () {
                    SageFrame.popup.close("showPopup");
                    $('div.sfUnsavedmodule').fadeOut();
                });
                $('.ChangeLayout').on('change', '#ddlLayouts', function () {
                    ModuleManager.LoadLayoutWireFrameChange();
                    if ($('#ddlLayouts option:selected').hasClass("defaultLayout")) {
                        $('#btnSavelayout').hide();
                        $('#btnLayoutCancel').hide();
                    }
                    else {
                        $('#btnSavelayout').show();
                        $('#btnLayoutCancel').show();
                    }
                });
                $('#btnSavelayout').on('click', function () {
                    ModuleManager.SaveLayout();
                });
                $('#btnLayoutCancel').on('click', function () {
                    $('#btnSavelayout').hide();
                    $('#btnLayoutCancel').hide();
                    var toSelect = $('#ddlLayouts option.defaultLayout').text();
                    $('#ddlLayouts').val(toSelect);
                    ModuleManager.LoadLayoutWireFrameChange();
                });
                $('.sfSearchModules').on('click', function () {
                    $('#txtSearchModules').width('160');
                    $('#txtSearchModules').focus();
                });
                $('#txtSearchModules').on('click', function () {
                    $(this).width('160');
                });
                $('#txtSearchModules').on('blur', function () {
                    $(this).width('20');
                    $(this).val('');
                    var SearchText = $('#txtSearchModules').val();
                    var isadmin = $('#rdbAdminModules').prop("checked") ? $('#chkPortalModules').prop("checked") ? false : true : false;
                    if (isadmin) {
                        ModuleManager.LoadAdminSearchModules();
                        ModuleManager.LoadLayoutWireFrame();
                    }
                    else {
                        ModuleManager.LoadSearchModules();
                    }
                });
            },
            SaveLayout: function () {
                ModuleManager.config.method = "SavePresets";
                ModuleManager.config.url = ModuleManager.config.baseURL + ModuleManager.config.method;
                ModuleManager.config.data = JSON2.stringify({
                    activelayout: $('#ddlLayouts option:selected').text(),
                    pageName: $('#categoryTree').find("li.active").find('.ui-tree-selected').find('span.true').attr("pagename"),
                    templateName: ModuleManager.config.TemplateName,
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                ModuleManager.config.ajaxCallMode = 13;
                ModuleManager.ajaxCall(ModuleManager.config);
            },
            SaveUserModuleDefault: function () {
                var _ModuleOrder = $(ModuleManager.config.ActiveElement).index();
                _ModuleOrder = _ModuleOrder + 1;
                var pageId;
                if ($('#categoryTree').find("li.active").find('.ui-tree-selected').find('span.true').length > 0) {
                    pageId = $('#categoryTree').find("li.active").find('.ui-tree-selected').find('span.true').parents('li').attr('id');
                }
                else {
                    pageId = $('#categoryTree li:eq(0)').attr('id');
                }
                var UserModuleDetails = {
                    UserModule: {
                        UserModuleID: ModuleManager.config.UserModuleID,
                        ModuleDefID: parseInt(ModuleDefID),
                        UserModuleTitle: $('#lblmoduleName').text(),
                        AllPages: false,
                        ShowInPages: "", InheritViewPermissions: true,
                        IsActive: true,
                        SEOName: $('#lblmoduleName').text(),
                        PageID: pageId,
                        ModuleOrder: _ModuleOrder, CacheTime: "10",
                        PaneName: $('#spnPaneName').text().toLowerCase(),
                        Alignment: "test",
                        Color: "black",
                        Visibility: true,
                        PortalID: parseInt(ModuleManager.config.PortalID),
                        IsHandheld: ModuleManager.config.IsHandheld,
                        SuffixClass: "",
                        HeaderText: "",
                        ShowHeaderText: false,
                        AddedBy: ModuleManager.config.UserName,
                        LSTUserModulePermission: lstModulePermission,
                        IsInAdmin: $('#rdbAdminModules').prop("checked") ? true : false
                    },
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                };
                ModuleManager.config.method = "AddUserModule";
                ModuleManager.config.url = ModuleManager.config.baseURL + ModuleManager.config.method;
                ModuleManager.config.data = JSON2.stringify(UserModuleDetails);
                ModuleManager.config.ajaxCallMode = 5;
                ModuleManager.ajaxCall(ModuleManager.config);
            },
            InitEqualHeights: function () {
                var blocks = $('div.sfOuterwrapper');
                $.each(blocks, function () {
                    var heights = $(this).find("div.sfblocks");
                    var arrHeights = [];
                    $.each(heights, function () {
                        arrHeights.push($(this).height());
                    });
                    var max_height = Math.max.apply(null, arrHeights);
                    if (max_height < 40) max_height = 40;
                    $(this).find("div.sfblocks").css("min-height", max_height + "px");
                });
                var middlewrapper = $('#sfMainWrapper').height();
                var leftwrapper = $('#sfLeft').height();
                var rightwrapper = $('#sfRight').height();
                var arrHeights = [middlewrapper, leftwrapper, rightwrapper];
                var biggest = Math.max.apply(null, arrHeights);
                biggest = biggest < 200 ? 200 : biggest;
                //$('#sfMainWrapper .sfContainer,#sfLeft .sfContainer,#sfRight .sfContainer').css("height", biggest - 22 + "px");
                var lefttop = $('#sfLeft div.sfLeftTop').height() == null ? 0 : $('#sfLeft div.sfLeftTop').height() + 22 + 10;
                var leftbottom = $('#sfLeft div.sfLeftBottom').height() == null ? 0 : $('#sfLeft div.sfLeftBottom').height() + 22 + 10;
                var sfColsWrapLeft = biggest - (lefttop + leftbottom) - 40;
                $('#sfLeft div.sfColswrap').css("height", sfColsWrapLeft + "px");
                $('#sfLeft div.sfColswrap div.sfLeftA div.sfWrapper,#sfLeft div.sfColswrap div.sfLeftB div.sfWrapper').css("height", sfColsWrapLeft - 22 + "px");

                var righttop = $('#sfRight div.sfRightTop').height() == null ? 0 : $('#sfRight div.sfRightTop').height() + 22 + 10;
                var rightbottom = $('#sfRight div.sfRightBottom').height() == null ? 0 : $('#sfRight div.sfRightBottom').height() + 22 + 10;
                var sfColsWrapRight = biggest - (righttop + rightbottom) - 40;
                $('#sfRight div.sfColswrap').css("height", sfColsWrapRight + "px");
                $('#sfRight div.sfColswrap').css("height", sfColsWrapRight + "px");
                $('#sfRight div.sfColswrap div.sfRightA div.sfWrapper,#sfRight div.sfColswrap div.sfRightB div.sfWrapper').css("height", sfColsWrapRight - 22 + "px");
                //                var middletop = $('#sfMainWrapper div.sfMiddletop').height() == null ? 0 : $('#sfMainWrapper div.sfMiddletop').height();
                //                var middlebottom = $('#sfMainWrapper div.sfMiddlebottom ').height() == null ? 0 : $('#sfMainWrapper div.sfMiddlebottom ').height();
                //                var middlemaintop = $('#sfMainWrapper div.sfMiddlemaintop ').height() == null ? 0 : $('#sfMainWrapper div.sfMiddlemaintop ').height();
                //                var middlemainbottom = $('#sfMainWrapper div.sfMiddlemainbottom ').height() == null ? 0 : $('#sfMainWrapper div.sfMiddlemainbottom ').height();
                //                var sfColsWrapMiddle = biggest - (middletop + middlebottom) - 40;
                //                $('#sfMainWrapper div.sfMainContent').css("height", sfColsWrapMiddle + "px");
            },
            LoadModules: function () {
                this.config.method = ModuleManager.config.adminmode == 0 ? "GetAllGenralModules" : "GetAllAdminModules";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 0;
                this.ajaxCall(this.config);
            },
            BindModules: function (data) {
                var modules = data.d;
                var html = '';
                $.each(modules, function (index, item) {
                    if (item != "") {
                        var styleclass = item == 0 ? 'class="element hideconfig widget-class _current"' : 'class="element hideconfig widget-class"';
                        html += '<li ' + styleclass + ' id=' + item.ModuleDefID + ' style="">' + item.FriendlyName + '</li>';
                    }
                });
                if (modules.length == 0)
                    html = SageFrame.messaging.showdivmessage(ModuleManager.messages.nomodules);
                $('#divDroppable ul.hdnModulelist').html(html);
                ModuleManager.InitPaging();
                if (ModuleManager.config.admincheck == 0) {
                    ModuleManager.LoadLayoutWireFrame();
                }
                else {
                    ModuleManager.wreload();
                }
            },
            LoadSearchModules: function (data) {
                this.config.method = "GetAllSearchGenralModules";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    SearchText: $('#txtSearchModules').val(),
                    IsAdmin: false,
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 10;
                this.ajaxCall(this.config);
            },
            BindSearchModules: function (data) {
                var modules = data.d;
                var html = '';
                if (modules.length == 0) {
                    $('#divDroppable ul.alt_content').html("No Modules Found");
                }
                $.each(modules, function (index, item) {
                    if (item != "") {
                        html += '<li class="widget-class" id=' + item.ModuleDefID + ' style=""><p>' + item.FriendlyName + '</p></li>';
                    }
                });
                $('#divDroppable ul.hdnModulelist').html('').append(html);
                ModuleManager.InitPaging();
                ModuleManager.wreload();
            },
            LoadAdminSearchModules: function (data) {
                this.config.method = "GetAllSearchAdminModules";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    SearchText: $('#txtSearchModules').val(),
                    IsAdmin: true,
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 11;
                this.ajaxCall(this.config);
            },
            BindAdminSearchModules: function (data) {
                var modules = data.d;
                var html = '';
                $.each(modules, function (index, item) {
                    if (item != "") {
                        html += '<li class="widget-class" id=' + item.ModuleDefID + ' style=""><p>' + item.FriendlyName + '</p></li>';
                    }
                });
                $('#divDroppable ul.hdnModulelist').html('').append(html);
                ModuleManager.InitPaging();
            },
            LoadSortModules: function (data) {
                var isAdmin = "0";
                if ($('#rdbGenralModules').length == 0)
                    isAdmin = 0
                else if ($('#rdbAdmin').prop("checked"))
                    isAdmin = $("#chkPortalModules").prop("checked") == true ? "0" : "1";
                this.config.method = "GetSortModules";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    flag: ModuleManager.config.flag,
                    isAdmin: isAdmin,
                    IncludePortalModules: $("#chkPortalModules").prop("checked") == true ? 1 : 0,
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 12;
                this.ajaxCall(this.config);
            },
            BindSortModules: function (data) {
                var modules = data.d;
                var html = '';
                $.each(modules, function (index, item) {
                    if (item != "") {
                        html += '<li class="widget-class" id=' + item.ModuleDefID + ' style=""><p>' + item.FriendlyName + '</p></li>';
                    }
                });
                $('#divDroppable ul.hdnModulelist').html('').append(html);
                ModuleManager.InitPaging();
            },
            ModuleManagerInformation: function (data) {
                this.config.method = "GetModuleInformation";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    FriendlyName: 'HTML',
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            },
            BindModulesInformation: function (data) {
                var modules = data.d;
                var html = '';
                $.each(modules, function (index, item) {
                    html += '<td>' + item.ModuleName + '</td>' + '<td>' + item.Description + '</td>' + '<td>' + item.Version + '</td>';
                });
                $('#dvInformation table tr').append(html);
            },
            SaveModuleOrder: function () {
                this.config.method = "AddModuleOrder";
                this.config.url = this.config.baseURL + this.config.method;
                var moduleOrder = dropStatus === 0 ? singlemodule : droppableOrder;
                this.config.data = JSON2.stringify({
                    ModuleOrder: moduleOrder,
                    ModuleID: 'ModuleID',
                    ModuleName: ModuleName,
                    PaneName: droppableAttr,
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },
            InitPaging: function () {
                var optInit = {
                    items_per_page: 10,
                    num_display_entries: 4,
                    current_page: 0,
                    num_edge_entries: 2,
                    link_to: "#",
                    prev_text: "<",
                    next_text: ">",
                    ellipse_text: "...",
                    prev_show_always: true,
                    next_show_always: true,
                    callback: ModuleManager.moduleselectCallback
                };
                var members = $('ul.hdnModulelist li');
                $("#Pagination").pagination(members.length, optInit);
                ModuleManager.wreload();
            },
            moduleselectCallback: function (page_index, jq) {
                // Get number of elements per pagionation page from form
                var items_per_page = 10;
                var members = $('#divDroppable ul.hdnModulelist li');
                var max_elem = Math.min((page_index + 1) * items_per_page, members.length);
                var newcontent = '';
                var modules = $('#divDroppable ul.hdnModulelist li');
                var i = 0;
                $.each(modules, function () {
                    if (i < max_elem && i >= page_index * items_per_page) {
                        newcontent += '<li class="element hideconfig widget-class" id=' + $(this).prop("id") + '>' + $(this).text() + '</li>';
                    }
                    i++;
                });
                $('#divDroppable ul.alt_content').empty().html(newcontent);
                ModuleManager.wreload();
                // Prevent click eventpropagation
                return false;
            },
            InitPagingPages: function () {
                var optInit = {
                    items_per_page: 5,
                    num_display_entries: 4,
                    current_page: 0,
                    num_edge_entries: 2,
                    link_to: "#",
                    prev_text: "<",
                    next_text: ">",
                    ellipse_text: "...",
                    prev_show_always: true,
                    next_show_always: true,
                    callback: ModuleManager.pageselectCallback
                };
                var members = $('#hdnPageList li');
                $("#pagePagination").pagination(members.length, optInit);
                ModuleManager.wreload();
            },
            pageselectCallback: function (page_index, jq) {
                // Get number of elements per pagionation page from form
                var items_per_page = 5;
                var members = $('#hdnPageList li');
                var max_elem = Math.min((page_index + 1) * items_per_page, members.length);
                var newcontent = '';
                var modules = $('#hdnPageList li');
                var i = 0;
                $.each(modules, function () {
                    if (i < max_elem && i >= page_index * items_per_page) {
                        newcontent += '<li id=' + $(this).prop("id") + '>' + $(this).text() + '</li>';
                    }
                    i++;
                });
                $('#pageTree_popup').empty().html(newcontent);
                ModuleManager.ReSelectPages();
                // Prevent click eventpropagation
                return false;
            },
            ReSelectPages: function () {
                var pages = $('#pageTree_popup li');
                $.each(pages, function () {
                    var id = $(this).prop("id");
                    var $realElem = $('#hdnPageList li[id=' + id + ']');
                    if ($realElem.hasClass("sfActive")) {
                        $(this).addClass("sfActive");
                    }
                });
            },
            GetPages: function (isadmin) {
                if (!isadmin) {
                    ModuleManager.config.url = ModuleManager.config.AppPath + '/Modules/Admin/MenuManager/MenuWebService.asmx/GetNormalPage';
                    ModuleManager.config.data = JSON2.stringify({
                        CultureCode: 'en-US',
                        PortalID: parseInt(SageFramePortalID),
                        UserName: SageFrameUserName,
                        userModuleID: p.UserModuleID,
                        secureToken: SageFrameSecureToken
                    });
                }
                else {
                    ModuleManager.config.url = ModuleManager.config.AppPath + '/Modules/Admin/MenuManager/MenuWebService.asmx/GetAdminPage';
                    ModuleManager.config.data = JSON2.stringify({
                        CultureCode: 'en-US',
                        portalID: parseInt(SageFramePortalID),
                        userName: SageFrameUserName,
                        userModuleID: p.UserModuleID,
                        secureToken: SageFrameSecureToken
                    });
                }
                this.config.ajaxCallMode = 6;
                this.ajaxCall(this.config);
            },
            GetPageList_PopUp: function () {
                var isAdmin = $('#rdbAdminModules').prop("checked") ? 1 : 0;
                this.config.url = isAdmin == 0 ? ModuleManager.config.AppPath + '/Modules/Admin/MenuManager/MenuWebService.asmx/GetNormalPage'
                                : ModuleManager.config.AppPath + '/Modules/Admin/MenuManager/MenuWebService.asmx/GetAdminPage';
                this.config.data = JSON2.stringify({
                    CultureCode: 'en-US',
                    PortalID: parseInt(SageFramePortalID),
                    UserName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 8;
                this.ajaxCall(this.config);
            },
            BindPageList_PopUp: function (data) {
                var pages = data.d;
                var PageID = "";
                var parentID = "";
                var PageLevel = 0;
                var itemPath = "";
                var html = "";
                $.each(pages, function (index, item) {
                    PageID = item.PageID;
                    parentID = item.ParentID;
                    categoryLevel = item.Level;
                    if (item.Level == 0) {
                        html += '<li id=' + PageID + '>';
                        html += item.PageName;
                        if (item.ChildCount > 0) {
                            itemPath += item.PageName;
                            html += ModuleManager.BindChildCategory_PopUp(pages, PageID);
                        }
                        html += "</li>";
                    }
                    itemPath = '';
                });
                $('#hdnPageList').html(html);
                var pageList = $('#hdnPageList li');
                html = '';
                var PageArr = [];
                $.each(pageList, function (index, item) {
                    var self = $(this);
                    PageArr.push('<li id=' + $(self).prop("id") + '>' + $(self).text() + '</li>');
                });
                $('#hdnPageList').html(PageArr.join(''));
                ModuleManager.InitPagingPages();
            },
            BindChildCategory_PopUp: function (response, PageID) {
                var strListmaker = '';
                var childNodes = '';
                var path = '';
                var itemPath = "";
                itemPath += "";
                $.each(response, function (index, item) {
                    if (item.Level > 0) {
                        if (item.ParentID == PageID) {
                            itemPath += item.PageName;
                            var prefix = ModuleManager.GetPrefixes(item.Level);
                            strListmaker += '<li id=' + item.PageID + '>' + prefix + item.PageName;
                            childNodes = ModuleManager.BindChildCategory(response, item.PageID);
                            itemPath = itemPath.replace(itemPath.lastIndexOf(item.AttributeValue), '');
                            if (childNodes != '') {
                                strListmaker += childNodes;
                            }
                            strListmaker += "</li>";
                        }
                    }
                });
                return strListmaker;
            },
            BindPageList_DropDown: function (data) {
                var parentPages = data.d;
                var html = '';
                $.each(parentPages, function (index, item) {
                    html += '<option value=' + item.PageID + '>' + item.PageName.replace(new RegExp("-", "g"), ' ') + '</option>';
                });
                $('#ddlPages').html('').html(html);
                ModuleManager.LoadModules();
            },
            BindChildCategory_DropDown: function (response, PageID) {
                var strListmaker = '';
                var childNodes = '';
                var path = '';
                var itemPath = "";
                itemPath += "";
                $.each(response, function (index, item) {
                    if (item.Level > 0) {
                        if (item.ParentID == PageID) {
                            itemPath += item.PageName;
                            var prefix = ModuleManager.GetPrefixes(item.Level);
                            strListmaker += '<option value=' + item.PageID + '>' + prefix + item.PageName + '</option>';
                            childNodes = ModuleManager.BindChildCategory(response, item.PageID);
                            itemPath = itemPath.replace(itemPath.lastIndexOf(item.AttributeValue), '');
                            if (childNodes != '') {
                                strListmaker += '<option>' + childNodes + '</option>';
                            }
                        }
                    }
                });
                return strListmaker;
            },
            BindChildCategory: function (response, PageID) {
                var strListmaker = '';
                var childNodes = '';
                var path = '';
                var itemPath = "";
                itemPath += "";
                $.each(response, function (index, item) {
                    if (item.Level > 0) {
                        if (item.ParentID == PageID) {
                            itemPath += item.PageName;
                            var prefix = ModuleManager.GetPrefixes(item.Level);
                            strListmaker += '<li id=' + item.PageID + '>' + prefix + item.PageName;
                            childNodes = ModuleManager.BindChildCategory(response, item.PageID);
                            itemPath = itemPath.replace(itemPath.lastIndexOf(item.AttributeValue), '');
                            if (childNodes != '') {
                                strListmaker += childNodes;
                            }
                            strListmaker += "</li>";
                        }
                    }
                });
                return strListmaker;
            },
            BindPageClickEvent: function () {
                //////$('#navContainer').on('click', '#categoryTree span.PageName', function() {
                $('#navContainer').on('click', '#categoryTree li', function (e) {
                    var $activePage = $(this);
                    //////$activePage.parents('li').siblings('.active').removeClass('active').end().addClass('active');
                    //////var pageId = $activePage.parents('li').attr('id');
                    //////var pname = $activePage.text().trim();
                    $('#categoryTree li').removeClass('active');
                    $('#categoryTree li span').removeClass('ui-tree-selected');
                    //alert($activePage.attr('id'));//find('span.ui-tree-title').eq(0).attr('class'));
                    //$activePage.siblings('.active').removeClass('active').end().addClass('active');

                    //$activePage.find('span.ui-tree-title').eq(0).removeClass('ui-tree-title');
                    if ($(this).hasClass('sfChild')) {
                        $activePage.parents('li').addClass('active');
                        $activePage.siblings('.active').removeClass('active');
                    }
                    else {
                        $activePage.siblings('.active').removeClass('active').end().addClass('active');
                    }
                    var pageId = $activePage.attr('id');
                    //var pname = $('#categoryTree').find('li.active').find('span.true').attr("pagename");
                    var pname = $activePage.find('span.true').attr("pagename");
                    var pagename = SageFrame.utils.GetPageSEOName(pname);
                    var isAdmin = $('#rdbAdminModules').prop("checked") ? 1 : 0;
                    var url = isAdmin == 0 ? (ModuleManager.config.IsMobile == 0 ? ModuleManager.config.baseURL + "LoadActiveLayout" : ModuleManager.config.IsType3 == true ? ModuleManager.config.baseURL + "LoadType3Layout" : ModuleManager.config.baseURL + "LoadHandheldLayout") : ModuleManager.config.baseURL + "LoadAdminLayout";
                    $.ajax({
                        type: ModuleManager.config.type,
                        contentType: ModuleManager.config.contentType,
                        cache: ModuleManager.config.cache,
                        url: url,
                        data: JSON2.stringify({
                            PageName: pagename,
                            TemplateName: ModuleManager.config.TemplateName,
                            portalID: parseInt(SageFramePortalID),
                            userName: SageFrameUserName,
                            userModuleID: p.UserModuleID,
                            secureToken: SageFrameSecureToken
                        }),
                        dataType: ModuleManager.config.dataType,
                        success: function (data) {
                            var layout = data.d;
                            $('#divLayoutWireframe').html(layout);
                            ModuleManager.LoadPageModules();
                            ModuleManager.GetActiveLayoutName(pagename);
                            var isPublish = false;
                            var previewCode = false;
                            isPublish = $activePage.find('.ui-tree-selected').eq(0).find('span.true').attr('ispublish');
                            previewCode = $activePage.find('.ui-tree-selected').eq(0).find('span.true').attr('preview');
                            if (isPublish == 'true') {
                                $('#btnpublish').attr("value", "Unpublish").attr("name", "false").addClass('icon-checked').removeClass('icon-unchecked');
                            } else {
                                $('#btnpublish').attr("value", "Publish").attr("name", "true").addClass('icon-unchecked').removeClass('icon-checked')
                            }
                            $('#btnPreview').attr('name', previewCode);
                        },
                        error: ModuleManager.ajaxFailure
                    });
                    $('#imbPageCancel').click();
                    $activePage.find('span.ui-tree-title').eq(0).addClass('ui-tree-selected');
                    e.stopPropagation();
                });

            },
            GetPrefixes: function (level) {
                var prefix = "";
                for (var i = 0; i < level; i++) {
                    prefix += "---";
                }
                return prefix;
            },
            LoadRoles: function () {
                this.config.method = "GetPortalRoles";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.async = false;
                this.config.data = JSON2.stringify({
                    portalID: ModuleManager.config.PortalID,
                    userName: ModuleManager.config.UserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            },
            BindPortalRoles: function (data) {
                var html = '';
                $('div.divPermissions table tr:gt(0)').remove();
                $.each(data.d, function (index, item) {
                    var accesscontrolled = item.RoleName.toLowerCase() === "superuser" || item.RoleName.toLowerCase() === "super user" ? 'checked="checked" disabled="true"' : "";
                    var rowStyle = index % 2 == 0 ? 'class="sfEven"' : 'class="sfOdd"';
                    html += '<tr ' + rowStyle + ' id=' + item.RoleID + '><td width="40%"><label>' + item.RoleName + '</label></td><td width="20%"><input type="checkbox" ' + accesscontrolled + ' class="sfCheckbox" title="view" /></td>';
                    html += '<td width="20%"><input type="checkbox" ' + accesscontrolled + ' class="sfCheckbox" title="edit"/></td><td width="10%">&nbsp;</td></tr>';
                });
                $('div.divPermissions table').append(html);
                ModuleManager.GetPageList_PopUp();
            },
            //SearchUsers: function() {
            //    this.config.method = "SearchUsers";
            //    this.config.url = this.config.baseURL + this.config.method;
            //    this.config.data = JSON2.stringify({
            //        SearchText: $('#txtSearchUser').val(),
            //        portalID: parseInt(SageFramePortalID),
            //        userName: SageFrameUserName,
            //        userModuleID: p.UserModuleID,
            //        secureToken: SageFrameSecureToken
            //    });
            //    this.config.ajaxCallMode = 4;
            //    this.ajaxCall(this.config);
            //},
            BindUsers: function (data) {
                var html = '';
                if (data.d.length > 0)
                    html += '';
                $.each(data.d, function (index, item) {
                    if (!ModuleManager.UserAlreadyExists(item.UserName)) {
                        var rowStyle = index % 2 == 0 ? 'class="sfEven"' : 'class="sfOdd"';
                        html += '<tr ' + rowStyle + '><td width="40%"><label>' + item.UserName + '</label></td><td width="20%"><input type="checkbox" class="sfCheckbox" title="view"/></td>';
                        html += '<td width="20%"><input type="checkbox" class="sfCheckbox" title="edit"/></td>';
                        html += '<td width="10%"><i class="delete icon-delete" id="imgDelete"></i></td></tr>';
                    }
                });
                $('#divModuleUser table').append(html).show();
            },
            UserAlreadyExists: function (username) {
                var Exists = false;
                var existingUsers = $('#tblUser tr');
                $.each(existingUsers, function () {
                    if ($(this).find("td:first label").text() == username) {
                        Exists = true;
                    }
                });
                return Exists;
            },
            GetSelectedPages: function () {
                var selecteds = $('#hdnPageList li.sfActive');
                var pageArray = new Array();
                if (!$('#rbAllPages').prop("checked")) {
                    $.each(selecteds, function () {
                        pageArray.push($(this).prop("id"));
                    });
                    var pageID = $('#categoryTree').find("li.active").find('.ui-tree-selected').find('span.true').parents('li').attr('id');
                    var found = $.inArray(pageID, pageArray) > -1;
                    if (!found) {
                        pageArray.push(pageID);
                    }
                }
                return pageArray.join();
            },
            GetSEOName: function (word) {
                word = word.replace(" ", "_");
                return word;
            },
            LoadPageModules: function () {
                var pageID = 0;
                if ($('#categoryTree').find("li.active").find('.ui-tree-selected').find('span.true').length > 0) {
                    pageID = $('#categoryTree').find("li.active").find('.ui-tree-selected').find('span.true').parents('li').attr('id');
                }
                else {
                    pageID = $('#categoryTree li:eq(0)').attr('id');;
                }
                //comparing to undefined
                //because  sometime Tree DOM is not created while switching between Pages
                var isHandheld = $('#layoutSwitch #imgMobileSwitch').hasClass('active') ? false : true;
                if (pageID != undefined) {
                    this.config.method = "GetPageModules";
                    this.config.url = this.config.baseURL + this.config.method;
                    this.config.data = JSON2.stringify({
                        PageID: pageID,
                        IsHandheld: isHandheld,
                        portalID: parseInt(SageFramePortalID),
                        userName: SageFrameUserName,
                        userModuleID: p.UserModuleID,
                        secureToken: SageFrameSecureToken
                    });
                    this.config.ajaxCallMode = 7;
                    this.ajaxCall(this.config);
                }
            },
            BindPageModules: function (data) {
                var modules = data.d;
                var positiondiv = $('div.sfPosition');
                var controlIconsArr = [
                  ModuleManager.config.AppPath + "/Administrator/Templates/Default/images/edit-icon.png",
                  ModuleManager.config.AppPath + "/Modules/ModuleManager/images/imgsetting.png",
                  ModuleManager.config.AppPath + "/Administrator/Templates/Default/images/delete.png"
                ];
                $.each(positiondiv, function () {
                    var self = $(this);
                    var html = "";
                    $.each(modules, function (index, item) {
                        var usermoduleid = "umod_" + item.UserModuleID + "_" + item.ModuleDefID;
                        //                        var url = ModuleManager.config.PortalID > 1 ? ModuleManager.config.AppPath + "/portal/" + ModuleManager.config.PortalName + "/Admin/ControlEditor.aspx?uid=" + item.UserModuleID
                        //                                : ModuleManager.config.AppPath + "/Admin/ControlEditor.aspx?uid=" + item.UserModuleID;
                        if (item.PaneName != null && (jQuery.trim(item.PaneName.toLowerCase()) === jQuery.trim($(self).text()))) {

                            html += '<div id=' + usermoduleid + ' class="widget-instance">';
                            html += '<p class="basics">' + item.UserModuleTitle.substring(0, 14) + '</p><p style="display:none">' + item.UserModuleTitle + '</p>';
                            html += '<span class="control">';
                            if (item.ControlsCount > 1)
                                html += '<i class="sfManageControl icon-edit"></i>';
                            html += '<i class="delete icon-delete"></i>';
                            html += '</span>';
                            html += '<div style="clear: both;">';
                            html += '</div>';
                            html += '</div>';
                        }
                    });
                    $(self).next("div").html(html);
                    $('p.basics').tooltip({
                        bodyHandler: function () {
                            return $(this).next("p").text()
                        },
                        showURL: false,
                        fade: 250,
                        track: true
                    });
                });
                var widgets = $('li.widget-instance');
                $.each(widgets, function (index, item) {
                    var pwidth = $(this).find("p").width();
                    var controlwidth = $(this).find("span.control").width();
                    if ($(this).width() < (pwidth + controlwidth)) {
                        $(this).addClass("sfNarrow");
                    }
                }
              );
                $('li.sfNarrow span.control').hide();
                ModuleManager.wreload();
                ModuleManager.InitUserModuleDeletion();
                ModuleManager.InitUserModuleEdition();
                var floatstatus = $.cookie("FloatStatus");
                if (floatstatus != "Pinned") {
                    ModuleManager.InitFloatableModules();
                }
                else {
                    $('#spnFix img').attr("src", "../Modules/ModuleManager/images/unpin.png");
                }
                ModuleManager.InitAutoWidth();
            },
            InitFloatableModules: function () {
                if (ModuleManager.config.ShowSideBar == "1") {
                    if (SidebarMgr.config.SidebarMode == "1") {
                        InitModuleFloat(230);
                    }
                    else {
                        InitModuleFloat(65);
                    }
                }
                else {
                    InitModuleFloat(25);
                }
            },
            InitAutoWidth: function () {
                var width = 0;
                $('li.sfNarrow').hover(function () {
                    var pwidth = $(this).find("p").width();
                    var controlwidth = $(this).find("span.control").width();
                    var total = pwidth + controlwidth + 20 + "px";
                    width = $(this).width() + "px";
                    $(this).stop(false, true).animate({ width: total }, 100, function () {
                        $(this).find("span.control").slideDown("fast");
                    });
                },
                                     function () {
                                         $(this).find("span.control").hide();
                                         $(this).stop(false, true).animate({ width: width }, 100, function () {
                                             $(this).find("span.control").hide();
                                         });

                                     });
            },
            ajaxSuccess: function (data) {
                switch (ModuleManager.config.ajaxCallMode) {
                    case 0:
                        ModuleManager.BindModules(data);
                        break;
                    case 1:
                        var id = "mod_" + data.d;
                        $(uniqueelem).prop("id", id);
                        break;
                    case 2:
                        ModuleManager.BindModulesInformation(data);
                        break;
                    case 3:
                        ModuleManager.BindPortalRoles(data);
                        break;
                    case 4:
                        //ModuleManager.BindUsers(data);
                        break;
                    case 5:
                        if (ModuleManager.config.Mode == 0) {
                            var arrModuleData = data.d.split('_');
                            ModuleManager.config.ActiveElement.attr("id", "umod_" + arrModuleData[0] + "_" + arrModuleData[1]);
                            if (parseInt(arrModuleData[1]) > 1) {
                                ModuleManager.config.ActiveElement.find("span.control i.delete").before('<a class="sfManageControl" href="#"><img src=' + SageFrame.utils.GetAdminImage("edit-icon.png") + '></a>');
                            }
                            ModuleManager.config.ActiveElement.removeClass("sfUnsavedmodule");
                            SageFrame.popup.close("showPopup");
                            SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "ModuleManager", "UserModuleAdded"), "Success");
                            ModuleManager.InitUserModuleDeletion();
                            ModuleManager.InitUserModuleEdition();
                            ModuleManager.LoadPageModules();
                        }
                        else {

                            SageFrame.popup.close("showPopup");
                            SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "ModuleManager", "UserModuleUpdated"), "Success");
                            ModuleManager.LoadPageModules();
                        }
                        break;
                    case 6:
                        ModuleManager.BindPageList_DropDown(data);
                        break;
                    case 7:
                        ModuleManager.BindPageModules(data);
                        break;
                    case 8:
                        ModuleManager.BindPageList_PopUp(data);
                        break;
                    case 9:
                        ModuleManager.BindAdminModules(data);
                        break;
                    case 10:
                        ModuleManager.BindSearchModules(data);
                        break;
                    case 11:
                        ModuleManager.BindAdminSearchModules(data);
                        break;
                    case 12:
                        ModuleManager.BindSortModules(data);
                        break;
                    case 13:
                        $('#btnSavelayout').hide();
                        $('#btnLayoutCancel').hide();
                        $('#ddlLayouts option').removeClass("defaultLayout");
                        $('#ddlLayouts option:selected').addClass("defaultLayout");
                        break;
                }
            },
            ajaxFailure: function () {
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: ModuleManager.config.type,
                    contentType: ModuleManager.config.contentType,
                    async: ModuleManager.config.async,
                    cache: ModuleManager.config.cache,
                    url: ModuleManager.config.url,
                    data: ModuleManager.config.data,
                    dataType: ModuleManager.config.dataType,
                    success: ModuleManager.ajaxSuccess,
                    error: ModuleManager.ajaxFailure
                });
            },
            wclass: function () {
                $('.widget-class').draggable({
                    //'scroll': true,
                    'revert': true,
                    cursor: 'pointer',
                    connectWith: '.sfblocks',
                    helper: 'clone'
                });
            },
            winstancedrag: function () {
                $('.sfblocks').sortable({
                    connectWith: '.sfblocks',
                    cursor: 'pointer',
                    placeholder: "sfHighlight",
                    hoverClass: "sfActivedroparea",
                    update: function (e, ui) {
                        ModuleManager.UpdateModuleOrder();
                    }
                }).droppable({
                    accept: '.widget-class',
                    hoverClass: "sfActivedroparea",
                    drop: function (event, ui) {
                        var addonName = null;
                        var addonGroup = null;
                        var uiId = ui.draggable.prop('id');
                        var spliter = uiId.split('-');
                        var blockId = $(this).prop('id');
                        var $li = $('<div class="widget-instance ' + blockId + '">').html('<p class="basics">' + ui.draggable.html() + '</p>');
                        $li.prop('id', uiId + '-' + uniquekey);
                        $li.addClass("sfUnsavedmodule");
                        $li.append("<span class='control'><img src=" + SageFrame.utils.GetAdminImage('delete.png') + " class='delete'></span><div style='clear:both'></div>");
                        $li.appendTo(this);
                        var self = this;
                        if (!ui.draggable.hasClass('widget-instance')) {
                            ModuleDefID = $(ui.draggable).prop("id");
                            var panename = $(self).parent().find("div.sfPosition").html();
                            $('#spnPaneName').text(panename);
                            $('#lblmoduleName').text(ui.draggable.html());
                            $('#trPages').hide();
                            $('#chkInheritPermissions').prop("checked", false);
                            ModuleManager.LoadRoles();
                            ModuleManager.config.ActiveElement = $li;
                            $('#hdnModuleInPage').val('0');
                            $('#hdnPageList li').removeClass('sfActive');
                            ModuleManager.ClearPopUpControl();
                            $('#trPages').slideUp();
                            ModuleManager.config.UserModuleID = 0;
                            if (!ModuleManager.config.nopopupflag) {
                                ModuleManager.config.Mode = 0;
                                ModuleManager.ShowPopUp('showPopup', "Module Details");
                            }
                            else if (ModuleManager.config.nopopupflag) {
                                ModuleManager.SaveUserModuleDefault();
                            }
                        }
                        if ($('#rdbAdminModules').prop('checked')) {
                            $('#rbAllPages').hide();
                            $('#lblAll').hide();
                            $('#trheader').hide();
                            $('#trSuffix').hide();
                        }
                        else {
                            $('#lblAll').show();
                            $('#rbAllPages').show();
                            $('#trheader').show();
                            $('#trSuffix').show();
                        }
                        uniquekey++;
                    }
                });
            },
            wreload: function () {
                this.wclass();
                this.winstancedrag();
            },
            LoadLayoutWireFrame: function () {
                //                var pageName = '';
                //                if ($('#categoryTree').find("li.active").find('.ui-tree-selected').find('span.true').length > 0) {
                //                    pageName = $('#categoryTree').find("li.active").find('.ui-tree-selected').find('span.true').attr("pagename");
                //                }
                //                else {
                //                    pageName = $('#categoryTree li:eq(0)').find('.ui-tree-selected').find('span.true').attr("pagename");
                //                }
                var pageName = '';
                if ($('#categoryTree').find('li.active').length > 0) {
                    pageName = $('#categoryTree').find('li.active').find('span.true').attr("pagename");
                }
                else {
                    pageName = $('#categoryTree li:eq(0)').find('.ui-tree-selected').find('span.true').attr("pagename");
                }
                pagename = SageFrame.utils.GetPageSEOName(pageName);
                var url = ModuleManager.config.adminmode == 0 ? (ModuleManager.config.IsMobile == 0 ? ModuleManager.config.baseURL + "LoadActiveLayout" : ModuleManager.config.IsType3 == true ? ModuleManager.config.baseURL + "LoadType3Layout" : ModuleManager.config.baseURL + "LoadHandheldLayout") : ModuleManager.config.baseURL + "LoadAdminLayout";
                $.ajax({
                    type: ModuleManager.config.type,
                    contentType: ModuleManager.config.contentType,
                    cache: ModuleManager.config.cache,
                    url: url,
                    data: JSON2.stringify({
                        PageName: pageName,
                        TemplateName: ModuleManager.config.TemplateName,
                        portalID: parseInt(SageFramePortalID),
                        userName: SageFrameUserName,
                        userModuleID: p.UserModuleID,
                        secureToken: SageFrameSecureToken
                    }),
                    dataType: ModuleManager.config.dataType,
                    success: function (data) {
                        var layout = data.d;
                        $('#divLayoutWireframe').html(layout);
                        ModuleManager.LoadPageModules();
                        ModuleManager.GetActiveLayoutName(pageName);
                    },
                    error: ModuleManager.ajaxFailure
                });
            },
            GetActiveLayoutName: function (pageName) {
                if ($('#categoryTree').find("li.active").find('.ui-tree-selected').find('span.true').length > 0) {
                    pageName = $('#categoryTree').find("li.active").find('.ui-tree-selected').find('span.true').attr('pagename');
                }
                else {
                    pageName = $('#categoryTree li:eq(0)').find('span.true').eq(0).attr('pagename');;
                }
                $.ajax({
                    type: ModuleManager.config.type,
                    contentType: ModuleManager.config.contentType,
                    cache: ModuleManager.config.cache,
                    url: ModuleManager.config.baseURL + "GetActiveLayoutName",
                    data: JSON2.stringify({
                        pageName: pageName,
                        templateName: ModuleManager.config.TemplateName,
                        portalID: parseInt(SageFramePortalID),
                        userName: SageFrameUserName,
                        userModuleID: p.UserModuleID,
                        secureToken: SageFrameSecureToken
                    }),
                    dataType: ModuleManager.config.dataType,
                    success: function (data) {
                        $('#ddlLayouts').val(data.d);
                        $('#ddlLayouts option').removeClass("defaultLayout");
                        $('#ddlLayouts option:selected').addClass('defaultLayout');
                    },
                    error: ModuleManager.ajaxFailure
                });
            },

            LoadLayoutWireFrameChange: function () {
                var url = ModuleManager.config.baseURL + "LoadLayout";
                $.ajax({
                    type: ModuleManager.config.type,
                    contentType: ModuleManager.config.contentType,
                    cache: ModuleManager.config.cache,
                    url: url,
                    data: JSON2.stringify({
                        activeLayout: $('#ddlLayouts option:selected').text(),
                        TemplateName: ModuleManager.config.TemplateName,
                        portalID: parseInt(SageFramePortalID),
                        userName: SageFrameUserName,
                        userModuleID: p.UserModuleID,
                        secureToken: SageFrameSecureToken
                    }),
                    dataType: ModuleManager.config.dataType,
                    success: function (data) {
                        var layout = data.d;
                        $('#divLayoutWireframe').html(layout);
                        ModuleManager.LoadPageModules();
                    },
                    error: ModuleManager.ajaxFailure
                });
            },
            UpdateModuleOrder: function () {
                var panes = $('#sfOuterWrapper div.sfblocks');
                var ModuleOrderObj = [];
                var param = {
                    lstPageModules: [],
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                };
                $.each(panes, function () {
                    var usermoduleorder = "";
                    var self = $(this);
                    var usermodules = $(this).find("div.widget-instance");
                    $.each(usermodules, function () {
                        if ($(this).prop("id") != "") {
                            var modorder = parseInt($(this).index());
                            modorder = modorder + 1;
                            var moduleidArr = new Array();
                            moduleidArr = $(this).prop("id").split('_');
                            param.lstPageModules.push({
                                "UserModuleID": moduleidArr[1],
                                "PaneName": $(self).prev("div").text(),
                                "ModuleOrder": modorder
                            });
                        }
                    });
                });
                $.ajax({
                    type: ModuleManager.config.type,
                    contentType: ModuleManager.config.contentType,
                    cache: ModuleManager.config.cache,
                    url: ModuleManager.config.AppPath + "/Modules/Pages/services/ModuleManagerWebService.asmx/UpdatePageModules",
                    data: JSON2.stringify(param),
                    dataType: ModuleManager.config.dataType,
                    success: function (data) {
                    },
                    error: ModuleManager.ajaxFailure
                });
            },
            LoadModuleDetails: function (UserModuleID) {
                ModuleManager.LoadRoles();
                $.ajax({
                    type: ModuleManager.config.type,
                    contentType: ModuleManager.config.contentType,
                    cache: ModuleManager.config.cache,
                    url: ModuleManager.config.baseURL + "GetUserModuleDetails",
                    data: JSON2.stringify({
                        userModuleID: parseInt(UserModuleID),
                        portalID: parseInt(SageFramePortalID),
                        userName: SageFrameUserName,
                        secureToken: SageFrameSecureToken
                    }),
                    async: false,
                    dataType: ModuleManager.config.dataType,
                    success: function (data) {
                        var usermodule = data.d;
                        var pageid = usermodule.PageID
                        $('#hdnModuleInPage').val(pageid);
                        $('#txtModuleTitle').val(usermodule.UserModuleTitle);
                        $('#spnPaneName').text(usermodule.PaneName);
                        $('#lblmoduleName').text(usermodule.FriendlyName);
                        $('#txtHeaderTxt').val(usermodule.HeaderText);
                        $('#txtModuleSuffix').val(usermodule.SuffixClass.trim());
                        $('#chkShowHeader').prop("checked", usermodule.ShowHeaderText);
                        $('#chkInheritPermissions').prop("checked", usermodule.InheritViewPermissions);
                        if (usermodule.IsActive)
                            $('#chkIsActive').prop("checked", true);
                        else
                            $('#chkIsActive').prop("checked", false);
                        if (usermodule.AllPages) {
                            $('#rbAllPages').prop("checked", true);
                            $('#trPages').hide();
                        }
                        else if (usermodule.ShowInPages != "") {
                            $('#trPages').show();
                            $('#rbCustomPages').prop("checked", true);
                            var pages = $('#pageTree_popup li');
                            var appliedpages = new Array();
                            appliedpages = usermodule.ShowInPages.split(',');
                            $.each(pages, function () {
                                var pageid1 = $(this).prop("id");
                                var self = $(this);
                                $.each(appliedpages, function (index, item) {
                                    if (pageid1 == item) {
                                        $(self).addClass("sfActive");
                                    }
                                });
                            });
                            $('#pageTree_popup ').find('li#' + pageid).addClass('sfActive');
                            //hidden pages
                            var pages_hdn = $('#hdnPageList li');
                            var appliedpages_hdn = new Array();
                            appliedpages_hdn = usermodule.ShowInPages.split(',');
                            $.each(pages_hdn, function () {
                                var pageid1 = $(this).prop("id");
                                var self = $(this);
                                $.each(appliedpages_hdn, function (index, item) {
                                    if (pageid1 == item) {
                                        $(self).addClass("sfActive");
                                    }
                                });
                            });
                            $('#hdnPageList ').find('li#' + pageid).addClass('sfActive');
                        }
                        ModuleManager.BindUserModulePermission(usermodule.LSTUserModulePermission);
                    },
                    error: ModuleManager.ajaxFailure
                });
            },
            BindUserModulePermission: function (permission) {
                $('#tblUser').html('');
                var PagePermission = permission;
                var arr = new Array();
                $.each(PagePermission, function (inxs, ww) {
                    if (jQuery.inArray(ww.UserName, arr) == -1)
                        arr.push(ww.UserName);
                });
                var userHtml = "<tbody>";
                $.each(arr, function (inx, itw) {
                    if (itw != "") {
                        var style = inx % 2 == 0 ? 'class="sfEven"' : 'class="sfOdd"';
                        userHtml += '<tr ' + style + '><td width="40%"><label>' + arr[inx] + '</label></td><td width="20%"><input type="checkbox" class="sfCheckbox" title="view"/></td>';
                        userHtml += '<td width="20%"><input type="checkbox" class="sfCheckbox" title="edit"/></td><td width="10%"><i class="delete icon-delete" id="imgDelete"></i></td></tr>';
                    }
                });
                userHtml += "</tbody>";
                if (userHtml != "") {
                    $('#divModuleUser table').append(userHtml);
                }
                if ($('#tblUser tr').length > 0)
                    $('#tblUser').show();
                else
                    $('#tblUser').hide();
                var roles = $('div.divPermissions tr:gt(0)');
                var user = $('#divModuleUser tr');
                $.each(PagePermission, function (indx, itm) {
                    $.each(roles, function (index, item) {
                        if (itm.UserName.length == 0) {
                            if ($(item).prop('id') == itm.RoleID.toLowerCase() && itm.PermissionID == 1) {
                                $(item).find('input[title="view"]').prop('checked', true);
                            }
                            else if ($(item).prop('id') == itm.RoleID.toLowerCase() && itm.PermissionID == 2) {
                                $(item).find('input[title="edit"]').prop('checked', true);
                            }
                        }
                    });
                    $.each(user, function (index, ite) {
                        if ($(ite).find('td:eq(0) label').html() == itm.UserName && itm.PermissionID == 1) {
                            $(ite).find('input[title="view"]').prop('checked', true);
                        }
                        else if ($(ite).find('td:eq(0) label').html() == itm.UserName && itm.PermissionID == 2) {
                            $(ite).find('input[title="edit"]').prop('checked', true);
                        }
                    });
                });

                $('#divModuleUser').on('click', 'i.delete', function () {
                    $(this).parents('tr').remove();
                });
            },
            InitUserModuleEdition: function () {
                $('i.sfManageControl').off().on('click', function () {
                    var moduleidArr = new Array();
                    moduleidArr = $(this).parents("div").prop("id").split('_');
                    var url = ModuleManager.config.AppPath + "/Sagin/HandleModuleControls.aspx?uid=" + moduleidArr[1] + '&pid=' + SageFramePortalID + '';
                    $('#divFrame').prop("src", url);
                    var dialogOptions = {
                        "title": $(this).parent().prev().text(),
                        "width": 800,
                        "height": 550,
                        "modal": true,
                        "position": [400, 200]
                    };
                    if ($("#button-cancel").prop("checked")) {
                        dialogOptions["buttons"] = {
                            "Cancel": function () {
                                $(this).dialog("close");
                            }
                        };
                    }
                    //dialog-extend options
                    var dialogExtendOptions = {
                        "maximize": true
                    };
                    //open dialog
                    $("#divFrame").dialog(dialogOptions).dialogExtend(dialogExtendOptions);
                    $('div.ui-dialog').css("z-index", "3000");
                    $('div.ui-widget-overlay').css("z-index", "2999");
                    return false;
                });
            },
            InitUserModuleDeletion: function () {
                $('div.widget-instance span.control i.delete').click(function () {

                    var moduleidArr = new Array();
                    moduleidArr = $(this).parents("div").prop("id").split('_');
                    var self = $(this);
                    $('#sf_lblConfirmation').text("Are you sure you want to delete this usermodule?");
                    $("#dialog").dialog({
                        title: "Confirmation",
                        modal: true,
                        buttons: {
                            "Yes": function () {
                                $.ajax({
                                    type: ModuleManager.config.type,
                                    contentType: ModuleManager.config.contentType,
                                    cache: ModuleManager.config.cache,
                                    url: ModuleManager.config.AppPath + "/Modules/pages/services/ModuleManagerWebService.asmx/DeleteUserModule",
                                    data: JSON2.stringify({
                                        userModuleID: parseInt(moduleidArr[1]),
                                        DeletedBy: 'superuser',
                                        portalID: parseInt(SageFramePortalID),
                                        userName: SageFrameUserName,
                                        secureToken: SageFrameSecureToken
                                    }),
                                    dataType: ModuleManager.config.dataType,
                                    success: function (data) {
                                        SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "ModuleManager", "UserModuleDeleted"), "Success");
                                        $(self).parents("div.widget-instance").remove();
                                    },
                                    error: ModuleManager.ajaxFailure
                                });
                                $(this).dialog("close");
                            },
                            "No": function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                });
            },
            ClosePopUp: function () {
                $('#showPopup,#fade').fadeOut()
            },
            ClearPopUpControl: function () {
                $('#txtModuleTitle,#txtHeaderTxt,#txtModuleSuffix').val('');
                $('#rbCustomPages').prop("checked", false);
                $('#rbAllPages').prop("checked", false);
                $('#chkShowHeader').prop("checked", false);
                $('#divModuleUser').find('tr').remove();
            },
            ShowPopUp: function (popupid, headertext) {
                var $tabs = $('#dvTabPanel').tabs();
                $('#txtSearchUser').val('');
                $('p.sfRequired').remove();
                // $tabs.tabs('select', 0);
                $tabs.tabs('option', 'selected');
                if ($('#rdbAdminModules').prop("checked")) {
                    $('span.sfShowheader').hide();
                }
                else {
                    $('span.sfShowheader').show();
                }
                var options = {
                    modal: true,
                    title: headertext,
                    minHeight: 125,
                    minWidth: 520,
                    maxWidth: 1000,
                    maxHeight: 1000,
                    dialogClass: "sfFormwrapper",
                    resizable: false,
                    close: function (event, ui) {
                        $('div.sfUnsavedmodule').fadeOut();
                    }
                };
                dlg = $('#' + popupid).dialog(options);
                dlg.parent().appendTo($('form:first'));
            }
        };
        ModuleManager.init();

    }
    $.fn.ModuleManager = function (p) {
        $.createModuleManager(p);
    };
})(jQuery);