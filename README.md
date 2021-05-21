# YouTubeApiSharp

<a href="https://github.com/thisistorsten/YouTubeApiSharp/blob/main/etc/bitcoin.txt" target="_blank">
<img src="https://github.com/thisistorsten/YouTubeApiSharp/blob/main/img/bitcoin-donate-black.png" alt="Bitcoin" title="Donate Bitcon" border="0" />
</a>

## Overview
A complete Private YouTube API for .NET (C#, VB.NET).

## Target platforms

- .NET Standard 2.0
- WinRT
- Windows
- Linux
- macOS
- Windows Phone
- Xamarin.Android
- Xamarin.iOS

## License

The YouTubeApiSharp code is licensed under the [MIT License](http://opensource.org/licenses/MIT).

## Example code

**Get the download URLs**

```c#

// Our test youtube link
string link = "insert youtube link";

/*
 * Get the available video formats.
 * We'll work with them in the video and audio download examples.
 */
IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);

```

**Download the video**

```c#

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

```

## Community

<a href="https://t.me/YouTubeApiSharp" target="_blank">
<img src="https://github.com/thisistorsten/YouTubeApiShar/blob/main/mg/telegram.png" alt="Telegram" title="Telegram Chat" border="0" />
</a>
