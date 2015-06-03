using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using AspxCommerce.Core;
using SageFrame.Core.Services;

namespace AspxCommerce.WishItem
{
    public class WishItemsIModuleExtraCodeExecute : IModuleExtraCodeExecute
    {

        public void ExecuteOnInstallation(XmlDocument doc, string tempFolderPath)
        {
            RegisterAPIjs(doc, tempFolderPath);
            ModuleSinglePageInfo mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxWishList";
            mpi.FriendlyName = "AspxWishItems";
            mpi.PageName = "My WishList";
            mpi.PageTitle = "MyWishList";
            mpi.Description = "Wish Items Module Description";

            PageControlInfo pci = new PageControlInfo();
            List<PageControlInfo> pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxWishList/WishItemList.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxWishList/WishItemsSetting.ascx";
            pci.ControlType = "Setting";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;

            CreateModulePackage cmp = new CreateModulePackage();
            cmp.CreateSinglePagesModulePackage(mpi);
        }

        private void RegisterAPIjs(XmlDocument doc, string tempFolderPath)
        {
            RegisterAPIjs rAPIjs = new RegisterAPIjs();
            rAPIjs.AddAPIjsOnInsatllation(doc, tempFolderPath);
        }

        public void ExecuteOnUnInstallation(XmlDocument doc)
        {
            DeleteRegisteredAPIjs(doc);
            ModuleSinglePageInfo mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxWishList";
            mpi.FriendlyName = "AspxWishItems";
            mpi.PageName = "My WishList";
            mpi.PageTitle = "MyWishList";
            mpi.Description = "Wish Items Module Description";

            PageControlInfo pci = new PageControlInfo();
            List<PageControlInfo> pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxWishList/WishItemList.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxWishList/WishItemsSetting.ascx";
            pci.ControlType = "Setting";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;

            CreateModulePackage cmp = new CreateModulePackage();
            cmp.DeleteSinglePageModulePackage(mpi);
        }

        private void DeleteRegisteredAPIjs(XmlDocument doc)
        {
            RegisterAPIjs rAPIjs = new RegisterAPIjs();
            rAPIjs.RemoveAPIjsOnUnstallation(doc);
        }
    }
}
