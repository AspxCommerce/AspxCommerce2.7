<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemImageCloudGallery.ascx.cs" Inherits="Modules_AspxCommerce_AspxImageCloudGallery_ItemImageCloudGallery" %>

<script type="text/javascript">
    //<![CDATA[
$(function () {
    //$(".sfLocale").localize({
    //    moduleKey: AspxItemImageGallery
    //});
    $(this).ItemImageGallery({
        ItemImageGalleryModulePath: '<%=ItemImageGalleryModulePath %>',
        referImagePath: '<%=referImagePath %>',
        ImageCount:'<%=ImageCount %>'
    });
});
    //]]>
</script>

<div class="cssClassProductBigPicture cssClassPad30 clearfix" style="display:none;">     
    <asp:Literal ID="ltrItemGallery" runat="server"></asp:Literal>   
    <asp:Literal ID="ltrItemThumb" runat="server"></asp:Literal>    
</div>
