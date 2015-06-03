#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Web;
#endregion


namespace SageFrame.Web
{
    /// <summary>
    /// Description for DateTimeHelper
    /// </summary>
    /// 
    public static class DateTimeHelper
    {

        /*
         * ===================================================================================================
                Specifier 	    DateTimeFormatInfo property 	        Pattern value (for en-US culture)
                t 	            ShortTimePattern 	                    h:mm tt
                d 	            ShortDatePattern 	                    M/d/yyyy
                T 	            LongTimePattern 	                    h:mm:ss tt
                D 	            LongDatePattern 	                    dddd, MMMM dd, yyyy
                f 	            (combination of D and t) 	            dddd, MMMM dd, yyyy h:mm tt
                F 	            FullDateTimePattern 	                dddd, MMMM dd, yyyy h:mm:ss tt
                g 	            (combination of d and t) 	            M/d/yyyy h:mm tt
                G 	            (combination of d and T) 	            M/d/yyyy h:mm:ss tt
                m, M 	        MonthDayPattern 	                    MMMM dd
                y, Y 	        YearMonthPattern 	                    MMMM, yyyy
                r, R 	        RFC1123Pattern 	                        ddd, dd MMM yyyy HH':'mm':'ss 'GMT' (*)
                s 	            SortableDateTi­mePattern 	            yyyy'-'MM'-'dd'T'HH':'mm':'ss (*)
                u 	            UniversalSorta­bleDateTimePat­tern 	    yyyy'-'MM'-'dd HH':'mm':'ss'Z' (*)
                Where (*) = culture independent
             *=================================================================================================
         */
        public static string cultureIndependentPattern = "u";//Pattern value (for en-US culture) "yyyy'-'MM'-'dd HH':'mm':'ss'Z'"
        public static string UniversalSortableDateTimePattern = "u"; //Pattern value (for en-US culture) "yyyy'-'MM'-'dd HH':'mm':'ss'Z'"
        public static string SortableDateTimePattern = "s"; //Pattern value (for en-US culture) "yyyy'-'MM'-'dd'T'HH':'mm':'ss"
        public static string RFC1123Pattern = "R";//Pattern value (for en-US culture) "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'" // r, R
        /// <summary>
        /// Get culture independent date time.
        /// </summary>
        /// <param name="dateortime">DateTime</param>
        /// <param name="outputformat">Output DateTime format.</param>
        /// <returns>Formated date time.</returns>
        public static string GetCultureIndependentDateTime(string dateortime, string outputformat)
        {
            string ret = "";
            if (dateortime == "")
                return "";
            try
            {
                ret = System.Convert.ToDateTime(dateortime).ToString(outputformat);
            }
            catch
            {
            }
            return ret;
        }
        /// <summary>
        /// Convert short date.
        /// </summary>
        /// <param name="str">DateTime</param>
        /// <returns>Formated short date.</returns>
        public static string ConvertToShortDate(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                DateTime dt = DateTime.Parse(str);
                str = dt.ToShortDateString();
            }
            return str;
        }
        /// <summary>
        /// Convert Long Date
        /// </summary>
        /// <param name="str">DateTime</param>
        /// <returns>Formated short date.</returns>
        public static string ConvertToLongDate(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                DateTime dt = DateTime.Parse(str);
                str = dt.ToLongDateString();
            }
            return str;
        }
    }
}
