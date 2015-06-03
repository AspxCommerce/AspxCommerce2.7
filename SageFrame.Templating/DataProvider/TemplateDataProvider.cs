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
using SageFrame.Web.Utilities;
using System.Data;
using System.Data.SqlClient;
using SageFrame.Web;
#endregion


namespace SageFrame.Templating
{
    /// <summary>
    /// Manupulates data for TemplateDataProvider.
    /// </summary>
    public class TemplateDataProvider
    {
        /// <summary>
        /// Connect to database and activate template during installation.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <param name="PortalID">PortalID</param>
        public static void ActivateTemplate(string TemplateName, int PortalID)
        {
             
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@TemplateName", TemplateName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsActive", true));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
          
            SQLHandler sagesql = new SQLHandler();
            sagesql.ExecuteNonQuery("usp_sftemplate_activate", ParaMeterCollection);

       
        }
        /// <summary>
        ///Connect to database and obtain active template.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Object of TemplateInfo class.</returns>
        public static TemplateInfo GetActiveTemplate(int PortalID)
        {   
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            return (sagesql.ExecuteAsObject<TemplateInfo>("usp_sftemplate_GetActiveTemplate",ParaMeterCollection));
        }
        /// <summary>
        /// Connect to database and obtain portal templates.
        /// </summary>
        /// <returns>List of TemplateInfo class.</returns>
        public static List<TemplateInfo> GetPortalTemplates()
        {
            SQLHandler sagesql = new SQLHandler();
            return (sagesql.ExecuteAsList<TemplateInfo>("usp_TemplateGetPortalTemplate"));
        }

        /// <summary>
        /// Connect to database and update active template.
        /// </summary>
        /// <param name="TemplateName">Template name.</param>
        /// <param name="conn">Connection string.</param>
        public static void UpdActivateTemplate(string TemplateName, string conn)
        {

            SqlConnection sqlcon = new SqlConnection(conn);
            sqlcon.Open();
            SqlCommand sqlcmd = new SqlCommand("usp_sftemplate_updactive", sqlcon);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@TemplateName", TemplateName);
            sqlcmd.ExecuteNonQuery();
            sqlcon.Close();
        }
        /// <summary>
        /// Connect to database and obtain application settings.
        /// </summary>
        /// <param name="objSetting">Object of SettingInfo class.</param>
        /// <returns>Object of SettingInfo class.</returns>
        public static SettingInfo GetSettingByKey(SettingInfo objSetting)
        {
            string sp = "[dbo].[usp_DashboardGetSettingByKey]";
            SQLHandler sagesql = new SQLHandler();

            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@SettingKey", objSetting.SettingKey));
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", objSetting.UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", objSetting.PortalID));
            try
            {
                return (sagesql.ExecuteAsObject<SettingInfo>(sp, ParamCollInput));

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
