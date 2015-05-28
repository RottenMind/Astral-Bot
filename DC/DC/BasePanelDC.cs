using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Astral.Forms;
using Astral.Logic.NW;
using MyNW.Classes;
using MyNW.Internals;
using Movements = MyNW.Internals.Movements;

namespace DC
{
    public partial class BasePanelDC : BasePanel
    {
        private static bool _killHealThread;
        private static Thread _healThread;

        private static readonly Power[] HealPowersList =
        {
            Powers.GetPowerByDisplayName("Healing Word"),
            Powers.GetPowerByDisplayName("Bastion of Health")
        };

        public BasePanelDC()
            : base("DC assist")
        {
            InitializeComponent();
        }

        private static bool MyGroupStatus()
        {
            return EntityManager.LocalPlayer.PlayerTeam.IsInTeam;
        }

        private static IEnumerable<Entity> GetTeamMembers()
        {
            return
                EntityManager.GetEntities()
                    .Where(entity => entity.PlayerTeam.TeamId.Equals(EntityManager.LocalPlayer.PlayerTeam.TeamId))
                    .ToList();
        }

        private static bool CanUsePower(Power healPower)
        {
            return healPower.IsInTray() && healPower.CanExec() && healPower.ChargesUsed < 3;
        }

        private static bool NeedHeal(Entity entity)
        {
            return entity.Character.AttribsBasic.HealthPercent < 90 && entity.Location.Distance3DFromPlayer < 50;
        }

        private static void ExecHeal()
        {
            if (!MyGroupStatus()) return;
            try
            {
                while (!_killHealThread)
                {
                    foreach (var teamMember in GetTeamMembers())
                    {
                        var member = teamMember;
                        foreach (var healPower in HealPowersList.Where(healPower => NeedHeal(member)).Where(CanUsePower)
                            )
                        {
                            Movements.NavToPos(teamMember.Location);
                            Thread.Sleep(500);
                            if (EntityManager.LocalPlayer.Character.CurrentTarget != teamMember)
                                teamMember.Location.Face();
                            Thread.Sleep(500);
                            Powers.ExecPower(healPower, teamMember, true);
                            Thread.Sleep(500);
                            Powers.ExecPower(healPower, teamMember, false);
                            Thread.Sleep(2000);
                        }
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            _killHealThread = false;
            _healThread = new Thread(ExecHeal);
            _healThread.Start();
            while (!_healThread.IsAlive)
            {
            }
            Thread.Sleep(1);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _killHealThread = true;
        }
    }
}