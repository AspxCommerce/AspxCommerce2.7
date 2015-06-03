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
    /// Class that contains  XML tags.
    /// </summary>
    public class XmlTag
    {
        /// <summary>
        /// Gets or sets XML index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets XML tag name.
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// Gets or sets XML complete tag.
        /// </summary>
        public string CompleteTag { get; set; }

        /// <summary>
        /// Gets or sets XML tag format.
        /// </summary>
        public string TagFormat { get; set; }

        /// <summary>
        /// Gets or sets XmlTagTypes object.
        /// </summary>
        public XmlTagTypes TagType { get; set; }

        /// <summary>
        /// Gets or sets list of LayoutAttribute class objects.
        /// </summary>
        public List<LayoutAttribute> LSTAttributes { get; set; }

        /// <summary>
        /// Gets or sets list of XmlTag class object.
        /// </summary>
        public List<XmlTag> LSTChildNodes { get; set; }

        /// <summary>
        /// Gets or sets inner html.
        /// </summary>
        public string InnerHtml { get; set; }

        /// <summary>
        /// Gets or sets child node count.
        /// </summary>
        public int ChildNodeCount { get; set; }

        /// <summary>
        /// Gets or sets attribute count.
        /// </summary>
        public int AttributeCount { get; set; }

        /// <summary>
        /// Gets or sets attribute string.
        /// </summary>
        public string AttributeString { get; set; }

        /// <summary>
        /// Gets or sets list of string  containing page name.
        /// </summary>
        public List<string> LSTPages { get; set; }

        /// <summary>
        /// Gets or sets preset name.
        /// </summary>
        public string PresetName { get; set; }

        /// <summary>
        /// Gets or sets array of string of positions.
        /// </summary>
        public string[] PositionsArr { get; set; }

        /// <summary>
        /// Gets or sets array of string of placeholders.
        /// </summary>
        public string[] PchArr { get; set; }

        /// <summary>
        /// Gets or sets placeholder name.
        /// </summary>
        public string Placeholders { get; set; }

        /// <summary>
        /// Gets or sets outer wrapper level.
        /// </summary>
        public int OuterWrapLevel { get; set; }

        /// <summary>
        /// Gets or sets inner wrapper level.
        /// </summary>
        public int InnerWrapLevel { get; set; }

        /// <summary>
        /// Initializes an instance of XmlTag class.
        /// </summary>
        public XmlTag() { }

        /// <summary>
        /// Initializes an instance of XmlTag class.
        /// </summary>
        /// <param name="_CompleteTag">Xml complete tag.</param>
        public XmlTag(string _CompleteTag)
        {
            this.CompleteTag = _CompleteTag;
        }
    }
}
