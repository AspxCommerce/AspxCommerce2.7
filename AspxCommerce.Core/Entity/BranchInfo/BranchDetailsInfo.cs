using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public class BranchDetailsInfo
    {

        [DataMember(Name = "_rowTotal", Order = 0)] private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_storeBranchID", Order = 1)] private int _storeBranchID;

        [DataMember(Name = "_branchName", Order = 2)] private string _branchName;

        [DataMember(Name = "_branchImage", Order = 3)] private string _branchImage;

        [DataMember(Name = "_addedOn", Order = 4)] private System.Nullable<System.DateTime> _addedOn;

        public BranchDetailsInfo()
        {
        }

        public System.Nullable<int> RowTotal
        {
            get { return this._rowTotal; }
            set
            {
                if ((this._rowTotal != value))
                {
                    this._rowTotal = value;
                }
            }
        }

        public int StoreBranchID
        {
            get { return this._storeBranchID; }
            set
            {
                if ((this._storeBranchID != value))
                {
                    this._storeBranchID = value;
                }
            }
        }

        public string BranchName
        {
            get { return this._branchName; }
            set
            {
                if ((this._branchName != value))
                {
                    this._branchName = value;
                }
            }
        }

        public string BranchImage
        {
            get { return this._branchImage; }
            set
            {
                if ((this._branchImage != value))
                {
                    this._branchImage = value;
                }
            }
        }

        public System.Nullable<System.DateTime> AddedOn
        {
            get { return this._addedOn; }
            set
            {
                if ((this._addedOn != value))
                {
                    this._addedOn = value;
                }
            }
        }
    }
}
