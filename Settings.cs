namespace WompRat
{
    internal class Settings
    {
        public string AddonLocation { get; set; }
        public string Theme { get; set; }

        public Settings()
        {
            // Default constructor with default values
            this.AddonLocation = "C://";
            this.Theme = "light";
        }
    }
}