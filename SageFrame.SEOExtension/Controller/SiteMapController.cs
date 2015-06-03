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
    public class SiteMapController
    {  
        /// <summary>
        /// Returns SiteMapInfo list for given PortalID.
        /// </summary>
        /// <param name="prefix">prefix</param>
        /// <param name="IsActive">IsActive</param>
        /// <param name="IsDeleted">IsDeleted</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="Username">Username</param>
        /// <param name="IsVisible">IsVisible</param>
        /// <param name="IsRequiredPage">IsRequiredPage</param>
        /// <returns>Returns list </returns>
        public static List<SiteMapInfo> GetSiteMap(string prefix, bool IsActive, bool IsDeleted, int PortalID, string Username, bool IsVisible, bool IsRequiredPage)
        {
            try
            {
                return (SiteMapDataProvider.GetSiteMap(prefix, IsActive, IsDeleted, PortalID, Username, IsVisible, IsRequiredPage));
            }
            catch (Exception)
            {

                throw;
            }
        }
    
    }
}
