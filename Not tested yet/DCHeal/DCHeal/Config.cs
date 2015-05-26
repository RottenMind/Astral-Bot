using System.Drawing;
using Astral.Addons;
using Astral.Forms;

namespace DCHeal
{
    public class Config : Plugin
    {
        public override void OnLoad()
        {
            //
        }

        public override void OnUnload()
        {
            //
        }

        public override void OnBotStart()
        {
            //
        }

        public override void OnBotStop()
        {
            //
        }

        public override string Name
        {
            get { return "DC Heal"; }
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
            get { return new BasePanelDCHeal();}
        }
    }
}
