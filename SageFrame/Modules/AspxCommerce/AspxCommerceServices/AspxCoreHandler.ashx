<%@ WebHandler Language="C#" Class="AspxCoreHandler" %>

using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using ServiceInvoker;
using System.Web.SessionState;
using SageFrame.Core;


public class AspxCoreHandler : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            
            HttpRequest request = context.Request;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (!ServiceSecurity.ValidateToken(context))
            {
                throw new System.Security.SecurityException("Authorization Failed.");
            }

            string methodName = "";
            if (request.RequestContext.RouteData.Values["methodName"] != null)
            {
                methodName = request.RequestContext.RouteData.Values["methodName"].ToString();
            }
            else
            {
                methodName = request.RawUrl.Substring(request.RawUrl.LastIndexOf("/") + 1);
                //throw new Exception(ErrorType.INVALID_METHOD_CALL.ToString());
            }

            //if form post was made way to get values
            //context.Request.Form["firstName"]; //parameter key
            string json = new System.IO.StreamReader(context.Request.InputStream).ReadToEnd();

            ServiceProcesser process = new ServiceProcesser(typeof(AspxCommerce.Core.AspxCoreController), json, methodName);
            var response = process.Invoke();

            if (response != null)
            {
                context.Response.ContentType = "application/json";
                context.Response.Write(jss.Serialize(new { d = response }));
            }
            else
            {
                context.Response.ContentType = "application/json";
                context.Response.Write(jss.Serialize(new { d = "" }));
            }
        }
        catch (Exception ex)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            context.Response.ContentType = "application/json";
            var resp = new { d = ex.Message.ToString() };
            context.Response.Write(jss.Serialize(resp));
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}