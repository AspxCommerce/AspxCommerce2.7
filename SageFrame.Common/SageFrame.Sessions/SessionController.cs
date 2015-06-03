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

namespace SageFrame.Common
{
    public class SessionController
    {
        /// <summary>
        /// Initializes a new instance SessionController 
        /// </summary>
        public SessionController()
        {
        }
        /// <summary>
        /// Set session value.
        /// </summary>
        /// <param name="sessionKey">sessionKey</param>
        /// <param name="objSessionValue">objSessionValue</param>
        public void SetSession(string sessionKey, object objSessionValue)
        {
            HttpContext.Current.Session[sessionKey] = objSessionValue;
        }
        /// <summary>
        /// Get session value.
        /// </summary>
        /// <typeparam name="T">Type of the object implementing.</typeparam>
        /// <param name="sessionKey">sessionKey</param>
        /// <returns>Session value.</returns>
        public T GetSessionValue<T>(string sessionKey)
        {
            return (T)HttpContext.Current.Session[sessionKey];
        }


        public static string GetSessionValue(string dictionaryName, string key)
        {
            string value = string.Empty;
            try
            {
                if (HttpContext.Current.Session[dictionaryName] != null)
                {
                    Dictionary<string, string> form = (Dictionary<string, string>)HttpContext.Current.Session[dictionaryName];
                    if (form.ContainsKey(key))
                    {
                        if (!string.IsNullOrEmpty(key))
                        {
                            value = form[key];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
                //Logger.Error("{0}: Error while checking Session value from Dictionary", ex, "SessionDictionary");
            }
            return value;
        }

        public static void SetSessionValue(string dictionaryName, string key, string value)
        {
            if (!String.IsNullOrEmpty(key))
            {
                try
                {
                    if (HttpContext.Current.Session[dictionaryName] != null)
                    {
                        Dictionary<string, string> form = (Dictionary<string, string>)HttpContext.Current.Session[dictionaryName];
                        if (form.ContainsKey(key))
                        {
                            form[key] = value;
                        }
                        else
                        {
                            form.Add(key, value);
                        }
                    }
                    else
                    {
                        Dictionary<string, string> sessionDict = new Dictionary<string, string>();
                        HttpContext.Current.Session.Add(dictionaryName, sessionDict);
                        if (sessionDict.ContainsKey(key))
                        {
                            sessionDict[key] = value;
                        }
                        else
                        {
                            sessionDict.Add(key, value);
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                    //Logger.Error("{0}: Error while checking Session value from Dictionary", ex, "SessionDictionary");
                }
            }
        }

        public static void RemoveSessionValue(string dictionaryName, string key)
        {
            if (!String.IsNullOrEmpty(key))
            {
                try
                {
                    if (HttpContext.Current.Session[dictionaryName] != null)
                    {
                        Dictionary<string, string> form = (Dictionary<string, string>)HttpContext.Current.Session[dictionaryName];
                        form.Remove(key); // no error if key didn't exist
                    }
                }
                catch 
                {
                    // Logger.Error("{0}: Error while checking Session value from Dictionary", ex, "SessionDictionary");
                }
            }
        }

    }
}
