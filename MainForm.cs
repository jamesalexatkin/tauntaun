using Flurl;
using HtmlAgilityPack;
using MaterialSkin;
using Newtonsoft.Json;
using SharpCompress.Readers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Tauntaun.Properties;

namespace Tauntaun
{
    public partial class MainForm : Form
    {
        const string SettingsFilename = "settings.json";
        const string KnownMapsFilename = "known_maps.json";
        ArrayList installedMaps = new ArrayList();
        string MapImagesDir = Path.Combine(Directory.GetCurrentDirectory(), "images");
        string TempDir = Path.Combine(Directory.GetCurrentDirectory(), "temp");
        Settings settings;
        KnownMaps knownMaps;
        MapInstallClient client = new MapInstallClient();
        const string moddbBaseUrl = "https://www.moddb.com/";

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

            // Create temp folder if it doesn't exist
            if (!Directory.Exists(TempDir))
            {
                Directory.CreateDirectory(TempDir);
            }

            // Read known maps file
            if (File.Exists(KnownMapsFilename))
            {
                // Load known maps
                string jsonString = File.ReadAllText(KnownMapsFilename);
                knownMaps = JsonConvert.DeserializeObject<KnownMaps>(jsonString);

                string[] subdirs = getDirectoriesJustNames(settings.AddonLocation);

                refreshInstalledMaps(subdirs);

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

                // Get and display number of maps available
                txtNumMapsAvailable.Text = lstVwGetMaps.Items.Count.ToString();
            }
        }

        private void refreshInstalledMaps(string[] subdirs)
        {
            lstVwInstalledMaps.Clear();
            installedMaps = new ArrayList();
            // Add installed maps to ListView
            foreach (string subdir in subdirs)
            {
                Map mapFound = findMapFromFolder(knownMaps.Maps, subdir);
                installedMaps.Add(mapFound);
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
        }

        private Image findMapImage(Map m)
        {
            string imagePath = Path.Combine(MapImagesDir, m.Folder + ".png");
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
            return new Map("Unrecognised map", subdir, "?", "?", "?", "?", "");
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
                refreshInstalledMaps(getDirectoriesJustNames(settings.AddonLocation));
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

        private void richTxt_LinkClicked(object sender, LinkClickedEventArgs e)
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
        }

        private void lstVwInstalledMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedLvis = lstVwInstalledMaps.SelectedItems;
            if (selectedLvis.Count > 0)
            {
                // We only display the first item
                ListViewItem curMapLvi = selectedLvis[0];
                string mapFolder = curMapLvi.SubItems[1].Text;
                Map curMap = findMapFromFolder(knownMaps.Maps, mapFolder);

                picBoxCurMapInstalled.Image = findMapImage(curMap);

                txtNameCurMapInstalled.Text = curMap.Name;
                txtAuthorCurMapInstalled.Text = curMap.Author;
                txtFolderCurMapInstalled.Text = curMap.Folder;
                txtTypeCurMapInstalled.Text = curMap.Type;
                txtDownloadCurMapInstalled.Text = curMap.DownloadUrl;

                // Enable uninstall button
                btnUninstallMap.Enabled = true;
            }
            else
            {
                // TODO
            }
        }

        private void lstVwGetMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedLvis = lstVwGetMaps.SelectedItems;
            if (selectedLvis.Count > 0) 
            {
                // We only display the first item
                ListViewItem curMapLvi = selectedLvis[0];
                string mapFolder = curMapLvi.SubItems[1].Text;
                Map curMap = findMapFromFolder(knownMaps.Maps, mapFolder);

                picBoxCurMapGet.Image = findMapImage(curMap);

                txtNameCurMapGet.Text = curMap.Name;
                txtAuthorCurMapGet.Text = curMap.Author;
                txtFolderCurMapGet.Text = curMap.Folder;
                txtTypeCurMapGet.Text = curMap.Type;
                txtDownloadCurMapGet.Text = curMap.DownloadUrl;
                
                if (installedMaps.Contains(curMap)) {
                    // Enable install button
                    btnInstallMap.Enabled = false;
                }
                else
                {
                    btnInstallMap.Enabled = true;
                }
            }
            else
            {
                // TODO
            }
        }

        private void btnInstallMap_Click(object sender, EventArgs e)
        {
            try
            {
                ListView.SelectedListViewItemCollection selectedLvis = lstVwGetMaps.SelectedItems;
                foreach (ListViewItem lvi in selectedLvis)
                {
                    // Only install if client is free
                    if (!client.IsBusy)
                    {
                        string mapFolder = lvi.SubItems[1].Text;
                        Map m = findMapFromFolder(knownMaps.Maps, mapFolder);
                        string downloadUrl = m.DownloadUrl;

                        // Check if link is to moddb
                        Regex moddbRegex = new Regex(moddbBaseUrl + ".*");
                        if (moddbRegex.Match(downloadUrl).Success)
                        {
                            downloadModdbMap(mapFolder, m, downloadUrl);
                        }
                        else
                        {
                            throw new MapInstallException("Can't download file. Maybe support for this site is not yet implemented.");
                        }
                        installedMaps.Add(m);
                    }
                    else
                    {
                        throw new MapInstallException("Only one file can be downloaded at a time. Please wait for the previous one to finish.");
                    }
                }
            }
            catch (MapInstallException mapInstallEx)
            {
                MessageBox.Show(mapInstallEx.Message, "Map installation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void downloadModdbMap(string mapFolder, Map m, string downloadUrl)
        {
            string tempMainPagePath = Path.Combine(TempDir, mapFolder + "ModdbMainPage.html");
            client = new MapInstallClient(m);
            client.DownloadFile(downloadUrl, tempMainPagePath);

            HtmlAgilityPack.HtmlDocument moddbMainPage = new HtmlAgilityPack.HtmlDocument();
            moddbMainPage.LoadHtml(File.ReadAllText(tempMainPagePath));
            HtmlAgilityPack.HtmlNode downloadButton = moddbMainPage.GetElementbyId("downloadmirrorstoggle");
            string downloadPageUrl = Url.Combine(moddbBaseUrl, downloadButton.GetAttributeValue("href", ""));

            string tempDownloadPagePath = Path.Combine(TempDir, mapFolder + "DownloadPage.html");
            //client = new MapInstallClient();
            client.DownloadFile(downloadPageUrl, tempDownloadPagePath);

            HtmlAgilityPack.HtmlDocument moddbDownloadPage = new HtmlAgilityPack.HtmlDocument();
            moddbDownloadPage.LoadHtml(File.ReadAllText(tempDownloadPagePath));

            IEnumerable<HtmlAgilityPack.HtmlNode> anchors = moddbDownloadPage.DocumentNode.Descendants("a");
            if (anchors != null)
            {
                Regex downloadFile = new Regex(@"download (.*)\.(.*)");

                foreach (HtmlNode a in anchors)
                {
                    Match match = downloadFile.Match(a.InnerText);

                    if (match.Success)
                    {
                        string filename = match.Groups[1].ToString();
                        string fileExtension = match.Groups[2].ToString();
                        string destFile = Path.Combine(TempDir, filename + "." + fileExtension);

                        string realDownloadUrl = Url.Combine(moddbBaseUrl, a.GetAttributeValue("href", ""));

                        client.DownloadFileCompleted += client_DownloadFileCompleted;
                        client.DownloadProgressChanged += client_DownloadProgressChanged;
                        //MessageBox.Show("File will start downloading.", "Downloading " + m.Name);
                        client.DownloadFileAsync(new Uri(realDownloadUrl), destFile);
                        client.downloadedFile = destFile;

                        // Show progress bar
                        progBarMapDownload.Visible = true;
                        // Show map name
                        lblMapInstalling.Visible = true;
                        lblMapInstalling.Text = m.Name;
                        lblInstallStatus.Visible = true;
                        lblInstallStatus.Text = "Downloading map...";

                        // Exit this now as we have found and processed our match
                        // Any other matches in the HTML are irrelevant
                        break;
                    }
                }
            }
        }

        private void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {                       
            Map mapToInstall = client.mapToInstall;

            // Install map
            string installationInstructions = mapToInstall.InstallationInstructions;
            try
            {
                lblInstallStatus.Text = "Finishing install...";
                parseInstallInstructions(client.downloadedFile, installationInstructions);
            }
            catch (MapInstallException mapInstallEx)
            {
                MessageBox.Show(mapInstallEx.Message, "Map installation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            MessageBox.Show("Finished installing " + mapToInstall.Name + ".");
        }

        private void parseInstallInstructions(string downloadedFile, string installationInstructions)
        {
            String[] instructions = installationInstructions.Split(',');
            foreach (string instruction in instructions)
            {
                string trimmedInstruction = instruction.Trim();
                String[] words = trimmedInstruction.Split(' ');
                if (words != null)
                {
                    int instructionsCompleted = 0;
                    switch (words[0])
                    {
                        // Extract archive
                        case "EXTRACT":
                            // Get file extension and make it all lowercase for consistency
                            string fileExtension = Path.GetExtension(downloadedFile).ToLower();
                            Console.WriteLine(fileExtension);
                            string destination = Path.Combine(TempDir, Path.GetFileNameWithoutExtension(downloadedFile));
                            lblInstallStatus.Text = "Extracting map...";
                            switch (fileExtension)
                            {
                                case ".zip":
                                case ".rar":
                                    extractZipOrRarFile(downloadedFile, destination);
                                    break;

                                case ".7z":
                                    throw new MapInstallException(".7z extraction not yet implemented");
                                    break;

                                default:
                                    throw new MapInstallException("Unrecognised archive type: '" + fileExtension + "'");
                                    break;

                            }
                            Console.WriteLine("Extracted " + downloadedFile + " to " + destination);
                            break;

                        case "MOVE":
                            string folderToMove = words[1];
                            string downloadedFileWoExtension = Path.GetFileNameWithoutExtension(downloadedFile);
                            string dirToMove = Path.Combine(TempDir, downloadedFileWoExtension);

                            destination = words[3];
                            // Handle addon shorthand
                            if (destination == "addon")
                            {
                                destination = settings.AddonLocation;
                            }

                            lblInstallStatus.Text = "Moving map...";
                            CopyDirectory(dirToMove, destination, true);

                            Console.WriteLine("Copied " + folderToMove + " to " + destination);                          

                            break;

                        case "RUN":
                            string target = words[1];

                            if (target == ".exe") 
                            {
                                string fileToRun = "";

                                // If we've downloaded a plain .exe, we can run it straight away
                                if (Path.GetExtension(downloadedFile) == ".exe")
                                {
                                    fileToRun = downloadedFile;
                                }
                                // Otherwise, the .exe was in an archive that we've unzipped
                                else
                                {
                                    downloadedFileWoExtension = Path.GetFileNameWithoutExtension(downloadedFile);
                                    string extractedDir = Path.Combine(TempDir, downloadedFileWoExtension);

                                    string[] foundFiles = System.IO.Directory.GetFiles(extractedDir, "*.exe");

                                    if (foundFiles.Length > 0)
                                    {
                                        fileToRun = foundFiles[0];
                                    }                                
                                    else
                                    {
                                        throw new MapInstallException("No executable installer found.");
                                    }
                                }

                                // Now run installer
                                MessageBox.Show("Installation will continue in external installer.");
                                Process.Start(fileToRun);
                            }

                            break;

                        default:
                            throw new MapInstallException("Malformed installation instructions");
                    }
                    instructionsCompleted++;
                    progBarMapDownload.Value = instructionsCompleted / instructions.Length * progBarMapDownload.Maximum;

                }
                else
                {
                    throw new MapInstallException("Empty instruction");
                }
            }
            // Hide progress bar
            progBarMapDownload.Visible = false;
            // Hide map name
            lblMapInstalling.Visible = false;
            lblInstallStatus.Visible = false;
        }

        private void CopyDirectory(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                // TODO: Make this overwrite a file if it already exists
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    CopyDirectory(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private void extractZipOrRarFile(string downloadedFile, string destination)
        {
            using (Stream stream = File.OpenRead(downloadedFile))
            using (var reader = ReaderFactory.Open(stream))
            {
                while (reader.MoveToNextEntry())
                {
                    if (!reader.Entry.IsDirectory)
                    {
                        using (var entryStream = reader.OpenEntryStream())
                        {
                            string filepath = Path.Combine(destination, reader.Entry.Key);
                            Directory.CreateDirectory(Path.GetDirectoryName(filepath));
                            using (FileStream destStream = File.Create(filepath))
                            {
                                entryStream.CopyTo(destStream);
                            }
                        }
                    }
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
            {
                // We have to delete our client manually when we close the window
                client.Dispose();
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progBarMapDownload.Value = e.ProgressPercentage;
        }

        private void btnUninstallMap_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedLvis = lstVwInstalledMaps.SelectedItems;
            foreach (ListViewItem lvi in selectedLvis)
            {
                string mapFolder = lvi.SubItems[1].Text;
                string fullMapFolderPath = settings.AddonLocation + "\\" + mapFolder;

                Map m = findMapFromFolder(knownMaps.Maps, mapFolder);

                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("Are you sure wish to uninstall '" + m.Name + "' and delete it from your computer?", "Uninstall map", buttons);
                if (result == DialogResult.Yes)
                {
                    Console.WriteLine(fullMapFolderPath);
                    Directory.Delete(fullMapFolderPath, true);
                    lstVwInstalledMaps.Items.Remove(lvi);
                    lstVwInstalledMaps.RedrawItems(0, lstVwInstalledMaps.Items.Count - 1, false);
                    // Get and display number of maps installed
                    txtNumMapsInstalled.Text = lstVwInstalledMaps.Items.Count.ToString();
                }
                else
                {
                    this.Close();
                }
                installedMaps.Remove(m);
            }
        }

        
    }
}
