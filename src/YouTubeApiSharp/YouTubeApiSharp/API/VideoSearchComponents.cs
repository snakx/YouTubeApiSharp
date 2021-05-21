using System;

namespace YouTubeApiSharp
{
    public class VideoSearchComponents
    {
        private String Title;
        private String Author;
        private String Description;
        private String Duration;
        private String Url;
        private String Thumbnail;
        private String ViewCount;

        public VideoSearchComponents(String Title, String Author, String Description, String Duration, String Url, String Thumbnail, String ViewCount)
        {
            this.setTitle(Title);
            this.setAuthor(Author);
            this.setDescription(Description);
            this.setDuration(Duration);
            this.setUrl(Url);
            this.setThumbnail(Thumbnail);
            this.setViewCount(ViewCount);
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

        public String getDescription()
        {
            return Description;
        }

        public void setDescription(String description)
        {
            Description = description;
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

        public String getViewCount()
        {
            return ViewCount;
        }

        public void setViewCount(String viewcount)
        {
            ViewCount = viewcount;
        }
    }
}
