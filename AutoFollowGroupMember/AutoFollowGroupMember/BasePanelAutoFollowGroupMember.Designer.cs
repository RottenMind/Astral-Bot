namespace AutoFollowGroupMember
{
    partial class BasePanelAutoFollowGroupMember
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelGroupMember = new System.Windows.Forms.Label();
            this.comboBoxGroupMember = new System.Windows.Forms.ComboBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelGroupMember
            // 
            this.labelGroupMember.AutoSize = true;
            this.labelGroupMember.Location = new System.Drawing.Point(4, 20);
            this.labelGroupMember.Name = "labelGroupMember";
            this.labelGroupMember.Size = new System.Drawing.Size(107, 13);
            this.labelGroupMember.TabIndex = 0;
            this.labelGroupMember.Text = "Chose group member";
            // 
            // comboBoxGroupMember
            // 
            this.comboBoxGroupMember.FormattingEnabled = true;
            this.comboBoxGroupMember.Location = new System.Drawing.Point(131, 17);
            this.comboBoxGroupMember.Name = "comboBoxGroupMember";
            this.comboBoxGroupMember.Size = new System.Drawing.Size(204, 21);
            this.comboBoxGroupMember.TabIndex = 1;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(131, 60);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(259, 60);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 3;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // BasePanelAutoFollowGroupMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.comboBoxGroupMember);
            this.Controls.Add(this.labelGroupMember);
            this.Name = "BasePanelAutoFollowGroupMember";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelGroupMember;
        private System.Windows.Forms.ComboBox comboBoxGroupMember;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
    }
}
