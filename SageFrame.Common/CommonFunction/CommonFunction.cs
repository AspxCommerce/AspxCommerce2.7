#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Collections;
using System.Resources;
using System.Web.UI.WebControls;
using System.Web;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Xml.Xsl;
using System.Xml;
using System.IO;
using System.Web.Security;
using SageFrame.Utilities;
#endregion


namespace SageFrame.Web
{
    /// <summary>
    /// Description for application CommonFunctions.
    /// </summary>
    /// 
    public class CommonFunction
    {
        public CommonFunction()
        {

        }
        /// <summary>
        ///  Return object.
        /// </summary>
        /// <param name="dtReturn">Datatable</param>
        /// <returns>object</returns>
        public object[] DataTableToObject(DataTable dtReturn)
        {
            object[] obj = new object[dtReturn.Rows.Count];
            for (int i = 0; i < dtReturn.Rows.Count; i++)
            {
                object[] objItem = new object[dtReturn.Columns.Count];
                for (int j = 0; j < dtReturn.Columns.Count; j++)
                {
                    objItem[j] = (object)dtReturn.Rows[i][j];
                }
                obj[i] = objItem;
            }
            return obj;
        }

        //Adedd By alok
        /// <summary>
        /// Return DataTable.
        /// </summary>
        /// <typeparam name="T">Type of the object implementing.</typeparam>
        /// <param name="varlist"></param>
        /// <returns>DataTable</returns>
        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();
            // column names 
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;
            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others 
                //will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null);
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        /// <summary>
        ///Check unwanted symbols or string.
        /// </summary>
        /// <param name="SearchString">Search string.</param>
        /// <returns>Return "False" if search string contains unwanted symbols.</returns>
        public bool CheckIgnorWords(string SearchString)
        {
            string IgnorWords = string.Empty;
            IgnorWords = "^ , ; : [] ] [ {} () } { ) ( _ = < > . + - \\ / \" \"\" ' ! % * @~ @# @& &? & # ? about 1 after 2 all also 3 an 4 and 5 another 6 any 7 are 8 as 9 at 0 be $ because been before being between both but by came can come could did do each for from get got has had he have her here him himself his how if in into is it like make many me might more most much must my never now of on only or other our out over said same see should since some still such take than that the their them then there these they this those through to too under up very was way we well were what where which while who with would you your a b c d e f g h i j k l m n o p q r s t u v w x y z";
            SearchString = RemoveSpcalSymbol(SearchString);
            if (IgnorWords.Contains(SearchString) || SearchString.Trim() == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        ///Check unwanted symbols or string.
        /// </summary>
        /// <param name="SearchString">Search string.</param>
        /// <returns>Replaced string</returns>
        public string RemoveUnnessaryKeywords(string SearchString)
        {
            string IgnorWords = string.Empty;
            IgnorWords = "^ , ; : [] ] [ {} () } { ) ( _ = < > . + - \\ / \" \"\" ' ! % * @~ @# @& &? & # ? about 1 after 2 all also 3 an 4 and 5 another 6 any 7 are 8 as 9 at 0 be $ because been before being between both but by came can come could did do each for from get got has had he have her here him himself his how if in into is it like make many me might more most much must my never now of on only or other our out over said same see should since some still such take than that the their them then there these they this those through to too under up very was way we well were what where which while who with would you your a b c d e f g h i j k l m n o p q r s t u v w x y z";
            SearchString = RemoveSpcalSymbol(SearchString);
            return SearchString;
        }
        /// <summary>
        ///Check unwanted symbols.
        /// </summary>
        /// <param name="SearchString">Search string.</param>
        /// <returns>Replaced string</returns>
        public string RemoveSpcalSymbol(string SearchString)
        {
            SearchString = SearchString.Replace("\"", "");
            SearchString = SearchString.Replace("@", "");
            SearchString = SearchString.Replace("?", "");
            SearchString = SearchString.Replace(":", "");
            SearchString = SearchString.Replace(";", "");
            SearchString = SearchString.Replace("_", "");
            SearchString = SearchString.Replace("=", "");
            SearchString = SearchString.Replace("<", "");
            SearchString = SearchString.Replace(">", "");
            SearchString = SearchString.Replace("[", "");
            SearchString = SearchString.Replace("]", "");
            SearchString = SearchString.Replace("{", "");
            SearchString = SearchString.Replace("}", "");
            SearchString = SearchString.Replace("!", "");
            SearchString = SearchString.Replace("#", "");
            SearchString = SearchString.Replace(",", "");
            SearchString = SearchString.Replace("-", "");
            SearchString = SearchString.Replace(".", "");
            SearchString = SearchString.Replace("^", "");
            SearchString = SearchString.Replace("(", "");
            SearchString = SearchString.Replace(")", "");
            SearchString = SearchString.Replace("/", "");
            SearchString = SearchString.Replace("~", "");
            SearchString = SearchString.Replace("|", "");
            SearchString = SearchString.Replace("$", "");
            SearchString = SearchString.Replace("%", "");
            SearchString = SearchString.Replace("&", "");
            SearchString = SearchString.Replace("*", "");
            SearchString = SearchString.Replace("and", "");
            return SearchString;
        }
        /// <summary>
        /// Export datatable to xml file.
        /// </summary>
        /// <param name="formattedDataTable">Datatable</param>
        /// <param name="filename">File name</param>
        public void exportDataTableToXML(DataTable formattedDataTable, string filename)
        {
            formattedDataTable.TableName = "LoginLog";
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            string result;
            using (StringWriter sw = new StringWriter())
            {
                formattedDataTable.WriteXml(sw);
                result = sw.ToString();
            }
            context.Response.Write(result);
            context.Response.ContentType = "application/octet-stream";
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ".xml");
            context.Response.End();
        }
        /// <summary>
        ///  Export datatable to csv file.
        /// </summary>
        /// <param name="formattedDataTable">DataTable</param>
        /// <param name="filename">File name</param>
        public void exportDataTableToCsv(DataTable formattedDataTable, string filename)
        {
            DataTable toExcel = formattedDataTable.Copy();
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            foreach (DataColumn column in toExcel.Columns)
            {
                context.Response.Write(column.ColumnName + ",");
            }
            context.Response.Write(Environment.NewLine);
            foreach (DataRow row in toExcel.Rows)
            {
                for (int i = 0; i < toExcel.Columns.Count; i++)
                {
                    context.Response.Write(row[i].ToString().Replace(",", string.Empty) + ",");
                }
                context.Response.Write(Environment.NewLine);
            }
            context.Response.ContentType = "text/csv";
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ".csv");
            context.Response.End();
        }
        /// <summary>
        ///  Export datatable to XLS file.
        /// </summary>
        /// <param name="formattedDataTable">DataTable</param>
        /// <param name="filename">File name</param>
        public void exportDataTableToXLS(DataTable formattedDataTable, string filename)
        {
            DataTable toExcel = formattedDataTable.Copy();
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            foreach (DataColumn column in toExcel.Columns)
            {
                context.Response.Write(column.ColumnName + "\t");//+ ","
            }
            context.Response.Write(Environment.NewLine);
            foreach (DataRow row in toExcel.Rows)
            {
                for (int i = 0; i < toExcel.Columns.Count; i++)
                {
                    //context.Response.Write(row[i].ToString().Replace(",", string.Empty) + ",");
                    context.Response.Write(row[i].ToString().Replace(",", string.Empty) + "\t");//
                }
                context.Response.Write(Environment.NewLine);
            }
            context.Response.ContentType = "application/xls";
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ".xls");
            context.Response.End();
        }
        /// <summary>
        ///  Export datatable to XLSX file.
        /// </summary>
        /// <param name="formattedDataTable">DataTable</param>
        /// <param name="filename">File name</param>
        public void exportDataTableToXLSX(DataTable formattedDataTable, string filename)
        {
            DataTable toExcel = formattedDataTable.Copy();
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            foreach (DataColumn column in toExcel.Columns)
            {
                context.Response.Write(column.ColumnName + "\t");//+ ","
            }
            context.Response.Write(Environment.NewLine);
            foreach (DataRow row in toExcel.Rows)
            {
                for (int i = 0; i < toExcel.Columns.Count; i++)
                {
                    context.Response.Write(row[i].ToString().Replace(",", string.Empty) + "\t");//
                }

                context.Response.Write(Environment.NewLine);
            }
            context.Response.ContentType = "application/vnd.ms-excel";//application/xls //application/vnd.ms-excel
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ".xlsx");
            context.Response.End();
        }
       
        //public string FormatDays(double mDays)
        //{
        //    string Ret = string.Empty;
        //    int Year = 0;
        //    int Month = 0;
        //    int Weeak = 0;
        //    int Days = 0;
        //    int Reminder = 0;
        //    if (mDays > 360 || mDays == 360)
        //    {
        //        Reminder = (int)mDays % 360;
        //        Year = (int)mDays / 360;
        //        if (Reminder > 30 || Reminder == 30)
        //        {

        //            Month = Reminder / 30;
        //            Reminder = Reminder % 30;
        //            if (Reminder > 7)
        //            {
        //                Weeak = Reminder / 7;
        //                Reminder = Reminder % 7;
        //                if (Reminder > 0)
        //                {
        //                    Days = Reminder;
        //                }
        //            }
        //            else
        //            {
        //                Days = Reminder;
        //            }
        //        }
        //        else
        //        {
        //            if (Reminder > 7)
        //            {
        //                Weeak = Reminder / 7;
        //                Reminder = Reminder % 7;
        //                if (Reminder > 0)
        //                {
        //                    Days = Reminder;
        //                }
        //            }
        //            else
        //            {
        //                Days = Reminder;
        //            }
        //        }
        //    }
        //    else if (mDays > 30 || mDays == 30)
        //    {
        //        Reminder = (int)mDays % 30;
        //        Month = (int)mDays / 30;
        //        if (Reminder > 7 || Reminder == 7)
        //        {
        //            Weeak = Reminder / 7;
        //            Reminder = Reminder % 7;
        //            if (Reminder > 0)
        //            {
        //                Days = Reminder;
        //            }
        //        }
        //        else
        //        {
        //            Days = Reminder;
        //        }
        //    }
        //    else if (mDays > 7 || mDays == 7)
        //    {
        //        Reminder = (int)mDays % 7;
        //        Weeak = (int)mDays / 7;
        //        if (Reminder > 0)
        //        {
        //            Days = Reminder;
        //        }
        //    }
        //    else
        //    {
        //        Days = (int)mDays;
        //    }

        //    if (Year > 0)
        //    {
        //        if (Year > 1)
        //            Ret += Year + " Years  ";
        //        else
        //            Ret += Year + " Year  ";

        //    }
        //    if (Month > 0)
        //    {
        //        if (Month > 1)
        //            Ret += Month + " Months  ";
        //        else
        //            Ret += Month + " Month  ";
        //    }
        //    if (Weeak > 0)
        //    {
        //        if (Weeak > 1)
        //            Ret += Weeak + " Weeaks  ";
        //        else
        //            Ret += Weeak + " Weeak  ";
        //    }
        //    if (Days > 0)
        //    {
        //        if (Days > 1)
        //            Ret += Days + " Days  ";
        //        else
        //            Ret += Days + " Day  ";
        //    }
        //    return Ret;
        //}
        /// <summary>
        /// Case for rate value.
        /// </summary>
        /// <param name="RateValue">Rate value</param>
        /// <returns>string</returns>
        public string FormatRating(int RateValue)
        {
            string ret = string.Empty;
            switch (RateValue)
            {
                case 1:
                    ret = "Basic level";
                    break;
                case 2:
                    ret = "Normal";
                    break;
                case 3:
                    ret = "Good";
                    break;
                case 4:
                    ret = "Advance";
                    break;
                case 5:
                    ret = "Fluient";
                    break;
            }
            return ret;
        }
        /// <summary>
        /// Format Bit value
        /// </summary>
        /// <param name="bitValue">Bit value.</param>
        /// <returns>Yes If bitValue is "1" else "No"</returns></returns>
        public string FormatbitValue(string bitValue)
        {
            string ret = "No";
            if (bitValue == "1")
            {
                ret = "Yes";
            }
            return ret;
        }
        /// <summary>
        /// Remove unwanted html tag.
        /// </summary>
        /// <param name="str">String with HTML tag</param>
        /// <returns>String without HTML tag</returns>
        public string RemoveUnwantedHTMLTAG(string str)
        {
            return Regex.Replace(str, @"<(.|\n)*?>", string.Empty);
        }
        //End of alok code
        /// <summary>
        /// Write .resx file for multi language.
        /// </summary>
        /// <param name="fileName">.resx filename</param>
        /// <param name="lstResDef">List of resource defination.</param>
        public static void WriteResX(string fileName, List<KeyValue> lstResDef)
        {
            try
            {
                using (ResXResourceWriter writer = new ResXResourceWriter(fileName))
                {
                    foreach (KeyValue obj in lstResDef)
                        writer.AddResource(obj.Key, obj.Value);
                    writer.Generate();
                }
            }
            catch { throw new Exception("Error while saving " + fileName); }
        }

        /// <summary>
        /// Repalce backslash with frontslash from file path.
        /// </summary>
        /// <param name="filepath">Filepath</param>
        /// <returns>string file path.</returns>
        public static string ReplaceBackSlash(string filepath)
        {
            if (filepath != null)
            {
                filepath = filepath.Replace("\\", "/");
            }
            return filepath;
        }
        /// <summary>
        /// Return The Name of the Logged in User by PortalID
        /// </summary>
        /// <param name="PortalID">PortalID</param>
        /// <returns>Logged In UserName</returns>

        public string GetUser(int portalID)
        {

            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsCookieName(portalID)];
            string user = string.Empty;
            if (authCookie != null && authCookie.Value != string.Empty)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                if (ticket != null)
                {
                    user = ticket.Name;
                }
                else
                {
                    user = "anonymoususer";
                }
            }
            else
            {
                user = "anonymoususer";
            }
            return user;
        }
        /// <summary>
        /// AuthenticationTicket for application security.
        /// </summary>
        /// <param name="portalID">PortalID</param>
        /// <returns>FormsAuthenticationTicket</returns>
        public FormsAuthenticationTicket GetUserTicket(int portalID)
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsCookieName(portalID)];
            if (authCookie != null && authCookie.Value != string.Empty)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                return ticket;
            }
            else
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "anonymoususer", DateTime.Now,
                                                                            DateTime.Now.AddMinutes(30),
                                                                              true,
                                                                              portalID.ToString(),
                                                                              FormsAuthentication.FormsCookiePath);
                return ticket;
            }
        }

        public static string GetApplicationName
        {
            get
            {
                return (HttpContext.Current.Request.ApplicationPath == "/" ? "" : HttpContext.Current.Request.ApplicationPath);
            }
        }

        /// <summary>
        /// Replace last occurance of string in a main string
        /// </summary>
        /// <param name="Source">Main String</param>
        /// <param name="Find">What To Search</param>
        /// <param name="Replace">What To Replace</param>
        /// <returns>Replaced String </returns>
        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.LastIndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

        /// <summary>
        /// To Check Whether SageFrame Is Installed Or Not
        /// </summary>
        /// <returns>True If It Is Install</returns>
        public bool IsInstalled()
        {
            bool isInstalled = false;
            string IsInstalled = Config.GetSetting("IsInstalled").ToString();
            string InstallationDate = Config.GetSetting("InstallationDate").ToString();
            if ((IsInstalled != "" && IsInstalled != "false") && InstallationDate != "")
            {
                isInstalled = true;
            }
            return isInstalled;
        }

        public string FormsCookieName(int portalID)
        {
            string formName = string.Empty;
            formName = FormsAuthentication.FormsCookieName + HttpContext.Current.Session + portalID.ToString();
            return formName;
        }
    }
}