using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public class IndexManagement
    {
        [DataMember(Name = "_rowTotal", Order = 0)]
        private System.Nullable<int> _rowTotal;

        [DataMember(Name = "_tableName", Order = 1)]
        private string _tableName;

        [DataMember(Name = "_indexName", Order = 2)]
        private string _indexName;

        [DataMember(Name = "_externalFragmentation", Order = 3)]
        private string _externalFragmentation;

        [DataMember(Name = "_internalFragmentation", Order = 4)]
        private string _internalFragmentation;

        [DataMember(Name = "_isRebuild", Order = 5)]
        private string _isRebuild;

        [DataMember(Name = "_isReady", Order = 6)]
        private string _isReady;        

        public IndexManagement()
        {
        }

        public System.Nullable<int> RowTotal
        {
            get
            {
                return this._rowTotal;
            }
            set
            {
                if ((this._rowTotal != value))
                {
                    this._rowTotal = value;
                }
            }
        }

        public string TableName
        {
            get
            {
                return this._tableName;
            }
            set
            {
                if ((this._tableName != value))
                {
                    this._tableName = value;
                }
            }
        }

        public string IndexName
        {
            get
            {
                return this._indexName;
            }
            set
            {
                if ((this._indexName != value))
                {
                    this._indexName = value;
                }
            }
        }

        public string ExternalFragmentation
        {
            get
            {
                return this._externalFragmentation;
            }
            set
            {
                if ((this._externalFragmentation != value))
                {
                    this._externalFragmentation = value;
                }
            }
        }

        public string InternalFragmentation
        {
            get
            {
                return this._internalFragmentation;
            }
            set
            {
                if ((this._internalFragmentation != value))
                {
                    this._internalFragmentation = value;
                }
            }
        }

        public string IsRebuild
        {
            get
            {
                double externalF = Convert.ToDouble(ExternalFragmentation);
                double internalF = Convert.ToDouble(InternalFragmentation);
                string result = "0";
                if (externalF < 15 && (internalF > 60 && internalF < 75))
                {
                    result = "1";
                }
                else
                {
                    result = "0";
                }

                _isRebuild = result;
                return result;
            }
        }

        public string IsReady
        {
            get
            {
                _isReady = "Ready";
                return "Ready";
            }
            set
            {
                if ((this._isReady != value))
                {
                    _isReady = "Ready";
                }
            }
        }

    }
}
