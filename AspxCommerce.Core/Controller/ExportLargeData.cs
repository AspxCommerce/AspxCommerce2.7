using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SageFrame.Common;
using SageFrame.Web.Utilities;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Web;
using SageFrame.Core;
namespace AspxCommerce.Core
{
    public class ExportLargeData
    {
        public ExportLargeData()
        {
        }

        static private int rowsPerSheet = 50000;
        public void ExportTOExcel(string filePath, string spName, List<KeyValuePair<string, object>> parameter, DataTable resultsData)
        {
            SQLHandler sqlh = new SQLHandler();
            SqlDataReader reader = sqlh.ExecuteAsDataReader(spName, parameter); //command.ExecuteReader();

            int c = 0;
            bool firstTime = true;

            //Get the Columns names, types, this will help
            //when we need to format the cells in the excel sheet.
            DataTable dtSchema = reader.GetSchemaTable();
            var listCols = new List<DataColumn>();
            if (dtSchema != null)
            {
                foreach (DataRow drow in dtSchema.Rows)
                {
                    string columnName = Convert.ToString(drow["ColumnName"]).ToString();
                    var column = new DataColumn(columnName, (Type)(drow["DataType"]));
                    column.Unique = (bool)drow["IsUnique"];
                    column.AllowDBNull = (bool)drow["AllowDBNull"];
                    column.AutoIncrement = (bool)drow["IsAutoIncrement"];
                    listCols.Add(column);
                    resultsData.Columns.Add(column);
                }
            }
            while (reader.Read())
            {
                DataRow dataRow = resultsData.NewRow();
                for (int i = 0; i < listCols.Count; i++)
                {
                    dataRow[(listCols[i])] = reader[i];
                }
                resultsData.Rows.Add(dataRow);
                c++;
                if (c == rowsPerSheet)
                {
                    c = 0;
                    ExportToOxml(firstTime, filePath, resultsData);
                    resultsData.Clear();
                    firstTime = false;
                }
            }
            if (resultsData.Rows.Count == 0)
            {
                DataRow dataRow = resultsData.NewRow();
                for (int i = 0; i < listCols.Count; i++)
                {
                    string paramType = listCols[i].DataType.FullName.ToLower();
                    if (listCols[i].AllowDBNull == false)
                    {
                        listCols[i].AllowDBNull = true;
                    }
                    if (paramType == "system.string")
                    {
                        dataRow[(listCols[i])] = "";
                    }
                    else
                    {
                        dataRow[(listCols[i])] = DBNull.Value;
                    }
                }
                resultsData.Rows.Add(dataRow);
            }
            if (resultsData.Rows.Count > 0)
            {
                ExportToOxml(firstTime, filePath, resultsData);
                resultsData.Clear();
            }
            reader.Close();
            DownloadFile(filePath,false);
        }

        public static void ExportToOxml(bool firstTime, string filePath, DataTable resultsData)
        {
            //Delete the file if it exists. 
            if (firstTime && File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            uint sheetId = 1; //Start at the first sheet in the Excel workbook.

            if (firstTime)
            {
                //This is the first time of creating the excel file and the first sheet.
                // Create a spreadsheet document by supplying the filepath.
                // By default, AutoSave = true, Editable = true, and Type = xlsx.
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.
                    Create(filePath, SpreadsheetDocumentType.Workbook);

                // Add a WorkbookPart to the document.
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                // Add a WorksheetPart to the WorkbookPart.
                var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);


                var bold1 = new Bold();
                CellFormat cf = new CellFormat();

                // Add Sheets to the Workbook.
                Sheets sheets;
                sheets = spreadsheetDocument.WorkbookPart.Workbook.
                    AppendChild<Sheets>(new Sheets());
                // Append a new worksheet and associate it with the workbook.
                var sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.
                        GetIdOfPart(worksheetPart),
                    SheetId = sheetId,
                    Name = "Sheet" + sheetId
                };
                sheets.Append(sheet);
                //Add Header Row.
                var headerRow = new Row();
                foreach (DataColumn column in resultsData.Columns)
                {
                    var cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(column.ColumnName)
                    };
                    headerRow.AppendChild(cell);
                }
                sheetData.AppendChild(headerRow);

                foreach (DataRow row in resultsData.Rows)
                {
                    var newRow = new Row();
                    foreach (DataColumn col in resultsData.Columns)
                    {
                        var cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(row[col].ToString())
                        };
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }
                workbookpart.Workbook.Save();

                spreadsheetDocument.Close();
            }
            else
            {
                // Open the Excel file that we created before, and start to add sheets to it.
                var spreadsheetDocument = SpreadsheetDocument.Open(filePath, true);

                var workbookpart = spreadsheetDocument.WorkbookPart;
                if (workbookpart.Workbook == null)
                    workbookpart.Workbook = new Workbook();

                var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                var sheets = spreadsheetDocument.WorkbookPart.Workbook.Sheets;

                if (sheets.Elements<Sheet>().Any())
                {
                    //Set the new sheet id
                    sheetId = sheets.Elements<Sheet>().Max(s => s.SheetId.Value) + 1;
                }
                else
                {
                    sheetId = 1;
                }

                // Append a new worksheet and associate it with the workbook.
                var sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.
                        GetIdOfPart(worksheetPart),
                    SheetId = sheetId,
                    Name = "Sheet" + sheetId
                };
                sheets.Append(sheet);

                //Add the header row here.
                var headerRow = new Row();

                foreach (DataColumn column in resultsData.Columns)
                {
                    var cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(column.ColumnName)
                    };
                    headerRow.AppendChild(cell);
                }
                sheetData.AppendChild(headerRow);

                foreach (DataRow row in resultsData.Rows)
                {
                    var newRow = new Row();

                    foreach (DataColumn col in resultsData.Columns)
                    {
                        var cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(row[col].ToString())
                        };
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }

                workbookpart.Workbook.Save();

                // Close the document.
                spreadsheetDocument.Close();

            }

        }

        public void ExportToCSV(bool includeHeaderAsFirstRow, string separator, string spName, List<KeyValuePair<string, object>> parameter, string filePath)
        {
            SQLHandler sqlh = new SQLHandler();
            SqlDataReader dataReader = sqlh.ExecuteAsDataReader(spName, parameter);
            List<string> csvRows = new List<string>();
            StringBuilder sb = null;
            StreamWriter sw = new StreamWriter(filePath, false);
            if (includeHeaderAsFirstRow)
            {
                sb = new StringBuilder();
                for (int index = 0; index < dataReader.FieldCount; index++)
                {
                    if (dataReader.GetName(index) != null)
                        sb.Append(dataReader.GetName(index));

                    if (index < dataReader.FieldCount - 1)
                        sb.Append(separator);
                }
                sw.WriteLine(sb.ToString());
            }

            while (dataReader.Read())
            {
                sb = new StringBuilder();
                for (int index = 0; index < dataReader.FieldCount - 1; index++)
                {
                    if (!dataReader.IsDBNull(index))
                    {
                        string value = dataReader.GetValue(index).ToString();
                        if (dataReader.GetFieldType(index) == typeof(String))
                        {
                            //If double quotes are used in value, ensure each are replaced but 2.
                            if (value.IndexOf("\"") >= 0)
                                value = value.Replace("\"", "\"\"");

                            //If separtor are is in value, ensure it is put in double quotes.
                            if (value.IndexOf(separator) >= 0)
                                value = "\"" + value + "\"";
                        }
                        sb.Append(value);
                    }

                    if (index < dataReader.FieldCount - 1)
                        sb.Append(separator);
                }

                if (!dataReader.IsDBNull(dataReader.FieldCount - 1))
                    sb.Append(dataReader.GetValue(dataReader.FieldCount - 1).ToString().Replace(separator, " "));
                sw.WriteLine(sb.ToString());
            }
            sw.Close();
            dataReader.Close();
            sb = null;
            DownloadFile(filePath,true);
        }

        public static void DownloadFile(string filePath, bool isCsv)
        {
            FileInfo file = new FileInfo(GetAbsolutePath(filePath));
            try
            {
                if (file.Exists)
                {
                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    if (isCsv)
                    {
                        HttpContext.Current.Response.AddHeader("Content-Type", "text/csv");
                        HttpContext.Current.Response.ContentType = "text/csv";
                    }
                    else
                    {
                        HttpContext.Current.Response.AddHeader("Content-Type", "application/Excel");
                        HttpContext.Current.Response.ContentType = "application/vnd.xls";
                    }

                    HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
                    HttpContext.Current.Response.WriteFile(file.FullName);
                    HttpContext.Current.Response.Flush();
                    file.Delete();
                   // HttpContext.Current.Response.End();

                }
                else
                {
                    HttpContext.Current.Response.Write("This file does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
           
        }

        public static string GetAbsolutePath(string filepath)
        {

            string str = HttpContext.Current.Request.PhysicalPath.ToString();
            string subStr = str.Substring(0, str.LastIndexOf("\\"));

            return (ReplaceBackSlash(Path.Combine(subStr, filepath)));

        }

        public static string ReplaceBackSlash(string filepath)
        {
            if (filepath != null)
            {
                filepath = filepath.Replace("\\", "/");
            }
            return filepath;
        }

    }
}
