#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

#endregion

namespace SageFrame.Core.TemplateManagement
{
    /// <summary>
    /// Setting class  for template.
    /// </summary>
    public static class TemplateSettings
    {
        /// <summary>
        /// Returns base director path for template.
        /// </summary>
        public static string BaseDir
        {
            get {
                string root = HttpContext.Current.Request.ApplicationPath.ToString();
                root = root.Substring(1, root.Length - 1);
                return root;
            }
            
        }

        /// <summary>
        /// Retuns template paths.
        /// </summary>
        public static string TemplatePath = "\\"+BaseDir + "\\Templates\\";
    }
}
