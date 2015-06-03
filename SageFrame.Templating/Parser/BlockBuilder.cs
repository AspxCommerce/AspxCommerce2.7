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
    /// Helper class for building block.
    /// </summary>
    public class BlockBuilder
    {
        /// <summary>
        /// Obtain customize middleblock markup.
        /// </summary>
        /// <param name="middleBlock">Object of XmlTag class.</param>
        /// <param name="lstWrapper">List of CustomWrapper class.</param>
        /// <param name="Mode">Mode</param>
        /// <returns>String format of customized middleblock markup.</returns>
        public static string GetMiddleCustomMarkup(XmlTag middleBlock, List<CustomWrapper> lstWrapper, int Mode)
        {
            List<KeyValue> widths = Calculator.CalculateMiddleBlockWidth(middleBlock);
            StringBuilder sb = new StringBuilder();
            sb.Append(HtmlBuilder.GetMiddleWrappersBegin());
            if (Decide.HasBlock(Placeholders.FULLTOPSPAN, middleBlock))
            {
                sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.FULLTOPSPAN, middleBlock, lstWrapper, Mode));
            }
            //Check for Left Blocks
            //add a wrapper to wrap left middle and right blocks
            sb.Append("<div class='sfMoreblocks clearfix'>");
            foreach (CustomWrapper start in lstWrapper)
            {
                if (start.Type == "placeholder" && start.Start == "left")
                {
                    string style = "";
                    for (int i = 1; i <= start.Depth; i++)
                    {
                        if (i == 1)
                        {
                            style = start.Depth > 1 ? string.Format("sfBlockwrap {0}", start.Class) : string.Format("sfBlockwrap {0} clearfix", start.Class);
                            sb.Append("<div class='" + style + "'>");
                        }
                        else
                        {
                            style = start.Depth == i ? string.Format("sfBlockwrap {0} sf{1} clearfix", start.Class, i) : string.Format("sfBlockwrap {0} sf{1}", start.Class, i);
                            sb.Append("<div class='" + style + "'>");
                        }
                    }

                }
            }
            //Build Left Block
            switch (Decide.LeftBlockMode(middleBlock))
            {
                case 0:
                    break;
                case 1:
                    ///All the blocks
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    //sb.Append(HtmlBuilder.GetLeftTop(Mode, Utils.GetTagInnerHtml(Placeholders.LEFTTOP, middleBlock)));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.LEFTTOP));

                    sb.Append(HtmlBuilder.GetLeftColsWrap(widths[1].Value, widths[2].Value, widths[0].Value, Mode, Utils.GetTagInnerHtml(Placeholders.LEFTA, middleBlock), Utils.GetTagInnerHtml(Placeholders.LEFTB, middleBlock), lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.LEFTBOTTOM));

                    sb.Append(HtmlBuilder.GetLeftEnd());
                    break;
                case 3:
                    ///All the blocks without leftA
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.LEFTTOP));

                    sb.Append(HtmlBuilder.GetLeftRightCol(widths[1].Value, Mode, Placeholders.LEFTB, lstWrapper, middleBlock));

                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.LEFTBOTTOM));


                    sb.Append(HtmlBuilder.GetLeftEnd());
                    break;
                case 2:
                    ///All the blocks without leftB
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.LEFTTOP));

                    sb.Append(HtmlBuilder.GetLeftRightCol(widths[1].Value, Mode, Placeholders.LEFTA, lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.LEFTBOTTOM));


                    sb.Append(HtmlBuilder.GetLeftEnd());
                    break;
                case 4:
                    ///All the blocks without leftA
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.LEFTTOP));

                    sb.Append(HtmlBuilder.GetLeftRightCol(widths[1].Value, Mode, Placeholders.LEFTA, lstWrapper, middleBlock));

                    sb.Append(HtmlBuilder.GetLeftEnd());
                    break;
                case 5:
                    ///All the blocks without leftA
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.LEFTTOP));

                    sb.Append(HtmlBuilder.GetLeftRightCol(widths[2].Value, Mode, Placeholders.LEFTB, lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetLeftEnd());
                    break;
                case 6:
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    sb.Append(HtmlBuilder.GetLeftRightCol(widths[2].Value, Mode, Placeholders.LEFTB, lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.LEFTBOTTOM));


                    sb.Append(HtmlBuilder.GetLeftEnd());
                    break;
                case 7:
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    sb.Append(HtmlBuilder.GetLeftRightCol(widths[1].Value, Mode, Placeholders.LEFTA, lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.LEFTBOTTOM));


                    sb.Append(HtmlBuilder.GetLeftEnd());
                    break;
                case 8:
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    sb.Append(HtmlBuilder.GetLeftColsWrap(widths[1].Value, widths[2].Value, widths[0].Value, Mode, Utils.GetTagInnerHtml(Placeholders.LEFTA, middleBlock), Utils.GetTagInnerHtml(Placeholders.LEFTB, middleBlock), lstWrapper, middleBlock));

                    sb.Append(HtmlBuilder.GetLeftEnd());
                    break;
                case 9:
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    sb.Append(HtmlBuilder.GetLeftRightCol(widths[2].Value, Mode, Placeholders.LEFTB, lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetLeftEnd());
                    break;
                case 10:
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    sb.Append(HtmlBuilder.GetLeftRightCol(widths[1].Value, Mode, Placeholders.LEFTA, lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetLeftEnd());
                    break;
                case 11:
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.LEFTTOP));

                    sb.Append(HtmlBuilder.GetLeftColsWrap(widths[1].Value, widths[2].Value, widths[0].Value, Mode, Utils.GetTagInnerHtml(Placeholders.LEFTA, middleBlock), Utils.GetTagInnerHtml(Placeholders.LEFTB, middleBlock), lstWrapper, middleBlock));


                    sb.Append(HtmlBuilder.GetLeftEnd());
                    break;
                case 12:
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    sb.Append(HtmlBuilder.GetLeftColsWrap(widths[1].Value, widths[2].Value, widths[0].Value, Mode, Utils.GetTagInnerHtml(Placeholders.LEFTA, middleBlock), Utils.GetTagInnerHtml(Placeholders.LEFTB, middleBlock), lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.LEFTBOTTOM));

                    sb.Append(HtmlBuilder.GetLeftEnd());
                    break;
            }


            foreach (CustomWrapper start in lstWrapper)
            {
                if (start.Type == "placeholder" && start.End == "left")
                {

                    for (int i = 1; i <= start.Depth; i++)
                    {
                        sb.Append("</div>");
                    }

                }
            }

            //Create the default Middle Block
            foreach (CustomWrapper start in lstWrapper)
            {
                if (start.Type == "placeholder" && start.Start == "middle")
                {
                    string style = "";
                    for (int i = 1; i <= start.Depth; i++)
                    {
                        if (i == 1)
                        {
                            style = start.Depth > 1 ? string.Format("sfBlockwrap {0}", start.Class) : string.Format("sfBlockwrap {0} clearfix", start.Class);
                            sb.Append("<div class='" + style + "'>");
                        }
                        else
                        {
                            style = start.Depth == i ? string.Format("sfBlockwrap {0} sf{1} clearfix", start.Class, i) : string.Format("sfBlockwrap {0} sf{1}", start.Class, i);
                            sb.Append("<div class='" + style + "'>");
                        }
                    }

                }
            }
            sb.Append(HtmlBuilder.GetMiddleWrapperBegin(widths[6].Value));

            if (Decide.HasBlock(Placeholders.MIDDLETOP, middleBlock) || Decide.HasBlock(Placeholders.MIDDLEBOTTOM, middleBlock) || 1 == 1)
            {
                if (Decide.HasBlock(Placeholders.MIDDLETOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEBOTTOM, middleBlock))
                {
                    //Has outer top and bottom
                    sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLETOP, middleBlock, lstWrapper, Mode));
                    sb.Append(HtmlBuilder.GetMiddleMainContentBegin());
                    if (Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINTOP, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINBOTTOM, middleBlock, lstWrapper, Mode));
                    }
                    else if (Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINTOP, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));

                    }
                    else if (!Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {

                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINBOTTOM, middleBlock, lstWrapper, Mode));
                    }
                    else if (!Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {

                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));

                    }
                    sb.Append(HtmlBuilder.GetMiddleMainContentEnd());
                    sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEBOTTOM, middleBlock, lstWrapper, Mode));
                }
                else if (Decide.HasBlock(Placeholders.MIDDLETOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEBOTTOM, middleBlock))
                {
                    sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLETOP, middleBlock, lstWrapper, Mode));
                    sb.Append(HtmlBuilder.GetMiddleMainContentBegin());
                    if (Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINTOP, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINBOTTOM, middleBlock, lstWrapper, Mode));
                    }
                    else if (Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINTOP, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));

                    }
                    else if (!Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {

                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINBOTTOM, middleBlock, lstWrapper, Mode));
                    }
                    else if (!Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {

                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));

                    }
                    sb.Append(HtmlBuilder.GetMiddleMainContentEnd());
                }
                else if (!Decide.HasBlock(Placeholders.MIDDLETOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEBOTTOM, middleBlock))
                {

                    sb.Append(HtmlBuilder.GetMiddleMainContentBegin());
                    if (Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINTOP, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINBOTTOM, middleBlock, lstWrapper, Mode));
                    }
                    else if (Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINTOP, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));

                    }
                    else if (!Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {

                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINBOTTOM, middleBlock, lstWrapper, Mode));
                    }
                    else if (!Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {

                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));

                    }
                    sb.Append(HtmlBuilder.GetMiddleMainContentEnd());
                    sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEBOTTOM, middleBlock, lstWrapper, Mode));
                }
                else if (!Decide.HasBlock(Placeholders.MIDDLETOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEBOTTOM, middleBlock))
                {

                    sb.Append(HtmlBuilder.GetMiddleMainContentBegin());
                    if (Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINTOP, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINBOTTOM, middleBlock, lstWrapper, Mode));
                    }
                    else if (Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINTOP, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));

                    }
                    else if (!Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {

                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINBOTTOM, middleBlock, lstWrapper, Mode));
                    }
                    else if (!Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {

                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));

                    }
                    sb.Append(HtmlBuilder.GetMiddleMainContentEnd());


                }
                foreach (CustomWrapper start in lstWrapper)
                {
                    if (start.Type == "placeholder" && start.End == "middle")
                    {

                        for (int i = 1; i <= start.Depth; i++)
                        {
                            sb.Append("</div>");
                        }
                    }
                }

            }
            else
            {
                //Generate Default Middle Block
                sb.Append(HtmlBuilder.GetMiddleDefaultBlock(widths[6].Value, Mode));
                foreach (CustomWrapper start in lstWrapper)
                {
                    if (start.Type == "placeholder" && start.End == "middle")
                    {

                        for (int i = 1; i <= start.Depth; i++)
                        {
                            sb.Append("</div>");
                        }
                    }
                }
            }



            sb.Append(HtmlBuilder.GetMiddleWrapperEnd());

            //Check for Right Blocks
            foreach (CustomWrapper start in lstWrapper)
            {
                if (start.Type == "placeholder" && start.Start == "right")
                {
                    string style = "";
                    for (int i = 1; i <= start.Depth; i++)
                    {
                        if (i == 1)
                        {
                            style = start.Depth > 1 ? string.Format("sfBlockwrap {0}", start.Class) : string.Format("sfBlockwrap {0} clearfix", start.Class);
                            sb.Append("<div class='" + style + "'>");
                        }
                        else
                        {
                            style = start.Depth == i ? string.Format("sfBlockwrap {0} sf{1} clearfix", start.Class, i) : string.Format("sfBlockwrap {0} sf{1}", start.Class, i);
                            sb.Append("<div class='" + style + "'>");
                        }
                    }

                }
            }

            //Build Right Block
            switch (Decide.RightBlockMode(middleBlock))
            {
                case 0:
                    break;
                case 1:
                    ///All the blocks
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.RIGHTTOP));
                    sb.Append(HtmlBuilder.GetRightColsWrap(widths[4].Value, widths[5].Value, widths[3].Value, Mode, lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.RIGHTBOTTOM));
                    sb.Append(HtmlBuilder.GetRightEnd());
                    break;
                case 3:
                    ///All the blocks without leftA
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.RIGHTTOP));
                    sb.Append(HtmlBuilder.GetLeftRightCol(widths[5].Value, Mode, Placeholders.RIGHTB, lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.RIGHTBOTTOM));
                    sb.Append(HtmlBuilder.GetRightEnd());
                    break;
                case 2:
                    ///All the blocks without leftB
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.RIGHTTOP));
                    sb.Append(HtmlBuilder.GetRightA(widths[4].Value, Mode, Utils.GetTagInnerHtml(Placeholders.RIGHTA, middleBlock)));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.RIGHTBOTTOM));
                    sb.Append(HtmlBuilder.GetRightEnd());
                    break;
                case 4:
                    ///All the blocks without leftA
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.RIGHTTOP));
                    sb.Append(HtmlBuilder.GetLeftRightCol(widths[4].Value, Mode, Placeholders.RIGHTA, lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetRightEnd());
                    break;
                case 5:
                    ///All the blocks without leftA
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.RIGHTTOP));
                    sb.Append(HtmlBuilder.GetLeftRightCol(widths[5].Value, Mode, Placeholders.RIGHTB, lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetRightEnd());
                    break;
                case 6:
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetLeftRightCol(widths[5].Value, Mode, Placeholders.RIGHTB, lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.RIGHTBOTTOM));
                    sb.Append(HtmlBuilder.GetRightEnd());
                    break;
                case 7:
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetLeftRightCol(widths[4].Value, Mode, Placeholders.RIGHTA, lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.RIGHTBOTTOM));
                    sb.Append(HtmlBuilder.GetRightEnd());
                    break;
                case 8:
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetRightColsWrap(widths[4].Value, widths[5].Value, widths[3].Value, Mode, lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetRightEnd());
                    break;
                case 9:
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetLeftRightCol(widths[5].Value, Mode, Placeholders.RIGHTB, lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetRightEnd());
                    break;
                case 10:
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetLeftRightCol(widths[4].Value, Mode, Placeholders.RIGHTA, lstWrapper, middleBlock));
                    sb.Append(HtmlBuilder.GetRightEnd());
                    break;
                case 11:
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.RIGHTTOP));
                    sb.Append(HtmlBuilder.GetRightColsWrap(widths[4].Value, widths[5].Value, widths[3].Value, Mode, lstWrapper, middleBlock));


                    sb.Append(HtmlBuilder.GetRightEnd());
                    break;
                case 12:
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetRightColsWrap(widths[4].Value, widths[5].Value, widths[3].Value, Mode, lstWrapper, middleBlock));

                    sb.Append(HtmlBuilder.GetLeftRightTopBottom(Mode, lstWrapper, middleBlock, Placeholders.RIGHTBOTTOM));

                    sb.Append(HtmlBuilder.GetRightEnd());
                    break;
            }

            foreach (CustomWrapper start in lstWrapper)
            {
                if (start.Type == "placeholder" && start.End == "right")
                {

                    for (int i = 1; i <= start.Depth; i++)
                    {
                        sb.Append("</div>");
                    }
                }
            }
            sb.Append("</div>");


            if (Decide.HasBlock(Placeholders.FULLBOTTOMSPAN, middleBlock))
            {
                sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.FULLBOTTOMSPAN, middleBlock, lstWrapper, Mode));
            }
            sb.Append(HtmlBuilder.EndSingleDiv());
            sb.Append(HtmlBuilder.EndSingleDiv());

            return sb.ToString();


        }
        /// <summary>
        /// Obtain top blocks.
        /// </summary>
        /// <param name="topBlock">Object of XmlTag class.</param>
        /// <param name="lstWrappers">List of CustomWrapper class.</param>
        /// <param name="Mode">Mode.</param>
        /// <returns>String format of top blocks markup.</returns>
        public static string GetTopBlocks(XmlTag topBlock, List<CustomWrapper> lstWrappers, int Mode)
        {

            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append(topBlock.LSTChildNodes.Count == 0 ? HtmlBuilder.GetTopBlockMarkupDefault() : BlockParser.ProcessPlaceholder(topBlock, lstWrappers, Mode));
                return sb.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// Obtain bottom blocks.
        /// </summary>
        /// <param name="BottomBlock">Object of XmlTag class.</param>
        /// <param name="lstWrappers">List of CustomWrapper class.</param>
        /// <param name="Mode">Mode</param>
        /// <returns>String format of bottom blocks markup.</returns>
        public static string GetBottomBlocks(XmlTag BottomBlock, List<CustomWrapper> lstWrappers, int Mode)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append(BottomBlock.LSTChildNodes.Count == 0 ? HtmlBuilder.GetBottomBlockMarkupDefault() : BlockParser.ProcessPlaceholder(BottomBlock, lstWrappers, Mode));
                return sb.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
