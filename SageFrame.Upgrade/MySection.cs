using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


namespace SageFrame.Upgrade
{
    public class MySection : ConfigurationSection
    {
        [ConfigurationProperty("isDown", DefaultValue = "false", IsRequired = true)]
        public bool IsDown
        {
            get { return (Boolean)this["isDown"]; }
            set { this["isDown"] = value; }

        }

        [ConfigurationProperty("toPage", DefaultValue = "Default.aspx", IsRequired = true)]
        public string ToPage
        {
            get { return (string)this["toPage"]; }
            set { this["toPage"] = value; }

        }
    }
}
