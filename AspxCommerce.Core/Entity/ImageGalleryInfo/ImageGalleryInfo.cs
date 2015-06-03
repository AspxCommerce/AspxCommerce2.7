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
    public class ImageGalleryInfo
    {
        #region "Constructors"
        public ImageGalleryInfo()
        {
        }
        #endregion

        #region "Private Members"          
        int _userModuleId;        
        int _portalID;        
        string _imageWidth;        
        string _imageHeight;        
        string _thumbWidth;        
        string _thumbHeight;        
        string _slideShowAtStart;        
        string _zoomShown;
        #endregion        

        #region "Public Properties"
        [DataMember]
        public int UserModuleId
        {
            get { return _userModuleId; }
            set { _userModuleId = value; }
        }
        [DataMember]
        public int PortalID
        {
            get { return _portalID; }
            set { _portalID = value; }
        }
        [DataMember]
        public string ImageWidth
        {
            get { return _imageWidth; }
            set { _imageWidth = value; }
        }
        [DataMember]
        public string ImageHeight
        {
            get { return _imageHeight; }
            set { _imageHeight = value; }
        }
        [DataMember]
        public string ThumbWidth
        {
            get { return _thumbWidth; }
            set { _thumbWidth = value; }
        }
        [DataMember]
        public string ThumbHeight
        {
            get { return _thumbHeight; }
            set { _thumbHeight = value; }
        }
        [DataMember]
        public string SlideShowAtStart
        {
            get { return _slideShowAtStart; }
            set { _slideShowAtStart = value; }
        }
        [DataMember]
        public string ZoomShown
        {
            get { return _zoomShown; }
            set { _zoomShown = value; }
        }  
        #endregion
    }
}

