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
using System.Globalization;
using SageFrame.Localization.Info;
#endregion

namespace SageFrame.Localization
{
    /// <summary>
    /// Business logic class for Localization.
    /// </summary>
    public class LocaleController
    {
        /// <summary>
        /// Obtains culture.
        /// </summary>
        /// <returns> List of Language class.</returns>
        public static List<Language> GetCultures()
        {
            List<Language> lstLocales = new List<Language>();
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                Language obj = new Language();
                obj.LanguageCode = ci.Name;
                obj.LanguageName = ci.EnglishName;
                obj.NativeName = ci.NativeName;
                lstLocales.Add(obj);
            }
            return lstLocales;
        }
        /// <summary>
        /// Adds local names to list.
        /// </summary>
        /// <param name="lstEnglishNames">List of object of Language class.</param>
        /// <returns>List of Language class containing local name.</returns>
        public static List<Language> AddNativeNamesToList(List<Language> lstEnglishNames)
        {
            List<Language> lstWithNativeNames = GetCultures();
            foreach (Language obj in lstEnglishNames)
            {
                int index = lstWithNativeNames.FindIndex(
                    delegate(Language newObj)
                    {
                        return (newObj.LanguageCode == obj.LanguageCode);
                    }
                  );
                if (index > -1)
                {
                    obj.NativeName = lstWithNativeNames[index].NativeName;
                }
            }
            return lstEnglishNames;
        }
        /// <summary>
        /// Obtains language name from code.
        /// </summary>
        /// <param name="code">code</param>
        /// <returns>Language name.</returns>
        public static string GetLanguageNameFromCode(string code)
        {
            string langugeFullName = code;
            List<Language> lstLanguage = GetCultures();
            int index = lstLanguage.FindIndex(
                delegate(Language obj)
                {
                    return (obj.LanguageCode == code);
                }
                );
            if (index > -1)
            {
                langugeFullName = lstLanguage[index].LanguageName + "(" + code + ")";
            }
            return langugeFullName;
        }
        /// <summary>
        /// Obtain code from language name.
        /// </summary>
        /// <param name="languageName">languageName</param>
        /// <returns>Return code.</returns>
        public static string GetCodeFromLanguageName(string languageName)
        {
            string code = languageName;
            List<Language> lstLanguage = GetCultures();
            int index = lstLanguage.FindIndex(
                delegate(Language obj)
                {
                    return (obj.LanguageName ==languageName);
                }
                );
            if (index > -1)
            {
                code = lstLanguage[index].LanguageCode;
            }
            return code;
        }
        /// <summary>
        /// Obtains language name only.
        /// </summary>
        /// <param name="code">code</param>
        /// <returns> Language name.</returns>
        public static string GetLanguageNameOnly(string code)
        {
            string langugeFullName = code;
            List<Language> lstLanguage = GetCultures();
            int index = lstLanguage.FindIndex(
                delegate(Language obj)
                {
                    return (obj.LanguageCode == code);
                }
                );
            if (index > -1)
            {
                langugeFullName = lstLanguage[index].LanguageName;
            }
            return langugeFullName;
        }
        /// <summary>
        /// Adds language switch settings for given lstKeyValue,UserModuleID and PortalID.
        /// </summary>
        /// <param name="lstKeyValue">List of object of LanguageSwitchKeyValue class.</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <param name="PortalID">PortalID</param>
        public static void AddLanguageSwitchSettings(List<LanguageSwitchKeyValue> lstKeyValue,int UserModuleID,int PortalID)
        {
            try
            {
                LocalizationSqlDataProvider.AddLanguageSwitchSettings(lstKeyValue,UserModuleID,PortalID);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Obtains language switch setting.
        /// </summary>
        /// <param name="portalId">portalId</param>
        /// <param name="UserModuleID">UserModuleID</param>
        /// <returns>List of LanguageSwitchKeyValue class containing SettingKey and SettingValue.</returns>
        public static List<LanguageSwitchKeyValue> GetLanguageSwitchSettings(int portalId,int UserModuleID)
        {
            try
            {
                return (LocalizationSqlDataProvider.GetLanguageSwitchSettings(portalId, UserModuleID));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Obtains local page name.
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="CultureCode">CultureCode</param>
        /// <returns>List of LocalPageInfo class containing pageid and pagename.</returns>
        public static List<LocalPageInfo> GetLocalPageName(int PortalID, string CultureCode)
        {
            try
            {
                return (LocalizationSqlDataProvider.GetLocalPageName(PortalID, CultureCode));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtains local module title
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <param name="CultureCode">CultureCode</param>
        /// <returns>List of LocalModuleInfo class containing user module id and user module title.</returns>
        public static List<LocalModuleInfo> GetLocalModuleTitle(int PortalID, string CultureCode)
        {
            try
            {
                return (LocalizationSqlDataProvider.GetLocalModuleTitle(PortalID, CultureCode));
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Adds or updates local page name.
        /// </summary>
        /// <param name="lstLocalPage">List of object of LocalPageInfo class.</param>
        public static void AddUpdateLocalPage(List<LocalPageInfo> lstLocalPage)
        {
            try
            {
                LocalizationSqlDataProvider.AddUpdateLocalPage(lstLocalPage);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Adds or updates local module title.
        /// </summary>
        /// <param name="lstLocalPage">List of object of LocalModuleInfo class.</param>
        public static void AddUpdateLocalModuleTitle(List<LocalModuleInfo> lstLocalPage)
        {
            try
            {
                LocalizationSqlDataProvider.AddUpdateLocalModuleTitle(lstLocalPage);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Deletes language.
        /// </summary>
        /// <param name="code">code</param>
        public void DeleteLanguage(string code)
        {
            try
            {
                LocalizationSqlDataProvider.DeleteLanguage(code);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
