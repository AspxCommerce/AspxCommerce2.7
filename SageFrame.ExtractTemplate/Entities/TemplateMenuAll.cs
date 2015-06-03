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
    /// Class that contains properties which are list collection of classes that are used for extracting template.
    /// </summary>
    public class TemplateMenuAll
    {
        /// <summary>
        /// Gets or sets list of TemplateMenu class ( <see cref="SageFrame.ExtractTemplate.TemplateMenu"/> ).
        /// </summary>
        public List<TemplateMenu> LstTemplateMenu { get; set; }

        /// <summary>
        /// Gets or sets list of TemplateMenuSettingValue class ( <see cref="SageFrame.ExtractTemplate.TemplateMenuSettingValue"/> ).
        /// </summary>
        public List<TemplateMenuSettingValue> LstTemplateSetting { get; set; }

        /// <summary>
        /// Initializes an instance of TemplateMenuAll class.
        /// </summary>
        public TemplateMenuAll() { }
    }
}
