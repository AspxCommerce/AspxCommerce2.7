(function ($) {
    $.ModuleMaker = function (p) {
        var rowCount = 1;
        var _source = [{
            label: "bigint",
            value: "bigint"
        }, {
            label: "binary(50)",
            value: "binary(50)"
        }, {
            label: "bit",
            value: "bit"
        }, {
            label: "char(10)",
            value: "char(10)"
        }, {
            label: "date",
            value: "date"
        }, {
            label: "datetime",
            value: "datetime"
        }, {
            label: "datetime2(7)",
            value: "datetime2(7)"
        }, {
            label: "datetimeoffset(7)",
            value: "datetimeoffset(7)"
        }, {
            label: "decimal(18, 0)",
            value: "decimal(18, 0)"
        }, {
            label: "float",
            value: "float"
        }, {
            label: "geography",
            value: "geography"
        }, {
            label: "geometry",
            value: "geometry"
        }, {
            label: "hierarchyid",
            value: "hierarchyid"
        }, {
            label: "image",
            value: "image"
        }, {
            label: "int",
            value: "int"
        }, {
            label: "money",
            value: "money"
        }, {
            label: "nchar(10)",
            value: "nchar(10)"
        }, {
            label: "ntext",
            value: "ntext"
        }, {
            label: "numeric(18, 0)",
            value: "numeric(18, 0)"
        }, {
            label: "nvarchar(50)",
            value: "nvarchar(50)"
        }, {
            label: "nvarchar(max)",
            value: "nvarchar(max)"
        }, {
            label: "real",
            value: "real"
        }, {
            label: "smalldatetime",
            value: "smalldatetime"
        }, {
            label: "smallint",
            value: "smallint"
        }, {
            label: "smallmoney",
            value: "smallmoney"
        }, {
            label: "sql_variant",
            value: "sql_variant"
        }, {
            label: "text",
            value: "text"
        }, {
            label: "time(7)",
            value: "time(7)"
        }, {
            label: "timestamp",
            value: "timestamp"
        }, {
            label: "tinyint",
            value: "tinyint"
        }, {
            label: "uniqueidentifier",
            value: "uniqueidentifier"
        }, {
            label: "varbinary(50)",
            value: "varbinary(50)"
        }, {
            label: "varbinary(max)",
            value: "varbinary(max)"
        }, {
            label: "varchar(50)",
            value: "varchar(50)"
        }, {
            label: "varchar(max)",
            value: "varchar(max)"
        }, {
            label: "xml",
            value: "xml"
        }];
        var wordlist = [
          "bigint", "binary(50)", "bit", "char", "date", "datetime", "datetime2", "datetimeoffset(7)",
          "datetimeoffset(7)", "decimal(18, 0)", "float", "geography", "geometry", "hierarchyid", "image",
          "int", "money", "nchar(10)", "nchar(10)", "ntext", "ntext", "numeric(18, 0)", "nvarchar(50)",
          "nvarchar(MAX)", "real", "smalldatetime", "smallint", "smallint", "smallmoney", "smallmoney",
          "sql_variant", "text", "time(7)", "timestamp", "tinyint", "uniqueidentifier", "varbinary(50)",
          "varbinary(MAX)", "varchar(50)", "varchar(MAX)", "xml"
        ];
        p = $.extend
                ({
                    CultureCode: '',
                    UserModuleID: '1',
                    hdnTableList: '',
                    databaseModuleList: '',
                    folderModuleList: '',
                    txtModuleName: '',
                    txttableName: '',
                    hdnXML: '',
                    txtModuleDescription: ''
                }, p);
        var columnsList = [];
        var datatypeList = [];
        var allowNullList = [];
        var firstEdit = true;
        var primaryKey = "";
        var ModuleMaker = {
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
                arr: [],
                arrModules: [],
                baseURL: SageFrameAppPath + '/Modules/Admin/ModuleMaker/Services/ModuleMaker.asmx/',
                Path: SageFrameAppPath + '/Modules/Admin/ModuleMaker/',
                PortalID: SageFramePortalID,
                UserName: SageFrameUserName,
                UserModuleID: p.UserModuleID
            },
            init: function () {
                $(p.txtModuleName).last().focus();
                ModuleMaker.InitNewRows(rowCount);
                $('#divModuleCreator').on('click', '#AddRow', function () {
                    //var addProperty = true;
                    //var addDatatype = true;
                    //$('#divProperties .Properties').each(function () {
                    //    if ($(this).val().trim().length == 0) {
                    //        addProperty = false;
                    //        return;
                    //    }
                    //});
                    //$('#divProperties .dataTypeInput').each(function () {
                    //    if ($(this).val().trim().length == 0) {
                    //        addDatatype = false;
                    //        return;
                    //    }
                    //});
                    //if (addProperty && addDatatype) {
                    ModuleMaker.AppendNewRow();
                    $('.sfMessage ').trigger('click');
                    //}
                    //else if (!addProperty) {
                    //    SageFrame.messaging.show("Property field can't be empty", "error");
                    //}
                    //else if (!addDatatype) {
                    //    SageFrame.messaging.show("Datatype field can't be empty", "error");
                    //}
                });
                $('#divProperties').on('click', '.deleterow', function () {
                    var addBtn = $('#divProperties').find('#AddRow');
                    $(this).parents('tr').remove();
                    addBtn.appendTo($('#divProperties').find('tr').last().find('td').last());
                });
                $(p.txtModuleName).on('keydown', function (e) {
                    return e.which != 32;// prevent user to type space in module name
                });
                $(p.txttableName).on('keydown', function (e) {
                    return e.which != 32;// prevent user to type space in table name
                });
                $('#divProperties').on('keydown', '.Properties', function (e) {
                    return e.which != 32;// prevent user to type space in ColumnName name
                });
                $('#divProperties').on('keydown', '.dataTypeInput', function (e) {
                    return (e.which != 173 && e.which != 109);// prevent user to type "-" in ColumnName name
                });
                $('#divProperties').on('blur', '.dataTypeInput', function () {
                    var $self = $(this);
                    var value = $self.val();
                    if (value.trim().length == 0) {
                        $self.val('nvarchar(256)');
                    }
                    else {
                        ModuleMaker.Validate(value, $self);
                    }
                });
                $('#rdbSetting').on("click", function () {
                    ModuleMaker.ChangeRdbActiveClass($(this));
                });
                $('#rdbPortal').on("click", function () {
                    ModuleMaker.ChangeRdbActiveClass($(this));
                });
                $(p.txtModuleName).on('keyup blur', function () {
                    $('.sfloader').show();
                    $('.error').hide();
                    var moduleName = $(this).val().trim();
                    setTimeout(function () {
                        if (moduleName.length > 0) {
                            var lowermoduleName = moduleName.toLowerCase();
                            var foldermodulelist = [];
                            foldermodulelist = ($(p.folderModuleList).val() + $(p.hdnTableList).val() + $(p.databaseModuleList).val()).split(',');
                            if ($.inArray(lowermoduleName, foldermodulelist) < 0) {
                                $('.error').hide().val(0);
                            }
                            else {
                                $('.error').show().text('Module name already exists').val(1);
                            }
                            if (firstEdit == true) {
                                $(p.txttableName).val(moduleName);
                                ModuleMaker.ValidatetableName();
                            }
                        }
                        else {
                            $('.error').hide().val(0);
                        }
                        $('.sfloader').hide();
                    }, 500);
                });
                $(p.txttableName).on('keyup blur', function () {
                    firstEdit = false;
                    ModuleMaker.ValidatetableName();
                });
                $('#btnCreateSQL').on('click', function () {
                    if ($('#autoincrement').prop('checked') == true) {
                        var dataType = $('#autocomplete_1').val();
                        var datatypeCheck = /bigint|int|smallint|tinyint|(decimal\(|(numeric\())/;
                        var datatypeExtracted = dataType.match(datatypeCheck);
                        if (datatypeExtracted == null) {
                            SageFrame.messaging.show("Auto increment datatypes can only be int, bigint, smallint, tinyint, decimal(18, 0) or numeric(18, 0)", "error");
                            return;
                        }
                    }
                    ModuleMaker.CreateXML();
                });
                $('#btnBack').on('click', function () {
                    $('#divModuleCreator').show();
                    $('#divSqlformation').hide();
                });
                //$('#slqProcedures').on('click', '.chkHeading', function () {
                //    var $self = $(this);
                //    if ($self.hasClass('active')) {
                //        $self.removeClass('active');
                //        $self.parents('li').removeClass('active');
                //        $self.parent('.subHeading').siblings('.procedures').slideUp();
                //    }
                //    else {
                //        $self.addClass('active');
                //        $self.parents('li').addClass('active');
                //        $self.parent('.subHeading').siblings('.procedures').slideDown();
                //    }
                //});
                $('#btnCreateNewModuleHelp').on('click', function () {
                    ModuleMaker.AddStoreProcedures();
                    $('input[id$="btnCreateNewModule"]').trigger('click');
                });
                $('#btnCreateZipHelp').on('click', function () {
                    ModuleMaker.AddStoreProcedures();
                    $('input[id$="btnCreateZip"]').trigger('click');
                    $('#btnBack').trigger('click');
                    SageFrame.messaging.show("class and database has been successfully zipped", "success");
                });

                $('input[id$="chkJS"]').on('click', function () {
                    $this = $('.lblWebservice');
                    if ($this.hasClass('hide')) {
                        $this.show();
                        $this.removeClass('hide');
                    }
                    else {
                        $this.hide();
                        $this.addClass('hide');
                    }
                });
            },
            AddStoreProcedures: function () {
                var tableName = $(p.txttableName).val().trim();
                var updateList = "";
                var xml = '<storeProcedures>';
                $('#slqProcedures').find('li').each(function (index, value) {
                    var $self = $(this);
                    switch (index) {
                        case 0:
                        case 3:
                        case 4:
                            {
                                var procedures = $self.find('.procedures').text();
                                xml += '<storeProcedure>' + procedures + '</storeProcedure>';
                            }
                            break;
                        case 1:
                            {
                                var column = [];
                                $self.find('.procedures').find('.chkColumn').each(function (index, value) {
                                    var $this = $(this);
                                    if ($this.prop('checked') == true) {
                                        column.push($this.parent().text().replace(",", ""));
                                    }
                                });
                                var procedures = 'CREATE PROCEDURE [dbo].[usp_' + tableName + '_GetallData]  @PortalID int,@UserModuleID int,@Culture nvarchar(50) AS SELECT ' + column.join(',') + ' FROM ' + tableName + ' WHERE PortalID = @PortalID AND UserModuleID = @UserModuleID AND Culture = @Culture';
                                $self.find('.procedures').text(procedures);
                                xml += '<storeProcedure>' + procedures + '</storeProcedure>';
                            }
                            break;
                        case 2:
                            {
                                var column = [];
                                var primaeryDatatype = datatypeList[0];
                                $self.find('.procedures').find('.chkColumn').each(function (index, value) {
                                    var $this = $(this);
                                    if ($this.prop('checked') == true) {
                                        column.push($this.parent().text().replace(",", ""));
                                    }
                                });
                                var procedures = 'CREATE PROCEDURE [dbo].[usp_' + tableName + '_GetByID] @' + primaryKey + ' ' + primaeryDatatype + ' AS SELECT ' + column.join(',') + ' from ' + tableName + ' WHERE ' + primaryKey + '= @' + primaryKey;
                                $self.find('.procedures').text(procedures);
                                xml += '<storeProcedure>' + procedures + '</storeProcedure>';
                            }
                            break;
                        case 5:
                            {
                                var parameters = "";
                                var column = [];
                                var updates = [];
                                parameters += '@' + columnsList[0] + ' ' + datatypeList[0] + ',';
                                if (($('#autoincrement').prop('checked') == true)) {
                                    updateList += "<row><properties>" + columnsList[0] + "</properties><datatype>" + datatypeList[0] + "</datatype></row>";
                                }
                                $self.find('.procedures').find('.chkColumn').each(function (index, value) {
                                    var $this = $(this);
                                    if (($('#autoincrement').prop('checked') == true)) {
                                        index = index + 1;
                                    }
                                    if ($this.prop('checked') == true) {
                                        column.push($this.parent().text().replace(",", ""));
                                        if (index != 0) {
                                            parameters += '@' + columnsList[index] + ' ' + datatypeList[index] + ',';
                                        }
                                        updateList += "<row><properties>" + columnsList[index] + "</properties><datatype>" + datatypeList[index] + "</datatype></row>";
                                    }
                                });
                                parameters = parameters.slice(0, -1);
                                var procedures = 'CREATE PROCEDURE [dbo].[usp_' + tableName + '_Update] ' + parameters + ' AS UPDATE [dbo].[' + tableName + ']  SET ' + column.join(',') + ' WHERE  ' + primaryKey + '= @' + primaryKey;
                                $self.find('.procedures').text(procedures);
                                xml += '<storeProcedure>' + procedures + '</storeProcedure>';
                            }
                            break;
                    }
                });
                xml += '</storeProcedures>';
                xml += '<updatelist>';
                xml += updateList;
                xml += '</updatelist>';
                xml += '</Module>';
                var html = $(p.hdnXML).val() + xml;
                html = $('<div/>').text(html).html();
                $(p.hdnXML).val(html);
            },
            ValidatetableName: function () {
                $('.sfloader1').show();
                $('.error1').hide();
                var tableName = $(p.txttableName).val().trim().toLowerCase();
                setTimeout(function () {
                    if (tableName.length > 0) {
                        var foldermodulelist = [];
                        foldermodulelist = ($(p.hdnTableList).val()).split(',');
                        if ($.inArray(tableName, foldermodulelist) < 0) {
                            $('.error1').hide().val(0);
                        }
                        else {
                            $('.error1').show().text('Table name already exists').val(1);
                        }
                    }
                    else {
                        $('.error1').hide().val(0);
                    }
                    $('.sfloader1').hide();
                }, 500);

            },
            CreateXML: function () {
                var columnlength = $('#divProperties tbody tr').length;
                if (columnlength < 2) {
                    SageFrame.messaging.show("One column table is not allowed", "error");
                    return;
                }
                if ($('.error').val() == "1") {
                    SageFrame.messaging.show("Module name already exists. Please change the module name", "error");
                    return;
                }
                if ($('.error1').val() == "1") {
                    SageFrame.messaging.show("Table name already exists. Please change the table name", "error");
                    return;
                }
                if ($(p.txtModuleName).val().trim().length == 0) {
                    SageFrame.messaging.show("Module name can't be empty", "error");
                    return;
                }
                if ($(p.txtModuleDescription).val().trim().length == 0) {
                    SageFrame.messaging.show("Module description can't be empty", "error");
                    return;
                }
                if ($(p.txttableName).val().trim().length == 0) {
                    SageFrame.messaging.show("Table name can't be empty", "error");
                    return;
                }
                var xml = "<?xml version='1.0' encoding='utf-8'?>";
                xml += "<Module>";
                xml += "<isadmin>" + ($('#rdbPortal').prop('checked') == true ? 0 : 1) + "</isadmin>";
                xml += "<autoincrement>" + ($('#autoincrement').prop('checked') == true ? 1 : 0) + "</autoincrement>";
                xml += "<rows>";
                columnsList = [];
                datatypeList = [];
                allowNullList = [];
                $('#divProperties tbody tr').each(function (index, value) {

                    var properties = $(this).find('.Properties').val();
                    var datatype = $(this).find('.dataTypeInput').val();
                    var allowNull = $(this).find('.chkNull').prop('checked');
                    if (properties.length > 0) {
                        if ($.inArray(properties, columnsList) < 0) {
                            columnsList.push(properties);
                            datatypeList.push(datatype);
                            allowNullList.push(allowNull);
                        }
                        else {
                            xml = "";
                            SageFrame.messaging.show("Duplicate column name detected.", "error");
                            return;
                        }
                        if (datatype.length > 0) {
                            xml += "<row><properties>" + properties + "</properties><datatype>" + datatype + "</datatype></row>";
                        }
                        else {
                            xml = "";
                            SageFrame.messaging.show("Datatype can't be empty.", "error");
                            return;
                        }
                    }
                    else {
                        SageFrame.messaging.show("Column can't be empty.", "error");
                        xml = "";
                        return;
                    }
                });
                if (xml.length != 0) {
                    xml += "<row><properties>PortalID</properties><datatype>int</datatype></row>";
                    xml += "<row><properties>UserModuleID</properties><datatype>int</datatype></row>";
                    xml += "<row><properties>Culture</properties><datatype>nvarchar(50)</datatype></row>";
                    columnsList.push("PortalID");
                    datatypeList.push("int");
                    allowNullList.push(false);
                    columnsList.push("UserModuleID");
                    datatypeList.push("int");
                    allowNullList.push(false);
                    columnsList.push("Culture");
                    datatypeList.push("nvarchar(50)");
                    allowNullList.push(false);
                    xml += "</rows>";
                    $(p.hdnXML).val(xml);
                    $('#divModuleCreator').hide();
                    $('#divSqlformation').show();
                    $('.sfMessage ').trigger('click');
                    $('body,html').animate({
                        scrollTop: 0
                    }, 800);
                    ModuleMaker.CreateSQLQueries();
                }
            },
            ChangeRdbActiveClass: function ($this) {
                $this.parents('div.sfRadiobutton').find('label.sfActive').removeClass('sfActive');
                $this.parents('label').addClass('sfActive');
            },
            CreateSQLQueries: function () {
                var html = "";
                var tableName = $(p.txttableName).val().trim();
                var createTable = 'CREATE TABLE [dbo].[' + tableName + '](';
                primaryKey = columnsList[0];
                var primaeryDatatype = datatypeList[0];
                var insertParameters = '';
                var updateParameters = '';
                var columnName = '';
                var update = '';
                var insertVariables = '';
                var insertColumns = '';
                var columnLength = columnsList.length;
                $.each(columnsList, function (index, value) {
                    //if the auto increment is true then they are not inserted or updated from the from end
                    if (!(($('#autoincrement').prop('checked') == true) && index == 0)) {
                        insertParameters += '<br />@' + value + ' ' + datatypeList[index] + ',';
                        insertVariables += '@' + value + ',';
                        if (value != "PortalID" && value != "UserModuleID" && value != "Culture") {
                            update += '<br /><span class="sfCheckboxs"><label><input type="checkbox" class="chkColumn" checked="checked" />' + value + '=  @' + value;
                            if (parseInt(columnLength - 1) != parseInt(index)) {
                                update += ',';
                            }
                            update += '</label></span>';
                        }
                        insertColumns += value + ',';
                    }
                    if (value != "PortalID" && value != "UserModuleID" && value != "Culture") {
                        updateParameters += '<br />@' + value + ' ' + datatypeList[index] + ',';
                    }
                    createTable += ' ' + value + ' ' + datatypeList[index];
                    if (index == 0) {
                        createTable += ' NOT NULL ';
                        // if the auto increment is true then it is incremented by one
                        if ($('#autoincrement').prop('checked') == true) {
                            createTable += ' IDENTITY(1,1) ';
                        }
                        createTable += ' PRIMARY KEY ';
                    }
                    else {
                        createTable += (allowNullList[index] ? ' NULL' : ' NOT NULL');
                    }
                    createTable += ',';
                    columnName += '<span class="sfCheckboxs"><label><input type="checkbox" class="chkColumn" checked="checked" />' + value + '</label></span>,';
                    html += '<span class="sfCheckboxs"><label><input type="checkbox" class="chkColumn" checked="checked" />' + value;
                    //appending commana after each column except for the last
                    if (parseInt(columnLength - 1) != parseInt(index)) {
                        html += ',';
                    }
                    html += '</label></span>';
                });
                createTable = createTable.slice(0, -1);
                createTable += ' )';
                //html = html.slice(0, -1);
                insertParameters = insertParameters.slice(0, -1);
                updateParameters = updateParameters.slice(0, -1);
                insertVariables = insertVariables.slice(0, -1);
                insertColumns = insertColumns.slice(0, -1);
                update = update.slice(0, -1);
                var li = '<li><div class="subHeading"><span class="icon-arrow-slim-e active "></span>Table Create</div><div class="procedures">' + createTable + '</li></div>';
                li += '<li><div class="subHeading"><span class="icon-arrow-slim-e active chkHeading"></span>Select data list </div><div class="procedures"> CREATE PROCEDURE [dbo].[usp_' + tableName + '_GetallData] <br /> @PortalID int,@UserModuleID int,@Culture nvarchar(50) AS<br /> SELECT ' + html + ' FROM ' + tableName + ' WHERE PortalID = @PortalID AND UserModuleID = @UserModuleID AND Culture = @Culture </div></li>';
                li += '<li><div class="subHeading"><span class="icon-arrow-slim-e active chkHeading"></span>Select single data </div> <div class="procedures"> CREATE PROCEDURE [dbo].[usp_' + tableName + '_GetByID]  <br /> @' + primaryKey + ' ' + primaeryDatatype + '<br /> AS <br />SELECT' + html + ' FROM ' + tableName + '<br /> WHERE ' + primaryKey + '= @' + primaryKey + '</div></li>';
                li += '<li><div class="subHeading"><span class="icon-arrow-slim-e active chkHeading"></span>Delete single data </div> <div class="procedures">CREATE PROCEDURE [dbo].[usp_' + tableName + '_DeleteByID] <br /> @' + primaryKey + ' ' + primaeryDatatype + '<br /> AS <br /> DELETE FROM [dbo].[' + tableName + ']' + '<br /> WHERE  ' + primaryKey + '= @' + primaryKey + '</div></li>';
                li += '<li><div class="subHeading"><span class="icon-arrow-slim-e active chkHeading"></span>Insert Single </div> <div class="procedures">CREATE PROCEDURE [dbo].[usp_' + tableName + '_Insert] ' + insertParameters + '<br /> AS <br />  INSERT INTO [dbo].[' + tableName + '](' + insertColumns + ')<br /> VALUES (' + insertVariables + ')</div></li>';
                li += '<li><div class="subHeading"><span class="icon-arrow-slim-e active chkHeading"></span>Update Single </div><div class="procedures">CREATE PROCEDURE [dbo].[usp_' + tableName + '_Update] ' + updateParameters + '<br /> AS <br /> UPDATE [dbo].[' + tableName + '] <br /> SET ' + update + '<br /> WHERE  ' + primaryKey + '= @' + primaryKey + ' </div></li>';
                $('#slqProcedures').html(li);
                //$('#slqProcedures div.procedures').each(function () {
                //    var $this = $(this);
                //    $this.html($this.text().replace(/\sCREATE|PROCEDURE|SELECT|AS|WHERE|INSERT|UPDATE|DELETE|SET\s/g, '<span style="color:blue;">' + $this.text() + '</span>'));
                //});
                //$('#slqProcedures div.procedures').each(function () {
                //    var $this = $(this);
                //    $this.html($this.text().replace(/\sCREATE\s/g, '<span style="color:blue;">CREATE </span>'));
                //});
            },
            Validate: function (value, self) {
                if (value == "datetime") {
                    self.val(value);
                    return;
                }
                //datetimeoffset|datetime2
                var nvarDateCheck = /(datetimeoffset|datetime2)\(\d+\)/; //checks for nvarchar(4000)
                var nvarDateExtracted = value.match(nvarDateCheck);
                if (nvarDateExtracted != null) {
                    var lowerlimit = 1;
                    var upperlimit = 7;
                    var numberRegex = /\d+/g;//checks for 4000 in  nvarchar(4000)
                    var stringRegex = /\w+/g;//checks for nvarchar in  nvarchar(4000)
                    var numberExtracted = "";
                    var stringExtracted = nvarDateExtracted[0].match(stringRegex);
                    if (stringExtracted[0] == "datetimeoffset") {
                        numberExtracted = nvarDateExtracted[0].match(numberRegex);
                    }
                    else {
                        numberExtracted = nvarDateExtracted[0].match(numberRegex)[1];
                    }

                    if (numberExtracted != null) {
                        if (lowerlimit > numberExtracted || numberExtracted > upperlimit) {
                            SageFrame.messaging.show("Setting for length must be in between " + lowerlimit + " to " + upperlimit, "error");
                            self.val(stringExtracted[0] + "(" + upperlimit + ")");
                        }
                        else {
                            self.val(stringExtracted[0] + "(" + numberExtracted + ")");
                        }
                    }
                    return;
                }
                var normalDataTypes = /bigint|bit|date|datetime|float|geography|geometry|hierarchyid|image|int|money|ntext|real|smalldatetime|smallint|smallmoney|sql_variant|text|timestamp|tinyint|uniqueidentifier|xml/;
                var normalCheck = value.match(normalDataTypes);
                if (normalCheck != null) {
                    self.val(normalCheck[0]);
                }
                else {
                    var nvarcharCheck = /(char|nchar|varchar|nvarchar|time|binary|varbinary)\(\d+\)/; //checks for nvarchar(4000)
                    var nvarCharExtracted = value.match(nvarcharCheck);
                    if (nvarCharExtracted != null) {
                        var lowerlimit = 1;
                        var upperlimit = 4000;
                        switch (nvarCharExtracted[1]) {
                            case 'char':
                            case 'varchar':
                            case 'varbinary':
                            case 'binary':
                                lowerlimit = 1;
                                upperlimit = 8000;
                                break;
                            case 'time':
                            case 'datetimeoffset':
                            case 'datetime2':
                                lowerlimit = 0;
                                upperlimit = 7;
                                break;
                            case 'nvarchar':
                            case 'nchar':
                                lowerlimit = 1;
                                upperlimit = 4000;
                                break;
                        }
                        var numberRegex = /\d+/g;//checks for 4000 in  nvarchar(4000)
                        var stringRegex = /\w+/g;//checks for nvarchar in  nvarchar(4000)
                        var numberExtracted = nvarCharExtracted[0].match(numberRegex);
                        var stringExtracted = nvarCharExtracted[0].match(stringRegex);
                        if (numberExtracted != null) {
                            if (lowerlimit > numberExtracted[0] || numberExtracted[0] > upperlimit) {
                                SageFrame.messaging.show("Setting for length must be in between " + lowerlimit + " to " + upperlimit, "error");
                                self.val(stringExtracted[0] + "(" + upperlimit + ")");
                            }
                            else {
                                self.val(stringExtracted[0] + "(" + numberExtracted + ")");
                            }
                        }
                    }
                    else {
                        var nvarcharmaxCheck = /(varchar|nvarchar|varbinary)\(\max\)/;//checks for nvarchar(max)
                        var nvarCharmaxExtracted = value.toLowerCase().match(nvarcharmaxCheck);
                        if (nvarCharmaxExtracted != null) {
                            self.val(nvarCharmaxExtracted[0]);
                        }
                        else {
                            var nvarcharDecimalCheck = /(numeric|decimal)\(\d+,\s?\d+\)/;//checks for decimal(18, 0)
                            var nvarCharDecimalExtracted = value.toLowerCase().match(nvarcharDecimalCheck);
                            if (nvarCharDecimalExtracted != null) {
                                var numberRegex = /\d+/g;
                                var stringRegex = /\w+/g;
                                var numberExtracted = nvarCharDecimalExtracted[0].match(numberRegex);
                                var stringExtracted = nvarCharDecimalExtracted[0].match(stringRegex);
                                var precision = numberExtracted[0];
                                var postcision = numberExtracted[1];
                                var upperSetting = 38;
                                var lowersetting = 0;
                                if ((precision > 38 || precision < 1) || (postcision > 38 || postcision < 0)) {
                                    SageFrame.messaging.show('Setting for precision must be from 1-38', 'error');
                                    self.val(stringExtracted[0] + "(18, 0)");
                                    return;
                                }
                                if (postcision > precision) {
                                    postcision = precision;
                                }
                                self.val(stringExtracted[0] + "(" + precision + ", " + postcision + ")");
                            }
                            else {
                                self.val('nvarchar(256)');
                            }
                        }
                    }
                }
            },
            AppendNewRow: function () {
                rowCount++;
                $('#divProperties').append(ModuleMaker.Rows(rowCount));
                ModuleMaker.InitNewRows(rowCount);
                $('#divProperties').find('#AddRow').appendTo($('#divProperties').find('tr').last().find('td').last());
                $('.Properties').last().focus();
            },
            Rows: function (rowID) {
                var row = '<tr>';
                row += '<td></td>';
                row += '<td><input type="text" class=" Properties sfInputbox" id="properties__' + rowID + '" /></td>';
                row += '<td><div id="autocompleteValue_' + rowID + '"></div><input class="sfInputbox dataTypeInput" type="text" id="autocomplete_' + rowID + '" /><span class="sfBtn icon-arrow-slim-s" id="btnDrop_' + rowID + '"></span></td>';
                row += '<td align="center"><input type="checkbox" class="chkNull" /></td>';
                row += '<td><span class="sfDelete deleterow icon-delete"></span></td>';
                row += '</tr>';
                return row;
            },
            InitNewRows: function (rowID) {
                $("#autocomplete_" + rowID).autocomplete({
                    source: _source,
                    minLength: 0,
                    delay: 10,
                    autoFocus: true,
                    appendTo: '#autocompleteValue_' + rowID,
                    focus: function (event, ui) { },
                    open: function (event, ui) { // this event executes when the input type loss focus
                    }
                });

                $("#autocomplete_" + rowID).on('focus', function () {
                    $("#autocomplete_" + rowID).autocomplete("search", "");
                });
                $('#btnDrop_' + rowID).on('click', function () {
                    var $self = $(this);
                    $self.toggleClass('active');
                    if ($self.hasClass('active')) {
                        $("#autocomplete_" + rowID).autocomplete("search", "");
                    }
                    else {
                        $("#autocomplete_" + rowID).autocomplete("close");
                    }
                });
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: ModuleMaker.config.type,
                    contentType: ModuleMaker.config.contentType,
                    cache: ModuleMaker.config.cache,
                    async: ModuleMaker.config.async,
                    url: ModuleMaker.config.url,
                    data: ModuleMaker.config.data,
                    dataType: ModuleMaker.config.dataType,
                    success: ModuleMaker.ajaxSuccess,
                    error: ModuleMaker.ajaxFailure
                });
            },
        }
        ModuleMaker.init();
    }
    $.fn.ModuleMaker = function (p) {
        $.ModuleMaker(p);
    };
})(jQuery);