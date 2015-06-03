#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Xml;
using System.Web;
using System.Data;
using SageFrame.Message;
using System.Web.Security;
using SageFrame.Web;
using SageFrame.UserManagement;

#endregion



namespace SageFrameClass.MessageManagement
{
    /// <summary>
    /// Handles  message token
    /// </summary>
    public class MessageToken
    {
        /// <summary>
        /// Returns list of all the tokens from the message token.xml.
        /// </summary>
        /// <returns>list of tokens.</returns>
        public static NameValueCollection GetListOfAllowedTokens()
        {
            NameValueCollection allowedTokens = new NameValueCollection();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("SageFrame\\App_GlobalResources\\MessageToken.xml");
                foreach (XmlNode node in doc.SelectSingleNode("messagetokens").ChildNodes)
                {
                    if ((node.NodeType != XmlNodeType.Comment))
                    {
                        allowedTokens.Add(node.Attributes["value"].Value, node.Attributes["key"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return allowedTokens;
        }       

        /// <summary>
        /// Replaces all the messagetoken in a messagetemple with  their respective values.
        /// </summary>
        /// <param name="messageTemplate">Message template.</param>
        /// <param name="username">User's name.</param>
        /// <param name="PortalID">portalID.</param>
        /// <returns>Replaced message template.</returns>
        public static string ReplaceAllMessageToken(string messageTemplate, string username, Int32 PortalID)
        {
            string[] tokens = GetAllToken(messageTemplate);
            foreach (string token in tokens)
            {
                switch (token)
                {
                    case "%UserFirstName%":
                        string fName = GetUserFirstName(username, PortalID);
                        messageTemplate = messageTemplate.Replace(token, fName);
                        break;
                    case "%UserLastName%":
                        string lName = GetUserLastName(username, PortalID);
                        messageTemplate = messageTemplate.Replace(token, lName);
                        break;
                    case "%UserEmail%":
                        string userEmail = GetUserEmail(username, PortalID);
                        messageTemplate = messageTemplate.Replace(token, userEmail);
                        break;
                    case "%UserActivationCode%":
                        string act = GetUserActivationCode(username, PortalID);
                        act = EncryptionMD5.Encrypt(act);
                        messageTemplate = messageTemplate.Replace(token, act);
                        break;
                    case "%Username%":
                        messageTemplate.Replace(token, username);
                        break;
                }
            }
            return messageTemplate;
        }

        /// <summary>
        /// Replaces all the message token in messagetemplate
        /// </summary>
        /// <param name="messageTemplate">Message template.</param>
        /// <param name="messageTokenValueDT">Message token values.</param>
        /// <returns>Replacef message template.</returns>
        public static string ReplaceAllMessageToken(string messageTemplate, DataTable messageTokenValueDT)
        {
            string messageToken = string.Empty;
            string messateTokenValue = string.Empty;
            for (int i = 0; i < messageTokenValueDT.Columns.Count; i++)
            {
                messageToken = messageTokenValueDT.Columns[i].ColumnName.ToString().Replace('_', '%');
                messateTokenValue = messageTokenValueDT.Rows[0][i].ToString();
                switch (messageToken)
                {
                    case "%UserActivationCode%":
                        messateTokenValue = EncryptionMD5.Encrypt(messateTokenValue);
                        break;
                }
                messageTemplate = messageTemplate.Replace(messageToken, messateTokenValue);
            }
            return messageTemplate;
        }

        /// <summary>
        /// Replaces token from template.
        /// </summary>
        /// <param name="template">Template.</param>
        /// <param name="token">Token.</param>
        /// <param name="value">Value to be replace.</param>
        /// <returns>Token replaced value</returns>
        public static string ReplaceToken(string template, string token, string value)
        {
            return template.Replace(token, value);
        }

        /// <summary>
        /// Returs user' First name.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>User's first name.</returns>
        public static string GetUserFirstName(string username, Int32 PortalID)
        {
            MessageManagementController objController = new MessageManagementController();
            MessageManagementInfo objInfo = objController.GetUserFirstName(username, PortalID);
            return objInfo.FirstName;
        }

        /// <summary>
        /// Returns user's last name.
        /// </summary>
        /// <param name="username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>last name.</returns>
        public static string GetUserLastName(string username, Int32 PortalID)
        {
            MessageManagementController objController = new MessageManagementController();
            MessageManagementInfo objInfo = objController.GetUserLastName(username, PortalID);
            return objInfo.LastName;
        }

        /// <summary>
        /// Returns user's email by user's name.
        /// </summary>
        /// <param name="username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>User's last name.</returns>
        public static string GetUserEmail(string username, Int32 PortalID)
        {
            MessageManagementController objController = new MessageManagementController();
            MessageManagementInfo objInfo = objController.GetUserEmail(username, PortalID);
            return objInfo.Email;
        }

        /// <summary>
        /// Returns user's activation code.
        /// </summary>
        /// <param name="username">User's name.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>User's activation code.</returns>
        public static string GetUserActivationCode(string username, Int32 PortalID)
        {
            MessageManagementController objController = new MessageManagementController();
            MessageManagementInfo objInfo = objController.GetUserActivationCode(username, PortalID);
            return objInfo.UserId.ToString();
        }
        
        /// <summary>
        /// Returns first token from the template.
        /// </summary>
        /// <param name="template">Template</param>
        /// <returns>First token</returns>
        public static string GetFirstToken(string template)
        {
            int preIndex = template.IndexOf('%', 0);
            int postIndex = template.IndexOf('%', preIndex + 1);
            string token = template.Substring(preIndex, postIndex - preIndex);
            return string.Empty;
        }

        /// <summary>
        /// Returns all the token from template.
        /// </summary>
        /// <param name="template">Template</param>
        /// <returns>Array of tokens</returns>
        public static string[] GetAllToken(string template)
        {
            List<string> returnValue = new List<string> { };
            int preIndex = template.IndexOf('%', 0);
            int postIndex = template.IndexOf('%', preIndex + 1);
            while (preIndex > -1)
            {

                returnValue.Add(template.Substring(preIndex, (postIndex - preIndex) + 1));
                template = template.Substring(postIndex + 1, (template.Length - postIndex) - 1);
                preIndex = template.IndexOf('%', 0);
                postIndex = template.IndexOf('%', preIndex + 1);
            }
            return returnValue.ToArray();
        }
       
    }
}
