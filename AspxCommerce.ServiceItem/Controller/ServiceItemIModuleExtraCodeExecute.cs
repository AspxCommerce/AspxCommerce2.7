using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Core.Services;
using AspxCommerce.Core;
using System.Xml;

namespace AspxCommerce.ServiceItem
{
    public class ServiceItemIModuleExtraCodeExecute : IModuleExtraCodeExecute
    {
        public void ExecuteOnInstallation(XmlDocument doc, string tempFolderPath)
        {
            ModuleSinglePageInfo mpi = new ModuleSinglePageInfo();

            List<ModuleSinglePageInfo> multiplePageInfo = new List<ModuleSinglePageInfo>();

            mpi.FolderName = "AspxCommerce/AspxServiceItems";
            mpi.FriendlyName = "AspxServiceViewAll";
            mpi.PageName = "Services";
            mpi.PageTitle = "Services";
            mpi.Description = "View All Services";
            List<PageControlInfo> pciList = new List<PageControlInfo>();
            PageControlInfo pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxServiceItems/ServicesAll.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;
           
            multiplePageInfo.Add(mpi);

            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxServiceItems";
            mpi.FriendlyName = "AspxServiceItemDetails";
            mpi.PageName = "Service Item Details";
            mpi.PageTitle = "Service Item Details";
            mpi.Description = "Display Service Item Details";
            pciList = new List<PageControlInfo>();
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxServiceItems/ServiceItemDetails.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;         
            multiplePageInfo.Add(mpi);

            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxServiceItems";
            mpi.FriendlyName = "AspxBookAnAppointment";
            mpi.PageName = "Book An Appointment";
            mpi.PageTitle = "Book An Appointment";
            mpi.Description = "Book An Appointment";
            pciList = new List<PageControlInfo>();
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxServiceItems/BookAppointment.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;
            multiplePageInfo.Add(mpi);

            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxServiceItems";
            mpi.FriendlyName = "AspxAppointmentSuccess";
            mpi.PageName = "Appointment Success";
            mpi.PageTitle = "Appointment Success";
            mpi.Description = "Appointment Success";
            pciList = new List<PageControlInfo>();
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxServiceItems/AppointmentSuccess.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;
            multiplePageInfo.Add(mpi);

            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxServiceItems";
            mpi.FriendlyName = "AspxServiceItemRss";
            mpi.PageName = "Service Rss";
            mpi.PageTitle = "Service Rss";
            mpi.Description = "Service Rss";
            pciList = new List<PageControlInfo>();
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxServiceItems/ServiceItemRss.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;
            multiplePageInfo.Add(mpi);

            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxServiceItems";
            mpi.FriendlyName = "AspxServiceDetails";
            mpi.PageName = "Services Detail";
            mpi.PageTitle = "Service Details";
            mpi.Description = "Service Details";
            pciList = new List<PageControlInfo>();
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxServiceItems/ServicesDetails.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;
            multiplePageInfo.Add(mpi);
            CreateModulePackage cmp = new CreateModulePackage();           

            cmp.CreateMultiplePagesModulePackage(multiplePageInfo);// for multiple page

        }

        public void ExecuteOnUnInstallation(XmlDocument doc)
        { 
            ModuleSinglePageInfo mpi = new ModuleSinglePageInfo();

            List<ModuleSinglePageInfo> multiplePageInfo = new List<ModuleSinglePageInfo>();

            mpi.FolderName = "AspxCommerce/AspxServiceItems";
            mpi.FriendlyName = "AspxServiceViewAll";
            mpi.PageName = "Services";
            mpi.PageTitle = "Services";
            mpi.Description = "View All Services";
            List<PageControlInfo> pciList = new List<PageControlInfo>();
            PageControlInfo pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxServiceItems/ServicesAll.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;

            multiplePageInfo.Add(mpi);

            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxServiceItems";
            mpi.FriendlyName = "AspxServiceItemDetails";
            mpi.PageName = "Service Item Details";
            mpi.PageTitle = "Service Item Details";
            mpi.Description = "Display Service Item Details";
            pciList = new List<PageControlInfo>();
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxServiceItems/ServiceItemDetails.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;
            multiplePageInfo.Add(mpi);

            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxServiceItems";
            mpi.FriendlyName = "AspxBookAnAppointment";
            mpi.PageName = "Book An Appointment";
            mpi.PageTitle = "Book An Appointment";
            mpi.Description = "Book An Appointment";
            pciList = new List<PageControlInfo>();
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxServiceItems/BookAppointment.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;

            multiplePageInfo.Add(mpi);

            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxServiceItems";
            mpi.FriendlyName = "AspxAppointmentSuccess";
            mpi.PageName = "Appointment Success";
            mpi.PageTitle = "Appointment Success";
            mpi.Description = "Appointment Success";
            pciList = new List<PageControlInfo>();
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxServiceItems/AppointmentSuccess.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;
            multiplePageInfo.Add(mpi);

            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxServiceItems";
            mpi.FriendlyName = "AspxServiceItemRss";
            mpi.PageName = "Service Rss";
            mpi.PageTitle = "Service Rss";
            mpi.Description = "Service Rss";
            pciList = new List<PageControlInfo>();
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxServiceItems/ServiceItemRss.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;
            multiplePageInfo.Add(mpi);

            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxServiceItems";
            mpi.FriendlyName = "AspxServiceDetails";
            mpi.PageName = "Service Details";
            mpi.PageTitle = "Service Details";
            mpi.Description = "Service Details";
            pciList = new List<PageControlInfo>();
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxServiceItems/ServicesDetails.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;
            multiplePageInfo.Add(mpi);
            CreateModulePackage cmp = new CreateModulePackage();   
            cmp.DeleteMultiplePageModulePackage(multiplePageInfo);// for multiple Page module package

        }
    }
}
