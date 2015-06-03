<%@ WebService Language="C#" Class="ModuleNameWebservice" %>
using System;
using SageFrame.Services;
using System.Web.Services;
using System.Collections.Generic;
//references
/// <summary>
/// Summary description for ModuleNameWebservice
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class ModuleNameWebservice : AuthenticateService
{
    public ModuleNameWebservice()
    {

    }
//Methodshere
}