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

namespace SageFrame.UserAgent
{
    public class UserAgentProvider
    {

        public  string GetUserAgent(int PortalID, bool IsActive)
        {

            string sp = "[dbo].[usp_UserAgentGetType]";
            SQLHandler sagesql = new SQLHandler();
            string content = "";
            SqlDataReader reader = null;
            try
            {
                List<KeyValuePair<string, object>> paramColl = new List<KeyValuePair<string, object>>();
                paramColl.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                paramColl.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                reader = sagesql.ExecuteAsDataReader(sp, paramColl);
                while (reader.Read())
                {
                    content = reader["AgentMode"] as string;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return content;
        }

        public  void SaveUserAgentMode(string AgentMode, int PortalID, string UserName, DateTime ChangeDate, bool IsActive)
        {
            string sp = "[dbo].[usp_UserAgentSaveType]";
            SQLHandler sagesql = new SQLHandler();

            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@AgentMode", AgentMode));
            ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@ChangedBy", UserName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@ChangedDate", ChangeDate));
            ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
            try
            {
                sagesql.ExecuteNonQuery(sp, ParamCollInput);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
