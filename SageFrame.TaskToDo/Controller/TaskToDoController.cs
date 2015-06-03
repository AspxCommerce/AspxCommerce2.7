using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.TaskToDo
{
   public class TaskToDoController
    {
     
       public List<TaskToDoInfo> GetTask(string CultureField, int PortalID, int UserModuleId, int offset, string str, string UserName, string SearchDate)
       {
           TaskToDoProvider provider = new TaskToDoProvider();
           return provider.GetTask(PortalID, UserModuleId, CultureField, offset, str, UserName, SearchDate);
       }

       public List<TaskToDoInfo> GetTaskContent(int id, int PortalID, int UserModuleId, string CultureCode)
       {
           TaskToDoProvider provider = new TaskToDoProvider();
           return provider.GetTaskContent(id, PortalID, UserModuleId,  CultureCode);
       }
      
        public void SaveTask(string Note, DateTime Date, string CultureField, int PortalID, int UserModuleId, string UserName, int Id)
        {
            TaskToDoProvider provider = new TaskToDoProvider();
            provider.SaveTask(Note, Date, CultureField, PortalID, UserModuleId, UserName, Id);
        }
        public void DeleteTask(int id, string UserName,int PortalID, int UserModuleId, string CultureCode)
        {
            TaskToDoProvider provider = new TaskToDoProvider();
            provider.DeleteTask(id, UserName, PortalID, UserModuleId,  CultureCode);
        }
    }
}
