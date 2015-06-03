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
using System.Data;
#endregion

namespace SageFrame.Security.Helpers
{
    /// <summary>
    /// Applicatio sql helper inherited from SQLHandler.
    /// </summary>
    public class SageFrameSQLHelper:SQLHandler
    {
        /// <summary>
        /// Initializes a new instance of the SageFrameSQLHelper class.
        /// </summary>
        public SageFrameSQLHelper() : base() { }
        /// <summary>
        ///  ExecuteNonQuery with multiple out put.
        /// </summary>
        /// <param name="StroredProcedureName">Strored procedure Name</param>
        /// <param name="InputParamColl">List of input parameter collecvtion.</param>
        /// <param name="OutPutParamColl">List of out put parameter collection.</param>
        /// <returns>List of multipal output.</returns>
        public List<KeyValuePair<int, string>> ExecuteNonQueryWithMultipleOutput(string StroredProcedureName, List<KeyValuePair<string, object>> InputParamColl, List<KeyValuePair<string, object>> OutPutParamColl)
        {
            SqlConnection SQLConn = new SqlConnection(base.connectionString);
            try
            {
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = StroredProcedureName;
                SQLCmd.CommandType = CommandType.StoredProcedure;
                //Loop for Paramets
                foreach (KeyValuePair<string,object> kvp in InputParamColl)
                {
                    SqlParameter sqlParaMeter = new SqlParameter();
                    sqlParaMeter.IsNullable = true;
                    sqlParaMeter.ParameterName = kvp.Key;
                    sqlParaMeter.Value = kvp.Value;
                    SQLCmd.Parameters.Add(sqlParaMeter);
                }

                foreach (KeyValuePair<string, object> kvp in OutPutParamColl)
                {
                    SqlParameter sqlParaMeter = new SqlParameter();
                    sqlParaMeter.IsNullable = true;
                    sqlParaMeter.ParameterName = kvp.Key;
                    sqlParaMeter.Value = kvp.Value;
                    sqlParaMeter.Direction = ParameterDirection.InputOutput;
                    sqlParaMeter.Size = 256;
                    SQLCmd.Parameters.Add(sqlParaMeter);
                }               
                SQLConn.Open();
                SQLCmd.ExecuteNonQuery();
                List<KeyValuePair<int, string>> lstRetValues = new List<KeyValuePair<int, string>>();
                for (int i = 0; i < OutPutParamColl.Count; i++)
                {
                    lstRetValues.Add(new KeyValuePair<int, string>(i, SQLCmd.Parameters[InputParamColl.Count+i].Value.ToString()));
                }
                return lstRetValues;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                SQLConn.Close();
            }
        }
        
    }
}
