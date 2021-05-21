using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YouTubeApiSharp
{
	public class PlaylistSearch
	{
        static List<PlaylistSearchComponents> items;

        private static String Id;
        private static String Title;
        private static String Author;
        private static String VideoCount;
        private static String Thumbnail;
        private static String Url;

        static string continuationCommand = string.Empty;

        public async Task<List<PlaylistSearchComponents>> GetPlaylists(string querystring, int querypages)
        {
            items = new List<PlaylistSearchComponents>();

            // Do search
            for (int i = 1; i <= querypages; i++)
            {
                string content = string.Empty;
                if (i == 1) // First page
                {
                    // Search address
                    content = await Web.getContentFromUrlWithProperty("https://www.youtube.com/results?search_query=" + querystring.Replace(" ", "+") + "&sp=EgIQAw%253D%253D");

                    // Continuation command
                    continuationCommand = Helper.ExtractValue(content, "\"continuationCommand\":{\"token\":\"", "\"").Replace("%3D", "=").Replace("%2F", "/");
                    if (Log.getMode())
                        Log.println(Helper.Folder, "continuationCommand: " + continuationCommand);

                    // Search string
                    string pattern = "playlistRenderer\":\\{\"playlistId\":\"(?<ID>.*?)\",\"title\":\\{\"simpleText\":\"(?<TITLE>.*?)\"},\"thumbnails\":\\[\\{\"thumbnails\":\\[\\{\"url\":\"(?<THUMBNAIL>.*?)\".*?videoCount\":\"(?<VIDEOCOUNT>.*?)\".*?\\{\"webCommandMetadata\":\\{\"url\":\"(?<URL>.*?)\".*?\"shortBylineText\":\\{\"runs\":\\[\\{\"text\":\"(?<AUTHOR>.*?)\"";
                    MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

                    for (int ctr = 0; ctr <= result.Count - 1; ctr++)
                    {
                        if (Log.getMode())
                            Log.println(Helper.Folder, "Match: " + result[ctr].Value);

                        // Id
                        Id = result[ctr].Groups[1].Value;

                        if (Log.getMode())
                            Log.println(Helper.Folder, "Id: " + Id);

                        // Title
                        Title = result[ctr].Groups[2].Value.Replace(@"\u0026", "&");

                        if (Log.getMode())
                            Log.println(Helper.Folder, "Title: " + Title);

                        // Author
                        Author = result[ctr].Groups[6].Value.Replace(@"\u0026", "&");

                        if (Log.getMode())
                            Log.println(Helper.Folder, "Author: " + Author);

                        // VideoCount
                        VideoCount = result[ctr].Groups[4].Value;

                        if (Log.getMode())
                            Log.println(Helper.Folder, "VideoCount: " + VideoCount);

                        // Thumbnail
                        Thumbnail = result[ctr].Groups[3].Value;

                        if (Log.getMode())
                            Log.println(Helper.Folder, "Thumbnail: " + Thumbnail);

                        // Url
                        Url = "http://youtube.com" + result[ctr].Groups[5].Value.Replace("\\u0026", "&");

                        if (Log.getMode())
                            Log.println(Helper.Folder, "Url: " + Url);

                        // Add item to list
                        items.Add(new PlaylistSearchComponents(Id, Utilities.HtmlDecode(Title),
                            Utilities.HtmlDecode(Author), VideoCount, Thumbnail, Url));
                    }
                }
                else // Next page
                {
                    // Search address
                    content = await Continuation.Scrape(continuationCommand);

                    // Continuation command
                    continuationCommand = Helper.ExtractValue(content.Replace("  ", ""), "\"continuationCommand\": { \"token\": \"", "\"").Replace("%3D", "=").Replace("%2F", "/");
                    if (Log.getMode())
                        Log.println(Helper.Folder, "continuationCommand: " + continuationCommand);

                    content = content.Replace("  ", "");

                    // Search string
                    string pattern = "playlistRenderer\":\\ { \"playlistId\": \"(?<ID>.*?)\", \"title\": \\{ \"simpleText\": \"(?<TITLE>.*?)\" }, \"thumbnails\": \\[ \\{ \"thumbnails\": \\[ \\{ \"url\": \"(?<THUMBNAIL>.*?)\".*?videoCount\": \"(?<VIDEOCOUNT>.*?)\".*?\\{ \"webCommandMetadata\": \\{ \"url\": \"(?<URL>.*?)\".*?\"shortBylineText\": \\{ \"runs\": \\[ \\{ \"text\": \"(?<AUTHOR>.*?)\"";
                    MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

                    for (int ctr = 0; ctr <= result.Count - 1; ctr++)
                    {
                        if (Log.getMode())
                            Log.println(Helper.Folder, "Match: " + result[ctr].Value);

                        // Id
                        Id = result[ctr].Groups[1].Value;

                        if (Log.getMode())
                            Log.println(Helper.Folder, "Id: " + Id);

                        // Title
                        Title = result[ctr].Groups[2].Value.Replace(@"\u0026", "&");

                        if (Log.getMode())
                            Log.println(Helper.Folder, "Title: " + Title);

                        // Author
                        Author = result[ctr].Groups[6].Value.Replace(@"\u0026", "&");

                        if (Log.getMode())
                            Log.println(Helper.Folder, "Author: " + Author);

                        // VideoCount
                        VideoCount = result[ctr].Groups[4].Value;

                        if (Log.getMode())
                            Log.println(Helper.Folder, "VideoCount: " + VideoCount);

                        // Thumbnail
                        Thumbnail = result[ctr].Groups[3].Value;

                        if (Log.getMode())
                            Log.println(Helper.Folder, "Thumbnail: " + Thumbnail);

                        // Url
                        Url = "http://youtube.com" + result[ctr].Groups[5].Value.Replace("\\u0026", "&");

                        if (Log.getMode())
                            Log.println(Helper.Folder, "Url: " + Url);

                        var c = items.FindAll(item => item.getUrl().Contains(Url)).Count; // Item not in list already
                        if (c == 0)
                        {
                            // Add item to list
                            items.Add(new PlaylistSearchComponents(Id, Utilities.HtmlDecode(Title),
                            Utilities.HtmlDecode(Author), VideoCount, Thumbnail, Url));
                        }
                    }
                }
            }

            return items;
        }
    }
}
