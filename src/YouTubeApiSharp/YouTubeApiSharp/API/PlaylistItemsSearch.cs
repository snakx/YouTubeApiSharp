using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YouTubeApiSharp
{
    public class PlaylistItemsSearch
    {
        static List<PlaylistItemsSearchComponents> items;

        private String Title;
        private String Author;
        private String Duration;
        private String Url;
        private String Thumbnail;

        bool hasMore = false;
        string indexUrl = string.Empty;
        string tempUrl = string.Empty;
        int index = 0;

        public async Task<List<PlaylistItemsSearchComponents>> GetPlaylistItems(string Playlisturl, int PlaylistItems)
        {
            items = new List<PlaylistItemsSearchComponents>();

            // Do search
            // Search address
            HASMORE:
            string content = string.Empty;
            if (!hasMore)
                content = await Web.getContentFromUrlWithProperty(Playlisturl);
            else
                content = await Web.getContentFromUrlWithProperty(indexUrl);

            // Search string
            string pattern = "playlistPanelVideoRenderer\":\\{\"title\":\\{\"accessibility\":\\{\"accessibilityData\":\\{\"label\":.*?\\,\"simpleText\":\"(?<TITLE>.*?)\".*?runs\":\\[\\{\"text\":\"(?<AUTHOR>.*?)\".*?\":\\{\"thumbnails\":\\[\\{\"url\":\"(?<THUMBNAIL>.*?)\".*?\"}},\"simpleText\":\"(?<DURATION>.*?)\".*?videoId\":\"(?<URL>.*?)\"";
            MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

            for (int ctr = 0; ctr <= result.Count - 1; ctr++)
            {
                index += 1;
                if (Log.getMode())
                    Log.println(Helper.Folder, "Match: " + result[ctr].Value);

                // Title
                Title = result[ctr].Groups[1].Value.Replace(@"\u0026", "&");

                if (Log.getMode())
                    Log.println(Helper.Folder, "Title: " + Title);

                // Author
                Author = result[ctr].Groups[2].Value.Replace(@"\u0026", "&");

                if (Log.getMode())
                    Log.println(Helper.Folder, "Author: " + Author);

                // Duration
                Duration = result[ctr].Groups[4].Value;

                if (Log.getMode())
                    Log.println(Helper.Folder, "Duration: " + Duration);

                // Thumbnail
                Thumbnail = result[ctr].Groups[3].Value;

                if (Log.getMode())
                    Log.println(Helper.Folder, "Thumbnail: " + Thumbnail);

                // Url
                Url = "http://youtube.com/watch?v=" + result[ctr].Groups[5].Value;

                if (Log.getMode())
                    Log.println(Helper.Folder, "Url: " + Url);

                if (hasMore)
                {
                    var c = items.FindAll(item => item.getUrl().Contains(Url)).Count; // Item not in list already
                    if (c == 0)
                    {
                        // Add item to list
                        items.Add(new PlaylistItemsSearchComponents(Utilities.HtmlDecode(Title),
                            Utilities.HtmlDecode(Author), Duration, Url, Thumbnail));
                    }
                }
                else
                {
                    // Add item to list
                    items.Add(new PlaylistItemsSearchComponents(Utilities.HtmlDecode(Title),
                        Utilities.HtmlDecode(Author), Duration, Url, Thumbnail));
                }
                
                // Temp url for parsing more items
                tempUrl = Url;
            }

            if (index < PlaylistItems) // Load more items
            {
                hasMore = true;
                indexUrl = tempUrl + "&list=" + Helper.ExtractValue(Playlisturl + ";", "list=", ";") + "&index=" + index;
                goto HASMORE;
            }

            return items;
        }
    }
}