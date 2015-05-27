using System.Drawing;
using Astral.Addons;
using Astral.Forms;

namespace AutoFollow
{
    public class Config : Plugin
    {
        public override string Name
        {
            get { return "Auto Follow"; }
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
            get { return new BasePanelAutoFollow(); }
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