<%@ Control Language="C#" AutoEventWireup="true" CodeFile="track_usps.ascx.cs" Inherits="Modules_AspxCommerce_AspxUSPS_track_usps" %>
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
            <div class="sfError">
                <asp:Label runat="server" ForeColor="Red" ID="lblError" 
                    meta:resourcekey="lblErrorResource1"></asp:Label>
            </div>
        </div>
    </div>
</div>
