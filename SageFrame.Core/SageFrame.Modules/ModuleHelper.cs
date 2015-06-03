using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SageFrame.Core
{
    //Note in StringBuilder, it is not suppose to use  string concatination "+" but here for the human readable code "+" concatination is done

    /// <summary>
    /// Module helper class contains the necessary methods needed to  generate the module dynamically.
    /// </summary>
    public class ModuleHelper
    {
        /// <summary>
        /// Builds the necessary code containing the simple controller logic required for the controller class.
        /// </summary>
        /// <param name="columnlist"> Dictionery containg the list of column name as key and datatype as value.</param>
        /// <param name="moduleName">Module name.</param>
        /// <returns>Returns the methods codes required for the controller class.</returns>

        public static StringBuilder ControllerCode(Dictionary<string, string> columnlist, string moduleName)
        {
            StringBuilder code = new StringBuilder();
            string firstColumnName = columnlist.First().Key;
            string firstColumnDataType = columnlist.First().Value;
            code.Append(ExecuteNonQueryCode(moduleName, "Insert"));
            code.Append(ExecuteNonQueryCode(moduleName, "Update"));
            code.Append(SelectQuery(moduleName, "GetallData", true, firstColumnName, firstColumnDataType));
            code.Append(SelectQuery(moduleName, "GetByID", false, firstColumnName, firstColumnDataType));
            code.Append(DeleteQuery(moduleName, "DeleteByID", firstColumnName, firstColumnDataType));
            return code;
        }

        /// <summary>
        /// Builds the code for the methods: Insert and Update 
        /// </summary>
        /// <param name="moduleName">Module name.</param>
        /// <param name="methodName">Methods name.</param>
        /// <returns>Returns StringBuilder code containing the methods</returns>
        public static StringBuilder ExecuteNonQueryCode(string moduleName, string methodName)
        {
            StringBuilder code = new StringBuilder();
            code.Append("\t\tpublic void " + methodName + "(" + moduleName + "Info obj)\n");
            code.Append(ModuleHelper.MethodOpenCurlyBrace);
            code.Append(ModuleHelper.ExceptionTryBlock);
            code.Append("\t\t\t\t" + moduleName + "DataProvider objDataProvider = new " + moduleName + "DataProvider();\n");
            code.Append("\t\t\t\tobjDataProvider." + methodName + "(obj);\n");
            code.Append(ModuleHelper.ExceptionCatchBlock);
            code.Append(ModuleHelper.MethodCloseCurlyBrace);
            return code;
        }

        /// <summary>
        /// Builds the code for the methods: Select single and select all.
        /// </summary>
        /// <param name="moduleName">Module name</param>
        /// <param name="methodName">Method name.</param>
        /// <param name="selectall">set true if all the data is to be selected.</param>
        /// <param name="selectProperties">Property name.</param>
        /// <param name="selectDataType">Property's datatype.</param>
        /// <returns>Returns StringBuilder contaning the method codes required for select.</returns>
        public static StringBuilder SelectQuery(string moduleName, string methodName, bool selectall, string selectProperties, string selectDataType)
        {
            StringBuilder code = new StringBuilder();
            if (selectall)
            {
                code.Append("\t\tpublic List<" + moduleName + "Info> " + methodName + "(" + moduleName + "Info obj" + moduleName + ")\n");
            }
            else
            {
                code.Append("\t\tpublic " + moduleName + "Info " + methodName + "(" + selectDataType + "  " + selectProperties + ")\n");
            }
            code.Append(ModuleHelper.MethodOpenCurlyBrace);
            code.Append(ModuleHelper.ExceptionTryBlock);
            code.Append("\t\t\t\t" + moduleName + "DataProvider objDataProvider = new " + moduleName + "DataProvider();\n");
            if (selectall)
            {
                code.Append("\t\t\t\t return objDataProvider." + methodName + "(obj" + moduleName + ");\n");
            }
            else
            {
                code.Append("\t\t\t\t return objDataProvider." + methodName + "(" + selectProperties + ");\n");
            }
            code.Append(ModuleHelper.ExceptionCatchBlock);
            code.Append(ModuleHelper.MethodCloseCurlyBrace);
            return code;
        }

        /// <summary>
        /// Builds the code required for the   
        /// </summary>
        /// <param name="moduleName">Module name.</param>
        /// <param name="methodName">Method name.</param>
        /// <param name="selectProperties">Property name.</param>
        /// <param name="selectDataType">Properties's datatype</param>
        /// <returns>Returns StringBuilder containing the codes of methods of delete.</returns>
        public static StringBuilder DeleteQuery(string moduleName, string methodName, string selectProperties, string selectDataType)
        {
            StringBuilder code = new StringBuilder();
            code.Append("\t\tpublic void " + methodName + "(" + selectDataType + "  " + selectProperties + ")\n");
            code.Append(ModuleHelper.MethodOpenCurlyBrace);
            code.Append(ModuleHelper.ExceptionTryBlock);
            code.Append("\t\t\t\t" + moduleName + "DataProvider objDataProvider = new " + moduleName + "DataProvider();\n");
            code.Append("\t\t\t\t objDataProvider." + methodName + "(" + selectProperties + ");\n");
            code.Append(ModuleHelper.ExceptionCatchBlock);
            code.Append(ModuleHelper.MethodCloseCurlyBrace);
            return code;
        }


        /// <summary>
        /// Builds the dataprovider class methods: Insert , Update, delete, select all and select a particular row.
        /// </summary>
        /// <param name="columnlist">Dictionery containg the list of column name as key and datatype as value.</param>
        /// <param name="moduleName">Module name.</param>
        /// <param name="updateList">Dictionery containg the list of column name as key and datatype as value for updates only.</param>
        /// <param name="autoIncrement">Set true if the autoincrement in table is true.</param>
        /// <returns>Returns StringBuilder containing the codes for data provider. </returns>
        public static StringBuilder DataProviderCode(Dictionary<string, string> columnlist, string moduleName, Dictionary<string, string> updateList, bool autoIncrement)
        {
            StringBuilder code = new StringBuilder();
            string propertyName = columnlist.First().Key;
            string propertyDataType = columnlist.First().Value;
            code.Append(DataProviderSelectCode(columnlist, propertyName, propertyDataType, moduleName, "GetallData", true));
            code.Append(DataProviderSelectCode(columnlist, propertyName, propertyDataType, moduleName, "GetByID", false));
            code.Append(DataProviderDeleteCode(propertyName, propertyDataType, moduleName, "DeleteByID"));
            code.Append(DataProviderExecuteNonQuery(columnlist, moduleName, "Insert", autoIncrement));
            code.Append(DataProviderExecuteNonQuery(updateList, moduleName, "Update", autoIncrement));
            return code;
        }

        /// <summary>
        /// Builds code for methods:insert & update of dataprovider
        /// </summary>
        /// <param name="columnlist">Dictionery containg the list of column name as key and datatype as value.</param>
        /// <param name="moduleName">ModuleName</param>
        /// <param name="methodName">Method name.</param>
        /// <param name="autoIncrement">Set true if the autoincrement in the primery key in the table is true.</param>
        /// <returns>Returns StringBuilder containing the codes for Insert or update methods for dataprovider class.</returns>
        public static StringBuilder DataProviderExecuteNonQuery(Dictionary<string, string> columnlist, string moduleName, string methodName, bool autoIncrement)
        {
            StringBuilder code = new StringBuilder();
            code.Append("\t\tpublic void " + methodName + "(" + moduleName + "Info obj)\n");
            code.Append(ModuleHelper.MethodOpenCurlyBrace);
            code.Append(ModuleHelper.ExceptionTryBlock);
            code.Append("\t\t\t\tList<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>();\n");
            foreach (KeyValuePair<string, string> properties in columnlist)
            {
                if (!(autoIncrement && methodName == "Insert"))
                {
                    code.Append("\t\t\t\tparam.Add(new KeyValuePair<string, object>(\"@" + properties.Key + "\", obj." + properties.Key + "));\n");
                }
            }
            code.Append(ModuleHelper.SQLHandlerDeclaration);
            code.Append("\t\t\t\tsagesql.ExecuteNonQuery(\"[dbo].[usp_" + moduleName + "_" + methodName + "]\", param);\n");
            code.Append(ModuleHelper.ExceptionCatchBlock);
            code.Append(ModuleHelper.MethodCloseCurlyBrace);
            return code;
        }

        /// <summary>
        /// Builds code for methods:select all & select particular row of dataprovider
        /// </summary>
        /// <param name="columnlist">Dictionery containg the list of column name as key and datatype as value.</param>
        /// <param name="selectProperties">Property name.</param>
        /// <param name="selectDataType">property's datatype.</param>
        /// <param name="moduleName">Module name.</param>
        /// <param name="methodName">Method name.</param>
        /// <param name="selectall">Set true if all the values are to be selected.</param>
        /// <returns>Returns StringBuilder containing the codes for SelectAll or SelectByID  methods for dataprovider class.</returns>
        public static StringBuilder DataProviderSelectCode(Dictionary<string, string> columnlist, string selectProperties, string selectDataType, string moduleName, string methodName, bool selectall)
        {
            StringBuilder code = new StringBuilder();
            if (selectall)
            {
                code.Append("\t\tpublic List<" + moduleName + "Info> " + methodName + "(" + moduleName + "Info obj" + moduleName + ")\n");
            }
            else
            {
                code.Append("\t\tpublic " + moduleName + "Info " + methodName + "(" + selectDataType + "  " + selectProperties + ")\n");
            }
            code.Append(ModuleHelper.MethodOpenCurlyBrace);
            code.Append(ModuleHelper.ExceptionTryBlock);
            code.Append("\t\t\t\tList<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>();\n");
            if (selectall)
            {
                foreach (KeyValuePair<string, string> properties in columnlist)
                {
                    string columnName = properties.Key;
                    if (columnName == "PortalID" || columnName == "UserModuleID" || columnName == "Culture")
                    {
                        code.Append("\t\t\t\tparam.Add(new KeyValuePair<string, object>(\"@" + columnName + "\", obj" + moduleName + "." + columnName + "));\n");
                    }
                }
            }
            else
            {
                code.Append("\t\t\t\tparam.Add(new KeyValuePair<string, object>(\"@" + selectProperties + "\", " + selectProperties + "));\n");
            }
            code.Append(ModuleHelper.SQLHandlerDeclaration);
            if (selectall)
            {
                code.Append("\t\t\t\treturn sagesql.ExecuteAsList<" + moduleName + "Info>(\"[dbo].[usp_" + moduleName + "_" + methodName + "]\", param);\n");
            }
            else
            {
                code.Append("\t\t\t\treturn sagesql.ExecuteAsObject<" + moduleName + "Info>(\"[dbo].[usp_" + moduleName + "_" + methodName + "]\", param);\n");
            }
            code.Append(ModuleHelper.ExceptionCatchBlock);
            code.Append(ModuleHelper.MethodCloseCurlyBrace);
            return code;
        }

        /// <summary>
        ///  Builds code for delete methods of dataprovider
        /// </summary>
        /// <param name="selectProperties">Propety name.</param>
        /// <param name="selectDataType">Propery's datatype.</param>
        /// <param name="moduleName">Module name.</param>
        /// <param name="methodName">Method name.</param>
        /// <returns>Returns StringBuilder containing the codes for delete method for dataprovider class</returns>
        public static StringBuilder DataProviderDeleteCode(string selectProperties, string selectDataType, string moduleName, string methodName)
        {
            StringBuilder code = new StringBuilder();
            code.Append("\t\tpublic void " + methodName + "(" + selectDataType + "  " + selectProperties + ")\n");
            code.Append(ModuleHelper.MethodOpenCurlyBrace);
            code.Append(ModuleHelper.ExceptionTryBlock);
            code.Append("\t\t\t\tList<KeyValuePair<string, object>> param = new List<KeyValuePair<string, object>>();\n");
            code.Append("\t\t\t\tparam.Add(new KeyValuePair<string, object>(\"@" + selectProperties + "\", " + selectProperties + "));\n");
            code.Append(ModuleHelper.SQLHandlerDeclaration);
            code.Append("\t\t\t\t sagesql.ExecuteNonQuery(\"[dbo].[usp_" + moduleName + "_" + methodName + "]\", param);\n");
            code.Append(ModuleHelper.ExceptionCatchBlock);
            code.Append(ModuleHelper.MethodCloseCurlyBrace);
            return code;
        }

        /// <summary>
        ///  Builds code for methods of WebService.
        /// </summary>
        /// <param name="columnlist">Dictionery containg the list of column name as key and datatype as value.</param>
        /// <param name="moduleName">Module name.</param>
        /// <returns>Returns StringBuilder containing the codes for of Webservice</returns>
        public static StringBuilder WebServiceCode(Dictionary<string, string> columnlist, string moduleName)
        {
            StringBuilder code = new StringBuilder();
            string propertyName = columnlist.First().Key;
            string propertyDataType = columnlist.First().Value;
            code.Append(ExecuteNonQueryWebServiceCode(moduleName, "Insert"));
            code.Append(ExecuteNonQueryWebServiceCode(moduleName, "Update"));
            code.Append(WebServiceSelectQuery(moduleName, "GetallData", true, propertyName, propertyDataType));
            code.Append(WebServiceSelectQuery(moduleName, "GetByID", false, propertyName, propertyDataType));
            code.Append(WebServiceDeleteQuery(moduleName, "DeleteByID", propertyName, propertyDataType));
            return code;
        }

        /// <summary>
        ///  Builds code for method:Insert and Update of web service.
        /// </summary>
        /// <param name="moduleName">Module name.</param>
        /// <param name="methodName">Method name.</param>
        /// <returns>Returns StringBuilder object containg the code for Insert and Update of web service.</returns>
        public static StringBuilder ExecuteNonQueryWebServiceCode(string moduleName, string methodName)
        {
            StringBuilder code = new StringBuilder();
            code.Append("\t\t[WebMethod]\n");
            code.Append("\t\tpublic void " + methodName + "(" + moduleName + "Info obj, int portalID,  int userModuleID, string userName, string secureToken)\n");
            code.Append(ModuleHelper.MethodOpenCurlyBrace);
            code.Append(ModuleHelper.ExceptionTryBlock);
            code.Append("\t\t\t\tif(IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))\n");
            code.Append("\t\t");
            code.Append(ModuleHelper.MethodOpenCurlyBrace);
            code.Append("\t\t\t\t\t\t" + moduleName + "Controller objController = new " + moduleName + "Controller();\n");
            code.Append("\t\t\t\t\t\tobjController." + methodName + "(obj);\n");
            code.Append("\t\t");
            code.Append(ModuleHelper.MethodCloseCurlyBrace);
            code.Append(ModuleHelper.ExceptionCatchBlock);
            code.Append(ModuleHelper.MethodCloseCurlyBrace);
            return code;
        }

        /// <summary>
        ///  Builds code for method:Select all and Select particular row of web service.
        /// </summary>
        /// <param name="moduleName">Module name.</param>
        /// <param name="methodName">Method name.</param>
        /// <param name="selectall">Set true if the method is for select all</param>
        /// <param name="selectProperties">Property name.</param>
        /// <param name="selectDataType">Property's datatype.</param>
        /// <returns>Returns StringBuilder object containg the code for Select all and Select particular row Method of web service.</returns>
        public static StringBuilder WebServiceSelectQuery(string moduleName, string methodName, bool selectall, string selectProperties, string selectDataType)
        {
            StringBuilder code = new StringBuilder();
            if (selectall)
            {
                code.Append("\t\tpublic List<" + moduleName + "Info> " + methodName + "(" + moduleName + "Info obj" + moduleName + ")\n");
            }
            else
            {
                code.Append("\t\tpublic " + moduleName + "Info " + methodName + "(" + selectDataType + "  " + selectProperties + ")\n");
            }
            code.Append(ModuleHelper.MethodOpenCurlyBrace);
            code.Append(ModuleHelper.ExceptionTryBlock);
            code.Append("\t\t\t\t" + moduleName + "Controller objController = new " + moduleName + "Controller();\n");
            if (selectall)
            {
                code.Append("\t\t\t\t return objController." + methodName + "(obj" + moduleName + ");\n");
            }
            else
            {
                code.Append("\t\t\t\t return objController." + methodName + "(" + selectProperties + ");\n");
            }
            code.Append(ModuleHelper.ExceptionCatchBlock);
            code.Append(ModuleHelper.MethodCloseCurlyBrace);
            return code;
        }

        /// <summary>
        /// Builds code for method:Select all and Select particular row of web service.
        /// </summary>
        /// <param name="moduleName">Module name.</param>
        /// <param name="methodName">Method name.</param>
        /// <param name="selectProperties">Property name.</param>
        /// <param name="selectDataType">Property's datatype.</param>
        /// <returns>Returns StringBuilder object containg the code for Select all and Select particular row Method of web service.</returns>
        public static StringBuilder WebServiceDeleteQuery(string moduleName, string methodName, string selectProperties, string selectDataType)
        {
            StringBuilder code = new StringBuilder();
            code.Append("\t\tpublic void " + methodName + "(" + selectDataType + "  " + selectProperties + " , int portalID,  int userModuleID, string userName, string secureToken)\n");
            code.Append(ModuleHelper.MethodOpenCurlyBrace);
            code.Append(ModuleHelper.ExceptionTryBlock);
            code.Append("\t\t\t\tif(IsPostAuthenticatedView(portalID, userModuleID, userName, secureToken))\n");
            code.Append("\t\t");
            code.Append(ModuleHelper.MethodOpenCurlyBrace);
            code.Append("\t\t\t\t\t\t" + moduleName + "Controller objController = new " + moduleName + "Controller();\n");
            code.Append("\t\t\t\t\t\t objController." + methodName + "(" + selectProperties + ");\n");
            code.Append("\t\t");
            code.Append(ModuleHelper.MethodCloseCurlyBrace);
            code.Append(ModuleHelper.ExceptionCatchBlock);
            code.Append(ModuleHelper.MethodCloseCurlyBrace);
            return code;
        }
        /// <summary>
        /// Read only property which returns exception try block code.
        /// </summary>
        public static StringBuilder ExceptionTryBlock
        {
            get
            {
                StringBuilder block = new StringBuilder();
                block.Append("\t\t\ttry\n");
                block.Append("\t\t\t{\n");
                return block;
            }
        }
        /// <summary>
        /// Read only property which returns exception catch block code.
        /// </summary>
        public static StringBuilder ExceptionCatchBlock
        {
            get
            {
                StringBuilder block = new StringBuilder();
                block.Append("\t\t\t}\n");
                block.Append("\t\t\tcatch\n");
                block.Append("\t\t\t{\n");
                block.Append("\t\t\t\tthrow;\n");
                block.Append("\t\t\t}\n");
                return block;
            }
        }

        /// <summary>
        /// Returns a fully declaration of SQLHandler object. The object is sagesql.
        /// </summary>
        /// <returns>stringbuilder object with sqlHandler declaration. </returns>
        public static StringBuilder SQLHandlerDeclaration
        {
            get
            {
                return new StringBuilder("\t\t\t\tSQLHandler sagesql = new SQLHandler();\n");
            }
        }

        /// <summary>
        /// Read only property which returns method's curly brace open
        /// </summary>
        public static StringBuilder MethodOpenCurlyBrace
        {
            get
            {
                return new StringBuilder("\t\t{\n");
            }
        }

        /// <summary>
        /// Read only property which returns method's curly brace close
        /// </summary>
        public static StringBuilder MethodCloseCurlyBrace
        {
            get
            {
                return new StringBuilder("\t\t}\n");
            }
        }

        /// <summary>
        /// Builds the property declaration code for given property and its datatype.
        /// </summary>
        /// <param name="datatype">Property's data type.</param>
        /// <param name="properties">Property's name.</param>
        /// <returns>Returns StringBuilder object containing the property declaration code.</returns>
        public static StringBuilder SageProp(string datatype, string properties)
        {
            StringBuilder code = new StringBuilder();
            code.Append("\t\t public ");
            code.Append(datatype);
            code.Append(" ");
            code.Append(properties);
            code.Append(" { get; set; }");
            code.AppendLine();
            return code;
        }

        /// <summary>
        /// Converts the MSSQL datatype into its respective C# datatype.
        /// </summary>
        /// <param name="datatype">MSSQL datatype</param>
        /// <returns>returns C# datatype.</returns>
        public static string CsharpDatatype(string datatype)
        {
            switch (datatype)
            {
                case "bigint":
                    datatype = "long";
                    break;
                case "bit":
                    datatype = "bool";
                    break;
                case "date":
                    datatype = "DateTime";
                    break;
                case "datetime":
                    datatype = "DateTime";
                    break;
                case "geography":
                    datatype = "byte[]";
                    break;
                case "geometry":
                    datatype = "byte[]";
                    break;
                case "hierarchyid":
                    datatype = "byte[]";
                    break;
                case "image":
                    datatype = "byte[]";
                    break;
                case "int":
                    datatype = "int";
                    break;
                case "money":
                    datatype = "decimal";
                    break;
                case "ntext":
                    datatype = "string";
                    break;
                case "nvarchar(max)":
                    datatype = "string";
                    break;
                case "real":
                    datatype = "double";
                    break;
                case "smalldatetime":
                    datatype = "DateTime";
                    break;
                case "smallint":
                    datatype = "int";
                    break;
                case "smallmoney":
                    datatype = "decimal";
                    break;
                case "sql_variant":
                    datatype = "object";
                    break;
                case "text":
                    datatype = "string";
                    break;
                case "timestamp":
                    datatype = "DateTime";
                    break;
                case "tinyint":
                    datatype = "byte";
                    break;
                case "uniqueidentifier":
                    datatype = "Guid";
                    break;
                case "varbinary(max)":
                    datatype = "string";
                    break;
                case "varchar(max)":
                    datatype = "string";
                    break;
                case "xml":
                    datatype = "string";
                    break;
                default:
                    string firstPattern = @"binary\(|varbinary\(|char\(|nchar\(|varchar\(|nvarchar\(";
                    Regex firstRegex = new Regex(firstPattern);
                    Match firstMatch = firstRegex.Match(datatype);
                    if (firstMatch.Success)
                    {
                        datatype = "string";
                    }
                    firstPattern = @"datetime2\(|datetimeoffset\(|time\(";
                    firstRegex = new Regex(firstPattern);
                    firstMatch = firstRegex.Match(datatype);
                    if (firstMatch.Success)
                    {
                        datatype = "DateTime";
                    }
                    firstPattern = @"decimal\(|numeric\(";
                    firstRegex = new Regex(firstPattern);
                    firstMatch = firstRegex.Match(datatype);
                    if (firstMatch.Success)
                    {
                        datatype = "decimal";
                    }
                    break;
            }
            return datatype;
        }

        /// <summary>
        /// Checks if the  directory already exists and if not creates the directory.
        /// </summary>
        /// <param name="directoryPath">Destination directory path.</param>
        public static void CreateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }
    }
}
