namespace WompRat
{
    public class Map
    {
        public string Name { get; set; }
        public string Folder { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public string DownloadLink { get; set; }

        public Map(string name, string folder, string author, string type, string downloadLink)
        {
            this.Name = name;
            this.Folder = folder;
            this.Author = author;
            this.Type = type;
            this.DownloadLink = downloadLink;
        }
    }
}