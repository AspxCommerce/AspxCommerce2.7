#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion


namespace SageFrame.Localization
{
    /// <summary>
    /// This class holds the properties for ResourceDefinition.
    /// </summary>

    public class ResourceDefinition
    {
        /// <summary>
        /// Gets or sets Key.
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Gets or sets Value.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Gets or sets DefaultValue.
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// Initializes a new instance of the ResourceDefinition class.
        /// </summary>
        public ResourceDefinition() { }
    }
}
