<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopSearchTerms.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxAdminDashBoard_TopSearchTerms" %>

<script type="text/javascript">
    //<![CDATA[

    //]]>
</script>



<div class="cssClassTabPanelTable sfPriMrg-t" >

    <div id="container-8">
        <ul>
            <li><a href="#fragment-1">
                <asp:Label ID="lblTabTopSearch" runat="server" Text="Top Search " 
                    meta:resourcekey="lblTabTopSearchResource1"></asp:Label>
            </a></li>
            <li><a href="#fragment-2">
                <asp:Label ID="lblTabLatestSearch" runat="server" Text="Latest Search" 
                    meta:resourcekey="lblTabLatestSearchResource1"></asp:Label>
            </a></li>
        </ul>
        <div id="fragment-1" style="padding:10px 0 0;">
            <div class="cssClassCommonBox Curve">
            <div class="sfGridwrapper">
                <div id="divTopSearchTerms">
                </div>
            </div>
            </div>
        </div>
        
        <div id="fragment-2" style="padding:10px 0 0;">
            <div class="cssClassCommonBox Curve">
            <div class="sfGridwrapper">
                <div id="divLatestSearchTerms">
                </div>
            </div>
            </div>
        </div>
    </div>
   
</div>
