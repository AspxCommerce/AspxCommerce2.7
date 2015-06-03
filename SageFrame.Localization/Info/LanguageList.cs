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
    #region "Countries"
    /// <summary>
    /// This class holds the properties for Countries.
    /// </summary>
    public class Countries
    {
        /// <summary>
        /// Gets or sets ImagePath.
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// Gets or sets CountryName.
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// Gets or sets CultureCode.
        /// </summary>
        public string CultureCode { get; set; }
        /// <summary>
        /// Initializes a new instance of the Countries class.
        /// </summary>
        /// <param name="imagepath">imagepath</param>
        /// <param name="countryname">countryname</param>
        /// <param name="culturecode">culturecode</param>
        public Countries(string imagepath, string countryname, string culturecode)
        {
            this.ImagePath = imagepath;
            this.CountryName = countryname;
            this.CultureCode = culturecode;
        }
    }
    #endregion

    #region "Languages"
    /// <summary>
    /// This class holds the properties for FallBackLanguages.
    /// </summary>
    public class FallBackLanguages
    {
        /// <summary>
        /// Gets or sets CultureName.
        /// </summary>
        public string CultureName { get; set; }
        /// <summary>
        /// Gets or sets CultureInfo.
        /// </summary>
        public string CultureInfo { get; set; }
        /// <summary>
        /// Gets or sets ImagePath.
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// Initializes a new instance of the FallBackLanguages class.
        /// </summary>
        /// <param name="culturename">culturename</param>
        /// <param name="cultureinfo">cultureinfo</param>
        /// <param name="imagepath">imagepath</param>
        public FallBackLanguages(string culturename, string cultureinfo, string imagepath)
        {
            this.CultureName = culturename;
            this.CultureInfo = cultureinfo;
            this.ImagePath = imagepath;
        }
    }
    #endregion

}
