using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web;
using SageFrame.Framework;
using SageFrame.ContactUs;
using SageFrame.Message;
using SageFrame.SageFrameClass.MessageManagement;

namespace SageFrame.ContactUs
{
    /// <summary>
    /// Business logic class for ContactUs.
    /// </summary>
    public class ContactUsController
    { 
        /// <summary>
        /// Adds contactus for given portalID.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="email">Email.</param>
        /// <param name="subject">Subject.</param>
        /// <param name="message">Message.</param>
        /// <param name="isActive">IsActive</param>
        /// <param name="portalID">Portal id.</param>
        /// <param name="addedBy">Added by.</param>
        public void ContactUsAdd(string name, string email, string subject, string message, bool isActive, int portalID, string addedBy)
        {
            try
            {
                ContactUsDataProvider contactProvider = new ContactUsDataProvider();
                contactProvider.ContactUsAdd(name, email, message, isActive, portalID, addedBy);
                SageFrameConfig pagebase = new SageFrameConfig();
                string emailSuperAdmin = pagebase.GetSettingValueByIndividualKey(SageFrameSettingKeys.SuperUserEmail);
                string emailSiteAdmin = pagebase.GetSettingValueByIndividualKey(SageFrameSettingKeys.SiteAdminEmailAddress);
                MailHelper.SendMailNoAttachment(email, emailSiteAdmin, subject, email, emailSuperAdmin, string.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Deletes contactus for given portalID.
        /// </summary>
        /// <param name="contactUsID">Contact us id.</param>
        /// <param name="portalID">Portal id.</param>
        /// <param name="deletedBy">Deleted by.</param>
        public void ContactUsDeleteByID(int contactUsID, int portalID, string deletedBy)
        {
            try
            {
                ContactUsDataProvider contactProvider = new ContactUsDataProvider();
                contactProvider.ContactUsDeleteByID(contactUsID, portalID, deletedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Returns ContactUsInfo list for given portalID.
        /// </summary>
        /// <param name="portalID">Portal id.</param>
        /// <returns>ContactUsInfo list</returns>
        public List<ContactUsInfo> ContactUsGetAll(int portalID)
        {
            try
            {
                ContactUsDataProvider contactProvider = new ContactUsDataProvider();
                return (contactProvider.ContactUsGetAll(portalID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
