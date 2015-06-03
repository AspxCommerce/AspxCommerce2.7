
(function ($) {
    var defaullts = {
        ShowImage: true,
        Thumbnail: true,
        Dimension: true,
        PassUrl: ""
    };
    var create = false;
    var $img = "";
    var mode = 0;
    var width, height;
    var x1, y1, x2, y2, tHeight, tWidth;
    var thumb = false;
    var fileName = "";
    $.fn.UploadFile = function (opt) {

        var options = $.extend(defaullts, opt);
        if (options.ShowImage == true) {
            $('#fileUploadContainer').append('<span class="sfCropMsg">Crop the image</span> <div id="divImageContainer"><div id="divFullImage"> </div>  ');
        }

        if (options.Dimension == true) {
            $('#divUploader ul li:last').prev().after('<li><input type="number" id="txtImageWidth" /></li> <li><label>Width </label></li><li><input type="text" id="txtImageHeight" /></li> <li><label>Height</label></li>');
        }
        if (options.Thumbnail == true) {
            $('#divImageContainer').append('<div id="divPreviewImage"><label>Preview</label></div><div class="divBtnThumb" ><input type="button" class="sfBtn" value="Create Thumbnail and save Image" id="btnCreateThumbnail"/></div><div id="divThumbnailImage"><span>Thumbnail </span> </div> </div>');
        }
        $('#divFullImage').imgAreaSelect({

            aspectRatio: '1:1',
            handles: true,
            fadeSpeed: 200,
            resizeable: false,
            maxHeight: 300,
            maxWidth: 200,
            minHeight: 100,
            minWidth: 50,
            onSelectChange: preview,
            onSelectEnd: getDimension,
            zIndex: 405
        });

        $('#btnUploadImage').click(function () {
            thumb = false;
            fileName = RandomName($('#files').val());
            mode = 1;
            fileName = $('#files').val().replace(/C:\\fakepath\\/i, '');
            var arr = [1, 2, 3, 4, 5];
            var rndNo = "";
            var i = 1;
            while (i <= 10) {
                rndNo += Math.floor(Math.random() * arr.length);
                i = i + 1;
            }
            fileName = rndNo + "_" + fileName;
            thumb = false;
            Init(fileName);

        });
        $('#btnCreateThumbnail').unbind().bind("click", function () {
            mode = 2;
            var oWid = $('#loadImage').width();
            var oHei = $('#loadImage').height();
            if (tWidth > 0) {
                createThumbnail(oWid, oHei);
            }
            else {
                alert("Select area to create thumbnail");
            }
        });
        //div to show inProcess
        $('#divUploader').append('<div class="cssOnProgress"> Digesting ....</div>')
        function Init(fileName) {
            var imageRegex = /([^\s]+(?=\.(jpg|gif|png|jpeg))\.\2)/gm;
            var fileRegex = /([^\s]+(?=\.(txt|text|doc|docx|css|xml))\.\2)/gm;
            if (fileName.match(imageRegex)) {
                checkStatus();
            }
            if (fileName.match(fileRegex)) {
                LoadDocument();
            }
            else {
                alert("Format not supported");
            }
        }
        function RandomName(imgName) {
            var num = Math.floor((Math.random() * 10000) + 1);
            var num1 = Math.floor((Math.random() * 10000) + 1);
            var ImageName = num + '_' + num1 + imgName;
            return ImageName;
        }
        //---- Add by Shree ----
        function LoadDocument() {
            var httpurl = WorkLogPath + 'UploadHandler.ashx?mode=0' + '&fn=' + fileName + '&userModuleId=' + fileManagerUserModuleID + '&portalID=' + SageFramePortalID + '&userName=' + SageFrameUserName + '&secureToken=' + SageFrameSecureToken;
            Upload(httpurl)
        }
        // ----End----
        function checkStatus() {
            width = "";
            height = "";
            var httpurl = WorkLogPath + "UploadHandler.ashx?wid=" + width + "&he=" + height + "&mode=1" + "&fn=" + fileName + "&gID=0" + "&userModuleId=" + fileManagerUserModuleID + "&portalID=" + SageFramePortalID + "&userName=" + SageFrameUserName + "&secureToken=" + SageFrameSecureToken;
            Upload(httpurl);
        }

        function Upload(httpurl) {
            //    file sending using xmlhttp

            var file = document.getElementById('files').files[0];
            var xhr = new XMLHttpRequest();
            xhr.file = file; // not necessary if you create scopes like this
            xhr.addEventListener('progress', function (e) {
                var done = e.position || e.loaded, total = e.totalSize || e.total;
                console.log('xhr progress: ' + (Math.floor(done / total * 1000) / 10) + '%');
            }, false);
            if (xhr.upload) {
                xhr.upload.onprogress = function (e) {
                    $('.cssOnProgress').addClass("shown");
                    var done = e.position || e.loaded, total = e.totalSize || e.total;
                    console.log('xhr.upload progress: ' + done + ' / ' + total + ' = ' + (Math.floor(done / total * 1000) / 10) + '%');
                };
            }
            xhr.onreadystatechange = function (e) {
                if (4 == this.readyState) {
                    $('.cssOnProgress').removeClass("shown");
                    $img = xhr.responseText;

                    if (thumb == false) {
                        loadImage();
                        thumb = true;
                    }
                    else {
                        loadThumb();

                    }

                    $('#ImageName').text(fileName);
                    $('#txtTitle').val(fileName);
                    $('#trUrl').show();
                    var url = location.protocol + location.host + $img;
                    $('#txtUrl').val(url);
                }
            };
            url = httpurl;
            xhr.open('post', url, true);
            if (mode == 1) {
                xhr.send(file);
            }
            if (mode == 2 || mode == 0) {
                xhr.send(file)
            }

        }
        //Loading the image to the Imgaearea
        function loadImage() {
            if (options.ShowImage == true) {
                $('#divFullImage').html('<img id="loadImage" src="' + $img + '" alt="Image"/>')
                $('#divPreviewImage').html('<img src="' + $img + '"></img>');
            }
        }
        //loading the thumbnail
        function loadThumb() {
            $('div[class ^=imgareaselect]').hide();
            $('#divThumbnailImage').html('');
            $('#divThumbnailImage').html('<img src="' + $img + '" alt="' + $img + '" />');
            var bool = 0;
            $('#divThumbnailImage').find('img').error(function () {
                bool = 1;
            });
            if (bool == 0) {
                $('.Logo').find('img:first').attr('src', $img); // SageFrameAppPath + '/modules/Groups/Images/thumbs/' + LogoImage);

                if (thumb == false) {
                    //jAlert('Your Image has been Updated Sucesfully', 'Image Update Success');
                }
                else {
                    //jAlert('Thumb Created Sucesfully', 'Created Success');
                }
            }
            else {
                //jAlert('Your Image is not Updated ', 'Update Failure');
            }
        }
        //preview image
        function preview(img, selection) {

            if (!selection.width || !selection.height)
                return;
            //200 is the #preview dimension, change this to your liking
            var scaleX = 200 / selection.width;
            var scaleY = 200 / selection.height;
            var imgwidth = $('#loadImage').innerWidth();
            var imgHeight = $('#loadImage').innerHeight();
            $('#divPreviewImage img').css({
                width: Math.round(scaleX * imgwidth),
                height: Math.round(scaleY * imgHeight),
                marginLeft: -Math.round(scaleX * selection.x1),
                marginTop: -Math.round(scaleY * selection.y1)
            });
        }
        function getDimension(img, selection) {
            x1 = selection.x1;
            y1 = selection.y1;
            tHeight = selection.height;
            tWidth = selection.width;
        }
        function createThumbnail(oWid, oHei) {
            var GroupID = $('#rightContent').find('span:first').text();
            if (GroupID.length == 0) {
                GroupID = -1;
                create = true;
            }

            filename = $('#loadImage').attr("src");
            filename = filename.substr(filename.lastIndexOf("/") + 1);
            if (filename == "")
                filename = $('txtTitle').val();
            httpurl = WorkLogPath + "UploadHandler.ashx?twid=" + tWidth + "&the=" + tHeight + "&x1=" + x1 + "&y1=" + y1 + "&wid=" + oWid + "&he=" + oHei + "&mode=2&edt=yes&fn=" + filename + "&userModuleId=" + fileManagerUserModuleID + "&portalID=" + SageFramePortalID + "&userName=" + SageFrameUserName + "&secureToken=" + SageFrameSecureToken;
            Upload(httpurl);
        }
    }
})(jQuery);


