using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspxCommerce.Core;
using AspxCommerce.USPS.Provider;
using AspxCommerce.USPS;
using AspxCommerce.USPS.Entity;
using RegisterModule;
using SageFrame.Web.Utilities;
using System.Web;
using System.Threading;

namespace AspxCommerce.USPS.Controller
{
   public class UspsController
    {
       public UspsController()
       {

       }

       public UspsSetting GetSetting(int providerId, AspxCommonInfo commonInfo)
       {
           try
           {
               return USPSProvider.GetSetting(providerId, commonInfo);
           }
           catch (Exception ex )
           {
                
               throw ex;
           }
          
       }

    }
}
