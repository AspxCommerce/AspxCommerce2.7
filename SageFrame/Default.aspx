<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" EnableViewState="false"
    Inherits="SageFrame._Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/TopStickyBar.ascx" TagName="TopStickyBar" TagPrefix="ucstickybar" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="head" enableviewstate="false">
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE" />
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type" />
    <meta content="text/javascript" http-equiv="Content-Script-Type" />
    <meta content="text/css" http-equiv="Content-Style-Type" />
    <meta id="MetaDescription" name="DESCRIPTION" />
    <meta id="MetaKeywords" name="KEYWORDS" />
    <meta id="MetaCopyright" name="COPYRIGHT" />
    <meta id="MetaGenerator" name="GENERATOR" />
    <meta id="MetaAuthor" name="AUTHOR" />
    <meta name="RESOURCE-TYPE" content="DOCUMENT" />
    <meta name="DISTRIBUTION" content="GLOBAL" />
    <meta id="MetaRobots" runat="server" name="ROBOTS" />
    <meta name="REVISIT-AFTER" content="1 DAYS" />
    <meta name="RATING" content="GENERAL" />
    <meta http-equiv="PAGE-ENTER" content="RevealTrans(Duration=0,Transition=1)" />
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" />
    <link type="icon shortcut" runat="server" id="favicon" media="icon" href="favicon.ico" />
    <!--[if IE 9]> 
    <link rel="stylesheet" href="css/ie9.css" type="text/css" media="screen" /><![endif]-->
     <!--[if lt IE 9]> 
    <link rel="stylesheet" href="css/IE.css" type="text/css" media="screen" /><![endif]-->
    <!--[if lt IE 7]>
        <script type="text/javascript" src="/js/SageFrameCorejs/IE8.js"></script> 
     <![endif]-->
    <title>SageFrame Website</title>
    <asp:Literal runat="server" ID="ltrJQueryLibrary"></asp:Literal>
    <asp:Literal ID="SageFrameModuleCSSlinks" EnableViewState="false" runat="server"></asp:Literal>    
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <asp:PlaceHolder ID="phdDefault" runat="server"></asp:PlaceHolder>
    <div id="divAdminControlPanel" runat="server" style="display: block;">
        <ucstickybar:TopStickyBar ID="topStickybar" runat="server" />
    </div>
    <noscript>
        <asp:Label ID="lblnoScript" EnableViewState="false" runat="server" Text="This page requires java-script to be enabled. Please adjust your browser-settings."></asp:Label>
    </noscript>
    <asp:Literal ID="ltrPlaceholders" runat="server"></asp:Literal>
    <div class="sfMessagewrapper" id="divMessage" runat="server">
    </div>
    <asp:PlaceHolder ID="pchWhole" runat="server"></asp:PlaceHolder>
    <asp:PlaceHolder ID="pchtest" runat="server"></asp:PlaceHolder>
    <asp:Literal ID="LitSageScript" runat="server"></asp:Literal>
    <iframe id="divFrame" style="display: none" width='100%'></iframe>
    <div class="sfCpanel sfInnerwrapper" runat="server" id="divActivation">
        <asp:Literal runat="server" ID="ltrActivation"></asp:Literal>
    </div>
    <script type="text/javascript">
        //<![CDATA[       
        $(function () { $(this).LoadFirst('<%=templatefavicon%>'); $(".sfLocalee").SystemLocalize(); });
        //]]>
    </script>
    </form>
</body>
</html>
