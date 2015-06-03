using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SageFrame.Web.Utilities;
using SageFrame.Modules;
using SageFrame.Web;

namespace AspxCommerce.Core
{
    public class ModuleMultiplePageInfo
    {
        public List<ModuleSinglePageInfo> MultiplePageInfo { get; set; }
    }

    public class ModuleSinglePageInfo
    {
        public string FolderName { get; set; }
        public string FriendlyName { get; set; }
        public string PageName { get; set; }
        public string PageTitle { get; set; }
        public string Description { get; set; }        
        public List<PageControlInfo> PageControls { get; set; }
        public string HelpURL { get; set; }
        public string Version { get; set; }
        public bool SupportPartialRendering { get; set; }
    }

    public class PageControlInfo
    {
        public string ControlSource { get; set; }
        public string ControlType { get; set; }
    }

    public class CreateModulePackage : BaseAdministrationUserControl
    {
        public CreateModulePackage() { }

        public enum ControlType
        {
            View = 1,
            Edit = 2,
            Setting = 3
        }

        private bool IsPageExists(string pagename)
        {
            SQLHandler sqlH = new SQLHandler();
            var paramCol = new List<KeyValuePair<string, object>>();
            paramCol.Add(new KeyValuePair<string, object>("@PortalID", -1));
            paramCol.Add(new KeyValuePair<string, object>("@PageName", pagename));
            // paramCol.Add(new KeyValuePair<string, object>("@PageName", pagename));
            return sqlH.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckPaymentPage]", paramCol, "@IsExist");

        }

        private int checkControlType(string controlType)
        {
            int returnValue = 0;
            switch (controlType)
            {
                case "View":
                    returnValue = (int)ControlType.View;
                    break;
                case "Edit":
                    returnValue = (int)ControlType.Edit;
                    break;
                case "Setting":
                    returnValue = (int)ControlType.Setting;
                    break;
                default:
                    returnValue = 0;
                    break;
            }
            return returnValue;
        }

        public void CreateSinglePagesModulePackage(ModuleSinglePageInfo pageObj)
        {
            ModuleController moduleCtr = new ModuleController();
            // PaymentGateWayModuleInfo module = new PaymentGateWayModuleInfo();
            ModuleInfo module = new ModuleInfo();
            SQLHandler sqlH = new SQLHandler();
            Int32? newModuleID = 0;
            Int32? newModuleDefID = 0;
            Int32? newPortalmoduleID = 0;

            if (!IsPageExists(pageObj.PageName))
            {
                try
                {
                    #region "Module Creation Logic"

                    // add into module table
                    ModuleInfo moduleObj = new ModuleInfo();
                    moduleObj.ModuleName = "AspxCommerce." + pageObj.FriendlyName;
                    moduleObj.Name = pageObj.FriendlyName;
                    moduleObj.PackageType = "Module";
                    moduleObj.Owner = "AspxCommerce";
                    moduleObj.Organization = "";
                    moduleObj.URL = "";
                    moduleObj.Email = "";
                    moduleObj.ReleaseNotes = "";
                    moduleObj.FriendlyName = pageObj.FriendlyName;
                    moduleObj.Description = pageObj.Description;
                    moduleObj.Version = pageObj.Version;
                    moduleObj.isPremium = true;
                    moduleObj.BusinessControllerClass = "";
                    moduleObj.FolderName = pageObj.FolderName;
                    moduleObj.supportedFeatures = 0;
                    moduleObj.CompatibleVersions = "";
                    moduleObj.dependencies = "";
                    moduleObj.permissions = "";
              

                        int[] outputValue;
                        outputValue = moduleCtr.AddModules(moduleObj, false, 0, true, DateTime.Now, GetPortalID, GetUsername);
                        moduleObj.ModuleID = outputValue[0];
                        moduleObj.ModuleDefID = outputValue[1];
                        newModuleID = moduleObj.ModuleID;
                        newModuleDefID = moduleObj.ModuleDefID;


                        //insert into ProtalModule table

                        newPortalmoduleID = moduleCtr.AddPortalModules(GetPortalID, newModuleID, true, DateTime.Now, GetUsername);
                    #endregion

                        //install permission for the installed module in ModuleDefPermission table with ModuleDefID and PermissionID
                        //int controlType = 0;
                        //controlType = checkControlType(pageObj.ControlType);
                        string IconFile = "";


                        foreach (var item in pageObj.PageControls)
                        {
                            int controlType = 0;
                            controlType = checkControlType(item.ControlType);
                            //add into module control table
                            moduleCtr.AddModuleCoontrols(newModuleDefID, pageObj.PageName + item.ControlType,
                                                               pageObj.PageTitle + item.ControlType, item.ControlSource,
                                                               IconFile, controlType, 0, pageObj.HelpURL,
                                                               pageObj.SupportPartialRendering, true,
                                                               DateTime.Now,
                                                               GetPortalID, GetUsername);
                        }
                   

                    //sp_ModuleDefPermissionAdd
                    string ModuleDefPermissionID;
                    List<KeyValuePair<string, object>> paramDef = new List<KeyValuePair<string, object>>();
                    paramDef.Add(new KeyValuePair<string, object>("@ModuleDefID", newModuleDefID));
                    paramDef.Add(new KeyValuePair<string, object>("@PortalModuleID", newPortalmoduleID));
                    paramDef.Add(new KeyValuePair<string, object>("@PermissionID", 1));
                    paramDef.Add(new KeyValuePair<string, object>("@IsActive", true));
                    paramDef.Add(new KeyValuePair<string, object>("@AddedOn", DateTime.Now));
                    paramDef.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                    paramDef.Add(new KeyValuePair<string, object>("@AddedBy", GetUsername));
                    ModuleDefPermissionID = sqlH.ExecuteNonQueryAsGivenType<string>("[dbo].[sp_ModuleDefPermissionAdd]", paramDef, "@ModuleDefPermissionID");

                    //ModuleDefPermissionID
                    List<KeyValuePair<string, object>> paramPage = new List<KeyValuePair<string, object>>();
                    paramPage.Add(new KeyValuePair<string, object>("@ModuleDefID", newModuleDefID));
                    paramPage.Add(new KeyValuePair<string, object>("@PageName", pageObj.PageName));
                    paramPage.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                    paramPage.Add(new KeyValuePair<string, object>("@ModuleDefPermissionID", int.Parse(ModuleDefPermissionID)));
                    sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_CreatePageModulePackage]", paramPage);

                }
                catch (Exception ex)
                {

                    ProcessException(ex);
                }
            }
        }

        public void CreateMultiplePagesModulePackage(List<ModuleSinglePageInfo> multiPageObj)
        {
            ModuleController moduleCtr = new ModuleController();
            ModuleInfo module = new ModuleInfo();
            SQLHandler sqlH = new SQLHandler();
            Int32? newModuleID = 0;
            Int32? newModuleDefID = 0;
            Int32? newPortalmoduleID = 0;

            foreach (ModuleSinglePageInfo pageObj in multiPageObj)
            {
                if (!IsPageExists(pageObj.PageName))
                {
                    try
                    {
                        #region "Module Creation Logic"

                        // add into module table
                        ModuleInfo moduleObj = new ModuleInfo();
                        moduleObj.ModuleName = "AspxCommerce." + pageObj.FriendlyName;
                        moduleObj.Name = pageObj.FriendlyName;
                        moduleObj.PackageType = "Module";
                        moduleObj.Owner = "AspxCommerce";
                        moduleObj.Organization = "";
                        moduleObj.URL = "";
                        moduleObj.Email = "";
                        moduleObj.ReleaseNotes = "";
                        moduleObj.FriendlyName = pageObj.FriendlyName;
                        moduleObj.Description = pageObj.Description;
                        moduleObj.Version = pageObj.Version;
                        moduleObj.isPremium = true;
                        moduleObj.BusinessControllerClass = "";
                        moduleObj.FolderName = pageObj.FolderName;
                        moduleObj.supportedFeatures = 0;
                        moduleObj.CompatibleVersions = "";
                        moduleObj.dependencies = "";
                        moduleObj.permissions = "";

                        int[] outputValue;
                        outputValue = moduleCtr.AddModules(moduleObj, false, 0, true, DateTime.Now, GetPortalID, GetUsername);
                        moduleObj.ModuleID = outputValue[0];
                        moduleObj.ModuleDefID = outputValue[1];
                        newModuleID = moduleObj.ModuleID;
                        newModuleDefID = moduleObj.ModuleDefID;


                        //insert into ProtalModule table

                        newPortalmoduleID = moduleCtr.AddPortalModules(GetPortalID, newModuleID, true, DateTime.Now, GetUsername);
                        #endregion

                        string IconFile = "";
                        //install permission for the installed module in ModuleDefPermission table with ModuleDefID and PermissionID
                        foreach (var item in pageObj.PageControls)
                        {
                            int controlType = 0;
                            controlType = checkControlType(item.ControlType);
                            //add into module control table
                            moduleCtr.AddModuleCoontrols(newModuleDefID, pageObj.PageName + "View",
                                                               pageObj.PageTitle + "View", item.ControlSource,
                                                               IconFile, controlType, 0, pageObj.HelpURL,
                                                               pageObj.SupportPartialRendering, true,
                                                               DateTime.Now,
                                                               GetPortalID, GetUsername);
                        }

                        //sp_ModuleDefPermissionAdd
                        string ModuleDefPermissionID;
                        List<KeyValuePair<string, object>> paramDef = new List<KeyValuePair<string, object>>();
                        paramDef.Add(new KeyValuePair<string, object>("@ModuleDefID", newModuleDefID));
                        paramDef.Add(new KeyValuePair<string, object>("@PortalModuleID", newPortalmoduleID));
                        paramDef.Add(new KeyValuePair<string, object>("@PermissionID", 1));
                        paramDef.Add(new KeyValuePair<string, object>("@IsActive", true));
                        paramDef.Add(new KeyValuePair<string, object>("@AddedOn", DateTime.Now));
                        paramDef.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                        paramDef.Add(new KeyValuePair<string, object>("@AddedBy", GetUsername));
                        ModuleDefPermissionID = sqlH.ExecuteNonQueryAsGivenType<string>("[dbo].[sp_ModuleDefPermissionAdd]", paramDef, "@ModuleDefPermissionID");

                        //ModuleDefPermissionID
                        List<KeyValuePair<string, object>> paramPage = new List<KeyValuePair<string, object>>();
                        paramPage.Add(new KeyValuePair<string, object>("@ModuleDefID", newModuleDefID));
                        paramPage.Add(new KeyValuePair<string, object>("@PageName", pageObj.PageName));
                        paramPage.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                        paramPage.Add(new KeyValuePair<string, object>("@ModuleDefPermissionID", int.Parse(ModuleDefPermissionID)));
                        sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_CreatePaymentGatewayPage]", paramPage);

                    }
                    catch (Exception ex)
                    {

                        ProcessException(ex);
                    }
                }
            }
        }

        public void DeleteSinglePageModulePackage(ModuleSinglePageInfo pageObj)
        {
            try
            {
                List<KeyValuePair<string, object>> paramColl = new List<KeyValuePair<string, object>>();
                paramColl.Add(new KeyValuePair<string, object>("@FriendlyName", pageObj.FriendlyName));
                paramColl.Add(new KeyValuePair<string, object>("@PageName", pageObj.PageName));
                paramColl.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeletePageModulePackage]", paramColl);

            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        public void DeleteMultiplePageModulePackage(List<ModuleSinglePageInfo> multiPageObj)
        {

            try
            {
                foreach (ModuleSinglePageInfo pageObj in multiPageObj)
                {
                    List<KeyValuePair<string, object>> paramColl = new List<KeyValuePair<string, object>>();
                    paramColl.Add(new KeyValuePair<string, object>("@FriendlyName", pageObj.FriendlyName));
                    paramColl.Add(new KeyValuePair<string, object>("@PageName", pageObj.PageName));
                    paramColl.Add(new KeyValuePair<string, object>("@PortalID", GetPortalID));
                    SQLHandler sqlH = new SQLHandler();
                    sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_DeletePageModulePackage]", paramColl);
                }

            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
    }
}
