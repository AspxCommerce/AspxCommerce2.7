var AttributeSetManage = "";
$(function() {
    var aspxCommonObj = function() {
        var aspxCommonInfo = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        return aspxCommonInfo;
    };
    var treeHTML = '';
    var attGroup = new Array();

    var flag = '';
    var ID = '';
    var SetName = '';
    var SetID = '';
    var isSystemUsedAttrSet = "no";
    AttributeSetManage = {
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
        ajaxCall: function(config) {
            $.ajax({
                type: AttributeSetManage.config.type,
                beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: AttributeSetManage.config.contentType,
                cache: AttributeSetManage.config.cache,
                async: AttributeSetManage.config.async,
                data: AttributeSetManage.config.data,
                dataType: AttributeSetManage.config.dataType,
                url: AttributeSetManage.config.url,
                success: AttributeSetManage.ajaxSuccess,
                error: AttributeSetManage.ajaxFailure
            });
        },
        init: function () {
            var v = $("#form1").validate({
                messages: {
                    GroupName: {
                        required: getLocale(AspxAttributesManagement, "Please Enter the Group Name")
                    }
                },
                rules:
                    {
                        GroupName: {
                            minlength: 1
                        }
                    }
            });
            AttributeSetManage.BindAttributeSetGrid(null, null, null);
            $("divAttribSetGrid").show();
            $("#divAttribSetAddForm").hide();
            $("#divAttribSetEditForm").hide();
            $("#languageSelect li").click(function () {
                $('#languageSelect').find('li').removeClass("languageSelected");
                $(this).addClass("languageSelected");

            });
            $('#btnSaveAttributeSet').click(function () {
                $('.btnResetEdit').show();
                $('.btnUpdateAttributeSet').show();
                var errors = '';
                var attributeSetName = $('#txtAttributeSetName').val();
                if (!attributeSetName) {
                    errors += getLocale(AspxAttributesManagement, "Please enter attribute set name.");
                }
                if (errors == '') {
                    AttributeSetManage.CheckUniqueness(0, attributeSetName, false);
                } else {
                    csscody.alert('<h1>' + getLocale(AspxAttributesManagement, "Information Alert") + '</h1><p>' + errors + '</p>');
                    $('#txtAttributeSetName').focus();
                    $("#btnAddNewGroup").show();
                    return false;
                }
            });

            $('.btnUpdateAttributeSet').click(function() {
                var errors = '';
                var attributeSetName = $('#txtOldAttributeSetName').val();
                if (!attributeSetName) {
                    errors += getLocale(AspxAttributesManagement, "Please enter attribute set name.");
                }
                if (errors == '') {
                    var attributeSet_Id = 0;
                    if ($(this).prop("id")) {
                        attributeSet_Id = $(this).prop("id").replace(/[^0-9]/gi, '');
                    }
                    AttributeSetManage.CheckUniqueness(attributeSet_Id, attributeSetName, true);
                } else {
                    csscody.alert('<h1>' + getLocale(AspxAttributesManagement, "Information Alert") + '</h1><p>' + errors + '</p>');
                    $('#txtOldAttributeSetName').focus();
                    return false;
                }
            });

            $('#btnAddNewGroup').click(function () {
                $("#txtGroupName").val("");
                ShowPopupControl('popuprel5');                
            });
            $("#btnSaveGroupName").click(function () {
                if (v.form()) {
                    var attributeSetId = $(".btnResetEdit").prop("id").replace(/[^0-9]/gi, '');
                    var txtGroupName = $("#txtGroupName").val();                    
                    AttributeSetManage.AddNewGroup(attributeSetId, txtGroupName);                    
                    $('#fade, #popuprel5').fadeOut();
                    RemovePopUp();
                }
            });
            $('#btnCollapse').click(function () {
                $("#tree").tree({
                    expand: false,
                    expandOn: 'click',
                    collapseOn: 'click',
                    droppable: [
                                   {
                                       element: 'li.ui-tree-node.file-folder',
                                                                             aroundTop: '25%',
                                       aroundBottom: '25%',
                                       aroundLeft: 0,
                                       aroundRight: 0
                                   }
                    ],
                    drop: function (event, ui) {
                        var draggebleSpanID = $('#tree').find('li.ui-draggable-dragging');
                        var dropableSpanID = $('#' + $(this).find('li span.ui-tree-droppable').parents('li').prop("id"));
                        var mouseTopPosition = event.pageY - dropableSpanID.offset().top;

                        var dropableSpanHeight = $(this).find('li span.ui-tree-droppable').parents('li').height();
                        var portionOne = dropableSpanHeight / 4;
                        var separateLevelOne = dropableSpanHeight - portionOne;
                        var separateLevelTwo = dropableSpanHeight - portionOne * 3;

                        var draggebleSpanTopPosition = draggebleSpanID.position().top;
                        var dropableSpanTopPosition = dropableSpanID.position().top;
                        var difference = draggebleSpanTopPosition + mouseTopPosition - dropableSpanTopPosition;                        var returnOverStatePosition = '';
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
                                                               break;
                            case 'bottom':
                                ui.target.after(ui.sender.getJSON(ui.draggable), ui.droppable);
                                ui.sender.remove(ui.draggable);
                                                               break;
                            case 'center':
                                ui.target.append(ui.sender.getJSON(ui.draggable), ui.droppable);
                                ui.sender.remove(ui.draggable);
                                $(ui.droppable).parent('li').addClass('ui-tree-expanded');
                                $(ui.droppable).parent('li').removeClass('ui-tree-list');
                                $(ui.droppable).parent('li').addClass('ui-tree-node');
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
                    }
                });
                $('.file-folder').draggable({ disabled: true });
            });
            $('#btnExpand').click(function () {
                $("#tree").tree({
                    expand: '*',
                    expandOn: 'click',
                    collapseOn: 'click',
                    droppable: [
                                   {
                                       element: 'li.ui-tree-node.file-folder',
                                                                             aroundTop: '25%',
                                       aroundBottom: '25%',
                                       aroundLeft: 0,
                                       aroundRight: 0
                                   }
                    ],
                    drop: function (event, ui) {
                        var draggebleSpanID = $('#tree').find('li.ui-draggable-dragging');
                        var dropableSpanID = $('#' + $(this).find('li span.ui-tree-droppable').parents('li').prop("id"));
                        var mouseTopPosition = event.pageY - dropableSpanID.offset().top;

                        var dropableSpanHeight = $(this).find('li span.ui-tree-droppable').parents('li').height();
                        var portionOne = dropableSpanHeight / 4;
                        var separateLevelOne = dropableSpanHeight - portionOne;
                        var separateLevelTwo = dropableSpanHeight - portionOne * 3;

                        var draggebleSpanTopPosition = draggebleSpanID.position().top;
                        var dropableSpanTopPosition = dropableSpanID.position().top;
                        var difference = draggebleSpanTopPosition + mouseTopPosition - dropableSpanTopPosition;                        var returnOverStatePosition = '';
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
                                                               break;
                            case 'bottom':
                                ui.target.after(ui.sender.getJSON(ui.draggable), ui.droppable);
                                ui.sender.remove(ui.draggable);
                                                               break;
                            case 'center':
                                ui.target.append(ui.sender.getJSON(ui.draggable), ui.droppable);
                                ui.sender.remove(ui.draggable);
                                $(ui.droppable).parent('li').addClass('ui-tree-expanded');
                                $(ui.droppable).parent('li').removeClass('ui-tree-list');
                                $(ui.droppable).parent('li').addClass('ui-tree-node');
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
                    }
                });
                $('.file-folder').draggable({ disabled: true });
            });

            $('#btnBackAdd').click(function() {
                AttributeSetManage.clearForm();
                               $("#divAttribSetAddForm").hide();
                $("#divAttribSetEditForm").hide();
                $("#divAttribSetGrid").show();
            });

            $('#btnBackEdit').click(function() {
                AttributeSetManage.clearForm();
                $("#divAttribSetAddForm").hide();
                $("#divAttribSetEditForm").hide();
                $("#divAttribSetGrid").show();
            });

            $('#btnAddNewSet').click(function() {
                AttributeSetManage.clearForm();
                AttributeSetManage.BindAttributesSet();
                $("#divAttribSetGrid").hide();
                $("#divAttribSetEditForm").hide();
                $("#divAttribSetAddForm").show();
            });

            $(".btnResetEdit").click(function() {
                $('#dvTree').html('');
                treeHTML = '';
                attGroup = new Array();
                               var attributeSetId = $(this).prop("id").replace(/[^0-9]/gi, '');
                AttributeSetManage.CallEditFunction(attributeSetId);
            });

            $(".btnDeleteAttributeSet").click(function() {
                               var attributeSetId = $(this).prop("id").replace(/[^0-9]/gi, '');
                AttributeSetManage.DeleteAttributeSet(attributeSetId);
            });
            $('#txtSearchAttributeSetName,#ddlIsActive,#ddlUserInSystem').keyup(function(event) {
                if (event.keyCode == 13) {
                    AttributeSetManage.SearchAttributeSetName();
                }
            });
        },

        AddGroupName: function(_attributeSetId) {
            var x = '';
            var properties = { 'onComplete': function (x) { AttributeSetManage.AddNewGroup(_attributeSetId, x); } };           
            csscody.prompt("<h1>" + getLocale(AspxAttributesManagement, 'Please Enter the Group Name') + "</h1><p>" + getLocale(AspxAttributesManagement, 'Information Alert') + "</p>", x, properties);
            $("#alert-BoxContenedor").removeAttr("class");
            $("#alert-BoxContenedor").addClass('attributeBox');
            $("#alert-BoxContenedor").find("p").remove();
        },

        AddNewGroup: function (_attributeSetId, node) {            
            SetID = '';
            SetID = _attributeSetId;
            if (node) {
                var _isActive = true;
                var _isModified = false;
                var _updateFlag = false;
                var _groupId = 0;
                var attributeSaveObj = {
                    AttributeSetID: _attributeSetId,
                    GroupID: _groupId,
                    GroupName: node,
                    AliasName: node,
                    IsActive: _isActive,
                    IsModified: _isModified,
                    Flag: _updateFlag
                };
                this.config.url = this.config.baseURL + "SaveUpdateAttributeGroupInfo";
                this.config.data = JSON2.stringify({ attributeSaveObj: attributeSaveObj, aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            }
        },

        BindAttributeSetGrid: function(attributeSetNm, isAct, userInSystem) {
            var offset_ = 1;
            var current_ = 1;
            var perpage = ($("#gdvAttributeSet_pagesize").length > 0) ? $("#gdvAttributeSet_pagesize :selected").text() : 10;
            var AttributeSetBindObj = {
                AttributeSetName: attributeSetNm,
                IsSystemUsed: userInSystem,
                IsActive: isAct
            };
            $("#gdvAttributeSet").sagegrid({
                url: this.config.baseURL,
                functionMethod: 'GetAttributeSetGrid',
                colModel: [
                    { display: getLocale(AspxAttributesManagement, "Attribute Set ID"), name: 'attr_id', cssclass: 'cssClassHeadCheckBox', hide: true, coltype: 'checkbox', align: 'center' },
                    { display: getLocale(AspxAttributesManagement, "Attribute Set Name"), name: 'attr_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: getLocale(AspxAttributesManagement, "In System"), name: 'IsSystemUsed', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(AspxAttributesManagement, "Active"), name: 'IsActive', cssclass: 'cssClassHeadBoolean', controlclass: '', coltype: 'label', align: 'left', type: 'boolean', format: 'Yes/No' },
                    { display: getLocale(AspxAttributesManagement, "Actions"), name: 'action', cssclass: 'cssClassAction', controlclass: '', coltype: 'label', align: 'center' }
                ],
                buttons: [{ display: getLocale(AspxAttributesManagement, "Edit"), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'AttributeSetManage.EditAttributesSet', arguments: '2' },
                    { display: getLocale(AspxAttributesManagement, "Delete"), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'AttributeSetManage.DeleteAttributesSet', arguments: '2' },
                    { display: getLocale(AspxAttributesManagement, "Activate"), name: 'active', enable: true, _event: 'click', trigger: '4', callMethod: 'AttributeSetManage.ActiveAttributesSet', arguments: '2' },
                    { display: getLocale(AspxAttributesManagement, "Deactivate"), name: 'deactive', enable: true, _event: 'click', trigger: '5', callMethod: 'AttributeSetManage.DeactiveAttributesSet', arguments: '2' }
                ],
                rp: perpage,
                nomsg: getLocale(AspxAttributesManagement, "No Records Found!"),
                param: { AttributeSetBindObj: AttributeSetBindObj, aspxCommonObj: aspxCommonObj() },
                current: current_,
                pnew: offset_,
                sortcol: { 0: { sorter: false }, 4: { sorter: false} }
            });
        },

        show: function(id) {
            el = document.getElementById(id);
            if (el.style.display == 'none') {
                el.style.display = '';
            } else {
                el.style.display = 'none';
            }
        },

        DeleteAttributesSet: function(tblID, argus) {
            switch (tblID) {
                case "gdvAttributeSet":
                    if (argus[3].toLowerCase() != "yes") {
                        AttributeSetManage.DeleteAttributeSet(argus[0]);
                    } else {
                        csscody.alert('<h1>' + getLocale(AspxAttributesManagement, "Information Alert") + '</h1><p>' + getLocale(AspxAttributesManagement, "You can\'t delete System Attribute Set") + '</p>');
                        return false;
                    }
                    break;
                default:
                    break;
            }
        },

        ActiveAttributesSet: function(tblID, argus) {
            switch (tblID) {
                case "gdvAttributeSet":
                    if (argus[3].toLowerCase() != "yes") {
                        AttributeSetManage.ActivateAttributeSet(argus[0], true);
                    } else {
                        csscody.alert('<h1>' + getLocale(AspxAttributesManagement, "Information Alert") + '</h1><p>' + getLocale(AspxAttributesManagement, "You can\'t activate System Attribute Set") + '</p>');
                    }
                    break;
                default:
                    break;
            }
        },

        DeactiveAttributesSet: function(tblID, argus) {
            switch (tblID) {
                case "gdvAttributeSet":
                    if (argus[3].toLowerCase() != "yes") {
                        AttributeSetManage.ActivateAttributeSet(argus[0], false);
                    } else {
                        csscody.alert('<h1>' + getLocale(AspxAttributesManagement, "Information Alert") + '</h1><p>' + getLocale(AspxAttributesManagement, "You can\'t deactivate System Attribute Set") + '</p>');
                    }
                    break;
                default:
                    break;
            }
        },

        DeleteAttributeSet: function(_attributeSetId) {
                       var properties = {
                onComplete: function(e) {
                    AttributeSetManage.DeleteAttributeSetID(_attributeSetId, e);
                }
            }
            csscody.confirm("<h1>" + getLocale(AspxAttributesManagement, 'Delete Confirmation') + "</h1><p>" + getLocale(AspxAttributesManagement, 'Are you sure you want to delete this attribute set?') + "</p>", properties);
        },

        DeleteAttributeSetID: function(_attributeSetId, event) {
            if (event) {
                               this.config.url = this.config.baseURL + "DeleteAttributeSetByAttributeSetID";
                this.config.data = JSON2.stringify({ attributeSetId: parseInt(_attributeSetId), aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            }
            return false;
        },

        ActivateAttributeSet: function(_attributeSetId, _isActive) {
                       this.config.url = this.config.baseURL + "UpdateAttributeSetIsActiveByAttributeSetID";
            this.config.data = JSON2.stringify({ attributeSetId: parseInt(_attributeSetId), aspxCommonObj: aspxCommonObj(), isActive: _isActive });
            this.config.ajaxCallMode = 3;
            this.ajaxCall(this.config);
            return false;
        },

        CheckUniqueness: function(AttributeSetID, AttributeSetName, UpdateFlag) {
            flag = UpdateFlag;
            ID = AttributeSetID;
            SetName = AttributeSetName;
            var checkUniqueAttrSet = {
                AttributeSetID: AttributeSetID,
                AttributeSetName: AttributeSetName,
                Flag: UpdateFlag
            };
            var aspxTempCommonObj = aspxCommonObj();
            aspxTempCommonObj.CultureName = $(".languageSelected").attr("value");
            this.config.url = this.config.baseURL + "CheckAttributeSetUniqueness";
            this.config.data = JSON2.stringify({ checkUniqueAttrSet: checkUniqueAttrSet, aspxCommonObj: aspxTempCommonObj });
            this.config.ajaxCallMode = 4;
            this.ajaxCall(this.config);
        },

        SaveAttributeSetTree: function() {
            var saveString = '';
            var attributeIds = '';
            $("#tree>li").each(function(i) {
                if (!AttributeSetManage.isUnassignedNode($(this))) {
                    var groupIds = this.id.replace(/[^0-9]/gi, '');
                    $(this).find('li').each(function() {
                        attributeIds += this.id.replace(/[^0-9]/gi, '') + ',';
                    });
                    attributeIds = attributeIds.substr(0, attributeIds.length - 1);
                    if (attributeIds == '') {
                        attributeIds = 0;
                    }
                    saveString += groupIds + '-' + attributeIds + '#';
                    attributeIds = '';
                }
            });
            return saveString = saveString.substr(0, saveString.length - 1);
        },

        isUnassignedNode: function(li) {
            return (li.hasClass('unassigned-attributes'));
        },

        AddUpdateAttributeSet: function(attributeSet_id, _attributeSetBaseID, attributeSetName, isActive, isModified, _updateFlag, _saveString) {
            AttributeSetManage.AttributeSetAddUpdate(attributeSet_id, _attributeSetBaseID, attributeSetName, isActive, isModified, _updateFlag, _saveString);
        },

        clearForm: function() {
            $(".btnResetEdit").removeAttr("id");
            $(".btnDeleteAttributeSet").removeAttr("id");
            $(".btnUpdateAttributeSet").removeAttr("id");
            $("#txtAttributeSetName").val('');
            $('#' + lblAttributeSetInfo).html(getLocale(AspxAttributesManagement, "Edit Attribute Set"));
            $("#txtAttributeSetName").val('');
        },

        EditAttributesSet: function(tblID, argus) {
            switch (tblID) {
                case "gdvAttributeSet":
                   
                    isSystemUsedAttrSet = argus[3].toLowerCase();
                    AttributeSetManage.CallEditFunction(argus[0]);
                    if (argus[3].toLowerCase() == "yes") {
                        $("#btnAddNewGroup").hide();
                    } else {
                        $("#btnAddNewGroup").show();
                    }
                    break;
                default:
                    break;
            }
        },

        FillForm: function(response) {
            $.each(response.d, function(index, item) {
                if (index == 0) {
                    $('#' + lblAttributeSetInfo).html(getLocale(AspxAttributesManagement, "Edit Attribute Set: ") + item.AttributeSetName);
                    $("#txtOldAttributeSetName").val(item.AttributeSetName);

                    if (item.IsSystemUsed) {
                        $(".btnDeleteAttributeSet").hide();
                    } else {
                        $(".btnDeleteAttributeSet").show();
                    }
                }
            });
        },

        AttributeSetAddUpdate: function(_attributeSetId, _attributeSetBaseId, _attributeSetName, _isActive, _isModified, _flag, _saveString) {
            flag = _flag;
            var aspxCommonInfo = aspxCommonObj();
            aspxCommonInfo.CultureName = $(".languageSelected").attr("value");
            var attributeSetObj = {
                AttributeSetID: _attributeSetId,
                AttributeSetBaseID: _attributeSetBaseId,
                AttributeSetName: _attributeSetName,
                IsActive: _isActive,
                IsModified: _isModified,
                AddedBy: aspxCommonInfo.UserName,
                Flag: flag,
                SaveString: _saveString
            }
            this.config.url = this.config.baseURL + "SaveUpdateAttributeSetInfo";
            this.config.data = JSON2.stringify({ attributeSetObj: attributeSetObj, aspxCommonObj: aspxCommonInfo });
            this.config.ajaxCallMode = 5;
            this.ajaxCall(this.config);
        },

        BindAttributesSet: function() {
            var _attributeSetId = 0;
            this.config.url = this.config.baseURL + "GetAttributeSetList";
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj() });
            this.config.ajaxCallMode = 6;
            this.ajaxCall(this.config);
        },

        CallEditFunction: function(_attributeSetId) {
            $(".btnResetEdit").attr("id", 'attributesetid_' + _attributeSetId);
            $(".btnDeleteAttributeSet").attr("id", 'attributesetid_' + _attributeSetId);
            $(".btnUpdateAttributeSet").attr("id", 'attributesetid_' + _attributeSetId);
            $("#divAttribSetGrid").hide();
            $("#divAttribSetAddForm").hide();
            $("#divAttribSetEditForm").show();
            var functionName = 'GetAttributeSetDetailsByAttributeSetID';
            var params = { attributeSetId: _attributeSetId, aspxCommonObj: aspxCommonObj() };
            var mydata = JSON2.stringify(params);
            $.ajax({
                type: "POST", beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", umi);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                url: aspxservicePath + "AspxCoreHandler.ashx/" + functionName,
                data: mydata,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {
                    treeHTML = '';
                    $("#tree").removeClass('ui-tree');
                    $("#tree").html('');
                    AttributeSetManage.FillForm(msg);
                    AttributeSetManage.BindTreeView(msg);
                    attGroup = new Array();
                    AttributeSetManage.AddDragDrop();
                    AttributeSetManage.AddContextMenu();
                },
                error: function() {
                    csscody.error('<h1>' + getLocale(AspxAttributesManagement, "Error Message") + '</h1><p>' + getLocale(AspxAttributesManagement, "Failed to update attributes set.") + '</p>');
                }
            });
                                                                                                                      },

        AddDragDrop: function() {

            $("#tree").tree({
                expand: false,
                expandOn: 'click',
                collapseOn: 'click',
                droppable: [
                               {
                                   element: 'li.ui-tree-node.file-folder',
                                                                     aroundTop: '25%',
                                   aroundBottom: '25%',
                                   aroundLeft: 0,
                                   aroundRight: 0
                               }
                ],
                drop: function(event, ui) {
                    var draggebleSpanID = $('#tree').find('li.ui-draggable-dragging');
                    var dropableSpanID = $('#' + $(this).find('li span.ui-tree-droppable').parents('li').prop("id"));
                    var mouseTopPosition = event.pageY - dropableSpanID.offset().top;

                    var dropableSpanHeight = $(this).find('li span.ui-tree-droppable').parents('li').height();
                    var portionOne = dropableSpanHeight / 4;
                    var separateLevelOne = dropableSpanHeight - portionOne;
                    var separateLevelTwo = dropableSpanHeight - portionOne * 3;

                    var draggebleSpanTopPosition = draggebleSpanID.position().top;
                    var dropableSpanTopPosition = dropableSpanID.position().top;
                    var difference = draggebleSpanTopPosition + mouseTopPosition - dropableSpanTopPosition;                    var returnOverStatePosition = '';
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
                                                       break;
                        case 'bottom':
                            ui.target.after(ui.sender.getJSON(ui.draggable), ui.droppable);
                            ui.sender.remove(ui.draggable);
                                                       break;
                        case 'center':
                            ui.target.append(ui.sender.getJSON(ui.draggable), ui.droppable);
                            ui.sender.remove(ui.draggable);
                            $(ui.droppable).parent('li').addClass('ui-tree-expanded');
                            $(ui.droppable).parent('li').removeClass('ui-tree-list');
                            $(ui.droppable).parent('li').addClass('ui-tree-node');
                            break;
                    }
                },
                over: function(event, ui) {
                    $(ui.droppable).addClass('ui-tree-droppable');
                                                                         },
                out: function(event, ui) {
                    $(ui.droppable).removeClass('ui-tree-droppable');
                },
                overtop: function(event, ui) {
                                       $(ui.droppable).addClass('ui-tree-droppable-top');
                                   },
                overcenter: function(event, ui) {
                                       $(ui.droppable).addClass('ui-tree-droppable-center');
                                   },
                overbottom: function(event, ui) {
                                       $(ui.droppable).addClass('ui-tree-droppable-bottom');
                                   },
                outtop: function(event, ui) {
                    $(ui.droppable).removeClass('ui-tree-droppable-top');
                },
                outcenter: function(event, ui) {
                    $(ui.droppable).removeClass('ui-tree-droppable-center');
                },
                outbottom: function(event, ui) {
                    $(ui.droppable).removeClass('ui-tree-droppable-bottom');
                }
            });
            $('.file-folder').draggable({ disabled: true });
        },

        AddContextMenu: function() {
            $('#tree>li').each(function(i) {
                if (!AttributeSetManage.isUnassignedNode($(this))) {
                    $(this).contextMenu('myMenu1', {
                                                                                                                   
                                               onShowMenu: function(e, menu) {
                            if (AttributeSetManage.isNode(AttributeSetManage.getSelect().parent('li'))) {
                                if (AttributeSetManage.isSystemUsedGroup(AttributeSetManage.getSelect().parent('li'))) {
                                    $('#remove, #delete', menu).remove();
                                } else {
                                    $('#remove', menu).remove();
                                }
                            } else if (AttributeSetManage.isSystemUsedAttribute(AttributeSetManage.getSelect().parent('li'))) {
                                $('#rename, #delete, #remove', menu).remove();
                                $(menu).html('');
                            } else {
                                $('#rename, #delete', menu).remove();
                            }
                            return menu;
                        },
                        bindings: {
                            'rename': function(t) {
                                var value = AttributeSetManage.GetTitle(t);
                                                               var html = "<input id=\"txtEdit\" type=\"text\" value=\"" + value + "\" onblur=\"AttributeSetManage.SaveValue(this, '" + value + "')\" onfocus=\"PutCursorAtEnd(this)\" />";
                                AttributeSetManage.SetTitle(t, html);
                                $("#txtEdit").focus();
                            },
                            'delete': function(t) {
                                var attributeSetId = $(".btnResetEdit").prop("id").replace(/[^0-9]/gi, '');
                                var groupId = t.id.replace(/[^0-9]/gi, '');

                                                               var properties = {
                                    onComplete: function(x) {
                                        AttributeSetManage.DeleteGroupFromAttributeSetID(attributeSetId, groupId, x);
                                    }
                                }
                                csscody.confirm("<h1>" + getLocale(AspxAttributesManagement, 'Confirmation') + " </h1><p>" + getLocale(AspxAttributesManagement, 'Are you sure to delete this group?') + "</p>", properties);
                                                                                                                         },
                            'remove': function(t) {
                                                               var attributeSet_Id = $(".btnResetEdit").prop("id").replace(/[^0-9]/gi, '');
                                var groupId = t.id.replace(/[^0-9]/gi, '');
                                var is = AttributeSetManage.getSelect();
                                var liElement = is.parent("LI");
                                var attributeId = liElement[0].id.replace(/[^0-9]/gi, '');
                                                               var properties = {
                                    onComplete: function(x) {
                                        AttributeSetManage.DeleteAttributeFromAttributeSetID(attributeSet_Id, groupId, attributeId, x);
                                    }
                                }
                                csscody.confirm("<h1>" + getLocale(AspxAttributesManagement, 'Remove Confirmation') + "</h1><p>" + getLocale(AspxAttributesManagement, 'Are you sure to remove this attribute?') + "</p>", properties);
                            }
                        },
                        menuStyle: {
                            border: '1px solid #000'
                        },
                        itemStyle: {
                            display: 'block',
                            cursor: 'pointer',
                            padding: '3px',
                            border: '1px solid #fff',
                            backgroundColor: 'transparent'
                        },
                        itemHoverStyle: {
                            border: '1px solid #0a246a',
                            backgroundColor: '#b6bdd2'
                        }
                    });
                }
            });
        },

        getSelect: function() {
            var select = $('.ui-tree-selected', this.element);
            if (select.length) return select;
            else return null;
        },

        isNode: function(li) {
            return (li.hasClass('ui-tree-node'));
        },

        isSystemUsedGroup: function(li) {
            return (li.hasClass('systemused-groups'));
        },

        isSystemUsedAttribute: function(li) {
            return (li.hasClass('systemused-attributes'));
        },

        PutCursorAtEnd: function(obj) {
                                                       
        },

        GetTitle: function(node) {
            var title = $('>span.ui-tree-title', node);
            var html = '';
            if (title.length) {
                html = title.text().replace(/<span class="?ui-tree-title-img"?[^>]*>[\s\S]*?<\/span>/gi, '');
            } else {
                node = node.length ? node : $(node);
                html = node.text().replace(/<ul[^>]*>[\s\S]*<\/ul>/gi, '').replace(/\s*style="[^"]*"/gi, '');
            }
            return $.trim(html.replace(/\n/g, '').replace(/<a[^>]*>/gi, '').replace(/<\/a>/gi, ''));
        },

        SetTitle: function(node, title) {
            this.GetSPAN(node).html(title);
                   },

        GetSPAN: function(node) {
            node = node.length ? node : $(node);
            if (this.parentNodeName(node) == 'li') return $('>span.ui-tree-title:eq(0)', node);
            else if (this.parentNodeName(node) == 'ul') return $('span.ui-tree-title:eq(0)', node.parent());
            else return node;
        },

        parentNodeName: function(node) {
            if (node.prop != undefined) {
                return (node.length ? node.prop('nodeName') : $(node).prop('nodeName')).toLowerCase();
            }
        },

        SaveValue: function(obj, oldValue) {
            var functionName = 'RenameAttributeSetGroupAliasByGroupID';
            var value = $(obj).val();
                       if (value != "") {
                if (value != oldValue) {
                                       var attributeSetId = $(".btnResetEdit").prop("id").replace(/[^0-9]/gi, '');
                    var id = $(obj).parent().parent().prop("id");
                    id = id.replace(/[^0-9]/gi, '');
                    $(obj).parent().html('<img id="imgSaving' + id + '" src="' + aspxTemplateFolderPath + '/images/saving.gif" alt="' + getLocale(AspxAttributesManagement, 'Saving..') + '"/>');
                    var aspxCommonInfo = aspxCommonObj();
                    var attributeSetInfoToUpdate = {
                        GroupID: id,
                        CultureName: aspxCommonInfo.CultureName,
                        AliasName: value,
                        AttributeSetID: attributeSetId,
                        StoreID: aspxCommonInfo.StoreID,
                        PortalID: aspxCommonInfo.PortalID,
                        IsActive: true,
                        IsModified: true,
                        UpdatedBy: aspxCommonInfo.UserName
                    }
                    var params = { attributeSetInfoToUpdate: attributeSetInfoToUpdate };
                    var mydata = JSON2.stringify(params);
                    $.ajax({
                        type: "POST", beforeSend: function (request) {
                            request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                            request.setRequestHeader("UMID", umi);
                            request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                            request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                            request.setRequestHeader("PType", "v");
                            request.setRequestHeader('Escape', '0');
                        },
                        url: aspxservicePath + "AspxCoreHandler.ashx/" + functionName,
                        data: mydata,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: AttributeSetManage.SaveGroup_Success,
                        error: AttributeSetManage.Error
                    });
                } else {
                    $(obj).parent().html('<b>' + oldValue + '</b>');
                }
            } else {
                csscody.alert('<h1>' + getLocale(AspxAttributesManagement, "Information Alert") + '</h1><p>' + getLocale(AspxAttributesManagement, "Please enter group name.") + '</p>');
                $(obj).parent().html('<b>' + oldValue + '</b>');
            }
        },

        SaveGroup_Success: function(data, status) {
            $.each(data.d, function(index, item) {               
                $("#imgSaving" + item.GroupID).parent('span').html('<b>' + item.AliasName + '</b>');
            });
        },

        Error: function(request, status, error) {
            csscody.error('<h1>' + getLocale(AspxAttributesManagement, "Error Message") + '</h1><p>' + request.statusText + '</p>');
        },

        DeleteAttributeFromAttributeSetID: function(attributeSetId, groupId, attributeId, event) {
            SetID = attributeSetId;
            if (event) {
                var deleteGroupObj = {
                    AttributeSetID: attributeSetId,
                    GroupID: groupId,
                    AttributeID: attributeId
                };
                this.config.url = this.config.baseURL + "DeleteAttributeByAttributeSetID";
                this.config.data = JSON2.stringify({ deleteGroupObj: deleteGroupObj, aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 8;
                this.ajaxCall(this.config);
            }
            return false;
        },

        DeleteGroupFromAttributeSetID: function(attributeSetId, groupId, event) {
            SetID = '';
            SetID = attributeSetId;
            if (event) {
                var deleteGroupObj = {
                    AttributeSetID: attributeSetId,
                    GroupID: groupId
                };
                this.config.url = this.config.baseURL + "DeleteAttributeSetGroupByAttributeSetID";
                this.config.data = JSON2.stringify({ deleteGroupObj: deleteGroupObj, aspxCommonObj: aspxCommonObj() });
                this.config.ajaxCallMode = 9;
                this.ajaxCall(this.config);
            }
            return false;
        },

        BindTreeView: function(response) {
            var unassignedAttributeExist = false;
            var unassignedAttributesIds = '';
            var unassignedAttributesName = '';
            var groupId = '';
            treeHTML += '<ul id="tree">';
            $.each(response.d, function(index, item) {
                AttributeSetManage.BindGroup(index, item.GroupID, item.GroupName, item.AttributeID, item.AttributeName, item.IsSystemUsed, item.IsSystemUsedGroup);
                groupId = item.GroupID;
                if (index == 0 && item.UnassignedAttributes != "") {
                    unassignedAttributeExist = true;
                    unassignedAttributesIds = item.UnassignedAttributes;
                    unassignedAttributesName = item.UnassignedAttributesName;
                }
            });

            if (unassignedAttributeExist && isSystemUsedAttrSet != "yes") {
                AttributeSetManage.AddUnassignedAttribute(groupId, unassignedAttributesIds, unassignedAttributesName);
            }
            if (groupId > 0) {
                treeHTML += '</ul></li>';
            }
            treeHTML += '</ul>';
            $("#dvTree").html(treeHTML);            
        },

        AddUnassignedAttribute: function(groupId, unassignedAttributesIds, unassignedAttributesName) {
            if (groupId > 0) {
                treeHTML += '</ul></li>';
            }
            treeHTML += '<li id="group" class="file-folder unassigned-attributes"><b>' + getLocale(AspxAttributesManagement, "Unassigned Attributes") + '</b><ul>';
            var unassignedName = unassignedAttributesName.split(',');
            var unassignedId = unassignedAttributesIds.split(',');
            for (var i = 0; i < unassignedId.length; i++) {
                               treeHTML += '<li id="attribute' + unassignedId[i] + '" class="file php">' + unassignedName[i] + '</li>';
            }
            treeHTML += '</ul></li>';
        },

        BindGroup: function(index, groupID, groupName, attributeID, attributeName, isSystemUsedAttrib, isSystemUsedGroup) {
            if (groupID > 0) {
                var isGroupExist = false;
                for (var i = 0; i < attGroup.length; i++) {
                    if (attGroup[i].key == groupID) {
                        isGroupExist = true;
                        break;
                    }
                }
                if (!isGroupExist) {
                    if (attGroup.length != 0) {
                        treeHTML += '</ul></li>';
                    }
                    if (isSystemUsedGroup) {
                        treeHTML += '<li id="group' + groupID + '" class="file-folder systemused-groups"><b>' + groupName + '</b><ul>';
                    } else {
                        treeHTML += '<li id="group' + groupID + '" class="file-folder"><b>' + groupName + '</b><ul>';
                    }
                }
                if (attributeName != "") {
                    if (isSystemUsedAttrib) {
                        treeHTML += '<li id="attribute' + attributeID + '" class="file html systemused-attributes">' + attributeName + '</li>';
                    } else {
                        treeHTML += '<li id="attribute' + attributeID + '" class="file html">' + attributeName + '</li>';
                    }
                }
                if (!isGroupExist) {
                    attGroup.push({ key: groupID, value: groupName });
                }
            }
        },
        SearchAttributeSetName: function() {
            var attributeSetNm = $.trim($("#txtSearchAttributeSetName").val());
            var isAct = $.trim($('#ddlIsActive').val()) == "" ? null : $.trim($('#ddlIsActive').val()) == "True" ? true : false;
            var userInSystem = $.trim($("#ddlUserInSystem").val()) == "" ? null : $.trim($("#ddlUserInSystem").val()) == "True" ? true : false;
            if (attributeSetNm.length < 1) {
                attributeSetNm = null;
            }
            AttributeSetManage.BindAttributeSetGrid(attributeSetNm, isAct, userInSystem);
        },
        ajaxSuccess: function(msg) {
            switch (AttributeSetManage.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    AttributeSetManage.CallEditFunction(SetID);
                    break;
                case 2:
                    AttributeSetManage.BindAttributeSetGrid(null, null, null);
                    csscody.info("<h2>" + getLocale(AspxAttributesManagement, 'Successful Message') + "</h2><p>" + getLocale(AspxAttributesManagement, 'Attribute set has been deleted successfully.') + "</p>");
                    AttributeSetManage.clearForm();
                    $("#divAttribSetAddForm").hide();
                    $("#divAttribSetEditForm").hide();
                    $("#divAttribSetGrid").show();
                    break;
                case 3:
                    AttributeSetManage.BindAttributeSetGrid(null, null, null);
                    csscody.info("<h2>" + getLocale(AspxAttributesManagement, 'Successful Message') + "</h2><p>" + getLocale(AspxAttributesManagement, 'Attribute set status has been updated successfully.') + "</p>");
                    break;
                case 4:
                    var errors = '';
                    if (!msg.d) {
                        var isActive = true;
                        var saveString = '';
                        var _AttributeSetBaseID = 0;
                        _AttributeSetBaseID = $("#ddlAttributeSet").val();
                        if (!_AttributeSetBaseID) {
                            _AttributeSetBaseID = 0;
                        }
                        var isModified = false;
                        if (flag) {
                            isModified = true;
                            saveString = AttributeSetManage.SaveAttributeSetTree();
                        }
                        AttributeSetManage.AddUpdateAttributeSet(ID, _AttributeSetBaseID, SetName, isActive, isModified, flag, saveString);
                        AttributeSetManage.BindAttributeSetGrid(null, null, null);
                    } else {
                        errors += getLocale(AspxAttributesManagement, "Please enter unique attribute set name!") + "'" + SetName + "'" + getLocale(AspxAttributesManagement, "already exists.") + '<br/>';
                                                                      csscody.alert('<h1>' + getLocale(AspxAttributesManagement, "Information Alert") + '</h1><p>' + errors + '</p>');
                        $('#txtAttributeSetName').focus();
                        return false;
                    }
                    break;
                case 5:
                    if (!flag) {
                        AttributeSetManage.CallEditFunction(msg.d);
                    } else {
                        AttributeSetManage.clearForm();
                        $("#divAttribSetAddForm").hide();
                        $("#divAttribSetEditForm").hide();
                        $("#divAttribSetGrid").show();
                        csscody.info("<h2>" + getLocale(AspxAttributesManagement, 'Successful Message') + "</h2><p>" + getLocale(AspxAttributesManagement, 'Attribute set has been saved successfully.') + "</p>");
                    }
                    break;
                case 6:
                    $("#ddlAttributeSet").get(0).options.length = 0;
                    var item;
                    var length = msg.d.length;
                    for (var index = 0; index < length; index++) {
                        item = msg.d[index];
                        if (item.AttributeSetID != 3) {
                            $("#ddlAttributeSet").get(0).options[$("#ddlAttributeSet").get(0).options.length] = new Option(item.AliasName, item.AttributeSetID);
                        }
                    };
                    break;
                case 7:
                    break;
                case 8:
                    AttributeSetManage.CallEditFunction(SetID);
                    AttributeSetManage.AddDragDrop();
                    AttributeSetManage.AddContextMenu();
                    break;
                case 9:
                    AttributeSetManage.CallEditFunction(SetID);
                    break;
            }
        },
        ajaxFailure: function() {
            switch (AttributeSetManage.config.ajaxCallMode) {
                case 0:
                    break;
                case 1:
                    csscody.error('<h1>' + getLocale(AspxAttributesManagement, "Error Message") + '</h1><p>' + getLocale(AspxAttributesManagement, "Failed to update attribute group") + '</p>');
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    csscody.error('<h1>' + getLocale(AspxAttributesManagement, "Error Message") + '</h1><p>' + getLocale(AspxAttributesManagement, "Failed to check attribute set uniqueness") + '</p>');
                    break;
                case 5:
                    csscody.error('<h1>' + getLocale(AspxAttributesManagement, "Error Message") + '</h1><p>' + getLocale(AspxAttributesManagement, "Failed to update attribute set") + '</p>');
                    break;
                case 6:
                    csscody.error('<h1>' + getLocale(AspxAttributesManagement, "Error Message") + '</h1><p>' + getLocale(AspxAttributesManagement, "Failed to load attribute set") + '</p>');
                    break;
                case 7:
                                       break;
                case 8:
                    csscody.error('<h1>' + getLocale(AspxAttributesManagement, "Error Message") + '</h1><p>' + getLocale(AspxAttributesManagement, "Failed to remove attribute") + '</p>');
                    break;
                case 9:
                    csscody.error('<h1>' + getLocale(AspxAttributesManagement, "Error Message") + '</h1><p>' + getLocale(AspxAttributesManagement, "Failed to delete attribute set group") + '</p>');
                    break;
            }
        }
    };
    AttributeSetManage.init();
});