/*
SageFrame® - http://www.sageframe.com
Copyright (c) 2009-2012 by SageFrame
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
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace SFE.GoogleAdUnit
{
    /// <summary>
    /// Internal class that contains unit dimensions.
    /// </summary>
    internal class UnitDimensions
    {
        internal Int16 HEIGHT;
        internal Int16 WIDTH;

        /// <summary>
        /// Initializes an instance of UnitDimensions class.
        /// </summary>
        /// <param name="iWidth">Width to be initialize.</param>
        /// <param name="iHeight">Height to be initialize.</param>
		internal UnitDimensions(Int16 iWidth, Int16 iHeight)
        {
            HEIGHT = iHeight;
            WIDTH = iWidth;
        }
    }
}
