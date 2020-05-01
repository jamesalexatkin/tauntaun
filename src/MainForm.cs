using Flurl;
using HtmlAgilityPack;
using Newtonsoft.Json;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Readers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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
        const string ModdbBaseUrl = "https://www.moddb.com/";
        //const string gamefrontBaseUrl = "https://www.gamefront.com/";

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
                WriteSettings(settings, SettingsFilename);
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

                string[] subdirs = GetDirectoriesJustNames(settings.AddonLocation);

                RefreshInstalledMaps(subdirs);

                // Add all known maps to Get Maps ListView
                foreach (Map m in knownMaps.Maps)
                {
                    // Only consider standalone maps or map packs (not maps that come as part of a map pack)
                    if (m.PartOfMapPack == null)
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
                        Image mapImage = FindMapImage(m);
                        imgLstGetMaps.Images.Add(mapImage);
                        lvi.ImageIndex = imgLstGetMaps.Images.Count - 1;

                        lstVwGetMaps.Items.Add(lvi);
                    }
                }

                // Get and display number of maps available
                txtNumMapsAvailable.Text = lstVwGetMaps.Items.Count.ToString();
            }

            // Check if this is the first time the user has opened the app
            if (settings.FirstTime)
            {
                // Move to settings tab
                tabCtrl.SelectedIndex = 2;
            }
        }

        private void RefreshInstalledMaps(string[] subdirs)
        {
            lstVwInstalledMaps.Clear();
            installedMaps = new ArrayList();
            // Add installed maps to ListView
            foreach (string subdir in subdirs)
            {
                Map mapFound = FindMapFromFolder(knownMaps.Maps, subdir);
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
                Image mapImage = FindMapImage(mapFound);
                imgLstInstalled.Images.Add(mapImage);
                lvi.ImageIndex = imgLstInstalled.Images.Count - 1;

                lstVwInstalledMaps.Items.Add(lvi);
            }

            // Get and display number of maps installed
            txtNumMapsInstalled.Text = lstVwInstalledMaps.Items.Count.ToString();
        }

        private Image FindMapImage(Map m)
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

        private string[] GetDirectoriesJustNames(string root)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(root);
                for (int i = 0; i < dirs.Length; i++)
                {
                    string dirName = new DirectoryInfo(dirs[i]).Name;
                    dirs[i] = dirName;
                }
                return dirs;
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                MessageBox.Show("'addon' folder not found. Please check that the directory exists and amend it in the 'Settings' tab.", "Couldn't find 'addon' folder.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string[] empty = new string[0];
                return empty;
            }
        }

        private Map FindMapFromFolder(List<Map> maps, string subdir)
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

        private void WriteSettings(Settings settings, string settingsFilename)
        {
            settings.FirstTime = false;
            string jsonToOutput = JsonConvert.SerializeObject(settings);
            System.IO.File.WriteAllText(settingsFilename, jsonToOutput);
        }

        private void tabCtrl_SelectedIndexChanged(Object sender, EventArgs e)
        {
            // Installed maps
            if (tabCtrl.SelectedTab == tabCtrl.TabPages[0])
            {
                RefreshInstalledMaps(GetDirectoriesJustNames(settings.AddonLocation));
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
            string addonFolderPath = settings.AddonLocation;

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
            //updatedSettings.Theme = cmbTheme.SelectedItem.ToString();

            if (Directory.Exists(updatedSettings.AddonLocation))
            {
                WriteSettings(updatedSettings, SettingsFilename);

                settings = updatedSettings;

                MessageBox.Show("Settings saved!");
            }
            else
            {
                MessageBox.Show("'addon' folder not found. Please check that the directory exists.", "Couldn't find 'addon' folder.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstVwInstalledMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedLvis = lstVwInstalledMaps.SelectedItems;
            if (selectedLvis.Count > 0)
            {
                // We only display the first item
                ListViewItem curMapLvi = selectedLvis[0];
                string mapFolder = curMapLvi.SubItems[1].Text;
                Map curMap = FindMapFromFolder(knownMaps.Maps, mapFolder);

                picBoxCurMapInstalled.Image = FindMapImage(curMap);

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
                Map curMap = FindMapFromFolder(knownMaps.Maps, mapFolder);

                picBoxCurMapGet.Image = FindMapImage(curMap);

                txtNameCurMapGet.Text = curMap.Name;
                txtAuthorCurMapGet.Text = curMap.Author;
                txtFolderCurMapGet.Text = curMap.Folder;
                txtTypeCurMapGet.Text = curMap.Type;
                txtDownloadCurMapGet.Text = curMap.DownloadUrl;

                if (installedMaps.Contains(curMap))
                {
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
                        Map m = FindMapFromFolder(knownMaps.Maps, mapFolder);
                        string downloadUrl = m.DownloadUrl;

                        // Check if link is to moddb
                        if (new Regex(ModdbBaseUrl + ".*").IsMatch(downloadUrl))
                        {
                            DownloadModdbMap(mapFolder, m, downloadUrl);
                        }
                        /*// Check if link is to gamefront
                        else if (new Regex(gamefrontBaseUrl + ".*").Match(downloadUrl).Success)
                        {
                            downloadGamefrontMap(mapFolder, m, downloadUrl);
                        }*/
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

        /*private void downloadGamefrontMap(string mapFolder, Map m, string downloadUrl)
        {
            // Download mod listing page
            string tempMainPagePath = Path.Combine(TempDir, mapFolder + "GamefrontMainPage.html");
            client = new MapInstallClient(m);
            client.DownloadFile(downloadUrl, tempMainPagePath);

            // TODO: Parse correct page element for download link
            HtmlAgilityPack.HtmlDocument gamefrontMainPage = new HtmlAgilityPack.HtmlDocument();
            gamefrontMainPage.LoadHtml(File.ReadAllText(tempMainPagePath));
            HtmlAgilityPack.HtmlNode downloadButton = gamefrontMainPage.GetElementbyId("downloadmirrorstoggle");
            string downloadPageUrl = Url.Combine(moddbBaseUrl, downloadButton.GetAttributeValue("href", ""));
        }*/

        private void DownloadModdbMap(string mapFolder, Map m, string downloadUrl)
        {
            // Download mod listing page
            string tempMainPagePath = Path.Combine(TempDir, mapFolder + "ModdbMainPage.html");
            client = new MapInstallClient(m);
            client.DownloadFile(downloadUrl, tempMainPagePath);

            // Scan for download button and retrieve link
            HtmlAgilityPack.HtmlDocument moddbMainPage = new HtmlAgilityPack.HtmlDocument();
            moddbMainPage.LoadHtml(File.ReadAllText(tempMainPagePath));
            HtmlAgilityPack.HtmlNode downloadButton = moddbMainPage.GetElementbyId("downloadmirrorstoggle");
            string downloadPageUrl = Url.Combine(ModdbBaseUrl, downloadButton.GetAttributeValue("href", ""));

            // Download download page
            string tempDownloadPagePath = Path.Combine(TempDir, mapFolder + "DownloadPage.html");
            client.DownloadFile(downloadPageUrl, tempDownloadPagePath);

            // Scan for a tags in download page
            HtmlAgilityPack.HtmlDocument moddbDownloadPage = new HtmlAgilityPack.HtmlDocument();
            moddbDownloadPage.LoadHtml(File.ReadAllText(tempDownloadPagePath));
            IEnumerable<HtmlAgilityPack.HtmlNode> anchors = moddbDownloadPage.DocumentNode.Descendants("a");
            if (anchors != null)
            {
                // Regex matches text displayed on page for correct download link
                Regex downloadFile = new Regex(@"download (.*)\.(.*)");

                foreach (HtmlNode a in anchors)
                {
                    Match match = downloadFile.Match(a.InnerText);

                    if (match.Success)
                    {
                        string filename = match.Groups[1].ToString();
                        string fileExtension = match.Groups[2].ToString();
                        string destFile = Path.Combine(TempDir, filename + "." + fileExtension);

                        string realDownloadUrl = Url.Combine(ModdbBaseUrl, a.GetAttributeValue("href", ""));

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
                ParseInstallInstructions(client.downloadedFile, installationInstructions);
            }
            catch (MapInstallException mapInstallEx)
            {
                MessageBox.Show(mapInstallEx.Message, "Map installation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MessageBox.Show("Finished installing " + mapToInstall.Name + ".");
        }

        private void ParseInstallInstructions(string downloadedFile, string installationInstructions)
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
                                    ExtractZipOrRarFile(downloadedFile, destination);
                                    break;

                                case ".7z":
                                    ExtractSevenZipFile(downloadedFile, destination);
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

                            string dirToMove = "";

                            // Sometimes the directory we want to copy is nested inside the dir we have downloaded
                            // In the cases of map packs with multiple maps, this is especially so
                            // Here, we check for addme.script files, each of which represents a map and is contained within the dir to copy
                            string[] fileMatches = Directory.GetFiles(Path.Combine(TempDir, downloadedFileWoExtension), "addme.script", SearchOption.AllDirectories);

                            if (fileMatches.Length > 0)
                            {
                                foreach (string fileMatch in fileMatches)
                                {
                                    // Get dir that addme.script is in
                                    string dirOfFile = System.IO.Path.GetDirectoryName(fileMatch);
                                    // Find the deepest subdir
                                    string lastDir = new Uri(dirOfFile).Segments.Last();
                                    // If this is the folder we want, take it and break
                                    if (folderToMove == lastDir)
                                    {
                                        dirToMove = dirOfFile;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                throw new MapInstallException("No map data found.");
                            }

                            destination = words[3];
                            // Handle addon shorthand
                            if (destination == "addon")
                            {
                                destination = Path.Combine(settings.AddonLocation, folderToMove);
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
                                MessageBox.Show("Installation will continue in external installer. You may want to copy your 'addon' folder path from the 'Settings' tab.");
                                Process.Start(fileToRun);
                            }
                            else
                            {
                                throw new MapInstallException("Unrecognised installer");
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

        private void ExtractZipOrRarFile(string downloadedFile, string destination)
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

        private void ExtractSevenZipFile(string downloadedFile, string destination)
        {
            using (Stream stream = File.OpenRead(downloadedFile))
            using (var archive = SevenZipArchive.Open(stream))
            using (var reader = archive.ExtractAllEntries())
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
                // Overwrite flag is set so file will overwrite an existing file if a conflict occurs
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



        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progBarMapDownload.Value = e.ProgressPercentage;
            lblInstallStatus.Text = "Downloading... (" + FormatBytes(e.BytesReceived) + " of " + FormatBytes(e.TotalBytesToReceive) + ")";
        }

        private string FormatBytes(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (bytes >= 1024 && order < sizes.Length - 1)
            {
                order++;
                bytes = bytes / 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            string result = String.Format("{0:0.##} {1}", bytes, sizes[order]);
            return result;
        }

        private void btnUninstallMap_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedLvis = lstVwInstalledMaps.SelectedItems;
            foreach (ListViewItem lvi in selectedLvis)
            {
                string mapFolder = lvi.SubItems[1].Text;
                string fullMapFolderPath = settings.AddonLocation + "\\" + mapFolder;

                Map m = FindMapFromFolder(knownMaps.Maps, mapFolder);

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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
            {
                // We have to delete our client manually when we close the window
                client.Dispose();
            }
        }
    }
}
