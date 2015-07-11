using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Messager.Interpals
{
    public static class Downloader
    {
        public static void Download(string url, string path)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile(new Uri(url),path);
        }
    }
}
