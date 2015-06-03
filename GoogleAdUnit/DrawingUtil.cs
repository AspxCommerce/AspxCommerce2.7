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
using System.Drawing;
using System.Drawing.Design;

namespace SFE.GoogleAdUnit
{
    /// <summary>
    /// Class that containd utilities for drawing.
    /// </summary>
	public class DrawingUtil
	{
        /// <summary>
        /// Returns hexa decimal value of color.
        /// </summary>
        /// <param name="clr">Color object.</param>
        /// <param name="bRemovePound">Set true if '#' has to be remove.</param>
        /// <returns></returns>
		public static String GetColorHexFormat(Color clr, Boolean bRemovePound)
        {
			String strHexValue = String.Empty;
			String r1, r2, g1, g2, b1, b2;

			r1 = GiveHexValue(System.Math.Floor((double)(clr.R / 16)));
			r2 = GiveHexValue(clr.R % 16);

			g1 = GiveHexValue(System.Math.Floor((double)(clr.G / 16)));
			g2 = GiveHexValue(clr.G % 16);

			b1 = GiveHexValue(System.Math.Floor((double)(clr.B / 16)));
			b2 = GiveHexValue(clr.B % 16);

			strHexValue = r1 + r2 + g1 + g2 + b1 + b2;

			if (!bRemovePound)
			{
				strHexValue = "#" + strHexValue;
			}

			return strHexValue.ToUpper();
        }

        /// <summary>
        /// Returns hexadecimal values for any double value.
        /// </summary>
        /// <param name="val">Double value</param>
        /// <returns>Hexadecimal value in terms of string.</returns>
		public static String GiveHexValue(double val)
		{
			string strHexValue = "";
			if (val == 10)
			{
				strHexValue = "A";
			}
			else if (val == 11)
			{
				strHexValue = "B";
			}
			else if (val == 12)
			{
				strHexValue = "C";
			}
			else if (val == 13)
			{
				strHexValue = "D";
			}
			else if (val == 14)
			{
				strHexValue = "E";
			}
			else if (val == 15)
			{
				strHexValue = "F";
			}
			else
			{
				strHexValue = val.ToString();
			}

			return strHexValue;
		}

	}
}
