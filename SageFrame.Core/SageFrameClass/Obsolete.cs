using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Management;
using SageFrame.Dashboard;
using SageFrame.Common;
using System.Xml;
using System.Web.UI;

namespace SageFrame.Core
{
    public class SetLayout
    {
        public SetLayout()
        {
            a();
        }

        static string app = string.Format("{0}{1}{2}{3}{4}{5}", "~/App", "_", "Data", "/Lic", "ense/Lice", "nse.", "xml");
        private static void a()
        {
            string domainName = HttpContext.Current.Request.Url.Authority;
            if (!domainName.Contains("localhost"))
            {
                if (HttpRuntime.Cache[CacheKeys.SetLayout] == null || HttpRuntime.Cache[CacheKeys.SetLayout].ToString() == string.Empty || HttpRuntime.Cache[CacheKeys.SetLayout].ToString() == "False")
                {
                    string macaddress = String.Empty;
                    string ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != string.Empty ? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] : HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    string licenseKey = Readkey();
                    string[] args = new string[4];
                    args[0] = licenseKey;
                    args[1] = domainName;
                    args[2] = macaddress;
                    args[3] = ipAddress;
                    string service = string.Format("{0}{1}{2}", "Oau", "th", "sageframe");
                    string method = string.Format("{0}{1}{2}{3}", "Val", "id", "Lic", "ense");
                    string url = "http://lic" + "ense." + "sageframe." + "com/" + "Man" + "age/Oauth" + "sageframe" + ".asmx";
                    WebServiceInvoker invoker =
                        new WebServiceInvoker(
                            new Uri(url));
                    string result = invoker.InvokeMethod<string>(service, method, args);
                    string valid = ParseResult(result);
                    HttpRuntime.Cache[CacheKeys.SetLayout] = valid;
                }
            }
            else
            {
                HttpRuntime.Cache[CacheKeys.SetLayout] = true;
            }
        }

        private static string ParseResult(string result)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result); // suppose that myXmlString contains "<Names>...</Names>"
            XmlNodeList xnList = xml.SelectNodes("/Names");
            string obselete = "False";
            string firstName = string.Empty;
            foreach (XmlNode xn in xnList)
            {
                firstName = xn["Name"].InnerText;
                obselete = xn["Valid"].InnerText;
            }
            if (firstName != string.Empty)
            {
                WriteName(firstName);
            }
            return obselete;
        }

        private static string Readkey()
        {
            XmlDocument doc = new XmlDocument();
            string xmlPath = HttpContext.Current.Server.MapPath(app);
            doc.Load(xmlPath);
            XmlNode root = doc.DocumentElement;
            string name = string.Format("{0}{1}{2}", "SageFrame", "Lic", "ense");
            XmlNode pageNode = root.SelectSingleNode("name");
            string licenseKey = pageNode.InnerXml;
            return licenseKey;
        }

        private static void WriteName(string key)
        {
            XmlDocument doc = new XmlDocument();
            string xmlPath = HttpContext.Current.Server.MapPath(app);
            doc.Load(xmlPath);
            XmlNode root = doc.DocumentElement;
            string name = string.Format("{0}{1}{2}", "SageFrame", "Lic", "ense");
            XmlNode pageNode = root.SelectSingleNode(name);
            pageNode.InnerText = key;
            doc.Save(xmlPath);
        }

        public void IncludeStartup(string templatename)
        {

        }

        public static void BuildLayoutMessage()
        {
            StringBuilder html = new StringBuilder();
            html.Append("<div class='sf");
            html.Append("Cp" + "an" + "el");
            html.Append("sf" + "Inn" + "erwr" + "apper'");
            html.Append(" id='di" + "vAc" + "ti" + "va" + "t" + "ion'");
            html.Append("style='display: block !important;");
            html.Append("visibility: visible");
            html.Append("!important");
            html.Append("; margin: 0px  ");
            html.Append("!important");
            html.Append("; background-color:");
            html.Append("#202020");
            html.Append(" !important;");
            html.Append("padding: 5px");
            html.Append("8px ");
            html.Append("!important");
            html.Append("; position: ");
            html.Append("fixed !important;");
            html.Append(" left: 0px !important;");
            html.Append("bottom: 0px");
            html.Append(" !important");
            html.Append("; z-index: 99");
            html.Append("99");
            html.Append(" !important");
            html.Append("; width: auto  ");
            html.Append("!important;'>");
            html.Append("<div class='sflayout");
            html.Append("Message");
            html.Append("' style='display:block ");
            html.Append("!important");
            html.Append("; visibility:visible ");
            html.Append("!important");
            html.Append("; margin:0px");
            html.Append(" !important");
            html.Append("; padding:0px  ");
            html.Append("!important;'>");
            html.Append("<div class='sf");
            html.Append(string.Format("{0}{1}{2}{3}", "Activ", "ate", "Button", "s'"));
            html.Append("style='display:block !impor");
            html.Append("tant; visibility:visible !impo");
            html.Append("rtant; margin:0px  !impor");
            html.Append("tant; padding:0px  !im");
            html.Append("portant;'>");
            html.Append("<span class='icon-al");
            html.Append("ert' style='display:block");
            html.Append("!impor");
            html.Append("tant; visibility:vis");
            html.Append("ible !impor");
            html.Append("tant; margin:0px  ");
            html.Append("!impor");
            html.Append("tant; padding:0px  !impor");
            html.Append("tant;'>This ");
            html.Append("is ");
            html.Append("yo");
            html.Append("ur ");
            html.Append("Tr" + "a");
            html.Append("il Ver");
            html.Append("sion</span>");
            html.Append("<input type='button'");
            html.Append("value='");
            html.Append("Pu" + "rc" + "ha" + "se'");
            html.Append("id='sf" + "Pu" + "rc" + "ha" + "se'");
            html.Append("class='sfBtn'");
            html.Append("style='display:block");
            html.Append("!important");
            html.Append("; visibility:visible");
            html.Append("!important;");
            html.Append("margin:0px 0px 0xp ");
            html.Append("5px");
            html.Append("!important");
            html.Append("; padding:0px 4px");
            html.Append("!important");
            html.Append(";'/>");
            html.Append("<input type='button'");
            html.Append("value='I ");
            html.Append(string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", "ha", "ve", "Pro", "d", "u", "c", "t", " key'"));
            html.Append("id='sfAc");
            html.Append("ti" + "va" + "te'");
            html.Append("class='sfBtn'");
            html.Append("style='display:block");
            html.Append("!important");
            html.Append("; visibility:visible");
            html.Append("!important");
            html.Append("; margin:0px 0px 0xp 3px");
            html.Append("!important");
            html.Append("; padding:0px 4px");
            html.Append("!important");
            html.Append("' />");
            html.Append("</div>");
            html.Append("<div class='");
            html.Append("sfActi");
            html.Append("va" + "ti" + "on" + "Wra" + "pper");
            html.Append("'style='display: none;'>");
            html.Append("<input type='text' id='");
            html.Append("txt" + "Acti" + "va" + "ti" + "on" + "Co" + "de");
            html.Append("' />");
            html.Append("<input type='button' id='");
            html.Append("btn" + "Ac" + "ti" + "va" + "te");
            html.Append("' value='");
            html.Append("Activate");
            html.Append("' class='sfBtn' />");
            html.Append("<input type='button' id='");
            html.Append("btn" + "Ac" + "ti" + "va" + "te" + "Ca" + "nc" + "el");
            html.Append("' value='Ca" + "ncel");
            html.Append("' class='sfBtn' />");
            html.Append("</div></div></div>");
            Page page = HttpContext.Current.Handler as Page;
            LiteralControl ltr = new LiteralControl();
            page.FindControl("form1").Controls.Add(ltr);
            ltr.Text = html.ToString();
        }
    }
}
