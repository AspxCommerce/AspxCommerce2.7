<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImportManager.ascx.cs" Inherits="Modules_AspxCommerce_AspxImportManager_ImportManager" %>

<script type="text/javascript">
    var ImportData = "";
    $(function () {
        $(".sfLocale").localize({
            moduleKey: ImportDataLan
        });
    });
    function aspxCommonObj() {
        var aspxCommonInfo = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            CultureName: AspxCommerce.utils.GetCultureName(),
            UserName: AspxCommerce.utils.GetUserName()
        };
        return aspxCommonInfo;
    }
    $(function () {
        ImportData = {
            variable: {
                fileName: ''
            },
            GetAttributeList: function () {
                $.ajax({
                    type: "POST",
                    url: aspxservicePath + "AspxImportExportHandler.ashx/GetAttributesListForImport",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $("#divAttributeList").html('');
                        var html = "<ul>";
                        $.each(msg.d, function (index, item) {
                            html += "<li><span class='cssClassLabel'>" + item.AttributeName + ":" + "</span>&nbsp<input type='text' size='5' validid=" + item.ValidationTypeID + " inputid=" + item.InputTypeID + " id=" + item.AttributeID + " autocomplete='on'></li>";
                        });
                        html += "</ul>";
                        $("#divAttributeList").append(html);
                    }
                });
            },
            GetExcelData: function (fileName) {
                $.ajax({
                    type: "POST",
                    url: aspxservicePath + "AspxImportExportHandler.ashx/GetExcelConnection",
                    data: JSON2.stringify({ fileName: fileName }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $("#divExcelHeader").html('');
                        var html = "<ul>";
                        $.each(msg.d, function (index, item) {
                            html += "<li><span id=" + (index + 1) + ">" + (index + 1) + ".</span>" + item + "</li>";
                        });
                        html += "</ul>";
                        $("#divExcelHeader").append(html);
                    },
                    error: function () {
                        csscody.error('<h2>' + getLocale(ImportDataLan, "Error Message") + '</h2><p>' + getLocale(ImportDataLan, 'External table is not in the expected format.') + '</p>');
                    }
                });
            },
            GetExcelCostVariantData: function () {
                $.ajax({
                    type: "POST",
                    url: aspxservicePath + "AspxImportExportHandler.ashx/GetExcelCostVariantData",
                    data: JSON2.stringify({ aspxCommonObj: aspxCommonObj(), fileName: ImportData.variable.fileName }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        csscody.alert('<h1>' + getLocale(ImportDataLan, 'Alert Message') + "</h1><p>" + getLocale(ImportDataLan, 'Cost variants data imported successfully') + '</p>');
                    },
                    error: function () {
                        csscody.error('<h1>' + getLocale(ImportDataLan, "Error Message") + "</h1><p>" + getLocale(ImportDataLan, 'Failed to get uploaded data') + '</p>');
                    }
                });
            },
            DataFileUploader: function () {
                var upload = new AjaxUpload($('#dataFileUploader'), {
                    action: aspxRootPath + 'Modules/AspxCommerce/AspxImportManager/DataFileUploadHandler.aspx',
                    name: 'myfile[]',
                    multiple: false,
                    data: "{}",
                    autoSubmit: true,
                    responseType: 'json',
                    onChange: function (file, ext) {
                    },
                    onSubmit: function (file, ext) {
                        if (ext != "exe") {
                            if (ext && /^(xls|xlsx|xlsm|xlsb|xltm|xlt)$/i.test(ext)) {
                                return true;
                            } else {
                                csscody.alert('<h1>' + getLocale(ImportDataLan, 'Alert Message') + "</h1><p>" + getLocale(ImportDataLan, 'Not a valid file!') + '</p>');
                                return false;
                            }
                        } else {
                            csscody.alert('<h1>' + getLocale(ImportDataLan, 'Alert Message') + "</h1><p>" + getLocale(ImportDataLan, 'Not a valid file!') + '</p>');
                            return false;
                        }
                    },
                    onComplete: function (file, response) {
                        var res = eval(response);
                        if (res.Message != null && res.Status > 0) {
                            ImportData.variable.fileName = res.Message;
                            ImportData.GetExcelData(res.Message);
                            $("#divAttributeList").show();
                            $("#divExcelHeader").show();
                            $(".cssClassButtonDiv").show();
                            return true;
                        } else {
                            csscody.error('<h1>' + getLocale(ImportDataLan, "Error Message") + "</h1><p>" + getLocale(ImportDataLan, 'Can not upload the file!') + '</p>');
                            return false;
                        }
                    }
                });
            },
            init: function () {
                ImportData.GetAttributeList();
                var fileName = "";
                ImportData.DataFileUploader();

                $("#btnMapping").click(function () {
                    var attrList = new Array();
                    var excelIds = new Array();
                    var finalArr = new Array();
                    $("#divAttributeList").find("ul li input[type=text]").each(function () {
                        if ($(this).val() != '') {
                            var attrData = {
                                AttributeID: $(this).prop('id'),
                                InputTypeID: $(this).prop('inputid'),
                                ValidationTypeID: $(this).prop('validid'),
                                MappingID: $(this).val()
                            };
                            attrList.push(attrData);
                        }
                    });
                    $("#divExcelHeader").find("ul li").each(function () {
                        var txt = $(this).text();
                        var subStr = txt.split(".");
                        var excelIDs = {
                            Header: subStr[1],
                            MappingID: subStr[0]
                        };
                        excelIds.push(excelIDs);
                    });
                    for (var i = 0; i < attrList.length; i++) {
                        for (var j = 0; j < excelIds.length; j++) {
                            if (attrList[i].MappingID == excelIds[j].MappingID) {
                                var finalList = {
                                    AttributeID: attrList[i].AttributeID,
                                    InputTypeID: attrList[i].InputTypeID,
                                    ValidationTypeID: attrList[i].ValidationTypeID,
                                    Header: excelIds[j].Header
                                };
                                finalArr.push(finalList);
                            }
                        }
                    }
                    $.ajax({
                        type: "POST",
                        url: aspxservicePath + "AspxImportExportHandler.ashx/GetExcelData",
                        data: JSON2.stringify({ finalInfo: finalArr, aspxCommonObj: aspxCommonObj(), fileName: ImportData.variable.fileName }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            csscody.alert('<h1>' + getLocale(ImportDataLan, 'Alert Message') + "</h1><p>" + getLocale(ImportDataLan, 'Imported items and categories successfully') + '</p>');
                        },
                        error: function () {
                            csscody.error('<h1>' + getLocale(ImportDataLan, "Error Message") + "</h1><p>" + getLocale(ImportDataLan, 'External table is not in the expected format.') + '</p>');
                        }
                    });
                });
                $('#btnBack').click(function () {
                    $("#divAttributeList").hide();
                    $("#divExcelHeader").html('').hide();
                    $(".cssClassButtonDiv").hide();
                });
            }
        },
        ImportData.init();
    });
</script>
<h1 class="sfLocale">Import Manager</h1>
<div id="fileUploader" class="sfFormwrapper">
    <label class="sfBold sfLocale">Select File: </label>
    <input id="dataFileUploader" class="cssClassBrowse" type="file" />
    <label id="filePath"></label>
    <div class="clear"></div>
</div>
<div id="divInfo">
    <div id="divExcelHeader" class="cssClassImportLeft" style="display: none">
    </div>
    <div id="divAttributeList" class="cssClassImportRight" style="display: none">
    </div>
    <div class="cssClassButtonDiv sfButtonwrapper" style="display: none">
        <%--<input type="button" value="Mapping" id="btnMapping" />
    <input type="button" value="Back" id="btnBack" />--%>
        <p>
            <button type="button" id="btnMapping" class="sfBtn">
                <span class="sfLocale icon-mapping">Mapping</span></button>
        </p>
        <br />
        <br />

        <p>
            <button type="reset" id="btnBack" class="sfBtn">
                <span class="sfLocale icon-arrow-slim-w">Back</span></button>
        </p>
    </div>
</div>
