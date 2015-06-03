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
    public class ReturnCommentsInfo
    {
        private int _commentID;
        private string _commentText;
        private string _addedBy;
        private string _addedOn;
        private string _isNotified;
        private int _customerID;

        [DataMember]
        public int CommentID
        {
            get
            {
                return this._commentID;
            }
            set
            {
                if ((this._commentID != value))
                {
                    this._commentID = value;
                }
            }
        }
        [DataMember]
        public string CommentText
        {
            get
            {
                return this._commentText;
            }
            set
            {
                if ((this._commentText != value))
                {
                    this._commentText = value;
                }
            }
        }
        [DataMember]
        public string AddedBy
        {
            get
            {
                return this._addedBy;
            }
            set
            {
                if ((this._addedBy != value))
                {
                    this._addedBy = value;
                }
            }
        }
        [DataMember]
        public string AddedOn
        {
            get
            {
                return this._addedOn;
            }
            set
            {
                if ((this._addedOn != value))
                {
                    this._addedOn = value;
                }
            }
        }
        [DataMember]
        public string IsNotified
        {
            get
            {
                return this._isNotified;
            }
            set
            {
                if ((this._isNotified != value))
                {
                    this._isNotified = value;
                }
            }
        }
        [DataMember]
        public int CustomerID
        {
            get
            {
                return this._customerID;
            }
            set
            {
                if ((this._customerID != value))
                {
                    this._customerID = value;
                }
            }
        }




    }
}
