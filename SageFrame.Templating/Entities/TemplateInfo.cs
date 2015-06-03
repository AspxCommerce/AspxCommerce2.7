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
    /// This class holds the properties of TemplateInfo.
    /// </summary>
    public class TemplateInfo
    {
        /// <summary>
        /// Get or set template name.
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// Get or set template path.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Get or set template thumbnail image path.
        /// </summary>
        public string ThumbImage { get; set; }
        /// <summary>
        /// Get or set true for active template.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Get or set object of PresetInfo class.
        /// </summary>
        public PresetInfo DefaultPreset { get; set; }
        /// <summary>
        /// Get or set author.
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Get or set description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Get or set website.
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// Get or set true for default.
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// Get or set PortalID.
        /// </summary>
        public int PortalID { get; set; }
        /// <summary>
        /// Get or set true for applicable.
        /// </summary>
        public bool IsApplied { get; set; }
        /// <summary>
        /// Initializes a new instance of the TemplateInfo class.
        /// </summary>
        public TemplateInfo() { }
        /// <summary>
        /// Initializes a new instance of the TemplateInfo class.
        /// </summary>
        /// <param name="_TemplateName">Template name.</param>
        public TemplateInfo(string _TemplateName)
        {
            this.TemplateName = _TemplateName;
        }
        /// <summary>
        /// Get template name replace space with '_'.
        /// </summary>
        public string TemplateSeoName
        {
            get { return (TemplateName.Replace(' ', '_')); }            
        }
        /// <summary>
        ///Initializes a new instance of the TemplateInfo class. 
        /// </summary>
        /// <param name="_TemplateName">Template name.</param>
        /// <param name="_Path">Template path.</param>
        /// <param name="_ThumbImage">t template thumbnail image path</param>
        /// <param name="_IsActive">True for active.</param>
        /// <param name="_IsDefault">True for default template.</param>
        public TemplateInfo(string _TemplateName, string _Path, string _ThumbImage,bool _IsActive,bool _IsDefault)
        {
            this.TemplateName = _TemplateName;
            this.Path = _Path;
            this.ThumbImage = _ThumbImage;
            this.IsActive = _IsActive;
            this.IsDefault = _IsDefault;
        }
    }
}
