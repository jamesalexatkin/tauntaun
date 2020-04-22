using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace WompRat
{
    public partial class MainForm : MaterialForm
    {
        const string SettingsFilename = "settings.json";
        Settings settings;
        MaterialSkinManager materialSkinManager;

        public MainForm()
        {
            InitializeComponent();

            // Load settings
            string jsonString = File.ReadAllText(SettingsFilename);
            Console.WriteLine(jsonString);
            settings = JsonConvert.DeserializeObject<Settings>(jsonString);
            Console.WriteLine(settings.Theme);


            // Create a material theme manager and add the form to manage (this)
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            updateTheme();
            
            
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

            string jsonToOutput = JsonConvert.SerializeObject(updatedSettings);
            System.IO.File.WriteAllText(SettingsFilename, jsonToOutput);

            settings = updatedSettings;

            MessageBox.Show("Settings saved!");

            updateTheme();
        }

        private void matTabCtrl_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (matTabCtrl.SelectedTab == matTabCtrl.TabPages[0])
            {

            }
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
    }
}
