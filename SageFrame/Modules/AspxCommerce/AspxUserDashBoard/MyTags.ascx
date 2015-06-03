<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyTags.ascx.cs" Inherits="Modules_AspxMyTags_MyTags" %>

<script type="text/javascript">
    var Tags = "";
    aspxCommonObj.UserName = AspxCommerce.utils.GetUserName();
    //<![CDATA[
    $(function() {
        $(".sfLocale").localize({
            moduleKey: AspxUserDashBoard
        });
    });

    Tags = {
        config: {
            isPostBack: false,
            async: true,
            cache: true,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: '{}',
            dataType: "json",
            baseURL:moduleRootPath,             url: "",
            method: "",
            ajaxCallMode: "",
            error: ""
        },

        ajaxCall: function(config) {
            $.ajax({
                type: Tags.config.type,
                beforeSend: function (request) {
                    request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                    request.setRequestHeader("UMID", userModuleIDUD);
                    request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                    request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                    request.setRequestHeader("PType", "v");
                    request.setRequestHeader('Escape', '0');
                },
                contentType: Tags.config.contentType,
                cache: Tags.config.cache,
                async: Tags.config.async,
                data: Tags.config.data,
                dataType: Tags.config.dataType,
                url: Tags.config.url,
                success: Tags.config.ajaxCallMode,
                error: Tags.config.error
            });
        },

        init: function() {
                    },

        GetMyTags: function () {
            this.config.method = "AspxCoreHandler.ashx/GetTagsByUserName";
            this.config.url = aspxservicePath + this.config.method;         
            this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
            this.config.ajaxCallMode = Tags.BindTagsByUserName;
            this.ajaxCall(this.config);
        },

        DeleteMyTag: function(obj) {
            var properties = { onComplete: function(e) {
                if (e) {
                    var itemTagId = $(obj).attr("value");
                    Tags.config.method = "AspxCoreHandler.ashx/DeleteUserOwnTag";
                    Tags.config.url = aspxservicePath + Tags.config.method;
                    Tags.config.data = JSON2.stringify({ itemTagID: itemTagId, aspxCommonObj: aspxCommonObj });
                    Tags.config.ajaxCallMode = Tags.BindTagsOnDelete;
                    Tags.config.error = Tags.GetTagsDeleteErrorMsg;
                    Tags.ajaxCall(Tags.config);
                }
                else return false;
            }
            }
            csscody.confirm("<h1>" + getLocale(AspxUserDashBoard, "Delete Confirmation") + "</h1><p>" + getLocale(AspxUserDashBoard, "Do you want to delete this tag?") + "</p>", properties);
        },


        BindTagsByUserName: function(data) {
            var MyTags = '';
            if (data.d.length > 0) {
                $.each(data.d, function(index, value) {
                    MyTags += '<li class="tag_content"><a href="' + aspxRedirectPath + 'ViewTaggedItems' + pageExtension + '?tagsId=' + value.ItemTagIDs + '"><span>' + value.Tag + '</span></a>';
                    MyTags += "<button type=\"button\" class=\"cssClassCross\" value=" + value.ItemTagIDs + " onclick ='Tags.DeleteMyTag(this)'><span>" + getLocale(AspxUserDashBoard, "x") + "</span></button></li>";
                });
            }
            else {
                MyTags = "<span class=\"cssClassNotFound\">" + getLocale(AspxUserDashBoard, "Your tag list is empty!") + "</span>";
            }
            $("#divMyTags >ul").html(MyTags);
        },

        BindTagsOnDelete: function() {
            csscody.info("<h2>" + getLocale(AspxUserDashBoard, "Information Alert") + "</h2><p>" + getLocale(AspxUserDashBoard, "Your Tag is deleted successfully!") + "</p>");
            Tags.GetMyTags();
        },

        GetTagsDeleteErrorMsg: function() {
            csscody.error("<h2>" + getLocale(AspxUserDashBoard, "Error Message") + "</h2><p>" + getLocale(AspxUserDashBoard, "Failed to delete!") + "</p>");
        }

    };
    
    $(function(){
        Tags.init();
    });
    //]]>  
</script>

<div class="sfFormwrapper">
    <div class="cssClassCommonCenterBox">
        <h2>
            <label id="lblMyTagsTitle" class="cssClassTags sfLocale">My Tags Content</label></h2>
        <div class="cssClassCommonCenterBoxTable cssClassPad25 cssClassTMar30">
            <asp:Literal ID="ltrMyTags" runat="server" EnableViewState="false"></asp:Literal>
        </div>
    </div>
</div>
