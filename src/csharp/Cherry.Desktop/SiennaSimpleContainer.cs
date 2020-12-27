using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace Cherry.Desktop
{
    [Designer(typeof(Designer.SiennaContainerDesigner))]
    public partial class SiennaSimpleContainer : UserControl
    {
        public SiennaSimpleContainer()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Designer.SiennaContainerDesigner.SideAlignment SideAlignment
        {
            get
            {
                return panel2.Dock == DockStyle.Right
                    ? Designer.SiennaContainerDesigner.SideAlignment.Right
                    : Designer.SiennaContainerDesigner.SideAlignment.Left;
            }
            set
            {
                switch (value)
                {
                    case Designer.SiennaContainerDesigner.SideAlignment.Left:
                        panel2.Dock = DockStyle.Left;
                        siennaSplit.Dock = DockStyle.Left;
                        break;
                    case Designer.SiennaContainerDesigner.SideAlignment.Right:
                        panel2.Dock = DockStyle.Right;
                        siennaSplit.Dock = DockStyle.Right;
                        break;
                }
            }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel Panel1
        {
            get
            {
                return this.panel1;
            }
            set
            {
                this.panel1 = value;
            }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel Panel2
        {
            get
            {
                return this.panel2;
            }
            set
            {
                this.panel2 = value;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int SideSize
        {
            get
            {
                return panel2.Width;
            }
            set
            {
                panel2.Width = value;
            }
        }

        public int SplitterWeight
        {
            get
            {
                return siennaSplit.Weight;
            }
        }

        public Splitter.Thickness SplitterThick
        {
            get
            {
                return siennaSplit.Thick;
            }
            set
            {
                siennaSplit.Thick = value;
            }
        }
    }
}
