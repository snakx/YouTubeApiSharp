using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeApiSharp
{
    class Web
    {
        static WebClient webclient;

        public static async Task<String> getContentFromUrl(String Url)
        {
            try
            {
                webclient = new WebClient();
                webclient.Encoding = Encoding.Default;

                Task<string> downloadStringTask = webclient.DownloadStringTaskAsync(new Uri(Url));
                var content = await downloadStringTask;

                return content.Replace('\r', ' ').Replace('\n', ' ');
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static async Task<String> getContentFromUrlWithProperty(String Url)
        {
            try
            {
                webclient = new WebClient();
                webclient.Encoding = Encoding.Default;
                webclient.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3731.0 Safari/537.36 Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Ubuntu Chromium/75.0.3731.0 Chrome/75.0.3731.0 Safari/537.36";

                Task<string> downloadStringTask = webclient.DownloadStringTaskAsync(new Uri(Url));
                var content = await downloadStringTask;

                return content.Replace('\r', ' ').Replace('\n', ' ');
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
