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
            this.materialListView1 = new MaterialSkin.Controls.MaterialListView();
            this.matTabCtrl = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPgInstalled = new System.Windows.Forms.TabPage();
            this.lstVwInstalledMaps = new MaterialSkin.Controls.MaterialListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAuthor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDownloadLink = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPgGet = new System.Windows.Forms.TabPage();
            this.tabPgSettings = new System.Windows.Forms.TabPage();
            this.rdBtnDark = new MaterialSkin.Controls.MaterialRadioButton();
            this.lblSettingsTheme = new MaterialSkin.Controls.MaterialLabel();
            this.rdBtnLight = new MaterialSkin.Controls.MaterialRadioButton();
            this.btnSettingsBrowse = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txtSettingsAddon = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.lblSettingsAddon = new MaterialSkin.Controls.MaterialLabel();
            this.btnSettingsSave = new MaterialSkin.Controls.MaterialRaisedButton();
            this.tabPgAbout = new System.Windows.Forms.TabPage();
            this.matTabSel = new MaterialSkin.Controls.MaterialTabSelector();
            this.matTabCtrl.SuspendLayout();
            this.tabPgInstalled.SuspendLayout();
            this.tabPgSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialListView1
            // 
            this.materialListView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialListView1.Depth = 0;
            this.materialListView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.materialListView1.FullRowSelect = true;
            this.materialListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.materialListView1.HideSelection = false;
            this.materialListView1.Location = new System.Drawing.Point(186, 100);
            this.materialListView1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialListView1.MouseState = MaterialSkin.MouseState.OUT;
            this.materialListView1.Name = "materialListView1";
            this.materialListView1.OwnerDraw = true;
            this.materialListView1.Size = new System.Drawing.Size(121, 97);
            this.materialListView1.TabIndex = 1;
            this.materialListView1.UseCompatibleStateImageBehavior = false;
            this.materialListView1.View = System.Windows.Forms.View.Details;
            // 
            // matTabCtrl
            // 
            this.matTabCtrl.Controls.Add(this.tabPgInstalled);
            this.matTabCtrl.Controls.Add(this.tabPgGet);
            this.matTabCtrl.Controls.Add(this.tabPgSettings);
            this.matTabCtrl.Controls.Add(this.tabPgAbout);
            this.matTabCtrl.Depth = 0;
            this.matTabCtrl.Location = new System.Drawing.Point(0, 127);
            this.matTabCtrl.MouseState = MaterialSkin.MouseState.HOVER;
            this.matTabCtrl.Name = "matTabCtrl";
            this.matTabCtrl.SelectedIndex = 0;
            this.matTabCtrl.Size = new System.Drawing.Size(1078, 533);
            this.matTabCtrl.TabIndex = 5;
            this.matTabCtrl.SelectedIndexChanged += new System.EventHandler(this.matTabCtrl_SelectedIndexChanged);
            // 
            // tabPgInstalled
            // 
            this.tabPgInstalled.Controls.Add(this.lstVwInstalledMaps);
            this.tabPgInstalled.Location = new System.Drawing.Point(4, 22);
            this.tabPgInstalled.Name = "tabPgInstalled";
            this.tabPgInstalled.Size = new System.Drawing.Size(1070, 507);
            this.tabPgInstalled.TabIndex = 0;
            this.tabPgInstalled.Text = "Installed maps";
            this.tabPgInstalled.UseVisualStyleBackColor = true;
            // 
            // lstVwInstalledMaps
            // 
            this.lstVwInstalledMaps.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstVwInstalledMaps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colFolder,
            this.colAuthor,
            this.colDownloadLink});
            this.lstVwInstalledMaps.Depth = 0;
            this.lstVwInstalledMaps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstVwInstalledMaps.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.lstVwInstalledMaps.FullRowSelect = true;
            this.lstVwInstalledMaps.HideSelection = false;
            this.lstVwInstalledMaps.Location = new System.Drawing.Point(0, 0);
            this.lstVwInstalledMaps.MouseLocation = new System.Drawing.Point(-1, -1);
            this.lstVwInstalledMaps.MouseState = MaterialSkin.MouseState.OUT;
            this.lstVwInstalledMaps.Name = "lstVwInstalledMaps";
            this.lstVwInstalledMaps.OwnerDraw = true;
            this.lstVwInstalledMaps.Size = new System.Drawing.Size(1070, 507);
            this.lstVwInstalledMaps.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstVwInstalledMaps.TabIndex = 0;
            this.lstVwInstalledMaps.UseCompatibleStateImageBehavior = false;
            this.lstVwInstalledMaps.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 376;
            // 
            // colFolder
            // 
            this.colFolder.Text = "Folder";
            this.colFolder.Width = 120;
            // 
            // colAuthor
            // 
            this.colAuthor.Text = "Author";
            this.colAuthor.Width = 120;
            // 
            // colDownloadLink
            // 
            this.colDownloadLink.Text = "Download link";
            this.colDownloadLink.Width = 200;
            // 
            // tabPgGet
            // 
            this.tabPgGet.Location = new System.Drawing.Point(4, 22);
            this.tabPgGet.Name = "tabPgGet";
            this.tabPgGet.Size = new System.Drawing.Size(776, 434);
            this.tabPgGet.TabIndex = 1;
            this.tabPgGet.Text = "Get maps";
            this.tabPgGet.UseVisualStyleBackColor = true;
            // 
            // tabPgSettings
            // 
            this.tabPgSettings.BackColor = System.Drawing.Color.White;
            this.tabPgSettings.Controls.Add(this.rdBtnDark);
            this.tabPgSettings.Controls.Add(this.lblSettingsTheme);
            this.tabPgSettings.Controls.Add(this.rdBtnLight);
            this.tabPgSettings.Controls.Add(this.btnSettingsBrowse);
            this.tabPgSettings.Controls.Add(this.txtSettingsAddon);
            this.tabPgSettings.Controls.Add(this.lblSettingsAddon);
            this.tabPgSettings.Controls.Add(this.btnSettingsSave);
            this.tabPgSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPgSettings.Name = "tabPgSettings";
            this.tabPgSettings.Size = new System.Drawing.Size(776, 434);
            this.tabPgSettings.TabIndex = 2;
            this.tabPgSettings.Text = "Settings";
            // 
            // rdBtnDark
            // 
            this.rdBtnDark.AutoSize = true;
            this.rdBtnDark.Depth = 0;
            this.rdBtnDark.Font = new System.Drawing.Font("Roboto", 10F);
            this.rdBtnDark.Location = new System.Drawing.Point(8, 172);
            this.rdBtnDark.Margin = new System.Windows.Forms.Padding(0);
            this.rdBtnDark.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rdBtnDark.MouseState = MaterialSkin.MouseState.HOVER;
            this.rdBtnDark.Name = "rdBtnDark";
            this.rdBtnDark.Ripple = true;
            this.rdBtnDark.Size = new System.Drawing.Size(57, 30);
            this.rdBtnDark.TabIndex = 13;
            this.rdBtnDark.TabStop = true;
            this.rdBtnDark.Text = "Dark";
            this.rdBtnDark.UseVisualStyleBackColor = true;
            // 
            // lblSettingsTheme
            // 
            this.lblSettingsTheme.AutoSize = true;
            this.lblSettingsTheme.Depth = 0;
            this.lblSettingsTheme.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblSettingsTheme.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSettingsTheme.Location = new System.Drawing.Point(7, 101);
            this.lblSettingsTheme.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblSettingsTheme.Name = "lblSettingsTheme";
            this.lblSettingsTheme.Size = new System.Drawing.Size(55, 19);
            this.lblSettingsTheme.TabIndex = 12;
            this.lblSettingsTheme.Text = "Theme";
            // 
            // rdBtnLight
            // 
            this.rdBtnLight.AutoSize = true;
            this.rdBtnLight.Depth = 0;
            this.rdBtnLight.Font = new System.Drawing.Font("Roboto", 10F);
            this.rdBtnLight.Location = new System.Drawing.Point(8, 130);
            this.rdBtnLight.Margin = new System.Windows.Forms.Padding(0);
            this.rdBtnLight.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rdBtnLight.MouseState = MaterialSkin.MouseState.HOVER;
            this.rdBtnLight.Name = "rdBtnLight";
            this.rdBtnLight.Ripple = true;
            this.rdBtnLight.Size = new System.Drawing.Size(60, 30);
            this.rdBtnLight.TabIndex = 11;
            this.rdBtnLight.TabStop = true;
            this.rdBtnLight.Text = "Light";
            this.rdBtnLight.UseVisualStyleBackColor = true;
            // 
            // btnSettingsBrowse
            // 
            this.btnSettingsBrowse.AutoSize = true;
            this.btnSettingsBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSettingsBrowse.Depth = 0;
            this.btnSettingsBrowse.Icon = null;
            this.btnSettingsBrowse.Location = new System.Drawing.Point(460, 44);
            this.btnSettingsBrowse.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSettingsBrowse.Name = "btnSettingsBrowse";
            this.btnSettingsBrowse.Primary = true;
            this.btnSettingsBrowse.Size = new System.Drawing.Size(76, 36);
            this.btnSettingsBrowse.TabIndex = 10;
            this.btnSettingsBrowse.Text = "Browse";
            this.btnSettingsBrowse.UseVisualStyleBackColor = true;
            this.btnSettingsBrowse.Click += new System.EventHandler(this.btnSettingsBrowse_Click);
            // 
            // txtSettingsAddon
            // 
            this.txtSettingsAddon.BackColor = System.Drawing.SystemColors.Control;
            this.txtSettingsAddon.Depth = 0;
            this.txtSettingsAddon.Hint = "";
            this.txtSettingsAddon.Location = new System.Drawing.Point(8, 44);
            this.txtSettingsAddon.MaxLength = 32767;
            this.txtSettingsAddon.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtSettingsAddon.Name = "txtSettingsAddon";
            this.txtSettingsAddon.PasswordChar = '\0';
            this.txtSettingsAddon.SelectedText = "";
            this.txtSettingsAddon.SelectionLength = 0;
            this.txtSettingsAddon.SelectionStart = 0;
            this.txtSettingsAddon.Size = new System.Drawing.Size(425, 23);
            this.txtSettingsAddon.TabIndex = 9;
            this.txtSettingsAddon.TabStop = false;
            this.txtSettingsAddon.UseSystemPasswordChar = false;
            // 
            // lblSettingsAddon
            // 
            this.lblSettingsAddon.AutoSize = true;
            this.lblSettingsAddon.Depth = 0;
            this.lblSettingsAddon.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblSettingsAddon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSettingsAddon.Location = new System.Drawing.Point(8, 14);
            this.lblSettingsAddon.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblSettingsAddon.Name = "lblSettingsAddon";
            this.lblSettingsAddon.Size = new System.Drawing.Size(111, 19);
            this.lblSettingsAddon.TabIndex = 2;
            this.lblSettingsAddon.Text = "Addon location";
            // 
            // btnSettingsSave
            // 
            this.btnSettingsSave.AutoSize = true;
            this.btnSettingsSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSettingsSave.Depth = 0;
            this.btnSettingsSave.Icon = null;
            this.btnSettingsSave.Location = new System.Drawing.Point(8, 234);
            this.btnSettingsSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSettingsSave.Name = "btnSettingsSave";
            this.btnSettingsSave.Primary = true;
            this.btnSettingsSave.Size = new System.Drawing.Size(55, 36);
            this.btnSettingsSave.TabIndex = 1;
            this.btnSettingsSave.Text = "Save";
            this.btnSettingsSave.UseVisualStyleBackColor = true;
            this.btnSettingsSave.Click += new System.EventHandler(this.btnSettingsSave_Click);
            // 
            // tabPgAbout
            // 
            this.tabPgAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPgAbout.Name = "tabPgAbout";
            this.tabPgAbout.Size = new System.Drawing.Size(776, 434);
            this.tabPgAbout.TabIndex = 3;
            this.tabPgAbout.Text = "About";
            this.tabPgAbout.UseVisualStyleBackColor = true;
            // 
            // matTabSel
            // 
            this.matTabSel.BackColor = System.Drawing.SystemColors.Control;
            this.matTabSel.BaseTabControl = this.matTabCtrl;
            this.matTabSel.Depth = 0;
            this.matTabSel.Location = new System.Drawing.Point(0, 64);
            this.matTabSel.MouseState = MaterialSkin.MouseState.HOVER;
            this.matTabSel.Name = "matTabSel";
            this.matTabSel.Size = new System.Drawing.Size(1078, 65);
            this.matTabSel.TabIndex = 8;
            this.matTabSel.Text = "materialTabSelector1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 659);
            this.Controls.Add(this.matTabSel);
            this.Controls.Add(this.matTabCtrl);
            this.Controls.Add(this.materialListView1);
            this.Name = "MainForm";
            this.Text = "Womp Rat | SWBFII Map Manager";
            this.matTabCtrl.ResumeLayout(false);
            this.tabPgInstalled.ResumeLayout(false);
            this.tabPgSettings.ResumeLayout(false);
            this.tabPgSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MaterialSkin.Controls.MaterialListView materialListView1;
        private MaterialSkin.Controls.MaterialTabControl matTabCtrl;
        private MaterialSkin.Controls.MaterialTabSelector matTabSel;
        private System.Windows.Forms.TabPage tabPgInstalled;
        private System.Windows.Forms.TabPage tabPgGet;
        private System.Windows.Forms.TabPage tabPgSettings;
        private System.Windows.Forms.TabPage tabPgAbout;
        private MaterialSkin.Controls.MaterialRaisedButton btnSettingsSave;
        private MaterialSkin.Controls.MaterialLabel lblSettingsAddon;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtSettingsAddon;
        private MaterialSkin.Controls.MaterialRaisedButton btnSettingsBrowse;
        private MaterialSkin.Controls.MaterialRadioButton rdBtnDark;
        private MaterialSkin.Controls.MaterialLabel lblSettingsTheme;
        private MaterialSkin.Controls.MaterialRadioButton rdBtnLight;
        private MaterialSkin.Controls.MaterialListView lstVwInstalledMaps;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colFolder;
        private System.Windows.Forms.ColumnHeader colAuthor;
        private System.Windows.Forms.ColumnHeader colDownloadLink;
    }
}

