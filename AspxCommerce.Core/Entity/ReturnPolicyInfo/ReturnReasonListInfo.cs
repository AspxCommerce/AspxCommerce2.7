
namespace AspxCommerce.Core
{      
   public class ReturnReasonListInfo
    { 
        private string _value;
        private string _text;

        public ReturnReasonListInfo()
        {
        }
      
        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                if ((this._value) != value)
                {
                    this._value = value;
                }
            }

        }
      
        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                if ((this._text) != value)
                {
                    this._text = value;
                }
            }
           
        }
    }
}
