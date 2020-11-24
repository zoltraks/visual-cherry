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
    [DefaultProperty("FrameColor")]
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    [ToolboxBitmap(typeof(FrameBox), "FrameBox.bmp")]
    [ToolboxItem(true)]
    public partial class FrameBox : UserControl
    {
        public FrameBox()
        {
            SetupComponent();
        }

        private void SetupComponent()
        {
            this.SuspendLayout();

            this.ForeColor = SystemColors.ControlText;
            this.Name = "FrameBox";
            this.Padding = new Padding(3);
            this.Size = new Size(195, 152);
            this.Scroll += new ScrollEventHandler(this.FrameBox_Scroll);
            this.ControlAdded += new ControlEventHandler(this.FrameBox_ControlAdded);

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);

            this.ResumeLayout(false);
        }

        //private Pen pen = new Pen(SystemColors.ControlDark, 1);

        private Color _FrameColor = SystemColors.ControlDark;

        /// <summary>
        /// Frame color
        /// </summary>
        [Description("Frame color"), Category("Appearance"), Browsable(true)]
        public Color FrameColor
        {
            get
            {
                return _FrameColor;
            }
            set
            {
                _FrameColor = value;
                Refresh();
            }
        }

        private int _FrameCorner = 5;

        /// <summary>
        /// Frame corner size
        /// </summary>
        [Description("Frame corner size"), Category("Appearance"), Browsable(true)]
        public int FrameCorner
        {
            get
            {
                return _FrameCorner;
            }
            set
            {
                _FrameCorner = value;
                Refresh();
            }
        }

        private int _FrameWidth = 1;

        /// <summary>
        /// Frame color
        /// </summary>
        [Description("Frame width"), Category("Appearance"), Browsable(true)]
        public int FrameWidth
        {
            get
            {
                return _FrameWidth;
            }
            set
            {
                if (value < 0)
                {
                    value = 1;
                }
                _FrameWidth = value;
                Refresh();
            }
        }

        private Padding _FrameMargin = new Padding(0);

        /// <summary>
        /// Frame margin
        /// </summary>
        [Description("Frame margin"), Category("Appearance"), Browsable(true), DefaultValue(0)]
        public Padding FrameMargin
        {
            get
            {
                return _FrameMargin;
            }
            set
            {
                _FrameMargin = value;
                Refresh();
            }
        }

        private Color _FillColor = Color.Transparent;

        /// <summary>
        /// Frame color
        /// </summary>
        [Description("Fill color"), Category("Appearance"), Browsable(true)]
        public Color FillColor
        {
            get
            {
                return _FillColor;
            }
            set
            {
                _FillColor = value;
                Refresh();
            }
        }

        private void FrameBox_ControlAdded(object sender, ControlEventArgs e)
        {
            SuspendLayout();
            Controls.Add(e.Control);
            //Controls.SetChildIndex(e.Control, 0);
            ResumeLayout();
        }

        private void FrameBox_Scroll(object sender, ScrollEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Overriden paint event that draws frame
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            try
            {
                Graphics g = pe.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                Brush b;

                b = new SolidBrush(BackColor);
                g.FillRectangle(b, 0, 0, Width, Height);

                if (_FillColor != Color.Transparent && _FillColor != BackColor)
                {
                    b = new SolidBrush(_FillColor);
                    if (_FrameColor == Color.Transparent || _FrameWidth == 0)
                    {
                        g.FillRectangle(b
                            , 0 + _FrameWidth + _FrameMargin.Left
                            , 0 + _FrameWidth + _FrameMargin.Top
                            , Width - _FrameWidth - _FrameWidth - _FrameMargin.Right - _FrameMargin.Left
                            , Height - _FrameWidth - _FrameWidth - _FrameMargin.Bottom - _FrameMargin.Top
                            );
                        return;
                    }
                    g.FillRectangle(b
                        , 0 + _FrameWidth + _FrameMargin.Left + _FrameCorner
                        , 0 + _FrameWidth + _FrameMargin.Top
                        , Width - _FrameWidth - _FrameWidth - _FrameMargin.Right - _FrameMargin.Left - _FrameCorner - _FrameCorner
                        , Height - _FrameWidth - _FrameWidth - _FrameMargin.Bottom - _FrameMargin.Top
                        );
                    if (_FrameCorner > 0)
                    {
                        g.FillRectangle(b
                            , 0 + _FrameWidth + _FrameMargin.Left
                            , 0 + _FrameWidth + _FrameMargin.Top + _FrameCorner
                            , _FrameCorner
                            , Height - _FrameWidth - _FrameWidth - _FrameMargin.Bottom - _FrameMargin.Top - _FrameCorner - _FrameCorner
                        );
                        g.FillRectangle(b
                            , Width - _FrameWidth - _FrameMargin.Left - _FrameCorner
                            , 0 + _FrameWidth + _FrameMargin.Top + _FrameCorner
                            , _FrameCorner
                            , Height - _FrameWidth - _FrameWidth - _FrameMargin.Bottom - _FrameMargin.Top - _FrameCorner - _FrameCorner
                        );
                    }
                }

                if (_FrameColor == Color.Transparent || _FrameWidth == 0)
                {
                    return;
                }

                int w = Width - 1;
                int h = Height - 1;
                Padding m = FrameMargin;
                int c = _FrameCorner;
                Pen p = new Pen(_FrameColor, _FrameWidth);

                if (c == 0)
                {
                    if (_FrameWidth > 1)
                    {
                        w++;
                        h++;
                    }
                    p.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    g.DrawRectangle(p, new Rectangle(m.Left, m.Top, w - m.Left - m.Right, h - m.Top - m.Bottom));
                    return;
                }

                int d = c * 2;

                if (this.VerticalScroll.Visible)
                {
                    w -= SystemInformation.VerticalScrollBarWidth;
                }

                float δ = _FrameWidth / 2.0f;
                int α = _FrameCorner % 2;
                int γ = _FrameWidth / 2;
                int ε = _FrameWidth - 1;

                if (!this.VerticalScroll.Visible)
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    p.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                    g.FillPie(b, new Rectangle(0 + m.Left + γ, 0 + m.Top + γ, d + ε + 1, d + ε + 1), 180, 90); // TopLeft
                    g.FillPie(b, new Rectangle(w - m.Right - γ - d - ε - 1, 0 + m.Top + γ, d + ε + 1, d + ε + 1), 270, 90); // TopRight
                    g.FillPie(b, new Rectangle(0 + m.Left + γ, h - m.Bottom - γ - d - ε - 1, d + ε + 1, d + ε + 1), 90, 90); // BottomLeft
                    g.FillPie(b, new Rectangle(w - m.Right - γ - d - ε - 1, h - m.Bottom - γ - d - ε - 1, d + ε + 1, d + ε + 1), 0, 90); // BottomLeft

                    g.DrawLine(p, 0 + m.Left + δ, c + m.Top + δ - α, 0 + m.Left + δ, h - m.Bottom - c - δ + α); // VerticalLeft
                    g.DrawLine(p, w - m.Right - δ + 0.1f, c + m.Top + δ - α, w - m.Right - δ + 0.1f, h - m.Bottom - c - δ + α); // VerticalRight
                    g.DrawLine(p, c + m.Left + δ - α, 0 + m.Top + δ, w - m.Right - c - δ + α, 0 + m.Top + δ); // HorizontalTop
                    g.DrawLine(p, c + m.Left + δ - α, h - m.Bottom - δ + 0.1f, w - m.Right - c - δ + α, h - m.Bottom - δ + 0.1f); // HorizontalBottom

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    p.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                    g.DrawArc(p, new Rectangle(0 + m.Left + γ, 0 + m.Top + γ, d, d), 180, 90); // TopLeft
                    g.DrawArc(p, new Rectangle(w - d - m.Right - γ, 0 + m.Top + γ, d, d), 270, 90); // TopRight
                    g.DrawArc(p, new Rectangle(0 + m.Left + γ, h - d - m.Bottom - γ, d, d), 90, 90); // BottomLeft
                    g.DrawArc(p, new Rectangle(w - d - m.Right - γ, h - d - m.Bottom - γ, d, d), 0, 90); // BottomRight
                }
                else
                {
                    if (VerticalScroll.Value <= VerticalScroll.Minimum)
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                        p.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                        g.FillPie(b, new Rectangle(0 + m.Left + γ, 0 + m.Top + γ, d + ε + 1, d + ε + 1), 180, 90); // TopLeft
                        g.FillPie(b, new Rectangle(w - m.Right - γ - d - ε - 1, 0 + m.Top + γ, d + ε + 1, d + ε + 1), 270, 90); // TopRight

                        g.FillRectangle(b, 0 + m.Left, Height - m.Bottom - d, d, d); // BottomLeft
                        g.FillRectangle(b, Width - m.Right - d, Height - m.Bottom - d, d, d); // BottomRight

                        g.DrawLine(p, 0 + m.Left + δ, c + m.Top + δ - α, 0 + m.Left + δ, h - m.Bottom - c - δ + α); // VerticalLeft
                        g.DrawLine(p, w - m.Right - δ + 0.1f, c + m.Top + δ - α, w - m.Right - δ + 0.1f, h - m.Bottom - c - δ + α); // VerticalRight
                        g.DrawLine(p, c + m.Left + δ - α, 0 + m.Top + δ, w - m.Right - c - δ + α, 0 + m.Top + δ); // HorizontalTop

                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        p.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                        g.DrawArc(p, new Rectangle(0 + m.Left + γ, 0 + m.Top + γ, d, d), 180, 90); // TopLeft
                        g.DrawArc(p, new Rectangle(w - d - m.Right - γ, 0 + m.Top + γ, d, d), 270, 90); // TopRight
                    }
                    else if (VerticalScroll.Value + this.Height >= VerticalScroll.Maximum - 1)
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                        p.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                        g.FillPie(b, new Rectangle(0 + m.Left + γ, h - m.Bottom - γ - d - ε - 1, d + ε + 1, d + ε + 1), 90, 90); // BottomLeft
                        g.FillPie(b, new Rectangle(w - m.Right - γ - d - ε - 1, h - m.Bottom - γ - d - ε - 1, d + ε + 1, d + ε + 1), 0, 90); // BottomLeft

                        g.FillRectangle(b, 0 + m.Left, 0 + m.Top, d, d); // TopLeft
                        g.FillRectangle(b, Width - m.Right - d, 0 + m.Top, d, d); // TopRight

                        g.DrawLine(p, 0 + m.Left + δ, c + m.Top + δ - α, 0 + m.Left + δ, h - m.Bottom - c - δ + α); // VerticalLeft
                        g.DrawLine(p, w - m.Right - δ + 0.1f, c + m.Top + δ - α, w - m.Right - δ + 0.1f, h - m.Bottom - c - δ + α); // VerticalRight
                        g.DrawLine(p, c + m.Left + δ - α, h - m.Bottom - δ + 0.1f, w - m.Right - c - δ + α, h - m.Bottom - δ + 0.1f); // HorizontalBottom

                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        p.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                        g.DrawArc(p, new Rectangle(0 + m.Left + γ, h - d - m.Bottom - γ, d, d), 90, 90); // BottomLeft
                        g.DrawArc(p, new Rectangle(w - d - m.Right - γ, h - d - m.Bottom - γ, d, d), 0, 90); // BottomRight
                    }
                    else
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                        p.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                        g.FillRectangle(b, 0 + m.Left, 0 + m.Top, d, d); // TopLeft
                        g.FillRectangle(b, Width - m.Right - d, 0 + m.Top, d, d); // TopRight
                        g.FillRectangle(b, 0 + m.Left, Height - m.Bottom - d, d, d); // BottomLeft
                        g.FillRectangle(b, Width - m.Right - d, Height - m.Bottom - d, d, d); // BottomRight

                        g.DrawLine(p, 0 + m.Left + δ, c + m.Top + δ - α, 0 + m.Left + δ, h - m.Bottom - c - δ + α); // VerticalLeft
                        g.DrawLine(p, w - m.Right - δ + 0.1f, c + m.Top + δ - α, w - m.Right - δ + 0.1f, h - m.Bottom - c - δ + α); // VerticalRight
                    }
                }
            }
            finally
            {
                base.OnPaint(pe);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Refresh();
        }
    }
}
