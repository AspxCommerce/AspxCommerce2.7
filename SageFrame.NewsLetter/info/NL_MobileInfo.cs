using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.NewsLetter
{
    /// <summary>
    /// This class holds the properties for NL_MobileInfo.
    /// </summary>
   public class NL_MobileInfo
    {
       /// <summary>
        /// Gets or sets MobSubscriberID
       /// </summary>
       public int MobSubscriberID
       {
           get;
           set;
       }
       /// <summary>
       /// Gets or sets MobileNumber.
       /// </summary>
       public string MobileNumber
       {
           get;
           set;
       }
       /// <summary>
       /// Gets or sets IsSubscribed.
       /// </summary>
       public bool IsSubscribed
       {
           get;
           set;
       }
       /// <summary>
       /// Gets or sets ClientIP.
       /// </summary>
       public string ClientIP
       {
           get;
           set;
       }
       /// <summary>
       /// Gets or sets UserModuleID.
       /// </summary>
       public int UserModuleID
       {
           get;
           set;
       }
       /// <summary>
       /// Gets or sets PortalID.
       /// </summary>
       public int PortalID
       {
           get;
           set;
       }
       /// <summary>
       /// Gets or sets added time.
       /// </summary>
       public DateTime AddedOn
       {
           get;
           set;
       }
       /// <summary>
       /// Gets or sets AddedBy.
       /// </summary>
       public string AddedBy
       {
           get;
           set;
       }
    }
}
