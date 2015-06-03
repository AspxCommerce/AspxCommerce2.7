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
using System.Data;
using SageFrame.Localization;
using System.Xml;
using System.IO;
using SageFrame.Web;
#endregion

public partial class Modules_Language_TimeZoneEditor : BaseAdministrationUserControl
{
    public event EventHandler CancelButtonClick;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindAvailableLocales();
            BindGrid(Localization.SystemLocale);
            GetFlagImage();
        }
    }
    protected void BindGrid(string language)
    {
        DataSet ds = new DataSet();
        bool isExists = File.Exists(Server.MapPath(ResourceFile(Localization.TimezonesFile, language, true)));
        if (isExists)
        {
            ds.ReadXml(Server.MapPath(ResourceFile(Localization.TimezonesFile, language, isExists)));
            gdvTimeZoneEditor.DataSource = ds;
            gdvTimeZoneEditor.DataBind();
        }
        else
        {
            ds.ReadXml(Server.MapPath(Localization.TimezonesFile));
            gdvTimeZoneEditor.DataSource=ds;
            gdvTimeZoneEditor.DataBind();
        }
    }
    protected void BindAvailableLocales()
    {
        this.ddlAvailableLocales.DataSource = LocalizationSqlDataProvider.GetAvailableLocales();
        this.ddlAvailableLocales.DataTextField = "LanguageName";
        this.ddlAvailableLocales.DataValueField = "LanguageCode";
        this.ddlAvailableLocales.DataBind();
    }
    private string ResourceFile(string filename, string language, bool isExists)
    {
        return (isExists ? filename.Substring(0, filename.Length - 4) + "." + language + ".xml" : filename);
    }
    protected void imbCancel_Click(object sender, EventArgs e) { CancelButtonClick(sender, e); }
    protected void ddlAvailableLocales_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid(this.ddlAvailableLocales.SelectedValue.ToString());
        GetFlagImage();
    }
    protected void imbUpdate_Click(object sender, EventArgs e)
    {
        string filename = Localization.TimezonesFile;
        string language = this.ddlAvailableLocales.SelectedValue;
        string destinationFileName = Server.MapPath(filename.Substring(0, filename.Length - 4) + "." + language + ".xml");
        XmlNode node;
        XmlDocument resDoc = new XmlDocument();
        bool isExist = File.Exists(destinationFileName);
        try
        {
            resDoc.Load(Server.MapPath(ResourceFile(Localization.TimezonesFile, this.ddlAvailableLocales.SelectedValue, isExist)));
            foreach (GridViewRow row in gdvTimeZoneEditor.Rows)
            {
                TextBox ctl = (TextBox)row.Cells[0].Controls[1];
                node = resDoc.SelectSingleNode("//root/timezone[@key='" + row.Cells[1].Text + "']");
                node.Attributes["name"].Value = ctl.Text;
                node.Attributes["default"].Value = row.Cells[2].Text;
            }
            switch (isExist)
            {
                case true:
                    UpdateTimeZoneFile(resDoc);
                    break;
                default:
                    CreateNewTimeZoneFile(resDoc, destinationFileName);
                    break;
            }
            ShowMessage(SageMessageTitle.Information.ToString(), "TimeZone file is saved successfully!", "", SageMessageType.Success);
            BindGrid(this.ddlAvailableLocales.SelectedValue);
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    protected void UpdateTimeZoneFile(XmlDocument resDoc)
    {
        File.SetAttributes(Server.MapPath(ResourceFile(Localization.TimezonesFile, this.ddlAvailableLocales.SelectedValue, true)), FileAttributes.Normal);
        resDoc.Save(Server.MapPath(ResourceFile(Localization.TimezonesFile, this.ddlAvailableLocales.SelectedValue, true)));
    }
    protected void CreateNewTimeZoneFile(XmlDocument resDoc, string fileName)
    {
        File.Copy(Server.MapPath(Localization.TimezonesFile), fileName);
        File.SetAttributes(fileName, FileAttributes.Normal);
        resDoc.Save(fileName);
    }
    protected void GetFlagImage()
    {
        string code = this.ddlAvailableLocales.SelectedValue;
        imgFlag.ImageUrl = ResolveUrl("~/images/flags/" + code.Substring(code.IndexOf("-") + 1) + ".png");
    }
}
