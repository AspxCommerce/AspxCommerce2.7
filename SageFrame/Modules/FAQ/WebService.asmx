<%@ WebService Language="C#" Class="WebService" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SageFrame.FAQ.Controller;
using SageFrame.FAQ.Info;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<FAQInfo> GetFAQList(int PortalID, int UserModuleID, string CultureName,int Offset,int limit)
    {
        try
        {
            FAQController clt = new FAQController();
            List<FAQInfo> list = new List<FAQInfo>();
            list = clt.GetFAQList(PortalID,UserModuleID,CultureName,Offset,limit);
            return list;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [WebMethod]
    public List<FAQInfo> GetSearchList(int PortalID, int UserModuleID, string CultureName,string SearchWord)
    {
        try
        {
            FAQController clt = new FAQController();
            List<FAQInfo> list = new List<FAQInfo>();
            list = clt.GetSearchList(PortalID,UserModuleID,CultureName,SearchWord);
            return list;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    
            [WebMethod]
    public List<FAQInfo> GetFaqCategory(int PortalID, int UserModuleID, string CultureName)
    {
        try
        {
            FAQController clt = new FAQController();
            List<FAQInfo> list = new List<FAQInfo>();
            list = clt.GetFaqCategory(PortalID, UserModuleID,CultureName);
            return list;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    
    [WebMethod]
    public List<FAQInfo> GetGraphDetails(int FAQId)
    {
        try
        {
            FAQController clt = new FAQController();
            List<FAQInfo> list = new List<FAQInfo>();
            list = clt.GetGraphDetails(FAQId);
            return list;
        }
        catch (Exception e)
        {
            throw e;
        }
    }


    [WebMethod]
    public List<FAQInfo> GetFaqUserReview(int FAQId)
    {
        try
        {
            FAQController clt = new FAQController();
            List<FAQInfo> list = new List<FAQInfo>();
            list = clt.GetFaqUserReview(FAQId);
            return list;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    
    [WebMethod]
    public void SubmitQuestion(int PortalID, int UserModuleID, string UserName, string EmailAddress, string Question, string CultureName)
    {
        try
        {
            FAQController clt = new FAQController();
            clt.SubmitQuestion(PortalID, UserModuleID, UserName, EmailAddress, Question,CultureName);          
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    [WebMethod]
    public void SubmitFAQViewOption(int PortalID, int UserModuleID, int FAQId, int OptionId, string CultureName)
    {
        try
        {
            FAQController clt = new FAQController();
            clt.SubmitFAQViewOption(PortalID, UserModuleID, FAQId, OptionId,CultureName);

        }
        catch (Exception e)
        {
            throw e;
        }
    }


    [WebMethod]
    public void SubmitReason(int FaqID, string Review, string userEmail,string CultureName)
    {
        try
        {
            FAQController clt = new FAQController();
            clt.SubmitReason(FaqID, Review, userEmail,CultureName);

        }
        catch (Exception e)
        {
            throw e;
        }
    }
    
        
    [WebMethod]
    public void DeleteReview(int ReviewID)
    {
        try
        {
            FAQController clt = new FAQController();
            clt.DeleteReview(ReviewID);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    
}


