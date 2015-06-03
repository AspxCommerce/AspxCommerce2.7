using SageFrame.Scheduler;
using SageFrame.Web;
using SageFrame.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspxCommerce.ApplyCatalogPriceRules
{
    public class ApplyCategoryPriceRules : TaskBase
    {
        private static int CurrentPortalID = 0;
        private static int CurrentOffset = 1;
        private static int CurrentLimit = 0;
        private static string CurrentCultureName = "en-US";

        public ApplyCategoryPriceRules(ScheduleHistory scheduleHistory)
            : base()
        {
            base.TaskRecordItem = scheduleHistory;
        }

        public override void Execute()
        {
            List<int> portalIds = GetListPortalIDs();

            foreach (int portalId in portalIds)
            {
                if (CheckCatalogRuleExist(portalId, portalId))
                {
                    CurrentPortalID = portalId;
                    int rowTotal = GetItemID(CurrentPortalID);
                    decimal intTempLoops = Math.Ceiling(Convert.ToDecimal((double)rowTotal / 1000));

                    List<Thread> listThreads = new List<Thread>();

                    for (int i = 1; i <= intTempLoops; i++)
                    {
                        bool startNewThread = true;
                        foreach (Thread thread in listThreads)
                        {
                            if (thread.IsAlive)
                            {
                                startNewThread = false;
                            }
                        }

                        if (startNewThread)
                        {
                            CurrentOffset = CurrentLimit + 1;
                            CurrentLimit = 1000 * i;
                            if (CurrentLimit >= rowTotal)
                            {
                                CurrentLimit = rowTotal;
                            }

                            Thread newThread = new Thread(AnalyseCatalogPrice);
                            listThreads.Add(newThread);
                            newThread.Start();
                        }
                        else
                        {
                            i--;
                        }

                    }

                    while (listThreads.Last().IsAlive)
                    {

                    }

                    TruncateCatalogAffectedPrice(CurrentPortalID); 
                    CurrentOffset = 1;
                    CurrentLimit = 0;
                }
                else
                {
                    DeleteCatalogAffectedPrice(portalId, portalId);
                }
            }
        }

        public static List<int> GetListPortalIDs()
        {
            try
            {
                List<int> objHDSetting = new List<int>();
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                SQLHandler sqLH = new SQLHandler();
                SqlDataReader result = sqLH.ExecuteAsDataReader("[dbo].[usp_Aspx_CatalogGetPortalIDs]", parameterCollection);
                try
                {
                    while (result.Read())
                    {
                        objHDSetting.Add(Convert.ToInt32(result["PortalID"]));
                    }

                }
                catch (Exception e)
                {
                    throw e;
                }

                return objHDSetting;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int GetItemID(int iPortalID)
        {
            int result = 0;
            
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", iPortalID));
                SQLHandler sqLH = new SQLHandler();
                result = sqLH.ExecuteAsScalar<int>("[dbo].[usp_Aspx_CatalogGetTotalItem]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public void DeleteCatalogAffectedPrice(int iPortalID, int iStoreID)
        {
            try
            {
                SQLHandler sqlHandler = new SQLHandler();
                List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
                paramList.Add(new KeyValuePair<string, object>("@PortalID", iPortalID));
                paramList.Add(new KeyValuePair<string, object>("@StoreID", iStoreID));
                sqlHandler.ExecuteNonQuery("[dbo].[usp_Aspx_CatalogAffectedPriceDelete]", paramList);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CheckCatalogRuleExist(int iPortalID, int iStoreID)
        {
            try
            {
                bool isExist = false;
                SQLHandler sqlHandler = new SQLHandler();
                List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
                paramList.Add(new KeyValuePair<string, object>("@PortalID", iPortalID));
                paramList.Add(new KeyValuePair<string, object>("@StoreID", iPortalID));
                isExist = sqlHandler.ExecuteNonQueryAsBool("[dbo].[usp_Aspx_CheckCatalogRuleExist]", paramList, "@IsExist");
                return isExist;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static void TruncateCatalogAffectedPrice(int iPortalID)
        {            
            try
            {
                List<KeyValuePair<string, object>> parameterCollection = new List<KeyValuePair<string, object>>();
                parameterCollection.Add(new KeyValuePair<string, object>("@PortalID", iPortalID));
                SQLHandler sqLH = new SQLHandler();
                sqLH.ExecuteNonQuery("[dbo].[usp_Aspx_CatalogTruncateAffectedPrice]", parameterCollection);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string ConnectionString
        {
            get { return SystemSetting.SageFrameConnectionString; }
        }

        public static void AnalyseCatalogPrice()
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Parameters.Add(new SqlParameter("@StoreID", CurrentPortalID));
            sqlCommand.Parameters.Add(new SqlParameter("@PortalID", CurrentPortalID));
            sqlCommand.Parameters.Add(new SqlParameter("@offset", CurrentOffset));
            sqlCommand.Parameters.Add(new SqlParameter("@limit", CurrentLimit));
            sqlCommand.Parameters.Add(new SqlParameter("@CultureName", CurrentCultureName));
            sqlCommand.CommandTimeout = 0;
            sqlCommand.CommandText = "usp_Aspx_ApplyCatalogPricingRuleForSchedular";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            try
            {
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
