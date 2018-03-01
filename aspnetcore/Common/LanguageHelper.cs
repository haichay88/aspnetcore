using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using tube.Model;

namespace aspnetcore.Common
{
    public class LanguageHelper
    {
        

        public class LanguageMang
        {
            public const string LangKey = "culture";

            public static List<Languages> AvailableLanguages = new List<Languages> {
            new Languages {
                LanguageFullName = "English", LanguageCultureName = "en-GB",hl="en"
            },
             new Languages {
                LanguageFullName = "English - United States", LanguageCultureName = "en-US",hl="en"
            },
             new Languages {
                LanguageFullName = "Australia", LanguageCultureName = "en-AU",hl="en"
            },
             new Languages {
                LanguageFullName = "Deutsche", LanguageCultureName = "de-DE",hl="de"
            },
            new Languages {
                LanguageFullName = "Tiếng Việt", LanguageCultureName = "vi-VN",hl="vi"
            },
            new Languages {
                LanguageFullName = "Denmark", LanguageCultureName = "da-DK",hl="en"
            },

            new Languages {
                LanguageFullName = "ประเทศไทย", LanguageCultureName = "th-TH",hl="th"
            },
              new Languages {
                LanguageFullName = "한국어", LanguageCultureName = "ko-KR",hl="ko"
            },
             new Languages {
                LanguageFullName = "日本", LanguageCultureName = "ja-JP",hl="ja"
            },
        };
            public static bool IsLanguageAvailable(string lang)
            {
                return AvailableLanguages.Where(a => a.LanguageCultureName.Equals(lang)).FirstOrDefault() != null ? true : false;
            }
            public static string GetDefaultLanguage()
            {
                return AvailableLanguages[0].LanguageCultureName;
            }
            public void SetLanguage(string lang)
            {
                try
                {
                    if (!IsLanguageAvailable(lang)) lang = GetDefaultLanguage();
                    var cultureInfo = new CultureInfo(lang);

                    Thread.CurrentThread.CurrentUICulture = cultureInfo;
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                    CookieManager.Set(LangKey, lang,60*24*30);
                }
                catch (Exception) { }
            }

            public static Languages LangIsSeleted()
            {
                string lang = string.Empty;
                var co = CookieManager.Get(LangKey);
                if (co != null)
                {
                    lang = string.IsNullOrWhiteSpace(co) ? GetDefaultLanguage() : co;
                }
                else
                    lang = GetDefaultLanguage();

                var langSelected = AvailableLanguages.Where(a => a.LanguageCultureName == lang).FirstOrDefault();
                return langSelected == null ? AvailableLanguages.FirstOrDefault() : langSelected;
            }
        }


        public class RegionManager
        {
            public const string RegionKey = "region";

            public static string RegionCode
            {
                get
                {
                    var co = CookieManager.Get(RegionKey) ;
                    if (co != null)
                    {
                        return string.IsNullOrWhiteSpace(co) ? "gb" : co;
                    }
                    else
                        return "gb";
                }
            }

            public static void SetRegion(string regionCode)
            {
                try
                {
                    CookieManager.Set(RegionKey, regionCode, 60 * 24 * 30);

                }
                catch (Exception) { }
            }

            public static string GetRegionCode(string lang)
            {
                if (!LanguageMang.IsLanguageAvailable(lang)) lang = LanguageMang.GetDefaultLanguage();

                var cultureInfo = new CultureInfo(lang);
                if (cultureInfo.IsNeutralCulture)
                {

                    return cultureInfo.TwoLetterISOLanguageName;
                }
                else
                {
                    var re = new RegionInfo(cultureInfo.Name);
                    return re.TwoLetterISORegionName;
                }
            }



        }

        public static class CookieManager
        {
            private static readonly IHttpContextAccessor _httpContextAccessor;

            /// <summary>  
            /// Get the cookie  
            /// </summary>  
            /// <param name="key">Key </param>  
            /// <returns>string value</returns>  
            public static string Get(string key)
            {
                return _httpContextAccessor.HttpContext.Request.Cookies["Key"];
            }
            /// <summary>  
            /// set the cookie  
            /// </summary>  
            /// <param name="key">key (unique indentifier)</param>  
            /// <param name="value">value to store in cookie object</param>  
            /// <param name="expireTime">expiration time</param>  
            public static void Set(string key, string value, int? expireTime)
            {
                CookieOptions option = new CookieOptions();
                if (expireTime.HasValue)
                    option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
                else
                    option.Expires = DateTime.Now.AddMilliseconds(10);
                _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
            }
            /// <summary>  
            /// Delete the key  
            /// </summary>  
            /// <param name="key">Key</param>  
            public static void Remove(string key)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
            }
        }
    }
}
