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
using SageFrame.Web.Utilities;
using System.Data;
#endregion

namespace SageFrame.ExtractTemplate
{
    /// <summary>
    /// Manipulates data related for extracting templates.
    /// </summary>
    public class ExtractTemplateDataProvider
    {

        /// <summary>
        /// Connects to database and returns list of ExtractInfo object containing the detail of Modules
        /// </summary>
        /// <param name="PaneName">Pane name.</param>
        /// <param name="portalID">Portal ID.</param>
        /// <returns>List of ExtractInfo object containing the detail of Modules</returns>
        public List<ExtractInfo> GetTemplateDetails(string PaneName, int portalID)
        {
            //getBasicMarkUp
            ExtractInfo tempExtractinfo = new ExtractInfo();
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@paneName", PaneName));
            ParamCollInput.Add(new KeyValuePair<string, object>("@portalID", portalID));
            List<ExtractInfo> lstTemplate = new List<ExtractInfo>();
            try
            {
                lstTemplate = sagesql.ExecuteAsList<ExtractInfo>("usp_Template_Extract ", ParamCollInput);

            }
            catch (Exception)
            {
                throw;
            }
            return lstTemplate;
        }

        /// <summary>
        /// Connects to database and returns template permission by usermoduelID. 
        /// </summary>
        /// <param name="userModuleID">User module ID.</param>
        /// <returns>List of template permissions.</returns>
        public List<TemplatePermission> GetTemplatePermission(string userModuleID)
        {
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> PageParamCollModule = new List<KeyValuePair<string, object>>();
            PageParamCollModule.Add(new KeyValuePair<string, object>("@userModuleID", userModuleID));
            List<TemplatePermission> lstpermission = new List<TemplatePermission>();
            try
            {
                lstpermission = sagesql.ExecuteAsList<TemplatePermission>("usp_Template_Permission ", PageParamCollModule);
            }
            catch (Exception)
            {
                throw;
            }
            return lstpermission;
        }

        /// <summary>
        /// Connects to database and returns HTML module details
        /// </summary>
        /// <param name="HtmlUserModuleID">HtmlUserModuleID</param>
        /// <returns>Dataset containg the details of html modules.</returns>
        public DataSet MakeHtmlDataSet(string HtmlUserModuleID)
        {
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@UserModuleID", HtmlUserModuleID));
            DataSet objDataSet = new DataSet();
            try
            {
                objDataSet = sagesql.ExecuteAsDataSet("usp_Template_ExtractModules", ParamCollInput);
            }
            catch (Exception)
            {
                throw;
            }
            return objDataSet;
        }

        /// <summary>
        /// Connects to database and returns menu details
        /// </summary>
        /// <param name="portalID">Portal ID</param>
        /// <param name="menuUserModuleID">Menu's usermodule ID.</param>
        /// <returns>Dataset containing menu details.</returns>
        public DataSet GetMenuDetail(int portalID, string menuUserModuleID)
        {
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
            ParamCollInput.Add(new KeyValuePair<string, object>("@portalID", portalID));
            ParamCollInput.Add(new KeyValuePair<string, object>("@userModuleID", menuUserModuleID));
            DataSet objDataSet = new DataSet();
            try
            {
                objDataSet = sagesql.ExecuteAsDataSet("usp_template_GetMenu", ParamCollInput);
            }
            catch (Exception)
            {
                throw;
            }
            return objDataSet;
        }

        /// <summary>
        /// Connects to database and returns List of page permission.
        /// </summary>
        /// <param name="pageID">Page ID.</param>
        /// <returns>List of page permission.</returns>
        public List<PagePermission> GetPagePermission(string pageID)
        {
            SQLHandler sagesql = new SQLHandler();
            List<KeyValuePair<string, object>> PageParamCollModule = new List<KeyValuePair<string, object>>();
            PageParamCollModule.Add(new KeyValuePair<string, object>("@pageID", pageID));

            List<PagePermission> objPagePermissionList = new List<PagePermission>();
            try
            {
                objPagePermissionList = sagesql.ExecuteAsList<PagePermission>("usp_Template_GetPagePermission ", PageParamCollModule);
                return objPagePermissionList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Connects to database and inserts template details.
        /// </summary>
        /// <param name="lstPageList">List of page extracted from XML.</param>
        /// <param name="objTemplateMenuall">List of menu extracted from XML.</param>
        /// <param name="portalID">Portal ID.</param>
        public void InsertTemplate(List<ExtractPageInfo> lstPageList, List<TemplateMenuAll> objTemplateMenuall, int portalID)
        {
            SQLHandler sagesql = new SQLHandler();
            List<ExtractPageInfo> objExtrtractPage = new List<ExtractPageInfo>();
            //menu
            List<int> extractedPageIDLst = new List<int>();
            List<int> newPageID = new List<int>();
            //parentID
            List<int> pageID = new List<int>();
            List<int> parentID = new List<int>();
            //showinallpages
            List<string> showinPagesList = new List<string>();
            List<int> newUserModuleIDLst = new List<int>();
            List<int> oldUserModuleIDList = new List<int>();

            foreach (ExtractPageInfo objPage in lstPageList)
            {
                string roleName = "";
                string allowAcess = "";
                string permissionID = "";
                string isActive = "";

                foreach (PagePermission objPagePermission in objPage.PagePermissionList)
                {
                    allowAcess += objPagePermission.AllowAcess + ",";
                    roleName += objPagePermission.RoleName + ",";
                    permissionID += objPagePermission.PermissionID + ",";
                    isActive += objPagePermission.IsActive + ",";
                }


                List<KeyValuePair<string, object>> PageParamColl = new List<KeyValuePair<string, object>>();
                PageParamColl.Add(new KeyValuePair<string, object>("@pageOrder", objPage.PageOrder));
                PageParamColl.Add(new KeyValuePair<string, object>("@isVisible", objPage.Isvisible));
                PageParamColl.Add(new KeyValuePair<string, object>("@level", objPage.Level));
                PageParamColl.Add(new KeyValuePair<string, object>("@portalID", portalID));
                PageParamColl.Add(new KeyValuePair<string, object>("@disableLink", objPage.DisableLink));
                PageParamColl.Add(new KeyValuePair<string, object>("@isSecure", objPage.IsSecure));
                PageParamColl.Add(new KeyValuePair<string, object>("@isActive", objPage.IsActive));
                PageParamColl.Add(new KeyValuePair<string, object>("@isShowInFooter", objPage.IsShowInFooter));
                PageParamColl.Add(new KeyValuePair<string, object>("@isRequiredPage", objPage.IsRequiredPage));
                PageParamColl.Add(new KeyValuePair<string, object>("@pageName", objPage.PageName));
                PageParamColl.Add(new KeyValuePair<string, object>("@iconFile", objPage.IconFile));
                PageParamColl.Add(new KeyValuePair<string, object>("@pageHeadText", objPage.PageHeadText));
                PageParamColl.Add(new KeyValuePair<string, object>("@description", objPage.Description));
                PageParamColl.Add(new KeyValuePair<string, object>("@keyWords", objPage.KeyWords));
                PageParamColl.Add(new KeyValuePair<string, object>("@url", objPage.Url));
                PageParamColl.Add(new KeyValuePair<string, object>("@tabPath", objPage.TabPath));
                PageParamColl.Add(new KeyValuePair<string, object>("@seoName", objPage.SEOName));
                PageParamColl.Add(new KeyValuePair<string, object>("@refreshInterval", objPage.RefreshInterval));
                PageParamColl.Add(new KeyValuePair<string, object>("@title", objPage.Title));

                PageParamColl.Add(new KeyValuePair<string, object>("@allowAcess", allowAcess));
                PageParamColl.Add(new KeyValuePair<string, object>("@roleName", roleName));
                PageParamColl.Add(new KeyValuePair<string, object>("@permissionID", permissionID));
                PageParamColl.Add(new KeyValuePair<string, object>("@isPermisssionActive", isActive));

                List<ExtractModuleInfo> lstpageInfo = new List<ExtractModuleInfo>();
                try
                {
                    ExtractUserModule pageInfo = new ExtractUserModule();
                    pageInfo = sagesql.ExecuteAsObject<ExtractUserModule>("usp_Template_InsertPage", PageParamColl);
                    if (objPage.ParentID > 0)
                    {
                        pageID.Add(pageInfo.PageID);
                        parentID.Add(objPage.ParentID);
                    }
                    extractedPageIDLst.Add(objPage.PageID);
                    newPageID.Add(pageInfo.PageID);
                    foreach (ExtractModuleInfo objModule in objPage.ModuleList)
                    {
                        string showInpages = "";
                        int newUserModuleID = 0;
                        objModule.ModuleDef.UserModule.PageID = pageInfo.PageID;
                        InsertModules(objModule, portalID, out showInpages, out newUserModuleID);
                        lstpageInfo.Add(objModule);
                        showinPagesList.Add(showInpages);
                        oldUserModuleIDList.Add(objModule.ModuleDef.UserModule.UserModuleId);
                        newUserModuleIDLst.Add(newUserModuleID);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                objExtrtractPage.Add(objPage);
            }

            // to change PageID in menu
            List<int> oldMenuItemID = new List<int>();
            List<int> oldMenuItemParentID = new List<int>();
            List<int> newMenuItemID = new List<int>();
            foreach (TemplateMenuAll objMenuAll in objTemplateMenuall)
            {
                string caption = "";
                string htmlContent = "";
                string imageIcon = "";
                string ismenuActive = "";
                string isVisible = "";
                string linkType = "";
                string linkUrl = "";
                string menuLevel = "";
                string menuOrder = "";
                string menuPageID = "";
                string title = "";
                string menuName = "";
                string settingKey = "";
                string settingValue = "";
                //string MenuItemParentID = "";
                int menuUserModuleID = 0;
                if (objMenuAll.LstTemplateSetting.Count != 0)
                {
                    foreach (TemplateMenu objMenu in objMenuAll.LstTemplateMenu)
                    {
                        int count = 0;
                        foreach (int i in extractedPageIDLst)
                        {
                            if (i == objMenu.PageID)
                            {
                                oldMenuItemID.Add(objMenu.MenuItemID);
                                oldMenuItemParentID.Add(objMenu.ParentID);

                                caption += objMenu.Caption + ",";
                                htmlContent += objMenu.HtmlContent + ",";
                                imageIcon += objMenu.ImageIcon + ",";
                                ismenuActive += objMenu.IsActive + ",";
                                isVisible += objMenu.Isvisible + ",";
                                linkType += objMenu.LinkType + ",";
                                linkUrl += objMenu.LinkURL + ",";
                                menuLevel += objMenu.MenuLevel + ",";
                                menuOrder += objMenu.MenuOrder + ",";
                                menuPageID += newPageID[count] + ",";
                                title += objMenu.Title + ",";
                                menuName = objMenu.MenuName;
                                //MenuItemParentID += ChangeMenuItemParentID(objMenu.ParentID, extractedPageIDLst, newPageID);
                                menuUserModuleID = objMenu.UserModuleID;
                            }
                            count++;
                        }
                    }
                    foreach (TemplateMenuSettingValue objSetting in objMenuAll.LstTemplateSetting)
                    {
                        settingKey += objSetting.SettingKey + ",";
                        settingValue += objSetting.SettingValue + ",";
                    }
                    settingKey = settingKey.Substring(0, settingKey.Length - 1);
                    settingValue = settingValue.Substring(0, settingValue.Length - 1);

                    menuUserModuleID = ChangeUserModuleID(newUserModuleIDLst, oldUserModuleIDList, menuUserModuleID);

                    DateTime dt = DateTime.Now;
                    menuName = menuName + "-" + dt.Year.ToString() + dt.Month.ToString() + dt.Second.ToString() + dt.Millisecond.ToString();

                    List<KeyValuePair<string, object>> PageParamColl = new List<KeyValuePair<string, object>>();
                    PageParamColl.Add(new KeyValuePair<string, object>("@caption", caption));
                    PageParamColl.Add(new KeyValuePair<string, object>("@htmlContent", htmlContent));
                    PageParamColl.Add(new KeyValuePair<string, object>("@imageIcon", imageIcon));
                    PageParamColl.Add(new KeyValuePair<string, object>("@ismenuActive", ismenuActive));
                    PageParamColl.Add(new KeyValuePair<string, object>("@isVisible", isVisible));
                    PageParamColl.Add(new KeyValuePair<string, object>("@linkType", linkType));
                    PageParamColl.Add(new KeyValuePair<string, object>("@linkUrl", linkUrl));
                    PageParamColl.Add(new KeyValuePair<string, object>("@menuLevel", menuLevel));
                    PageParamColl.Add(new KeyValuePair<string, object>("@menuOrder", menuOrder));
                    PageParamColl.Add(new KeyValuePair<string, object>("@pageID", menuPageID));
                    PageParamColl.Add(new KeyValuePair<string, object>("@title", title));
                    PageParamColl.Add(new KeyValuePair<string, object>("@menuName", menuName));
                    PageParamColl.Add(new KeyValuePair<string, object>("@settingKey", settingKey));
                    PageParamColl.Add(new KeyValuePair<string, object>("@settingValue", settingValue));
                    PageParamColl.Add(new KeyValuePair<string, object>("@portalID", portalID));
                    PageParamColl.Add(new KeyValuePair<string, object>("@userModuleID", menuUserModuleID));
                    List<ExtractModuleInfo> lstpageInfo = new List<ExtractModuleInfo>();
                    try
                    {
                        List<TemplateMenu> objmenuIDList = new List<TemplateMenu>();
                        objmenuIDList = sagesql.ExecuteAsList<TemplateMenu>("usp_Template_MenuUpdate", PageParamColl);
                        foreach (TemplateMenu t in objmenuIDList)
                        {
                            newMenuItemID.Add(t.MenuItemID);
                        }

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            //Update Menu ParentID
            string newMenuItemIDstring = "";
            string newMenuParentIDstring = "";            
            int menuParentIDCount = 0;
            foreach (int i in oldMenuItemParentID)
            {

                if (i > 0)
                {
                    int menuItemIDCount = 0;
                    foreach (int j in oldMenuItemID)
                    {
                        if (i == j)
                        {
                            newMenuItemIDstring += newMenuItemID[menuParentIDCount].ToString() + ",";
                            newMenuParentIDstring += newMenuItemID[menuItemIDCount].ToString() + ",";
                        }
                        menuItemIDCount++;
                    }
                }
                menuParentIDCount++;
            }
            if (newMenuItemIDstring.Length > 0)
            {
                List<KeyValuePair<string, object>> pageParamColl = new List<KeyValuePair<string, object>>();
                pageParamColl.Add(new KeyValuePair<string, object>("@newMenuItemIDstring", newMenuItemIDstring));
                pageParamColl.Add(new KeyValuePair<string, object>("@newMenuParentIDstring", newMenuParentIDstring));
                try
                {
                    sagesql.ExecuteNonQuery("usp_Template_ParentIDUpdate", pageParamColl);
                }
                catch (Exception)
                {
                    throw;

                }
            }

            // to change showIn pages
            string userModuleIDJoined = "";
            string showInPagesJoined = "";
            foreach (string pages in showinPagesList)
            {
                if (pages.Length > 0)
                {
                    string[] pagelist = pages.Split(',');
                    string newPageList = "";
                    foreach (string page in pagelist)
                    {
                        //Int16.Parse(Page)
                        int count = 0;
                        foreach (int i in extractedPageIDLst)
                        {
                            //string tempPage = "";
                            if (i == Int16.Parse(page))
                            {
                                newPageList += newPageID[count] + ",";
                            }
                            count++;
                        }
                    }
                    newPageList = newPageList.Substring(0, newPageList.Length - 1);
                    userModuleIDJoined += newUserModuleIDLst[showinPagesList.IndexOf(pages)].ToString() + ":";
                    showInPagesJoined += newPageList + ":";
                }
            }
            if (userModuleIDJoined != "")
            {
                userModuleIDJoined = userModuleIDJoined.Substring(0, userModuleIDJoined.Length - 1);
                showInPagesJoined = showInPagesJoined.Substring(0, showInPagesJoined.Length - 1);
            }

            string pageIDstring = "";
            string ParentIDstring = "";
            foreach (int i in parentID)
            {
                foreach (int j in extractedPageIDLst)
                {
                    if (i == j)
                    {
                        pageIDstring += pageID[parentID.IndexOf(i)] + ",";
                        ParentIDstring += newPageID[extractedPageIDLst.IndexOf(j)].ToString() + ",";
                    }
                }
            }
            if (parentID.Count > 0)
            {
                pageIDstring = pageIDstring.Substring(0, pageIDstring.Length - 1);
                ParentIDstring = ParentIDstring.Substring(0, ParentIDstring.Length - 1);
            }
            try
            {
                List<KeyValuePair<string, object>> PageParamColl = new List<KeyValuePair<string, object>>();
                PageParamColl.Add(new KeyValuePair<string, object>("@pageID", pageIDstring));
                PageParamColl.Add(new KeyValuePair<string, object>("@parentID", ParentIDstring));
                PageParamColl.Add(new KeyValuePair<string, object>("@userModuleID", userModuleIDJoined));
                PageParamColl.Add(new KeyValuePair<string, object>("@showInAllPages", showInPagesJoined));
                List<ExtractModuleInfo> lstpageInfo = new List<ExtractModuleInfo>();
                try
                {
                    ExtractUserModule pageInfo = new ExtractUserModule();
                    pageInfo = sagesql.ExecuteAsObject<ExtractUserModule>("usp_Template_PageIDUpdates", PageParamColl);
                }
                catch (Exception)
                {
                    throw;
                }

            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Changes the newly generated menu item.
        /// </summary>
        /// <param name="ParentID">Old menu parent ID to be change.</param>
        /// <param name="extractedPageIDLst">List of extracted page ID.</param>
        /// <param name="newPageID">List of new page ID.</param>
        /// <returns>Returns new menu parent ID.</returns>
        public int ChangeMenuItemParentID(int ParentID, List<int> extractedPageIDLst, List<int> newPageID)
        {

            foreach (int j in extractedPageIDLst)
            {
                if (ParentID == j)
                {
                    ParentID = newPageID[extractedPageIDLst.IndexOf(j)];
                }
            }
            return ParentID;
        }

        /// <summary>
        /// Changes old use rmodule ID with new user module ID.
        /// </summary>
        /// <param name="newUserModuleIDLst">List  of new userModuleID.</param>
        /// <param name="oldUserModuleIDList">List of old usermoduelID.</param>
        /// <param name="userModule">Old user module to be change.</param>
        /// <returns>Returns new usermodule ID.</returns>
        public int ChangeUserModuleID(List<int> newUserModuleIDLst, List<int> oldUserModuleIDList, int userModule)
        {
            foreach (int i in oldUserModuleIDList)
            {
                if (i == userModule)
                {
                    userModule = newUserModuleIDLst[oldUserModuleIDList.IndexOf(i)];
                }
            }
            return userModule;
        }

        /// <summary>
        /// Connects to database and inserts module to database, updates new usermoduleID and maps page with usermodule.
        /// </summary>
        /// <param name="objExtract">ExtractModuleInfo containing the module details.</param>
        /// <param name="portalID">Portal ID.</param>
        /// <param name="showInpages">Set true if the module is showin in many pages.</param>
        /// <param name="newUserModuleID">New usermodule ID.</param>
        public void InsertModules(ExtractModuleInfo objExtract, int portalID, out string showInpages, out int newUserModuleID)
        {
            string roleName = "";
            string allowAcess = "";
            string permissionID = "";

            foreach (TemplatePermission objPermission in objExtract.ModuleDef.UserModule.TemplatePermission)
            {
                allowAcess += objPermission.AllowAccess + ",";
                roleName += objPermission.RoleName + ",";
                permissionID += objPermission.PermissionID + ",";
            }
            SQLHandler sagesql = new SQLHandler();
            //objExtract.ModuleDef.UserModule.ShowInPages;
            List<KeyValuePair<string, object>> PageParamCollModule = new List<KeyValuePair<string, object>>();
            PageParamCollModule.Add(new KeyValuePair<string, object>("@pageID", objExtract.ModuleDef.UserModule.PageID));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@ModuleName", objExtract.ModuleName));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@FriendlyName", objExtract.FriendlyName));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@userModuleTitle", objExtract.ModuleDef.FriendlyName));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@paneName", objExtract.ModuleDef.UserModule.PaneName));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@allowAcess", allowAcess));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@roleName", roleName));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@permissionID", permissionID));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@portalID", portalID));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@allPages", objExtract.ModuleDef.UserModule.AllPages));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@inheritViewPermissions", objExtract.ModuleDef.UserModule.InheritViewPermissions));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@header", objExtract.ModuleDef.UserModule.Header));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@footer", objExtract.ModuleDef.UserModule.Footer));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@isActive", objExtract.ModuleDef.UserModule.IsActive));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@seoName", objExtract.ModuleDef.UserModule.SEOName));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@ShowInPages", objExtract.ModuleDef.UserModule.ShowInPages));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@IsHandheld", objExtract.ModuleDef.UserModule.IsHandheld));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@suffixClass", objExtract.ModuleDef.UserModule.SuffixClass));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@headerText", objExtract.ModuleDef.UserModule.HeaderText));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@showHeaderText", objExtract.ModuleDef.UserModule.ShowHeaderText));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@isInAdmin", objExtract.ModuleDef.UserModule.IsInAdmin));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@query", objExtract.ModuleDef.UserModule.Query));
            PageParamCollModule.Add(new KeyValuePair<string, object>("@level", objExtract.ModuleDef.UserModule.Level));

            ExtractUserModule objUserModule = new ExtractUserModule();
            try
            {
                objUserModule = sagesql.ExecuteAsObject<ExtractUserModule>("usp_Template_InsertModule ", PageParamCollModule);
                newUserModuleID = objUserModule.UserModuleId;
                showInpages = objExtract.ModuleDef.UserModule.ShowInPages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
