using System;
using System.Collections.Generic;
using System.Text;

namespace tube.Model
{
    public class RequestBase
    {
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public string topicId { get; set; }
        public string channelId { get; set; }
        public string keyword { get; set; }
        public bool IsLive { get; set; }
        public string RegionCode { get; set; }
        public int MaxResults { get; set; }
    }
}
