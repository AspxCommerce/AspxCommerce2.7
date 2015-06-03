using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class CommonParmBuilder
    {
        public CommonParmBuilder()
        {
        }

        /// <summary>
        /// To Get Parameter Having @StoreID,@PortalID
        /// </summary>
        /// <param name="aspxCommonObj"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, object>> GetParamSP(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            return ParaMeter;
        }
        /// <summary>
        /// To Get Parameter Having @StoreID,@PortalID,@UserName
        /// </summary>
        /// <param name="aspxCommonObj"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, object>> GetParamSPU(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            return ParaMeter;
        }

        /// <summary>
        /// To Get Parameter Having @StoreID,@PortalID,@CultureName
        /// </summary>
        /// <param name="aspxCommonObj"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, object>> GetParamSPC(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            return ParaMeter;
        }

        /// <summary>
        /// To Get Parameter Having @StoreID,@PortalID,@CustomerID,@SessionCode,@UserName,@CultureName
        /// </summary>
        /// <param name="aspxCommonObj"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, object>> GetFullParam(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", aspxCommonObj.SessionCode));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            return ParaMeter;
        }

        /// <summary>
        /// To Get Parameter Having @StoreID,@PortalID,@SessionCode,@UserName,@CultureName
        /// </summary>
        /// <param name="aspxCommonObj"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, object>> GetParamNoCID(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", aspxCommonObj.SessionCode));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            return ParaMeter;
        }

        /// <summary>
        /// To Get Parameter Having @StoreID,@PortalID,@UserName,@CultureName
        /// </summary>
        /// <param name="aspxCommonObj"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, object>> GetParamSPUC(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            return ParaMeter;
        }
        /// <summary>
        /// To Get Parameter Having @CustomerID,@SessionCode,@StoreID,@PortalID
        /// </summary>
        /// <param name="aspxCommonObj"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, object>> GetParamSPSCt(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", aspxCommonObj.SessionCode));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            return ParaMeter;
        }

        /// <summary>
        /// To Get Parameter Having @StoreID,@PortalID,@CustomerID
        /// </summary>
        /// <param name="aspxCommonObj"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, object>> GetParamSPCt(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            return ParaMeter;
        }

        /// <summary>
        /// To Get Parameter Having @PortalID,@UserName,@CultureName
        /// </summary>
        /// <param name="aspxCommonObj"></param>
        /// <returns></returns>

        public static List<KeyValuePair<string, object>> GetParamPUC(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            return ParaMeter;
        }
        /// <summary>
        /// To Get Parameter Having @StoreID,@PortalID,@UserName,@SessionCode
        /// </summary>
        /// <param name="aspxCommonObj"></param>
        /// <returns></returns>

        public static List<KeyValuePair<string, object>> GetParamSPUS(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", aspxCommonObj.SessionCode));
            return ParaMeter;
        }
        /// <summary>
        /// To Get Parameter Having @StoreID,@PortalID,@UserName,@CustomerID
        /// </summary>
        /// <param name="aspxCommonObj"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, object>> GetParamSPUCt(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
            return ParaMeter;
        }
        /// <summary>
        /// To Get Parameter Having @StoreID,@PortalID,@UserName,@CultureName,@CustomerID
        /// </summary>
        /// <param name="aspxCommonObj"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, object>> GetFParamNoSCode(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@UserName", aspxCommonObj.UserName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
            return ParaMeter;
        }

         /// <summary>
        /// To Get Parameter Having @StoreID,@PortalID,@CustomerID,@CultureName
         /// </summary>
         /// <param name="aspxCommonObj"></param>
         /// <returns></returns>

        public static List<KeyValuePair<string, object>> GetParamSPCtC(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            return ParaMeter;
        }
        /// <summary>
        /// To Get Parameter Having @StoreID,@PortalID,@CustomerID,@SessionCode,@CultureName
        /// </summary>
        /// <param name="aspxCommonObj"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, object>> GetFParamNoU(AspxCommonInfo aspxCommonObj)
        {
            List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
            ParaMeter.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
            ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
            ParaMeter.Add(new KeyValuePair<string, object>("@CustomerID", aspxCommonObj.CustomerID));
            ParaMeter.Add(new KeyValuePair<string, object>("@SessionCode", aspxCommonObj.SessionCode));
            ParaMeter.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
            return ParaMeter;
        }

    }
}
