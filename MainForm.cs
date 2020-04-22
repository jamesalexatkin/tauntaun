using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace WompRat
{
    public partial class MainForm : MaterialForm
    {
        const string SettingsFilename = "settings.json";
        const string KnownMapsFilename = "known_maps.json";
        Settings settings;
        KnownMaps knownMaps;
        MaterialSkinManager materialSkinManager;

        public MainForm()
        {
            InitializeComponent();

            if (File.Exists(SettingsFilename))
            {
                // Load settings
                string jsonString = File.ReadAllText(SettingsFilename);
                settings = JsonConvert.DeserializeObject<Settings>(jsonString);
            }
            else
            {
                settings = new Settings();
                writeSettings(settings, SettingsFilename);
            }

            if (File.Exists(KnownMapsFilename))
            {
                // Load known maps
                string jsonString = File.ReadAllText(KnownMapsFilename);
                knownMaps = JsonConvert.DeserializeObject<KnownMaps>(jsonString);

                string[] subdirs = getDirectoriesJustNames(settings.AddonLocation);
                    
                // Add installed ones to ListView
                foreach(string subdir in subdirs)
                {
                    Map mapFound = findMapFromFolder(knownMaps.Maps, subdir);
                    ListViewItem lvi = new ListViewItem();
                    if (mapFound != null)
                    {                   
                        lvi.Text = mapFound.Name;
                        lvi.SubItems.Add(mapFound.Folder);
                        lvi.SubItems.Add(mapFound.Author);
                        lvi.SubItems.Add(mapFound.DownloadLink);
                    }
                    else
                    {
                        lvi.Text = "Unrecognised map";
                    }
                    lstVwInstalledMaps.Items.Add(lvi);
                }

                // Get and display number of maps installed
                lblNumMapsInstalled.Text = lstVwInstalledMaps.Items.Count.ToString();
            }

            // Create a material theme manager and add the form to manage (this)
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            updateTheme();
            
        }

        private string[] getDirectoriesJustNames(string root)
        {
            string[] dirs = Directory.GetDirectories(root);
            for(int i = 0; i < dirs.Length; i++)
            {
                string dirName = new DirectoryInfo(dirs[i]).Name;
                dirs[i] = dirName;
            }
            return dirs;
        }

        private Map findMapFromFolder(List<Map> maps, string subdir)
        {
            foreach(Map m in maps) {
                if (m.Folder.Equals(subdir))
                {
                    return m;
                }
            }
            // In this case, we haven't recognised the map
            return new Map("Unrecognised map", subdir, "?", "?", "?");
        }

        private void updateTheme()
        {
            if (settings.Theme.Equals("dark"))
            {
                materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            }
            else
            {
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            }
        }

        private void btnSettingsSave_Click(object sender, EventArgs e)
        {
            Settings updatedSettings = new Settings();
            updatedSettings.AddonLocation = txtSettingsAddon.Text;
            if (rdBtnLight.Checked)
            {
                updatedSettings.Theme = "light";
            }
            else if (rdBtnDark.Checked)
            {
                updatedSettings.Theme = "dark";
            }

            writeSettings(updatedSettings, SettingsFilename);

            settings = updatedSettings;

            MessageBox.Show("Settings saved!");

            updateTheme();
        }

        private void writeSettings(Settings settings, string settingsFilename)
        {
            string jsonToOutput = JsonConvert.SerializeObject(settings);
            System.IO.File.WriteAllText(settingsFilename, jsonToOutput);
        }

        private void matTabCtrl_SelectedIndexChanged(Object sender, EventArgs e)
        {
            // Installed maps
            if (matTabCtrl.SelectedTab == matTabCtrl.TabPages[0])
            {
                lstVwInstalledMaps.RedrawItems(0, lstVwInstalledMaps.Items.Count - 1, false);
            }
            // Get maps
            else if (matTabCtrl.SelectedTab == matTabCtrl.TabPages[1])
            {

            }
            // Settings
            else if (matTabCtrl.SelectedTab == matTabCtrl.TabPages[2])
            {
                txtSettingsAddon.Text = settings.AddonLocation;
                if (settings.Theme == "dark")
                {
                    rdBtnDark.Checked = true;
                } 
                else
                {
                    rdBtnLight.Checked = true;
                }
            }
            // About
            else if (matTabCtrl.SelectedTab == matTabCtrl.TabPages[3])
            {

            }
        }

        private void btnSettingsBrowse_Click(object sender, EventArgs e)
        { 
            string addonFolderPath = "";

            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    addonFolderPath = folderDialog.SelectedPath;
                }
            }

            txtSettingsAddon.Text = addonFolderPath;
        }

        private void richTxtAbout_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
