using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SageFrame.Logo
{     
    /// <summary>
    /// Business logic class for Logo.
    /// </summary>
    public class LogoController
    {     
        /// <summary>
        /// Saves logo data.
        /// </summary>
        /// <param name="logoText">logoText</param>
        /// <param name="logoPath">logoPath</param>
        /// <param name="userModuleID">userModuleID</param>
        /// <param name="portalID">portalID</param>
        /// <param name="Slogan">Slogan</param>
        /// <param name="URL">URL</param>
        /// <param name="CultureCode">CultureCode</param>
        public void SaveLogoSettings(string logoText, string logoPath, int userModuleID, int portalID, string Slogan, string URL, string CultureCode)
        {
            try
            {
                LogoDataProvider objProvider = new LogoDataProvider();
                objProvider.SaveLogoSettings(logoText, logoPath, userModuleID, portalID, Slogan, URL, CultureCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Returns LogoEntity object for a given portalid with provided culturecode.
        /// </summary>
        /// <param name="userModuleID">userModuleID</param>
        /// <param name="portalID">portalID</param>
        /// <param name="CultureCode">CultureCode</param>
        /// <returns>LogoEntity object for a given portalid with provided culturecode</returns>
        public LogoEntity GetLogoData(int userModuleID, int portalID, string CultureCode)
        {
            try
            {
                LogoEntity objLogoEntity = new LogoEntity();
                if (HttpRuntime.Cache["LogoImage_" + CultureCode + "_" + userModuleID.ToString()] != null)
                {
                    objLogoEntity = HttpRuntime.Cache["LogoImage_" + CultureCode + "_" + userModuleID.ToString()] as LogoEntity;
                }
                else
                {
                    LogoDataProvider objProvider = new LogoDataProvider();
                    objLogoEntity = objProvider.GetLogoData(userModuleID, portalID, CultureCode);
                    if (objLogoEntity != null)
                    {
                        HttpRuntime.Cache["LogoImage_" + CultureCode + "_" + userModuleID.ToString()] = objLogoEntity;
                    }
                }
                return objLogoEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
