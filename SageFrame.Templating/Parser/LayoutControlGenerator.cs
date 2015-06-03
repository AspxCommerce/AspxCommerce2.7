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
using SageFrame.Templating.xmlparser;
#endregion

namespace SageFrame.Templating
{
    /// <summary>
    /// Class that helps to generates layout markups.
    /// </summary>
    public class LayoutControlGenerator
    {
        /// <summary>
        /// Generates HTML tag for list of tags provided.
        /// </summary>
        /// <param name="lstTags">List of tags.</param>
        /// <returns>HTML markup.</returns>
        public string GenerateHTML(List<XmlTag> lstTags)
        {
            string markup = "";
            foreach (XmlTag tag in lstTags)
            {
                if (tag.TagType == XmlTagTypes.Section)
                {
                    markup += GenerateSectionMarkup(tag);
                }

            }
            return (GenerateExternalWrapper(markup));
        }

        /// <summary>
        /// Generates placeholder markup.
        /// </summary>
        /// <param name="Section">Section node.</param>
        /// <returns>Placeholder markup.</returns>
        public string GeneratePlaceHolderMarkup(XmlTag Section)
        {
            foreach (XmlTag tag in Section.LSTChildNodes)
            {
            }
            return "";
        }

        /// <summary>
        /// Generates section markups.
        /// </summary>
        /// <param name="section">Section node.</param>
        /// <returns>Section markup.</returns>
        public string GenerateSectionMarkup(XmlTag section)
        {
            string markup = "";
            if (section.AttributeCount > 0)
            {
                foreach (LayoutAttribute attr in section.LSTAttributes)
                {
                    switch (attr.Type)
                    {
                        case XmlAttributeTypes.NAME:
                            markup = GetSectionMarkup(attr.Value, section);
                            break;
                        case XmlAttributeTypes.TYPE:
                            break;
                        case XmlAttributeTypes.SPLIT:
                            break;
                    }
                }
            }
            return markup;
        }

        /// <summary>
        /// Generates section mark up for given node.
        /// </summary>
        /// <param name="name">Section name.</param>
        /// <param name="section">Section node.</param>
        /// <returns>Section markup.</returns>
        public string GetSectionMarkup(string name, XmlTag section)
        {
            string html = "";
            if (Enum.IsDefined(typeof(SectionTypes), name.ToUpper()))
            {
                SectionTypes _type = (SectionTypes)Enum.Parse(typeof(SectionTypes), name.ToUpper());
                switch (_type)
                {
                    case SectionTypes.SFHEADER:
                        html = GetTopMarkup(section);
                        break;
                    case SectionTypes.SFCONTENT:
                        html = GetMiddleWrapper(section);
                        break;
                    case SectionTypes.SFFOOTER:
                        html = GetBottomMarkup(section);
                        break;
                }
            }
            return html;
        }

        /// <summary>
        /// Generates outer wrapper markup for any markup.
        /// </summary>
        /// <param name="markup">Markup to be wrapped.</param>
        /// <returns>Wrapped up Markup.</returns>
        public string GenerateExternalWrapper(string markup)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id=\"sfOuterWrapper\">");
            sb.Append(markup);
            sb.Append("</div>");
            return sb.ToString();
        }

        /// <summary>
        /// Returns top  markup of any section provided.
        /// </summary>
        /// <param name="section">Section name.</param>
        /// <returns>Returns top markup.</returns>
        public string GetTopMarkup(XmlTag section)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(LayoutControlBuilder.GetTopBlocks(section));
            return sb.ToString();
        }

        /// <summary>
        /// Returns middle wrapper.
        /// </summary>
        /// <param name="section">Section name.</param>
        /// <returns>Middle wrapper.</returns>
        public string GetMiddleWrapper(XmlTag section)
        {
            StringBuilder sb = new StringBuilder();

            string layout = LayoutControlBuilder.GenerateMiddleBlockStart(section);
            sb.Append(layout);
            return sb.ToString();
        }

        /// <summary>
        /// Returns bottom markup.
        /// </summary>
        /// <param name="section">Section node.</param>
        /// <returns>Bottom markup.</returns>
        public string GetBottomMarkup(XmlTag section)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(LayoutControlBuilder.GetBottomBlocks(section));
            return sb.ToString();
        }

        /// <summary>
        /// Return Prefix  for placeholder.
        /// </summary>
        /// <param name="count">Times the number of repetition "--"</param>
        /// <returns>Returns prefix.</returns>
        public string GetPrefix(int count)
        {
            string prefix = "";
            for (int i = 0; i < count; i++)
            {
                prefix += "--";
            }
            return prefix;
        }

        /// <summary>
        /// Returns attributes from a list of attributes.
        /// </summary>
        /// <param name="lstAttr">List of layout attributes.</param>
        /// <returns>List of attributes.</returns>
        public string GetAttributeString(List<LayoutAttribute> lstAttr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (LayoutAttribute attr in lstAttr)
            {
                sb.Append("[");
                sb.Append(attr.Name);
                sb.Append(":");
                sb.Append(attr.Value);
                sb.Append("],");
            }
            return sb.ToString();
        }
    }
}
