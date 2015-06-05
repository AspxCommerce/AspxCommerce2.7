using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace ServiceInvoker
{

    public class ServiceRouteHandler //: IRouteHandler
    {
        //private static IHttpHandlerFactory ScriptHandlerFactory;
        //static ServiceRouteHandler()
        //{
        //    var assembly = typeof(System.Web.Script.Services.ScriptMethodAttribute).Assembly;
        //    var type = assembly.GetType("System.Web.Script.Services.ScriptHandlerFactory");
        //    //   var type = typeof(ScriptHandlerFactory);
        //    ScriptHandlerFactory = (IHttpHandlerFactory)Activator.CreateInstance(type, true);
        //}

        //private string virtualPath;
        //public ServiceRouteHandler(string virtualPath)
        //{
        //    this.virtualPath = virtualPath;
        //}

        //public IHttpHandler GetHttpHandler(RequestContext requestContext)
        //{
        //    //for fix error Request format is unrecognized for URL unexpectedly ending in 'HelloWorld'.
        //    // for web get request 
        //    //<webServices>
        //    //<protocols>
        //    //   <add name="HttpGet"/>
        //    //        <add name="HttpPost"/>
        //    //    </protocols>
        //    //</webServices>


        //    string pathInfo = requestContext.RouteData.Values["pathInfo"] as string;
        //    if (!string.IsNullOrWhiteSpace(pathInfo))
        //        pathInfo = string.Format("/{0}", pathInfo);

        //    requestContext.HttpContext.RewritePath(this.virtualPath, pathInfo, requestContext.HttpContext.Request.QueryString.ToString());
        //    var handler = ScriptHandlerFactory.GetHandler(HttpContext.Current, requestContext.HttpContext.Request.HttpMethod, this.virtualPath, requestContext.HttpContext.Server.MapPath(this.virtualPath));
        //    return handler;
        //}
    }
}
