using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace WompRat
{
    public partial class MainForm : Form
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
                        lvi.SubItems.Add(mapFound.DownloadUrl);
                    }
                    else
                    {
                        lvi.Text = "Unrecognised map";
                    }
                    

                    // Download image 
                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            string imagePath = Directory.GetCurrentDirectory() + "/" + mapFound.Folder + ".png";                            
                            client.DownloadFile(new Uri(mapFound.ImageUrl), imagePath);
                            Image mapImage = Image.FromFile(imagePath);
                            imgLstInstalled.Images.Add(mapImage);
                            lvi.ImageIndex = imgLstInstalled.Images.Count - 1;
                        } 
                        catch(System.UriFormatException e)
                        {
                            Console.WriteLine(e.StackTrace);
                        }
                    }

                    lstVwInstalledMaps.Items.Add(lvi);
                }

                // Get and display number of maps installed
                txtNumMapsInstalled.Text = lstVwInstalledMaps.Items.Count.ToString();
            }

            // Create a material theme manager and add the form to manage (this)
            /*materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            updateTheme();*/


            listView1.Items[0].ImageIndex = 0;
            listView1.Items[1].ImageIndex = 0;
            listView1.Items[2].ImageIndex = 0;
            listView1.Items[3].ImageIndex = 4;
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
            return new Map("Unrecognised map", subdir, "?", "?", "?", "?");
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

        private void writeSettings(Settings settings, string settingsFilename)
        {
            string jsonToOutput = JsonConvert.SerializeObject(settings);
            System.IO.File.WriteAllText(settingsFilename, jsonToOutput);
        }

        private void tabCtrl_SelectedIndexChanged(Object sender, EventArgs e)
        {
            // Installed maps
            if (tabCtrl.SelectedTab == tabCtrl.TabPages[0])
            {
                lstVwInstalledMaps.RedrawItems(0, lstVwInstalledMaps.Items.Count - 1, false);
            }
            // Get maps
            else if (tabCtrl.SelectedTab == tabCtrl.TabPages[1])
            {

            }
            // Settings
            else if (tabCtrl.SelectedTab == tabCtrl.TabPages[2])
            {
                txtSettingsAddon.Text = settings.AddonLocation;
                switch(settings.Theme)
                {
                    case "Light":
                        cmbTheme.SelectedItem = cmbTheme.Items[0];
                        break;
                    case "Dark":
                        cmbTheme.SelectedItem = cmbTheme.Items[1];
                        break;
                    default:
                        cmbTheme.SelectedItem = cmbTheme.Items[0];
                        break;
                }
            }
            // About
            else if (tabCtrl.SelectedTab == tabCtrl.TabPages[3])
            {

            }
        }

        private void richTxtAbout_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
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

        private void btnSettingsSave_Click(object sender, EventArgs e)
        {
            Settings updatedSettings = new Settings();
            updatedSettings.AddonLocation = txtSettingsAddon.Text;
            updatedSettings.Theme = cmbTheme.SelectedItem.ToString();

            writeSettings(updatedSettings, SettingsFilename);

            settings = updatedSettings;

            MessageBox.Show("Settings saved!");

            updateTheme();
        }
    }
}
