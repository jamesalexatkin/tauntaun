using System.Net;

namespace Tauntaun
{
    public class MapInstallClient : WebClient
    {
        public Map mapToInstall { get; set; }
        public string downloadedFile { get; set; }

        public MapInstallClient(Map mapToInstall)
        {
            this.mapToInstall = mapToInstall;
        }

        public MapInstallClient()
        {
        }
    }
}