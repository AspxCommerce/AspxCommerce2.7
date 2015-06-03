<%@ WebService Language="C#" Class="ModuleManagerWebService" %>
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using SageFrame.ModuleManager;
using SageFrame.Templating.xmlparser;
using SageFrame.Templating;
using System.IO;
using SageFrame.Security.Entities;
using SageFrame.Security;
using SageFrame.ModuleManager.DataProvider;
using SageFrame.ModuleManager.Controller;
using System.Collections.Generic;

/// <summary>
/// Summary description for ModuleManagerWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ModuleManagerWebService : SageFrame.Services.AuthenticateService
{

    public ModuleManagerWebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<LayoutMgrInfo> GetAllModules(int portalID, string userName, int userModuleID, string secureToken)
    {
        List<LayoutMgrInfo> lminfo = new List<LayoutMgrInfo>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            lminfo = LayoutMgrDataProvider.GetModules(portalID);
        }
        return (lminfo);
    }
    [WebMethod]
    public List<LayoutMgrInfo> GetModuleInformation(string FriendlyName, int portalID, string userName, int userModuleID, string secureToken)
    {
        List<LayoutMgrInfo> lminfo = new List<LayoutMgrInfo>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            lminfo = LayoutMgrDataProvider.GetModuleInformation(FriendlyName);
        }
        return (lminfo);
    }

    [WebMethod]
    public int AddModuleOrder(string ModuleOrder, string ModuleID, string ModuleName, string PaneName, int portalID, string userName, int userModuleID, string secureToken)
    {
        try
        {
            int moduleOrder = 0;
            if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
            {
                LayoutMgrInfo obj = new LayoutMgrInfo();
                obj.ModuleOrder = ModuleOrder;
                obj.PortelID = portalID;
                obj.ModuleID = ModuleID;
                obj.ModuleName = ModuleName;
                obj.PaneName = PaneName;
                obj.UserModuleID = userModuleID;
                moduleOrder = LayoutMgrDataProvider.AddLayOutMgr(obj);
            }
            return moduleOrder;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string LoadActiveLayout(string PageName, string TemplateName, int portalID, string userName, int userModuleID, string secureToken)
    {
        try
        {
            string layout = string.Empty;
            if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
            {
                BlockParser.CheckFilePath();
                string activeLayout = PresetHelper.LoadActivePagePreset(TemplateName, PageName).ActiveLayout;
                List<XmlTag> lstXmlTags = new List<XmlTag>();
                XmlParser parser = new XmlParser();
                string templatePath = Decide.IsTemplateDefault(TemplateName) ? Utils.GetTemplatePath_Default(TemplateName) : Utils.GetTemplatePath(TemplateName);
                if (!Directory.Exists(templatePath))
                {
                    templatePath = Utils.GetTemplatePath_Default(TemplateName);
                }
                string filePath = string.Format("{0}/layouts/{1}.xml", templatePath, activeLayout);
                lstXmlTags = parser.GetXmlTags(filePath, "layout/section");
                List<XmlTag> lstWrappers = parser.GetXmlTags(filePath, "layout/wrappers");
                ModulePaneGenerator wg = new ModulePaneGenerator();
                layout = wg.GenerateHTML(lstXmlTags, lstWrappers, 1);
            }
            return layout;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string GetActiveLayoutName(string templateName, string pageName, int portalID, string userName, int userModuleID, string secureToken)
    {
        string activeLayout = string.Empty;
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            activeLayout = PresetHelper.LoadActivePagePreset(templateName, pageName).ActiveLayout;
        }
        return activeLayout;
    }


    [WebMethod]
    public string LoadLayout(string activeLayout, string TemplateName, int portalID, string userName, int userModuleID, string secureToken)
    {
        try
        {
            string layout = string.Empty;
            if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
            {
                BlockParser.CheckFilePath();
                List<XmlTag> lstXmlTags = new List<XmlTag>();
                XmlParser parser = new XmlParser();
                string templatePath = Decide.IsTemplateDefault(TemplateName) ? Utils.GetTemplatePath_Default(TemplateName) : Utils.GetTemplatePath(TemplateName);
                if (!Directory.Exists(templatePath))
                {
                    templatePath = Utils.GetTemplatePath_Default(TemplateName);
                }
                string filePath = string.Format("{0}/layouts/{1}.xml", templatePath, activeLayout);
                lstXmlTags = parser.GetXmlTags(filePath, "layout/section");
                List<XmlTag> lstWrappers = parser.GetXmlTags(filePath, "layout/wrappers");
                ModulePaneGenerator wg = new ModulePaneGenerator();
                layout = wg.GenerateHTML(lstXmlTags, lstWrappers, 1);
            }
            return layout;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public void SavePresets(string activelayout, string pageName, string templateName, int portalID, string userName, int userModuleID, string secureToken)
    {

        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {

            HttpRuntime.Cache.Remove(SageFrame.Common.CacheKeys.SageFrameJs);
            HttpRuntime.Cache.Remove(SageFrame.Common.CacheKeys.SageFrameCss);
            string optimized_path = Server.MapPath(SageFrame.Common.SageFrameConstants.OptimizedResourcePath);
            SageFrame.Common.IOHelper.DeleteDirectoryFiles(optimized_path, ".js,.css");
            if (File.Exists(Server.MapPath(SageFrame.Common.SageFrameConstants.OptimizedCssMap)))
            {
                SageFrame.Web.XmlHelper.DeleteNodes(Server.MapPath(SageFrame.Common.SageFrameConstants.OptimizedCssMap), "resourcemaps/resourcemap");
            }
            if (File.Exists(Server.MapPath(SageFrame.Common.SageFrameConstants.OptimizedJsMap)))
            {
                SageFrame.Web.XmlHelper.DeleteNodes(Server.MapPath(SageFrame.Common.SageFrameConstants.OptimizedJsMap), "resourcemap/resourcemap");
            }
            PresetInfo preset = new PresetInfo();
            preset = PresetHelper.LoadActivePagePreset(templateName, pageName);
            preset.ActiveLayout = activelayout;
            List<KeyValue> lstLayouts = preset.lstLayouts;
            SageFrame.Web.SageFrameConfig sfConfig = new SageFrame.Web.SageFrameConfig();
            pageName = Path.GetFileNameWithoutExtension(pageName);
            pageName = pageName.ToLower().Equals("default") ? sfConfig.GetSettingsByKey(SageFrame.Web.SageFrameSettingKeys.PortalDefaultPage) : pageName;
            bool isNewLayout = false;
            int oldPageCount = 0;
            bool isNewPage = false;
            bool deleteRepeat = false;
            bool duplicateLayout = false;
            List<string> pageList = new List<string>();
            foreach (KeyValue kvp in lstLayouts)
            {
                if (kvp.Key == preset.ActiveLayout)
                {
                    duplicateLayout = true;
                }
                string[] pages = kvp.Value.Split(',');
                pageList.Add(string.Join(",", pages));
                if (pages.Count() == 1 && pages.Contains(pageName)) // for single pagename and if page = currentpageName
                {
                    kvp.Key = preset.ActiveLayout;
                }
                else if (pages.Count() > 1 && pages.Contains(pageName))// for multiple pagename and if page = currentpageName
                {
                    isNewLayout = true;                             //its because we have to insert another layout
                    List<string> lstnewpage = new List<string>();
                    foreach (string page in pages)
                    {
                        if (page.ToLower() != pageName.ToLower())
                        {
                            lstnewpage.Add(page);
                        }
                    }
                    kvp.Value = string.Join(",", lstnewpage.ToArray());
                    pageList.Add(kvp.Value);
                }
                else
                {
                    oldPageCount++;
                }
                if (kvp.Value == "All" && kvp.Key == preset.ActiveLayout)
                {
                    deleteRepeat = true;
                }
            }
            if (lstLayouts.Count == oldPageCount)
            {
                isNewPage = true;
            }
            List<KeyValue> lstNewLayouts = new List<KeyValue>();
            if (isNewPage)
            {
                bool isAppended = false;
                foreach (KeyValue kvp in lstLayouts)
                {
                    if (kvp.Key == preset.ActiveLayout)
                    {
                        if (kvp.Value.ToLower() != "all")
                        {
                            kvp.Value += "," + pageName;
                        }
                        isAppended = true;
                    }
                    lstNewLayouts.Add(new KeyValue(kvp.Key, kvp.Value));
                }
                if (!isAppended)
                {
                    lstNewLayouts.Add(new KeyValue(preset.ActiveLayout, pageName));
                }
                lstLayouts = lstNewLayouts;
            }
            else if (isNewLayout)
            {
                bool isAppended = false;
                bool isAll = false;
                foreach (KeyValue kvp in lstLayouts)
                {
                    if (kvp.Key == preset.ActiveLayout)
                    {
                        if (kvp.Value.ToLower() != "all")
                        {
                            kvp.Value += "," + pageName;
                            isAll = true;
                        }
                        isAppended = true;
                    }
                    lstNewLayouts.Add(new KeyValue(kvp.Key, kvp.Value));
                }
                if (!isAppended && !isAll)
                {
                    lstNewLayouts.Add(new KeyValue(preset.ActiveLayout, pageName));
                }
                lstLayouts = lstNewLayouts;
            }
            else if (deleteRepeat)
            {
                foreach (KeyValue kvp in lstLayouts)
                {
                    if (kvp.Value.ToLower() != pageName.ToLower())
                    {
                        lstNewLayouts.Add(new KeyValue(kvp.Key, kvp.Value));
                    }
                }
                lstLayouts = lstNewLayouts;
            }
            else if (duplicateLayout)
            {
                string key = preset.ActiveLayout;
                List<string> pages = new List<string>();
                foreach (KeyValue kvp in lstLayouts)
                {
                    if (kvp.Key.ToLower() != preset.ActiveLayout.ToLower())
                    {
                        lstNewLayouts.Add(new KeyValue(kvp.Key, kvp.Value));
                    }
                    else
                    {
                        pages.Add(kvp.Value);
                    }
                }
                lstNewLayouts.Add(new KeyValue(key, string.Join(",", pages.ToArray())));
                lstLayouts = lstNewLayouts;
            }

            preset.lstLayouts = lstLayouts;
            string presetPath = Decide.IsTemplateDefault(templateName.Trim()) ? Utils.GetPresetPath_DefaultTemplate(templateName) : Utils.GetPresetPath(templateName);
            string pagepreset = presetPath + "/" + TemplateConstants.PagePresetFile;
            presetPath += "/" + "pagepreset.xml";
            SageFrame.Core.AppErazer.ClearSysHash(SageFrame.Framework.ApplicationKeys.ActivePagePreset + "_" + portalID);
            PresetHelper.WritePreset(presetPath, preset);
            SageFrame.Common.CacheHelper.Clear("PresetList");
        }
    }

    [WebMethod]
    public string LoadHandHeldLayout(string PageName, string TemplateName, int portalID, string userName, int userModuleID, string secureToken)
    {
        string layout = string.Empty;
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            BlockParser.CheckFilePath();
            List<XmlTag> lstXmlTags = new List<XmlTag>();
            XmlParser parser = new XmlParser();
            string templatePath = Decide.IsTemplateDefault(TemplateName) ? Utils.GetTemplatePath_Default(TemplateName) : Utils.GetTemplatePath(TemplateName);
            string filePath = templatePath + "/layouts/handheld.xml";
            filePath = File.Exists(filePath) ? filePath : string.Format("{0}/layouts/handheld.xml", Utils.GetTemplatePath_Default(TemplateName));
            lstXmlTags = parser.GetXmlTags(filePath, "layout/section");
            ModulePaneGenerator wg = new ModulePaneGenerator();
            layout = wg.GenerateHTML(lstXmlTags, lstXmlTags, 1);
        }
        return layout;
    }
    [WebMethod]
    public string LoadType3Layout(string PageName, string TemplateName, int portalID, string userName, int userModuleID, string secureToken)
    {
        string layout = string.Empty;
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            BlockParser.CheckFilePath();
            List<XmlTag> lstXmlTags = new List<XmlTag>();
            XmlParser parser = new XmlParser();
            string templatePath = Decide.IsTemplateDefault(TemplateName) ? Utils.GetTemplatePath_Default(TemplateName) : Utils.GetTemplatePath(TemplateName);
            string filePath = templatePath + "/layouts/devicetype3.xml";
            filePath = File.Exists(filePath) ? filePath : string.Format("{0}/layouts/devicetype3.xml", Utils.GetTemplatePath_Default(TemplateName));
            lstXmlTags = parser.GetXmlTags(filePath, "layout/section");
            ModulePaneGenerator wg = new ModulePaneGenerator();
            layout = wg.GenerateHTML(lstXmlTags, lstXmlTags, 1);
        }
        return layout;
    }

    [WebMethod]
    public List<RoleInfo> GetPortalRoles(int portalID, string userName, int userModuleID, string secureToken)
    {
        List<RoleInfo> objRole = new List<RoleInfo>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            RoleController _role = new RoleController();
            objRole = (_role.GetPortalRoles(portalID, 1, userName));
        }
        return objRole;

    }

    [WebMethod]
    public List<UserInfo> SearchUsers(string SearchText, int portalID, string userName, int userModuleID, string secureToken)
    {
        List<UserInfo> lstUsers = new List<UserInfo>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            MembershipController m = new MembershipController();
            lstUsers = m.SearchUsers("", SearchText, portalID, userName).UserList;
        }
        return lstUsers;
    }

    [WebMethod]
    public string AddUserModule(UserModuleInfo UserModule, int portalID, string userName, int userModuleID, string secureToken)
    {
        string moduleAdd = string.Empty;
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            moduleAdd = (ModuleController.AddUserModule(UserModule));
            ClearBundleCache();
        }
        return moduleAdd;
    }

    [WebMethod]
    public void UpdateUserModule(UserModuleInfo UserModule, int portalID, string userName, int userModuleID, string secureToken)
    {
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            ModuleController.UpdateUserModule(UserModule);
        }
    }

    [WebMethod]
    public List<UserModuleInfo> GetPageModules(int PageID, bool IsHandheld, int portalID, string userName, int userModuleID, string secureToken)
    {
        List<UserModuleInfo> objModuleInfo = new List<UserModuleInfo>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            objModuleInfo = ModuleController.GetPageModules(PageID, portalID, IsHandheld);
        }
        return objModuleInfo;
    }

    [WebMethod]
    public void DeleteUserModule(string DeletedBy, int portalID, string userName, int userModuleID, string secureToken)
    {
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            ModuleController.DeleteUserModule(userModuleID, portalID, DeletedBy);
            ClearBundleCache();
        }
    }

    [WebMethod]
    public void UpdatePageModules(List<PageModuleInfo> lstPageModules, int portalID, string userName, int userModuleID, string secureToken)
    {
        try
        {
            if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
            {
                ModuleController.UpdatePageModule(lstPageModules);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    [WebMethod]
    public UserModuleInfo GetUserModuleDetails(int portalID, string userName, int userModuleID, string secureToken)
    {
        try
        {
            UserModuleInfo objModuleInfo = new UserModuleInfo();
            if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
            {
                objModuleInfo = (ModuleController.GetUserModuleDetails(userModuleID, portalID));
            }
            return objModuleInfo;
        }
        catch (Exception)
        {
            throw;
        }
    }


    [WebMethod]
    public List<LayoutMgrInfo> GetAllGenralModules(int portalID, string userName, int userModuleID, string secureToken)
    {
        List<LayoutMgrInfo> lminfo = new List<LayoutMgrInfo>();

        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            lminfo = LayoutMgrDataProvider.GetModules(portalID);
        }
        return (lminfo);
    }
    [WebMethod]
    public List<LayoutMgrInfo> GetAllAdminModules(int portalID, string userName, int userModuleID, string secureToken)
    {
        List<LayoutMgrInfo> lminfo = new List<LayoutMgrInfo>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            lminfo = LayoutMgrDataProvider.GetAdminModules(portalID);
            if (portalID > 1)
            {
                var item = lminfo.First(x => x.FriendlyName == "Upgrader");
                lminfo.Remove(item);
            }
        }
        return (lminfo);
    }

    [WebMethod]
    public List<LayoutMgrInfo> GetSortModules(int flag, string isAdmin, int IncludePortalModules, int portalID, string userName, int userModuleID, string secureToken)
    {
        List<LayoutMgrInfo> objModules = new List<LayoutMgrInfo>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            bool IsAdmin = isAdmin == "0" ? false : true;
            objModules = LayoutMgrDataProvider.GetSortModules(flag, IsAdmin, portalID, IncludePortalModules);
        }
        return objModules;
    }
    [WebMethod]
    public List<LayoutMgrInfo> GetAllSearchAdminModules(string SearchText, bool IsAdmin, int portalID, string userName, int userModuleID, string secureToken)
    {
        List<LayoutMgrInfo> objModules = new List<LayoutMgrInfo>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            objModules = LayoutMgrDataProvider.SearchModules(SearchText, portalID, IsAdmin);
        }
        return objModules;
    }

    [WebMethod]
    public List<LayoutMgrInfo> GetAllSearchGenralModules(string SearchText, bool IsAdmin, int portalID, string userName, int userModuleID, string secureToken)
    {
        List<LayoutMgrInfo> objGeneralModules = new List<LayoutMgrInfo>();
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            objGeneralModules = LayoutMgrDataProvider.SearchModules(SearchText, portalID, IsAdmin);
        }
        return objGeneralModules;
    }
    [WebMethod]
    public string LoadAdminLayout(string PageName, string TemplateName, int portalID, string userName, int userModuleID, string secureToken)
    {
        string layout = string.Empty;
        if (IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))
        {
            BlockParser.CheckFilePath();
            List<XmlTag> lstXmlTags = new List<XmlTag>();
            XmlParser parser = new XmlParser();
            string templatePath = Utils.GetAdminTemplatePath();
            string filePath = templatePath + "Default/layouts/layout.xml";
            lstXmlTags = parser.GetXmlTags(filePath, "layout/section");
            ModulePaneGenerator wg = new ModulePaneGenerator();
            layout = wg.GenerateHTML(lstXmlTags, lstXmlTags, 1);
        }
        return layout;
    }

    public void ClearBundleCache()
    {
        Hashtable hst = new Hashtable();
        HttpRuntime.Cache[SageFrame.Common.CacheKeys.SageFrameJs] = hst;
        HttpRuntime.Cache[SageFrame.Common.CacheKeys.SageFrameCss] = hst;
    }
}