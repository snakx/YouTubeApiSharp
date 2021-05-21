using System;

namespace YouTubeApiSharp
{
    public class ChannelSearchComponents
    {
        private String Id;
        private String Title;
        private String Description;
        private String VideoCount;
        private String SubscriberCount;
        private String Thumbnail;
        private String Url;

        public ChannelSearchComponents(String Id, String Title, String Description, String VideoCount, String SubscriberCount, String Url, String Thumbnail)
        {
            this.setId(Id);
            this.setTitle(Title);
            this.setDescription(Description);
            this.setVideoCount(VideoCount);
            this.setSubscriberCount(SubscriberCount);
            this.setUrl(Url);
            this.setThumbnail(Thumbnail);
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

        public String getDescription()
        {
            return Description;
        }

        public void setDescription(String description)
        {
            Description = description;
        }

        public String getVideoCount()
        {
            return VideoCount;
        }

        public void setVideoCount(String videocount)
        {
            VideoCount = videocount;
        }

        public String getSubscriberCount()
        {
            return SubscriberCount;
        }

        public void setSubscriberCount(String subscribercount)
        {
            SubscriberCount = subscribercount;
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
