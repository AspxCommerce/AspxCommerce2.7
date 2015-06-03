<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemSocialLinks.ascx.cs" Inherits="Modules_AspxCommerce_AspxItemSocialLinks_ItemSocialLinks" %>
<div class="smpl-share clearfix">
    <div class="facebook item"><i class="i-facebook"></i></div>
    <div class="twitter item"><i class="i-twitter"></i></div>
    <div class="googleplus item"><i class="i-googleplus"></i></div>
    <div class="linkedin item"><i class="i-linkedin"></i></div>
</div>
<script>
    $(document).ready(function () {
        $(".smpl-share").smplShare({
            services: ['twitter', 'facebook', 'googleplus', 'linkedin']
        });
    });
</script>
