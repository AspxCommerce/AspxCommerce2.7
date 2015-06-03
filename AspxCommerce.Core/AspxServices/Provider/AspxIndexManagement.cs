/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Collections.Generic;
using SageFrame.Web.Utilities;
using System.Data.SqlClient;
using System.Data;
using AspxCommerce.Core;

namespace AspxCommerce.Core
{
    public class AspxIndexManagement
    {
        public static List<IndexManagement> GetIndexedTables(int offset, int limit)
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string,object>>();
                parameter.Add(new KeyValuePair<string, object>("@offset", offset));
                parameter.Add(new KeyValuePair<string, object>("@limit", limit));
                SQLHandler sqlH = new SQLHandler();
                List<IndexManagement> lstIndexTables = sqlH.ExecuteAsList<IndexManagement>("[dbo].[usp_Aspx_GetTablesIndexation]", parameter);
                return lstIndexTables;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void ReOrganizeTable(string tableName)
        {
            try
            {
                
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteSQL("ALTER INDEX ALL ON " + tableName + " REORGANIZE");
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void ReIndexTable(string tableName)
        {
            try
            {

                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteSQL("ALTER INDEX ALL ON " + tableName + " REBUILD WITH (FILLFACTOR=90,ONLINE=ON)");

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void ReIndexAllTables()
        {
            try
            {
                List<KeyValuePair<string, object>> parameter = new List<KeyValuePair<string, object>>();
                SQLHandler sqlH = new SQLHandler();
                sqlH.ExecuteNonQuery("[dbo].[usp_Aspx_ReIndexAllTables]");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
