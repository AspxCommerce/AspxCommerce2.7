(function ($) {
    $.createPageRoleSettings = function (p) {
        p = $.extend
               ({
                   CultureCode: '',
                   UserModuleID: '1',
                   Mode: false,
                   RoleID: ''
               }, p);
        var $ajaxCall = function (param, url, successfx, errorfx) {
            $.ajax({
                type: PageRoleSettings.config.type,
                contentType: PageRoleSettings.config.contentType,
                cache: PageRoleSettings.config.cache,
                url: url,
                data: param,
                dataType: PageRoleSettings.config.dataType,
                success: successfx,
                error: errorfx
            });
        };
        var PageRoleSettings = {
            config: {
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
                lstPagePermission: [],
                arr: [],
                arrModules: [],
                baseURL: SageFrameAppPath + '/Modules/Admin/UserManagement/Services/PageRoleSettingsWebService.asmx/',
                Path: SageFrameAppPath + '/Modules/Admin/UserManagement/',
                PortalID: SageFramePortalID,
                UserName: SageFrameUserName,
                UserModuleID: p.UserModuleID,
                Mode: p.Mode,
                RoleID: ''
            },
            ajaxSuccess: function (data) {
                switch (PageRoleSettings.config.ajaxCallMode) {
                    case 1:
                        PageRoleSettings.BindRolesList(data)
                        break;
                    case 2:
                        PageRoleSettings.BindPageWithRoles(data)
                        break;
                    case 3:
                        PageRoleSettings.ShowMessage(data)
                        break;
                }
            },
            ajaxFailure: function () {
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: PageRoleSettings.config.type,
                    contentType: PageRoleSettings.config.contentType,
                    cache: PageRoleSettings.config.cache,
                    url: PageRoleSettings.config.url,
                    data: PageRoleSettings.config.data,
                    dataType: PageRoleSettings.config.dataType,
                    success: PageRoleSettings.ajaxSuccess,
                    error: PageRoleSettings.ajaxFailure
                });
            },
            GetRolesList: function () {
                $ajaxCall(JSON2.stringify({
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                }),
                this.config.baseURL + "GetPortalRoles", PageRoleSettings.BindRolesList, function () { });
            },
            GetPageWithRolesDetails: function (RoleID) {
                var PortalID = -1
                if ($('#rdbPortalPages').prop("checked")) {
                    PortalID = parseInt(SageFramePortalID);
                }
                $ajaxCall(JSON2.stringify({
                    RoleID: RoleID,
                    PortalID: parseInt(PortalID),
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                }), this.config.baseURL + "GetPageDetailsByRoleID", PageRoleSettings.BindPageWithRoles, function () { });
            },
            AddUpdatePage: function () {
                var isAdmin;
                if (parseInt(SageFramePortalID) == -1)
                    isAdmin = true;
                else
                    isAdmin = false;
                $ajaxCall(JSON2.stringify({
                    lstPagePermission: PageRoleSettings.config.lstPagePermission,
                    portalID: parseInt(SageFramePortalID),
                    addedBy: SageFrameUserName,
                    isAdmin: isAdmin,
                    userName: SageFrameUserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                }), this.config.baseURL + "AddUpdatePageRolePermission", PageRoleSettings.ShowMessage, function () { });
            },
            BindSuperRole: function (index) {
                $("ul#roleTree li").removeClass('sfActive');
                $("ul#roleTree").find("li:eq(0)").addClass('sfActive');
                var RoleID = p.RoleID;
                PageRoleSettings.GetPageWithRolesDetails(RoleID);
            },
            BindPageWithRoles: function (data) {
                $('div.divPermission table').html('');
                var html = '';
                var roleNameCheck;
                if (data.d.length > 0)
                    html += '<tr><th><label>Page</label></th><th><label>View</label></th><th><label>Edit</label></th><th>&nbsp;</th></tr>';
                $('div.divPermission table').append(html);
                $.each(data.d, function (index, item) {
                    var html1 = '';
                    roleNameCheck = item.RoleName;
                    var accesscontrolled = item.RoleName.toLowerCase() === "superuser" || item.RoleName.toLowerCase() === "super user" ? 'checked="checked" disabled="true" class="sfCheckbox sfChecked"' : 'class="sfCheckbox"';
                    html1 += '<tr id=Page_' + item.PageID + '><td width="40%"><label>' + item.PageName + '</label></td>';
                    var PermissionID = item.PermissionID;
                    if (PermissionID != null) {
                        var PermissionArr = PermissionID.split(',');
                        if (parseInt(PermissionArr[0]) == 1) {
                            html1 += '<td width="20%"><input class="sfCheckbox" checked="checked" type="checkbox" ' + accesscontrolled + ' title="view" /></td>';
                        }
                        else {
                            html1 += '<td width="20%"><input class="sfCheckbox" type="checkbox" ' + accesscontrolled + ' title="view" /></td>';
                        }
                        if (parseInt(PermissionArr[1]) == 2) {
                            html1 += '<td width="20%"><input class="sfCheckbox" checked="checked" type="checkbox" ' + accesscontrolled + '  title="edit" /></td>';
                        }
                        else {
                            html1 += '<td width="20%"><input class="sfCheckbox" type="checkbox" ' + accesscontrolled + '  title="edit" /></td>';
                        }
                    }
                    if (item.RoleName.toLowerCase() === "superuser" || item.RoleName.toLowerCase() === "super user") {
                        html1 += '<td width="20%">&nbsp;</td>';
                        html1 += '</tr>';
                    }
                    else {
                        html1 += '<td width="20%"><input type="button" class="btnPermissionSave sfBtn sfSingleSaveBtn" id=PageBtn_' + item.PageID + ' class="icon-save" value="save" /></td>';
                        html1 += '</tr>';
                    }
                    $('div.divPermission table').append(html1);
                });
                if (roleNameCheck.toLowerCase() === "superuser" || roleNameCheck.toLowerCase() === "super user") {
                    $("#btnSubmit").hide();
                }
                else {
                    $("#btnSubmit").show();
                }
            },
            BindRolesList: function (data) {
                var html = "";
                html += '<h3>Roles</h3>';
                html += '<div class="clear"/>';
                html += '<ul id="roleTree">';
                $.each(data.d, function (index, item) {
                    html += '<li class="sfRoleName" id=' + item.RoleID + '>' + item.RoleName + '</li>';
                });
                html += '</ul>';
                html += '</div>';
                html += '</div>';
                $('.sfLeftdivB').html(html);
                $("ul#roleTree").find("li:eq(0)").addClass('sfActive');
            },
            ShowMessage: function (data) {
                SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "PageManager", "PermissionUpdatedSuccessful"), "Success");
            },
            Init: function () {
                PageRoleSettings.BindEvent();
                PageRoleSettings.GetRolesList();
                PageRoleSettings.BindSuperRole();
            },
            BindEvent: function () {
                $('#divBindRolesList').on('click', '.sfRoleName', function () {
                    $("ul#roleTree li").removeClass('sfActive');
                    $(this).addClass('sfActive');
                    RoleID = $(this).attr('id');
                    PageRoleSettings.GetPageWithRolesDetails(RoleID);
                });
                $('input[type=radio][name=PageMode]').on('change', function () {
                    $("#dvRoleType").find("label").removeClass('sfActive');
                    $(this).parents("label").addClass('sfActive');
                    RoleID = $("ul#roleTree").find(".sfActive").prop("id");
                    PageRoleSettings.GetPageWithRolesDetails(RoleID);
                });
                $('#tblPermission').on('click', '.btnPermissionSave', function () {
                    PageRoleSettings.config.lstPagePermission = [];
                    $viewPermission = $(this).parent().parent().find('input[type=checkbox][title=view]');
                    $editPermission = $(this).parent().parent().find('input[type=checkbox][title=edit]');
                    var permissionView = "0";
                    $viewPermission.is(":checked") ? permissionView = "1" : permissionView = "0";
                    var lstPermissionView = { "PageID": $(this).attr('id').replace('PageBtn_', ''), "PermissionID": permissionView, "RoleID": $("ul#roleTree").find(".sfActive").prop("id"), "Username": "", "AllowAccess": true };
                    PageRoleSettings.config.lstPagePermission.push(lstPermissionView);

                    var permissionEdit = "0";
                    $editPermission.is(":checked") ? permissionEdit = "2" : permissionEdit = "0";
                    var lstPermissionEdit = { "PageID": $(this).attr('id').replace('PageBtn_', ''), "PermissionID": permissionEdit, "RoleID": $("ul#roleTree").find(".sfActive").prop("id"), "Username": "", "AllowAccess": true };
                    PageRoleSettings.config.lstPagePermission.push(lstPermissionEdit);
                    PageRoleSettings.AddUpdatePage();
                });
                $('#btnSubmit').bind("click", function () {
                    var items = $('#tblPermission tr:gt(0)')
                    $.each(items, function (index, item) {
                        $viewPermission = $(this).find('td:eq(1) input');
                        $editPermission = $(this).find('td:eq(2) input');
                        var permissionView = "0";
                        $viewPermission.is(":checked") ? permissionView = "1" : permissionView = "0";
                        var lstPermissionView = { "PageID": $(this).attr('id').replace('Page_', ''), "PermissionID": permissionView, "RoleID": $("ul#roleTree").find(".sfActive").prop("id"), "Username": "", "AllowAccess": true };
                        PageRoleSettings.config.lstPagePermission.push(lstPermissionView);

                        var permissionEdit = "0";
                        $editPermission.is(":checked") ? permissionEdit = "2" : permissionEdit = "0";
                        var lstPermissionEdit = { "PageID": $(this).attr('id').replace('Page_', ''), "PermissionID": permissionEdit, "RoleID": $("ul#roleTree").find(".sfActive").prop("id"), "Username": "", "AllowAccess": true };
                        PageRoleSettings.config.lstPagePermission.push(lstPermissionEdit);
                    });
                    PageRoleSettings.AddUpdatePage();
                });
                $('#imbPageCancel').bind("click", function () {
                    window.location.href = window.location.href;
                });
            }
        };
        PageRoleSettings.Init();
    };
    $.fn.PageRoleSettings = function (p) {
        $.createPageRoleSettings(p);
    };

})(jQuery);