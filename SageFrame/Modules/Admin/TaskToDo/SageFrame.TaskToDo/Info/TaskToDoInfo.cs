using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.TaskToDo
{
    public class TaskToDoInfo
    {
        public int TaskID { get; set; }
        public string Note{ get; set; }
        public DateTime Date { get; set; }
        public string Released { get { return Date.ToString("dd-MMM-yy"); } set { Released = value; } }
        public int Total{get;set;}
        public string FullDate { get { return Date.ToString("dd-MM-yyyy"); } set {   FullDate = value; } }
    }
}
