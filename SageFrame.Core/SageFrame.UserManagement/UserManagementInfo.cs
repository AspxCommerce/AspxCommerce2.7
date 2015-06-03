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


namespace SageFrame.UserManagement
{
    /// <summary>
    /// Entity class for user management.
    /// </summary>
    public class UserManagementInfo
    {
        /// <summary>
        /// Gets or sets user's name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets message template ID.
        /// </summary>
        public int MessageTemplateID { get; set; }

        /// <summary>
        /// Gets or sets message template type ID.
        /// </summary>
        public int MessageTemplateTypeID { get; set; }

        /// <summary>
        /// Gets or sets message subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets message body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets message mailing email.
        /// </summary>
        public string MailFrom { get; set; }

        /// <summary>
        /// Initializes an intance of UserManagementInfo class.
        /// </summary>
        public UserManagementInfo() { }
    }
}
