using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeApiSharp
{
    class Continuation
    {
        static WebClient webclient;
        public static async Task<string> Scrape(string initContToken)
        {
            var continuationToken = initContToken;
            var continuationPost = @"{
                    'context': {
                        'client': {
                            'clientName': '1',
                            'clientVersion': '2.20200701.03.01'
                        }
                    },
                    'continuation': '" + continuationToken + @"'
                }";

            try
            {
                webclient = new WebClient();
                webclient.Encoding = Encoding.Default;

                Task<string> uploadStringTask = webclient.UploadStringTaskAsync(new Uri("https://www.youtube.com/youtubei/v1/search?key=AIzaSyAO_FJ2SlqU8Q4STEHLGCilw_Y9_11qcW8"), continuationPost);
                var content = await uploadStringTask;

                return content.Replace('\r', ' ').Replace('\n', ' ');
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
