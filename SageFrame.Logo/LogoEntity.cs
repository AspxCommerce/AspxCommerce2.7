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

namespace SageFrame.Logo
{    
    /// <summary>
    /// This class holds the properties for Logo
    /// </summary>
   public class LogoEntity
    {
       /// <summary>
        /// Initializes a new instance of the LogoEntity class.
       /// </summary>
       public LogoEntity()
       {
       }
      
       private string _logoText;
       
       private string _logoPath;
       /// <summary>
       /// Gets or sets logo text
       /// </summary>
       public string LogoText
       {
           get { return this._logoText; }
           set { this._logoText = value; }
       }
       /// <summary>
       /// Gets or sets logo path
       /// </summary>
       public string LogoPath
       {
           get { return this._logoPath; }
           set { this._logoPath = value; }
       }
       /// <summary>
       /// Gets or sets slogan
       /// </summary>
       public string Slogan { get; set; }
      /// <summary>
      /// Gets or sets url
      /// </summary>
       public string url { get; set; }
    }
}
