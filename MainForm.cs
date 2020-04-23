﻿using MaterialSkin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using WompRat.Properties;

namespace WompRat
{
    public partial class MainForm : Form
    {
        const string SettingsFilename = "settings.json";
        const string KnownMapsFilename = "known_maps.json";
        string MapImagesDir = Directory.GetCurrentDirectory() + "/images";
        Settings settings;
        KnownMaps knownMaps;
        MaterialSkinManager materialSkinManager;

        public MainForm()
        {
            InitializeComponent();

            // Load settings if they exist
            if (File.Exists(SettingsFilename))
            {
                // Load settings
                string jsonString = File.ReadAllText(SettingsFilename);
                settings = JsonConvert.DeserializeObject<Settings>(jsonString);
            }
            // Else create default settings file
            else
            {
                settings = new Settings();
                writeSettings(settings, SettingsFilename);
            }

            // Create map images folder if it doesn't exist
            if (!Directory.Exists(MapImagesDir))
            {
                Directory.CreateDirectory(MapImagesDir);
            }

            // Read known maps file
            if (File.Exists(KnownMapsFilename))
            {
                // Load known maps
                string jsonString = File.ReadAllText(KnownMapsFilename);
                knownMaps = JsonConvert.DeserializeObject<KnownMaps>(jsonString);

                string[] subdirs = getDirectoriesJustNames(settings.AddonLocation);

                // Add installed maps to ListView
                foreach (string subdir in subdirs)
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


                    // Get image
                    Image mapImage = findMapImage(mapFound);
                    imgLstInstalled.Images.Add(mapImage);
                    lvi.ImageIndex = imgLstInstalled.Images.Count - 1;

                    lstVwInstalledMaps.Items.Add(lvi);
                }

                // Get and display number of maps installed
                txtNumMapsInstalled.Text = lstVwInstalledMaps.Items.Count.ToString();

                // Add all known maps to Get Maps ListView
                foreach (Map m in knownMaps.Maps)
                {
                    ListViewItem lvi = new ListViewItem();
                    if (m != null)
                    {
                        lvi.Text = m.Name;
                        lvi.SubItems.Add(m.Folder);
                        lvi.SubItems.Add(m.Author);
                        lvi.SubItems.Add(m.DownloadUrl);
                    }
                    else
                    {
                        lvi.Text = "Unrecognised map";
                    }

                    // Get image
                    Image mapImage = findMapImage(m);
                    imgLstGetMaps.Images.Add(mapImage);
                    lvi.ImageIndex = imgLstGetMaps.Images.Count - 1;

                    lstVwGetMaps.Items.Add(lvi);
                }
            }

            
        }

        private Image findMapImage(Map m)
        {
            string imagePath = MapImagesDir + m.Folder + ".png";
            Image mapImage;

            // If image is already downloaded
            if (File.Exists(imagePath))
            {
                mapImage = Image.FromFile(imagePath);
            }
            else
            {
                // Download image if it's not 
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        client.DownloadFile(new Uri(m.ImageUrl), imagePath);
                        mapImage = Image.FromFile(imagePath);
                    }
                    catch (System.UriFormatException e)
                    {
                        Console.WriteLine(e.StackTrace);
                        mapImage = Resources.MissingImage;
                    }
                }
            }
            return mapImage;
        }

        private string[] getDirectoriesJustNames(string root)
        {
            string[] dirs = Directory.GetDirectories(root);
            for (int i = 0; i < dirs.Length; i++)
            {
                string dirName = new DirectoryInfo(dirs[i]).Name;
                dirs[i] = dirName;
            }
            return dirs;
        }

        private Map findMapFromFolder(List<Map> maps, string subdir)
        {
            foreach (Map m in maps)
            {
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
                switch (settings.Theme)
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
