using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;

namespace ServiceInvoker
{
    public class HttpHandlerRoute : IRouteHandler
    {

        private String _VirtualPath = null;

        public HttpHandlerRoute(String virtualPath)
        {
            _VirtualPath = virtualPath;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            IHttpHandler httpHandler = (IHttpHandler)BuildManager.CreateInstanceFromVirtualPath(_VirtualPath, typeof(IHttpHandler));
            return httpHandler;
        }
    }
}
