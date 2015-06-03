<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ScheduleView.ascx.cs"
    Inherits="Modules_Scheduler_ScheduleView" %>
<div id="backgroundFilter">
</div>
<div id="wrapper">
    <div id="schedulerHeader">
        <h1>
            <span>Job Scheduler</span></h1>
    </div>
    <div class="cssClassGridWrapper" id="divShowItemRatingDetails">
        <div class="sfButtonwrapper">      
                <%--<img src="<%=ImagePath%>images/add.png" onclick="ShowNewTaskPopUp(true)" style="float: left" />--%>
                <label class="icon-addnew sfBtn" onclick="ShowNewTaskPopUp(true)">
                    Add New Task</label>          
        </div>
    </div>
    <div class="sfGridwrapper">
        <div class="cssClassGridWrapperContent">
          <%--  <div id="loading">
                <img src="<%=ImagePath%>images/ajax-loader.gif" />
            </div>--%>
            <div id="log">
            </div>
            <table id="gdvSchedularListAll" width="100%" border="0" cellpadding="0" cellspacing="0">
            </table>
        </div>
    </div>
</div>
<div id="bottomControlDiv" class="sfButtonwrapper">
</div>
</div>
<div id="popDiv" class="invisibleDiv">
    <h2 align="center">
        This is another div</h2>
    <div style="text-align: center">
        <input type="text" name="tb" />
        <input type="button" name="btn" value="Click" />
        <br />
        <a style="color: #fff; font-weight: bold" href="#" class="close" id="closeLink">Close
            Me</a>
    </div>
</div>
<div id="newScheduleDiv" class="invisibleDiv sfFormwrapper">
    <div class="cssClassPopUpBoxInfo cssClassPadding10 cssClassCurve">
        <span class="closePopUp">
            <input type="image" src="<%=ImagePath%>images/closelabel.png" onclick="javascript:return false" />
        </span>
        <h2>
            <span class="headerSpan">ADD NEW TASK</span></h2>
        <input type="hidden" id="sid" />
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="30%">
                    <label>
                        Task Name:</label>
                </td>
                <td>
                    <input type="text" id="txtTaskName" class="cssClassNormalTextBox required" style="width: 170px;" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Assembly Name:</label>
                </td>
                <td>
                    <input type="text" id="txtAssemblyName" name="txtAssemblyName" class="cssClassNormalTextBox required txtAssemblyName"
                        style="width: 350px;" />
                    <br />
                    [Namespace.ClassName, Namespace]
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Enabled:</label>
                </td>
                <td>
                    <input type="checkbox" id="chkIsEnabled" checked="checked" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlRunningMode" runat="server" GroupingText="Running Mode">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="22%">
                                    <label>
                                        Start Date:</label>
                                </td>
                                <td>
                                    <input id="txtStartDate" type="text" class="cssClassNormalTextBox required" style="width: 60px;" />
                                    &nbsp; &nbsp; &nbsp;
                                    <label>
                                        End Date:</label>
                                    <input id="txtEndDate" type="text" name="endDates" class="cssClassNormalTextBox required ValidDates"
                                        style="width: 60px;" />
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Start Time:</label>
                                </td>
                                <td>
                                    <select id="txtHours" class="required sfShortSelectbox">
                                    </select>
                                    &nbsp; : &nbsp;
                                    <select id="txtMins" class="required sfShortSelectbox">
                                    </select>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="30%">
                                    <label>
                                        Select Repeat Mode:</label>
                                </td>
                                <td>
                                    <select id="running_mode">
                                        <option value="">Select</option>
                                        <option value="0">Hourly</option>
                                        <option value="1">Daily</option>
                                        <option value="4">Calendar</option>
                                        <option value="5">Once</option>
                                    </select>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" id="running_mode_selection">
                                    <div class="cssClassOnClickShow">
                                        <span id="s0" style="display: none">
                                            <label>
                                                Run the schedule every</label>
                                            <select id="txtRepeatHrs" class="sfShortSelectbox">
                                            </select>
                                            <select id="txtRepeatMins" class="sfShortSelectbox">
                                            </select>
                                        </span><span id="s1" style="display: none">
                                            <label>
                                                Repeat After :
                                            </label>
                                            <input type="text" id="RepeatDays" class="cssClassNormalTextBox" style="width: 20px;"
                                                maxlength="3" />
                                            day(s) </span>
                                        <div id="s2" style="display: none">
                                            <label>
                                                On following days</label>
                                            <ul>
                                                <li>
                                                    <input type="checkbox" name="chkday" value="1" />
                                                    <label>
                                                        Sunday</label>
                                                    <input type="checkbox" name="chkday" value="2" />
                                                    <label>
                                                        Monday</label>
                                                    <input type="checkbox" name="chkday" value="3" />
                                                    <label>
                                                        Tuesday</label>
                                                    <input type="checkbox" name="chkday" value="4" />
                                                    <label>
                                                        Wenesday</label>
                                                    <input type="checkbox" name="chkday" value="5" />
                                                    <label>
                                                        Thursday</label>
                                                    <input type="checkbox" name="chkday" value="6" />
                                                    <label>
                                                        Friday</label>
                                                    <input type="checkbox" name="chkday" value="7" />
                                                    <label>
                                                        Saturday</label>
                                                </li>
                                            </ul>
                                            <div class="cssClassclear">
                                            </div>
                                        </div>
                                        <div id="s4">
                                            <div class="cssClassclear">
                                            </div>
                                        </div>
                                        <div id="s3" style="display: none">
                                            <select id="Weekly">
                                            </select>
                                            <div class="cssClassclear">
                                            </div>
                                        </div>
                                        <div id="s5" style="display: none">
                                            <textarea cols="30" rows="5" id="popupDatepicker" class="cssClassNormalTextBox"></textarea>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div id="timeFrameDiv">
                                        <label>
                                            From:</label>
                                        <input type="text" id="txtFromDate" />
                                        <label>
                                            To:</label>
                                        <input type="text" id="txtToDate" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Retry Frequency:</label>
                </td>
                <td>
                    <input type="text" id="txtRetryFreq" class="cssClassNormalTextBox" style="width: 20px;"
                        maxlength="2" />
                    <select id="ddlRetryUnits">
                        <option value="1">Sec</option>
                        <option value="2">Min</option>
                        <option value="3">Hrs</option>
                        <option value="4">Day</option>
                        <option value="5">Week</option>
                        <option value="6">Month</option>
                    </select>
                </td>
                <td>
                </td>
            </tr>
            <tr id="trDllUpload" style="display: ">
                <td>
                    <label>
                        Upload Assembly:</label>
                </td>
                <td>
                    <input type="file" id="fileUpload" name="fileUpload" class="fileClass" />
                    <br />
                    <span class="fileUploadMsg"></span>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                <label id="lblFile"></label>
                    <input type="hidden" id="uploadFileName" />
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Run on Event:</label>
                </td>
                <td>
                    <select id="ddlEventList">
                        <option value="1">None</option>
                        <option value="2">APPLICATION_START</option>
                    </select>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Catch Up Enabled:</label>
                </td>
                <td>
                    <input type="checkbox" id="chkIsCatchUpEnabled" />
                </td>
            </tr>
        </table>
        <div class="sfButtonwrapper">
            <span class="btnAddTaskspan">
                <input type="submit" id="btnAddTask" value="Add Task" class="sfBtn" />
            </span><span class="btnupdateTaskspan" style="display: none">
                <input type="submit" id="btnUpdateTask" value="Update Task" class="sfBtn" />
            </span>
        </div>
    </div>
</div>
<%--  Schedular History Section--%>
<!-- main container-->
<div id="scheduleHistoryList" style="display: none">
    <!-- show all schedules -->
    <div>
        <ul>
            <li><strong>Schedule Name:</strong><span class="spanScheduleName"></span></li>
            <li><strong>Full Class Name:</strong> <span class="spanScheduleNamespace"></span>
            </li>
            <li><strong>Start Date:</strong> <span class="spanScheduleStartDate"></span></li>
            <li><strong>End Date:</strong> <span class="spanScheduleEndDate"></span></li>
            <li><strong>Running Mode:</strong> <span class="spanScheduleRunningMode"></span>
            </li>
        </ul>
        <div class="sfButtonwrapper clearfix">
            <span class="btnRunNow">
                <input type="button" class="btnRunSchedule sfBtn" onclick="javascript:return false"
                    value="Run Now" /></span></div>
    </div>
    <div class="sfGridwrapper cssClassmarginTop10">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" id="gdvSchedularHistoryList">
        </table>
    </div>
</div>
