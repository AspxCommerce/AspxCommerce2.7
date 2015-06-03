using SageFrame.Security.Entities;
using SageFrame.Security.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageFrame.Security.Controllers
{
    /// <summary>
    /// Manupulates data for SuspendedIPController.
    /// </summary>
    public class SuspendedIPController
    {
        /// <summary>
        /// Save Suspended IP.
        /// </summary>
        /// <param name="IpAddress">IpAddress</param>
        public void SaveSuspendedIP(string IpAddress)
        {
            SuspendedIPProvider objProvider = new SuspendedIPProvider();
            objProvider.SaveSuspendedIP(IpAddress);
        }
        /// <summary>
        /// Check condition for suspended IP.
        /// </summary>
        /// <param name="IpAddress">IpAddress</param>
        /// <returns>Returns True for SuspendedIP Address</returns>
        public bool IsSuspendedIP(string IpAddress)
        {
            SuspendedIPProvider objProvider = new SuspendedIPProvider();
            return objProvider.IsSuspendedIP(IpAddress);
        }

        /// <summary>
        /// Returns list of Suspended IP.
        /// </summary>
        /// <returns>List ofSuspended IP</returns>
        public List<SuspendedIPInfo> GetSuspendedIP()
        {
            try
            {
                SuspendedIPProvider objProvider = new SuspendedIPProvider();
                return objProvider.GetSuspendedIP();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Update Suspended IP
        /// </summary>
        /// <param name="SuspendedIPID">ID of SuspendedIP</param>
        /// <param name="IsSuspended">True for SuspendedIP</param>
        public void UpdateSuspendedIP(string SuspendedIPID, string IsSuspended)
        {
            try
            {
                SuspendedIPProvider objProvider = new SuspendedIPProvider();
                objProvider.UpdateSuspendedIP(SuspendedIPID, IsSuspended);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }            
        }
    }
}
