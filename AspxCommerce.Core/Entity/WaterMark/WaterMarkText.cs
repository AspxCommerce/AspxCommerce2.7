using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspxCommerce.Core
{
    public class WaterMarkText
    {
        public string Text { get; set; }
        public TextPosition TextPosition { get; set; }
        public double RotationAngle { get; set; }

    }
    public enum TextPosition
    {
        TOP_CENTER,MID_CENTER,BOTTOM_CENTER,TOP_LEFT,MID_LEFT,BOTTOM_LEFT
    }

    public class WaterMarkImage
    {
        public string ImagePath { get; set; }
        public string RotationAngle { get; set; }
        public ImagePosition ImagePosition { get; set; }
    }

    public enum ImagePosition
    {
        TOP_LEFT,TOP_RIGHT,CENTER,BOTTOM_LEFT,BOTTOM_RIGHT
    }
}
