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
using System.Data.SqlClient;

#endregion


namespace SageFrame.PortalSetting
{
    /// <summary>
    /// Manipulates data for Portal settings
    /// </summary>
    public class PortalProvider
    {
        /// <summary>
        /// Connects to database and returns list of portals details
        /// </summary>
        /// <returns>list of portals details</returns>
        public static List<PortalInfo> GetPortalList()
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                return SQLH.ExecuteAsList<PortalInfo>("[dbo].[sp_PortalGetList]");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and returns portal details by portal ID.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="UserName"> User's name.</param>
        /// <returns>Returns portal </returns>
        public static PortalInfo GetPortalByPortalID(int PortalID, string UserName)
        {
            string sp = "[dbo].[sp_PortalGetByPortalID]";
            SqlDataReader reader = null;
            try
            {

                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));


                reader = SQLH.ExecuteAsDataReader(sp, ParamCollInput);
                PortalInfo objList = new PortalInfo();

                while (reader.Read())
                {

                    objList.PortalID = int.Parse(reader["PortalID"].ToString());
                    objList.Name = reader["Name"].ToString();
                    objList.SEOName = reader["SEOName"].ToString();
                    objList.IsParent = bool.Parse(reader["IsParent"].ToString());
                    objList.ParentPortalName = reader["ParentPortalName"].ToString();
                }
                return objList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

        }

        /// <summary>
        /// Connects to database and deletes portal. 
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="UserName">User's name.</param>
        public static void DeletePortal(int PortalID, string UserName)
        {
            string sp = "[dbo].[sp_PortalDelete]";
            SQLHandler SQLH = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));

                SQLH.ExecuteNonQuery(sp, ParamCollInput);
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and updates portal details.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="PortalName">Portal name.</param>
        /// <param name="IsParent">Set true if the portal is parent portal.</param>
        /// <param name="UserName">User's name.</param>
        /// <param name="PortalURL">Portal URL.</param>
        /// <param name="ParentID">Portal's parents ID.</param>
        public static void UpdatePortal(int PortalID, string PortalName, bool IsParent, string UserName, string PortalURL, int ParentID)
        {
            string sp = "[dbo].[sp_PortalUpdate]";
            SQLHandler SQLH = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalName", PortalName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsParent", IsParent));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalURL", PortalURL));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ParentID", ParentID));
                SQLH.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and returns portal modules list by portalID and username.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="UserName">User's name.</param>
        /// <returns>lists of portal modules.</returns>
        public static List<PortalInfo> GetPortalModulesByPortalID(int PortalID, string UserName)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", UserName));

                return SQLH.ExecuteAsList<PortalInfo>("[dbo].[sp_PortalModulesGetByPortalID]", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Connects to database and updates portal modules.
        /// </summary>
        /// <param name="ModuleIDs">Module IDs join by ','</param>
        /// <param name="IsActives">Set actives join by ',' if the respective modules are active.</param>
        /// <param name="PortalID">Portal ID</param>
        /// <param name="UpdatedBy">Modules updated user's name.</param>
        public static void UpdatePortalModules(string ModuleIDs, string IsActives, int PortalID, string UpdatedBy)
        {
            string sp = "[dbo].[sp_PortalModulesUpdate]";
            SQLHandler SQLH = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleIDs", ModuleIDs));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActives", IsActives));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedBy", UpdatedBy));

                SQLH.ExecuteNonQuery(sp, ParamCollInput);
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and returns parent portal lists
        /// </summary>
        /// <returns>list of parent portals</returns>
        public List<PortalInfo> GetParentPortalList()
        {
            string sp = "[dbo].[usp_PortalGetParent]";
            SQLHandler SQLH = new SQLHandler();
            try
            {
                return SQLH.ExecuteAsList<PortalInfo>(sp);
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
