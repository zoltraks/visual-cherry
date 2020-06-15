using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace Cherry.Desktop
{
    [DesignerCategory("")]
    [DefaultProperty("FrameColor")]
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    [ToolboxBitmap(typeof(FrameBox), "FrameBox.bmp")]
    [ToolboxItem(true)]
    public partial class FrameBox : UserControl
    {
        /// <summary>
        /// Panel with color frame painted around
        /// </summary>
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

            this.ResumeLayout(false);
        }

        private readonly Pen pen = new Pen(SystemColors.ControlDark);

        /// <summary>
        /// Frame color
        /// </summary>
        [Description("Frame color"), Category("Appearance"), Browsable(true)]
        public Color FrameColor
        {
            get
            {
                return pen.Color;
            }
            set
            {
                pen.Color = value;
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
            base.OnPaint(pe);
            if (FrameColor == Color.Transparent)
            {
                return;
            }
            Graphics g = pe.Graphics;
            int w = Width - 1;
            int h = Height - 1;
            Padding m = FrameMargin;
            int c = _FrameCorner;
            if (c == 0)
            {
                g.DrawRectangle(pen, new Rectangle(m.Left, m.Top, w - m.Left - m.Right, h - m.Top - m.Bottom));
                return;
            }

            int d = c * 2;

            if (this.VerticalScroll.Visible)
            {
                w -= SystemInformation.VerticalScrollBarWidth;
            }

            if (!this.VerticalScroll.Visible)
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

                g.DrawLine(pen, 0 + m.Left, c + m.Top, 0 + m.Left, h - m.Bottom - c);
                g.DrawLine(pen, w - m.Right, c + m.Top, w - m.Right, h - m.Bottom - c);
                g.DrawLine(pen, c + m.Left, 0 + m.Top, w - m.Right - c, 0 + m.Top);
                g.DrawLine(pen, c + m.Left, h - m.Bottom, w - m.Right - c, h - m.Bottom);

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                g.DrawArc(pen, new Rectangle(0 + m.Left, 0 + m.Top, d, d), 180, 90);
                g.DrawArc(pen, new Rectangle(0 + m.Left, h - d - m.Bottom, d, d), 90, 90);
                g.DrawArc(pen, new Rectangle(w - d - m.Right, 0 + m.Top, d, d), 270, 90);
                g.DrawArc(pen, new Rectangle(w - d - m.Right, h - d - m.Bottom, d, d), 0, 90);
            }
            else
            {
                if (VerticalScroll.Value <= VerticalScroll.Minimum)
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

                    g.DrawLine(pen, 0 + m.Left, c + m.Top, 0 + m.Left, h - m.Bottom);
                    g.DrawLine(pen, w - m.Left, c + m.Top, w - m.Right, h - m.Bottom);
                    g.DrawLine(pen, c + m.Left, 0 + m.Top, w - m.Right - c, 0 + m.Top);

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    g.DrawArc(pen, new Rectangle(0 + m.Left, 0 + m.Top, d, d), 180, 90);
                    g.DrawArc(pen, new Rectangle(w - d - m.Right, 0 + m.Top, d, d), 270, 90);
                }
                else if (VerticalScroll.Value + this.Height >= VerticalScroll.Maximum - 1)
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

                    g.DrawLine(pen, 0 + m.Left, m.Top, 0 + m.Left, h - m.Bottom - c);
                    g.DrawLine(pen, w - m.Right, m.Top, w - m.Right, h - m.Bottom - c);
                    g.DrawLine(pen, c + m.Left, h - m.Bottom, w - m.Right - c, h - m.Bottom);

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    g.DrawArc(pen, new Rectangle(0 + m.Left, h - d - m.Bottom, d, d), 90, 90);
                    g.DrawArc(pen, new Rectangle(w - d - m.Right, h - d - m.Bottom, d, d), 0, 90);
                }
                else
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

                    g.DrawLine(pen, 0 + m.Left, m.Top, 0 + m.Left, h - m.Bottom);
                    g.DrawLine(pen, w - m.Right, m.Top, w - m.Right, h - m.Bottom);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Refresh();
        }
    }
}
