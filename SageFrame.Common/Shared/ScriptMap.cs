#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
#endregion

namespace SageFrame.Common.Shared
{
    public static class ScriptMap
    {
        /// <summary>
        /// Core SageFrame Scripts
        /// </summary>
        public static KeyValuePair<string, string> CoreJsonKVP = new KeyValuePair<string, string>("SageJSON", "/js/json2.min.js");

        /// <summary>
        /// BreadCrumb Scripts
        /// </summary>
        public static KeyValuePair<string, string> BreadCrumbScript = new KeyValuePair<string, string>("BreadCrumbScript", "/js/BreadCrumb/BreadCrumb.js");
        /// <summary>
        /// Admin Menu Scripts
        /// </summary>
        public static KeyValuePair<string, string> MenuJQueryHoverIntent = new KeyValuePair<string, string>("JQueryHoverIntent", "/Modules/SageMenu/js/hoverIntent.js");
        public static KeyValuePair<string, string> MenuJQuerySuperFish = new KeyValuePair<string, string>("JQuerySuperFish", "/Modules/SageMenu/js/superfish.js");
        public static KeyValuePair<string, string> MenuSageAdminMenuScript = new KeyValuePair<string, string>("SageAdminMenuScript", "/js/Menu/AdminMenu.js");
        public static KeyValuePair<string, string> AdminMenuScript = new KeyValuePair<string, string>("AdminMenuScript", "/js/Menu/SageAdminMenu.js");
        /// <summary>
        /// Sage Menu Scripts
        /// </summary>
        public static KeyValuePair<string, string> SageMenuHoverIntent = new KeyValuePair<string, string>("JQueryHoverIntent", "js/hoverIntent.js");
        public static KeyValuePair<string, string> SageMenuSuperFish = new KeyValuePair<string, string>("JQuerySuperFish", "js/superfish.js");
        public static KeyValuePair<string, string> MenuSageMenuScript = new KeyValuePair<string, string>("SageMenuScript", "js/SageMenu.min.js");

         
           
        
        
    }
}
