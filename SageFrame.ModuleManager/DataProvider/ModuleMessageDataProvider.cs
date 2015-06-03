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
#endregion

namespace SageFrame.ModuleMessage
{
    /// <summary>
    /// Manupulates data for ModuleMessageDataProvider.
    /// </summary>
    public class ModuleMessageDataProvider
    {
        /// <summary>
        /// Connect to database and obtain all modules.
        /// </summary>
        /// <returns>List of ModuleMessageInfo class.</returns>
        public static List<ModuleMessageInfo> GetAllModules()
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();              
                return SQLH.ExecuteAsList<ModuleMessageInfo>("[dbo].[usp_ModuleMessageGetModules]");
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connect to database and add module message.
        /// </summary>
        /// <param name="objModuleMessage">Object of ModuleMessageInfo class.</param>
        public static void AddModuleMessage(ModuleMessageInfo objModuleMessage)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleID", objModuleMessage.ModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Message", objModuleMessage.Message));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", objModuleMessage.Culture));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", objModuleMessage.IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MessageType", objModuleMessage.MessageType));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MessageMode", objModuleMessage.MessageMode));
                ParamCollInput.Add(new KeyValuePair<string, object>("@MessagePosition", objModuleMessage.MessagePosition));

                SQLH.ExecuteNonQuery("[dbo].[usp_ModuleMessageAdd]",ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connect to database and obtain module message.
        /// </summary>
        /// <param name="ModuleID">ModuleID</param>
        /// <param name="Culture">Culture code.</param>
        /// <returns>Object of ModuleMessageInfo.</returns>
        public static ModuleMessageInfo GetModuleMessage(int ModuleID,string Culture)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleID", ModuleID));             
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", Culture));

                return SQLH.ExecuteAsObject<ModuleMessageInfo>("[dbo].[usp_ModuleMessageGet]", ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connect to database and obtain module message based on user module ID and culture code.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="Culture">Culture code.</param>
        /// <returns>Object of ModuleMessageInfo class.</returns>
        public static ModuleMessageInfo GetModuleMessageByUserModuleID(int UserModuleID, string Culture)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@UserModuleID", UserModuleID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", Culture));

                return SQLH.ExecuteAsObject<ModuleMessageInfo>("[dbo].[usp_ModuleMessageGetByUserModuleID]", ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Connect to database and update message status.
        /// </summary>
        /// <param name="ModuleID">ModuleID</param>
        /// <param name="IsActive">true if active.</param>
        public static void UpdateMessageStatus(int ModuleID, bool IsActive)
        {
            try
            {
                SQLHandler SQLH = new SQLHandler();
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ModuleID",ModuleID));               
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive",IsActive));

                SQLH.ExecuteNonQuery("[dbo].[usp_ModuleMessageUpdateStatus]", ParamCollInput);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
