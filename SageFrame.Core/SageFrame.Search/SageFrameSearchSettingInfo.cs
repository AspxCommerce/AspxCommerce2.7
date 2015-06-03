#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion


namespace SageFrame.Search
{
    /// <summary>
    /// Enum values for search button.
    /// </summary>
    public enum SageFrameSearchButtonType
    {
        Button,
        Image,
        Link
    }

    /// <summary>
    /// Entity class for search settings.
    /// </summary>
    public class SageFrameSearchSettingInfo
    {
        private int _SearchButtonType = 0;/* 0 For Button 1 For Image 2 For Link*/
        private string _SearchButtonText = "Search";
        private string _SearchButtonImage = "imbSageSearch.png";
        private int _SearchResultPerPage = 10;
        private string _SearchResultPageName = "SageSearchResult";
        private int _MaxSearchChracterAllowedWithSpace = 200;
        private int _MaxResultCharacterAllowedWithSpace = 200;

        /// <summary>
        /// Initializes an instance of  SageFrameSearchSettingInfo class.
        /// </summary>
        public SageFrameSearchSettingInfo()
        {

        }
        /// <summary>
        /// Initializes an instance of  SageFrameSearchSettingInfo class.
        /// </summary>
        /// <param name="searchButtonType">Search button type.</param>
        /// <param name="searchButtonText">Search button text.</param>
        /// <param name="searchButtonImage">Search button image.</param>
        /// <param name="searchResultPerPage">Search result per page.</param>
        /// <param name="searchResultPageName">Search result page name.</param>
        /// <param name="maxSearchChracterAllowedWithSpace">Maximum search character allowed with space.</param>
        /// <param name="maxResultCharacterAllowedWithSpace">Maximum result character allowed with space.</param>
        public SageFrameSearchSettingInfo(int searchButtonType, string searchButtonText, string searchButtonImage, int searchResultPerPage, string searchResultPageName, int maxSearchChracterAllowedWithSpace, int maxResultCharacterAllowedWithSpace)
        {
            this.SearchButtonType = searchButtonType;
            this.SearchButtonText = searchButtonText;
            this.SearchButtonImage = searchButtonImage;
            this.SearchResultPerPage = searchResultPerPage;
            this.SearchResultPageName = searchResultPageName;
            this.MaxSearchChracterAllowedWithSpace = maxSearchChracterAllowedWithSpace;
            this.MaxResultChracterAllowedWithSpace = maxResultCharacterAllowedWithSpace;
        }

        /// <summary>
        /// Gets or sets search button type.
        /// </summary>
        public int SearchButtonType
        {
            get { return _SearchButtonType; }
            set { _SearchButtonType = value; }
        }


        /// <summary>
        /// Gets or sets search button text.
        /// </summary>
        public string SearchButtonText
        {
            get { return _SearchButtonText; }
            set { _SearchButtonText = value; }
        }


        /// <summary>
        /// Gets or sets search button image.
        /// </summary>
        public string SearchButtonImage
        {
            get { return _SearchButtonImage; }
            set { _SearchButtonImage = value; }
        }


        /// <summary>
        /// Gets or sets search result per page.
        /// </summary>
        public int SearchResultPerPage
        {
            get { return _SearchResultPerPage; }
            set { _SearchResultPerPage = value; }
        }


        /// <summary>
        /// Gets or sets search result page name.
        /// </summary>
        public string SearchResultPageName
        {
            get { return _SearchResultPageName; }
            set { _SearchResultPageName = value; }
        }


        /// <summary>
        /// Gets or sets maximium search character allowed with space.
        /// </summary>
        public int MaxSearchChracterAllowedWithSpace
        {
            get { return _MaxSearchChracterAllowedWithSpace; }
            set { _MaxSearchChracterAllowedWithSpace = value; }
        }


        /// <summary>
        /// Gets or sets maximum result character allowed with space.
        /// </summary>
        public int MaxResultChracterAllowedWithSpace
        {
            get { return _MaxResultCharacterAllowedWithSpace; }
            set { _MaxResultCharacterAllowedWithSpace = value; }
        }
    }
}
