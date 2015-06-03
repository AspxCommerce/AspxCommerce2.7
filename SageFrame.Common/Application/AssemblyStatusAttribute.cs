#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Web;
#endregion

namespace SageFrame.Application
{
    /// <summary>
    /// Options for application release mode.
    /// </summary>
    public enum ReleaseMode
    {

        None,
        Alpha,
        Beta,
        RC,
        Stable,
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    public class AssemblyStatusAttribute : System.Attribute
    {
        private ReleaseMode _releaseMode;
        /// <summary>
        /// Initializes a new instance of the application status.
        /// </summary>
        /// <param name="releaseMode">Release mode.</param>

        public AssemblyStatusAttribute(ReleaseMode releaseMode)
        {
            _releaseMode = releaseMode;
        }
        /// <summary>
        /// Get release mode.
        /// </summary>
        public ReleaseMode Status
        {
            get
            {
                return _releaseMode;
            }
        }
    }
}
