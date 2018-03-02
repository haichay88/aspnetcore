using System;
using System.Collections.Generic;
using System.Configuration;
using tube.Model;

namespace tube.Ultilites
{
    public class CommonKey
    {
        public const string KeylocalFile = "key.json";
        public static string KeyActive { get; set; }
        public static List<APIFile> Keys { get; set; }
        public static string DOMAIN
        {
            get
            {
                return ConfigurationManager.AppSettings["DOMAIN"];
            }
        }

    }
}
