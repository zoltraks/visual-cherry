using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Cherry.Desktop
{
    /// <summary>
    /// Sienna
    /// </summary>
    [ToolboxBitmap(typeof(SiennaSplit), "SiennaSplit.bmp")]
    public partial class SiennaSplit : UserControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SiennaSplit()
        {
            InitializeComponent();
            Sienna_Load(null, null);
        }

        /// <summary>
        /// OnPaintBackground
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // do nothing
        }

        ///// <summary>
        ///// Size
        ///// </summary>
        //[Browsable(true)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public new Size Size
        //{
        //    get
        //    {
        //        return base.Size;
        //    }
        //    set
        //    {
        //        base.Size = value;
        //    }
        //}

        /// <summary>THICKNESS_TINY</summary>
        public int THICKNESS_TINY = 3;

        /// <summary>THICKNESS_MEDIUM</summary>
        public int THICKNESS_MEDIUM = 5;

        /// <summary>THICKNESS_LARGE</summary>
        public int THICKNESS_LARGE = 7;

        private bool Horizontal
        {
            get
            {
                return this.Dock == DockStyle.Top || this.Dock == DockStyle.Bottom;
            }
        }

        private bool Vertical
        {
            get
            {
                return !Horizontal;
            }
        }

        /// <summary>
        /// Get line size
        /// </summary>
        /// <returns>int</returns>
        public int GetLineSize()
        {
            if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
            {
                return this.Width;
            }
            if (this.Dock == DockStyle.Top || this.Dock == DockStyle.Bottom)
            {
                return this.Height;
            }
            return this.Width < this.Height ? this.Width : this.Height;
        }

        /// <summary>
        /// SetLineSize
        /// </summary>
        /// <param name="size">int</param>
        public void SetLineSize(int size)
        {
            if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
            {
                this.Size = new Size(size, this.Height);
            }
            if (this.Dock == DockStyle.Top || this.Dock == DockStyle.Bottom)
            {
                this.Size = new Size(this.Width, size);
            }
            if (this.Width < this.Height)
            {
                this.Size = new Size(size, this.Height);
            }
            else
            {
                this.Size = new Size(this.Width, size);
            }
            Invalidate();
        }

        /// <summary>
        /// Line
        /// </summary>
        [Category("Appearance"), Browsable(true)]
        [Description("Splitter line thickness")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Splitter.Thickness Thick
        {
            get
            {
                int size = GetLineSize();
                if (size == THICKNESS_TINY) return Splitter.Thickness.Tiny;
                if (size == THICKNESS_MEDIUM) return Splitter.Thickness.Medium;
                if (size == THICKNESS_LARGE) return Splitter.Thickness.Large;
                return Splitter.Thickness.None;
            }
            set
            {
                switch (value)
                {
                    default:
                    case Splitter.Thickness.None: return;
                    case Splitter.Thickness.Tiny: SetLineSize(THICKNESS_TINY); break;
                    case Splitter.Thickness.Medium: SetLineSize(THICKNESS_MEDIUM); break;
                    case Splitter.Thickness.Large: SetLineSize(THICKNESS_LARGE); break;
                }
            }
        }

        /// <summary>
        /// Back color when disabled
        /// </summary>
        private Color disableColor = SystemColors.Control;

        /// <summary>
        /// Back color when disabled
        /// </summary>
        [Description("Back color when disabled"), Category("Appearance"), Browsable(true)]
        public Color DisableColor
        {
            get
            {
                return disableColor;
            }
            set
            {
                disableColor = value;
                Refresh();
            }
        }

        public int Weight
        {
            get
            {
                return GetLineSize();
            }
        }

        private Color back;
        private Color transition;
        private Color start;

        private Timer timer = new Timer();
        private double p;
        private int s = 15;

        private void Sienna_Load(object sender, EventArgs e)
        {
            back = this.BackColor;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 50;
            this.Paint += new PaintEventHandler(Sienna_Paint);
        }

        private void Sienna_MouseEnter(object sender, EventArgs e)
        {
            Start(Color.MediumSeaGreen);
        }

        private void Sienna_MouseLeave(object sender, EventArgs e)
        {
            Start(back);
        }

        private void Start(Color color)
        {
            transition = color;
            start = this.BackColor;
            p = 0;
            timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!this.Visible) timer.Enabled = false;

            if ((p += (double)s / 100) > 1.0) p = 1.0;

            int a, r, g, b;

            a = (int)(start.A * (1 - p) + p * transition.A);
            r = (int)(start.R * (1 - p) + p * transition.R);
            g = (int)(start.G * (1 - p) + p * transition.G);
            b = (int)(start.B * (1 - p) + p * transition.B);

            Color color = Color.FromArgb(a, r, g, b);

            this.BackColor = color;

            if (p == 1.0) timer.Enabled = false;
        }

        private void Sienna_Paint(object sender, PaintEventArgs e)
        {
            int w = GetLineSize();

            Graphics graphics = e.Graphics;
            Rectangle rectangle = this.ClientRectangle;

            if (!this.Enabled)
            {
                graphics.FillRectangle(new SolidBrush(disableColor), rectangle);
            }
            else if (w < THICKNESS_MEDIUM)
            {
                graphics.FillRectangle(new SolidBrush(this.BackColor), rectangle);
            }
            else
            {
                int x = (w - 1) / 2;

                for (int i = 0; i <= x; i++)
                {
                    double p = i / (double)x;
                    Pen pen = new Pen(Cherry.Common.Utility.ColorTransition(this.BackColor, SystemColors.Control, p));
                    if (Horizontal)
                    {
                        graphics.DrawLine(pen, rectangle.Left, rectangle.Top + i, rectangle.Right - 1, rectangle.Top + i);
                        graphics.DrawLine(pen, rectangle.Left, rectangle.Bottom - 1 - i, rectangle.Right - 1, rectangle.Bottom - 1 - i);
                    }
                    else
                    {
                        graphics.DrawLine(pen, rectangle.Left + i, rectangle.Top, rectangle.Left + i, rectangle.Bottom);
                        graphics.DrawLine(pen, rectangle.Right - 1 - i, rectangle.Top, rectangle.Right - 1 - i, rectangle.Bottom);
                    }
                }
            }
        }
    }
}
