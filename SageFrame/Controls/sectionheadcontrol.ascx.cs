/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using System.Web.UI.HtmlControls;

namespace SageFrame.Controls
{
    public partial class sectionheadcontrol : BaseAdministrationUserControl
    {
        private bool _includeRule = false;
        private bool _isExpanded = true;
        private string _javaScript = "__sfe_SectionMaxMin";
        private string _maxImageUrl = string.Empty;
        private string _minImageUrl = string.Empty;
        private string _resourceKey;
        private string _section;

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// CssClass determines the Css Class used for the Title Text
        /// </summary>
        /// <value>A string representing the name of the css class</value>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [alok]    03/30/2010    created
        /// </history>
        /// -----------------------------------------------------------------------------
        public string CssClass
        {
            get
            {
                return lblTitle.CssClass;
            }
            set
            {
                lblTitle.CssClass = value;
            }
        }

        public bool IncludeRule
        {
            get
            {
                return _includeRule;
            }
            set
            {
                _includeRule = value;
            }
        }

        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
               
            }
            set
            {
                _isExpanded = value;
            }
        }

        public string JavaScript
        {
            get
            {
                return _javaScript;
            }
            set
            {
                _javaScript = value;
            }
        }

        public string MaxImageUrl
        {
            get
            {
                return GetTemplateImageUrl("plus.png", false);
            }
            set
            {
                _maxImageUrl = value;
            }
        }

        public string MinImageUrl
        {
            get
            {
                return GetTemplateImageUrl("minus.png", false);
            }
            set
            {
                _minImageUrl = value;
            }
        }

        public string ResourceKey
        {
            get
            {
                return _resourceKey;
            }
            set
            {
                _resourceKey = value;
            }
        }

        public string Section
        {
            get
            {
                return _section;
            }
            set
            {
                _section = value;
            }
        }

        public string Text
        {
            get
            {
                return lblTitle.Text;
            }
            set
            {
                lblTitle.Text = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Assign resource key to label for localization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///    [alok]    03/30/2010    created
        /// </history>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if ((ResourceKey != string.Empty))
                    {
                        //lblTitle.Attributes["resourcekey"] = ResourceKey;
                        lblTitle.Text = Text;
                    }
                }
            }
            catch (Exception exc)
            {
                // Module failed to load
                ProcessException(exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Renders the SectionHeadControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///     [alok]    03/30/2010    created        
        /// </history>
        /// -----------------------------------------------------------------------------
        protected void Page_PreRender(object sender, System.EventArgs e)
        {

        }
    }
}