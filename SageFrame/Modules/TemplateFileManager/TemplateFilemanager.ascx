<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TemplateFilemanager.ascx.cs"
    Inherits="Modules_FileManager_FileMgr" %>
<script type="text/javascript">
    var FileManager;
    $(function () {
        var editor = CodeMirror.fromTextArea(document.getElementById("txtEditFile"), { lineNumbers: true });
        var jcrop_api;
        FileManager = {

            config: {
                url: "WCF/ExtensionManagerWCF.svc/",
                uploadFolder: "/Modules/FileManagerFiles/",
                filepath: "/Modules/FileManagerFiles/",
                folderId: 0,
                toFolderId: 0,
                fileId: 0,
                destinationPath: "",
                mode: "normal",
                copyPath: "",
                address: "/",
                UserID: '<%=UserID%>',
                UserModuleID: '<%=UserModuleID%>',
                hostdirName: '<%=hostdirName%>',
                UserName: '<%=UserName%>',
                FileManagerControlArrayContainers: "#divControls li.upload,#divControls li.delete,#divControls li.delete_folder,#divControls li.copy,#divControls li.move,#divControls li.sfRefresh,#divControls li.permission,#divBottomControls",
                FileManagerControlArray: "#divControls li.upload,#divControls li.delete,#divControls li.delete_folder,#divControls li.copy,#divControls li.move,#divControls li.sfRefresh,#divControls li.permission,#imgAddFile,#imgAddFolder,#btnCopy,#imgDelFolder,#imgDelFile",
                PortalID: 1,
                ConfirmMode: "single",
                RowCount: 0,
                CurrentPage: 1,
                SelectedFolderID: 0

            },
            init: function () {
                FileManager.BindControlEvents();
                FileManager.LoadTree();
                FileManager.LoadImages();
                FileManager.BindEvents();
                FileManager.SetAddress();
                FileManager.InitializeToolTip();
                var TemplateName = FileManager.GetQuerySring()["tname"];
                setTimeout(function () {
                    $('#divFileTree ul li a').each(function (index) {
                        if ($(this).text() === TemplateName) {
                            $('#divFileTree ul li a').eq(index).trigger('click');
                            return false;

                        }
                    });
                }, 500);


            },
            InitCodeMirror: function () {
                var editor = CodeMirror.fromTextArea(document.getElementById("code"), {

                    lineNumbers: true

                });
                editor.setOption("theme", "night");
                var hlLine = editor.setLineClass(0, "activeline");
            },
            GetQuerySring: function () {
                var vars = [], hash;
                var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                for (var i = 0; i < hashes.length; i++) {
                    hash = hashes[i].split('=');
                    vars.push(hash[0]);
                    vars[hash[0]] = hash[1];
                }
                return vars;
            },

            BindControlEvents: function () {
                $('#divControls li.sfUpload').bind("click", function () {
                    if (FileManager.config.address == "/") {
                        SageFrame.messaging.show("Please Select a folder", "Alert");
                    }
                    else {
                        var param = JSON.stringify({
                            filePath: FileManager.config.destinationPath,
                            folderId: parseInt(FileManager.config.folderId),
                            userModuleID: parseInt(FileManager.config.UserModuleID),
                            userName: FileManager.config.UserName,
                            portalID: parseInt(SageFramePortalID),
                            secureToken: SageFrameSecureToken
                        });
                        var method = FileManagerPath + 'WebMethods/WebMethods.aspx/SetFilePath';
                        $.jsonRequest(method, param, FileManager.successFn);
                        FileManager.ShowPopUp("uploadFilePopUp");
                    }
                });
                $('#imgAddFile').bind("click", function () {
                    if (FileManager.config.address == "/") {
                        FileManager.ShowMessagePopUp("divMessagePopUp", "Please select a folder");
                    }
                    else {
                        var param = JSON.stringify({
                            filePath: FileManager.config.destinationPath,
                            folderId: parseInt(FileManager.config.folderId),
                            userModuleID: parseInt(FileManager.config.UserModuleID),
                            userName: FileManager.config.UserName,
                            portalID: parseInt(SageFramePortalID),
                            secureToken: SageFrameSecureToken
                        });
                        var method = FileManagerPath + 'WebMethods/WebMethods.aspx/SetFilePath';
                        $.jsonRequest(method, param, FileManager.successFn);
                        FileManager.ShowPopUp("uploadFilePopUp");
                    }
                });
                $('li.sfAddfolder').bind("click", function () {
                    if (FileManager.config.address == "/") {
                        SageFrame.messaging.show("Please Select a folder", "Alert");
                    }
                    else {
                        FileManager.ShowPopUp("newFolderPopUp");
                    }
                });
                $('#imgDelFile').bind("click", function () {

                    var count = 0;
                    $('#fileList tr').each(function (index) {
                        var checkbox = $(this).find("span[class='check']").find("input");
                        if ($(checkbox).prop("checked") == true) {
                            count++;
                        }
                    });
                    if (count > 0) {
                        FileManager.config.ConfirmMode = "multiple";
                        FileManager.ShowConfirmPopUp("Are you sure you want to delete?");
                    }
                    else {

                        SageFrame.messaging.show("No file is selected ", "Alert");
                    }

                });
                $('#imgDelFolder').bind("click", function () {
                    FileManager.config.ConfirmMode = "folder";
                    FileManager.ShowConfirmPopUp("Are you sure you want to delete?");
                });


                $('.closePopUp').bind("click", function () {
                    $('#fade , #popuprel , #popuprel2 , #popuprel3,#newFolderPopUp,#uploadFilePopUp,#divMessagePopUp,#divEditPopUp,#divSuccessPopUp,#divConfirmPopUp,#divCopyFiles').fadeOut();

                });
                $('#divControls li.sfDelete').bind("click", function () {
                    var count = 0;
                    $('#fileList tr').each(function (index) {
                        var checkbox = $(this).find("span[class='check']").find("input");
                        if ($(checkbox).prop("checked") == true) {
                            count++;
                        }
                    });
                    if (count > 0) {
                        FileManager.config.ConfirmMode = "multiple";
                        FileManager.ShowConfirmPopUp("Are you sure you want to delete?");
                    }
                    else {
                        SageFrame.messaging.show("No file is selected ", "Alert");

                    }
                });

                $('#divControls li.sfCopy').bind("click", function () {
                    var count = 0;
                    $('#fileList tr').each(function (index) {
                        var checkbox = $(this).find("span[class='check']").find("input");
                        if ($(checkbox).prop("checked") == true) {
                            count++;
                        }
                    });
                    if (count > 0) {
                        FileManager.config.mode = "copy";
                        FileManager.ShowCopyPopUp();
                    }
                    else {
                        SageFrame.messaging.show("No files selected ", "Alert");
                    }
                });

                $('#btnCopy').bind("click", function () {
                    $('#fileList tr').each(function (index) {
                        var checkbox = $(this).find("span[class='check']").find("input");

                        if ($(checkbox).prop("checked") == true) {

                            var _filePath = $(this).find("a.delete").prop("rel");
                            var _fromPath = FileManager.config.destinationPath;
                            var _toPath = FileManager.config.copyPath;
                            var destinationFoler = _toPath.replace('/', '\\');
                            var Filename = _filePath.substr(_filePath.lastIndexOf("\\") + 1);
                            var Directory = _filePath.replace(Filename, '');
                            var param = JSON.stringify({
                                filePath: _filePath,
                                fromPath: _fromPath,
                                toPath: _toPath,
                                userModuleID: parseInt(FileManager.config.UserModuleID),
                                userName: FileManager.config.UserName,
                                portalID: parseInt(SageFramePortalID),
                                secureToken: SageFrameSecureToken
                            });
                            var method = "";
                            if (FileManager.config.mode == "copy")
                                method = FileManagerPath + 'WebMethods/WebMethods.aspx/CopyFile';
                            else if (FileManager.config.mode == "move") {
                                method = FileManagerPath + 'WebMethods/WebMethods.aspx/MoveFile';
                            }
                            $.ajax({
                                type: "POST",
                                url: method,
                                contentType: "application/json; charset=utf-8",
                                data: param,
                                dataType: "json",
                                success: function (msg) {

                                    FileManager.ClosePopUp("#divCopyFiles");

                                    if (FileManager.config.mode == "copy") {
                                        SageFrame.messaging.show("Files copied successfully ", "Success");
                                    }
                                    else if (FileManager.config.mode == "move") {
                                        SageFrame.messaging.show("files moved successfully", "Success");
                                    }
                                    FileManager.BindPageSizeDropdown(destinationFoler);
                                    FileManager.config.CurrentPage = 1;

                                },
                                error: function (msg) { FileManager.errorFn(); }
                            });

                        }

                    });

                });
                ///move file
                $('#divControls li.sfMove').bind("click", function () {

                    var count = 0;
                    $('#fileList tr').each(function (index) {
                        var checkbox = $(this).find("span[class='check']").find("input");
                        if ($(checkbox).prop("checked") == true) {
                            count++;
                        }
                    });

                    if (count > 0) {
                        FileManager.config.mode = "move";
                        FileManager.ShowCopyPopUp();
                    }
                    else {
                        SageFrame.messaging.show("No files selected ", "Alert");
                    }
                });
                $('#divControls li.sfDeletefolder').bind("click", function () {
                    if (FileManager.config.address == "/") {
                        SageFrame.messaging.show("Please select a folder ", "Alert");
                    } else {
                        FileManager.config.ConfirmMode = "folder";
                        FileManager.ShowConfirmPopUp("Are you sure you want to delete?");
                    }
                });

                $('#btnUpdateFile').bind("click", function () {
                    FileManager.UpdateFile();
                    FileManager.ClosePopUp('#divEditPopUp');
                });

                $('#btnCreateFolder').bind("click", function () {
                    FileManager.CreateNewFolder();
                    FileManager.LoadTree();
                    $('#txtFolderName').val("");
                });
                $("#txtSearch").keydown(function () {
                    $('#lblInvalid').remove();
                });
                $('#imbSearch').bind("click", function () {
                    $('#divPagerNav').html('');
                    if ($('#divFileList').html() !== '') {
                        if ($('#txtSearch').val() !== "") {
                            FileManager.config.mode = "search";
                            FileManager.SearchFileList($('#txtSearch').val());
                        }
                        else {
                            $('#lblInvalid').remove();
                            $('#imbSearch').after("<label id='lblInvalid' class='sfError'><br/>Please enter search key word.</label>");
                        }
                    }
                    else {
                        SageFrame.messaging.show("No file to search ", "Alert");
                    }
                });

                $('span.sfClosepopup').bind("click", function () {
                    $('#fade , #popuprel , #popuprel2 , #popuprel3,#newFolderPopUp,#uploadFilePopUp,#divMessagePopUp,#divEditPopUp,#divSuccessPopUp,#divConfirmPopUp,#divCopyFiles,#divEditMode').fadeOut();
                    if ($('#divFileTree').find('a.cssClassTreeSelected').length > 0) {
                        $('#divFileTree').find('a.cssClassTreeSelected').trigger('click');
                        $('#divFileTree').find('a.cssClassTreeSelected').trigger('click');
                    }
                    //jcrop_api.release();
                });

                $('#fade').on("click", function () {
                    $('#fade , #popuprel , #popuprel2 , #popuprel3,#newFolderPopUp,#uploadFilePopUp,#divMessagePopUp,#divEditPopUp,#divSuccessPopUp,#divConfirmPopUp').fadeOut();
                    return false;
                });
                $('#divControls li.sfRefresh').bind("click", function () {
                    $('#divFileList').html('');
                    $('#txtAddress').val('/');
                    $('#divFileTree').html('');
                    $('#divPagerNav').html('');
                    $('#txtSearch').val('');
                    FileManager.LoadTree();
                    location.reload();
                });
                ///Confirmation events
                $('#btnConfirmYes').bind("click", function () {
                    switch (FileManager.config.ConfirmMode) {
                        case "single":
                            $('#divConfirmPopUp,#fade').fadeOut();
                            FileManager.DeleteFile(FileManager.config.filePath);
                            break;
                        case "multiple":
                            $('#divConfirmPopUp,#fade').fadeOut();
                            FileManager.DeleteFiles();
                            break;
                        case "folder":
                            $('#divConfirmPopUp,#fade').fadeOut();
                            FileManager.DeleteFolder();
                            break;

                        case "extract":
                            $('#divConfirmPopUp,#fade').fadeOut();
                            FileManager.ExtractFiles();
                            break;
                    }
                    $('#divConfirmPopUp,#fade').fadeOut();
                });
                $('#btnConfirmNo').bind("click", function () {
                    $('#divConfirmPopUp,#fade').fadeOut();
                });

                $('#ddlPageSize').bind("change", function () {
                    $('#divPagerNav,#divFileList').html('');
                    FileManager.config.CurrentPage = 1;

                    var currentNav = 1;
                    var pageSize = $('#ddlPageSize option:selected').val() == null ? 10 : $('#ddlPageSize option:selected').val();
                    var navCount = FileManager.config.RowCount / pageSize;

                    if (currentNav <= 10) {
                        FileManager.CreatePagerNavModel1();
                    }
                    else if (currentNav > 10 && currentNav < (navCount - 10)) {
                        FileManager.CreatePagerNavModel2();
                    }
                    else if (currentNav >= (navCount - 10) || currentNav == navCount) {
                        FileManager.CreatePagerNavModel3();
                    }
                    else {
                        FileManager.CreatePagerNavModel1();
                    }
                    if (FileManager.config.mode == "normal") {
                        FileManager.LoadFileList(FileManager.config.address);
                    }
                    else if (FileManager.config.mode = "search") {
                        FileManager.LoadSearchedFiles();
                    }
                    FileManager.HighlightSelectedPage();
                    if (FileManager.config.mode == "normal") {
                        FileManager.LoadFileList(FileManager.config.address);
                    }
                    else if (FileManager.config.mode = "search") {
                        FileManager.LoadSearchedFiles();
                    }
                    FileManager.CheckPagerNavVisibility();
                });

                $('#divPagerNav').on('click', 'ul li[class="nav"]', function () {
                    FileManager.config.CurrentPage = $(this).find("a").text();
                    var currentNav = $(this).find("a").text();
                    var pageSize = $('#ddlPageSize option:selected').val() == null ? 10 : $('#ddlPageSize option:selected').val();
                    var navCount = FileManager.config.RowCount / pageSize;

                    if (currentNav <= 10) {
                        FileManager.CreatePagerNavModel1();
                    }
                    else if (currentNav > 10 && currentNav < (navCount - 10)) {
                        FileManager.CreatePagerNavModel2();
                    }
                    else if (currentNav >= (navCount - 10) || currentNav == navCount) {
                        FileManager.CreatePagerNavModel3();
                    }
                    else {
                        FileManager.CreatePagerNavModel1();
                    }
                    if (FileManager.config.mode == "normal") {
                        FileManager.LoadFileList(FileManager.config.address);
                    }
                    else if (FileManager.config.mode = "search") {
                        FileManager.LoadSearchedFiles();
                    }
                    FileManager.HighlightSelectedPage();

                });



                $('#divPagerNav').on('click', 'ul li[class="prev"]', function () {
                    var currentNav = FileManager.config.CurrentPage;

                    if (currentNav > 1) {
                        FileManager.config.CurrentPage = parseInt(FileManager.config.CurrentPage) - 1;
                        var pageSize = $('#ddlPageSize option:selected').val() == null ? 10 : $('#ddlPageSize option:selected').val();
                        var navCount = FileManager.config.RowCount / pageSize;

                        if (currentNav <= 10) {
                            FileManager.CreatePagerNavModel1();
                        }
                        else if (currentNav > 10 && currentNav < (navCount - 10)) {
                            FileManager.CreatePagerNavModel2();
                        }
                        else if (currentNav >= (navCount - 10) || currentNav == navCount) {
                            FileManager.CreatePagerNavModel3();
                        }
                        else {
                            FileManager.CreatePagerNavModel1();
                        }
                        if (FileManager.config.mode == "normal") {
                            FileManager.LoadFileList(FileManager.config.address);
                        }
                        else if (FileManager.config.mode = "search") {
                            FileManager.LoadSearchedFiles();
                        }

                        FileManager.HighlightSelectedPage();
                    }
                });

                $('#divPagerNav').on('click', 'ul li[class="next"]', function () {
                    var currentNav = FileManager.config.CurrentPage;
                    var pageSize = $('#ddlPageSize option:selected').val() == null ? 10 : $('#ddlPageSize option:selected').val();
                    var navCount = FileManager.config.RowCount / pageSize;
                    if (currentNav < navCount) {
                        FileManager.config.CurrentPage = parseInt(FileManager.config.CurrentPage) + 1;

                        if (currentNav <= 10) {
                            FileManager.CreatePagerNavModel1();
                        }
                        else if (currentNav > 10 && currentNav < (navCount - 10)) {
                            FileManager.CreatePagerNavModel2();
                        }
                        else if (currentNav >= (navCount - 10) || currentNav == navCount) {
                            FileManager.CreatePagerNavModel3();
                        }
                        else {
                            FileManager.CreatePagerNavModel1();
                        }
                        if (FileManager.config.mode == "normal") {
                            FileManager.LoadFileList(FileManager.config.address);
                        }
                        else if (FileManager.config.mode = "search") {
                            FileManager.LoadSearchedFiles();
                        }
                        FileManager.HighlightSelectedPage();
                    }
                });

                $('#ddlPageSize').append('<option>10</option>');

            },
            LoadTree: function () {
                //$('#divFileTree').empty();
                $('#divFileTree').html('');
                $('#divFileTree').fileTree({
                    root: FileManager.GetRoot(),
                    script: FileManagerPath + 'js/script.aspx',
                    expandSpeed: 300,
                    collapseSpeed: 300,
                    multiFolder: false
                }, function (file) {
                    //alert(file);
                },
             function (dir) {

                 if (FileManager.config.mode == "search") {
                     FileManager.config.mode = "normal";
                 }

                 FileManager.RemoveSelected();
                 var directory = dir.substring(0, dir.indexOf('##'));

                 FileManager.config.SelectedFolderID = dir.substring(dir.indexOf('##') + 2, dir.length) == "" ? 0 : dir.substring(dir.indexOf('##') + 2, dir.length);
                 //Check for permission keys

                 if (FileManager.config.mode == "normal") {
                     FileManager.config.folderId = dir.substring(dir.indexOf('##') + 2, dir.length) == "" ? 0 : dir.substring(dir.indexOf('##') + 2, dir.length);
                     //set the current path to session
                     FileManager.config.destinationPath = directory;
                     FileManager.config.address = directory;
                     FileManager.BindPageSizeDropdown(directory);
                     FileManager.config.CurrentPage = 1;

                 }
                 else if (FileManager.config.mode == "copy" || FileManager.config.mode == "move") {
                     FileManager.config.copyPath = directory;
                     FileManager.config.toFolderId = dir.substring(dir.indexOf('##') + 2, dir.length) == "" ? 0 : dir.substring(dir.indexOf('##') + 2, dir.length);

                 }

                 FileManager.config.address = directory;
                 FileManager.SetAddress();
                 $('#divFileUpload').on("click", function () {
                     FileManager.ShowPopUp("uploadFilePopUp");
                 });

             });

            },
            HighlightSelectedPage: function () {
                var list = $('#divPagerNav ul li');
                var totalLength = list.length;
                $.each(list, function (index, item) {
                    if (FileManager.config.CurrentPage == $(this).find("a").text()) {
                        var self = $(this);
                        $(this).addClass("currentPage");
                        if (self.text() == 1) {
                            $('.prev').hide();
                        }
                        if ($(this).text() == totalLength - 2) {
                            $('.next').hide();
                        }
                    }
                    else {
                        $(this).removeClass("currentPage");
                    }
                });
            },
            DeleteFiles: function () {
                var flag = 0;
                $('#fileList tr').each(function (index) {
                    var checkbox = $(this).find("span[class='check']").find("input");
                    if ($(checkbox).prop("checked") == true) {

                        var _filePath = $(this).find("a.delete").prop("rel");
                        var Filename = _filePath.substr(_filePath.lastIndexOf("\\") + 1);
                        var Directory = _filePath.replace(Filename, '');
                        var param = JSON.stringify({
                            filePath: _filePath,
                            portalID: parseInt(SageFramePortalID),
                            userName: SageFrameUserName,
                            userModuleID: FileManager.config.UserModuleID,
                            secureToken: SageFrameSecureToken
                        });
                        var method = FileManagerPath + 'WebMethods/WebMethods.aspx/DeleteFile';
                        $.ajax({
                            type: "POST",
                            url: method,
                            contentType: "application/json; charset=utf-8",
                            data: param,
                            dataType: "json",
                            success: function (msg) {
                                if (flag == 0) {
                                    SageFrame.messaging.show("Files Deleted Successfully", "Success");
                                    flag++
                                }

                                FileManager.config.CurrentPage = 1;
                                FileManager.BindPageSizeDropdown(Directory);
                                //FileManager.ShowMessagePopUp("divMessagePopUp", msg.d);

                            },
                            error: function (msg) { SageFrame.messaging.show("Something went wrong! please try again", "Error"); }
                        });
                    }
                });
            },
            InitializeToolTip: function () {
                $('#divBottomControls li img').tooltip({ effect: 'fade', position: 'bottom center' });
            },
            CloseCopyPopUp: function () {
                $('#divCopyFiles,#divEditPopUp').fadeOut();
                FileManager.config.mode = "normal";
            },
            DeleteFolder: function () {
                ///Get the selected folder
                if (FileManager.config.address == "/") {
                    SageFrame.messaging.show("No Folder selected", "Alert");
                }
                else {
                    var selectedFolder = FileManager.config.uploadFolder;
                    var _folderId = FileManager.config.folderId;
                    var param = JSON.stringify({
                        filePath: selectedFolder,
                        folderId: parseInt(_folderId), fileId: 0,
                        portalID: parseInt(SageFramePortalID),
                        userName: SageFrameUserName,
                        userModuleID: FileManager.config.UserModuleID,
                        secureToken: SageFrameSecureToken
                    });


                    var method = FileManagerPath + 'WebMethods/WebMethods.aspx/DeleteFile';
                    $.ajax({
                        type: "POST",
                        url: method,
                        contentType: "application/json; charset=utf-8",
                        data: param,
                        dataType: "json",
                        success: function (msg) {
                            if ((msg.d) == "Folder Deleted Successfully")

                                SageFrame.messaging.show(msg.d, "Success");
                            else
                                SageFrame.messaging.show(msg.d, "Error");
                            FileManager.LoadTree();
                        },
                        error: function (msg) {
                            SageFrame.messaging.show("Something went wrong! please try again", "Error");
                        }
                    });
                }
            },
            GetMode: function () {

                var downloadPath = $('#lblFilepath').val();
                var FilePath = downloadPath.split("&");
                var imageName = FilePath[0].substring(FilePath[0].lastIndexOf("=") + 1);

                var Mode = "";
                var css = /([^\s]+(?=\.(css))\.\2)/gm;
                var xml = /([^\s]+(?=\.(html|xml))\.\2)/gm;
                if (imageName.match(css)) {

                    Mode = "text/css";
                }
                else if (imageName.match(xml)) {
                    Mode = "application/xml";
                }
                else {
                    Mode = "scheme";
                }
                return Mode;
            },
            BindEvents: function () {
                $('#lblSaveDocument').on("click", function () {
                    var downloadPath = $('#lblFilepath').val();
                    var FilePath = downloadPath.split("&");
                    var imageName = FilePath[0].substring(FilePath[0].lastIndexOf("=") + 1);
                    var RealFilePath = FilePath[1].substring(FilePath[1].lastIndexOf("=") + 1);
                    var AbsPath = RealFilePath.split('\\' + FileManager.config.hostdirName + '\\');
                    var RealAbsPath = "";
                    if (AbsPath.length > 1) {
                        RealAbsPath = AbsPath[1] + "/" + imageName;
                    }
                    else {
                        RealAbsPath = imageName;
                    }
                    var appPath = SageFrameAppPath;
                    RealAbsPath = RealAbsPath.replace('\\', '/');
                    var FilePath = SageFrameAppPath + '/' + RealAbsPath;
                    FilePath = FilePath.replace('\\', '/');
                    var UpdateValue = editor.getValue();
                    if (UpdateValue != "")
                        FileManager.UpdateFileContain(FilePath, UpdateValue);

                });
                $('#divTab  li').off().on("click", function () {
                    $('#txtAddress').val('/');
                    $('#lblInvalid').remove();
                    FileManager.config.address = '/';
                    $(this).addClass("sfActive");
                    $('#divTab li').not($(this)).removeClass("sfActive");
                    FileManager.LoadTree();
                    if ($(this).prop('id') == "System") {
                        FileManager.LoadFileList(FileManager.config.address);
                        //For paging.
                        FileManager.BindPageSizeDropdown(FileManager.config.address);
                    }
                    else {
                        $('#divFileList').html('');
                        $('#divPagerNav').html('');
                    }
                });
                $('#btnCrop').off().on("click", function () {

                    FileManager.CropImage($("#hdnPath").val());


                });
                $('#divFileList').off().on('click', 'tr td a.download_link span#spnEditImage', function () {
                    $('#lblFilepath').val($(this).attr('value'));
                    var downloadPath = $('#lblFilepath').val();
                    $('#downloadLink').prop('href', $('#lblFilepath').val());
                    $('#downloadFile').prop('href', $('#lblFilepath').val());
                    var FilePath = downloadPath.split("&");
                    var imageName = FilePath[0].substring(FilePath[0].lastIndexOf("=") + 1).toLowerCase();
                    var RealFilePath = FilePath[1].substring(FilePath[1].lastIndexOf("=") + 1);
                    var AbsPath = RealFilePath.split('\\' + FileManager.config.hostdirName + '\\');
                    var RealAbsPath = "";
                    if (AbsPath.length > 1) {
                        RealAbsPath = AbsPath[1] + "/" + imageName;
                    }
                    else {
                        RealAbsPath = imageName;
                    }

                    $('#txtTitle').text(imageName);
                    var html = "";
                    var appPath = SageFrameAppPath;
                    RealAbsPath = RealAbsPath.replace(/\\/g, '/');
                    var imageurl = SageFrameAppPath + '/' + RealAbsPath;
                    imageurl = imageurl.replace(/\\/g, '/');
                    var ShareLink = location.protocol + location.host + imageurl;
                    var imageRegex = /([^\s]+(?=\.(jpg|gif|png|jpeg))\.\2)/gm;
                    var fileRegex = /([^\s]+(?=\.(html|css|xml|ascx|js|config))\.\2)/gm;
                    var Mode = "";
                    var css = /([^\s]+(?=\.(css))\.\2)/gm;
                    var xml = /([^\s]+(?=\.(html|xml))\.\2)/gm;
                    if (imageName.match(css)) {
                        Mode = "text/css";
                    }
                    else if (imageName.match(xml)) {
                        Mode = "application/xml";
                    }
                    else {
                        Mode = "scheme";
                    }
                    if (imageName.match(imageRegex)) {
                        $('#divCropedImage').html('');
                        $("#originalImage").prop('src', '');
                        $("#originalImage").prop("src", imageurl);
                        $(".jcrop-holder img").prop("src", imageurl);
                        $("#hdnPath").val(imageurl);
                        $('#txtUrl').val(ShareLink);
                        FileManager.ShowPopUp("divEditMode");
                        $('#divEditFile').hide();
                        $('#divImages').show();

                        $('#originalImage').Jcrop({
                            onChange: showCoords,
                            onSelect: showCoords
                        },
                        function () {
                            jcrop_api = this;
                        }
                    );
                    }
                    else if (imageName.match(fileRegex)) {
                        FileManager.ReadFiles(imageurl, Mode);
                        FileManager.ShowPopUp("divEditMode");
                        $('#divImages').hide();
                        $('#divEditFile').show();
                        $('#txtShareFile').val(ShareLink);
                    }
                    else {
                        jAlert('File is not Editable', 'File Manage Alert');
                    }
                });

                $('#fileList ').on('click', 'tr a.download_link', function (e) {
                    var el = $(this).closest("li").prop("class");
                    if (el != "directory collapsed") {
                    }
                    else {
                        var path = $(this).prop("rel");
                        FileManager.LoadFileList(path);
                    }

                });

                $('#fileList tr a.delete').bind("click", function () {
                    var el = $(this).prop("rel");
                    FileManager.config.fileId = $(this).parents("tr").find("a[class='download_link']").prop("rel");
                    FileManager.config.ConfirmMode = "single";
                    FileManager.config.filePath = el;
                    FileManager.ShowConfirmPopUp("Are you sure you want to delete?");
                });

                $('#fileList tr a.edit').bind("click", function () {

                    FileManager.config.fileId = $(this).parents("tr").children("td").find("a.download_link").prop("rel");

                    var fileName = $(this).parents("tr").children("td").find("a.download_link").text();
                    var el = $(this).prop("rel");
                    var prop = $(this).parents("tr").children("td").find("span.info").children("span.prop").text();
                    FileManager.config.filePath = el;
                    FileManager.ShowEditPopUp(fileName, prop);


                });

                $('#fileList tr a.decompress').bind("click", function () {
                    FileManager.config.ConfirmMode = "extract";
                    FileManager.config.filepath = $(this).prop("rel");
                    FileManager.ShowConfirmPopUp("Do you want to extract the files?");
                });


                $('#fileList tr a.preview').lightBox({
                    imageBtnClose: FileManagerPath + "images/lightbox-btn-close.gif",
                    imageLoading: FileManagerPath + "images/lightbox-ico-loading.gif",
                    imageBtnNext: FileManagerPath + "images/lightbox-btn-next.gif",
                    imageBtnPrev: FileManagerPath + "images/lightbox-btn-prev.gif",
                    imageBlank: FileManagerPath + "images/lightbox-blank.gif"
                });


                $('#imgSort').bind("click", function () {
                    var fldrId = FileManager.config.folderId;
                    var pageSize = $('#ddlPageSize option:selected').val();
                    var param = JSON.stringify({
                        filePath: FileManager.config.uploadFolder,
                        folderId: parseInt(fldrId),
                        UserID: parseInt(FileManager.config.UserID),
                        IsSort: 1,
                        userModuleID: parseInt(FileManager.config.UserModuleID),
                        userName: FileManager.config.UserName,
                        CurrentPage: parseInt(FileManager.config.CurrentPage),
                        PageSize: parseInt(pageSize),
                        portalID: parseInt(SageFramePortalID),
                        secureToken: SageFrameSecureToken
                    });
                    var method = FileManagerPath + 'WebMethods/WebMethods.aspx/GetFileList';
                    $.jsonRequest(method, param, FileManager.BindFileList);
                });

                $('#chkSelectAll').bind("click", function () {
                    if ($(this).prop("checked") == true) {
                        $('#fileList tr').each(function (index) {
                            $(this).find("span[class='check']").find("input").prop("checked", true);
                        });
                    }
                    else {
                        $('#fileList tr').each(function (index) {
                            $(this).find("span[class='check']").find("input").prop("checked", false);
                        });
                    }

                });
            },

            CropImage: function (ImagePath) {

                var X1 = $('#x1').val();
                var Y1 = $('#y1').val();
                var X2 = $('#x2').val();
                var Y2 = $('#y2').val();
                var w = $('#w').val();
                var h = $('#h').val();

                var method = FileManagerPath + 'WebMethods/WebMethods.aspx/CropImage';
                var data = "";
                $.ajax({
                    type: "POST",
                    url: method,
                    contentType: "application/json; charset=utf-8",
                    data: JSON2.stringify({
                        ImagePath: ImagePath,
                        X1: parseInt(X1),
                        Y1: parseInt(Y1),
                        X2: parseInt(X2),
                        Y2: parseInt(X2),
                        w: parseInt(w),
                        h: parseInt(h),
                        userModuleID: parseInt(FileManager.config.UserModuleID),
                        userName: FileManager.config.UserName,
                        portalID: parseInt(SageFramePortalID),
                        secureToken: SageFrameSecureToken
                    }),
                    dataType: "json",
                    async: false,
                    success: function (msg) {
                        var CropImagePath = '';
                        CropImagePath = msg.d;
                        $('#divCropedImage').html(CropImagePath);

                        $('#divImages').show();
                        $('#divEditFile').hide();


                        jcrop_api.release();

                        $('#originalImage').Jcrop({
                            onChange: showCoords,
                            onSelect: showCoords

                        },
                        function () {
                            jcrop_api = this;
                        }
                        );
                    },
                    error: function (msg) { FileManager.errorFn(); }
                });
            },
            ReadFiles: function (FilePath, Mode) {
                var method = FileManagerPath + 'WebMethods/WebMethods.aspx/ReadFiles';
                var data = "";
                $.ajax({
                    type: "POST",
                    url: method,
                    contentType: "application/json; charset=utf-8",
                    data: JSON2.stringify({
                        FullPath: FilePath,
                        userModuleID: parseInt(FileManager.config.UserModuleID),
                        userName: FileManager.config.UserName,
                        portalID: parseInt(SageFramePortalID),
                        secureToken: SageFrameSecureToken
                    }),
                    dataType: "json",
                    async: false,
                    success: function (msg) {
                        $('#txtEditFile').val('');

                        // $('#txtEditFile').val(msg.d);
                        //var id = $('#txtEditFile');
                        var Mode = FileManager.GetMode();
                        editor.setValue('')
                        editor.setValue(msg.d)
                        editor.setOption("mode", Mode);


                        var hlLine = editor.setLineClass(0, "activeline");
                        $('#divImages').hide();
                        $('#divLibrary').hide();
                        $('#divEditFile').show();

                    },
                    error: function (msg) { FileManager.errorFn(); }
                });
            },
            UpdateFileContain: function (FilePath, UpdateValue) {
                var method = FileManagerPath + 'WebMethods/WebMethods.aspx/UpdateFileContain';
                var data = "";

                $.ajax({
                    type: "POST",
                    url: method,
                    contentType: "application/json; charset=utf-8",
                    data: JSON2.stringify({
                        FullPath: FilePath,
                        UpdateValue: UpdateValue,
                        userModuleID: parseInt(FileManager.config.UserModuleID),
                        userName: FileManager.config.UserName,
                        portalID: parseInt(SageFramePortalID),
                        secureToken: SageFrameSecureToken
                    }),
                    dataType: "json",
                    async: false,
                    success: function (msg) {
                        jAlert('Successfully Update.', '');
                    },
                    error: function (msg) { FileManager.errorFn(); }
                });
            },
            ExtractFiles: function () {
                var fileName = FileManager.config.filepath;
                var param = JSON.stringify({
                    FilePath: fileName,
                    FolderID: parseInt(FileManager.config.folderId),
                    userModuleID: parseInt(FileManager.config.UserModuleID),
                    userName: FileManager.config.UserName,
                    portalID: parseInt(SageFramePortalID),
                    secureToken: SageFrameSecureToken
                });
                var method = FileManagerPath + 'WebMethods/WebMethods.aspx/UnzipFiles';
                $.ajax({
                    type: "POST",
                    url: method,
                    contentType: "application/json; charset=utf-8",
                    data: param,
                    dataType: "json",
                    async: false,
                    success: function (msg) {
                        FileManager.LoadFileList(FileManager.config.uploadFolder);
                        if (msg.d.length == 0) {
                            SageFrame.messaging.show("File Extracted ", "Success");
                        }
                        else {
                            SageFrame.messaging.show(msg.d, "Alert");
                        }
                    },
                    error: function (msg) {
                        SageFrame.messaging.show("Extraction failed ", "Error");
                    }
                });
            },

            UpdateFile: function () {
                var fileName = $('#txtEditFileName').val();
                var _attrString = FileManager.GetAttrString();
                var param = JSON.stringify({
                    fileName: fileName, filePath: FileManager.config.filePath,
                    attrString: _attrString,
                    userModuleID: parseInt(FileManager.config.UserModuleID),
                    userName: FileManager.config.UserName,
                    portalID: parseInt(SageFramePortalID),
                    secureToken: SageFrameSecureToken
                });
                var method = FileManagerPath + 'WebMethods/WebMethods.aspx/UpdateFile';
                $.ajax({
                    type: "POST",
                    url: method,
                    contentType: "application/json; charset=utf-8",
                    data: param,
                    dataType: "json",
                    async: false,
                    success: function (msg) {
                        FileManager.LoadFileList(FileManager.config.uploadFolder);
                    },
                    error: function (msg) {
                        FileManager.ShowErrorPopUp("Could not update the file");
                    }
                });
            },
            GetAttrString: function () {

                var attrString = "";

                if ($('#chkArchive').prop("checked") == true) {
                    attrString += "A-";
                }
                if ($('#chkRead').prop("checked") == true) {
                    attrString += "R-";
                }
                if ($('#chkSystem').prop("checked") == true) {
                    attrString += "S-";
                }
                if ($('#chkHidden').prop("checked") == true) {
                    attrString += "H-";
                }
                attrString = attrString.substring(0, attrString.length - 1);
                return attrString;
            },

            RemoveSelected: function () {
                var tree = $('#divFileTree ul li');
                if (tree.length > 0) {
                    $.each(tree, function (index, item) {
                        $(this).find("a").removeClass("cssClassTreeSelected");
                        $(this).find("a").removeClass("cssClassTreeSelectedDB");
                        $(this).find("a").removeClass("cssClassTreeSelectedLocked");
                    });
                }

            },

            ClearChecks: function () {
                var checks = $('.ulPermission li ul[class="header"] li[class="check"]');
                $.each(checks, function (i, itm) {
                    var role = $(this).parent().find("li[class='role']").text().toLowerCase().replace(/ /g, '');
                    if (role != "superuser" && role != "siteadmin")
                        $(this).find("input").prop("checked", false);

                });

            },
            GetRoot: function () {
                var tab = $('#divTab  li');
                var getFolders = "";
                var folderPath = "";
                $.each(tab, function (index, item) {
                    if ($(this).hasClass("sfActive")) {
                        getFolders = $(this).prop('id');
                    }
                });

                if (getFolders == "Template") {
                    folderPath = "/Templates/";
                }
                if (getFolders == "Module") {

                    folderPath = "/Modules/";
                }
                if (getFolders == "System") {
                    folderPath = "/";
                }
                return folderPath;
            },
            AddSelectStyle: function () {
                var folderId = FileManager.config.folderId;
            },
            LoadFileList: function (_fileName) {
                FileManager.config.uploadFolder = _fileName;
                var fldrId = FileManager.config.SelectedFolderID;
                if (FileManager.config.mode == "copy" || FileManager.config.mode == "move") {
                    fldrId = FileManager.config.toFolderId;
                }

                var pageSize = $('#ddlPageSize option:selected').val() == null ? 10 : $('#ddlPageSize option:selected').val();
                var param = JSON.stringify({
                    filePath: _fileName,
                    folderId: parseInt(fldrId),
                    UserID: parseInt(FileManager.config.UserID),
                    IsSort: 0,
                    CurrentPage: parseInt(FileManager.config.CurrentPage),
                    PageSize: parseInt(pageSize),
                    userModuleID: parseInt(FileManager.config.UserModuleID),
                    userName: FileManager.config.UserName,
                    portalID: parseInt(SageFramePortalID),
                    secureToken: SageFrameSecureToken
                });

                var method = FileManagerPath + 'WebMethods/WebMethods.aspx/GetFileList';
                $.jsonRequest(method, param, FileManager.BindFileList);
            },
            SearchFileList: function (_SearchQuery) {
                $('#ddlPageSize').html('');
                $('#divPagerNav').html('');
                var method = FileManagerPath + 'WebMethods/WebMethods.aspx/GetSearchCount';
                $.ajax({
                    type: "POST",
                    url: method,
                    contentType: "application/json; charset=utf-8",
                    data: JSON2.stringify({
                        SearchQuery: _SearchQuery,
                        FilePath: $('#txtAddress').val(),
                        userModuleID: parseInt(FileManager.config.UserModuleID),
                        userName: FileManager.config.UserName,
                        portalID: parseInt(SageFramePortalID),
                        secureToken: SageFrameSecureToken
                    }),
                    dataType: "json",
                    async: false,
                    success: function (msg) {
                        FileManager.config.RowCount = msg.d;
                        var html = '';
                        if (msg.d < 10) {
                            html += '<option>10</option>';
                        }
                        else {
                            for (var i = 0; i <= FileManager.config.RowCount; i += 10) {
                                if (i == 0) {
                                    //html += '<option>All</option>';
                                }
                                else if (i < 75) {
                                    html += '<option>' + i + '</option>';
                                }
                            }
                        }
                        $('#ddlPageSize').append(html);
                        FileManager.CreatePagerNavModel1();
                        FileManager.LoadSearchedFiles(_SearchQuery);
                    },
                    error: function (msg) { jAlert('Error', 'Error'); }
                });
            },
            LoadSearchedFiles: function () {
                var _UserModuleID = FileManager.config.UserModuleID;
                var _UserName = FileManager.config.UserName;
                var _SearchQuery = $('#txtSearch').val();
                var pageSize = $('#ddlPageSize option:selected').val() == null ? 10 : $('#ddlPageSize option:selected').val();
                var param = JSON.stringify({
                    SearchQuery: _SearchQuery,
                    CurrentPage: parseInt(FileManager.config.CurrentPage),
                    PageSize: parseInt(pageSize), FilePath: $('#txtAddress').val(),
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: FileManager.config.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                var method = FileManagerPath + 'WebMethods/WebMethods.aspx/SearchFiles';
                $.jsonRequest(method, param, FileManager.BindFileList);

            },
            BindFileList: function (data) {
                $('#divFileList').html('');
                $('#lblInvalid').remove();
                $('#divFileList').append(data);
                FileManager.BindEvents();
            },
            DeleteFile: function (path) {
                var Filename = path.substr(path.lastIndexOf("\\") + 1);
                var Directory = path.replace(Filename, '');
                var param = JSON.stringify({
                    filePath: path,
                    portalID: parseInt(SageFramePortalID),
                    userName: SageFrameUserName,
                    userModuleID: FileManager.config.UserModuleID,
                    secureToken: SageFrameSecureToken
                });
                var method = FileManagerPath + 'WebMethods/WebMethods.aspx/DeleteFile';
                $.ajax({
                    type: "POST",
                    url: method,
                    contentType: "application/json; charset=utf-8",
                    data: param,
                    dataType: "json",
                    async: false,
                    success: function (msg) {
                        SageFrame.messaging.show("File Deleted Successfully.", "Success");
                        FileManager.config.CurrentPage = 1;
                        FileManager.BindPageSizeDropdown(Directory);
                    },
                    error: function (msg) { FileManager.errorFn(); }
                });
            },
            errorFn: function () {
                SageFrame.messaging.show("Something went wrong! Please try again.", "Error");
            },
            successFn: function () {
            },
            ClosePopUp: function (divId) {
                $(divId).fadeOut();
                $('#fade , #popuprel , #popuprel2 , #popuprel3,#newFolderPopUp,#uploadFilePopUp,#divMessagePopUp,#divEditPopUp,#divConfirmPopUp').fadeOut();
            },
            ShowCopyPopUp: function () {
                $('#divCopyFiles').fadeIn();
                var popuptopmargin = ($('#divCopyFiles').height() + 10) / 2;
                var popupleftmargin = ($('#divCopyFiles').width() + 10) / 2;
                $('#divCopyFiles').css({
                    'margin-top': -popuptopmargin,
                    'margin-left': -popupleftmargin
                });
                if (FileManager.config.mode == "copy") {
                    $('#hAction').text("Copy Files");
                    $('#copyMessage').text("Select the target folder you want to copy to");
                    $('#btnCopy').prop("value", "Copy");
                }
                else if (FileManager.config.mode = "move") {
                    $('#hAction').text("Move Files");
                    $('#copyMessage').text("Select the target folder you want to move to.");
                    $('#btnCopy').prop("value", "Move");
                }
            },
            ShowEditPopUp: function (fileName, prop) {
                FileManager.ShowPopUp("divEditPopUp");
                $('#txtEditFileName').val(fileName);
                if (prop.indexOf("A") > -1) {
                    $('#chkArchive').prop("checked", true);
                }
                if (prop.indexOf("R") > -1) {
                    $('#chkRead').prop("checked", true);
                }
                if (prop.indexOf("S") > -1) {
                    $('#chkSystem').prop("checked", true);
                }
                if (prop.indexOf("H") > -1) {
                    $('#chkHidden').prop("checked", true);
                }

            },
            CreateNewFolder: function () {
                var _folderName = $('#txtFolderName').val();
                var _fileType = 0;
                var param = JSON.stringify({
                    FolderID: parseInt(FileManager.config.folderId),
                    filePath: FileManager.config.uploadFolder + _folderName,
                    folderName: _folderName, fileType: parseInt(_fileType),
                    userModuleID: parseInt(FileManager.config.UserModuleID),
                    userName: FileManager.config.UserName,
                    portalID: parseInt(SageFramePortalID),
                    secureToken: SageFrameSecureToken
                });
                var method = FileManagerPath + 'WebMethods/WebMethods.aspx/CreateFolder';
                $.ajax({
                    type: "POST",
                    url: method,
                    contentType: "application/json; charset=utf-8",
                    data: param,
                    dataType: "json",
                    success: function (msg) {
                        FileManager.LoadFileList(FileManager.config.uploadFolder);
                        SageFrame.messaging.show("Folder successfully created.", "Success");

                    },
                    error: function (msg) { FileManager.errorFn(); }
                });

                FileManager.BindEvents();
                FileManager.ClosePopUp("#newFolderPopUp");
            },
            ImageUploader: function (successCase) {
                var upload = new AjaxUpload($('#fileUpload'), {
                    action: FileManagerPath + 'FileUploadHandler.aspx?userModuleId=' + parseInt(FileManager.config.UserModuleID),
                    name: 'myfile[]',
                    multiple: false,
                    data: {},
                    autoSubmit: true,
                    responseType: 'json',
                    async: false,
                    onChange: function (file, ext) {

                    },
                    onSubmit: function (file, ext) {
                        var data = FileManager.GetValidExtensions();
                        if (data.indexOf(ext.toLowerCase()) > -1) {

                            $("#uploadFilePopUp,#fade").hide();
                        }
                        else {
                            FileManager.ClosePopUp("#uploadFilePopUp");
                            SageFrame.messaging.show("Select a valid file.", "Alert");
                            //  FileManager.ShowMessagePopUp("divMessagePopUp", "Not a valid file");
                            return false;
                        }
                    },
                    onComplete: function (file, response) {
                        SageFrame.messaging.show("File successfully uploaded.", "Success");
                        FileManager.config.mode = "normal";
                        FileManager.BindPageSizeDropdown($('#txtAddress').val());
                    }
                });
            },
            GetValidExtensions: function () {
                var method = FileManagerPath + 'WebMethods/WebMethods.aspx/GetExtensions';
                var param = JSON2.stringify({
                    userModuleID: parseInt(FileManager.config.UserModuleID),
                    userName: FileManager.config.UserName,
                    portalID: parseInt(SageFramePortalID),
                    secureToken: SageFrameSecureToken
                });
                var data = "";
                $.ajax({
                    type: "POST",
                    url: method,
                    contentType: "application/json; charset=utf-8",
                    data: param,
                    dataType: "json",
                    async: false,
                    success: function (msg) {
                        data = msg.d;
                    },
                    error: function (msg) { FileManager.errorFn(); }
                });
                return data;
            },
            SetAddress: function () {
                $('#txtAddress').val(FileManager.config.address);
            },
            LoadImages: function () {
                $('#imbUpload').prop("src", FileManagerPath + "images/btnUpload.png");
                $('#imgDeleteFile').prop("src", FileManagerPath + "images/delete_page.png");
                $('#imgDeleteFolder').prop("src", FileManagerPath + "images/delete_folder.png");
                $('#imgCopy').prop("src", FileManagerPath + "images/Copy-icon.png");
                $('#imgMove').prop("src", FileManagerPath + "images/lnkrestart.png");
                $('#imgRefresh').prop("src", FileManagerPath + "images/btnRefresh.png");
                // $('#imgSync').prop("src", FileManagerPath + "images/sync.png");
                //$('#imbSearch').prop("src", FileManagerPath + "images/search.png");
                $('#imgPermission').prop("src", FileManagerPath + "images/btnsetting.png");
                $('#imgAddFile').prop("src", FileManagerPath + "images/addfile.png");
                $('#imgAddFolder').prop("src", FileManagerPath + "images/addfolder.png");
                $('#imgDelFile').prop("src", FileManagerPath + "images/deletefile.png");
                $('#imgDelFolder').prop("src", FileManagerPath + "images/deletefolder.png");
                $('#imgCancelCopy,#imgCancelEdit,#imgNewFolder,#imgUpload,#imgMessage,#imgConfirmDelete,#imgSuccessClose,#imgErrorClose,#imgEditPopUp').prop("src", FileManagerPath + "images/cancel.png");


            },
            ShowPopUp: function (popupid) {

                $('#' + popupid).fadeIn();
                $('body').append('<div id="fade"></div>');
                $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn();
                var popuptopmargin = ($('#' + popupid).height() + 10) / 2;
                var popupleftmargin = ($('#' + popupid).width() + 10) / 2;
                $('#' + popupid).css({
                    'margin-top': -popuptopmargin,
                    'margin-left': -popupleftmargin
                });

                switch (popupid) {
                    case "uploadFilePopUp":
                        FileManager.ImageUploader("uploadFilePopUp");
                        break;
                }

            },
            ShowMessagePopUp: function (popupid, message) {

                $('#' + popupid).show();
                $('body').append('<div id="fade"></div>');
                $('#fade').css({ 'filter': 'alpha(opacity=80)' }).show();
                var popuptopmargin = ($('#' + popupid).height() + 10) / 2;
                var popupleftmargin = ($('#' + popupid).width() + 10) / 2;
                $('#' + popupid).css({
                    'margin-top': -popuptopmargin,
                    'margin-left': -popupleftmargin
                });

                $('.cssClassMessage').text(message);

            },
            ShowConfirmPopUp: function (message) {

                $('#divConfirmPopUp').show();
                $('body').append('<div id="fade"></div>');
                $('#fade').css({ 'filter': 'alpha(opacity=80)' }).show();
                var popuptopmargin = ($('#divConfirmPopUp').height() + 10) / 2;
                var popupleftmargin = ($('#divConfirmPopUp').width() + 10) / 2;
                $('#divConfirmPopUp').css({
                    'margin-top': -popuptopmargin,
                    'margin-left': -popupleftmargin
                });
                $('.sfConfirmmsg').text(message);

            },
            ShowSuccessPopUp: function (message) {

                $('#divSuccessPopUp').show();
                $('body').append('<div id="fade"></div>');
                $('#fade').css({ 'filter': 'alpha(opacity=80)' }).show();
                var popuptopmargin = ($('#divSuccessPopUp').height() + 10) / 2;
                var popupleftmargin = ($('#divSuccessPopUp').width() + 10) / 2;
                $('#divSuccessPopUp').css({
                    'margin-top': -popuptopmargin,
                    'margin-left': -popupleftmargin
                });

                $('.cssClassSuccessMessage').text(message);

                $('#divSuccessPopUp').delay(1000).hide();
                $('#fade').delay(1000).fadeOut();

            },
            ShowErrorPopUp: function (message) {

                $('#divErrorPopUp').show();
                $('body').append('<div id="fade"></div>');
                $('#fade').css({ 'filter': 'alpha(opacity=80)' }).show();
                var popuptopmargin = ($('#divErrorPopUp').height() + 10) / 2;
                var popupleftmargin = ($('#divErrorPopUp').width() + 10) / 2;
                $('#divErrorPopUp').css({
                    'margin-top': -popuptopmargin,
                    'margin-left': -popupleftmargin
                });

                $('.cssClassErrorMessage').text(message);

                $('#divErrorPopUp').delay(1000).hide();
                $('#fade').delay(1000).hide();

            },
            ///Pager area
            CreatePagerNavModel1: function () {
                $('#divPagerNav').html('');
                var pageSize = $('#ddlPageSize option:selected').val() == null ? 10 : $('#ddlPageSize option:selected').val();
                if (FileManager.config.RowCount > pageSize) {
                    var navCount = FileManager.config.RowCount / pageSize;
                    var pageCount = navCount * pageSize
                    navCount = parseInt(navCount);
                    if (FileManager.config.RowCount > (navCount * pageSize)) {
                        navCount++;
                    }
                    if (navCount > 15) {
                        var html = "<ul class='simplePagerNav'>";
                        html += '<li class="prev"><a href="#"><</a></li>';
                        html += '<li class="nav"><a href="#">1</a></li>';
                        html += '<li class="nav"><a href="#">2</a></li>';
                        for (var i = 3; i <= 15; i++) {
                            html += '<li class="nav"><a href="#">' + i + '</a></li>';
                        }
                        html += '<li  class="MoreNav"><a href="#">.........</a></li>';
                        html += '<li class="nav"><a href="#">' + parseInt(navCount - 2) + '</a></li>';
                        html += '<li class="nav"><a href="#">' + parseInt(navCount - 1) + '</a></li>';
                        html += '<li class="next"><a href="#">></a></li>';
                        html += '</ul>';
                    }
                    else {
                        var html = "<ul class='simplePagerNav'>";
                        html += '<li class="prev"><a href="#"><</a></li>';
                        for (var i = 1; i <= navCount; i++) {
                            html += '<li class="nav"><a href="#">' + i + '</a></li>';
                        }
                        html += '<li class="next"><a href="#">></a></li>';
                        html += '</ul>';
                    }

                    $('#divPagerNav').append(html);
                }
                FileManager.CheckPagerNavVisibility();
                FileManager.HighlightSelectedPage();
            },
            CreatePagerNavModel2: function () {
                $('#divPagerNav').html('');
                var pageSize = $('#ddlPageSize option:selected').val() == null ? 10 : $('#ddlPageSize option:selected').val();

                if (FileManager.config.RowCount > pageSize) {
                    var navCount = FileManager.config.RowCount / pageSize;
                    var pageCount = navCount * pageSize
                    navCount = parseInt(navCount);
                    if (FileManager.config.RowCount > (navCount * pageSize)) {
                        navCount++;
                    }
                    var currentPage = FileManager.config.CurrentPage;
                    var startIndex = currentPage - 5;
                    var endIndex = parseInt(currentPage) + 5;
                    var html = "<ul class='simplePagerNav'>";
                    html += '<li class="prev"><a href="#"><</a></li>';
                    html += '<li class="nav"><a href="#">1</a></li>';
                    html += '<li class="nav"><a href="#">2</a></li>';
                    html += '<li  class="MoreNav"><a href="#">.........</a></li>';
                    for (var i = startIndex; i <= endIndex; i++) {
                        html += '<li class="nav"><a href="#">' + i + '</a></li>';
                    }
                    html += '<li  class="MoreNav"><a href="#">.........</a></li>';
                    html += '<li class="nav"><a href="#">' + parseInt(navCount - 2) + '</a></li>';
                    html += '<li class="nav"><a href="#">' + parseInt(navCount - 1) + '</a></li>';
                    html += '<li class="next"><a href="#">></a></li>';
                    html += '</ul>';



                    $('#divPagerNav').append(html);
                }
                FileManager.CheckPagerNavVisibility();
                FileManager.HighlightSelectedPage();
            },
            CreatePagerNavModel3: function () {
                $('#divPagerNav').html('');
                var pageSize = $('#ddlPageSize option:selected').val() == null ? 10 : $('#ddlPageSize option:selected').val();

                if (FileManager.config.RowCount > pageSize) {
                    var navCount = FileManager.config.RowCount / pageSize;
                    var pageCount = navCount * pageSize
                    navCount = parseInt(navCount);
                    if (FileManager.config.RowCount > (navCount * pageSize)) {
                        navCount++;
                    }
                    var currentPage = FileManager.config.CurrentPage;
                    var startIndex = navCount - 15;
                    var endIndex = navCount;

                    var html = "<ul class='simplePagerNav'>";
                    html += '<li class="prev"><a href="#"><</a></li>';
                    html += '<li class="nav"><a href="#">1</a></li>';
                    html += '<li class="nav"><a href="#">2</a></li>';
                    html += '<li  class="MoreNav"><a href="#">.........</a></li>';
                    for (var i = startIndex; i <= endIndex; i++) {
                        html += '<li class="nav"><a href="#">' + i + '</a></li>';
                    }

                    html += '<li class="next"><a href="#">></a></li>';
                    html += '</ul>';
                    $('#divPagerNav').append(html);

                }
                FileManager.HighlightSelectedPage();
            },
            BindPageSizeDropdown: function (FilePath) {
                $('#ddlPageSize').html('');
                var method = FileManagerPath + 'WebMethods/WebMethods.aspx/GetCount';

                $.ajax({
                    type: "POST",
                    url: method,
                    contentType: "application/json; charset=utf-8",
                    data: JSON2.stringify({
                        FilePath: FilePath,
                        portalID: parseInt(SageFramePortalID),
                        userName: SageFrameUserName,
                        userModuleID: FileManager.config.UserModuleID,
                        secureToken: SageFrameSecureToken
                    }),
                    dataType: "json",
                    success: function (msg) {
                        FileManager.config.RowCount = msg.d;
                        var html = '';
                        var total = FileManager.config.RowCount;
                        var loop = total / 10;
                        var reminder = total % 10;
                        var roundValue = Math.round(loop / 10);
                        if (reminder < 5) {
                            loop = loop + 1;
                        }
                        for (var i = 1; i <= loop; i++) {
                            html += '<option>' + i * 10 + '</option>';
                        }
                        if (loop < 1 && roundValue == 0) {
                            html += '<option>' + 10 + '</option>';
                        }
                        $('#ddlPageSize').append(html);
                        FileManager.LoadFileList(FileManager.config.address);
                        FileManager.config.mode = "normal";
                        FileManager.CreatePagerNavModel1();
                    },
                    error: function (msg) { jAlert('Error', 'Error'); }
                });
            },
            CheckPagerNavVisibility: function () {
                var pageSize = $('#ddlPageSize option:selected').val();
                var status = parseInt(pageSize) * parseInt(FileManager.config.CurrentPage);
                if (FileManager.config.CurrentPage === 1) {
                    if (status > FileManager.config.RowCount || status == FileManager.config.RowCount) {
                        $('#divPagerNav').html('');
                    }
                }
            }

        };
        FileManager.init();

    });

    (function ($) {
        $.jsonRequest = function (url, para, successFn) {
            $.ajax({
                type: "POST",
                url: url,
                cache: true,
                contentType: "application/json; charset=utf-8",
                data: para,
                dataType: "json",
                success: function (msg) {
                    successFn(msg.d);
                },
                error: function (msg) { FileManager.errorFn(); },
                async: false
            });
        }
    })(jQuery);

</script>
<h1>Template File Manager</h1>
<div class="sfFilemanageholder sfMargintop">
    <div id="divFileTreeOuter">
        <div id="divTab">
            <ul class="sfTab clearfix">
                <li id="Template" class="sfActive">
                    <label class="sfFormlabel">
                        Template</label></li>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div id="divFileTree">
        </div>
    </div>
    <div class="sfRightcol sfContentWrap">
        <div class="sfContent">
            <div class="clearfix sfFormwrapper sfPadding sfBorderNone">
                <div id="divControls">
                    <ul>
                        <li class="sfUpload icon-add-file sfBtn">Add File</li>
                        <li class="sfDelete icon-delete-file sfBtn">Delete File</li>
                        <li class="sfAddfolder icon-add-folder sfBtn">Add Folder</li>
                        <li class="sfDeletefolder icon-delete-folder sfBtn">Delete Folder</li>
                        <li class="sfCopy icon-copy sfBtn">Copy</li>
                        <li class="sfMove icon-move sfBtn">Move</li>
                        <li class="sfRefresh icon-refresh sfBtn">Refresh</li>
                    </ul>
                    <div class="clear">
                    </div>
                </div>
                <div id="divSearch" class="sfTableOption clearfix">
                    <div class="sfLeftdiv">
                        <table border="0" cellspacing="0" cellpadding="0" class="sfFloatleft">
                            <tr>
                                <td>
                                    <label class="sfFormlabel">
                                        Address:</label>
                                </td>
                                <td>
                                    <input type="text" id="txtAddress" class="sfInputbox" disabled="disabled" />
                                </td>
                                <td>
                                    <label class="sfFormlabel">
                                        Search:</label>
                                </td>
                                <td>
                                    <input type="text" id="txtSearch" class="sfInputbox sfAuto sfFloatLeft" />
                                    <label id="imbSearch" class="icon-search sfBtn">
                                    </label>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                    <div class="sfRightdiv">
                        <label class="sfFormlabel">
                            Items Per Page:</label>
                        <select id="ddlPageSize" class="sfListmenu sfAuto">
                        </select>
                    </div>
                </div>
            </div>
            <div id="divFileList">
            </div>
            <div id='divPagerNav' class="sfPagination clearfix">
            </div>
        </div>
    </div>
</div>
<div id="uploadFilePopUp" class="popupbox sfFormwrapper" style="height: 150px; width: 200px; background-color: #fff">
    <div class="ui-widget-header">
        <span class="ui-dialog-title" id="ui-dialog-title-divEditPopUp">Add File</span><a
            href="#" class="ui-dialog-titlebar-close" role="button"><span class="sfClosepopup ui-icon ui-icon-closethick">close</span></a>
    </div>
    <table>
        <%-- <tr>
       <td colspan="3">
                <span id="spnPath"></span>
            </td>
        </tr>--%>
        <tr>
            <td>
                <span class="sfFormlabel">Browse Files: </span>
            </td>
            <td>
                <input type="file" id="fileUpload" class="fileClass" />
            </td>
        </tr>
    </table>
</div>
<div id="newFolderPopUp" class="popupbox sfFormwrapper">
    <div class="ui-widget-header">
        <span class="ui-dialog-title" id="Span1">Add New Folder</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all"
            role="button"><span class="sfClosepopup ui-icon ui-icon-closethick">close</span></a>
    </div>
    <table>
        <tr>
            <td>
                <span class="sfFormlabel">FolderName:</span>
            </td>
            <td>
                <input type="text" id="txtFolderName" class="sfInputbox" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <span class="sfFormlabel">Type:</span>
            </td>
            <td>
                <input type="checkbox" id="chkFolderType" disabled="disabled" checked="checked" />
                Standard
                <%-- <select id="ddlFileType" class="sfListmenu">
                    <option value="0">Standard</option>
                    <option value="1">Secured</option>
                    <option value="2">Database</option>
                </select>--%>
            </td>
            <td>
                <div class="sfButtonwrapper">
                    <input type="button" id="btnCreateFolder" class="sfBtn" value="Create" />
                </div>
            </td>
        </tr>
    </table>
</div>
<div id="divEditMode" class="popupbox sfFormwrapper" style="height: 75%; width: 75%; overflow: scroll; background-color: #fff">
    <div class="ui-widget-header">
        <span class="ui-dialog-title" id="Span4">FileMgrPopUp</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all"><span
            class="sfClosepopup ui-icon ui-icon-closethick">close</span></a>
    </div>
    <div id="divImages">
        <h2>Edit Image</h2>
        <div id="divFileInforMation">
        </div>
        <table>
            <tr id="trUrl">
                <td>
                    <label class="sfFormlabel">
                        Share Link:
                    </label>
                    <input type="text" class="sfInputbox" id="txtUrl" readonly="readonly" style="width: 400px;" />
                </td>
                <td>
                    <a id='downloadLink' class='icon-download sfBtn'>Download</a>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="hdnPath" style="display: none">
                    </label>
                    <label class="sfFormlabel">
                        Image:
                    </label>
                    <label id="txtTitle">
                    </label>
                </td>
                <td>
                    <label class="sfFormlabel">
                        Cropped image:</label>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="sfOrgImage">
                        <img src="" alt="My Image" title="Image" id="originalImage" />
                    </div>
                    <div>
                        X1
                        <input type="text" size="4" id="x1" name="x1" class="coor" readonly="readonly" />
                        Y1
                        <input type="text" size="4" id="y1" name="y1" class="coor" readonly="readonly" />
                        X2
                        <input type="text" size="4" id="x2" name="x2" class="coor" readonly="readonly" />
                        Y2
                        <input type="text" size="4" id="y2" name="y2" class="coor" readonly="readonly" />
                        <br />
                        W
                        <input type="text" size="4" id="w" name="w" class="coor" readonly="readonly" />
                        H
                        <input type="text" size="4" id="h" name="h" class="coor" readonly="readonly" />
                    </div>
                    <div>
                        <label id="btnCrop" class="icon-crop sfBtn" onclick="return ValidateSelected();">
                            Crop</label>
                    </div>
                </td>
                <td style="vertical-align: top;">
                    <div id="divCropedImage" class="sfCropImage">
                    </div>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
    </div>
    <div id="divEditFile">
        <label class="sfFormlabel">
            Share Link:
        </label>
        <input type="text" class="sfInputbox" id="txtShareFile" readonly="readonly" style="width: 400px;" />
        <a id='downloadFile' class='icon-download sfBtn'>Download</a>
        <textarea id="txtEditFile" name="txtEditFile" style="display: none"></textarea>
        <label id="lblFilepath" style="display: none">
        </label>
        <div class="sfButtonwrapper sftype1">
            <label id="lblSaveDocument" class="icon-save sfBtn">
                Save</label>
        </div>
    </div>
</div>
<div id="divEditPopUp" class="popupbox sfFormwrapper">
    <div class="ui-widget-header">
        <span class="ui-dialog-title" id="Span2">Rename File</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all"
            role="button"><span class="sfClosepopup ui-icon ui-icon-closethick">close</span></a>
    </div>
    <table>
        <tr>
            <td width="80px">
                <span class="sfFormlabel">File Name:</span>
            </td>
            <td>
                <input type="text" id="txtEditFileName" class="sfInputbox" />
            </td>
        </tr>
        <tr>
            <td>
                <span class="sfFormlabel">File Mode:</span>
            </td>
            <td>
                <input type="checkbox" id="chkArchive" value="A" disabled="disabled" />
                Archive<br />
            </td>
            <%--   <td>
                <input type="checkbox" id="chkRead" value="R" />
                Read
            </td>--%>
        </tr>
        <%--  <tr>
            <td>
            </td>
            <td>
                <input type="checkbox" id="chkSystem" value="W" />
                Write<br />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <input type="checkbox" id="chkHidden" value="H" />
                Hidden<br />
            </td>
        </tr>--%>
        <tr>
            <td colspan="2"></td>
            <td>
                <div class="sfButtonwrapper">
                    <input type="button" id="btnUpdateFile" value="Update" class="sfBtn" />
                </div>
            </td>
        </tr>
    </table>
</div>
<div id="divCopyFiles" class="popupbox">
    <div class="ui-widget-header">
        <span class="ui-dialog-title" id="Span3">Message</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all"
            role="button"><span class="sfClosepopup ui-icon ui-icon-closethick"> close</span></a>
    </div>
    <p id="copyMessage">
    </p>
    <div class="sfButtonwrapper">
        <input type="button" id="btnCopy" class="sfBtn" value="" />
    </div>
</div>
<div id="divMessagePopUp" class="popupbox">
    <div class="ui-widget-header">
        <a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button"><span class="sfClosepopup ui-icon ui-icon-closethick">close</span></a>
    </div>
    <div class="cssClassFileManagerPopUP" style="text-align: center">
        <span class="cssClassMessage sfFormlabel"></span>
    </div>
</div>
<div id="divSuccessPopUp" class="popupbox">
    <div class="ui-widget-header">
        <a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button"><span class="sfClosepopup ui-icon ui-icon-closethick">close</span></a>
    </div>
    <div class="cssClassFileManagerPopUP" style="text-align: center">
        <span class="cssClassSuccessMessage sfFormlabel"></span>
    </div>
</div>
<div id="divErrorPopUp" class="popupbox">
    <div class="ui-widget-header">
        <span class="ui-dialog-title" id="Span6">Add New Folder</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all"
            role="button"><span class="sfClosepopup ui-icon ui-icon-closethick">close</span></a>
    </div>
    <div class="cssClassFileManagerPopUP" style="text-align: center">
        <span class="cssClassErrorMessage sfFormlabel"></span>
    </div>
</div>
<div id="divConfirmPopUp" class="popupbox sfFormwrapper">
    <div class="ui-widget-header">
        <span class="ui-dialog-title" id="Span7">Confirmation</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all"
            role="button"><span class="sfClosepopup ui-icon ui-icon-closethick">close</span></a>
    </div>
    <span class="sfFormlabel sfConfirmmsg" style="text-align: center"></span>
    <div class="sfButtonwrapper" style="text-align: center">
        <input type="button" id="btnConfirmYes" value="Yes" style="float: none" class="sfBtn" />
        <input type="button" id="btnConfirmNo" value="No" style="float: none" class="sfBtn" />
    </div>
</div>
<asp:HiddenField ID="hdnDestinationPath" runat="server" Value="" />
