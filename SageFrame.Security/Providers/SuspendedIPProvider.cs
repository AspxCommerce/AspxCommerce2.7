using SageFrame.Security.Entities;
using SageFrame.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageFrame.Security.Providers
{
    /// <summary>
    /// Manupulates data for SuspendedIPProvider.
    /// </summary>
    public class SuspendedIPProvider
    {
        /// <summary>
        /// Connect to database and save Suspended IP.
        /// </summary>
        /// <param name="IpAddress">IpAddress</param>
        public void SaveSuspendedIP(string IpAddress)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IpAddress", IpAddress));
            SQLHandler sageSQL = new SQLHandler();
            sageSQL.ExecuteNonQuery("usp_SaveSuspendedIP", ParaMeterCollection);
        }

       /// <summary>
       /// Connect to the database and check condition for suspended IP.
       /// </summary>
       /// <param name="IpAddress">IpAddress</param>
        /// <returns>Returns True for SuspendedIP Address</returns>
        public bool IsSuspendedIP(string IpAddress)
        {
            string sp = "[dbo].[usp_IsSuspendedIP]";
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@IpAddress", IpAddress));
            SqlDataReader reader = null;
            bool isSuspended = true;
            try
            {
                reader = sagesql.ExecuteAsDataReader(sp, ParamCollInput);
                while (reader.Read())
                {
                    isSuspended = bool.Parse(reader["Suspended"].ToString());
                }
                reader.Close();
                return isSuspended;
            }
            catch (Exception ex)
            {
                throw (ex);
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
        /// Connects to database and returns list of Suspended IP.
        /// </summary>
        /// <returns>List of Suspended IP</returns>
        public List<SuspendedIPInfo> GetSuspendedIP()
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                return SQLH.ExecuteAsList<SuspendedIPInfo>("[dbo].[usp_GetSuspendedIP]", ParamCollInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Connect to database and update Suspended IP
        /// </summary>
        /// <param name="SuspendedIPID">ID of SuspendedIP</param>
        /// <param name="IsSuspended">True for SuspendedIP Address</param>
        public void UpdateSuspendedIP(string SuspendedIPID, string IsSuspended)
        {
            string sp = "[dbo].[usp_UpdateSuspendedIP]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@SuspendedIPID", SuspendedIPID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsSuspended", IsSuspended));
                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
