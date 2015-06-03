using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using System.Xml;
using System.IO;
using SageFrame.Templating.xmlparser;
using System.Text;
using SageFrame.Application;

public partial class Modules_Admin_PageHelp_PageHelp : BaseAdministrationUserControl
{
    public string pageName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        GetPageHelpMessage();
    }
    private void GetPageHelpMessage()
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            string xmlPath = HttpContext.Current.Server.MapPath("~/XMLMessage/SageXML." + GetCurrentCultureName + ".xml");
            if (!File.Exists(xmlPath))
                xmlPath = HttpContext.Current.Server.MapPath("~/XMLMessage/SageAdminHelpText.xml");
            doc.Load(xmlPath);
            string URL = Request.Url.ToString().Split('?')[0];
            pageName = GetPageSEOName(URL);
            lnkpage.Text = "Documentation on " + pageName;
            string navigateURL = "http://www.aspxcommerce.com/Page-Documentation.aspx?pageName=" + pageName;

            SageFrameConfig objSageConfig = new SageFrameConfig();
            ApplicationInfo objApplication = objSageConfig.GetApplicationInfo("SageFrame");

            if (objApplication != null)
            {
                navigateURL += objApplication.ApplicationId.ToString();
            }
            lnkpage.NavigateUrl = navigateURL + "&V=3.0";
            XmlNode root = doc.DocumentElement;
            XmlNode pageNode = root.SelectSingleNode(pageName.ToLower());
            if (pageNode != null)
            {
                BindPageHelpHtml(pageNode);
            }
            else {
                string defaultMsg = "";
                defaultMsg = "This is a newly created page.If you want to have help notification for this page go to filemanager and update in SageAdminHelpText.xml file.";
                StringBuilder description = new StringBuilder();
                description.Append("<div class='sfHorTabContentHolder' id='sfHorTabContentHolder_");
                description.Append("'><p>");
                description.Append(defaultMsg);
                description.Append("</p></div>");
                ltrPageHelp.Text = description.ToString();
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    public void BindPageHelpHtml(XmlNode pageNode)
    {
        StringBuilder title = new StringBuilder();
        StringBuilder description = new StringBuilder();
        title.Append("<ul class='sfHorTabMenu'>");
        int count = 0;
        int length = pageNode.ChildNodes.Count;
        foreach (XmlNode objNode in pageNode)
        {
            if (length > 1)
            {
                title.Append("<li><a href='#sfHorTabContentHolder_");
                title.Append(count);
                title.Append("'>");
                title.Append(objNode.Name);
                title.Append("</a></li>");
            }
            description.Append("<div class='sfHorTabContentHolder' id='sfHorTabContentHolder_");
            description.Append(count);
            description.Append("'><p>");
            description.Append(objNode.InnerText);
            description.Append("</p></div>");
            count++;
        }
        title.Append("</ul>");
        ltrPageHelp.Text = title.ToString() + description.ToString();
    }
}