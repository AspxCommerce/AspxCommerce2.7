using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Core.Services;
using AspxCommerce.Core;
using System.Xml;

namespace AspxCommerce.BrandView
{
    public class BrandViewIModuleExtraCodeExecute: IModuleExtraCodeExecute
    {
        public void ExecuteOnInstallation(XmlDocument doc, string tempFolderPath)
        {         

            ModuleSinglePageInfo mpi = new ModuleSinglePageInfo();

            List<ModuleSinglePageInfo> multiplePageInfo = new List<ModuleSinglePageInfo>();

            mpi.FolderName = "AspxCommerce/AspxBrandView";
            mpi.FriendlyName = "AspxBrandViewAll";
            mpi.PageName = "Brands";
            mpi.PageTitle = "Brands";
            mpi.Description = "Display All Brand Page";
            List<PageControlInfo> pciList = new List<PageControlInfo>();
            PageControlInfo pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxBrandView/BrandView.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;  
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;
            multiplePageInfo.Add(mpi);


            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxBrandView";
            mpi.FriendlyName = "AspxBrandRssView";
            mpi.PageName = "Brand Rss";
            mpi.PageTitle = "Brand Rss";
            mpi.Description = "Brand Rss View";
            pciList = new List<PageControlInfo>();
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxBrandView/BrandRss.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;

            multiplePageInfo.Add(mpi);
            CreateModulePackage cmp = new CreateModulePackage();  
            cmp.CreateMultiplePagesModulePackage(multiplePageInfo);

        }

        public void ExecuteOnUnInstallation(XmlDocument doc)
        {          

            ModuleSinglePageInfo mpi = new ModuleSinglePageInfo();

            List<ModuleSinglePageInfo> multiplePageInfo = new List<ModuleSinglePageInfo>();

            mpi.FolderName = "AspxCommerce/AspxBrandView";
            mpi.FriendlyName = "AspxBrandViewAll";
            mpi.PageName = "Brands";
            mpi.PageTitle = "Brands";
            mpi.Description = "Display All Brand Page";
            List<PageControlInfo> pciList = new List<PageControlInfo>();
            PageControlInfo pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxBrandView/BrandView.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;
            multiplePageInfo.Add(mpi);


            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxBrandView";
            mpi.FriendlyName = "AspxBrandRssView";
            mpi.PageName = "Brand Rss";
            mpi.PageTitle = "Brand Rss";
            mpi.Description = "Brand Rss View";
            pciList = new List<PageControlInfo>();
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxBrandView/BrandRss.ascx";
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
