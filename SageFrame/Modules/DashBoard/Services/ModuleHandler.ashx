<%@ WebHandler Language="C#" Class="SageFrame.Modules.DashBoard.ModuleHandler" %>

using System;
using System.Web;
using System.Net;
using SageFrame.Dashboard;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SageFrame.Modules.DashBoard
{
    [System.Web.Script.Services.ScriptService]
    public class ModuleHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string service = "DashBoardSectionsWebService";
            string method = "GetModules";
            string[] args = new string[1];
            args[0] = "5";
            if (HttpContext.Current.Request.Form.Count > 0)
            {
                string searchKey = HttpContext.Current.Request.Form[0] as string;
                if (searchKey != null)
                {
                    //searchKey = searchKey.Replace("%22", "").Replace("%3a", ":");
                    Match match = Regex.Match(searchKey, ".*:\\s*\"(?<key>.*)\"}");
                    if (match.Success)
                    {
                        searchKey = match.Groups[1].Value;
                        method = "SearchModules";
                        args[0] = searchKey;
                    }
                }
            }
            WebServiceInvoker invoker =
                new WebServiceInvoker(
                    new Uri("http://www.sageframe.com/Modules/Dashboard/Services/DashBoardSectionsWebService.asmx"));
            string result = invoker.InvokeMethod<string>(service, method, args);
            context.Response.ContentType = "application/json";
            context.Response.Write(result);
        }
                
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}