#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
#endregion

namespace SageFrame.Common
{
    /// <summary>
    /// Application script information.
    /// </summary>
    [Serializable]
    public class CssScriptInfo
    {
        /// <summary>
        /// Get or set script index.
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Get or set module name.
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// Get or set script path.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Get or set file name.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Get or set script property.
        /// </summary>
        public int MyProperty { get; set; }
        /// <summary>
        /// Get or set true if optimization is allowed.
        /// </summary>
        public bool AllowOptimization { get; set; }
        /// <summary>
        /// Get or set true if combination is allowed.
        /// </summary>
        public bool AllowCombination { get; set; }
        /// <summary>
        /// Get or set script position.
        /// </summary>
        public int Position { get; set; }
        /// <summary>
        /// Get or set script loading mode.
        /// </summary>
        public int LoadingMode { get; set; }
        /// <summary>
        /// Get or set true if request from handheld device.
        /// </summary>
        public bool IsHandheld { get; set; }
        /// <summary>
        /// Get or set PathMode.
        /// </summary>
        public bool PathMode { get; set; }
        /// <summary>
        /// Get or set true if compressed is true.
        /// </summary>
        public bool IsCompress { get; set; }
        /// <summary>
        /// Get or set true if CDN is true.
        /// </summary>
        public bool IsCDN { get; set; }
        /// <summary>
        /// Get or set true if script is js .
        /// </summary>
        public bool IsJs { get; set; }
        /// <summary>
        /// Get or set ResourceType.
        /// </summary>
        public ResourceType rtype { get; set; }
        /// <summary>
        /// Initializes a new instance of the CssScriptInfo.
        /// </summary>
        /// <param name="_ModuleName">ModuleName</param>
        /// <param name="_FileName">FileName</param>
        /// <param name="_Path">Path</param>
        /// <param name="_Position">Position</param>
        public CssScriptInfo(string _ModuleName, string _FileName, string _Path, int _Position)
        {
            this.ModuleName = _ModuleName;
            this.FileName = _FileName;
            this.Path = _Path;
            this.Position = _Position;

        }
        /// <summary>
        ///  Initializes a new instance of the CssScriptInfo.
        /// </summary>
        /// <param name="_ModuleName">ModuleName</param>
        /// <param name="_FileName">FileName</param>
        /// <param name="_Path">Path</param>
        /// <param name="_Position">Position</param>
        /// <param name="_AllowOptimization">AllowOptimization</param>
        public CssScriptInfo(string _ModuleName, string _FileName, string _Path, int _Position, bool _AllowOptimization)
        {
            this.ModuleName = _ModuleName;
            this.FileName = _FileName;
            this.Path = _Path;
            this.Position = _Position;
            this.AllowOptimization = _AllowOptimization;

        }
        /// <summary>
        /// Initializes a new instance of the CssScriptInfo.
        /// </summary>
        /// <param name="_ModuleName">ModuleName</param>
        /// <param name="_FileName">FileName</param>
        public CssScriptInfo(string _ModuleName, string _FileName)
        {
            this.ModuleName = _ModuleName;
            this.FileName = _FileName;
        }
        /// <summary>
        /// Initializes a new instance of the CssScriptInfo.
        /// </summary>
        /// <param name="_ModuleName">ModuleName</param>
        /// <param name="_Path">Path</param>
        /// <param name="_Position">Position</param>
        /// <param name="_IsCDN">IsCDN</param>
        /// <param name="_IsJs">IsJS</param>
        public CssScriptInfo(string _ModuleName, string _Path, int _Position, bool _IsCDN, bool _IsJs)
        {
            this.ModuleName = _ModuleName;
            this.Path = _Path;
            this.Position = _Position;
            this.IsCDN = _IsCDN;
            this.IsJs = _IsJs;

        }

    }
}
