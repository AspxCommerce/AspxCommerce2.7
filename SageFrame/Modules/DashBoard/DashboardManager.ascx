<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DashboardManager.ascx.cs"
    Inherits="Modules_DashBoard_DashboardManager" %>
<script type="text/javascript">
    //<![CDATA[
    $(function () {
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
                ajaxCallMode: 0,
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
                PageExtension: '<%=PageExtension%>',
                userModuleId: '<%=userModuleId%>'
            },
            init: function () {
                this.InitTabs();
                this.BindEvents();
                //this.BindPages('#ddlPages');
                //this.GetQuickLinks();
                //this.IconUploader();
                this.TabIconUploader();
                $('#btnCancelSidebar').hide();
                this.BindSelectedTheme();


                DashboardMgr.BindPages('#ddlPagesSidebar');
                DashboardMgr.IconUploaderSidebar();
                DashboardMgr.LoadSidebar();
                DashboardMgr.LoadParentLinks();

                setTimeout(function () {
                    $('div.sfSidebarItems ul li div i.expand').eq(0).trigger('click');
                }, 200);
            },
            BindSelectedTheme: function () {
                $('div.sfAppearanceOptions input:radio').each(function () {
                    if ($(this).val() == DashboardMgr.config.Theme) {
                        $(this).prop("checked", true);
                    }
                });
            },
            InitTabs: function () {
                $('#tabDashboard').tabs({ fx: [null, { height: 'show', opacity: 'show'}] });
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
                $('#lbaAddSidebarTab').on("click", function () {
                    $('.sfAddSidebar').hide();
                    $('#divSidebarTab').show();
                    $('#lblError').text("");
                });
                $('#txtTabName').keyup(function (e) {
                    if ($(this).val().length > 0) {
                        $('#lblError').text('');
                    }
                });
                $('#lblCancelTab').on("click", function () {
                    $('.sfAddSidebar').show();
                    $('#divSidebarTab').hide();
                    $('#lblError').text("");
                });

                $('#btnAddQuickLink').bind("click", function () {
                    if (v.form()) {
                        var order = $('div.sfQuicklinklist ul li:last').index();
                        order = order + 1;
                        var url = $('#ddlPages option:selected').val() + DashboardMgr.config.PageExtension;
                        var imagepath = $('div.sfUploadedFiles img.sfIcon').prop("title");
                        var param = { linkObj: {
                            DisplayName: $('#txtLnkName').val(),
                            URL: url,
                            ImagePath: imagepath,
                            DisplayOrder: order,
                            PageID: $('#ddlPages').val(),
                            IsActive: $('#chkIsActiveQuicklink').prop("checked"),
                            QuickLinkID: parseInt(DashboardMgr.config.QuickLinkID)
                        },
                            UserName: DashboardMgr.config.UserName,
                            PortalID: DashboardMgr.config.PortalID,
                            userModuleId: DashboardMgr.config.userModuleId,
                            secureToken: SageFrameSecureToken
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
                $('div.sfSidebarItems').on('click', 'ul li.parent i.expand', function () {
                    $(this).parent().next("ul").slideDown();
                    $(this).removeClass("expand icon-arrow-slim-s").addClass("collapse icon-arrow-slim-n");
                });
                $('div.sfSidebarItems').on('click', 'ul li.parent i.collapse', function () {
                    $(this).parent().next("ul").slideUp();
                    $(this).removeClass("collapse icon-arrow-slim-n").addClass("expand icon-arrow-slim-s");
                });

                $('div.sfSidebarItems').on('click', 'ul li.parent ul li.child i.expand', function () {
                    $(this).parent().next("ul").slideDown();
                    $(this).removeClass("expand icon-arrow-slim-s").addClass("collapse icon-arrow-slim-n");
                });
                $('div.sfSidebarItems').on('click', 'ul li.child i.collapse', function () {
                    $(this).parent().next("ul").slideUp();
                    $(this).removeClass("collapse icon-arrow-slim-n").addClass("expand icon-arrow-slim-s");
                });

                $('#tabDashboard li.tab-sidebar a').bind("click", function () {
                    DashboardMgr.BindPages('#ddlPagesSidebar');
                    DashboardMgr.IconUploaderSidebar();
                    DashboardMgr.LoadSidebar();
                    DashboardMgr.LoadParentLinks();
                });

                $('#lblSaveTab').bind("click", function () {

                    if ($('#txtTabName').val() != "") {
                        $('#lblError').text('');
                        DashboardMgr.config.SaveMode == "Add";
                        if (DashboardMgr.config.SaveMode == "Add") {

                            var order = $('div.sfSidebarItems ul').children('li.index:last').index();
                            order = order + 1;
                            var url = ($('#txtTabName').val()) + DashboardMgr.config.PageExtension;
                            var imagepath = $('div.sfUploadedTabIcon img.sfIcon').prop("title");
                            var depth = 0;

                            var param = { sidebarObj: {
                                DisplayName: $('#txtTabName').val(),
                                Depth: depth,
                                ImagePath: imagepath,
                                URL: url,
                                ParentID: 0,
                                IsActive: true,
                                DisplayOrder: order,
                                SidebarItemID: parseInt(DashboardMgr.config.SidebarItemID),
                                PageID: 2
                            },
                                UserName: DashboardMgr.config.UserName,
                                PortalID: DashboardMgr.config.PortalID,
                                userModuleId: DashboardMgr.config.userModuleId,
                                secureToken: SageFrameSecureToken
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
                                }
                            });
                        }
                    }
                    else {
                        $('#lblError').text("Field should not be blank.");
                    }
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
                        var imagepath = $('div.sfUploadedFilesSidebar img.sfIcon').prop("title");
                        var depth = $('#ddlParentLinks').val() > 0 ? $('#ddlParentLinks').val() : 0;
                        var param = { sidebarObj: {
                            DisplayName: $('#txtSidebarName').val(),
                            Depth: depth,
                            ImagePath: imagepath,
                            URL: url,
                            ParentID: $('#ddlParentLinks').val(),
                            IsActive: $('#chkIsActiveSidebar').prop("checked"),
                            DisplayOrder: order,
                            SidebarItemID: parseInt(DashboardMgr.config.SidebarItemID),
                            PageID: $('#ddlPagesSidebar').val()
                        },
                            UserName: DashboardMgr.config.UserName,
                            PortalID: DashboardMgr.config.PortalID,
                            userModuleId: DashboardMgr.config.userModuleId,
                            secureToken: SageFrameSecureToken
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
                                $('#btnAddSidebar').text("Add Sidebar Item").addClass("icon-addnew").removeClass("icon-save");
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
                $('div.sfSidebarItems').on('click', 'i.delete', function () {

                    var self = $(this);
                    $('#sf_lblConfirmation').text("Are you sure you want to delete this item?");
                    $("#dialog").dialog({
                        modal: true,
                        buttons: {
                            "Confirm": function () {
                                DashboardMgr.DeleteSidebarItem($(self).prop("id"));
                                $(this).dialog("close");
                            },
                            "Cancel": function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                });
                $('.sfQuicklinklist').on('click', 'i.edit', function () {
                    var id = $(this).prop("id").replace("edit_", "");
                    DashboardMgr.LoadQuickLinkItem(id);
                    $('#btnAddQuickLink').text("Save").addClass("sfSave").removeClass("sfAdd");
                    $('#btnCancelQuickLink').show();
                    DashboardMgr.config.QuickLinkID = id;
                    DashboardMgr.config.SaveMode = "Edit";
                });
                $('div.sfSidebarItems').on('click', 'i.edit', function () {
                    $('.sfAddSidebar').show();
                    $('#divSidebarTab').hide();
                    var id = $(this).prop("id").replace("edit_", "");
                    DashboardMgr.LoadSidebarItem(id);
                    $('#btnAddSidebar').text("Save").addClass("icon-save").removeClass("icon-addnew");
                    $('#btnCancelSidebar').show();
                    DashboardMgr.config.SidebarItemID = id;
                    DashboardMgr.config.SaveMode = "Edit";
                });
                $('#btnSaveSidebarOrder').bind("click", function () {
                    var li = $('div.sfSidebarItems ul li');
                    var param = { OrderList: [], UserName: DashboardMgr.config.UserName, PortalID: DashboardMgr.config.PortalID, userModuleId: DashboardMgr.config.userModuleId, secureToken: SageFrameSecureToken };
                    $.each(li, function () {
                        param.OrderList.push({ "Key": $(this).prop("id").replace("li_", ""), "Value": parseInt($(this).index()) + 1 });

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
                $('#btnSaveQuickLinkOrder').bind("click", function () {
                    var li = $('div.sfQuicklinklist ul li');
                    var param = { OrderList: [], UserName: DashboardMgr.config.UserName, PortalID: DashboardMgr.config.PortalID, userModuleId: DashboardMgr.config.userModuleId, secureToken: SageFrameSecureToken };
                    $.each(li, function () {
                        param.OrderList.push({ "Key": $(this).prop("id").replace("ql_", ""), "Value": parseInt($(this).index()) + 1 });
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
                    $('#btnAddSidebar').text("Add Sidebar Item").addClass("icon-addnew").removeClass("icon-save");
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
                    var param = JSON2.stringify({ theme: option, PortalID: 1, UserName: 'superuser', userModuleId: DashboardMgr.config.userModuleId, secureToken: SageFrameSecureToken });
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
                $('#tabDashboard').on('click', '.deleteIcon', function () {
                    var IconPath = $('.sfIcon').prop('title');
                    $('.sfIcon').parent('div').remove();
                    DashboardMgr.DeleteIcon(IconPath);
                });
                $('div.sfQuicklinklist').on('click', 'i.delete', function () {
                    var self = $(this);
                    $('#sf_lblConfirmation').text("Are you sure you want to delete this item?");

                    $("#dialog").dialog({
                        modal: true,
                        buttons: {
                            "Confirm": function () {
                                DashboardMgr.DeleteLink($(self).prop("id"));
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
                var param = JSON2.stringify({ PortalID: parseInt(DashboardMgr.config.PortalID), userModuleId: DashboardMgr.config.userModuleId, UserName: SageFrameUserName, secureToken: SageFrameSecureToken });
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
                    data: JSON2.stringify({ UserName: DashboardMgr.config.UserName, PortalID: DashboardMgr.config.PortalID, userModuleId: DashboardMgr.config.userModuleId, secureToken: SageFrameSecureToken }),
                    dataType: DashboardMgr.config.dataType,
                    success: function (msg) {
                        var links = msg.d;
                        var html = '<ul>';
                        $.each(links, function (index, item) {
                            var editid = "edit_" + item.SidebarItemID;
                            var liid = 'li_' + item.SidebarItemID;
                            if (item.ChildCount == 0 && item.ParentID == 0) {
                                html += '<li id=' + liid + ' class="single index"><span class="title">' + item.DisplayName + '</span>';
                                html += '<i class="delete icon-delete" id=' + item.SidebarItemID + ' ></i>';
                                html += '<i class="edit icon-edit" id=' + editid + '></i>';
                                html += '</li>';
                            }
                            else if (item.ChildCount > 0 && item.ParentID == 0) {
                                html += '<li id=' + liid + ' class="parent index"><div class="sfHolder"><span class="title">' + item.DisplayName + '</span>';
                                html += "<i class='expand icon-arrow-slim-s'></i>";
                                html += '<i class="delete icon-delete" id=' + item.SidebarItemID + ' ></i>';
                                html += '<i class="edit icon-edit" id=' + editid + ' ></i>';
                                html += '</div><ul>';
                                $.each(links, function (i, child) {
                                    var editid = "edit_" + child.SidebarItemID;
                                    var liid = 'li_' + child.SidebarItemID;
                                    if (child.ParentID == item.SidebarItemID && child.ChildCount == 0) {
                                        html += '<li id=' + liid + ' class="child"><span>' + child.DisplayName + '</span>';
                                        html += '<i class="delete icon-delete" id=' + child.SidebarItemID + '></i>';
                                        html += '<i class="edit icon-edit" id=' + editid + '></i>';
                                        html += '</li>';
                                    }
                                    else if (child.ParentID == item.SidebarItemID && child.ChildCount > 0) {
                                        html += '<li id=' + liid + ' class="parent index"><div class="sfHolder"><span class="title">' + child.DisplayName + '</span>';
                                        html += "<i class='expand icon-arrow-slim-s'></i>";
                                        html += '<i class="delete icon-delete" id=' + child.SidebarItemID + ' ></i>';
                                        html += '<i class="edit icon-edit" id=' + editid + ' ></i>';
                                        html += '</div><ul>';
                                        $.each(links, function (j, grandchild) {
                                            var edit = 'edit_' + grandchild.SidebarItemID;
                                            var liid = 'li_' + grandchild.SidebarItemID;
                                            if (grandchild.ParentID == child.SidebarItemID && grandchild.ChildCount == 0) {                                               
                                                html += '<li id=' + liid + ' class="child"><span>' + grandchild.DisplayName + '</span>';
                                                html += '<i class="delete icon-delete" id=' + grandchild.SidebarItemID + '></i>';
                                                html += '<i class="edit icon-edit" id=' + edit + '></i>';
                                                html += '</li>';
                                            }
                                            else if (grandchild.ParentID == child.SidebarItemID && grandchild.ChildCount > 0) {                                                
                                                html += '<li id=' + liid + ' class="child"><div class="sfHolder"><span>' + grandchild.DisplayName + '</span>';
                                                html += "<i class='expand icon-arrow-slim-s'></i>";
                                                html += '<i class="delete icon-delete" id=' + grandchild.SidebarItemID + ' ></i>';
                                                html += '<i class="edit icon-edit" id=' + edit + '></i></div><ul>';
                                                $.each(links, function (k, grandgrandChild) {
                                                    if (grandgrandChild.ParentID == grandchild.SidebarItemID && grandgrandChild.ChildCount == 0) {
                                                        var edit = 'edit_' + grandgrandChild.SidebarItemID;
                                                        var liid = 'li_' + grandgrandChild.SidebarItemID;
                                                        html += '<li id=' + liid + '><span>' + grandgrandChild.DisplayName + '</span>';
                                                        html += '<i class="delete icon-delete" id=' + grandgrandChild.SidebarItemID + ' ></i>';
                                                        html += '<i class="edit icon-edit" id=' + edit + ' ></i>';
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
                    data: JSON2.stringify({ UserName: DashboardMgr.config.UserName, PortalID: SageFramePortalID, userModuleId: DashboardMgr.config.userModuleId }),
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
            LoadSidebarItem: function (sidebaritemid) {
                $.ajax({
                    type: DashboardMgr.config.type,
                    contentType: DashboardMgr.config.contentType,
                    cache: DashboardMgr.config.cache,
                    url: DashboardMgr.config.baseURL + "GetSidebarItem",
                    data: JSON2.stringify({ SidebarItemID: parseInt(sidebaritemid) }),
                    dataType: DashboardMgr.config.dataType,
                    success: function (msg) {
                        var sidebar = msg.d;
                        var html = '';
                        $('#txtSidebarName').val(sidebar.DisplayName);
                        $('#chkIsActiveSidebar').prop("checked", sidebar.IsActive);
                        if (sidebar.ImagePath != "") {
                            var image = DashboardMgr.config.Path + "Icons/" + sidebar.ImagePath;
                            var html = '<div><img class="sfIcon" title=' + sidebar.ImagePath + ' src="' + image + '" /><i class="deleteIcon icon-close"></i></div>';
                            $('div.sfUploadedFilesSidebar').html(html);
                        }
                        $('#ddlPagesSidebar').val(sidebar.URL.replace(DashboardMgr.config.PageExtension, ""));
                        $('#ddlParentLinks').val(sidebar.ParentID);
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
                    data: JSON2.stringify({ SidebarItemID: parseInt(0), UserName: DashboardMgr.config.UserName, PortalID: SageFramePortalID, userModuleId: DashboardMgr.config.userModuleId, secureToken: SageFrameSecureToken }),
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

                var param = JSON2.stringify({ QuickLinkID: parseInt(quicklinkid),
                    UserName: DashboardMgr.config.UserName,
                    PortalID: DashboardMgr.config.PortalID,
                    userModuleId: DashboardMgr.config.userModuleId,
                    secureToken: SageFrameSecureToken
                });
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
                var param = JSON2.stringify({ SidebarItemID: parseInt(sidebaritemid),
                    UserName: DashboardMgr.config.UserName,
                    PortalID: DashboardMgr.config.PortalID,
                    userModuleId: DashboardMgr.config.userModuleId,
                    secureToken: SageFrameSecureToken
                });
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
                    data: JSON2.stringify({ UserName: DashboardMgr.config.UserName, PortalID: DashboardMgr.config.PortalID, userModuleId: DashboardMgr.config.userModuleId, secureToken: SageFrameSecureToken }),
                    dataType: DashboardMgr.config.dataType,
                    success: function (msg) {
                        var links = msg.d;
                        var html = '<ul>';
                        $.each(links, function (index, item) {
                            var editid = 'edit_' + item.QuickLinkID;
                            var id = 'ql_' + item.QuickLinkID;

                            html += '<li id=' + id + '><span class="title">' + item.DisplayName + '</span>';
                            html += '<i class="delete icon-delete" id=' + item.QuickLinkID + '></i>';
                            html += '<i class="edit icon-edit" id=' + editid + '></i>';
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
                    data: JSON2.stringify({ UserName: DashboardMgr.config.UserName, PortalID: DashboardMgr.config.PortalID, userModuleId: DashboardMgr.config.userModuleId }),
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
                        $('#chkIsActiveQuicklink').prop("checked", quicklink.IsActive);

                        if (quicklink.ImagePath != "") {
                            var image = DashboardMgr.config.Path + "Icons/" + quicklink.ImagePath;
                            //var html = '<div><img class="sfIcon" title=' + quicklink.ImagePath + ' src="' + image + '" /><span class="deleteIcon"><img class="delete" src=' + SageFrame.utils.GetAdminImage("imgdelete.png") + ' alt="delete"/></span></div>';
                            var html = '<div><img class="sfIcon" title=' + quicklink.ImagePath + ' src="' + image + '" /><i class="deleteIcon icon-delete"></i></div>';
                            $('div.sfUploadedFiles').html(html);
                        }
                        $('#ddlPages').val(quicklink.URL.replace(DashboardMgr.config.PageExtension, ""));
                    }
                });
            },
            IconUploader: function () {
                var uploadFlag = false;
                var upload = new AjaxUpload($('#fupIcon'), {
                    action: DashboardMgr.config.Path + 'UploadHandler.ashx?userModuleId=' + DashboardMgr.config.userModuleId + '&portalID=' + SageFramePortalID + '&userName=' + SageFrameUserName + '&secureToken=' + SageFrameSecureToken,
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
                            jAlert('Not a valid image file!', 'Invalid File');
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
            TabIconUploader: function () {
                var uploadFlag = false;
                var upload = new AjaxUpload($('#fupTabIcon'), {
                    action: DashboardMgr.config.Path + 'UploadHandler.ashx?userModuleId=' + DashboardMgr.config.userModuleId + '&portalID=' + SageFramePortalID + '&userName=' + SageFrameUserName + '&secureToken=' + SageFrameSecureToken,
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
                            jAlert('Not a valid image file!', 'Invalid File');
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
                        $('div.sfUploadedTabIcon').html(html);
                    }
                });
            },
            IconUploaderSidebar: function () {
                var uploadFlag = false;
                var upload = new AjaxUpload($('#fupIconSidebar'), {
                    action: DashboardMgr.config.Path + 'UploadHandler.ashx?userModuleId=' + DashboardMgr.config.userModuleId + '&portalID=' + SageFramePortalID + '&userName=' + SageFrameUserName + '&secureToken=' + SageFrameSecureToken,
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
                            jAlert('Not a valid image file!', 'Invalid File');
                            return false;
                        }
                    },
                    onComplete: function (file, response) {
                        if (response == "LargeImagePixel") {
                            return ConfirmDialog(this, 'message', "The image size is too large in pixel");
                        }
                        var sidebaricon = file.split(" ").join("_");
                        var filePath = DashboardMgr.config.Path + "Icons/" + sidebaricon;
                        $('div.sfAddSidebar span.filename').text(sidebaricon);
                        var html = '<div><img class="sfIcon" title="' + sidebaricon + '" src="' + filePath + '" /><i class="deleteIcon icon-close"></span></div>';
                        $('div.sfUploadedFilesSidebar').html(html);
                    }
                });
            }
        };
        DashboardMgr.init();
    });
    //]]>
</script>
<h1>
    <asp:Label ID="lblDashMgr" runat="server" Text="Dashboard Link Manager"></asp:Label>
</h1>
<div>
    <div id="tabDashboard" class="cssClassTabpanelContent">
        <ul>
            <%-- <li><a href="#dvQuickLinks">Quick Links</a></li>--%>
            <li class="tab-sidebar"><a href="#dvSidebar">Sidebar</a></li>
            <%-- <li><a href="#dvSettings">Appearance</a></li>--%>
        </ul>
        <%--<div id="dvQuickLinks" class="sfDashboardpanel">
            <div class="sfTabLeftDiv">
                <fieldset>
                    <legend>Add Quick Link: </legend>
                    <div class="sfAddQuickLink">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td width="100">
                                    <label class="sfFormlabel">
                                        Display Name</label>
                                </td>
                                <td width="30">
                                    :
                                </td>
                                <td>
                                    <input type="text" id="txtLnkName" name="txtLnkName" class="sfInputbox" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="sfFormlabel">
                                        Pages</label>
                                </td>
                                <td width="30">
                                    :
                                </td>
                                <td>
                                    <select id="ddlPages" class="sfListmenu">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="sfFormlabel">
                                        Upload Icon</label>
                                </td>
                                <td width="30">
                                    :
                                </td>
                                <td>
                                    <input type="file" id="fupIcon" class="sfListmenu" />
                                    <div class="sfUploadedFiles">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="sfFormlabel">
                                        Is Active</label>
                                </td>
                                <td width="30">
                                    :
                                </td>
                                <td>
                                    <input type="checkbox" id="chkIsActiveQuicklink" checked="checked" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td width="30">
                                </td>
                                <td>
                                    <div class="sfButtonwrapper sftype1 sfMarginnone">
                                        <i class="icon-addnew sfBtn" id="btnAddQuickLink">Add QuickLink Item</i> <i class="icon-close sfBtn"
                                            id="btnCancelQuickLink" style="display: none">Cancel</i>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="sfQuicklinklist">
                    </div>
                    <div class="clear">
                    </div>
                </fieldset>
                <div class="sfButtonwrapper sftype1">
                    <i class="icon-save sfBtn" id="btnSaveQuickLinkOrder">Save Item Order</i>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>--%>
        <div id="dvSidebar" class="cssClassTabPabelTabel sfFormwrapper">
            <div class="sfTabLeftDiv">
                <div class="sfAddSidebar">
                    <div class="sfButtonwrapper sftype1 clearfix sfMarginnone">
                        <i class="icon-addnew sfBtn" id="lbaAddSidebarTab">Add Sidebar Tab</i>
                    </div>
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td width="100">
                                <label class="sfFormlabel">
                                    Display Name</label>
                            </td>
                            <td width="30">
                                :
                            </td>
                            <td>
                                <input type="text" id="txtSidebarName" name="txtSidebarName" class="sfInputbox" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="sfFormlabel">
                                    Parent</label>
                            </td>
                            <td width="30">
                                :
                            </td>
                            <td>
                                <select id="ddlParentLinks" class="sfListmenu">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="sfFormlabel">
                                    Pages</label>
                            </td>
                            <td width="30">
                                :
                            </td>
                            <td>
                                <select id="ddlPagesSidebar" class="sfListmenu">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="sfFormlabel">
                                    Upload Icon</label>
                            </td>
                            <td width="30">
                                :
                            </td>
                            <td>
                                <input type="file" id="fupIconSidebar" class="sfListmenu" />
                                <div class="sfUploadedFilesSidebar">
                                </div>
                                <br />
                                <p class="sfNote sfLocale">
                                    <i class="icon-info"></i>&nbsp;&nbsp;If you don't choose an icon image file, default
                                    system icon will be set automatically.</p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="sfFormlabel">
                                    Active</label>
                            </td>
                            <td width="30">
                                :
                            </td>
                            <td>
                                <input type="checkbox" checked="checked" id="chkIsActiveSidebar" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp
                            </td>
                            <td width="30">
                            </td>
                            <td>
                                <div class="sfButtonwrapper sftype1 clearfix sfMarginnone">
                                    <i class="icon-addnew sfBtn" id="btnAddSidebar">Add Sidebar Item</i> <i class="icon-close sfBtn"
                                        id="btnCancelSidebar">Cancel</i>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="sfSidebarItems">
                </div>
                <div id="divSidebarTab" style="display: none">
                    <table>
                        <tr>
                            <td>
                                <label class="sfFormlabel">
                                    Add Tab Name :
                                </label>
                                <input type="text" id="txtTabName" />
                                <label id="lblError" class="sfError">
                                </label>
                            </td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                            <td>
                                <label class="sfFormlabel">
                                    Upload Icon :</label>
                                <input type="file" id="fupTabIcon" class="sfListmenu" />
                                <div class="sfUploadedTabIcon">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="sfButtonwrapper sftype1 clearfix sfMarginnone">
                                    <i class="icon-save sfBtn" id="lblSaveTab">Save Sidebar Tab</i> <i class="icon-close sfBtn"
                                        id="lblCancelTab">Cancel</i>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="clear">
                </div>
                <div class="sfButtonwrapper sftype1">
                    <i class="sfSave icon-save sfBtn sfFloatRight" id="btnSaveSidebarOrder">Save Item Order</i>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
        <div id="dvSettings" class="cssClassTabPabelTabel" runat="server" visible="false">
            <table cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td width="120">
                        <label class="sfFormlabel">
                            Choose Appearance:</label>
                    </td>
                    <td width="30">
                        :
                    </td>
                    <td>
                        <div class="sfRadio sfAppearanceOptions">
                            <input type="radio" id="rdbDefault" value="default" name="selecttheme" checked="checked" />
                            <label>
                                Default</label>
                            <input type="radio" id="rdbGray" value="gray" name="selecttheme" />
                            <label>
                                Gray</label>
                            <input type="radio" id="rdbGreen" value="green" name="selecttheme" />
                            <label>
                                Green</label>
                            <input type="radio" id="rdbRed" value="red" name="selecttheme" />
                            <label>
                                Red</label>
                            <input type="radio" id="rdbBlack" value="black" name="selecttheme" />
                            <label>
                                Black</label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                    <td>
                        <div class="sftype1 sfMargintop">
                            <label id="btnSaveAppearance" class="sfSave">
                                Save</label>
                            <label id="btnRefresh" class="sfRefresh">
                                Refresh</label>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
