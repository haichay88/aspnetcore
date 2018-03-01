using System;
using System.Collections.Generic;
using System.Text;

namespace tube.Model
{
    public class VideoDTO
    {
        public string imgUrl { get; set; }
        public string imgUrlchanel { get; set; }
        public string title { get; set; }
        public string chanelTitle { get; set; }
        public string chanelId { get; set; }

        public string videoId { get; set; }
        public string description { get; set; }
        public string duration { get; set; }
        public long? height { get; set; }
        public long? width { get; set; }
        public ulong? viewcount { get; set; }
        public string dislikecount { get; set; }
        public string likecount { get; set; }
        public bool isLive { get; set; }
        public IList<string> tags { get; set; }

        public DateTime? publishDated { get; set; }
        public string publishDateView
        {
            get
            {
                return this.publishDated.HasValue ? publishDated.Value.ConvertDateTimeToRealTime() : string.Empty;
            }
        }
        public string viewcountView
        {
            get
            {
                return string.Format("{0:n0}", this.viewcount);
            }
        }
        public string videoUrl { get; set; }
        public string durationView
        {
            get
            {
                return !string.IsNullOrEmpty(duration) ? System.Xml.XmlConvert.ToTimeSpan(duration).ToString() : string.Empty;
            }
        }
        public string tag
        {
            get
            {
                if (this.tags == null) return string.Empty;
                string result = string.Empty;
                foreach (var item in this.tags)
                {
                    result = string.IsNullOrEmpty(result) ? item : result + "," + item;
                }
                return result;
            }
        }
    }
    public class ViewDetailVideoDTO
    {
        public VideoDTO Video { get; set; }
        public ChannelDTO channel { get; set; }
        public List<VideoDTO> VideoRelateds { get; set; }
        public bool IsValid { get; set; }
    }

    public class ViewHomeVideoDTO
    {

        public List<VideoDTO> VideoTrendings { get; set; }
        public List<VideoDTO> VideoMusics { get; set; }
        public List<VideoDTO> VideoComedys { get; set; }
        public List<VideoDTO> VideoShows { get; set; }
        public List<VideoDTO> VideoSports { get; set; }
        public List<ChannelTrendingDTO> ChannelHots { get; set; }
    }

    public class VideoHistoryDTO
    {
        public string imgUrl { get; set; }
        public string imgUrlchanel { get; set; }
        public string title { get; set; }
        public string chanelTitle { get; set; }
        public string chanelId { get; set; }
        public string videoId { get; set; }

        public string duration { get; set; }
        public DateTime? publishDated { get; set; }
        public string publishDateView
        {
            get
            {
                return this.publishDated.HasValue ? publishDated.Value.ConvertDateTimeToRealTime() : string.Empty;
            }
        }

        public string durationView
        {
            get
            {
                return !string.IsNullOrEmpty(duration) ? System.Xml.XmlConvert.ToTimeSpan(duration).ToString() : string.Empty;
            }
        }
        public string viewcountView { get; set; }
    }
}
