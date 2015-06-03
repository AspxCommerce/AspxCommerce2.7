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
using System.Data;
using SageFrame.Web.Utilities;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections;
using SageFrame.Common.CommonFunction;
using SageFrame.Core.SageFrame.Search;
using System.Data.SqlClient;
using SageFrame.Common;

#endregion
namespace SageFrame.Search
{
    /// <summary>
    /// Manipulates data for search.
    /// </summary>
    public class SageFrameSearch
    {
        /// <summary>
        /// initializes an instance of SageFrameSearch class.
        /// </summary>
        public SageFrameSearch()
        {

        }

        /// <summary>
        /// Connects to database and adds or updates search settings.
        /// </summary>
        /// <param name="objSearchSettingInfo">SageFrameSearchSettingInfo object.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="CultureName">Culture name.</param>
        /// <param name="AddedBy">Setting adding user's name.</param>
        public void AddUpdateSageFrameSearchSetting(SageFrameSearchSettingInfo objSearchSettingInfo, int PortalID, string CultureName, string AddedBy)
        {

            try
            {
                string SettingKeys = string.Empty;
                string SettingValues = string.Empty;
                //Pre pare Key value for the save;
                SettingKeys = "SearchButtonType#SearchButtonText#SearchButtonImage#SearchResultPerPage#SearchResultPageName#MaxSearchChracterAllowedWithSpace#MaxResultChracterAllowedWithSpace";
                SettingValues = objSearchSettingInfo.SearchButtonType.ToString() + "#" + objSearchSettingInfo.SearchButtonText + "#" +
                    objSearchSettingInfo.SearchButtonImage + "#" + objSearchSettingInfo.SearchResultPerPage.ToString() +
                    "#" + objSearchSettingInfo.SearchResultPageName +
                    "#" + objSearchSettingInfo.MaxSearchChracterAllowedWithSpace.ToString() +
                    "#" + objSearchSettingInfo.MaxResultChracterAllowedWithSpace.ToString();

                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();

                ParaMeterCollection.Add(new KeyValuePair<string, string>("@SettingKeys", SettingKeys));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@SettingValues", SettingValues));

                ParaMeterCollection.Add(new KeyValuePair<string, string>("@CultureName", CultureName));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", PortalID.ToString()));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@AddedBy", AddedBy));
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("dbo.sp_SageFrameSearchSettingValueAddUpdate", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and deletes search procedure.
        /// </summary>
        /// <param name="SageFrameSearchProcedureID">SageFrame search procedure.</param>
        /// <param name="DeletedBy">Search procedure deleted user's name.</param>
        public void SageFrameSearchProcedureDelete(int SageFrameSearchProcedureID, string DeletedBy)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();

                ParaMeterCollection.Add(new KeyValuePair<string, string>("@SageFrameSearchProcedureID", SageFrameSearchProcedureID.ToString()));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@DeletedBy", DeletedBy));
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("dbo.sp_SageFrameSearchProcedureDelete", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Connects to database and adds or updates SageFrame search
        /// </summary>
        /// <param name="objInfo">Object of SageFrameSearchProcedureInfo class.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="AddedBy">Search added user's name.</param>
        public void SageFrameSearchProcedureAddUpdate(SageFrameSearchProcedureInfo objInfo, int PortalID, string AddedBy)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();

                ParaMeterCollection.Add(new KeyValuePair<string, string>("@SageFrameSearchProcedureID", objInfo.SageFrameSearchProcedureID.ToString()));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@SageFrameSearchTitle", objInfo.SageFrameSearchTitle));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@SageFrameSearchProcedureName", objInfo.SageFrameSearchProcedureName.ToString()));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@SageFrameSearchProcedureExecuteAs", objInfo.SageFrameSearchProcedureExecuteAs));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@IsActive", objInfo.IsActive.ToString()));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@AddedOn", objInfo.AddedOn.ToString()));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", PortalID.ToString()));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@AddedBy", AddedBy));
                SQLHandler sagesql = new SQLHandler();
                sagesql.ExecuteNonQuery("dbo.sp_SageFrameSearchProcedureAddUpdate", ParaMeterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and checks if the search page exists.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="PageName">Page name.</param>
        /// <returns>Returns true if search result exists.</returns>
        public bool SearchPageExists(int PortalID, string PageName)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();

                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PageName", PageName));

                SQLHandler sagesql = new SQLHandler();
                int count = 0;
                count = sagesql.ExecuteAsScalar<int>("usp_SearchResultPageExists", ParaMeterCollection);
                return count > 0 ? true : false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and  returns list of search contents.
        /// </summary>
        /// <param name="offset">Paging offset.</param>
        /// <param name="limit">Paging limit.</param>
        /// <param name="Searchword">Search word.</param>
        /// <param name="SearchBy">Searching user's name.</param>
        /// <param name="CultureName">Culture name.</param>
        /// <param name="IsUseFriendlyUrls">Set true if the URL is user friendly.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>list of search contents</returns>
        public List<SageFrameSearchInfo> GetSageSearchResultBySearchWord(int offset, int limit, string Searchword, string SearchBy, string CultureName, bool IsUseFriendlyUrls, int PortalID)
        {
            SqlDataReader reader = null;
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@Searchword", Searchword));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@CultureName", CultureName));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@IsUseFriendlyUrls", IsUseFriendlyUrls));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@SearchBy", SearchBy));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@PortalID", PortalID));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@offset", offset));
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@limit", limit));
                SQLHandler Objsql = new SQLHandler();

                reader = Objsql.ExecuteAsDataReader("[dbo].[sp_HtmlContentSearch]", ParaMeterCollection);
                List<SageFrameSearchInfo> searchList = new List<SageFrameSearchInfo>();
                while (reader.Read())
                {
                    SageFrameSearchInfo obj = new SageFrameSearchInfo();
                    obj.RowTotal = Convert.ToInt32(reader["RowTotal"]);
                    obj.PageName = reader["PageName"].ToString();
                    obj.HTMLContent = reader["HTMLContent"].ToString();
                    obj.URL = reader["URL"].ToString();
                    obj.UpdatedContentOn = reader["UpdatedContentOn"].ToString();
                    searchList.Add(obj);
                }
                return searchList;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

        }

        /// <summary>
        /// Connects to database and search by search word.
        /// </summary>
        /// <param name="offset">Paging offset.</param>
        /// <param name="limit">Paging  limit.</param>
        /// <param name="Searchword">Search word.</param>
        /// <param name="SearchBy">Searching user's name.</param>
        /// <param name="CultureName">Culture name.</param>
        /// <param name="IsUseFriendlyUrls">Set true if the url is user friendly.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>List of Search contents.</returns>
        public List<SageFrameSearchInfo> SageSearchBySearchWord(int offset, int limit, string Searchword, string SearchBy, string CultureName, bool IsUseFriendlyUrls, int PortalID)
        {

            try
            {

                List<SageFrameSearchInfo> searchList = new List<SageFrameSearchInfo>();

                SQLHandler sagesql = new SQLHandler();

                DataSet ds = new DataSet();

                ds = sagesql.ExecuteScriptAsDataSet("[dbo].[sp_SageSearchBySearchKey] N'" + Searchword + "','" + SearchBy + "','" + IsUseFriendlyUrls + "','" + CultureName + "','" + PortalID + "','" + offset + "'," + limit);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    SageFrameSearchInfo obj = new SageFrameSearchInfo();
                    obj.RowTotal = Convert.ToInt32(row["RowTotal"]);
                    obj.PageName = row["PageName"].ToString();
                    obj.HTMLContent = row["HTMLContent"].ToString();
                    obj.URL = row["URL"].ToString();
                    obj.UpdatedContentOn = row["UpdatedContentOn"].ToString();
                    obj.SearchWord = row["SearchWord"].ToString();
                    searchList.Add(obj);
                }

                return searchList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Connects to database and returns search settings by portal ID and culture name.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <param name="CultureName">Culture name.</param>
        /// <returns>List of setting values.</returns>
        public SageFrameSearchSettingInfo LoadSearchSettings(int PortalID, string CultureName)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();

                ParaMeterCollection.Add(new KeyValuePair<string, string>("@CultureName", CultureName));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", PortalID.ToString()));
                DataSet ds = new DataSet();
                SQLHandler sagesql = new SQLHandler();
                ds = sagesql.ExecuteAsDataSet("dbo.sp_SageFrameSearchSettingValueGet", ParaMeterCollection);
                SageFrameSearchSettingInfo objSearchSettingInfo = new SageFrameSearchSettingInfo();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string strKey = dt.Rows[i]["SettingKey"].ToString();
                        switch (strKey)
                        {
                            case "SearchButtonType":
                                if (dt.Rows[i]["SettingValue"].ToString() != string.Empty)
                                {
                                    objSearchSettingInfo.SearchButtonType = Int32.Parse(dt.Rows[i]["SettingValue"].ToString());
                                }
                                break;
                            case "SearchButtonText":
                                objSearchSettingInfo.SearchButtonText = dt.Rows[i]["SettingValue"].ToString();
                                break;
                            case "SearchButtonImage":
                                objSearchSettingInfo.SearchButtonImage = dt.Rows[i]["SettingValue"].ToString();
                                break;
                            case "SearchResultPerPage":
                                if (dt.Rows[i]["SettingValue"].ToString() != string.Empty)
                                {
                                    objSearchSettingInfo.SearchResultPerPage = Int32.Parse(dt.Rows[i]["SettingValue"].ToString());
                                }
                                break;
                            case "SearchResultPageName":
                                objSearchSettingInfo.SearchResultPageName = dt.Rows[i]["SettingValue"].ToString();
                                break;
                            case "MaxSearchChracterAllowedWithSpace":
                                if (dt.Rows[i]["SettingValue"].ToString() != string.Empty)
                                {
                                    objSearchSettingInfo.MaxSearchChracterAllowedWithSpace = Int32.Parse(dt.Rows[i]["SettingValue"].ToString());
                                }
                                break;
                            case "MaxResultChracterAllowedWithSpace":
                                if (dt.Rows[i]["SettingValue"].ToString() != string.Empty)
                                {
                                    objSearchSettingInfo.MaxResultChracterAllowedWithSpace = Int32.Parse(dt.Rows[i]["SettingValue"].ToString());
                                }
                                break;
                        }
                    }
                }
                return objSearchSettingInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        ///  Connects to database and returns searching procedure list.
        /// </summary>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>DataTable containing list of procedures.</returns>
        public DataTable ListSageSerchProcedures(int PortalID)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();

                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", PortalID.ToString()));
                DataSet ds = new DataSet();
                SQLHandler sagesql = new SQLHandler();
                ds = sagesql.ExecuteAsDataSet("dbo.sp_SageFrameSearchProcedureList", ParaMeterCollection);
                return ds.Tables[0];

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Connects to database and returns list of search contents.
        /// </summary>
        /// <param name="Searchword">Search word.</param>
        /// <param name="SearchBy">Searching user's name.</param>
        /// <param name="CultureName">Culture name.</param>
        /// <param name="IsUseFriendlyUrls">Set true if the URL is user friendly.</param>
        /// <param name="PortalID">Portal ID.</param>
        /// <returns>DataSet containing the list of searched contents.</returns>
        public DataSet SageSearchBySearchWord(string Searchword, string SearchBy, string CultureName, bool IsUseFriendlyUrls, int PortalID)
        {
            try
            {
                List<KeyValuePair<string, string>> ParaMeterCollection = new List<KeyValuePair<string, string>>();
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@Searchword", Searchword));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@CultureName", CultureName));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@IsUseFriendlyUrls", IsUseFriendlyUrls.ToString()));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@SearchBy", SearchBy));
                ParaMeterCollection.Add(new KeyValuePair<string, string>("@PortalID", PortalID.ToString()));
                DataSet ds = new DataSet();
                SQLHandler sagesql = new SQLHandler();
                ds = sagesql.ExecuteAsDataSet("dbo.sp_SageSearchBySearchKey", ParaMeterCollection);
                return ds;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Connects to database and returns search procedure name by search procedure ID.
        /// </summary>
        /// <param name="SageFrameSearchProcedureID">Search procedure ID.</param>
        /// <returns>Search procedure.</returns>
        public SageFrameSearchProcedureInfo SageFrameSearchProcedureGet(string SageFrameSearchProcedureID)
        {
            try
            {
                List<KeyValuePair<string, object>> ParaMeterCollection = new List<KeyValuePair<string, object>>();
                ParaMeterCollection.Add(new KeyValuePair<string, object>("@SageFrameSearchProcedureID", SageFrameSearchProcedureID));
                SQLHandler sagesql = new SQLHandler();
                SageFrameSearchProcedureInfo objSpIno = sagesql.ExecuteAsObject<SageFrameSearchProcedureInfo>("dbo.sp_SageFrameSearchProcedureGet", ParaMeterCollection);
                return objSpIno;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        /// <summary>
        /// Adds quotation for search words
        /// </summary>
        /// <param name="InputString">saerching string</param>
        /// <returns>Quoted searching string.</returns>
        public string AddQuotesForSQLSearch(string InputString)
        {
            string ReturnString = string.Empty;
            try
            {
                ReturnString = InputString.Trim();
                // Remove white space from beginning and end.
                if (InputString.Contains("'"))
                {
                    ReturnString = string.Empty;
                    string[] arrColl = InputString.Split("'".ToCharArray());
                    for (int i = 0; i < arrColl.Length; i++)
                    {
                        if (arrColl[i].ToString().Trim() != string.Empty)
                        {
                            if (i != arrColl.Length - 1)
                            {
                                ReturnString += arrColl[i].ToString() + "''";
                            }
                            else
                            {
                                ReturnString += arrColl[i].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ReturnString = "Exception caught: " + e.ToString();
            }
            return ReturnString;
        }

        /// <summary>
        /// Checks Ignoring words which helps tp prevent from script injection.
        /// </summary>
        /// <param name="SearchString">Searching string.</param>
        /// <param name="currentCulture">Culture name.</param>
        /// <returns>Returns true if there is no ignoring words.</returns>
        public bool CheckIgnorWords(string SearchString, string currentCulture)
        {
            string IgnorWords = string.Empty;

            IgnorWords = "^ , ; : [] ] [ {} () } { ) ( _ = < > . + - \\ / \" \"\" ' ! % * @~ @# @& &? & # ? about 1 after 2 all also 3 an 4 and 5 another 6 any 7 are 8 as 9 at 0 be $ because been before being between both but by came can come could did do each for from get got has had he have her here him himself his how if in into is it like make many me might more most much must my never now of on only or other our out over said same see should since some still such take than that the their them then there these they this those through to too under up very was way we well were what where which while who with would you your a b c d e f g h i j k l m n o p q r s t u v w x y z";
            SearchString = RemoveSpcalSymbol(SearchString);
            if (currentCulture == "en-US")
            {
                if (IgnorWords.Contains(SearchString) || SearchString.Trim() == "" || !HTMLHelper.IsValidSearchWord(SearchString))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Removes special space symbol from the input.
        /// </summary>
        /// <param name="SearchString">input string.</param>
        /// <returns>Removed space symbol.</returns>
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
    }

}
