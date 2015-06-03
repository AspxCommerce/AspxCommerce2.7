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


namespace SageFrame.UserManagement
{
    /// <summary>
    /// Manipulates data to manage user's detail.
    /// </summary>
    public class UserManagementProvider
    {
        /// <summary>
        /// Connects to database and updates user's details.
        /// </summary>
        /// <param name="Usernames">User's name.</param>
        /// <param name="IsActives">Set true if the user is active.</param>
        /// <param name="PortalId">Portal ID.</param>
        /// <param name="UpdatedBy">User's detail updated user's name.</param>
        public static void UpdateUsersChanges(string Usernames, string IsActives, int PortalId, string UpdatedBy)
        {
            string sp = "[dbo].[sp_UsersUpdateChanges]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@Usernames", Usernames));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActives", IsActives));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalId));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedBy", UpdatedBy));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Connects to database and deletes selected user.
        /// </summary>
        /// <param name="Usernames">User's name to be deleted.</param>
        /// <param name="PortalId">Portal ID.</param>
        /// <param name="DeletedBy">Detail deleted by.</param>
        public static void DeleteSelectedUser(string Usernames, int PortalId, string DeletedBy)
        {
            string sp = "[dbo].[sp_UsersDeleteSeleted]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@Usernames", Usernames));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalId));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DeletedBy", DeletedBy));

                sagesql.ExecuteNonQuery(sp, ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Connects to database and returns list of user's name by  portal ID.
        /// </summary>
        /// <param name="Prefix">Prefix</param>
        /// <param name="Count">Count of users.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="Username">User's name.</param>
        /// <returns>List of user's name.</returns>
        public static List<UserManagementInfo> GetUsernameByPortalIDAuto(string Prefix, int Count, int PortalID, string Username)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@Prefix", Prefix));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Count", Count));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));

                return SQLH.ExecuteAsList<UserManagementInfo>("[dbo].[sp_GetUsernameByPortalIDAuto]", ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Connects to database and returns message template by message template type ID.
        /// </summary>
        /// <param name="MessageTemplateTypeID">Message template type ID.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>User's password details.</returns>
        public static ForgotPasswordInfo GetMessageTemplateByMessageTemplateTypeID(int MessageTemplateTypeID, int PortalID)
        {
            SqlDataReader reader = null;
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MessageTemplateTypeID", MessageTemplateTypeID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                reader = SQLH.ExecuteAsDataReader("[dbo].[sp_MessageTemplateByMessageTemplateTypeID]", ParamCollInput);
                ForgotPasswordInfo objList = new ForgotPasswordInfo();
                while (reader.Read())
                {
                    objList.MessageTemplateID = int.Parse(reader["MessageTemplateID"].ToString());
                    objList.MessageTemplateTypeID = int.Parse(reader["MessageTemplateTypeID"].ToString());
                    objList.Subject = reader["Subject"].ToString();
                    objList.Body = reader["Body"].ToString();
                    objList.MailFrom = reader["MailFrom"].ToString();
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
        /// Connects to database and returns message template list by message template ID.
        /// </summary>
        /// <param name="MessageTemplateTypeID">Message template type ID.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>List of message template.</returns>
        public static List<ForgotPasswordInfo> GetMessageTemplateListByMessageTemplateTypeID(int MessageTemplateTypeID, int PortalID)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MessageTemplateTypeID", MessageTemplateTypeID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                return SQLH.ExecuteAsList<ForgotPasswordInfo>("[dbo].[sp_MessageTemplateByMessageTemplateTypeID]", ParamCollInput);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and  returns password recovery successful token.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>Password recovery token.</returns>
        public static DataTable GetPasswordRecoverySuccessfulTokenValue(string Username, int PortalID)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                DataSet ds = SQLH.ExecuteAsDataSet("[dbo].[sp_GetPasswordRecoverySuccessfulTokenValue]", ParamCollInput);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and deactivates recovery code.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        public static void DeactivateRecoveryCode(string Username, int PortalID)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                SQLH.ExecuteNonQuery("[dbo].[usp_PasswordRecoveryDeactivateCode]", ParamCollInput);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Connects to database and returns sucessfully activated token value.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>Activation code.</returns>
        public static DataTable GetActivationSuccessfulTokenValue(string Username, int PortalID)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

                DataSet ds = SQLH.ExecuteAsDataSet("[dbo].[sp_GetActivationSuccessfulTokenValue]", ParamCollInput);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Connects to database and returns activation token value.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>Activation token code.</returns>
        public static DataTable GetActivationTokenValue(string Username, int PortalID)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

                DataSet ds = SQLH.ExecuteAsDataSet("[dbo].[sp_GetActivationTokenValue]", ParamCollInput);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Connects to database and returns recovery password 's token value.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>Returns token value. </returns>
        public static DataTable GetPasswordRecoveryTokenValue(string Username, int PortalID)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                DataSet ds = SQLH.ExecuteAsDataSet("[dbo].[sp_GetPasswordRecoveryTokenValue]", ParamCollInput);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Connects to database and returns user's name.
        /// </summary>
        /// <param name="Code">Activation code.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>User's name.</returns>
        public static ForgotPasswordInfo GetUsernameByActivationOrRecoveryCode(string Code, int PortalID)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@Code", Code));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                List<ForgotPasswordInfo> lstpwdinfo = new List<ForgotPasswordInfo>();
                lstpwdinfo = SQLH.ExecuteAsList<ForgotPasswordInfo>("[dbo].[sp_GetUsernameByActivationOrRecoveryCode]", ParamCollInput);
                ForgotPasswordInfo objInfo = new ForgotPasswordInfo();
                if (lstpwdinfo.Count > 0)
                {
                    return lstpwdinfo[0];
                }
                else
                {
                    objInfo.IsAlreadyUsed = true;
                    return objInfo;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


    }
}
