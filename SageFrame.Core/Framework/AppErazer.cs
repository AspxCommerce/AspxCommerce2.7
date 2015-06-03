using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web;
using SageFrame.Common;

namespace SageFrame.Core
{
    /// <summary>
    /// Provides the method that helps to clear the cache
    /// </summary>
    public partial class AppErazer : System.Web.UI.Page
    {
        /// <summary>
        /// Clears hash cache for provided string
        /// </summary>
        /// <param name="key">cache name</param>
        public static void ClearSysHash(string key)
        {
            Hashtable sysHst = Globals.sysHst;
            if (sysHst[key] != null)
            {
                sysHst.Remove(key);
            }
        }

        /// <summary>
        /// Clears all the cache value  with in the system
        /// </summary>
        public static void ClearSysCache()
        {            
            foreach (System.Collections.DictionaryEntry item in HttpRuntime.Cache)
            {
                if (!item.Key.ToString().Contains("."))
                {
                    string key = item.Key.ToString();
                    HttpRuntime.Cache.Remove(key);
                }
            }           
        }
    }
}
