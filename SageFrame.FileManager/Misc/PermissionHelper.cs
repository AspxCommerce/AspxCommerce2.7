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
using System.Data;
using SageFrame.Web.Utilities;
using System.Data.SqlClient;
#endregion

namespace SageFrame.FileManager
{
    /// <summary>
    /// Permission Helper.
    /// </summary>
    public class PermissionHelper
    {
        /// <summary>
        /// Connects to database and obatins all portal roles.
        /// </summary>
        /// <param name="portalID">portalID</param>
        /// <param name="isAll">isAll</param>
        /// <param name="userName">userName</param>
        /// <returns>List of Roles class containing RoleID and RoleName. </returns>
        public static List<Roles> GetAllRoles(int portalID,bool isAll,string userName)
        {
            List<Roles> lstRoles = new List<Roles>();
            string StoredProcedureName = "sp_PortalRoleList";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsAll", isAll));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", userName));

            SqlDataReader SQLReader = null;
            try
            {
                SQLReader = sagesql.ExecuteAsDataReader(StoredProcedureName, ParaMeterCollection);
                while (SQLReader.Read())
                {
                    Roles obj = new Roles();
                    obj.RoleID = new Guid(SQLReader["RoleID"].ToString());
                    obj.RoleName = SQLReader["RoleName"].ToString();
                    lstRoles.Add(obj);
                }
            }
            catch(Exception)
            {

            }
            finally
            {
                if (SQLReader != null)
                {
                    SQLReader.Close();
                }
            }
            return lstRoles;
        }
    }
}
