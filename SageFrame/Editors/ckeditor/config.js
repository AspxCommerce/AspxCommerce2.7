/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {


    var location = window.location.href;

    var fileLocationPath = "";
    var count = location.match(/\//g).length;

    var num_slashes = count >= 5 ? count - 5 : count - 4;
    for (var i = 0; i <= num_slashes; i++) {
        fileLocationPath += "../";
    }

    config.filebrowserUploadUrl = fileLocationPath + 'Editors/ckeditor/UploadHandler.ashx?userModuleId=' + ckEditorUserModuleID + '&portalID=' + SageFramePortalID + '&userName=' + SageFrameUserName + '&sageFrameSecureToken=' + SageFrameSecureToken;
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    // CKEDITOR.editorConfig = function (config) {
    //    config.extraPlugins = "newplugin";
    //    config.toolbar =
    //    [
    //        ['Bold'], ['Italic'], ['newplugin']
    //    ];
    //};


    config.filebrowserBrowseUrl = fileLocationPath + 'Editors/ckeditor/FileBrowsddder.aspx?path=Userfiles/File&editor=FCK&userModuleId=' + ckEditorUserModuleID;
    config.filebrowserImageBrowseUrl = fileLocationPath + 'Editors/ckeditor/FileBrowser.aspx?type=Image&path=Userfiles/Image&editor=FCK&userModuleId=' + ckEditorUserModuleID;
    //  config.filebrowserWindowWidth: 800;
    // config.filebrowserWindowHeight: 500;
    config.enterMode = CKEDITOR.ENTER_BR;
    config.toolbar =
    [
        ['Source'], ['NewPage', 'Preview'],
        ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord'],
        '/',
        ['Styles', 'Format'],
        ['Bold', 'Italic', 'Strike'],
        ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'Image']

    ];
    // ALLOW <i></i>
    config.protectedSource.push(/<i[^>]*><\/i>/g);
};
//CKEDITOR.replace('editor_id', {
//    filebrowserBrowseUrl: '/browser/browse/type/all',
//    filebrowserUploadUrl: '/browser/upload/type/all',
//    filebrowserImageBrowseUrl: '/browser/browse/type/image',
//    filebrowserImageUploadUrl: '/browser/upload/type/image',
//   
//});
