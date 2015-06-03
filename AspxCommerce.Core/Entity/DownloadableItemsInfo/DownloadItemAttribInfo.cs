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

namespace AspxCommerce.Core
{
    [Serializable]
    public class DownloadItemAttribInfo
    {
        public DownloadItemAttribInfo()
        {
        }

        private string _fileName;

        private string _fileType;

        private decimal _fileSize;

        public string FileName
        {
            get
            {
                return this._fileName;
            }
            set
            {
                if ((this.FileName != value))
                {
                    this._fileName = value;
                }
            }
        }

        public string FileType
        {
            get
            {
                return this._fileType;
            }
            set
            {
                if ((this._fileType != value))
                {
                    this._fileType = value;
                }
            }
        }

        public decimal FileSize
        {
            get
            {
                return this._fileSize;
            }
            set
            {
                if ((this._fileSize != value))
                {
                    this._fileSize = value;
                }
            }
        }       
    }
}
