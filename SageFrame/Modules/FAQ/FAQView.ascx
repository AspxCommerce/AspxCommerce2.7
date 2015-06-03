<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FAQView.ascx.cs" Inherits="Modules_FAQ_FAQView" %>
<script type="text/javascript">

    $(function () {
        $(this).SageFAQList({
            PortalID: '<%=PortalID %>',
            baseURL: '<%=baseURL %>',
            UserModuleID: '<%=UserModuleID %>',
            CultureName: '<%=CultureName%>'
        });
    });
</script>
<div class="sfFormwrapper">
    <div class="sfEmptyFAQ" style="display: none">
        <p class="sfLocalee">
            No question has been posted.</p>
    </div>
   
    <div class="dvFAQwrapper">
    <div class="uvField uvField-idea">
            <label for="suggestion_title" class="uvFieldInner">              
                    <span><input type="text" class="uvFieldText" id="txtSearchFaq" /></span>
                        <span><input type="button" class="sfBtn" id="btnSearchFAQ" value="Search" /></span>
            </label>
        </div>     
         <div class="sfEmptyresult" style="display:none;"><p>No Result has been found.</p></div>  
        <div id="divFAQ" class="Containerlist">
            <ul id="ulFAQList" class="content">
            </ul>
            <div id="Pagination">
        </div>
        </div>
        <%--<div id="AddQuestion" style="display: none">
            <input type="button" id="btnAddQuestion" class="sfBtn sfLocalee" value="Ask Question" />
        </div>--%>
        <%--<div id="fade1">
        </div>--%>
    </div>
</div>
