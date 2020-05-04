using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Text;

namespace Cherry.Common
{
    public class NinePatch
    {
        private Bitmap _Image;

        public Bitmap Image { get => _Image; set => _Image = value; }

        private DrawMatrix _Matrix;

        public DrawMatrix Matrix { get => _Matrix; }

        private StretchRange[] _StretchX;

        public StretchRange[] StretchX { get => _StretchX; }

        private StretchRange[] _StretchY;

        public StretchRange[] StretchY { get => _StretchY; }

        public class PixelRange
        {
            public int A;

            public int B;

            public override string ToString()
            {
                return $"{A}:{B}";
            }
        }

        public class StretchRange
        {
            /// <summary>
            /// First pixel
            /// </summary>
            public int A;

            /// <summary>
            /// Last pixel
            /// </summary>
            public int B;

            /// <summary>
            /// To stretch or not to stretch
            /// </summary>
            public byte S;

            /// <summary>
            /// Pixel count
            /// </summary>
            public int C { get => GetCount(); }

            /// <summary>
            /// Pixel difference
            /// </summary>
            public int D { get => GetDifference(); }

            private int GetCount()
            {
                return 1 + B - A;
            }

            private int GetDifference()
            {
                return B - A;
            }

            public override string ToString()
            {
                char x = 0 == S ? '-' : '+';
                return $"{A}{x}{B}";
            }
        }

        [Flags]
        public enum StretchMode
        {
            None = 0,
            Horizontal = 1,
            Vertical = 2,
            Both = Vertical + Horizontal
        }

        public class DrawBox
        {
            public int L;
            public int T;
            public int R;
            public int B;
            public StretchMode S;

            public override string ToString()
            {
                char x = '\0';
                if (false)
                { }
                else if (S == StretchMode.None)
                {
                    x = '0';
                }
                else if (S == StretchMode.Both)
                {
                    x = 'X';
                }
                else if (S == StretchMode.Horizontal)
                {
                    x = 'H';
                }
                else if (S == StretchMode.Vertical)
                {
                    x = 'V';
                }
                return $"[({L},{T}){x}({R},{B})]";
            }
        }

        public int GetFixedPixelCountX()
        {
            if (null == _StretchX)
            {
                return 0;
            }
            int c = 0;
            for (int i = 0, l = _StretchX.Length; i < l; i++)
            {
                if (0 == _StretchX[i].S)
                {
                    c += _StretchX[i].C;
                }
            }
            return c;
        }

        public int GetFixedPixelCountY()
        {
            if (null == _StretchY)
            {
                return 0;
            }
            int c = 0;
            for (int i = 0, l = _StretchY.Length; i < l; i++)
            {
                if (0 == _StretchY[i].S)
                {
                    c += _StretchY[i].C;
                }
            }
            return c;

        }

        public class DrawMatrix
        {
            private DrawBox[,] _Matrix;

            private int _Rows;

            private int _Cols;

            public DrawBox this[int y, int x] => GetYX(y, x);

            public int H => _Cols;

            public int V => _Rows;

            private DrawBox GetYX(int y, int x)
            {
                return _Matrix[y, x];
            }

            public DrawMatrix(DrawBox[,] matrix, int rows, int cols)
            {
                _Matrix = matrix;
                _Rows = rows;
                _Cols = cols;
            }
        }

        public NinePatch(Bitmap bitmap)
        {
            this._Image = bitmap;

            System.Drawing.Color c;
            bool z, f;

            var h = new List<StretchRange>();

            f = false;

            for (int i = 1; i < bitmap.Width - 1; i++)
            {
                c = bitmap.GetPixel(i, 0);
                z = c.A == 0 && c.R == 0 && c.G == 0 && c.B == 0;
                if (!z && !f)
                {
                    if (0 < h.Count)
                    {
                        h[h.Count - 1].B = i - 1;
                    }
                    else if (1 < i)
                    {
                        h.Add(new StretchRange() { A = 1, B = i - 1, S = 0 });
                    }
                    h.Add(new StretchRange() { A = i, S = 1 });
                    f = true;
                    continue;
                }
                if (z && f)
                {
                    if (0 < h.Count)
                    {
                        h[h.Count - 1].B = i - 1;
                    }
                    h.Add(new StretchRange() { A = i, S = 0 });
                    f = false;
                    continue;
                }
            }
            if (0 < h.Count)
            {
                h[h.Count - 1].B = bitmap.Width - 2;
            }
            else
            {
                h.Add(new StretchRange() { A = 1, B = bitmap.Width - 2, S = 1 });
            }

            _StretchX = h.ToArray();

            var v = new List<StretchRange>();

            f = false;

            for (int i = 1; i < bitmap.Height - 1; i++)
            {
                c = bitmap.GetPixel(0, i);
                z = c.A == 0 && c.R == 0 && c.G == 0 && c.B == 0;
                if (!z && !f)
                {
                    if (0 < v.Count)
                    {
                        v[v.Count - 1].B = i - 1;
                    }
                    else if (1 < i)
                    {
                        v.Add(new StretchRange() { A = 1, B = i - 1, S = 0 });
                    }
                    v.Add(new StretchRange() { A = i, S = 1 });
                    f = true;
                    continue;
                }
                if (z && f)
                {
                    if (0 < v.Count)
                    {
                        v[v.Count - 1].B = i - 1;
                    }
                    v.Add(new StretchRange() { A = i, S = 0 });
                    f = false;
                    continue;
                }
            }
            if (0 < v.Count)
            {
                v[v.Count - 1].B = bitmap.Height - 2;
            }
            else
            {
                v.Add(new StretchRange() { A = 1, B = bitmap.Height - 2, S = 1 });
            }

            _StretchY = v.ToArray();

            DrawBox[,] m = new DrawBox[v.Count, h.Count];

            for (int y = 0; y < v.Count; y++)
            {
                for (int x = 0; x < h.Count; x++)
                {
                    StretchMode s = StretchMode.None;
                    if (0 < h[x].S)
                    {
                        s |= StretchMode.Horizontal;
                    }
                    if (0 < v[y].S)
                    {
                        s |= StretchMode.Vertical;
                    }
                    m[y, x] = new DrawBox()
                    {
                        L = h[x].A,
                        R = h[x].B,
                        T = v[y].A,
                        B = v[y].B,
                        S = s
                    };
                }
            }

            _Matrix = new DrawMatrix(m, v.Count, h.Count);
        }

        public Bitmap CreateBitmap(int width, int height)
        {
            if (null == _Image || null == _StretchX || null == _StretchY)
            {
                return null;
            }
            if (width < 1 || height < 1)
            {
                return null;
            }
            Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            //g.CompositingQuality = CompositingQuality.HighQuality;
            //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //g.SmoothingMode = SmoothingMode.HighQuality;

            int w = 0 - 2 + _Image.Width;
            int h = 0 - 2 + _Image.Height;

            int fpx = GetFixedPixelCountX();
            int fpy = GetFixedPixelCountY();
            
            int spx = w - fpx;
            int spy = h - fpy;

            int dpx = width - fpx;
            int dpy = height - fpy;

            double rpx = 1.0 * dpx / spx;
            double rpy = 1.0 * dpy / spy;

            int x1, x2, y1, y2;

            int xd, yd, xn, yn;

            int cnx = 0;
            for (int i = 0, l = _StretchX.Length; i < l; i++)
            {
                if (0 != _StretchX[i].S)
                {
                    cnx++;
                }
            }
            int cny = 0;
            for (int i = 0, l = _StretchY.Length; i < l; i++)
            {
                if (0 != _StretchY[i].S)
                {
                    cny++;
                }
            }

            Debug.WriteLine($"Width {width} Height {height}");
            Debug.WriteLine($"fpx {fpx} fpy {fpy} spx {spx} spy {spy} dpx {dpx} dpy {dpy} rpx {rpx} rpy {rpy}");

            y1 = 0;
            yd = dpy;
            yn = cny;
            for (int iy = 0, ly = _StretchY.Length; iy < ly; iy++)
            {
                byte sy = _StretchY[iy].S;
                if (0 == sy)
                {
                    y2 = y1 + _StretchY[iy].D;
                }
                else
                {
                    int dy = (int)(Math.Floor(1.0 * rpy * _StretchY[iy].C * _StretchY[iy].C / spy));
                    y2 = y1 - 1 + dy;
                    yd -= dy;
                    if (0 == --yn)
                    {
                        Debug.WriteLine($"Δy {yd}");
                        y2 -= yd;
                    }
                }
                x1 = 0;
                xd = dpx;
                xn = cnx;
                for (int ix = 0, lx = _StretchX.Length; ix < lx; ix++)
                {
                    byte sx = _StretchX[ix].S;
                    if (0 == sx)
                    {
                        x2 = x1 + _StretchX[ix].D;
                    }
                    else
                    {
                        int dx = (int)(Math.Floor(1.0 * rpx * _StretchX[ix].C * _StretchX[ix].C / spx));
                        x2 = x1 - 1 + dx;
                        xd -= dx;
                        if (0 == --xn)
                        {
                            Debug.WriteLine($"Δx {xd}");
                            x2 -= xd;
                        }
                    }
                    Rectangle rs = new Rectangle(_StretchX[ix].A, _StretchY[iy].A, _StretchX[ix].C, _StretchY[iy].C);
                    Rectangle rd = new Rectangle(x1, y1, 1 - 0 + x2 - x1, 1 - 0 + y2 - y1);
                    g.DrawImage(_Image, rd, rs, GraphicsUnit.Pixel);

                    //using (System.Drawing.Pen myPen = new System.Drawing.Pen((ix + iy) % 2 == 0 ? Color.Gray : Color.DarkGray))
                    //{
                    //    g.DrawRectangle(myPen, rd);
                    //}

                    Debug.WriteLine("Rectangle " + rd);
                    x1 = x2 + 1;
                }
                y1 = y2 + 1;
            }

            //using (System.Drawing.Pen myPen = new System.Drawing.Pen(Color.Black))
            //{
            //    g.DrawRectangle(myPen, 0, 0, width - 1, height - 1);
            //}

            return bitmap;
        }
    }
}
