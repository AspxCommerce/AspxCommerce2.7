(function ($) {
    $.createMenu = function (p) {
        var order = 0;
        var level = 0;
        p = $.extend
                ({
                    CultureCode: '',
                    UserModuleID: ''
                }, p);
        var MenuMgr = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                method: "",
                url: "",
                categoryList: "",
                ajaxCallMode: 0,
                arr: [],
                arrModules: [],
                baseURL: SageFrameAppPath + '/Modules/Admin/MenuManager/MenuWebService.asmx/',
                Path: SageFrameAppPath + '/Modules/Admin/MenuManager/',
                iconPath: SageFrameAppPath,
                MenuItemID: 0,
                PortalID: SageFramePortalID,
                UserName: SageFrameUserName,
                lstMenuPermission: [],
                DefaultMenuID: 0,
                lstMenu: [],
                ItemAddMode: 0,
                UserModuleID: p.UserModuleID
            },
            messages:
            {
                nomenucreated: "No Menu Created"
            },
            init: function () {
                this.LoadMenu();
                this.IconUploaderExtLink();
                this.IconUploaderHtmlContent();
                //SageFrame.tooltip.GetToolTip("imgToolTip", "#h3MenuType", "This is from where you can include items into your menu ");
                // SageFrame.tooltip.GetToolTip("imgToolTip1", "#lblChoosePage", "List of pages that you can include in the menu ");
                $('#rdbDropdown').prop("checked", true);
                $('#txtExternalLink').on("change", function () {
                    var ExternalLink = $(this).val();
                    if ($(this).val().length > 0) {
                        if (!$(this).val().match(/^http/)) {
                            $(this).val('http://' + ExternalLink);
                        }
                    }
                });
                $('lblError').hide();
                var v = $("#form1").validate({
                    ignore: ':hidden',
                    rules: {
                        txtLinkTitle: { required: true },
                        txtExternalLink: { required: true },
                        txtTitleHtmlContent: { required: true }
                    },
                    messages: {
                        txtLinkTitle: "Please enter a Link Title",
                        txtExternalLink: "Please enter an External Link",
                        txtTitleHtmlContent: "Please enter a HTML Title"
                    }
                });
                $('#imgAdd').show();
                $('#imgUpdate').hide();
                $('#divLstMenu').html('<p class="sfNote">No Menu Selected</p>');
                $('#trPages').hide();
                $('#txtHtmlContent').ckeditor("config");
                // $('#MenuMgrSetting,#MenuMgrPermissiom').hide();
                $('#MenuMgrSetting').hide();
                $('div.divExternalLink,div.divHtmlContent,div.divSubText,#pageMenuitem,#trSubtext').hide();
                $('#imgAddMenuCancel').bind("click", function () {
                    MenuMgr.ClearTreeSelection();
                    MenuMgr.config.ItemAddMode = 0;
                    $('#rdbPages').prop("checked", true);
                    $('#trPages').hide();
                    $('#pageMenuitem').hide();
                    $('#chkLinkActive').prop("checked", false);
                    $('#chkLinkVisible').prop("checked", false);
                    $('#chkVisibleHtmlContent').prop("checked", false);
                    $('.sfIcon').parent('div').hide();
                    $('#txtHtmlContent').val('');
                    MenuMgr.ClearValidation();
                });
                $('#txtMenuName').keyup(function (e) {
                    if ($(this).val().length > 0) {
                        $('#lblError').hide();
                    }
                });
                $('.sfUploadedFilesExtLink').on("click", ".delete", function () {
                    var IconPath = $('.sfIcon').attr('title');
                    $('.sfIcon').parent('div').remove();
                    $('span.filename').html('No file selected');
                    MenuMgr.DeleteIcon(IconPath);
                });
                $('.sfUploadedFilesHtmlContent ').on("click", ".deleteIcon", function () {
                    var IconPath = $('.sfIcon').attr('title');
                    $('.sfIcon').parent('div').remove();
                    $('span.filename').html('No file selected');
                    MenuMgr.DeleteIcon(IconPath);
                });
                $('#imgAddmenuItem').on("click", function () {
                    if ($('#menuList').find(" li.sfSelected").length == 0) {
                        return ConfirmDialog(this, 'message', 'No menu selected. Please select a menu first');
                    }
                    if (v.form()) {
                        var selected = $('#pageTree').find("span.ui-tree-selected").length;
                        if (selected > 0) {

                            if ($('#rdbPages').prop("checked") || $('#rdbAdminPages').prop("checked")) {
                                MenuMgr.UpdatePageItem();
                                MenuMgr.config.ItemAddMode = 1;
                                MenuMgr.LoadMenuItem($("#menuList ").find("ul li.sfSelected").prop("id"));
                            }
                            else {
                                level = 0;
                                if ($("#rdbExternalLink").prop("checked")) {
                                    MenuMgr.ValidateContain();
                                    MenuMgr.SaveExternalLink("E");
                                }
                                if ($("#rdbHtmlContent").prop("checked")) {
                                    MenuMgr.ValidateContain();
                                    MenuMgr.SaveHtmlContent("E");
                                }
                                MenuMgr.config.ItemAddMode = 1;
                                MenuMgr.LoadMenuItem($("#menuList ").find("ul li.sfSelected").prop("id"));
                            }
                        }
                        else {
                            if ($('#rdbPages').prop("checked") || $('#rdbAdminPages').prop("checked")) {
                                var arrSave = new Array();
                                $(".cssPages:checked").not(".cssPages:disabled").each(function () {
                                    order++;
                                    var level = $("#selMenuItem option:selected").attr("level");
                                    level = parseInt(level) + 1;
                                    var pageid = $(this).prop("id").replace("chk", "");
                                    var parentid = $(this).parent("li").prop("id");
                                    var title = $(this).next("label").text();
                                    var arrVal = new Array();
                                    arrVal = parentid.split("_");
                                    MenuMgr.SavePages(title, arrVal, "A");
                                });
                                MenuMgr.LoadMenuItem($("#menuList ").find("ul li.sfSelected").prop("id"));
                            }
                            else {
                                level = 0;

                                if ($("#rdbExternalLink").prop("checked")) {
                                    MenuMgr.ValidateContain();
                                    MenuMgr.SaveExternalLink("A");
                                }
                                if ($("#rdbHtmlContent").prop("checked")) {
                                    MenuMgr.ValidateContain();
                                    MenuMgr.SaveHtmlContent("A");
                                }
                                MenuMgr.config.ItemAddMode = 1;
                                MenuMgr.LoadMenuItem($("#menuList ").find("ul li.sfSelected").prop("id"));
                            }
                        }
                        SageFrame.messaging.show("Menu item added successfully", "Success");
                        $('#chkPageOrder').prop('checked', false);
                    }
                    else {
                        $('#chkPageOrder').prop('checked', false);
                        return;
                    }
                });
                $('#selMenu').on("click", function () {
                   
                    MenuMgr.LoadMenuItem($("#selMenu option:selected").val());
                });
                $('#rdbPages').bind("click", function () {
                    MenuMgr.ClearValidation();
                    $('div.divExternalLink,div.divHtmlContent').hide();
                    MenuMgr.GetPages(false);
                    MenuMgr.CheckPage();
                    $('#trPages').slideDown();
                    MenuMgr.ClearTreeSelection();
                    $(this).parents('div.sfRadiobutton').find('label').removeClass('sfActive');
                    $(this).parent().addClass('sfActive');
                });
                $('#rdbAdminPages').bind("click", function () {
                    MenuMgr.ClearValidation();
                    $('div.divExternalLink,div.divHtmlContent').hide();
                    $('#txtLinkTitle,#txtExternalLink,#txtCaptionExtLink').val('');
                    $('#txtTitleHtmlContent,#txtCaptionHtmlContent').val('');
                    MenuMgr.GetPages(false);
                    $('#trPages').slideDown();
                    MenuMgr.ClearTreeSelection();
                    $(this).parents('div.sfRadiobutton').find('label').removeClass('sfActive');
                    $(this).parent().addClass('sfActive');
                });
                $('#rdbExternalLink').bind("click", function () {
                    MenuMgr.ClearValidation();
                    $('div.divExternalLink,#pageMenuitem').slideDown();
                    $('div.divHtmlContent,#trPages,div.divSubText,#trSubtext').hide();
                    $('span.filename').html('No file selected');
                    $('.sfIcon').parent('div').hide();
                    $('#txtLinkTitle,#txtExternalLink,#txtCaptionExtLink').val('');
                    $('#chkLinkActive').prop("checked", false);
                    $('#chkLinkVisible').prop("checked", false);
                    $('#chkVisibleHtmlContent').prop("checked", false);
                    MenuMgr.ClearTreeSelection();
                    $(this).parents('div.sfRadiobutton').find('label').removeClass('sfActive');
                    $(this).parent().addClass('sfActive');
                });
                $('#rdbHtmlContent').bind("click", function () {
                    $('div.divHtmlContent,#pageMenuitem').slideDown();
                    $('div.divExternalLink,#trPages,div.divSubText,#trSubtext').hide();
                    $('span.filename').html('No file selected');
                    $('.sfIcon').parent('div').hide();
                    $('#txtTitleHtmlContent,#txtCaptionHtmlContent,#txtHtmlContent').val('');
                    $('#chkLinkActive').prop("checked", false);
                    $('#chkLinkVisible').prop("checked", false);
                    $('#chkVisibleHtmlContent').prop("checked", false);
                    MenuMgr.ClearTreeSelection();
                    $(this).parents('div.sfRadiobutton').find('label').removeClass('sfActive');
                    $(this).parent().addClass('sfActive');
                });
                $('#divPagelist').on("click", '#chkSelectAllPages', function () {
                    if ($(this).prop("checked")) {
                        $('#pagesTree').find("li input").prop("checked", true);
                    }
                    else {
                        $('#pagesTree').find('li').eq(1).find('ul li input').not('#pagesTree ul li input:disabled').prop("checked", false);
                    }
                });
                $('#menuList').on('click', 'span.sfMenuname', function () {

                    $('div.sfRadiobutton').find('label.sfActive').eq(0).removeClass('sfActive');
                    $('div.sfRadiobutton').find('label').eq(0).addClass('sfActive');

                    var parentLi = $(this).parent('li');
                    $('#menuList').find('li.sfHighLight').removeClass('sfHighLight');
                    $('#imgCancel').text('Add Menu');
                    $('#imgCancel').removeClass('sfCancel').addClass('sfAdd');
                    $('#trSubtext').hide();
                    $('#menuList').find("li").not(parentLi).removeClass("sfSelected");
                    $('#spnName').remove();
                    $('#lblError').text('');
                    parentLi.addClass("sfSelected");
                    MenuMgr.LoadMenuItem(parentLi.prop("id"));
                    MenuMgr.LoadMenuPermission();
                    $('#txtMenuName').val($(this).text());
                    $('#imgAddmenuItem').text("Add Menu Item");
                    if ($('#rdbSetting').prop("checked")) {
                        MenuMgr.LoadMenuSettings();
                    }
                    $('div.divExternalLink,div.divHtmlContent,#pageMenuitem').hide();
                    $('#trPages').slideDown();
                    $('#imgAdd').hide();
                    $('#imgUpdate').show();
                    $('#chkPageOrder').prop('checked', false);
                    $('.sfDivFlying').hide();
                });
                $('#divLstMenu').on("click", 'span.ui-tree-title', function () {
                    var ids = $(this).parent('li').prop('id').split('_');
                    MenuMgr.config.MenuItemID = ids[0];
                    $('#imgAddmenuItem').text("Save Menu Item");
                    MenuMgr.LoadParentPages($("#menuList ").find("ul li.sfSelected").prop("id"));
                    MenuMgr.LoadMenuItemDetails(ids[0]);
                    $(this).parent().find('.ui-tree-title').each(function () {
                        var self = $(this).text();
                        $("#selMenuItem > option").each(function () {
                            option = $(this);
                            if (self.toLowerCase().trim() == option.text().toLowerCase().replace(/-/g, '').trim()) {
                                option.remove();
                            }
                        });
                    });
                });
                $('div.sfInformationcontent:nth-child(2)').show();
                $('div.sfInformationholder:first').addClass("Active");
                $('div.sfInformationheader').on("click", function () {
                    $(this).next("div").slideToggle("fast", function () {
                        $(this).closest("div.sfInformationholder").addClass("Active");
                        $("div.sfInformationholder").not($(this).closest("div.sfInformationholder")).removeClass("Active");
                    });
                    $('div.sfInformationcontent').not($(this).next("div")).hide();
                });
                //--------Permission-------------------------
                //$('#imbAddUsers').bind("click", function () {
                //    $("#divAddUsers").dialog("open");
                //});
                $('#imgDelete').on('click', function () {
                    $(this).parents('tr').remove();
                });
                //$('#btnSearchUsers').on("click", function () {
                //    MenuMgr.SearchUsers();
                //});
                $('#divSearchedUsers').on("click", "li", function () {
                    if (!$(this).hasClass("sfActive")) {
                        $(this).addClass("sfActive");
                    }
                    else {
                        $(this).removeClass("sfActive");
                    }
                });
                $('#imgSavePermission').on("click", function () {
                    var checks = $('div.divPermissions tr:gt(0), #dvUser tr:gt(0)').find('input.sfCheckbox:checked');
                    MenuMgr.config.lstMenuPermission = [];
                    $.each(checks, function (index, item) {
                        if ($(this).prop("checked")) {
                            if ($(this).closest('table').prop('id') == "tblUser") {
                                MenuMgr.config.lstMenuPermission[index] = { "PermissionID": $(this).prop('title') == "view" ? 1 : 2, "RoleID": null, "Username": $(this).closest('tr').find('td:eq(0)').html(), "AllowAccess": true };
                            }
                            else {
                                MenuMgr.config.lstMenuPermission[index] = { "PermissionID": $(this).prop('title') == "view" ? 1 : 2, "RoleID": $(this).closest('tr').prop('id'), "Username": "", "AllowAccess": true };
                            }
                        }
                    });
                    var MenuID = $("#menuList").find("ul li.sfSelected").prop("id");
                    MenuMgr.SavePermissionSetting(MenuID, MenuMgr.config.PortalID);
                });
                $('#rdbView').on("click", function () {
                    $('#MenuMgrView').show();

                    //$('#MenuMgrSetting,#MenuMgrPermissiom').hide();
                    $('#MenuMgrSetting').hide();

                    if ($("#rdbHorizontalMenu").prop("checked")) {
                        //$('#tblSideMenu').hide();
                        $('#tblTopClientMenu').show();
                    }
                    if ($("#rdbSideMenu").prop("checked")) {
                        //$('#tblSideMenu').hide();
                        $('#tblTopClientMenu').hide();
                    }
                    if ($("#rdbFooter").prop("checked")) {
                        //$('#tblSideMenu').hide();
                        $('#tblTopClientMenu').hide();
                    }
                    MenuMgr.ChangeRdbActiveClass($(this));
                });
                $('#rdbSetting').on("click", function () {
                    $('#MenuMgrSetting').show();
                    //$('#MenuMgrView,#MenuMgrPermissiom').hide();
                    $('#MenuMgrView').hide();

                    $('#tblSideMenu').hide();
                    MenuMgr.LoadMenuSettings();
                    if ($('#chkCaption').prop("checked") == true) {
                        $('#divCaption').hide();
                    }
                    else {
                        $('#divCaption').hide()
                    }
                    MenuMgr.ChangeRdbActiveClass($(this));
                });
                //$('#rdbPermission').on("click", function () {
                //    $('#MenuMgrPermissiom').show();
                //    $('#MenuMgrView,#MenuMgrSetting').hide();
                //});

                $('#chkCaption').on("click", function () {
                    if (this.checked) {
                        $('#divCaption').hide();
                    }
                    else {
                        $('#divCaption').hide();
                    }
                });
                $('#rdbSideMenu').bind("click", function () {
                    if ($(this).prop("checked")) {
                        $('#tblTopClientMenu').slideUp("fast");
                        //$('#tblSideMenu').slideDown("fast");
                        MenuMgr.ChangeRdbActiveClass($(this));
                    }
                });
                $('#rdbHorizontalMenu').bind("click", function () {
                    if ($(this).prop("checked")) {
                        $('#tblTopClientMenu').slideDown("fast");
                        //$('#tblSideMenu').slideUp("fast");
                        MenuMgr.ChangeRdbActiveClass($(this));
                    }
                });
                $('#rdbFooter').bind("click", function () {
                    $('#tblTopClientMenu').slideUp("fast");
                    //$('#tblSideMenu').slideUp("fast");
                    MenuMgr.ChangeRdbActiveClass($(this));
                });

                $('#btnSave').on("click", function () {
                    MenuMgr.SaveSetting();
                });
                $('#chkShowText').bind("change", function () {
                    if (!$('#chkShowImage').prop("checked") && !$(this).prop("checked")) {
                        $('#chkShowImage').prop("checked", true);
                    }
                });
                $('#chkShowImage').bind("change", function () {
                    if (!$('#chkShowText').prop("checked") && !$(this).prop("checked")) {
                        $('#chkShowText').prop("checked", true);
                    }
                });
                $('#menuList').on("click", "ul li .delete", function () {
                    var MenuID = $(this).parents('li').prop('id');
                    var self = $(this);
                    if (MenuID == MenuMgr.config.DefaultMenuID) {
                        SageFrame.messaging.show("Default Menu Cannot be deleted", "Alert");
                    }
                    else {
                        $('#sf_lblConfirmation').text("Are you sure you want to delete this Menu?");
                        $("#dialog").dialog({
                            modal: true,
                            buttons: {
                                "Confirm": function () {
                                    MenuMgr.DeleteMenu(MenuID);
                                    $(self).parent('li').remove();
                                    $(this).dialog("close");
                                    $('#imgAdd').show();
                                    $('#imgUpdate').hide();
                                },
                                "Cancel": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });
                    }
                });
                $('#imgCancel').bind("click", function () {
                    $(this).text('Cancel');
                    $('#imgCancel').removeClass('sfAdd').addClass('sfCancel');
                    $('#menuList li').removeClass("sfSelected");
                    $('#txtMenuName').val('');
                    $('#imgAdd').show();
                    $('#imgUpdate').hide();
                    $('#divLstMenu').html('<p class="sfNote">No menu selected</p>');

                });
                $('#imgUpdate').on("click", function () {
                    var messagehtml;
                    if ($('#txtMenuName').val().length > 25) {
                        messagehtml = '';
                        messagehtml = "<span id='spnChar'class='sfError'>Menu Name cannot be more than 25 chars long</span>";
                        $('#txtMenuName').after(messagehtml);
                        $('#txtMenuName').focus();
                        return false;
                    }
                    else { $('#spnChar').remove(); }

                    if ($('#txtMenuName').val() != "") {
                        var status = false; var menus = $('#menuList ul li');
                        $.each(menus, function (index, item) {
                            if ($('#txtMenuName').val().trim().toLowerCase() == $(this).find("span.sfMenuname").text().trim().toLowerCase()) {
                                $('#txtMenuName').val('');
                                $('#lblError').show();
                                $('#lblError').text("Please Choose Unique Name");
                                $('#imgAdd').show();
                                $('#imgUpdate').hide();
                                $('#txtMenuName').focus();
                                status = true;
                            }
                        });
                        if (!status) {
                            var MenuID = $('#hdnMenuID').val();
                            var PortalID = MenuMgr.config.PortalID;
                            MenuMgr.UpdateMenu(MenuID);
                            //MenuMgr.SavePermissionSetting(MenuID, MenuMgr.config.PortalID);
                            $('#lblError').hide();
                        }
                        else {
                            SageFrame.messaging.show("Please Choose Unique menu Name", "Error");
                            $('.sfDivFlying').hide();
                        }
                    }
                    else {
                        $('#lblError').show();
                        $('#lblError').text("Field Should Not be Blank.");
                        $('#txtMenuName').focus();
                        return false;
                    }
                });
                $('#txtMenuName').bind("keypress", function () {
                    $('#spnChar').remove();
                    $('#spnName').remove();
                    $('#spnUnique').remove();
                });
                $('#txtLinkTitle').bind("keypress", function () {
                    $('#spnLnkTitle').remove();
                });
                $('#txtCaptionExtLink').bind("keypress", function () {
                    $('#spnCaption').remove();
                });
                $('#txtTitleHtmlContent').bind("keypress", function () {
                    $('#spnHtmlContain').remove();
                });
                $('#txtCaptionHtmlContent').bind("keypress", function () {
                    $('#spnCaptionHtml').remove();
                });
                $('#txtSubText').bind("keypress", function () {
                    $('#spnsubText').remove();
                });

                $('#imgAdd').on("click", function () {
                    var messagehtml;
                    $('#spnName').remove();
                    $('#lblError').text('');
                    if ($('#txtMenuName').val().length > 25) {
                        messagehtml = '';
                        messagehtml = "<span id='spnChar'class='sfError'>Menu Name cannot be more than 25 chars long</span>";
                        $('#txtMenuName').after(messagehtml);
                        $('#txtMenuName').val('');
                        $('#txtMenuName').focus();
                        return false;
                    }
                    else { $('#spnChar').remove(); }
                    if ($('#txtMenuName').val() == "") {
                        $('#lblError').show();
                        $('#lblError').text("Please enter a Menu Name");
                        $('#txtMenuName').focus();
                        messagehtml = '';
                        messagehtml = "<span id='spnName'class='sfError'>Please enter a Menu Name</span>";
                        $('#txtMenuName').after(messagehtml);
                        $('#txtMenuName').val('');
                        $('#txtMenuName').focus();
                        return false;
                    }
                    else {

                        var status = false;
                        var menus = $('#menuList').find("ul li");
                        $.each(menus, function (index, item) {
                            if ($('#txtMenuName').val().trim().toLowerCase() == $(this).find("span.sfMenuname").text().trim().toLowerCase()) {
                                //                                messagehtml = '';
                                //                                messagehtml = "<span id='spnUnique'class='sfError'>Please Choose Unique Name</span>";
                                //                                $('#txtMenuName').after(messagehtml);
                                $('#txtMenuName').val('');
                                $('#txtMenuName').focus();
                                $('#imgAdd').show();
                                $('#imgUpdate').hide();
                                status = true;
                            }
                        });
                        if (!status) {
                            MenuMgr.SaveMenu('#txtMenuName'.valueOf(), $("input:radio[name=rdbChooseMenuType]:checked").val(), false);
                            MenuMgr.LoadMenu();
                            SageFrame.messaging.show("Menu Saved Successfully", "Success");
                            $('#imgAdd').show();
                            $('#imgUpdate').hide();
                            $('#txtMenuName').val('');
                            $('#spnChar').remove();
                            $('#spnName').remove();
                            $('#spnUnique').remove();
                        }
                        else {
                            SageFrame.messaging.show("Please Choose Unique menu Name", "Error");
                            $('.sfDivFlying').hide();
                        }
                    }
                });
                $('#menuList').find("ul li:first span.sfMenuname").trigger('click');
                MenuMgr.BindFlyingEdit();
                $('#rdbCssMenu').on('click', function () {
                    MenuMgr.ChangeRdbActiveClass($(this));
                });
                $('#rdbDropdown').on('click', function () {
                    MenuMgr.ChangeRdbActiveClass($(this));
                });

                $('#txtMenuName').keyup(function (event) {
                    if (event.keyCode == 13) {
                        $('#imgAdd').click();
                        $('.sfDivFlyingClose').click();
                    }
                });
            },
            ChangeRdbActiveClass: function ($this) {
                $this.parents('div.sfRadiobutton').find('label.sfActive').removeClass('sfActive');
                $this.parents('label').addClass('sfActive');
            },
            BindFlyingEdit: function () {
                $('#menuList').on('click', '.MenuTypeEdit', function () {
                    $('.sfDivFlying').show();
                    $('#txtMenuName').focus();
                    var top = $(this).offset().top;
                    var left = $(this).parents('li').offset().left;
                    var height = $(this).height();
                    $('.sfDivFlying').offset({ top: (top + height + 7), left: left });
                    $('#imgAdd').hide();
                    $('#imgUpdate').show();
                    $('#txtMenuName').val($(this).parent().prev().text());
                    $('#menuList ul li').not('.sfSelected').removeClass('sfHighLight');
                    $(this).parents('li').addClass('sfHighLight');
                    $('#hdnMenuID').val($(this).parents('li').prop('id'));
                    if ($(this).parents('li').hasClass('sfSelected')) {
                        $('.sfDivFlying').removeClass('sfDivFlyingHighlighted');
                    }
                    else {
                        $('.sfDivFlying').addClass('sfDivFlyingHighlighted');
                    }
                });
                $('#btnAddpage').on('click', function () {
                    $('.sfDivFlying').show();
                    $('#txtMenuName').focus();
                    var top = $(this).offset().top;
                    var left = $(this).offset().left;
                    var height = $(this).height();
                    $('.sfDivFlying').offset({ top: (top + height), left: left - 105 });
                    $('#imgAdd').show();
                    $('#imgUpdate').hide();
                    $('#txtMenuName').val('');
                    $(this).addClass('active');
                });
                $('.sfDivFlyingClose').on('click', function () {
                    //$(this).text('Cancel');
                    //$('#imgCancel').removeClass('sfAdd').addClass('sfCancel');
                    //$('#menuList li').removeClass("sfSelected");
                    $('#txtMenuName').val('');
                    //$('#imgAdd').show();
                    $('.sfDivFlying').hide();
                    $('#btnAddpage').removeClass('active');
                    //$('#imgUpdate').hide();
                    //$('#divLstMenu').html('<p class="sfNote">No menu selected</p>');
                    $('#menuList ul li').removeClass('sfHighLight');
                    $('.sfDivFlying').addClass('sfDivFlyingHighlighted');
                });
                //$('.sfLeftdiv').on('hoverout')
            },
            //string CultureCode, int PortalID
            ClearCache: function () {
                this.config.method = "ClearCache";
                this.config.url = MenuMgr.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    CultureCode: p.CultureCode,
                    PortalID: p.PortalID
                });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            },
            DeleteIcon: function (IconPath) {
                this.config.method = "DeleteIcon";
                this.config.url = MenuMgr.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    IconPath: IconPath,
                    PortalID: SageFramePortalID,
                    UserModuleID: MenuMgr.config.UserModuleID,
                    UserName: SageFrameUserName,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },
            LoadMenu: function () {
                this.config.method = "GetAllMenu";
                this.config.data = JSON2.stringify({ UserName: MenuMgr.config.UserName, PortalID: MenuMgr.config.PortalID, userModuleId: MenuMgr.config.UserModuleID, secureToken: SageFrameSecureToken });
                this.config.url = MenuMgr.config.baseURL + this.config.method;
                this.config.ajaxCallMode = 0;
                this.ajaxCall(this.config);
            },
            BindMenuList: function (data) {
                var LstMenu = data.d;
                var html = '';
                var menulist = '<ul>';
                $.each(LstMenu, function (index, item) {
                    if (item != "") {
                        MenuMgr.config.lstMenu.push(item.MenuName);
                        var editid = 'edit_' + item.MenuID;
                        var delid = 'del_' + item.MenuID;
                        // var styleClass = index % 2 == 0 ? 'class="sfOdd"' : "";
                        if (!item.IsDefault) {
                            menulist += '<li id=' + item.MenuID + '><span class="sfMenuname">' + item.MenuName + '</span><span class="sfMenuOption"> <i class="MenuTypeEdit icon-edit"></i>';
                            menulist += '<i class="delete icon-delete" id=' + delid + '></i></span>';
                        }
                        else {
                            menulist += '<li id=' + item.MenuID + '><span class="sfMenuname">' + item.MenuName + '</span><span class="sfMenuOption"> <i class="MenuTypeEdit icon-edit"></i></span>';
                            menulist += '<span class="sfDefault">(Default)</span>';
                        }
                        menulist += '</li>';
                        if (item.IsDefault) {
                            MenuMgr.config.DefaultMenuID = item.MenuID;
                        }
                    }
                });
                menulist += '</ul>';
                if (LstMenu.length == 0)
                    menulist = SageFrame.messaging.showdivmessage(MenuMgr.messages.nomenucreated);
                $('#menuList').html(menulist);
            },
            LoadMenuItem: function (MenuID) {
                this.config.method = "GetAllMenuItem";
                this.config.url = MenuMgr.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    MenuID: MenuID,
                    PortalID: SageFramePortalID,
                    userModuleId: MenuMgr.config.UserModuleID,
                    UserName: SageFrameUserName,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);
            },
            BindParentPages: function (data) {
                var LstMenu = data.d;
                var html = '';

                html += '<option level="na"  class="lstMenuItem" id="0" value="0">--[None]--</option>';

                $.each(LstMenu, function (index, item) {
                    if (item != "") {
                        var title = item.LinkType == 0 ? item.PageName : item.Title;
                        html += '<option level=' + item.MenuLevel + '  class="lstMenuItem" value=' + item.MenuItemID + '>' + MenuMgr.GetPrefixes(item.MenuLevel) + title + '</option>';
                    }
                });
                $('#selMenuItem').html('').append(html);
                MenuMgr.BindMenu(data);
            },
            LoadParentPages: function (MenuID) {
                this.config.method = "GetAllMenuItem";
                this.config.url = MenuMgr.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    MenuID: MenuID,
                    PortalID: SageFramePortalID,
                    userModuleId: MenuMgr.config.UserModuleID,
                    UserName: SageFrameUserName,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 12;
                this.ajaxCall(this.config);
            },
            BindParentPagesOnly: function (data) {
                var LstMenu = data.d;
                var html = '';
                html += '<option level="na"  class="lstMenuItem" id="0" value="0">--[None]--</option>';
                $.each(LstMenu, function (index, item) {
                    if (item != "") {
                        var title = item.LinkType == 0 ? item.PageName : item.Title;
                        html += '<option level=' + item.MenuLevel + '  class="lstMenuItem" value=' + item.MenuItemID + '>' + MenuMgr.GetPrefixes(item.MenuLevel) + title + '</option>';
                    }
                });
                $('#selMenuItem').html('').append(html);
            },
            LoadChildMenuItem: function () {
                this.config.method = "GetAllMenuItem";
                this.config.url = MenuMgr.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    MenuID: $("#menuList").find("ul li.sfSelected").prop("id"), PortalID: SageFramePortalID, userModuleId: MenuMgr.config.UserModuleID, UserName: SageFrameUserName
                });
                this.config.ajaxCallMode = 5;
                this.ajaxCall(this.config);
            },
            SaveMenu: function () {
                var checks = $('div.divPermissions tr:gt(0), #dvUser tr:gt(0)').find('input.sfCheckbox:checked');
                MenuMgr.config.lstMenuPermission = [];
                $.each(checks, function (index, item) {
                    if ($(this).prop("checked")) {
                        if ($(this).closest('table').prop('id') == "tblUser") {
                            MenuMgr.config.lstMenuPermission[index] = { "PermissionID": $(this).attr('title') == "view" ? 1 : 2, "RoleID": null, "Username": $(this).closest('tr').find('td:eq(0)').html(), "AllowAccess": true };
                        }
                        else {
                            MenuMgr.config.lstMenuPermission[index] = { "PermissionID": $(this).attr('title') == "view" ? 1 : 2, "RoleID": $(this).closest('tr').prop('id'), "Username": "", "AllowAccess": true };
                        }
                    }
                });
                this.config.method = "AddNewMenu";
                this.config.url = MenuMgr.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    lstMenuPermissions: MenuMgr.config.lstMenuPermission,
                    MenuName: $('#txtMenuName').val(),
                    MenuType: $("input:radio[name=rdbChooseMenuType]:checked").val(),
                    IsDefault: false,
                    PortalID: MenuMgr.config.PortalID,
                    CultureCode: p.CultureCode,
                    userModuleId: MenuMgr.config.UserModuleID,
                    UserName: SageFrameUserName,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },
            DeleteMenu: function (MenuID) {
                $.ajax({
                    type: MenuMgr.config.type,
                    contentType: MenuMgr.config.contentType,
                    cache: MenuMgr.config.cache,
                    url: MenuMgr.config.baseURL + "DeleteMenu",
                    data: JSON2.stringify({
                        MenuID: MenuID,
                        CultureCode: p.CultureCode,
                        PortalID: MenuMgr.config.PortalID,
                        userModuleId: MenuMgr.config.UserModuleID,
                        UserName: SageFrameUserName,
                        secureToken: SageFrameSecureToken
                    }),
                    async: false,
                    dataType: MenuMgr.config.dataType,
                    success: function (msg) {
                        $('#txtMenuName').val('');
                        $('#menuList').find('#' + MenuID).remove();
                        SageFrame.messaging.show("Menu Deleted Successfully", "Success");
                    }
                });
            },
            UpdateMenu: function (MenuID) {
                var checks = $('div.divPermissions tr:gt(0), #dvUser tr:gt(0)').find('input.sfCheckbox:checked');
                MenuMgr.config.lstMenuPermission = [];
                $.each(checks, function (index, item) {
                    if ($(this).prop("checked")) {
                        if ($(this).closest('table').prop('id') == "tblUser") {
                            MenuMgr.config.lstMenuPermission[index] = { "PermissionID": $(this).attr('title') == "view" ? 1 : 2, "RoleID": null, "Username": $(this).closest('tr').find('td:eq(0) label').html(), "AllowAccess": true };
                        }
                        else {
                            MenuMgr.config.lstMenuPermission[index] = { "PermissionID": $(this).attr('title') == "view" ? 1 : 2, "RoleID": $(this).closest('tr').prop('id'), "Username": "", "AllowAccess": true };
                        }
                    }
                });
                $.ajax({
                    type: MenuMgr.config.type,
                    contentType: MenuMgr.config.contentType,
                    cache: MenuMgr.config.cache,
                    url: MenuMgr.config.baseURL + "UpdateMenu",
                    data: JSON2.stringify({
                        lstMenuPermissions: MenuMgr.config.lstMenuPermission,
                        MenuID: MenuID,
                        MenuName: $('#txtMenuName').val(),
                        MenuType: $("input:radio[name=rdbChooseMenuType]:checked").val(),
                        IsDefault: false,
                        PortalID: MenuMgr.config.PortalID,
                        CultureCode: p.CultureCode,
                        userModuleId: MenuMgr.config.UserModuleID,
                        UserName: SageFrameUserName,
                        secureToken: SageFrameSecureToken
                    }),
                    async: false,
                    dataType: MenuMgr.config.dataType,
                    success: function (msg) {
                        $('#menuList').find('#' + MenuID).find('.sfMenuname').text($('#txtMenuName').val());
                        $('.sfDivFlying ').hide();
                        //MenuMgr.LoadMenu();
                        $('#imgAdd').show();
                        $('#imgUpdate').hide();
                        $('#txtMenuName').val('');
                        $('#txtMenuName').focus();
                        $('#menuList').find('#' + MenuID).find('.sfMenuname').trigger('click');
                    }
                });
            },

            GetPages: function (isadmin) {
                if ($('#rdbPages').prop("checked")) {
                    this.config.method = "GetNormalPage";
                    this.config.url = MenuMgr.config.baseURL + this.config.method;
                    this.config.data = JSON2.stringify({
                        PortalID: MenuMgr.config.PortalID,
                        UserName: MenuMgr.config.UserName,
                        CultureCode: 'en-US',
                        userModuleID: MenuMgr.config.UserModuleID,
                        secureToken: SageFrameSecureToken
                    });

                }
                if ($('#rdbAdminPages').prop("checked")) {
                    this.config.method = "GetAdminPage";
                    this.config.url = MenuMgr.config.baseURL + this.config.method;
                    this.config.data = JSON2.stringify({
                        PortalID: MenuMgr.config.PortalID,
                        UserName: MenuMgr.config.UserName,
                        CultureCode: 'en-US',
                        userModuleId: MenuMgr.config.UserModuleID,
                        secureToken: SageFrameSecureToken
                    });
                }
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            },
            CheckPage: function () {

                var menuitems = $('#pageTree').find("li");
                $.each(menuitems, function () {
                    var pageArr = new Array();
                    pageArr = $(this).prop("id").split("_");
                    var pageid1 = pageArr[1] == "-1" ? "0" : pageArr[1];
                    var pages = $('#pagesTree').find("li:gt(0)");
                    $.each(pages, function () {
                        var pageArr1 = new Array();
                        pageArr1 = $(this).prop("id").split("_");
                        var pageid2 = pageArr1[0];

                        var self = $(this);
                        if (pageid1 == pageid2) {
                            $(self).find("input:first").prop("checked", true).attr("disabled", "disabled");
                        }

                    });
                });
                $('#trPages').slideDown();
            },
            BindPages: function (data) {
                var pages = data.d;
                var PageID = "";
                var parentID = "";
                var PageLevel = 0;
                var itemPath = "";
                var html = "";
                html += '<ul id="pagesTree"><li><input id="chkSelectAllPages" type="checkbox"/><label class="sfSelectall">Select All</label></li><li>';
                html += '<ul>';
                $.each(pages, function (index, item) {
                    PageID = item.PageID;
                    parentID = item.ParentID;
                    categoryLevel = item.Level;
                    if (item.Level == 0) {

                        var ListID = item.PageID + "_" + item.ParentID + "_" + item.PageOrder + "_" + item.Level;

                        html += '<li id=' + ListID + '>';
                        html += '<input id="chk  ' + PageID + '" type="checkbox" class="cssPages" />';
                        html += '<label>' + item.SEOName.replace(new RegExp("-", "g"), ' ') + '</label>';
                        if (item.ChildCount > 0) {
                            itemPath += item.SEOName;
                            html += "<ul>";
                            html += MenuMgr.BindChildCategory(pages, PageID);
                            html += "</ul>";
                        }
                        html += '</li>';
                    }
                    itemPath = '';
                });
                html += '</ul>';
                $('#divPagelist').html(html);
                $('#divPagelist').append("</li></ul>");
            },
            BindChildCategory: function (response, PageID) {
                var strListmaker = '';
                var childNodes = '';
                var path = '';
                var itemPath = "";
                itemPath += "";
                $.each(response, function (index, item) {
                    if (item.Level > 0) {
                        if (item.ParentID == PageID && item.PageName != "") {
                            itemPath += item.PageName;

                            var prefix = MenuMgr.GetPrefixes(item.Level);
                            var ListID = item.PageID + "_" + item.ParentID + "_" + item.PageOrder + "_" + item.Level;
                            strListmaker += '<li id=' + ListID + '>' + '<input id="chk' + item.PageID + '" type="checkbox" class="cssPages" /><label>' + item.PageName + '</label>';

                            childNodes = MenuMgr.BindChildCategory(response, item.PageID);

                            if (childNodes != '') {
                                strListmaker += "<ul>" + childNodes + "</ul>";
                            }
                            strListmaker += "</li>";
                        }
                    }
                });
                return strListmaker;
            },
            BindMenu: function (data) {
                var pages = data.d;
                var PageID = "";
                var parentID = "";
                var PageLevel = 0;
                var itemPath = "";
                var html = "";
                html += '<ul id="pageTree">';
                $.each(pages, function (index, item) {
                    PageID = item.MenuItemID + '_' + item.PageID;
                    parentID = item.ParentID;
                    if (item.MenuLevel == 0) {
                        if (item.ChildCount > 0) {
                            html += '<li class="file-folder" id=' + PageID + '><span></span>';
                        }
                        else {
                            html += '<li id=' + PageID + '> <span></span>';
                        }
                        html += item.LinkType == 0 ? item.PageName : item.Title;
                        if (item.ChildCount > 0) {
                            html += "<ul>";
                            itemPath += item.LinkType == 0 ? item.PageName : item.Title;
                            html += MenuMgr.BindMenuItem(pages, item.MenuItemID);
                            html += "</ul>";
                        }
                        html += "</li>";
                    }
                    itemPath = '';
                });
                //html += '<li class="noDrag"><div class="test" >Drag here to delete</li>';
                html += '</ul>';
                $('#divLstMenu').html(html);
                MenuMgr.AddDragDrop();
                MenuMgr.AddContextMenu();
                if (MenuMgr.config.ItemAddMode == 0) {
                    $('#rdbPages').prop("checked", true);
                    MenuMgr.GetPages(false);
                    MenuMgr.CheckPage();
                }
            },
            BindMenuItem: function (response, PageID) {
                var strListmaker = '';
                var childNodes = '';
                var path = '';
                var itemPath = "";
                itemPath += "";
                $.each(response, function (index, item) {
                    if (item.MenuLevel > 0) {
                        if (item.ParentID == PageID) {
                            itemPath += item.Title;
                            var prefix = MenuMgr.GetPrefixes(item.MenuLevel);
                            var ListID = item.ParentID;
                            var ID = item.MenuItemID + '_' + item.PageID;
                            if (item.ChildCount > 0) {
                                strListmaker += '<li class="file-folder sfChild" id=' + ID + '><span></span>';
                            }
                            else {
                                strListmaker += '<li class="sfChild" id=' + ID + '> <span></span>';
                            }
                            strListmaker += item.LinkType == 0 ? item.PageName : item.Title;
                            if (item.ChildCount > 0) {
                                strListmaker += "<ul>";
                                strListmaker += item.LinkType == 0 ? item.PageName : item.Title;
                                strListmaker += MenuMgr.BindMenuItem(response, item.MenuItemID);
                                strListmaker += "</ul>";
                            }
                            strListmaker += "</li>";
                        }
                    }
                });
                return strListmaker;
            },
            ClearValidation: function () {
                $('#spnCaption').remove();
                $('#spnLnkTitle').remove();
                $('#spnHtmlContain').remove();
                $('#spnCaptionHtml').remove();
                $('#txtTitleHtmlContent').val('');
                $('#txtCaptionHtmlContent').val('');
                $('#chkVisibleHtmlContent').prop('checked', false);
                $('#txtHtmlContent').val('');
                $('div.sfUploadedFilesHtmlContent').html('');
                $('.filename').html('');
                $('#txtLinkTitle').val('');
                $('#txtExternalLink').val('');
                $('#txtCaptionExtLink').val('');
                $('#chkLinkActive').prop('checked', false);
                $('#chkLinkVisible').prop('checked', false);
                $('div.sfUploadedFilesExtLink').html('');
            },
            ValidateContain: function () {
                var messagehtml;
                if ($("#rdbExternalLink").prop("checked")) {
                    if ($('#txtLinkTitle').val().length > 25) {
                        messagehtml = '';
                        messagehtml = "<span id='spnLnkTitle'class='sfError'>Link Title cannot be more than 25 chars long</span>";
                        $('#txtLinkTitle').after(messagehtml);
                        $('#txtLinkTitle').val('');
                        $('#txtLinkTitle').focus();
                        return false;
                    }
                    if ($('#txtCaptionExtLink').val().length > 25) {
                        messagehtml = '';
                        messagehtml = "<span id='spnCaption'class='sfError'>Caption cannot be more than 25 chars long</span>";
                        $('#txtCaptionExtLink').after(messagehtml);
                        $('#txtCaptionExtLink').val('');
                        $('#txtCaptionExtLink').focus();
                        return false;
                    }
                    else { $('#spnCaption').remove(); $('#spnLnkTitle').remove(); }
                }
                if ($("#rdbHtmlContent").prop("checked")) {
                    if ($('#txtTitleHtmlContent').val().length > 25) {
                        messagehtml = '';
                        messagehtml = "<span id='spnHtmlContain'class='sfError'>HTML Title cannot be more than 25 chars long</span>";
                        $('#txtTitleHtmlContent').after(messagehtml);
                        $('#txtTitleHtmlContent').val('');
                        $('#txtTitleHtmlContent').focus();
                        return false;
                    }
                    if ($('#txtCaptionHtmlContent').val().length > 25) {
                        messagehtml = '';
                        messagehtml = "<span id='spnCaptionHtml'class='sfError'>Caption cannot be more than 25 chars long</span>";
                        $('#txtCaptionHtmlContent').after(messagehtml);
                        $('#txtCaptionHtmlContent').val('');
                        $('#txtCaptionHtmlContent').focus();
                        return false;
                    }
                    else { $('#spnHtmlContain').remove(); $('#spnCaptionHtml').remove(); }
                }
            },
            SaveExternalLink: function (mode) {
                var imagepath = $('div.sfUploadedFilesExtLink img.sfIcon').attr("title");
                var menulevel = $("#selMenuItem option:selected").attr("level");
                var ExternalLinkItems = {
                    objInfo: {
                        MenuID: $("#menuList").find("ul li.sfSelected").prop("id"),
                        MenuItemID: MenuMgr.config.MenuItemID,
                        LinkType: $("input:radio[name=rdbMenuItem]:checked").val(),
                        Title: $('#txtLinkTitle').val(),
                        Linkurl: $('#txtExternalLink').val(),
                        ImageIcon: imagepath,
                        Caption: $('#txtCaptionExtLink').val(),
                        ParentId: $('#selMenuItem').val(),
                        MenuLevel: menulevel == "na" ? 0 : parseInt(menulevel) + 1,
                        MenuOrder: "1",
                        Mode: mode,
                        IsActive: $("#chkLinkActive").prop("checked") ? true : false,
                        CultureCode: p.CultureCode,
                        IsVisible: $("#chkLinkVisible").prop("checked") ? true : false
                    },
                    PortalID: SageFramePortalID,
                    userModuleId: MenuMgr.config.UserModuleID,
                    UserName: SageFrameUserName,
                    secureToken: SageFrameSecureToken
                };
                $.ajax({
                    type: MenuMgr.config.type,
                    contentType: MenuMgr.config.contentType,
                    cache: MenuMgr.config.cache,
                    url: MenuMgr.config.baseURL + "AddExternalLink",
                    data: JSON2.stringify(ExternalLinkItems),
                    dataType: MenuMgr.config.dataType,
                    async: false,
                    success: function (msg) {
                        MenuMgr.ClearValidation();
                    }
                });
            },
            SaveHtmlContent: function (mode) {
                var imagepath = $('div.sfUploadedFilesHtmlContent').find("img.sfIcon").attr("title");
                var menulevel = $("#selMenuItem option:selected").attr("level");
                var HtmlContent = {
                    objInfo: {
                        MenuID: $("#menuList").find("ul li.sfSelected").prop("id"),
                        MenuItemID: MenuMgr.config.MenuItemID,
                        LinkType: $("input:radio[name=rdbMenuItem]:checked").val(),
                        Title: $('#txtTitleHtmlContent').val(),
                        HtmlContent: $('#txtHtmlContent').val(),
                        ImageIcon: imagepath,
                        Caption: $('#txtCaptionHtmlContent').val(),
                        ParentId: $('#selMenuItem').val(),
                        MenuLevel: menulevel == "na" ? 0 : parseInt(menulevel) + 1,
                        Mode: mode,
                        IsVisible: $("#chkVisibleHtmlContent").prop("checked") ? true : false,
                        IsActive: $("#chkVisibleHtmlContent").prop("checked") ? true : false
                    },
                    PortalID: SageFramePortalID,
                    userModuleId: MenuMgr.config.UserModuleID,
                    UserName: SageFrameUserName,
                    secureToken: SageFrameSecureToken
                };
                $.ajax({
                    type: MenuMgr.config.type,
                    contentType: MenuMgr.config.contentType,
                    cache: MenuMgr.config.cache,
                    url: MenuMgr.config.baseURL + "AddHtmlContent",
                    data: JSON2.stringify(HtmlContent),
                    dataType: MenuMgr.config.dataType,
                    async: false,
                    success: function (msg) {
                        MenuMgr.ClearValidation();
                    }
                });
            },
            SavePages: function (title, arrVal, mode) {
                var ParentID;
                if ($("#selMenuItem option:selected").val() == 0) {
                    ParentID = 0;
                }
                else {
                    ParentID = $("#selMenuItem option:selected").val();
                }
                var MenuItem = new Array();
                MenuItems = {
                    objInfo: {
                        MenuID: parseInt($("#menuList").find("ul li.sfSelected").prop("id")),
                        MenuItemID: MenuMgr.config.MenuItemID,
                        LinkType: $("input:radio[name=rdbMenuItem]:checked").val(),
                        PageID: arrVal[0],
                        Title: title,
                        Linkurl: "",
                        ImageIcon: "",
                        Caption: "",
                        HtmlContent: "",
                        ParentId: arrVal[1],
                        MenuLevel: arrVal[3],
                        MenuOrder: arrVal[2],
                        Mode: mode,
                        PreservePageOrder: $('#chkPageOrder').is(":checked") ? true : false,
                        MainParent: $('#selMenuItem').val(),
                        IsActive: true,
                        IsVisible: true,
                        CultureCode: p.CultureCode,
                        PortalID: MenuMgr.config.PortalID
                    },
                    userModuleId: MenuMgr.config.UserModuleID,
                    UserName: SageFrameUserName,
                    secureToken: SageFrameSecureToken

                };
                this.config.method = "AddMenuItem";
                this.config.url = MenuMgr.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify(MenuItems),
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            },
            UpdatePageItem: function () {
                $('#chkPageOrder').attr("checked");
                if ($('#txtSubText').val().length > 25) {
                    var messagehtml = '';
                    messagehtml = "<span id='spnsubText'class='sfError'>HTML Title cannot be more than 25 chars long</span>";
                    $('#txtSubText').after(messagehtml);
                    $('#txtSubText').val('');
                    $('#txtSubText').focus();
                    return false;
                }
                else { $('#spnsubText').remove(); }
                var MenuItem = new Array();
                MenuItems = {
                    objInfo: {
                        MenuID: parseInt($("#menuList").find("ul li.sfSelected").prop("id")),
                        MenuItemID: MenuMgr.config.MenuItemID,
                        LinkType: 0,
                        PageID: 0,
                        Title: '',
                        Linkurl: "",
                        ImageIcon: "",
                        Caption: $('#txtSubText').val(),
                        HtmlContent: "",
                        ParentId: $('#selMenuItem').val(),
                        MenuLevel: 0,
                        MenuOrder: 0,
                        Mode: 'E',
                        PreservePageOrder: $('#chkPageOrder').prop("checked") ? true : false,
                        MainParent: $('#selMenuItem').val(),
                        IsActive: $('#chkIsActivePage').prop("checked"),
                        IsVisible: $('#chkIsVisiblePage').prop("checked"),
                        CultureCode: p.CultureCode,
                        PortalID: MenuMgr.config.PortalID
                    },
                    userModuleId: MenuMgr.config.UserModuleID,
                    UserName: SageFrameUserName,
                    secureToken: SageFrameSecureToken
                };
                this.config.method = "AddMenuItem";
                this.config.url = MenuMgr.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify(MenuItems),
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            },
            LoadMenuPermission: function () {

                var MenuID = parseInt($("#menuList").find("ul li.sfSelected").prop("id"));
                this.config.method = "GetMenuPermission";
                this.config.url = MenuMgr.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    PortalID: MenuMgr.config.PortalID,
                    MenuID: MenuID,
                    UserModuleID: MenuMgr.config.UserModuleID,
                    UserName: SageFrameUserName,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 10;
                if ($("#menuList ul li.sfSelected").length > 0)
                    this.ajaxCall(this.config);
            },
            BindMenuPermission: function (data) {
                var PagePermission = data.d;
                var roles = $('div.divPermissions').find("tr:gt(0)");
                var user = $('#dvUser').find("tr:gt(0)");
                var htmlArr = [];
                $.each(PagePermission, function (indx, item) {
                    if (item.Username != "") {
                        var viewStatus = false;
                        var editStatus = "checked='checked'";
                        if ($(item).find('td:eq(0)').html() == item.Username && item.PermissionID == 1) {
                            viewStatus = "checked='checked'";
                        }
                        else if ($(item).find('td:eq(0)').html() == item.Username && item.PermissionID == 2) {
                            editStatus = "checked='checked'";
                        }
                        htmlArr.push('<tr><td>' + item.Username + '</td><td><input type="checkbox" title="view" class="sfCheckbox" ' + viewStatus + '></td><td><input type="checkbox" title="edit" ' + editStatus + ' class="sfCheckbox"></td><td><a class="delete" href="#" rel="imagessageframe.jpg"><img src=' + SageFrame.utils.GetAdminImage("imgdelete") + ' id="imgDelete" class="delete"></a></td></tr>');

                    }
                    else {

                        $.each(roles, function (index, itm) {

                            if ($(itm).prop('id').toLowerCase() == item.RoleID.toLowerCase() && item.PermissionID == 1) {
                                $(itm).find('input[title="view"]').prop('checked', true);
                            }
                            else if ($(itm).prop('id').toLowerCase() == item.RoleID.toLowerCase() && item.PermissionID == 2) {
                                $(itm).find('input[title="edit"]').prop('checked', true);
                            }
                        });
                    }
                });
                $('#tblUser').html(htmlArr.join(''));
            },
            LoadRoles: function () {
                this.config.method = "GetPortalRoles";
                this.config.url = MenuMgr.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    PortalID: MenuMgr.config.PortalID,
                    UserName: MenuMgr.config.UserName,
                    userModuleId: MenuMgr.config.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 7;
                this.ajaxCall(this.config);
            },
            BindPortalRoles: function (data) {
                var html = '';
                if (data.d.length > 0)
                    html += '<tr><th>Role</th><th>View</th><th>Edit</th><th>&nbsp;</th></tr>';
                $.each(data.d, function (index, item) {
                    var accesscontrolled = item.RoleName.toLowerCase() === "superuser" || item.RoleName.toLowerCase() === "super user" ? 'checked="checked" disabled="true"' : "";
                    var rowStyle = index % 2 == 0 ? 'class="sfEven"' : 'class="sfOdd"';
                    html += '<tr ' + rowStyle + ' id=' + item.RoleID + '><td width="40%"><label>' + item.RoleName + '</label></td><td><input type="checkbox" ' + accesscontrolled + ' class="sfCheckbox" title="view" /></td>';
                    html += '<td><input type="checkbox" ' + accesscontrolled + ' class="sfCheckbox" title="edit"/></td><td>&nbsp;</td></tr>';
                });
                $('div.divPermissions').find("table").html(html);
                MenuMgr.InitCustomControls();
                MenuMgr.LoadMenuPermission();
            },
            ClearControls: function () {

                $("input[type='checkbox']").each(function () {
                    if (this.checked == true)
                        this.checked = false;
                });

            },
            ClearTreeSelection: function () {
                $('#chkIsActivePage').prop("checked", false);
                $('#chkIsVisiblePage').prop("checked", false);
                $('#pageTree').find("li span").removeClass('ui-tree-selected');
                $('#imgAddmenuItem').text("Add Menu Item");
                if ($('#rdbPages').prop("checked") || $('#rdbAdminPages').prop("checked")) {
                    $("#trPages").slideDown("fast");
                    $('div.divSubText').slideUp("fast");
                }
            },
            SavePermissionSetting: function (MenuID, PortalID) {
                //MenuID,PermissionID,RoleId,Username,AllowAccess,PortalID
                this.config.method = "AddMenuPermission";
                this.config.url = MenuMgr.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    lstMenuPermissions: MenuMgr.config.lstMenuPermission,
                    MenuID: parseInt(MenuID),
                    PortalID: parseInt(PortalID),
                    userModuleId: MenuMgr.config.UserModuleID,
                    UserName: SageFrameUserName,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 8;
                this.ajaxCall(this.config);
            },
            InitCustomControls: function () {
                $("input:submit,input:button").button();
                $("#divAddUsers").dialog({
                    autoOpen: false,
                    width: 350,
                    modal: true

                });
            },
            //SearchUsers: function () {
            //    this.config.method = "SearchUsers";
            //    this.config.url = MenuMgr.config.baseURL + this.config.method;
            //    this.config.data = JSON2.stringify({
            //        SearchText: $('#txtSearchUsers').val(),
            //        PortalID: MenuMgr.config.PortalID,
            //        UserName: MenuMgr.config.UserName,
            //        userModuleId: MenuMgr.config.UserModuleID,
            //        secureToken: SageFrameSecureToken
            //    });
            //    this.config.ajaxCallMode = 9;
            //    this.ajaxCall(this.config);
            //},
            BindUsers: function (data) {
                var html = '<ul>';
                if (data.d.length > 1) {
                    $('#divSearchedUsers').show();
                }
                $.each(data.d, function (index, item) {
                    html += '<li id=' + item.UserID + '>' + item.UserName + '</li>';
                });
                html += '</ul>';
                $('#divSearchedUsers').html(html);
            },
            //..end permission.
            LoadMenuItemDetails: function (_MenuItemID) {
                var param = JSON2.stringify({
                    MenuItemID: parseInt(_MenuItemID),
                    PortalID: SageFramePortalID,
                    userModuleId: MenuMgr.config.UserModuleID,
                    UserName: SageFrameUserName,
                    secureToken: SageFrameSecureToken
                });
                $.ajax({
                    type: MenuMgr.config.type,
                    contentType: MenuMgr.config.contentType,
                    cache: MenuMgr.config.cache,
                    url: MenuMgr.config.baseURL + "GetDetails",
                    data: param,
                    async: false,
                    dataType: MenuMgr.config.dataType,
                    success: function (msg) {
                        var data = msg.d;
                        $('#selMenuItem').val(data.ParentID);
                        switch (parseInt(data.LinkType)) {
                            case 0:
                                $('div.divSubText,#trSubtext').slideDown();
                                $('div.divExternalLink,div.divHtmlContent,#pageMenuitem').hide();
                                //MenuMgr.GetPages(false);
                                $('#trPages').slideUp(500);
                                $('#rdbPages').prop("checked", true);
                                $('div.sfRadiobutton').find('label').removeClass('sfActive');
                                $('#rdbPages').parent().addClass("sfActive");
                                $('#txtSubText').val(data.Caption);
                                $('#chkIsVisiblePage').prop('checked', data.IsVisible);
                                $('#chkIsActivePage').prop('checked', data.IsActive);
                                break;
                            case 1:
                                $('div.divHtmlContent,#pageMenuitem').slideDown();
                                $('div.divExternalLink,#trPages,div.divSubText,#trSubtext').hide();
                                $('div.sfRadiobutton').find('label').removeClass('sfActive');
                                $('#rdbHtmlContent').parent().addClass("sfActive");
                                $('#rdbHtmlContent').prop("checked", true);
                                $('#txtTitleHtmlContent').val(data.Title);
                                $('#txtCaptionHtmlContent').val(data.Caption);
                                if (data.ImageIcon != "") {
                                    var image = MenuMgr.config.iconPath + "/PageImages/" + data.ImageIcon;
                                    var html = '<div><img class="sfIcon" Title=' + data.ImageIcon + ' src="' + image + '" /><span class="deleteIcon"><i class="delete icon-close"></i></span></div>';
                                    $('div.sfUploadedFilesHtmlContent').html(html);
                                }
                                $('#selMenuItem').val(data.ParentID);
                                $('#txtHtmlContent').val(data.HtmlContent);
                                if (data.IsVisible == true) {
                                    $('#chkVisibleHtmlContent').prop('checked', 'checked');
                                }
                                else {
                                    $('#chkVisibleHtmlContent').prop('checked', false);
                                }
                                break;
                            case 2:
                                $('div.divExternalLink,#pageMenuitem').slideDown();
                                $('div.divHtmlContent,#trPages,div.divSubText').hide();
                                $('div.sfRadiobutton').find('label').removeClass('sfActive');
                                $('#rdbExternalLink').parent().addClass("sfActive");
                                $('#rdbExternalLink').prop("checked", true);
                                $('#txtLinkTitle').val(data.Title);
                                $('#txtExternalLink').val(data.LinkURL);
                                $('#txtCaptionExtLink').val(data.Caption);
                                if (data.ImageIcon != "") {
                                    var image = MenuMgr.config.iconPath + "/PageImages/" + data.ImageIcon;
                                    var html = '<div><img class="sfIcon" Title=' + data.ImageIcon + ' src="' + image + '" /><span class="deleteIcon"><i class="delete icon-close" ><i></span></div>';
                                    $('div.sfUploadedFilesExtLink').html(html);
                                }
                                $('#selMenuItem').val(data.ParentID);
                                if (data.IsVisible == true) {
                                    $('#chkLinkVisible').prop('checked', 'checked');
                                }
                                else {
                                    $('#chkLinkVisible').prop('checked', false);
                                }
                                if (data.IsActive == true) {
                                    $('#chkLinkActive').prop('checked', 'checked');
                                }
                                else {
                                    $('#chkLinkActive').prop('checked', false);
                                }
                                break;
                        }
                        // MenuMgr.LoadMenuItem($("#menuList ul li.sfSelected").attr("id"));
                    }
                });
            },
            ajaxSuccess: function (data) {
                switch (MenuMgr.config.ajaxCallMode) {
                    case 0:
                        MenuMgr.BindMenuList(data);
                        break;
                    case 1:
                        SageFrame.messaging.show("Setting successfully saved", "Success");
                        $('.sfDivFlying').hide();
                        break;
                    case 2:
                        MenuMgr.BindPages(data);
                        break;
                    case 3:
                        break;
                    case 4:
                        MenuMgr.BindParentPages(data);
                        break;
                    case 5:
                        MenuMgr.BindMenu(data);
                        break;
                    case 6:
                        MenuMgr.BindMenuSettings(data);
                        break;
                    case 7:
                        MenuMgr.BindPortalRoles(data);
                        break;
                    case 8:
                        SageFrame.messaging.show("Menu Saved Successfully", "Success");
                        break;
                    case 9:
                        //MenuMgr.BindUsers(data);
                        break;
                    case 10:
                        MenuMgr.BindMenuPermission(data);
                        break;
                    case 11:
                        $('#divExternalLink,#divHtmlContent,div.divSubText,#trSubtext,#pageMenuitem').hide();
                        $('#trPages').slideDown();
                        MenuMgr.GetPages(false);
                        MenuMgr.LoadMenuItem($("#menuList").find("ul li.sfSelected").prop("id"));
                        MenuMgr.CheckPage();
                        $('#rdbPages').prop("checked", true);
                        break;
                    case 12:
                        MenuMgr.BindParentPagesOnly(data);
                }
            },
            ajaxFailure: function () {
                return false;
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: MenuMgr.config.type,
                    contentType: MenuMgr.config.contentType,
                    cache: MenuMgr.config.cache,
                    async: MenuMgr.config.async,
                    url: MenuMgr.config.url,
                    data: MenuMgr.config.data,
                    dataType: MenuMgr.config.dataType,
                    success: MenuMgr.ajaxSuccess,
                    error: MenuMgr.ajaxFailure
                });

            },
            GetPrefixes: function (level) {
                var prefix = "";
                for (var i = 0; i < level; i++) {
                    prefix += "---";
                }
                return prefix;
            },
            IconUploaderExtLink: function () {
                var uploadFlag = false;
                var upload = new AjaxUpload($('#fupIconExtLink'), {
                    action: MenuMgr.config.Path + 'UploadHandler.ashx?userModuleId=' + MenuMgr.config.UserModuleID + '&portalID=' + SageFramePortalID + '&userName=' + SageFrameUserName + '&sageFrameSecureToken=' + SageFrameSecureToken,
                    name: 'myfile[]',
                    multiple: false,
                    data: {
                    },
                    autoSubmit: true,
                    responseType: 'json',
                    onChange: function (file, ext) {
                    },
                    onSubmit: function (file, ext) {
                        ext = ext.toLowerCase();
                        if (ext == "png" || ext == "jpg" || ext == "gif" || ext == "bmp" || ext == "JPEG" || ext == "jpeg" || ext == "ico") {
                            return true;
                        }
                        else {
                            jAlert('Not a valid image file!', 'Sage Alert');
                            return false;
                        }
                    },
                    onComplete: function (file, response) {
                        if (response == "LargeImagePixel") {
                            return ConfirmDialog(this, 'message', "The image size is too large in pixel");
                        }
                        var Eximage = file.split(" ").join("_");
                        var filePath = MenuMgr.config.iconPath + "/PageImages/" + Eximage;
                        $('span.filename').text(Eximage);
                        var html = '<div><img class="sfIcon" title="' + Eximage + '" src="' + filePath + '" /><span class="deleteIcon"><i class="delete icon-close" ></i></span></div>';
                        $('div.sfUploadedFilesExtLink').html(html);
                    }
                });
            },
            IconUploaderHtmlContent: function () {
                var uploadFlag = false;
                var upload = new AjaxUpload($('#fupIconHtmlContent'), {
                    action: MenuMgr.config.Path + 'UploadHandler.ashx?userModuleId=' + MenuMgr.config.UserModuleID + '&portalID=' + SageFramePortalID + '&userName=' + SageFrameUserName + '&sageFrameSecureToken=' + SageFrameSecureToken,
                    name: 'myfile[]',
                    multiple: false,
                    data: {},
                    autoSubmit: true,
                    responseType: 'json',
                    onChange: function (file, ext) {
                    },
                    onSubmit: function (file, ext) {
                        ext = ext.toLowerCase();
                        if (ext == "png" || ext == "jpg" || ext == "gif" || ext == "bmp" || ext == "JPEG" || ext == "jpeg" || ext == "ico") {
                            return true;
                        }
                        else {
                            jAlert('Not a valid image file!', 'Sage Alert');
                            return false;
                        }
                    },
                    onComplete: function (file, response) {
                        if (response == "LargeImagePixel") {
                            return ConfirmDialog(this, 'message', "The image size is too large in pixel");
                        }
                        var fileimage = file.split(" ").join("_");
                        var filePath = MenuMgr.config.iconPath + "/PageImages/" + fileimage;
                        $('span.filename').text(fileimage);
                        var html = '<div><img class="sfIcon" title="' + fileimage + '" src="' + filePath + '" /><span class="deleteIcon icon-close"><i class="delete" ></i></span></div>';
                        $('div.sfUploadedFilesHtmlContent').html(html);
                    }
                });
            },
            AddDragDrop: function () {
                $('#pageTree').tree({
                    expand: 'false',
                    tolerance: ".noDrag",
                    droppable: [
            		            {
            		                element: 'li.ui-tree-node',
            		                //tolerance: 'around',
            		                aroundTop: '25%',
            		                aroundBottom: '25%',
            		                aroundLeft: 0,
            		                aroundRight: 0
            		            },
            		            {
            		                element: 'li.ui-tree-list,li.ui-tree-node',
            		                //tolerance: 'around',
            		                aroundTop: '25%',
            		                aroundBottom: '25%',
            		                aroundLeft: 0,
            		                aroundRight: 0
            		            }
                    ],
                    drop: function (event, ui) {

                        var draggebleSpanID = $('#pageTree').find('li.ui-draggable-dragging');
                        var dropableSpanID = $('#' + $(this).find('li span.ui-tree-droppable').parents('li').attr("id"));
                        var mouseTopPosition = event.pageY - draggebleSpanID.offset().top;
                        var dropableSpanHeight = $(this).find('li span.ui-tree-droppable').parents('li').height();
                        var portionOne = dropableSpanHeight / 4;
                        var separateLevelOne = dropableSpanHeight - portionOne;
                        var separateLevelTwo = dropableSpanHeight - portionOne * 3;
                        var draggebleSpanTopPosition = draggebleSpanID.position().top;
                        var dropableSpanTopPosition = dropableSpanID.position().top;
                        var difference = draggebleSpanTopPosition + mouseTopPosition - dropableSpanTopPosition; // Math.abs(
                        var returnOverStatePosition = '';

                        //alert("separateLevelOne: " + separateLevelOne + " separateLevelTwo: " + separateLevelTwo + " difference: " + difference + " draggebleSpanTopPosition: " + draggebleSpanTopPosition + " mouseTopPosition: " + mouseTopPosition + " dropableSpanTopPosition: " + dropableSpanTopPosition);
                        if ((separateLevelOne) < difference) {
                            returnOverStatePosition = 'bottom';
                        } else if ((separateLevelTwo) < difference) {
                            returnOverStatePosition = 'center';
                        } else {
                            returnOverStatePosition = 'top';
                        }
                        $('.ui-tree-droppable').removeClass('ui-tree-droppable ui-tree-droppable-top ui-tree-droppable-center ui-tree-droppable-bottom');
                        switch (returnOverStatePosition) {
                            case 'top':
                                ui.target.before(ui.sender.getJSON(ui.draggable), ui.droppable);
                                ui.sender.remove(ui.draggable);
                                var MenuItemID = $(ui.draggable).prop("id");
                                var ParentID = $(ui.droppable).closest('ul').parent('li').prop('id');
                                var BeforeID = $(ui.droppable).parent("li").prop("id");
                                var AfterID = 0;
                                ParentID = ParentID == null ? 0 : ParentID;
                                MenuMgr.SortMenu(MenuItemID, ParentID, BeforeID, AfterID, 1);
                                break;
                            case 'bottom':
                                ui.target.after(ui.sender.getJSON(ui.draggable), ui.droppable);
                                ui.sender.remove(ui.draggable);
                                var MenuItemID = $(ui.draggable).prop("id");
                                var ParentID = $(ui.droppable).closest('ul').parent('li').prop('id');
                                var BeforeID = 0;
                                var AfterID = $(ui.droppable).parent("li").prop("id");
                                ParentID = ParentID == null ? 0 : ParentID;
                                MenuMgr.SortMenu(MenuItemID, ParentID, BeforeID, AfterID, 1);
                                break;

                            case 'center':
                                ui.target.append(ui.sender.getJSON(ui.draggable), ui.droppable);
                                ui.sender.remove(ui.draggable);
                                $(ui.droppable).parent('li').addClass('ui-tree-expanded');
                                $(ui.droppable).parent('li').removeClass('ui-tree-list');
                                $(ui.droppable).parent('li').addClass('ui-tree-node');
                                var MenuItemID = $(ui.draggable).prop("id");
                                var ParentID = $(ui.droppable).parent('li').prop('id');
                                var BeforeID = 0;
                                var AfterID = 0;
                                ParentID = ParentID == null ? 0 : ParentID;
                                MenuMgr.SortMenu(MenuItemID, ParentID, BeforeID, AfterID, 1);
                                break;
                        }
                    },
                    over: function (event, ui) {
                        $(ui.droppable).addClass('ui-tree-droppable');
                    },
                    out: function (event, ui) {
                        $(ui.droppable).removeClass('ui-tree-droppable');
                    },
                    overtop: function (event, ui) {
                        $(ui.droppable).addClass('ui-tree-droppable-top');
                    },
                    overcenter: function (event, ui) {
                        $(ui.droppable).addClass('ui-tree-droppable-center');
                    },
                    overbottom: function (event, ui) {
                        $(ui.droppable).addClass('ui-tree-droppable-bottom');
                    },
                    outtop: function (event, ui) {
                        $(ui.droppable).removeClass('ui-tree-droppable-top');
                    },
                    outcenter: function (event, ui) {
                        $(ui.droppable).removeClass('ui-tree-droppable-center');
                    },
                    outbottom: function (event, ui) {
                        $(ui.droppable).removeClass('ui-tree-droppable-bottom');
                    },
                    click: function (event, ui) {
                    }
                });
            }, AddContextMenu: function () {
                $('#pageTree>li').each(function (i) {

                    $(this).contextMenu('myMenu1', {
                        bindings: {
                            'add': function (t) {
                                PageTreeView.ClearControls();
                            },
                            'addmodule': function (t) {
                            },
                            'remove': function (t) {
                                var arrVal = new Array();
                                arrVal = $(t).find('span.ui-tree-selected').parent('li').prop('id').split('_');
                                $(t).find('span.ui-tree-selected').parent('li').remove();
                                MenuMgr.DeleteLink(arrVal[0]);
                            }
                        }
                    });
                });
            },
            DeleteLink: function (MenuItemID) {
                this.config.method = "DeleteLink";
                this.config.url = MenuMgr.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    MenuItemID: MenuItemID,
                    PortalID: SageFramePortalID,
                    UserModuleID: MenuMgr.config.UserModuleID,
                    UserName: SageFrameUserName,
                    secureToken: SageFrameSecureToken,
                    CultureCode: p.CultureCode
                });
                this.config.ajaxCallMode = 11;
                this.ajaxCall(this.config);
            },
            SortMenu: function (menuitemid, parentid, beforeid, afterid, portalid) {
                $.ajax({
                    type: MenuMgr.config.type,
                    contentType: MenuMgr.config.contentType,
                    cache: MenuMgr.config.cache,
                    url: MenuMgr.config.baseURL + "SortMenu",
                    data: JSON2.stringify({
                        MenuItemID: parseInt(menuitemid),
                        ParentID: parseInt(parentid),
                        BeforeID: parseInt(beforeid),
                        AfterID: parseInt(afterid),
                        PortalID: portalid,
                        userModuleId: MenuMgr.config.UserModuleID,
                        UserName: SageFrameUserName,
                        secureToken: SageFrameSecureToken,
                        cultureCode: p.CultureCode
                    }),
                    dataType: MenuMgr.config.dataType,
                    success: function (msg) {

                    }

                });
            },
            SaveSetting: function () {
                this.config.method = "AddSetting";
                this.config.url = MenuMgr.config.baseURL + this.config.method;
                this.config.data = MenuMgr.GetSettingList(),
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },
            GetSettingList: function () {
                var _SettingValue = "";
                var displaymode = $('#chkShowImage').prop("checked") && $('#chkShowText').prop("checked") ? 2
                                : $('#chkShowImage').prop("checked") && !$('#chkShowText').prop("checked") ? 0 : 1;
                var Caption = $('#chkCaption').prop("checked") ? 1 : 0;
                var Level = $('#chkCaption').prop("checked") ? $('#selLevel').val() : 0;
                var SettingKeys = ["MenuType", "DisplayMode", "TopMenuSubType", "Caption", "SubTitleLevel", "SideMenuType"];
                var SettingValues = [$("input:radio[name=rdbChooseMenuType]:checked").val(), displaymode, $("input:radio[name=rdbMenuTypeStyle]:checked").val(), Caption, Level, 2];
                var MenuID = parseInt($("#menuList ul li.sfSelected").prop("id"));
                var param = {
                    lstSettings: [],
                    PortalID: SageFramePortalID,
                    UserModuleID: MenuMgr.config.UserModuleID,
                    UserName: SageFrameUserName,
                    secureToken: SageFrameSecureToken,
                    CultureCode: p.CultureCode
                };
                $.each(SettingKeys, function (index, item) {
                    _SettingValue = SettingValues[index];
                    param.lstSettings.push({ "MenuID": MenuID, "SettingKey": item, "SettingValue": _SettingValue, "IsActive": true, "PortalID": MenuMgr.config.PortalID, "UpdatedBy": MenuMgr.config.UserName, "AddedBy": MenuMgr.config.UserName });

                });
                return JSON2.stringify(param);
            },
            LoadMenuSettings: function () {
                var MenuID = $("#menuList").find("ul li.sfSelected").prop("id");
                if (MenuID != undefined) {
                    MenuID = parseInt(MenuID);
                    this.config.method = "GetMenuSettings";
                    this.config.url = MenuMgr.config.baseURL + this.config.method;
                    this.config.data = JSON2.stringify({
                        PortalID: MenuMgr.config.PortalID,
                        MenuID: MenuID,
                        UserModuleID: MenuMgr.config.UserModuleID,
                        UserName: SageFrameUserName,
                        secureToken: SageFrameSecureToken
                    });
                    this.config.ajaxCallMode = 6;
                    this.ajaxCall(this.config);
                }
            },
            BindMenuSettings: function (data) {
                var settings = data.d;
                switch (parseInt(settings.MenuType)) {
                    case 1:
                        $('#rdbHorizontalMenu').prop("checked", "checked");
                        $('#rdbHorizontalMenu').trigger('click');
                        //$('#tblSideMenu').slideUp();
                        $('#tblTopClientMenu').slideDown();
                        break;
                    case 2:
                        $('#rdbSideMenu').prop("checked", "checked");
                        $('#rdbSideMenu').trigger('click');
                        //$('#tblSideMenu').slideDown();
                        $('#tblTopClientMenu').slideUp();
                        break;
                    case 3:
                        $('#rdbFooter').prop("checked", "checked");
                        $('#rdbFooter').trigger('click');
                        //$('#tblSideMenu').slideUp();
                        $('#tblTopClientMenu').slideUp();
                        break;

                }
                switch (parseInt(settings.TopMenuSubType)) {
                    case 1:
                        $('#rdbFlyOutMenu').prop("checked", "checked");
                        MenuMgr.ChangeRdbActiveClass($('#rdbFlyOutMenu'));
                        break;
                    case 2:
                        $('#rdbNavBar').prop("checked", "checked");
                        MenuMgr.ChangeRdbActiveClass($('#rdbNavBar'));
                        break;
                    case 3:
                        $('#rdbDropdown').prop("checked", "checked");
                        MenuMgr.ChangeRdbActiveClass($('#rdbDropdown'));
                        break;
                    case 4:
                        $('#rdbCssMenu').prop("checked", "checked");
                        MenuMgr.ChangeRdbActiveClass($('#rdbCssMenu'));
                        break;
                }
                switch (parseInt(settings.DisplayMode)) {
                    case 0:
                        $('#chkShowImage').prop("checked", "checked");
                        $('#chkShowText').prop("checked", false);
                        break;
                    case 1:
                        $('#chkShowImage').prop("checked", false);
                        $('#chkShowText').prop("checked", true);
                        break;
                    case 2:
                        $('#chkShowImage').prop("checked", true);
                        $('#chkShowText').prop("checked", true);
                        break;
                }

                switch (parseInt(settings.Caption)) {
                    case 0:
                        $('#chkCaption').prop("checked", false);
                        break;
                    case 1:
                        $('#chkCaption').prop("checked", "checked");
                        break;
                }
                switch (parseInt(settings.SideMenuType)) {
                    case 1:
                        $('#rdbDynamic').prop("checked", true);
                        break;
                    case 2:
                        $('#rdbCustom').prop("checked", true);
                        break;
                }
                $('#selLevel').val(parseInt(settings.TopMenuSubType));
            }
        };
        MenuMgr.init();
    }
    $.fn.MenuManager = function (p) {
        $.createMenu(p);
    };
})(jQuery);