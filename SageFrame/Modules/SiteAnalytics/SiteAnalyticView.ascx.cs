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
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.Framework;
using DashBoardControl;
using DashBoardControl.Info;
using iTextSharp.text;
using iTextSharp.text.pdf;
#endregion

public partial class Modules_SiteAnalytic_SiteAnalyticView : BaseAdministrationUserControl
{
    public int PortalID;
    public int ModuleID;
    public string StartDate = string.Empty;
    public string EndDate = string.Empty;
    public int Flag = 0;
    IPAddressToCountryResolver objIP = new IPAddressToCountryResolver();

    protected void Page_Load(object sender, EventArgs e)
    {

        
        SageFrameConfig objConfig = new SageFrameConfig();
        bool EnableSessionTracker = bool.Parse(objConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.EnableSessionTracker));
        if (EnableSessionTracker)
        {
            GetSetting();
            PortalID = GetPortalID;
            ModuleID = int.Parse(SageUserModuleID);
            LoadCss();
            Flag = 1;
        }
        else
        {
            divAnalytic.Visible = false;
            divMessage.Visible = true;
            lblMessage.InnerText = "Session tracker is not enable.";
            Flag = 0;
        }
    }
    public void LoadCss()
    {
        //Plugin css/JS
        IncludeCss("SiteAnalytics", "/Modules/SiteAnalytics/css/module.css");
        IncludeCss("SiteAnalytics", "/Modules/SiteAnalytics/css/jquery.jqplot.css");
        IncludeCss("SiteAnalytics", "/Modules/SiteAnalytics/syntaxhighlighter/styles/shCoreDefault.min.css");
        IncludeCss("SiteAnalytics", "/Modules/SiteAnalytics/syntaxhighlighter/styles/shThemejqPlot.min.css");
        IncludeJs("SiteAnalytics", "/Modules/SiteAnalytics/pjs/jquery.jqplot.min.js");
        IncludeJs("SiteAnalytics", "/Modules/SiteAnalytics/pjs/excanvas.min.js");
        IncludeJs("SiteAnalytics", "/Modules/SiteAnalytics/pjs/jqplot.barRenderer.min.js");
        IncludeJs("SiteAnalytics", "/Modules/SiteAnalytics/pjs/jqplot.pieRenderer.min.js");
        IncludeJs("SiteAnalytics", "/Modules/SiteAnalytics/pjs/jqplot.categoryAxisRenderer.js");
        IncludeJs("SiteAnalytics", "/Modules/SiteAnalytics/pjs/jqplot.pointLabels.min.js");
        IncludeJs("SiteAnalytics", "/Modules/SiteAnalytics/pjs/jqplot.meterGaugeRenderer.min.js");
        IncludeJs("SiteAnalytics", "/Modules/SiteAnalytics/pjs/jqplot.cursor.min.js");
        IncludeJs("SiteAnalytics", "/Modules/SiteAnalytics/pjs/jqplot.dateAxisRenderer.min.js");
        //Our JS
        IncludeJs("SiteAnalytics", "/Modules/SiteAnalytics/js/SiteAnalytic.js");
        IncludeJs("DashBoardControl", "/Modules/SiteAnalytics/js/PagingRef.js", "/Modules/SiteAnalytics/js/PagingVisited.js", "/Modules/SiteAnalytics/js/PagingBrowsed.js", "/Modules/SiteAnalytics/js/PagingCountry.js");
    }
    #region "Analytic Report Export To PDF And EXCEL"

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        string table = "";
        try
        {


            List<DashBoardSettingInfo> lstPages = DashBoardController.GetTopVisitedPage_Report(StartDate, EndDate);
            List<DashBoardSettingInfo> lstBrowser = DashBoardController.GetTopBrowser_Report(StartDate, EndDate);
            List<DashBoardSettingInfo> lstCountry = DashBoardController.GetTopVisitedCountry_Report(StartDate, EndDate);
            List<DashBoardSettingInfo> lstRefSite = DashBoardController.GetRefSite_Report(StartDate, EndDate);

            //Pages
            table += "<table><tr><th>Visited Page</th><th>Count</th></tr>";
            foreach (DashBoardSettingInfo objInfo in lstPages)
            {
                table += " <tr>";
                table += "<td>";
                table += objInfo.VistPageWithoutExtension;
                table += "</td>";
                table += "<td>";
                table += objInfo.VisitTime;
                table += "</td>";
                table += "</tr>";
            }
            table += "</table>";

            //Browser
            table += "<table><tr><th>Browser</th><th>Count</th></tr>";
            foreach (DashBoardSettingInfo objBrowserInfo in lstBrowser)
            {
                table += " <tr>";
                table += "<td>";
                table += objBrowserInfo.Browser;
                table += "</td>";
                table += "<td>";
                table += objBrowserInfo.VisitTime;
                table += "</td>";
                table += "</tr>";
            }
            table += "</table>";

            //Country
            table += "<table><tr><th>Country</th><th>Count</th></tr>";

            foreach (DashBoardSettingInfo objCountryInfo in lstCountry)
            {
                string CountryName = string.Empty;
                objIP.GetCountry(objCountryInfo.SessionUserHostAddress, out CountryName);
                if (CountryName == string.Empty)
                {
                    objCountryInfo.Country = objCountryInfo.SessionUserHostAddress;
                }
                else
                {
                    objCountryInfo.Country = CountryName;
                }

                table += " <tr>";
                table += "<td>";
                table += objCountryInfo.Country;
                table += "</td>";
                table += "<td>";
                table += objCountryInfo.VisitTime;
                table += "</td>";
                table += "</tr>";
            }
            table += "</table>";
            //Ref Sites 
            table += "<table><tr><th>References Sites</th><th>Count</th></tr>";
            foreach (DashBoardSettingInfo objRefInfo in lstRefSite)
            {
                table += " <tr>";
                table += "<td>";
                table += objRefInfo.RefPage;
                table += "</td>";
                table += "<td>";
                table += objRefInfo.VisitTime;
                table += "</td>";
                table += "</tr>";
            }
            table += "</table>";

            ExportToExcel(ref table, "Analytic-Report");
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void GetSetting()
    {

        List<DashBoardSettingInfo> lstSetting = DashBoardController.ListDashBoardSettingForView(int.Parse(SageUserModuleID), GetPortalID);
        foreach (DashBoardSettingInfo objInfo in lstSetting)
        {
            StartDate = objInfo.StartDate;
            EndDate = objInfo.EndDate;
        }

    }

    public void ExportToExcel(ref string table, string fileName)
     {
        table = table.Replace("&gt;", ">");
        table = table.Replace("&lt;", "<");
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".xls");
        HttpContext.Current.Response.ContentType = "application/x-msexcel";
        HttpContext.Current.Response.Write(table);
        HttpContext.Current.Response.End();
    }
    public void ExportToPDF()
    {
        List<DashBoardSettingInfo> lstPages = DashBoardController.GetTopVisitedPage_Report(StartDate, EndDate);
        List<DashBoardSettingInfo> lstBrowser = DashBoardController.GetTopBrowser_Report(StartDate, EndDate);
        List<DashBoardSettingInfo> lstCountry = DashBoardController.GetTopVisitedCountry_Report(StartDate, EndDate);
        List<DashBoardSettingInfo> lstRefSite = DashBoardController.GetRefSite_Report(StartDate, EndDate);
       
        HttpContext.Current.Response.ContentType = "application/pdf";
        HttpContext.Current.Response.AddHeader("content-disposition",
                                               "attachment;filename=" + "CountryVisitCount_" +
                                               DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".pdf");
        Document doc = new Document(iTextSharp.text.PageSize.A4, 0, 0, 20, 20);
        PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Current.Response.OutputStream);
        doc.Open();

        var bodyFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL, new BaseColor(0, 0, 0));
        var cellHeadingColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#E3EDFA"));

        PdfPCell ClearCell = new PdfPCell();
        ClearCell.Border = Rectangle.NO_BORDER;
        ClearCell.Colspan = 2;

        //Page

        PdfPTable tblPages;
        PdfPTable tblPage = new PdfPTable(2);

        tblPages = tblPage;

        var ReportHeaderFont = FontFactory.GetFont(FontFactory.COURIER_BOLD, 14, Font.NORMAL, new BaseColor(125, 88, 15));

        PdfPCell AnalyticReport = new PdfPCell();
        AnalyticReport.Colspan = 2;
        Paragraph AnalyticHeader = new Paragraph("Site Analytic Report", ReportHeaderFont);
        AnalyticReport.AddElement(AnalyticHeader);
        AnalyticHeader.Alignment = Element.ALIGN_CENTER;
        AnalyticReport.Border = Rectangle.NO_BORDER;
        tblPages.AddCell(AnalyticReport);
        tblPage.AddCell(ClearCell);

        var CellHeader = FontFactory.GetFont(FontFactory.COURIER_BOLD, 12, Font.NORMAL, new BaseColor(0, 0, 0));

        var PageHeaderFont = FontFactory.GetFont(FontFactory.COURIER_BOLD, 12, Font.NORMAL, new BaseColor(80, 160, 191));
        PdfPCell blankCell = new PdfPCell();
        blankCell.Colspan = 2;
        Paragraph PageHeader = new Paragraph("Page wise visit", PageHeaderFont);
        blankCell.AddElement(PageHeader);
        blankCell.Border = Rectangle.NO_BORDER;
        tblPages.AddCell(blankCell);


        PdfPTable PageHeaderTbl = new PdfPTable(2);
        PageHeaderTbl.SetWidths(new int[2] { 10, 15 });
        PageHeaderTbl.TotalWidth = doc.PageSize.Width;


        Paragraph headingPage = new Paragraph("Page", CellHeader);
        PdfPCell PageDateCell = new PdfPCell(headingPage);
        PageDateCell.HorizontalAlignment = Element.ALIGN_CENTER;
        tblPages.AddCell(PageDateCell);

        Paragraph HeadingPageCount = new Paragraph("Count", CellHeader);
        PdfPCell PageCount = new PdfPCell(HeadingPageCount);
        PageCount.HorizontalAlignment = Element.ALIGN_CENTER;
        tblPages.AddCell(PageCount);



        foreach (DashBoardSettingInfo objPageinfo in lstPages)
        {
            var PageCell = new PdfPCell(new Paragraph(objPageinfo.VistPageWithoutExtension, bodyFont));
            PageCell.BackgroundColor = cellHeadingColor;
            PageCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblPages.AddCell(PageCell);

            var CountCell = new PdfPCell(new Paragraph(objPageinfo.VisitTime, bodyFont));
            CountCell.BackgroundColor = cellHeadingColor;
            CountCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblPages.AddCell(CountCell);
        }

        tblPage.AddCell(ClearCell);
        tblPage.AddCell(ClearCell);
        doc.Add(tblPages);



        //Browser wise visit
        PdfPTable tblBrowsers;
        PdfPTable tblBrowser = new PdfPTable(2);
        tblBrowsers = tblBrowser;

        var BrowserBodyFont = FontFactory.GetFont(FontFactory.COURIER_BOLD, 12, Font.NORMAL, new BaseColor(80, 160, 191));
        PdfPCell BrowserblankCell = new PdfPCell();
        BrowserblankCell.Colspan = 2;
        Paragraph HeadingBrowser = new Paragraph("Browser wise visit", BrowserBodyFont);
        BrowserblankCell.AddElement(HeadingBrowser);
        BrowserblankCell.Border = Rectangle.NO_BORDER;
        tblBrowsers.AddCell(BrowserblankCell);



        PdfPTable BrowserHeaderTbl = new PdfPTable(2);
        PageHeaderTbl.SetWidths(new int[2] { 10, 15 });
        PageHeaderTbl.TotalWidth = doc.PageSize.Width;

        Paragraph headingPara = new Paragraph("Browser", CellHeader);
        PdfPCell BrowserHeading = new PdfPCell(headingPara);
        BrowserHeading.HorizontalAlignment = Element.ALIGN_CENTER;
        BrowserHeading.PaddingLeft = 15f;
        tblBrowsers.AddCell(BrowserHeading);

        Paragraph BrowserCount = new Paragraph("Count", CellHeader);
        PdfPCell BCount = new PdfPCell(BrowserCount);
        BCount.HorizontalAlignment = Element.ALIGN_CENTER;
        BCount.PaddingLeft = 50f;
        tblBrowsers.AddCell(BCount);



        foreach (DashBoardSettingInfo objBrowserinfo in lstBrowser)
        {
            var BrowserCell = new PdfPCell(new Paragraph(objBrowserinfo.Browser, bodyFont));
            BrowserCell.BackgroundColor = cellHeadingColor;
            BrowserCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblBrowsers.AddCell(BrowserCell);

            var BrowserCountCell = new PdfPCell(new Paragraph(objBrowserinfo.VisitTime, bodyFont));
            BrowserCountCell.HorizontalAlignment = Element.ALIGN_CENTER;
            BrowserCountCell.BackgroundColor = cellHeadingColor;
            tblBrowsers.AddCell(BrowserCountCell);
        }
        tblBrowsers.AddCell(ClearCell);
        tblBrowsers.AddCell(ClearCell);
        doc.Add(tblBrowsers);


        //Country
        PdfPTable tblCountrys;
        PdfPTable tblCountry = new PdfPTable(2);
        tblCountrys = tblCountry;

        var CountryBodyFont = FontFactory.GetFont(FontFactory.COURIER_BOLD, 12, Font.NORMAL, new BaseColor(80, 160, 191));
        PdfPCell CountryblankCell = new PdfPCell();
        CountryblankCell.Colspan = 2;
        Paragraph HeadingCountry = new Paragraph("Country wise visit", CountryBodyFont);
        CountryblankCell.AddElement(HeadingCountry);
        CountryblankCell.Border = Rectangle.NO_BORDER;
        tblCountrys.AddCell(CountryblankCell);



        PdfPTable CountryHeaderTbl = new PdfPTable(2);
        PageHeaderTbl.SetWidths(new int[2] { 10, 15 });
        PageHeaderTbl.TotalWidth = doc.PageSize.Width;

        Paragraph CountryPara = new Paragraph("Country", CellHeader);
        PdfPCell CountryHeading = new PdfPCell(CountryPara);
        CountryHeading.HorizontalAlignment = Element.ALIGN_CENTER;
        tblCountrys.AddCell(CountryHeading);

        Paragraph CountryCount = new Paragraph("Count", CellHeader);
        PdfPCell CCount = new PdfPCell(CountryCount);
        CCount.HorizontalAlignment = Element.ALIGN_CENTER;
        tblCountrys.AddCell(CCount);



        foreach (DashBoardSettingInfo objCountryInfo in lstCountry)
        {
            string CountryName = string.Empty;
            objIP.GetCountry(objCountryInfo.SessionUserHostAddress, out CountryName);
            if (CountryName == string.Empty)
            {
                objCountryInfo.Country = objCountryInfo.SessionUserHostAddress;
            }
            else
            {
                objCountryInfo.Country = CountryName;
            }
            var CountryCell = new PdfPCell(new Paragraph(objCountryInfo.Country, bodyFont));
            CountryCell.BackgroundColor = cellHeadingColor;
            CountryCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblCountrys.AddCell(CountryCell);

            var CountryVisitCell = new PdfPCell(new Paragraph(objCountryInfo.VisitTime, bodyFont));
            CountryVisitCell.HorizontalAlignment = Element.ALIGN_CENTER;
            CountryVisitCell.BackgroundColor = cellHeadingColor;
            tblCountrys.AddCell(CountryVisitCell);
        }
        tblCountrys.AddCell(ClearCell);
        tblCountrys.AddCell(ClearCell);
        doc.Add(tblCountrys);

        //References Sites

        PdfPTable tblRefSites;
        PdfPTable tblRefSite = new PdfPTable(2);
        tblRefSites = tblRefSite;

        var RefBodyFont = FontFactory.GetFont(FontFactory.COURIER_BOLD, 12, Font.NORMAL, new BaseColor(80, 160, 191));
        PdfPCell RefblankCell = new PdfPCell();
        RefblankCell.Colspan = 2;
        Paragraph HeadingRef = new Paragraph("Reference pages", RefBodyFont);
        RefblankCell.AddElement(HeadingRef);
        RefblankCell.Border = Rectangle.NO_BORDER;
        tblRefSites.AddCell(RefblankCell);



        PdfPTable RefHeaderTbl = new PdfPTable(2);
        PageHeaderTbl.SetWidths(new int[2] { 10, 15 });
        PageHeaderTbl.TotalWidth = doc.PageSize.Width;

        Paragraph RefPara = new Paragraph("Reference pages", CellHeader);
        PdfPCell RefHeading = new PdfPCell(RefPara);
        RefHeading.HorizontalAlignment = Element.ALIGN_CENTER;
        tblRefSites.AddCell(RefHeading);

        Paragraph RefCount = new Paragraph("Count", CellHeader);
        PdfPCell RefParaCount = new PdfPCell(RefCount);
        RefParaCount.HorizontalAlignment = Element.ALIGN_CENTER;
        tblRefSites.AddCell(RefParaCount);



        foreach (DashBoardSettingInfo objRefInfo in lstRefSite)
        {
            var RefCell = new PdfPCell(new Paragraph(objRefInfo.RefPage, bodyFont));
            RefCell.HorizontalAlignment = Element.ALIGN_CENTER;
            RefCell.BackgroundColor = cellHeadingColor;
            RefCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tblRefSites.AddCell(RefCell);

            var RefVisitCell = new PdfPCell(new Paragraph(objRefInfo.VisitTime, bodyFont));
            RefVisitCell.HorizontalAlignment = Element.ALIGN_CENTER;
            RefVisitCell.BackgroundColor = cellHeadingColor;
            tblRefSites.AddCell(RefVisitCell);
        }
        tblRefSites.AddCell(ClearCell);
        tblRefSites.AddCell(ClearCell);
        doc.Add(tblRefSites);

        doc.Close();
    }

    protected void btnExportToPDF_Click(object sender, EventArgs e)
    {
        ExportToPDF();
    }

    #endregion
}