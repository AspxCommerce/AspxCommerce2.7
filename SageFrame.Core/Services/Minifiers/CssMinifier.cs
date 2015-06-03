#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

#endregion

namespace CssJscriptOptimizer.Minifiers
{
    /// <summary>
    /// Class that helps to optimizes the css.
    /// </summary>
    public static class CssMinifier
    {
        /// <summary>
        /// Rewrites the css image path from normal folder to optimation folder.
        /// </summary>
        /// <param name="content">Content  from where path to be change</param>
        /// <param name="modulePath">Module Path.</param>
        /// <param name="appRoot">Applicatio root path.</param>
        /// <param name="imageFolder">Image's folder name. </param>
        /// <returns>Changed image url path.</returns>
        public static string RewriteCssImagePath(string content, string modulePath, string appRoot, string imageFolder)
        {
            modulePath = modulePath.Trim();
            if (modulePath.EndsWith("/"))
            {
                modulePath = modulePath.Substring(0, modulePath.Length - 1);
            }
            string imagePath = appRoot + modulePath;
            //Match for image path without / in front
            string pattern = "url\\s*\\(\\s*[\"']?(?<imgfile>\\s*[^\"')]*)[\"')]?";
            MatchCollection collection = Regex.Matches(content, pattern);
            foreach (Match match in collection)
            {
                string path = match.Groups[1].Value.Trim();
                if (!path.Substring(0, 1).Equals("/") && !path.Substring(0, 1).Equals("."))
                {
                    string urlPath = match.Groups[1].Value;
                    if (!Regex.IsMatch(urlPath, @"\\s*http.*", RegexOptions.IgnoreCase))
                    {
                        string elementPattern = "url\\s*\\(\\s*[\"']?" + match.Groups[1].Value + "[\"']?";
                        content = Regex.Replace(content, elementPattern, "url('" + imagePath + "/" + match.Groups[1].Value + "'");
                    }
                }
            }
            return Parse(content, imagePath);
        }

        /// <summary>
        /// Changes the string's relative path 
        /// </summary>
        /// <param name="content">Content to be change.</param>
        /// <param name="relativePath">Relative path.</param>
        /// <returns>Changed string.</returns>
        static string Parse(string content, string relativePath)
        {
            int slashCount = GetSlashCount(relativePath);

            for (int i = slashCount; i > 1; i--)
            {
                string pathPortion = relativePath;
                string oldpattern = "";
                string newPath = "";
                for (int j = i; j > 1; j--)
                {
                    oldpattern += "\\.\\./";

                    int end = pathPortion.LastIndexOf("/");
                    newPath = relativePath.Substring(0, end);
                    pathPortion = pathPortion.Substring(0, pathPortion.LastIndexOf("/"));
                }

                string pattern = "url\\s*\\(\\s*[\"']?" + oldpattern + "(?<imgfile>\\s*[^\"')]*)[\"')]?";

                content = Regex.Replace(content, oldpattern, newPath + "/");

            }

            return content;

        }

        /// <summary>
        /// Returns the count of slash '/' in an string.
        /// </summary>
        /// <param name="folderPath">Folder path.</param>
        /// <returns>Counts of slashes in the string.</returns>
        static int GetSlashCount(string folderPath)
        {
            int count = 0;
            string pattern = "/";
            Match match = Regex.Match(folderPath, pattern);
            while (match.Success)
            {
                count++;
                match = match.NextMatch();
            }
            return count;
        }

        /// <summary>
        /// Appends the given  input and replacement.
        /// </summary>
        /// <param name="match">Regex.</param>
        /// <param name="sb">StringBuilder object.</param>
        /// <param name="input">Input string.</param>
        /// <param name="replacement">String to be replaced.</param>
        /// <param name="index">Substring to be taken after this index</param>
        /// <returns>sum of index and length of string.</returns>
        public static int AppendReplacement(this Match match, StringBuilder sb, string input, string replacement, int index)
        {
            var preceding = input.Substring(index, match.Index - index);
            sb.Append(preceding);
            sb.Append(replacement);
            return match.Index + match.Length;
        }

        /// <summary>
        /// Appends string to the tails.
        /// </summary>
        /// <param name="match">Regex.</param>
        /// <param name="sb">StringBuilder object.</param>
        /// <param name="input">Tail of which to be added.</param>
        /// <param name="index">Upto which the sting to be appended.</param>
        public static void AppendTail(this Match match, StringBuilder sb, string input, int index)
        {
            sb.Append(input.Substring(index));
        }

        /// <summary>
        /// Converts to Unsigned integer.
        /// </summary>
        /// <param name="instance">Instance name.</param>
        /// <returns>Unsigned interger.</returns>
        public static uint ToUInt32(this ValueType instance)
        {
            return Convert.ToUInt32(instance);
        }

        /// <summary>
        /// Replaces the string for the pattern provided with the input string.
        /// </summary>
        /// <param name="input">The string to search for a match</param>
        /// <param name="pattern">The regular expression patter to match.</param>
        /// <param name="replacement">The replacement string.</param>
        /// <returns>Returns the replaced string.</returns>
        public static string RegexReplace(this string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement);
        }

        /// <summary>
        /// Replaces the string for the pattern provided with the input string.
        /// </summary>
        /// <param name="input">The string to search for a match</param>
        /// <param name="pattern">The regular expression patter to match.</param>
        /// <param name="replacement">The replacement string.</param>
        /// <param name="options">RegexOptions obj.</param>
        /// <returns></returns>
        public static string RegexReplace(this string input, string pattern, string replacement, RegexOptions options)
        {
            return Regex.Replace(input, pattern, replacement, options);
        }

        /// <summary>
        /// Combines the strig
        /// </summary>
        /// <param name="format">String to format.</param>
        /// <param name="args">Array of object containing zero or more object to format.</param>
        /// <returns>Concatinated strings.</returns>
        public static string Fill(this string format, params object[] args)
        {
            return String.Format(format, args);
        }

        /// <summary>
        /// Deletes the specified number of characters from this instance beginning from the specified position.
        /// </summary>
        /// <param name="input">The string to be </param>
        /// <param name="startIndex">The zero-based position to begin deleting character.</param>
        /// <param name="endIndex">The ending position to delete character.</param>
        /// <returns>Returns deleted string. </returns>
        public static string RemoveRange(this string input, int startIndex, int endIndex)
        {
            return input.Remove(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// Determines where the given two strings are equal or not.
        /// </summary>
        /// <param name="left">String to be compared.</param>
        /// <param name="right">String to be compared.</param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string left, string right)
        {
            return String.Compare(left, right, true) == 0;
        }

        /// <summary>
        /// Converts int to hexadecimal value.
        /// </summary>
        /// <param name="value">Integer value.</param>
        /// <returns></returns>
        public static string ToHexString(this int value)
        {
            var sb = new StringBuilder();
            var input = value.ToString();
            foreach (char digit in input)
            {
                sb.Append("{0:x2}".Fill(digit.ToUInt32()));
            }
            return sb.ToString();
        }

        #region YUI Compressor's CssMin originally written by Isaac Schlueter

        /// <summary>
        /// Minifies CSS.
        /// </summary>
        /// <param name="css">The CSS content to minify.</param>
        /// <returns>Minified CSS content.</returns>
        public static string CssMinify(this string css)
        {
            return CssMinify(css, 0);
        }

        /// <summary>
        /// Minifies CSS with a column width maximum.
        /// </summary>
        /// <param name="css">The CSS content to minify.</param>
        /// <param name="columnWidth">The maximum column width.</param>
        /// <returns>Minified CSS content.</returns>
        public static string CssMinify(this string css, int columnWidth)
        {
            css = css.RemoveCommentBlocks();
            css = css.RegexReplace("\\s+", " ");
            css = css.RegexReplace("\"\\\\\"}\\\\\"\"", "___PSEUDOCLASSBMH___");
            css = css.RemovePrecedingSpaces();
            css = css.RegexReplace("([!{}:;>+\\(\\[,])\\s+", "$1");
            css = css.RegexReplace("([^;\\}])}", "$1;}");
            css = css.RegexReplace("([\\s:])(0)(px|em|%|in|cm|mm|pc|pt|ex)", "$1$2");
            css = css.RegexReplace(":0 0 0 0;", ":0;");
            css = css.RegexReplace(":0 0 0;", ":0;");
            css = css.RegexReplace(":0 0;", ":0;");
            css = css.RegexReplace("background-position:0;", "background-position:0 0;");
            css = css.RegexReplace("(:|\\s)0+\\.(\\d+)", "$1.$2");
            css = css.ShortenRgbColors();
            css = css.ShortenHexColors();
            css = css.RegexReplace("[^\\}]+\\{;\\}", "");
            css = css.RegexReplace("@mediaonlyscreenand", "@media only screen and");
            css = css.RegexReplace("screenand", "screen and ");
            css = css.RegexReplace("@mediaprint", "@media print");
            css = css.RegexReplace("and\\(", "and (");
            if (columnWidth > 0)
            {
                css = css.BreakLines(columnWidth);
            }
            css = css.RegexReplace("___PSEUDOCLASSBMH___", "\"\\\\\"}\\\\\"\"");
            css = css.Trim();
            return css;
        }

        /// <summary>
        /// Removes comment blocks from any string.
        /// </summary>
        /// <param name="input">String from where the comment is to be deleted.</param>
        /// <returns>Comment refined string.</returns>
        private static string RemoveCommentBlocks(this string input)
        {
            var startIndex = 0;
            var endIndex = 0;
            var iemac = false;

            startIndex = input.IndexOf(@"/*", startIndex);
            while (startIndex >= 0)
            {
                endIndex = input.IndexOf(@"*/", startIndex + 2);
                if (endIndex >= startIndex + 2)
                {
                    if (input[endIndex - 1] == '\\')
                    {
                        startIndex = endIndex + 2;
                        iemac = true;
                    }
                    else if (iemac)
                    {
                        startIndex = endIndex + 2;
                        iemac = false;
                    }
                    else
                    {
                        input = input.RemoveRange(startIndex, endIndex + 2);
                    }
                }
                startIndex = input.IndexOf(@"/*", startIndex);
            }

            return input;
        }

        /// <summary>
        /// Shortens the RBG color in any css.
        /// </summary>
        /// <param name="css">String containing the css strings.</param>
        /// <returns>RBG shortened string.</returns>
        private static string ShortenRgbColors(this string css)
        {
            var sb = new StringBuilder();
            Regex p = new Regex("rgb\\s*\\(\\s*([0-9,\\s]+)\\s*\\)");
            Match m = p.Match(css);

            int index = 0;
            while (m.Success)
            {
                string[] colors = m.Groups[1].Value.Split(',');
                StringBuilder hexcolor = new StringBuilder("#");

                foreach (string color in colors)
                {
                    int val = Int32.Parse(color);
                    if (val < 16)
                    {
                        hexcolor.Append("0");
                    }
                    hexcolor.Append(val.ToHexString());
                }

                index = m.AppendReplacement(sb, css, hexcolor.ToString(), index);
                m = m.NextMatch();
            }

            m.AppendTail(sb, css, index);
            return sb.ToString();
        }

        /// <summary>
        /// Shortens the HexColors from css string.
        /// </summary>
        /// <param name="css">String containing the css strings.</param>
        /// <returns>HexColor shortened string of css.</returns>
        private static string ShortenHexColors(this string css)
        {
            var sb = new StringBuilder();
            Regex p = new Regex("([^\"'=\\s])(\\s*)#([0-9a-fA-F])([0-9a-fA-F])([0-9a-fA-F])([0-9a-fA-F])([0-9a-fA-F])([0-9a-fA-F])");
            Match m = p.Match(css);

            int index = 0;
            while (m.Success)
            {
                if (m.Groups[3].Value.EqualsIgnoreCase(m.Groups[4].Value) &&
                    m.Groups[5].Value.EqualsIgnoreCase(m.Groups[6].Value) &&
                    m.Groups[7].Value.EqualsIgnoreCase(m.Groups[8].Value))
                {
                    var replacement = String.Concat(m.Groups[1].Value, m.Groups[2].Value, "#", m.Groups[3].Value, m.Groups[5].Value, m.Groups[7].Value);
                    index = m.AppendReplacement(sb, css, replacement, index);
                }
                else
                {
                    index = m.AppendReplacement(sb, css, m.Value, index);
                }

                m = m.NextMatch();
            }

            m.AppendTail(sb, css, index);
            return sb.ToString();
        }

        /// <summary>
        /// Removes the spaces if found are two or more between the word, lines or paragraph.
        /// </summary>
        /// <param name="css">String containing the css strings.</param>
        /// <returns>A paragraph from the input stings.</returns>
        private static string RemovePrecedingSpaces(this string css)
        {
            var sb = new StringBuilder();
            Regex p = new Regex("(^|\\})(([^\\{:])+:)+([^\\{]*\\{)");
            Match m = p.Match(css);

            int index = 0;
            while (m.Success)
            {
                var s = m.Value;
                s = s.RegexReplace(":", "___PSEUDOCLASSCOLON___");
                index = m.AppendReplacement(sb, css, s, index);
                m = m.NextMatch();
            }
            m.AppendTail(sb, css, index);
            var result = sb.ToString();
            result = result.RegexReplace("\\s+([!{};:>+\\(\\)\\],])", "$1");
            result = result.RegexReplace("___PSEUDOCLASSCOLON___", ":");
            return result;
        }

        /// <summary>
        /// Inserts breaklines after certain length of closing bracket( '}' ).
        /// </summary>
        /// <param name="css">String containing the css strings.</param>
        /// <param name="columnWidth">Break to be inserted after certain index.</param>
        /// <returns>Break line inserted sting.</returns>
        private static string BreakLines(this string css, int columnWidth)
        {
            int i = 0;
            int start = 0;

            var sb = new StringBuilder(css);
            while (i < sb.Length)
            {
                var c = sb[i++];
                if (c == '}' && i - start > columnWidth)
                {
                    sb.Insert(i, '\n');
                    start = i;
                }
            }
            return sb.ToString();
        }
        #endregion

    }
}
