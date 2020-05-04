using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace Cherry.Common.Test
{
    [TestClass]
    public class NinePatch
    {
        [TestMethod]
        public void TestMethod1()
        {
            Bitmap b, bm1;
            Cherry.Common.NinePatch p;
            System.Drawing.Color c1 = new Cherry.Common.Hex("#0000");
            System.Drawing.Color c2 = new Cherry.Common.Hex("#000f");

            b = new System.Drawing.Bitmap(3 + 2, 1 + 2);

            b.SetPixel(1, 0, c1);
            b.SetPixel(2, 0, c2);
            b.SetPixel(3, 0, c1);

            p = new Cherry.Common.NinePatch(b);
            Assert.IsNotNull(p);
            Assert.AreEqual(Cherry.Common.NinePatch.StretchMode.Vertical, p.Matrix[0, 0].S);
            Assert.AreEqual(Cherry.Common.NinePatch.StretchMode.Both, p.Matrix[0, 1].S);
            Assert.AreEqual(Cherry.Common.NinePatch.StretchMode.Vertical, p.Matrix[0, 2].S);

            b = new System.Drawing.Bitmap(3 + 2, 3 + 2);

            b.SetPixel(1, 0, c1);
            b.SetPixel(2, 0, c2);
            b.SetPixel(3, 0, c1);

            b.SetPixel(0, 1, c1);
            b.SetPixel(0, 2, c2);
            b.SetPixel(0, 3, c1);

            p = new Cherry.Common.NinePatch(b);
            Assert.IsNotNull(p);

            Assert.AreEqual(2, p.GetFixedPixelCountX());
            Assert.AreEqual(2, p.GetFixedPixelCountY());

            bm1 = p.CreateBitmap(5, 5);
            Assert.IsNotNull(bm1);
            Assert.AreEqual(5, bm1.Width);
            Assert.AreEqual(5, bm1.Height);

            b = new System.Drawing.Bitmap(3 + 2, 3 + 2);
            p = new Cherry.Common.NinePatch(b);
            //o.Bitmap.SetPixel()
        }
    }
}
