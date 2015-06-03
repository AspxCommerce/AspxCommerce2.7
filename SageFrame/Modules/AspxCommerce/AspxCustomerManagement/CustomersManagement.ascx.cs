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
using System.Linq;
using System.Web;
using System.Web.UI;
using AspxCommerce.Core;
using SageFrame.Web;
using SageFrame.Security;
using SageFrame.Message;
using SageFrame.Security.Entities;
using System.Web.Security;
using SageFrame.Security.Helpers;
using SageFrame.Security.Providers;
using SageFrame.NewLetterSubscriber;
using SageFrame.NewsLetter;

public partial class Modules_AspxCommerce_AspxCustomerManagement_CustomersManagement : BaseAdministrationUserControl
{
	public int StoreID, PortalID;
    public string userName, CultureName, UserModuleId;
	public string headerTemplate = string.Empty;
	private Random random = new Random();
	SageFrameConfig pagebase = new SageFrameConfig();
	MembershipController _member = new MembershipController();
	public int CheckIfSucccess = 1;
	public string NewCustomerRss, RssFeedUrl;
  
	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			StoreID = GetStoreID;
			PortalID = GetPortalID;
			userName = GetUsername;
            UserModuleId = SageUserModuleID;
			CultureName = GetCurrentCultureName;
			IncludeCss("CustomersManagement", "/Templates/" + TemplateName + "/css/GridView/tablesort.css", "/Templates/" + TemplateName + "/css/MessageBox/style.css");
			  
			if (!IsPostBack)
			{
				Session["customerRefresh"] = Server.UrlEncode(System.DateTime.Now.ToString());  
			   IncludeJs("CustomersManagement", "/js/GridView/jquery.grid.js", "/js/GridView/SagePaging.js", "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js",
							"/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js", "/Modules/AspxCommerce/AspxCustomerManagement/js/CustomerManage.js");

				MessageManagementController msgController = new MessageManagementController();
				List<MessageManagementInfo> template = msgController.GetMessageTemplateTypeTokenListByMessageTemplateType(SystemSetting.USER_REGISTRATION_HELP, GetPortalID);
				foreach (MessageManagementInfo messageToken in template)
				{
					if (template != null)
					{
						headerTemplate = "<div>" + messageToken.Body + "</div>";
					}

					//if (_member.EnableCaptcha)
					//{
					//    InitializeCaptcha();
					//}
					//else { HideCaptcha(); }
					
				}

                SetValidatorErrorMessage();

				var ssc = new StoreSettingConfig();
				NewCustomerRss = ssc.GetStoreSettingsByKey(StoreSetting.NewCustomerRss, StoreID, PortalID, CultureName);
				if(NewCustomerRss.ToLower()=="true")
				{
				   RssFeedUrl = ssc.GetStoreSettingsByKey(StoreSetting.RssFeedURL, StoreID, PortalID, CultureName);
				}
			}
			IncludeLanguageJS();
		}
		catch (Exception ex)
		{
			ProcessException(ex);
		}
	}

	protected void Page_Init(object sender, EventArgs e)
	{
		ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalTemplateFolderPath", " var aspxTemplateFolderPath='" + ResolveUrl("~/") + "Templates/" + TemplateName + "';", true);
		try
		{
			ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalServicePath", " var aspxservicePath='" + ResolveUrl("~/") + "Modules/AspxCommerce/AspxCommerceServices/" + "';", true);
			ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalTemplateFolderPath", " var aspxTemplateFolderPath='" + ResolveUrl("~/") + "Templates/" + TemplateName + "';", true);
			InitializeJS();        
		}
		catch (Exception ex)
		{
			ProcessException(ex);
		}
	}

	private void InitializeJS()
	{ 
		Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));    
	}
	
	//private void InitializeCaptcha()
	//{
	//    CaptchaValue.Text = "";
	//    this.Session["CaptchaImageText"] = GenerateRandomCode();
	//    cvCaptchaValue.ValueToCompare = this.Session["CaptchaImageText"].ToString();
	//    CaptchaImage.ImageUrl = "~/CaptchaImageHandler.aspx";
	//    Refresh.ImageUrl = GetTemplateImageUrl("imgrefresh.png", true);
	//    CaptchaImage.Visible = true;
	//    CaptchaLabel.Visible = true;
	//    CaptchaValue.Visible = true;
	//    captchaValidator.Visible = false;
	//    Refresh.Visible = true;
	//    DataLabel.Visible = true;
	//    rfvCaptchaValueValidator.Enabled = true;
	//}
	
	//private void HideCaptcha()
	//{
	//    CaptchaImage.Visible = false;
	//    CaptchaLabel.Visible = false;
	//    CaptchaValue.Visible = false;
	//    Refresh.Visible = false;
	//    DataLabel.Visible = false;
	//    rfvCaptchaValueValidator.Enabled = false;
	//    captchaValidator.Visible = false;
	//}

	private void SetValidatorErrorMessage()
	{
		rfvUserNameRequired.Text = "*";
		rfvPasswordRequired.Text = "*";
		rfvConfirmPasswordRequired.Text = "*";
		rfvFirstName.Text = "*";
		rfvLastName.Text = "*";
		rfvEmailRequired.Text = "*";
		rfvQuestionRequired.Text = "*";
		rfvAnswerRequired.Text = "*";
	 		cvPasswordCompare.Text = "Password mismatched!";
	        revEmail.Text = "Please enter a valid Email";
		rfvUserNameRequired.ErrorMessage = GetSageMessage("UserRegistration", "UserNameIsRequired");
		rfvPasswordRequired.ErrorMessage = GetSageMessage("UserRegistration", "PasswordIsRequired");
		rfvConfirmPasswordRequired.ErrorMessage = GetSageMessage("UserRegistration", "PasswordConfirmIsRequired");
		rfvFirstName.ErrorMessage = GetSageMessage("UserRegistration", "FirstNameIsRequired");
		rfvLastName.ErrorMessage = GetSageMessage("UserRegistration", "LastNameIsRequired");
		rfvEmailRequired.ErrorMessage = GetSageMessage("UserRegistration", "EmailAddressIsRequired");
		rfvQuestionRequired.ErrorMessage = GetSageMessage("UserRegistration", "SecurityQuestionIsRequired");
		rfvAnswerRequired.ErrorMessage = GetSageMessage("UserRegistration", "SecurityAnswerIsRequired");
	//    rfvCaptchaValueValidator.ErrorMessage = GetSageMessage("UserRegistration", "EnterTheCorrectCapchaCode");
		cvPasswordCompare.ErrorMessage = GetSageMessage("UserRegistration", "PasswordRetypeMatch");
	 		revEmail.ErrorMessage = "Please enter a valid Email";
	}

	//protected void Refresh_Click(object sender, EventArgs e)
	//{
	//   refresh = true;
	//    GenerateCaptchaImage();                 
	//   	//   	//   	//   	//   	//   	//   	//   	//   	//   	//   	//}

	//private void GenerateCaptchaImage()
	//{
	//    try
	//    {
	//        this.Session["CaptchaImageText"] = GenerateRandomCode();
	//        cvCaptchaValue.ValueToCompare = this.Session["CaptchaImageText"].ToString();
	//        CaptchaImage.ImageUrl = ResolveUrl("~") +"CaptchaImageHandler.aspx?=dummy" + DateTime.Now.ToLongTimeString();
	//        CaptchaValue.Text = "";
	//    }
	//    catch (Exception ex)
	//    {
	//        ProcessException(ex);
	//    }
	//}

	private string GenerateRandomCode()
	{
		string s = "";
		string[] CapchaValue = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
		for (int i = 0; i < 6; i++)
			s = String.Concat(s, CapchaValue[this.random.Next(36)]);
		return s;
	}


	protected void FinishButton_Click(object sender, EventArgs e)
	{
		if (Session["customerRefresh"] != null)
		{

			if (Session["customerRefresh"].ToString() == ViewState["customerRefresh"].ToString())
				// If page not Refreshed
			{
				RegisterUser();
				Session["customerRefresh"] = Server.UrlEncode(System.DateTime.Now.ToString());
			}
			else
			{
				//nothing
			}
		}

		//this.Session["CaptchaImageText"] = null;
		//if (_member.EnableCaptcha)
		//{
		//    if (ValidateCaptcha())
		//    {
		//        RegisterUser();
		//    }
		//}
		//else
		//{
		//    RegisterUser();
		//}
	 	}

	//private bool ValidateCaptcha()
	//{
	//    if (!(cvCaptchaValue.ValueToCompare == CaptchaValue.Text))
	//    {
	//        ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserRegistration", "EnterTheCorrectCapchaCode"), "", SageMessageType.Error);
	//        return false;
	//    }

	//    return true;
	//}

	private void RegisterUser()
	{
		try
		{
			if (string.IsNullOrEmpty(UserName.Text) || string.IsNullOrEmpty(FirstName.Text) || string.IsNullOrEmpty(LastName.Text) || string.IsNullOrEmpty(Email.Text))
			{
				ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserRegistration", "PleaseEnterAllRequiredFields"), "", SageMessageType.Alert);
				CheckIfSucccess = 0;
			}
			else
			{
				int UserRegistrationType = pagebase.GetSettingIntByKey(SageFrameSettingKeys.PortalUserRegistration);

				bool isUserActive = UserRegistrationType == 2 ? true : false;

				UserInfo objUser = new UserInfo();
				objUser.ApplicationName = Membership.ApplicationName;
				objUser.FirstName = FirstName.Text;
				objUser.UserName = UserName.Text;
				objUser.LastName = LastName.Text;
				string Pwd, PasswordSalt;
				PasswordHelper.EnforcePasswordSecurity(_member.PasswordFormat, Password.Text, out Pwd, out PasswordSalt);
				objUser.Password = Pwd;
				objUser.PasswordSalt = PasswordSalt;
				objUser.Email = Email.Text;
				objUser.SecurityQuestion = Question.Text;
				objUser.SecurityAnswer = Answer.Text;
				objUser.IsApproved = true;
				objUser.CurrentTimeUtc = DateTime.Now;
				objUser.CreatedDate = DateTime.Now;
				objUser.UniqueEmail = 0;
				objUser.PasswordFormat = _member.PasswordFormat;
				objUser.PortalID = GetPortalID;
				objUser.AddedOn = DateTime.Now;
				objUser.AddedBy = GetUsername;
				objUser.UserID = Guid.NewGuid();
				objUser.RoleNames = SystemSetting.REGISTER_USER_ROLENAME;
				objUser.StoreID = GetStoreID;
				objUser.CustomerID = 0;

				UserCreationStatus status = new UserCreationStatus();
				CheckRegistrationType(UserRegistrationType, ref objUser);

				MembershipDataProvider.CreatePortalUser(objUser, out status, UserCreationMode.REGISTER);
				if (status == UserCreationStatus.DUPLICATE_USER)
				{
					ShowMessage(SageMessageTitle.Notification.ToString(), UserName.Text.Trim() + " " + GetSageMessage("UserManagement", "NameAlreadyExists"), "", SageMessageType.Alert);
					CheckIfSucccess = 0;

				}
				else if (status == UserCreationStatus.DUPLICATE_EMAIL)
				{
					ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "EmailAddressAlreadyIsInUse"), "", SageMessageType.Alert);
					CheckIfSucccess = 0;

				}
				else if (status == UserCreationStatus.SUCCESS)
				{
					if (chkIsSubscribeNewsLetter.Checked)
					{
						int? newID = 0;
						ManageNewsLetterSubscription(Email.Text, ref newID);
					}
					ShowMessage(SageMessageTitle.Notification.ToString(), GetSageMessage("UserManagement", "UserCreatedSuccessfully"), "", SageMessageType.Success);
					CheckIfSucccess = 1;
				 					ClearFormValue();            
				}
			}
		}

		catch (Exception ex)
		{
			ProcessException(ex);
		}
	}

	private void ManageNewsLetterSubscription(string email, ref int? newID)
	{
		string clientIP = Request.UserHostAddress.ToString();
        int userModuleID = 0;
        userModuleID = Int32.Parse(SageUserModuleID);
		//NewLetterSubscriberController.AddNewLetterSubscribers(email, clientIP, true, GetUsername, DateTime.Now, GetPortalID);
        NL_Controller nlc = new NL_Controller();
        nlc.SaveEmailSubscriber(email, userModuleID, GetPortalID, GetUsername, clientIP);
	}

	private void CheckRegistrationType(int UserRegistrationType, ref UserInfo user)
	{
		switch (UserRegistrationType)
		{
			case 0:
				break;
			case 1:
				user.IsApproved = false;
				break;
			case 2:
				user.IsApproved = true;
				break;
			case 3:
				user.IsApproved = false;
				break;
		}
	}

	private void LogInPublicModeRegistration()
	{
		string strRoles = string.Empty;
		MembershipController member = new MembershipController();
		RoleController role = new RoleController();
		UserInfo user = member.GetUserDetails(GetPortalID, UserName.Text);

		if (!(string.IsNullOrEmpty(UserName.Text) && string.IsNullOrEmpty(Password.Text)))
		{
			if (PasswordHelper.ValidateUser(user.PasswordFormat, Password.Text, user.Password, user.PasswordSalt))
			{
				string userRoles = role.GetRoleNames(user.UserName, GetPortalID);
				strRoles += userRoles;
				if (strRoles.Length > 0)
				{
					SetUserRoles(strRoles);
                    //SessionTracker sessionTracker = (SessionTracker)Session[SessionKeys.Tracker];
                    //sessionTracker.PortalID = GetPortalID.ToString();
                    //sessionTracker.Username = UserName.Text;
                    //Session[SessionKeys.Tracker] = sessionTracker;
                    SageFrame.Web.SessionLog SLog = new SageFrame.Web.SessionLog();
                    SLog.SessionTrackerUpdateUsername(UserName.Text, GetPortalID.ToString());
					{
						FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
								 user.UserName,
								 DateTime.Now,
								 DateTime.Now.AddMinutes(30),
								 true,
								 GetPortalID.ToString(),
								 FormsAuthentication.FormsCookiePath);

						// Encrypt the ticket.
						string encTicket = FormsAuthentication.Encrypt(ticket);

						// Create the cookie.
						Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
						bool IsUseFriendlyUrls = pagebase.GetSettingBollByKey(SageFrameSettingKeys.UseFriendlyUrls);
						if (IsUseFriendlyUrls)
						{
                            if (!IsParent)
							{
                                Response.Redirect(ResolveUrl(GetParentURL + "/portal/" + GetPortalSEOName + "/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + SageFrameSettingKeys.PageExtension), false);
							}
							else
							{
								Response.Redirect(ResolveUrl("~/" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage) + SageFrameSettingKeys.PageExtension), false);
							}
						}
						else
						{
							Response.Redirect(ResolveUrl("~/Default"+SageFrameSettingKeys.PageExtension+"?ptlid=" + GetPortalID + "&ptSEO=" + GetPortalSEOName + "&pgnm=" + pagebase.GetSettingsByKey(SageFrameSettingKeys.PortalDefaultPage)), false);
						}

					}
				}

			}

		}
	}

	public void SetUserRoles(string strRoles)
	{
		Session["SageUserRoles"] = strRoles;
		HttpCookie cookie = HttpContext.Current.Request.Cookies["SageUserRolesCookie"];
		if (cookie == null)
		{
			cookie = new HttpCookie("SageUserRolesCookie");
		}
		cookie["SageUserRolesProtected"] = strRoles;
		HttpContext.Current.Response.Cookies.Add(cookie);
	}

	public void ClearFormValue()
	{
		FirstName.Text = string.Empty;
		LastName.Text = string.Empty;
		Email.Text = string.Empty;
		UserName.Text = string.Empty;
		Password.Text = string.Empty;
		ConfirmPassword.Text = string.Empty;
		Question.Text = string.Empty;
		Answer.Text = string.Empty;
	  	}

	protected override void OnPreRender(EventArgs e)
	{
		ViewState["customerRefresh"] = Session["customerRefresh"];
	}
}
