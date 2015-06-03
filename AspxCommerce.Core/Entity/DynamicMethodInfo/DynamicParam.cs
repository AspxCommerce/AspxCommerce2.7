using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class DynamicParam
    {
        private string _parameterName;
        private string _parameterType;
        private int _parameterOrder;

        public string ParameterName
        {
            get { return this._parameterName; }
            set
            {
                if ((this._parameterName) != value)
                {
                    this._parameterName = value;
                }
            }
        }

        public string ParameterType
        {
            get { return this._parameterType; }
            set
            {
                if ((this._parameterType) != value)
                {
                    this._parameterType = value;
                }
            }
        }

        public int ParameterOrder
        {
            get { return this._parameterOrder; }
            set
            {
                if ((this._parameterOrder) != value)
                {
                    this._parameterOrder = value;
                }
            }
        }

    }
}
