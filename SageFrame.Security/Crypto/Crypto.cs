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
using System.Security.Cryptography;
using System.IO;
#endregion

namespace SageFrame.Security.Crypto
{
    /// <summary>
    /// Crypto class for protecting application information. 
    /// </summary>
    public class Crypto
    {

        #region Variables
        HashAlgorithm HashProvider;
        int SalthLength;
        #endregion

        #region Constants
        private const int ENCRYPTION_KEY_BYTES = 16;
        private const string ENCRYPTION_KEY = "T@#r$t%!@#$%^&*()_+xr";
        private const string ENCRYPTION_INIT_VECTOR = "!@#$IV7890123456";
        private const string ENCRYPTION_ALGORITHM_NAME = "Rijndael";
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Crypto class.
        /// </summary>
        /// <param name="HashAlgorithm">Object of HashAlgorithm class.</param>
        /// <param name="theSaltLength">Salt length.</param>
        public Crypto(HashAlgorithm HashAlgorithm, int theSaltLength)
        {
            HashProvider = HashAlgorithm;
            SalthLength = theSaltLength;
        }
        /// <summary>
        /// Initializes a new instance of the Crypto class.
        /// </summary>
        public Crypto()
            : this(new SHA256Managed(), 4)
        {
        }
        #endregion

        #region HashPassword
        /// <summary>
        /// Compute hash.
        /// </summary>
        /// <param name="Data">Bytes array of data.</param>
        /// <param name="Salt">Bytes array of salt.</param>
        /// <returns>Bytes array of computing hash value of our plain text with appended salt.</returns>
        private byte[] ComputeHash(byte[] Data, byte[] Salt)
        {
            // Allocate memory to store both the Data and Salt together
            byte[] DataAndSalt = new byte[Data.Length + SalthLength];

            // Copy both the data and salt into the new array
            Array.Copy(Data, DataAndSalt, Data.Length);
            Array.Copy(Salt, 0, DataAndSalt, Data.Length, SalthLength);

            // Calculate the hash
            // Compute hash value of our plain text with appended salt.
            return HashProvider.ComputeHash(DataAndSalt);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Data">Bytes array of Data.</param>
        /// <param name="Hash">Bytes array of Hash.</param>
        /// <param name="Salt">Bytes array of Salt.</param>
        public void GetHashAndSalt(byte[] Data, out byte[] Hash, out byte[] Salt)
        {
            // Allocate memory for the salt
            Salt = new byte[SalthLength];

            // Strong runtime pseudo-random number generator, on Windows uses CryptAPI
            // on Unix /dev/urandom
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

            // Create a random salt
            random.GetNonZeroBytes(Salt);

            // Compute hash value of our data with the salt.
            Hash = ComputeHash(Data, Salt);
        }
        /// <summary>
        /// Obtain Hash and Salt string.
        /// </summary>
        /// <param name="Data">Data</param>
        /// <param name="Hash">Hash</param>
        /// <param name="Salt">Salt</param>
        public void GetHashAndSaltString(string Data, out string Hash, out string Salt)
        {
            byte[] HashOut;
            byte[] SaltOut;

            // Obtain the Hash and Salt for the given string
            GetHashAndSalt(Encoding.UTF8.GetBytes(Data), out HashOut, out SaltOut);

            // Transform the byte[] to Base-64 encoded strings
            Hash = Convert.ToBase64String(HashOut);
            Salt = Convert.ToBase64String(SaltOut);
        }
        /// <summary>
        /// Verify hash.
        /// </summary>
        /// <param name="Data">Bytes array of Data.</param>
        /// <param name="Hash">Bytes array of Hash.</param>
        /// <param name="Salt">Bytes array of Salt.</param>
        /// <returns>True if verified.</returns>
        public bool VerifyHash(byte[] Data, byte[] Hash, byte[] Salt)
        {
            byte[] NewHash = ComputeHash(Data, Salt);

            //  No easy array comparison in C# -- we do the legwork
            if (NewHash.Length != Hash.Length) return false;

            for (int Lp = 0; Lp < Hash.Length; Lp++)
                if (!Hash[Lp].Equals(NewHash[Lp]))
                    return false;

            return true;
        }
        /// <summary>
        /// Verify hash string.
        /// </summary>
        /// <param name="Data">Data</param>
        /// <param name="Hash">Hash</param>
        /// <param name="Salt">Salt</param>
        /// <returns>True if verified.</returns>
        public bool VerifyHashString(string Data, string Hash, string Salt)
        {
            byte[] HashToVerify = Convert.FromBase64String(Hash);
            byte[] SaltToVerify = Convert.FromBase64String(Salt);
            byte[] DataToVerify = Encoding.UTF8.GetBytes(Data);
            return VerifyHash(DataToVerify, HashToVerify, SaltToVerify);
        }
        #endregion

        #region EncryptPassword
        /// <summary>
        /// Encrypt given value.
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Encrypted value.</returns>
        public static string Encrypt(string value)
        {
            MemoryStream memoryStreamIn = new MemoryStream();
            MemoryStream memoryStreamOut = new MemoryStream();
            CryptoStream cryptoStream;
            SymmetricAlgorithm symmetricAlgorithm;
            ICryptoTransform cryptoTransform;
            int valueLength;
            byte[] byteBuffer = new byte[2048];
            string result = string.Empty;

            try
            {
                memoryStreamIn.Write(System.Text.Encoding.Default.GetBytes(value), 0, System.Text.Encoding.Default.GetBytes(value).Length);
                memoryStreamIn.Position = 0;
                symmetricAlgorithm = SymmetricAlgorithm.Create(ENCRYPTION_ALGORITHM_NAME);
                symmetricAlgorithm.IV = System.Text.Encoding.Default.GetBytes(ENCRYPTION_INIT_VECTOR);
                symmetricAlgorithm.Key = GetKey(ENCRYPTION_KEY);
                cryptoTransform = symmetricAlgorithm.CreateEncryptor();
                cryptoStream = new CryptoStream(memoryStreamOut, cryptoTransform, CryptoStreamMode.Write);
                valueLength = memoryStreamIn.Read(byteBuffer, 0, byteBuffer.Length);

                while ((valueLength > 0))
                {
                    cryptoStream.Write(byteBuffer, 0, valueLength);
                    valueLength = memoryStreamIn.Read(byteBuffer, 0, byteBuffer.Length);
                }

                cryptoStream.FlushFinalBlock();
                result = System.Convert.ToBase64String(memoryStreamOut.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                memoryStreamIn.Close();
                memoryStreamOut.Close();
            }

            return result;
        }
        /// <summary>
        /// Decrypt value.
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Decrypted value.</returns>
        public static string Decrypt(string value)
        {
            MemoryStream memoryStreamIn = new MemoryStream();
            MemoryStream memoryStreamOut = new MemoryStream();
            CryptoStream cryptoStream;
            SymmetricAlgorithm symmetricAlgorithm;
            ICryptoTransform cryptoTransform;
            int valueLength;
            byte[] byteBuffer = new byte[2048];
            string result = string.Empty;

            try
            {
                memoryStreamIn.Write(Convert.FromBase64String(value), 0, Convert.FromBase64String(value).Length);
                memoryStreamIn.Position = 0;
                symmetricAlgorithm = SymmetricAlgorithm.Create(ENCRYPTION_ALGORITHM_NAME);
                symmetricAlgorithm.IV = System.Text.Encoding.Default.GetBytes(ENCRYPTION_INIT_VECTOR);
                symmetricAlgorithm.Key = GetKey(ENCRYPTION_KEY);
                cryptoTransform = symmetricAlgorithm.CreateDecryptor();
                cryptoStream = new CryptoStream(memoryStreamIn, cryptoTransform, CryptoStreamMode.Read);
                valueLength = cryptoStream.Read(byteBuffer, 0, byteBuffer.Length);

                while ((valueLength > 0))
                {
                    memoryStreamOut.Write(byteBuffer, 0, valueLength);
                    valueLength = cryptoStream.Read(byteBuffer, 0, byteBuffer.Length);
                }

                result = System.Text.Encoding.Default.GetString(memoryStreamOut.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                memoryStreamIn.Close();
                memoryStreamOut.Close();
            }

            return result;
        }
        /// <summary>
        /// Obtain encryption key.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Bytes array of encryption key.</returns>
        private static byte[] GetKey(string key)
        {
            string resultKey;
            if (key.Length >= ENCRYPTION_KEY_BYTES)
            {
                resultKey = key.Substring(0, ENCRYPTION_KEY_BYTES);
            }
            else
            {
                resultKey = key;

                for (int index = key.Length + 1; index <= ENCRYPTION_KEY_BYTES; index++)
                {
                    resultKey = resultKey + "0";
                }
            }
            return System.Text.Encoding.Default.GetBytes(resultKey);
        }
        #endregion
    }
}
