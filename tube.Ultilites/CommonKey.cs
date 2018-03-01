using System;
using System.Configuration;

namespace tube.Ultilites
{
    public class CommonKey
    {
        public static string KeyActive { get; set; }
        public static string DOMAIN
        {
            get
            {
                return ConfigurationManager.AppSettings["DOMAIN"];
            }
        }

    }
}
