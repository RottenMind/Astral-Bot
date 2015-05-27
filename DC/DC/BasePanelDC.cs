using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Astral.Forms;
using Astral.Logic.NW;
using MyNW.Classes;
using MyNW.Internals;

namespace DC
{
    public partial class BasePanelDC : BasePanel
    {
        public BasePanelDC() 
            : base("DC assist")
        {
            InitializeComponent();
        }

        private static Timer _healTimer;
        private static Thread _healThread;
        private static readonly string[] HealPowerNameList = { "Healing Word", "Bastion of Health" };

        private static bool MyGroupStatus()
        {
            return EntityManager.LocalPlayer.PlayerTeam.IsInTeam;
        }
        private static IEnumerable<Entity> GetTeamMembers()
        {
            return EntityManager.GetEntities().Where(entity => entity.PlayerTeam.TeamId.Equals(EntityManager.LocalPlayer.PlayerTeam.TeamId)).ToList();
        }
        private static Power GetPower(string powerName)
        {
            return Powers.GetPowerByDisplayName(powerName);
        }
        private static bool CanUsePower(string powerName)
        {
            return !GetPower(powerName).IsOnCooldown && GetPower(powerName).IsInTray() && GetPower(powerName).CanExec();
        }
        private static bool NeedHeal(Entity teamMember)
        {
            return teamMember.IsValid && !teamMember.IsDead && teamMember.Character.AttribsBasic.HealthPercent < 90
                && teamMember.Location.Distance3DFromPlayer < 80;
        }
        private static void ExecHeal()
        {
            if (!MyGroupStatus()) return;
            foreach (var teamMember in GetTeamMembers())
            {
                var member = teamMember;
                foreach (var healPowerName in HealPowerNameList.Where(healPowerName => NeedHeal(member)).Where(CanUsePower))
                {
                    Combats.Face(teamMember.Location);
                    Thread.Sleep(500);
                    GetPower(healPowerName).CastOnEntity(teamMember, true);
                    Thread.Sleep(500);
                    GetPower(healPowerName).CastOnEntity(teamMember, false);
                }
            }
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            _healTimer = new Timer(CastHeal, null, 1000, Timeout.Infinite);
        }
        private static void CastHeal(object state)
        {
            _healThread = new Thread(ExecHeal);
            _healTimer.Change(1000, Timeout.Infinite);
        }
        private void buttonStop_Click(object sender, EventArgs e)
        {
            _healTimer.Dispose();
            _healThread.Abort();
        }
    }
}
