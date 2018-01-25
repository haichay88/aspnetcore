using System;

namespace tube.Entities
{
    public class Apikey: EntitiesBase
    {
       
        public string KeyAPI { get; set; }
        public bool IsOverLimit { get; set; }
        public string Website { get; set; }
        public string AccountResgister { get; set; }
      
    }
}
