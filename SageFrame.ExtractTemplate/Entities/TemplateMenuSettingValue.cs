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

namespace SageFrame.ExtractTemplate
{
    /// <summary>
    /// Class that contains template menu setting values.
    /// </summary>
    public class TemplateMenuSettingValue
    {
        /// <summary>
        /// Gets or sets menu manager's setting value ID.
        /// </summary>
        public int MenuMgrSettingValueID { get; set; }

        /// <summary>
        /// Gets or sets menu name.
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// Gets or sets  menu ID.
        /// </summary>
        public int MenuID { get; set; }

        /// <summary>
        /// Gets or sets setting key.
        /// </summary>
        public string SettingKey { get; set; }

        /// <summary>
        /// Gets or sets setting value.
        /// </summary>
        public int SettingValue { get; set; }

        /// <summary>
        /// Initializes  an instance of TemplateMenuSettingValue class.
        /// </summary>
        public TemplateMenuSettingValue() { }

    }
}
