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

namespace SageFrame.ModuleMessage
{
    /// <summary>
    /// This class holds the properties of ModuleMessageInfo class.
    /// </summary>
    public class ModuleMessageInfo
    {
        /// <summary>
        /// Get or set ModuleMessageID.
        /// </summary>
        public int ModuleMessageID { get; set; }
        /// <summary>
        /// Get or set ModuleID.
        /// </summary>
        public int ModuleID { get; set; }
        /// <summary>
        /// Get or set message.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Get or set culture code.
        /// </summary>
        public string Culture { get; set; }
        /// <summary>
        /// Get or set true for active.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Get or set message type.
        /// </summary>
        public int MessageType { get; set; }
        /// <summary>
        /// Get or set message mode.
        /// </summary>
        public int MessageMode { get; set; }
        /// <summary>
        /// Get or set true for disable.
        /// </summary>
        public bool DisableAll { get; set; }
        /// <summary>
        /// Get or set friendly name.
        /// </summary>
        public string FriendlyName { get; set; }
        /// <summary>
        /// Get or set message position.
        /// </summary>
        public int MessagePosition { get; set; }
        ///  /// <summary>
        /// Initializes a new instance of the ModuleMessageInfo class.
        /// </summary>
        public ModuleMessageInfo() { }
        /// <summary>
        ///  Initializes a new instance of the ModuleMessageInfo class.
        /// </summary>
        /// <param name="_ModuleID">ModuleID</param>
        /// <param name="_Message">Module message.</param>
        /// <param name="_Culture">Culture code.</param>
        /// <param name="_IsActive">true for active.</param>
        /// <param name="_MessageType">Module message type.</param>
        /// <param name="_MessageMode">Module message mode.</param>
        /// <param name="_DisableAll">true for disable.</param>
        public ModuleMessageInfo(int _ModuleID, string _Message, string _Culture, bool _IsActive, int _MessageType, int _MessageMode,bool _DisableAll)
        {
            this.ModuleID = _ModuleID;
            this.Message = _Message;
            this.Culture = _Culture;
            this.IsActive = _IsActive;
            this.MessageType = _MessageType;
            this.MessageMode = _MessageMode;
            this.DisableAll = _DisableAll;
        }
    }
}
