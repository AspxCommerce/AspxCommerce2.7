using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxAdminDashBoard_UserRecords : BaseAdministrationUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
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

                IncludeCss("StoreStaticsDisplay", "/Templates/" + TemplateName + "/css/Charts/basic.css", "/Templates/" + TemplateName + "/css/Charts/visualize-dark.css", "/Templates/" + TemplateName + "/css/Charts/visualize.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css");
                IncludeJs("StoreStaticsDisplay", "/js/SageFrameCorejs/excanvas.js", "/js/GraphChart/visualize.jQuery.js", "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js", "/Modules/AspxCommerce/AspxAdminDashBoard/js/StoreStaticsDisplay.js");


            }
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}
