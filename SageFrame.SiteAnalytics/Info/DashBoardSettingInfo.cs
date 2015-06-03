using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DashBoardControl.Info
{   
    /// <summary>
    /// This class holds the properties for DashBoardSettingInfo.
    /// </summary>
    public class DashBoardSettingInfo
    {   
        /// <summary>
        /// Gets or Sets DashBoardSettingID(Enums)
        /// </summary>
        public int DashBoardSettingID { get; set; }
        /// <summary>
        /// Gets or sets SettingKey(Enums type)
        /// </summary>
        public string SettingKey { get; set; }
        /// <summary>
        /// Gets or sets SettingValue
        /// </summary>
        public string SettingValue { get; set; }
        /// <summary>
        /// Gets or sets PortalID
        /// </summary>
        public int PortalID { get; set; }
        /// <summary>
        /// Gets or sets UserModuleID
        /// </summary>
        public int UserModuleID { get; set; }
        /// <summary>
        /// Gets or sets IsActive
        /// </summary>
        public int IsActive { get; set; }
        /// <summary>
        /// Gets or sets AddedBy
        /// </summary>
        public string AddedBy { get; set; }
        /// <summary>
        /// Gets or sets page visit count
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Gets or sets rownum
        /// </summary>
        public int RowNum { get; set; }
        /// <summary>
        /// Gets or sets StartDate
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// Gets or sets enddate
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// Gets or sets selectiontype
        /// </summary>
        public string SelectionType { get; set; }
        /// <summary>
        /// Gets or sets country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Gets or sets page visit time
        /// </summary>
        public string VisitTime { get; set; }
        /// <summary>
        /// Gets or sets broswer
        /// </summary>
        public string Browser { get; set; }
        /// <summary>
        /// Gets or sets page visit
        /// </summary>
        public string VisitPage { get; set; }
        /// <summary>
        /// Gets or sets page visit date
        /// </summary>
        public string VisitedDate { get; set; }
        /// <summary>
        /// Gets or sets page visit count
        /// </summary>
        public string VisitCount { get; set; }

        /// <summary>
        /// Gets or sets referral page
        /// </summary>
        public string RefPage { get; set; }
        /// <summary>
        /// Gets or sets page visit without extension
        /// </summary>
        public string VistPageWithoutExtension
        {
            get
            {
                return Path.GetFileNameWithoutExtension(VisitPage);
            }
        }
        /// <summary>
        /// Gets or sets user ip
        /// </summary>
        public string SessionUserHostAddress { get; set; }
       
        /// <summary>
        /// Initializes a new instance of the DashBoardSettingInfo class.
        /// </summary>
        public DashBoardSettingInfo() { }
        /// <summary>
        /// Initializes a new instance of the DashBoardSettingInfo class.
        /// </summary>
        /// <param name="_SettingKey">SettingKey</param>
        /// <param name="_SettingValue">SettingValue</param>
        public DashBoardSettingInfo(string _SettingKey, string _SettingValue)
        {
            this.SettingKey = _SettingKey;
            this.SettingValue = _SettingValue;
        }
        /// <summary>
        /// Initializes a new instance of the DashBoardSettingInfo class.
        /// </summary>
        /// <param name="StartDate">StartDate</param>
        /// <param name="EndDate">EndDate</param>
        /// <param name="SelectionType">SelectionType</param>
        public DashBoardSettingInfo(string StartDate, string EndDate, string SelectionType)
        {
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.SelectionType = SelectionType;
        }
    
    }
}
