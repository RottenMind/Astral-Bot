using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Astral.Forms;
using Astral.Logic.NW;
using MyNW.Classes;
using MyNW.Internals;

namespace DCHeal
{
    public partial class BasePanelDCHeal : BasePanel
    {
        public BasePanelDCHeal()
            : base("DC Heal")
        {
            InitializeComponent();
        }

        private Timer _healTimer;

        private static bool CheckGroupStatus(Entity entity)
        {
            return entity.PlayerTeam.Team.MembersCount > 0;
        }

        private static List<Entity> GetGroupMembers()
        {
            var groupMembersList = new List<Entity>();
            if (CheckGroupStatus(EntityManager.LocalPlayer))
            {
                groupMembersList.AddRange(EntityManager.GetEntities().Where(entity => entity.PlayerTeam.TeamId.Equals(EntityManager.LocalPlayer.PlayerTeam.TeamId)));
            }
            return groupMembersList;
        }

        private static Power GetPower(string healPower)
        {
            return Powers.GetPowerByDisplayName(healPower);
        }

        private static bool CheckAvailabilityOfPower(string power)
        {
            return !GetPower(power).IsOnCooldown && GetPower(power).IsInTray() && GetPower(power).CanExec();
        }

        private static bool DoesGroupMemberNeedHeal(Entity groupMember)
        {
            return groupMember.Character.AttribsBasic.HealthPercent < 90;
        }

        private static void ExecHealPowerOnMember(Entity entity)
        {
            if (DoesGroupMemberNeedHeal(entity) && CheckAvailabilityOfPower("Healing Word"))
                GetPower("Healing Word").CastOnEntity(entity, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _healTimer = new Timer(ExecHealPower, null, 1000, Timeout.Infinite);
        }

        private void ExecHealPower(object state)
        {
            foreach (var groupMember in GetGroupMembers())
            {
                ExecHealPowerOnMember(groupMember);
            }
            _healTimer.Change(500, Timeout.Infinite);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _healTimer.Dispose();
        }
    }
}
