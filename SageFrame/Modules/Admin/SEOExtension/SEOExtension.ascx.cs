#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using SageFrame.Web;
using System.Text;
using SageFrame.SEOExtension;
using SageFrame.Shared;
using SageFrame.MenuManager;
using System.Net;
using SageFrame.Pages;
using SageFrame.Common;
#endregion

public partial class Modules_Admin_SEOExtension_SEOExtension : BaseAdministrationUserControl
{
    string ChangeFreuency;
    string PriorityValues;
    XmlDocument xd = null;
    SiteMapDataProvider objSitemap = new SiteMapDataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptInclude("Validate", ResolveUrl("~/js/jquery.validate.js"));
        if (!IsPostBack)
        {
            LoadGrid();
        }
        try
        {
            if (!IsPostBack)
            {
                BindData();
                ChekAllCheckBox();
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    private void ChekAllCheckBox()
    {
        foreach (ListItem li in chkChoice.Items)
        {
            li.Selected = true;
        }
        foreach (ListItem li in chkSubmitSitemap.Items)
        {
            li.Selected = true;
        }
    }
    private void AddImageUrls()
    {
        //imbSave.ImageUrl = GetTemplateImageUrl("btnudate.png", true);
        //imbRefresh.ImageUrl = GetTemplateImageUrl("imgrefresh.png", true);
    }

    private void SaveSettings()
    {
        try
        {
            if (txtvalue.Text.Trim() != string.Empty)
            {
                SettingProvider sp = new SettingProvider();
                sp.GoogleAnalyticsAddUpdate(txtvalue.Text, chkIsActive.Checked, GetPortalID, GetUsername);
                HttpRuntime.Cache.Remove(CacheKeys.SageGoogleAnalytics);
                AlertUpdate();
                BindData();
            }
            else
            {
                ShowMessage("", GetSageMessage("WebAnalytics", "PleaseFillJSCodeProvidedByGoogle"), "", SageMessageType.Alert);
            }

        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void AlertUpdate()
    {
        ShowMessage("", GetSageMessage("WebAnalytics", "GoogleAnalyticsUpdatedSuccessfully"), "", SageMessageType.Success);
    }

    private void BindData()
    {
        try
        {
            txtvalue.Text = "";
            SettingProvider sp = new SettingProvider();
            GoogleAnalyticsInfo objGA = new GoogleAnalyticsInfo();
            objGA = sp.GetGoogleAnalyticsByPortalID(GetPortalID);
            if (objGA != null && objGA.GoogleJSCode != null)
            {
                txtvalue.Text = objGA.GoogleJSCode;
                chkIsActive.Checked = objGA.IsActive;
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }

    }

    protected void imbSave_Click(object sender, EventArgs e)
    {
        SaveSettings();
    }

    protected void imbRefresh_Click(object sender, EventArgs e)
    {
        BindData();
    }
    private void GenerateSitemap()
    {
        if (!File.Exists(HttpRuntime.AppDomainAppPath + "sitemap_" + GetPortalID + ".xml"))
        {
            GenerateSitemapIndex();
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpRuntime.AppDomainAppPath + "sitemap_index.xml");
            XmlNodeList nodeList = doc.GetElementsByTagName("lastmod");
            foreach (XmlNode xmlNode in nodeList)
            {
                xmlNode.InnerText = DateTime.Now.ToString();
            }
        }
        string sSiteMapFilePath = HttpRuntime.AppDomainAppPath + "sitemap_" + GetPortalID + ".xml";
        List<SitemapInfo> items = MenuManagerDataController.GetSiteMapPages(GetUsername, GetCurrentCultureName);
        FileInfo fi = new FileInfo(sSiteMapFilePath);

        xd = new XmlDocument();
        XmlNode rootNode = xd.CreateElement("urlset");

        XmlAttribute attrXmlNS = xd.CreateAttribute("xmlns");
        attrXmlNS.InnerText = "http://www.sitemaps.org/schemas/sitemap/0.9";
        rootNode.Attributes.Append(attrXmlNS);

        ChangeFreuency = ddlChangeFrequency.SelectedItem.ToString();
        PriorityValues = ddlPriorityValues.SelectedItem.ToString();

        XmlTextWriter writer = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
        foreach (SitemapInfo info in items)
        {
            DateTime Updated;
            if (info.UpdatedOn == null || info.UpdatedOn == "")
            {
                Updated = DateTime.Parse(info.AddedOn);
            }
            else
            {
                Updated = DateTime.Parse(info.UpdatedOn);
            }
            //Valid option for changefreq:(always,hourly,daily,weekly,monthly,yearly,never)
            //Valid priority values have range interval [0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0].
            string urlpath = info.PortalID > 1 ? string.Format("/portal/{0}{1}", info.PortalName, info.TabPath) : info.TabPath;
            rootNode.AppendChild(GenerateUrlNode(Page.Request.Url.Scheme + "://" + Request.Url.Authority + GetApplicationName + urlpath + SageFrameSettingKeys.PageExtension, Updated, ChangeFreuency, PriorityValues));
        }
        xd.AppendChild(rootNode);
        xd.InsertBefore(xd.CreateXmlDeclaration("1.0", "UTF-8", null), rootNode);
        xd.Save(sSiteMapFilePath);


    }

    private void GenerateSitemapIndex()
    {
        string sSiteMapFilePath = HttpRuntime.AppDomainAppPath + "sitemap_index.xml";
        if (!File.Exists(HttpRuntime.AppDomainAppPath + "sitemap_index.xml"))
        {

            List<SitemapInfo> items = MenuManagerDataController.GetSiteMapPages(GetUsername, GetCurrentCultureName);
            FileInfo fi = new FileInfo(sSiteMapFilePath);

            xd = new XmlDocument();
            XmlNode rootNode = xd.CreateElement("urlset");

            XmlAttribute attrXmlNS = xd.CreateAttribute("xmlns");
            attrXmlNS.InnerText = "http://www.sitemaps.org/schemas/sitemap/0.9";
            rootNode.Attributes.Append(attrXmlNS);

            XmlTextWriter writer = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
            rootNode.AppendChild(GenerateIndexNode(Page.Request.Url.Scheme + "://" + Request.Url.Authority + GetApplicationName + "/sitemap_" + GetPortalID + ".xml"));

            xd.AppendChild(rootNode);
            xd.InsertBefore(xd.CreateXmlDeclaration("1.0", "UTF-8", null), rootNode);
            xd.Save(sSiteMapFilePath);
        }
        else
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpRuntime.AppDomainAppPath + "sitemap_index.xml");
            XmlNode node = GenerateIndexNode(Page.Request.Url.Scheme + "://" + Request.Url.Authority + GetApplicationName + "/sitemap_" + GetPortalID + ".xml");
            XmlNode childNode = xmlDoc.DocumentElement;
            childNode.InsertAfter(node, childNode.LastChild);
            xmlDoc.Save(sSiteMapFilePath);


        }



    }

    public XmlNode GenerateIndexNode(string Loc)
    {
        XmlNode nodeSite = xd.CreateElement("sitemap");
        XmlNode nodeLoc = xd.CreateElement("loc");
        nodeLoc.InnerText = Loc;
        XmlNode nodeMode = xd.CreateElement("lastmod");
        nodeMode.InnerText = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss+00:00");
        nodeSite.AppendChild(nodeLoc);
        nodeSite.AppendChild(nodeMode);
        return nodeSite;

    }

    private void SubmitSitemap(string PortalName)
    {
        string RootPath = Page.Request.Url.Scheme + "://" + Request.Url.Authority + GetApplicationName;

        if (!File.Exists(HttpRuntime.AppDomainAppPath + "sitemap_index.xml"))
        {
            GenerateSitemap();
        }
        bool bolSelectionMade = false;
        foreach (ListItem item in chkSubmitSitemap.Items)
        {
            if (item.Selected == true)
            {
                bolSelectionMade = true;
            }
        }

        //PING SEARCH ENGINES TO LET THEM KNOW WE CRATE/UPDATED OUR SITEMAP

        //resubmit to google
        if (bolSelectionMade == true)
        {
            int flag = 0;
            string res = string.Empty;
            StringBuilder sb = new StringBuilder();
            StringBuilder er = new StringBuilder();
            foreach (ListItem li in chkSubmitSitemap.Items)
            {
                if (li.Selected == true && li.Value == "Google")
                {
                    try
                    {
                        System.Net.WebRequest reqGoogle = System.Net.WebRequest.Create("http://www.google.com/webmasters/tools/ping?sitemap=" + HttpUtility.UrlEncode(RootPath + "/sitemap_index.xml"));
                        reqGoogle.GetResponse();

                        HttpWebResponse resp = (HttpWebResponse)reqGoogle.GetResponse();
                        sb.Append(resp.StatusCode == HttpStatusCode.OK ? "Sitemap received by Google, " : "Couldn't submiting sitemap to Google<br>").ToString();
                    }
                    catch (Exception)
                    {
                        er.Append("Google");
                        flag = 1;
                    }
                }

                if (li.Selected == true && li.Value == "Ask")
                {
                    //resubmit to ask
                    try
                    {
                        System.Net.WebRequest reqAsk = System.Net.WebRequest.Create("http://submissions.ask.com/ping?sitemap=" + HttpUtility.UrlEncode(RootPath + "/sitemap_index.xml"));
                        reqAsk.GetResponse();
                        HttpWebResponse resp = (HttpWebResponse)reqAsk.GetResponse();
                        sb.Append(resp.StatusCode == HttpStatusCode.OK ? "Sitemap received by Ask. " : "Couldn't submiting sitemap to Ask<br>").ToString();
                    }
                    catch (Exception)
                    {
                        er.Append("Ask");
                        flag = 1;
                    }
                }

                if (li.Selected == true && li.Value == "Yahoo")
                {
                    //resubmit to yahoo
                    try
                    {
                        System.Net.WebRequest reqYahoo = System.Net.WebRequest.Create("http://search.yahooapis.com/SiteExplorerService/V1/updateNotification?appid=YahooDemo&url=" + HttpUtility.UrlEncode(RootPath + "/sitemap_index.xml"));
                        reqYahoo.GetResponse();
                        HttpWebResponse resp = (HttpWebResponse)reqYahoo.GetResponse();
                        sb.Append(resp.StatusCode == HttpStatusCode.OK ? "Sitemap received by Yahoo. " : "Couldn't submiting sitemap to Yahoo<br>").ToString();
                    }
                    catch (Exception)
                    {
                        er.Append("Yahoo");
                        flag = 1;
                    }
                }
                if (li.Selected == true && li.Value == "Bing")
                {
                    //resubmit to bing
                    try
                    {
                        System.Net.WebRequest reqBing = System.Net.WebRequest.Create("http://www.bing.com/webmaster/ping.aspx?siteMap=" + HttpUtility.UrlEncode(RootPath + "/sitemap_index.xml"));
                        reqBing.GetResponse();
                        HttpWebResponse resp = (HttpWebResponse)reqBing.GetResponse();
                        sb.Append(resp.StatusCode == HttpStatusCode.OK ? "Sitemap received by Bing.<br/>" : "Couldn't submiting sitemap to Bing<br/>").ToString();
                    }
                    catch (Exception)
                    {
                        er.Append("Bing");
                        flag = 1;
                    }
                }
            }
            if (flag == 1)
            {
                res = "Unable to submit sitemap to " + er.ToString() + ".Check Your Connection. ";
                string message = sb.ToString() + res;
                ShowMessage("", "", message, SageMessageType.Alert);
            }
            else
            {
                string message = sb.ToString();
                ShowMessage("", "", message, SageMessageType.Success);
            }
        }
        else
        {
            string message = "Please select atleast one search engine.";
            ShowMessage("", "", message, SageMessageType.Alert);
        }
    }

    public XmlNode GenerateUrlNode(string Loc, DateTime LastMod, string ChangeFreq, string Priority)
    {
        try
        {
            XmlNode nodeUrl = xd.CreateElement("url");
            XmlNode nodeLoc = xd.CreateElement("loc");
            nodeLoc.InnerText = Loc;
            nodeUrl.AppendChild(nodeLoc);
            XmlNode nodeLastMod = xd.CreateElement("lastmod");
            nodeLastMod.InnerText = LastMod.ToString("yyyy-MM-ddThh:mm:ss+00:00");
            nodeUrl.AppendChild(nodeLastMod);
            XmlNode nodeChangeFreq = xd.CreateElement("changefreq");
            nodeChangeFreq.InnerText = ChangeFreq;
            nodeUrl.AppendChild(nodeChangeFreq);
            XmlNode nodePriority = xd.CreateElement("priority");
            nodePriority.InnerText = Priority;
            nodeUrl.AppendChild(nodePriority);
            return nodeUrl;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnGenerateSitemap_Click(object sender, EventArgs e)
    {
        GenerateSitemap();
        string message = "Sitemap Generated Successfully!";
        ShowMessage("", "", message, SageMessageType.Success);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SubmitSitemap(GetPortalSEOName);
    }

    protected void btnGenerateRobots_Click(object sender, EventArgs e)
    {
        generateRobots();

    }

    private void LoadGrid()
    {
        gdvRobots.DataSource = RobotsController.GetRobots(GetPortalID);
        gdvRobots.DataBind();
    }

    private void generateRobots()
    {
        RobotsController.DeleteExistingRobots(GetPortalID);
        List<RobotsInfo> items = RobotsController.GetRobots(GetPortalID);
        int flag = 0;
        string TabPath = string.Empty;
        foreach (ListItem chkitem in chkChoice.Items)
        {
            if (chkitem.Selected == false)
            {
                flag++;
            }
            else if (chkitem.Selected == true && chkitem.Text == "Google")
            {
                foreach (GridViewRow gvRow in gdvRobots.Rows)
                {
                    CheckBox chk = (CheckBox)gvRow.FindControl("chkPath");
                    if (chk.Checked)
                    {


                        Label lblTabPath = (Label)gvRow.FindControl("lblTabPath");
                        TabPath = lblTabPath.Text;
                        if (!IsParent)
                        {
                            TabPath = "/portal/" + GetPortalSEOName + TabPath;
                        }
                        RobotsController.SaveRobotsPage(GetPortalID, "Googlebot", TabPath);


                    }
                }


            }
            else if (chkitem.Selected == true && chkitem.Text == "Yahoo")
            {

                foreach (GridViewRow gvRow in gdvRobots.Rows)
                {
                    CheckBox chk = (CheckBox)gvRow.FindControl("chkPath");
                    if (chk.Checked)
                    {
                        Label lblTabPath = (Label)gvRow.FindControl("lblTabPath");
                        TabPath = lblTabPath.Text;
                        if (!IsParent)
                        {
                            TabPath = "/portal/" + GetPortalSEOName + TabPath;
                        }
                        RobotsController.SaveRobotsPage(GetPortalID, "Slurp", TabPath);

                    }
                }

            }
            else if (chkitem.Selected == true && chkitem.Text == "Msn")
            {

                foreach (GridViewRow gvRow in gdvRobots.Rows)
                {
                    CheckBox chk = (CheckBox)gvRow.FindControl("chkPath");
                    if (chk.Checked)
                    {
                        Label lblTabPath = (Label)gvRow.FindControl("lblTabPath");
                        TabPath = lblTabPath.Text;
                        if (!IsParent)
                        {
                            TabPath = "/portal/" + GetPortalSEOName + TabPath;
                        }

                        RobotsController.SaveRobotsPage(GetPortalID, "msnbot", TabPath);

                    }
                }

            }
            else if (chkitem.Selected == true && chkitem.Text == "Bing")
            {

                foreach (GridViewRow gvRow in gdvRobots.Rows)
                {
                    CheckBox chk = (CheckBox)gvRow.FindControl("chkPath");
                    if (chk.Checked)
                    {
                        Label lblTabPath = (Label)gvRow.FindControl("lblTabPath");
                        TabPath = lblTabPath.Text;
                        if (!IsParent)
                        {
                            TabPath = "/portal/" + GetPortalSEOName + TabPath;
                        }
                        RobotsController.SaveRobotsPage(GetPortalID, "bingbot", TabPath);
                    }
                }

            }
            if (flag == chkChoice.Items.Count)
            {

                string message = "Select Searchengine";
                ShowMessage("", message, "", SageMessageType.Alert);

            }
            else
            {
                WriteRobots();
                string message = "Robots Generated Successfully";
                ShowMessage("", message, "", SageMessageType.Success);
            }

        }

    }
    public void WriteRobots()
    {
        try
        {
            if (GetPortalID == 1)
            {
                System.IO.StreamWriter objStreamWriter =
                 new System.IO.StreamWriter(HttpRuntime.AppDomainAppPath + "\\robots.txt");
                objStreamWriter.WriteLine("User-agent: Googlebot\n");
                List<RobotsInfo> lstGooglebot = RobotsController.GenerateRobots("Googlebot");
                foreach (RobotsInfo objinfo in lstGooglebot)
                {
                    objStreamWriter.WriteLine("Disallow: " + objinfo.PagePath + "/" + "\n");
                }
                objStreamWriter.WriteLine("User-agent: Slurp\n");
                List<RobotsInfo> lstSlurp = RobotsController.GenerateRobots("Slurp");
                foreach (RobotsInfo objinfo in lstSlurp)
                {
                    objStreamWriter.WriteLine("Disallow: " + objinfo.PagePath + "/" + "\n");
                }
                objStreamWriter.WriteLine("User-agent: msnbot\n");
                List<RobotsInfo> lstmsnbot = RobotsController.GenerateRobots("msnbot");
                foreach (RobotsInfo objinfo in lstmsnbot)
                {
                    objStreamWriter.WriteLine("Disallow: " + objinfo.PagePath + "/" + "\n");
                }
                objStreamWriter.WriteLine("User-agent: bingbot\n");
                List<RobotsInfo> lstbingbot = RobotsController.GenerateRobots("bingbot");
                foreach (RobotsInfo objinfo in lstbingbot)
                {
                    objStreamWriter.WriteLine("Disallow: " + objinfo.PagePath + "/" + "\n");
                }
                objStreamWriter.Close();
            }
            else
            {

                System.IO.StreamWriter objStreamWriter =
                      new System.IO.StreamWriter(HttpRuntime.AppDomainAppPath + "\\robots." + GetPortalID + ".txt");
                objStreamWriter.WriteLine("User-agent: Googlebot\n");
                List<RobotsInfo> lstGooglebot = RobotsController.GenerateRobots("Googlebot");
                foreach (RobotsInfo objinfo in lstGooglebot)
                {
                    objStreamWriter.WriteLine("Disallow: " + objinfo.PagePath + "/" + "\n");
                }
                objStreamWriter.WriteLine("User-agent: Slurp\n");
                List<RobotsInfo> lstSlurp = RobotsController.GenerateRobots("Slurp");
                foreach (RobotsInfo objinfo in lstSlurp)
                {
                    objStreamWriter.WriteLine("Disallow: " + objinfo.PagePath + "/" + "\n");
                }
                objStreamWriter.WriteLine("User-agent: msnbot\n");
                List<RobotsInfo> lstmsnbot = RobotsController.GenerateRobots("msnbot");
                foreach (RobotsInfo objinfo in lstmsnbot)
                {
                    objStreamWriter.WriteLine("Disallow: " + objinfo.PagePath + "/" + "\n");
                }
                objStreamWriter.WriteLine("User-agent: bingbot\n");
                List<RobotsInfo> lstbingbot = RobotsController.GenerateRobots("bingbot");
                foreach (RobotsInfo objinfo in lstbingbot)
                {
                    objStreamWriter.WriteLine("Disallow: " + objinfo.PagePath + "/" + "\n");
                }
                objStreamWriter.Close();
            }
        }
        catch (Exception)
        {

            throw;
        }


    }
}
