(function ($) {
    $.createLayoutManager = function (p) {
        p = $.extend
        ({
            PortalID: 1,
            AppPath: SageFrameAppPath,
            Extension: '',
            EditFilePath: '',
            UserModuleID: ''
        }, p);
        var editor = CodeMirror.fromTextArea(document.getElementById("txtLayoutEditor"), { lineNumbers: true, mode: { name: "xml", htmlMode: true } });
        var editorCreate = CodeMirror.fromTextArea(document.getElementById("txtLayoutCreator"), { lineNumbers: true, mode: { name: "xml", htmlMode: true } });
        var LayoutManager = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: { data: '' },
                dataType: 'json',
                baseURL: p.AppPath + '/Modules/LayoutManager/WebMethod.aspx/',
                method: "",
                url: "",
                ajaxCallMode: 0,
                ActiveLayout: '',
                ActiveTheme: '',
                SettingMode: 'Theme',
                Template: 'Default',
                AppPath: p.AppPath,
                NavMode: 'Folder',
                ReturnData: null,
                FileName: "",
                PortalID: p.PortalID,
                pchArr: [],
                wrapArr: [],
                ActivePageDiv: "",
                PanName: [],
                NewValue: [],
                sfHeader: '',
                sfFooter: '',
                sfContain: ''
            },
            init: function () {
                this.BindEvents();
                this.LoadTemplates();
                this.InitVisibility();
                SageFrame.tooltip.GetToolTip("imgToolTip", "#spnPresets", SageFrame.messages.Presets);
                SageFrame.tooltip.GetToolTip("imgToolTip1", "#spnActivelayout", "This is from where you can include items into your menu");
                SageFrame.tooltip.GetToolTip("imgToolTip2", "#spnActivetheme", "This is from where you can include items into your menu");
                SageFrame.tooltip.GetToolTip("imgToolTip3", "#spnActivewidth", "This is from where you can include items into your menu");
                SageFrame.tooltip.GetToolTip("imgToolTip4", "#spnHandheld", "This is from where you can include items into your menu");
                SageFrame.tooltip.GetToolTip("imgToolTip5", "#spnApplytopages", "This is from where you can include items into your menu");
                $('div.sfLayoutmanager').hide();
                $('#tabLayoutMgr').tabs({ fx: [null, { height: 'show', opacity: 'show' }] });
                $('div.sfPresetmessage').hide();
                LayoutManager.config.pchArr.push("&lt;placeholder");
                LayoutManager.config.wrapArr.push("&lt;wrap");
                $('#divLayoutWireframe').on('click', 'th', function () {
                    var tableId = $(this).parents('table').prop('id');
                    var self = $(this);
                    var th = self.parents('table').find('th');
                    if (self.prev('th').hasClass('sfActive') || self.next('th').hasClass('sfActive')) {
                        self.toggleClass('sfActive');
                        $('#merge_' + tableId).show();
                        $('#btnMerge_' + tableId).show();
                        $('#split_' + tableId).hide();
                    }
                    else {
                        th.not(self).removeClass('sfActive');
                        self.toggleClass('sfActive');
                        $('#split_' + tableId).show();
                        $('#btnSplit_' + tableId).show();
                        $('#merge_' + tableId).hide();
                    }
                    if (self.prev('th').hasClass('sfActive') && self.next('th').hasClass('sfActive')) {
                        th.removeClass('sfActive');
                        $('#merge_' + tableId).hide();
                        $('#split_' + tableId).hide();
                    }
                    if ($('#' + tableId).find('.sfActive').length == 0) {
                        $('#merge_' + tableId).hide();
                        $('#split_' + tableId).hide();
                    }
                    if ($('#' + tableId).find('.sfActive').length == 1) {
                        $('#merge_' + tableId).hide();
                        $('#btnSplit_' + tableId).show();
                        $('#split_' + tableId).show();
                    }
                    if ($('#' + tableId).find('.sfActive').length > 1) {
                        $('#merge_' + tableId).show();
                        $('#btnMerge_' + tableId).show();
                        $('#split_' + tableId).hide();
                    }
                });

                $('#divLayoutWireframe').on('click', '#btnReset', function () {
                    LayoutManager.ResetCore();
                });

                $('#lblSaveLayoutChange').on("click", function () {
                    LayoutManager.config.PanName = [];
                    LayoutManager.config.NewValue = [];
                    $('.sfOuterwrapper').each(function (index) {
                        if ($(this).prop('id') !== 'sfBodyContent') {
                            var tableId = $(this).prop('id') + "_mytable";
                            LayoutManager.LoadPanName($(this).prop('id'));
                            var pchId = '#divPlaceHolder_' + tableId.toLowerCase();
                            LayoutManager.LoadNewLayout($(pchId).text());
                        }
                    });
                    var containA = "";
                    LayoutManager.config.sfHeader = "";
                    LayoutManager.config.sfContent = "";
                    LayoutManager.config.sfFooter = "";
                    var temp = 0;
                    $('#sfOuterWrapper div.sfOuterwrapper').each(function (index) {
                        var id = $(this).prop('id');
                        var tableID = id + "_mytable";
                        var pchId = '#divPlaceHolder_' + tableID.toLowerCase();
                        var containA = $(pchId).text();
                        if (id !== "sfBodyContent") {
                            if (temp == 1) {
                                temp = 2;
                            }
                        }
                        else {
                            temp = 1;
                            $('.sfMainContent,.sfMiddletop,.sfMiddlebottom,.sfFulltopspan,.sfFullbottomspan').find('table').each(function () {
                                var pid = "#divPlaceHolder_" + $(this).prop('id');
                                containA += $(pid).text();
                            });
                        }
                        if (temp == 0) {
                            LayoutManager.config.sfHeader += containA;
                        }
                        if (temp == 1) {
                            LayoutManager.config.sfContent += containA;
                        }
                        if (temp == 2) {
                            LayoutManager.config.sfFooter += containA;
                        }
                    });
                    var fileName = $('#ddlLayoutList').val();
                    LayoutManager.RecreateLayout(fileName, LayoutManager.config.sfHeader.length > 0 ? LayoutManager.config.sfHeader : "", LayoutManager.config.sfContent.length > 0 ? LayoutManager.config.sfContent : "", LayoutManager.config.sfFooter.length > 0 ? LayoutManager.config.sfFooter : "");
                });
                $('#ddlAttr').bind("change", function () {
                    switch ($('#ddlAttr option:selected').val()) {
                        case 0:
                            break;
                        case "1":
                            if ($('#spnNameTooltip').length == 0) {
                                $('#tblAttr').append("<tr><td width='20%'><label class='sfFormlabel'>name=</label></td><td><input id='txtName' title='name' type='text' class='sfInputbox sfInputlm'/></td><td><span id='spnNameTooltip'></span></td></tr>");
                                SageFrame.tooltip.GetToolTip("imgToolTip5", "#spnNameTooltip", "The name of the placeholder");
                                LayoutManager.config.pchArr.push(" name='fixed'");
                            }
                            break;
                        case "2":
                            if ($('#spnModeTooltip').length == 0) {
                                $('#tblAttr').append("<tr><td width='20%'><label class='sfFormlabel'>mode=</label></td><td>'fixed'</td><td><span id='spnModeTooltip'></span></td></tr>");
                                SageFrame.tooltip.GetToolTip("imgToolTip", "#spnModeTooltip", "If Mode='fixed', the positions inside have fix width");
                                LayoutManager.config.pchArr.push(" mode='fixed'");
                            }
                            break;
                        case "3":
                            if ($('#spnWidthTooltip').length == 0) {
                                $('#tblAttr').append("<tr><td width='20%'><label class='sfFormlabel'>width=</label></td><td><input id='txtWidth' type='text' title='width' class='sfInputbox sfInputlm'/></td><td><span id='spnWidthTooltip'></span></td></tr>");
                                SageFrame.tooltip.GetToolTip("imgToolTip2", "#spnWidthTooltip", "Here we can specify the widths of positions<br/> E.g:width='20,80' ");
                                LayoutManager.config.pchArr.push(" width=''");
                            }
                            break;
                        case "4":
                            if ($('#spnWrapinTooltip').length == 0) {
                                $('#tblAttr').append("<tr><td><label class='sfFormlabel'>wrapinner=</label></td><td><input id='txtWrapinner' type='text' title='wrapinner' class='sfInputbox sfInputlm'/></td><td><span id='spnWrapinTooltip'></span></td></tr>");
                                SageFrame.tooltip.GetToolTip("imgToolTip3", "#spnWrapinTooltip", "It specifies the number of inner wrappers with the class sfInnerwrapper");
                                LayoutManager.config.pchArr.push(" wrapinner=''");
                            }
                            break;
                        case "5":
                            if ($('#spnWrapoutTooltip').length == 0) {
                                $('#tblAttr').append("<tr><td><label class='sfFormlabel'>wrapouter=</label></td><td><input id='txtWrapouter' type='text' title='wrapouter' class='sfInputbox sfInputlm'/></td><td><span id='spnWrapoutTooltip'></span></td></tr>");
                                SageFrame.tooltip.GetToolTip("imgToolTip4", "#spnWrapoutTooltip", "Specifies the number of outer wrappers with the class sfOuterwrapper");
                                LayoutManager.config.pchArr.push(" wrapouter=''");
                            }
                            break;
                    }
                    var count = LayoutManager.config.pchArr.length;
                    $('#pchPreview').html(LayoutManager.config.pchArr.join(' ') + '&gt;&lt;placeholder&gt;');
                });
                $('#txtName,#txtWidth,#txtWrapinner,#txtWrapouter').on("keyup", function () {

                    var self = $(this);
                    $.each(LayoutManager.config.pchArr, function (index, item) {
                        var controlKey = $(self).prop("title");
                        if (index > 0) {
                            var key = item.split('=')[0];
                            key = jQuery.trim(key);
                            controlKey = jQuery.trim(controlKey);
                            if (controlKey == key) {
                                LayoutManager.config.pchArr[index] = key + "=" + "'" + $(self).val() + "'";
                            }
                        }
                    });
                    var count = LayoutManager.config.pchArr.length;
                    $('#pchPreview').html(LayoutManager.config.pchArr.join(' ') + '&gt;&lt;placeholder&gt;');
                });
                $('#txtPlaceholder').on("keyup", function () {
                    $('#pchPreview').html(LayoutManager.config.pchArr.join(' ') + '&gt;' + $(this).val() + '&lt;placeholder&gt;');
                });
                //wrap markup creator
                $('#ddlWrapattr').bind("change", function () {
                    switch ($('#ddlWrapattr option:selected').val()) {
                        case 0:
                            break;
                        case "1":
                            if ($('#spnWrapNameTooltip').length == 0) {
                                $('#tblWrapAttr').append("<tr><td width='20%'><label class='sfFormlabel'>name=</label></td><td><input id='txtWrapName' title='name' type='text' class='sfInputbox'/></td><td><span id='spnWrapNameTooltip'></span></td></tr>");
                                SageFrame.tooltip.GetToolTip("imgToolTip6", "#spnWrapNameTooltip", "The name of the wrapper used");
                                LayoutManager.config.wrapArr.push(" name='mywrapper'");
                            }
                            break;
                        case "2":
                            if ($('#spnWrapTypeTooltip').length == 0) {
                                $('#tblWrapAttr').append("<tr><td width='20%'><label class='sfFormlabel'>type=</label></td><td><select title='type' id='selWraptype' class='sfListmenu'><option value='position'>position</option><option value='placeholder'>placeholder</option></select></td><td><span id='spnWrapTypeTooltip'></span></td></tr>");
                                SageFrame.tooltip.GetToolTip("imgToolTip7", "#spnWrapTypeTooltip", "The type of divs the wrapper wraps.");
                                LayoutManager.config.wrapArr.push(" type='position'");
                            }
                            break;
                        case "3":
                            if ($('#spnWrapClassTooltip').length == 0) {
                                $('#tblWrapAttr').append("<tr><td><label class='sfFormlabel'>class=</label></td><td><input id='txtWrapClass' type='text' title='class' class='sfInputbox'/></td><td><span id='spnWrapClassTooltip'></span></td></tr>");
                                SageFrame.tooltip.GetToolTip("imgToolTip8", "#spnWrapClassTooltip", "This is used to inject custom classes to the wrappers");
                                LayoutManager.config.wrapArr.push(" class=''");
                            }
                            break;
                        case "4":
                            if ($('#spnWrapDepthTooltip').length == 0) {
                                $('#tblWrapAttr').append("<tr><td width='20%'><label class='sfFormlabel'>depth=</label></td><td><input id='txtWrapDepth' type='text' title='depth' class='sfInputbox'/></td><td><span id='spnWrapDepthTooltip'></span></td></tr>");
                                SageFrame.tooltip.GetToolTip("imgToolTip8", "#spnWrapDepthTooltip", "The number of wrappers needed");
                                LayoutManager.config.wrapArr.push(" depth=''");
                            }
                            break;
                    }
                    var count = LayoutManager.config.wrapArr.length;
                    $('#wrapPreview').html(LayoutManager.config.wrapArr.join(' ') + '&gt;&lt;wrap&gt;');
                });
                $('#txtWrapName,#txtWrapClass,#txtWrapDepth').on("keyup", function () {
                    var self = $(this);
                    $.each(LayoutManager.config.wrapArr, function (index, item) {
                        var controlKey = $(self).prop("title");

                        if (index > 0) {
                            var key = item.split('=')[0];
                            key = jQuery.trim(key);
                            controlKey = jQuery.trim(controlKey);

                            if (controlKey == key) {
                                LayoutManager.config.wrapArr[index] = key + "=" + "'" + $(self).val() + "'";
                            }
                        }
                    });
                    var count = LayoutManager.config.wrapArr.length;
                    $('#wrapPreview').html(LayoutManager.config.wrapArr.join(' ') + '&gt;&lt;wrap&gt;');
                });
                $('#selWraptype').on("change", function () {
                    var self = $(this);
                    $.each(LayoutManager.config.wrapArr, function (index, item) {
                        var controlKey = $(self).prop("title");

                        if (index > 0) {
                            var key = item.split('=')[0];
                            key = jQuery.trim(key);
                            controlKey = jQuery.trim(controlKey);

                            if (controlKey == key) {
                                LayoutManager.config.wrapArr[index] = key + "=" + "'" + $(self).val() + "'";
                            }
                        }
                    });
                    var count = LayoutManager.config.wrapArr.length;
                    $('#wrapPreview').html(LayoutManager.config.wrapArr.join(' ') + '&gt;&lt;wrap&gt;');
                });
                $('#txtPositions').on("keyup", function () {
                    $('#wrapPreview').html(LayoutManager.config.wrapArr.join(' ') + '&gt;' + $(this).val() + '&lt;wrap&gt;');
                });
                $('div.sflayoutbuilderhead').bind("click", function () {
                    $(this).next("div").slideToggle();
                });
                //$('#divMsgTemplate').html(SageFrame.messaging.showdivmessage("Presets are a way of achieving multiple layout variations in your site"));
                $('div.sfPresetpages span.delete').on("click", function () {
                    $(this).parent("li").remove();
                });
                $('#activeLayoutList').on('change', 'select', function () {
                    $(this).parent().find("img.sfAddpage").remove();
                    if ($(this).val() == "2") {
                        if ($(this).parent().find(".customize").length == 0) {
                            $(this).parent().append('<span class="customize"><i class="icon-page-add"><i/></span>');
                            $(this).parent().find("div.sfPresetpages").remove();
                        }
                    }
                    else {
                        $(this).parent().find("div.sfPresetpages").remove();
                    }
                });
                $('#selTypes').on("change", function () {
                    switch ($(this).val()) {
                        case "0":
                            $('#tblPch').show();
                            $('#tblWrap').hide();
                            break;
                        case "1":
                            $('#tblWrap').show();
                            $('#tblPch').hide();
                            break;
                    }
                });
            },
            InitEqualHeights: function () {
                //                var middlewrapper = $('#sfMainWrapper').height();
                //                var leftwrapper = $('#sfLeft').height();
                //                var rightwrapper = $('#sfRight').height();
                //                var arrHeights = [middlewrapper, leftwrapper, rightwrapper];
                //                var biggest = Math.max.apply(null, arrHeights);
                //                biggest = biggest < 200 ? 200 : biggest;
                //                $('#sfMainWrapper .sfContainer,#sfLeft .sfContainer,#sfRight .sfContainer').css("height", biggest - 22 + "px");
                //                var lefttop = $('#sfLeft div.sfLeftTop').height() == null ? 0 : $('#sfLeft div.sfLeftTop').height() + 22 + 10;
                //                var leftbottom = $('#sfLeft div.sfLeftBottom').height() == null ? 0 : $('#sfLeft div.sfLeftBottom').height() + 22 + 10;
                //                var sfColsWrapLeft = biggest - (lefttop + leftbottom) - 40;
                //                $('#sfLeft div.sfColswrap').css("height", sfColsWrapLeft + "px");
                //                $('#sfLeft div.sfColswrap div.sfLeftA div.sfWrapper,#sfLeft div.sfColswrap div.sfLeftB div.sfWrapper').css("height", sfColsWrapLeft - 22 + "px");

                //                var righttop = $('#sfRight div.sfRightTop').height() == null ? 0 : $('#sfRight div.sfRightTop').height() + 22 + 10;
                //                var rightbottom = $('#sfRight div.sfRightBottom').height() == null ? 0 : $('#sfRight div.sfRightBottom').height() + 22 + 10;
                //                var sfColsWrapRight = biggest - (righttop + rightbottom) - 40;
                //                $('#sfRight div.sfColswrap').css("height", sfColsWrapRight + "px");
                //                $('#sfRight div.sfColswrap').css("height", sfColsWrapRight + "px");
                //                $('#sfRight div.sfColswrap div.sfRightA div.sfWrapper,#sfRight div.sfColswrap div.sfRightB div.sfWrapper').css("height", sfColsWrapRight - 22 + "px");

                //                
                //                var middletop = $('#sfMainWrapper div.sfMiddletop').height() == null ? 0 : $('#sfMainWrapper div.sfMiddletop').height();
                //                var middlebottom = $('#sfMainWrapper div.sfMiddlebottom ').height() == null ? 0 : $('#sfMainWrapper div.sfMiddlebottom ').height();
                //                var middlemaintop = $('#sfMainWrapper div.sfMiddlemaintop ').height() == null ? 0 : $('#sfMainWrapper div.sfMiddlemaintop ').height();
                //                var middlemainbottom = $('#sfMainWrapper div.sfMiddlemainbottom ').height() == null ? 0 : $('#sfMainWrapper div.sfMiddlemainbottom ').height();
                //                var sfColsWrapMiddle = biggest - (middletop + middlebottom) - 40;

                //                $('#sfMainWrapper div.sfMainContent').css("height", sfColsWrapMiddle + "px");

                //                //$('#sfMainWrapper div.sfMiddlemaincurrent').css("height", sfColsWrapMiddle-middlemaintop-middlemainbottom - 22 + "px");
            },
            ajaxSuccess: function (data) {
                switch (LayoutManager.config.ajaxCallMode) {
                    case 0:
                        editor.setValue(data.d);
                        break;
                    case 1:
                        LayoutManager.BindThemes(data);
                        break;
                    case 2:
                        LayoutManager.BindSetting(data);
                        break;
                    case 3:
                        LayoutManager.BindLayoutList(data);
                        break;
                    case 4:
                        LayoutManager.BindSectionList(data);
                        break;
                    case 5:
                        LayoutManager.BindBlockHTML(data);
                        break;
                    case 6:
                        LayoutManager.BindPages(data);
                        break;
                    case 7:
                        LayoutManager.BindLayoutList_Preset(data);
                        break;
                    case 8:
                        LayoutManager.BindThemeList_Preset(data);
                        break;
                    case 9:
                        LayoutManager.BindTemplates(data);
                        break;
                    case 10:
                        SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "TemplateManager", "TemplateActivated"), "Success");
                        LayoutManager.LoadTemplates();
                        break;
                    case 11:
                        LayoutManager.BindBasicSettings(data);
                        break;
                    case 12:
                        LayoutManager.BindLayoutList_Visual(data);
                        break;
                    case 13:
                        LayoutManager.BindLayoutWireFrame(data);
                        LayoutManager.SortPan();
                        break;
                    case 14:
                        LayoutManager.BindUpdatedLayout(data);
                        break;
                    case 15:
                        LayoutManager.BindFiles(data);
                        break;
                    case 16:
                        LayoutManager.BindFileToEditor(data);
                        break;
                    case 17:
                        LayoutManager.BindPresets(data);
                        break;
                    case 18:
                        SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "TemplateManager", "TemplateDeleted"), "Success");
                        LayoutManager.LoadTemplates();
                        break;
                    case 19:
                        LayoutManager.BindPreviewImages(data);
                        break;
                    case 20:
                        SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "TemplateManager", "ThemeDeleted"), "Success");
                        LayoutManager.BindThemes(data);
                        break;
                    case 21:
                        $('div.sfPresetmessage').slideUp();
                        SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "TemplateManager", "PresetDeleted"), "Success");
                        break;
                    case 22:
                        LayoutManager.BindLayoutList_Creator(data);
                        break;
                    case 23:
                        LayoutManager.BindLayout_Create(data);
                        break;
                    case 24:
                        LayoutManager.PostLayoutCreationActions(data);
                        break;
                    case 25:
                        LayoutManager.CheckDuplicateTemplate(data);
                        break;
                    case 26:
                        SageFrame.messaging.show("Template successfully created ", "Success");
                        LayoutManager.LoadTemplates();
                        break;
                    case 27:
                        LayoutManager.LoadLayoutWireFrame($('#ddlLayoutList option:selected').val());
                        SageFrame.messaging.show(" Layout saved successfully", "Success");
                        break;
                    case 28:
                        LayoutManager.LoadLayoutWireFrame($('#ddlLayoutList option:selected').val());
                        break;
                }
            },
            BindEvents: function () {
                $('#lnkThemes').bind("click", function () {
                    $('#lblSaveLayoutChange').hide();
                    $('div.sfNavbar ul li').removeClass("sfActive");
                    $(this).parent("li").addClass("sfActive");
                    $('#sectionsDiv,#layoutsDiv,#basicsDiv,#presetsDiv,div.cssClassLayoutBottom,#visualLayoutMgr').hide();
                    $('#themesDiv').show();
                    LayoutManager.ReadThemes();
                });
                $('#lnkLayoutMgr').bind("click", function () {
                    $('div.sfNavbar ul li').removeClass("sfActive");
                    $(this).parent("li").addClass("sfActive");
                    $('#sectionsDiv,#basicsDiv,#themesDiv,#presetsDiv,div.cssClassLayoutBottom,#visualLayoutMgr').hide();
                    $('#layoutsDiv').show();
                    LayoutManager.LoadLayoutList();
                });
                $('#lnkSectionMgr').bind("click", function () {
                    $('div.sfNavbar ul li').removeClass("sfActive");
                    $(this).parent("li").addClass("sfActive");
                    $('#basicsDiv,#themesDiv,#layoutsDiv,#presetsDiv,div.cssClassLayoutBottom,#visualLayoutMgr').hide();
                    $('#sectionsDiv').show();
                    LayoutManager.config.SettingMode = "Section";
                });
                $('#lnkBasicSettings').bind("click", function () {
                    $('#lblSaveLayoutChange').hide();
                    $('div.sfNavbar ul li').removeClass("sfActive");
                    $(this).parent("li").addClass("sfActive");
                    $('#sectionsDiv,#themesDiv,#layoutsDiv,div.cssClassLayoutBottom,#presetsDiv,#visualLayoutMgr').hide();
                    $('#basicsDiv').show();
                    var templateName = LayoutManager.config.Template
                    LayoutManager.LoadTemplate(templateName);
                });

                $('#lnkPresets').bind("click", function () {
                    $('#lblSaveLayoutChange').hide();
                    $('div.sfNavbar ul li').removeClass("sfActive");
                    $(this).parent("li").addClass("sfActive");
                    $('#basicsDiv,#sectionsDiv,#themesDiv,#layoutsDiv,div.cssClassLayoutBottom,#visualLayoutMgr').hide();
                    $('#presetsDiv').show();
                    LayoutManager.LoadPresets();
                });

                $('li.liDelete').on('click', function () {
                    var selDiv = $(this).prop("id").replace("del_", "");
                    $('.' + selDiv).removeClass(selDiv).addClass("remove");
                    html = '';
                    html += '<option>' + selDiv + '</option>';
                    html += '</ul>'
                    $('#ddlDeletelist').append(html);
                    var attrName = selDiv;
                    var FileName = $('#ddlLayoutList option:selected').val();
                    var FilePath = FileName;
                    RewriteWireFrame(FilePath, attrName);
                });
                $('#spApply').on('click', function () {
                    var FileName = $('#ddlLayoutList option:selected').val();
                    var FilePath = FileName;
                    setXml(FilePath);
                });
                $('#imgUndo').on('click', function () {
                    var restore = $('#ddlDeletelist option:selected').val();
                    $('#del_' + restore).parents('div .remove').prop('class', restore);
                    $('#ddlDeletelist option:selected').remove();
                    var FileName = $('#ddlLayoutList option:selected').val();
                    var FilePath = FileName;
                    var attrName = restore;
                    RemoveVisible(FilePath, attrName);
                });
                $('#btnCreateTemplate').click(function () {
                    $('#sfCreateTemplate').show();
                    $('#txtNewTemplate').val('');
                });
                $('#btnSaveTemplate').click(function () {
                    var TemplateName = $('#txtNewTemplate').val();
                    if (TemplateName == "") {
                        SageFrame.messaging.show("Enter Template Name ", "Alert");
                        return false;
                    }
                    else {
                        LayoutManager.CheckExistingTemplate();
                        LayoutManager.InitVisibility();
                    }
                });
                function RemoveVisible(filepath, attrName) {
                    $.ajax({
                        type: "POST",
                        url: '/Modules/LayoutManager/WebMethod.aspx/RemoveVisible',
                        data: JSON.stringify({ FilePath: filepath, attrName: attrName }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $('#divLayoutWireframe').html(msg.d);
                        },
                        error: function () {
                            alert("fail remove visible");
                        }
                    });
                }
                function setXml(filepath) {
                    $.ajax({
                        type: "POST",
                        url: '/Modules/LayoutManager/WebMethod.aspx/setXml',
                        data: JSON.stringify({ FilePath: filepath }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $('#divLayoutWireframe').html(msg.d);
                        },
                        error: function () {
                            alert("fail remove setXml");
                        }
                    });
                }
                function RewriteWireFrame(filepath, attrName) {
                    $.ajax({
                        type: "POST",
                        url: '/Modules/LayoutManager/WebMethod.aspx/RewriteWireFrame',
                        data: JSON.stringify({ FilePath: filepath, attrName: attrName }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $('#divLayoutWireframe').html(msg.d);
                        },
                        error: function () {
                            alert("fail rewrite");
                        }
                    });
                }

                // Add PCH
                $('#imgAddPlaceHolder').toggle(function () {
                    $('#tblPch').slideToggle("slow");
                    $('#txtLayoutEditor').removeClass("layoutEditor").addClass("layoutEditorWithPch");
                    $('#imgAddPlaceHolder').prop("src", "/Modules/LayoutManager/images/allow_list_close.jpg");
                }, function () {
                    $('#txtLayoutEditor').removeClass("layoutEditorWithPch").addClass("layoutEditor");
                    $('#tblPch').hide();
                    $('#imgAddPlaceHolder').prop("src", "/Modules/LayoutManager/images/text_code_add.png");
                });
                $('#pchAdd').on('click', function () {
                    $(editor).insertAtPch($('#pchPreview').html());
                });

                // themes
                $("#layoutList table tr img.edit").on("click", function () {
                    var fileName = $(this).prev("input[type='hidden']").val();
                    LayoutManager.ReadLayout(fileName);
                });
                $("#sectionList table tr img.edit").on("click", function () {
                    var fileName = $(this).prev("input[type='hidden']").val();
                    LayoutManager.ReadBlockHTML(fileName);
                });
                $('#layoutsDiv div.controls ul li.Add').bind("click", function () {
                    LayoutManager.ShowPopUp("divAddLayout", "Create a Layout");
                });
                $('#chkEnableHandheld_preset').bind("click", function () {
                    if ($(this).prop("checked")) {
                        $('#trHandheld').show();
                    }
                    else {
                        $('#trHandheld').hide();
                    }
                });
                $('#rbPresetCustom').bind("click", function () {
                    LayoutManager.ShowPopUp("divPagePresets", "Select Pages");
                    LayoutManager.GetPages();
                });
                $('span.editLayout').bind("click", function (e) {
                    if (!$(this).hasClass("cssClassActive")) {
                        $('span.editLayout,span.editTheme,span.editWidth').removeClass("cssClassActive");
                        $(this).addClass("cssClassActive");
                        $('#activeLayoutList').show();
                        LayoutManager.LoadLayoutList_Preset();
                    }
                    else {
                        $(this).removeClass("cssClassActive");
                    }
                });
                $(document).scroll(function () {
                    $('#dropEditLayout').hide();
                });
                $(document).click(function () {
                    $('#dropEditLayout').hide();
                });
                $('#dropEditLayout,span.editLayout,span.editTheme,span.editWidth').click(function (event) {
                    event.stopPropagation();
                });
                $('#pageList').on('click', '#pageTree li span', function () {

                    if (!$(this).hasClass("sfActivepage") && !$(this).hasClass("sfAssigned")) {
                        $(this).addClass("sfActivepage");
                    }
                    else {
                        $(this).removeClass("sfActivepage");
                    }
                });
                $('#spnSavePagePreset').bind("click", function () {
                    var pages = $('#pageTree li span.sfActivepage');
                    var html = '<div class="sfPresetpages"><ul>';
                    $.each(pages, function (index, item) {
                        html += '<li class="sfCurve"><span class="sfPageName">' + $.trim($(this).text().replace(/-/g, " ")).replace(/ /g, '-') + '</span><span class="sfDelete"><i class="icon-delete"></i></span></li>';
                    });
                    html += '</ul></div>';
                    var activeElement = LayoutManager.config.ActivePageDiv;
                    $(activeElement).parent().append(html);
                    $('#divPagePresets').dialog("close");
                });
                $('.sfFormwrapper').on('click', '.customize', function () {
                    LayoutManager.config.ActivePageDiv = $(this).prev("select");
                    LayoutManager.ShowPopUp("divPagePresets", "Select Pages");
                    LayoutManager.GetPages();
                });
                $('#presetPages li span').on('click', '.delete', function () {

                    $(this).parent("li").remove();
                });

                $('div.sfButtonwrapper').on('click', 'ul li.preview', function () {

                    var template = $(this).find("a").prop("id");
                    template = template.substring(template.indexOf('#') + 1, template.length);
                    LayoutManager.config.Template = template;
                    LayoutManager.ShowPopUp("divTemplatePreview", "Preview");
                    LayoutManager.GetPreviewImages(template);
                });
                $('a').on('click', '.sfTemplatethumb', function () {

                    var template = $(this).prop("rel");
                    LayoutManager.config.Template = template;
                    LayoutManager.ShowPopUp("divTemplatePreview", "Preview");
                    LayoutManager.GetPreviewImages(template);
                });
                $('div.cssClassPreview span.return').bind("click", function () {
                    $('div.sfLayoutmanager').hide();
                    $('div.sfTemplatemanger').show();
                    LayoutManager.LoadTemplates();
                });
                $('#templateList').on('click', '.sfTemplatedetail', function () {
                    $('div.sfLayoutmanager').show();
                    $('div.sfTemplatemanger').hide();
                    $('#lnkBasicSettings').parent("li").addClass("cssClassActive");
                    var templateName = $(this).find("li.title").text();
                    $('#tabLayoutMgr ul li').removeClass('ui-tabs-selected ui-state-active');
                    $('#tabLayoutMgr ul li:first').addClass('ui-tabs-selected ui-state-active');
                    LayoutManager.InitVisibility();
                    LayoutManager.config.Template = templateName;
                    LayoutManager.LoadTemplate(templateName);
                });

                $('#templateList').on('click', '.templatePreset', function () {
                    $('div.sfLayoutmanager').show();
                    $('div.sfTemplatemanger').hide();
                    $('#lnkBasicSettings').parent("li").addClass("cssClassActive");
                    var templateName = $(this).attr('data');
                    $('#tabLayoutMgr ul li').removeClass('ui-tabs-selected ui-state-active');
                    $('#tabLayoutMgr ul li').eq(1).addClass('ui-tabs-selected ui-state-active');
                    LayoutManager.InitVisibility();
                    LayoutManager.config.Template = templateName;
                    LayoutManager.LoadTemplate(templateName);
                    $('#tabLayoutMgr ul').find('li a').eq(1).trigger('click');
                });

                $('#templateList').on('click', '.templateLayout', function () {
                    $('div.sfLayoutmanager').show();
                    $('div.sfTemplatemanger').hide();
                    $('#lnkBasicSettings').parent("li").addClass("cssClassActive");
                    var templateName = $(this).attr('data');
                    $('#tabLayoutMgr ul li').removeClass('ui-tabs-selected ui-state-active');
                    $('#tabLayoutMgr ul li').eq(2).addClass('ui-tabs-selected ui-state-active');
                    LayoutManager.InitVisibility();
                    LayoutManager.config.Template = templateName;
                    LayoutManager.LoadTemplate(templateName);
                    $('#tabLayoutMgr ul').find('li a').eq(2).trigger('click');
                });

                $('#lnkVisualLayoutMgr').bind("click", function () {
                    $('#lblSaveLayoutChange').hide();
                    $('div.cssClassNavBar ul li').removeClass("cssClassActive");
                    $(this).parent("li").addClass("cssClassActive");
                    $('#basicsDiv,#sectionsDiv,#themesDiv,#layoutsDiv,div.cssClassLayoutBottom,#presetsDiv').hide();
                    $('#visualLayoutMgr').show();
                    LayoutManager.LoadLayoutList_Visual();
                });

                $('#ddlLayoutList').on('change', function () {
                    var layout = $('#ddlLayoutList option:selected').val();
                    //  if (layout == "Core" || layout == "UnderConstruction") {
                    if (layout == "Core") {
                        $('#imgEditLayout_Visual,#btnDeleteLayout').hide();
                        $('#lblSaveLayoutChange').hide();
                    }
                    else {
                        $('#imgEditLayout_Visual,#btnDeleteLayout').show();
                        $('#lblSaveLayoutChange').show();
                    }
                    LayoutManager.LoadLayoutWireFrame(layout);
                });
                $('#imgEditLayout_Visual').bind("click", function () {

                    var fileName = $('#ddlLayoutList option:selected').val();
                    $('#hdnLayoutName').val(fileName);
                    var dialogOptions = {
                        "title": $(this).parent().prev().find("option:selected").val(),
                        "minWidth": 650,
                        "minHeight": 500,
                        "modal": true,
                        "position": [400, 200],
                        "dialogClass": "sfFormwrapper"
                    };
                    if ($("#button-cancel").prop("checked")) {

                        dialogOptions["buttons"] = {
                            "Cancel": function () {
                                //;
                                $(this).dialog("close");
                            }
                        };
                    }
                    //dialog-extend options
                    var dialogExtendOptions = {
                        "maximize": true
                    };
                    //open dialog
                    $("#divEditXML").dialog(dialogOptions).dialogExtend(dialogExtendOptions);
                    //$('div.ui-dialog').css("z-index", "3000");
                    LayoutManager.ReadLayout(fileName);
                });
                $('#imgAddLayout').bind("click", function () {
                    var empty = '';
                    LayoutManager.ShowPopUp("divAddLayout", "Add Layout");
                    LayoutManager.LoadLayoutList_Creator();
                    editorCreate.setValue(empty);
                    $('#txtNewLayoutName').val('');
                });
                $('#ddlClonebleLayouts').bind("change", function () {
                    var fileName = $('#ddlClonebleLayouts option:selected').val();
                    LayoutManager.ReadLayout_Create(fileName);
                });
                $('#btnCreateLayout').bind("click", function () {
                    var fileName = SageFrame.utils.GetPageSEOName($('#txtNewLayoutName').val());
                    var hasSpace = $('#txtNewLayoutName').val().indexOf(' ');
                    var regex = /^[A-Za-z]+$/;
                    var templateName = $('#txtNewLayoutName').val();
                    if (hasSpace >= 0) {
                        SageFrame.messaging.alert("Space is not valid for layout name.", '#msgAddLayout');
                    }
                    else {
                        if ($.trim(fileName) == '' || fileName.length == 0) {
                            SageFrame.messaging.alert("Required Field cannot be blank", '#msgAddLayout');
                        }
                        else if (SageFrame.utils.IsNumber(SageFrame.utils.GetFileNameWithoutExtension(fileName))) {
                            SageFrame.messaging.alert("Layout name can contains only alphabets", '#msgAddLayout');
                        }
                        else if (editorCreate.getValue() == "") {
                            SageFrame.messaging.alert("Layout cannot be empty", '#msgAddLayout');
                        }
                        else if (!regex.test(fileName)) {
                            SageFrame.messaging.alert("Layout name can contains only alphabets", '#msgAddLayout');
                        }
                        else if ($("select[id$='ddlClonebleLayouts'] option:contains('" + templateName + "')").length > 0) {
                            SageFrame.messaging.alert("Layout with same name already exists.Please choose different name.", '#msgAddLayout');
                        }
                        else {

                            var xml = editorCreate.getValue();
                            fileName = SageFrame.utils.GetPageSEOName(fileName);
                            LayoutManager.CreateLayout(fileName, xml);
                        }
                    }
                });
                $('#SaveLayout_Edit').bind("click", function () {

                    var fileName = $('#hdnLayoutName').val();
                    var xml = editor.getValue();
                    LayoutManager.UpdateLayout(fileName, xml);
                });
                $('#lblSaveLayout').on("click", function () {
                    var fileName = $('#ddlLayoutList').val();
                    var newValue = $('#divPlaceHolder').text();
                    var template = LayoutManager.config.Template;
                    LayoutManager.RecreateLayout('Core\\Template\\layouts\\' + fileName + '.xml', newValue);
                });
                $('#divFileList ul li a').on("click", function () {
                    var fileName = $(this).text();
                    if (LayoutManager.IsFile(fileName)) {
                        LayoutManager.config.NavMode = "File";
                        if (!LayoutManager.IsImage(fileName)) {
                            $('div.cssClassFileMgr').hide();
                            $('#divInlineEditor').show();
                            $('#btnSaveFile').show();
                            LayoutManager.config.FileName = fileName;
                            LayoutManager.ReadFile(LayoutManager.config.Template, LayoutManager.GetFolderPath($('#divFileMgrBreadCrumb span').last().text()) + "/" + fileName);
                        }
                    }
                    else {
                        LayoutManager.BuildBreadCrumb($(this).text(), 0);
                        var path = LayoutManager.GetFolderPath($(this).text());
                        LayoutManager.LoadFiles(LayoutManager.config.Template, path);
                    }
                });
                $('#divFileMgrBreadCrumb span').on("click", function () {
                    LayoutManager.ResetInlineEditor();
                    var breadCrumb = $('#divFileMgrBreadCrumb span');
                    var path = "";
                    var self = $(this).text();
                    var keepChecking = true;
                    var newBreadCrumb = "";
                    $.each(breadCrumb, function (index, item) {
                        if (keepChecking) {
                            if (self != $(this).text()) {
                                if (index > 0) {
                                    path += $(this).text();
                                    path += "/";
                                }
                                newBreadCrumb += '<span>' + $(this).text() + '</span>';
                                newBreadCrumb += ">>";
                            }
                            else {
                                path += $(this).text();
                                newBreadCrumb += '<span>' + $(this).text() + '</span>';
                                keepChecking = false;
                            }
                        }
                    });
                    path = LayoutManager.config.Template === path ? "" : path;
                    LayoutManager.BuildBreadCrumb(newBreadCrumb, 1);
                    LayoutManager.LoadFiles(LayoutManager.config.Template, path);
                });
                $('div.navBack').bind("click", function () {
                    switch (LayoutManager.config.NavMode) {
                        case "Folder":
                            var length = $('#divFileMgrBreadCrumb span').length;
                            length = length - 1;
                            var selected = $('#divFileMgrBreadCrumb span:nth-child(' + length + ')').text();
                            var path = LayoutManager.GetFolderPath(selected);
                            LayoutManager.LoadFiles(LayoutManager.config.Template, path);
                            LayoutManager.BuildNewBreadCrumb(selected);
                            break;
                        case "File":
                            var selected = $('#divFileMgrBreadCrumb span').last().text();
                            if (LayoutManager.config.Template === selected) {
                                LayoutManager.LoadFiles(LayoutManager.config.Template, "");
                            }
                            else {
                                var path = LayoutManager.GetFolderPath(selected);
                                LayoutManager.LoadFiles(LayoutManager.config.Template, path);
                            }
                            LayoutManager.ResetInlineEditor();
                            break;
                    }
                });
                $('#btnSavePreset').bind("click", function () {
                    LayoutManager.SavePreset();
                    $('div.sfPresetmessage').slideUp();
                });
                $('#btnCreatePreset').on("click", function () {
                    var html = "";
                    html += '<input type="text" id="txtNewPreset" title="Preset Name Goes Here" class="sfInputbox"/>';
                    html += '<img id="imbCreatePreset" src=' + SageFrame.utils.GetAdminImage("imgapprove.png") + '>';
                    html += '<img id="imbCancelPreset" src=' + SageFrame.utils.GetAdminImage("btndelete.png") + '>';
                    $('#liCreatePreset').html(html);
                });
                $('#activeWidthList ul li').on("click", function () {
                    $('#spnActiveWidth').text($(this).text());
                    $('#activeWidthList ul li').removeClass('sfActive');
                    $(this).addClass('sfActive');
                });
                $('#presetsDiv').on('click', '#activeThemeList ul li', function () {
                    $('#spnActiveTheme').text($(this).text());
                    $('#activeThemeList ul li').removeClass('sfActive');
                    $(this).addClass('sfActive');
                });
                $('#btnSaveFile').bind("click", function () {
                    var path = LayoutManager.GetFolderPath(LayoutManager.config.FileName);
                    var filepath = path + LayoutManager.config.FileName;
                    LayoutManager.SaveFileInEditor(filepath);
                });
                $('#lblCancelEditMode').bind("click", function () {
                    $('div.sfLayoutmanager').hide();
                    $('div.sfTemplatemanger').show();
                });
                $('.sfFormwrapper').on('click', 'div.sfPresetpages span.sfDelete', function () {
                    if ($(this).parent().parent("ul").find("li").length == 1) {
                        $(this).parents("div.sfPresetpages").prev("span").remove();
                        $(this).parents("div.sfPresetpages").prev("select").val("0");
                    }
                    $(this).parent("li").remove();
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
                    async: false,
                    data: param,
                    dataType: this.config.dataType,
                    success: function (msg) { data = msg.d; },
                    error: this.ajaxFailure
                });
                return data;
            },
            SaveFileInEditor: function (filePath) {
                var saveddata = this.ajaxCall_return(this.config.baseURL + "UpdateFile", JSON2.stringify({ TemplateName: LayoutManager.config.Template, FileName: filePath }));
            },
            ShowPopUp: function (popupid, headertext) {
                $('#msgDiv').html('');
                $('#msgAddLayout').html('');
                var options = {
                    modal: true,
                    title: headertext,
                    height: 550,
                    width: 650,
                    dialogClass: "sfFormwrapper"
                };
                dlg = $('#' + popupid).dialog(options);
                dlg.parent().appendTo($('form:first'));
            },
            DropPopUp: function (popupid, e) {
                //getting height and width of the message box
                var height = $(popupid).height();
                var width = $(popupid).width();
                //calculating offset for displaying popup message
                leftVal = e.pageX - width + "px";
                topVal = e.pageY + "px";
                //show the popup message and hide with fading effect                  
                $(popupid).css({ top: topVal }).show();
            },
            HidePopUp: function () {
                $('#divEditXML,#divAddLayout,#divPagePresets').dialog().dialog("close");
            },
            InitVisibility: function () {
                $('#sectionsDiv,#layoutsDiv,#themesDiv').hide();
                $('div.cssClassLayoutBottom,#presetsDiv,#trHandheld').hide();
                $('#basicsDiv').show();
                $('#sfCreateTemplate').hide();
            },
            ReadSettings: function () {
                this.config.method = "ReadSettings";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = '{}';
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            },
            ReadLayout: function (fileName) {
                this.config.method = "ReadXML";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ filePath: fileName, TemplateName: LayoutManager.config.Template });
                this.config.ajaxCallMode = 0;
                this.ajaxCall(this.config);
            },
            ResetCore: function () {
                this.config.method = "ResetCore";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ FilePath: $('#ddlLayoutList').val(), TemplateName: LayoutManager.config.Template });
                this.config.ajaxCallMode = 28;
                this.ajaxCall(this.config);
            },
            LoadLayoutList: function () {
                this.config.method = "LoadLayout";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ TemplateName: LayoutManager.config.Template });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            },
            BindLayoutList: function (data) {
                var layouts = data.d;
                var html = '<table><tr ><td >S.N</td><td>Layout</td><td>Activate</td><td>Edit</td><td>Delete</td></tr>';
                $.each(layouts, function (index, item) {
                    var sn = parseInt(index) + 1;
                    var flag = item.Key === LayoutManager.config.ActiveLayout + ".xml" ? LayoutManager.config.ActiveFlag : LayoutManager.config.InActiveFlag;
                    html += '<tr><td>' + sn + '</td><td>' + item.Key + '</td><td><img src="' + flag + '"/></td><td><input type="hidden" value=' + item.Key + '><img class="edit" src="' + LayoutManager.config.EditButton + '"/></td><td><img src="' + LayoutManager.config.DeleteButton + '"/></td></tr>';
                });
                html += '</table>';
                $('#layoutList').html(html);
            },
            LoadLayoutList_Preset: function () {
                this.config.method = "LoadLayout";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ TemplateName: LayoutManager.config.Template });
                this.config.ajaxCallMode = 7;
                this.ajaxCall(this.config);
            },
            BindLayoutList_Preset: function (data) {
                var layouts = data.d;
                var html = '<ul>';
                $.each(layouts, function (index, item) {
                    var sn = parseInt(index) + 1;
                    var oddEven = "clearfix";
                    html += ' <li class=' + oddEven + '><label class="sfLayout">' + item.Key + '</label>';
                    html += '<select class="sfListmenu"><option value="0">None</option><option value="1">All</option><option value="2">Custom</option></select></li>';
                });
                html += '</table><div class="cssClassClear"></div>';
                $('#activeLayoutList').html(html);
                LayoutManager.LoadThemeList_Preset();
            },
            CreateTemplate: function () {
                var _folderName = SageFrame.utils.GetPageSEOName($('#txtNewTemplate').val());
                this.config.method = "CreateTemplate";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ filepath: LayoutManager.config.AppPath, FolderName: _folderName });
                this.config.ajaxCallMode = 26;
                this.ajaxCall(this.config);
            },
            CheckExistingTemplate: function () {
                var _folderName = $('#txtNewTemplate').val();
                this.config.method = "CheckExistingTemplate";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ filepath: LayoutManager.config.AppPath, FolderName: _folderName });
                this.config.ajaxCallMode = 25;
                this.ajaxCall(this.config);
            },
            CheckDuplicateTemplate: function (data) {
                var item = data.d;
                var NewTemplate = $('#txtNewTemplate').val().toLowerCase();
                if (NewTemplate == item.Existfolder) {
                    SageFrame.messaging.show("Template already exist, create new template ", "Alert");
                    return false;
                }
                else if (NewTemplate == "default") {
                    SageFrame.messaging.show("Access Denied, Can't Create template name \"Default\" ", "Alert");
                }
                else {
                    this.CreateTemplate();
                }
            },
            //            LoadSectionList: function() {
            //                this.config.method = "LoadBlockTypes";
            //                this.config.url = this.config.baseURL + this.config.method;
            //                this.config.data = '{}';
            //                this.config.ajaxCallMode = 4;
            //                this.ajaxCall(this.config);
            //            },
            BindSectionList: function (data) {
                var blocks = data.d;
                var html = '<table><tr ><td >S.N</td><td>Section</td><td>Edit</td><td>Delete</td></tr>';
                $.each(blocks, function (index, item) {
                    var sn = parseInt(index) + 1;
                    html += '<tr><td>' + sn + '</td><td>' + item.SectionName + '</td><td><input type="hidden" value=' + item.SectionName + '><img class="edit" src="' + LayoutManager.config.EditButton + '"/></td><td><img src="' + LayoutManager.config.DeleteButton + '"/></td></tr>';
                });
                html += '</table>';
                $('#sectionList').html(html);
                LayoutManager.InitPaging();
            },
            BindLayout: function (data) {
                var xml = data.d;
                $('#txtLayoutEditor').text(xml);
            },
            ReadThemes: function () {
                this.config.method = "GetThemes";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ TemplateName: LayoutManager.config.Template });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },
            BindThemes: function (data) {
                var themes = data.d;
                var html = '<table width="100%" cellspacing="0" cellpadding="0"><tr ><th class="sfIndex">S.N</th><th>Theme</th><th class="sfDelete">Delete</th></tr>';
                if (data.d.length < 1) {
                    html = SageFrame.messaging.showdivmessage("No themes available");
                }
                $.each(themes, function (index, item) {
                    var sn = parseInt(index) + 1;
                    var id = 'del_' + item.Key;
                    var flag = item.Key === LayoutManager.config.ActiveTheme ? LayoutManager.config.ActiveFlag : LayoutManager.config.InActiveFlag;
                    var oddeven = index % 2 == 0 ? "sfEven" : "sfOdd";
                    html += '<tr class=' + oddeven + '><td>' + sn + '</td><td>' + item.Key + '</td><td><img id=' + id + ' class="sfDelete" src="' + LayoutManager.config.DeleteButton + '"/></td></tr>';
                });
                html += '</table>';
                $('#themeList').html(html);
                //$('#themeList img.sfDelete').easyconfirm({ locale: { title: 'Select Yes or No', text: 'Are you sure you want to delete this theme?', button: ['No', 'Yes']} });

                $('#themeList img.sfDelete').click(function () {
                    var me = $(this);
                    jConfirm('Are you sure you want to delete this layout?', 'Confirmation Dialog', function (r) {
                        if (r) {
                            var dName = me.prop("id").replace("del_", "");
                            var TemplateName = LayoutManager.config.Template;
                            LayoutManager.DeleteTheme(TemplateName, dName);
                            LayoutManager.config.SettingMode = "Theme";
                        }
                    });
                });
            },
            LoadThemeList_Preset: function () {
                this.config.method = "GetThemes";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({
                    TemplateName: LayoutManager.config.Template
                });
                this.config.ajaxCallMode = 8;
                this.ajaxCall(this.config);
            },
            BindThemeList_Preset: function (data) {
                var themes = data.d;
                var html = '<ul>';
                html += '<li>default</li>';
                $.each(themes, function (index, item) {
                    var sn = parseInt(index) + 1;
                    html += '<li class="sfCurve">' + item.Key + '</li>';
                });
                html += '</ul>';
                $('#activeThemeList').html(html);
                LayoutManager.BindPresetDetails();
            },
            ReadBlockHTML: function (fileName) {
                this.config.method = "ReadBlockHTML";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ fileName: "/Modules/LayoutManager/Sections/" + fileName });
                this.config.ajaxCallMode = 5;
                this.ajaxCall(this.config);
            },
            BindBlockHTML: function (data) {
                var html = data.d;
                $('#txtHTMLEditor').text(html);
                LayoutManager.ShowPopUp("divEditHTML");
            },
            InitPaging: function () {
                $('#paging_container3').pajinate({
                    items_per_page: 5,
                    item_container_id: '#sectionList table tbody',
                    nav_panel_id: '.alt_page_navigation'
                });
            },
            GetPages: function () {
                this.config.method = "GetNormalPage";
                this.config.url = LayoutManager.config.AppPath + '/Modules/Admin/MenuManager/MenuWebService.asmx/' + this.config.method;
                this.config.data = JSON2.stringify({ PortalID: p.PortalID, UserName: SageFrameUserName, CultureCode: 'en-US', secureToken: SageFrameSecureToken, userModuleID: p.UserModuleID });
                this.config.ajaxCallMode = 6;
                this.ajaxCall(this.config);
            },
            BindPages: function (data) {
                var pages = data.d;
                var PageID = "";
                var parentID = "";
                var PageLevel = 0;
                var itemPath = "";
                var html = "";
                html += '<ul id="pageTree">';
                $.each(pages, function (index, item) {
                    PageID = item.PageID;
                    parentID = item.ParentID;
                    categoryLevel = item.Level;
                    if (item.Level == 0) {
                        html += '<li id=' + PageID + '>';
                        html += LayoutManager.ContainsPage(item.PageName) ? '<span class="sfAssigned">' + item.PageName + '</span>' : '<span class="page parent">' + item.PageName + '</span>';
                        if (item.ChildCount > 0) {
                            html += "<ul>";
                            itemPath += item.PageName;
                            html += LayoutManager.BindChildCategory(pages, PageID);
                            html += "</ul>";
                        }
                        html += "</li>";
                    }
                    itemPath = '';
                });
                html += '</ul>';
                $('#pageList').html(html);
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
                            var prefix = LayoutManager.GetPrefixes(item.Level);
                            strListmaker += '<li id=' + PageID + '>';
                            strListmaker += LayoutManager.ContainsPage(item.PageName) ? '<span class="page sfAssigned">' + prefix + item.PageName + '</span>' : '<span class="page">' + prefix + item.PageName + '</span>';
                            childNodes = LayoutManager.BindChildCategory(response, item.PageID);
                            itemPath = itemPath.replace(itemPath.lastIndexOf(item.AttributeValue), '');
                            if (childNodes != '') {
                                strListmaker += "<ul>" + childNodes + "</ul>";
                            }
                            strListmaker += "</li>";
                        }
                    }
                });
                return strListmaker;
            },
            ContainsPage: function (pagename) {
                var addedPages = $('div.sfPresetpages ul li');
                var status = false;
                $.each(addedPages, function () {
                    if ($(this).text().toLowerCase() == SageFrame.utils.GetPageSEOName(pagename.toLowerCase())) {
                        status = true;
                    }
                });
                return status;
            },
            GetPrefixes: function (level) {
                var prefix = "";
                for (var i = 0; i < level; i++) {
                    prefix += "---";
                }
                return prefix;
            },
            LoadTemplates: function () {
                this.config.method = "GetTemplateList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ PortalID: LayoutManager.config.PortalID });
                this.config.ajaxCallMode = 9;
                this.ajaxCall(this.config);
            },
            BindTemplates: function (data) {
                var template = data.d;
                var html = '';
                $.each(template, function (index, item) {
                    var tempMarkup = "";
                    var isActiveClass = item.IsActive ? 'class="sfTemplateholder sfCurve sfActivetemplate"' : 'class="sfTemplateholder  sfCurve"';
                    tempMarkup += ' <div ' + isActiveClass + '>';
                    tempMarkup += '<a href="#" rel="' + item.TemplateName + '" class="sfTemplatethumb"> <img alt="Default" src="' + item.ThumbImage + '"> </a>';
                    tempMarkup += '<div class="sfTemplatedetail">';
                    tempMarkup += '<ul><li class="title"><span>' + item.TemplateName + '</span> </li> <li class="author"><span>By:<a href="#">SageFrame</a></span> </li>';
                    tempMarkup += ' </ul></div>';
                    var activateId = 'lnkActivate#' + item.TemplateSeoName;
                    var previewId = 'lnkPreview#' + item.TemplateSeoName;

                    if (!item.IsDefault) {
                        if (!item.IsActive)
                            tempMarkup += '<i class="icon-close sfDelete" href="#" rel=' + item.TemplateSeoName + '></i>';
                    }

                    var isActivated = 'Activate';
                    if (item.IsActive) {
                        isActivated = 'Activated';
                    }
                    tempMarkup += '<div class="sfButtonwrapper">';
                    tempMarkup += '<ul class="sfTemplateSetting"><li class="sfViewDemo"><a href="#" >View Demo</a></li><li class="activate"><a href="#" id=' + activateId + '>' + isActivated + '</a></li>';
                    tempMarkup += ' <li class="sfTemplateCustomize">Customize <i class="icon-arrow-slim-e"></i><ul  class="sfTemplateEdit">';
                    tempMarkup += '<li class="sfPages"><a href="' + SageFrameHostURL + '/Admin/Pages' + p.Extension + '" target="_blank" >Pages</a></li>';
                    tempMarkup += '<li class="templatePreset" data="' + item.TemplateName + '">Preset</li><li class="templateLayout" data="' + item.TemplateName + '">Layout Manager</li></ul></li>';
                    var editFileLink = p.EditFilePath + '/Admin/Template-File-Editor' + p.Extension + '?tname=' + item.TemplateSeoName;
                    // if (!item.IsDefault) {
                    tempMarkup += '<li class="sfEditfiles"><a href=' + editFileLink + ' id="lnkEditFiles">Edit Files</a></li>';
                    //                        if (!item.IsActive)
                    //                            tempMarkup += ' <li class="sfDelete"><a href="#" id="lnkDelete" rel=' + item.TemplateSeoName + '>Delete</a></li>';
                    //}
                    tempMarkup += '</ul></div><div class="clear"></div></div>';
                    //  if (SageFramePortalID == 1) {
                    //     html += tempMarkup;
                    //}
                    //else {
                    if (!item.IsApplied) {
                        html += tempMarkup;
                    }

                    //}
                });
                $('#templateList').html(html);
                //$('div.sfButtonwrapper li.sfDelete').easyconfirm({ locale: { title: 'Select Yes or No', text: 'Are you sure you want to delete this template?', button: ['No', 'Yes']} });

                $('div.sfButtonwrapper').on('click', 'ul li.activate', function () {
                    var template = $(this).find("a").prop("id");
                    template = template.substring(template.indexOf('#') + 1, template.length);
                    LayoutManager.ActivateTemplate(template);
                });
                $('div.sfTemplateholder  ').on('click', 'i.sfDelete', function () {
                    var me = $(this);
                    jConfirm('Are you sure you want to delete this layout?', 'Confirmation Dialog', function (r) {
                        if (r) {
                            var template = me.parent().find("a.sfTemplatethumb").prop("rel");
                            LayoutManager.DeleteTemplate(template);
                        }
                    });
                });
            },
            ActivateTemplate: function (templateName) {
                this.config.method = "ActivateTemplate";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ TemplateName: templateName, PortalID: LayoutManager.config.PortalID });
                this.config.ajaxCallMode = 10;
                this.ajaxCall(this.config);
            },
            LoadTemplate: function (templateName) {
                this.config.method = "GetBasicSettings";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ TemplateName: templateName });
                this.config.ajaxCallMode = 11;
                this.ajaxCall(this.config);
            },
            BindBasicSettings: function (data) {
                var basics = data.d;
                $('#spnTemplateName').text(basics.TemplateName);
                $('#spnAuthor').text(basics.Author);
                $('#spnDescription').text(basics.Description);
                $('#spnWebsite').text(basics.Website);
            },
            LoadLayoutList_Visual: function () {
                this.config.method = "LoadLayout";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ TemplateName: LayoutManager.config.Template });
                this.config.ajaxCallMode = 12;
                this.ajaxCall(this.config);
            },
            BindLayoutList_Visual: function (data) {

                var layouts = data.d;
                var html = '';
                if (layouts != null && typeof (layouts) != "undefined" && layouts.length > 0) {
                    $.each(layouts, function (index, item) {
                        var sn = parseInt(index) + 1;
                        if (item != null && typeof (item.Key) != "undefined") {
                            html += '<option>' + item.Key + '</option>';
                        }
                    });
                    html += '</ul>';
                    $('#ddlLayoutList').html(html);
                    $('#imgEditLayout_Visual,#btnDeleteLayout').hide().delay(1000);
                    var layout = $('#ddlLayoutList option:selected').val();
                    LayoutManager.LoadLayoutWireFrame(layout);
                    //$('#btnDeleteLayout').easyconfirm({ locale: { title: 'Select Yes or No', text: 'Are you sure you want to delete this layout?', button: ['No', 'Yes']} });
                    $('#btnDeleteLayout').click(function () {
                        jConfirm('Are you sure you want to delete this layout?', 'Confirmation Dialog', function (r) {
                            if (r) {
                                var result = LayoutManager.ajaxCall_return(LayoutManager.config.baseURL + "DeleteLayout", JSON2.stringify({ TemplateName: LayoutManager.config.Template, Layout: $('#ddlLayoutList').val() }));
                                SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "TemplateManager", "LayoutDeleted"), "Success");
                                //  LayoutManager.LoadLayoutList_Visual();
                            }
                        });
                        //});
                    });
                }
            },
            LoadLayoutWireFrame: function (layout) {
                this.config.method = "GenerateWireFrame";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ FilePath: layout, TemplateName: $.trim(LayoutManager.config.Template) });
                this.config.ajaxCallMode = 13;
                this.ajaxCall(this.config);
            },
            BindLayoutWireFrame: function (data) {
                var layout = data.d;
                $('#divLayoutWireframe').html(layout);
                LayoutManager.InitResizeLayout();
                LayoutManager.InitEqualHeights();
            },
            LoadNewLayout: function (NewLayOut) {
                LayoutManager.config.NewValue.push(NewLayOut);
            },
            LoadPanName: function (PanName) {
                var PName = PanName.split('sf')[1];
                LayoutManager.config.PanName.push(PName);
            },
            InitResizeLayout: function () {
                LayoutManager.config.PanName = [];
                $('.sfOuterwrapper').each(function (index) {
                    if ($(this).prop('id') !== 'sfBodyContent') {
                        var tableId = $(this).prop('id').toLowerCase() + "_mytable";
                        $('#' + tableId).before('<span style="display:none"  id="split_' + tableId + '" class="sfSplit"><img src=' + SageFrame.utils.GetAdminImage("split.png") + '  alt="Split"  id=btnSplit_' + tableId + ' /></span>');
                        $('#' + tableId).before('<span style="display:none" id="merge_' + tableId + '" class="sfMerge"><img src=' + SageFrame.utils.GetAdminImage("merge.png") + '  alt="Merge"  id=btnMerge_' + tableId + ' /></span><div style="display:none" id=divPlaceHolder_' + tableId + '></div>');
                        $('#' + tableId).resize();
                    }
                });
                $('.sfMainContent,.sfMiddletop,.sfMiddlebottom,.sfFulltopspan,.sfFullbottomspan').find('table').each(function () {
                    var tableId = $(this).prop('id');
                    $('#' + tableId).before('<span style="display:none" id="split_' + tableId + '" class="sfSplit"><img src=' + SageFrame.utils.GetAdminImage("split.png") + '  alt="Split" id=btnSplit_' + tableId + ' /></span>');
                    $('#' + tableId).before('<span style="display:none" id="merge_' + tableId + '" class="sfMerge"><img src=' + SageFrame.utils.GetAdminImage("merge.png") + '  alt="Merge" id=btnMerge_' + tableId + ' /></span><div style="display:none" id=divPlaceHolder_' + tableId + '></div>');
                    $('#' + tableId).resize();
                });
            },
            BindEditButton: function () {
                $(".sfOuterwrapper").each(function () {
                    // if ($('#ddlLayoutList').val() !== 'Core')
                    //$(this).before('<span class="sfEditLayoutPage"><img src=' + SageFrame.utils.GetAdminImage("page-edit.png") + '  alt="Edit LayOut" /></span>');
                });
            },
            RecreateLayout: function (FileName, SfHeader, SfContain, SfFooter) {
                this.config.method = "RecreateLayout";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ FilePath: FileName, TemplateName: LayoutManager.config.Template, sfHeader: SfHeader, sfContain: SfContain, sfFooter: SfFooter });
                this.config.ajaxCallMode = 27;
                this.ajaxCall(this.config);
            },
            SortPan: function () {
                $('#sfOuterWrapper').sortable({
                    //revert: true
                    items: ":not(.sfBlockwrap,#sfOuterWrapper,#sfBodyContent,#sfOuterWrapper div div,#sfOuterWrapper div table thead tr th div,#sfOuterWrapper div table thead tr th,#sfOuterWrapper div table thead tr,#sfOuterWrapper div table thead,#sfOuterWrapper div table)",
                    stop: function () {

                    }
                });
                $('table thead tr').sortable({
                    connectWith: '#' + $(this).parents().find('table').prop('id'),
                    stop: function () {
                        $("#" + $(this).parents().find('table').prop('id')).resize();
                    }
                });
                $("#sfOuterWrapper,table thead tr").disableSelection();
            },

            ///End
            UpdateLayout: function (filename, xml) {
                this.config.method = "UpdateLayout";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ FilePath: filename, Xml: xml, TemplateName: LayoutManager.config.Template });
                this.config.ajaxCallMode = 14;
                this.ajaxCall(this.config);
            },
            BindUpdatedLayout: function (data) {
                if (data.d == 0) {
                    LayoutManager.LoadLayoutWireFrame($('#hdnLayoutName').val());
                    LayoutManager.HidePopUp();
                }
                else if (data.d == 1) {
                    SageFrame.messaging.alert("XML Document contains invalid tags", '#msgDiv');
                }
            },
            LoadFiles: function (_TemplateName, _path) {
                this.config.method = "GetFiles";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ TemplateName: _TemplateName, FolderPath: _path });
                this.config.ajaxCallMode = 15;
                this.ajaxCall(this.config);
            },
            BindFiles: function (data) {
                var files = data.d;
                var html = '<ul>';
                $.each(files, function (index, item) {
                    var TemplatePath = LayoutManager.config.AppPath + "/" + item.FilePath.substring(item.FilePath.indexOf("Templates"), item.FilePath.Length);
                    TemplatePath = TemplatePath.replace(/\\/gi, "\/");
                    var previewClass = LayoutManager.IsImage(item.FileName) ? "class='preview' href='" + TemplatePath + "'" : "href='#'";
                    html += '<li><a ' + previewClass + '>' + LayoutManager.GetFileImage(item.FileExtension) + '<div class="name" id=' + item.FilePath + '>' + item.FileName + '</div></a><div class="size">' + item.Size + ' <i>bytes</i></div><div class="time">' + item.CreatedDate + '</div>';
                    html += LayoutManager.IsFile(item.FileName) ? '<span class="delete"><img align="absmiddle" src=' + LayoutManagerPath + "images/popupclose.png" + '></span></li>' : '</li>';
                });
                html += '</ul>';
                $('#divFileList').html(html);
                LayoutManager.config.NavMode = "Folder";
                LayoutManager.InitLightBox();
            },
            GetFileImage: function (ext) {
                var filePic = "file";
                switch (ext) {
                    case '.htm':
                        filePic = "html";
                        break;
                    case '.css':
                        filePic = "css";
                        break;
                    case '.xml':
                        filePic = "xml";
                        break;
                    case '.ico':
                        filePic = "picture";
                        break;
                    case '.ascx':
                        filePic = "html";
                        break;
                    case '.png':
                        filePic = "picture";
                        break;
                    case '.jpg':
                        filePic = "picture";
                        break;
                    case '.gif':
                        filePic = "picture";
                        break;
                    default:
                        filePic = "directory";
                        break;
                }
                filePic = filePic + ".png";
                return '<img src="../images/fileimages/' + filePic + '" align="absmiddle">';
            },
            BuildBreadCrumb: function (directory, mode) {
                if (mode == 0) {
                    var breadCrumb = $('#divFileMgrBreadCrumb').html();
                    breadCrumb += ">>";
                    breadCrumb += '<span>' + directory + '</span>';
                    $('#divFileMgrBreadCrumb').html(breadCrumb);
                }
                else if (mode == 1) {
                    $('#divFileMgrBreadCrumb').html(directory);
                }
            },
            GetFolderPath: function (self) {
                var breadCrumb = $('#divFileMgrBreadCrumb span');
                var path = "";
                var keepChecking = true;
                $.each(breadCrumb, function (index, item) {
                    if (keepChecking) {
                        if (self != $(this).text()) {
                            if (index > 0) {
                                path += $(this).text();
                                path += "/";
                            }
                        }
                        else {
                            path += $(this).text();
                            keepChecking = false;
                        }
                    }
                });
                path = LayoutManager.config.Template === path ? "" : path;
                return path;
            },
            BuildNewBreadCrumb: function (self) {
                var breadCrumb = $('#divFileMgrBreadCrumb span');
                var keepChecking = true;
                var newBreadCrumb = "";
                $.each(breadCrumb, function (index, item) {
                    if (keepChecking) {
                        if (self != $(this).text()) {
                            newBreadCrumb += '<span>' + $(this).text() + '</span>';
                            newBreadCrumb += ">>";
                        }
                        else {
                            newBreadCrumb += '<span>' + $(this).text() + '</span>';
                            keepChecking = false;
                        }
                    }
                });
                LayoutManager.BuildBreadCrumb(newBreadCrumb, 1);
            },
            IsFile: function (fileName) {
                return (fileName.indexOf(".") > -1 ? true : false);
            },
            IsImage: function (fileName) {
                var status = false;
                if (fileName.indexOf(".png") > -1 || fileName.indexOf(".jpg") > -1 || fileName.indexOf(".gif") > -1) {
                    status = true;
                }
                return status;
            },
            InitCodeMirror: function () {
                var editor = CodeMirror.fromTextArea(document.getElementById("code"), {
                    mode: "application/xml",
                    lineNumbers: true,
                    onCursorActivity: function () {
                        editor.setLineClass(hlLine, null);
                        hlLine = editor.setLineClass(editor.getCursor().line, "activeline");
                    }
                });
                editor.setOption("theme", "night");
                var hlLine = editor.setLineClass(0, "activeline");
            },
            ResetInlineEditor: function () {
                $('div.CodeMirror').remove();
                $('#divInlineEditor').hide();
            },
            ReadFile: function (_TemplateName, _FileName) {
                this.config.method = "ReadFiles";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ TemplateName: _TemplateName, FilePath: _FileName });
                this.config.ajaxCallMode = 16;
                this.ajaxCall(this.config);
            },
            BindFileToEditor: function (data) {
                $('#code').val(data.d);
                LayoutManager.InitCodeMirror();
            },
            InitLightBox: function () {
                $('#divFileList ul li a.preview').lightBox({
                    imageBtnClose: LayoutManagerPath + "images/lightbox-btn-close.gif",
                    imageLoading: LayoutManagerPath + "images/lightbox-ico-loading.gif",
                    imageBtnNext: LayoutManagerPath + "images/lightbox-btn-next.gif",
                    imageBtnPrev: LayoutManagerPath + "images/lightbox-btn-prev.gif",
                    imageBlank: LayoutManagerPath + "images/lightbox-blank.gif"
                });
            },
            LoadPresets: function () {
                this.config.method = "LoadPresets";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ TemplateName: LayoutManager.config.Template });
                this.config.ajaxCallMode = 17;
                this.ajaxCall(this.config);
            },
            BindPresets: function (data) {
                var presets = data.d;
                var html = '';
                var activepresets = this.ajaxCall_return(this.config.baseURL + "LoadActivePresets", JSON2.stringify({ TemplateName: LayoutManager.config.Template }));
                $.each(presets, function (index, item) {
                    var activeClass = LayoutManager.IsPresetActive(activepresets, item.Key) ? "class='sfActive'" : "";
                    html += '<li ' + activeClass + '>' + item.Key + '<span class="delete"><img src=' + SageFrame.utils.GetAdminImage("btndelete.png") + ' /></span></li>';
                });
                $('#ulPresetList').html(html);
                $('#ulPresetList li').removeClass("selectedPreset");
                var firstActivePreset = LayoutManager.GetFirstActivePreset(true);
                LayoutManager.LoadLayoutList_Preset();
            },
            IsPresetActive: function (activepresets, preset) {
                var IsActive = false;
                $.each(activepresets, function (index, item) {
                    if (item.PresetName + ".xml" === preset) {
                        IsActive = true;
                    }
                });
                return IsActive;
            },
            GetFirstActivePreset: function (highlight) {
                var firstpreset = "";
                var presetList = $('#ulPresetList li');
                var keepChecking = true;
                $.each(presetList, function () {
                    if (keepChecking) {
                        if ($(this).hasClass('sfActive')) {
                            firstpreset = $(this).text();
                            keepChecking = false;
                            if (highlight) {
                                $(this).addClass('selectedPreset');
                            }
                        }
                    }
                });
                return firstpreset;
            },
            BindPresetDetails: function () {
                var presetdetails = this.ajaxCall_return(this.config.baseURL + "GetPresetDetails", JSON2.stringify({ TemplateName: LayoutManager.config.Template, PortalID: SageFramePortalID }));
                LayoutManager.ClearPresetDetails();
                var themes = $('#activeThemeList ul li');
                $.each(themes, function (index, item) {
                    if ($(this).text() == presetdetails.ActiveTheme) {
                        $(this).addClass("sfActive");
                    }
                });
                var widths = $('#activeWidthList ul li');
                $.each(widths, function (index, item) {
                    if ($(this).text().toLowerCase() == presetdetails.ActiveWidth) {
                        $(this).addClass("sfActive");
                    }
                });
                var layouts = $('label.sfLayout');
                $.each(layouts, function () {
                    var activelayouts = presetdetails.lstLayouts;
                    var self = $(this);
                    $.each(activelayouts, function (index, item) {
                        if ((self).text().toLowerCase() == item.Key.toLowerCase()) {
                            $(self).addClass("sfActive");
                            if (item.Value.toLowerCase() == "all") {
                                $(self).next("select").val("1");
                            }
                            else {
                                $(self).next("select").val("2");
                                $(self).parent().append('<span class="customize"><i class="icon-page-add" data-title="Add Page" ></i></span>');
                                var pages = item.Value.split(',');
                                var html = '<div class="sfPresetpages"><ul>';
                                $.each(pages, function (i, it) {
                                    html += '<li class="sfCurve"><span class="sfPageName">' + SageFrame.utils.GetPageSEOName(it) + '</span><span class="sfDelete"><i class ="icon-delete"></i></span></li>';
                                });
                                html += '</ul></div>';
                                $(self).parent().append(html);
                            }
                        }
                    });
                });
            },
            ClearPresetDetails: function () {
                $('#spnActiveLayout').text("");
                $('#spnActiveTheme').text("");
                $('#spnActiveWidth').text("");
                $('#chkCssOpt_preset').prop("checked", false);
                $('#chkJsOpt_preset').prop("checked", false);
                $('#chkShowCpanel_preset').prop("checked", false);
                $('#chkEnableHandheld_preset').prop("checked", false);
                $('#trHandheld').hide();
            },
            SavePreset: function () {
                var param = {
                    TemplateName: LayoutManager.config.Template,
                    ActiveTheme: $('#activeThemeList ul li.sfActive').text().toLowerCase(),
                    ActiveWidth: $('#activeWidthList ul li.sfActive').text().toLowerCase(),
                    lstLayouts: LayoutManager.SelectPages(),
                    portalID: LayoutManager.config.PortalID
                };
                var preset = this.ajaxCall_return(this.config.baseURL + "SavePreset", JSON2.stringify(param));
                switch (preset) {
                    case 0:
                        SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "TemplateManager", "PresetSaved"), "Success");
                        break;
                    case 1:
                        SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "TemplateManager", "ApplyToAllForMultiplePresets"), "Alert");
                        break;
                    case 2:
                        SageFrame.messaging.show(SageFrame.messaging.GetLocalizedMessage("en-US", "TemplateManager", "PageAlreadyAppliedToPreset"), "Alert");
                        break;
                }
            },
            SelectPages: function () {
                var pages = "All";
                var layouts = $('#activeLayoutList ul li');
                var selectedPages = [];
                var allPageFlag = 0;
                var pagesArr = [];
                var i = 0;
                $.each(layouts, function (index, item) {
                    switch ($(this).find("select").val()) {
                        case "0":
                            break;
                        case "1":
                            if (allPageFlag == 0) {
                                selectedPages[i] = { "Key": $(this).find("label.sfLayout").text(), "Value": "All" };
                                i++;
                                allPageFlag = 1;
                            }
                            break;
                        case "2":
                            var pages = $(this).find("div.sfPresetpages ul li");
                            var selected = [];
                            $.each(pages, function () {
                                if (jQuery.inArray($(this).text(), pagesArr) == -1) {
                                    selected.push($(this).text());
                                    pagesArr.push($(this).text());
                                }
                            });
                            if (selected.length > 0) {
                                selectedPages[i] = { "Key": $(this).find("label.sfLayout").text(), "Value": selected.join(',') };
                                i++;
                            }
                            break;
                    }
                });
                return selectedPages;
            },
            GetSEOName: function (word) {
                return (word.replace(/ /g, "-"));
            },
            DeleteTemplate: function (template) {
                this.config.method = "DeleteTemplate";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ TemplateName: template });
                this.config.ajaxCallMode = 18;
                this.ajaxCall(this.config);
            },
            InitGallery: function () {
                $(".image").click(function () {
                    var image = $(this).prop("rel");
                    $('#imagePreview').html('<img src="' + image + '"/>');
                    var newheight = $('#imagePreview img').height();
                    $('#imagePreview').animate({ height: newheight + "px" }, 500);
                    return false;
                });
            },
            GetPreviewImages: function (templateName) {
                this.config.method = "GetPreviewImages";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ TemplateName: LayoutManager.config.Template });
                this.config.ajaxCallMode = 19;
                this.ajaxCall(this.config);
            },
            BindPreviewImages: function (data) {
                var images = data.d;
                var html = '';
                if (images.length == 0) {
                    var imagepath = LayoutManager.config.AppPath + '/images/defaulttemplate.jpg';
                    html += '<a href="#" rel=' + imagepath + ' class="image"><img src=' + imagepath + ' class="thumb" border="0"/></a>';
                    $('#imagePreview').html('<img src=' + imagepath + ' border="0"/>');
                }
                $.each(images, function (index, item) {
                    if (index < 4) {
                        var imagepath = LayoutManager.config.Template.toLowerCase() == "default" ? LayoutManager.config.AppPath + '/Core/Template/screenshots/' + item.Key : LayoutManager.config.AppPath + '/Templates/' + LayoutManager.config.Template + "/screenshots/" + item.Key;
                        var imgsrc = LayoutManager.config.Template.toLowerCase() == "default" ? LayoutManager.config.AppPath + '/Core/Template/screenshots/' + item.Key : LayoutManager.config.AppPath + '/Templates/' + LayoutManager.config.Template + "/screenshots/" + item.Key;
                        html += '<a href="#" rel=' + imagepath + ' class="image"><img src=' + imgsrc + ' class="thumb" border="0"/></a>';
                        if (index == 0) {
                            $('#imagePreview').html('<img src=' + imgsrc + ' border="0"/>');
                        }
                    }
                });
                $('#imageThumbs').html(html);
                LayoutManager.InitGallery();
            },
            DeleteTheme: function (templatename, theme) {
                this.config.method = "DeleteTheme";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ TemplateName: LayoutManager.config.Template, ThemeName: theme });
                this.config.ajaxCallMode = 20;
                this.ajaxCall(this.config);
            },
            LoadLayoutList_Creator: function () {
                this.config.method = "LoadLayout";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ TemplateName: LayoutManager.config.Template });
                this.config.ajaxCallMode = 22;
                this.ajaxCall(this.config);
            },
            BindLayoutList_Creator: function (data) {
                var layouts = data.d;
                var html = '';
                html += '<option>[None]</option>';
                $.each(layouts, function (index, item) {
                    var sn = parseInt(index) + 1;
                    html += '<option>' + item.Key + '</option>';
                });
                html += '</ul>';
                $('#ddlClonebleLayouts').html(html);
            },
            ReadLayout_Create: function (fileName) {
                this.config.method = "ReadXML";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ filePath: fileName, TemplateName: LayoutManager.config.Template });
                this.config.ajaxCallMode = 23;
                this.ajaxCall(this.config);
            },
            BindLayout_Create: function (data) {
                var xml = data.d;
                editorCreate.setValue(xml);
            },
            CreateLayout: function (filename, xml) {
                this.config.method = "CreateLayout";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ FilePath: filename, Xml: xml, TemplateName: LayoutManager.config.Template });
                this.config.ajaxCallMode = 24;
                this.ajaxCall(this.config);
            },
            PostLayoutCreationActions: function (data) {
                if (parseInt(data.d) == 0) {
                    SageFrame.messaging.show("Layout Created Successfully", "Success");
                    LayoutManager.LoadLayoutList_Visual();
                    LayoutManager.HidePopUp();
                }
                else {
                    SageFrame.messaging.alert("Invalid Layout", '#msgAddLayout');
                }
            }
        };
        LayoutManager.init();
        LayoutManager.InitResizeLayout();
    }
    $.fn.LayoutManager = function (p) {
        $.createLayoutManager(p);
    };
})(jQuery);