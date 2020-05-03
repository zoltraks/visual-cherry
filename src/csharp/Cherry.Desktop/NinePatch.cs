using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Visual.Cherry.Desktop
{
    [DesignerCategory("")]
    [ToolboxBitmap(typeof(NinePatch), "NinePatch.bmp")]
    [ToolboxItem(true)]
    public partial class NinePatch: UserControl
    {
        public NinePatch()
        {
            InitializeComponent();
            SetupComponent();
        }

        private Visual.Cherry.Common.NinePatch _NinePatch;

        private Bitmap _Bitmap;

        private Image _Image;

        [Bindable(true)]
        [Localizable(true)]
        public Image Image { get => _Image; set => SetImage(value); }

        private void SetImage(Image value)
        {
            if (value == _Image)
            {
                return;
            }
            _NinePatch = null;
            _Bitmap = null;
            _Image = value;
        }

        private Color _BackColor = Color.Transparent;

        [Browsable(true)]
        public override Color BackColor { get => _BackColor; set => SetBackColor(value); }

        private void SetBackColor(Color value)
        {
            if (value == _BackColor)
            {
                return;
            }
            _BackColor = value;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, _BackColor != Color.Transparent);
        }

        private bool _IsNinePatch = true;

        [Browsable(true)]
        [DefaultValue(true)]
        public bool IsNinePatch { get => _IsNinePatch; set => SetNinePatch(value); }

        private void SetNinePatch(bool value)
        {
            if (value == _IsNinePatch)
            {
                return;
            }
            _IsNinePatch = value;
            Invalidate();
        }

        private void SetupComponent()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, _BackColor != Color.Transparent);
            SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.UserPaint, false);
            SetStyle(ControlStyles.ResizeRedraw, true);
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //SetStyle(ControlStyles.Opaque, true);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Name = "NinePatch";
            this.Size = new System.Drawing.Size(50, 50);
            //this.BackColor = Color.Transparent;
            //this.DoubleBuffered = true;

            this.ResumeLayout(false);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        private const int WS_EX_TRANSPARENT = 0x20;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_TRANSPARENT;
                return cp;
            }
        }

        private static void PaintTransparentBackground(Control c, PaintEventArgs e)
        {
            if (c.Parent == null || !Application.RenderWithVisualStyles)
            {
                return;
            }

            ButtonRenderer.DrawParentBackground(e.Graphics, c.ClientRectangle, c);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Call the OnPaint method of the base class.  
            //base.OnPaint(e);
            //PaintTransparentBackground(this, e);

            //e.Graphics.FillRectangle(new SolidBrush(Color.Transparent), this.ClientRectangle);

            if (this.BackColor != Color.Transparent)
            {
                using (var brush = new System.Drawing.SolidBrush(this.BackColor))
                {
                    e.Graphics.FillRectangle(brush, e.ClipRectangle);
                }
            }

            Point zero = new Point(0, 0);

            Rectangle rectangle = new Rectangle(zero, new Size(this.Width, this.Height));

            if (null != Image)
            {
                //e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                //e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                if (_IsNinePatch)
                {
                    if (null != _Bitmap)
                    {
                        if (_Bitmap.Width != this.Width || _Bitmap.Height != this.Height)
                        {
                            _Bitmap = null;
                        }
                    }
                    if (null == _Bitmap)
                    {
                        if (null == _NinePatch)
                        {
                            _NinePatch = new Visual.Cherry.Common.NinePatch(_Image as Bitmap);
                        }
                        _Bitmap = _NinePatch.CreateBitmap(this.Width, this.Height);
                    }
                    e.Graphics.DrawImage(_Bitmap, rectangle, rectangle, GraphicsUnit.Pixel);
                }
                else
                {
                    Image image = this.Image;
                    Rectangle source = new Rectangle(zero, new Size(image.Width, image.Height));
                    e.Graphics.DrawImage(image, rectangle, source, GraphicsUnit.Pixel);
                }
            }

            ////// Declare and instantiate a new pen.
            //Color frameColor = _IsNinePatch ? Color.DarkGreen : Color.Maroon;
            //using (System.Drawing.Pen myPen = new System.Drawing.Pen(frameColor))
            //{
            //    // Set the DashCap to round.
            //    myPen.DashCap = System.Drawing.Drawing2D.DashCap.Round;

            //    // Create a custom dash pattern.
            //    myPen.DashPattern = new float[] { 1.0F, 1.0F, 1.0F, 1.0F };

            //    // Draw an aqua rectangle in the rectangle represented by the control.  
            //    e.Graphics.DrawRectangle(myPen, rectangle);
            //}
        }
    }
}
