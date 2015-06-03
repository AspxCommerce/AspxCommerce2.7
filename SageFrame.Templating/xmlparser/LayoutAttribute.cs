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

namespace SageFrame.Templating.xmlparser
{
    /// <summary>
    /// Class that contains properties for layout attributes.
    /// </summary>
    public class LayoutAttribute
    {
        public int Index = -1;
		public string Name = null;
		public string Value = null;
		public AttributeTypes AttributeType = AttributeTypes.Attribute;
		public string Text = null;
		public string TagFormat = null;

        /// <summary>
        /// Gets or sets AttributeTypes object.
        /// </summary>
        public XmlAttributeTypes Type { get; set; }

        /// <summary>
        /// Initializes an instance of LayoutAttribute class.
        /// </summary>
		public LayoutAttribute()
		{

		}

        /// <summary>
        /// Initializes an instance of LayoutAttribute class.
        /// </summary>
        /// <param name="name">Layout name.</param>
        /// <param name="value">Layout value.</param>
        /// <param name="_type">Attribute type.</param>
        public LayoutAttribute(string name, string value, XmlAttributeTypes _type)
		{
			this.Name = name;
			this.Value = value;
            this.Type = _type;
		}
    }
}
