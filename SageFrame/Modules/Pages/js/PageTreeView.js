(function ($) {
    $.createPageTreeView = function (p) {
        p = $.extend
        ({
            PortalID: 1,
            UserModuleID: 1,
            UserName: 'user',
            PageName: 'Home',
            ContainerClientID: 'divNav1',
            CultureCode: 'en-US',
            baseURL: Path + '/Modules/PageTreeView/MenuWebService.asmx/',
            Mode: false,
            AppName: '',
            HostURL: "",
            StartupPage: '',
            ActiveTemplateName: 'Default'
        }, p);
        var PageTreeView = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=u0tf-8",
                data: { data: '' },
                dataType: 'json',
                baseURL: p.baseURL,
                baseUrlForPages: p.baseUrlForPages,
                method: "",
                url: "",
                ajaxCallMode: 0,
                UserModuleID: p.UserModuleID,
                PortalID: p.PortalID,
                UserName: p.UserName,
                PageName: p.PageName,
                ContainerClientID: p.ContainerClientID,
                CultureCode: p.CultureCode,
                Mode: p.Mode,
                LSTPages: []
            },
            init: function () {
                this.GetPages();
                this.BindEvents();
            },
            ajaxSuccess: function (data) {
                switch (PageTreeView.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        PageTreeView.BuildMenu(data);
                        break;
                    case 2:
                        PageTreeView.BindPages(data);
                        break;
                    case 3:
                        PageTreeView.BuildFooterMenu(data);
                        break;
                    case 4:
                        PageTreeView.BuildSideMenu(data);
                        break;
                    case 5:
                        SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "PageManager", "PageDeletedSuccessful"), "Success");
                        PageTreeView.GetPages();
                        break;
                    case 6:
                        $("#categoryTree li").eq(0).siblings().removeClass("sfChild");
                        PageTreeView.AddContextMenu();
                        break;
                    case 7:
                        PageTreeView.BindPageDetails(data);
                        break;
                }
            },
            BindEvents: function () {
                $('#imbPageCancel').bind("click", function () {
                    PageTreeView.ClearControls();
                    PageTreeView.PageShow(2);
                    $('#btnAddpage').removeClass('sfAddActive');
                    $('#categoryTree li.active').removeClass('sfTempDeactive');
                });
                $('#trShowInDashboard').hide();
                if ($('#rdbFronMenu').attr("checked")) {
                    $('#trShowInDashboard').hide();
                }
                $('#btnAddpage').bind("click", function () {
                    PageTreeView.PageShow(1);
                    PageTreeView.ClearControls();
                    //   PageTreeView.GetPages();
                    $("#flIcon").next('.filename').html('No file selected.');
                    $('#txtPageName').next("label").remove();
                    $('#txtTitle').next("ladzbel").remove();
                    $('#txtPageName').removeAttr("readonly");
                    $(this).addClass('sfAddActive');
                    $('#categoryTree li.active').addClass('sfTempDeactive');
                });
                $('#rdbAdmin, #rdbFronMenu').bind('click', function () {
                    PageTreeView.ClearControls();
                    PageTreeView.config.Mode = $(this).attr('id') === 'rdbAdmin' ? true : false;
                    PageMode = PageTreeView.config.Mode;
                    PageTreeView.GetPages();
                    if ($(this).attr("id") == "rdbAdmin") {
                        $('#trIncludeInMenuLbl').hide();
                        $('#trShowInDashboard').show();
                        $('#trIncludeInMenu').hide();
                        //$('#trParent').hide();
                        PageTreeView.IconUploaderAdmin();
                        $('#divIncludeModules').show();
                        $('#rdbAdminModules').trigger('click');
                        $('.publishbar').hide();
                        $('#PageHeader').hide();
                    }
                    else {
                        $('#rdbGenralModules').trigger('click');
                        $('.publishbar').show();
                        $('#divIncludeModules').hide();
                        $('#trIncludeInMenuLbl').show();
                        $('#trShowInDashboard').hide();
                        $('#lblSelectMenu').hide();
                        $('#trIncludeInMenu').show();
                        $('#trParent').show();
                        PageTreeView.IconUploader();
                        $('#PageHeader').show();
                    }

                });
                $('#navContainer').on('click', '.editPage', function () {
                    PageTreeView.PageShow(1);
                    PageTreeView.ClearControls();
                    var id = $(this).attr('id').replace('imgEditNew_', '');
                    PageTreeView.GetPageDetails(id);
                });
                $('#navContainer').on('click', '.deletePage', function () {
                    var $me = $(this);
                    var id = $me.attr('id').replace('imgDelete_', '');
                    var pageName = $me.parents('li').find('.PageName').text();

                    if (pageName == "Under Construction") {
                        $('#sf_lblConfirmation').text("Under Construction page is not deleted.");
                        $("#dialog").dialog({
                            modal: true,
                            buttons: {
                                "Cancel": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });
                    }
                    else if ($me.parents('li').find('li.required').length > 0 || $me.parent("li").hasClass("required")) {
                        $('#sf_lblConfirmation').text("Unable to  delete.Before delete change startup page.");
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
                        $('#sf_lblConfirmation').text("Are you sure you want to delete this page?");
                        $("#dialog").dialog({
                            modal: true,
                            buttons: {
                                "Confirm": function () {
                                    PageTreeView.DeletePage(id, p.UserName, p.PortalID);
                                    $me.parents('li').remove();
                                    $(this).dialog("close");
                                    PageTreeView.ClearControls();
                                },
                                "Cancel": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });
                    }
                });

                $('#portalPages').on('click', function () {
                    $('#rdbFronMenu').trigger('click');
                    // $('#chkIsSecure').prop('checked', true)
                    $('#adminPages').removeClass('sfActive');
                    $(this).addClass('sfActive');
                });

                $('#adminPages').on('click', function () {
                    $('#imgPCSwitch').removeClass('active');
                    $('#imgMobileSwitch').addClass('active');
                    $('#rdbAdmin').trigger('click');
                    $('#portalPages').removeClass('sfActive');
                    $(this).addClass('sfActive');
                });

                $('#chkMenu').bind("click", function () {
                    if ($(this).prop("checked")) {
                        var selected = $('#categoryTree  span.ui-tree-selected').length;
                        $('#trIncludeInMenu').slideDown();
                        $('#lblSelectMenu').show();
                        $('#selMenulist').show();
                        PageTreeView.GetMenuList();
                    }
                    else {
                        $('#trIncludeInMenu').slideUp();
                        $('#lblSelectMenu').hide();
                    }
                });
                $('#navContainer').on('click', '.sfSearchPages', function () {
                    $('#txtSearch').width('172');
                    $('#txtSearch').focus();
                });
                $('#navContainer').on('click', '#txtSearch', function () {
                    $(this).width('172');
                });
                $('#navContainer').on('blur', '#txtSearch', function () {
                    $(this).width('20');
                    $(this).val('');
                    $('#categoryTree li').removeClass('sfHighlight').find("ul").css("display", "none");
                });
            },
            ajaxFailure: function () {
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: PageTreeView.config.type,
                    contentType: PageTreeView.config.contentType,
                    cache: PageTreeView.config.cache,
                    url: PageTreeView.config.url,
                    data: PageTreeView.config.data,
                    dataType: PageTreeView.config.dataType,
                    success: PageTreeView.ajaxSuccess,
                    error: PageTreeView.ajaxFailure
                });
            },
            GetPages: function () {
                this.config.method = "GetPortalPages";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    IsAdmin: PageTreeView.config.Mode,
                    userName: p.UserName,
                    portalID: parseInt(PageTreeView.config.PortalID),
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            },
            DeletePage: function (pageID, deletedBy, portalId) {
                this.config.method = "DeleteChildPages";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    pageID: pageID, deletedBY: deletedBy,
                    portalID: $('#rdbAdmin').prop("checked") ? -1 : portalId,
                    userName: p.UserName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 5;
                this.ajaxCall(this.config);
            },
            UpdatePage: function (pageID, isVisiable, isPublished, portalID, deletedBY, updateFor) {
                this.config.method = "UpdatePageAsContextMenu";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    pageID: pageID, isVisiable: isVisiable,
                    isPublished: isPublished,
                    portalID: portalID,
                    userName: deletedBY,
                    updateFor: updateFor,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 6;
                this.ajaxCall(this.config);
            },
            SortTreeViewMenu: function (pageID, parentID, pageName, BeforeID, AfterID, portalID, userName) {
                if ($('#rdbAdmin').prop("checked")) {
                    parentID = 2;
                }
                this.config.method = "SortFrontEndMenu";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    pageID: pageID,
                    parentID: parentID,
                    pageName: pageName,
                    BeforeID: BeforeID,
                    AfterID: AfterID,
                    portalID: portalID,
                    userName: userName,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 6;
                this.ajaxCall(this.config);
            },
            SortAdminTreeViewMenu: function () {
                var lstPageOrder = [];
                var html = '';
                var count = 0;
                $('#categoryTree li').each(function (ind, itm) {
                    if ($(this).prop('id').trim().length > 0) {
                        lstPageOrder[count] = { "PageID": parseInt($(this).prop('id')), "PageOrder": parseInt(ind), "PortalID": parseInt(p.PortalID) };
                        count++;
                    }
                });
                lstPageOrder.pop();
                this.config.method = "SortAdminPages";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    "lstPages": lstPageOrder,
                    userName: p.UserName,
                    portalID: parseInt(p.PortalID),
                    userModuleID: parseInt(p.UserModuleID),
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 6;
                this.ajaxCall(this.config);
            },
            BuildMenu: function (data) {
                var setting = data.d;
                switch (parseInt(setting.MenuType)) {
                    case 0:
                        PageTreeView.LoadTopAdminMenu();
                        break;
                    case 1:
                        PageTreeView.GetPages();
                        break;
                    case 2:
                        PageTreeView.LoadSideMenu();
                        break;
                    case 3:
                        PageTreeView.LoadFooterMenu();
                        break;
                }
            },
            BindPages: function (data) {
                var pages = data.d;
                var PageID = "";
                var parentID = "";
                var PageLevel = 0;
                var itemPath = "";
                var html = "";
                html += '<h3>Pages';
                html += '<div class="sfSearch">';
                html += '<input type="text" id="txtSearch" class="sfInputbox" style="width: 20px;"/><span class="icon-search sfSearchPages"></span></div></h3>';
                html += '<div class="clear"/>';
                html += '<ul id="categoryTree">';
                var levelSelect = PageTreeView.config.Mode ? 1 : 0;
                $.each(pages, function (index, item) {
                    PageID = item.PageID;
                    parentID = item.ParentID;
                    categoryLevel = item.Level;
                    if (item.Level == levelSelect) {
                        /* if (item.ChildCount > 0) {
                        html += '<li ' + styles + '  id=' + PageID + '><span title=' + item.IsPublished + ' ispublish=' + item.IsVisible + ' class="PageName" preview=' + item.PreviewCode + '></span>';
                        html += '<span><img title="EditPage" alt="editPage" id="ctl18_imgEditNew" src="../Administrator/Templates/Default/images/context-add-page.png" style="vertical-align: middle; padding-right: 2px;">';
                        html += '<img title="Remove" alt="Remove" id="ctl18_imgRemove" src="../Administrator/Templates/Default/images/context-delete.png" style="vertical-align: middle; padding-right: 2px;"></span>';
                        }
                        else {
                        var styles = item.PageName.toLowerCase() === p.StartupPage.toLowerCase() ? 'class="file-folder required"' : 'class="file-folder"';
                        html += '<li ' + styles + ' id=' + PageID + '> <span title=' + item.IsPublished + ' ispublish=' + item.IsVisible + ' class="PageName" preview=' + item.PreviewCode + '>' + item.PageName.replace(new RegExp("-", "g"), ' ') + '</span>';
                        html += '<span><img class="editPage" title="EditPage" alt="editPage" id="imgEditNew_' + PageID + '" src="../Administrator/Templates/Default/images/context-add-page.png" style="vertical-align: middle; padding-right: 2px;">';
                        html += '<img class="deletePage" title="Remove" alt="Remove" id="imgDelete_' + PageID + '" src="../Administrator/Templates/Default/images/context-delete.png" style="vertical-align: middle; padding-right: 2px;"></span>';
                        } */
                        var pageName = item.PageName; //.replace(new RegExp("-", "g"));
                        if (item.ChildCount > 0) {
                            var styles = item.PageName.toLowerCase() === p.StartupPage.toLowerCase() ? 'class="file-folder required"' : 'class="file-folder"';
                            html += '<li ' + styles + ' id=' + PageID + '> <span name="sfPageName" isPublish=' + item.IsVisible + ' preview=' + item.PreviewCode + ' title=' + item.IsActive + ' class="true" pagename=' + pageName + '></span>';
                        }
                        else {
                            var styles = item.PageName.toLowerCase() === p.StartupPage.toLowerCase() ? 'class="file-folder required"' : 'class="file-folder"';
                            html += '<li ' + styles + ' id=' + PageID + '> <span name="sfPageName" isPublish=' + item.IsVisible + ' preview=' + item.PreviewCode + ' title=' + item.IsActive + ' class="true" pagename=' + pageName + '></span>';
                        }
                        html += pageName.replace(new RegExp("-", "g"), ' ');
                        if (item.ChildCount > 0) {
                            html += "<ul>";
                            itemPath += item.PageName;
                            html += PageTreeView.BindChildCategory(pages, PageID);
                            html += "</ul>";
                        }
                        html += '</li>';
                    }
                    itemPath = '';
                });
                html += '</ul>';
                $(PageTreeView.config.ContainerClientID).html(html);
                PageTreeView.AddDragDrop();
                PageTreeView.AddContextMenu();
                PageTreeView.BindParentPages(data);
                LSTSagePages = data.d;
                $('#navContainer').keyup('#txtSearch', function (event) {
                    //if (event.keyCode == 13) {
                    if ($('#txtSearch').val() != "") {
                        $('#categoryTree li').removeClass('sfHighlight').find("ul").css("display", "none");
                        var categoryTree = $('#categoryTree span');
                        $.each(categoryTree, function (index, item) {
                            if ($(this).text().search(new RegExp($('#txtSearch').val(), "i")) > -1) {
                                $(this).parent('li').addClass('sfHighlight');
                                $(this).parents('ul').css('display', 'block');
                            }
                        });
                    }
                    else {
                        $('#categoryTree li').removeClass('sfHighlight').find("ul").css("display", "none");
                    }
                    //}
                });
                $("#categoryTree").find("li:eq(0)").addClass('active');
                $("#categoryTree").find("li:eq(0)").find('span.ui-tree-title').eq(0).addClass('ui-tree-selected');
                ////// $("#categoryTree").find("li:eq(0)").find('span.ui-tree-title').addClass('ui-tree-selected');
                var defaultPublish = $("#categoryTree").find('.ui-tree-selected').find("span.true").attr("ispublish");
                if (defaultPublish == "true") {
                    $('#btnpublish').attr("name", "false").removeClass('icon-unchecked').addClass('icon-checked');
                }
                else {
                    $('#btnpublish').attr("name", "true").removeClass('icon-checked').addClass('icon-unchecked');
                }
                var previewCode = $("#categoryTree").find('.ui-tree-selected').find("span.true").attr("preview");
                $('#btnPreview').attr('name', previewCode);
            },
            BindPageDetails: function (data) {
                $('#divSearchedUsers').html('');
                $('#txtPageName').val(data.d.PageName);
                $('#txtTitle').val(data.d.Title);
                $('#txtDescription').val(data.d.Description);
                $('#txtKeyWords').val(data.d.KeyWords);
                $('#txtRefreshInterval').val(data.d.RefreshInterval);
                $('#txtStartDate').val(data.d.StartDate);
                $('#txtEndDate').val(data.d.EndDate);
                $('#cboParentPage').val(data.d.ParentID);
                $('#txtCaption').val(data.d.Caption);
                $('#chkShowInDashboard').prop("checked", data.d.IsVisible);
                if (!$('#rdbAdmin').prop("checked")) {
                    var str = data.d.MenuPages;
                    PageTreeView.GetMenuList();
                    $("#selMenulist option").prop('selected', false);
                    var substr = str.split('/');
                    if (substr == "0") {
                        $('#trIncludeInMenu').hide();
                    }
                    else {
                        $('#trIncludeInMenu,#selMenulist').show();
                        $.each(substr, function (index, item) {
                            $("#selMenulist option[value='" + substr[index] + "']").prop('selected', 'selected');
                        });
                    }
                }
                if (data.d.IsSecure == true ? $('#chkIsSecure').prop('checked', true) : $('#chkIsSecure').prop('checked', false));
                if (data.d.IsVisible == true ? $('#chkMenu').prop('checked', true) : $('#chkMenu').prop('checked', false));
                if (substr == "0") {
                    $('#chkMenu').prop("checked", false);
                }
                $('div.cssClassUploadFiles').html('');
                if (data.d.IconFile != "") {
                    var filePath = SageFrameHostURL + "/PageImages/" + data.d.IconFile;
                    var html = '<img title="' + data.d.IconFile + '" src="' + filePath + '"/><span class="deleteIcon"><label class="sfBtn icon-close"></label></span>';
                    $('div.cssClassUploadFiles').html(html);
                }
                var arr = new Array();
                $.each(data.d.LstPagePermission, function (inxs, ww) {
                    if (jQuery.inArray(ww.Username, arr) == -1)
                        arr.push(ww.Username);
                });
                $('#dvUser table').html('').show();
                var userHtml = "";
                $.each(arr, function (inx, itw) {
                    if (itw != "") {
                        var style = inx % 2 == 0 ? 'class="sfEven"' : 'class="sfOdd"';
                        userHtml += '<tr ' + style + '><td width="40%"><label>' + arr[inx] + '</label></td><td width="20%"><input type="checkbox" class="sfCheckbox" title="view"/></td>';
                        userHtml += '<td width="20%"><input type="checkbox" class="sfCheckbox" title="edit"/></td><td width="10%"><i id="imgDelete" class="icon-delete"></i></td></tr>';
                    }
                });
                if (userHtml != "")
                    $('#dvUser table').append(userHtml);
                if ($('#tblUser tr').length > 0)
                    $('#tblUser').show();
                else
                    $('#tblUser').hide();



                var roles = $('div.divPermission tr:gt(0)');
                var user = $('#dvUser tr');
                $.each(data.d.LstPagePermission, function (indx, itm) {
                    $.each(roles, function (index, item) {
                        if ($(item).prop('id') == itm.RoleID && itm.PermissionID == 1 && itm.Username == "") {
                            $(item).find('input[title="view"]').prop('checked', true);
                        }
                        else if ($(item).prop('id') == itm.RoleID && itm.PermissionID == 2 && itm.Username == "") {
                            $(item).find('input[title="edit"]').prop('checked', true);
                        }
                    });
                    $.each(user, function (index, ite) {
                        if ($(ite).find('td:eq(0) label').html() == itm.Username && itm.PermissionID == 1) {
                            $(ite).find('input[title="view"]').prop('checked', true);
                        }
                        else if ($(ite).find('td:eq(0) label').html() == itm.Username && itm.PermissionID == 2) {
                            $(ite).find('input[title="edit"]').prop('checked', true);
                        }
                    });
                });
            },
            BindChildCategory: function (response, PageID) {
                var strListmaker = '';
                var childNodes = '';
                var path = '';
                var itemPath = "";
                itemPath += "";
                var tabPath = '';
                $.each(response, function (index, item) {
                    if (item.Level > 0) {
                        if (item.ParentID == PageID) {
                            itemPath += item.PageName;
                            var PageName = item.PageName.replace(/^[-=\s]*/mg, "");
                            var styles = PageName.toLowerCase() === p.StartupPage.toLowerCase() ? 'class="sfChild file-folder required"' : 'class="sfChild file-folder"';
                            //////strListmaker += '<li  ' + styles + ' id=' + item.PageID + '><span title=' + item.IsPublished + ' ispublish=' + item.IsVisible + ' class="PageName" class=' + item.PreviewCode + '>' + item.PageNameWithoughtPrefix + '</span>';
                            //////strListmaker += '<span><img title="EditPage" alt="editPage" id="ctl18_imgEditNew" src="../Administrator/Templates/Default/images/context-add-page.png" style="vertical-align: middle; padding-right: 2px;">';
                            //////strListmaker += '<img title="Remove" alt="Remove" id="ctl18_imgRemove" src="../Administrator/Templates/Default/images/context-delete.png" style="vertical-align: middle; padding-right: 2px;"></span>';
                            //                            strListmaker += '<li  ' + styles + ' id=' + item.PageID + '><span title=' + item.IsPublished + ' class=' + item.IsActive + '></span>' + item.PageNameWithoughtPrefix;
                            var pageName = item.PageName.replace(new RegExp("-", "g"), ' ').trim().replace(/ /g, '-');
                            strListmaker += '<li ' + styles + ' id=' + item.PageID + '> <span name="sfPageName" isPublish=' + item.IsVisible + ' preview=' + item.PreviewCode + ' title=' + item.IsActive + ' class="true" pagename=' + pageName + '></span>' + item.PageNameWithoughtPrefix;
                            childNodes = PageTreeView.BindChildCategory(response, item.PageID);
                            itemPath = itemPath.replace(itemPath.lastIndexOf(item.AttributeValue), '');
                            if (childNodes != '') {
                                strListmaker += "<ul>" + childNodes + "</ul>";
                            }
                            strListmaker += '</li>';
                        }
                    }
                });
                return strListmaker;
            },
            UpdSettingKeyValue: function (ActiveTemplateName, PageName, OldPageName) {
                this.config.method = "UpdSettingKeyValue";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    ActiveTemplateName: ActiveTemplateName,
                    PageName: PageName,
                    OldPageName: OldPageName,
                    userName: p.UserName,
                    portalID: p.PortalID,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 0;
                this.ajaxCall(this.config);
            },
            AddDragDrop: function () {
                $('#categoryTree').tree({
                    expand: 'false',
                    droppable: [
            		            {
            		                element: 'categoryTreeli.ui-tree-node',
            		                //tolerance: 'fit',
            		                aroundTop: '25%',
            		                aroundBottom: '25%',
            		                aroundLeft: 0,
            		                aroundRight: 0
            		            },
            		            {
            		                element: 'li.ui-tree-list,li.ui-tree-node',
            		                //tolerance: 'fit',
            		                aroundTop: '25%',
            		                aroundBottom: '25%',
            		                aroundLeft: 0,
            		                aroundRight: 0
            		            }
                    ],
                    drop: function (event, ui) {

                        var draggebleSpanID = $('#categoryTree').find('li.ui-draggable-dragging');
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
                                var html = $(ui.draggable).find('span:eq(1)').html();
                                var pageName = html.split('>');
                                var parentID = typeof $(ui.droppable).closest('ul').parent('li').prop('id') == 'undefined' ? 0 : $(ui.droppable).closest('ul').parent('li').prop('id');
                                if ($('#rdbFronMenu').prop("checked")) {
                                    PageTreeView.SortTreeViewMenu($(ui.draggable).prop('id'), parentID, pageName[pageName.length - 1], $(ui.droppable).parent('li').prop('id'), 0, p.PortalID, p.UserName);
                                }
                                break;
                            case 'bottom':
                                ui.target.after(ui.sender.getJSON(ui.draggable), ui.droppable);
                                ui.sender.remove(ui.draggable);
                                var html = $(ui.draggable).find('span:eq(1)').text();
                                var pageName = html.split('>');
                                var parentID = $(ui.droppable).closest('ul').parent('li').prop('id');
                                if (parentID == undefined) {
                                    parentID = 0;
                                }
                                if ($('#rdbFronMenu').prop("checked")) {
                                    PageTreeView.SortTreeViewMenu($(ui.draggable).prop('id'), parentID, pageName[pageName.length - 1], 0, $(ui.droppable).parent('li').prop('id'), p.PortalID, p.UserName);
                                }
                                break;
                            case 'center':
                                if (!$('#rdbAdmin').prop("checked")) {
                                    ui.target.append(ui.sender.getJSON(ui.draggable), ui.droppable);
                                    ui.sender.remove(ui.draggable);
                                    $(ui.droppable).parent('li').addClass('ui-tree-expanded');
                                    $(ui.droppable).parent('li').removeClass('ui-tree-list');
                                    $(ui.droppable).parent('li').addClass('ui-tree-node');

                                    var html = $(ui.draggable).find('span:eq(1)').html();
                                    var pageName = html.split('>');
                                    PageTreeView.SortTreeViewMenu($(ui.draggable).prop('id'), $(ui.droppable).parent('li').prop('id'), pageName[pageName.length - 1], 0, 0, p.PortalID, p.UserName);
                                }
                                break;
                        }
                    },
                    stop: function (event, ui) {
                        if ($('#rdbAdmin').prop("checked")) {
                            PageTreeView.SortAdminTreeViewMenu();
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
                    dblclick: function (event, ui) {
                        var id = ui.draggable[0].id;
                        id = ui.draggable[0].id.replace(/[^0-9]/gi, '');
                        GetCategoryByCagetoryID(id);
                        ResetImageTab();
                    }
                });
            },
            AddContextMenu: function () {
                var pageTree = $('#categoryTree li');
                $(pageTree).each(function (i) {
                    var self = $(this);
                    $(this).find("span").contextMenu('myMenu1', {
                        bindings: {
                            'add': function (t) {
                                PageTreeView.ClearControls();
                                PageTreeView.PageShow(1);
                                PageTreeView.ClearControls();
                                //  location.reload();
                            },
                            'edit': function (t) {
                                if ($(t).parents('li').find('li.required').length > 0 || $(t).parent("li").hasClass("required")) {
                                    $('#sf_lblConfirmation').text("Unable to edit a Portal Start Up Page");
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
                                    PageTreeView.ClearControls();
                                    PageTreeView.PageShow(1);
                                    PageTreeView.ClearControls();
                                    var pageID = $(t).parents('li').prop("id");
                                    $('#txtPageName').prop('title', pageID);
                                    PageTreeView.GetPageDetails(pageID);
                                }
                            },
                            'addmodule': function (t) {
                            },
                            'publish': function (t) {
                                PageTreeView.UpdatePage($(t).find('span.ui-tree-selected').parent('li').prop('id'), 0, 1, p.PortalID, p.UserName, "P");
                            },
                            'ubpublish': function (t) {
                                PageTreeView.UpdatePage($(t).find('span.ui-tree-selected').parent('li').prop('id'), 0, 0, p.PortalID, p.UserName, "P");
                            },
                            'showinmenu': function (t) {
                                PageTreeView.UpdatePage($(t).find('span.ui-tree-selected').parent('li').prop('id'), 1, 0, p.PortalID, p.UserName, "M");
                            },
                            'hideinmenu': function (t) {
                                PageTreeView.UpdatePage($(t).find('span.ui-tree-selected').parent('li').prop('id'), 0, 0, p.PortalID, p.UserName, "M");
                            },
                            'rename': function (t) {
                            },
                            'startpage': function (t) {
                                if ($('#rdbAdmin').prop("checked")) {
                                    $('#sf_lblConfirmation').text("Admin page cannot be start up page.");
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
                                    var StartUpPagePageName = $(t).closest('span.ui-tree-selected').text();
                                    PageTreeView.UpdSettingKeyValue(p.ActiveTemplateName, StartUpPagePageName, StartUpPagePageName);
                                    $('#hypPreview').prop("href", PagePath + "/" + StartUpPagePageName.replace(' ', '-') + Extension);
                                    PageTreeView.ClearControls();
                                    $('#categoryTree li').removeClass('required');
                                    $(t).parents('li').addClass('required');
                                }
                            },
                            'remove': function (t) {
                                if ($(t).text() == "Under Construction") {
                                    $('#sf_lblConfirmation').text("Under Construction page is not deleted.");
                                    $("#dialog").dialog({
                                        modal: true,
                                        buttons: {
                                            "Cancel": function () {
                                                $(this).dialog("close");
                                            }
                                        }
                                    });
                                }
                                else if ($(t).parents('li').find('li.required').length > 0 || $(t).parent("li").hasClass("required")) {
                                    $('#sf_lblConfirmation').text("Unable to  delete.Before delete change startup page.");
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
                                    $('#sf_lblConfirmation').text("Are you sure you want to delete this page?");
                                    $("#dialog").dialog({
                                        modal: true,
                                        buttons: {
                                            "Confirm": function () {
                                                PageTreeView.DeletePage($(self).find('span.ui-tree-selected').parent('li').prop('id'), p.UserName, p.PortalID);
                                                $(self).find('span.ui-tree-selected').parent('li').remove();
                                                $(this).dialog("close");
                                                PageTreeView.ClearControls();
                                            },
                                            "Cancel": function () {
                                                $(this).dialog("close");
                                            }
                                        }
                                    });
                                }
                            }
                        },
                        menuStyle: {
                            //                                border: '1px solid #000'
                        },
                        itemStyle: {
                            //                                display: 'block',
                            //                                cursor: 'pointer',
                            //                                padding: '3px',
                            //                                border: '1px solid #fff',
                            //                                backgroundColor: 'transparent'
                        },
                        itemHoverStyle: {
                            //                                border: '1px solid #0a246a',
                            //                                backgroundColor: '#b6bdd2'
                        }
                    });

                });
            },
            IconUploader: function () {
                var uploadFlag = false;
                var upload = new AjaxUpload($('#flIcon'), {
                    action: Path + 'UploadHandler.ashx',
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
            PageShow: function (show) {
                switch (show) {
                    case 1:
                        $('.sfPageModules').hide();
                        $('.sfNewPage').show();
                        $('#divDroppable').hide();
                        $('.divLayoutContainer').hide();
                        $('#divLayout').attr('style', 'margin-right:0px');
                        break;
                    case 2:
                        $('.sfPageModules').show();
                        $('.sfNewPage').hide();
                        $('#divDroppable').show();
                        $('.divLayoutContainer').show();
                        $('#divLayout').attr('style', 'margin-right:200px');
                        break;
                }
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
            ClearControls: function () {
                $('#txtPageName').val('').removeAttr("disabled").removeClass("sfDisable");
                $('#txtCaption').val('');
                $('#cboParentPage').val('');
                $('#flIcon').val('');
                $('#txtTitle').val('');
                $('#txtDescription').val('');
                $('#txtKeyWords').val('');
                $('#txtStartDate').val('');
                $('#txtEndDate').val('');
                $('#txtRefreshInterval').val('');
                $('#chkIsSecure').prop("checked", false);
                LstPagePermission: lstPagePermission = [];
                $('#txtPageName').prop('title', 0);
                $('#cboPositionTab').val('');
                $('span.ui-tree-selected').removeClass("ui-tree-selected")
                $("div.divPermission tr:gt(1)").each(function () {
                    $(this).find("input:checkbox").prop("checked", false);
                });
                $('#tblUser tr:gt(0)').remove();
                $('#tblUser').hide();
                $('div.cssClassUploadFiles').html('');
                $('#chkMenu').prop("checked", false);
                $('#gdvModules,#hdnModules,#divPager').html("");
                $('#selMenulist').html('').hide();
                $('#lblSelectMenu').hide();
                $('label.sfError').remove();
                $('span.filename').text('No files selected');
            },
            IsUnassignedNode: function (li) {
                return (li.hasClass('unassigned-attributes'));
            },
            BindParentPages: function (pages) {
                var parentPages = pages.d;
                var html = "";
                if (PageTreeView.config.Mode) {
                    html += '<option value="2">---None---</option>';
                }
                else {
                    html += '<option value="0">---None---</option>';
                }
                $.each(parentPages, function (index, item) {
                    html += '<option value=' + item.PageID + '>' + item.PageName.replace(new RegExp("-", "g"), ' ') + '</option>';
                });
                $('#cboParentPage').html(html);
            },
            //changeed 
            GetPageDetails: function (pageID) {
                this.config.method = "GetPageDetails";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    pageID: pageID,
                    userName: p.UserName,
                    portalID: p.PortalID,
                    userModuleID: p.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                this.config.ajaxCallMode = 7;
                this.ajaxCall(this.config);
            },
            GetMenuList: function () {
                $.ajax({
                    type: PageTreeView.config.type,
                    contentType: PageTreeView.config.contentType,
                    cache: PageTreeView.config.cache,
                    async: false,
                    url: PageTreeView.config.baseURL + "GetAllMenu",
                    data: JSON2.stringify({
                        userName: p.UserName,
                        portalID: p.PortalID,
                        userModuleID: p.UserModuleID,
                        secureToken: SageFrameSecureToken
                    }),
                    dataType: PageTreeView.config.dataType,
                    success: function (msg) {
                        var LstMenu = msg.d;
                        var html = '';
                        var menulist = '';
                        var check = '';
                        $.each(LstMenu, function (index, item) {
                            if (item != "") {
                                menulist += '<option value=' + item.MenuID + '>' + item.MenuName + '</li>';
                            }
                        });
                        if (LstMenu.length == 0) {
                            menulist = '<option value="0">No Menu Available</option>';
                        }
                        $('#selMenulist').html(menulist);
                    }
                });
            }
        };
        PageTreeView.init();
    };
    $.fn.SageTreeBuilder = function (p) {
        $.createPageTreeView(p);
    };
})(jQuery);


