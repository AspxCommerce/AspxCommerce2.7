using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Core.Services;
using System.Xml;
using System.Web;
using AspxCommerce.Core;

namespace AspxCommerce.CompareItem
{
    public class CompareItemsIModuleExtraCodeExecute : IModuleExtraCodeExecute
    {
        public void ExecuteOnInstallation(XmlDocument doc, string tempFolderPath)
        {
            ModuleSinglePageInfo mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxCompareItems";
            mpi.FriendlyName = "AspxCompareItemsDetails";
            mpi.PageName = "Compare Item List";
            mpi.PageTitle = "Compare Item List";
            mpi.Description = "Compre Items Module Description";

            PageControlInfo pci = new PageControlInfo();
            List<PageControlInfo> pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxCompareItems/ItemCompareDetails.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxCompareItems/ItemsCompareSetting.ascx";
            pci.ControlType = "Setting";
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
            mpi.FolderName = "AspxCommerce/AspxCompareItems";
            mpi.FriendlyName = "AspxCompareItemsDetails";
            mpi.PageName = "Compare Item List";
            mpi.PageTitle = "Compare Item List";
            mpi.Description = "Compare Items Module Description";

            PageControlInfo pci = new PageControlInfo();
            List<PageControlInfo> pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxCompareItems/ItemCompareDetails.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxCompareItems/ItemsCompareSetting.ascx";
            pci.ControlType = "Setting";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;

            CreateModulePackage cmp = new CreateModulePackage();
            cmp.DeleteSinglePageModulePackage(mpi);
        }
    }
}
