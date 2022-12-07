using System.Net;

namespace InternetManager
{
    public class NetworkManager
    {
        public static string DownloadJSON(string URL)
        {
            WebClient client = new WebClient();
            return client.DownloadString(URL);
        }
    }
}