/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.BreadCrum.Controller
{  
    /// <summary>
    /// Business logic class for BreadCrum.
    /// </summary>
    public class BreadCrumbController
    {   
        /// <summary>
        /// Returns list for given portalID.
        /// </summary>
        /// <param name="SEOName">SEOName</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="MenuId">MenuId</param>
        /// <param name="CultureCode">CultureCode</param>
        /// <returns>Returns SEOName, PortalID, MenuId, CultureCode </returns>
        public List< BreadCrumInfo> GetBreadCrumb(string SEOName, int PortalID, int MenuId, string CultureCode)
        {
            try
            {
                BreadCrumDataProvider dp = new BreadCrumDataProvider();
                return (dp.GetBreadCrumb(SEOName, PortalID, MenuId, CultureCode));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
