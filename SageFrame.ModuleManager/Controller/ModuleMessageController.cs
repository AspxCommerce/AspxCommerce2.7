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

namespace SageFrame.ModuleMessage
{
    /// <summary>
    /// Business logic for module message.
    /// </summary>
    public class ModuleMessageController
    {
        /// <summary>
        /// Get all modules.
        /// </summary>
        /// <returns>List of ModuleMessageInfo class.</returns>
        public static List<ModuleMessageInfo> GetAllModules()
        {
            return (ModuleMessageDataProvider.GetAllModules());
        }
        /// <summary>
        /// Add module message.
        /// </summary>
        /// <param name="objModuleMessage">Object of ModuleMessageInfo class.</param>
        public static void AddModuleMessage(ModuleMessageInfo objModuleMessage)
        {
            try
            {
                ModuleMessageDataProvider.AddModuleMessage(objModuleMessage);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Obtain module message.
        /// </summary>
        /// <param name="ModuleID">ModuleID</param>
        /// <param name="Culture">Culture code.</param>
        /// <returns>object of ModuleMessageInfo class.</returns>
        public static ModuleMessageInfo GetModuleMessage(int ModuleID, string Culture)
        {
            try
            {
                return (ModuleMessageDataProvider.GetModuleMessage(ModuleID, Culture));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Obtain module message based on UserModuleID and culture.
        /// </summary>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="Culture">Culture code.</param>
        /// <returns>Object of ModuleMessageInfo class.</returns>
        public static ModuleMessageInfo GetModuleMessageByUserModuleID(int UserModuleID, string Culture)
        {
            try
            {
                return (ModuleMessageDataProvider.GetModuleMessageByUserModuleID(UserModuleID, Culture));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Update message status.
        /// </summary>
        /// <param name="ModuleID">ModuleID</param>
        /// <param name="IsActive">true if active.</param>
        public static void UpdateMessageStatus(int ModuleID, bool IsActive)
        {
            try
            {
                ModuleMessageDataProvider.UpdateMessageStatus(ModuleID, IsActive);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
