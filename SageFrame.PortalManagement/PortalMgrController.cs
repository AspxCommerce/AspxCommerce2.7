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

namespace SageFrame.PortalManagement
{
    /// <summary>
    /// Business logic for PortalMgrController.
    /// </summary>
    public class PortalMgrController
    {
        /// <summary>
        /// Add new portal.
        /// </summary>
        /// <param name="PortalName">Portal name.</param>
        /// <param name="IsParent">True for parent portal.</param>
        /// <param name="UserName">User name.</param>
        /// <param name="TemplateName">template name.</param>
        /// <param name="ParentPortal">Parent portal ID.</param>
        /// <param name="PSEOName">Page SEO name.</param>
        public static void AddPortal(string PortalName, bool IsParent, string UserName, string TemplateName,int ParentPortal,string  PSEOName)
        {
            try
            {
                PortalMgrDataProvider.AddPortal(PortalName, IsParent, UserName, TemplateName, ParentPortal, PSEOName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Update existing portal.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="PortalName">Portal name.</param>
        /// <param name="IsParent">True for parent.</param>
        /// <param name="UserName">User name.</param>
        /// <param name="TemplateName">Template name.</param>
        public static void UpdatePortal(int PortalID, string PortalName, bool IsParent, string UserName, string TemplateName)
        {
            try
            {
                PortalMgrDataProvider.UpdatePortal(PortalID, PortalName, IsParent, UserName, TemplateName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        ///  Create portal for AspxCommerce.
        /// </summary>
        /// <param name="storeName">Store name.</param>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="email">Email.</param>
        /// <param name="companyName">Company name.</param>
        /// <param name="contact">Contact.</param>
        /// <param name="isParent">True for parent.</param>
        /// <param name="username">User name.</param>
        /// <param name="password">Password.</param>
        /// <param name="passwordSalt">Password salt.</param>
        /// <param name="passwordFormat">Password format.</param>
        /// <param name="isFromFront">True if  portal creation request is from demo user.</param>
        /// <returns>Newly created store ID.</returns>
        public static int AddStoreSubscriber(string storeName, string firstName, string lastName, string email, string companyName,
          System.Nullable<bool> contact, System.Nullable<bool> isParent, string username, string password,
          string passwordSalt, System.Nullable<int> passwordFormat, System.Nullable<bool> isFromFront)
        {
            try
            {
                PortalMgrDataProvider pmdp = new PortalMgrDataProvider();
                int i = pmdp.AddStoreSubscriber(storeName, firstName, lastName, email, companyName,
             contact, isParent, username, password, passwordSalt, passwordFormat, isFromFront);
                return i;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
