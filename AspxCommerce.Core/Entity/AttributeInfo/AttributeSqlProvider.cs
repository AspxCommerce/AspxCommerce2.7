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

namespace AspxCommerce.Core
{
    public class AttributeSqlProvider
    {
        //public List<AttributeFormInfo> GetFormAttributes(Int32 StoreID, Int32 PortalID, string Culture)
        //{
        //    List<AttributeFormInfo> formAttributeList = new List<AttributeFormInfo>();
        //    SqlCommand sqlCmd = new SqlCommand();
        //    sqlCmd.Connection = conn;
        //    sqlCmd.CommandType = CommandType.StoredProcedure;
        //    sqlCmd.CommandText = "aspx_sp_TestDynamicForm";
        //    SqlParameter param1 = new SqlParameter("@StoreID", StoreID);
        //    SqlParameter param2 = new SqlParameter("@PortalID", PortalID);
        //    SqlParameter param3 = new SqlParameter("@Culture", Culture);
        //    sqlCmd.Parameters.Add(param1);
        //    sqlCmd.Parameters.Add(param2);
        //    sqlCmd.Parameters.Add(param3);
        //    SqlDataReader reader;
        //    try
        //    {
        //        conn.Open();
        //        using (reader = sqlCmd.ExecuteReader())
        //        {
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    //forumList=DataSourceHelper.FillCollection(reader, typeof(Forum));
        //                    AttributeFormInfo form = new AttributeFormInfo();
        //                    form.AttributeID = Int32.Parse(reader["AttributeID"].ToString());
        //                    form.AttributeName = reader["AttributeName"].ToString();
        //                    form.DefaultValue = reader["DefaultValue"].ToString();
        //                    form.GroupID = Int32.Parse(reader["GroupID"].ToString());
        //                    form.GroupName = reader["GroupName"].ToString();
        //                    form.Help = reader["Help"].ToString();
        //                    form.InputTypeID = Int32.Parse(reader["InputTypeID"].ToString());
        //                    form.InputTypeValues = reader["InputTypeValues"].ToString();
        //                    form.IsEnableEditor = bool.Parse(reader["IsEnableEditor"].ToString());
        //                    form.IsRequired = bool.Parse(reader["IsRequired"].ToString());
        //                    form.PortalID = Int32.Parse(reader["PortalID"].ToString());
        //                    form.IsUnique = bool.Parse(reader["IsUnique"].ToString());
        //                    form.Length = Int32.Parse(reader["Length"].ToString());
        //                    form.StoreID = Int32.Parse(reader["StoreID"].ToString());
        //                    form.ToolTip = reader["ToolTip"].ToString();
        //                    form.ValidationTypeID = Int32.Parse(reader["ValidationTypeID"].ToString());
        //                    formAttributeList.Add(form);
        //                }
        //            }
        //        }
        //        return formAttributeList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}
        public bool CheckUniquenessBoolean(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, bool attributeValue)
        {
            try
            {

                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@Flag", flag));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", attributeValue));               
                bool isUnique = sqlH.ExecuteAsScalar<bool>("dbo.usp_Aspx_CheckUniqueness_Boolean", parameterCollection);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckUniquenessDecimal(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, Decimal attributeValue)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@Flag", flag));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", attributeValue));
                bool isUnique = sqlH.ExecuteAsScalar<bool>("dbo.usp_Aspx_CheckUniqueness_Decimal", parameterCollection);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckUniquenessDate(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, DateTime attributeValue)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@Flag", flag));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", attributeValue));
                bool isUnique = sqlH.ExecuteAsScalar<bool>("dbo.usp_Aspx_CheckUniqueness_Date", parameterCollection);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckUniquenessFile(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, string attributeValue)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@Flag", flag));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", attributeValue));
                bool isUnique = sqlH.ExecuteAsScalar<bool>("dbo.usp_Aspx_CheckUniqueness_File", parameterCollection);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public bool CheckUniquenessInt(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, Int32 attributeValue)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@Flag", flag));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", attributeValue));
                bool isUnique = sqlH.ExecuteAsScalar<bool>("dbo.usp_Aspx_CheckUniqueness_Int", parameterCollection);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckUniquenessNvarchar(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, string attributeValue)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@Flag", flag));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", attributeValue));
                bool isUnique = sqlH.ExecuteAsScalar<bool>("dbo.usp_Aspx_CheckUniqueness_Nvarchar", parameterCollection);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckUniquenessText(Int32 flag, Int32 storeID, Int32 portalID, Int32 attributeID, string attributeValue)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@Flag", flag));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", portalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", storeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeValue", attributeValue));
                bool isUnique = sqlH.ExecuteAsScalar<bool>("dbo.usp_Aspx_CheckUniqueness_Text", parameterCollection);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------- Added for unique name check ---------------------
        public bool CheckUniqueName(AttributeBindInfo attrbuteUniqueObj, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                SQLHandler sqlH = new SQLHandler();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeName", attrbuteUniqueObj.AttributeName));
                parameterCollection.Add(new KeyValuePair<string, object>("@AttributeID", attrbuteUniqueObj.AttributeID));
                parameterCollection.Add(new KeyValuePair<string, object>("@StoreID", aspxCommonObj.StoreID));
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", aspxCommonObj.PortalID));
                parameterCollection.Add(new KeyValuePair<string, object>("@CultureName", aspxCommonObj.CultureName));
                return sqlH.ExecuteNonQueryAsBool("dbo.usp_Aspx_AttributeUniquenessCheck", parameterCollection, "@IsUnique");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}