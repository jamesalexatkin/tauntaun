namespace WompRat
{
    partial class MainForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "first"}, -1, System.Drawing.Color.Empty, System.Drawing.SystemColors.HotTrack, null);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("second");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("third");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("fourth");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txtNumMapsInstalled = new System.Windows.Forms.TextBox();
            this.lblNumMapsInstalledHeader = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txtSettingsAddon = new System.Windows.Forms.TextBox();
            this.btnSettingsBrowse = new System.Windows.Forms.Button();
            this.lblSettingsAddon = new System.Windows.Forms.Label();
            this.richTxtAbout = new System.Windows.Forms.RichTextBox();
            this.btnSettingsSave = new System.Windows.Forms.Button();
            this.lblSettingsTheme = new System.Windows.Forms.Label();
            this.cmbTheme = new System.Windows.Forms.ComboBox();
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lstVwInstalledMaps = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgLstInstalled = new System.Windows.Forms.ImageList(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabCtrl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNumMapsInstalled
            // 
            this.txtNumMapsInstalled.BackColor = System.Drawing.Color.White;
            this.txtNumMapsInstalled.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNumMapsInstalled.Location = new System.Drawing.Point(888, 39);
            this.txtNumMapsInstalled.Name = "txtNumMapsInstalled";
            this.txtNumMapsInstalled.ReadOnly = true;
            this.txtNumMapsInstalled.Size = new System.Drawing.Size(100, 13);
            this.txtNumMapsInstalled.TabIndex = 13;
            this.txtNumMapsInstalled.Text = "0";
            // 
            // lblNumMapsInstalledHeader
            // 
            this.lblNumMapsInstalledHeader.AutoSize = true;
            this.lblNumMapsInstalledHeader.Location = new System.Drawing.Point(885, 23);
            this.lblNumMapsInstalledHeader.Name = "lblNumMapsInstalledHeader";
            this.lblNumMapsInstalledHeader.Size = new System.Drawing.Size(128, 13);
            this.lblNumMapsInstalledHeader.TabIndex = 12;
            this.lblNumMapsInstalledHeader.Text = "Number of maps installed:";
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(17, 30);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(722, 351);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Tile;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "christian-bowen--ysg_LNh5Lo-unsplash.jpg");
            this.imageList1.Images.SetKeyName(1, "christian-bowen--ysg_LNh5Lo-unsplash.jpg");
            this.imageList1.Images.SetKeyName(2, "christian-bowen--ysg_LNh5Lo-unsplash.jpg");
            this.imageList1.Images.SetKeyName(3, "christian-bowen--ysg_LNh5Lo-unsplash.jpg");
            this.imageList1.Images.SetKeyName(4, "erik-mclean-kZNl5Xpvhqg-unsplash.jpg");
            // 
            // txtSettingsAddon
            // 
            this.txtSettingsAddon.Location = new System.Drawing.Point(14, 30);
            this.txtSettingsAddon.Name = "txtSettingsAddon";
            this.txtSettingsAddon.Size = new System.Drawing.Size(443, 20);
            this.txtSettingsAddon.TabIndex = 16;
            // 
            // btnSettingsBrowse
            // 
            this.btnSettingsBrowse.Location = new System.Drawing.Point(463, 30);
            this.btnSettingsBrowse.Name = "btnSettingsBrowse";
            this.btnSettingsBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnSettingsBrowse.TabIndex = 15;
            this.btnSettingsBrowse.Text = "Browse";
            this.btnSettingsBrowse.UseVisualStyleBackColor = true;
            this.btnSettingsBrowse.Click += new System.EventHandler(this.btnSettingsBrowse_Click);
            // 
            // lblSettingsAddon
            // 
            this.lblSettingsAddon.AutoSize = true;
            this.lblSettingsAddon.Location = new System.Drawing.Point(11, 14);
            this.lblSettingsAddon.Name = "lblSettingsAddon";
            this.lblSettingsAddon.Size = new System.Drawing.Size(107, 13);
            this.lblSettingsAddon.TabIndex = 14;
            this.lblSettingsAddon.Text = "Addon folder location";
            // 
            // richTxtAbout
            // 
            this.richTxtAbout.BackColor = System.Drawing.Color.White;
            this.richTxtAbout.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTxtAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTxtAbout.Location = new System.Drawing.Point(12, 15);
            this.richTxtAbout.Name = "richTxtAbout";
            this.richTxtAbout.ReadOnly = true;
            this.richTxtAbout.Size = new System.Drawing.Size(770, 198);
            this.richTxtAbout.TabIndex = 2;
            this.richTxtAbout.Text = resources.GetString("richTxtAbout.Text");
            this.richTxtAbout.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTxtAbout_LinkClicked);
            // 
            // btnSettingsSave
            // 
            this.btnSettingsSave.Location = new System.Drawing.Point(14, 74);
            this.btnSettingsSave.Name = "btnSettingsSave";
            this.btnSettingsSave.Size = new System.Drawing.Size(75, 23);
            this.btnSettingsSave.TabIndex = 17;
            this.btnSettingsSave.Text = "Save";
            this.btnSettingsSave.UseVisualStyleBackColor = true;
            this.btnSettingsSave.Click += new System.EventHandler(this.btnSettingsSave_Click);
            // 
            // lblSettingsTheme
            // 
            this.lblSettingsTheme.AutoSize = true;
            this.lblSettingsTheme.Location = new System.Drawing.Point(11, 100);
            this.lblSettingsTheme.Name = "lblSettingsTheme";
            this.lblSettingsTheme.Size = new System.Drawing.Size(40, 13);
            this.lblSettingsTheme.TabIndex = 18;
            this.lblSettingsTheme.Text = "Theme";
            this.lblSettingsTheme.Visible = false;
            // 
            // cmbTheme
            // 
            this.cmbTheme.FormattingEnabled = true;
            this.cmbTheme.Items.AddRange(new object[] {
            "Light",
            "Dark"});
            this.cmbTheme.Location = new System.Drawing.Point(14, 116);
            this.cmbTheme.Name = "cmbTheme";
            this.cmbTheme.Size = new System.Drawing.Size(121, 21);
            this.cmbTheme.TabIndex = 19;
            this.cmbTheme.Visible = false;
            // 
            // tabCtrl
            // 
            this.tabCtrl.Controls.Add(this.tabPage1);
            this.tabCtrl.Controls.Add(this.tabPage2);
            this.tabCtrl.Controls.Add(this.tabPage3);
            this.tabCtrl.Controls.Add(this.tabPage4);
            this.tabCtrl.Location = new System.Drawing.Point(12, 12);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(1064, 578);
            this.tabCtrl.TabIndex = 9;
            this.tabCtrl.SelectedIndexChanged += new System.EventHandler(this.tabCtrl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lstVwInstalledMaps);
            this.tabPage1.Controls.Add(this.txtNumMapsInstalled);
            this.tabPage1.Controls.Add(this.lblNumMapsInstalledHeader);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1056, 552);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Installed maps";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lstVwInstalledMaps
            // 
            this.lstVwInstalledMaps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lstVwInstalledMaps.HideSelection = false;
            this.lstVwInstalledMaps.LargeImageList = this.imgLstInstalled;
            this.lstVwInstalledMaps.Location = new System.Drawing.Point(19, 23);
            this.lstVwInstalledMaps.Name = "lstVwInstalledMaps";
            this.lstVwInstalledMaps.Size = new System.Drawing.Size(860, 508);
            this.lstVwInstalledMaps.TabIndex = 14;
            this.lstVwInstalledMaps.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Folder";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Author";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Download link";
            // 
            // imgLstInstalled
            // 
            this.imgLstInstalled.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgLstInstalled.ImageSize = new System.Drawing.Size(128, 96);
            this.imgLstInstalled.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1056, 552);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Get maps";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cmbTheme);
            this.tabPage3.Controls.Add(this.txtSettingsAddon);
            this.tabPage3.Controls.Add(this.lblSettingsTheme);
            this.tabPage3.Controls.Add(this.lblSettingsAddon);
            this.tabPage3.Controls.Add(this.btnSettingsSave);
            this.tabPage3.Controls.Add(this.btnSettingsBrowse);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1056, 552);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.richTxtAbout);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1056, 552);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "About";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 603);
            this.Controls.Add(this.tabCtrl);
            this.Name = "MainForm";
            this.Text = "Womp Rat | SWBFII Map Manager";
            this.tabCtrl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTxtAbout;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox txtNumMapsInstalled;
        private System.Windows.Forms.Label lblNumMapsInstalledHeader;
        private System.Windows.Forms.TextBox txtSettingsAddon;
        private System.Windows.Forms.Button btnSettingsBrowse;
        private System.Windows.Forms.Label lblSettingsAddon;
        private System.Windows.Forms.ComboBox cmbTheme;
        private System.Windows.Forms.Label lblSettingsTheme;
        private System.Windows.Forms.Button btnSettingsSave;
        private System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView lstVwInstalledMaps;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ImageList imgLstInstalled;
    }
}

