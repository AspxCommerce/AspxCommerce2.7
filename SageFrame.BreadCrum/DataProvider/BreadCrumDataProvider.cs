/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace SageFrame.BreadCrum
{   
    /// <summary>
    ///  Manipulates data for BreadCrumbController Class.
    /// </summary>
    public class BreadCrumDataProvider
    {
        /// <summary>
        /// Connects to database and gets breadcrumb.
        /// </summary>
        /// <param name="SEOName">SEOName</param>
        /// <param name="PortalID">PortalID</param>
        /// <param name="MenuId">MenuId</param>
        /// <param name="CultureCode">CultureCode</param>
        /// <returns>Returns BreadCrumInfo list </returns>
        public List<BreadCrumInfo> GetBreadCrumb(string SEOName, int PortalID, int MenuId, string CultureCode)
        {
            try
            {

                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@SEOName", SEOName));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureCode", CultureCode));
                //if (MenuId != 0)
                //{
                //    ParaMeterCollection.Add(new KeyValuePair<string, object>("@MenuID", MenuId));
                //    SQLHandler SQLH = new SQLHandler();
                //    SQLH.ExecuteNonQuery("[dbo].[usp_BreadCrumbGetFromMenuItem]", ParaMeterCollection);
                //    return SQLH.ExecuteAsList<BreadCrumInfo>("usp_BreadCrumbMenuItemPath");
                //}
                //else
                //{
                SQLHandler SQLH = new SQLHandler();
                return SQLH.ExecuteAsList<BreadCrumInfo>("usp_BreadCrumbGetFromPageName", ParaMeterCollection);
                //}
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
