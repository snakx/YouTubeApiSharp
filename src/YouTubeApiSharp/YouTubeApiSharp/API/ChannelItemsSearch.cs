using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YouTubeApiSharp
{
	public class ChannelItemsSearch
	{
		public async Task<List<PlaylistItemsSearchComponents>> GetChannelItems(string Channelurl, int ChannelItems)
		{
			// Do search
			// Search address
			string content = await Web.getContentFromUrlWithProperty(Channelurl);

			string chUrl = string.Empty;

			// Search string
			string pattern = "playAllButton.*?\"commandMetadata\":\\{\"webCommandMetadata\":\\{\"url\":\"(?<URL>.*?)\"";
			MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

			if (result.Count > 0)
				chUrl = "http://youtube.com" + result[0].Groups[1].Value.Replace(@"\u0026", "&");
			else
				chUrl = string.Empty;

			return await new PlaylistItemsSearch().GetPlaylistItems(chUrl, ChannelItems);
		}
	}
}
