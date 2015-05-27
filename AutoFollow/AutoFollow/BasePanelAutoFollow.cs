﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Astral.Forms;
using MyNW.Classes;
using MyNW.Internals;
using Timer = System.Threading.Timer;

namespace AutoFollow
{
    public partial class BasePanelAutoFollow : BasePanel
    {
        private static bool _killFollowThread;
        private Thread _followThread;
        private Timer _followTimer;

        public BasePanelAutoFollow()
            : base("Auto Follow")
        {
            InitializeComponent();
            FirstUpdateComboBoxTeam();
            UpdateComboBoxTeam();
        }

        private static bool MyGroupStatus()
        {
            return EntityManager.LocalPlayer.PlayerTeam.IsInTeam;
        }

        private static IEnumerable<Entity> GetTeamMembers()
        {
            return
                EntityManager.GetEntities()
                    .Where(entity => entity.PlayerTeam.TeamId.Equals(EntityManager.LocalPlayer.PlayerTeam.TeamId)
                                     && entity.Name != EntityManager.LocalPlayer.Name).ToList();
        }

        private void FirstUpdateComboBoxTeam()
        {
            if (!MyGroupStatus()) return;
            foreach (var teamMember in GetTeamMembers())
            {
                comboBoxTeam.Items.Add(teamMember.Name);
            }
        }

        private void UpdateComboBoxTeam()
        {
            var timer = new System.Windows.Forms.Timer();
            timer.Tick += TimerOnTickAddTeamNameToComboBoxTeam;
            timer.Interval = 1000;
            timer.Start();
        }

        private void TimerOnTickAddTeamNameToComboBoxTeam(object sender, EventArgs eventArgs)
        {
            if (!MyGroupStatus())
            {
                comboBoxTeam.SelectedItem = null;
                comboBoxTeam.Items.Clear();
                return;
            }

            foreach (
                var teamMember in GetTeamMembers().Where(teamMember => !comboBoxTeam.Items.Contains(teamMember.Name)))
            {
                comboBoxTeam.Items.Add(teamMember.Name);
            }
            //to do
            //clear player name from combo box when player leaves team
        }

        private void FollowTeamMember()
        {
            if (MyGroupStatus())
            {
                try
                {
                    while (!_killFollowThread)
                    {
                        foreach (
                            var teamMember in
                                GetTeamMembers()
                                    .Where(teamMember => teamMember.Name.Equals(comboBoxTeam.SelectedItem.ToString())))
                        {
                            Movements.NavToPos(teamMember.Location, 3f);
                            _followTimer.Change(500, Timeout.Infinite);
                        }
                    }
                }
                catch (ThreadAbortException)
                {
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Chose teammate!", "Auto Follow Plugin");
                    _killFollowThread = true;
                }
            }
            else
                MessageBox.Show("You are not in team!", "Auto Follow Plugin");
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            _killFollowThread = false;
            _followTimer = new Timer(Follow, null, 1000, Timeout.Infinite);
        }

        private void Follow(object state)
        {
            _followThread = new Thread(FollowTeamMember);
            _followThread.Start();
            while (!_followThread.IsAlive)
            {
            }
            Thread.Sleep(1);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _killFollowThread = true;
        }
    }
}