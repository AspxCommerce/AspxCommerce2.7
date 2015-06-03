using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace AspxCommerce.Core
{
    public class AspxCurrencyController
    {
        public AspxCurrencyController()
        {
        }
        public static List<CurrencyInfo> BindCurrencyList(AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CurrencyInfo> lstCurrency = AspxCurrencyProvider.BindCurrencyList(aspxCommonObj);
                return lstCurrency;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CurrencyInfo> BindCurrencyAddedLists(int offset, int limit, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CurrencyInfo> lstCurrency = AspxCurrencyProvider.BindCurrencyAddedLists(offset, limit, aspxCommonObj);
                return lstCurrency;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<CurrencyInfoByCode> GetDetailsByCountryCode(string countryCode, string countryName, AspxCommonInfo aspxCommonObj)
        {
            try
            {
                List<CurrencyInfoByCode> lstCountryDetails = AspxCurrencyProvider.GetDetailsByCountryCode(countryCode, countryName, aspxCommonObj);
                return lstCountryDetails;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void InsertNewCurrency(AspxCommonInfo aspxCommonObj, CurrencyInfo currencyInsertObj)
        {
            try
            {
                AspxCurrencyProvider.InsertNewCurrency(aspxCommonObj, currencyInsertObj);
                string CurrencyValue = currencyInsertObj.Region + ',' + currencyInsertObj.CurrencySymbol + ',' + currencyInsertObj.CurrencyCode;
                CurruncyJsWrite(CurrencyValue);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static void CurruncyJsWrite(string hdnCurrencyCode)
        {
            try
            {
                string StrRootPath= HostingEnvironment.MapPath("~/").ToString();
                string destinationDir = StrRootPath + @"Upload\temp\";
                //currency jquery.currencies.js
                string filename1 = StrRootPath + @"Upload\temp\jquery.currencies.js";
                //currency jquery.formatCurrency.all.js
                string filename2 = StrRootPath + @"Upload\temp\jquery.formatCurrency.all.js";
                //check if file1 exists
                if (File.Exists(filename1))
                {
                    File.Delete(filename1);
                }
                using (FileStream fs = new FileStream(filename1, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                {
                    fs.Close();
                }

                //check if file2 exists
                if (File.Exists(filename2))
                {
                    File.Delete(filename2);                    
                }
                using (FileStream fs = new FileStream(filename2, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                {
                    fs.Close();
                }

                //garbage collection flush
                GC.Collect();
                string[] curValues = hdnCurrencyCode.Split(',');
                string CurrencySymbol = curValues[0];
                string ReplaceCurSymbolWith = curValues[1];
                string CurrencyCode = curValues[2];

                ReplaceTextInFile1(System.Web.HttpContext.Current.Server.MapPath("~/js/CurrencyFormat/jquery.currencies.js"), System.Web.HttpContext.Current.Server.MapPath("~/Upload/temp/jquery.currencies.js"), CurrencyCode, ReplaceCurSymbolWith);
                ReplaceTextInFile2(System.Web.HttpContext.Current.Server.MapPath("~/js/CurrencyFormat/jquery.formatCurrency.all.js"), System.Web.HttpContext.Current.Server.MapPath("~/Upload/temp/jquery.formatCurrency.all.js"), CurrencySymbol, ReplaceCurSymbolWith);

                //delete js file1 and file2 from actual js folder
                File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/js/CurrencyFormat/jquery.formatCurrency.all.js").ToString());
                File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/js/CurrencyFormat/jquery.currencies.js").ToString());

                //copy new files to the location
                File.Copy(filename2, System.Web.HttpContext.Current.Server.MapPath("~/js/CurrencyFormat/jquery.formatCurrency.all.js"));
                File.Copy(filename1, System.Web.HttpContext.Current.Server.MapPath("~/js/CurrencyFormat/jquery.currencies.js"));
                //garbage collection flush
                GC.Collect();
            }
            catch 
            {
            }
        }

        private static void ReplaceTextInFile1(string originalFile, string outputFile, string searchTerm, string replaceTerm)
        {
            // Read lines from source file.
            string[] arr = File.ReadAllLines(originalFile);
            using (FileStream fs = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        string line = arr[i];
                        if (line.Contains(searchTerm))
                        {
                            char findchar1 = '\"';
                            int index1 = Positions(line, findchar1).Skip(3 - 1).DefaultIfEmpty(-1).First();
                            int index2 = line.IndexOf("{");
                            int index3 = Positions(line, '}')
                                .Skip(2 - 1)
                                .DefaultIfEmpty(-1)
                                .First();
                            int index4 = Positions(line, findchar1)
                                .Skip(4 - 1)
                                .DefaultIfEmpty(-1)
                                .First();
                            string currencySymbolBeforeBracket = line.Substring(index1 + 1, index2 - index1 - 1);
                            string currencySymbolAfterBracket = line.Substring(index3 + 1, index4 - index3 - 1);
                            if (currencySymbolBeforeBracket != "")
                            {
                                if (!(currencySymbolBeforeBracket.Trim() == searchTerm.Trim()))
                                {
                                    //replace here
                                    line = line.Replace(currencySymbolBeforeBracket, replaceTerm);

                                }
                            }
                            else if (currencySymbolAfterBracket != "")
                            {
                                if (!(currencySymbolAfterBracket.Trim() == searchTerm.Trim()))
                                {
                                    //replace here
                                    line = line.Replace(currencySymbolAfterBracket, replaceTerm);
                                }
                            }

                        }
                        writer.WriteLine(line);
                        //close the writer
                        writer.Flush();
                    }
                }
            }
            //garbage collection flush
            GC.Collect();

        }

        public static IEnumerable<int> Positions(string str, char c)
        {
            for (int position = 0; position < str.Length; position++)
            {
                char x = str[position];
                if (x == c)
                {
                    yield return position;
                }
            }
        }
        private static void ReplaceTextInFile2(string originalFile, string outputFile, string searchTerm, string replaceTerm)
        {
            // Read lines from source file.
            string[] arr = File.ReadAllLines(originalFile);
            using (FileStream fs = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        string line = arr[i];
                        if (line.Contains(searchTerm))
                        {
                            writer.WriteLine(line);
                            string line2 = arr[i + 1];
                            string testString = line2;
                            if (testString.Contains("symbol"))
                            {
                                int initialLoc = testString.IndexOf("symbol");
                                string substring = testString.Substring(initialLoc, testString.IndexOf(",") - initialLoc);
                                line = testString;
                                line = line.Replace(substring, "symbol: '" + replaceTerm + "'");
                                i = i + 1;
                            }
                        }
                        writer.WriteLine(line);
                        //close the writer
                        writer.Flush();
                    }
                }
            }
            //garbage collection flush
            GC.Collect();

        }

        public static bool CheckUniquenessForDisplayOrderForCurrency(AspxCommonInfo aspxCommonObj, int value, int currencyID)
        {
            try
            {
                bool isUnique = AspxCurrencyProvider.CheckUniquenessForDisplayOrderForCurrency(aspxCommonObj, value, currencyID);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckCurrencyCodeUniqueness(AspxCommonInfo aspxCommonObj, string currencyCode, int currencyID)
        {
            try
            {
                bool isUnique = AspxCurrencyProvider.CheckCurrencyCodeUniqueness(aspxCommonObj, currencyCode, currencyID);
                return isUnique;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void UpdateRealTimeRate(AspxCommonInfo aspxCommonObj, string currencyCode, double rate)
        {
            try
            {
                AspxCurrencyProvider.UpdateRealTimeRate(aspxCommonObj,currencyCode, rate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
       public static double GetRatefromTable(AspxCommonInfo aspxCommonObj,  string currencyCode)
       {
           try
           {
               double rate = AspxCurrencyProvider.GetRatefromTable(aspxCommonObj, currencyCode);
               return rate;
           }
           catch (Exception e)
           {
               throw e;
           }
       }

       public static void SetStorePrimary(AspxCommonInfo aspxCommonObj, string currencyCode)
       {
           try
           {
               AspxCurrencyProvider.SetStorePrimary(aspxCommonObj, currencyCode);
               
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public static void DeleteMultipleCurrencyByCurrencyID(string currencyIDs, AspxCommonInfo aspxCommonObj)
       {
           try
           {
               AspxCurrencyProvider.DeleteMultipleCurrencyByCurrencyID(currencyIDs, aspxCommonObj);
           }
           catch (Exception e)
           {
               throw e;
           }
       }
       public static List<CurrrencyRateInfo> GetCountryCodeRates(AspxCommonInfo aspxCommonObj)
       {
           try
           {
               List <CurrrencyRateInfo> currencyList= AspxCurrencyProvider.GetCountryCodeRates(aspxCommonObj);
               return currencyList;
           }
           catch (Exception e)
           {
               throw e;
           }
       }
    }
}
