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
using System.Collections;
using System.IO;
using System.Web;
#endregion

namespace SageFrame.Templating
{
    /// <summary>
    ///  Helper class for template.
    /// </summary>
    public class TemplateHelper
    {
        const string sfCol = "sfCol_";
        /// <summary>
        /// Create template information.
        /// </summary>
        /// <param name="lstXml">List of XmlTag class.</param>
        /// <returns>Object of TemplateInfo class.</returns>
        public static TemplateInfo CreateTemplateObject(List<XmlTag> lstXml)
        {
            TemplateInfo objTemp = new TemplateInfo();
            List<XmlTag> lstDetails = lstXml[0].LSTChildNodes;
            foreach (XmlTag tag in lstDetails)
            {
                if (Utils.IsValidTag(tag))
                {
                    switch (tag.TagType)
                    {
                        case XmlTagTypes.NAME:
                            objTemp.TemplateName = tag.InnerHtml;
                            break;
                        case XmlTagTypes.AUTHOR:
                            objTemp.Author = tag.InnerHtml;
                            break;
                        case XmlTagTypes.DESCRIPTION:
                            objTemp.Description = tag.InnerHtml;
                            break;
                        case XmlTagTypes.WEBSITE:
                            objTemp.Website = tag.InnerHtml;
                            break;

                    }
                }
            }
            return objTemp;
        }
        /// <summary>
        /// Create empty template information.
        /// </summary>
        /// <returns>Object of TemplateInfo class.</returns>
        public static TemplateInfo CreateEmptyTemplateObject()
        {
            TemplateInfo tempObj = new TemplateInfo();
            tempObj.TemplateName = "N/A";
            tempObj.Author = "N/A";
            tempObj.Description = "N/A";
            tempObj.Website = "N/A";
            return tempObj;
        }
        /// <summary>
        /// Obtain template left tag.
        /// </summary>
        /// <returns>Object of XmlTag class.</returns>
        public static XmlTag GetLeftTag()
        {
            return (TagBuilder(XmlTagTypes.Placeholder.ToString().ToLower(), XmlTagTypes.Placeholder, XmlAttributeTypes.NAME, XmlAttributeTypes.NAME.ToString().ToLower(), "leftA", ""));
        }
        /// <summary>
        /// XML tag builder.
        /// </summary>
        /// <param name="tagName">Tag name.</param>
        /// <param name="type">XML tag type.<see cref="T:SageFrame.Templating.xmlparser.XmlTagTypes"/></param>
        /// <param name="attType">Attribute type. <see cref="T:SageFrame.Templating.xmlparser.XmlAttributeTypes"/></param>
        /// <param name="attName">Attribute name.</param>
        /// <param name="attValue">Attribute value.</param>
        /// <param name="innerHTML">Inner HTML.</param>
        /// <returns>Object of XmlTag class.</returns>
        public static XmlTag TagBuilder(string tagName, XmlTagTypes type, XmlAttributeTypes attType, string attName, string attValue, string innerHTML)
        {
            XmlTag tag = new XmlTag();
            tag.TagType = type;
            tag.TagName = tagName;
            tag.LSTAttributes = AddAttributes(attName, attValue, attType);
            tag.InnerHtml = innerHTML;
            return tag;
        }
        /// <summary>
        /// Add new attribute.
        /// </summary>
        /// <param name="attName">Attribute name.</param>
        /// <param name="attValue">Attribute value.</param>
        /// <param name="attType">Attribute type. <see cref="T:SageFrame.Templating.xmlparser.XmlAttributeTypes"/></param>
        /// <returns>List of LayoutAttribute class.</returns>
        public static List<LayoutAttribute> AddAttributes(string attName, string attValue, XmlAttributeTypes attType)
        {
            List<LayoutAttribute> lstAttributes = new List<LayoutAttribute>();
            lstAttributes.Add(new LayoutAttribute(attName, attValue, attType));
            return lstAttributes;
        }
        /// <summary>
        /// Obtain attribute value by name.
        /// </summary>
        /// <param name="tag">Object of XmlTag class.</param>
        /// <param name="_type">Attribute type. <see cref="T:SageFrame.Templating.xmlparser.XmlAttributeTypes"/></param>
        /// <returns>Attribute value.</returns>
        public static string GetAttributeValueByName(XmlTag tag, XmlAttributeTypes _type)
        {
            string value = string.Empty;
            string name = _type.ToString();
            LayoutAttribute attr = new LayoutAttribute();
            attr = tag.LSTAttributes.Find(
                delegate(LayoutAttribute attObj)
                {
                    return (Utils.CompareStrings(attObj.Name, name));
                }
                );
            return attr == null ? "" : attr.Value;
        }
        /// <summary>
        /// Obtain attribute value by name.
        /// </summary>
        /// <param name="tag">Object of XmlTag class.</param>
        /// <param name="_type">Attribute type. <see cref="T:SageFrame.Templating.xmlparser.XmlAttributeTypes"/></param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Attribute value.</returns>
        public static string GetAttributeValueByName(XmlTag tag, XmlAttributeTypes _type, string defaultValue)
        {
            string value = string.Empty;
            string name = _type.ToString();
            LayoutAttribute attr = new LayoutAttribute();
            attr = tag.LSTAttributes.Find(
                delegate(LayoutAttribute attObj)
                {
                    return (Utils.CompareStrings(attObj.Name, name));
                }
                );
            return attr == null ? defaultValue : attr.Value;
        }
        /// <summary>
        /// Generate starting place holder.
        ///1.Check for classes like no-wrap and no-main
        //2.Check for wrap-inner and main-inner classes
        //3.If Wrap-inner is not defined simply create a wrap class and a id from the key
        //4.Check for main inner and other wrappers
        //5.If we are using special markup tags this is the place to begin the markups
        //6.Generate main-inner wrappers if present
        /// </summary>
        /// <param name="placeholder">Object of XmlTag class.</param>
        /// <returns>String format of placeholder.</returns>
        public static string GeneratePlaceholderStart(XmlTag placeholder)
        {
            

            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("<div id=\"id-{0}\" class=\"sf{1}\">", GetAttributeValueByName(placeholder, XmlAttributeTypes.NAME), GetAttributeValueByName(placeholder, XmlAttributeTypes.NAME)));
            return sb.ToString();
        }
        /// <summary>
        /// Generate ending place holder.
        /// </summary>
        /// <returns>Empty string.</returns>
        public static string GeneratePlaceholderEnd()
        {
            //1.Check the above conditions and generate closing divs
            return "";
        }
        /// <summary>
        /// Generate starting middle block.
        /// </summary>
        /// <param name="middleBlock">Object of XmlTag class.</param>
        /// <returns>String format of middle block markup.</returns>
        public static string GenerateMiddleBlockStart(XmlTag middleBlock)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(middleBlock.LSTChildNodes.Count == 0 ? GetMiddleWrapperBegin("100%") + GetMiddleDefaultBlock("100%") + GetMiddleWrapperEnd() : GetMiddleCustomMarkup(middleBlock));
            return sb.ToString();
        }
        /// <summary>
        /// Obtain middle block custom markup.
        /// </summary>
        /// <param name="middleBlock">Object of XmlTag class.</param>
        /// <returns>String format of placeholder.</returns>
        public static string GetMiddleCustomMarkup(XmlTag middleBlock)
        {
            List<KeyValue> widths = CalculateMiddleBlockWidth(middleBlock);
            StringBuilder sb = new StringBuilder();
            sb.Append(GetMiddleWrappersBegin());
            //Check for Left Blocks
            if (HasBlock(Placeholders.LEFTTOP, middleBlock) || HasBlock(Placeholders.LEFTBOTTOM, middleBlock) || (HasBlock(Placeholders.LEFTA, middleBlock) && HasBlock(Placeholders.LEFTB, middleBlock)))
            {
                if (HasBlock(Placeholders.LEFTTOP, middleBlock) && HasBlock(Placeholders.LEFTBOTTOM, middleBlock))
                {
                    if ((HasBlock(Placeholders.LEFTA, middleBlock) && HasBlock(Placeholders.LEFTB, middleBlock)))
                    {
                        //Every block is present on the left side
                        sb.Append(GetLeftBegin(widths[0].Value));
                        sb.Append(GetLeftTop());
                        sb.Append(GetLeftColsWrap(widths[1].Value, widths[2].Value, widths[0].Value));
                        sb.Append(GetLeftBottom());
                        sb.Append(GetLeftEnd());
                    }
                    else if (HasBlock(Placeholders.LEFTA, middleBlock) && !HasBlock(Placeholders.LEFTB, middleBlock))
                    {
                        sb.Append(GetLeftBegin(widths[0].Value));
                        sb.Append(GetLeftTop());
                        sb.Append(GetLeftA(widths[1].Value));
                        sb.Append(GetLeftBottom());
                        sb.Append(GetLeftEnd());
                    }
                    else if (!HasBlock(Placeholders.LEFTA, middleBlock) && HasBlock(Placeholders.LEFTB, middleBlock))
                    {
                        sb.Append(GetLeftBegin(widths[0].Value));
                        sb.Append(GetLeftTop());
                        sb.Append(GetLeftB(widths[2].Value));
                        sb.Append(GetLeftBottom());
                        sb.Append(GetLeftEnd());
                    }
                }
                else
                {
                    //Left Content Mass Blocks are not present
                    sb.Append(GetLeftBegin(widths[0].Value));
                    sb.Append(GetLeftColsWrap(widths[1].Value, widths[2].Value, widths[0].Value));
                    sb.Append(GetLeftEnd());
                }

            }
            else if (HasBlock(Placeholders.LEFTA, middleBlock))
            {
                if (HasBlock(Placeholders.LEFTTOP, middleBlock) && HasBlock(Placeholders.LEFTBOTTOM, middleBlock))
                {
                    //Every block is present on the left side
                    sb.Append(GetLeftBegin(widths[0].Value));
                    sb.Append(GetLeftTop());
                    sb.Append(GetLeftA(widths[1].Value));
                    sb.Append(GetLeftBottom());
                    sb.Append(GetLeftEnd());
                }
                else
                {
                    sb.Append(GetLeftBegin(widths[0].Value));
                    sb.Append(GetLeftA(widths[1].Value));
                    sb.Append(GetLeftEnd());
                }
            }
            else if (HasBlock(Placeholders.LEFTB, middleBlock))
            {
                if (HasBlock(Placeholders.LEFTTOP, middleBlock) && HasBlock(Placeholders.LEFTBOTTOM, middleBlock))
                {
                    //Every block is present on the left side
                    sb.Append(GetLeftBegin(widths[0].Value));
                    sb.Append(GetLeftTop());
                    sb.Append(GetLeftB(widths[2].Value));
                    sb.Append(GetLeftBottom());
                    sb.Append(GetLeftEnd());
                }
                else
                {
                    sb.Append(GetLeftBegin(widths[0].Value));
                    sb.Append(GetLeftB(widths[2].Value));
                    sb.Append(GetLeftEnd());
                }
            }




            //Check for Right Blocks
            if (HasBlock(Placeholders.RIGHTTOP, middleBlock) || HasBlock(Placeholders.RIGHTBOTTOM, middleBlock) || (HasBlock(Placeholders.RIGHTA, middleBlock) && HasBlock(Placeholders.RIGHTB, middleBlock)))
            {
                if (HasBlock(Placeholders.RIGHTTOP, middleBlock) && HasBlock(Placeholders.RIGHTBOTTOM, middleBlock))
                {
                    if (HasBlock(Placeholders.RIGHTA, middleBlock) && HasBlock(Placeholders.RIGHTB, middleBlock))
                    {
                        //Every block is present on the left side
                        sb.Append(GetRightBegin(widths[3].Value));
                        sb.Append(GetRightTop());
                        sb.Append(GetRightColsWrap(widths[4].Value, widths[5].Value, widths[3].Value));
                        sb.Append(GetRightBottom());
                        sb.Append(GetRightEnd());
                    }
                    else if (HasBlock(Placeholders.RIGHTA, middleBlock) && !HasBlock(Placeholders.RIGHTB, middleBlock))
                    {
                        sb.Append(GetRightBegin(widths[3].Value));
                        sb.Append(GetRightTop());
                        sb.Append(GetRightA(widths[4].Value));
                        sb.Append(GetRightBottom());
                        sb.Append(GetRightEnd());
                    }
                    else if (!HasBlock(Placeholders.RIGHTA, middleBlock) && HasBlock(Placeholders.RIGHTB, middleBlock))
                    {
                        sb.Append(GetRightBegin(widths[3].Value));
                        sb.Append(GetRightTop());
                        sb.Append(GetRightB(widths[5].Value));
                        sb.Append(GetRightBottom());
                        sb.Append(GetRightEnd());
                    }

                }
                else
                {
                    //Left Content Mass Blocks are not present
                    sb.Append(GetRightBegin(widths[3].Value));
                    sb.Append(GetRightColsWrap(widths[4].Value, widths[5].Value, widths[3].Value));
                    sb.Append(GetRightEnd());
                }

            }
            else if (HasBlock(Placeholders.RIGHTA, middleBlock))
            {

                if (HasBlock(Placeholders.RIGHTTOP, middleBlock) && HasBlock(Placeholders.RIGHTBOTTOM, middleBlock))
                {
                    //Every block is present on the Right side
                    sb.Append(GetRightBegin(widths[3].Value));
                    sb.Append(GetRightTop());
                    sb.Append(GetRightA(widths[4].Value));
                    sb.Append(GetRightBottom());
                    sb.Append(GetRightEnd());
                }
                else
                {
                    sb.Append(GetRightBegin(widths[3].Value));
                    sb.Append(GetRightA(widths[4].Value));
                    sb.Append(GetRightEnd());
                }
            }
            else if (HasBlock(Placeholders.RIGHTB, middleBlock))
            {
                if (HasBlock(Placeholders.RIGHTTOP, middleBlock) && HasBlock(Placeholders.RIGHTBOTTOM, middleBlock))
                {
                    //Every block is present on the Right side
                    sb.Append(GetRightBegin(widths[3].Value));
                    sb.Append(GetRightTop());
                    sb.Append(GetRightB(widths[5].Value));
                    sb.Append(GetRightBottom());
                    sb.Append(GetRightEnd());
                }
                else
                {
                    sb.Append(GetRightBegin(widths[3].Value));
                    sb.Append(GetRightA(widths[5].Value));
                    sb.Append(GetRightEnd());
                }
            }




            //Create the default Middle Block
            sb.Append(GetMiddleWrapperBegin(widths[6].Value));

            if (HasBlock(Placeholders.CONTENTTOP, middleBlock) || HasBlock(Placeholders.CONTENTBOTTOM, middleBlock))
            {
                if (HasBlock(Placeholders.CONTENTTOP, middleBlock) && HasBlock(Placeholders.CONTENTBOTTOM, middleBlock))
                {
                    //Has outer top and bottom
                    sb.Append(GetMiddleTopContent());
                    sb.Append(GetMiddleMainContentBegin());
                    if (HasBlock(Placeholders.CONTENTMAINTOP, middleBlock) && HasBlock(Placeholders.CONTENTMAINBOTTOM, middleBlock))
                    {
                        sb.Append(GetMiddleMainTop());
                        sb.Append(GetMiddleMainCurrent());
                        sb.Append(GetMiddleMainBottom());
                    }
                    else if (HasBlock(Placeholders.CONTENTMAINTOP, middleBlock) && !HasBlock(Placeholders.CONTENTMAINBOTTOM, middleBlock))
                    {
                        sb.Append(GetMiddleMainTop());
                        sb.Append(GetMiddleMainCurrent());

                    }
                    else if (!HasBlock(Placeholders.CONTENTMAINTOP, middleBlock) && HasBlock(Placeholders.CONTENTMAINBOTTOM, middleBlock))
                    {

                        sb.Append(GetMiddleMainCurrent());
                        sb.Append(GetMiddleMainBottom());
                    }
                    else if (!HasBlock(Placeholders.CONTENTMAINTOP, middleBlock) && !HasBlock(Placeholders.CONTENTMAINBOTTOM, middleBlock))
                    {

                        sb.Append(GetMiddleMainCurrent());

                    }
                    sb.Append(GetMiddleMainContentEnd());
                    sb.Append(GetMiddleBottomContent());
                }
                else if (HasBlock(Placeholders.CONTENTTOP, middleBlock) && !HasBlock(Placeholders.CONTENTBOTTOM, middleBlock))
                {
                    sb.Append(GetMiddleTopContent());
                    sb.Append(GetMiddleMainContentBegin());
                    if (HasBlock(Placeholders.CONTENTMAINTOP, middleBlock) && HasBlock(Placeholders.CONTENTMAINBOTTOM, middleBlock))
                    {
                        sb.Append(GetMiddleMainTop());
                        sb.Append(GetMiddleMainCurrent());
                        sb.Append(GetMiddleMainBottom());
                    }
                    else if (HasBlock(Placeholders.CONTENTMAINTOP, middleBlock) && !HasBlock(Placeholders.CONTENTMAINBOTTOM, middleBlock))
                    {
                        sb.Append(GetMiddleMainTop());
                        sb.Append(GetMiddleMainCurrent());

                    }
                    else if (!HasBlock(Placeholders.CONTENTMAINTOP, middleBlock) && HasBlock(Placeholders.CONTENTMAINBOTTOM, middleBlock))
                    {

                        sb.Append(GetMiddleMainCurrent());
                        sb.Append(GetMiddleMainBottom());
                    }
                    else if (!HasBlock(Placeholders.CONTENTMAINTOP, middleBlock) && !HasBlock(Placeholders.CONTENTMAINBOTTOM, middleBlock))
                    {

                        sb.Append(GetMiddleMainCurrent());

                    }
                    sb.Append(GetMiddleMainContentEnd());
                }
                else if (!HasBlock(Placeholders.CONTENTTOP, middleBlock) && HasBlock(Placeholders.CONTENTBOTTOM, middleBlock))
                {

                    sb.Append(GetMiddleMainContentBegin());
                    if (HasBlock(Placeholders.CONTENTMAINTOP, middleBlock) && HasBlock(Placeholders.CONTENTMAINBOTTOM, middleBlock))
                    {
                        sb.Append(GetMiddleMainTop());
                        sb.Append(GetMiddleMainCurrent());
                        sb.Append(GetMiddleMainBottom());
                    }
                    else if (HasBlock(Placeholders.CONTENTMAINTOP, middleBlock) && !HasBlock(Placeholders.CONTENTMAINBOTTOM, middleBlock))
                    {
                        sb.Append(GetMiddleMainTop());
                        sb.Append(GetMiddleMainCurrent());

                    }
                    else if (!HasBlock(Placeholders.CONTENTMAINTOP, middleBlock) && HasBlock(Placeholders.CONTENTMAINBOTTOM, middleBlock))
                    {

                        sb.Append(GetMiddleMainCurrent());
                        sb.Append(GetMiddleMainBottom());
                    }
                    else if (!HasBlock(Placeholders.CONTENTMAINTOP, middleBlock) && !HasBlock(Placeholders.CONTENTMAINBOTTOM, middleBlock))
                    {

                        sb.Append(GetMiddleMainCurrent());

                    }
                    sb.Append(GetMiddleMainContentEnd());
                    sb.Append(GetMiddleBottomContent());
                }
                else if (!HasBlock(Placeholders.CONTENTTOP, middleBlock) && !HasBlock(Placeholders.CONTENTBOTTOM, middleBlock))
                {

                    sb.Append(GetMiddleMainContentBegin());
                    if (HasBlock(Placeholders.CONTENTMAINTOP, middleBlock) && HasBlock(Placeholders.CONTENTMAINBOTTOM, middleBlock))
                    {
                        sb.Append(GetMiddleMainTop());
                        sb.Append(GetMiddleMainCurrent());
                        sb.Append(GetMiddleMainBottom());
                    }
                    else if (HasBlock(Placeholders.CONTENTMAINTOP, middleBlock) && !HasBlock(Placeholders.CONTENTMAINBOTTOM, middleBlock))
                    {
                        sb.Append(GetMiddleMainTop());
                        sb.Append(GetMiddleMainCurrent());

                    }
                    else if (!HasBlock(Placeholders.CONTENTMAINTOP, middleBlock) && HasBlock(Placeholders.CONTENTMAINBOTTOM, middleBlock))
                    {

                        sb.Append(GetMiddleMainCurrent());
                        sb.Append(GetMiddleMainBottom());
                    }
                    else if (!HasBlock(Placeholders.CONTENTMAINTOP, middleBlock) && !HasBlock(Placeholders.CONTENTMAINBOTTOM, middleBlock))
                    {

                        sb.Append(GetMiddleMainCurrent());

                    }
                    sb.Append(GetMiddleMainContentEnd());

                }


            }
            else
            {
                //Generate Default Middle Block
                sb.Append(GetMiddleDefaultBlock(widths[6].Value));
            }

            sb.Append(GetMiddleWrapperEnd());
            sb.Append(EndSingleDiv());
            return sb.ToString();


        }
        /// <summary>
        /// Calculate middle block width.
        /// </summary>
        /// <param name="middleBlock">Object of XmlTag class.</param>
        /// <returns>List of key value pairs of block name and block width.</returns>
        public static List<KeyValue> CalculateMiddleBlockWidth(XmlTag middleBlock)
        {
            double left = 0.0, leftA = 0.0, leftB = 0.0;
            if (HasBlock(Placeholders.LEFTTOP, middleBlock) || HasBlock(Placeholders.LEFTBOTTOM, middleBlock) || (HasBlock(Placeholders.LEFTA, middleBlock) && HasBlock(Placeholders.LEFTB, middleBlock)))
            {
                if (HasBlock(Placeholders.LEFTA, middleBlock) && HasBlock(Placeholders.LEFTB, middleBlock))
                {
                    left = 2;
                    leftA = double.Parse(CalculateColumnWidth(middleBlock, Placeholders.LEFTA));
                    leftB = double.Parse(CalculateColumnWidth(middleBlock, Placeholders.LEFTB));
                }
                else if (HasBlock(Placeholders.LEFTA, middleBlock) && !HasBlock(Placeholders.LEFTB, middleBlock))
                {
                    left = 1;
                    leftA = double.Parse(CalculateColumnWidth(middleBlock, Placeholders.LEFTA));

                }
                else if (!HasBlock(Placeholders.LEFTA, middleBlock) && HasBlock(Placeholders.LEFTB, middleBlock))
                {
                    left = 1;
                    leftB = double.Parse(CalculateColumnWidth(middleBlock, Placeholders.LEFTB));
                }
            }
            else if (HasBlock(Placeholders.LEFTA, middleBlock))
            {
                left = 1;
                leftA = double.Parse(CalculateColumnWidth(middleBlock, Placeholders.LEFTA));
            }
            else if (HasBlock(Placeholders.LEFTB, middleBlock))
            {
                left = 1;
                leftB = double.Parse(CalculateColumnWidth(middleBlock, Placeholders.LEFTB));
            }

            double totalleft = left > 0 ? (leftA + leftB) / left : 0;

            double right = 0.0, rightA = 0.0, rightB = 0.0;
            if (HasBlock(Placeholders.RIGHTTOP, middleBlock) || HasBlock(Placeholders.RIGHTBOTTOM, middleBlock) || (HasBlock(Placeholders.RIGHTA, middleBlock) && HasBlock(Placeholders.RIGHTB, middleBlock)))
            {
                if (HasBlock(Placeholders.RIGHTA, middleBlock) && HasBlock(Placeholders.RIGHTB, middleBlock))
                {
                    right = 2;
                    rightA = double.Parse(CalculateColumnWidth(middleBlock, Placeholders.RIGHTA));
                    rightB = double.Parse(CalculateColumnWidth(middleBlock, Placeholders.RIGHTB));
                }
                else if (HasBlock(Placeholders.RIGHTA, middleBlock) && !HasBlock(Placeholders.RIGHTB, middleBlock))
                {
                    right = 1;
                    rightA = double.Parse(CalculateColumnWidth(middleBlock, Placeholders.RIGHTA));

                }
                else if (!HasBlock(Placeholders.RIGHTA, middleBlock) && HasBlock(Placeholders.RIGHTB, middleBlock))
                {
                    right = 1;

                    rightB = double.Parse(CalculateColumnWidth(middleBlock, Placeholders.RIGHTB));
                }
            }
            else if (HasBlock(Placeholders.RIGHTA, middleBlock))
            {
                right = 1;
                rightA = double.Parse(CalculateColumnWidth(middleBlock, Placeholders.RIGHTA));
            }
            else if (HasBlock(Placeholders.RIGHTB, middleBlock))
            {
                right = 1;
                rightB = double.Parse(CalculateColumnWidth(middleBlock, Placeholders.RIGHTB));
            }

            double totalright = right > 0 ? (rightA + rightB) / right : 0;
            double totalWidth = 100;
            rightA = (rightA * 100) / (totalright * right);
            rightA = rightA.ToString() == "NaN" ? 0 : rightA;
            rightB = totalWidth - rightA;
            rightB = rightB.ToString() == "NaN" ? 0 : rightB;

            double middle = totalWidth - totalright;
            middle = totalWidth - totalleft - totalright;

            //double leftWidth = (totalleft * 100) / (totalleft+middle);
            //double leftWidth=totalWidth-

            leftA = (leftA * 100) / (totalleft * left);
            leftA = leftA.ToString() == "NaN" ? 0 : leftA;
            leftB = totalWidth - leftA;
            leftB = leftB.ToString() == "NaN" ? 0 : leftB;
            middle = totalWidth - totalleft - totalright;

            List<KeyValue> widthsKvp = new List<KeyValue>();
            widthsKvp.Add(new KeyValue("Left", totalleft.ToString()));
            widthsKvp.Add(new KeyValue("LeftA", leftA.ToString()));
            widthsKvp.Add(new KeyValue("LeftB", leftB.ToString()));
            widthsKvp.Add(new KeyValue("Right", totalright.ToString()));
            widthsKvp.Add(new KeyValue("RightA", rightA.ToString()));
            widthsKvp.Add(new KeyValue("RightB", rightB.ToString()));
            widthsKvp.Add(new KeyValue("Center", middle.ToString()));

            DecreaseWidthForAdjustment(ref widthsKvp);

            return widthsKvp;

        }
        /// <summary>
        /// Decrease block width for adjustment.
        /// </summary>
        /// <param name="lstWidth">List of key value pairs of block name and block width.</param>
        public static void DecreaseWidthForAdjustment(ref List<KeyValue> lstWidth)
        {
            foreach (KeyValue kvp in lstWidth)
            {
                if (kvp.Value != "NaN" && kvp.Value != "0")
                {
                    kvp.Value = (double.Parse(kvp.Value.ToString())).ToString();
                    //kvp.Value = kvp.Value + "%";
                }
            }
        }
        /// <summary>
        /// Obtain midddle block default markup.
        /// </summary>
        /// <returns>Empty string.</returns>
        public static string GetMiddleDefaultMarkup()
        {
            return "";
        }
        /// <summary>
        /// Generate ending middle block markup.
        /// </summary>
        /// <returns>Empty string.</returns>
        public static string GenerateMiddleBlockEnd()
        {
            return "";
        }
        /// <summary>
        /// Generate starting middle wrapper markup.
        /// </summary>
        /// <returns>String format of middle wrapper markup.</returns>
        public static string GetMiddleWrappersBegin()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='sfBodyContent' class='sfOuterwrapper sfCurve clearfix'>");
            return sb.ToString();
        }

        #region TopMarkupGenerator
        /// <summary>
        /// Obtain top block markup.
        /// </summary>
        /// <param name="topBlock">Object of XmlTag class.</param>
        /// <returns>String format of top blocks.</returns>
        public static string GetTopBlocks(XmlTag topBlock)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(topBlock.LSTChildNodes.Count == 0 ? GetTopBlockMarkupDefault() : GetTopBlockMarkupCustom(topBlock));
            return sb.ToString();
        }
        /// <summary>
        /// Obtain default top block markup.
        /// </summary>
        /// <returns>String format of top block markup.</returns>
        public static string GetTopBlockMarkupDefault()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='#sfHeader' class='sfOuterwrapper sfCurve'><div class='sfContainer sfCurve'>");
            sb.Append(AppendDroppableArea("top"));
            sb.Append("</div></div>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain custom top block markup.
        /// </summary>
        /// <param name="section">Object of XmlTag class.</param>
        /// <returns>String format of custom top block markup.</returns>
        public static string GetTopBlockMarkupCustom(XmlTag section)
        {
            StringBuilder sb = new StringBuilder();
            foreach (XmlTag placeholder in section.LSTChildNodes)
            {
                if (IsCustomBlockDefined(placeholder))
                {
                    sb.Append(ParseCustomBlocks(placeholder));
                }
                else
                {
                    if (IsSpotLight(placeholder))
                    {
                        sb.Append(ParseSpotlight(placeholder));
                    }
                    else
                    {
                        string id = "sf" + UppercaseFirst(GetAttributeValueByName(placeholder, XmlAttributeTypes.NAME));
                        sb.Append("<div id='" + id + "' class='sfOuterwrapper sfCurve'>");
                        sb.Append(GetCommonWrapper(placeholder.InnerHtml));
                        sb.Append("</div>");
                    }
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// Obtain common wrapper.
        /// </summary>
        /// <param name="position">Position</param>
        /// <returns>String format of wrapper holder.</returns>
        public static string GetCommonWrapper(string position)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea(position));
            sb.Append("</div>");

            return sb.ToString();
        }

        /// <summary>
        ///Change first character capitalized.
        /// </summary>
        /// <param name="s">String for first character capitalized.</param>
        /// <returns>String with the first character capitalized.</returns>
        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
        #endregion

        #region BottomMarkupGenerator
        /// <summary>
        /// Obtain bottom block markup.
        /// </summary>
        /// <param name="BottomBlock">Object of XmlTag class.</param>
        /// <returns>String format of buttom block markup.</returns>
        public static string GetBottomBlocks(XmlTag BottomBlock)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(BottomBlock.LSTChildNodes.Count == 0 ? GetBottomBlockMarkupDefault() : GetBottomBlockMarkupCustom(BottomBlock));
            return sb.ToString();
        }
        /// <summary>
        /// Obtain default bottom block markup.
        /// </summary>
        /// <returns>String format of buttom block markup.</returns>
        public static string GetBottomBlockMarkupDefault()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='#sfFooter' class='sfOuterwrapper sfCurve'><div class='sfContainer sfCurve'>");
            sb.Append(AppendDroppableArea("footer"));
            sb.Append("</div></div>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain custom bottom block markup.
        /// </summary>
        /// <param name="section">Object of XmlTag class.</param>
        /// <returns>String format of buttom block markup.</returns>
        public static string GetBottomBlockMarkupCustom(XmlTag section)
        {
            StringBuilder sb = new StringBuilder();
            foreach (XmlTag placeholder in section.LSTChildNodes)
            {
                if (IsCustomBlockDefined(placeholder))
                {
                    sb.Append(ParseCustomBlocks(placeholder));
                }
                else
                {
                    if (IsSpotLight(placeholder))
                    {
                        sb.Append(ParseSpotlight(placeholder));
                    }
                    else
                    {
                        string id = "sf" + UppercaseFirst(GetAttributeValueByName(placeholder, XmlAttributeTypes.NAME));
                        sb.Append("<div id='" + id + "' class='sfOuterwrapper sfCurve'>");
                        sb.Append(GetCommonWrapper(placeholder.InnerHtml));
                        sb.Append("</div>");
                    }
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// Check for custom block.
        /// </summary>
        /// <param name="placeholder">Object of XmlTag class.</param>
        /// <returns>True for defined custom block.</returns>
        static bool IsCustomBlockDefined(XmlTag placeholder)
        {

            string activeTemplate = HttpContext.Current.Session["SageFrame.ActiveTemplate"] != null ? HttpContext.Current.Session["SageFrame.ActiveTemplate"].ToString() : "Default";
            string pchName = GetAttributeValueByName(placeholder, XmlAttributeTypes.NAME);
            string FilePath = HttpContext.Current.Server.MapPath("~/") + "//" + activeTemplate + "//sections";
            bool status = false;
            if (Directory.Exists(FilePath))
            {
                DirectoryInfo dir = new DirectoryInfo(FilePath);
                foreach (FileInfo file in dir.GetFiles("*.htm"))
                {
                    if (Utils.CompareStrings(Path.GetFileNameWithoutExtension(file.Name), pchName))
                    {
                        status = true;
                        break;
                    }
                }
            }
            return status;
        }
        /// <summary>
        /// Parse custom block.
        /// </summary>
        /// <param name="placeholder">Object of XmlTag class.</param>
        /// <returns>String format of HTML markup.</returns>
        static string ParseCustomBlocks(XmlTag placeholder)
        {
            string activeTemplate = HttpContext.Current.Session["SageFrame.ActiveTemplate"] != null ? HttpContext.Current.Session["SageFrame.ActiveTemplate"].ToString() : "Default";

            string FilePath = HttpContext.Current.Server.MapPath("~/") + "//" + activeTemplate + "//sections";
            string fileName = GetAttributeValueByName(placeholder, XmlAttributeTypes.NAME);
            fileName = UppercaseFirst(fileName);
            FilePath = FilePath + fileName + ".htm";
            HTMLBuilder hb = new HTMLBuilder();
            StringBuilder sb = new StringBuilder();
            sb.Append(hb.ReadHTML(FilePath));
            return sb.ToString();
        }
        #endregion

        #region LeftBlocks
        //Get the Left Blocks
        /// <summary>
        /// Obtain starting left markup.
        /// </summary>
        /// <param name="Left">Class to recognize for left markup.</param>
        /// <returns>String format of starting left markup.</returns>
        public static string GetLeftBegin(string Left)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='sfLeft' class=' " + sfCol + Left + "'><div class='sfContainer sfCurve'>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain ending left markup.
        /// </summary>
        /// <returns>String format of ending left markup.</returns>
        public static string GetLeftEnd()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("</div></div>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain top left markup.
        /// </summary>
        /// <returns>String format of top left markup.</returns>
        public static string GetLeftTop()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfLeftTop sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("lefttop"));
            sb.Append("</div>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain bottom left markup.
        /// </summary>
        /// <returns>String format of bottom left markup.</returns>
        public static string GetLeftBottom()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfLeftBottom sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("leftbottom"));
            sb.Append("</div>");
            return sb.ToString();
        }
        /// <summary>
        ///  Obtain left markup with wrapper class.
        /// </summary>
        /// <param name="LeftA">Panename LeftA's class.</param>
        /// <param name="LeftB">Panename LeftB's class.</param>
        /// <param name="Left">Optional parameter.</param>
        /// <returns>String format of left markup with wrapper class.</returns>
        public static string GetLeftColsWrap(string LeftA, string LeftB, string Left)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfColswrap sfCurve sfDouble clearfix'><div class='sfLeftA " + sfCol + LeftA + "'><div class='sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("leftA"));
            sb.Append("</div></div><div class='sfLeftB' class=' " + sfCol + LeftB + "'><div class='sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("leftB"));
            sb.Append("</div></div></div>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain LeftA.
        /// </summary>
        /// <param name="LeftA">Panename LeftA's class.</param>
        /// <returns>String format of markup with LeftA's class.</returns>
        public static string GetLeftA(string LeftA)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfColswrap sfCurve sfSingle clearfix'><div class='sfLeftA " + sfCol + LeftA + "'><div class='sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("leftA"));
            sb.Append("</div></div></div>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain LeftB.
        /// </summary>
        /// <param name="LeftB">Panename LeftB's class.</param>
        /// <returns>String format of markup with LeftB's class.</returns>
        public static string GetLeftB(string LeftB)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfColswrap sfCurve sfSingle clearfix'><div class='sfLeftB " + sfCol + LeftB + "'><div class='sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("leftB"));
            sb.Append("</div></div></div>");
            return sb.ToString();
        }
        #endregion

        #region RightBlocks
        /// <summary>
        /// Obtain starting right markup.
        /// </summary>
        /// <param name="Right">Class to recognize for right markup.</param>
        /// <returns>String format of starting right markup.</returns>
        public static string GetRightBegin(string Right)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='sfRight' class='" + sfCol + Right + "'><div class='sfContainer sfCurve'>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain ending right markup.
        /// </summary>
        /// <returns>String format of ending right markup.</returns>
        public static string GetRightEnd()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("</div></div>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain top right markup.
        /// </summary>
        /// <returns>String format of top right markup.</returns>
        public static string GetRightTop()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfRightTop sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("rightTop"));
            sb.Append("</div>");
            return sb.ToString();
        }
        /// <summary>
        ///  Obtain bottom right markup.
        /// </summary>
        /// <returns>String format of bottom right markup.</returns>
        public static string GetRightBottom()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfRightBottom sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("rightBottom"));
            sb.Append("</div>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain right markup with wrapper class.
        /// </summary>
        /// <param name="RightA">Panename RightA's class.</param>
        /// <param name="RightB">Panename RightB's class.</param>
        /// <param name="Right">Optional parameter</param>
        /// <returns>String format of right markup with wrapper class.</returns>
        public static string GetRightColsWrap(string RightA, string RightB, string Right)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfColswrap sfCurve sfDouble clearfix'><div  class='sfRightA " + sfCol + RightA + "'><div class='sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("rightA"));
            sb.Append("</div></div><div  class='sfRightB " + sfCol + RightB + "'><div class='sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("rightB"));
            sb.Append("</div></div></div>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain RightA's markup.
        /// </summary>
        /// <param name="RightA">Panename RightA's class.</param>
        /// <returns>String format of RightA markup.</returns>
        public static string GetRightA(string RightA)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfColswrap sfCurve sfSingle clearfix'><div class='sfRightA " + sfCol + RightA + "'><div class='sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("rightA"));
            sb.Append("</div></div></div>");
            return sb.ToString();
        }
        /// <summary>
        ///  Obtain RightB's markup.
        /// </summary>
        /// <param name="RightB">Panename RightB's class.</param>
        /// <returns>String format of  RightB markup.</returns>
        public static string GetRightB(string RightB)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfColswrap sfCurve sfSingle'><div  class='sfRightB " + sfCol + RightB + "'><div class='sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("rightB"));
            sb.Append("</div></div></div>");
            return sb.ToString();
        }
        #endregion

        #region MiddleBlocks
        /// <summary>
        /// Obtain default middle block.
        /// </summary>
        /// <param name="Center">Optional parameter.</param>
        /// <returns>String format of middle block.</returns>
        public static string GetMiddleDefaultBlock(string Center)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfMainContent sfCurve'><div class='sfMiddleMainCurrent sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("middlemaincurrent"));
            sb.Append("</div></div>");
            return sb.ToString();
        }
        /// <summary>
        ///  Obtain top middle markup.
        /// </summary>
        /// <returns>String format of top middle markup.</returns>
        public static string GetMiddleTopContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfMiddleTopContent sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("middletop"));
            sb.Append("</div>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain middle markup with wrapper class.
        /// </summary>
        /// <param name="Center">Wrapper class.</param>
        /// <returns>String format of middle markup with wrapper class.</returns>
        public static string GetMiddleWrapperBegin(string Center)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='sfMainWrapper' class='" + sfCol + Center + "'><div class='sfContainer'>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain ending middle block markup.
        /// </summary>
        /// <returns>String format of ending markup.</returns>
        public static string GetMiddleWrapperEnd()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("</div></div>");
            return sb.ToString();
        }

        /// <summary>
        /// Obtain bottom middle markup.
        /// </summary>
        /// <returns>String format of bottom middle markup.</returns>
        public static string GetMiddleBottomContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div  class='sfMiddleBottomContent sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("middlebottom"));
            sb.Append("</div>");
            return sb.ToString();
        }
        /// <summary>
        ///  Obtain starting middlemain content markup.
        /// </summary>
        /// <returns>String format of middlemain content markup.</returns>
        public static string GetMiddleMainContentBegin()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfMainContent sfCurve'>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain ending middlemain content markup.
        /// </summary>
        /// <returns>String format of ending middlemain content markup.</returns>
        public static string GetMiddleMainContentEnd()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("</div>");
            return sb.ToString();
        }
        /// <summary>
        /// Ending of single div.
        /// </summary>
        /// <returns>String format of closing div.</returns>
        public static string EndSingleDiv()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("</div>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain middlemain top block.
        /// </summary>
        /// <returns>String format of maiddle main top markup.</returns>
        public static string GetMiddleMainTop()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfMiddleMainTop sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("middlemaintop"));
            sb.Append("</div>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain middlemain current block.
        /// </summary>
        /// <returns>String format of middlemain current block.</returns>
        public static string GetMiddleMainCurrent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfMiddleMainCurrent sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("middlemaincurrent"));
            sb.Append("</div>");
            return sb.ToString();

        }
        /// <summary>
        /// Obtain middle main bottom.
        /// </summary>
        /// <returns>String format of middlemain bottom markup.</returns>
        public static string GetMiddleMainBottom()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfMiddleMainBottom sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea("middlemainbottom"));
            sb.Append("</div>");
            return sb.ToString();
        }
        /// <summary>
        /// Obtain seprate div row for layout purpose.
        /// </summary>
        /// <returns>String format of seprate div row . </returns>
        public static string GetClearFix()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div style='clear:both'></div>");
            return sb.ToString();
        }

        #endregion
        /// <summary>
        /// Check for spotlight.
        /// </summary>
        /// <param name="placeholder">Object of XmlTag class.</param>
        /// <returns>True for spotlight.</returns>
        static bool IsSpotLight(XmlTag placeholder)
        {
            string pchName = GetAttributeValueByName(placeholder, XmlAttributeTypes.NAME);
            bool status = false;
            if (Utils.CompareStrings(pchName, "spotlight"))
            {
                status = true;
            }
            return status;
        }
        /// <summary>
        /// parsing spotlight markup.
        /// </summary>
        /// <param name="placeholder">Object of XmlTag class.</param>
        /// <returns>String format of spotlight markup.</returns>
        static string ParseSpotlight(XmlTag placeholder)
        {
            StringBuilder sb = new StringBuilder();
            string positions = placeholder.InnerHtml;
            string[] positionsAr = positions.Split(',');
            double spotWidth = 100 / positionsAr.Length;
            string width = spotWidth.ToString() + "%";
            string minheight = GetAttributeValueByName(placeholder, XmlAttributeTypes.MINHEIGHT, "200px");


            sb.Append("<div id='sfSpotLight' class='sfOuterwrapper sfCurve'><div class='sfContainer sfCurve clearfix'>");

            for (int i = 0; i < positionsAr.Length; i++)
            {
                string adjustedWidth = width;

                string style = GetAttributeValueByName(placeholder, XmlAttributeTypes.CSSCLASS);
                if (i == 0)
                {
                    style += " sfFirst";
                }
                if (i == positionsAr.Length - 1)
                {
                    style += " sfLast";
                }

                sb.Append("<div class='sfSpotLight" + style + "' style='width:" + adjustedWidth + ";'><div class='sfWrapper sfCurve'>");
                //sb.Append(positionsAr[i]);
                sb.Append(AppendDroppableArea(positionsAr[i]));
                sb.Append("</div></div>");
            }
            sb.Append("</div></div>");
            return sb.ToString();
        }
        /// <summary>
        /// Check for block.
        /// </summary>
        /// <param name="pch">Placeholders.<see cref="T:SageFrame.Templating.Placeholders"/></param>
        /// <param name="middleBlock">Object of XmlTag class.</param>
        /// <returns>True for block.</returns>
        public static bool HasBlock(Placeholders pch, XmlTag middleBlock)
        {
            bool status = false;
            status = middleBlock.LSTChildNodes.Exists(
                delegate(XmlTag tag)
                {
                    return (Utils.CompareStrings(GetAttributeValueByName(tag, XmlAttributeTypes.NAME), pch));
                }
                );
            return status;
        }
        /// <summary>
        /// Render HTML tag.
        /// </summary>
        /// <returns>Empty string.</returns>
        public static string RenderHTML()
        {

            //Add the right placeholders with appropriate ids and classes and return the whole markup
            return "";
        }
        /// <summary>
        /// Calculates the column width for the middle block
        /// Takes the ColumnWidth from the Parent Node which applies to right and left
        /// However the inline "width" attributes overrides the Parent's colwidth attribute
        /// </summary>
        /// <param name="section">Object of XmlTag class.</param>
        /// <param name="_type">Placeholders.<see cref="T:SageFrame.Templating.Placeholders"/></param>
        /// <returns>String format of calculated width.</returns>
        public static string CalculateColumnWidth(XmlTag section, Placeholders _type)
        {
            string width = "20";
            foreach (XmlTag tag in section.LSTChildNodes)
            {
                if (Utils.CompareStrings(_type, GetAttributeValueByName(tag, XmlAttributeTypes.NAME)))
                {
                    int widthIndex = -1;
                    widthIndex = tag.LSTAttributes.FindIndex(
                        delegate(LayoutAttribute tagAttr)
                        {
                            return (Utils.CompareStrings(tagAttr.Name, XmlAttributeTypes.WIDTH));
                        }
                        );
                    if (widthIndex > -1)
                    {
                        width = tag.LSTAttributes[widthIndex].Value;
                    }
                    else
                    {
                        foreach (LayoutAttribute attr in section.LSTAttributes)
                        {
                            if (Utils.CompareStrings(attr.Name, XmlAttributeTypes.WIDTH))
                            {
                                width = attr.Value;
                            }
                            else if (Utils.CompareStrings(attr.Name, XmlAttributeTypes.COLWIDTH))
                            {
                                width = attr.Value;
                            }
                        }
                    }
                }
            }
            return width;
        }
        /// <summary>
        /// Calculate main width.
        /// </summary>
        /// <returns>Empty string.</returns>
        public static string CalculateMainWidth()
        {
            return "";
        }
        /// <summary>
        /// Append droppable area.
        /// </summary>
        /// <param name="position">Appending position.</param>
        /// <returns>String format of droppable area markup.</returns>
        public static string AppendDroppableArea(string position)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfPosition'>" + position + "</div>");
            return sb.ToString();
        }
        /// <summary>
        /// Append placeholder.
        /// </summary>
        /// <param name="position">Placeholder position.</param>
        /// <returns>String format of placeholder markup.</returns>
        public static string AppendPlaceHolder(string position)
        {
            string PlaceHolderID = "pch_" + position;
            StringBuilder sb = new StringBuilder();
            sb.Append("<asp:PlaceHolder ID=" + PlaceHolderID + " runat=\"server\">");
            return sb.ToString();
        }


    }
}
