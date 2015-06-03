using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.NewsLetter
{   
    /// <summary>
    /// This class holds the properties for NL_Info.
    /// </summary>
   public  class NL_Info
    {
       /// <summary>
        /// Gets or sets SubscriberID.
       /// </summary>
       public int SubscriberID
       {
           get;
           set;
       }
       /// <summary>
       /// Gets or sets SubscriberEmail.
       /// </summary>
       public string SubscriberEmail
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
       /// Gets or sets EmailCount.
       /// </summary>
       public int EmailCount
       {
           get;
           set;

       }

    }
}
