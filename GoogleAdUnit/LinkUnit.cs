/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.ComponentModel;

namespace SFE.GoogleAdUnit
{
    /// <summary>
    /// Class that contains the basic properties and methods for advertisement.
    /// </summary>
    [
        DefaultProperty("AffiliateId"),
        ToolboxData("<{0}:LinkUnit runat=\"server\"></{0}:LinkUnit>")
    ]
    public class LinkUnit : BaseAdUnit
	{
		#region Class Members
		private LinkUnitFormat m_LinkUnitFormat = LinkUnitFormat.H_728x15;
        private LinkAdsPerUnit m_AdsPerUnit = LinkAdsPerUnit.AdCount_4;
		#endregion

		#region Public Properties

 		/// <summary>
		/// Gets or sets advertisement link's unit format.
		/// </summary>
		public LinkUnitFormat LinkUnitFormat
		{
			get { return this.m_LinkUnitFormat; }
			set { this.m_LinkUnitFormat = value; }
		}

        /// <summary>
        /// Gets or sets advertisemt links per unit.
        /// </summary>
        public LinkAdsPerUnit AdsPerUnit
        {
            get { return this.m_AdsPerUnit; }
            set { this.m_AdsPerUnit = value; }
        }

  		#endregion

		#region Helper Methods
        /// <summary>
        /// Helps to design rendering time.
        /// </summary>
        /// <param name="writer"></param>
		override protected void DesignTimeRender(System.Web.UI.HtmlTextWriter writer)
		{
			StringBuilder strStream = new StringBuilder();

            String strFormat = this.LinkUnitFormat.ToString();

            UnitDimensions obDim = AdUnitGlobals.g_LinkUnitDimensions[this.LinkUnitFormat] as UnitDimensions;
            if (strFormat.StartsWith("H"))
            {
                strStream.AppendFormat(AdUnitGlobals.g_strLinkUnitDesigntimeRender_H.ToString(), obDim.WIDTH, obDim.HEIGHT, DrawingUtil.GetColorHexFormat(this.BorderColor, false), DrawingUtil.GetColorHexFormat(this.TextColor, false), DrawingUtil.GetColorHexFormat(this.BackColor, false), "&nbsp;");
            }
            else
            {
                strStream.AppendFormat(AdUnitGlobals.g_strLinkUnitDesigntimeRender_V.ToString(), obDim.WIDTH, obDim.HEIGHT, DrawingUtil.GetColorHexFormat(this.BorderColor, false), DrawingUtil.GetColorHexFormat(this.TextColor, false), DrawingUtil.GetColorHexFormat(this.BackColor, false), "&nbsp;");
            }
            

			writer.Write(strStream.ToString());
		}
		
        /// <summary>
        /// Returns advertisement format.
        /// </summary>
        /// <returns></returns>
        override protected String GetAdFormat()
		{
            String strFormat = String.Empty;
            switch (this.LinkUnitFormat)
			{
                case LinkUnitFormat.H_468x15:
                    return "468x15_0ads_al";
                case LinkUnitFormat.S_200x90:
                    return "120x600_0ads_al";
                case LinkUnitFormat.S_180x90:
                    return "160x600_0ads_al";
                case LinkUnitFormat.S_160x90:
                    return "120x240_0ads_al";
                case LinkUnitFormat.S_120x90:
                    return "125x125_0ads_al";
                case LinkUnitFormat.H_728x15:
				default:
                    return "728x15_0ads_al";
			}

            //switch (this.AdsPerUnit)
            //{
            //    case LinkAdsPerUnit.AdCount_4:
            //        break;
            //    case LinkAdsPerUnit.AdCount_5:
            //        strFormat += "_s";
            //        break;
            //}

            //return strFormat;
		}

        /// <summary>
        /// Returns Unit dimension for the image in advertisement.
        /// </summary>
        /// <returns></returns>
        internal override UnitDimensions GetUnitDimensions()
        {
            return AdUnitGlobals.g_LinkUnitDimensions[this.LinkUnitFormat] as UnitDimensions;
        }
		#endregion
	}
}
