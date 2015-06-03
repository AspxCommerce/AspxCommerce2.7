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
    /// This class holds the properties for Language class.
    /// </summary>
    [Serializable]
    public class Language
    {
        /// <summary>
        /// Gets or sets LanguageID.
        /// </summary>
        public int LanguageID { get; set; }
        /// <summary>
        /// Gets or sets LanguageName.
        /// </summary>
        public string LanguageName { get; set; }
        /// <summary>
        /// Gets or sets LanguageCode.
        /// </summary>
        public string LanguageCode { get; set; }
        /// <summary>
        /// Gets or sets FallBackLanguage.
        /// </summary>
        public string FallBackLanguage { get; set; }
        /// <summary>
        /// Gets or sets FallBackLanguageCode.
        /// </summary>
        public string FallBackLanguageCode { get; set; }
        /// <summary>
        /// Gets or sets NativeName.
        /// </summary>
        public string NativeName { get; set; }
        /// <summary>
        /// Gets or sets FlagPath.
        /// </summary>
        public string FlagPath { get; set; }

        private string _Language;
        /// <summary>
        /// Gets or sets Language.
        /// </summary>
        public string LanguageN
        {
            get { return (_Language.Substring(0, _Language.IndexOf('('))); }
            set { _Language = value; }
        }
        private string _Country;
        /// <summary>
        /// Gets or sets Country.
        /// </summary>
        public string Country
        {
            get { return (_Country.Substring(_Country.IndexOf('(') + 1, _Country.Length - _Country.IndexOf('(') - 2)); }
            set { _Country = value; }
        }
        /// <summary>
        /// Initializes a new instance of the Language class.
        /// </summary>
        public Language() { }
        /// <summary>
        /// Initializes a new instance of the Language class.
        /// </summary>
        /// <param name="languageid">languageid</param>
        /// <param name="languagename">languagename</param>
        /// <param name="languagecode">languagecode</param>
        public Language(int languageid, string languagename, string languagecode)
        {
            this.LanguageID = languageid;
            this.LanguageName = languagename;
            this.LanguageCode = languagecode;
        }
        /// <summary>
        /// Initializes a new instance of the Language class.
        /// </summary>
        /// <param name="languagename">languagename</param>
        /// <param name="languagecode">languagecode</param>
        /// <param name="fallbacklanguage">fallbacklanguage</param>
        public Language(string languagename, string languagecode, string fallbacklanguage)
        {
            this.LanguageName = languagename;
            this.LanguageCode = LanguageCode;
            this.FallBackLanguage = fallbacklanguage;
        }
        /// <summary>
        /// Initializes a new instance of the Language class.
        /// </summary>
        /// <param name="languagename">languagename</param>
        /// <param name="languagecode">languagecode</param>
        /// <param name="fallbacklanguage">fallbacklanguage</param>
        /// <param name="fallbacklanguagecode">fallbacklanguagecode</param>
        public Language(string languagename, string languagecode, string fallbacklanguage, string fallbacklanguagecode)
        {
            this.LanguageName = languagename;
            this.LanguageCode = LanguageCode;
            this.FallBackLanguage = fallbacklanguage;
            this.FallBackLanguageCode = fallbacklanguagecode;
        }
    }
}
