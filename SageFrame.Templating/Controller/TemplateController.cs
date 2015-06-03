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

namespace SageFrame.Templating
{
    /// <summary>
    /// Business logic for TemplateController.
    /// </summary>
    public class TemplateController
    {
        /// <summary>
        /// Activate template during installation.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <param name="PortalID">PortalID</param>
        public static void ActivateTemplate(string TemplateName, int PortalID)
        {
            try
            {
                TemplateDataProvider.ActivateTemplate(TemplateName, PortalID);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        ///Obtain active template.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Object of TemplateInfo class.</returns>
        public static TemplateInfo GetActiveTemplate(int PortalID)
        {
            try
            {
                return (TemplateDataProvider.GetActiveTemplate(PortalID));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Update active template.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <param name="conn">Connection string.</param>
        public static void UpdActivateTemplate(string TemplateName, string conn)
        {
            try
            {
                TemplateDataProvider.UpdActivateTemplate(TemplateName, conn);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Obtain application settings.
        /// </summary>
        /// <param name="objSetting">Object of SettingInfo class.</param>
        /// <returns>Object of SettingInfo class.</returns>
        public static SettingInfo GetSettingByKey(SettingInfo objSetting)
        {
            try
            {
                return (TemplateDataProvider.GetSettingByKey(objSetting));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtain portal templates.
        /// </summary>
        /// <returns>List of TemplateInfo class.</returns>
        public static List<TemplateInfo> GetPortalTemplates()
        {
            return TemplateDataProvider.GetPortalTemplates();
        }
    }
}
