<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModuleControlsDetails.ascx.cs"
    Inherits="SageFrame.Modules.Admin.Extensions.Editors.ModuleControlsDetails" %>
<%@ Register Src="~/Controls/sectionheadcontrol.ascx" TagName="sectionheadcontrol"
    TagPrefix="sfe" %>

<asp:UpdatePanel ID="udpModuleControlSettings" runat="server">
  <ContentTemplate>
      <div class="sfCollapsewrapper sfFormwrapper">      
        <div id="divModuleControlSetting" runat="server" class="sfCollapsecontent">
          <div class="sfMarginTopPri">
           <h3>Module Control Settings</h3>
            <p class="sfNote">
              <asp:Label ID="lblModuleControlSettingsHelp" runat="server" Text="In this section, you can set up more advanced settings for Module Controls on this Module."                        
                        meta:resourcekey="lblModuleControlSettingsHelpResource1"></asp:Label>
            </p>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
              <tr id="rowModuleEdit" runat="server" visible="False">
                <td width="20%" runat="server"><asp:Label ID="lblModuleEdit" runat="server" Text="Module" CssClass="sfFormlabel"></asp:Label></td>
                
                <td runat="server"><asp:Label ID="lblModuleD" runat="server"></asp:Label></td>
              </tr>
              <tr id="rowDefinitionEdit" runat="server" visible="False">
                <td runat="server"><asp:Label ID="lblDefinitionEdit" runat="server" Text="Definition" CssClass="sfFormlabel"></asp:Label></td>
                
                <td runat="server"><asp:Label ID="lblDefinitionD" runat="server"></asp:Label></td>
              </tr>
              <tr id="rowSource" runat="server" visible="False">
                <td runat="server"><asp:Label ID="lblSource" runat="server" Text="Source" CssClass="sfFormlabel"></asp:Label></td>
                
                <td valign="top" runat="server"><asp:DropDownList ID="ddlSource" runat="server" CssClass="sfListmenu" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlSource_SelectedIndexChanged" /></td>
              </tr>
              <tr>
                <td width="20%"><asp:Label ID="lblKey" runat="server" Text="Key" CssClass="sfFormlabel" ></asp:Label></td>
                
                <td><asp:TextBox ID="txtKey" runat="server" CssClass="sfInputbox" 
                                        meta:resourcekey="txtKeyResource1"></asp:TextBox>
                  <asp:RequiredFieldValidator Display="Dynamic" ID="rfvModulekey" runat="server" ControlToValidate="txtKey"
                                        ValidationGroup="vdgExtension" ErrorMessage="*" SetFocusOnError="True" 
                                        CssClass="sfRequired" meta:resourcekey="rfvModulekeyResource1"></asp:RequiredFieldValidator></td>
              </tr>
              <tr>
                <td><asp:Label ID="lblTitle" runat="server" Text="Title" 
                                        CssClass="sfFormlabel"></asp:Label></td>
                
                <td><asp:TextBox ID="txtTitle" runat="server" CssClass="sfInputbox" 
                                        meta:resourcekey="txtTitleResource1"></asp:TextBox>
                  <asp:RequiredFieldValidator Display="Dynamic" ID="rfvModuleTitle" runat="server" ControlToValidate="txtTitle"
                                        ValidationGroup="vdgExtension" ErrorMessage="*" SetFocusOnError="True" 
                                        CssClass="sfRequired" meta:resourcekey="rfvModuleTitleResource1"></asp:RequiredFieldValidator></td>
              </tr>
              <tr>
                <td><asp:Label ID="lblType" runat="server" Text="Type" 
                                        CssClass="sfFormlabel" ></asp:Label></td>
                
                <td><asp:DropDownList ID="ddlType" runat="server" CssClass="sfListmenu" 
                                        meta:resourcekey="ddlTypeResource1" />
                  <asp:Label ID="lblErrorControlType" runat="server" CssClass="sfError"
                                        Text="*" Visible="False" meta:resourcekey="lblErrorControlTypeResource1"></asp:Label></td>
              </tr>
              <tr id="rowDisplayOrder" runat="server" visible="False">
                <td runat="server"><asp:Label ID="lblDisplayOrder" runat="server" Text="Display Order" CssClass="sfFormlabel"></asp:Label></td>
                
                <td runat="server"><asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="sfInputbox"
                                        MaxLength="2" Text="0"></asp:TextBox></td>
              </tr>
              <tr>
                <td><asp:Label ID="lblIcon" runat="server" Text="Icon" 
                                        CssClass="sfFormlabel"></asp:Label></td>
                
                <td><asp:DropDownList ID="ddlIcon" runat="server" CssClass="sfListmenu" 
                                        meta:resourcekey="ddlIconResource1" /></td>
              </tr>
              <tr>
                <td><asp:Label ID="lblHelpURL" runat="server" Text="Help URL" 
                                        CssClass="sfFormlabel" ></asp:Label></td>
                
                <td><asp:TextBox ID="txtHelpURL" runat="server" CssClass="sfInputbox" 
                                        meta:resourcekey="txtHelpURLResource1"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="revHelpUrl" runat="server" ControlToValidate="txtHelpURL"
                                        CssClass="sfRequired" ErrorMessage="The Help Url is not valid." SetFocusOnError="True"
                                        
                                        ValidationExpression="^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&amp;?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$" 
                                        meta:resourcekey="revHelpUrlResource1"></asp:RegularExpressionValidator></td>
              </tr>
              <%--<tr>
                <td><asp:Label ID="lblSupportsPartialRendering" runat="server" Text="Supports Partial Rendering?"
                                        CssClass="sfFormlabel"></asp:Label></td>
               
                <td><asp:CheckBox ID="chkSupportsPartialRendering" runat="server" 
                                        CssClass="sfCheckbox" 
                                        meta:resourcekey="chkSupportsPartialRenderingResource1" /></td>
              </tr>--%>
            </table>
          </div>
        </div>
      </div>
      <div runat="server" id="pUpdatePane" visible="False" 
                class="sfButtonwrapper">
        <asp:LinkButton ID="imbUpdateModlueControl" runat="server" CssClass="icon-save sfBtn"
                    OnClick="imbUpdateModlueControl_Click" ValidationGroup="vdgExtension" 
                    meta:resourcekey="imbUpdateModlueControlResource1" Text="Save" />
       <%-- <asp:Label Style="cursor: hand;" ID="lblCreateModule" runat="server" Text="Save"
                    AssociatedControlID="imbUpdateModlueControl" 
                    meta:resourcekey="lblCreateModuleResource1" />--%>
        <asp:LinkButton ID="imbCancelModlueControl" runat="server" CausesValidation="False" Text="Cancel"
                    OnClick="imbCancelModlueControl_Click" CssClass="icon-close sfBtn"
                    meta:resourcekey="imbCancelModlueControlResource1" />
       <%-- <asp:Label Style="cursor: hand;" ID="lblCancel" runat="server" Text="Cancel " 
                    AssociatedControlID="imbCancelModlueControl" 
                    meta:resourcekey="lblCancelResource1" />--%>
      </div>
  </ContentTemplate>
</asp:UpdatePanel>
