using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Core.Services;
using AspxCommerce.Core;
using System.Xml;

namespace AspxCommerce.SpecialItems
{
    public class SpecialItemsIModuleExtraCodeExecute : IModuleExtraCodeExecute
    {
        public void ExecuteOnInstallation(XmlDocument doc, string tempFolderPath)
        {
            ModuleSinglePageInfo mpi = new ModuleSinglePageInfo();
            List<ModuleSinglePageInfo> multiplePageInfo = new List<ModuleSinglePageInfo>();

            mpi.FolderName = "AspxCommerce/AspxSpecialsItems";
            mpi.FriendlyName = "AspxSpecialItemsDetail";
            mpi.PageName = "SpecialDetail";
            mpi.PageTitle = "SpecialDetail";
            mpi.Description = "Display All Special Items";
            PageControlInfo pci = new PageControlInfo();
            List<PageControlInfo> pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxSpecialsItems/SpecialItemsViewAll.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.50.00";
            mpi.SupportPartialRendering = false;

            multiplePageInfo.Add(mpi);


            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxSpecialsItems";
            mpi.FriendlyName = "SpecialItemsRssFeed";
            mpi.PageName = "SpecialRss";
            mpi.PageTitle = "SpecialRss";
            mpi.Description = "Heavy Discount Rss Feed View";
            pci = new PageControlInfo();
            pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxSpecialsItems/SpecialItemsRss.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.50.00";
            mpi.SupportPartialRendering = false;

            multiplePageInfo.Add(mpi);
            CreateModulePackage cmp = new CreateModulePackage();

            cmp.CreateMultiplePagesModulePackage(multiplePageInfo);// for multiple page

        }

        public void ExecuteOnUnInstallation(XmlDocument doc)
        {
            ModuleSinglePageInfo mpi = new ModuleSinglePageInfo();
            List<ModuleSinglePageInfo> multiplePageInfo = new List<ModuleSinglePageInfo>();

            mpi.FolderName = "AspxCommerce/AspxSpecialsItems";
            mpi.FriendlyName = "AspxSpecialItemsDetail";
            mpi.PageName = "SpecialDetail";
            mpi.PageTitle = "SpecialDetail";
            mpi.Description = "Display All Special Items";
            PageControlInfo pci = new PageControlInfo();
            List<PageControlInfo> pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxSpecialsItems/SpecialItemsViewAll.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.50.00";
            mpi.SupportPartialRendering = false;

            multiplePageInfo.Add(mpi);


            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxSpecialsItems";
            mpi.FriendlyName = "SpecialItemsRssFeed";
            mpi.PageName = "SpecialRssFeed";
            mpi.PageTitle = "SpecialRssFeed";
            mpi.Description = "Heavy Discount Rss Feed View";
            pci = new PageControlInfo();
            pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxSpecialsItems/SpecialItemsRss.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.50.00";
            mpi.SupportPartialRendering = false;

            multiplePageInfo.Add(mpi);
            CreateModulePackage cmp = new CreateModulePackage();
            cmp.DeleteMultiplePageModulePackage(multiplePageInfo);// for multiple Page module package
        }
    }
}
