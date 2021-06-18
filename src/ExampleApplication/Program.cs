using System;
using System.Collections.Generic;
using System.Linq;

using YouTubeApiSharp;

namespace ExampleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            /* ********** */
            /* Unit Tests */
            /* ********** */

            /* Search video */
            Search();

            /* Download audio, video */
            //Download();

            /* Search playlist */
            //Search_Playlist();

            /* Search playlist items */
            //Search_PlaylistItems();

            /* Search channel */
            //Search_Channel();

            /* Search channel items */
            //Search_ChannelItems();

            Console.ReadLine();
        }

        static async void Search()
        {
            // Disable logging
            Log.setMode(false);

            // Keyword
            string querystring = "Kurdo";

            // Number of result pages
            int querypages = 1;

            ////////////////////////////////
            // Start searching
            ////////////////////////////////

            VideoSearch videos = new VideoSearch();
            var items = await videos.GetVideos(querystring, querypages);

            int i = 1;

            foreach (var item in items)
            {
                Console.WriteLine("# " + i);
                Console.WriteLine("Title: " + item.getTitle());
                Console.WriteLine("Author: " + item.getAuthor());
                Console.WriteLine("Description: " + item.getDescription());
                Console.WriteLine("Duration: " + item.getDuration());
                Console.WriteLine("Url: " + item.getUrl());
                Console.WriteLine("Thumbnail: " + item.getThumbnail());
                Console.WriteLine("ViewCount: " + item.getViewCount());
                Console.WriteLine("");
                i++;
            }
        }

        static async void Search_PlaylistItems()
        {
            // Disable logging
            Log.setMode(false);

            // Url
            string playlisturl = "https://www.youtube.com/watch?v=hvP9_UEVIw4&list=PL90240A2D521E753B";

            // Items
            int plitems = 786;

            ////////////////////////////////
            // Start searching
            ////////////////////////////////

            PlaylistItemsSearch playlistItems = new PlaylistItemsSearch();
            var items = await playlistItems.GetPlaylistItems(playlisturl, plitems);

            int i = 0;

            foreach (var item in items)
            {
                i += 1;
                Console.WriteLine("#" + i);
                Console.WriteLine("Title: " + item.getTitle());
                Console.WriteLine("Author: " + item.getAuthor());
                Console.WriteLine("Duration: " + item.getDuration());
                Console.WriteLine("Thumbnail: " + item.getThumbnail());
                Console.WriteLine("Url: " + item.getUrl());
                Console.WriteLine("");
            }
        }

        static async void Search_ChannelItems()
        {
            // Disable logging
            Log.setMode(false);

            // Url
            string channelurl = "https://www.youtube.com/channel/UCHte7RKGIYJXDZKShCNz9gw";

            // Items
            int chitems = 184;

            ////////////////////////////////
            // Start searching
            ////////////////////////////////

            ChannelItemsSearch channelItems = new ChannelItemsSearch();
            var items = await channelItems.GetChannelItems(channelurl, chitems);

            int i = 0;

            foreach (var item in items)
            {
                i += 1;
                Console.WriteLine("#" + i);
                Console.WriteLine("Title: " + item.getTitle());
                Console.WriteLine("Author: " + item.getAuthor());
                Console.WriteLine("Duration: " + item.getDuration());
                Console.WriteLine("Thumbnail: " + item.getThumbnail());
                Console.WriteLine("Url: " + item.getUrl());
                Console.WriteLine("");
            }
        }

        static async void Search_Playlist()
        {
            // Disable logging
            Log.setMode(false);

            // Keyword
            string querystring = "Kurdo";

            // Number of result pages
            int querypages = 2;

            ////////////////////////////////
            // Start searching
            ////////////////////////////////

            PlaylistSearch playlist = new PlaylistSearch();
            var items = await playlist.GetPlaylists(querystring, querypages);

            int i = 0;

            foreach (var item in items)
            {
                i += 1;
                Console.WriteLine("#" + i);
                Console.WriteLine("Id: " + item.getId());
                Console.WriteLine("Title: " + item.getTitle());
                Console.WriteLine("Author: " + item.getAuthor());
                Console.WriteLine("VideoCount: " + item.getVideoCount());
                Console.WriteLine("Thumbnail: " + item.getThumbnail());
                Console.WriteLine("Url: " + item.getUrl());
                Console.WriteLine("");
            }
        }

        static async void Search_Channel()
        {
            // Disable logging
            Log.setMode(false);

            // Keyword
            string querystring = "Kurdo";

            // Number of result pages
            int querypages = 2;

            ////////////////////////////////
            // Start searching
            ////////////////////////////////

            ChannelSearch channel = new ChannelSearch();
            var items = await channel.GetChannels(querystring, querypages);

            int i = 0;

            foreach (var item in items)
            {
                i += 1;
                Console.WriteLine("#" + i);
                Console.WriteLine("Id: " + item.getId());
                Console.WriteLine("Title: " + item.getTitle());
                Console.WriteLine("Description: " + item.getDescription());
                Console.WriteLine("VideoCount: " + item.getVideoCount());
                Console.WriteLine("SubscriberCount: " + item.getSubscriberCount());
                Console.WriteLine("Thumbnail: " + item.getThumbnail());
                Console.WriteLine("Url: " + item.getUrl());
                Console.WriteLine("");
            }
        }

        static void Download()
        {
            // Disable logging
            Log.setMode(false);

            // YouTube url

            // Protected
            string link = "https://www.youtube.com/watch?v=LN--3zgY5oM";

            // Free
            //string link = "https://www.youtube.com/watch?v=curXTlNnGho";

            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link, false);

            foreach (var v in videoInfos)
                Console.WriteLine(v.ToString() + ", Audio bitrate: " + v.AudioBitrate + ", Adaptive type: " + v.AdaptiveType);

            DownloadVideo(videoInfos);
        }

        private static void DownloadVideo(IEnumerable<VideoInfo> videoInfos)
        {
            // Select the first .mp4 video with 360p resolution
            VideoInfo video = videoInfos
                .First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);

            // Decrypt only if needed
            if (video.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(video);
            }

            // Create the video downloader.
            VideoDownloader dl = new VideoDownloader();
            dl.DownloadFile(video.DownloadUrl, video.Title, true, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), video.VideoExtension);
        }
    }
}
