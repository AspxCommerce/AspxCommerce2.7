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
    /// This class holds the properties for CustomCultureInfo.
    /// </summary>
    public class CustomCultureInfo
    {
        /// <summary>
        /// Gets or sets display name.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Gets or sets language code.
        /// </summary>
        public string LanguageCode{get;set;}
        /// <summary>
        /// Gets or sets language name.
        /// </summary>
        public string LanguageName { get; set; }
        /// <summary>
        /// Initializes a new instance of the CustomCultureInfo class.
        /// </summary>
        public CustomCultureInfo() { }
    }
   
}
