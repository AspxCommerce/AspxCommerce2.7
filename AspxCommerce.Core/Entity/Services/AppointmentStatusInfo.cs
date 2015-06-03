using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
   public class AppointmentStatusInfo
    {
       public AppointmentStatusInfo()
       {
       }

       public int AppointmentStatusID { get; set; }
       public string AppointmentStatusName { get; set; }
       public string AliasToolTip { get; set; }
    }
    public class AppointmentSatusInfoBasic
    {
        public int RowTotal { get; set; }
        public int AppointmentStatusID { get; set; }
        public string AppointmentStatusAliasName { get; set; }
        public string AliasToolTip { get; set; }
        public bool? IsActive { get; set; }
        public bool IsSystemUsed { get; set; }
      
    }
}
