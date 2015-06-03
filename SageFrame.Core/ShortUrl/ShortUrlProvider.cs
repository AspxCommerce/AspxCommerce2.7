using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace SageFrame.Core
{
    /// <summary>
    /// Manipulates data for shorten URL.
    /// </summary>
    public class ShortUrlProvider
    {
        /// <summary>
        /// Connects to database and encodes the url.
        /// </summary>
        /// <param name="url">User readable URL</param>
        /// <param name="code">Encrpted url.</param>
        /// <returns>Encrypted URL.</returns>
        public string EncodeUrl(string url, string code)
        {
            try
            {
                List<KeyValuePair<string, object>> ParameterCollection = new List<KeyValuePair<string, object>>();
                ParameterCollection.Add(new KeyValuePair<string, object>("@Url", url));
                ParameterCollection.Add(new KeyValuePair<string, object>("@Code", code));
                SQLHandler objHandler = new SQLHandler();
                return objHandler.ExecuteAsScalar<string>("[dbo].[USP_SHORTURL_ENCODE]", ParameterCollection);
            }
            catch 
            {
                //SageFrame.Web.ProcessException(ex);
                throw;
            }
        }

        /// <summary>
        /// Decodes the given encrpted URL to user friendly URL.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string DecodeUrl(string key)
        {
            try
            {
                List<KeyValuePair<string, object>> ParameterCollection = new List<KeyValuePair<string, object>>();
                ParameterCollection.Add(new KeyValuePair<string, object>("@Key", key));
                SQLHandler objHandler = new SQLHandler();
                return objHandler.ExecuteAsScalar<string>("[dbo].[USP_SHORTURL_DECODE]", ParameterCollection);
            }
            catch 
            {
                throw;
            }
        }

    }
}
