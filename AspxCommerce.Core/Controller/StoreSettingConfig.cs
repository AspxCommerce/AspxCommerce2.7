using System;
using System.Collections;
using System.Web;
using System.Data;

namespace AspxCommerce.Core
{
    public class StoreSettingConfig
    {
        public StoreSettingConfig()
        {
        }
        public void GetStoreSettingParamTwo(string key1, string key2, out string value1, out string value2,
            int storeID, int portalID, string cultureName) 
        {
            try
            {
                string strStoreID = storeID.ToString() ;
                string strPortalID = portalID.ToString();
                StoreSettingProvider sep = new StoreSettingProvider();
                Hashtable hst = new Hashtable();
                if (HttpContext.Current.Cache["AspxStoreSetting" + strPortalID+ strStoreID] != null)
                {
                    hst = (Hashtable)HttpContext.Current.Cache["AspxStoreSetting" + strPortalID+ strStoreID];
                }
                else
                {
                    DataTable dt = sep.GetStoreSettings(storeID, portalID, cultureName); //GetSettingsByPortal();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                        }
                    }
                }
                //need to be cleared when any key is chnaged
                HttpContext.Current.Cache.Insert("AspxStoreSetting" + strPortalID+ strStoreID, hst);//

                value1 = hst[strPortalID+ strStoreID + "." + key1].ToString();
                value2 = hst[strPortalID+ strStoreID + "." + key2].ToString();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void GetStoreSettingParamThree(string key1, string key2, string key3, out string value1, out string value2, 
            out string value3, int storeID, int portalID, string cultureName)
        {
            try
            {
                string strStoreID = storeID.ToString();
                string strPortalID = portalID.ToString();
                StoreSettingProvider sep = new StoreSettingProvider();
                Hashtable hst = new Hashtable();
                if (HttpContext.Current.Cache["AspxStoreSetting" + strPortalID+ strStoreID] != null)
                {
                    hst = (Hashtable)HttpContext.Current.Cache["AspxStoreSetting" + strPortalID+ strStoreID];
                }
                else
                {
                    DataTable dt = sep.GetStoreSettings(storeID, portalID, cultureName); //GetSettingsByPortal();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                        }
                    }
                }
                //need to be cleared when any key is chnaged
                HttpContext.Current.Cache.Insert("AspxStoreSetting" + strPortalID+ strStoreID, hst);//

                value1 = hst[strPortalID+ strStoreID + "." + key1].ToString();
                value2 = hst[strPortalID+ strStoreID + "." + key2].ToString();
                value3 = hst[strPortalID+ strStoreID + "." + key3].ToString();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void GetStoreSettingParamFour(string key1, string key2, string key3, string key4, out string value1, out string value2,
       out string value3, out string value4, int storeID, int portalID, string cultureName)
        {
            try
            {
                string strStoreID = storeID.ToString();
                string strPortalID = portalID.ToString();
                StoreSettingProvider sep = new StoreSettingProvider();
                Hashtable hst = new Hashtable();
                if (HttpContext.Current.Cache["AspxStoreSetting" + strPortalID + strStoreID] != null)
                {
                    hst = (Hashtable)HttpContext.Current.Cache["AspxStoreSetting" + strPortalID + strStoreID];
                }
                else
                {
                    DataTable dt = sep.GetStoreSettings(storeID, portalID, cultureName); //GetSettingsByPortal();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                        }
                    }
                }
                //need to be cleared when any key is chnaged
                HttpContext.Current.Cache.Insert("AspxStoreSetting" + strPortalID + strStoreID, hst);//

                value1 = hst[strPortalID + strStoreID + "." + key1].ToString();
                value2 = hst[strPortalID + strStoreID + "." + key2].ToString();
                value3 = hst[strPortalID + strStoreID + "." + key3].ToString();
                value4 = hst[strPortalID + strStoreID + "." + key4].ToString();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void GetStoreSettingParamFive(string key1, string key2, string key3, string key4, string key5, out string value1, out string value2,
 out string value3, out string value4, out string value5, int storeID, int portalID, string cultureName)
        {
            try
            {
                string strStoreID = storeID.ToString();
                string strPortalID = portalID.ToString();
                StoreSettingProvider sep = new StoreSettingProvider();
                Hashtable hst = new Hashtable();
                if (HttpContext.Current.Cache["AspxStoreSetting" + strPortalID + strStoreID] != null)
                {
                    hst = (Hashtable)HttpContext.Current.Cache["AspxStoreSetting" + strPortalID + strStoreID];
                }
                else
                {
                    DataTable dt = sep.GetStoreSettings(storeID, portalID, cultureName); //GetSettingsByPortal();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                        }
                    }
                }
                //need to be cleared when any key is chnaged
                HttpContext.Current.Cache.Insert("AspxStoreSetting" + strPortalID + strStoreID, hst);//

                value1 = hst[strPortalID + strStoreID + "." + key1].ToString();
                value2 = hst[strPortalID + strStoreID + "." + key2].ToString();
                value3 = hst[strPortalID + strStoreID + "." + key3].ToString();
                value4 = hst[strPortalID + strStoreID + "." + key4].ToString();
                value5 = hst[strPortalID + strStoreID + "." + key5].ToString();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void GetStoreSettingParamEight(string key1, string key2, string key3, string key4, string key5, 
            string key6, string key7, string key8, out string value1, out string value2, out string value3, 
            out string value4, out string value5, out string value6, out string value7, out string value8, 
            int storeID, int portalID, string cultureName)
        {
            try
            {
                string strStoreID = storeID.ToString();
                string strPortalID = portalID.ToString();
                StoreSettingProvider sep = new StoreSettingProvider();
                Hashtable hst = new Hashtable();
                if (HttpContext.Current.Cache["AspxStoreSetting" + strPortalID+ strStoreID] != null)
                {
                    hst = (Hashtable)HttpContext.Current.Cache["AspxStoreSetting" + strPortalID+ strStoreID];
                }
                else
                {
                    DataTable dt = sep.GetStoreSettings(storeID, portalID, cultureName); //GetSettingsByPortal();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                        }
                    }
                }
                //need to be cleared when any key is chnaged
                HttpContext.Current.Cache.Insert("AspxStoreSetting" + strPortalID+ strStoreID, hst);//

                value1 = hst[strPortalID+ strStoreID + "." + key1].ToString();
                value2 = hst[strPortalID+ strStoreID + "." + key2].ToString();
                value3 = hst[strPortalID+ strStoreID + "." + key3].ToString();
                value4 = hst[strPortalID+ strStoreID + "." + key4].ToString();
                value5 = hst[strPortalID+ strStoreID + "." + key5].ToString();
                value6 = hst[strPortalID+ strStoreID + "." + key6].ToString();
                value7 = hst[strPortalID+ strStoreID + "." + key7].ToString();
                value8 = hst[strPortalID+ strStoreID + "." + key8].ToString();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void GetStoreSettingParamSeven(string key1, string key2, string key3, string key4, string key5, string key6, string key7,
            out string value1, out string value2, out string value3, out string value4,
            out string value5, out string value6, out string value7,
            int storeID, int portalID, string cultureName)
        {
            try
            {
                string strStoreID = storeID.ToString();
                string strPortalID = portalID.ToString();
                StoreSettingProvider sep = new StoreSettingProvider();
                Hashtable hst = new Hashtable();
                if (HttpContext.Current.Cache["AspxStoreSetting" + strPortalID+ strStoreID] != null)
                {
                    hst = (Hashtable)HttpContext.Current.Cache["AspxStoreSetting" + strPortalID+ strStoreID];
                }
                else
                {
                    DataTable dt = sep.GetStoreSettings(storeID, portalID, cultureName); //GetSettingsByPortal();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                        }
                    }
                }
                //need to be cleared when any key is chnaged
                HttpContext.Current.Cache.Insert("AspxStoreSetting" + strPortalID+ strStoreID, hst);//

                value1 = hst[strPortalID+ strStoreID + "." + key1].ToString();
                value2 = hst[strPortalID+ strStoreID + "." + key2].ToString();
                value3 = hst[strPortalID+ strStoreID + "." + key3].ToString();
                value4 = hst[strPortalID+ strStoreID + "." + key4].ToString();
                value5 = hst[strPortalID+ strStoreID + "." + key5].ToString();
                value6 = hst[strPortalID+ strStoreID + "." + key6].ToString();
                value7 = hst[strPortalID+ strStoreID + "." + key7].ToString();
              

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void GetStoreSettingParamNine(string key1, string key2, string key3, string key4, string key5,
            string key6, string key7, string key8, string key9, out string value1, out string value2, out string value3,
            out string value4, out string value5, out string value6, out string value7, out string value8, out string value9,
            int storeID, int portalID, string cultureName)
        {
            try
            {
                string strStoreID = storeID.ToString();
                string strPortalID = portalID.ToString();
                StoreSettingProvider sep = new StoreSettingProvider();
                Hashtable hst = new Hashtable();
                if (HttpContext.Current.Cache["AspxStoreSetting" + strPortalID + strStoreID] != null)
                {
                    hst = (Hashtable)HttpContext.Current.Cache["AspxStoreSetting" + strPortalID + strStoreID];
                }
                else
                {
                    DataTable dt = sep.GetStoreSettings(storeID, portalID, cultureName); //GetSettingsByPortal();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                        }
                    }
                }
                //need to be cleared when any key is chnaged
                HttpContext.Current.Cache.Insert("AspxStoreSetting" + strPortalID + strStoreID, hst);//

                value1 = hst[strPortalID + strStoreID + "." + key1].ToString();
                value2 = hst[strPortalID + strStoreID + "." + key2].ToString();
                value3 = hst[strPortalID + strStoreID + "." + key3].ToString();
                value4 = hst[strPortalID + strStoreID + "." + key4].ToString();
                value5 = hst[strPortalID + strStoreID + "." + key5].ToString();
                value6 = hst[strPortalID + strStoreID + "." + key6].ToString();
                value7 = hst[strPortalID + strStoreID + "." + key7].ToString();
                value8 = hst[strPortalID + strStoreID + "." + key8].ToString();
                value9 = hst[strPortalID + strStoreID + "." + key9].ToString();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void GetStoreSettingParamEleven(string key1, string key2, string key3, string key4, string key5,
           string key6, string key7, string key8, string key9, string key10, string key11, out string value1, out string value2, out string value3,
           out string value4, out string value5, out string value6, out string value7, out string value8, out string value9, out string value10, out string value11,
           int storeID, int portalID, string cultureName)
        {
            try
            {
                string strStoreID = storeID.ToString();
                string strPortalID = portalID.ToString();
                StoreSettingProvider sep = new StoreSettingProvider();
                Hashtable hst = new Hashtable();
                if (HttpContext.Current.Cache["AspxStoreSetting" + strPortalID + strStoreID] != null)
                {
                    hst = (Hashtable)HttpContext.Current.Cache["AspxStoreSetting" + strPortalID + strStoreID];
                }
                else
                {
                    DataTable dt = sep.GetStoreSettings(storeID, portalID, cultureName); //GetSettingsByPortal();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                        }
                    }
                }
                //need to be cleared when any key is chnaged
                HttpContext.Current.Cache.Insert("AspxStoreSetting" + strPortalID + strStoreID, hst);//

                value1 = hst[strPortalID + strStoreID + "." + key1].ToString();
                value2 = hst[strPortalID + strStoreID + "." + key2].ToString();
                value3 = hst[strPortalID + strStoreID + "." + key3].ToString();
                value4 = hst[strPortalID + strStoreID + "." + key4].ToString();
                value5 = hst[strPortalID + strStoreID + "." + key5].ToString();
                value6 = hst[strPortalID + strStoreID + "." + key6].ToString();
                value7 = hst[strPortalID + strStoreID + "." + key7].ToString();
                value8 = hst[strPortalID + strStoreID + "." + key8].ToString();
                value9 = hst[strPortalID + strStoreID + "." + key9].ToString();
                value10 = hst[strPortalID + strStoreID + "." + key10].ToString();
                value11 = hst[strPortalID + strStoreID + "." + key11].ToString();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetStoreSettingsByKey(string key, int storeID, int portalID, string cultureName)
        {
            try
            {
                string strStoreID = storeID.ToString();
                string strPortalID = portalID.ToString();
                string strValue;
                StoreSettingProvider sep = new StoreSettingProvider();
                Hashtable hst = new Hashtable();
                if (HttpContext.Current.Cache["AspxStoreSetting" + strPortalID + strStoreID] != null)
                {
                    hst = (Hashtable)HttpContext.Current.Cache["AspxStoreSetting" + strPortalID + strStoreID];
                }
                else
                {
                    DataTable dt = sep.GetStoreSettings(storeID, portalID, cultureName); //GetSettingsByPortal();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                        }
                    }
                }
                //need to be cleared when any key is chnaged
                HttpContext.Current.Cache.Insert("AspxStoreSetting" + strPortalID + strStoreID, hst);//

                strValue = hst[strPortalID+ strStoreID + "." + key].ToString();
                return strValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ResetStoreSettingKeys(int storeID, int portalID, string cultureName)
        {
            StoreSettingProvider sep = new StoreSettingProvider();
            Hashtable hst = new Hashtable();
            DataTable dt = sep.GetStoreSettings(storeID, portalID, cultureName);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    hst.Add(dt.Rows[i]["SettingKey"].ToString(), dt.Rows[i]["SettingValue"].ToString());
                }
            }
            //need to be cleared when any key is chnaged
            HttpContext.Current.Cache.Insert("AspxStoreSetting" + portalID.ToString()+ storeID.ToString(), hst);//
        }

    }
}
