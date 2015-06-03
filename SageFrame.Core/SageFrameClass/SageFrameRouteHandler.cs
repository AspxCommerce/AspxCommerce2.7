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
using System.Web.Routing;
using System.Web.Compilation;
using System.Web.UI;
using SageFrame.Web;


#endregion


namespace SageFrame
{
    /// <summary>
    /// SageFrame's routing handler that inherits IRouteHandler.
    /// </summary>
    public class SageFrameRouteHandler : IRouteHandler
    {
        /// <summary>
        /// Initializes an instance of SageFrmeRouteHandler class
        /// </summary>
        /// <param name="virtualPath">Virtual path.</param>
        public SageFrameRouteHandler(string virtualPath)
        {
            this.VirtualPath = virtualPath;
        }

        /// <summary>
        /// Returns virtual path.
        /// </summary>
        public string VirtualPath { get; private set; }

        /// <summary>
        /// Returns IHttpHandler object for current requested url.
        /// </summary>
        /// <param name="requestContext">RequestContext object.</param>
        /// <returns>IHttpHandler object</returns>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var page = BuildManager.CreateInstanceFromVirtualPath(VirtualPath, typeof(Page)) as SageFrameRoute;
            if (requestContext.RouteData.Values["PagePath"] != null)
            {
                page.PagePath = requestContext.RouteData.Values["PagePath"].ToString();
            }
            if (requestContext.RouteData.Values["Param"] != null)
            {
                try
                {
                    var Param = requestContext.RouteData.Values["Param"].ToString().Split('/');
                    int count = Param.Length;
                    for (int i = 0; i < count; i++)
                    {
                        if (Param[i + 1] != null)
                        {
                            requestContext.HttpContext.Items.Add(Param[i], Param[i + 1]);
                        }
                        i = i + 1;
                    }
                }
                catch 
                { throw; }
            }

            if (requestContext.RouteData.Values["PortalSEOName"] != null)
            {
                page.PortalSEOName = requestContext.RouteData.Values["PortalSEOName"].ToString();
            }
            if (requestContext.RouteData.Values["UserModuleID"] != null)
            {
                page.UserModuleID = requestContext.RouteData.Values["UserModuleID"].ToString();
            }
            if (requestContext.RouteData.Values["ControlType"] != null)
            {
                page.ControlType = requestContext.RouteData.Values["ControlType"].ToString();
            }
            if (requestContext.RouteData.Values["uniqueWord"] != null)
            {
                Route route = (Route)requestContext.RouteData.Route;
                string url = route.Url;

                if (url.Equals("category/{*uniqueWord}") || url.Equals("portal/{PortalSEOName}/category/{*uniqueWord}"))
                {
                    page.ControlMode = "category";
                }
                else if (url.Equals("item/{*uniqueWord}") || url.Equals("portal/{PortalSEOName}/item/{*uniqueWord}"))
                {
                    page.ControlMode = "item";
                }
                else if (url.Equals("tags/{*uniqueWord}") || url.Equals("portal/{PortalSEOName}/tags/{*uniqueWord}"))
                {
                    page.ControlMode = "tags";
                }
                else if (url.Equals("tagsitems/{*uniqueWord}") || url.Equals("portal/{PortalSEOName}/tagsitems/{*uniqueWord}"))
                {
                    page.ControlMode = "tagsitems";
                }
                else if (url.Equals("search/{*uniqueWord}") || url.Equals("portal/{PortalSEOName}/search/{*uniqueWord}"))
                {
                    page.ControlMode = "search";
                }
                else if (url.Equals("option/{*uniqueWord}") || url.Equals("portal/{PortalSEOName}/option/{*uniqueWord}"))
                {
                    page.ControlMode = "option";
                }
                else if (url.Equals("brand/{*uniqueWord}") || url.Equals("portal/{PortalSEOName}/brand/{*uniqueWord}"))
                {
                    page.ControlMode = "brand";
                }
                else if (url.Equals("service/{*uniqueWord}") || url.Equals("portal/{PortalSEOName}/service/{*uniqueWord}"))
                {
                    page.ControlMode = "service";
                }

                string pageName = requestContext.RouteData.Values["uniqueWord"].ToString();
                if (pageName.IndexOf(SageFrameSettingKeys.PageExtension) > 0)
                {
                    page.Key = pageName.Substring(0, pageName.IndexOf(SageFrameSettingKeys.PageExtension));
                    if (page.ControlMode == "item")
                    {
                        page.PagePath = "Item-Detail";
                    }
                    else if (page.ControlMode == "category")
                    {
                        page.PagePath = "Category-Detail";
                    }
                    else if (page.ControlMode == "service")
                    {
                        page.PagePath = "Services-Detail";
                    }
                    else
                    {
                        page.PagePath = "Show-Details-Page";
                    }
                    pageName = page.PagePath;
                }
            }
            return page;
        }

    }
}
