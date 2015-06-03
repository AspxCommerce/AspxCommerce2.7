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

namespace SageFrame.SEOExtension
{
    /// <summary>
    /// Business logic class for SEO.
    /// </summary>
    public class RobotsController
    {
        /// <summary>
        ///  Returns list for given PortalID.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Returns list</returns>
        public static List<RobotsInfo> GetRobots(int PortalID)
        {
            try
            {
                return (RobotsDataProvider.GetRobots(PortalID));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Saves page.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="UserAgent">User Agent(Search Engines)</param>
        /// <param name="PagePath"> Page path</param>
        public static void SaveRobotsPage(int PortalID, string UserAgent, string PagePath)
        {
            try
            {
                 RobotsDataProvider.SaveRobotsPage(PortalID, UserAgent, PagePath);
            }
            catch (Exception )
            {
                
                throw ;
            }
            
        }
        /// <summary>
        /// Generate robots.
        /// </summary>
        /// <param name="UserAgent">UserAgent</param>
        /// <returns>Returns list</returns>
        public static List<RobotsInfo> GenerateRobots(string UserAgent)
        {
            try
            {
                return (RobotsDataProvider.GenerateRobots(UserAgent));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Delete existing robots.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        public static void DeleteExistingRobots(int PortalID)
        {
            try
            {
                RobotsDataProvider.DeleteExistingRobots(PortalID);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    
    }
}
