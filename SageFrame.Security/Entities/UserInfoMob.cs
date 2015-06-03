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

namespace SageFrame.Security.Entities
{
    /// <summary>
    /// This class holds the properties  of UserInfoMob. 
    /// </summary>
    public class UserInfoMob
    {
        /// <summary>
        /// Initializes a new instance of the UserInfoMob class.
        /// </summary>
        public UserInfoMob() { }
        /// <summary>
        /// Get or set user name.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Get or set user first name.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Get or set user last name.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Get or set email.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Get or set current email.
        /// </summary>
        public string EmailCurrent { get; set; }
        /// <summary>
        /// Get or set true for user existence.
        /// </summary>
        public bool UserExists { get; set; }
        /// <summary>
        /// Get or set CustomerID.
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// Get or set status.
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Initializes a new instance of the UserInfoMob class.
        /// </summary>
        /// <param name="_UserExists">True for user existance.</param>
        public UserInfoMob(bool _UserExists)
        {
            this.UserExists = _UserExists;
        }
    }

}
