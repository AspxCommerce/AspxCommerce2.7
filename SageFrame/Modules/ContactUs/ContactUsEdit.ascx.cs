/*
SageFrame® - http://www.sageframe.com
Copyright (c) 2009-2010 by SageFrame
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
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.ContactUs;

namespace SageFrame.Modules.ContactUs
{
    public partial class ContactUsEdit : BaseAdministrationUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadContactList();
        }

        protected void gdvContacters_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvContacters.PageIndex = e.NewPageIndex;
            LoadContactList();
        }

        protected void gdvContacters_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gdvContacters_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gdvContacters_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gdvContacters_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int contactUsID = int.Parse(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                case "Delete":
                    DeleteContact(contactUsID);
                    break;
            }
        }

        private void DeleteContact(int contactUsID)
        {
            ContactUsController contactController = new ContactUsController();
            contactController.ContactUsDeleteByID(contactUsID, GetPortalID, GetUsername);            
            ShowMessage(SageMessageTitle.Information.ToString(), SageMessage.GetSageModuleLocalMessageByVertualPath("Modules/ContactUs/ModuleLocalText", "ContactUsIsDeletedSuccessfully"), "", SageMessageType.Success);
            LoadContactList();
        }

        private void LoadContactList()
        {
            ContactUsController contactController = new ContactUsController();
            List<ContactUsInfo> objContactList = contactController.ContactUsGetAll(GetPortalID);
            gdvContacters.DataSource = objContactList;
            gdvContacters.DataBind();
        }

        protected void gdvContacters_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void gdvContacters_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
    }
}