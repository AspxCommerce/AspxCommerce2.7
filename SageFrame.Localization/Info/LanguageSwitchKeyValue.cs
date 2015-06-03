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
    /// This class holds the properties for LanguageSwitchKeyValue.
    /// </summary>
    public class LanguageSwitchKeyValue
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
        /// Gets or sets AddedBy.
        /// </summary>
        public string AddedBy { get; set; }
        /// <summary>
        /// Gets or sets IsActive.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Initializes a new instance of the LanguageSwitchKeyValue class.
        /// </summary>
        public LanguageSwitchKeyValue() { }
        /// <summary>
        /// Initializes a new instance of the LanguageSwitchKeyValue class.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public LanguageSwitchKeyValue(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
