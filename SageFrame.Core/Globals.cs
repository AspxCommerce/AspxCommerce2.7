using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SageFrame.Core
{
    /// <summary>
    /// Class that hold the global variablesof the application.
    /// </summary>
    public class Globals : System.Web.HttpApplication
    {        
        /// <summary>
        /// Global hash table.
        /// </summary>
        public static Hashtable sysHst = new Hashtable();
    }
}