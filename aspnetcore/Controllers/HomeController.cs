using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aspnetcore.Models;
using tube.Services;
using tube.Model;
using aspnetcore.Common;

namespace aspnetcore.Controllers
{
    public class HomeController : Controller
    {
        #region Contructor
        public HomeController()
        {
            _Service = new VideoService();
        }
        #endregion
        #region Properties
        private VideoService _Service { get; set; }
        private static readonly log4net.ILog log =
          log4net.LogManager.GetLogger(
                   System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion
        [TubeActionFilter]
        public IActionResult Index()
        {
            ViewHomeVideoDTO model = new ViewHomeVideoDTO();
            model.VideoTrendings = _Service.GetTrendingVideo();

            if (model.VideoTrendings != null)
            {
                addMetaTag(model.VideoTrendings[0]);
            }
            RequestBase request = new RequestBase()
            {
                MaxResults = 12
            };
            // lay video theo chu de music
            request.CategoryId = "10";
            model.VideoMusics = _Service.GetVideoByCate(request);


            // lay video theo chu de sports
            request.CategoryId = "17";
            model.VideoSports = _Service.GetVideoByCate(request);


            // lay video theo chu de comedy
            request.CategoryId = "20";
            model.VideoComedys = _Service.GetVideoByCate(request);



            // lay video theo chu de VideoShows
            request.CategoryId = "2";
            model.VideoShows = _Service.GetVideoByCate(request);

            model.ChannelHots = _Service.ChannelBy();

            return View(model);

        }

        private void addMetaTag(VideoDTO model)
        {
            if (model != null)
            {
                ViewBag.Keywords = model.tag;
                ViewBag.CreatedDate = model.publishDated;
                ViewBag.Description = model.tag;
                ViewBag.Image = model.imgUrl;
                ViewBag.Title = model.title;
                //ViewBag.culture = LanguageMang.LangIsSeleted().LanguageCultureName;
                //ViewBag.Url = "https://" + CommonKey.DOMAIN + Url.RouteUrl("chitietvideo", new { text = MyFinance.Utils.CommonUtil.FriendlyURL(model.title), id = model.videoId });
            }
            else
            {
                ViewBag.Title = "watch video ";
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
