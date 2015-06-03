(function ($) {
    $.createDashboardLinks = function (p) {
        p = $.extend
        ({
            PortalID: 1,
            UserName: 'user',
            baseURL: 'Home',
            Path: 'divNav1',
            Theme: 'Default'
        }, p);

        var DashboardMgr = {
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
                ajaxCallMode: 0, ///0 for get categories and bind, 1 for notification,2 for versions bind                                   
                arr: [],
                arrModules: [],
                baseURL: '<%=appPath%>' + '/Modules/Dashboard/Services/DashboardWebService.asmx/',
                PortalID: 1,
                Path: '<%=appPath%>' + '/Modules/Dashboard/',
                SaveMode: "Add",
                SidebarItemID: 0,
                QuickLinkID: 0,
                Theme: '<%=Theme%>',
                UserName: '<%=UserName%>',
                PortalID: '<%=PortalID%>',
                PageExtension: '<%=PageExtension%>'
            },
            init: function () {
                this.InitTabs();
                this.BindEvents();
                this.BindPages('#ddlPages');
                this.GetQuickLinks();
                this.IconUploader();
                $('#btnCancelSidebar').hide();
                this.BindSelectedTheme();
            },
            BindSelectedTheme: function () {
                $('div.sfAppearanceOptions input:radio').each(function () {
                    if ($(this).val() == DashboardMgr.config.Theme) {
                        $(this).prop("checked", true);
                    }
                });
            },
            InitTabs: function () {
                $('#tabDashboard').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
            },
            BindEvents: function () {
                var v = $("#form1").validate({
                    rules: {
                        txtLnkName: { required: true },
                        txtSidebarName: { required: true }
                    },
                    messages: {
                        txtLnkName: "<br/>Please enter a Name",
                        txtSidebarName: "<br/>Please enter a Name"
                    }
                });

                $('#btnAddQuickLink').bind("click", function () {
                    if (v.form()) {
                        var order = $('div.sfQuicklinklist ul li:last').index();
                        order = order + 1;
                        var url = $('#ddlPages option:selected').val() + DashboardMgr.config.PageExtension;
                        var imagepath = $('div.sfUploadedFiles img.sfIcon').attr("title");
                        var param = {
                            linkObj: {
                                DisplayName: $('#txtLnkName').val(),
                                URL: url,
                                ImagePath: imagepath,
                                DisplayOrder: order,
                                PageID: $('#ddlPages').val(),
                                IsActive: $('#chkIsActiveQuicklink').prop("checked"),
                                QuickLinkID: parseInt(DashboardMgr.config.QuickLinkID),
                                secureToken: SageFrameSecureToken
                            }
                        };
                        $.ajax({
                            type: DashboardMgr.config.type,
                            contentType: DashboardMgr.config.contentType,
                            cache: DashboardMgr.config.cache,
                            url: DashboardMgr.config.SaveMode == "Add" ? DashboardMgr.config.baseURL + "AddLink" : DashboardMgr.config.baseURL + "UpdateLink",
                            data: JSON2.stringify(param),
                            dataType: DashboardMgr.config.dataType,
                            success: function (msg) {
                                SageFrame.messaging.show(DashboardMgr.GetLocalizedMessage("en-US", "DashboardManager", "LinkAddedSuccessfully"), "Success");
                                DashboardMgr.GetQuickLinks();
                                $('#txtLnkName').val('');
                                $('#ddlPages').val('');
                                $('div.sfUploadedFiles').html('');
                                $('span.filename').text('No file selected');
                            }
                        });
                    }
                    else {
                        return;
                    }
                });
                $('div.sfSidebarItems ul li.parent').on('click', 'img.expand', function () {
                    $(this).parent().next("ul").slideDown();
                    $(this).attr("src", SageFrame.utils.GetAdminImage("arrow1.png")).removeClass("expand").addClass("collapse");
                });
                $('div.sfSidebarItems ul li.parent').on('click', 'img.expand', function () {
                    $(this).parent().next("ul").slideUp();
                    $(this).attr("src", SageFrame.utils.GetAdminImage("arrow2.png")).removeClass("collapse").addClass("expand");
                });
                $('#tabDashboard li.tab-sidebar a').bind("click", function () {
                    DashboardMgr.BindPages('#ddlPagesSidebar');
                    DashboardMgr.IconUploaderSidebar();
                    DashboardMgr.LoadSidebar();
                    DashboardMgr.LoadParentLinks();
                });
                $('#btnAddSidebar').bind("click", function () {
                    if (DashboardMgr.config.SaveMode == "Add") {
                        var count = $('div.sfSidebarItems ul>li.parent').length + $('div.sfSidebarItems ul>li.single').length;
                        if (count > 15) {
                            return ConfirmDialog(this, "message", "Cannot add more than 16 items in the sidebar");
                        }
                    }
                    if (v.form()) {
                        var order = $('div.sfSidebarItems ul li:last').index();
                        order = order + 2;
                        var url = $('#ddlPagesSidebar option:selected').val() + DashboardMgr.config.PageExtension;
                        var imagepath = $('div.sfUploadedFilesSidebar img.sfIcon').attr("title");
                        var depth = $('#ddlParentLinks').val() > 0 ? $('#ddlParentLinks').val() : 0;
                        var param = {
                            sidebarObj: {
                                DisplayName: $('#txtSidebarName').val(),
                                Depth: depth,
                                ImagePath: imagepath,
                                URL: url,
                                ParentID: $('#ddlParentLinks').val(),
                                IsActive: $('#chkIsActiveSidebar').prop("checked"),
                                DisplayOrder: order,
                                SidebarItemID: parseInt(DashboardMgr.config.SidebarItemID),
                                PageID: $('#ddlPagesSidebar').val()
                            }
                        };

                        $.ajax({
                            type: DashboardMgr.config.type,
                            contentType: DashboardMgr.config.contentType,
                            cache: DashboardMgr.config.cache,
                            url: DashboardMgr.config.SaveMode == "Add" ? DashboardMgr.config.baseURL + "AddSidebar" : DashboardMgr.config.baseURL + "UpdateSidebarLinks",
                            data: JSON2.stringify(param),
                            dataType: DashboardMgr.config.dataType,
                            success: function (msg) {
                                DashboardMgr.LoadSidebar();
                                DashboardMgr.LoadParentLinks();
                                //DashboardMgr.LoadRealSidebar();
                                $('#btnAddSidebar').text("Add Sidebar Item").addClass("sfAdd").removeClass("sfSave");
                                $('#btnCancelSidebar').hide();
                                $('#txtSidebarName').val('');
                                $('#ddlParentLinks').val(0);
                                $('#ddlPagesSidebar').val('');
                                $('span.filename').text('No file selected');
                                $('div.sfUploadedFilesSidebar').html('');
                            }
                        });
                    }
                    else {
                        return;
                    }
                });
                $('div.sfSidebarItems').on('click', 'img.delete', function () {

                    var self = $(this);
                    $('#sf_lblConfirmation').text("Are you sure you want to delete this item?");
                    $("#dialog").dialog({
                        modal: true,
                        buttons: {
                            "Confirm": function () {
                                DashboardMgr.DeleteSidebarItem($(self).attr("id"));
                                $(this).dialog("close");
                            },
                            "Cancel": function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                });
                $('div.sfQuicklinklist').on('click', 'img.edit', function () {
                    var id = $(this).attr("id").replace("edit_", "");
                    DashboardMgr.LoadQuickLinkItem(id);
                    $('#btnAddQuickLink').text("Save").addClass("sfSave").removeClass("sfAdd");
                    $('#btnCancelQuickLink').show();
                    DashboardMgr.config.QuickLinkID = id;
                    DashboardMgr.config.SaveMode = "Edit";
                });
                $('div.sfSidebarItems').on('click', 'img.edit', function () {
                    var id = $(this).attr("id").replace("edit_", "");
                    DashboardMgr.LoadSidebarItem(id);
                    $('#btnAddSidebar').text("Save").addClass("sfSave").removeClass("sfAdd");
                    $('#btnCancelSidebar').show();
                    DashboardMgr.config.SidebarItemID = id;
                    DashboardMgr.config.SaveMode = "Edit";
                });
                $('#btnSaveSidebarOrder').bind("click", function () {
                    var li = $('div.sfSidebarItems ul li');
                    var param = { OrderList: [] };
                    $.each(li, function () {
                        param.OrderList.push({ "Key": $(this).attr("id").replace("li_", ""), "Value": parseInt($(this).index()) + 1 });
                    });
                    $.ajax({
                        type: DashboardMgr.config.type,
                        contentType: DashboardMgr.config.contentType,
                        cache: DashboardMgr.config.cache,
                        url: DashboardMgr.config.baseURL + "ReorderSidebar",
                        data: JSON2.stringify(param),
                        dataType: DashboardMgr.config.dataType,
                        success: function (msg) {
                            DashboardMgr.LoadSidebar();
                            DashboardMgr.LoadParentLinks();
                            $('#txtSidebarName').val('');
                            $('#ddlParentLinks').val(0);
                            $('#ddlPagesSidebar').val('');
                            $('div.sfUploadedFilesSidebar').html('');
                        }
                    });
                });
                $$('#btnSaveQuickLinkOrder').bind("click", function () {
                    var li = $('div.sfQuicklinklist ul li');
                    var param = { OrderList: [] };
                    $.each(li, function () {
                        param.OrderList.push({ "Key": $(this).attr("id").replace("ql_", ""), "Value": parseInt($(this).index()) + 1 });
                    });
                    $.ajax({
                        type: DashboardMgr.config.type,
                        contentType: DashboardMgr.config.contentType,
                        cache: DashboardMgr.config.cache,
                        url: DashboardMgr.config.baseURL + "ReorderQuickLinks",
                        data: JSON2.stringify(param),
                        dataType: DashboardMgr.config.dataType,
                        success: function (msg) {
                            DashboardMgr.GetQuickLinks();
                            $('#txtLnkName').val('');
                            $('#ddlPages').val('');
                            $('div.sfUploadedFiles').html('');
                            SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "DashboardManager", "ItemOrderSaved"), "Success");
                        }
                    });
                });
                $('#btnCancelSidebar').bind("click", function () {
                    $('#txtSidebarName').val('');
                    $('#ddlParentLinks').val(0);
                    $('#ddlPagesSidebar').val('');
                    $('div.sfUploadedFilesSidebar').html('');
                    $('#btnAddSidebar').text("Add Sidebar Item").addClass("sfAdd").removeClass("sfSave");
                    $('#btnCancelSidebar').hide();
                    DashboardMgr.config.SidebarItemID = 0;
                    DashboardMgr.config.SaveMode = "Add";
                });
                $('#btnCancelQuickLink').bind("click", function () {
                    $('#txtLnkName').val('');
                    $('#ddlPages').val('');
                    $('div.sfUploadedFiles').html('');
                    $('#btnAddQuickLink').text("Add QuickLink Item").addClass("sfAdd").removeClass("sfSave");
                    $('#btnCancelQuickLink').hide();
                    DashboardMgr.config.QuickLinkItemID = 0;
                    DashboardMgr.config.SaveMode = "Add";
                });
                $('#btnSaveAppearance').bind("click", function () {
                    var option = $('div.sfAppearanceOptions input:radio:checked').val();
                    var param = JSON2.stringify({ theme: option, PortalID: 1, UserName: 'superuser' });
                    $.ajax({
                        type: DashboardMgr.config.type,
                        contentType: DashboardMgr.config.contentType,
                        cache: DashboardMgr.config.cache,
                        url: DashboardMgr.config.baseURL + "UpdateAppearance",
                        data: param,
                        dataType: DashboardMgr.config.dataType,
                        success: function (msg) {
                            SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "DashboardManager", "ThemeChanged"), "Success");
                        }
                    });
                });
                $('#btnRefresh').bind("click", function () {
                    location.reload();
                });
                $('.deleteIcon').on('click', '.delete', function () {
                    //alert(71); //not working
                    var IconPath = $('.sfIcon').attr('title');
                    $('.sfIcon').parent('div').remove();
                    DashboardMgr.DeleteIcon(IconPath);
                });
                $('.sfTabLeftDiv').on('click', 'img.delete', function () {
                    var self = $(this);
                    $('#sf_lblConfirmation').text("Are you sure you want to delete this item?");

                    $("#dialog").dialog({
                        modal: true,
                        buttons: {
                            "Confirm": function () {
                                DashboardMgr.DeleteLink($(self).attr("id"));
                                $(this).dialog("close");
                            },
                            "Cancel": function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                });
            },
            ajaxFailure: function () {
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: this.config.type,
                    contentType: this.config.contentType,
                    cache: this.config.cache,
                    url: this.config.url,
                    data: this.config.data,
                    dataType: this.config.dataType,
                    success: this.ajaxSuccess,
                    error: this.ajaxFailure
                });
            },
            ajaxCall_return: function (url, param) {
                var data = null;
                $.ajax({
                    type: this.config.type,
                    contentType: this.config.contentType,
                    cache: this.config.cache,
                    url: url,
                    async: true,
                    data: '{}',
                    dataType: this.config.dataType,
                    success: function (msg) { data = msg.d; },
                    error: this.ajaxFailure
                });
                return data;
            },
            initsort: function () {
                $('div.sfQuicklinklist ul').sortable({ 'cursor': 'crosshair', 'placeholder': 'sfHighlight' });
                $('div.sfSidebarItems ul').sortable({ 'cursor': 'move', 'placeholder': 'sfHighlight' });
                $('div.sfSidebarItems ul li ul').sortable({ 'cursor': 'move' });
            },
            DeleteIcon: function (IconPath) {
                $.ajax({
                    type: DashboardMgr.config.type,
                    contentType: DashboardMgr.config.contentType,
                    cache: DashboardMgr.config.cache,
                    url: DashboardMgr.config.baseURL + "DeleteIcon",
                    data: JSON2.stringify({ IconPath: IconPath }),
                    dataType: DashboardMgr.config.dataType,
                    success: function (msg) {
                    }
                });
            },
            BindPages: function (id) {
                var param = JSON2.stringify({ PortalID: parseInt(DashboardMgr.config.PortalID) });
                $.ajax({
                    type: DashboardMgr.config.type,
                    contentType: DashboardMgr.config.contentType,
                    cache: DashboardMgr.config.cache,
                    url: DashboardMgr.config.baseURL + "GetPages",
                    data: param,
                    dataType: DashboardMgr.config.dataType,
                    success: function (msg) {
                        var pages = msg.d;
                        var html = '';
                        $.each(pages, function (index, item) {
                            //html += '<option value=' + item.PageID + '>' + item.PageName + DashboardMgr.config.PageExtension + '</option>';
                            html += '<option value=' + item.PageID + '>' + item.PageName + '</option>';
                        });
                        $(id).html(html);
                        $('#ajaxBusy').hide();
                    }
                });
            },
            LoadSidebar: function () {
                $.ajax({
                    type: DashboardMgr.config.type,
                    contentType: DashboardMgr.config.contentType,
                    cache: DashboardMgr.config.cache,
                    url: DashboardMgr.config.baseURL + "GetSidebar",
                    data: JSON2.stringify({ UserName: DashboardMgr.config.UserName, PortalID: DashboardMgr.config.PortalID }),
                    dataType: DashboardMgr.config.dataType,
                    success: function (msg) {
                        var links = msg.d;
                        var html = '<ul>';
                        $.each(links, function (index, item) {
                            var editid = "edit_" + item.SidebarItemID;
                            var liid = 'li_' + item.SidebarItemID;
                            if (item.ChildCount == 0 && item.ParentID == 0) {
                                html += '<li id=' + liid + ' class="single index"><span class="title">' + item.DisplayName + '</span>';
                                html += '<img class="delete" id=' + item.SidebarItemID + ' src="<%=appPath%>/Administrator/Templates/Default/images/imgdelete.png"/>';
                                html += '<img class="edit" id=' + editid + ' src=' + SageFrame.utils.GetAdminImage("imgedit.png") + '>';
                                html += '</li>';
                            }
                            else if (item.ChildCount > 0 && item.ParentID == 0) {
                                html += '<li id=' + liid + ' class="parent index"><div class="sfHolder"><span class="title">' + item.DisplayName + '</span>';
                                html += "<img class='expand' src=" + SageFrame.utils.GetAdminImage("arrow1.png") + ">";
                                html += '<img class="delete" id=' + item.SidebarItemID + ' src="<%=appPath%>/Administrator/Templates/Default/images/imgdelete.png"/>';
                                html += '<img class="edit" id=' + editid + ' src=' + SageFrame.utils.GetAdminImage("imgedit.png") + '>';
                                html += '</div><ul>';
                                $.each(links, function (i, child) {
                                    if (child.ParentID == item.SidebarItemID && child.ChildCount == 0) {
                                        var edit = 'edit_' + child.SidebarItemID;
                                        var liid = 'li_' + child.SidebarItemID;
                                        html += '<li id=' + liid + '><span>' + child.DisplayName + '</span>';
                                        html += '<img class="delete" id=' + child.SidebarItemID + ' src="<%=appPath%>/Administrator/Templates/Default/images/imgdelete.png"/>';
                                        html += '<img class="edit" id=' + edit + ' src=' + SageFrame.utils.GetAdminImage("imgedit.png") + '>';
                                        html += '</li>';
                                    }
                                    else if (child.ParentID == item.SidebarItemID && child.ChildCount > 0) {

                                        var edit = 'edit_' + child.SidebarItemID;
                                        var liid = 'li_' + child.SidebarItemID;
                                        html += '<li id=' + liid + '><span>' + child.DisplayName + '</span>';
                                        html += "<img class='expand' src=" + SageFrame.utils.GetAdminImage("arrow1.png") + ">";
                                        html += '<img class="delete" id=' + child.SidebarItemID + ' src="<%=appPath%>/Administrator/Templates/Default/images/imgdelete.png"/>';
                                        html += '<img class="edit" id=' + edit + ' src=' + SageFrame.utils.GetAdminImage("imgedit.png") + '><ul>';
                                        $.each(links, function (i, grandChild) {
                                            if (grandChild.ParentID == child.SidebarItemID && grandChild.ChildCount == 0) {
                                                var edit = 'edit_' + grandChild.SidebarItemID;
                                                var liid = 'li_' + grandChild.SidebarItemID;
                                                html += '<li id=' + liid + '><span>' + grandChild.DisplayName + '</span>';
                                                html += '<img class="delete" id=' + grandChild.SidebarItemID + ' src="<%=appPath%>/Administrator/Templates/Default/images/imgdelete.png"/>';
                                                html += '<img class="edit" id=' + edit + ' src=' + SageFrame.utils.GetAdminImage("imgedit.png") + '>';
                                                html += '</li>';
                                            }
                                        });
                                        html += '</ul></li>';
                                    }
                                });
                                html += '</ul>';
                                html += '</li>';
                            }
                        });
                        html += '</ul>';
                        $('div.sfSidebarItems').html(html);
                        DashboardMgr.initsort();
                        $('#ajaxBusy').hide();
                    }
                });
            },
            LoadRealSidebar: function () {
                $.ajax({
                    type: DashboardMgr.config.type,
                    contentType: DashboardMgr.config.contentType,
                    cache: DashboardMgr.config.cache,
                    url: DashboardMgr.config.baseURL + "GetSidebar",
                    data: JSON2.stringify({ UserName: DashboardMgr.config.UserName }),
                    dataType: DashboardMgr.config.dataType,
                    success: function (msg) {
                        var links = msg.d;
                        var html = '<ul class="menu">';
                        $.each(links, function (index, item) {
                            var image = DashboardMgr.config.Path + "Icons/" + item.ImagePath;
                            var url = '<%=appPath%>' + item.URL;
                            if (item.ChildCount == 0 && item.ParentID == 0) {
                                html += '<li><a href=' + url + '><img src=' + image + '><span>' + item.DisplayName + '</span></a></li>';
                            }
                            else if (item.ChildCount > 0) {
                                html += '<li class="parent"><a href="#"><img src=' + image + ' ><span>' + item.DisplayName + '</span></a>';
                                html += '<ul class="acitem">';
                                $.each(links, function (i, it) {
                                    if (it.ParentID == item.SidebarItemID) {
                                        html += '<li><a href="#">' + it.DisplayName + '</a></li>';
                                    }
                                });
                                html += '</ul>';
                                html += '</li>';
                            }
                        });
                        html += '</ul>';
                        var toggleSwitch = '<div class="sfHidepanel clearfix"><a href="#"><img src="/Administrator/Templates/Default/images/hide-arrow.png" alt="Hide " /><span>Hide Panel</span></a></div>';
                        $('div.sfSidebar').html(html).append(toggleSwitch);
                        $('.menu').initMenu();
                        $('#ajaxBusy').hide();
                    }
                });
            },
            LoadParentLinks: function () {
                $.ajax({
                    type: DashboardMgr.config.type,
                    contentType: DashboardMgr.config.contentType,
                    cache: DashboardMgr.config.cache,
                    url: DashboardMgr.config.baseURL + "GetParentLinks",
                    data: JSON2.stringify({ SidebarItemID: parseInt(0) }),
                    dataType: DashboardMgr.config.dataType,
                    success: function (msg) {
                        var links = msg.d;
                        var html = '';
                        $.each(links, function (index, item) {
                            html += '<option value=' + item.SidebarItemID + '>' + item.DisplayName + '</option>';
                        });
                        $('#ddlParentLinks').html(html);
                        $('#ajaxBusy').hide();
                    }
                });
            },
            DeleteLink: function (quicklinkid) {
                var param = JSON2.stringify({ QuickLinkID: parseInt(quicklinkid) });
                $.ajax({
                    type: DashboardMgr.config.type,
                    contentType: DashboardMgr.config.contentType,
                    cache: DashboardMgr.config.cache,
                    url: DashboardMgr.config.baseURL + "DeleteQuickLink",
                    data: param,
                    dataType: DashboardMgr.config.dataType,
                    success: function (msg) {
                        SageFrame.messaging.show(DashboardMgr.GetLocalizedMessage("en-US", "DashboardManager", "LinkDeletedSuccessfully"), "Success");
                        DashboardMgr.GetQuickLinks();
                        $('#ajaxBusy').hide();
                    }
                });
            },
            DeleteSidebarItem: function (sidebaritemid) {
                var param = JSON2.stringify({ SidebarItemID: parseInt(sidebaritemid) });
                $.ajax({
                    type: DashboardMgr.config.type,
                    contentType: DashboardMgr.config.contentType,
                    cache: DashboardMgr.config.cache,
                    url: DashboardMgr.config.baseURL + "DeleteSidebarItem",
                    data: param,
                    dataType: DashboardMgr.config.dataType,
                    success: function (msg) {
                        DashboardMgr.LoadSidebar();
                        DashboardMgr.LoadParentLinks();
                        $('#ajaxBusy').hide();
                    }
                });
            },
            GetLocalizedMessage: function (culturecode, modulename, messagetype) {
                var message = "";
                var param = JSON2.stringify({ CultureCode: culturecode, ModuleName: modulename, MessageType: messagetype });
                $.ajax({
                    type: DashboardMgr.config.type,
                    contentType: DashboardMgr.config.contentType,
                    cache: DashboardMgr.config.cache,
                    url: DashboardMgr.config.baseURL + "GetLocalizedMessage",
                    data: param,
                    dataType: DashboardMgr.config.dataType,
                    async: false,
                    success: function (msg) {
                        message = msg.d;
                    }
                });
                return message;
            },
            GetQuickLinks: function () {
                $.ajax({
                    type: DashboardMgr.config.type,
                    contentType: DashboardMgr.config.contentType,
                    cache: DashboardMgr.config.cache,
                    url: DashboardMgr.config.baseURL + "GetQuickLinks",
                    data: JSON2.stringify({ UserName: DashboardMgr.config.UserName, PortalID: DashboardMgr.config.PortalID }),
                    dataType: DashboardMgr.config.dataType,
                    success: function (msg) {
                        var links = msg.d;
                        var html = '<ul>';
                        $.each(links, function (index, item) {
                            var editid = 'edit_' + item.QuickLinkID;
                            var id = 'ql_' + item.QuickLinkID;

                            html += '<li id=' + id + '><span class="title">' + item.DisplayName + '</span>';
                            html += '<img class="delete" id=' + item.QuickLinkID + ' src="<%=appPath%>/Administrator/Templates/Default/images/imgdelete.png"/>';
                            html += '<img class="edit" id=' + editid + ' src=' + SageFrame.utils.GetAdminImage("imgedit.png") + '>';
                            html += '</li>';
                        });
                        html += '</ul>';
                        $('div.sfQuicklinklist').html(html);
                        DashboardMgr.initsort();
                        $('#ajaxBusy').hide();
                    }
                });
            },
            GetRealQuickLinks: function () {
                $.ajax({
                    type: DashboardMgr.config.type,
                    contentType: DashboardMgr.config.contentType,
                    cache: DashboardMgr.config.cache,
                    url: DashboardMgr.config.baseURL + "GetQuickLinks",
                    data: JSON2.stringify({ UserName: DashboardMgr.config.UserName }),
                    dataType: DashboardMgr.config.dataType,
                    success: function (msg) {
                        var links = msg.d;
                        var html = '<ul>';
                        $.each(links, function (index, item) {
                            var image = DashboardMgr.config.Path + "Icons/" + item.ImagePath;
                            var url = '<%=appPath%>' + item.URL;
                            html += '<li><a href=' + url + '><img src=' + image + ' width="24" height="24" alt=' + item.DisplayName + ' /><span>' + item.DisplayName + '</span></a></li>';
                        });
                        html += '</ul>';
                        $('div.sfquicklinks').html(html);
                        $('div.sfquicklinks').jcarousel();
                    }
                });
            },
            LoadQuickLinkItem: function (quicklinkitemid) {
                $.ajax({
                    type: DashboardMgr.config.type,
                    contentType: DashboardMgr.config.contentType,
                    cache: DashboardMgr.config.cache,
                    url: DashboardMgr.config.baseURL + "GetQuickLinkItem",
                    data: JSON2.stringify({ QuickLinkItemID: parseInt(quicklinkitemid) }),
                    dataType: DashboardMgr.config.dataType,
                    success: function (msg) {
                        var quicklink = msg.d;
                        var html = '';
                        $('#txtLnkName').val(quicklink.DisplayName);
                        $('#chkIsActiveQuicklink').attr("checked", quicklink.IsActive);

                        if (quicklink.ImagePath != "") {
                            var image = DashboardMgr.config.Path + "Icons/" + quicklink.ImagePath;
                            var html = '<div><img class="sfIcon" title=' + quicklink.ImagePath + ' src="' + image + '" /><span class="deleteIcon"><img class="delete" src=' + SageFrame.utils.GetAdminImage("imgdelete.png") + ' alt="delete"/></span></div>';
                            $('div.sfUploadedFiles').html(html);
                        }
                        $('#ddlPages').val(quicklink.URL.replace(DashboardMgr.config.PageExtension, ""));
                    }
                });
            },
            IconUploader: function () {
                var uploadFlag = false;
                var upload = new AjaxUpload($('#fupIcon'), {
                    action: DashboardMgr.config.Path + 'UploadHandler.ashx?userModuleId=' + p.UserModuleID + '&portalID=' + SageFramePortalID + '&userName=' + SageFrameUserName + '&secureToken=' + SageFrameSecureToken,
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
                            alert("Not a valid image file");
                            return false;
                        }
                    },
                    onComplete: function (file, response) {
                        if (response == "LargeImagePixel") {
                            return ConfirmDialog(this, 'message', "The image size is too large in pixel");
                        }
                        var linkicon = file.split(" ").join("_");
                        var filePath = DashboardMgr.config.Path + "Icons/" + linkicon;
                        $('div.sfAddQuickLink span.filename').text(linkicon);
                        var html = '<div><img class="sfIcon" title="' + linkicon + '" src="' + filePath + '" /><span class="deleteIcon"><img class="delete" src=' + SageFrame.utils.GetAdminImage("imgdelete.png") + ' alt="delete"/></span></div>';
                        $('div.sfUploadedFiles').html(html);
                    }
                });
            },
            IconUploaderSidebar: function () {
                var uploadFlag = false;
                var upload = new AjaxUpload($('#fupIconSidebar'), {
                    //action: DashboardMgr.config.Path + 'UploadHandler.ashx',
                    action: DashboardMgr.config.Path + 'UploadHandler.ashx?userModuleId=' + p.UserModuleID + '&portalID=' + SageFramePortalID + '&userName=' + SageFrameUserName + '&secureToken=' + SageFrameSecureToken,
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
                            alert('Alert Message<p>Not a valid image file!</p>');
                            return false;
                        }
                    },
                    onComplete: function (file, response) {
                        if (response == "LargeImagePixel") {
                            return ConfirmDialog(this, 'message', "The image size is too large in pixel");
                        }
                        var filePath = DashboardMgr.config.Path + "Icons/" + file;
                        $('div.sfAddSidebar span.filename').text(file);
                        var html = '<div><img class="sfIcon" title="' + file + '" src="' + filePath + '" /><span class="deleteIcon"><img class="delete" src=' + SageFrame.utils.GetAdminImage("imgdelete.png") + ' alt="delete"/></span></div>';
                        $('div.sfUploadedFilesSidebar').html(html);
                    }
                });
            }
        };
        DashboardMgr.init();
    };
    $.fn.InitDashboard = function (p) {
        $.createDashboardLinks(p);
    };
})(jQuery);