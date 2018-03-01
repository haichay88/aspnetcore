using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tube.Ultilites;
using static tube.Ultilites.LanguageHelper;

namespace aspnetcore.Controllers
{
    public class BaseController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected override IAsyncResult OnActionExecuting(AsyncCallback callback, object state)
        {
          //  _httpContextAccessor.HttpContext.Request.Cookies
            string lang = null;
            HttpCookie langCookie = Request.Cookies["culture"];
            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                var userLanguage = Request.UserLanguages;
                var userLang = userLanguage != null ? userLanguage[0] : "";
                if (userLang != "")
                {
                    lang = userLang;
                 
                }
                else
                {
                    lang = LanguageMang.GetDefaultLanguage();
                }
            }
            new LanguageMang().SetLanguage(lang);

            HttpCookie regionCookie = Request.Cookies[RegionManager.RegionKey];
            if (regionCookie == null)
            {
                string regioncode = RegionManager.GetRegionCode(lang);
                RegionManager.SetRegion(regioncode);
            }

            /// init key api default
            /// 
            if (CommonKey.IS_PRODUCTION)
            {
                IoC.Get<IAPIKeyBusiness>().GetDefaultKey();

            }
            else
            {
                CommonKey.KeyActive = "AIzaSyAfoDjvz2PdCLBNjR2Fg0QzhbSFaxU6Uq0";
            }
           
            
            return base.BeginExecuteCore(callback, state);
        }
    }
}