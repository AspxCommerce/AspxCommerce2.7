<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LocalizerSwitchSettings.ascx.cs"
    Inherits="Modules_Language_LocalizerSwitchSettings" %>

<script type="text/javascript">
    //<![CDATA[    
    var PortalID = '<%=PortalID %>';
    var UserModuleID = '<%=UserModuleID %>';
    $(document).ready(function() {
        LoadSettings();
        InitializeCarousel();
        $('#carousel_container').hide();
        $('#ddlFlags').msDropDown().data("dd");
        $('#txtFlagButton,#divFlagsDDL,#divDropDownSubSetting,#ddlNames').hide();


        $('input[type="radio"]').change(function() {
            var id = $(this).attr("id");
            switch (id) {
                case "rbIsList":
                    $('#divListSubSetting,#imgFlagButton').fadeIn("slow");
                    $('#txtFlagButton,#divDropDownSubSetting,#divFlagsDDL,#ddlNames,#carousel_container').hide();
                    $('#chkFlags,#chkNames').prop('checked', true);

                    $('#imgFlagButton li img,#imgFlagButton li span').show();
                    var elem = '';
                    elem += '<li><img alt="" src="<%=appPath%>/images/flags/us.png" /><span>EngLish(United States)</span></li>';
                    elem += '<li><img alt="" src="<%=appPath%>/images/flags/np.png" /><span>Nepali(Nepal)</span></li><li>';
                    elem += ' <img alt="" src="<%=appPath%>/images/flags/in.png" /><span>Hindi(India)</span></li><li>';
                    elem += ' <img alt="" src="<%=appPath%>/images/flags/jp.png" /><span>Japanese(Japan)</span></li>';

                    // alert("reached");
                    $("#imgFlagButton").html(elem);
                    break;
                case "rbIsDropDown":
                    $('#divDropDownSubSetting').fadeIn("slow");
                    $('#ddlNames').fadeIn("slow");
                    $('#rbIsNormal').attr("checked", true);
                    $('#divListSubSetting,#imgFlagButton,#carousel_container').hide();
                    break;
                case "rbIsFlag":
                    $('#divFlagsDDL').fadeIn("slow");
                    $('#ddlNames,#carousel_container').hide();
                    break;
                case "rbIsNormal":
                    $('#ddlNames').fadeIn("slow");
                    $('#divFlagsDDL').hide();
                    break;
                case "chkShowCarousel":
                    if ($(this).prop("checked") == true && $('#rbHorizontal').prop("checked") == true) {

                        $('#carousel_container').fadeIn();
                        $('#imgFlagButton,#ddlNames,#divFlagsDDL,#divDropDownSubSetting,#divFlagAlignment,#divListSubSetting').hide();
                    }
                    break;
            }
        });
        $('input[type="checkbox"]').change(function() {
            var ElemWithFlag = '';
            ElemWithFlag += '<li><img alt="" src="<%=appPath%>/images/flags/us.png" /><span>EngLish(United States)</span></li>';
            ElemWithFlag += '<li><img alt="" src="<%=appPath%>/images/flags/np.png" /><span>Nepali(Nepal)</span></li><li>';
            ElemWithFlag += ' <img alt="" src="<%=appPath%>/images/flags/in.png" /><span>Hindi(India)</span></li><li>';
            ElemWithFlag += ' <img alt="" src="<%=appPath%>/images/flags/jp.png" /><span>Japanese(Japan)</span></li>';


            var ElemWithNoFlag = '';
            ElemWithNoFlag += '<li><span>EngLish(English(United States))</span></li>';
            ElemWithNoFlag += '<li><span>Nepali(नेपाली(नेपाल))</span></li><li>';
            ElemWithNoFlag += '<span>Hindi(हिंदी(इंडिया)</span></li><li>';
            ElemWithNoFlag += 'span>Japanese(じゃぱねせ(ジャパン))</span></li>';

            var id = $(this).attr("id");
            switch (id) {
                case "chkFlags":
                    if ($('#chkNames').prop("checked") == true && $(this).prop("checked") == true) {
                        // alert("reached");
                        $("#imgFlagButton").html(ElemWithFlag);
                    }
                    else if ($('#chkNames').prop("checked") == true && $(this).prop("checked") == false) {
                        // alert("reached");
                        $("#imgFlagButton").html(ElemWithNoFlag);
                    }
                    else if ($('#chkNames').prop("checked") == false && $(this).prop("checked") == true) {
                        $("#imgFlagButton").html(ElemWithFlag);
                        $('#imgFlagButton li img').show();
                        $('#imgFlagButton li span').hide();
                    }
                    else if ($('#chkNames').prop("checked") == false && $(this).prop("checked") == false) {
                        $('#imgFlagButton li img').hide();
                        $('#imgFlagButton li span').hide();
                    }
                    break;
                case "chkNames":
                    if ($('#chkFlags').prop("checked") == true && $(this).prop("checked") == true) {
                        $("#imgFlagButton").html(ElemWithFlag);
                    }
                    else if ($('#chkFlags').prop("checked") == false && $(this).prop("checked") == true) {
                        $("#imgFlagButton").html(ElemWithNoFlag);
                        $('#imgFlagButton li img').hide();
                        $('#imgFlagButton li span').show();
                    }
                    else if ($('#chkFlags').prop("checked") == true && $(this).prop("checked") == false) {
                        $("#imgFlagButton").html(ElemWithFlag);
                        $('#imgFlagButton li img').show();
                        $('#imgFlagButton li span').hide();
                    }
                    else if ($('#chkFlags').prop("checked") == false && $(this).prop("checked") == false) {
                        $('#imgFlagButton li img').hide();
                        $('#imgFlagButton li span').hide();
                    }
                    break;
            }
        });
        BindEvents();

        $('#chkFlags,#chkNames').prop("checked", true);

        $('#imgFlagButton li img,#imgFlagButton li span').show();
        var elem = '';
        elem += '<li><img alt="" src="<%=appPath%>/images/flags/us.png" /><span>EngLish(United States)</span></li>';
        elem += '<li><img alt="" src="<%=appPath%>/images/flags/np.png" /><span>Nepali(Nepal)</span></li><li>';
        elem += ' <img alt="" src="<%=appPath%>/images/flags/in.png" /><span>Hindi(India)</span></li><li>';
        elem += ' <img alt="" src="<%=appPath%>/images/flags/jp.png" /><span>Japanese(Japan)</span></li>';

        // alert("reached");
        $("#imgFlagButton").html(elem);

    });

    function BindDefaultSettings() {
        $('#rbIsList').prop("checked", true);
        $('#divListSubSetting,#imgFlagButton').fadeIn("slow");
        $('#txtFlagButton,#divDropDownSubSetting,#divFlagsDDL,#ddlNames,#carousel_container').hide();
        $('#chkFlags,#chkNames').prop("checked", true);
    }
    function BindEvents() {
        $('#btnSaveSettings').bind("click", function() {
            //declare the setting variables          
            var _SwitchType = "";
            var _ListTypeFlags = false;
            var _ListTypeName = false;
            var _ListTypeBoth = false;
            var _ListAlign = "H";
            var _EnableCarousel = false;
            var _DropDownType = "Flag";
            //end of setting variables declaration
            var jsonObj = "";
            var id = ""
            var chkFlag = $('#chkFlags');
            var chkName = $('#chkNames');
            if ($('#rbIsList').prop("checked") == true) {
                id = "rbIsList";
                _SwitchType = "List";
            }
            else if ($('#rbIsDropDown').prop("checked") == true) {
                id = "rbIsDropDown";
                _SwitchType = "DropDown";
            }
            else {
                id = "chkShowCarousel";
            }
            switch (id) {
                case "rbIsList":
                    if ($(chkFlag).prop("checked") == true && $(chkName).prop("checked") == false) {
                        _ListTypeFlags = true;
                    }
                    else if ($(chkFlag).prop("checked") == false && $(chkName).prop("checked") == true) {
                        _ListTypeName = true;
                    }
                    else if ($(chkFlag).prop("checked") == true && $(chkName).prop("checked") == true) {
                        _ListTypeBoth = true;
                    }
                    else if ($(chkFlag).prop("checked") == false && $(chkName).prop("checked") == false) {
                    }
                case "rbIsDropDown":
                    if ($('#rbIsFlag').prop("checked") == true) {
                        _DropDownType = "Flag";
                    }
                    else if ($('#rbIsNormal').prop("checked") == true) {
                        _DropDownType = "Normal";
                    }
                    break;
                case "chkShowCarousel":
                    if ($('#chkShowCarousel').prop("checked") == true) {
                        _EnableCarousel = true;
                        _SwitchType = "list";
                        break;
                    }
            }
            var param = JSON2.stringify({ SwitchType: _SwitchType, ListTypeFlags: _ListTypeFlags, ListTypeName: _ListTypeName, ListTypeBoth: _ListTypeBoth, ListAlign: _ListAlign, EnableCarousel: _EnableCarousel, DropDownType: _DropDownType, PortalID: PortalID, UserModuleID: '<%=UserModuleID%>' });
            var method = SageFrameAppPath + "/Modules/LanguageSwitcher/js/WebMethods.aspx/AddLanguageSwitchSettings";
            $.ajax({
                type: "POST",
                url: method,
                contentType: "application/json; charset=utf-8",
                data: param,
                dataType: "json",
                success: function(msg) {
                    SageFrame.messaging.show("Settings successfully saved", "Success");
                }
            });
        });
    }
    function InitializeCarousel() {
        //move he last list item before the first item. The purpose of this is if the user clicks to slide left he will be able to see the last item.
        $('#carousel_ul li:first').before($('#carousel_ul li:last'));
        //when user clicks the image for sliding right        
        $('#right_scroll img').click(function() {
            //get the width of the items ( i like making the jquery part dynamic, so if you change the width in the css you won't have o change it here too ) '
            var item_width = $('#carousel_ul li').outerWidth() + 10;
            //calculae the new left indent of the unordered list
            var left_indent = parseInt($('#carousel_ul').css('left')) - item_width;
            //make the sliding effect using jquery's anumate function '
            $('#carousel_ul:not(:animated)').animate({ 'left': left_indent }, 500, function() {
                //get the first list item and put it after the last list item (that's how the infinite effects is made) '
                $('#carousel_ul li:last').after($('#carousel_ul li:first'));
                //and get the left indent to the default -210px
                $('#carousel_ul').css({ 'left': '-5px' });
            });
        });
        //when user clicks the image for sliding left
        $('#left_scroll img').click(function() {
            var item_width = $('#carousel_ul li').outerWidth() + 10;
            /* same as for sliding right except that it's current left indent + the item width (for the sliding right it's - item_width) */
            var left_indent = parseInt($('#carousel_ul').css('left')) + item_width;
            $('#carousel_ul:not(:animated)').animate({ 'left': left_indent }, 500, function() {
                /* when sliding to left we are moving the last item before the first list item */
                $('#carousel_ul li:first').before($('#carousel_ul li:last'));
                /* and again, when we make that change we are setting the left indent of our unordered list to the default -210px */
                $('#carousel_ul').css({ 'left': '-5px' });
            });
        });
    }

    function LoadSettings() {
        var method = SageFrameAppPath + "/Modules/LanguageSwitcher/" + "js/WebMethods.aspx/GetLanguageSettings";
        $.ajax({
            type: "POST",
            url: method,
            contentType: "application/json; charset=utf-8",
            data: JSON2.stringify({ PortalID: parseInt(PortalID), UserModuleID: parseInt(UserModuleID) }),
            dataType: "json",
            success: function(msg) {
                var switchSettings = msg.d;
                var _SwitchType = "";
                var _ListTypeFlags = "false";
                var _ListTypeName = "false";
                var _ListTypeBoth = "false";
                var _ListAlign = "H";
                var _EnableCarousel = "false";
                var _DropDownType = "Flag";
                $.each(switchSettings, function(index, item) {
                    if (item.Key == "ListTypeBoth") {
                        _ListTypeBoth = item.Value;
                    }
                    if (item.Key == "ListTypeFlag") {
                        _ListTypeFlags = item.Value;
                    }
                    if (item.Key == "ListTypeName") {
                        _ListTypeName = item.Value;
                    }
                    if (item.Key == "ListAlign") {
                        _ListAlign = item.Value;
                    }
                    if (item.Key == "EnableCarousel") {
                        _EnableCarousel = item.Value;
                    }
                    if (item.Key == "SwitchType") {
                        _SwitchType = item.Value;
                    }
                    if (item.Key == "DropDownType") {
                        _DropDownType = item.Value;
                    }
                });
                if (_EnableCarousel.toLowerCase() == "false") {
                    switch (_SwitchType.toLowerCase()) {
                        case "list":
                            $('#rbIsList').prop("checked", true);
                            $('#rbIsDropDown').prop("checked", false);
                            if (_ListTypeBoth.toLowerCase() == "true") {
                                $('#chkFlags').prop("checked", true);
                                $('#chkNames').prop("checked", true);
                                $('#imgFlagButton li img').show();
                                $('#imgFlagButton li span').show();
                                $('#chkFlags,#chkNames').prop("checked", true);

                                $('#imgFlagButton li img,#imgFlagButton li span').show();
                                var elem = '';
                                elem += '<li><img alt="" src="<%=appPath%>/images/flags/us.png" /><span>EngLish(United States)</span></li>';
                                elem += '<li><img alt="" src="<%=appPath%>/images/flags/np.png" /><span>Nepali(Nepal)</span></li><li>';
                                elem += ' <img alt="" src="<%=appPath%>/images/flags/in.png" /><span>Hindi(India)</span></li><li>';
                                elem += ' <img alt="" src="<%=appPath%>/images/flags/jp.png" /><span>Japanese(Japan)</span></li>';

                                // alert("reached");
                                $("#imgFlagButton").html(elem);
                            }
                            if (_ListTypeFlags.toLowerCase() == "true") {
                                $('#chkFlags').prop("checked", true);
                                $('#chkNames').prop("checked", false);
                                $('#imgFlagButton li img').show();
                                $('#imgFlagButton li span').hide();
                            }
                            if (_ListTypeName.toLowerCase() == "true") {
                                $('#chkFlags').prop("checked", false);
                                $('#chkNames').prop("checked", true);
                                $('#imgFlagButton li img').hide();
                                $('#imgFlagButton li span').show();
                            }
                            break;
                        case "dropdown":
                            $('#rbIsList').prop("checked", false);
                            $('#rbIsDropDown').prop("checked", true);
                            $('#divDropDownSubSetting').fadeIn("slow");
                            $('#divListSubSetting,#imgFlagButton,#carousel_container').hide();
                            switch (_DropDownType.toLowerCase()) {
                                case "normal":
                                    $('#divFlagsDDL,#imgFlagButton,#carousel_container').hide();
                                    $('#ddlNames').show();
                                    $('#rbIsNormal').prop("checked", true);
                                    break;
                                case "flag":
                                    $('#ddlNames,#imgFlagButton,#carousel_container').hide();
                                    $('#divFlagsDDL').show();
                                    $('#rbIsFlag').prop("checked", true);
                                    break;
                            }
                            break;
                    }
                }
                else {
                    $('#imgFlagButton,#ddlNames,#divFlagsDDL,#divDropDownSubSetting,#divListSubSetting').hide();
                    $('#chkShowCarousel').prop("checked", true);
                    $('#rbIsList,#rbIsDropDown,#chkFlags,#chkNames').prop("checked", false);
                    $('#carousel_container').show();
                    $('#carousel_container').fadeIn("slow");
                }
            }
        });
    }
    //]]>	
</script>

<h2>
    Language Switch Settings</h2>
<div class="sfFormwrapper">
    <div>
        <div id="divMainSetting" class="cssClassRadioBtnWrapper">
            <input type="radio" id="rbIsList" class="mainSwitch" checked="checked" name="displayStyle" />
            <span>Lists</span>
            <input type="radio" id="rbIsDropDown" class="mainSwitch" name="displayStyle" />
            <span>DropDown</span>
            <input type="radio" id="chkShowCarousel" class="mainSwitch" name="displayStyle" />
            <span>Carousel</span>
        </div>
        <div id="divListSubSetting" class="cssClassCheckBoxWrapper">
            <input type="checkbox" id="chkFlags" checked="checked" />
            <span>Flags</span>
            <input type="checkbox" id="chkNames" checked="checked" />
            <span>Names</span>
        </div>
        <div id="divFlagAlignment" class="cssClassFlagAlignment" style="display: none">
            <input type="radio" id="rbHorizontal" name="alignment" checked="checked" />
        </div>
        <div id="divDropDownSubSetting" class="sfRadiobutton">
            <input type="radio" id="rbIsNormal" checked="checked" name="ddlType" />
            <span>Plain</span>
            <input type="radio" id="rbIsFlag" name="ddlType" />
            <span>With Flags</span>
        </div>
        <div class="cssClassclear">
        </div>
        <div id="divPreviewHeader">
            <h2>
                Preview</h2>
        </div>
        <div id="divPreview">
            <div class="cssClassFlagButtonWrapper">
                <ul id="imgFlagButton" class="defaultButtonClass">
                    <li>
                        <img alt="" src="<%=appPath%>/images/flags/us.png" /><span>EngLish(English(United States))</span></li>
                    <li>
                        <img alt="" src="<%=appPath%>/images/flags/np.png" /><span>Nepali(नेपाली(नेपाल))</span></li>
                    <li>
                        <img alt="" src="<%=appPath%>/images/flags/in.png" /><span>Hindi(हिंदी(इंडिया)</span></li>
                    <li>
                        <img alt="" src="<%=appPath%>/images/flags/jp.png" /><span>Japanese(じゃぱねせ(ジャパン))</span></li>
                    <%--<li>
                        <img alt="" src="<%=appPath%>/images/flags/ar.png" /><span>Argentina</span></li>--%>
                </ul>
                <div class="cssClassDropDownFlag">
                    <select id="ddlNames" class="sfListmenu">
                        <option value="ad.png">en-US</option>
                        <option value="ad.png">ne-NP</option>
                        <option value="ad.png">Hi-IN</option>
                        <option value="ad.png">af-ZA</option>
                        <option value="ad.png">ko-KR</option>
                    </select>
                </div>
                <div id="divFlagsDDL">
                    <select id="ddlFlags" class="sfListmenu">
                        <option value="en-US" selected="selected" title="<%=appPath%>/images/flags/us.png">en-US</option>
                        <option value="en-US" selected="selected" title="<%=appPath%>/images/flags/np.png">ne-NP</option>
                        <option value="en-US" selected="selected" title="<%=appPath%>/images/flags/jp.png">ja-JP</option>
                        <option value="en-US" selected="selected" title="<%=appPath%>/images/flags/in.png">hi-IN</option>
                        <option value="en-US" selected="selected" title="<%=appPath%>/images/flags/ad.png">ad-AR</option>
                        <option value="en-US" selected="selected" title="<%=appPath%>/images/flags/ar.png">es-AR</option>
                    </select>
                </div>
                <div id='carousel_container' class="CssClassLanguageWrapper">
                    <div class="CssClassLanguageWrapperInside">
                        <div id='left_scroll'>
                            <img alt="" src='<%=ImagePath%>images/left.png' /></div>
                        <div id='carousel_inner'>
                            <ul id='carousel_ul'>
                                <li><a href='#'>
                                    <img alt="" src='<%=appPath%>/images/flags/an.png' /></a></li>
                                <li><a href='#'>
                                    <img alt="" src='<%=appPath%>/images/flags/ad.png' /></a></li>
                                <li><a href='#'>
                                    <img alt="" src='<%=appPath%>/images/flags/ae.png' /></a></li>
                                <li><a href='#'>
                                    <img alt="" src='<%=appPath%>/images/flags/af.png' /></a></li>
                                <li><a href='#'>
                                    <img alt="" src='<%=appPath%>/images/flags/ag.png' /></a></li>
                                <li><a href='#'>
                                    <img alt="" src='<%=appPath%>/images/flags/ai.png' /></a></li>
                                <li><a href='#'>
                                    <img alt="" src='<%=appPath%>/images/flags/al.png' /></a></li>
                                <li><a href='#'>
                                    <img alt="" src='<%=appPath%>/images/flags/am.png' /></a></li>
                            </ul>
                        </div>
                        <div id='right_scroll'>
                            <img alt="" src='<%=ImagePath%>images/right.png' /></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="sfButtonwrapper">
            <input type="button" id="btnSaveSettings" value="Save Settings" class="sfBtn" />
        </div>
    </div>
</div>
