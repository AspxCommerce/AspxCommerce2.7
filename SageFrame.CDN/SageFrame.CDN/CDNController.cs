using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.CDN
{
    public class CDNController
    {
        public void SaveLinks(List<CDNInfo> objInfo)
        {
            try
            {
                CDNProvider obj = new CDNProvider();
                obj.SaveLinks(objInfo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Returns CDN object list for given portalID
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns> CDNInfo object list  </returns>
        public List<CDNInfo> GetCDNLinks(int PortalID)
        {
            try
            {
                CDNProvider obj = new CDNProvider();
                return obj.GetCDNLinks(PortalID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Saves  URL loading order of CDN links
        /// </summary>
        /// <param name="objOrder">List of CDNInfo objects</param>
        public void SaveOrder(List<CDNInfo> objOrder)
        {
            try
            {
                CDNProvider obj = new CDNProvider();
                obj.SaveOrder(objOrder);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Deletes url of CDN
        /// </summary>
        /// <param name="UrlID">Url id</param>
        /// <param name="PortalID">Portal id</param>
        public void DeleteURL(int UrlID, int PortalID)
        {
            CDNProvider objController = new CDNProvider();
            objController.DeleteURL(UrlID, PortalID);
        }
    }
}
