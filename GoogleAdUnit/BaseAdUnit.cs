/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Drawing;

namespace SFE.GoogleAdUnit
{
    /// <summary>
    /// Base class for AdUnit and LinkUnit controls
    /// </summary>
    public abstract class BaseAdUnit : Control
    {
        #region Class Members
        private String m_strAffiliateId = String.Empty;
        private String m_strChannelId = String.Empty;
        private String m_strTopLabel = String.Empty;
        private String m_strBottomLabel = String.Empty;
        private String m_strAnotherSiteUrl = String.Empty;
        private AlternateAdTypes m_AlternateAdType = AlternateAdTypes.PublicServiceAds;
        private System.Drawing.Color m_BorderColor = Color.AliceBlue;
        private System.Drawing.Color m_BackColor = Color.White;
        private System.Drawing.Color m_LinkColor = Color.Blue;
        private System.Drawing.Color m_TextColor = Color.Black;
        private System.Drawing.Color m_SolidFillColor = Color.Blue;
        private System.Drawing.Color m_UrlColor = Color.Green;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets advertisement affiliation.
        /// </summary>
        public String AffiliateId
        {
            get { return this.m_strAffiliateId; }
            set { this.m_strAffiliateId = value; }
        }

        /// <summary>
        /// Gets or sets channel id of advertisement.
        /// </summary>
        public String ChannelId
        {
            get { return this.m_strChannelId; }
            set { this.m_strChannelId = value; }
        }

        /// <summary>
        /// Gets or sets alternative advertisement URL.
        /// </summary>
        public String AnotherUrl
        {
            get { return this.m_strAnotherSiteUrl; }
            set { this.m_strAnotherSiteUrl = value; }
        }

        /// <summary>
        /// Gets or sets AlternateAdTypes object.
        /// </summary>
        public AlternateAdTypes AlternateAdType
        {
            get { return this.m_AlternateAdType; }
            set { this.m_AlternateAdType = value; }
        }

        /// <summary>
        /// Gets or sets color of advertisement.
        /// </summary>
        public Color TextColor
        {
            get { return this.m_TextColor; }
            set { this.m_TextColor = value; }
        }

        /// <summary>
        /// Gets or sets background color of advertisement.
        /// </summary>
        public Color BackColor
        {
            get { return this.m_BackColor; }
            set { this.m_BackColor = value; }
        }

        /// <summary>
        /// Gets or sets border color of advertisement.
        /// </summary>
        public Color BorderColor
        {
            get { return this.m_BorderColor; }
            set { this.m_BorderColor = value; }
        }

        /// <summary>
        /// Gets or sets link color  of advertisement.
        /// </summary>
        public Color LinkColor
        {
            get { return this.m_LinkColor; }
            set { this.m_LinkColor = value; }
        }

        /// <summary>
        /// Gets or sets URL color of advertisement.
        /// </summary>
        public Color UrlColor
        {
            get { return this.m_UrlColor; }
            set { this.m_UrlColor = value; }
        }

        /// <summary>
        /// Gets or sets solid fill color of advertisement.
        /// </summary>
        public Color SolidFillColor
        {
            get { return this.m_SolidFillColor; }
            set { this.m_SolidFillColor = value; }
        }
        #endregion

        #region Override Methods

        /// <summary>
        /// Is override protected method to render at design or runtime.
        /// </summary>
        /// <param name="writer">HtmlTextWriter object.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.DesignMode)
            {
                DesignTimeRender(writer);
            }
            else
            {
                RunTimeRender(writer);
            }
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// Designs render time.
        /// </summary>
        /// <param name="writer">HtmlTextWriter object.</param>
        protected virtual void DesignTimeRender(System.Web.UI.HtmlTextWriter writer)
        {

        }

        /// <summary>
        /// Renders at runtime.
        /// </summary>
        /// <param name="writer">HtmlTextWriter object.</param>
        protected void RunTimeRender(System.Web.UI.HtmlTextWriter writer)
        {
            if (null == AffiliateId ||
                String.Empty == AffiliateId)
            {
                throw new ApplicationException("No client id has been specified");
            }

            UnitDimensions obDim = GetUnitDimensions();

            writer.Write("<div id=\"{0}\">", this.ClientID);

            writer.Write("<script type=\"text/javascript\">");
            writer.Write(String.Format("google_ad_client=\"{0}\";", this.AffiliateId));
            switch (this.AlternateAdType)
            {
                case AlternateAdTypes.AnotherUrlAds:
                    writer.Write(String.Format("google_alternate_ad_url=\"{0}\";", this.AnotherUrl));
                    break;
                case AlternateAdTypes.SolidColorFill:
                    writer.Write(String.Format("google_alternate_color=\"{0}\";", DrawingUtil.GetColorHexFormat(this.SolidFillColor, true)));
                    break;
            }
            writer.Write(String.Format("google_ad_width={0};", obDim.WIDTH));
            writer.Write(String.Format("google_ad_height={0};", obDim.HEIGHT));
            writer.Write(String.Format("google_ad_format=\"{0}\";", this.GetAdFormat()));
            writer.Write(String.Format("google_ad_channel=\"{0}\";", this.ChannelId));
            writer.Write(String.Format("google_color_border=\"{0}\";", DrawingUtil.GetColorHexFormat(this.BorderColor, true)));
            writer.Write(String.Format("google_color_bg=\"{0}\";", DrawingUtil.GetColorHexFormat(this.BackColor, true)));
            writer.Write(String.Format("google_color_link=\"{0}\";", DrawingUtil.GetColorHexFormat(this.LinkColor, true)));
            writer.Write(String.Format("google_color_url=\"{0}\";", DrawingUtil.GetColorHexFormat(this.UrlColor, true)));
            writer.Write(String.Format("google_color_text=\"{0}\";", DrawingUtil.GetColorHexFormat(this.TextColor, true)));

            InsertSpecificScript(writer);

            writer.Write("</script>");
            writer.Write("<script type=\"text/javascript\"");
            writer.Write(" src=\"http://pagead2.googlesyndication.com/pagead/show_ads.js\">");
            writer.Write("</script>");

            writer.Write("</div>");
        }

        /// <summary>
        /// Inserts specific script.
        /// </summary>
        /// <param name="writer">HtmlTextWriter object.</param>
        protected virtual void InsertSpecificScript(System.Web.UI.HtmlTextWriter writer)
        {

        }

        /// <summary>
        /// Returns advertisement format.
        /// </summary>
        /// <returns>Empty string.</returns>
        protected virtual String GetAdFormat()
        {
            return String.Empty;
        }

        /// <summary>
        /// Returns advertisement type.
        /// </summary>
        /// <returns>Empty string.</returns>
        protected virtual String GetAdType()
        {
            return String.Empty;
        }

        /// <summary>
        /// Returns dimensions of the Adverting image.
        /// </summary>
        /// <returns>Null.</returns>
        internal virtual UnitDimensions GetUnitDimensions()
        {
            return null;
        }
        #endregion
    }
}
