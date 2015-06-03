using System;
using System.Collections.Generic;
using System.Text;

namespace SageFrame.Application
{
    /// <summary>
    ///  This class holds the properties for Application.
    /// </summary>
    public class ApplicationInfo
    {
        /// <summary>
        /// Get or set application name.
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// Get or set application ID
        /// </summary>
        public Guid ApplicationId { get; set; }
        /// <summary>
        /// Get or set application description
        /// </summary>
        public string Description { get; set; }        
    }
}
