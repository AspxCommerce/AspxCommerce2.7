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
    /// Entities class for user registration and activation.
    /// </summary>
    public class ForgotPasswordInfo
    {
        /// <summary>
        /// Gets or sets message template ID.
        /// </summary>
        public int MessageTemplateID { get; set; }

        /// <summary>
        /// Gets or sets message template type ID.
        /// </summary>
        public int MessageTemplateTypeID { get; set; }

        /// <summary>
        /// Gets or sets mail's subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets mail's body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets mail sending email
        /// </summary>
        public string MailFrom { get; set; }

        /// <summary>
        /// Returns or retains true if the mail is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Returns or retains true if the mail is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Returns or retains true if the mail is modified.
        /// </summary>
        public bool IsModified { get; set; }

        /// <summary>
        /// Gets or sets password created date.
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Gets or sets password updated date.
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets password deleted date.
        /// </summary>
        public DateTime DeletedOn { get; set; }

        /// <summary>
        /// Gets or sets portal ID.
        /// </summary>
        public int PortalID { get; set; }

        /// <summary>
        /// Gets or sets password added user's name.
        /// </summary>
        public string AddedBy { get; set; }

        /// <summary>
        /// Gets or sets password updated user's name.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets password deleted user's name.
        /// </summary>
        public string DeletedBy { get; set; }

        /// <summary>
        /// Gets or sets user activation code.
        /// </summary>
        public Guid _UserActivationCode_ { get; set; }

        /// <summary>
        /// Gets or sets user's name.
        /// </summary>
        public string _Username_ { get; set; }

        /// <summary>
        /// Gets or sets user's first name.
        /// </summary>
        public string _UserFirstName_ { get; set; }

        /// <summary>
        /// Gets or sets user's first name.
        /// </summary>
        public string _UserLastName_ { get; set; }

        /// <summary>
        /// Gets or sets user's email.
        /// </summary>
        public string _UserEmail_ { get; set; }

        /// <summary>
        /// Gets or sets email activation code.
        /// </summary>
        public Guid Code { get; set; }

        /// <summary>
        /// Gets or sets activation start datetime.
        /// </summary>
        public DateTime ActiveFrom { get; set; }

        /// <summary>
        /// Gets or sets activation ended date.
        /// </summary>
        public DateTime ActiveTo { get; set; }

        /// <summary>
        /// Gets or sets code for what purpose.
        /// </summary>
        public string CodeForPurpose { get; set; }

        /// <summary>
        /// Gets or sets code for username.
        /// </summary>
        public string CodeForUsername { get; set; }

        /// <summary>
        /// Returns or retains true if the code is already used.
        /// </summary>
        public bool IsAlreadyUsed { get; set; }

        /// <summary>
        /// Initializes an instance of ForgotPasswordInfo class.
        /// </summary>
        public ForgotPasswordInfo() { }
    }
}
