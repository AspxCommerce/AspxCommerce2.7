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
using SageFrame.Security.Crypto;
using SageFrame.Security.Enums;
#endregion

namespace SageFrame.Security.Helpers
{
    /// <summary>
    /// Application password helper.
    /// </summary>
    public class PasswordHelper
    {
        /// <summary>
        /// Check for valid user.
        /// </summary>
        /// <param name="PasswordFormat">password format.</param>
        /// <param name="passwordText">Password.</param>
        /// <param name="passwordHash">Hash password.</param>
        /// <param name="passwordSalt">Salt password.</param>
        /// <returns>True for valid user.</returns>
        public static bool ValidateUser(int PasswordFormat,string passwordText, string passwordHash, string passwordSalt)
        {
            Crypto.Crypto c = new SageFrame.Security.Crypto.Crypto();
            bool verificationStatus = false;
            switch (PasswordFormat)
            {
                case (int)PasswordFormats.CLEAR:
                case (int)PasswordFormats.ONE_WAY_HASHED:
                    verificationStatus = c.VerifyHashString(passwordText, passwordHash, passwordSalt);
                    break;
                case (int)PasswordFormats.ENCRYPTED_AES:                                 
                    verificationStatus = Crypto.Crypto.Decrypt(passwordHash) == passwordText ? true : false;
                    break;
                case (int)PasswordFormats.ENCRYPTED_RSA:
                    break;

            }
            return verificationStatus;

        }
        /// <summary>
        /// Enforce password security.
        /// </summary>
        /// <param name="PasswordFormat">Password format.</param>
        /// <param name="data">data</param>
        /// <param name="PassWord">Password.</param>
        /// <param name="PasswordSalt">Salt password.</param>
        public static void EnforcePasswordSecurity(int PasswordFormat, string data, out string PassWord, out string PasswordSalt)
        {
            Crypto.Crypto cryptObj = new Crypto.Crypto();
            string _Password="", _Salt="";
            switch (PasswordFormat)
            {
                case (int)PasswordFormats.CLEAR:
                case (int)PasswordFormats.ONE_WAY_HASHED:
                    cryptObj.GetHashAndSaltString(data, out _Password, out _Salt);
                    break;
                case (int)PasswordFormats.ENCRYPTED_AES:                   
                    _Password = Crypto.Crypto.Encrypt(data);
                    _Salt = "";
                    break;
                case (int)PasswordFormats.ENCRYPTED_RSA:
                    break;                

            }
            PassWord = _Password;
            PasswordSalt = _Salt;
        }
    }
}
