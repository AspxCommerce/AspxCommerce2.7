#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SageFrame.Web;
using SageFrame.Framework;
using System.Collections;
#endregion 

namespace SageFrame.Modules.Admin.CSSManagement
{
    public partial class ctl_ManageCss : BaseAdministrationUserControl
    {
        SageFrameConfig pb = new SageFrameConfig();
        private static string path = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    AddImageUrls();
                    path = Server.MapPath("~/Templates/" + pb.GetSettingsByKey(SageFrameSettingKeys.PortalCssTemplate) + "/css");
                    BindCSSFile();
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }
        private void BindCSSFile()
        {
           try
           {
               string[] files = Directory.GetFiles(path);
               ArrayList arrColl = new ArrayList();
               if (files.Length > 0)
               {
                   foreach (string str in files)
                   {
                       string tempstr = str.Remove(0, str.LastIndexOf("\\") + 1);
                       if (tempstr.Length > 4)
                       {
                           if (tempstr.Substring(tempstr.Length - 4).ToLower() == ".css")
                           {
                               arrColl.Add(tempstr);
                           }
                       }
                   }
               }
               ddlCssFileList.DataSource = arrColl;
               ddlCssFileList.DataBind();
               ddlCssFileList.Items.Insert(0, new ListItem("--Select--", "-1"));
           }
           catch (Exception ex)
           {
               ProcessException(ex);
           } 

        }
        private void AddImageUrls()
        {
            imbSave.ImageUrl = GetTemplateImageUrl("imgsave.png", true);
            imbRefresh.ImageUrl = GetTemplateImageUrl("imgrefresh.png", true);
        }
        protected void imbSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                SaveCssContent(path + "\\" + ddlCssFileList.SelectedValue);
                ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("CSSManagement", "CSSIsSavedSuccessfully"), "", SageMessageType.Success);
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void imbRefresh_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                LoadCSSContent(path + "\\" + ddlCssFileList.SelectedValue);
            }
            catch(Exception ex)
            {
                ProcessException(ex);
            }
        }

        protected void ddlCssFileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCssFileList.SelectedValue != "-1" && ddlCssFileList.SelectedIndex != -1)
            {
                LoadCSSContent(path + "\\" + ddlCssFileList.SelectedValue);
            }
        }

        private void LoadCSSContent(string filePath)
        {
            FileStream file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(file);
            txtFileContent.Text = sr.ReadToEnd();
            sr.Close();
            file.Close();

        }

        private void SaveCssContent(string filePath)
        {
            File.Delete(filePath);
            FileStream file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(file);
            sw.Write(txtFileContent.Text);
            sw.Close();
            file.Close();
        }
    }
}