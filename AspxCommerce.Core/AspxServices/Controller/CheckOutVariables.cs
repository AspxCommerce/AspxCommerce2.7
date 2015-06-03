using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for CheckOutVariables
/// </summary>
public class CheckOutVariables
{
    public CheckOutVariables()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public decimal DiscountAll
    {
        get
        {
            if (HttpContext.Current.Session["DiscountAll"] != null)
            {
                return decimal.Parse(HttpContext.Current.Session["DiscountAll"].ToString());
            }
            else
            {
                return 0;
            }
        }

        set
        {
            if (value > 0)
                HttpContext.Current.Session["DiscountAll"] = value;
        }
    }

    public decimal ShippingCostAll
    {
        get
        {
            if (HttpContext.Current.Session["ShippingCostAll"] != null)
            {
                return decimal.Parse(HttpContext.Current.Session["ShippingCostAll"].ToString());
            }
            else
            {
                return 0;
            }
        }

        set
        {
            if (value > 0)
                HttpContext.Current.Session["ShippingCostAll"] = value;
        }
    }
    public string ShippingMethodName
    {
        get
        {
            if (HttpContext.Current.Session["ShippingMethodName"] != null)
            {
                return HttpContext.Current.Session["ShippingMethodName"].ToString();
            }
            else
            {
                return "";
            }
        }

        set
        {
            if (value != "")
                HttpContext.Current.Session["ShippingMethodName"] = value;
        }
    }
    public decimal GrandTotalAll
    {
        get
        {
            if (HttpContext.Current.Session["GrandTotalAll"] != null)
            {
                return decimal.Parse(HttpContext.Current.Session["GrandTotalAll"].ToString());
            }
            else
            {
                return 0;
            }
        }

        set
        {
            if (value > 0)
                HttpContext.Current.Session["GrandTotalAll"] = value;
        }
    }
    public decimal TaxAll
    {
        get
        {
            if (HttpContext.Current.Session["TaxAll"] != null)
            {
                return decimal.Parse(HttpContext.Current.Session["TaxAll"].ToString());
            }
            else
            {
                return 0;
            }
        }

        set
        {
            if (value > 0)
                HttpContext.Current.Session["TaxAll"] = value;
        }
    }

    public int Gateway
    {
        get
        {
            if (HttpContext.Current.Session["Gateway"] != null)
            {
                return int.Parse(HttpContext.Current.Session["Gateway"].ToString());
            }
            else
            {
                return 0;
            }
        }

        set
        {
            if (value > 0)
                HttpContext.Current.Session["Gateway"] = value;
        }
    }

}


public class SessionType {

    public string Key { get; set; }
    public bool IsAdditional { get; set; }
    public object Value { get; set; }
}

public class CheckOutSessions
{
    
    static List<SessionType> Sessions = new List<SessionType>();
    static Dictionary<string, object> AdditionalSessions = new Dictionary<string, object>();

    public static void Add(string key, object value, bool isAdditional)
    {
        if (isAdditional)
        {
            Sessions = new List<SessionType>();
            if (HttpContext.Current.Session["CheckOutSessions"] != null)
            {
                Sessions = (List<SessionType>)HttpContext.Current.Session["CheckOutSessions"];
            }
            else
            {
                HttpContext.Current.Session["CheckOutSessions"] = Sessions;
            }

            if (!Sessions.Exists(e => e.Key == key))
            {
                Sessions.Add(new SessionType() { Key = key, Value = value, IsAdditional = true });
            }
            else
            {
                Sessions.SingleOrDefault(e => e.Key == key).Value = value;
            }
            HttpContext.Current.Session["CheckOutSessions"] = Sessions;

        }
        else
        {
            Add(key, value);
        }

    }

    public static void Add(string key, object value)
    {
        Sessions = new List<SessionType>();
        if (HttpContext.Current.Session["CheckOutSessions"] != null)
        {
            Sessions = (List<SessionType>)HttpContext.Current.Session["CheckOutSessions"];
        }
        else
        {
            HttpContext.Current.Session["CheckOutSessions"] = Sessions;
        }

        if (!Sessions.Exists(e=>e.Key==key))
        {
            Sessions.Add(new SessionType() { Key = key, Value = value, IsAdditional = false });
        }
        else
        {
            Sessions.SingleOrDefault(e => e.Key == key).Value = value;    
        }
        HttpContext.Current.Session["CheckOutSessions"] = Sessions;
    }

    public static List<SessionType> GetAdditional() {

        List<SessionType> additionals = new List<SessionType>();
        if (HttpContext.Current.Session["CheckOutSessions"] != null)
        {
            Sessions = (List<SessionType>)HttpContext.Current.Session["CheckOutSessions"];

            additionals = Sessions.Where(e => e.IsAdditional == true).ToList();
        }

        return additionals;
    }

    public static T Get<T>(string key, object defaultValue) where T : struct, IConvertible
    {
        T retType = default(T);
        if (HttpContext.Current.Session["CheckOutSessions"] != null)
        {
            Sessions = (List<SessionType>)HttpContext.Current.Session["CheckOutSessions"];
        }
        else
        {
            return (T)Convert.ChangeType(0, retType.GetTypeCode()); ;

        }
        object obj = null;
        if (Sessions.Count == 0)
        {
            if (HttpContext.Current.Session["CheckOutSessions"] != null)
            {
                Sessions = (List<SessionType>)HttpContext.Current.Session["CheckOutSessions"];
            }
        }
    
        if (Sessions.Exists(e => e.Key == key))
        {
            obj = Sessions.SingleOrDefault(e => e.Key == key).Value;
        }
        else
        {

            obj = defaultValue;

        }

        return (T)Convert.ChangeType(obj, retType.GetTypeCode());
    }


    public static string Get(string key, object defaultValue)
    {       
        if (HttpContext.Current.Session["CheckOutSessions"] != null)
        {
            Sessions = (List<SessionType>)HttpContext.Current.Session["CheckOutSessions"];
        }
        else
        {
            return defaultValue.ToString();

        }

        object obj = null;
        if (Sessions.Count == 0)
        {
            if (HttpContext.Current.Session["CheckOutSessions"] != null)
            {
                Sessions = (List<SessionType>)HttpContext.Current.Session["CheckOutSessions"];
            }
        }

        if (Sessions.Exists(e => e.Key == key))
        {
            obj = Sessions.SingleOrDefault(e => e.Key == key).Value;
        }
        else
        {

            obj = defaultValue;

        }

        return obj.ToString();
    }

    public static T Get<T>(string key)
    {
        object obj = null;
        if (HttpContext.Current.Session["CheckOutSessions"] != null)
        {
            Sessions = (List<SessionType>)HttpContext.Current.Session["CheckOutSessions"];
        }
        else
        {
            obj = Activator.CreateInstance<T>();
            return (T)obj;
        }


        if (Sessions.Count == 0)
        {
            if (HttpContext.Current.Session["CheckOutSessions"] != null)
            {
                Sessions = (List<SessionType>)HttpContext.Current.Session["CheckOutSessions"];
            }
        }

        if (Sessions.Exists(e => e.Key == key))
        {
            obj = Sessions.SingleOrDefault(e => e.Key == key).Value;
        }
        else
        {

            obj = Activator.CreateInstance<T>();

        }

        return (T)obj;
    }

    public static void Delete(string key)
    {
        if (HttpContext.Current.Session["CheckOutSessions"] != null)
        {
            Sessions = (List<SessionType>)HttpContext.Current.Session["CheckOutSessions"];
        }
        else
        {
            return;

        }

        if (Sessions.Count == 0)
        {
            if (HttpContext.Current.Session["CheckOutSessions"] != null)
            {
                Sessions = (List<SessionType>)HttpContext.Current.Session["CheckOutSessions"];
            }
        }
        if (Sessions.Exists(e => e.Key == key))
        {
            Sessions.Remove(Sessions.SingleOrDefault(e => e.Key == key));
        }
       
        HttpContext.Current.Session["CheckOutSessions"] = Sessions;
    }

    public void Clear()
    {

        Sessions = new List<SessionType>();
        HttpContext.Current.Session.Remove("CheckOutSessions");
    }


}

public class CheckOutHelper
{

    public CheckOutHelper()
    {

    }

    public void ClearSessions()
    {

        if (HttpContext.Current.Session["CheckOutSessions"] != null)
        {
            CheckOutSessions obj = new CheckOutSessions();
            obj.Clear();
        }


    }

}