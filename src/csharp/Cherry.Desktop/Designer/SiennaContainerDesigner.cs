using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;

namespace Cherry.Desktop.Designer
{
    public class SiennaContainerDesigner : ParentControlDesigner
    {
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);

            if (this.Control is SiennaSimpleContainer)
            {
                this.EnableDesignMode(((SiennaSimpleContainer)this.Control).Panel1, "Panel1");
                this.EnableDesignMode(((SiennaSimpleContainer)this.Control).Panel2, "Panel2");
            }
        }

        public enum SideAlignment
        {
            Right,
            Left
        }

        protected override void PreFilterProperties(
            System.Collections.IDictionary properties)
        {
            //properties.Remove("Dock");

            base.PreFilterProperties(properties);
        }
    }
}
