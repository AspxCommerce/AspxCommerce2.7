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


namespace SageFrame.Message
{
    /// <summary>
    /// Manupulates data for mail message 
    /// </summary>
    public class MessageManagementProvider
    {
        /// <summary>
        /// Connects to database and Returns all the message template type.
        /// </summary>
        /// <param name="IsActive">Set true if the message template is active.</param>
        /// <param name="IsDeleted">Set true if the message template is deleted.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="Username">User's name.</param>
        /// <param name="CurrentCulture">Culture name.</param>
        /// <returns>List of MessageManagementInfo object containing message template type.</returns>
        public List<MessageManagementInfo> GetMessageTemplateTypeList(bool IsActive, bool IsDeleted, int PortalID, string Username, string CurrentCulture)
        {
            try
            {
                string sp = "[dbo].[sp_GetMessageTemplateTypeList]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsDeleted", IsDeleted));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("@CurrentCulture", CurrentCulture));

                return SQLH.ExecuteAsList<MessageManagementInfo>(sp, ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and returns message template type token list by message template type.
        /// </summary>
        /// <param name="MessageTemplateTypeID">Message template type ID</param>
        /// <param name="PortalID">Portal ID</param>
        /// <returns>List of message template type token</returns>
        public List<MessageManagementInfo> GetMessageTemplateTypeTokenListByMessageTemplateType(int MessageTemplateTypeID, int PortalID)
        {
            try
            {
                string sp = "[dbo].[sp_MessageTemplateTypeTokenListByMessageTemplateType]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MessageTemplateTypeID", MessageTemplateTypeID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

                return SQLH.ExecuteAsList<MessageManagementInfo>(sp, ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and  returns message template by message template type ID.
        /// </summary>
        /// <param name="MessageTemplateTypeID">Message template type ID.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>list of  message templates.</returns>
        public static List<MessageManagementInfo> GetMessageTemplateByMessageTemplateTypeID(int MessageTemplateTypeID, int PortalID)
        {
            try
            {
                string sp = "[dbo].[sp_MessageTemplateByMessageTemplateTypeID]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MessageTemplateTypeID", MessageTemplateTypeID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

                return SQLH.ExecuteAsList<MessageManagementInfo>(sp, ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and updates message template
        /// </summary>
        /// <param name="MessageTemplateID">Message template ID.</param>
        /// <param name="MessageTemplateTypeID">Message template type ID.</param>
        /// <param name="Subject">Email subject.</param>
        /// <param name="Body">Email body.</param>
        /// <param name="MailFrom">Email from.</param>
        /// <param name="IsActive">Set true if email is active.</param>
        /// <param name="UpdatedOn">Email updated on.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="UpdatedBy">Email updated user's name.</param>
        /// <param name="CurrentCulture">Culture name.</param>
        public void UpdateMessageTemplate(int MessageTemplateID, int MessageTemplateTypeID, string Subject, string Body, string MailFrom, bool IsActive, DateTime UpdatedOn, int PortalID, string UpdatedBy, string CurrentCulture)
        {
            try
            {
                string sp = "sp_MessageTemplateUpdate";
                SQLHandler SQLH = new SQLHandler();

                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MessageTemplateID", MessageTemplateID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MessageTemplateTypeID", MessageTemplateTypeID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Subject", Subject));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Body", Body));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MailFrom", MailFrom));

                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedOn", UpdatedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UpdatedBy", UpdatedBy));
                ParamCollInput.Add(new KeyValuePair<string, object>("@CurrentCulture", CurrentCulture));

                SQLH.ExecuteNonQuery(sp, ParamCollInput);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and adds message template.
        /// </summary>
        /// <param name="MessageTemplateTypeID">Message template type ID.</param>
        /// <param name="Subject">Email subject.</param>
        /// <param name="Body">Email body.</param>
        /// <param name="MailFrom">Email from.</param>
        /// <param name="IsActive">Set true if email is active</param>
        /// <param name="AddedOn">Message added date.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="AddedBy">Message added by.</param>
        /// <param name="CurrentCulture">Culture name.</param>
        /// <returns>Returns message template ID</returns>
        public int AddMessageTemplate(int MessageTemplateTypeID, string Subject, string Body, string MailFrom, bool IsActive, DateTime AddedOn, int PortalID, string AddedBy, string CurrentCulture)
        {
            try
            {
                string sp = "[dbo].[sp_MessageTemplateAdd]";
                SQLHandler SQLH = new SQLHandler();

                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();

                ParamCollInput.Add(new KeyValuePair<string, object>("@MessageTemplateTypeID", MessageTemplateTypeID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Subject", Subject));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Body", Body));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MailFrom", MailFrom));

                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", AddedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", AddedBy));
                ParamCollInput.Add(new KeyValuePair<string, object>("@CurrentCulture", CurrentCulture));

                return SQLH.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "@MessageTemplateID");


            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Connects to database and returns message template list.
        /// </summary>
        /// <param name="IsActive">Set true if active message template is to be retrive.</param>
        /// <param name="IsDeleted">Set true if deleted message template is to be retrive.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="Username">User's name.</param>
        /// <param name="CurrentCulture">Culture name.</param>
        /// <returns>Returns list of message.</returns>
        public List<MessageManagementInfo> GetMessageTemplateList(bool IsActive, bool IsDeleted, int PortalID, string Username, string CurrentCulture)
        {
            try
            {
                string sp = "[dbo].[sp_GetMessageTemplateList]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsDeleted", IsDeleted));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("@CurrentCulture", CurrentCulture));

                return SQLH.ExecuteAsList<MessageManagementInfo>(sp, ParamCollInput);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and returns message template  by message template ID.
        /// </summary>
        /// <param name="MessageTemplateID">Message Template ID.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>MessageManagemenetInfo class onject contain message details</returns>
        public MessageManagementInfo GetMessageTemplate(int MessageTemplateID, int PortalID)
        {
            SqlDataReader reader = null;
            try
            {

                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MessageTemplateID", MessageTemplateID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                reader = SQLH.ExecuteAsDataReader("[dbo].[sp_GetMessageTemplate]", ParamCollInput);
                MessageManagementInfo objList = new MessageManagementInfo();

                while (reader.Read())
                {

                    objList.Subject = reader["Subject"].ToString();
                    objList.Body = reader["Body"].ToString();
                    objList.MailFrom = reader["MailFrom"].ToString();
                    objList.IsActive = bool.Parse(reader["IsActive"].ToString());
                    objList.MessageTemplateTypeID = int.Parse(reader["MessageTemplateTypeID"].ToString());

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
        ///  Connects to database and deletes message template.
        /// </summary>
        /// <param name="MessageTemplateID">Message template ID.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="DeletedOn">Message deleted date.</param>
        /// <param name="DeletedBy">Message deleted user's name.</param>
        public void DeleteMessageTemplate(int MessageTemplateID, int PortalID, DateTime DeletedOn, string DeletedBy)
        {
            try
            {
                string sp = "[dbo].[sp_MessageTemplateDelete]";
                SQLHandler SQLH = new SQLHandler();

                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MessageTemplateID", MessageTemplateID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DeletedOn", DeletedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DeletedBy", DeletedBy));

                SQLH.ExecuteNonQuery(sp, ParamCollInput);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and adds message template type.
        /// </summary>
        /// <param name="Name">Template type name.</param>
        /// <param name="IsActive">Set true if message template type is active.</param>
        /// <param name="AddedOn">Template type added date.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="AddedBy">Template type added user's name.</param>
        /// <returns>Returns message template type ID</returns>
        public int AddMessageTemplateType(string Name, bool IsActive, DateTime AddedOn, int PortalID, string AddedBy)
        {
            try
            {
                string sp = "[dbo].[sp_MessageTemplateTypeAdd]";
                SQLHandler SQLH = new SQLHandler();

                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@Name", Name));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", AddedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", AddedBy));
                return SQLH.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "@MessageTemplateTypeID");


            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Connects to database and returns user's first name.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>User's first name.</returns>
        public MessageManagementInfo GetUserFirstName(string Username, int PortalID)
        {
            SqlDataReader reader = null;
            try
            {
                string sp = "[dbo].[sp_GetUserFirstName]";
                SQLHandler SQLH = new SQLHandler();

                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

                reader = SQLH.ExecuteAsDataReader(sp, ParamCollInput);
                MessageManagementInfo objInfo = new MessageManagementInfo();

                while (reader.Read())
                {
                    objInfo.FirstName = reader["FirstName"].ToString();
                }
                return objInfo;
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
        }

        /// <summary>
        /// Connects to database and returns user's last name.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>User's last name.</returns>
        public MessageManagementInfo GetUserLastName(string Username, int PortalID)
        {
            SqlDataReader reader = null;
            try
            {
                string sp = "[dbo].[sp_GetUserLastName]";
                SQLHandler SQLH = new SQLHandler();

                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

                reader = SQLH.ExecuteAsDataReader(sp, ParamCollInput);
                MessageManagementInfo objInfo = new MessageManagementInfo();

                while (reader.Read())
                {
                    objInfo.LastName = reader["LastName"].ToString();
                }
                return objInfo;
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
        }

        /// <summary>
        /// Connects to database and returns user's email by user's name and potral ID
        /// </summary>
        /// <param name="Username">User's name</param>
        /// <param name="PortalID">Portal ID</param>
        /// <returns>User's email</returns>
        public MessageManagementInfo GetUserEmail(string Username, int PortalID)
        {
            SqlDataReader reader = null;
            try
            {
                string sp = "[dbo].[sp_GetUserEmail]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                reader = SQLH.ExecuteAsDataReader(sp, ParamCollInput);
                MessageManagementInfo objInfo = new MessageManagementInfo();
                while (reader.Read())
                {
                    objInfo.Email = reader["Email"].ToString();
                }
                return objInfo;
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
        }

        /// <summary>
        /// Connects to database and returns activation code
        /// </summary>
        /// <param name="Username">User's name</param>
        /// <param name="PortalID">Portal ID</param>
        /// <returns>Activation code</returns>
        public MessageManagementInfo GetUserActivationCode(string Username, int PortalID)
        {
            SqlDataReader reader = null;
            try
            {
                string sp = "[dbo].[sp_GetUserActivationCode]";
                SQLHandler SQLH = new SQLHandler();

                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserName", Username));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", PortalID));

                reader = SQLH.ExecuteAsDataReader(sp, ParamCollInput);
                MessageManagementInfo objInfo = new MessageManagementInfo();

                while (reader.Read())
                {
                    objInfo.UserId = new Guid(reader["UserId"].ToString());
                }
                return objInfo;
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
        }

        /// <summary>
        /// Connects to database and adds message template token
        /// </summary>
        /// <param name="messageTokenID">Message token ID</param>
        /// <param name="messageTemplateTypeID">Message template type ID</param>
        /// <param name="name">Message template type name</param>
        /// <param name="isActive">Set true if message template</param>
        /// <param name="addedOn">Message template added date</param>
        /// <param name="portalID">Portal ID</param>
        /// <param name="addedBy">Message template adding user's name</param>
        /// <returns>returns message token ID</returns>
        public int MessageTemplateTokenAdd(int messageTokenID, int messageTemplateTypeID, string name, bool isActive, DateTime addedOn, int portalID, string addedBy)
        {
            try
            {
                string sp = "[dbo].[usp_MessageTemplateTokenAdd]";
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@MessageTemplateTypeID", messageTemplateTypeID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Name", name));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", isActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedOn", addedOn));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", addedBy));
                return  SQLH.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "@messageTokenID");
            }
            catch(Exception e)
            {
                throw e;
            }
        }        

        /// <summary>
        /// Connects to database and  checks for message template uniqueness.
        /// </summary>
        /// <param name="messageTempTypeName">Message template type name.</param>
        /// <param name="portalID">Portal ID.</param>
        /// <returns>Returns true if messasge template type is unique.</returns>
        public bool CheckMessgeTemplateUnique(string messageTempTypeName, int portalID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
                ParaMeter.Add(new KeyValuePair<string, object>("@MsgTemplateTypeName", messageTempTypeName));
                ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                return sqlH.ExecuteNonQueryAsBool("[usp_MsgTempTypeUniquenessCheck]", ParaMeter, "@IsUnique");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and checks for message token uniqueness.
        /// </summary>
        /// <param name="messageTempTokenName">Message template token name.</param>
        /// <param name="messageTemplateTypeID">Messege template type ID</param>
        /// <param name="portalID">Portal ID</param>
        /// <returns>Returns true if message token is unique </returns>
        public bool CheckMessgeTokenUnique(string messageTempTokenName, int messageTemplateTypeID, int portalID)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> ParaMeter = new List<KeyValuePair<string, object>>();
                ParaMeter.Add(new KeyValuePair<string, object>("@MsgTemplateTokenName", messageTempTokenName));
                ParaMeter.Add(new KeyValuePair<string, object>("@MsgTemplateTypeID", messageTemplateTypeID));
                ParaMeter.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                return sqlH.ExecuteNonQueryAsBool("[usp_MsgTempTokenUniquenessCheck]", ParaMeter, "@IsUnique");
            }
            catch (Exception e)
            {
                throw e;
            }
        }     
    }
}
