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


namespace SageFrame.UserAgent
{
    public class UserAgentController
    {
        public string GetUserAgent(int PortalID, bool IsActive)
        {
            try
            {
                UserAgentProvider objProvider = new UserAgentProvider();
                return (objProvider.GetUserAgent(PortalID, IsActive));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SaveUserAgentMode(string AgentMode, int PortalID, string UserName, DateTime ChangeDate, bool IsActive)
        {
            try
            {
                UserAgentProvider objProvider = new UserAgentProvider();
                objProvider.SaveUserAgentMode(AgentMode, PortalID, UserName, DateTime.Now, true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
