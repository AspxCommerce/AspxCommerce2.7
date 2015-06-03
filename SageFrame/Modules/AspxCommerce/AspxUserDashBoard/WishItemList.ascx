<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WishItemList.ascx.cs"
    Inherits="WishItemList" %>

<script type="text/javascript" >
    //<![CDATA[
    var DashWishItem;
    var allowRealTimeNotifications = '<%=AllowRealTimeNotifications %>';
    $(function () {

        $(".sfLocale").localize({
            moduleKey: AspxUserDashBoard
        });
        DashWishItem = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: AspxCommerce.utils.GetAspxServicePath(),
                method: "",
                url: "",
                ajaxCallMode: 0,
                error: 0
            },
            ajaxCall: function (config) {
                $.ajax({
                    type: DashWishItem.config.type,
                    beforeSend: function (request) {
                        request.setRequestHeader('ASPX-TOKEN', _aspx_token);
                        request.setRequestHeader("UMID", userModuleIDUD);
                        request.setRequestHeader("UName", AspxCommerce.utils.GetUserName());
                        request.setRequestHeader("PID", AspxCommerce.utils.GetPortalID());
                        request.setRequestHeader("PType", "v");
                        request.setRequestHeader('Escape', '0');
                    },
                    contentType: DashWishItem.config.contentType,
                    cache: DashWishItem.config.cache,
                    async: DashWishItem.config.async,
                    url: DashWishItem.config.url,
                    data: DashWishItem.config.data,
                    dataType: DashWishItem.config.dataType,
                    success: DashWishItem.ajaxSuccess,
                    error: DashWishItem.ajaxFailure
                });
            },
            ajaxSuccess: function (msg) {
                switch (DashWishItem.config.ajaxCallMode) {
                    case 1:
                        $("#tblWishItemList>tbody").html('');
                        var length = msg.d.length;
                        if (length > 0) {
                            var item;
                            for (var index = 0; index < length; index++) {
                                item = msg.d[index];
                                DashWishItem.BindWishListItems(item, index);
                            };
                            $(".comment").each(function () {
                                if ($(this).val() == "") {
                                    $(this).addClass("lightText").val(getLocale(AspxUserDashBoard, "enter a comment.."));
                                }
                            });

                            $(".comment").bind("focus", function () {
                                if ($(this).val() == "enter a comment..") {
                                    $(this).removeClass("lightText").val("");
                                }
                            });
                            $(".comment").bind("blur", function () {
                                if ($(this).val() == "") {
                                    $(this).val("enter a comment..").addClass("lightText");
                                }
                            });
                        }
                        else {
                            $("#tblWishItemList>thead").hide();
                            $("#wishitemBottom").hide();
                            $("#tblWishItemList").html("<p>" + getLocale(AspxUserDashBoard, "Your Wishlist is empty!") + "</p>");
                        }
                        break;
                    case 2:
                        if ($(".cssClassWishListCount").html() != null) {
                            DashWishItem.UpdateHeaderWishlistCount();
                        }
                        DashWishItem.GetWishItemList();
                        alert(getLocale(AspxUserDashBoard, "Success"));
                        break;
                    case 3:
                        alert(getLocale(AspxUserDashBoard, "success"));
                        break;
                    case 4:
                        alert(getLocale(AspxUserDashBoard, "Successfully cleared your wishlist!"));
                        break;

                }
                if (allowRealTimeNotifications.toLowerCase() == 'true') {
                    UpdateNotifications(1);
                }
            }, ajaxFailure: function () {

            },
            GetWishItemList: function () {
                this.config.method = "AspxCommerceWebService.asmx/GetWishItemList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj, flagShowAll: isAll, count: count });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);

            }, BindWishListItems: function (response, index) {
                var imagePath = itemImagePath + response.ImagePath;
                if (response.ImagePath == "") {
                    imagePath = AspxCommerce.utils.GetAspxRootPath() + "Modules/AspxCommerce/AspxItemsManagement/uploads/noitem.png";
                }
                else if (response.AlternateText == "") {
                    response.AlternateText = response.ItemName;
                }
                ItemIDs = response.ItemID + "#";
                ItemComments = $("#comment" + response.ItemID + "").innerText;

                var WishDate = DashWishItem.DateDeserialize(response.WishDate, "yyyy/M/d");
                if (index % 2 == 0) {
                    Items = '<tr class="sfEven" id="tr_' + response.ItemID + '"><td class="cssClassWishItemDetails"><img src="' + AspxCommerce.utils.GetAspxRootPath() + imagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '"  title="' + response.AlternateText + '"/><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + pageExtension + '">' + response.ItemName + '</a><span class="cssClassPrice">' + response.Price + '</span></td><td class="cssClassWishComment"><textarea id="comment_' + response.ItemID + '" class="comment">' + response.Comment + '</textarea></td><td class="cssClassWishDate">' + WishDate + '</td><td class="cssClassWishToCart"> <div class="sfButtonwrapper"><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + pageExtension + '"><span>' + getLocale(AspxUserDashBoard, "Add To Cart") + '</span></a></div></td><td class="cssClassDelete"><img id="imgdelete" onclick="DashWishItem.DeleteWishItem(' + response.ItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + 'asdf/images/admin/btndelete.png" alt="delete" title=' + getLocale(AspxUserDashBoard, "Delete Item") + '/></td></tr>';
                }
                else {
                    Items = '<tr class="sfOdd" id="tr_' + response.ItemID + '"><td class="cssClassWishItemDetails"><img src="' + AspxCommerce.utils.GetAspxRootPath() + imagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '"  title="' + response.AlternateText + '"/><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + pageExtension + '">' + response.ItemName + '</a><span class="cssClassPrice">' + response.Price + '</span></td><td class="cssClassWishComment"><textarea id="comment_' + response.ItemID + '" class="comment">' + response.Comment + '</textarea></td><td class="cssClassWishDate">' + WishDate + '</td><td class="cssClassWishToCart"> <div class="sfButtonwrapper"><a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + pageExtension + '"><span>' + getLocale(AspxUserDashBoard, "Add To Cart") + '</span></a></div></td><td class="cssClassDelete"><img id="imgdelete" onclick="DashWishItem.DeleteWishItem(' + response.ItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + 'asdf/images/admin/btndelete.png" alt="delete" title=' + getLocale(AspxUserDashBoard, "Delete Item") + '/></td></tr>';
                }
                $("#tblWishItemList>tbody").append(Items);
            }, DeleteWishItem: function (itemId) {
                var properties = {
                    onComplete: function (e) {
                        DashWishItem.ConfirmSingleDelete(itemId, e);
                    }
                }
                csscody.confirm("<h2>" + getLocale(AspxUserDashBoard, "Delete Confirmation") + "</h2><p>" + getLocale(AspxUserDashBoard, "Do you want to delete this item from your wishlist?") + "</p>", properties);
            }, ConfirmSingleDelete: function (id, event) {
                if (event) {
                    this.config.method = "AspxCommerceWebService.asmx/DeleteWishItem";
                    this.config.url = this.config.baseURL + this.config.method;
                    this.config.data = JSON2.stringify({ ID: id, aspxCommonObj: aspxCommonObj });
                    this.config.ajaxCallMode = 2;
                    this.ajaxCall(this.config);

                }
            },
            UpdateHeaderWishlistCount: function () {
                var wishListCount = $(".cssClassWishListCount").html().replace(/[^0-9]/gi, '');
                wishListCount = parseInt(wishListCount) - 1;
                $(".cssClassWishListCount").html("[" + wishListCount + "]");
            },
            UpdateWishList: function () {
                var comment = '';
                var itemId = '';
                $(".comment").each(function () {
                    comment += $(this).val() + ',';
                    itemId += parseInt($(this).prop("id").replace(/[^0-9]/gi, '')) + ',';
                });
                comment = comment.substring(0, comment.length - 1);
                itemId = itemId.substring(0, itemId.length - 1);

                this.config.method = "AspxCommerceWebService.asmx/UpdateWishList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ ID: itemId, comment: comment, aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);

            }, ClearWishList: function () {
                this.config.method = "AspxCommerceWebService.asmx/ClearWishList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ aspxCommonObj: aspxCommonObj });
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);

            }, DateDeserialize: function (content, format) {
                content = eval('new ' + content.replace(/[/]/gi, ''));
                return formatDate(content, format);
            }, Init: function () {
                DashWishItem.GetWishItemList();
            }

        }
        DashWishItem.Init();
    });


    //]]>
</script>

<div class="sfFormwrapper">
    <div class="cssClassCommonCenterBox">
        <h2>
            <asp:Label ID="lblMyWishListTitle" runat="server" Text="My WishList Content"
                CssClass="cssClassWishItem" meta:resourcekey="lblMyWishListTitleResource1"></asp:Label></h2>
        <div class="cssClassCommonCenterBoxTable">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblWishItemList" class="sfGridWrapperTable">
                <thead>
                    <tr class="cssClassCommonCenterBoxTableHeading">
                        <td class="cssClassWishItemDetails">
                            <asp:Label ID="lblItem" runat="server" Text="Item"
                                meta:resourcekey="lblItemResource1"></asp:Label>
                        </td>
                        <td class="cssClassWishListComment">
                            <asp:Label ID="lblComment" runat="server" Text="Comment"
                                meta:resourcekey="lblCommentResource1"></asp:Label>
                        </td>
                        <td class="cssClassAddedOn">
                            <asp:Label ID="lblAddedOn" runat="server" Text="Added On"
                                meta:resourcekey="lblAddedOnResource1"></asp:Label>
                        </td>
                        <td class="cssClassAddToCart">
                            <asp:Label ID="lblAddToCart" runat="server" Text="Add To Cart"
                                meta:resourcekey="lblAddToCartResource1"></asp:Label>
                        </td>
                        <td class="cssClassDelete"></td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <div class="sfButtonwrapper" id="wishitemBottom">
                <button type="button" id="updateWishList" onclick="DashWishItem.UpdateWishList();" class="sfBtn">
                    <span class="sfLocale icon-refresh">Update WishList</span></button>
                <button type="button" id="clearWishList" onclick="DashWishItem.ClearWishList();" class="sfBtn">
                    <span class="sfLocale icon-clear-cache">Clear WishList</span></button>
            </div>
        </div>
    </div>
</div>
