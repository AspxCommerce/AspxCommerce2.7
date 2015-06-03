<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CDNView.ascx.cs" Inherits="Modules_HTML_CDNView" %>

<script type="text/javascript">
    $(function() {
        $(this).SageFrameCDN({
            UserModuleID: '<%=UserModuleID %>'
        });
    });
</script>

<%--<div id="divEnableCDN">
    <label id="lblEnableCDN" class="sfFormlabel">
        Enable CDN :</label>
    <input type="checkbox" id="chkEnableCDN" />
</div>--%>
<h1>
    Content Delivery Network</h1>
<div class="clearfix sfAddUrl">
    <div class="sfCol_50">
        <fieldset>
            <legend>Add JS Url</legend>
            <div id="divAddjsURL">
            </div>
            <div id="divAddJS">
                <span id="spnAddJS" class="sfFormlabel sfLocale icon-addnew sfBtn">Add JS Url </span>
            </div>
        </fieldset>
    </div>
    <div class="sfCol_50">
        <fieldset>
            <legend>Add CSS Url</legend>
            <div id="divAddcssURL">
            </div>
            <div id="divAddCSS">
                <span id="spnAddCSS" class="sfFormlabel sfLocale icon-addnew sfBtn">Add CSS Url
                </span>
            </div>
        </fieldset>
    </div>
    <div id="divSave" style="display: none; clear: both;" class="clearfix">
        <br />
        <span id="spnSave" class="sfFormlabel sfLocale icon-save sfBtn ">Save </span>
    </div>
</div>
<div id="divViewList">
    <ul id="ulUrlList">
    </ul>
</div>
<div id="divEdit">
    <span id="spnEdit" class="sfEdit sfLocale icon-edit sfBtn">Edit </span>
</div>
