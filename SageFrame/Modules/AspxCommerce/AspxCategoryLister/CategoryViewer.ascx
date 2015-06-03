<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryViewer.ascx.cs"
    Inherits="Modules_CategoryLister_CategoryViewer" %>
<div id="divCategoryListerHorizantal" class="cssClassCategoryWrapper">
    <div class="cssClassCategoryWrapperHor" id="divCategoryListerH" runat="server" enableviewstate="false"
        style="display: none;">
    </div>
</div>
<script type="text/javascript">
    var categoryRss = '<%=CategoryRss %>';
    var rssFeedUrl = '<%=RssFeedUrl %>';
    $(document).ready(function () {
        $(".sfLocale").localize({
            moduleKey: AspxCategoryLister
        });
        $(".cssClassCategoryWrapperHor").show();
        $(".sf-menu").superfish();
    });
</script>