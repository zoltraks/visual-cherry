using System.Diagnostics;
using System.Windows.Forms.Design;

namespace Cherry.Desktop.Designer
{
    public class PanelClientDesigner : ParentControlDesigner
    {

        public string[] LandingCandidates => new string[]
        {
            "PanelClient",
            "panelClient",
            "Panel",
            "Client",
            "panel1",
        };

        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);

            //this.EnableDesignMode((this.Control as PanelBox)?.Panel, "Panel");

            foreach (string name in LandingCandidates)
            {
                object box = Cherry.Common.Class.GetElementValue(this.Control, name, false, true);

                if (null == box)
                {
                    continue;
                }

                if (box is System.Windows.Forms.Panel)
                {
                    this.EnableDesignMode((System.Windows.Forms.Panel)box, name);
                    Debug.WriteLine($"XXX {name}");
                    break;
                }
            }
        }

    }
}
