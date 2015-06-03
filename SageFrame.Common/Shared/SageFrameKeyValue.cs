#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace SageFrame.Web
{
    /// <summary>
    ///  This class holds the properties of key value for Application.
    /// </summary>
    public class SageFrameStringKeyValue
    {
        string _Key = string.Empty;
        string _Value = string.Empty;
        /// <summary>
        /// Initializes a new instance of the SageFrameStringKeyValue class.
        /// </summary>
        public SageFrameStringKeyValue() { }

        /// <summary>
        /// Initializes a new instance of the SageFrameStringKeyValue class.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public SageFrameStringKeyValue(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        /// <summary>
        /// Get or set Key.
        /// </summary>
        public string Key
        {
            set { _Key = value; }
            get { return _Key; }
        }
        /// <summary>
        /// Get or set Value.
        /// </summary>
        public string Value
        {
            set { _Value = value; }
            get { return _Value; }
        }
    }
    /// <summary>
    ///  This class holds the properties of key value for Application.
    /// </summary>
    public class SageFrameIntKeyValue
    {
        int _Key = 0;
        string _Value = string.Empty;
        /// <summary>
        /// Initializes a new instance of the SageFrameIntKeyValue class.
        /// </summary>
        public SageFrameIntKeyValue() { }
        /// <summary>
        /// Initializes a new instance of the SageFrameIntKeyValue class.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public SageFrameIntKeyValue(int key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        /// <summary>
        /// Get or set Key
        /// </summary>
        public int Key
        {
            set { _Key = value; }
            get { return _Key; }
        }
        /// <summary>
        /// Get or set Value.
        /// </summary>
        public string Value
        {
            set { _Value = value; }
            get { return _Value; }
        }
    }
}
