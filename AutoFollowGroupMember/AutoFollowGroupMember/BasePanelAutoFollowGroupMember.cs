using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Astral.Forms;
using MyNW.Classes;
using MyNW.Internals;
using Timer = System.Threading.Timer;
using TimerWF = System.Windows.Forms.Timer;

namespace AutoFollowGroupMember
{
    public partial class BasePanelAutoFollowGroupMember : BasePanel
    {
        private Timer _followGroupMemberTimertimer;

        public BasePanelAutoFollowGroupMember()
            : base("Auto Follow")
        {
            InitializeComponent();
            FirstUpdateComboBoxGroupMember();
            UpdateComboBoxGroupMember();
        }

        private void FirstUpdateComboBoxGroupMember()
        {
            foreach (var entity in GetGroupMembersList())
            {
                comboBoxGroupMember.Items.Add(entity.Name);
            }
        }

        private static IEnumerable<Entity> GetGroupMembersList()
        {
            return
                EntityManager.GetEntities()
                    .Where(
                        entity =>
                            entity.PlayerTeam.TeamId.Equals(EntityManager.LocalPlayer.PlayerTeam.TeamId) &&
                            entity.Name != EntityManager.LocalPlayer.Name)
                    .ToList();
        }

        private void UpdateComboBoxGroupMember()
        {
            var timer = new TimerWF();
            timer.Tick += timer_Tick;
            timer.Interval = 30000;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (
                var entity in GetGroupMembersList().Where(entity => !comboBoxGroupMember.Items.Contains(entity.Name)))
            {
                comboBoxGroupMember.Items.Add(entity.Name);
            }
        }

        private Vector3 GetGroupMemberToFollowLocation()
        {
            if (comboBoxGroupMember.SelectedItem == null)
                throw new Exception("Chose group member!");
            var groupMemberToFollowLocation = Vector3.Empty;
            foreach (
                var entity in
                    GetGroupMembersList().Where(entity => entity.Name == comboBoxGroupMember.SelectedItem.ToString()))
            {
                groupMemberToFollowLocation = entity.Location;
            }
            return groupMemberToFollowLocation;
        }

        private void FollowGroupMember(object state)
        {
            if (GetGroupMemberToFollowLocation() == null)
                throw new Exception("Chose a group member!");
            Movements.NavToPos(GetGroupMemberToFollowLocation(), 3f);
            _followGroupMemberTimertimer.Change(500, Timeout.Infinite);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                _followGroupMemberTimertimer = new Timer(FollowGroupMember, null, 1000, Timeout.Infinite);
            }
            catch (Exception exception)
            {
                _followGroupMemberTimertimer.Dispose();
                MessageBox.Show(exception.Message, "Auto Follow Group Member");
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _followGroupMemberTimertimer.Dispose();
        }
    }
}