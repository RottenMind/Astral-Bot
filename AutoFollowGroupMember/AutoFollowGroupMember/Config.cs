using System.Drawing;
using Astral.Addons;
using Astral.Forms;

namespace AutoFollowGroupMember
{
    public class Config : Plugin
    {
        public override string Name
        {
            get { return "Auto Follow Group Member"; }
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
            get { return new BasePanelAutoFollowGroupMember(); }
        }

        public override void OnLoad()
        {
            // not implemented
        }

        public override void OnUnload()
        {
            // not implemented
        }

        public override void OnBotStart()
        {
            // not implemented
        }

        public override void OnBotStop()
        {
            // not implemented
        }
    }
}