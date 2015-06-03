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
    /// Enum for  Advetise Unit type
    /// </summary>
    public enum AdUnitType
    {
        /// <summary>
        /// value =0
        /// </summary>
        TextAndImage = 0,
        /// <summary>
        /// value=1
        /// </summary>
        ImageOnly = 1,
        /// <summary>
        /// value =2
        /// </summary>
        TextOnly = 2
    }

    /// <summary>
    /// Enum for advertise unit format type.
    /// </summary>
    public enum AdUnitFormat
    {
       
        LeaderBoard_728x90_H = 0,
        Banner_468x60_H = 1,
        HalfBanner_234x60_H = 2,
        SkyScapper_120x600_V = 3,
        WideSkyScrapper_160x600_V = 4,
        VerticalBanner_120x240_V = 5,
        Button_125x125_S = 6,
        MediumRectangle_300x250_S = 7,
        Square_250x250_S = 8,
        LargeRectangle_336x280_S = 9,
        SmallRectangle_180x150_S = 10
    }

	/// <summary>
	/// Enum for link unit format type.
	/// </summary>
	public enum LinkUnitFormat
	{
		H_728x15 = 0,
		H_468x15 = 1,
		S_200x90 = 2,
		S_180x90 = 3,
		S_160x90 = 4,
		S_120x90 = 5
	}

	/// <summary>
	/// Enum for alternate Ad Types.
	/// </summary>
	public enum AlternateAdTypes
	{
		PublicServiceAds = 0,
		AnotherUrlAds = 1,
		SolidColorFill = 2
	}

    /// <summary>
    /// Enum for link Ads Per Unit.
    /// </summary>
    public enum LinkAdsPerUnit
    {
        AdCount_4 = 0,
        AdCount_5 = 1
    }
}
