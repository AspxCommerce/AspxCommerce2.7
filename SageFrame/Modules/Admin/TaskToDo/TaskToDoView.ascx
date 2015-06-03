<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaskToDoView.ascx.cs"
    Inherits="Modules_TaskToDo_TaskToDoView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">
    $(function() {
        $(this).TaskEdit
        ({
            BaseUrl: '<%=BaseUrl%>',
            UserModuleId: '<%=UserModuleId%>'
        });
        $(".sfLocale").Localize({ moduleKey: TaskToDoLanguage })
        $('#tabTaskList').tabs({ active: 1 });

    });

</script>

<div class="sfTaskToDoWrapper">
    <h2>
        To-Do List</h2>
    <label class="sfLocale icon-addnew sfBtn" id="AddTaskTab" value="Add">
        Add</label>
    <div id="dvSearch" class="sfSearch">
        <input type="text" id="txtSearchDate" style="width: 20px" readonly="readonly" />
        <span class="icon-search" id="btnSearchTab"></span>
    </div>
</div>
<div id="tabTaskList">
    <ul>
        <li><a id="liPrevious" href="#tabsPrevious">Previous</a></li>
        <li><a id="liToday" href="#tabsToday">Today</a></li>
        <li><a id="liTommorow" href="#tabsUpcomming">Upcoming</a></li>
        <li><a id="liAll" href="#tabsAll">All</a></li>
    </ul>
    <div id="tabsToday">
    </div>
    <div id="tabsAll">
    </div>
    <div id="tabsPrevious">
    </div>
    <div id="tabsUpcomming">
    </div>
    <div class="divscroll" id="dvScroll">
        <div id="dvTask">
        </div>
    </div>
</div>
<div class="addNew" id="dvaddNew">
<h4>Add Task</h4>
    <div>
        <label class="sfLocale sfFormlabel">
            Task</label>
        <textarea id="txtAddNote" name="txtAddNote" class="sfInputbox" rows="20" cols="5"></textarea></div>
    <div>
        <label class="sfLocale sfFormlabel">
            Date</label>
        <input type="text" name="txtDate" id="txtDate" class="sfDatepicker" /></div>
    <div class="sfButtonwrapper">
        <label id="btnAddNewTask" class="sfLocale icon-addnew sfBtn">
            Add</label>
        <label id="btnTaskCancel" class="sfLocale icon-close sfBtn">
            Cancel</label></div>
</div>
<div class="divscroll" id="divSearch" style="display: none;">
    <label class="sfLocale icon-arrow-slim-w sfBtn" id="BtnTaskBack" value="back">
        Back</label>
    <div id="dvSearchTaskList">
    </div>
</div>

