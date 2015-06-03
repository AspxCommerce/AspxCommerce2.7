<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreBranchManage.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxStoreBranchesManagement_StoreBranchManage" %>

<script type="text/javascript">
    //<![CDATA[
    var StoreBranchManage = "";

    $(function () {
        var umi = '<%=UserModuleID%>';
        var aspxCommonObj = {
            StoreID: AspxCommerce.utils.GetStoreID(),
            PortalID: AspxCommerce.utils.GetPortalID(),
            UserName: AspxCommerce.utils.GetUserName(),
            CultureName: AspxCommerce.utils.GetCultureName()
        };
        var modulePath = '<%=modulePath %>';
        var maxFileSize = '<%=MaxFileSize%>';
        var progressTime = null;
        var progress = 0;
        var pcount = 0;
        var isUnique = false;
        var percentageInterval = [10, 20, 30, 40, 60, 80, 100];
        var timeInterval = [1, 2, 4, 2, 1, 5, 1];
        StoreBranchManage = {
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
                method: "",
                ajaxCallMode: 0
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: StoreBranchManage.config.type,
                    contentType: StoreBranchManage.config.contentType,
                    cache: StoreBranchManage.config.cache,
                    async: StoreBranchManage.config.async,
                    data: StoreBranchManage.config.data,
                    dataType: StoreBranchManage.config.dataType,
                    url: StoreBranchManage.config.url,
                    success: StoreBranchManage.ajaxSuccess,
                    error: StoreBranchManage.ajaxFailure
                });
            },
            BindStoreBranchDetails: function () {
                StoreBranchManage.config.method = "GetStoreBranchList";
                StoreBranchManage.config.data = { aspxCommonObj: aspxCommonObj };
                var data = this.config.data;
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvBranchManage_pagesize").length > 0) ? $("#gdvBranchManage_pagesize :selected").text() : 10;

                $("#gdvBranchManage").sagegrid({
                    url: StoreBranchManage.config.baseURL,
                    functionMethod: StoreBranchManage.config.method,
                    colModel: [
                        { display: getLocale(AspxStoreBranchesManagement, 'Store Branch ID'), name: 'store_branchID', cssclass: 'cssClassHeadCheckBox', coltype: 'checkbox', align: 'center', elemClass: 'StoreBranchChkbox', elemDefault: false, controlclass: 'itemsHeaderChkbox' },
                        { display: getLocale(AspxStoreBranchesManagement, 'Branch Name'), name: 'branch_name', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                        { display: getLocale(AspxStoreBranchesManagement, 'Branch Image'), name: 'branch_image', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                        { display: getLocale(AspxStoreBranchesManagement, 'Added On'), name: 'AddedOn', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left', type: 'date', format: 'yyyy/MM/dd', hide: false },
                        { display: getLocale(AspxStoreBranchesManagement, 'Actions'), name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
                    ],
                    buttons: [
                        { display: getLocale(AspxStoreBranchesManagement, 'Edit'), name: 'edit', enable: true, _event: 'click', trigger: '1', callMethod: 'StoreBranchManage.EditStoreBranch', arguments: '0,1,2,3,4,5' },
                        { display: getLocale(AspxStoreBranchesManagement, 'Delete'), name: 'delete', enable: true, _event: 'click', trigger: '2', callMethod: 'StoreBranchManage.DeleteStoreBranch', arguments: '' }
                    ],
                    rp: perpage,
                    nomsg: getLocale(AspxStoreBranchesManagement, "No Records Found!"),
                    param: data,
                    current: current_,
                    pnew: offset_,
                    sortcol: { 0: { sorter: false }, 4: { sorter: false } }
                });
            },
            EditStoreBranch: function (tblID, argus) {
                switch (tblID) {
                    case "gdvBranchManage":
                        $("#hdnStoreBranchID").val(argus[0]);
                        $("#txtBranchName").val(argus[4]);
                        $("#storeBranchIcon").html('');
                        if (argus[5] != null && argus[5] != "") {
                            $("#storeBranchIcon").html('<img src="' + aspxRootPath + "Modules/AspxCommerce/AspxStoreBranchesManagement/uploads/" + argus[5] + '" class="uploadImage" height="144px" width="256px"/><img src="' + aspxRootPath + 'Administrator/Templates/Default/images/imgdelete.png' + '" id="btnImageClose" alt="Delete" title="Delete" />');
                        }
                        StoreBranchManage.HideDiv();
                        $("#divAddBranch").show();
                        $("#btnImageClose").on('click', function () {
                            $("#storeBranchIcon").html('');
                        });
                        break;
                    default:
                        break;
                }
            },
            DeleteStoreBranch: function (tblID, argus) {
                switch (tblID) {
                    case "gdvBranchManage":
                        var properties = {
                            onComplete: function (e) {
                                StoreBranchManage.DeleteStoreBranchInfo(argus[0], e);
                            }
                        };
                        csscody.confirm("<h2>" + getLocale(AspxStoreBranchesManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxStoreBranchesManagement, "Are you sure you want to delete this store branch?") + "</p>", properties);
                        break;
                    default:
                        break;
                }
            },
            ConfirmDeleteMultipleStoreBranch: function (Ids, event) {
                StoreBranchManage.DeleteStoreBranchInfo(Ids, event);
            },
            DeleteStoreBranchInfo: function (storeBranchIds, event) {
                if (event) {
                    StoreBranchManage.config.url = StoreBranchManage.config.baseURL + "DeleteStoreBranches";
                    StoreBranchManage.config.data = JSON2.stringify({ storeBranchIds: storeBranchIds, aspxCommonObj: aspxCommonObj });
                    StoreBranchManage.config.ajaxCallMode = 3;
                    StoreBranchManage.ajaxCall(StoreBranchManage.config);
                    StoreBranchManage.BindStoreBranchDetails();
                }
                return false;
            },
            SaveAndUpdateStoreBranch: function () {
                var storeBranchID = $("#hdnStoreBranchID").val();
                var branchNm = $("#txtBranchName").val();
                var branchImage = '';
                if ($("#storeBranchIcon>img").length > 0) {
                    branchImage = $("#storeBranchIcon>img:eq(0)").prop("src").replace(aspxRootPath, "");
                    branchImage = branchImage.slice(branchImage.lastIndexOf('/') + 1);
                }
                StoreBranchManage.config.url = StoreBranchManage.config.baseURL + "SaveAndUpdateStorebranch";
                StoreBranchManage.config.data = JSON2.stringify({ branchName: branchNm, branchImage: branchImage, storeBranchId: storeBranchID, aspxCommonObj: aspxCommonObj });
                StoreBranchManage.config.ajaxCallMode = 1;
                StoreBranchManage.ajaxCall(StoreBranchManage.config);
            },
            ajaxSuccess: function (data) {
                switch (StoreBranchManage.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        csscody.info('<h2>' + getLocale(AspxStoreBranchesManagement, "Successful Message") + "</h2><p>" + getLocale(AspxStoreBranchesManagement, "Branch has been saved successfully") + '</p>');
                        StoreBranchManage.BindStoreBranchDetails();
                        StoreBranchManage.HideDiv();
                        $("#divShowStoreBranchGrid").show();
                        $("#validationLabel").html('');
                        break;
                    case 3:
                        isUnique = data.d;
                        if (data.d == true) {
                            $('#txtBranchName').removeClass('error');
                            $('#validationLabel').html('');
                        } else {
                            $('#txtBranchName').addClass('error');
                            $('#validationLabel').html(getLocale(AspxStoreBranchesManagement, 'This Branch name already exist!')).css("color", "red");
                            return false;
                        }
                        break;
                }
            },
            HideDiv: function () {
                $("#divShowStoreBranchGrid").hide();
                $("#divAddBranch").hide();
            },
            ClearForm: function () {
                $("#txtBranchName").val('');
                $("#storeBranchIcon").html('');
                $("#validationLabel").html('');
                $('#txtBranchName').removeClass('error');
                $(".error").html('');
            },
            ImageUploader: function () {
                var upload = new AjaxUpload($('#fileUpload'), {
                    action: modulePath + "StoreBranchFileUpload.aspx",
                    name: 'myfile[]',
                    multiple: false,
                    data: {},
                    autoSubmit: true,
                    responseType: 'json',
                    onChange: function (file, ext) {
                    },
                    onSubmit: function (file, ext) {
                        pcount = 0;
                        var percentage = $('.progress').find('.percentage');
                        var progressBar = $('.progress').find('.progressBar');
                        $('.progress').show();
                        StoreBranchManage.dummyProgress(progressBar, percentage);

                        if (ext != "exe") {
                            if (ext && /^(jpg|jpeg|jpe|gif|bmp|png|ico)$/i.test(ext)) {
                                this.setData({
                                    'MaxFileSize': maxFileSize
                                });
                            } else {
                                csscody.alert('<h2>' + getLocale(AspxStoreBranchesManagement, "Information Alert") + "</h2><p>" + getLocale(AspxStoreBranchesManagement, "Sorry! it is not a valid image type.") + '</p>');
                                return false;
                            }
                        } else {
                            csscody.alert('<h2>' + getLocale(AspxStoreBranchesManagement, "Information Alert") + "</h2><p>" + getLocale(AspxStoreBranchesManagement, "Sorry! it is not a valid image type") + '</p>');
                            return false;
                        }
                    },
                    onComplete: function (file, response) {
                        var res = eval(response);
                        if (res.Message != null && res.Status > 0) {
                            StoreBranchManage.AddNewImages(res);
                            return false;
                        } else {
                            csscody.error('<h2>' + getLocale(AspxStoreBranchesManagement, "Error Message") + '</h2><p>' + res.Message + '</p>');
                            return false;
                        }
                    }
                });
            },
            dummyProgress: function (progressBar, percentage) {
                if (percentageInterval[pcount]) {
                    progress = percentageInterval[pcount] + Math.floor(Math.random() * 10 + 1);
                    percentage.text(progress.toString() + '%');
                    progressBar.progressbar({
                        value: progress
                    });
                    var percent = percentage.text();
                    percent = percent.replace('%', '');
                    if (percent == 100 || percent > 100) {
                        percentage.text('100%');
                        $('.progress').hide();
                    }
                }
                if (timeInterval[pcount]) {
                    progressTime = setTimeout(function () {
                        StoreBranchManage.dummyProgress(progressBar, percentage);
                    }, timeInterval[pcount] * 10);
                }
                pcount++;
            },
            AddNewImages: function (response) {
                if (response.Message != null && response.Message != "") {
                    $("#storeBranchIcon").html('<img src="' + aspxRootPath + response.Message + '" class="uploadImage" height="144px" width="256px"/><img src="' + aspxRootPath + 'Administrator/Templates/Default/images/imgdelete.png' + '" id="btnImageClose" alt="Delete" title="Delete" />');
                }
                $("#btnImageClose").on('click', function () {
                    $("#storeBranchIcon").html('');
                });
            },
            CheckBranchNameUniquness: function (storeBranchID) {
                var branchName = $.trim($('#txtBranchName').val());
                StoreBranchManage.config.url = StoreBranchManage.config.baseURL + "CheckBranchNameUniqueness";
                StoreBranchManage.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, storeBranchId: storeBranchID, storeBranchName: branchName });
                StoreBranchManage.config.ajaxCallMode = 3;
                StoreBranchManage.ajaxCall(StoreBranchManage.config);
                return isUnique;
            },
            init: function () {
                StoreBranchManage.BindStoreBranchDetails();
                $("#hdnStoreBranchID").val(0);
                $("#btnSave").click(function () {
                    var v = $("#form1").validate({
                        messages: {
                            branchName: {
                                required: getLocale(AspxStoreBranchesManagement, 'Branch name required!'),
                                minlength: getLocale(AspxStoreBranchesManagement, "* (at least 2 chars)")
                            }
                        }
                    });
                    if (v.form() && StoreBranchManage.CheckBranchNameUniquness($("#hdnStoreBranchID").val())) {
                        StoreBranchManage.SaveAndUpdateStoreBranch();
                    } else {
                        return false;
                    }
                });
                $("#txtBranchName").bind('focusout', function () {
                    StoreBranchManage.CheckBranchNameUniquness($("#hdnStoreBranchID").val());
                });
                StoreBranchManage.HideDiv();
                StoreBranchManage.ClearForm();
                $("#divShowStoreBranchGrid").show();
                $("#btnAddNewStoreBranch").click(function () {
                    $("#hdnStoreBranchID").val(0);
                    StoreBranchManage.ClearForm();
                    StoreBranchManage.HideDiv();
                    $("#divAddBranch").show();
                    $("#validationLabel").html('');
                });
                $("#btnCancel").click(function () {
                    StoreBranchManage.HideDiv();
                    StoreBranchManage.ClearForm();
                    $("#divShowStoreBranchGrid").show();
                    $("#validationLabel").html('');
                });
                $("#btnImageClose").on('click', function () {
                    $("#storeBranchIcon").html('');
                });
                $('#btnDeleteSelected').click(function () {
                    var storeBranchIds = '';
                    storeBranchIds = SageData.Get("gdvBranchManage").Arr.join(',');
                    if (storeBranchIds.length > 0) {
                        var properties = {
                            onComplete: function (e) {
                                StoreBranchManage.ConfirmDeleteMultipleStoreBranch(storeBranchIds, e);
                            }
                        };
                        csscody.confirm("<h2>" + getLocale(AspxStoreBranchesManagement, "Delete Confirmation") + "</h2><p>" + getLocale(AspxStoreBranchesManagement, "Are you sure you want to delete the selected store branch?") + "</p>", properties);
                    } else {
                        csscody.alert('<h2>' + getLocale(AspxStoreBranchesManagement, "Information Alert") + "</h2><p>" + getLocale(AspxStoreBranchesManagement, "Please select at least one store branch before delete.") + '</p>');
                    }
                });
                StoreBranchManage.ImageUploader();
            }
        };
        StoreBranchManage.init();
    });
    //]]> 
</script>
<script type="text/javascript">
    //<![CDATA[
    $(function () {
        $(".sfLocale").localize({
            moduleKey: AspxStoreBranchesManagement
        });
    });
    //]]> 

</script>
<div id="divShowStoreBranchGrid">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h1>
                <asp:Label ID="lblTitleBranchManagement" runat="server" Text="Branch Management"
                    meta:resourcekey="lblTitleBranchManagementResource1"></asp:Label>
            </h1>
            <div class="cssClassHeaderRight">
                <div class="sfButtonwrapper">
                    <p>
                        <button type="button" id="btnAddNewStoreBranch" class="sfBtn">
                            <span class="sfLocale icon-addnew">Add New Branch</span></button>
                    </p>
                    <p>
                        <button type="button" id="btnDeleteSelected" class="sfBtn">
                            <span class="sfLocale icon-delete">Delete All Selected</span></button>
                    </p>

                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="sfGridwrapper">
            <div class="sfGridWrapperContent">
                <table id="gdvBranchManage" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divAddBranch" style="display: none">
    <div class="cssClassBodyContentWrapper">
        <div class="cssClassCommonBox Curve">
            <div class="cssClassHeader">
                <h1>
                    <label class="sfLocale">
                        Add Branch</label></h1>
            </div>
            <div class="sfFormwrapper">
                <table cellspacing="0" cellpadding="0" border="0" width="100%" class="cssClassPadding">
                    <tr>
                        <td width="10%">
                            <label class="sfFormlabel sfLocale">
                                Branch Name:</label>
                        </td>
                        <td>
                            <input type="text" id="txtBranchName" name="branchName" class="sfInputbox required" />
                            <label id="validationLabel"></label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="sfFormlabel sfLocale">
                                Branch Image</label>
                        </td>
                        <td>
                            <input type="file" id="fileUpload" />
                        </td>
                        <tr>
                            <td></td>
                            <td>
                                <div class="progress ui-helper-clearfix">
                                    <div class="progressBar" id="progressBar">
                                    </div>
                                    <div class="percentage">
                                    </div>
                                </div>
                                <div id="storeBranchIcon">
                                </div>
                            </td>
                        </tr>
                    </tr>
                </table>
            </div>
            <div class="sfButtonwrapper">
                <p>
                    <button type="button" id="btnSave" class="sfBtn">
                        <span class="sfLocale icon-save">Save</span></button>
                    <button type="button" id="btnCancel" class="sfBtn">
                        <span class="sfLocale icon-close">Cancel</span></button>
                </p>
                <div class="cssClassClear">
                </div>
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="hdnStoreBranchID" />

