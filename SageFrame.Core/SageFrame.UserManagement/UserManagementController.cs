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

#endregion


namespace SageFrame.UserManagement
{
    /// <summary>
    /// Business logic class  for user management.
    /// </summary>
    public class UserManagementController
    {
        /// <summary>
        /// Updates user's details.
        /// </summary>
        /// <param name="Usernames">User's name.</param>
        /// <param name="IsActives">Set true if the user is active.</param>
        /// <param name="PortalId">Portal ID.</param>
        /// <param name="UpdatedBy">User's detail updated user's name.</param>
        public static void UpdateUsersChanges(string Usernames, string IsActives, int PortalId, string UpdatedBy)
        {
            try
            {
                UserManagementProvider.UpdateUsersChanges(Usernames, IsActives, PortalId, UpdatedBy);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Deletes selected user.
        /// </summary>
        /// <param name="Usernames">User's name to be deleted.</param>
        /// <param name="PortalId">Portal ID.</param>
        /// <param name="DeletedBy">Detail deleted by.</param>
        public static void DeleteSelectedUser(string Usernames, int PortalId, string DeletedBy)
        {
            try
            {
                UserManagementProvider.DeleteSelectedUser(Usernames, PortalId, DeletedBy);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns list of user's name by  portal ID.
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
                return UserManagementProvider.GetUsernameByPortalIDAuto(Prefix, Count, PortalID, Username);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns message template by message template type ID.
        /// </summary>
        /// <param name="MessageTemplateTypeID">Message template type ID.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>User's password details.</returns>
        public static ForgotPasswordInfo GetMessageTemplateByMessageTemplateTypeID(int MessageTemplateTypeID, int PortalID)
        {
            try
            {
                return UserManagementProvider.GetMessageTemplateByMessageTemplateTypeID(MessageTemplateTypeID, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns message template list by message template ID.
        /// </summary>
        /// <param name="MessageTemplateTypeID">Message template type ID.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>List of message template.</returns>
        public static List<ForgotPasswordInfo> GetMessageTemplateListByMessageTemplateTypeID(int MessageTemplateTypeID, int PortalID)
        {
            try
            {
                return UserManagementProvider.GetMessageTemplateListByMessageTemplateTypeID(MessageTemplateTypeID, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns password recovery successful token.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>Password recovery token.</returns>
        public static DataTable GetPasswordRecoverySuccessfulTokenValue(string Username, int PortalID)
        {
            try
            {
                return UserManagementProvider.GetPasswordRecoverySuccessfulTokenValue(Username, PortalID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///Deactivates recovery code.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        public static void DeactivateRecoveryCode(string Username, int PortalID)
        {
            try
            {
                UserManagementProvider.DeactivateRecoveryCode(Username, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns sucessfully activated token value.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>Activation code.</returns>
        public static DataTable GetActivationSuccessfulTokenValue(string Username, int PortalID)
        {
            try
            {
                return UserManagementProvider.GetActivationSuccessfulTokenValue(Username, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Returns user's name by activation or recocery code.
        /// </summary>
        /// <param name="Code">Activation code.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>User's name.</returns>
        public static ForgotPasswordInfo GetUsernameByActivationOrRecoveryCode(string Code, int PortalID)
        {
            try
            {
                return UserManagementProvider.GetUsernameByActivationOrRecoveryCode(Code, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Returns recovery password 's token value.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>Returns token value. </returns>
        public static DataTable GetPasswordRecoveryTokenValue(string Username, int PortalID)
        {
            try
            {
                return UserManagementProvider.GetPasswordRecoveryTokenValue(Username, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Returns activation token value.
        /// </summary>
        /// <param name="Username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>Activation token code.</returns>
        public static DataTable GetActivationTokenValue(string Username, int PortalID)
        {
            try
            {
                return UserManagementProvider.GetActivationTokenValue(Username, PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
