/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace SFE.GoogleAdUnit
{
    /// <summary>
    /// Internal class for Adverisement unit globals.
    /// </summary>
    internal class AdUnitGlobals
    {
        internal static readonly String m_strAdUnitUrl = "http://pagead2.googlesyndication.com/pagead/show_ads.js";
        internal static Dictionary<AdUnitFormat, UnitDimensions> g_AdUnitDimensions;
		internal static Dictionary<LinkUnitFormat, UnitDimensions> g_LinkUnitDimensions;
        internal static String g_strAdUnitDesigntimeRender;
        internal static StringBuilder g_strLinkUnitDesigntimeRender_V;
        internal static StringBuilder g_strLinkUnitDesigntimeRender_H;

        /// <summary>
        /// Initializes an instance of AdUnitGlobals class.
        /// </summary>
        static AdUnitGlobals()
        {
            g_AdUnitDimensions = new Dictionary<AdUnitFormat, UnitDimensions>();

			g_AdUnitDimensions.Add(AdUnitFormat.LeaderBoard_728x90_H, new UnitDimensions(728, 90));
			g_AdUnitDimensions.Add(AdUnitFormat.Banner_468x60_H, new UnitDimensions(468, 60));
			g_AdUnitDimensions.Add(AdUnitFormat.HalfBanner_234x60_H, new UnitDimensions(234, 60));
			g_AdUnitDimensions.Add(AdUnitFormat.SkyScapper_120x600_V, new UnitDimensions(120, 600));
			g_AdUnitDimensions.Add(AdUnitFormat.WideSkyScrapper_160x600_V, new UnitDimensions(160, 600));
			g_AdUnitDimensions.Add(AdUnitFormat.VerticalBanner_120x240_V, new UnitDimensions(120, 240));
			g_AdUnitDimensions.Add(AdUnitFormat.Button_125x125_S, new UnitDimensions(125, 125));
			g_AdUnitDimensions.Add(AdUnitFormat.MediumRectangle_300x250_S, new UnitDimensions(300, 250));
			g_AdUnitDimensions.Add(AdUnitFormat.Square_250x250_S, new UnitDimensions(250, 250));
			g_AdUnitDimensions.Add(AdUnitFormat.LargeRectangle_336x280_S, new UnitDimensions(336, 280));
			g_AdUnitDimensions.Add(AdUnitFormat.SmallRectangle_180x150_S, new UnitDimensions(180, 150));

            g_LinkUnitDimensions = new Dictionary<LinkUnitFormat, UnitDimensions>();

            g_LinkUnitDimensions.Add(LinkUnitFormat.H_728x15, new UnitDimensions(728, 15));
            g_LinkUnitDimensions.Add(LinkUnitFormat.H_468x15, new UnitDimensions(468, 15));
            g_LinkUnitDimensions.Add(LinkUnitFormat.S_200x90, new UnitDimensions(200, 90));
            g_LinkUnitDimensions.Add(LinkUnitFormat.S_180x90, new UnitDimensions(180, 90));
            g_LinkUnitDimensions.Add(LinkUnitFormat.S_160x90, new UnitDimensions(160, 90));
            g_LinkUnitDimensions.Add(LinkUnitFormat.S_120x90, new UnitDimensions(120, 90));

			// Set design time tags to render.
			g_strAdUnitDesigntimeRender = "<div style=\"width:{0}px;height={1};border:1 solid {2};color:{3};background-color:{4};\">";
			g_strAdUnitDesigntimeRender += "<table cellspacing='0px' cellpadding='0px' width='100%'><tr><td width='100%' bgcolor='{2}'>Ads By Google</td></tr><tr><td>{5}</td></tr><tr width='100%' valign='bottom'><td width='50%'>Ads by google</td></tr></table>";
			g_strAdUnitDesigntimeRender += "</div>";

            g_strLinkUnitDesigntimeRender_V = new StringBuilder();
            g_strLinkUnitDesigntimeRender_V.Append("<div style=\"width:{0}px;height={1};border:1 solid {2};color:{3};background-color:{4};\">");
            g_strLinkUnitDesigntimeRender_V.Append("<table cellspacing='0px' cellpadding='0px' width='100%'><tr><td width='100%' bgcolor='{2}'>Ads By Google</td></tr>");
            g_strLinkUnitDesigntimeRender_V.Append("<tr><td>");
            g_strLinkUnitDesigntimeRender_V.Append("<table>");
            g_strLinkUnitDesigntimeRender_V.Append("<tr><td><a href=\"link1.html\">Ad Link</a></td></tr>");
            g_strLinkUnitDesigntimeRender_V.Append("<tr><td><a href=\"link2.html\">Ad Link</a></td></td></tr>");
            g_strLinkUnitDesigntimeRender_V.Append("<tr><td><a href=\"link3.html\">Ad Link</a></td></tr>");
            g_strLinkUnitDesigntimeRender_V.Append("<tr><td><a href=\"link4.html\">Ad Link</a></td></tr>");
            g_strLinkUnitDesigntimeRender_V.Append("</table>");
            g_strLinkUnitDesigntimeRender_V.Append("</td></tr>");
            g_strLinkUnitDesigntimeRender_V.Append("</table>");
            g_strLinkUnitDesigntimeRender_V.Append("</div>");

            g_strLinkUnitDesigntimeRender_H = new StringBuilder();
            g_strLinkUnitDesigntimeRender_H.Append("<div style=\"width:{0}px;height={1};border:1 solid {2};color:{3};background-color:{4};\">");
            g_strLinkUnitDesigntimeRender_H.Append("<table cellspacing='0px' cellpadding='0px' width='100%'><tr><td width='100%' bgcolor='{2}'>Ads By Google</td></tr>");
            g_strLinkUnitDesigntimeRender_H.Append("<tr><td>");
            g_strLinkUnitDesigntimeRender_H.Append("<table width='100%' cellpadding='5px'>");
            g_strLinkUnitDesigntimeRender_H.Append("<tr><td><a href=\"link1.html\">Ad Link</a></td>");
            g_strLinkUnitDesigntimeRender_H.Append("<td><a href=\"link2.html\">Ad Link</a></td></td>");
            g_strLinkUnitDesigntimeRender_H.Append("<td><a href=\"link3.html\">Ad Link</a></td>");
            g_strLinkUnitDesigntimeRender_H.Append("<td><a href=\"link4.html\">Ad Link</a></td></tr>");
            g_strLinkUnitDesigntimeRender_H.Append("</table>");
            g_strLinkUnitDesigntimeRender_H.Append("</td></tr>");
            g_strLinkUnitDesigntimeRender_H.Append("</table>");
            g_strLinkUnitDesigntimeRender_H.Append("</div>");
        }
    }
}
