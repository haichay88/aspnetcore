using System;
using System.Collections.Generic;

namespace tube.Model
{
    public class ChannelDTO
    {
        public ChannelDTO()
        {
            this.Videos = new List<VideoDTO>();
        }
        public string title { get; set; }
        public string channelId { get; set; }
        public string videoCount { get; set; }
        public string viewCount { get; set; }
        public string subscriberCount { get; set; }
        public string description { get; set; }
        public string imgUrl { get; set; }
        public List<VideoDTO> Videos { get; set; }
    }

    public class ChannelTrendingDTO
    {
        public ChannelTrendingDTO()
        {
            this.Videos = new List<VideoDTO>();
        }
        public ChannelDTO Channel { get; set; }

        public List<VideoDTO> Videos { get; set; }
    }
}
