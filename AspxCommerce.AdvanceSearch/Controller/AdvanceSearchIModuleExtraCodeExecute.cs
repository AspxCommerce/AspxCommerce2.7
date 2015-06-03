using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Core;
using SageFrame.Core.Services;
using AspxCommerce.Core;
using System.Xml;

namespace AspxCommerce.AdvanceSearch
{
    class AdvanceSearchIModuleExtraCodeExecute : IModuleExtraCodeExecute
    {
        public void ExecuteOnInstallation(XmlDocument doc, string tempFolderPath)
        {
            RegisterAPIjs(doc, tempFolderPath);
            ModuleSinglePageInfo mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxAdvanceSearch";
            mpi.FriendlyName = "AspxAdvanceSearch";
            mpi.PageName = "AdvanceSearch";
            mpi.PageTitle = "AdvanceSearch";
            mpi.Description = "AdvanceSearch Module Description";

           PageControlInfo pci = new PageControlInfo();
           List<PageControlInfo> pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxAdvanceSearch/AdvanceSearch.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxAdvanceSearch/AdvanceSearchSetting.ascx";
            pci.ControlType = "Setting";
            pciList.Add(pci);
            mpi.PageControls = pciList;          
            mpi.HelpURL = "http://www.sageframe.com/default.aspx";
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
            mpi.FolderName = "AspxCommerce/AspxAdvanceSearch";
            mpi.FriendlyName = "AspxAdvanceSearch";
            mpi.PageName = "AdvanceSearch";
            mpi.PageTitle = "AdvanceSearch";
            mpi.Description = "AdvanceSearch Module Description";
            PageControlInfo pci = new PageControlInfo();
            List<PageControlInfo> pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxAdvanceSearch/AdvanceSearch.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            pci = new PageControlInfo();
            pci.ControlSource = "Modules/AspxCommerce/AspxAdvanceSearch/AdvanceSearchSetting.ascx";
            pci.ControlType = "Setting";
            pciList.Add(pci);
            mpi.PageControls = pciList;           
            mpi.HelpURL = "http://www.sageframe.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false; ;

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
