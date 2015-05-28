using System.Drawing;
using Astral.Addons;
using Astral.Forms;

namespace DC
{
    public class Config : Plugin
    {
        public override string Name
        {
            get { return "DC Plugin"; }
        }

        public override string Author
        {
            get { return "jedzus"; }
        }

        public override Image Icon
        {
            get { return null; }
        }

        public override BasePanel Settings
        {
            get { return new BasePanelDC(); }
        }

        public override void OnLoad()
        {
        }

        public override void OnUnload()
        {
        }

        public override void OnBotStart()
        {
        }

        public override void OnBotStop()
        {
        }
    }
}