<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShipmentTrackProcessor.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxShipmentsManagement_ShipmentTrackProcessor" %>


        <div id="dvProviderList" class="sfFormwrapper">
            <p>
                <asp:Label runat="server" Text="Please Choose Shipping Provider:" 
                    meta:resourcekey="LabelResource1"></asp:Label>
            </p>
            <asp:RadioButtonList runat="server" ID="rblProviderList" RepeatDirection="Horizontal"
                AutoPostBack="True" 
                OnSelectedIndexChanged="rblProviderList_SelectedIndexChanged" 
                meta:resourcekey="rblProviderListResource1">
            </asp:RadioButtonList>
        </div>
        <div class="clear">
        </div>
        <div id="dvTrackProvider">
            <asp:PlaceHolder runat="server" ID="phTrackPackageHolder"></asp:PlaceHolder>
        </div>
        <div class="clearboth">
        </div>
        <div id="dvTrackCustom" runat="server" visible="False">
            <div class="sfFormwrapper">
                <div class="sfGridwrapper">
                    <div class="sfGridwrapperContent">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Tracking No:" ID="lblTrackingNo" 
                                        meta:resourcekey="lblTrackingNoResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTrackingNo" 
                                        meta:resourcekey="txtTrackingNoResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvTrackNo" ControlToValidate="txtTrackingNo"
                                        ErrorMessage="*" ValidationGroup="track" 
                                        meta:resourcekey="rfvTrackNoResource1"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <div class="sfButtonwrapper">
                                        <p>
                                            <asp:Button runat="server" ID="btnTrack" Text="Track Package" ValidationGroup="track"
                                                OnClick="btnTrack_Click" meta:resourcekey="btnTrackResource1" />
                                        </p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div class="clear">
                        </div>
                        <div class="sfFormwrapper">
                            <div class="sfGridwrapper">
                                <div id="dvTrackResponse" runat="server">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

