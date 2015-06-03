#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq; 

#endregion

namespace SageFrame.Web.Templates.Payment
{
    /// <summary>
    /// IConfigurePaymentMethodModule
    /// </summary>
    public interface IConfigurePaymentMethodModule
    {
        void Save();
    }
}
