using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Cherry.Common.Test
{
    [TestClass]
    public class Utility
    {
        [TestMethod]
        public void HexToColor()
        {
            System.Drawing.Color color;
            string hex;

            hex = null;
            color = Cherry.Common.Utility.HexToColor(hex);
            Assert.AreEqual(System.Drawing.Color.Transparent, color);
            hex = "";
            color = Cherry.Common.Utility.HexToColor(hex);
            Assert.AreEqual(System.Drawing.Color.Transparent, color);

            hex = "#";
            color = Cherry.Common.Utility.HexToColor(hex);
            Assert.AreEqual(System.Drawing.Color.Transparent, color);
            hex = "###";
            color = Cherry.Common.Utility.HexToColor(hex);
            Assert.AreEqual(System.Drawing.Color.Transparent, color);
            hex = "ggg";
            color = Cherry.Common.Utility.HexToColor(hex);
            Assert.AreEqual(System.Drawing.Color.Transparent, color);
            hex = "33333";
            color = Cherry.Common.Utility.HexToColor(hex);
            Assert.AreEqual(System.Drawing.Color.Transparent, color);
            hex = "3333333";
            color = Cherry.Common.Utility.HexToColor(hex);
            Assert.AreEqual(System.Drawing.Color.Transparent, color);
            hex = "333333333";
            color = Cherry.Common.Utility.HexToColor(hex);
            Assert.AreEqual(System.Drawing.Color.Transparent, color);

            hex = "333";
            color = Cherry.Common.Utility.HexToColor(hex);
            Assert.AreNotEqual(System.Drawing.Color.Transparent, color);
            Assert.AreEqual(0x33, color.R);
            Assert.AreEqual(0x33, color.G);
            Assert.AreEqual(0x33, color.B);
            Assert.AreEqual(0xff, color.A);
            hex = "#333";
            color = Cherry.Common.Utility.HexToColor(hex);
            Assert.AreNotEqual(System.Drawing.Color.Transparent, color);
            Assert.AreEqual(0x33, color.R);
            Assert.AreEqual(0x33, color.G);
            Assert.AreEqual(0x33, color.B);
            Assert.AreEqual(0xff, color.A);
            hex = "#3333";
            color = Cherry.Common.Utility.HexToColor(hex);
            Assert.AreNotEqual(System.Drawing.Color.Transparent, color);
            Assert.AreEqual(0x33, color.R);
            Assert.AreEqual(0x33, color.G);
            Assert.AreEqual(0x33, color.B);
            Assert.AreEqual(0x33, color.A);
            hex = "3333";
            color = Cherry.Common.Utility.HexToColor(hex);
            Assert.AreNotEqual(System.Drawing.Color.Transparent, color);
            Assert.AreEqual(0x33, color.R);
            Assert.AreEqual(0x33, color.G);
            Assert.AreEqual(0x33, color.B);
            Assert.AreEqual(0x33, color.A);
            hex = "333333";
            color = Cherry.Common.Utility.HexToColor(hex);
            Assert.AreNotEqual(System.Drawing.Color.Transparent, color);
            Assert.AreEqual(0x33, color.R);
            Assert.AreEqual(0x33, color.G);
            Assert.AreEqual(0x33, color.B);
            Assert.AreEqual(0xff, color.A);
            hex = "33333333";
            color = Cherry.Common.Utility.HexToColor(hex);
            Assert.AreNotEqual(System.Drawing.Color.Transparent, color);
            Assert.AreEqual(0x33, color.R);
            Assert.AreEqual(0x33, color.G);
            Assert.AreEqual(0x33, color.B);
            Assert.AreEqual(0x33, color.A);
        }
    }
}
