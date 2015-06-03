<%@ WebService Language="C#" Class="TaskToDoWebService" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SageFrame.TaskToDo;
using SageFrame.Services;
/// <summary>
/// Summary description for AccordanceWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class TaskToDoWebService : AuthenticateService
{
    [WebMethod]
    public List<TaskToDoInfo> GetTask(string CultureField, int PortalID, int UserModuleId, int offset, string UserName, string str, string SearchDate, string secureToken)
    {
        if (IsPostAuthenticatedView(PortalID, UserModuleId, UserName, secureToken))
        {
            TaskToDoController controller = new TaskToDoController();
            List<TaskToDoInfo> list = controller.GetTask(CultureField, PortalID, UserModuleId, offset, str, UserName, SearchDate);
            return list;
        }
        else
        {
            return null;
        }
    }

    [WebMethod]
    public List<TaskToDoInfo> GetTaskContent(int Id, int PortalID, int UserModuleId, string CultureCode, string UserName, string secureToken)
    {
        if (IsPostAuthenticatedView(PortalID, UserModuleId, UserName, secureToken))
        {
            TaskToDoController controller = new TaskToDoController();
            List<TaskToDoInfo> list = controller.GetTaskContent(Id, PortalID, UserModuleId, CultureCode);
            return list;
        }
        else
        {
            return null;

        }
    }
    [WebMethod]
    public void SaveTask(string Note, DateTime date, string CultureField, int PortalID, int UserModuleId, string UserName, int Id, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleId, UserName, secureToken))
        {
            TaskToDoController controller = new TaskToDoController();
            string note = HttpUtility.HtmlEncode(Note);
            controller.SaveTask(note, date, CultureField, PortalID, UserModuleId, UserName, Id);
        }
    }
    [WebMethod]
    public void DeleteTask(int Id, string UserName, int PortalID, int UserModuleId, string CultureCode, string secureToken)
    {
        if (IsPostAuthenticated(PortalID, UserModuleId, UserName, secureToken))
        {
            TaskToDoController controller = new TaskToDoController();
            controller.DeleteTask(Id, UserName, PortalID, UserModuleId, CultureCode);
        }
    }
}