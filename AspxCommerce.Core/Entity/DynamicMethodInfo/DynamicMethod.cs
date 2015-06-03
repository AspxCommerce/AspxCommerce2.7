using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class DynamicMethod
    {
        private int _dynamicMethodId;
        private string _methodName;
        private string _className;
        private string _assemblyName;
        private string _namespace;
        private int _parameterCount;
        private string _methodType;
        public List<DynamicParam> DynamicParams { get; set; }


        public string MethodName
        {
            get
            {
                return this._methodName;
            }
            set
            {
                if ((this._methodName) != value)
                {
                    this._methodName = value;
                }
            }
        }

        public string ClassName
        {
            get
            {
                return this._className;
            }
            set
            {
                if ((this._className) != value)
                {
                    this._className = value;
                }
            }
        }

        public string AssemblyName
        {
            get
            {
                return this._assemblyName;
            }
            set
            {
                if ((this._assemblyName) != value)
                {
                    this._assemblyName = value;
                }
            }
        }

        public string NameSpace
        {
            get
            {
                return this._namespace;
            }
            set
            {
                if ((this._namespace) != value)
                {
                    this._namespace = value;
                }
            }
        }
        public int ParameterCount
        {
            get
            {
                return this._parameterCount;
            }
            set
            {
                if ((this._parameterCount) != value)
                {
                    this._parameterCount = value;
                }
            }
        }
        public int DynamicMethodId
        {
            get
            {
                return this._dynamicMethodId;
            }
            set
            {
                if ((this._dynamicMethodId) != value)
                {
                    this._dynamicMethodId = value;
                }
            }
        }
        public string MethodType
        {
            get
            {
                return this._methodType;
            }
            set
            {
                if ((this._methodType) != value)
                {
                    this._methodType = value;
                }
            }
        }


    }
}
