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

namespace SageFrame.UserProfile
{
    /// <summary>
    /// This class holds the properties  of UserProfileInfo. 
    /// </summary>
    public class UserProfileInfo
    {
        /// <summary>
        /// Get or set UserID.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Get or set path for user image.
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Get or set user name.
        /// </summary>
        public string UserName { get; set;}
        /// <summary>
        /// Get or set user first name.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Get or set user last name.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Get or set user full name.
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Get or set 1 for Male.
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// Get or set location.
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// Get or set text about users.
        /// </summary>
        public string AboutYou { get; set; }
        /// <summary>
        /// Get or set email.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Get or set mobile phone.
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// Get or set mobile.
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// Get or set resident phone.
        /// </summary>
        public string ResPhone { get; set; }
        /// <summary>
        /// Get or set work phone.
        /// </summary>
        public string WorkPhone { get; set; }
        /// <summary>
        /// Get or set additional information about user.
        /// </summary>
        public string Others { get; set; }
        /// <summary>
        /// Get or set true for user deletion.
        /// </summary>
        public bool IsDeleted{get;set;}
        /// <summary>
        /// Get or set true for modification.
        /// </summary>
        public bool IsModified	{get;set;}
        /// <summary>
        /// Get or set added date time.
        /// </summary>
        public DateTime AddedOn{get;set;}
        /// <summary>
        /// Get or set updated date time.
        /// </summary>
        public DateTime UpdatedOn{get;set;}
        /// <summary>
        /// Get or set deletion date time..
        /// </summary>
        public DateTime DeletedOn { get; set; }
        /// <summary>
        /// Get or set user birth date.
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Get or set PortalID.
        /// </summary>
        public int PortalID{get;set;}
        /// <summary>
        /// Get6 or set added user name.
        /// </summary>
        public string  AddedBy{get;set;}
        /// <summary>
        /// Get or set user name,
        /// </summary>
        public string UpdatedBy	{get;set;}
        /// <summary>
        /// Get or set user name.
        /// </summary>
        public string DeletedBy { get; set; }
        /// <summary>
        /// Get or set Customer ID
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// Initializes a new instance of the UserProfileInfo class.
        /// </summary>
        public UserProfileInfo() { }

    }
}
