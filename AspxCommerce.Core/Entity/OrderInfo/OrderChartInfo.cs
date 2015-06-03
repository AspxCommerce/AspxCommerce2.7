/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Runtime.Serialization;

namespace AspxCommerce.Core
{
    [DataContract]
    [Serializable]
    public class OrderChartInfo
    {

        private decimal _grandTotal;
        private string _date;
        private int _customerVisit;
        private int _orders;
        

        public OrderChartInfo()
        {
        }

        [DataMember]
        public decimal GrandTotal
        {
            get
            {
                return this._grandTotal;
            }
            set
            {
                if ((this._grandTotal != value))
                {
                    this._grandTotal = value;
                }
            }
        }

        [DataMember]
        public string Date
        {
            get
            {
                return this._date;
            }
            set
            {
                if ((this._date != value))
                {
                    this._date = value;
                }
            }
        }
         [DataMember]
        public int CustomerVisit
        {
            get
            {
                return this._customerVisit;
            }
            set
            {
                if ((this._customerVisit != value))
                {
                    this._customerVisit = value;
                }
            }
        }
         [DataMember]
         public int Orders
         {
             get
             {
                 return this._orders;
             }
             set
             {
                 if ((this._orders != value))
                 {
                     this._orders = value;
                 }
             }
         }
    }
}
