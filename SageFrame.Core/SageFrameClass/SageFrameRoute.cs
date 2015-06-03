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
using System.Web.Routing;

#endregion


namespace SageFrame
{
    /// <summary>
    /// Interface for routing in the application.
    /// </summary>
    public interface SageFrameRoute : IHttpHandler
    {

        /// <summary>
        /// Declaration of property which must gets or sets pagepath.
        /// </summary>
        string PagePath { get; set; }

        /// <summary>
        /// Declaration of property which must gets or sets portal seo name.
        /// </summary>
        string PortalSEOName { get; set; }

        /// <summary>
        /// Declaration of property which must gets or sets user module ID.
        /// </summary>
        string UserModuleID { get; set; }

        /// <summary>
        /// Declaration of property which must gets or sets control type.
        /// </summary>
        string ControlType { get; set; }

        /// <summary>
        /// Declaration of property which must gets or sets control mode.
        /// </summary>
		string ControlMode { get; set; }

        /// <summary>
        /// Declaration of property which must gets or sets rounting key.
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// Declaration of property which must gets or sets routing parameter.
        /// </summary>
        string Param { get; set; }
    }
}
