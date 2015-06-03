using System;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public class WareHouse
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private int _rowTotal;

        public int RowTotal
        {
            get
            {
                return this._rowTotal;
            }
            set
            {
                this._rowTotal = value;

            }
        }
        [DataMember(Name = "_wareHouseId", Order = 1)]
        private int _wareHouseId;
        public int WareHouseID
        {
            get
            {
                return this._wareHouseId;
            }
            set
            {
                this._wareHouseId = value;

            }
        }
        [DataMember(Name = "_name", Order = 2)]
        private string _name;

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;

            }
        }
        [DataMember(Name = "_address", Order = 3)]
        private string _address;

        public string Address
        {
            get
            {
                return this._address;
            }
            set
            {
                this._address = value;

            }
        }
        [DataMember(Name = "_isPrimary", Order = 4)]
        private bool? _isPrimary;

        public bool? IsPrimary
        {
            get
            {
                return this._isPrimary;
            }
            set
            {
                this._isPrimary = value;

            }
        }
      



   }

   
}