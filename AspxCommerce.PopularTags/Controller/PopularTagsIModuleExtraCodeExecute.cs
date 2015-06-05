using System.Collections.Generic;
using AspxCommerce.Core;
using SageFrame.Core.Services;
using System.Xml;

namespace AspxCommerce.PopularTags
{
    public class PopularTagsIModuleExtraCodeExecute : IModuleExtraCodeExecute
    {
        public void ExecuteOnInstallation(XmlDocument doc, string tempFolderPath)
        {           
            ModuleSinglePageInfo mpi = new ModuleSinglePageInfo();
            List<ModuleSinglePageInfo> multiplePageInfo = new List<ModuleSinglePageInfo>();

            mpi.FolderName = "AspxCommerce/AspxPopularTags";
            mpi.FriendlyName = "PopularTagsViewAll";
            mpi.PageName = "AllTags";
            mpi.PageTitle = "AllTags";
            mpi.Description = "Display All Popular Tags";
            PageControlInfo pci = new PageControlInfo();
            List<PageControlInfo> pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxPopularTags/ViewAllTags.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;

            multiplePageInfo.Add(mpi);

            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxPopularTags";
            mpi.FriendlyName = "PopularTagsTaggedItem";
            mpi.PageName = "TaggedItem";
            mpi.PageTitle = "TaggedItem";
            mpi.Description = "Display All Tagged Items";
            pci = new PageControlInfo();
            pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxPopularTags/ViewTaggedItems.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;

            multiplePageInfo.Add(mpi);


            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxPopularTags";
            mpi.FriendlyName = "PopularTagsRssView";
            mpi.PageName = "TagsRssFeed";
            mpi.PageTitle = "TagsRssFeed";
            mpi.Description = "Popular Tags Rss Feed View";
            pci = new PageControlInfo();
            pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxPopularTags/PopularTagsRss.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "";
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

            mpi.FolderName = "AspxCommerce/AspxPopularTags";
            mpi.FriendlyName = "PopularTagsViewAll";
            mpi.PageName = "AllTags";
            mpi.PageTitle = "AllTags";
            mpi.Description = "Display All Popular Tags";
            PageControlInfo pci = new PageControlInfo();
            List<PageControlInfo> pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxPopularTags/ViewAllTags.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;

            multiplePageInfo.Add(mpi);

            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxPopularTags";
            mpi.FriendlyName = "PopularTagsTaggedItem";
            mpi.PageName = "TaggedItem";
            mpi.PageTitle = "TaggedItem";
            mpi.Description = "Display All Tagged Items";
            pci = new PageControlInfo();
            pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxPopularTags/ViewTaggedItems.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "http://www.aspxcommerce.com/default.aspx";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;

            multiplePageInfo.Add(mpi);


            mpi = new ModuleSinglePageInfo();
            mpi.FolderName = "AspxCommerce/AspxPopularTags";
            mpi.FriendlyName = "PopularTagsRssView";
            mpi.PageName = "TagsRssFeed";
            mpi.PageTitle = "TagsRssFeed";
            mpi.Description = "Popular Tags Rss Feed View";
            pci = new PageControlInfo();
            pciList = new List<PageControlInfo>();
            pci.ControlSource = "Modules/AspxCommerce/AspxPopularTags/PopularTagsRss.ascx";
            pci.ControlType = "View";
            pciList.Add(pci);
            mpi.PageControls = pciList;
            mpi.HelpURL = "";
            mpi.Version = "02.05.00";
            mpi.SupportPartialRendering = false;

            multiplePageInfo.Add(mpi);
            CreateModulePackage cmp = new CreateModulePackage();
            cmp.DeleteMultiplePageModulePackage(multiplePageInfo);// for multiple Page module package
        }
    }
}
