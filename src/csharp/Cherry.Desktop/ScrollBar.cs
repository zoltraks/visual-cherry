using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace Cherry.Desktop
{
    /// <summary>
    /// Panel with color frame painted around.
    /// </summary>
    [DesignerCategory("Code")]
    [DefaultProperty("Orientation")]
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    [ToolboxBitmap(typeof(FrameBox), "ScrollBar.bmp")]
    [ToolboxItem(true)]
    public partial class ScrollBar : UserControl
    {
        public ScrollBar()
        {
            SetupComponent();
        }

        private void SetupComponent()
        {
            this.SuspendLayout();

            this.BackColor = SystemColors.ControlDarkDark;
            this.ForeColor = SystemColors.ControlText;
            this.Name = "ScrollBar";
            this.Padding = new Padding(0);
            this.Size = new Size(200, 20);
            this.SliderBackColor = Color.Transparent;
            this.SliderForeColor = SystemColors.ControlDark;
            this.GripBeforeVisible = false;
            this.GripAfterVisible = false;
            this.ArrowBeforeVisible = true;
            this.ArrowAfterVisible = true;
            this.GripBeforeSize = 20;
            this.GripAfterSize = 20;
            this.ArrowBeforeSize = 20;
            this.ArrowAfterSize = 20;
            this.Minimum = 0;
            this.Maximum = 100;
            this.Position = 0;
            this.Portion = 0;
            this.SliderMinimumSize = 20;

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);

            this.ResumeLayout(false);
        }

        private Orientation _Orientation;

        [Category("Appearance")]
        public Orientation Orientation
        {
            get
            {
                return _Orientation;
            }
            set
            {
                SetOrientation(value);
            }
        }

        private void SetOrientation(Orientation value)
        {
            if (_Orientation == value)
            {
                return;
            }
            _Orientation = value;
            if (this.Dock != DockStyle.Fill)
            {
                Flip();
            }
        }

        private void Flip()
        {
            this.Size = new Size(this.Height, this.Width);
        }

        private int _Minimum;

        [Category("Behavior")]
        public int Minimum
        {
            get
            {
                return _Minimum;
            }
            set
            {
                _Minimum = value;
            }
        }

        private int _Maximum;

        [Category("Behavior")]
        public int Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                _Maximum = value;
            }
        }

        private int _Position;

        [Category("Behavior")]
        public int Position
        {
            get
            {
                return _Position;
            }
            set
            {
                _Position = value;
            }
        }

        private int _Portion;

        [Category("Behavior")]
        public int Portion
        {
            get
            {
                return _Portion;
            }
            set
            {
                _Portion = value;
            }
        }


        private bool _GripBeforeVisible;

        [Category("Appearance")]
        public bool GripBeforeVisible
        {
            get
            {
                return _GripBeforeVisible;
            }
            set
            {
                _GripBeforeVisible = value;
            }
        }

        private int _GripBeforeSize;

        [Category("Layout")]
        public int GripBeforeSize
        {
            get
            {
                return _GripBeforeSize;
            }
            set
            {
                _GripBeforeSize = value;
            }
        }

        private bool _GripAfterVisible;

        [Category("Appearance")]
        public bool GripAfterVisible
        {
            get
            {
                return _GripAfterVisible;
            }
            set
            {
                _GripAfterVisible = value;
            }
        }

        private int _GripAfterSize;

        [Category("Layout")]
        public int GripAfterSize
        {
            get
            {
                return _GripAfterSize;
            }
            set
            {
                _GripAfterSize = value;
            }
        }

        private bool _ArrowBeforeVisible;

        [Category("Appearance")]
        [Browsable(true)]
        public bool ArrowBeforeVisible
        {
            get
            {
                return _ArrowBeforeVisible;
            }
            set
            {
                _ArrowBeforeVisible = value;
            }
        }

        private int _ArrowBeforeSize;

        [Category("Layout")]
        public int ArrowBeforeSize
        {
            get
            {
                return _ArrowBeforeSize;
            }
            set
            {
                _ArrowBeforeSize = value;
            }
        }

        private bool _ArrowAfterVisible;

        [Category("Appearance")]
        public bool ArrowAfterVisible
        {
            get
            {
                return _ArrowAfterVisible;
            }
            set
            {
                _ArrowAfterVisible = value;
            }
        }

        private int _ArrowAfterSize;

        [Category("Layout")]
        public int ArrowAfterSize
        {
            get
            {
                return _ArrowAfterSize;
            }
            set
            {
                _ArrowAfterSize = value;
            }
        }

        private int _SliderMinimumSize;

        [Category("Layout")]
        public int SliderMinimumSize
        {
            get
            {
                return _SliderMinimumSize;
            }
            set
            {
                _SliderMinimumSize = value;
            }
        }

        private Color _SliderBackColor;

        [Category("Appearance")]
        public Color SliderBackColor
        {
            get
            {
                return _SliderBackColor;
            }
            set
            {
                _SliderBackColor = value;
            }
        }

        private Color _SliderForeColor;

        [Category("Appearance")]
        public Color SliderForeColor
        {
            get
            {
                return _SliderForeColor;
            }
            set
            {
                _SliderForeColor = value;
            }
        }

        [Category("Layout")]
        public new Padding Padding
        {
            get
            {
                return base.Padding;
            }
            set
            {
                base.Padding = value;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            Brush b;
            b = new SolidBrush(BackColor);
            g.FillRectangle(b, 0, 0, Width, Height);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            try
            {
                Graphics g;
                Brush b;

                g = pe.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

                int sliderPixel = 0;
                int sliderLength = 0;

                if (Orientation.Horizontal == _Orientation)
                {
                    sliderLength = Width;
                }
                if (Orientation.Vertical == _Orientation)
                {
                    sliderLength = Height;
                }
                if (_GripBeforeVisible)
                {
                    sliderPixel += _GripBeforeSize;
                    sliderLength -= _GripBeforeSize;
                }
                if (_GripAfterVisible)
                {
                    sliderLength -= _GripAfterSize;
                }
                if (_ArrowBeforeVisible)
                {
                    sliderPixel += _ArrowBeforeSize;
                    sliderLength -= _ArrowBeforeSize;
                }
                if (_ArrowAfterVisible)
                {
                    sliderLength -= _ArrowAfterSize;
                }

                if (Orientation.Horizontal == _Orientation)
                {
                    if (Color.Transparent != _SliderBackColor)
                    {
                        b = new SolidBrush(_SliderBackColor);
                        Rectangle sliderRect = new Rectangle(Padding.Left + sliderPixel, Padding.Top, sliderLength - Padding.Left - Padding.Right, Height - Padding.Top - Padding.Bottom);
                        g.FillRectangle(b, sliderRect);
                    }
                }

                if (Orientation.Horizontal == _Orientation)
                {
                    b = new SolidBrush(_SliderForeColor);
                }

                //    b = new SolidBrush(BackColor);
                //    g.FillRectangle(b, 0, 0, Width, Height);

                //    if (_FillColor != Color.Transparent && _FillColor != BackColor)
                //    {
                //        b = new SolidBrush(_FillColor);
                //        if (_FrameColor == Color.Transparent || _FrameWidth == 0)
                //        {
                //            g.FillRectangle(b
                //                , 0 + _FrameWidth + _FrameMargin.Left
                //                , 0 + _FrameWidth + _FrameMargin.Top
                //                , Width - _FrameWidth - _FrameWidth - _FrameMargin.Right - _FrameMargin.Left
                //                , Height - _FrameWidth - _FrameWidth - _FrameMargin.Bottom - _FrameMargin.Top
                //                );
                //            return;
                //        }
                //        g.FillRectangle(b
                //            , 0 + _FrameWidth + _FrameMargin.Left + _FrameCorner
                //            , 0 + _FrameWidth + _FrameMargin.Top
                //            , Width - _FrameWidth - _FrameWidth - _FrameMargin.Right - _FrameMargin.Left - _FrameCorner - _FrameCorner
                //            , Height - _FrameWidth - _FrameWidth - _FrameMargin.Bottom - _FrameMargin.Top
                //            );
                //        if (_FrameCorner > 0)
                //        {
                //            g.FillRectangle(b
                //                , 0 + _FrameWidth + _FrameMargin.Left
                //                , 0 + _FrameWidth + _FrameMargin.Top + _FrameCorner
                //                , _FrameCorner
                //                , Height - _FrameWidth - _FrameWidth - _FrameMargin.Bottom - _FrameMargin.Top - _FrameCorner - _FrameCorner
                //            );
                //            g.FillRectangle(b
                //                , Width - _FrameWidth - _FrameMargin.Left - _FrameCorner
                //                , 0 + _FrameWidth + _FrameMargin.Top + _FrameCorner
                //                , _FrameCorner
                //                , Height - _FrameWidth - _FrameWidth - _FrameMargin.Bottom - _FrameMargin.Top - _FrameCorner - _FrameCorner
                //            );
                //        }
                //    }

                //    if (_FrameColor == Color.Transparent || _FrameWidth == 0)
                //    {
                //        return;
                //    }

                //    int w = Width - 1;
                //    int h = Height - 1;
                //    Padding m = FrameMargin;
                //    int c = _FrameCorner;
                //    Pen p = new Pen(_FrameColor, _FrameWidth);

                //    if (c == 0)
                //    {
                //        if (_FrameWidth > 1)
                //        {
                //            w++;
                //            h++;
                //        }
                //        p.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                //        g.DrawRectangle(p, new Rectangle(m.Left, m.Top, w - m.Left - m.Right, h - m.Top - m.Bottom));
                //        return;
                //    }

                //    int d = c * 2;

                //    if (this.VerticalScroll.Visible)
                //    {
                //        w -= SystemInformation.VerticalScrollBarWidth;
                //    }

                //    float δ = _FrameWidth / 2.0f;
                //    int α = _FrameCorner % 2;
                //    int γ = _FrameWidth / 2;
                //    int ε = _FrameWidth - 1;

                //    if (!this.VerticalScroll.Visible)
                //    {
                //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                //        p.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                //        g.FillPie(b, new Rectangle(0 + m.Left + γ, 0 + m.Top + γ, d + ε + 1, d + ε + 1), 180, 90); // TopLeft
                //        g.FillPie(b, new Rectangle(w - m.Right - γ - d - ε - 1, 0 + m.Top + γ, d + ε + 1, d + ε + 1), 270, 90); // TopRight
                //        g.FillPie(b, new Rectangle(0 + m.Left + γ, h - m.Bottom - γ - d - ε - 1, d + ε + 1, d + ε + 1), 90, 90); // BottomLeft
                //        g.FillPie(b, new Rectangle(w - m.Right - γ - d - ε - 1, h - m.Bottom - γ - d - ε - 1, d + ε + 1, d + ε + 1), 0, 90); // BottomLeft

                //        g.DrawLine(p, 0 + m.Left + δ, c + m.Top + δ - α, 0 + m.Left + δ, h - m.Bottom - c - δ + α); // VerticalLeft
                //        g.DrawLine(p, w - m.Right - δ + 0.1f, c + m.Top + δ - α, w - m.Right - δ + 0.1f, h - m.Bottom - c - δ + α); // VerticalRight
                //        g.DrawLine(p, c + m.Left + δ - α, 0 + m.Top + δ, w - m.Right - c - δ + α, 0 + m.Top + δ); // HorizontalTop
                //        g.DrawLine(p, c + m.Left + δ - α, h - m.Bottom - δ + 0.1f, w - m.Right - c - δ + α, h - m.Bottom - δ + 0.1f); // HorizontalBottom

                //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //        p.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                //        g.DrawArc(p, new Rectangle(0 + m.Left + γ, 0 + m.Top + γ, d, d), 180, 90); // TopLeft
                //        g.DrawArc(p, new Rectangle(w - d - m.Right - γ, 0 + m.Top + γ, d, d), 270, 90); // TopRight
                //        g.DrawArc(p, new Rectangle(0 + m.Left + γ, h - d - m.Bottom - γ, d, d), 90, 90); // BottomLeft
                //        g.DrawArc(p, new Rectangle(w - d - m.Right - γ, h - d - m.Bottom - γ, d, d), 0, 90); // BottomRight
                //    }
                //    else
                //    {
                //        if (VerticalScroll.Value <= VerticalScroll.Minimum)
                //        {
                //            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                //            p.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                //            g.FillPie(b, new Rectangle(0 + m.Left + γ, 0 + m.Top + γ, d + ε + 1, d + ε + 1), 180, 90); // TopLeft
                //            g.FillPie(b, new Rectangle(w - m.Right - γ - d - ε - 1, 0 + m.Top + γ, d + ε + 1, d + ε + 1), 270, 90); // TopRight

                //            g.FillRectangle(b, 0 + m.Left, Height - m.Bottom - d, d, d); // BottomLeft
                //            g.FillRectangle(b, Width - m.Right - d, Height - m.Bottom - d, d, d); // BottomRight

                //            g.DrawLine(p, 0 + m.Left + δ, c + m.Top + δ - α, 0 + m.Left + δ, h - m.Bottom - c - δ + α); // VerticalLeft
                //            g.DrawLine(p, w - m.Right - δ + 0.1f, c + m.Top + δ - α, w - m.Right - δ + 0.1f, h - m.Bottom - c - δ + α); // VerticalRight
                //            g.DrawLine(p, c + m.Left + δ - α, 0 + m.Top + δ, w - m.Right - c - δ + α, 0 + m.Top + δ); // HorizontalTop

                //            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //            p.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                //            g.DrawArc(p, new Rectangle(0 + m.Left + γ, 0 + m.Top + γ, d, d), 180, 90); // TopLeft
                //            g.DrawArc(p, new Rectangle(w - d - m.Right - γ, 0 + m.Top + γ, d, d), 270, 90); // TopRight
                //        }
                //        else if (VerticalScroll.Value + this.Height >= VerticalScroll.Maximum - 1)
                //        {
                //            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                //            p.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                //            g.FillPie(b, new Rectangle(0 + m.Left + γ, h - m.Bottom - γ - d - ε - 1, d + ε + 1, d + ε + 1), 90, 90); // BottomLeft
                //            g.FillPie(b, new Rectangle(w - m.Right - γ - d - ε - 1, h - m.Bottom - γ - d - ε - 1, d + ε + 1, d + ε + 1), 0, 90); // BottomLeft

                //            g.FillRectangle(b, 0 + m.Left, 0 + m.Top, d, d); // TopLeft
                //            g.FillRectangle(b, Width - m.Right - d, 0 + m.Top, d, d); // TopRight

                //            g.DrawLine(p, 0 + m.Left + δ, c + m.Top + δ - α, 0 + m.Left + δ, h - m.Bottom - c - δ + α); // VerticalLeft
                //            g.DrawLine(p, w - m.Right - δ + 0.1f, c + m.Top + δ - α, w - m.Right - δ + 0.1f, h - m.Bottom - c - δ + α); // VerticalRight
                //            g.DrawLine(p, c + m.Left + δ - α, h - m.Bottom - δ + 0.1f, w - m.Right - c - δ + α, h - m.Bottom - δ + 0.1f); // HorizontalBottom

                //            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //            p.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                //            g.DrawArc(p, new Rectangle(0 + m.Left + γ, h - d - m.Bottom - γ, d, d), 90, 90); // BottomLeft
                //            g.DrawArc(p, new Rectangle(w - d - m.Right - γ, h - d - m.Bottom - γ, d, d), 0, 90); // BottomRight
                //        }
                //        else
                //        {
                //            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                //            p.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                //            g.FillRectangle(b, 0 + m.Left, 0 + m.Top, d, d); // TopLeft
                //            g.FillRectangle(b, Width - m.Right - d, 0 + m.Top, d, d); // TopRight
                //            g.FillRectangle(b, 0 + m.Left, Height - m.Bottom - d, d, d); // BottomLeft
                //            g.FillRectangle(b, Width - m.Right - d, Height - m.Bottom - d, d, d); // BottomRight

                //            g.DrawLine(p, 0 + m.Left + δ, c + m.Top + δ - α, 0 + m.Left + δ, h - m.Bottom - c - δ + α); // VerticalLeft
                //            g.DrawLine(p, w - m.Right - δ + 0.1f, c + m.Top + δ - α, w - m.Right - δ + 0.1f, h - m.Bottom - c - δ + α); // VerticalRight
                //        }
                //    }
            }
            finally
            {
                base.OnPaint(pe);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            //base.OnResize(e);
            //Refresh();
        }
    }
}
