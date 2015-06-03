using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.ExportUser
{
    /// <summary>
    /// This class holds the properties of ExportUserInfo.
    /// </summary>
   public class ExportUserInfo
    {
       /// <summary>
       /// Get or set UserID.
       /// </summary>
       public Guid UserID { get; set; }
       /// <summary>
       /// Get or set UserName
       /// </summary>
       public string UserName { get; set; }
       /// <summary>
       /// Get or set FirstName
       /// </summary>
       public string FirstName { get; set; }
       /// <summary>
       /// Get or set LastName
       /// </summary>
       public string LastName { get; set; }
       /// <summary>
       /// Get or set Email
       /// </summary>
       public string Email { get; set; }
       /// <summary>
       /// Get or set Password
       /// </summary>
       public string Password { get; set; }
       /// <summary>
       /// Get or set Password Format
       /// </summary>
       public string PasswordFormat { get; set; }
       /// <summary>
       /// Get or set RoleName
       /// </summary>
       public string RoleName { get; set; }
       /// <summary>
       /// Get or set PortalID
       /// </summary>
       public int PortalID { get; set; }
       /// <summary>
       /// Get or set IsApproved
       /// </summary>
       public bool IsApproved { get; set; }
       /// <summary>
       /// Get or set PasswordSalt
       /// </summary>
       public string PasswordSalt { get; set; }
       /// <summary>
       /// Initializes a new instance of the ExportUserInfo class.
       /// </summary>
       public ExportUserInfo() { }
    }
}
