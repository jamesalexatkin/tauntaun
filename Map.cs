namespace WompRat
{
    public class Map
    {
        public string Name { get; set; }
        public string Folder { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public string DownloadUrl { get; set; }
        public string ImageUrl { get; set; }

        public Map(string name, string folder, string author, string type, string downloadUrl, string imageUrl)
        {
            this.Name = name;
            this.Folder = folder;
            this.Author = author;
            this.Type = type;
            this.DownloadUrl = downloadUrl;
            this.ImageUrl = imageUrl;
        }
    }
}