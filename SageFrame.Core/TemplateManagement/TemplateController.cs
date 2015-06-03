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

#endregion


namespace SageFrame.Core.TemplateManagement
{
    /// <summary>
    /// Business logic for templates.
    /// </summary>
    public class TemplateController
    {
        /// <summary>
        /// Returns list of template list.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="UserName">User's name.</param>
        /// <returns>List of template.</returns>
        public static List<TemplateInfo> GetTemplateList(int PortalID, string UserName)
        {
            try
            {
                return (TemplateDataProvider.GetTemplateList(PortalID, UserName));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Adds template.
        /// </summary>
        /// <param name="obj">TemplateInfo object containing the template details.</param>
        /// <returns>True if the template is added successfully.</returns>
        public static bool AddTemplate(TemplateInfo obj)
        {
            try
            {
                return (TemplateDataProvider.AddTemplate(obj));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
