namespace Tauntaun
{
    internal class Settings
    {
        public string AddonLocation { get; set; }
        public string Theme { get; set; }
        public bool FirstTime { get; set; }

        public Settings()
        {
            // Default constructor with default values
            this.AddonLocation = "C://";
            this.Theme = "light";
            this.FirstTime = true;
        }
    }
}