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


namespace SageFrame.Packages
{
    /// <summary>
    /// Business logic for packages
    /// </summary>
    public class PackagesController
    {

        /// <summary>
        /// Updates packages change.
        /// </summary>
        /// <param name="ModuleIDs">Module ID</param>
        /// <param name="IsActives">Set true if the respective modules are active.</param>
        /// <param name="UpdatedBy">Change updated user's name.</param>
        public void UpdatePackagesChange(string ModuleIDs, string IsActives, string UpdatedBy)
        {
            try
            {
                PackagesProvider.UpdatePackagesChange(ModuleIDs, IsActives, UpdatedBy);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Returns package by portal ID
        /// </summary>
        /// <param name="PortalID">Portal ID</param>
        /// <param name="SearchText">Search Text</param>
        /// <returns>List of packages</returns>
        public static List<PackagesInfo> GetPackagesByPortalID(int PortalID, string SearchText)
        {
            try
            {
                return PackagesProvider.GetPackagesByPortalID(PortalID, SearchText);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
    }
}
