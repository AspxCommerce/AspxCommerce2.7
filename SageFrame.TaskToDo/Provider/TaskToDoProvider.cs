using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.TaskToDo;
using SageFrame.Web;
using SageFrame.Web.Utilities;


namespace SageFrame.TaskToDo
{
  public class TaskToDoProvider
    {
     
      public List<TaskToDoInfo> GetTask(int PortalID, int UserModuleId, string CultureField, int offset, string str, string UserName, string SearchDate)
      {
          try
          {
              List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
              Param.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
              Param.Add(new KeyValuePair<string, object>("@ModuleId", UserModuleId));
              Param.Add(new KeyValuePair<string, object>("@CultureField", CultureField));
              Param.Add(new KeyValuePair<string, object>("@offset", offset));
              Param.Add(new KeyValuePair<string, object>("@STR", str));
              Param.Add(new KeyValuePair<string, object>("@Date", SearchDate));
              Param.Add(new KeyValuePair<string, object>("@UserName", UserName));
              SQLHandler handler = new SQLHandler();
              return handler.ExecuteAsList<TaskToDoInfo>("[dbo].[usp_TaskToDo_GetTask]", Param);
          }
          catch (Exception e)
          {
              throw e;
          }
      }
        public List<TaskToDoInfo> GetTaskContent(int Id,int PortalID, int UserModuleId, string CultureCode)
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                Param.Add(new KeyValuePair<string, object>("@UserModuleId", UserModuleId));
                Param.Add(new KeyValuePair<string, object>("@CultureCode", CultureCode));
                Param.Add(new KeyValuePair<string, object>("@Id", Id));
                SQLHandler handler = new SQLHandler();
                return handler.ExecuteAsList<TaskToDoInfo>("[dbo].[usp_TaskToDo_GetTaskContent]", Param);

            }
            catch (Exception e)
            {
                throw e;
            }
        }     
        public void SaveTask(string Note,DateTime Date, string CultureField, int PortalID, int ModuleId, string UserName, int Id)
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@Note", Note));
                Param.Add(new KeyValuePair<string, object>("@Date", Date));
                Param.Add(new KeyValuePair<string, object>("@CultureField", CultureField));
                Param.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                Param.Add(new KeyValuePair<string, object>("@ModuleId", ModuleId));
                Param.Add(new KeyValuePair<string, object>("@UserName", UserName));
                Param.Add(new KeyValuePair<string, object>("@Id", Id));
                SQLHandler handler = new SQLHandler();
                handler.ExecuteNonQuery("[dbo].[usp_TaskToDo_InsertTask]", Param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void DeleteTask(int id, string UserName, int PortalID, int UserModuleId, string CultureCode)
        {
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                Param.Add(new KeyValuePair<string, object>("@UserModuleId", UserModuleId));
                Param.Add(new KeyValuePair<string, object>("@CultureCode", CultureCode));
                Param.Add(new KeyValuePair<string, object>("@Id", id));
                Param.Add(new KeyValuePair<string, object>("@UserName", UserName));
                SQLHandler handler = new SQLHandler();
                handler.ExecuteNonQuery("[dbo].[usp_TaskToDo_DeleteTask]", Param);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
