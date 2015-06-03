<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HTMLEdit.ascx.cs" Inherits="SageFrame.Modules.AdvanceHTML.HTMLEdit" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET"  TagPrefix="CKEditor" %>

<div class="cssClassHTMLModule">
<asp:UpdatePanel ID="udpSage" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hdfHTMLTextID" runat="server" />
        <asp:HiddenField ID="hdnUserModuleID" runat="server" Value="0" />
        <asp:HiddenField ID="hdfIsActive" runat="server" />
        <div id="divViewWrapper" runat="server" class="cssClassViewWrraper">
            <div class="cssClassTopSetting" id="divEditContent" runat="server">                
            </div>
            <asp:Literal ID="ltrContent" EnableViewState="false" runat="server"></asp:Literal>
        </div>
        <div id="divEditWrapper" runat="server" class="cssClassEditWrapper">
            <div class="sfFormwrapper">
                <table cellspacing="0" cellpadding="0" border="0" width="95%" class="editorborder">
                    <tr>
                        <td>
                            <div class="cssClassHtmlViewBorder">
                                <table cellspacing="0" cellpadding="0" border="0" id="tblTextEditor" width="100%" runat="server" class="cssClassHtmlViewTable">
                                    <tr>
                                        <td class="editorheading">
                                            <asp:Label ID="lblView" runat="server" CssClass="sfFormlabel" Text="Editor:" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                           <CKEditor:CKEditorControl  ID="txtBody" runat="server"></CKEditor:CKEditorControl>
                                            <asp:Label ID="lblError" runat="server" CssClass="sfError" Visible="false"
                                                EnableViewState="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="sfButtonwrapper" style="display:none">
                                <asp:Button ID="btnCustomizeEditor" runat="server" CausesValidation="false" CssClass="cssClassButtonEditor"
                                    Text="Customize Editor" OnClick="btnCustomizeEditor_Click" />
                                <asp:Button ID="btnDefault" runat="server" CausesValidation="false" CssClass="cssClassButtonEditor"
                                    Text="Default Editor" OnClick="btnDefault_Click" Visible="false" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td >                        
                            <asp:Label ID="lblPublish" runat="server" CssClass="sfFormlabel" style="display:none" Text="Publish" />
                            <asp:CheckBox ID="chkPublish" runat="server" Checked="true" style="display:none" CssClass="sfCheckbox" />
                            <asp:Label ID="lblAllowComment" style="display:none" runat="server" CssClass="sfFormlabel" Text="Allow Comment" />
                            <asp:CheckBox ID="chkAllowComment" style="display:none" runat="server" CssClass="sfCheckbox" />
                        </td>
                    </tr>
                </table>
                <div class="sfButtonwrapper">
                    <asp:ImageButton ID="imbSave" runat="server" OnClick="imbSave_Click" ValidationGroup="body" />
                    <asp:Label ID="lblSave" runat="server" Text="Save" AssociatedControlID="imbSave" CssClass="cssClassHtmlViewCursor"></asp:Label>
                </div>
            </div>
        </div>
        <div id="divAddComment" runat="server" style="display:none" class="cssClassCommentWrapper">
            <div class="cssClassAddComment cssClassTopSetting">
                <asp:ImageButton ID="imbAddComment" runat="server" OnClick="imbAddComment_Click" />
                <asp:Label ID="lblAddComment" runat="server" Text="Add Comment" AssociatedControlID="imbAddComment" CssClass="cssClassHtmlViewCursor"></asp:Label>
            </div>
        </div>
        <div id="divViewComment" style="display:none" runat="server" class="cssClassCommentWrapper">
            <div class="cssClassGridWrapper">
                <div class="cssClassHtmlViewComentTable"><asp:GridView ID="gdvHTMLList" runat="server" AutoGenerateColumns="False" Width="100%" GridLines="none" RowStyle-VerticalAlign="Top"
                    AllowPaging="True" PageSize="15" OnPageIndexChanging="gdvList_PageIndexChanging"
                    OnRowCommand="gdvList_RowCommand" OnRowDataBound="gdvList_RowDataBound" OnRowDeleting="gdvList_RowDeleting"
                    OnRowEditing="gdvList_RowEditing" OnRowUpdating="gdvList_RowUpdating" OnSelectedIndexChanged="gdvHTMLList_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblCommentCount" runat="server"></asp:Label>
                                <asp:Label ID="lblCommentTitle" runat="server" Text="Comment(s)"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="cssClassCommentBy">
                                    <asp:Label ID="lblAddedBy" runat="server" Text='<%#Eval("AddedBy") + " on " + Eval("AddedOn") + " Says:"%>' /></div>
                                <div class="cssClassComment">
                                    <asp:Literal ID="ltrComment" runat="server" Text='<%#Eval("Comment") %>'></asp:Literal></div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandArgument='<%#Eval("HTMLCommentID") %>'
                                    CommandName="Edit" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>'
                                    ToolTip="Edit" CssClass="cssClassColumnEdit" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDelete" runat="server" CausesValidation="False" CommandArgument='<%#Eval("HTMLCommentID") %>'
                                    CommandName="Delete" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
                                    ToolTip="Delete" CssClass="cssClassColumnDelete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="cssClassHeadingOne" />
                    <RowStyle CssClass="cssClassAlternativeOdd" />
                    <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
                </asp:GridView></div>
            </div>
        </div>
        <div id="divEditComment" style="display:none" runat="server" class="cssClassEditCommentWrapper">
            <div class="sfFormwrapper">
                <table id="tblEditComment" runat="server" cellpadding="0" cellspacing="0" border="0" class="cssClassHtmlViewTable">
                 <tr>
                        <td>
                            <asp:Label ID="lblComment" runat="server" CssClass="sfFormlabel" Text="Comment">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtComment" CssClass="CssClassNormalTextBoxHtml" runat="server" TextMode="MultiLine"
                                Rows="6" ValidationGroup="ValidateComment">
                            </asp:TextBox>                            
                        </td>
                    </tr>
                    <tr>
                        <td>                            
                        </td>
                        <td>
                            <asp:Label ID="lblErrorMessage" runat="server" CssClass="sfError" EnableViewState="false"></asp:Label>                        
                        </td>
                    </tr>
                    <tr id="rowApprove" runat="server">
                        <td>
                            <asp:Label ID="lblApprove" runat="server" CssClass="sfFormlabel" Text="Approve"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkApprove" runat="server" CssClass="sfCheckbox"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr id="rowIsActive" runat="server">
                        <td>
                            <asp:Label ID="lblIsActive" runat="server" CssClass="sfFormlabel" Text="IsActive" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsActive" runat="server" CssClass="sfCheckbox"></asp:CheckBox>
                        </td>
                    </tr>
                </table>
                <div class="sfButtonwrapper" style="display:none">
                    <asp:ImageButton ID="imbAdd" runat="server" OnClick="imbAdd_Click" ValidationGroup="ValidateComment"
                        CausesValidation="true" />
                    <asp:Label ID="lblAdd" runat="server" Text="Add" AssociatedControlID="imbAdd" CssClass="cssClassHtmlViewCursor"></asp:Label>
                    <asp:ImageButton ID="imbBack" runat="server" OnClick="imbBack_Click" />
                    <asp:Label ID="lblBack" runat="server" Text="Cancel" AssociatedControlID="imbBack" CssClass="cssClassHtmlViewCursor"></asp:Label>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</div>