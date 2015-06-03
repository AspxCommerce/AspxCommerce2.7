(function ($) {
    $.createPageBuilder = function (p) {
        p = $.extend
        ({
            PortalID: 1,
            UserModuleID: 1,
            UserName: 'user',
            PageName: 'Home',
            ContainerClientID: 'divNav1',
            CultureCode: 'en-US',
            baseURL: Path + 'Services/PagesWebService.asmx/',
            AppName: "/sageframe",
            HostURL: "",
            StartupPage: 'Home',
            ActiveTemplateName: 'Default',
            PageExtension: ''
        }, p);
        var SagePages = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: { data: '' },
                dataType: 'json',
                baseURL: Path + 'Services/PagesWebService.asmx/',
                method: "",
                url: "",
                categoryList: "",
                ajaxCallMode: 0,
                lstPagePermission: [],
                Mode: false,
                PageID: 0,
                PortalID: p.PortalID,
                arrUsers: [],
                OldPage: "",
                IsPageDuplicate: false,
                lstAdminPages: [],
                lstNormalPages: []
            },
            messages:
                {
                    nomenu: "No Menu"
                },
            init: function (config) {
                this.InitializeCotrols();
                this.InitToolTips();
                this.LoadRoles();
                this.BindEvents();
                this.IconUploader();
                // SagePages.GetMenuList();
            },
            InitializeCotrols: function () {
                $('#txtStartDate').datepicker();
                $('#txtEndDate').datepicker({ rangeSelect: true, numberOfMonths: 2 });
            },
            ajaxSuccess: function (data) {
                switch (parseInt(SagePages.config.ajaxCallMode)) {
                    case 0:
                        SagePages.BindPortalRoles(data);
                        break;
                    case 1:
                        //SagePages.BindUsers(data);
                        break;
                    case 2:
                        SagePages.BindPageDetails(data);
                        SagePages.IconUploaderAdmin();
                        break;
                    case 3:
                        SagePages.RefreshPage(data);
                        break;
                    case 4:
                        SagePages.BindChildPages(data);
                        break;
                    case 5:
                        SagePages.BindPageModules(data);
                        break;
                    case 6:
                        //SagePages.CheckDuplicatePages(data);
                        break;
                    case 7:

                        //                        var isPublish = $('#btnpublish').attr("name");
                        //                        if (isPublish == "true")
                        //                            isPublish = "false";
                        //                        else
                        //                            isPublish = "true";
                        //                        this.config.method = "PublishPage";
                        //                        this.config.url = this.config.baseURL + this
                        if (data.d == true) {
                            var isPublish = $('#btnpublish').attr("name");
                            if (isPublish == "true") {
                                $('#btnpublish').attr("name", "false").addClass('icon-checked').removeClass('icon-unchecked');
                                $('#navContainer').find('#categoryTree li.active').find('.ui-tree-selected').eq(0).find('span.true').attr('ispublish', 'true')
                            }
                            else {
                                $('#btnpublish').attr("name", "true").addClass('icon-unchecked').removeClass('icon-checked');
                                $('#navContainer').find('#categoryTree li.active').find('.ui-tree-selected').eq(0).find('span.true').attr('ispublish', 'false');
                            }
                        } else {
                        }
                        break;
                }
            },
            ajaxFailure: function () {
                //SageFrame.messaging.show("Some kind of error occured", "Error");
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: SagePages.config.type,
                    contentType: SagePages.config.contentType,
                    cache: SagePages.config.cache,
                    url: SagePages.config.url,
                    data: SagePages.config.data,
                    dataType: SagePages.config.dataType,
                    success: SagePages.ajaxSuccess,
                    error: SagePages.ajaxFailure,
                    async: false
                });
            },
            InitToolTips: function () {
                //SageFrame.tooltip.GetTextBoxToolTipImage("txtPageName", "The name of the page");
                //SageFrame.tooltip.GetTextBoxToolTipImage("txtRefreshInterval", "The time interval after which page refreshes.</br> Store in the meta tags");
            },
            BindEvents: function () {
                var v = $("#form1").validate({
                    ignore: ':hidden',
                    rules: {
                        txtPageName: { required: true },
                        txtTitle: { required: true }
                    },
                    messages: {
                        txtPageName: "<br/>Please enter a Page Name",
                        txtTitle: "<br/>Please enter a Title"
                    }
                });
                $('#btnCancelUser').bind("click", function () {
                    $('#divAddUsers').dialog("close");
                });
                var attrTitleValue;
                $('#txtPageName').hover(function () {
                    attrTitleValue = $(this).prop("title");
                    $(this).prop("title", "");
                }, function () {
                    $(this).prop("title", attrTitleValue);
                });
                $('#txtRefreshInterval').keyup(function () {
                    $("#lblIntegerError").text('');
                    var refreshInterval = $('#txtRefreshInterval').val();
                    if (isNaN(refreshInterval)) {
                        $('#txtRefreshInterval').after('<label class="Error"  id="lblIntegerError"><br/>Please Enter Positive Numeric Value.</label>');
                        return false;
                    }
                    else if (refreshInterval < 0) {
                        $('#txtRefreshInterval').after('<label class="Error"  id="lblIntegerError"><br/>Please Enter Positive Numeric Value.</label>');
                        return false;
                    }
                    else {
                        $("#lblIntegerError").hide();
                    }
                });
                $('#btnAddUser').bind("click", function () {
                    var users = $('#divAddUsers ul li.sfActive');
                    var html = '';
                    $.each(users, function (index, item) {
                        var userid = $(this).prop("id");
                        var username = $(this).html();
                        var rowStyle = index % 2 == 0 ? 'class="sfEven"' : 'class="sfOdd"';
                        if (!SagePages.UserAlreadyExists(username)) {
                            html += '<tr ' + rowStyle + ' id=' + userid + '><td width="40%"><label>' + username + '</label></td><td width="20%"><input type="checkbox" class="sfCheckbox" title="view"/></td>';
                            html += '<td width="20%"><input type="checkbox" class="sfCheckbox" title="edit"/></td><td width="10%"><i class="icon-delete" id="imgDelete"></i></td></tr>';
                        }
                    });
                    $('#tblUser').show();
                    $('#dvUser table').append(html);
                    $('#divAddUsers').dialog("close");
                });
                $('#trIncludeInMenu').hide();
                var appPath = SageFrame.utils.getapplicationname();
                var href = SageFramePortalID > 1 ? appPath + "/portal/" + SageFramePortalName + '/Admin/Page-Modules' + p.PageExtension : appPath + "/Admin/Page-Modules" + p.PageExtension;
                $('#btnManageModules').prop("href", href);
                $('#rdrtManagemodule').prop("href", href);
                //$('#imbAddUsers').bind("click", function () {
                //    $('#txtSearchUsers').val('');
                //    $('#divSearchedUsers ul').html("");
                //    $('#btnAddUser,#btnCancelUser').hide();
                //    if ($('#divSearchedUsers li').hasClass('sfActive'))
                //        $('#btnAddUser,#btnCancelUser').show();
                //    else
                //        $('#btnAddUser,#btnCancelUser').hide();
                //    $("#divAddUsers").dialog("open");
                //});
                //$('#btnSearchUsers').on("click", function () {
                //    SagePages.SearchUsers();
                //});
                //$('#txtSearchUsers').keyup(function (event) {
                //    if (event.keyCode == 13) {
                //        SagePages.SearchUsers();
                //    }
                //});
                //$('#divSearchedUsers').on("click", 'li', function () {
                //    if (!$(this).hasClass("sfActive")) {
                //        $(this).addClass("sfActive");
                //    }
                //    else {
                //        $(this).removeClass("sfActive");
                //    }
                //    if ($('#divSearchedUsers li').hasClass('sfActive')) {
                //        $('#btnAddUser,#btnCancelUser').show();
                //    }
                //    else {
                //        $('#btnAddUser,#btnCancelUser').hide();
                //    }
                //});
                $('.cssClassUploadFiles').on('click', 'span.deleteIcon', function () {
                    var iconPath = $('.cssClassUploadFiles img').prop('title');
                    $(this).parent('div').html('');
                    $('span.filename').text('No file selected');
                    SagePages.DeleteIcon(iconPath);
                });
                $('#txtPageName').bind("change", function () {
                    if ($('#txtPageName').val() != "") {
                        if (!SageFrame.utils.ContainsInvalidChar($('#txtPageName').val())) {
                            $('#txtPageName').next("label").remove();
                            $('#txtPageName').after("<label id='lblInvalid' class='sfError sfInvalid'><br/>Contains Invalid Characters</label>");
                        }
                        else {
                            $('label.sfInvalid').remove();
                        }
                    }
                });
                $('#txtPageName').bind("keypress", function () {
                    $('#spnPagename').remove();
                    $('#lblInvalid').remove();

                });
                $('#txtTitle').bind("keypress", function () {
                    $('#spnTitle').remove();
                });
                $('#txtDescription').bind("keypress", function () {
                    $('#spnDescription').remove();
                });
                $('#txtCaption').bind("keypress", function () {
                    $('#spnCaption').remove();
                });
                $('#btnSubmit').bind("click", function () {
                    $('#txtPageName').removeAttr("readonly").removeClass("sfDisable");
                    var refreshInterval = $('#txtRefreshInterval').val();
                    SagePages.config.lstNormalPages = [];
                    SagePages.config.lstAdminPages = [];
                    if (isNaN(refreshInterval)) {
                        $("#lblIntegerError").text('');
                        $('#txtRefreshInterval').after('<label class="Error"  id="lblIntegerError"><br/>Please Enter Positive Numeric Value.</label>');
                        return false;
                    }
                    else if (refreshInterval < 0) {
                        $("#lblIntegerError").text('');
                        $('#txtRefreshInterval').after('<label class="Error"  id="lblIntegerError"><br/>Please Enter Positive Numeric Value.</label>');
                        return false;
                    }
                    else if (Date.parse($('#txtStartDate').val()) > Date.parse($('#txtEndDate').val())) {
                        $("#lblError").html("<br/>End date should be after start date");
                        return false;
                    }
                    else {
                        $("#lblError").text("");
                    }
                    if (v.form()) {
                        var pageName = String($('#txtPageName').val());
                        var status = false;
                        SagePages.GetPages();
                        var pageList = $('#categoryTree li');
                        var pageID = $('#txtPageName').prop('title');
                        var pageList = $('#categoryTree li');

                        var dropDownpageID = $('#cboParentPage option:selected').val();
                        if (pageID != 0) {
                            if (pageID == dropDownpageID) {
                                SageFrame.messaging.show("The Page can't be parent of itself", "Alert");
                                return;
                            }
                        }
                        if ($('#rdbFronMenu').prop('checked') == true) {
                            $.each(pageList, function (index, item) {
                                var self = $(this);
                                var id = self.attr('id');
                                if (id != pageID) {
                                    var pName = self.find('span.true').attr('pageName');
                                    SagePages.config.lstNormalPages.push(pName.toLowerCase())
                                }
                            });

                        }
                        else if ($('#rdbAdmin').prop('checked') == true) {
                            $.each(pageList, function (index, item) {
                                var self = $(this);
                                var id = self.attr('id');
                                if (id != pageID) {
                                    var pName = self.find('span.true').attr('pageName');
                                    SagePages.config.lstAdminPages.push(pName.toLowerCase())
                                }
                            });

                        }

                        if (SagePages.config.lstNormalPages.length > 0) {
                            $.each(SagePages.config.lstNormalPages, function (index, item) {
                                if (item == pageName.toLowerCase().trim().replace(/ /g, "-")) {
                                    status = true;
                                }
                            });
                        }
                        if (SagePages.config.lstAdminPages.length > 0) {
                            $.each(SagePages.config.lstAdminPages, function (index, item) {
                                if (item == pageName.toLowerCase().trim().replace(/ /g, "-")) {
                                    status = true;
                                }
                            });
                        }
                        if (!status) {
                            if ($('#txtPageName').val().length > 50 || $('#txtTitle').val().length > 60 || $('#txtDescription').val().length > 150 || $('#txtCaption').val().length > 25) {
                                var messagehtml = '';
                                if ($('#txtPageName').val().length > 50) {
                                    messagehtml = '';
                                    messagehtml = "<span id='spnPagename' class='sfError'><br>Page Name cannot be more than 50 chars long</span>";
                                    $('#txtPageName').after(messagehtml);
                                    $('#txtPageName').val('');
                                    $('#txtPageName').focus();
                                }
                                else {
                                    $('spnPagename').remove();
                                }
                                if ($('#txtTitle').val().length > 60) {
                                    messagehtml = '';
                                    messagehtml = "<span id='spnTitle'class='sfError'><br>Page Title cannot be more than 60 chars long</span>";
                                    $('#txtTitle').after(messagehtml);
                                    $('#txtTitle').val('');
                                    $('#txtTitle').focus();
                                }
                                else {
                                    $('spnTitle').remove();
                                }
                                if ($('#txtCaption').val().length > 25) {
                                    messagehtml = '';
                                    messagehtml = "<span id='spnCaption'class='sfError'><br>Caption  cannot be more than 25 chars long</span>";
                                    $('#txtCaption').after(messagehtml);
                                    $('#txtCaption').val('');
                                    $('#txtCaption').focus();
                                }
                                else {
                                    $('spnCaption').remove();
                                }
                                if ($('#txtDescription').val().length > 150) {
                                    messagehtml = '';
                                    messagehtml = "<span id='spnDescription' class='sfError'><br>Page Description cannot be more than 150 chars long</span>";
                                    $('#txtDescription').after(messagehtml);
                                    $('#txtDescription').val('');
                                    $('#txtDescription').focus();
                                }
                                else {
                                    $('spnDescription').remove();
                                }
                                return false;
                            }
                            if (!SageFrame.utils.ContainsInvalidChar($('#txtPageName').val())) {
                                $('#txtPageName').next("label").remove();
                                $('#txtPageName').after("<label class='sfError'><br/>Contains Invalid Characters</label>");
                            }
                            else {
                                if ($('#rdbFronMenu').prop('checked') == true) {
                                    SagePages.config.lstNormalPages.push(pageName.toLowerCase().trim().replace(/ /g, "-"));
                                }
                                else if ($('#rdbAdmin').prop('checked') == true) {
                                    SagePages.config.lstAdminPages.push(pageName.toLowerCase().trim().replace(/ /g, "-"));
                                }
                                SagePages.AddUpdatePage();
                                $("#flIcon").next('.filename').html('No file selected.');
                                $('#txtPageName').next("label").remove();
                                $('#txtTitle').next("label").remove();
                            }
                        }
                        else {
                            SageFrame.messaging.show("The page name should be unique. A page with a same name already exists", "Alert");
                        }
                    }
                    else {
                        return;
                    }
                });
                $('input[value="Cancel"]').click(function () {
                    SagePages.ClearControls();
                });
                $("#tblUser i.delete").on("click", function () {
                    $(this).parent().parent('tr').remove();
                });

                $('#cboParentPage').bind('change', function () {
                    SagePages.GetChildPages($('#cboParentPage').val(), null, null, null, p.UserName, p.PortalID);
                });
                $('#dvUser').on('click', '#imgDelete', function () {
                    $(this).parents('tr').remove();
                });
                //                $('#chkMenu').bind("click", function() {
                //                    if ($(this).prop("checked")) {
                //                        var selected = $('#categoryTree  span.ui-tree-selected').length;
                //                        $('#trIncludeInMenu').slideDown();
                //                        $('#lblSelectMenu').show();
                //                        $('#selMenulist').show();
                //                        SagePages.GetMenuList();
                //                    }
                //                    else {
                //                        $('#trIncludeInMenu').slideUp();
                //                        $('#lblSelectMenu').hide();
                //                    }
                //                });
                $("#btnpublish").click(function () {
                    SagePages.PublishPage();
                });
                $("#btnPreview").click(function () {
                    SagePages.PreviewPage();
                });
                $('label.sfError').remove();
                $('.sfPageDetailCancel').on('click', function () {
                    $('#imbPageCancel').click();
                });
            },
            GetPages: function () {
                this.config.method = "GetPortalPages";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    portalID: p.PortalID,
                    IsAdmin: $('#rdbFronMenu').prop('checked') ? true : false,
                    userName: p.UserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 6;
                this.ajaxCall(this.config);
            },
            CheckDuplicatePages: function (data) {
                var pages = data.d;
                SagePages.config.IsPageDuplicate = false;
                $.each(pages, function (index, item) {
                    if ($('#txtPageName').val().toLowerCase() == item.PageName.toLowerCase()) {
                        SagePages.config.IsPageDuplicate = true;
                    }
                });
            },
            IconUploaderAdmin: function () {
                var uploadFlag = false;
                var upload = new AjaxUpload($('#flIcon'), {
                    action: Path + 'UploadHandler.ashx',
                    name: 'myfile[]',
                    multiple: false,
                    data: { rdbChecked: "A" },
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
                            return ConfirmDialog(this, 'message', "Not a valid image file");
                            return false;
                        }
                    },
                    onComplete: function (file, response) {
                        if (response == "LargeImagePixel") {
                            return ConfirmDialog(this, 'message', "The image size is too large in pixel");
                        }
                        if (response == "large") {
                            return ConfirmDialog(this, 'message', "The image size is too large. Should be less than 1mb");
                        }
                        var pageimage = file.split(" ").join("_");
                        var filePath = p.HostURL + "/PageImages/" + pageimage;
                        $('span.filename').text(pageimage);
                        var html = '<img title="' + pageimage + '" src="' + filePath + '" /><span class="deleteIcon"><label class="sfBtn icon-close"></label></span>';
                        $('div.cssClassUploadFiles').html(html);
                    }
                });
            },
            BindParentPages: function () {
                var parentPages = LSTSagePages;
                var html = '';
                var selectedpage = $('#categoryTree span.ui-tree-selected').parent("li").prop("id");
                if ($('#rdbAdmin').prop("checked")) {
                    html += '<option value="2">---None---</option>';
                }
                else {
                    html += '<option value="0">---None---</option>';
                }
                $.each(parentPages, function (index, item) {
                    if (item.PageID != selectedpage && item.ParentID != selectedpage)
                        html += '<option value=' + item.PageID + '>' + String(item.PageName).replace(new RegExp("-", "g"), ' ') + '</option>';
                });
                $('#cboParentPage').html(html);
            },
            DeleteIcon: function (IconPath) {
                this.config.method = "DeleteIcon";
                this.config.url = SagePages.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    IconPath: IconPath,
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 6;
                this.ajaxCall(this.config);
            },
            LoadRoles: function () {
                this.config.method = "GetPortalRoles";
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
            UpdSettingKeyValue: function (ActiveTemplateName, PageName, OldPageName) {
                this.config.method = "UpdSettingKeyValue";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    ActiveTemplateName: ActiveTemplateName,
                    PageName: PageName,
                    OldPageName: OldPageName,
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 0;
                this.ajaxCall(this.config);
            },
            AddUpdatePage: function () {
                var Mode = '';
                var pageID = $('#txtPageName').prop('title');
                if (pageID > 0) {
                    Mode = "E";
                } else { Mode = "A"; }
                var UpdateLabel = '';
                if ($('#cboParentPage').val() == '0') {
                    UpdateLabel = "NA";
                } else { UpdateLabel = "PA"; }
                var checks = $('div.divPermission tr:gt(0), #dvUser tr').find('input.sfCheckbox:checked');
                SagePages.config.lstPagePermission = [];
                $.each(checks, function (index, item) {
                    if ($(this).prop("checked")) {
                        //if ($(this).closest('table').prop('id') == "tblUser")
                        //    SagePages.config.lstPagePermission[index] = { "PermissionID": $(this).prop('title') == "view" ? 1 : 2, "RoleID": null, "Username": $(this).closest('tr').find('td:eq(0) label').html(), "AllowAccess": true };
                        //else
                        //    SagePages.config.lstPagePermission[index] = { "PermissionID": $(this).prop('title') == "view" ? 1 : 2, "RoleID": $(this).closest('tr').prop('id'), "Username": "", "AllowAccess": true };
                        SagePages.config.lstPagePermission[index] = { "PermissionID": $(this).prop('title') == "view" ? 1 : 2, "RoleID": $(this).closest('tr').prop('id'), "Username": "", "AllowAccess": true };
                    }
                });
                var beforeID = 0;
                var afterID = 0;
                if ($('#rdbBefore').prop('checked') == true) {
                    beforeID = $('#cboPositionTab').val();
                }
                else if ($('#rdbAfter').prop('checked') == true) {
                    afterID = $('#cboPositionTab').val();
                }
                var iconFile = '';
                $('div.cssClassUploadFiles > ul > li > span.iconFile').each(function () {
                    iconFile += $(this).html() + ', ';
                });
                if (iconFile != '')
                    iconFile = iconFile.substring(0, iconFile.length - 2);
                var MenuArr = new Array();
                var MenuSelected = 0;
                if ($('#chkMenu').is(':checked')) {
                    var MenuList = $('#selMenulist option:selected');
                    $.each(MenuList, function (index, item) {
                        MenuArr.push($(this).val());
                    });
                    MenuSelected = MenuArr.join(',');
                }
                var _IsVisible = $('#rdbAdmin').prop('checked') ? $('#chkShowInDashboard').prop("checked") : true;
                var PageDetails = {
                    PageEntity: {
                        Mode: Mode,
                        Caption: $('#txtCaption').val(),
                        PageID: pageID,
                        PageName: $.trim($('#txtPageName').val()),
                        IsVisible: _IsVisible, ParentID: $('#cboParentPage').val(),
                        IconFile: $('div.cssClassUploadFiles img:eq(0)').prop('title'),
                        Title: $('#txtTitle').val(),
                        Description: $('#txtDescription').val(),
                        KeyWords: $('#txtKeyWords').val(),
                        Url: "", StartDate: $('#txtStartDate').val(),
                        EndDate: $('#txtEndDate').val(),
                        RefreshInterval: $('#txtRefreshInterval').val() == "" ? 0 : $('#txtRefreshInterval').val(),
                        PageHeadText: "SageFrame", IsSecure: $('#chkIsSecure').prop("checked") ? true : false,
                        PortalID: p.PortalID,
                        IsActive: true,
                        AddedBy: p.UserName, BeforeID: beforeID, AfterID: afterID,
                        IsAdmin: $('#rdbAdmin').prop('checked') ? true : false,
                        LstPagePermission: SagePages.config.lstPagePermission,
                        MenuList: MenuSelected,
                        UpdateLabel: UpdateLabel
                    }
                };
                if (p.StartupPage == SagePages.config.OldPage) {
                    var NewPageName = String($('#txtPageName').val());
                    SagePages.UpdSettingKeyValue(p.ActiveTemplateName, NewPageName, SagePages.config.OldPage);
                    //UPDATE THE PREVIEW LINK AS WELL
                    $('#hypPreview').prop("href", PagePath + "/" + NewPageName.replace(' ', '-') + Extension);
                    p.StartupPage = NewPageName;
                }
                this.config.method = "AddUpdatePages";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    "objPageInfo": PageDetails.PageEntity,
                    Culture: p.CultureCode,
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            },
            GetPageDetails: function (pageID) {
                this.config.method = "GetPageDetails";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    pageID: pageID,
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            },
            BindPortalRoles: function (data) {
                var html = '';
                if (data.d.length > 0)
                    html += '<tr><th><label>Role</label></th><th><label>View</label></th><th><label>Edit</label></th><th>&nbsp;</th></tr>';
                $.each(data.d, function (index, item) {
                    var accesscontrolled = item.RoleName.toLowerCase() === "superuser" || item.RoleName.toLowerCase() === "super user" ? 'checked="checked" disabled="true" class="sfCheckbox sfChecked"' : 'class="sfCheckbox"';
                    var style = index % 2 == 0 ? 'class="sfEven"' : 'class="sfOdd"';
                    html += '<tr ' + style + ' id=' + item.RoleID + '><td width="40%"><label>' + item.RoleName + '</label></td><td width="20%"><input type="checkbox" ' + accesscontrolled + ' title="view" /></td>';
                    html += '<td width="20%"><input type="checkbox" ' + accesscontrolled + ' title="edit" /></td><td width="10%">&nbsp;</td></tr>';
                });
                $('div.divPermission table').append(html);
                //$('div.divPermissions table').find('.sfChecked').prop("checked", "checked");
                SagePages.InitCustomControls();
            },
            //SearchUsers: function () {
            //    this.config.method = "SearchUsers";
            //    this.config.url = this.config.baseURL + this.config.method;
            //    this.config.data = JSON2.stringify({
            //        SearchText: $('#txtSearchUsers').val(),
            //        portalID: parseInt(SageFramePortalID),
            //        userName: SageFrameUserName,
            //        userModuleID: p.UserModuleID,
            //        secureToken: SageFrameSecureToken
            //    });
            //    this.config.ajaxCallMode = 1;
            //    this.ajaxCall(this.config);
            //},
            BindUsers: function (data) {
                var selectedUsers = $('#tblUser tr');
                if (data.d.length > 0) {
                    $('#divSearchedUsers').show();
                }
                else {
                    $('#btnAddUser,#btnCancelUser').hide();
                }
                $.each(selectedUsers, function () {
                    SagePages.config.arrUsers.push($(this).find("td:first label").text());
                });
                var html = '<ul>';
                $.each(data.d, function (index, item) {
                    var style = jQuery.inArray(item.UserName.toLowerCase(), SagePages.config.arrUsers) > -1 ? 'class="sfActive"' : "";
                    html += '<li ' + style + ' id=' + item.UserID + '>' + item.UserName + '</li>';
                });
                html += '</ul>';
                $('#divSearchedUsers').html(html);
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
            GetChildPages: function (parentID, isActive, isVisiable, isRequiredPage, userName, portalID) {
                this.config.method = "GetChildPages";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    parentID: parentID,
                    isActive: isActive,
                    isVisiable: isVisiable,
                    isRequiredPage: isRequiredPage,
                    userName: userName,
                    portalID: portalID,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);
            },
            BindChildPages: function (data) {
                var html = '';
                $.each(data.d, function (index, item) {
                    html += '<option value=' + item.PageID + '>' + item.PageName + '</option>';
                });
                $('#cboPositionTab').html(html);
            },
            InitCustomControls: function () {
                $("input:submit,input:button").button();
                $("#divAddUsers").dialog({
                    autoOpen: false,
                    width: 350,
                    modal: true
                });
            },
            IconUploader: function () {
                var uploadFlag = false;
                var upload = new AjaxUpload($('#flIcon'), {
                    action: Path + 'UploadHandler.ashx?userModuleId=' + p.UserModuleID + '&portalID=' + SageFramePortalID + '&userName=' + SageFrameUserName + '&secureToken=' + SageFrameSecureToken,
                    name: 'myfile[]',
                    multiple: false,
                    data: { rdbChecked: "NA" },
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
                            return ConfirmDialog(this, 'message', "Not a valid image file");
                            return false;
                        }
                    },
                    onComplete: function (file, response) {
                        if (response == "LargeImagePixel") {
                            return ConfirmDialog(this, 'message', "The image size is too large in pixel");
                        }
                        if (response == "large") {
                            return ConfirmDialog(this, 'message', "The image size is too large. Should be less than 1mb");
                        }
                        var pageimage = file.split(" ").join("_");
                        var filePath = p.HostURL + "/PageImages/" + pageimage;
                        $('span.filename').text(pageimage);
                        var html = '<img title="' + pageimage + '" src="' + filePath + '" /><span class="deleteIcon"><label class="sfBtn icon-close"></label></span>';
                        $('div.cssClassUploadFiles').html(html);
                    }
                });
            },
            RefreshPage: function (data) {
                var returnValue = data.d;
                if (returnValue == "2") {
                    SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "PageManager", "PageSavedSuccessful"), "Success");
                }
                else {
                    SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "PageManager", "PageUpdatedSuccessful"), "Success");
                }
                if ($('#rdbFronMenu').prop('checked') == true) {
                    SagePages.config.lstNormalPages = [];
                }
                else if ($('#rdbAdmin').prop('checked') == true) {
                    SagePages.config.lstAdminPages = [];
                }
                if (returnValue == "2" || returnValue == "3") {
                    $('#categoryTree').html('');
                    $(this).SageTreeBuilder({
                        PortalID: p.PortalID,
                        UserModuleID: p.UserModuleID,
                        UserName: p.UserName,
                        PageName: p.PageName,
                        ContainerClientID: p.ContainerClientID,
                        CultureCode: p.CultureCode,
                        baseURL: p.baseURL,
                        Mode: $('#rdbAdmin').prop('checked')
                    });
                    SagePages.ClearControls();
                }

            },
            ClearControls: function () {
                if ($('#rdbAdmin').prop('checked')) {
                    $('#trShowInDashboard').show();
                }
                $('#txtPageName').val('').removeAttr("disabled").removeClass("sfDisable");
                $('#cboParentPage').val('');
                $('#flIcon').val('');
                $('#txtTitle').val('');
                $('#txtDescription').val('');
                $('#txtCaption').val('');
                $('#txtKeyWords').val('');
                $('#txtStartDate').val('');
                $('#txtEndDate').val('');
                $('#txtRefreshInterval').val('');
                $('#chkIsSecure').prop("checked", false);
                LstPagePermission: lstPagePermission = [];
                $('#txtPageName').prop('title', 0);
                $('#cboPositionTab').val('');
                $('span.ui-tree-selected').removeClass("ui-tree-selected")
                $('#tblUser tr').remove();
                $("div.divPermission tr:gt(1)").each(function () {
                    $(this).find("input:checkbox").prop("checked", false);
                });
                $('div.cssClassUploadFiles').html('');
                $('#chkMenu').prop("checked", false);
                $('#selMenulist').html('').hide();
                $('#trIncludeInMenu').hide();
                $('span.filename').text('No files selected');
            },
            GetPageModules: function (_pageID, _portalID) {
                this.config.method = "GetPageModules";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    pageID: parseInt(_pageID),
                    portalID: parseInt(_portalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 5;
                this.ajaxCall(this.config);
            },
            BindPageModules: function (data) {
                var html = '<table width="100%" cellpadding="0" cellspacing="0">';
                var modules = data.d;
                if (modules.length > 0) {
                    html += '<tr><th>UserModule</th><th>Postion</th><th></th></tr>';
                }
                var link = p.AppName + '/Admin/ControlEditor.aspx?uid=';
                $.each(modules, function (index, item) {
                    if (item != null) {
                        var hreflink = link + item.UserModuleID;
                        if (index % 2 == 0) {
                            html += '<tr class="sfEven"><td>' + item.UserModuleTitle + '</td><td>' + item.PaneName + '</td></tr>';
                        }
                        else {
                            html += '<tr class="sfOdd"><td>' + item.UserModuleTitle + '</td><td>' + item.PaneName + '</td></tr>';
                        }
                    }
                });
                html += '</table>';
                $('#hdnModules').html(html);

                if ($('#hdnModules table tr').length == 0) {
                    $('#hdnModules').html(SageFrame.messaging.showdivmessage(SageFrame.messages.ModulesAssigned));
                }
                SagePages.InitPaging();
            },
            DeletePageModules: function (tblID, argus) {
                switch (tblID) {
                    case "gdvModule":
                        if (argus[3]) {
                            var properties = { onComplete: function (e) { SagePages.PageModuleDelete(argus[0], deletedBy, portalId, e); } }
                            csscody.confirm("<h2>Delete Confirmation</h2><p>Do you want to delete this module?</p>", properties);
                        }
                        break;
                    default:
                        break;
                }
            },
            PageModuleDelete: function (moduleID, deletedBy, portalID, event) {
                if (event) {
                    $.ajax({
                        type: "POST",
                        url: servicePath + "DeletePageModule",
                        data: JSON2.stringify({ moduleID: moduleID, deletedBy: dele4edBy, portalID: portalId }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function () {
                        }
                    });
                }
                return false;
            },
            InitPaging: function () {
                var optInit = {
                    items_per_page: 15,
                    num_display_entries: 10,
                    current_page: 0,
                    num_edge_entries: 2,
                    link_to: "#",
                    prev_text: "<<",
                    next_text: ">>",
                    ellipse_text: "...",
                    prev_show_always: true,
                    next_show_always: true,
                    callback: SagePages.pagingCallback
                };
                var members = $('#hdnModules tr');
                $("#divPager").pagination(members.length, optInit);
            },
            pagingCallback: function (page_index, jq) {
                // Get number of elements per pagionation page from form
                var items_per_page = 15;
                var members = $('#hdnModules tr');
                var max_elem = Math.min((page_index + 1) * items_per_page, members.length);
                var newcontent = '<table cellspacing="0" cellpadding="0" width="100%"><tr><th>UserModule</th><th>Postion</th><th></th></tr>';
                var modules = $('#hdnModules tr');
                var i = 0;
                $.each(modules, function () {
                    if (i < max_elem && i >= page_index * items_per_page) {
                        var style = i % 2 == 0 ? 'class="sfEven"' : 'class="sfOdd"';
                        newcontent += '<tr ' + style + '><td>' + $(this).find("td:first").text() + '</td><td>' + $(this).find("td:eq(1)").text() + '</td></tr>';
                    }
                    i++;
                });
                newcontent += '</table>';
                $('#gdvModules').empty().html(newcontent);
                // Prevent click eventpropagation
                return false;
            },
            PublishPage: function () {
                if ($('#categoryTree').find('li.active').hasClass('required')) {
                    $('#sf_lblConfirmation').text("Unable to  UnPublish  a Portal Start Up Page.");
                    $("#dialog").dialog({
                        modal: true,
                        buttons: {
                            "Cancel": function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                }
                else {
                    var pageId;
                    if ($('#categoryTree').find('li.active').length > 0) {
                        pageId = $('#categoryTree').find('li.active').find('span.ui-tree-selected').find('span.true').parents('li').attr("id");
                    } else {
                        pageId = $('#categoryTree').find('li:eq(0)').find('span.ui-tree-selected').find('span.true').parents('li').attr("id");
                    }
                    var isPublish = $('#btnpublish').attr("name");
                    this.config.method = "PublishPage";
                    this.config.url = this.config.baseURL + this.config.method;
                    this.config.data = JSON2.stringify({
                        pageId: pageId,
                        isPublish: isPublish,
                        portalID: parseInt(SageFramePortalID),
                        userName: SageFrameUserName,
                        userModuleID: p.UserModuleID,
                        secureToken: SageFrameSecureToken
                    });
                    this.config.ajaxCallMode = 7;
                    this.ajaxCall(this.config);
                }
            },
            PreviewPage: function () {
                var previewPage;
                var rand = "none";
                if ($('#categoryTree').find("li.active").find('.ui-tree-selected').find('span.true').length > 0) {
                    previewPage = $('#categoryTree').find("li.active").find('.ui-tree-selected').find('span.true').attr('pagename');
                    rand = $('#categoryTree').find("li.active").find('.ui-tree-selected').find('span.true').attr('preview');
                }
                else {
                    previewPage = $('#categoryTree').find("li:eq(0)").find('span.true').attr('pagename');
                    rand = $('#categoryTree').find("li:eq(0)").find('span.true').attr('preview');
                }
                var url = ServicePath + "/" + previewPage + p.PageExtension + "?preview=" + rand;
                window.open(url);
            }
        };
        SagePages.init();
    }
    $.fn.SageFramePageBuilder = function (p) {
        $.createPageBuilder(p);
    };
})(jQuery);