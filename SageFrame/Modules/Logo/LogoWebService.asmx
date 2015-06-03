<%@ WebService Language="C#" Class="LogoWebService" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using System.IO;
using SageFrame.Logo;
using SageFrame.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class LogoWebService : AuthenticateService
{

    [WebMethod]
    public void SaveLogoSettings(string logoText, string logoPath, int userModuleID, int portalID, string Slogan, string URL, string CultureCode, string UserName, string secureToken)
    {
        try
        {
            if (IsPostAuthenticated(portalID, userModuleID, UserName, secureToken))
            {
                LogoController objController = new LogoController();
                objController.SaveLogoSettings(logoText, logoPath, userModuleID, portalID, Slogan, URL, CultureCode);
                HttpRuntime.Cache.Remove("LogoImage_" + CultureCode + "_" + userModuleID.ToString());
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    [WebMethod]
    public LogoEntity GetLogoData(int userModuleID, int portalID, string CultureCode, string UserName, string secureToken)
    {
        try
        {
            if (IsPostAuthenticated(portalID, userModuleID, UserName, secureToken))
            {
                LogoEntity objLogEntity = new LogoEntity();
                if (HttpRuntime.Cache["LogoImage_" + CultureCode + "_" + userModuleID.ToString()] != null)
                {
                    objLogEntity = HttpRuntime.Cache["LogoImage_" + CultureCode + "_" + userModuleID.ToString()] as LogoEntity;
                }
                else
                {
                    LogoController objController = new LogoController();
                    objLogEntity = objController.GetLogoData(userModuleID, portalID, CultureCode);
                    if (objLogEntity != null)
                    {
                        HttpRuntime.Cache["LogoImage_" + CultureCode + "_" + userModuleID.ToString()] = objLogEntity;
                    }
                }
                return objLogEntity;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void DeleteIcon(string IconPath, int userModuleID, string CultureCode, int PortalID, string UserName, string secureToken)
    {
        try
        {
            if (IsPostAuthenticated(PortalID, userModuleID, UserName, secureToken))
            {
                string filepath = SageFrame.Templating.Utils.GetAbsolutePath(string.Format("Modules/Logo/image/{0}", IconPath));
                if (File.Exists(filepath))
                {
                    File.SetAttributes(filepath, System.IO.FileAttributes.Normal);
                    File.Delete(filepath);
                }
                HttpRuntime.Cache.Remove("LogoImage_" + CultureCode + "_" + userModuleID.ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}