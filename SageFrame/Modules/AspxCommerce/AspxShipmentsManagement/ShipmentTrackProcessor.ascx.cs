using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspxCommerce.Core;
using SageFrame.Web;

public partial class Modules_AspxCommerce_AspxShipmentsManagement_ShipmentTrackProcessor : BaseAdministrationUserControl
{
    protected void Page_Init(object sender, EventArgs e)
    {

        BindProvider();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadControl();
    }

    private void BindProvider()
    {

        List<ShippingProvider> data = LoadProvider();
        rblProviderList.DataSource = data;
        rblProviderList.DataTextField = "ShippingProviderName";
        rblProviderList.DataValueField = "ShippingProviderID";
        rblProviderList.DataBind();
        rblProviderList.ClearSelection();
              


    }

    private List<ShippingProvider> LoadProvider()
    {
        TrackController ctl = new TrackController();
        AspxCommonInfo commonInfo = new AspxCommonInfo();
        commonInfo.StoreID = GetStoreID;
        commonInfo.PortalID = GetPortalID;
        var data = ctl.GetProviderList(commonInfo);
        return data;
    }

    private bool _loaded = false;
    private string GetTrackControlPath(int providerId)
    {

        List<ShippingProvider> data = LoadProvider();
        string trackPath = data.First(x => x.ShippingProviderID == providerId).TrackControlPath;
        return trackPath;

    }


    protected void rblProviderList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblProviderList.SelectedIndex != -1)
        {
            int providerId = int.Parse(rblProviderList.SelectedValue);
            Session["_trk_providerId"] = providerId;
            string path = GetTrackControlPath(providerId);
            if (!String.IsNullOrEmpty(path))
            {
                if (!_loaded)
                    LoadTrackControl(path);
            }
            else
            {
                if (Session["_trk_path"] != null)
                    Session.Remove("_trk_path");

                phTrackPackageHolder.Controls.Clear();
                dvTrackCustom.Visible = true;

            }
        }
    }
    private void ClearCustomForm()
    {
        txtTrackingNo.Text = "";
        dvTrackResponse.InnerHtml = "";
    }

    private void LoadControl()
    {
        if (IsPostBack)
        {
            if (Session["_trk_providerId"] != null)
            {
                string id = Session["_trk_providerId"].ToString();

                                             
                               if (id == rblProviderList.SelectedValue)
                {
                    if (Session["_trk_path"] != null)
                    {
                        string path = Session["_trk_path"].ToString();
                        string controlPath = ResolveUrl(@"~/" + path);
                        BaseAdministrationUserControl uc = (BaseAdministrationUserControl)Page.LoadControl(controlPath);
                        dvTrackCustom.Visible = false;
                        phTrackPackageHolder.Controls.Clear();
                        phTrackPackageHolder.Controls.Add(uc);
                        _loaded = true;
                    }
                    else
                    {
                        dvTrackCustom.Visible = true;
                    }
                }
            }
        }
        else
        {
            if (Session["_trk_providerId"] != null)
            {
                rblProviderList.SelectedValue = Session["_trk_providerId"].ToString();
                if (Session["_trk_path"] != null)
                {
                    string path = Session["_trk_path"].ToString();
                    string controlPath = ResolveUrl(@"~/" + path);
                    BaseAdministrationUserControl uc = (BaseAdministrationUserControl)Page.LoadControl(controlPath);
                    dvTrackCustom.Visible = false;
                    phTrackPackageHolder.Controls.Clear();
                    phTrackPackageHolder.Controls.Add(uc);
                }
                else
                {
                    rblProviderList_SelectedIndexChanged(this, EventArgs.Empty);
                }
            }
        }

    }
    private void LoadTrackControl(string path)
    {
        Session["_trk_path"] = path;
        string controlPath = ResolveUrl(@"~/" + path);
        BaseAdministrationUserControl uc = (BaseAdministrationUserControl)Page.LoadControl(controlPath);
        dvTrackCustom.Visible = false;
        ClearCustomForm();
        phTrackPackageHolder.Controls.Clear();
        phTrackPackageHolder.Controls.Add(uc);
    }

    protected void btnTrack_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
                TrackPackage();
        }
        catch (Exception ex)
        {

            ProcessException(ex);
        }

    }

    private void TrackPackage()
    {
               dvTrackResponse.InnerHtml = "<h2>Result </h2><div><p>No Update available. </p></div>";
    }
}
