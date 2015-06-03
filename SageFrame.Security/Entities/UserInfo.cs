/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.Security.Entities
{
    /// <summary>
    /// This class holds the properties of UserInfo.
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        /// <summary>
        /// Get or set portal user ID.
        /// </summary>
        public int PortalUserID { get; set; }  
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
        /// Get or set salt password.
        /// </summary>
        public string PasswordSalt { get; set; }
        /// <summary>
        /// Get or set email/
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Get or set password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Get or set security question.
        /// </summary>
        public string SecurityQuestion { get; set; }
        /// <summary>
        /// Get or set security answer.
        /// </summary>
        public string SecurityAnswer { get; set; }
        /// <summary>
        /// Get or set true for approved.
        /// </summary>
        public bool IsApproved { get; set; }
        /// <summary>
        /// Get or set application name.
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// Get or set UTC current date time.
        /// </summary>
        public DateTime  CurrentTimeUtc { get; set; }
        /// <summary>
        /// Get or set current date.
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Get or set 1 for unique email.
        /// </summary>
        public int UniqueEmail { get; set; }
        /// <summary>
        /// Get or set  <see cref="T:SageFrame.Security.Enums.PasswordFormats"/> 
        /// </summary>
        public int PasswordFormat { get; set; }
        /// <summary>
        /// Get or set PortalID.
        /// </summary>
        public int PortalID { get; set; }
        /// <summary>
        /// Get or set added date time.
        /// </summary>
        public DateTime AddedOn { get; set; }
        /// <summary>
        /// Get or set added user name.
        /// </summary>
        public string AddedBy { get; set; }
        /// <summary>
        /// Get or set UserID.
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// Get or set true for active.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Get or set updated user name.
        /// </summary>
        public string  UpdatedBy { get; set; }
        /// <summary>
        /// Get or set last activity date.
        /// </summary>
        public DateTime LastActivityDate { get; set; }
        /// <summary>
        /// Get or set last activity date.
        /// </summary>
        public DateTime LastLoginDate { get; set; }
        /// <summary>
        /// Get or set last password change date.
        /// </summary>
        public DateTime LastPasswordChangeDate { get; set; }
        /// <summary>
        /// Get or set current email.
        /// </summary>
        public string EmailCurrent { get; set; }
        /// <summary>
        /// Get or set true for user existence.
        /// </summary>
        public bool UserExists { get; set; }
        /// <summary>
        /// Get or set list of RoleInfo class.
        /// </summary>
        public List<RoleInfo> LSTRoles { get; set; }
        /// <summary>
        /// Get or set role name.
        /// </summary>
        public string RoleNames { get; set; }
        /// <summary>
        /// Get or set StoreID.
        /// </summary>
        public int StoreID { get; set; }
        /// <summary>
        /// Get or set CustomerID.
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// Initializes a new instance of the UserInfo class.
        /// </summary>
        public UserInfo() { }
        /// <summary>
        /// Initializes a new instance of the UserInfo class.
        /// </summary>
        /// <param name="_UserName">User name.</param>
        /// <param name="_FirstName">user first name.</param>
        /// <param name="_Email">Email</param>
        /// <param name="_PortalID">PortalID</param>
        /// <param name="_UserID">UserID</param>
        public UserInfo(string _UserName, string _FirstName, string _Email, int _PortalID, Guid _UserID)
        {
            
            this.UserName = _UserName;
            this.FirstName = _FirstName;
            this.Email = _Email;
            this.PortalID = _PortalID;
            this.UserID = _UserID;
        }
        /// <summary>
        /// Initializes a new instance of the UserInfo class.
        /// </summary>
        /// <param name="_UserName">User name.</param>
        /// <param name="_PortalID">PortalID</param>
        /// <param name="_ApplicationName">Application name.</param>
        /// <param name="_AddedBy">User name.</param>
        public UserInfo(string _UserName, int _PortalID, string _ApplicationName, string _AddedBy)
        {
            this.UserName = _UserName;
            this.PortalID = _PortalID;
            this.ApplicationName = _ApplicationName;
            this.AddedBy = _AddedBy;

        }
        /// <summary>
        /// Initializes a new instance of the UserInfo class.
        /// </summary>
        /// <param name="_UserName">User name.</param>
        /// <param name="_PortalID">PortalID</param>
        /// <param name="_ApplicationName">Application name.</param>
        /// <param name="_AddedBy">User name.</param>
        /// <param name="_StoreID">StoreID</param>
        public UserInfo(string _UserName, int _PortalID, string _ApplicationName, string _AddedBy, int _StoreID)
        {
            this.UserName = _UserName;
            this.PortalID = _PortalID;
            this.ApplicationName = _ApplicationName;
            this.AddedBy = _AddedBy;
            this.StoreID = _StoreID;

        }
        /// <summary>
        /// Initializes a new instance of the UserInfo class.
        /// </summary>
        /// <param name="_ApplicationName">Application name.</param>
        /// <param name="_UserID">UserID</param>
        /// <param name="_RoleNames">Role names.</param>
        /// <param name="_PortalID">PortalID</param>
        public UserInfo(string _ApplicationName, Guid _UserID, string _RoleNames, int _PortalID)
        {
            this.ApplicationName = _ApplicationName;
            this.UserID = _UserID;
            this.RoleNames = _RoleNames;
            this.PortalID = _PortalID;
        }
        /// <summary>
        /// Initializes a new instance of the UserInfo class.
        /// </summary>
        /// <param name="_UserID">UserID</param>
        /// <param name="_Password">Password</param>
        /// <param name="_PasswordSalt">Salt password.</param>
        public UserInfo(Guid _UserID, string _Password, string _PasswordSalt)
        {
            this.UserID = _UserID;
            this.Password = _Password;
            this.PasswordSalt = _PasswordSalt;
        }
        /// <summary>
        /// Initializes a new instance of the UserInfo class.
        /// </summary>
        /// <param name="_UserID">UserID</param>
        /// <param name="_Password">Password.</param>
        /// <param name="_PasswordSalt">Salt password.</param>
        /// <param name="_PasswordFormat">Password format.</param>
        public UserInfo(Guid _UserID, string _Password, string _PasswordSalt,int _PasswordFormat)
        {
            this.UserID = _UserID;
            this.Password = _Password;
            this.PasswordSalt = _PasswordSalt;
            this.PasswordFormat = _PasswordFormat;
        }
        /// <summary>
        /// Initializes a new instance of the UserInfo class.
        /// </summary>
        /// <param name="_ApplicationName">Application name.</param>
        /// <param name="_UserName">User name.</param>
        /// <param name="_UserID">UserID</param>
        /// <param name="_FirstName">User first name.</param>
        /// <param name="_LastName">User last name.</param>
        /// <param name="_Email">Email</param>
        /// <param name="_PortalID">PortalID</param>
        /// <param name="_IsApproved">true for approved.</param>
        /// <param name="_UpdatedBy">User name.</param>
        public UserInfo(string _ApplicationName, string _UserName, Guid _UserID, string _FirstName, string _LastName, string _Email, int _PortalID, bool _IsApproved, string _UpdatedBy)
        {
            this.ApplicationName = _ApplicationName;
            this.UserName = _UserName;
            this.UserID = _UserID;
            this.FirstName = _FirstName;
            this.LastName = _LastName;
            this.Email = _Email;
            this.PortalID = _PortalID;
            this.IsApproved = _IsApproved;
            this.UpdatedBy = _UpdatedBy;        
        }
        /// <summary>
        /// Initializes a new instance of the UserInfo class.
        /// </summary>
        /// <param name="_ApplicationName">Application name.</param>
        /// <param name="_UserName">User name.</param>
        /// <param name="_UserID">UserID</param>
        /// <param name="_FirstName">First name.</param>
        /// <param name="_LastName">User last name.</param>
        /// <param name="_Email">Email</param>
        /// <param name="_PortalID">PortalID</param>
        /// <param name="_IsApproved">User name.</param>
        /// <param name="_UpdatedBy"></param>
        /// <param name="_StoreID">StoreID</param>
        public UserInfo(string _ApplicationName, string _UserName, Guid _UserID, string _FirstName, string _LastName, string _Email, int _PortalID, bool _IsApproved, string _UpdatedBy, int _StoreID)
        {
            this.ApplicationName = _ApplicationName;
            this.UserName = _UserName;
            this.UserID = _UserID;
            this.FirstName = _FirstName;
            this.LastName = _LastName;
            this.Email = _Email;
            this.PortalID = _PortalID;
            this.IsApproved = _IsApproved;
            this.UpdatedBy = _UpdatedBy;
            this.StoreID = _StoreID;
        }
        /// <summary>
        /// Initializes a new instance of the UserInfo class.
        /// </summary>
        /// <param name="_ApplicationName">Application name.</param>
        /// <param name="_UserName">User name.</param>
        /// <param name="_UserID">UserID</param>
        /// <param name="_FirstName">User first name.</param>
        /// <param name="_LastName">User lat name.</param>
        /// <param name="_Email">Email.</param>
        /// <param name="_PortalID">PortalID</param>
        /// <param name="_IsApproved">true for approved.</param>
        /// <param name="_UpdatedBy">User name.</param>
        /// <param name="_StoreID">StoreID</param>
        /// <param name="_CustomerID">CustomerID</param>
        public UserInfo(string _ApplicationName, string _UserName, Guid _UserID, string _FirstName, string _LastName, string _Email, int _PortalID, bool _IsApproved, string _UpdatedBy, int _StoreID, int _CustomerID)
        {
            this.ApplicationName = _ApplicationName;
            this.UserName = _UserName;
            this.UserID = _UserID;
            this.FirstName = _FirstName;
            this.LastName = _LastName;
            this.Email = _Email;
            this.PortalID = _PortalID;
            this.IsApproved = _IsApproved;
            this.UpdatedBy = _UpdatedBy;
            this.StoreID = _StoreID;
            this.CustomerID = _CustomerID;
        }
        /// <summary>
        /// Initializes a new instance of the UserInfo class.
        /// </summary>
        /// <param name="_UserExists">true for user existence.</param>
        public UserInfo(bool _UserExists)
        {
            this.UserExists = _UserExists;
        }



      
    }
}
