using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AspxCommerce.Core
{
    public class AspxUtility
    {
        public static string fixedEncodeURIComponent(string str)
        {
            ////     str = HttpUtility.UrlEncode(str); // this appends + sign for space
            //str = Uri.EscapeUriString(str);
            //str = Regex.Replace(str, "/!/g", "%21");
            //str = Regex.Replace(str, "/'/g", "%27");
            //str = Regex.Replace(str, @"/\(/g", "%28");
            //str = Regex.Replace(str, @"/\)/g", "%29");

            //str = Regex.Replace(str, "/-/g", "_");
            //str = Regex.Replace(str, @"/\*/g", "%2A");
            //str = Regex.Replace(str, "/&/g", "%26");
            //str = Regex.Replace(str, "/%26/g", "ampersand");
            //str = Regex.Replace(str, "/%20/g", "'-'");
            //return str;

            String Results = Uri.EscapeUriString(str);
           // Results = Results.Replace("%", "%25");
            Results = Results.Replace("!", "%21");
            Results = Results.Replace("'", "%27");
            Results = Results.Replace("(", "%28");
            Results = Results.Replace(")", "%29");
            Results = Results.Replace("*", "%2A");

            Results = Results.Replace("<", "%3C");
            Results = Results.Replace(">", "%3E");
            Results = Results.Replace("#", "%23");
            Results = Results.Replace("{", "%7B");
            Results = Results.Replace("}", "%7D");
            Results = Results.Replace("|", "%7C");
            Results = Results.Replace("\"", "%5C");
            Results = Results.Replace("^", "%5E");
            Results = Results.Replace("~", "%7E");
            Results = Results.Replace("[", "%5B");
            Results = Results.Replace("]", "%5D");
            Results = Results.Replace("`", "%60");
            Results = Results.Replace(";", "%3B");
            Results = Results.Replace("/", "%2F");
            Results = Results.Replace("?", "%3F");
            Results = Results.Replace(":", "%3A");
            Results = Results.Replace("@", "%40");
            Results = Results.Replace("=", "%3D");
            Results = Results.Replace("&", "%26");
            Results = Results.Replace("%26", "ampersand");
            Results = Results.Replace("$", "%24");
            return Results;

        }

        public static string fixedDecodeURIComponent(string str)
        {
            String Results = str;
            // Results = Results.Replace("%25","%");
            Results = Results.Replace("%21", "!");
            Results = Results.Replace("%27", "'");
            Results = Results.Replace("%28", "(");
            Results = Results.Replace("%29", ")");
            Results = Results.Replace("%2A", "*");


            Results = Results.Replace("%3C", "<");
            Results = Results.Replace("%3E", ">");
            Results = Results.Replace("%23", "#");
            Results = Results.Replace("%7B", "{");
            Results = Results.Replace("%7D", "}");
            Results = Results.Replace("%7C", "|");
            Results = Results.Replace("%5C", "\"");
            Results = Results.Replace("%5E", "^");
            Results = Results.Replace("%7E", "~");
            Results = Results.Replace("%5B", "[");
            Results = Results.Replace("%5D", "]");
            Results = Results.Replace("%60", "`");
            Results = Results.Replace("%3B", ";");
            Results = Results.Replace("%2F", "/");
            Results = Results.Replace("%3F", "?");
            Results = Results.Replace("%3A", ":");
            Results = Results.Replace("%40", "@");
            Results = Results.Replace("%3D", "=");
            Results = Results.Replace("ampersand", "%26");
            Results = Results.Replace("%26", "&");
            Results = Results.Replace("%24", "$");
            return Results;
        }
    }
}
