using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using tube.Business;
using tube.Extention;
using tube.Model;
using tube.Ultilites;
using static tube.Ultilites.LanguageHelper;

namespace tube.Services
{
    public class VideoService
    {

        #region Contructor
        public VideoService()
        {
            InitService();
            _keyservice = new APIFileBusiness();
        }

        private void InitService()
        {


            _Service = new YouTubeService(new BaseClientService.Initializer()
            {

                ApiKey = CommonKey.KeyActive,

                ApplicationName = "tube"
            });
           
        }
        #endregion
        #region Properties
        private string key { get; set; }
        private YouTubeService _Service { get; set; }
        private IAPIKeyBusiness _keyservice { get; set; }
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(
                    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion
        #region Methods
        public List<VideoDTO> SearchVideo(RequestBase request)
        {
            try
            {
                var searchListRequest = _Service.Search.List("snippet");

                searchListRequest.RegionCode = request.RegionCode; // Replace with your search term.

                searchListRequest.Q = request.keyword;

                searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;
                searchListRequest.MaxResults = 50;
                searchListRequest.Type = "video";
                // Call the search.list method to retrieve results matching the specified query term.
                var searchListResponse = searchListRequest.Execute();

                List<VideoDTO> videos = new List<VideoDTO>();

                // Add each result to the appropriate list, and then display the lists of
                // matching videos, channels, and playlists.
                foreach (var searchResult in searchListResponse.Items)
                {
                    var row = new VideoDTO()
                    {
                        title = searchResult.Snippet.Title,
                        videoId = searchResult.Id.VideoId,
                        chanelTitle = searchResult.Snippet.ChannelTitle,
                        imgUrl = searchResult.Snippet.Thumbnails.Medium.Url,
                        chanelId = searchResult.Snippet.ChannelId,
                        publishDated = searchResult.Snippet.PublishedAt,

                    };
                    videos.Add(row);
                }
                return videos;
            }
            catch (Google.GoogleApiException ex)
            {
                log.Error("SearchVideo ex: " + ex);
                if (ex.HttpStatusCode != System.Net.HttpStatusCode.BadRequest)
                {
                    _keyservice.SetOverLimit();
                    _keyservice.GetDefaultKey();
                    InitService();
                    var result = SearchVideo(request);
                    return result;
                }
                return null;

            }


        }

        public List<VideoDTO> LiveVideo(RequestBase request)
        {
            try
            {
                var searchListRequest = _Service.PlaylistItems.List("snippet,ContentDetails");

                searchListRequest.PlaylistId = "PLU12uITxBEPGILPLxvkCc4L_iL7aHf4J2"; // Replace with your search term.

                searchListRequest.MaxResults = 50;

                // Call the search.list method to retrieve results matching the specified query term.
                var searchListResponse = searchListRequest.Execute();

                List<VideoDTO> videos = new List<VideoDTO>();

                if (searchListResponse.Items == null || searchListResponse.Items.Count <= 0) return null;

                // Add each result to the appropriate list, and then display the lists of
                // matching videos, channels, and playlists.

                foreach (var searchResult in searchListResponse.Items)
                {
                    var row = new VideoDTO()
                    {
                        title = searchResult.Snippet.Title,
                        videoId = searchResult.ContentDetails.VideoId,
                        chanelTitle = searchResult.Snippet.ChannelTitle,
                        imgUrl = searchResult.Snippet.Thumbnails.Medium.Url,
                        chanelId = searchResult.Snippet.ChannelId,
                        isLive = true
                        // publishDated = searchResult.ContentDetails.Start,

                    };
                    videos.Add(row);
                }
                return videos;
            }
            catch (Google.GoogleApiException ex)
            {
                log.Error("LiveVideo ex: " + ex);
                if (ex.HttpStatusCode != System.Net.HttpStatusCode.BadRequest)
                {
                    _keyservice.SetOverLimit();
                    _keyservice.GetDefaultKey();

                    InitService();
                    var result = LiveVideo(request);
                    return result;
                }
                else
                {
                    return null;
                }

            }


        }

        public List<VideoDTO> GetTrendingVideo(RequestBase request)
        {
            try
            {

                var searchListRequest = _Service.Videos.List("snippet,contentDetails,statistics");

                searchListRequest.RegionCode = request.RegionCode; // Replace with your search term.
                searchListRequest.Chart = VideosResource.ListRequest.ChartEnum.MostPopular;

                searchListRequest.MaxResults = 24;

                // Call the search.list method to retrieve results matching the specified query term.
                var searchListResponse = searchListRequest.Execute();

                List<VideoDTO> videos = new List<VideoDTO>();

                // Add each result to the appropriate list, and then display the lists of
                // matching videos, channels, and playlists.
                if (searchListResponse.Items == null || searchListResponse.Items.Count <= 0) return null;

                foreach (var searchResult in searchListResponse.Items)
                {
                    var row = new VideoDTO()
                    {
                        title = searchResult.Snippet.Title,
                        videoId = searchResult.Id,
                        chanelTitle = searchResult.Snippet.ChannelTitle,
                        imgUrl = searchResult.Snippet.Thumbnails.Medium.Url,
                        chanelId = searchResult.Snippet.ChannelId,
                        tags = searchResult.Snippet.Tags,
                        publishDated = searchResult.Snippet.PublishedAt,
                        duration = searchResult.ContentDetails.Duration,
                        viewcount = searchResult.Statistics.ViewCount
                    };
                    videos.Add(row);
                }
                return videos;
            }
            catch (Google.GoogleApiException ex)
            {
                log.Error("GetTrendingVideo ex: " + ex);
                log.Info("RegionCode: " + request.RegionCode);

                if (ex.HttpStatusCode != System.Net.HttpStatusCode.BadRequest)
                {

                    _keyservice.SetOverLimit();
                    _keyservice.GetDefaultKey();
                    InitService();
                    return GetTrendingVideo(request);
                }
                else
                {
                    return null;
                }


            }

        }

        public List<VideoDTO> GetVideoByCate(RequestBase request)
        {
            List<VideoDTO> videos = new List<VideoDTO>();
           
            try
            {
                var searchListRequest = _Service.Videos.List("snippet,contentDetails,statistics");

                searchListRequest.RegionCode = request.RegionCode; // Replace with your search term.
                searchListRequest.Chart = VideosResource.ListRequest.ChartEnum.MostPopular;
                searchListRequest.Hl = request.hl;
                searchListRequest.VideoCategoryId = request.CategoryId;
                searchListRequest.MaxResults = request.MaxResults;


                // Call the search.list method to retrieve results matching the specified query term.
                var searchListResponse = searchListRequest.Execute();

                foreach (var searchResult in searchListResponse.Items)
                {
                    var row = new VideoDTO()
                    {
                        title = searchResult.Snippet.Title,
                        videoId = searchResult.Id,
                        chanelTitle = searchResult.Snippet.ChannelTitle,
                        imgUrl = searchResult.Snippet.Thumbnails.Medium.Url,
                        chanelId = searchResult.Snippet.ChannelId,
                        tags = searchResult.Snippet.Tags,
                        publishDated = searchResult.Snippet.PublishedAt,
                        duration = searchResult.ContentDetails.Duration,
                        viewcount = searchResult.Statistics.ViewCount

                    };
                    videos.Add(row);
                }
                return videos;
            }
            catch (Google.GoogleApiException ex)
            {

                log.Error("GetVideoByCate ex: " + ex);
                log.Info("key use: " + CommonKey.KeyActive);
                log.Info("hl: " + request.hl);
                if (ex.HttpStatusCode != System.Net.HttpStatusCode.BadRequest)
                {

                    _keyservice.SetOverLimit();
                    _keyservice.GetDefaultKey();

                    InitService();
                    var result = GetVideoByCate(request);
                    return result;
                }
                else
                {
                    return null;
                }
            }

        }

        public List<VideoDTO> GetVideoByChannel(RequestBase request)
        {
            List<VideoDTO> videos = new List<VideoDTO>();

            var searchListRequest = _Service.Search.List("snippet");

            searchListRequest.RegionCode = request.RegionCode; // Replace with your search term.
            searchListRequest.ChannelId = request.channelId;
            searchListRequest.MaxResults = request.MaxResults;

            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.VideoCount;
            searchListRequest.Type = "video";

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = searchListRequest.Execute();

            foreach (var searchResult in searchListResponse.Items)
            {
                var row = new VideoDTO()
                {
                    title = searchResult.Snippet.Title,
                    videoId = searchResult.Id.VideoId,
                    chanelTitle = searchResult.Snippet.ChannelTitle,
                    imgUrl = searchResult.Snippet.Thumbnails.Medium.Url,
                    chanelId = searchResult.Snippet.ChannelId,

                    publishDated = searchResult.Snippet.PublishedAt,

                };
                videos.Add(row);
            }
            return videos;
        }

        public List<ChannelTrendingDTO> ChannelBy(RequestBase request)
        {
            List<ChannelTrendingDTO> data = new List<ChannelTrendingDTO>();
            try
            {
                var searchListRequest = _Service.Videos.List("snippet");
               
                searchListRequest.RegionCode = request.RegionCode; // Replace with your search term.
                searchListRequest.Chart = VideosResource.ListRequest.ChartEnum.MostPopular;
                searchListRequest.MaxResults = 4;
                searchListRequest.Hl = request.hl;


                // Call the search.list method to retrieve results matching the specified query term.
                var searchListResponse = searchListRequest.Execute();

                foreach (var searchResult in searchListResponse.Items)
                {
                    var row = new ChannelTrendingDTO()
                    {
                        Channel = new ChannelDTO()
                        {
                            title = searchResult.Snippet.ChannelTitle,
                            channelId = searchResult.Snippet.ChannelId
                        },
                        Videos = GetVideoByChannel(new RequestBase() { channelId = searchResult.Snippet.ChannelId, MaxResults = 12 })
                    };
                    data.Add(row);
                }
                return data;
            }
            catch (Google.GoogleApiException ex)
            {
                log.Error("ChannelBy" + ex);
                return null;
            }


        }


        public ViewDetailVideoDTO GetDetailVideo(RequestBase request)
        {
            try
            {
                ///check valid video
                //var isvalid = IoC.Get<IAPIKeyBusiness>().CheckVaildVideo(request.Id);

                //if (!isvalid) return null;


                var searchListRequest = _Service.Videos.List("snippet,ContentDetails,Statistics");
                searchListRequest.Id = request.Id;

                var searchListResponse = searchListRequest.Execute();
                if (searchListResponse.Items == null || searchListResponse.Items.Count <= 0)
                {

                    return null;
                }

                var row = searchListResponse.Items[0];
                var video = new VideoDTO()
                {
                    videoId = request.Id,
                    title = row.Snippet.Title,
                    imgUrl = row.Snippet.Thumbnails.High.Url,
                    chanelId = row.Snippet.ChannelId,
                    chanelTitle = row.Snippet.ChannelTitle,
                    description = row.Snippet.Description,
                    tags = row.Snippet.Tags,
                    publishDated = row.Snippet.PublishedAt,
                    duration = row.ContentDetails.Duration,
                    viewcount = row.Statistics.ViewCount,
                    dislikecount = string.Format("{0:n0}", row.Statistics.DislikeCount),
                    likecount = string.Format("{0:n0}", row.Statistics.LikeCount),
                    height = row.Snippet.Thumbnails.High.Height,
                    width = row.Snippet.Thumbnails.High.Width,

                };
                ///lay video lien quan
                var related = VideoRelated(request.Id);
                /// lay thong tin kenh 
                var channel = GetChannelInfo(video.chanelId);


                var data = new ViewDetailVideoDTO()
                {
                    Video = video,
                    channel = channel,
                    VideoRelateds = related,
                    //IsValid = isvalid,
                };

                return data;
            }
            catch (Google.GoogleApiException ex)
            {
                log.Error("GetDetailVideo ex: " + ex);
                log.Info("key use: " + CommonKey.KeyActive);

                if (ex.HttpStatusCode != System.Net.HttpStatusCode.BadRequest)
                {

                    _keyservice.SetOverLimit();
                    _keyservice.GetDefaultKey();

                    InitService();
                    var result = GetDetailVideo(request);
                    return result;
                }
                else
                {
                    return null;
                }


            }

        }


        public List<VideoDTO> VideoRelated(string id)
        {
            List<VideoDTO> result = new List<VideoDTO>();
            var searchListRequest = _Service.Search.List("snippet");
            searchListRequest.RelatedToVideoId = id;
            searchListRequest.MaxResults = 30;
            searchListRequest.Type = "video";
            var searchListResponse = searchListRequest.Execute();

            foreach (var searchResult in searchListResponse.Items)
            {
                var row = new VideoDTO()
                {
                    title = searchResult.Snippet.Title,
                    videoId = searchResult.Id.VideoId,
                    chanelTitle = searchResult.Snippet.ChannelTitle,
                    imgUrl = searchResult.Snippet.Thumbnails.Medium.Url,
                    chanelId = searchResult.Snippet.ChannelId,

                };
                result.Add(row);
            }
            return result;
        }
        private ChannelDTO GetChannelInfo(string id)
        {
            var searchListRequest = _Service.Channels.List("snippet");
            searchListRequest.Id = id;

            var searchListResponse = searchListRequest.Execute();
            if (searchListResponse.Items == null || searchListResponse.Items.Count <= 0) return null;
            var row = searchListResponse.Items[0];

            var result = new ChannelDTO()
            {
                imgUrl = row.Snippet.Thumbnails.Medium.Url

            };
            return result;
        }


        #endregion
    }
}
