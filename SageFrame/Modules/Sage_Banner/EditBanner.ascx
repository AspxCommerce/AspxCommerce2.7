<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditBanner.ascx.cs" Inherits="Modules_Sage_Banner_EditBanner" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script type="text/javascript">
    //<![CDATA[
    var editorID = '<%= _imageEditor.ClientID %>';
    jQuery(function () {
        jQuery('#' + editorID).Jcrop({
            onChange: showCoords,
            onSelect: showCoords
        });
    });
    function showCoords(c) {
        var tdX = document.getElementById('tdX');
        var tdY = document.getElementById('tdY');
        var tdWidth = document.getElementById('tdWidth');
        var tdHeight = document.getElementById('tdHeight');
        tdX.innerHTML = c.x;
        tdY.innerHTML = c.y;
        tdWidth.innerHTML = c.w;
        tdHeight.innerHTML = c.h;
        var xField = document.getElementById('<%= _xField.ClientID %>');
        var yField = document.getElementById('<%= _yField.ClientID %>');
        var widthField = document.getElementById('<%= _widthField.ClientID %>');
        var heightField = document.getElementById('<%= _heightField.ClientID %>');
        xField.value = c.x;
        yField.value = c.y;
        widthField.value = c.w;
        heightField.value = c.h;
    }
    $(document).ready(function () {
        $('.ui-icon-newwin').trigger("click");
        $('#imbAddBanner').bind("click", function () {
            ShowPopUp('divAddBanner');
        });
        $('#fade').bind("click", function () {
            $('#fade,#divAddBanner').fadeOut();
        });
        $('#imbAddHtmlContent').bind("click", function () {
            ClearHTMLForm();
            $('#<%=divHtmlBannerContainer.ClientID%>').hide();
            $('#<%=divEditWrapper.ClientID%>').show();
        });
        $('#imbAddNewImage').bind("click", function () {
            ClearImageForm();
            $('#<%=divbannerImageContainer.ClientID%>').hide();
            $('#<%=divEditBannerImage.ClientID%>').show();
        });
        var webUrl = '<%=WebUrl %>';
        if (webUrl == 1) {
            $('#trddlPagesLoad').hide();
            $('#trtxtWebUrl').show();
            var weburl = $('#<%=txtWebUrl.ClientID %>');
            if (weburl.val().length == 0) {
                $('#<%=txtWebUrl.ClientID %>').val('http://');
            }
        }
        else if (webUrl == 0) {
            $('#trddlPagesLoad').show();
            $('#trtxtWebUrl').hide();
        }
    });
    function ClearImageForm() {
        $("#<%= txtCaption.ClientID %>").val('');
        $("#<%= txtReadButtonText.ClientID %>").val('');
        $("#<%= txtBannerDescriptionToBeShown.ClientID %>").val('');
        $("#<%= imgEditBannerImageImage.ClientID %>").hide();
        $("#<%=txtWebUrl.ClientID %>").val("http://");
    }
    function ClearHTMLForm() {
        $("#<%= imgEditNavImage.ClientID %>").hide();
    }
    function ShowPopUp(popupid) {
        $('#' + popupid).fadeIn();
        var popuptopmargin = ($('#' + popupid).height() + 10) / 2;
        var popupleftmargin = ($('#' + popupid).width() + 10) / 2;
        $('#' + popupid).css({
            'margin-top': -popuptopmargin,
            'margin-left': -popupleftmargin
        });
    }
    function GetRadioButtonListSelectedValue(radioButtonList) {
        for (var i = 0; i < radioButtonList.rows.length; ++i) {
            if (radioButtonList.rows[i].cells[0].firstChild.checked) {
                $('#trddlPagesLoad').show();
                $('#trtxtWebUrl').hide();
            }
            else {
                $('#trddlPagesLoad').hide();
                $('#trtxtWebUrl').show();
                var webUrl = $('#<%=txtWebUrl.ClientID %>');
                if (webUrl.val().length == 0) {
                    $('#<%=txtWebUrl.ClientID %>').val('http://');
                }
            }
        }
    }
    $('#txtWebUrl').on("change", function () {
        var DemoUrl = $(this).val();
        if ($(this).val().length > 0) {
            if (!$(this).val().match(/^http/)) {
                $(this).val('http://' + DemoUrl);
            }
        }
    });
    //]]> 
</script>
<asp:Panel ID="pnlBannercontainer" Style="display: none;" runat="server" CssClass="sfFormwrapper"
    Width="100%">
    <cc1:TabContainer ID="SageBannerTabcontainer" runat="server" ActiveTabIndex="0" Width="100%">
        <cc1:TabPanel ID="tpSageBanner" runat="server">
            <HeaderTemplate>
                <asp:Label ID="lblBannerImage" runat="server" CssClass="sfFormlabel" Text='Banner Image' />
            </HeaderTemplate>
            <ContentTemplate>
                <p class="sfNote">
                    <asp:Label ID="lblAddBanner" runat="server" Text=' In this section, you can add and manage Banner image.' />
                </p>
                <div id="divbannerImageContainer" runat="server">
                    <div class="sfButtonwrapper sftype1">
                        <label id="imbAddNewImage" class="icon-addnew sfBtn">
                            Add Banner Image</label>
                    </div>
                    <div id="dvGrid" class="sfGridwrapper sfBannerEdit">
                        <asp:GridView ID="gdvBannerImages" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            EmptyDataText="..........No Data Found.........." GridLines="None" OnPageIndexChanged="gdvBannerImages_PageIndexChanged"
                            OnRowCommand="gdvBannerImages_RowCommand" OnRowDataBound="gdvBannerImages_RowDataBound"
                            OnRowDeleting="gdvBannerImages_RowDeleting" OnRowEditing="gdvBannerImages_RowEditing"
                            Width="100%" OnPageIndexChanging="gdvBannerImages_PageIndexChanging" PageSize="6">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Panel ID="pnlImage" runat="server">
                                            <img id="bannerimgGrd" alt="image" height="50" src='<%#ResolveUrl("~/Modules/Sage_Banner/images/ThumbNail/Small/"+Eval("ImagePath")) %>'
                                                width="50" /></asp:Panel>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        Image
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkImageEdit" runat="server" CommandArgument='<%# Eval("ImageID") %>'
                                            CommandName="Editimage" CssClass="icon-crop"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div>
                                            <asp:LinkButton ID="imgListUp" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ImageID") %>'
                                                CommandName="SortUp" CssClass="icon-arrow-n" ToolTip="Move Up" meta:resourcekey="imgListUpResource1"></asp:LinkButton>
                                        </div>
                                        <div>
                                            <asp:LinkButton ID="imgListDown" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ImageID") %>'
                                                CommandName="SortDown" ToolTip="Move Down" meta:resourcekey="imgListDownResource1"
                                                CssClass="icon-arrow-s"></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="sfEdit" Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgEdit" runat="server" CssClass="icon-edit" CausesValidation="False"
                                            CommandArgument='<%# Eval("ImageID") %>' CommandName="Edit" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="cssClassColumnDelete" Width="60px" VerticalAlign="Top" />
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imdDelete" runat="server" CssClass="icon-delete" CausesValidation="False"
                                            CommandArgument='<%# Eval("ImageID") %>' CommandName="Delete" OnClientClick="return ConfirmDialog(this, 'Confirmation', 'Are you sure you want to delete ?');" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="sfDelete" Width="60px" VerticalAlign="Top" />
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle CssClass="sfEven" />
                            <PagerStyle CssClass="sfPagination" />
                            <RowStyle CssClass="sfOdd" />
                            <EmptyDataRowStyle CssClass="sfEmptyrow" />
                        </asp:GridView>
                    </div>
                </div>
                <div id="divEditBannerImage" runat="server">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="150">
                                <asp:Label ID="lblReadMorePageType" runat="server" CssClass="sfFormlabel">Read More Page Type</asp:Label>
                            </td>
                            <td width="30">
                                :
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdbReadMorePageType" runat="server" CssClass="sfRadiobutton"
                                    onclick="GetRadioButtonListSelectedValue(this);" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Selected="True">Page</asp:ListItem>
                                    <asp:ListItem Value="1">Web Url</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr id="trddlPagesLoad">
                            <td>
                                <asp:Label ID="lblReadMorePages" runat="server" CssClass="sfFormlabel">Redirect To:</asp:Label>
                            </td>
                            <td width="30">
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPagesLoad" runat="server" CssClass="sfListmenu">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trtxtWebUrl" style="display: none;">
                            <td>
                                <asp:Label ID="lblWebUrl" runat="server" CssClass="sfFormlabel">Web Link</asp:Label>
                            </td>
                            <td width="30">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtWebUrl" runat="server" CssClass="sfInputbox">http://</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblChooseFile" runat="server" CssClass="sfFormlabel">Choose Image</asp:Label>
                            </td>
                            <td width="30">
                                :
                            </td>
                            <td>
                                <asp:FileUpload ID="fuFileUpload" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Image ID="imgEditBannerImageImage" runat="server" Visible="False" CssClass="sfBannerimage" />
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>
                                <asp:Label ID="lblCaptionDetail" runat="server" CssClass="sfFormlabel">Caption</asp:Label>
                            </td>
                            <td width="30">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtCaption" runat="server" CssClass="sfInputbox" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblReadButtonText" runat="server" CssClass="sfFormlabel">Read Button Text</asp:Label>
                            </td>
                            <td width="30">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtReadButtonText" runat="server" CssClass="sfInputbox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblBannerDescriptionToBeShow" runat="server" Text="Banner Description:"
                                    CssClass="sfFormlabel"></asp:Label>
                            </td>
                            <td width="30">
                                :
                            </td>
                            <td valign="top">
                                <CKEditor:CKEditorControl ID="txtBannerDescriptionToBeShown" runat="server" Height="150px"
                                    TextMode="MultiLine" Width=""></CKEditor:CKEditorControl>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <div class="sfButtonwrapper sfMarginnone">
                                    <label class="sfLocale icon-save sfBtn">
                                        Save
                                        <asp:Button ID="imbSave" runat="server" ValidationGroup="text" OnClick="imbSave_Click" />
                                    </label>
                                    <label class="sfLocale icon-close sfBtn">
                                        Cancel
                                        <asp:Button ID="imbCancel" runat="server" OnClick="imbCancel_Click" />
                                    </label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="tpSageBannerHTML" runat="server">
            <HeaderTemplate>
                HTML Content
            </HeaderTemplate>
            <ContentTemplate>
                <p class="sfNote">
                    <asp:Label ID="lblHTMLContentAdd" runat="server" Text='In this section, you can add and manage Banner HTML Content.' />
                </p>
                <div id="divHtmlBannerContainer" runat="server">
                    <div class="sfButtonwrapper sftype1">
                        <label class="sfLocale icon-addnew sfBtn">
                            Add HtmlContent
                            <asp:Button ID="imAddHtmlContent" runat="server" ValidationGroup="save" OnClick="imAddHtmlContent_Click" />
                        </label>
                    </div>
                    <div id="divHTMLContent" runat="server" class="sfGridwrapper">
                        <asp:GridView ID="gdvHTMLContent" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            EmptyDataText="..........Data Not Found.........." GridLines="None" Width="100%"
                            OnRowCommand="gdvHTMLContent_RowCommand" PageSize="3" OnPageIndexChanging="gdvHTMLContent_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        BannerID
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblImageID" runat="server" Font-Bold="true" Text='<%# Eval("ImageID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        HTML Content
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblHTMLBodyText" runat="server" Font-Bold="true" Text='<%# Eval("HTMLBodyText")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgEdit" CssClass="icon-edit" runat="server" CausesValidation="False"
                                            CommandArgument='<%# Eval("ImageID") %>' CommandName="EditHTML" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="sfDelete" VerticalAlign="Top" />
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imdDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ImageID") %>'
                                            CommandName="DeleteHTML" CssClass="icon-delete" OnClientClick="return ConfirmDialog(this, 'Confirmation', 'Are you sure you want to delete ?');" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="sfDelete" VerticalAlign="Top" />
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="sfPagination" />
                            <RowStyle CssClass="sfOdd" />
                            <AlternatingRowStyle CssClass="sfEven" />
                            <EmptyDataRowStyle CssClass="sfEmptyrow" />
                        </asp:GridView>
                    </div>
                </div>
                <div id="divEditWrapper" runat="server">
                    <div class="sfFormwrapper">
                        <table cellspacing="0" width="100%" cellpadding="0" border="0" class="editorborder">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" id="tblTextEditor"
                                        runat="server">
                                        <tr>
                                            <td class="tdCkeditor">
                                                <CKEditor:CKEditorControl ID="txtBody" runat="server" Height="350px"> </CKEditor:CKEditorControl>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="display: none;">
                                    <asp:Label ID="lblNavigationImage" runat="server" Text="Navigation Image" CssClass="sfFormlabel"></asp:Label>
                                    <asp:FileUpload ID="fluBannerNavigationImage" runat="server" CssClass="cssClassNormalFileUpload" />
                                    <br />
                                    <asp:Image ID="imgEditNavImage" runat="server" Visible="false" />
                                </td>
                            </tr>
                        </table>
                        <div class="sfButtonwrapper">
                            <label class="sfLocale icon-save sfBtn">
                                Save
                                <asp:Button ID="imbSaveEditorContent" runat="server" ValidationGroup="save" OnClick="imbSaveEditorContent_Click" /></label>
                            <%--<asp:Label ID="lblSaveEditorContent" runat="server" Text="Save" AssociatedControlID="imbSaveEditorContent"></asp:Label>--%>
                            <label class="sfLocale icon-close sfBtn">
                                Cancel
                                <asp:Button ID="imgCancelHtmlContent" runat="server" OnClick="imgCancelHtmlContent_Click" /></label>
                            <%--<asp:Label ID="lblcancelHtml" runat="server" Text="Cancel" AssociatedControlID="imgCancelHtmlContent"></asp:Label>--%>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    <div class="sftype1 sfMargintop">
        <label class="sfLocale  icon-arrow-slimdouble-w">
            Back
            <asp:Button runat="server" ID="imbReturnBack" OnClick="imbReturnBack_Click" /></label>
    </div>
</asp:Panel>
<asp:Panel ID="pnlBannerList" runat="server" meta:resourcekey="pnlPageListResource1"
    CssClass="sfFormwrapper">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="20%">
                <asp:Label ID="lblBannerName" runat="server" CssClass="sfFormlabel">Banner Name</asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:TextBox ID="txtBannerName" runat="server" CssClass="sfInputbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtBannerName" runat="server" ControlToValidate="txtBannerName"
                    SetFocusOnError="true" ValidationGroup="bannername" ErrorMessage="*" CssClass="cssClasssNormalRed"
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td>
                <div class="sfButtonwrapper">
                    <label class="sfLocale icon-addnew sfBtn">
                        Add
                        <asp:Button ID="imbSaveBanner" runat="server" CssClass="icon-addnew sfBtn" ValidationGroup="bannername"
                            OnClick="imbSaveBanner_Click" /></label>
                </div>
            </td>
        </tr>
        <tr style="display: none">
            <td>
                <asp:Label ID="lblBannerDescription" runat="server" CssClass="sfFormlabel">Description</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtBannerDescription" runat="server" CssClass="sfInputbox" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="sfGridwrapper">
        <asp:GridView ID="gdvBannerList" AllowPaging="true" runat="server" AutoGenerateColumns="False"
            GridLines="None" Width="100%" meta:resourcekey="gdvPageListResource1" OnRowCommand="gdvBannerList_RowCommand"
            OnPageIndexChanging="gdvBannerList_PageIndexChanging" PageSize="6">
            <Columns>
                <asp:TemplateField HeaderText="Banner Name" meta:resourcekey="TemplateFieldResource48">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnBannerID" runat="server" Value='<%# Eval("BannerID") %>' />
                        <asp:HiddenField ID="hdnBannerName" runat="server" Value='<%# Eval("BannerName") %>' />
                        <asp:LinkButton ID="lnkBannerName" runat="server" Value='<%# Eval("BannerName") %>'
                            Text='<%# Eval("BannerName")%>' CommandArgument='<%# Eval("BannerID")%>' CommandName="BannerEdit"
                            meta:resourcekey="lnkPageNameResource1"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton ID="imgEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("BannerID")%>'
                            CommandName="BannerEdit" CssClass="icon-edit" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="sfDelete" VerticalAlign="Top" />
                    <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="imbDeletePage" CssClass="icon-delete" runat="server" CommandName="BannerDelete"
                            AlternateText="Delete" CommandArgument='<%# Eval("BannerID") %>' OnClientClick="return ConfirmDialog(this, 'Confirmation', 'Are you sure you delete this banner?');"
                            meta:resourcekey="imbDeletePageResource1" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="sfDelete" />
                </asp:TemplateField>
            </Columns>
            <AlternatingRowStyle CssClass="sfEven" />
            <RowStyle CssClass="sfOdd" />
            <PagerStyle CssClass="sfPagination" />
        </asp:GridView>
    </div>
</asp:Panel>
<div id="divAddBanner" class="sfPopup sfFormwrapper" style="display: none">
    <span class="sfPopupclose"></span>
</div>
<div id="divImageEditor" runat="server" style="display: none;" class="sfFormwrapper">
    <div class="cssClassFormHeading">
        <asp:Label ID="lblCropArea" runat="server" CssClass="sfFormlabel"> Drag on the image area to crop the image:</asp:Label>
    </div>
    <div>
        <asp:Image runat="server" ID="_imageEditor" />
    </div>
    <table>
        <tr>
            <td>
                x:
                <td>
                    x:
                </td>
                <td id="tdX">
                    -
                </td>
                <td>
                    y:
                </td>
                <td id="tdY">
                    -
                </td>
                <td>
                    width:
                </td>
                <td id="tdWidth">
                    -
                </td>
                <td>
                    height:
                </td>
                <td id="tdHeight">
                    -
                </td>
        </tr>
    </table>
    <div class="sfButtonwrapper">
        <label id="lblSaveCrop" class="sfLocale icon-save sfBtn">
            Save
            <asp:Button runat="server" ID="_cropCommand" OnClick="_cropCommand_Click" />
        </label>
        <label id="lblCancelImageEdit" class="sfLocale icon-close sfBtn">
            Cancel
            <asp:Button ID="imbCancelImageEdit" runat="server" OnClick="imbCancelImageEdit_Click" />
        </label>
    </div>
    <input type="hidden" runat="server" id="_xField" />
    <input type="hidden" runat="server" id="_yField" />
    <input type="hidden" runat="server" id="_widthField" />
    <input type="hidden" runat="server" id="_heightField" />
</div>
