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
using SageFrame.SageFrameClass;
using SageFrame.Web;

namespace SageFrame.Modules.Admin.Extensions.Editors
{
    public partial class PackageDetails : BaseAdministrationUserControl
    {
        private string packageName = string.Empty;        
        private string description = string.Empty;
        private string license=string.Empty;
        private string releaseNotes=string.Empty;
        private string owner=string.Empty;
        private string organization=string.Empty;
        private string url = string.Empty;
        private string email = string.Empty;
        private string firstVersion = "01";
        private string secondVersion = "00";
        private string lastVersion = "00";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {                    
                    txtPackageName.Text = packageName;
                    txtDescription.Text = description;
                    txtLicense.Text = license;
                    txtReleaseNotes.Text = releaseNotes;
                    txtOwner.Text = owner;
                    txtOrganization.Text = organization;
                    txtUrl.Text = url;
                    txtEmail.Text = email;
                    BindVersionDropdownlist();
                    ddlFirst.SelectedIndex = ddlFirst.Items.IndexOf(ddlFirst.Items.FindByValue(firstVersion));
                    ddlSecond.SelectedIndex = ddlSecond.Items.IndexOf(ddlSecond.Items.FindByValue(secondVersion));
                    ddlLast.SelectedIndex = ddlLast.Items.IndexOf(ddlLast.Items.FindByValue(lastVersion));
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void BindVersionDropdownlist()
        {
            try
            {
                ddlFirst.DataSource = SageFrameLists.VersionType();
                ddlFirst.DataBind();               

                ddlSecond.DataSource = SageFrameLists.VersionType();
                ddlSecond.DataBind();

                ddlLast.DataSource = SageFrameLists.VersionType();
                ddlLast.DataBind();
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }   

        #region Properties of Package

        public string PackageName
        {
            get
            {
                return txtPackageName.Text;
            }
            set
            {
                this.packageName = value;
            }
        }

        public string Description
        {
            get
            {
                return txtDescription.Text;
            }
            set
            {
                this.description = value;
            }
        }

        public string License
        {
            get
            {
                return txtLicense.Text;
            }
            set
            {
                this.license = value;
            }
        }

        public string ReleaseNotes
        {
            get
            {
                return txtReleaseNotes.Text;
            }
            set
            {
                this.releaseNotes = value;
            }
        }

        public string Owner
        {
            get
            {
                return txtOwner.Text;
            }
            set
            {
                this.owner = value;
            }
        }

        public string Organization
        {
            get
            {
                return txtOrganization.Text;
            }
            set
            {
                this.organization = value;
            }
        }

        public string Url
        {
            get
            {
                return txtUrl.Text;
            }
            set
            {
                this.url = value;
            }
        }

        public string Email
        {
            get
            {
                return txtEmail.Text;
            }
            set
            {
                this.email = value;
            }
        }

        public string FirstVersion
        {
            get
            {
                return ddlFirst.SelectedValue;
            }
            set
            {
                this.firstVersion = value;
            }
        }

        public string SecondVersion
        {
            get
            {
                return ddlSecond.SelectedValue;
            }
            set
            {
                this.secondVersion = value;
            }
        }

        public string LastVersion
        {
            get
            {
                return ddlLast.SelectedValue;
            }
            set
            {
                this.lastVersion = value;
            }
        }

        #endregion 
    }
}