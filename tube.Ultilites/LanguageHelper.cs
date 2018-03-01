using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using tube.Model;

namespace tube.Ultilites
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
                    HttpCookie langCookie = new HttpCookie("culture", lang);
                    langCookie.Expires = DateTime.Now.AddYears(1);
                    HttpContext.Current.Response.Cookies.Add(langCookie);
                }
                catch (Exception) { }
            }

            public static Languages LangIsSeleted()
            {
                string lang = string.Empty;
                var co = HttpContext.Current.Request.Cookies[LangKey];
                if (co != null)
                {
                    lang = string.IsNullOrWhiteSpace(co.Value) ? GetDefaultLanguage() : co.Value;
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
                    var co = HttpContext.Current.Request.Cookies[RegionKey];
                    if (co != null)
                    {
                        return string.IsNullOrWhiteSpace(co.Value) ? "gb" : co.Value;
                    }
                    else
                        return "gb";
                }
            }

            public static void SetRegion(string regionCode)
            {
                try
                {

                    HttpCookie langCookie = new HttpCookie(RegionKey, regionCode);
                    langCookie.Expires = DateTime.Now.AddYears(1);
                    HttpContext.Current.Response.Cookies.Add(langCookie);


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
    }
}
