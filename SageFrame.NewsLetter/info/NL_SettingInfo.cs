using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.NewsLetter
{
    /// <summary>
    /// This class holds the properties for NL_SettingInfo.
    /// </summary>
   public class NL_SettingInfo
    {
       /// <summary>
        /// Gets or sets ModuleHeader.
       /// </summary>
       public string ModuleHeader
       {
           get;
           set;
       }
       /// <summary>
       /// Gets or sets ModuleDescription.
       /// </summary>
       public string ModuleDescription
       {
           get;
           set;
       }
       /// <summary>
       /// Gets or sets IsMobileSubscription.
       /// </summary>
       public string IsMobileSubscription
       {
           get;
           set;
       }
       /// <summary>
       /// Gets or sets UnSubscribePageName.
       /// </summary>
       public string UnSubscribePageName
       {
           get;
           set;
       }

    }
}
