using System;

namespace YouTubeApiSharp
{
	public class PlaylistSearchComponents
	{
        private String Id;
        private String Title;
        private String Author;
        private String VideoCount;
        private String Thumbnail;
        private String Url;

        public PlaylistSearchComponents(String Id, String Title, String Author, String VideoCount, String Thumbnail, String Url)
        {
            this.setId(Id);
            this.setTitle(Title);
            this.setAuthor(Author);
            this.setVideoCount(VideoCount);
            this.setThumbnail(Thumbnail);
            this.setUrl(Url);
        }

        public String getId()
        {
            return Id;
        }

        public void setId(String id)
        {
            Id = id;
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

        public String getVideoCount()
        {
            return VideoCount;
        }

        public void setVideoCount(String videocount)
        {
            VideoCount = videocount;
        }

        public String getThumbnail()
        {
            return Thumbnail;
        }

        public void setThumbnail(String thumbnail)
        {
            Thumbnail = thumbnail;
        }

        public String getUrl()
        {
            return Url;
        }

        public void setUrl(String url)
        {
            Url = url;
        }
    }
}
