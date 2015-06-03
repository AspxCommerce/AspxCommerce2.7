/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxAdminDashBoard_StoreStaticsDisplay : BaseAdministrationUserControl
{
    public int StoreID, PortalID;
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
                StoreID = GetStoreID;
                PortalID = GetPortalID;
            }
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {      
        
    }
}
