using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Configuration;
using System.Xml;
using System.Web.Security;
using SageFrame.Web;

namespace SageFrame.Upgrade
{
    public class Router : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            try
            {
                app.BeginRequest += new EventHandler(MaintainenceCheck);
            }
            catch 
            {
                throw;
            }
        }

        private void MaintainenceCheck(object sender, EventArgs args)
        {
            try
            {
                CommonFunction objAppController = new CommonFunction();
                if (objAppController.IsInstalled())
                {
                    HttpApplication app = sender as HttpApplication;
                    HttpContext context = HttpContext.Current;
                    if (context.Request.Path.EndsWith(".aspx"))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(context.Server.MapPath("~/Modules/Upgrade/SiteMaintainceConfig.xml"));
                        XmlNode node = doc.SelectSingleNode("/Config/MySection1");
                        bool flag = Convert.ToBoolean(node.ChildNodes[0].InnerText);
                        if (flag && !context.Request.Path.ToLower().Contains("login.aspx") && !context.Request.Path.Contains(node.ChildNodes[1].InnerText))
                        {
                            bool IsAdmin = false;
                            if (HttpContext.Current.User != null)
                            {
                                MembershipUser user = Membership.GetUser();
                                if (user != null)
                                {

                                    string[] sysRoles = SystemSetting.SYSTEM_SUPER_ROLES;

                                    foreach (string role in sysRoles)
                                    {
                                        if (Roles.IsUserInRole(user.UserName, role))
                                        {
                                            IsAdmin = true;
                                            break;
                                        }
                                    }

                                }
                                if (IsAdmin && (context.Request.Path.ToLower().Contains("/admin/") || context.Request.Path.Contains("/Super-User") || !context.Request.Path.Contains("Upgrade/upgrade.aspx")))
                                {

                                    context.Response.Redirect("~/Modules/Upgrade/upgrade.aspx", true);
                                }
                            }
                            context.Response.Redirect("~" + node.ChildNodes[1].InnerText, false);
                        }
                    }
                }
            }
            catch 
            {
                throw;
            }
        }
        public void Dispose()
        {


        }
    }
}
