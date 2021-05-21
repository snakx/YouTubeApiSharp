using System;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace YouTubeApiSharp
{
	public class VideoDownloader
	{
        internal string Folder;

        /// <summary>
        /// Download video file
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="Title"></param>
        /// <param name="isVideo"></param>
        /// <param name="Folder"></param>
        /// <param name="file_extension"></param>
        public void DownloadFile(string URL, string Title, bool isVideo, string Folder, string file_extension)
        {
            WebClient DownloadFile = new WebClient();

            DownloadFile.DownloadProgressChanged += (sender, e) => DownloadFileProgressChanged(sender, e, DownloadFile);

            // Event when download completed
            DownloadFile.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompleted);

            // Start download
            this.Folder = Folder;
            var directory = Folder;

            // Check if directory exists
            try
            {
                if (!System.IO.Directory.Exists(directory))
                    System.IO.Directory.CreateDirectory(directory);
            }
            catch { }

            string file = string.Empty;
            if (isVideo)
            {
                file = Path.Combine(directory, Helper.makeFilenameValid(Title).Replace("/", "")
                    .Replace(".", "")
                    .Replace("|", "")
                    .Replace("?", "")
                    .Replace("!", "") + file_extension);
            }
            else
            {
                file = Path.Combine(directory, Helper.makeFilenameValid(Title).Replace("/", "")
                    .Replace(".", "")
                    .Replace("|", "")
                    .Replace("?", "")
                    .Replace("!", "") + ".m4a");
            }

#if DEBUG
            Console.WriteLine(file);
#endif

            if (Log.getMode())
                Log.println(Helper.Folder, file);

            DownloadFile.DownloadFileAsync(new Uri(URL), file, file + "|" + Title);

#if DEBUG
            Console.WriteLine("Download started");
#endif

            if (Log.getMode())
                Log.println(Helper.Folder, "Download started");
        }

        /// <summary>
        /// Download progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="webClient"></param>
        private void DownloadFileProgressChanged(object sender, DownloadProgressChangedEventArgs e, WebClient webClient)
        {
            // Progress
            string s = e.UserState as String;
            string[] s_ = s.Split(new char[] { '|' });

            long ProgressPercentage = 0;

            try
            {
                var contentLength = webClient.ResponseHeaders.Get("Content-Length");
                var totalBytesToReceive = Convert.ToInt64(contentLength);

                ProgressPercentage = 100 * e.BytesReceived / totalBytesToReceive;
            }
            catch
            {
                ProgressPercentage = 0;
            }

#if DEBUG
            Console.WriteLine(ProgressPercentage + " %");
#endif

            if (Log.getMode())
                Log.println(Helper.Folder, ProgressPercentage + " %");
        }

        /// <summary>
        /// Download file completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // Split arguments
            string s = e.UserState as String;

            string[] s_ = s.Split(new char[] { '|' });

            // Directory
            var directory = this.Folder;

            // File
            string mediaFile = s_[0];

            // Filename
            var filename = mediaFile.Replace(Convert.ToString(directory), "").Replace("/", "");

            if (filename.EndsWith(".m4a"))
            {
                // Write m4a tags
                try
                {
                    string performer = "";
                    string title = "";

                    if (filename.Contains("-"))
                    {
                        string[] split = filename.Split(new char[] { '-' });

                        performer = split[0].TrimStart().TrimEnd();

                        foreach (var s__ in split)
                        {
                            if (!s__.Contains(split[0]))
                                title += s__;
                        }

                        title = title.TrimStart().TrimEnd().Replace(".m4a", "").Replace(".mp3", "").Replace(".mp4", "").Replace(".m4u", "");
                    }
                    else
                    {
                        if (filename.Contains(" "))
                        {
                            string[] split = filename.Split(new char[] { ' ' });

                            performer = split[0].TrimStart().TrimEnd();

                            foreach (var s__ in split)
                            {
                                if (!s__.Contains(split[0]))
                                    title += s__ + " ";
                            }

                            title = title.TrimStart().TrimEnd().Replace(".m4a", "").Replace(".mp3", "").Replace(".mp4", "").Replace(".m4u", "");
                        }
                        else
                        {
                            performer = filename;
                            title = " ";
                        }
                    }

                    TagLib.File file = TagLib.File.Create(mediaFile);

                    file.Tag.Performers = new string[] { performer };
                    file.Tag.Title = title;

                    file.Save();
                }
                catch (Exception ex)
                {
#if DEBUG
                    Console.WriteLine(ex.ToString());
#endif

                    if (Log.getMode())
                        Log.println(Helper.Folder, ex.ToString());
                }
            }
#if DEBUG
            Console.WriteLine("Download completed: " + Helper.makeFilenameValid(filename));
#endif

            if (Log.getMode())
                Log.println(Helper.Folder, "Download completed: "+ Helper.makeFilenameValid(filename));
        }
    }
}
