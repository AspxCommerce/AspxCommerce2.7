using System;
using System.Runtime.Serialization;


namespace AspxCommerce.MegaCategory
{
    [DataContract]
    [Serializable]
    public class MegaCategorySettingInfo
    {
        [DataMember]
        private string _modeOfview;

        [DataMember]
        private int _noOfColumn;

        [DataMember]
        private bool _showCategoryImage;

        [DataMember]
        private bool _showSubCategoryImage;

        [DataMember]
        private string _speed;

        [DataMember]
        private string _effect;

        [DataMember]
        private string _direction;

        [DataMember]
        private string _event;


        public string ModeOfView
        {
            get
            {
                return this._modeOfview;
            }
            set
            {
                if ((this._modeOfview != value))
                {
                    this._modeOfview = value;
                }
            }
        }

        public int NoOfColumn
        {
            get
            {
                return this._noOfColumn;
            }
            set
            {
                if ((this._noOfColumn != value))
                {
                    this._noOfColumn = value;
                }
            }
        }

        public bool ShowCategoryImage
        {
            get
            {
                return this._showCategoryImage;
            }
            set
            {
                if ((this._showCategoryImage != value))
                {
                    this._showCategoryImage = value;
                }
            }
        }

        public bool ShowSubCategoryImage
        {
            get
            {
                return this._showSubCategoryImage;
            }
            set
            {
                if ((this._showSubCategoryImage != value))
                {
                    this._showSubCategoryImage = value;
                }
            }
        }
        public string Speed
        {
            get
            {
                return this._speed;
            }
            set
            {
                if ((this._speed != value))
                {
                    this._speed = value;
                }
            }
        }
        public string Effect
        {
            get
            {
                return this._effect;
            }
            set
            {
                if ((this._effect != value))
                {
                    this._effect = value;
                }
            }
        }
        public string Direction
        {
            get
            {
                return this._direction;
            }
            set
            {
                if ((this._direction != value))
                {
                    this._direction = value;
                }
            }
        }
        public string EventMega
        {
            get
            {
                return this._event;
            }
            set
            {
                if ((this._event != value))
                {
                    this._event = value;
                }
            }
        }
       
    }
}
