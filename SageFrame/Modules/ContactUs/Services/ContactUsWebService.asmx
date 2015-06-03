<%@ WebService Language="C#" Class="ContactUsWebService" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SageFrame.ContactUs;

/// <summary>
/// Summary description for ContactUsWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ContactUsWebService : System.Web.Services.WebService
{

    public ContactUsWebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void ContactUsAddAndSendEmail(string name, string email,string subject, string message, bool isActive, int portalID, string addedBy)
    {
        try
        {
            ContactUsController contactController = new ContactUsController();
            contactController.ContactUsAdd(name, email,subject, message, isActive, portalID, addedBy);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}

