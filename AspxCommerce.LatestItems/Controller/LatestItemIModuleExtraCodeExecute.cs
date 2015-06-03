using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Core.Services;
using AspxCommerce.Core;
using System.Xml;

namespace AspxCommerce.LatestItems
{
    public class LatestItemIModuleExtraCodeExecute : IModuleExtraCodeExecute
    {
        public LatestItemIModuleExtraCodeExecute()
        {

        }

        public void ExecuteOnInstallation(XmlDocument doc, string tempFolderPath)
        {

            ModuleSinglePageInfo mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxLatestItems";
            mpi.FriendlyName = "AspxLatestItemsRss";
            mpi.PageName = "Latest Item  Rss";
            mpi.PageTitle = "Latest Item  Rss";
            mpi.Description = "Display Latest Item  Rss";
            PageControlInfo pci = new PageControlInfo();
            List<PageControlInfo> pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxLatestItems/LatestItemRss.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;
            CreateModulePackage cmp = new CreateModulePackage();
            cmp.CreateSinglePagesModulePackage(mpi);
        }

        public void ExecuteOnUnInstallation(XmlDocument doc)
        {
            ModuleSinglePageInfo mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxLatestItems";
            mpi.FriendlyName = "AspxLatestItemsRss";
            mpi.PageName = "Latest Item  Rss";
            mpi.PageTitle = "Latest Item  Rss";
            mpi.Description = "Display Latest Item  Rss";
            PageControlInfo pci = new PageControlInfo();
            List<PageControlInfo> pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxLatestItems/LatestItemRss.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;
            CreateModulePackage cmp = new CreateModulePackage();
            cmp.DeleteSinglePageModulePackage(mpi);// for single page module package          

        }
    }
}
