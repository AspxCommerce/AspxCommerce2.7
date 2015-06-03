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
using SageFrame.Web.Utilities;
#endregion

namespace SageFrame.PortalManagement
{
    /// <summary>
    /// Manupulates data for PortalMgrDataProvider.
    /// </summary>
    public class PortalMgrDataProvider
    {
        /// <summary>
        /// Connect to database and add new portal.
        /// </summary>
        /// <param name="PortalName">Portal name.</param>
        /// <param name="IsParent">True for parent portal.</param>
        /// <param name="UserName">User name.</param>
        /// <param name="TemplateName">template name.</param>
        /// <param name="ParentPortal">Parent portal ID.</param>
        /// <param name="PSEOName">Page SEO name.</param>
        public static void AddPortal(string PortalName, bool IsParent, string UserName, string TemplateName,int ParentPortal,string PSEOName)
        {

            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalName", PortalName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsParent", IsParent));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@TemplateName", TemplateName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", UserName));

            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalParentID", ParentPortal));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PSEOName", PSEOName));

            SQLHandler sagesql = new SQLHandler();
            sagesql.ExecuteNonQuery("sp_PortalAdd", ParaMeterCollection);
        }
        /// <summary>
        /// Connect to database and update existing portal.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="PortalName">Portal name.</param>
        /// <param name="IsParent">True for parent.</param>
        /// <param name="UserName">User name.</param>
        /// <param name="TemplateName">Template name.</param>
        public static void UpdatePortal(int PortalID, string PortalName, bool IsParent, string UserName, string TemplateName)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalName", PortalName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsParent", IsParent));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", UserName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@TemplateName", TemplateName));

            SQLHandler sagesql = new SQLHandler();
            sagesql.ExecuteNonQuery("[sp_PortalUpdate]", ParaMeterCollection);


        }
        /// <summary>
        ///  Connect to database and create portal for AspxCommerce.
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
        public int AddStoreSubscriber(string storeName, string firstName, string lastName, string email, string companyName, bool? contact, bool? isParent, string username,
           string password, string passwordSalt, int? passwordFormat, bool? isFromFront)
        {
            List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@StoreName", storeName));
            
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@FirstName", firstName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@LastName", lastName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Email", email));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@CompanyName", companyName));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Contact", contact));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsParent", isParent));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@UserName", username));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@Password", password));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PasswordSalt", passwordSalt));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@PasswordFormat", passwordFormat));
            ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsFromFront", isFromFront));
            SQLHandler sagesql = new SQLHandler();            
            int i = sagesql.ExecuteNonQuery("[usp_Aspx_AddStoreSubscriber]", ParaMeterCollection, "@CustomerID");
            return i;
        }
    }
}
