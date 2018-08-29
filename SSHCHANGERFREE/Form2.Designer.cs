namespace SSHCHANGERFREE
{
    partial class Form2
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.gbSSH = new System.Windows.Forms.GroupBox();
            this.txtLinkssh = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbSSHserver = new System.Windows.Forms.RadioButton();
            this.rbSSHfile = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbChangessh = new System.Windows.Forms.GroupBox();
            this.rbAuto247 = new System.Windows.Forms.RadioButton();
            this.nmrScheduled = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.rbAutobytime = new System.Windows.Forms.RadioButton();
            this.rbManual = new System.Windows.Forms.RadioButton();
            this.gbOther = new System.Windows.Forms.GroupBox();
            this.cblocation = new System.Windows.Forms.CheckBox();
            this.cbhidebvs = new System.Windows.Forms.CheckBox();
            this.cbLoopssh = new System.Windows.Forms.CheckBox();
            this.cbSavessh = new System.Windows.Forms.CheckBox();
            this.cbRandomssh = new System.Windows.Forms.CheckBox();
            this.gbWhoer = new System.Windows.Forms.GroupBox();
            this.cbcheckbl = new System.Windows.Forms.CheckBox();
            this.cbcheckproxy = new System.Windows.Forms.CheckBox();
            this.cbShowinfo = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.gbSSH.SuspendLayout();
            this.gbChangessh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrScheduled)).BeginInit();
            this.gbOther.SuspendLayout();
            this.gbWhoer.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSSH
            // 
            this.gbSSH.Controls.Add(this.txtLinkssh);
            this.gbSSH.Controls.Add(this.label1);
            this.gbSSH.Controls.Add(this.rbSSHserver);
            this.gbSSH.Controls.Add(this.rbSSHfile);
            this.gbSSH.Location = new System.Drawing.Point(13, 13);
            this.gbSSH.Name = "gbSSH";
            this.gbSSH.Size = new System.Drawing.Size(200, 100);
            this.gbSSH.TabIndex = 0;
            this.gbSSH.TabStop = false;
            this.gbSSH.Text = "Import SSH";
            // 
            // txtLinkssh
            // 
            this.txtLinkssh.Enabled = false;
            this.txtLinkssh.Location = new System.Drawing.Point(44, 68);
            this.txtLinkssh.Name = "txtLinkssh";
            this.txtLinkssh.Size = new System.Drawing.Size(150, 20);
            this.txtLinkssh.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Link:";
            // 
            // rbSSHserver
            // 
            this.rbSSHserver.AutoSize = true;
            this.rbSSHserver.Location = new System.Drawing.Point(7, 44);
            this.rbSSHserver.Name = "rbSSHserver";
            this.rbSSHserver.Size = new System.Drawing.Size(82, 17);
            this.rbSSHserver.TabIndex = 1;
            this.rbSSHserver.Text = "From Server";
            this.rbSSHserver.UseVisualStyleBackColor = true;
            this.rbSSHserver.CheckedChanged += new System.EventHandler(this.rbSSHserver_CheckedChanged);
            // 
            // rbSSHfile
            // 
            this.rbSSHfile.AutoSize = true;
            this.rbSSHfile.Checked = true;
            this.rbSSHfile.Location = new System.Drawing.Point(7, 20);
            this.rbSSHfile.Name = "rbSSHfile";
            this.rbSSHfile.Size = new System.Drawing.Size(91, 17);
            this.rbSSHfile.TabIndex = 0;
            this.rbSSHfile.TabStop = true;
            this.rbSSHfile.Text = "From Text File";
            this.rbSSHfile.UseVisualStyleBackColor = true;
            this.rbSSHfile.CheckedChanged += new System.EventHandler(this.rbSSHfile_CheckedChanged);
            // 
            // gbChangessh
            // 
            this.gbChangessh.Controls.Add(this.rbAuto247);
            this.gbChangessh.Controls.Add(this.nmrScheduled);
            this.gbChangessh.Controls.Add(this.label2);
            this.gbChangessh.Controls.Add(this.rbAutobytime);
            this.gbChangessh.Controls.Add(this.rbManual);
            this.gbChangessh.Location = new System.Drawing.Point(13, 120);
            this.gbChangessh.Name = "gbChangessh";
            this.gbChangessh.Size = new System.Drawing.Size(200, 123);
            this.gbChangessh.TabIndex = 1;
            this.gbChangessh.TabStop = false;
            this.gbChangessh.Text = "Change SSH";
            // 
            // rbAuto247
            // 
            this.rbAuto247.AutoSize = true;
            this.rbAuto247.Location = new System.Drawing.Point(7, 46);
            this.rbAuto247.Name = "rbAuto247";
            this.rbAuto247.Size = new System.Drawing.Size(73, 17);
            this.rbAuto247.TabIndex = 4;
            this.rbAuto247.TabStop = true;
            this.rbAuto247.Text = "Auto 24/7";
            this.rbAuto247.UseVisualStyleBackColor = true;
            // 
            // nmrScheduled
            // 
            this.nmrScheduled.Enabled = false;
            this.nmrScheduled.Location = new System.Drawing.Point(112, 95);
            this.nmrScheduled.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nmrScheduled.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrScheduled.Name = "nmrScheduled";
            this.nmrScheduled.Size = new System.Drawing.Size(67, 20);
            this.nmrScheduled.TabIndex = 3;
            this.nmrScheduled.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Scheduled (Minute)";
            // 
            // rbAutobytime
            // 
            this.rbAutobytime.AutoSize = true;
            this.rbAutobytime.Location = new System.Drawing.Point(7, 72);
            this.rbAutobytime.Name = "rbAutobytime";
            this.rbAutobytime.Size = new System.Drawing.Size(126, 17);
            this.rbAutobytime.TabIndex = 1;
            this.rbAutobytime.Text = "Automatic Scheduled";
            this.rbAutobytime.UseVisualStyleBackColor = true;
            this.rbAutobytime.CheckedChanged += new System.EventHandler(this.rbAutobytime_CheckedChanged);
            // 
            // rbManual
            // 
            this.rbManual.AutoSize = true;
            this.rbManual.Checked = true;
            this.rbManual.Location = new System.Drawing.Point(7, 20);
            this.rbManual.Name = "rbManual";
            this.rbManual.Size = new System.Drawing.Size(60, 17);
            this.rbManual.TabIndex = 0;
            this.rbManual.TabStop = true;
            this.rbManual.Text = "Manual";
            this.rbManual.UseVisualStyleBackColor = true;
            this.rbManual.CheckedChanged += new System.EventHandler(this.rbManual_CheckedChanged);
            // 
            // gbOther
            // 
            this.gbOther.Controls.Add(this.cblocation);
            this.gbOther.Controls.Add(this.cbhidebvs);
            this.gbOther.Controls.Add(this.cbLoopssh);
            this.gbOther.Controls.Add(this.cbSavessh);
            this.gbOther.Controls.Add(this.cbRandomssh);
            this.gbOther.Location = new System.Drawing.Point(228, 13);
            this.gbOther.Name = "gbOther";
            this.gbOther.Size = new System.Drawing.Size(167, 137);
            this.gbOther.TabIndex = 2;
            this.gbOther.TabStop = false;
            this.gbOther.Text = "Other";
            // 
            // cblocation
            // 
            this.cblocation.AutoSize = true;
            this.cblocation.Checked = true;
            this.cblocation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cblocation.Location = new System.Drawing.Point(7, 114);
            this.cblocation.Name = "cblocation";
            this.cblocation.Size = new System.Drawing.Size(87, 17);
            this.cblocation.TabIndex = 4;
            this.cblocation.Text = "Get Location";
            this.cblocation.UseVisualStyleBackColor = true;
            // 
            // cbhidebvs
            // 
            this.cbhidebvs.AutoSize = true;
            this.cbhidebvs.Location = new System.Drawing.Point(7, 90);
            this.cbhidebvs.Name = "cbhidebvs";
            this.cbhidebvs.Size = new System.Drawing.Size(82, 17);
            this.cbhidebvs.TabIndex = 3;
            this.cbhidebvs.Text = "Hide Bitvise";
            this.cbhidebvs.UseVisualStyleBackColor = true;
            // 
            // cbLoopssh
            // 
            this.cbLoopssh.AutoSize = true;
            this.cbLoopssh.Checked = true;
            this.cbLoopssh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLoopssh.Location = new System.Drawing.Point(7, 67);
            this.cbLoopssh.Name = "cbLoopssh";
            this.cbLoopssh.Size = new System.Drawing.Size(75, 17);
            this.cbLoopssh.TabIndex = 2;
            this.cbLoopssh.Text = "Loop SSH";
            this.cbLoopssh.UseVisualStyleBackColor = true;
            // 
            // cbSavessh
            // 
            this.cbSavessh.AutoSize = true;
            this.cbSavessh.Checked = true;
            this.cbSavessh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSavessh.Location = new System.Drawing.Point(7, 43);
            this.cbSavessh.Name = "cbSavessh";
            this.cbSavessh.Size = new System.Drawing.Size(137, 17);
            this.cbSavessh.TabIndex = 1;
            this.cbSavessh.Text = "Auto save SSH unused";
            this.cbSavessh.UseVisualStyleBackColor = true;
            // 
            // cbRandomssh
            // 
            this.cbRandomssh.AutoSize = true;
            this.cbRandomssh.Checked = true;
            this.cbRandomssh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRandomssh.Location = new System.Drawing.Point(7, 20);
            this.cbRandomssh.Name = "cbRandomssh";
            this.cbRandomssh.Size = new System.Drawing.Size(91, 17);
            this.cbRandomssh.TabIndex = 0;
            this.cbRandomssh.Text = "Random SSH";
            this.cbRandomssh.UseVisualStyleBackColor = true;
            // 
            // gbWhoer
            // 
            this.gbWhoer.Controls.Add(this.cbcheckbl);
            this.gbWhoer.Controls.Add(this.cbcheckproxy);
            this.gbWhoer.Controls.Add(this.cbShowinfo);
            this.gbWhoer.Location = new System.Drawing.Point(228, 156);
            this.gbWhoer.Name = "gbWhoer";
            this.gbWhoer.Size = new System.Drawing.Size(167, 96);
            this.gbWhoer.TabIndex = 3;
            this.gbWhoer.TabStop = false;
            this.gbWhoer.Text = "Whoer.net";
            // 
            // cbcheckbl
            // 
            this.cbcheckbl.AutoSize = true;
            this.cbcheckbl.Location = new System.Drawing.Point(7, 68);
            this.cbcheckbl.Name = "cbcheckbl";
            this.cbcheckbl.Size = new System.Drawing.Size(110, 17);
            this.cbcheckbl.TabIndex = 2;
            this.cbcheckbl.Text = "Next if IP blacklist";
            this.cbcheckbl.UseVisualStyleBackColor = true;
            // 
            // cbcheckproxy
            // 
            this.cbcheckproxy.AutoSize = true;
            this.cbcheckproxy.Location = new System.Drawing.Point(7, 44);
            this.cbcheckproxy.Name = "cbcheckproxy";
            this.cbcheckproxy.Size = new System.Drawing.Size(85, 17);
            this.cbcheckproxy.TabIndex = 1;
            this.cbcheckproxy.Text = "Next if Proxy";
            this.cbcheckproxy.UseVisualStyleBackColor = true;
            // 
            // cbShowinfo
            // 
            this.cbShowinfo.AutoSize = true;
            this.cbShowinfo.Checked = true;
            this.cbShowinfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowinfo.Location = new System.Drawing.Point(7, 20);
            this.cbShowinfo.Name = "cbShowinfo";
            this.cbShowinfo.Size = new System.Drawing.Size(73, 17);
            this.cbShowinfo.TabIndex = 0;
            this.cbShowinfo.Text = "Show info";
            this.cbShowinfo.UseVisualStyleBackColor = true;
            this.cbShowinfo.CheckedChanged += new System.EventHandler(this.cbShowinfo_CheckedChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(298, 258);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(108, 17);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Update/Support";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 284);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.gbWhoer);
            this.Controls.Add(this.gbOther);
            this.Controls.Add(this.gbChangessh);
            this.Controls.Add(this.gbSSH);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.gbSSH.ResumeLayout(false);
            this.gbSSH.PerformLayout();
            this.gbChangessh.ResumeLayout(false);
            this.gbChangessh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrScheduled)).EndInit();
            this.gbOther.ResumeLayout(false);
            this.gbOther.PerformLayout();
            this.gbWhoer.ResumeLayout(false);
            this.gbWhoer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSSH;
        private System.Windows.Forms.TextBox txtLinkssh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbSSHserver;
        private System.Windows.Forms.RadioButton rbSSHfile;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox gbChangessh;
        private System.Windows.Forms.NumericUpDown nmrScheduled;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbAutobytime;
        private System.Windows.Forms.RadioButton rbManual;
        private System.Windows.Forms.RadioButton rbAuto247;
        private System.Windows.Forms.GroupBox gbOther;
        private System.Windows.Forms.CheckBox cbSavessh;
        private System.Windows.Forms.CheckBox cbRandomssh;
        private System.Windows.Forms.GroupBox gbWhoer;
        private System.Windows.Forms.CheckBox cbcheckbl;
        private System.Windows.Forms.CheckBox cbcheckproxy;
        private System.Windows.Forms.CheckBox cbShowinfo;
        private System.Windows.Forms.CheckBox cbLoopssh;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox cbhidebvs;
        private System.Windows.Forms.CheckBox cblocation;
    }
}