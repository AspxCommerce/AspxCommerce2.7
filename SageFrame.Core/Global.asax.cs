#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using SageFrame.RolesManagement;
using SageFrame.Web;
using SageFrame.Framework;
using System.Data.SqlClient;
using SageFrame.Utilities;
using System.Web.UI;
using System.IO.Compression;
using System.IO;
using SageFrame.Common;
using SageFrame.Scheduler;
using System.Threading;

#endregion

namespace SageFrame.Core
{
    /// <summary>
    /// Global class of the sytem
    /// </summary>
    public class Global : Globals
    {
        #region "Public Variables"

        public string ANONYMOUS_ROLEID;

        #endregion

        #region "Protected Methods"

        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                //RouteTable.Routes.MapHubs();
                ApplicationController objAppController = new ApplicationController();
                if (objAppController.IsInstalled())
                {
                    SageFrameConfig SageConfig = new SageFrameConfig();
                    RolesManagementController objController = new RolesManagementController();
                    RolesManagementInfo res = objController.GetRoleIDByRoleName(SystemSetting.AnonymousUsername);
                    if (res != null)
                    {
                        SystemSetting.ANONYMOUS_ROLEID = res.RoleId.ToString();
                    }
                    SageFrameSettingKeys.PageExtension = SageConfig.GetSettingsByKey(SageFrameSettingKeys.SettingPageExtension);
                    bool isSchedulerOn = bool.Parse(SageConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.Scheduler));
                    RegisterRoutes(RouteTable.Routes);
                    if (isSchedulerOn)
                    {
                        RunSchedule();
                    }
                }
            }
            catch
            {
            }

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            try
            {
                //HttpContext.Current.Session[SessionKeys.ModuleCss] = new List<CssScriptInfo>();
                //HttpContext.Current.Session[SessionKeys.ModuleJs] = new List<CssScriptInfo>();
                ApplicationController objAppController = new ApplicationController();
                if (objAppController.IsInstalled())
                {

                    HttpContext.Current.Session[SessionKeys.SageFrame_PortalID] = null;
                    Session[SessionKeys.SageFrame_PortalID] = null;
                    Session[SessionKeys.SageFrame_PortalSEOName] = null;
                    SessionTracker sessionTracker = new SessionTracker();

                    SageFrameConfig SageConfig = new SageFrameConfig();
                    SageFrameSettingKeys.PageExtension = SageConfig.GetSettingsByKey(SageFrameSettingKeys.SettingPageExtension);
                    bool EnableSessionTracker = bool.Parse(SageConfig.GetSettingsByKey(SageFrameSettingKeys.EnableSessionTracker));

                    if (sessionTracker != null && EnableSessionTracker)
                    {
                        string sessionID = HttpContext.Current.Session.SessionID;
                        //SessionLog sLog = new SessionLog();
                        //sLog.SessionLogStart(sessionTracker, sessionID);
                        Thread newThread = new Thread(() => UpdateSessionTracker(sessionTracker, sessionID));
                        newThread.Start();
                    }
                    // HttpContext.Current.Session[SessionKeys.Tracker] = sessionTracker;
                }
            }
            catch
            {
            }
        }

        private void UpdateSessionTracker(SessionTracker sessionTracker, string sessionID)
        {
            SessionLog sLog = new SessionLog();
            sLog.SessionLogStart(sessionTracker, sessionID);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {


        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (null != HttpContext.Current)
            {
                string url = HttpContext.Current.Request.Url.ToString();
                ApplicationController objAppController = new ApplicationController();
                if (objAppController.IsImageRequest(url))
                {
                    ErrorHandler erHandler = new ErrorHandler();
                    erHandler.LogCommonException(ex);
                }
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
            try
            {
                // SessionTracker sessionTracker = (SessionTracker)Session[SessionKeys.Tracker];
                FormsAuthentication.SignOut();
                //if ((sessionTracker == null))
                //{
                //    return;
                //}
                //else
                //{

                SageFrameConfig SageConfig = new SageFrameConfig();
                SageFrameSettingKeys.PageExtension = SageConfig.GetSettingsByKey(SageFrameSettingKeys.SettingPageExtension);
                bool EnableSessionTracker = bool.Parse(SageConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.EnableSessionTracker));
                if (EnableSessionTracker)
                {
                    SessionLog sLog = new SessionLog();
                    int PortalID = int.Parse(HttpContext.Current.Session[SessionKeys.SageFrame_PortalID].ToString());
                    sLog.SessionLogEnd(PortalID);
                }
                //}

            }
            catch
            {
            }
            if (HttpContext.Current != null)
            {
                if (null != HttpContext.Current.Session)
                    HttpContext.Current.Session.Abandon();
            }

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        #endregion

        #region "Private Methods"

        private static void RegisterRoutes(RouteCollection routes)
        {
            #region "Getting SageFrame Page Extension"

            string ext = SageFrameSettingKeys.PageExtension;
            if (string.IsNullOrEmpty(ext))
            {
                ext = ".htm";
            }

            #endregion

            #region "SageFrame Ignore Routing"

            routes.RouteExistingFiles = false;
            routes.Add(new Route("{resource}.axd/{*pathInfo}", new StopRoutingHandler()));
            routes.Ignore("{*alljs}", new { alljs = @".*\.(js|asmx|jpg|png|jpeg|css)(/.*)?" });
            routes.Ignore("install", new { installwizard = "Install/InstallWizard.aspx" });
            routes.Add(new Route("Modules/{*pathInfo}", new StopRoutingHandler()));

            #endregion

            #region "SageFrame Core Routing"

            routes.Add("sfPortalfa", new System.Web.Routing.Route("Admin/Admin/Portal.aspx" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfrouting2", new System.Web.Routing.Route("sf/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Admin.aspx")));
            routes.Add("sfrouting3", new System.Web.Routing.Route("sf/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Admin.aspx")));
            routes.Add("sfrouting4", new System.Web.Routing.Route("sf/sf/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Admin.aspx")));
            routes.Add("sfrouting5", new System.Web.Routing.Route("portal/{PortalSEOName}/sf/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Admin.aspx")));
            routes.Add("sfrouting6", new System.Web.Routing.Route("portal/{PortalSEOName}/sf/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Admin.aspx")));
            routes.Add("SageFrameRoutingCategory", new Route("category/{*uniqueWord}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("SageFrameRoutingProductDetail", new Route("item/{*uniqueWord}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("SageFrameRoutingTagsAll", new Route("tags/{*uniqueWord}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("SageFrameRoutingBrand", new Route("brand/{*uniqueWord}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("SageFrameRoutingService", new Route("service/{*uniqueWord}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("SageFrameRoutingTagsItems", new Route("tagsitems/{*uniqueWord}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("SageFrameRoutingSearchTerm", new Route("search/{*uniqueWord}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("SageFrameRoutingShoppingOption", new Route("option/{*uniqueWord}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("SageFrameRoutingCouponsAll", new Route("coupons/{*uniqueWord}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("SageFrameRouting0", new Route("portal/{PortalSEOName}/category/{*uniqueWord}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("SageFrameRouting01", new Route("portal/{PortalSEOName}/item/{*uniqueWord}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("SageFrameRouting02", new Route("portal/{PortalSEOName}/tags/{*uniqueWord}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("SageFrameRouting03", new Route("portal/{PortalSEOName}/tagsitems/{*uniqueWord}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("SageFrameRouting04", new Route("portal/{PortalSEOName}/search/{*uniqueWord}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("SageFrameRouting05", new Route("portal/{PortalSEOName}/option/{*uniqueWord}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("SageFrameRouting06", new Route("portal/{PortalSEOName}/brand/{*uniqueWord}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));

            #endregion

            #region "AspxCommerce Routing"

            routes.Add("sfAspx1", new System.Web.Routing.Route("Admin/AspxCommerce/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspx2", new System.Web.Routing.Route("admin/AspxCommerce/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspx3", new System.Web.Routing.Route("Admin/Admin/AspxCommerce/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspx4", new System.Web.Routing.Route("admin/admin/AspxCommerce/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspx5", new System.Web.Routing.Route("Admin/AspxCommerce/{parentname}/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspx6", new System.Web.Routing.Route("admin/AspxCommerce/{parentname}/{pagepath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspx7", new System.Web.Routing.Route("Admin/AspxCommerce/{parentname}/{subparent}/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspx8", new System.Web.Routing.Route("admin/AspxCommerce/{parentname}/{subparent}/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspx9", new System.Web.Routing.Route("Admin/AspxCommerce/{parentname}/{subparent}/{subsubparent}/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspx10", new System.Web.Routing.Route("admin/AspxCommerce/{parentname}/{subparent}/{subsubparent}/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Default.aspx")));


            routes.Add("sfAspxAdmin1", new Route("portal/{PortalSEOName}/Admin/AspxCommerce/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspxAdmin2", new Route("portal/{PortalSEOName}/admin/AspxCommerce/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspxAdmin3", new Route("portal/{PortalSEOName}/Admin/Admin/AspxCommerce/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspxAdmin4", new Route("portal/{PortalSEOName}/admin/admin/AspxCommerce/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspxAdmin5", new Route("portal/{PortalSEOName}/Admin/AspxCommerce/{parentname}/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspxAdmin6", new Route("portal/{PortalSEOName}/admin/AspxCommerce/{parentname}/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspxAdmin7", new Route("portal/{PortalSEOName}/Admin/AspxCommerce/{parentname}/{subparent}/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspxAdmin8", new Route("portal/{PortalSEOName}/admin/AspxCommerce/{parentname}/{subparent}/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspxAdmin9", new Route("portal/{PortalSEOName}/Admin/AspxCommerce/{parentname}/{subparent}/{subsubparent}/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfAspxAdmin10", new Route("portal/{PortalSEOName}/admin/AspxCommerce/{parentname}/{subparent}/{subsubparent}/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));

            #endregion

            #region "Routes To ManagePage"

            //  routes to managepage
            routes.Add("SageFrameRouting1", new Route("portal/{PortalSEOName}/{UserModuleID}/Sagin/{ControlType}/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/ManagePage.aspx")));
            routes.Add("SageFrameRouting2", new Route("{UserModuleID}/Sagin/{ControlType}/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/ManagePage.aspx")));
            routes.Add("controlrouting1", new Route("sagin/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/HandleModuleControls.aspx")));
            routes.Add("controlrouting2", new Route("sagin/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/HandleModuleControls.aspx")));
            routes.Add("controlrouting3", new Route("sagin/{PagePath}.aspx" + "/{*Param}", new SageFrameRouteHandler("~/Sagin/HandleModuleControls.aspx")));

            #endregion

            #region "Portal Routing"

            //Portal routing
            routes.Add("sfPortal1", new System.Web.Routing.Route("portal/{PortalSEOName}/Admin/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal2", new System.Web.Routing.Route("portal/{PortalSEOName}/Admin/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal3", new System.Web.Routing.Route("portal/{PortalSEOName}/Admin/Admin/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal4", new System.Web.Routing.Route("portal/{PortalSEOName}/Admin/Admin/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal5", new System.Web.Routing.Route("portal/{PortalSEOName}/Super-User/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal6", new System.Web.Routing.Route("portal/{PortalSEOName}/Super-User/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal7", new System.Web.Routing.Route("Admin/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal8", new System.Web.Routing.Route("Admin/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal9", new System.Web.Routing.Route("Admin/Admin/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal10", new System.Web.Routing.Route("admin/admin/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal11", new System.Web.Routing.Route("portal/{PortalSEOName}/admin/admin/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal12", new System.Web.Routing.Route("portal/{PortalSEOName}/admin/admin/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal13", new System.Web.Routing.Route("portal/{PortalSEOName}/admin/Super-User/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal14", new System.Web.Routing.Route("portal/{PortalSEOName}/admin/Super-User/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal15", new System.Web.Routing.Route("admin/super-user/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal16", new System.Web.Routing.Route("admin/super-user/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal17", new System.Web.Routing.Route("Admin" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal18", new System.Web.Routing.Route("Admin" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal19", new System.Web.Routing.Route("Super-User" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal20", new System.Web.Routing.Route("Super-User" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal21", new System.Web.Routing.Route("Super-User/{PagePath}" + ext, new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal22", new System.Web.Routing.Route("Super-User/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/Sagin/Default.aspx")));
            routes.Add("sfPortal23", new System.Web.Routing.Route("portal/{PortalSEOName}/{PagePath}" + ext, new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("sfPortal24", new System.Web.Routing.Route("portal/{PortalSEOName}/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));

            #endregion

            #region "Page Routing"

            //page routing
            routes.Add("sage", new Route("Default.aspx", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("sage1", new Route("{PagePath}" + ext, new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("sage2", new Route("{parentname}/{PagePath}" + ext, new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("sage3", new Route("{parentname}/{subparent}/{PagePath}" + ext, new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("sage4", new Route("{parentname}/{subparent}/{modulename}/{PagePath}" + ext, new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("sage5", new Route("{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("sage6", new Route("{parentname}/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("sage7", new Route("{parentname}/{subparent}/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("sage8", new Route("{parentname}/{subparent}/{modulename}/{PagePath}" + ext + "/{*Param}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));
            routes.Add("allroute", new Route("{*PagePath}", new SageFrameRouteHandler("~/" + CommonHelper.DefaultPage)));

            #endregion
        }

        #endregion

        #region "Public Methods"

        /// <summary>
        /// Runs Scheduler if is actived.
        /// </summary>
        public static void RunSchedule()
        {
            try
            {
                Scheduler.Scheduler scheduler = new Scheduler.Scheduler(1);
                Scheduler.Scheduler.KeepRunning = true;
                Scheduler.Scheduler.KeepThreadAlive = true;
                System.Threading.Thread RequestScheduleThread = null;
                RequestScheduleThread = new System.Threading.Thread(Scheduler.Scheduler.Start);
                RequestScheduleThread.IsBackground = true;
                RequestScheduleThread.Start();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Application's  prerequest handler execute
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">event arguments.</param>
        public void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            try
            {
                ApplicationController objAppController = new ApplicationController();
                if (objAppController.IsInstalled())
                {
                    if ((Context.Session != null))
                    {
                        SessionTracker tracker = (SessionTracker)Session[SessionKeys.Tracker];
                        if ((tracker != null))
                        {
                            tracker.AddPage(Request.Url.ToString());
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        #region "Obsolete Methods"

        [Obsolete("Not used after SageFrame2.0")]
        private void CompressResponseIfAllowed(object sender, EventArgs e)
        {
            try
            {
                HttpRequest SageRequest = HttpContext.Current.Request;
                if (IsExtensionisAllowedToCompress(SageRequest))
                {
                    HttpResponse Response = HttpContext.Current.Response;
                    if (IsCompressionAllowed()) // If client browser supports HTTP compression
                    {
                        //Browser supported encoding format
                        //string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
                        //if client browser supports both "deflate"
                        //and "gzip" compression, Then we will consider "deflate" first , because it is
                        // more efficient than "gzip"
                        HttpApplication app = sender as HttpApplication;
                        string AcceptEncoding = app.Request.Headers["Accept-Encoding"];
                        Stream prevUncompressedStream = app.Response.Filter;

                        if (!(app.Context.CurrentHandler is Page ||
                            app.Context.CurrentHandler.GetType().Name == "SyncSessionlessHandler") ||
                            app.Request["HTTP_X_MICROSOFTAJAX"] != null)
                            return;

                        if (AcceptEncoding == null || AcceptEncoding.Length == 0)
                            return;

                        AcceptEncoding = AcceptEncoding.ToLower();
                        if (AcceptEncoding != null && AcceptEncoding.Contains("deflate"))
                        {
                            Response.AppendHeader("Content-Encoding", "deflate");
                            Response.Filter = new DeflateStream(Response.Filter, CompressionMode.Compress);

                        }
                        else
                        {
                            Response.AppendHeader("Content-Encoding", "gzip");
                            Response.Filter = new GZipStream(Response.Filter, CompressionMode.Compress);

                        }
                    }
                    // Allow proxy servers to cache encoded and unencoded versions separately
                    Response.AppendHeader("Vary", "Content-Encoding");
                }
            }
            catch
            {
            }
            try
            {
                HttpRequest SageRequest = HttpContext.Current.Request;
                if (IsExtensionisAllowedToCompress(SageRequest))
                {
                    HttpResponse Response = HttpContext.Current.Response;
                    if (IsCompressionAllowed()) // If client browser supports HTTP compression
                    {
                        //Browser supported encoding format
                        string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
                        //if client browser supports both "deflate"
                        //and "gzip" compression, Then we will consider "deflate" first , because it is
                        // more efficient than "gzip"
                        if (AcceptEncoding != null && AcceptEncoding.Contains("deflate"))
                        {
                            Response.Filter = new DeflateStream(Response.Filter, CompressionMode.Compress);
                            Response.AppendHeader("Content-Encoding", "deflate");
                        }
                        else
                        {
                            Response.Filter = new GZipStream(Response.Filter, CompressionMode.Compress);
                            Response.AppendHeader("Content-Encoding", "gzip");
                        }
                    }

                    // Allow proxy servers to cache encoded and unencoded versions separately
                    Response.AppendHeader("Vary", "Content-Encoding");
                }
            }
            catch
            {
            }
        }

        [Obsolete("Not used after SageFrame2.0")]
        public bool IsExtensionisAllowedToCompress(HttpRequest SageRequest)
        {
            string RequestRawURL = SageRequest.RawUrl;
            return (CommonHelper.Contains(SystemSetting.INCOMPRESSIBLE_EXTENSIONS, RequestRawURL));
        }

        [Obsolete("Not used after SageFrame2.0")]
        public bool IsCompressionAllowed()
        {
            //Browser supported encoding format
            string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];

            if (!string.IsNullOrEmpty(AcceptEncoding))
            {
                if (AcceptEncoding.Contains("gzip") || AcceptEncoding.Contains("deflate"))
                    return true; ////If browser supports encoding
            }

            return false;
        }

        #endregion
    }
}