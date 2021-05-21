using System;

namespace YouTubeApiSharp
{
	public class PlaylistItemsSearchComponents
	{
        private String Title;
        private String Author;
        private String Duration;
        private String Url;
        private String Thumbnail;

        public PlaylistItemsSearchComponents(String Title, String Author, String Duration, String Url, String Thumbnail)
        {
            this.setTitle(Title);
            this.setAuthor(Author);
            this.setDuration(Duration);
            this.setUrl(Url);
            this.setThumbnail(Thumbnail);
        }

        public String getTitle()
        {
            return Title;
        }

        public void setTitle(String title)
        {
            Title = title;
        }

        public String getAuthor()
        {
            return Author;
        }

        public void setAuthor(String author)
        {
            Author = author;
        }

        public String getDuration()
        {
            return Duration;
        }

        public void setDuration(String duration)
        {
            Duration = duration;
        }

        public String getUrl()
        {
            return Url;
        }

        public void setUrl(String url)
        {
            Url = url;
        }

        public String getThumbnail()
        {
            return Thumbnail;
        }

        public void setThumbnail(String thumbnail)
        {
            Thumbnail = thumbnail;
        }
    }
}
