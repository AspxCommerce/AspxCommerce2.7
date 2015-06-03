using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;


namespace AspxCommerce.Core
{
    public static class CurrencyConverter
    {



        //public static double GetRate(string from, string to)
        //{
        //    CurrencyConvertor c = new CurrencyConvertor();
        //    Currency Currencyfrom = (Currency)Enum.Parse(typeof(Currency), from);
        //    Currency Currencyto = (Currency)Enum.Parse(typeof(Currency), to);
        //    System.Net.ServicePointManager.Expect100Continue = false;
        //    double rate = c.ConversionRate(Currencyfrom, Currencyto);
        //    return rate;
        //}


        public static double ConvertCurrency(string from, string to, double amount)
        {

            double rate = GetRate(from, to);
            double result = amount*rate;
            return result;

        }

        public static double GetRate(string from, string to)
        {
            //Help http://currencies.apps.grandtrunk.net/
            double rate = 0.0;
           

                try
                {
                    System.Net.ServicePointManager.Expect100Continue = false;
                    string test = string.Empty;
                    test = "http://currencies.apps.grandtrunk.net/getlatest/" + from.Trim() + "/" + to.Trim();
                    var req = (HttpWebRequest)WebRequest.Create(test);
                    //Set values for the request back
                    req.Method = "GET";
                    var respond = (HttpWebResponse)req.GetResponse();
                    var streamIn1 = new StreamReader(respond.GetResponseStream());
                    string strResponse = streamIn1.ReadToEnd();

                    if (!strResponse.Trim().StartsWith("Invalid currency "))
                        rate = double.Parse(strResponse);

                }
                catch (Exception)
                {
                    rate= GetRateFromGoogle(from, to);
                }
            
            return rate;
        }

        public static double GetRateFromGoogle(string fromCurrency, string toCurrency)
        {
            double rate = 0.00;
            try
            {
                var web = new WebClient();
                string url = string.Format("http://www.google.com/ig/calculator?hl=en&q={2}{0}%3D%3F{1}",
                                           fromCurrency.ToUpper(), toCurrency.ToUpper(), 1.00);

                string response = web.DownloadString(url);
                //response = response.Replace("\"", "");
                //"{lhs: \"1 U.S. dollar\",rhs: \"1.02609988 Canadian dollars\",error: \"\",icc: true}"
                //  Regex regex = new Regex("rhs:"(d*.d*)");
                Regex regex = new Regex("rhs: \\\"(\\d*.\\d*)");
                //  Regex regex = new Regex(@"rhs: \""(\d*.\d*\.\d)");
                Match match = regex.Match(response);
                string strrate = (match.Groups[1].Value.Trim());
                strrate = Regex.Replace(strrate, @"\s", "");
                rate = Convert.ToDouble(strrate);
                // System.Convert.ToDouble(match.Groups[1].Value);
            }
            catch (Exception)
            {
                rate= GetRateFromYahoo(fromCurrency, toCurrency);
            }
            return rate;
        }

        


        public static double GetRateFromYahoo(string from, string to)
        {
            double rate = 0.0;
            try
            {

                var web = new WebClient();
                string url =
                    string.Format("http://download.finance.yahoo.com/d/quotes.csv?e=.csv&f=sl1d1t1&s=" +
                                  from.Trim().ToUpper() + to.Trim().ToUpper() + "=X");
                string response = web.DownloadString(url);
                if (response != "")
                {
                    string[] data = response.Split(',');
                    rate = System.Convert.ToDouble(data[1]);
                }
                //data[1] =todays rate
                //data[1]=/date of exchange rate/
                //data[2]=/time of exchange rate /
            }
            catch (Exception ex)
            {
                //error converting 
                throw ex;

            }
            return rate;
        }


    }

    public class GoogleCurrencyRate
    {
        public string lhs { get; set; }
        public string rhs { get; set; }
        public string error { get; set; }
        public string icc { get; set; }
    }
}
