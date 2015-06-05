<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Adsense.ascx.cs" Inherits="SageFrame.Modules.Admin.Adsense.Adsense" %>
<%@ Register Assembly="SageFrame.GoogleAdUnit" Namespace="SageFrame.GoogleAdUnit" TagPrefix="wwc" %>
<asp:HiddenField ID="hdnUserModuleID" runat="server" Value="0" />
<wwc:AdUnit ID="AdsenseDisplay" runat="server" Visible="False" 
    AdUnitFormat="LeaderBoard_728x90_H" AdUnitType="TextAndImage" AffiliateId="" 
    AlternateAdType="PublicServiceAds" AnotherUrl="" BackColor="White" 
    BorderColor="AliceBlue" ChannelId="" LinkColor="Blue" SolidFillColor="Blue" 
    TextColor="Black" UrlColor="Green">
</wwc:AdUnit>
