using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AspxCommerce.Core
{
   public class ExportData
    {

       public void ExportToExcel(ref string table, string fileName)
       {
         //  table = table.Replace("&gt;", ">");
         //  table = table.Replace("&lt;", "<");
           HttpContext.Current.Response.ClearContent();
           HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".xls");
           HttpContext.Current.Response.ContentType = "application/xls";
           HttpContext.Current.Response.Write(table);
           HttpContext.Current.Response.End();
       }

        public void ExportToCsv(ref string table, string fileName)
        {
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition",
                                                   "attachment;filename=" + fileName + "_" +
                                                   DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".csv");
            HttpContext.Current.Response.ContentType = "application/csv";
            HttpContext.Current.Response.Write(table);
            HttpContext.Current.Response.End();
        }
    }
}
