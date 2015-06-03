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


namespace SageFrame.Message
{
    /// <summary>
    /// Business logic for message management
    /// </summary>
    public class MessageManagementController
    {
        /// <summary>
        /// Returns all the message template type.
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
                MessageManagementProvider objProvider = new MessageManagementProvider();
                return objProvider.GetMessageTemplateTypeList(IsActive, IsDeleted, PortalID, Username, CurrentCulture);
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Returns message template type token list by message template type.
        /// </summary>
        /// <param name="MessageTemplateTypeID">Message template type ID</param>
        /// <param name="PortalID">Portal ID</param>
        /// <returns>List of message template type token</returns>
        public List<MessageManagementInfo> GetMessageTemplateTypeTokenListByMessageTemplateType(int MessageTemplateTypeID, int PortalID)
        {
            try
            {
                MessageManagementProvider objProvider = new MessageManagementProvider();
                return objProvider.GetMessageTemplateTypeTokenListByMessageTemplateType(MessageTemplateTypeID, PortalID);
            }
            catch (Exception)
            {

                throw;
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
                return MessageManagementProvider.GetMessageTemplateByMessageTemplateTypeID(MessageTemplateTypeID, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updates message template
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
                MessageManagementProvider objProvider = new MessageManagementProvider();
                objProvider.UpdateMessageTemplate(MessageTemplateID, MessageTemplateTypeID, Subject, Body, MailFrom, IsActive, UpdatedOn, PortalID, UpdatedBy, CurrentCulture);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Adds message template.
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
                MessageManagementProvider objProvider = new MessageManagementProvider();
                return objProvider.AddMessageTemplate(MessageTemplateTypeID, Subject, Body, MailFrom, IsActive, AddedOn, PortalID, AddedBy, CurrentCulture);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns message template list.
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
                MessageManagementProvider objProvider = new MessageManagementProvider();
                return objProvider.GetMessageTemplateList(IsActive, IsDeleted, PortalID, Username, CurrentCulture);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns message template  by message template ID.
        /// </summary>
        /// <param name="MessageTemplateID">Message Template ID.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>MessageManagemenetInfo class onject contain message details</returns>
        public MessageManagementInfo GetMessageTemplate(int MessageTemplateID, int PortalID)
        {
            try
            {
                MessageManagementProvider objProvider = new MessageManagementProvider();
                return objProvider.GetMessageTemplate(MessageTemplateID, PortalID);
            }
            catch (Exception)
            {

                throw;
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
                MessageManagementProvider objProvider = new MessageManagementProvider();
                objProvider.DeleteMessageTemplate(MessageTemplateID, PortalID, DeletedOn, DeletedBy);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Adds message template type.
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
                MessageManagementProvider objProvider = new MessageManagementProvider();
                return objProvider.AddMessageTemplateType( Name, IsActive, AddedOn, PortalID, AddedBy);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns user's first name.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>User's first name.</returns>
        public MessageManagementInfo GetUserFirstName(string Username, int PortalID)
        {
            try
            {
                MessageManagementProvider objProvider = new MessageManagementProvider();
                return objProvider.GetUserFirstName(Username, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns user's last name.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>User's last name.</returns>
        public MessageManagementInfo GetUserLastName(string Username, int PortalID)
        {
            try
            {
                MessageManagementProvider objProvider = new MessageManagementProvider();
                return objProvider.GetUserLastName(Username, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns user's email by user's name and potral ID
        /// </summary>
        /// <param name="Username">User's name</param>
        /// <param name="PortalID">Portal ID</param>
        /// <returns>User's email</returns>
        public MessageManagementInfo GetUserEmail(string Username, int PortalID)
        {
            try
            {
                MessageManagementProvider objProvider = new MessageManagementProvider();
                return objProvider.GetUserEmail(Username, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns activation code
        /// </summary>
        /// <param name="Username">User's name</param>
        /// <param name="PortalID">Portal ID</param>
        /// <returns>Activation code</returns>
        public MessageManagementInfo GetUserActivationCode(string Username, int PortalID)
        {
            try
            {
                MessageManagementProvider objProvider = new MessageManagementProvider();
                return objProvider.GetUserActivationCode(Username, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Adds message template token
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
                MessageManagementProvider objProvider = new MessageManagementProvider();
                return objProvider.MessageTemplateTokenAdd(messageTokenID, messageTemplateTypeID, name, isActive, addedOn, portalID, addedBy);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Checks for message template uniqueness.
        /// </summary>
        /// <param name="messageTempTypeName">Message template type name.</param>
        /// <param name="portalID">Portal ID.</param>
        /// <returns>Returns true if messasge template type is unique.</returns>
        public bool CheckMessgeTemplateUnique(string messageTempTypeName, int portalID)
        {
            try
            {
                 MessageManagementProvider objProvider = new MessageManagementProvider();
                 return objProvider.CheckMessgeTemplateUnique(messageTempTypeName, portalID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Checks for message token uniqueness.
        /// </summary>
        /// <param name="messageTempTokenName">Message template token name.</param>
        /// <param name="messageTemplateTypeID">Messege template type ID</param>
        /// <param name="portalID">Portal ID</param>
        /// <returns>Returns true if message token is unique </returns>
        public bool CheckMessgeTokenUnique(string messageTempTokenName, int messageTemplateTypeID, int portalID)
        {
            try
            {
                MessageManagementProvider objProvider = new MessageManagementProvider();
                return objProvider.CheckMessgeTokenUnique(messageTempTokenName, messageTemplateTypeID, portalID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
