<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopFiveCountry.ascx.cs"
    Inherits="Modules_SiteAnalytics_TopFiveCountry" %>

<script type="text/javascript">
    $(function () {
        var Flag = '<%=Flag %>';
        if (Flag == 1) {
            var BrowserWiseVisit = [];
            var data = '<%=topCountry %>';
            var dataArray = data.split(',');
            var length = dataArray.length;

            for (var i = 0; i < dataArray.length; i = i + 2) {
                var arrVisit = [];
                arrVisit.push(dataArray[i]);
                arrVisit.push(parseInt(dataArray[i + 1]));
                BrowserWiseVisit.push(arrVisit);
            }
            $('#BrowserWiseVisit').html('');
            plot1 = jQuery.jqplot('BrowserWiseVisit', [BrowserWiseVisit], {
                title: ' ',
                seriesDefaults: {
                    renderer: jQuery.jqplot.PieRenderer,
                    rendererOptions: { showDataLabels: true, varyBarColor: true },
                    shadow: false,   // show shadow or not.
                    shadowAngle: 360,    // angle (degrees) of the shadow, clockwise from x axis.
                    shadowOffset: 5, // offset from the line of the shadow.
                    shadowDepth: 0    // Number of strokes to make when drawing shadow.  Each

                },
                legend: { show: true },
                cursor: { show: false },
                seriesColors: ["#8781bd", "#7da7d9", "#deabaa", "#bd8dbf", "#6dcff6"],
                grid: {
                    drawGridLines: true,        // wether to draw lines across the grid or not.
                    gridLineColor: '#cccccc',   // CSS color spec of the grid lines.
                    background: 'transparent',      // CSS color spec for background color of grid.
                    borderColor: '#ff0000',     // CSS color spec for border around grid.
                    borderWidth: 0,           // pixel width of border around grid.
                    shadow: false,               // draw a shadow for grid.
                    shadowAngle: 0,            // angle of the shadow.  Clockwise from x axis.
                    shadowOffset: 1.5,          // offset from the line of the shadow.
                    shadowWidth: 3,             // width of the stroke for the shadow.
                    shadowDepth: 3
                }
            });

        }
    });
</script>
<div id="divtopFiveCountry" runat="server">
<div class="topFiveCountry">
    <h2>
        Top 5 Country Wise Visit</h2>
</div>
<div class='sfCountryHolder'>
    <div id="BrowserWiseVisit" style="margin-top: 20px; margin-left: 20px; width: 320px;
        height: 300px; float: left">
    </div>
    <div class="sftotalVisitor clearfix">
        <asp:Literal runat="server" ID="ltrTotal"></asp:Literal>
        <asp:HyperLink runat="server" ID="hyLnkSiteAnalytics" Text="View Full Statistics" CssClass="icon-chart sfBtn"></asp:HyperLink>
    </div>
</div>
    </div>
<div id="divMsg" runat="server" visible="false">

    <label id="lblMsg" runat="server"></label>
</div>
