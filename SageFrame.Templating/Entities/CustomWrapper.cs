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

namespace SageFrame.Templating
{
    /// <summary>
    /// This class holds the properties of CustomWrapper.
    /// </summary>
    public class CustomWrapper
    {
        /// <summary>
        /// Get or set wrapper name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Get or set wrapper class.
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// Get or set wrapper depth.
        /// </summary>
        public int Depth { get; set; }
        /// <summary>
        /// Get or set wrapper start point.
        /// </summary>
        public string Start { get; set; }
        /// <summary>
        /// Get or set wrapper end point.
        /// </summary>
        public string End { get; set; }
        /// <summary>
        /// Get or set list of positions.
        /// </summary>
        public List<string> LSTPositions { get; set; }
        /// <summary>
        /// Get or set list of exception.
        /// </summary>
        public List<string> LSTExceptions { get; set; }
        /// <summary>
        /// Get or set wrapper order.
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// Get or set wrapper type.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Get or set wrapper Index.
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Initializes a new instance of the CustomWrapper class.
        /// </summary>
        public CustomWrapper() { }
    }
}
