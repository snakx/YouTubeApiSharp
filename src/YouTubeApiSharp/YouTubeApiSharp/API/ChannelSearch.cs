using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YouTubeApiSharp
{
    public class ChannelSearch
    {
        static List<ChannelSearchComponents> items;

        private String Id;
        private String Title;
        private String Description;
        private String VideoCount;
        private String SubscriberCount;
        private String Thumbnail;
        private String Url;

        static string continuationCommand = string.Empty;

        public async Task<List<ChannelSearchComponents>> GetChannels(string querystring, int querypages)
        {
            items = new List<ChannelSearchComponents>();

            // Do search
            for (int i = 1; i <= querypages; i++)
            {
                string content = string.Empty;
                if (i == 1) // First page
                {
                    // Search address
                    content = await Web.getContentFromUrlWithProperty("https://www.youtube.com/results?search_query=" + querystring.Replace(" ", "+") + "&sp=EgIQAg%253D%253D");

                    // Continuation command
                    continuationCommand = Helper.ExtractValue(content, "\"continuationCommand\":{\"token\":\"", "\"").Replace("%3D", "=").Replace("%2F", "/");
                    if (Log.getMode())
                        Log.println(Helper.Folder, "continuationCommand: " + continuationCommand);

                    content = Helper.ExtractValue(content, "ytInitialData", "ytInitialPlayerResponse");

                    // Search string
                    string pattern = "channelRenderer\":\\{\"channelId\":\"(?<ID>.*?)\",\"title\":{\"simpleText\":\"(?<TITLE>.*?)\".*?\"canonicalBaseUrl\":\"(?<URL>.*?)\"}}.*?\\{\"thumbnails\":\\[\\{\"url\":\"(?<THUMBNAIL>.*?)\".*?videoCountText\":\\{\"runs\":\\[\\{\"text\":\"(?<VIDEOCOUNT>.*?)\".*?clickTrackingParams";
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

                        // Description
                        Description = Helper.ExtractValue(result[ctr].Value, "\"descriptionSnippet\":{\"runs\":[{\"text\":\"", "\"}]},").Replace(@"\u0026", "&");

                        if (Log.getMode())
                            Log.println(Helper.Folder, "Description: " + Description);

                        // VideoCount
                        VideoCount = result[ctr].Groups[5].Value;

                        if (VideoCount.Contains(" ")) // -> 1 Video
                            VideoCount = VideoCount.Replace(" Video", "");

                        if (Log.getMode())
                            Log.println(Helper.Folder, "VideoCount: " + VideoCount);

                        // SubscriberCount
                        SubscriberCount = Helper.ExtractValue(result[ctr].Value, "subscriberCountText\":{\"accessibility\":{\"accessibilityData\":{\"label\":\"", " ");

                        if (Log.getMode())
                            Log.println(Helper.Folder, "SubscriberCount: " + SubscriberCount);

                        // Thumbnail
                        if (result[ctr].Groups[4].Value.StartsWith("https"))
                            Thumbnail = result[ctr].Groups[4].Value;
                        else
                            Thumbnail = "https:" + result[ctr].Groups[4].Value;

                        if (Log.getMode())
                            Log.println(Helper.Folder, "Thumbnail: " + Thumbnail);

                        // Url
                        Url = "http://youtube.com" + result[ctr].Groups[3].Value;

                        if (Log.getMode())
                            Log.println(Helper.Folder, "Url: " + Url);

                        // Add item to list
                        items.Add(new ChannelSearchComponents(Id, Utilities.HtmlDecode(Title),
                            Utilities.HtmlDecode(Description), VideoCount, SubscriberCount, Url, Thumbnail));
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
                    string pattern = "channelRenderer\":\\ { \"channelId\": \"(?<ID>.*?)\", \"title\": { \"simpleText\": \"(?<TITLE>.*?)\".*?\"canonicalBaseUrl\": \"(?<URL>.*?)\" } }.*?\\{ \"thumbnails\": \\[ \\{ \"url\": \"(?<THUMBNAIL>.*?)\".*?videoCountText\": \\{ \"runs\": \\[ \\{ \"text\": \"(?<VIDEOCOUNT>.*?)\".*?clickTrackingParams";
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

                        // Description
                        Description = Helper.ExtractValue(result[ctr].Value.Replace("  ", ""), "\"descriptionSnippet\": { \"runs\": [ { \"text\": \"", "\" } ] },").Replace(@"\u0026", "&");

                        if (Log.getMode())
                            Log.println(Helper.Folder, "Description: " + Description);

                        // VideoCount
                        VideoCount = result[ctr].Groups[5].Value;

                        if (VideoCount.Contains(" ")) // -> 1 Video
                            VideoCount = VideoCount.Replace(" Video", "");

                        if (Log.getMode())
                            Log.println(Helper.Folder, "VideoCount: " + VideoCount);

                        // SubscriberCount
                        SubscriberCount = Helper.ExtractValue(result[ctr].Value.Replace("  ", ""), "subscriberCountText\":{\"accessibility\":{\"accessibilityData\":{\"label\":\"", " ");

                        if (Log.getMode())
                            Log.println(Helper.Folder, "SubscriberCount: " + SubscriberCount);

                        // Thumbnail
                        if (result[ctr].Groups[4].Value.StartsWith("https"))
                            Thumbnail = result[ctr].Groups[4].Value;
                        else
                            Thumbnail = "https:" + result[ctr].Groups[4].Value;

                        if (Log.getMode())
                            Log.println(Helper.Folder, "Thumbnail: " + Thumbnail);

                        // Url
                        Url = "http://youtube.com" + result[ctr].Groups[3].Value;

                        if (Log.getMode())
                            Log.println(Helper.Folder, "Url: " + Url);

                        var c = items.FindAll(item => item.getUrl().Contains(Url)).Count; // Item not in list already
                        if (c == 0)
                        {
                            // Add item to list
                            items.Add(new ChannelSearchComponents(Id, Utilities.HtmlDecode(Title),
                            Utilities.HtmlDecode(Description), VideoCount, SubscriberCount, Url, Thumbnail));
                        }
                    }
                }
            }

            return items;
        }
    }
}
