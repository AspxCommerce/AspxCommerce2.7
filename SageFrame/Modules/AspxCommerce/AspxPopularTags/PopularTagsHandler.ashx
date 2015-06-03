﻿<%@ WebHandler Language="C#" Class="PopularTagsHandler" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using ServiceInvoker;
using System.Web.SessionState;

public class PopularTagsHandler : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            
                HttpRequest request = context.Request;
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
                JavaScriptSerializer jss = new JavaScriptSerializer();
                ServiceProcesser process = new ServiceProcesser(typeof(AspxCommerce.PopularTags.PopularTagsController), json, methodName);
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