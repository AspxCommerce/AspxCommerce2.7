#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
#endregion

namespace SageFrame.Common.Shared
{
    /// <summary>
    /// Encode and decode helper. 
    /// </summary>
    public class EncodingHelper
    {
        /// <summary>
        /// Base 64 encoding
        /// </summary>
        /// <param name="str">string value for encode.</param>
        /// <returns>Encoded value.</returns>
        public static string Encode(string str)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }
        /// <summary>
        /// Base 64 decoding.
        /// </summary>
        /// <param name="str">String value for decode.</param>
        /// <returns>Decoded value.</returns>
        public static string Decode(string str)
        {
            byte[] decbuff = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }
        /// <summary>
        /// UTF-8 encoding
        /// </summary>
        /// <param name="str">string value for encode.</param>
        /// <returns>Encoded value</returns>
        
        public static string Encodeutf8(string str)
        {
            return(HttpContext.Current.Server.HtmlEncode(str));
        }
        /// <summary>
        /// UTF-8 decoding
        /// </summary>
        /// <param name="str">String value for decode.</param>
        /// <returns>Decoded value.</returns>
        public static string Decodeutf8(string str)
        {
            return (HttpContext.Current.Server.HtmlDecode(str));
        }

    }
}
