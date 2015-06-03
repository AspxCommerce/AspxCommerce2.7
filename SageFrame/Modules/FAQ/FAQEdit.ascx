<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FAQEdit.ascx.cs" Inherits="Modules_FAQ_FAQEdit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script type="text/javascript">

    var FAQquestion = '';
    var PortalID = '<%=PortalID %>';
    var UserModuleID = '<%=UserModuleID %>';
    var baseURL = '<%=baseURL %>';
    $(function () {
        $('#spnClose').click(function () {
            $('#fade,#dvPreviewGraph').fadeOut();
            RemoveGraph();
        });
        $('#spnReviewclose').click(function () {
            $('#fade,#dvReview').fadeOut();
        });
        $('#imgdeleteReview').live('click', function () {
            var ReviewID = $(this).parent().attr('id');
            DeleteUserReview(ReviewID);
            $(this).parent().parent().html('');
        });
    });

    function flipFlopException(eTarget) {
        if (document.getElementById(eTarget).style.display == 'none') {
            document.getElementById(eTarget).style.display = '';
        }
        else {
            document.getElementById(eTarget).style.display = 'none';
        }
    }

    function ShowPopup(obj) {
        FAQId = ($(obj).attr("FAQId"));
        GetGraphDetails(FAQId);
        ShowPopUp("dvPreviewGraph");
        return false;
    }

    function ShowReviewList(obj) {
        FAQquestion = ($(obj).attr("FAQ"));
        FAQId = ($(obj).attr("FAQId"));
        GetUserReviewList(FAQId);
        ShowPopUp("dvReview");
        return false;
    }

    function clickButton(e, buttonid) {     
        var evt = e ? e : window.event;
        var bt = document.getElementById(buttonid);
        if (bt) {
            if (evt.keyCode == 13) {
                bt.click();
                return false;
            }
        }
    }

    function GetUserReviewList(FAQId) {
        $.ajax({
            type: "POST",
            url: baseURL + 'WebService.asmx/GetFaqUserReview',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ FAQId: FAQId }),
            dataType: "json",
            success: function (msg) {
                $('#spnHeaderquestion').html(FAQquestion);
                var html = '';
                $.each(msg.d, function (index, item) {
                    html += '<li class="sfReviewlist"><span id="' + item.UserReviewID + '" class="spndelete"><img id="imgdeleteReview" alt="" src="' + baseURL + 'images/delete.png"/></span><p class="review">' + item.Review + '</p>';
                    html += '<span class="sfuserEmail">' + item.userEmail + '</span></br><span class="sfReviewdate">' + item.AddedOn + '</span></li>';
                });

                $('#ulFaqReview').html(html);
            },
            error: function () {
                SageFrame.messaging.show(getLocale(FAQLanguage, "Error during data loading"), "Alert");
            }
        });
    }

      function GetGraphDetails(FAQId) {
        $.ajax({
            type: "POST",
            url: baseURL + 'WebService.asmx/GetGraphDetails',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ FAQId: FAQId }),
            dataType: "json",
            success: function (msg) {
                $("#dvGraphs").html('');
                $('#h2EmptyGraph').css('display', 'none');
                var count = 0;
                $.each(msg.d, function (index, item) {
                    if (item.Counter != 0) {
                        count++;
                    }
                });
                if (count > 0) {
                    var orderChart = '<table><thead><tr><td></td>';
                    $.each(msg.d, function (index, item) {
                        orderChart += "<th>" + item.Option + "</th></td>";
                    });
                    orderChart += '</tr><tr>';
                    $.each(msg.d, function (index, item) {
                        orderChart += "<td>" + item.Counter + "</td>";
                    });
                    orderChart += '</tr></tbody></table>';
                    $("#dvGraphs").append(orderChart);
                    $("#dvGraphs").visualize();
                }
                else {
                    $('#h2EmptyGraph').css('display', 'block');
                }
            },
            error: function () {
                SageFrame.messaging.show(getLocale(FAQLanguage, "Error during data loading"), "Alert");
            }
        });
    }
    function RemoveGraph() {
        $('.visualize-bar').remove();
        $('.visualize-pie').remove();
        $('.visualize-line').remove();
    }

    function ShowPopUp(popupid) {
        $('#' + popupid).attr('style', "");
        $('#' + popupid).fadeIn();
        $('body').append('<div id="fade"></div>');
        $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn();
        var popuptopmargin = ($('#' + popupid).height() + 10) / 2;
        var popupleftmargin = ($('#' + popupid).width() + 10) / 2;
        $('#' + popupid).css({
            'margin-top': -popuptopmargin,
            'margin-left': -popupleftmargin
        });
        $("#fade").bind('click', function () {
            $("#fade, #" + popupid).fadeOut();
            RemoveGraph();
        });
    }

    function DeleteUserReview(revID) {
        $.ajax({
            type: "POST",
            url: baseURL + 'WebService.asmx/DeleteReview',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ReviewID: revID }),
            dataType: "json",
            success: function (msg) {
            },
            error: function () {

            }
        });
    }
    

          
</script>
<div class="sfFormwrapper">
    <div id="dvPreviewGraph" class="sfPopupwrap" style="display: none;">
        <div class="sfPopup sfFormPreview">
            <h2>
                Chart Graph <a href="#" class="sfClose"><span id="spnClose">Close</span></a></h2>
            <div class="sfPopupcontents">
                <div class="sfPreviewbody sfFormcontentinner">
                    <div id="h2EmptyGraph" class="ncEmptyGraph" style="display: none">
                        <span class="sfLocalee">No Review For This Question.</span></div>
                    <div id="dvGraphs" class="popupbox" style="display: none;">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="dvReview" class="sfPopupwrap" style="display: none;">
        <div class="sfPopup sfFormPreview">
            <h2>
                <span id="spnHeaderquestion"></span><a href="#" class="sfClose"><span id="spnReviewclose">
                    Close</span></a></h2>
            <div class="sfPopupcontents">
                <div class="sfPreviewbody sfFormcontentinner">
                    <ul id="ulFaqReview">
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div id="fade">
    </div>
    <div class="sfTabcontainer">
        <asp:Panel ID="pnlExtensionEditFirst" runat="server">
            <cc1:TabContainer ID="TabSetting" runat="server" ActiveTabIndex="0">
                <cc1:TabPanel ID="TabMainSetting" runat="server">
                    <HeaderTemplate>
                        <asp:Label ID="lblMasterSetting" runat="server" Text="FAQ List"></asp:Label></HeaderTemplate>
                    <ContentTemplate>
                        <div id="dvAddFAQ" runat="server" class="sfButtonwrapper">                         
                            <asp:ImageButton ID="imbAddFaq" runat="server" OnClick="imbAddFaq_Click" />
                            <asp:Label ID="lblAddFaq" CssClass="sfFormlabel" runat="server" AssociatedControlID="imbAddFaq"
                                Text="Add FAQs"></asp:Label>
                        </div>
                        <div runat="server" id="dvForm" class="sfFormwrapper">
                            <table id="tblAddFAQ" runat="server" width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCategory" runat="server" Text="Category" CssClass="sfFormlabel sfLocalee"></asp:Label>
                                    </td>
                                    <td style="width: 30px">
                                        :
                                    </td>
                                    <td>
                                        <div id="dvddlCategory" runat="server">
                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="sfListmenu">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr id="Tr1" runat="server">
                                    <td id="Td1" runat="server">
                                        <asp:Label ID="lblFAQQuestion" runat="server" Text="Question" CssClass="sfFormlabel sfLocalee"></asp:Label>
                                    </td>
                                    <td style="width: 30px">
                                        :
                                    </td>
                                    <td id="Td2" runat="server">
                                        <asp:TextBox ID="txtQuestion" runat="server" CssClass="sfInputbox" Width="500px"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="rfvHeader" CssClass="sfLocalee" runat="server" ErrorMessage="Question is required !!"
                                            ControlToValidate="txtQuestion" ValidationGroup="btnSave"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="Tr2" runat="server">
                                    <td id="Td3" runat="server">
                                        <asp:Label ID="lblFAQAnswer" runat="server" CssClass="sfFormlabel sfLocalee" Text="Answer"></asp:Label>
                                    </td>
                                    <td style="width: 30px">
                                        :
                                    </td>
                                    <td id="Td4" runat="server">
                                        <CKEditor:CKEditorControl ID="CkEditorFAQAnswer" runat="server" Width="900px" Height="200px"
                                            textmode="MultiLine"></CKEditor:CKEditorControl>
                                        <asp:RequiredFieldValidator ID="rfv" runat="server" CssClass="sfLocalee" ErrorMessage="Answer is required !!"
                                            ControlToValidate="CkEditorFAQAnswer" ValidationGroup="btnSave"></asp:RequiredFieldValidator><br />
                                    </td>
                                </tr>
                            </table>
                            <div class="sfButtonwrapper">
                                <asp:ImageButton ID="imgSave" runat="server" ValidationGroup="btnSave" OnClick="imgSave_Click" />
                                <asp:Label ID="lblSave" CssClass="sfFormlabel sfLocalee" runat="server" AssociatedControlID="imgSave"
                                    Text="Save"></asp:Label>
                                <asp:ImageButton ID="imbCancel" runat="server" OnClick="imbCancel_Click" />
                                <asp:Label ID="lblCancel" CssClass="sfFormlabel sfLocalee" runat="server" AssociatedControlID="imbCancel"
                                    Text="Cancel"></asp:Label>
                            </div>
                        </div>
                        <div id="divGrid" runat="server">
                            <div class="sfFaqHeader clear">
                                <div class="sfCategoryList">
                                    <asp:Label ID="lblFilterCategory" runat="server" CssClass="sfFormlabel" Text="Filter By Category :"></asp:Label>
                                    <asp:DropDownList ID="ddlCategoryList" runat="server" CssClass="sfListmenu" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlCategoryList_SelectedIndexChanged">
                                    </asp:DropDownList>
                                 
                                </div>
                                <div class="sfFAQSearch">
                                <asp:Label ID="lblSearchFaq" runat="server" Text="Search :" CssClass="sfFormlabel"></asp:Label>
                                <asp:TextBox ID="txtSearchFAQ"  CssClass="sfInputbox" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="imbSearchFAQ" OnClick="imbSearchFAQ_Click" runat="server" />
                               
                                </div>                                   
                                <div class="sfPageSize">
                                    <asp:Label ID="lblRows" runat="server" Text="Show rows" CssClass="sfFormlabel sfLocalee"></asp:Label>
                                    <asp:DropDownList ID="ddlRecordsPerPage" runat="server" CssClass="sfListmenu sfAuto"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlRecordsPerPage_SelectedIndexChanged"
                                        meta:resourcekey="ddlRecordsPerPageResource1">
                                        <asp:ListItem Value="10" meta:resourcekey="ListItemResource1">10</asp:ListItem>
                                        <asp:ListItem Value="20" meta:resourcekey="ListItemResource2">20</asp:ListItem>
                                        <asp:ListItem Value="30" meta:resourcekey="ListItemResource3">30</asp:ListItem>
                                        <asp:ListItem Value="40" meta:resourcekey="ListItemResource4">40</asp:ListItem>
                                        <asp:ListItem Value="50" meta:resourcekey="ListItemResource5">50</asp:ListItem>
                                        <asp:ListItem Value="100" meta:resourcekey="ListItemResource6">100</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="sfGridwrapper">
                                <asp:GridView Width="100%" runat="server" ID="gdvFAQ" GridLines="None" AutoGenerateColumns="False"
                                    EmptyDataText="..........No Data Found.........." OnRowCommand="gdvFAQ_RowCommand"
                                    OnRowDataBound="gdvFAQ_RowDataBound" OnRowDeleting="gdvFAQ_RowDeleting" OnRowEditing="gdvFAQ_RowEditing"
                                    OnSelectedIndexChanged="gdvFAQ_SelectedIndexChanged" OnSelectedIndexChanging="gdvFAQ_SelectedIndexChanging"
                                    PageSize="10" OnPageIndexChanging="gdvFAQ_PageIndexChanging" AllowPaging="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.N">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            <HeaderStyle VerticalAlign="Top" CssClass="sfEdit" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Title">
                                            <ItemTemplate>
                                                <table style="width: 100%; border: none;">
                                                    <tr>
                                                        <td onclick='flipFlopException(&#039;_<%# Eval("FAQId")%>&#039;)' style="cursor: pointer;
                                                            width: 30%">
                                                            <span style="overflow: hidden;">&nbsp;
                                                                <asp:Label ID="lblPageHead" runat="server" Font-Bold="true" Text='<%# Eval("Question")%>' />
                                                            </span>
                                                        </td>
                                                    </tr>
                                                    <tr id='_<%# (DataBinder.Eval(Container.DataItem,"FAQId").ToString())%>' style="display: none;
                                                        width: 100%">
                                                        <td class="sfFAQDetails" colspan="4">
                                                            <asp:Panel ID="pnlClientIP" runat="server" Width="100%">
                                                                <p>
                                                                    <asp:Label ID="lblAnswer" runat="server" CssClass="sfFormlabel" Text="Answer:"></asp:Label>
                                                                    <asp:Literal ID="ltrAnswer" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"Answer").ToString()) %>'></asp:Literal>
                                                                </p>
                                                                <p>
                                                                    <asp:Label ID="lblAddedBy" runat="server" CssClass="sfFormlabel" Text="Added By:"></asp:Label>
                                                                    <asp:Literal ID="ltrAddedBy" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"AddedBy").ToString()) %>'></asp:Literal>
                                                                </p>
                                                                <p>
                                                                    <asp:Label ID="lblAddedOn" runat="server" CssClass="sfFormlabel" Text="AddedOn:"></asp:Label>
                                                                    <asp:Literal ID="ltrAddedOn" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"AddedOn").ToString()) %>'></asp:Literal>
                                                                </p>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                      
                                        <asp:TemplateField HeaderText="UserView">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgUserReview" runat="server" FAQ='<%# Eval("Question")%>' CausesValidation="False"
                                                    FAQId='<%# Eval("FAQId")%>' CommandArgument='<%#Eval("FAQId")%>' CommandName="Graph"
                                                    ImageUrl='<%# GetTemplateImageUrl("imgpreview.png", true) %>' />
                                            </ItemTemplate>
                                            <HeaderStyle VerticalAlign="Top" CssClass="sfEdit" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Graph">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgViewGraph" CssClass="sfEdit" runat="server" CausesValidation="False"
                                                    FAQId='<%# Eval("FAQId")%>' CommandArgument='<%#Eval("FAQId")%>' CommandName="Graph"
                                                    ImageUrl='<%# GetTemplateImageUrl("menu.png", true) %>' />
                                            </ItemTemplate>
                                            <HeaderStyle VerticalAlign="Top" CssClass="sfEdit" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgEdit" CssClass="sfEdit" runat="server" CausesValidation="False"
                                                    CommandArgument='<%#Eval("FAQId")%>' CommandName="Edit" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>' />
                                            </ItemTemplate>
                                            <HeaderStyle VerticalAlign="Top" CssClass="sfEdit" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgDelete" runat="server" CausesValidation="False" CommandArgument='<%#Eval("FAQId")%>'
                                                    CommandName="Delete" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
                                                    OnClientClick="return ConfirmDialog(this, 'Confirmation', 'Are you sure you want to delete FAQ ?');" />
                                            </ItemTemplate>
                                            <HeaderStyle VerticalAlign="Top" CssClass="sfDelete" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="sfEven" />
                                    <PagerStyle CssClass="sfPagination" />
                                    <RowStyle CssClass="sfOdd" />
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel1" runat="server">
                    <HeaderTemplate>
                        <asp:Label ID="Label1" runat="server" Text="FAQ Category"></asp:Label></HeaderTemplate>
                    <ContentTemplate>
                        <div id="dvUpdateCategory" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCategoryName" CssClass="sfFormlabel" Text="Enter Category" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 30px">
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCategoryName" Width="500px" runat="server" ValidationGroup="btnSaveCategory"
                                            CssClass="sfInputbox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ErrorMessage="Enter Category !"
                                            ControlToValidate="txtCategoryName" ValidationGroup="btnSaveCategory"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <div class="sfButtonwrapper">
                                            <asp:ImageButton ID="imbSaveCategory" runat="server" ValidationGroup="btnSaveCategory"
                                                OnClick="imbSaveCategory_Click" />
                                            <asp:Label ID="lblSavecategory" CssClass="sfFormlabel" runat="server" AssociatedControlID="imbSaveCategory"
                                                Text="Save"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="sfGridwrapper" id="dvCategoryGrid" runat="server">
                            <asp:GridView Width="100%" runat="server" ID="gdvCategory" GridLines="None" AutoGenerateColumns="False"
                                EmptyDataText="..........No Category Found.........." PageSize="10" AllowPaging="True"
                                OnPageIndexChanging="gdvCategory_PageIndexChanging" OnRowCommand="gdvCategory_RowCommand"
                                OnRowDataBound="gdvCategory_RowDataBound" OnRowDeleting="gdvCategory_RowDeleting"
                                OnRowEditing="gdvCategory_RowEditing" OnSelectedIndexChanged="gdvCategory_SelectedIndexChanged"
                                OnSelectedIndexChanging="gdvCategory_SelectedIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.N">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" CssClass="sfEdit" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPageHead" runat="server" Font-Bold="true" Text='<%# Eval("CategoryName")%>' />
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEdit" CssClass="sfEdit" runat="server" CausesValidation="False"
                                                CommandArgument='<%#Eval("CategoryID")%>' CommandName="Edit" ImageUrl='<%# GetTemplateImageUrl("imgedit.png", true) %>' />
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" CssClass="sfEdit" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnCategoryID" Value='<%# Eval("CategoryID") %>' runat="server" />
                                            <asp:ImageButton ID="ImgDelete" runat="server" CausesValidation="False" CommandArgument='<%#Eval("CategoryID")%>'
                                                CommandName="Delete" ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>'
                                                OnClientClick="return ConfirmDialog(this, 'Confirmation', 'Are you sure you want to delete with all category List ?');" />
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" CssClass="sfDelete" />
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle CssClass="sfEven" />
                                <PagerStyle CssClass="sfPagination" />
                                <RowStyle CssClass="sfOdd" />
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </asp:Panel>
    </div>
</div>
