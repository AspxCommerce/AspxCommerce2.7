<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebMethod.aspx.cs" Inherits="Modules_LayoutManager_WebMethod" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ul class="sfTemplateSetting">
            <li>View Demo</li>
            <li class="activate"><a href="#" id=' + activateId + '>Activate</a></li>
            <li>Custoimize
                <ul style="display: none;" class="sfTemplateEdit">
                    <li>pages</li>
                    <li>Preset</li>
                    <li>Layout Manger</li>
                </ul>
            </li>
        </ul>
        <ul class="sfTemplateManage">
            <li class="sfEditfiles"><a href=' + editFileLink + ' id="lnkEditFiles">Edit Source File</a></li>
            <li>Delete</li>
        </ul>
    </div>
    </form>
</body>
</html>
