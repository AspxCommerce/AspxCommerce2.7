#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;
#endregion


/// <summary>
/// Summary description for EncryptionMD5
/// </summary>
///
namespace SageFrame.Web
{
    public class EncryptionMD5
    {
        public EncryptionMD5()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        const string DESKey = "AQWSEDRF";
        const string DESIV = "HGFEDCBA";

        /// <summary>
        /// Decrypt passing content.
        /// </summary>
        /// <param name="stringToDecrypt">Content to decrypt.</param>
        /// <returns>Decrypt contain.</returns>
        public static string Decrypt(string stringToDecrypt)
        {
            stringToDecrypt = stringToDecrypt.Replace(" ", "+");
            byte[] key;
            byte[] IV;

            byte[] inputByteArray;
            try
            {

                key = Convert2ByteArray(DESKey);

                IV = Convert2ByteArray(DESIV);

                int len = stringToDecrypt.Length; inputByteArray = Convert.FromBase64String(stringToDecrypt);


                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                MemoryStream ms = new MemoryStream();

                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);

                cs.FlushFinalBlock();

                Encoding encoding = Encoding.UTF8; return encoding.GetString(ms.ToArray());
            }

            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        ///  Encrypt passing content.
        /// </summary>
        /// <param name="stringToEncrypt">Content to encrypt.</param>
        /// <returns>Encrypted contain.</returns>
        public static string Encrypt(string stringToEncrypt)
        {

            byte[] key;
            byte[] IV;

            byte[] inputByteArray;
            try
            {

                key = Convert2ByteArray(DESKey);

                IV = Convert2ByteArray(DESIV);

                inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                MemoryStream ms = new MemoryStream(); CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);

                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }

            catch (System.Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// Convert to byte array.
        /// </summary>
        /// <param name="strInput">Content to convert byte array.</param>
        /// <returns>Converted byte array.</returns>
        static byte[] Convert2ByteArray(string strInput)
        {

            int intCounter; char[] arrChar;
            arrChar = strInput.ToCharArray();

            byte[] arrByte = new byte[arrChar.Length];

            for (intCounter = 0; intCounter <= arrByte.Length - 1; intCounter++)
                arrByte[intCounter] = Convert.ToByte(arrChar[intCounter]);

            return arrByte;
        }
        //private const string cryptoKey = "cryptoKey";

        //// The Initialization Vector for the DES encryption routine
        //private static readonly byte[] IV =
        //    new byte[8] { 240, 3, 45, 29, 0, 76, 173, 59 };

        ///// <summary>
        ///// Encrypts provided string parameter
        ///// </summary>
        //public static string Encrypt(string s)
        //{
        //    if (s == null || s.Length == 0) return string.Empty;

        //    string result = string.Empty;

        //    try
        //    {
        //        byte[] buffer = Encoding.ASCII.GetBytes(s);

        //        TripleDESCryptoServiceProvider des =
        //            new TripleDESCryptoServiceProvider();

        //        MD5CryptoServiceProvider MD5 =
        //            new MD5CryptoServiceProvider();

        //        des.Key =
        //            MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));

        //        des.IV = IV;
        //        result = Convert.ToBase64String(
        //            des.CreateEncryptor().TransformFinalBlock(
        //                buffer, 0, buffer.Length));
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// Decrypts provided string parameter
        ///// </summary>
        //public static string Decrypt(string s)
        //{
        //    if (s == null || s.Length == 0) return string.Empty;

        //    string result = string.Empty;

        //    try
        //    {
        //        byte[] buffer = Convert.FromBase64String(s);

        //        TripleDESCryptoServiceProvider des =
        //            new TripleDESCryptoServiceProvider();

        //        MD5CryptoServiceProvider MD5 =
        //            new MD5CryptoServiceProvider();

        //        des.Key =
        //            MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));

        //        des.IV = IV;

        //        result = Encoding.ASCII.GetString(
        //            des.CreateDecryptor().TransformFinalBlock(
        //            buffer, 0, buffer.Length));
        //    }
        //    catch
        //    {
              
        //        throw;
        //    }

        //    return result;
        //}

    }
}
